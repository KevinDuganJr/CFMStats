using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using CFMStats.Classes.JSON;

namespace CFMStats.Classes
{
    public class oRosters : Dictionary<int, oPlayer>
    {
        public oRosters GetPlayerRatings(int positionGroupId, int teamId, int developmentId, int playerId, bool is25AndUnder, bool onPracticeSquad, bool isRookie, int leagueId)
        {
            var collection = new oRosters();
            var sp = new StoredProc
            {
                Name = "PlayerRatings_select", DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, ParameterSet = new SqlCommand()
            };

            sp.ParameterSet.Parameters.AddWithValue("@leagueId", leagueId);

            if (positionGroupId == 99)
            {
                sp.ParameterSet.Parameters.AddWithValue("@positionGroupID", DBNull.Value);
            }
            else
            {
                sp.ParameterSet.Parameters.AddWithValue("@positionGroupID", positionGroupId);
            }

            if (teamId == 99)
            {
                sp.ParameterSet.Parameters.AddWithValue("@teamID", DBNull.Value);
            }
            else
            {
                sp.ParameterSet.Parameters.AddWithValue("@teamID", teamId);
            }

            sp.ParameterSet.Parameters.AddWithValue("@developmentID", developmentId);

            if (playerId == 0)
            {
                sp.ParameterSet.Parameters.AddWithValue("@playerId", DBNull.Value);
            }
            else
            {
                sp.ParameterSet.Parameters.AddWithValue("@playerId", playerId);
            }

            sp.ParameterSet.Parameters.AddWithValue("@playerAge", is25AndUnder ? 25 : 99);

            sp.ParameterSet.Parameters.AddWithValue("@yearsPro", isRookie ? 0 : 99);

            sp.ParameterSet.Parameters.AddWithValue("@onPracticeSquad", onPracticeSquad);

            var ds = StoredProc.ShowMeTheData(sp);

            if (ds.Tables.Count == 0)
            {
                return collection;
            }

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                var p = new oPlayer
                {
                    playerId = item.Field<int>("playerid"),
                    teamId = Helper.IntegerNull(item["teamId"]),
                    abbrName = Helper.StringNull(item["abbrName"]),
                    teamName = Helper.StringNull(item["displayName"]),
                    cityName = Helper.StringNull(item["cityName"]),
                    position = item.Field<string>("position"),
                    jerseyNum = item.Field<int>("jerseyNum"),
                    devTrait = item.Field<int>("devTrait"),
                    injuryLength = item.Field<int>("injuryLength"),
                    injuryType = item.Field<int>("injuryType"),
                    age = item.Field<int>("age"),
                    height = item.Field<int>("height"),
                    yearsPro = item.Field<int>("yearsPro"),
                    firstName = item.Field<string>("firstName"),
                    lastName = item.Field<string>("lastName"),
                    playerBestOvr = item.Field<int>("playerBestOvr"),
                    playerSchemeOvr = item.Field<int>("playerSchemeOvr"),
                    teamSchemeOvr = item.Field<int>("teamSchemeOvr"),
                    experiencePoints = item.Field<int>("experiencePoints"),
                    skillPoints = Helper.IntegerNull(item["skillPoints"]),
                    isActive = item.Field<bool>("isActive"),
                    isFreeAgent = item.Field<bool>("isFreeAgent"),
                    isOnIR = item.Field<bool>("isOnIR"),
                    isRetired = item.Field<bool>("isRetired"),
                    isOnPracticeSquad = item.Field<bool>("isOnPracticeSquad"),
                    accelRating = item.Field<int>("accelRating"),
                    agilityRating = item.Field<int>("agilityRating"),
                    awareRating = item.Field<int>("awareRating"),
                    bCVRating = item.Field<int>("bCVRating"),
                    blockShedRating = item.Field<int>("blockShedRating"),
                    carryRating = item.Field<int>("carryRating"),
                    catchRating = item.Field<int>("catchRating"),
                    cITRating = item.Field<int>("cITRating"),
                    confRating = item.Field<int>("confRating"),
                    changeOfDirectionRating = item.Field<int>("changeOfDirectionRating"),
                    finesseMovesRating = item.Field<int>("finesseMovesRating"),
                    hitPowerRating = item.Field<int>("hitPowerRating"),
                    impactBlockRating = item.Field<int>("impactBlockRating"),
                    injuryRating = item.Field<int>("injuryRating"),
                    jukeMoveRating = item.Field<int>("jukeMoveRating"),
                    jumpRating = item.Field<int>("jumpRating"),
                    kickAccRating = item.Field<int>("kickAccRating"),
                    kickPowerRating = item.Field<int>("kickPowerRating"),
                    kickRetRating = item.Field<int>("kickRetRating"),
                    manCoverRating = item.Field<int>("manCoverRating"),
                    passBlockRating = item.Field<int>("passBlockRating"),
                    playActionRating = item.Field<int>("playActionRating"),
                    playRecRating = item.Field<int>("playRecRating"),
                    powerMovesRating = item.Field<int>("powerMovesRating"),
                    pressRating = item.Field<int>("pressRating"),
                    pursuitRating = item.Field<int>("pursuitRating"),
                    releaseRating = item.Field<int>("releaseRating"),
                    runBlockRating = item.Field<int>("runBlockRating"),
                    specCatchRating = item.Field<int>("specCatchRating"),
                    speedRating = item.Field<int>("speedRating"),
                    spinMoveRating = item.Field<int>("spinMoveRating"),
                    staminaRating = item.Field<int>("staminaRating"),
                    stiffArmRating = item.Field<int>("stiffArmRating"),
                    strengthRating = item.Field<int>("strengthRating"),
                    tackleRating = item.Field<int>("tackleRating"),
                    throwAccDeepRating = item.Field<int>("throwAccDeepRating"),
                    throwAccMidRating = item.Field<int>("throwAccMidRating"),
                    throwAccRating = item.Field<int>("throwAccRating"),
                    throwAccShortRating = item.Field<int>("throwAccShortRating"),
                    throwOnRunRating = item.Field<int>("throwOnRunRating"),
                    throwPowerRating = item.Field<int>("throwPowerRating"),
                    toughRating = item.Field<int>("toughRating"),
                    truckRating = item.Field<int>("truckRating"),
                    zoneCoverRating = item.Field<int>("zoneCoverRating"),
                    breakSackRating = Helper.IntegerNull(item["breakSackRating"]),
                    breakTackleRating = Helper.IntegerNull(item["breakTackleRating"]),
                    leadBlockRating = Helper.IntegerNull(item["leadBlockRating"]),
                    passBlockFinesseRating = Helper.IntegerNull(item["passBlockFinesseRating"]),
                    passBlockPowerRating = Helper.IntegerNull(item["passBlockPowerRating"]),
                    routeRunDeepRating = Helper.IntegerNull(item["routeRunDeepRating"]),
                    routeRunMedRating = Helper.IntegerNull(item["routeRunMedRating"]),
                    routeRunShortRating = Helper.IntegerNull(item["routeRunShortRating"]),
                    runBlockFinesseRating = Helper.IntegerNull(item["runBlockFinesseRating"]),
                    runBlockPowerRating = Helper.IntegerNull(item["runBlockPowerRating"]),
                    throwUnderPressureRating = Helper.IntegerNull(item["throwUnderPressureRating"])
                };

                collection.Add(p.playerId, p);
            }

            return collection;
        }

        public oRosters GetDraftHistory(int rookieYear, int teamId, int positionGroupId, int developmentId,  int leagueId)
        {
            var collection = new oRosters();
            var sp = new StoredProc
            {
                Name = "DraftHistory_select", DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, ParameterSet = new SqlCommand()
            };

            if (rookieYear == 99)
            {
                sp.ParameterSet.Parameters.AddWithValue("@rookieYear", DBNull.Value);
            }
            else
            {
                sp.ParameterSet.Parameters.AddWithValue("@rookieYear", rookieYear);
            }

            if (teamId == 99)
            {
                sp.ParameterSet.Parameters.AddWithValue("@teamID", DBNull.Value);
            }
            else
            {
                sp.ParameterSet.Parameters.AddWithValue("@teamID", teamId);
            }

            if (positionGroupId == 99)
            {
                sp.ParameterSet.Parameters.AddWithValue("@positionGroupID", DBNull.Value);
            }
            else
            {
                sp.ParameterSet.Parameters.AddWithValue("@positionGroupID", positionGroupId);
            }
    
            sp.ParameterSet.Parameters.AddWithValue("@developmentID", developmentId);

            sp.ParameterSet.Parameters.AddWithValue("@leagueId", leagueId);


            var ds = StoredProc.ShowMeTheData(sp);

            if (ds.Tables.Count == 0)
            {
                return collection;
            }

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                var p = new oPlayer
                {
                    teamId = Helper.IntegerNull(item["teamId"]),
                    abbrName = Helper.StringNull(item["abbrName"]),
                    teamName = Helper.StringNull(item["displayName"]),
                    cityName = Helper.StringNull(item["cityName"]),

                    position = item.Field<string>("position"),

                    playerId = item.Field<int>("playerid"),
                    firstName = item.Field<string>("firstName"),
                    lastName = item.Field<string>("lastName"),
                    age = item.Field<int>("age"),
                    devTrait = item.Field<int>("devTrait"),
                    yearsPro = item.Field<int>("yearsPro"),

                    rookieYear = item.Field<int>("rookieYear"),
                    draftRound = item.Field<int>("draftRound"),
                    draftPick = item.Field<int>("draftPick"),

                    rookieRating= item.Field<int>("rookieRating"),

                    playerBestOvr = item.Field<int>("playerBestOvr"),
                    playerSchemeOvr = item.Field<int>("playerSchemeOvr"),
                    teamSchemeOvr = item.Field<int>("teamSchemeOvr"),
                    isOnPracticeSquad = item.Field<bool>("isOnPracticeSquad")
                };

                collection.Add(p.playerId, p);
            }

            return collection;
        }

        public oRosters GetPlayerSalaries(int positionGroupID, int teamID, int leagueId)
        {
            var collection = new oRosters();
            var sp = new StoredProc
            {
                Name = "PlayerSalary_select", DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, ParameterSet = new SqlCommand()
            };

            sp.ParameterSet.Parameters.AddWithValue("@leagueId", leagueId);

            if (positionGroupID == 99)
            {
                sp.ParameterSet.Parameters.AddWithValue("@positionGroupID", DBNull.Value);
            }
            else
            {
                sp.ParameterSet.Parameters.AddWithValue("@positionGroupID", positionGroupID);
            }

            if (teamID == 99)
            {
                sp.ParameterSet.Parameters.AddWithValue("@teamID", DBNull.Value);
            }
            else
            {
                sp.ParameterSet.Parameters.AddWithValue("@teamID", teamID);
            }

            var ds = StoredProc.ShowMeTheData(sp);

            if (ds.Tables.Count == 0)
            {
                return collection;
            }

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                var p = new oPlayer
                {
                    playerId = item.Field<int>("playerId"),
                    teamId = Helper.IntegerNull(item["teamId"]),
                    abbrName = Helper.StringNull(item["abbrName"]),
                    teamName = Helper.StringNull(item["displayName"]),
                    cityName = Helper.StringNull(item["cityName"]),
                    capRoom = item.Field<int>("capRoom"),
                    capSpent = item.Field<int>("capSpent"),
                    devTrait = item.Field<int>("devTrait"),
                    age = item.Field<int>("age"),
                    firstName = item.Field<string>("firstName"),
                    lastName = item.Field<string>("lastName"),
                    playerBestOvr = item.Field<int>("playerBestOvr"),
                    playerSchemeOvr = item.Field<int>("playerSchemeOvr"),
                    teamSchemeOvr = item.Field<int>("teamSchemeOvr"),
                    position = item.Field<string>("position"),
                    yearsPro = item.Field<int>("yearsPro"),
                    capHit = item.Field<int>("capHit"),
                    capReleaseNetSavings = item.Field<int>("capReleaseNetSavings"),
                    capReleasePenalty = item.Field<int>("capReleasePenalty"),
                    contractBonus = item.Field<int>("contractBonus"),
                    contractLength = item.Field<int>("contractLength"),
                    contractSalary = item.Field<int>("contractSalary"),
                    contractYearsLeft = item.Field<int>("contractYearsLeft"),
                    desiredBonus = item.Field<int>("desiredBonus"),
                    desiredLength = item.Field<int>("desiredLength"),
                    desiredSalary = item.Field<int>("desiredSalary"),
                    reSignStatus = item.Field<int>("reSignStatus"),
                    skillPoints = item.Field<int>("skillPoints")
                };

                collection.Add(p.playerId, p);
            }

            return collection;
        }

        public oRosters GetPlayerTraits(int playerId)
        {
            var collection = new oRosters();
            var sp = new StoredProc
            {
                Name = "PlayerTraits_select", DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, ParameterSet = new SqlCommand()
            };

            sp.ParameterSet.Parameters.AddWithValue("@playerId", playerId);

            var ds = StoredProc.ShowMeTheData(sp);

            if (ds.Tables.Count == 0)
            {
                return collection;
            }

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                var p = new oPlayer
                {
                    playerId = item.Field<int>("playerid"),
                    bigHitTrait = item.Field<int>("bigHitTrait"),
                    clutchTrait = item.Field<int>("clutchTrait"),
                    coverBallTrait = item.Field<int>("coverBallTrait"),
                    devTrait = item.Field<int>("devTrait"),
                    dLBullRushTrait = item.Field<int>("dLBullRushTrait"),
                    dLSpinTrait = item.Field<int>("dLSpinTrait"),
                    dLSwimTrait = item.Field<int>("dLSwimTrait"),
                    dropOpenPassTrait = item.Field<int>("dropOpenPassTrait"),
                    feetInBoundsTrait = item.Field<int>("feetInBoundsTrait"),
                    fightForYardsTrait = item.Field<int>("fightForYardsTrait"),
                    forcePassTrait = item.Field<int>("forcePassTrait"),
                    highMotorTrait = item.Field<int>("highMotorTrait"),
                    hPCatchTrait = item.Field<int>("hPCatchTrait"),
                    lBStyleTrait = item.Field<int>("lBStyleTrait"),
                    penaltyTrait = item.Field<int>("penaltyTrait"),
                    playBallTrait = item.Field<int>("playBallTrait"),
                    posCatchTrait = item.Field<int>("posCatchTrait"),
                    predictTrait = item.Field<int>("predictTrait"),
                    qBStyleTrait = item.Field<int>("qBStyleTrait"),
                    sensePressureTrait = item.Field<int>("sensePressureTrait"),
                    stripBallTrait = item.Field<int>("stripBallTrait"),
                    throwAwayTrait = item.Field<int>("throwAwayTrait"),
                    tightSpiralTrait = item.Field<int>("tightSpiralTrait"),
                    yACCatchTrait = item.Field<int>("yACCatchTrait")
                };

                collection.Add(p.playerId, p);
            }

            return collection;
        }

        public bool SetToFreeAgentAndRetired(int leagueId)
        {
            var sp = new StoredProc
            {
                Name = "SetPlayersAsRetiredAndFreeAgent", DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, ParameterSet = new SqlCommand()
            };

            sp.ParameterSet.Parameters.AddWithValue("@leagueId", leagueId);

            return StoredProc.NonQuery(sp);
        }

        public void UpdateAbility(int leagueId, JSONRosters.Signatureslotlist ability)
        {
            var sp = new StoredProc
            {
                Name = "Abilities_update", DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, ParameterSet = new SqlCommand()
            };

            sp.ParameterSet.Parameters.AddWithValue("@leagueId", leagueId);
            sp.ParameterSet.Parameters.AddWithValue("@signatureLogoId", ability.signatureAbility.signatureLogoId);
            sp.ParameterSet.Parameters.AddWithValue("@signatureTitle", ability.signatureAbility.signatureTitle);
            sp.ParameterSet.Parameters.AddWithValue("@signatureDescription", ability.signatureAbility.signatureDescription);
            sp.ParameterSet.Parameters.AddWithValue("@signatureActivationDescription", ability.signatureAbility.signatureActivationDescription);
            sp.ParameterSet.Parameters.AddWithValue("@signatureDeactivationDescription", ability.signatureAbility.signatureDeactivationDescription);

            var status = StoredProc.NonQuery(sp);
        }

        public bool UpdatePlayer(int leagueId, JSONRosters.Rosterinfolist i)
        {
            var sp = new StoredProc
            {
                Name = "Player_update", DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, ParameterSet = new SqlCommand()
            };

            sp.ParameterSet.Parameters.AddWithValue("@leagueId", leagueId);

            sp.ParameterSet.Parameters.AddWithValue("accelRating", i.accelRating);
            sp.ParameterSet.Parameters.AddWithValue("age", i.age);
            sp.ParameterSet.Parameters.AddWithValue("agilityRating", i.agilityRating);
            sp.ParameterSet.Parameters.AddWithValue("awareRating", i.awareRating);
            sp.ParameterSet.Parameters.AddWithValue("bCVRating", i.bCVRating);
            sp.ParameterSet.Parameters.AddWithValue("bigHitTrait", i.bigHitTrait);
            sp.ParameterSet.Parameters.AddWithValue("birthDay", i.birthDay);
            sp.ParameterSet.Parameters.AddWithValue("birthMonth", i.birthMonth);
            sp.ParameterSet.Parameters.AddWithValue("birthYear", i.birthYear);
            sp.ParameterSet.Parameters.AddWithValue("blockShedRating", i.blockShedRating);
            sp.ParameterSet.Parameters.AddWithValue("breakSackRating", i.breakSackRating);
            sp.ParameterSet.Parameters.AddWithValue("breakTackleRating", i.breakTackleRating);
            sp.ParameterSet.Parameters.AddWithValue("capHit", i.capHit);
            sp.ParameterSet.Parameters.AddWithValue("capReleaseNetSavings", i.capReleaseNetSavings);
            sp.ParameterSet.Parameters.AddWithValue("capReleasePenalty", i.capReleasePenalty);
            sp.ParameterSet.Parameters.AddWithValue("carryRating", i.carryRating);
            sp.ParameterSet.Parameters.AddWithValue("catchRating", i.catchRating);
            sp.ParameterSet.Parameters.AddWithValue("changeOfDirectionRating ", i.changeOfDirectionRating);
            sp.ParameterSet.Parameters.AddWithValue("cITRating", i.cITRating);
            sp.ParameterSet.Parameters.AddWithValue("clutchTrait", i.clutchTrait);
            sp.ParameterSet.Parameters.AddWithValue("college", i.college);
            sp.ParameterSet.Parameters.AddWithValue("confRating", i.confRating);
            sp.ParameterSet.Parameters.AddWithValue("contractBonus", i.contractBonus);
            sp.ParameterSet.Parameters.AddWithValue("contractLength", i.contractLength);
            sp.ParameterSet.Parameters.AddWithValue("contractSalary", i.contractSalary);
            sp.ParameterSet.Parameters.AddWithValue("contractYearsLeft", i.contractYearsLeft);
            sp.ParameterSet.Parameters.AddWithValue("coverBallTrait", i.coverBallTrait);
            sp.ParameterSet.Parameters.AddWithValue("desiredBonus", i.desiredBonus);
            sp.ParameterSet.Parameters.AddWithValue("desiredLength", i.desiredLength);
            sp.ParameterSet.Parameters.AddWithValue("desiredSalary", i.desiredSalary);
            sp.ParameterSet.Parameters.AddWithValue("devTrait", i.devTrait);
            sp.ParameterSet.Parameters.AddWithValue("dLBullRushTrait", i.dLBullRushTrait);
            sp.ParameterSet.Parameters.AddWithValue("dLSpinTrait", i.dLSpinTrait);
            sp.ParameterSet.Parameters.AddWithValue("dLSwimTrait", i.dLSwimTrait);
            sp.ParameterSet.Parameters.AddWithValue("draftPick", i.draftPick);
            sp.ParameterSet.Parameters.AddWithValue("draftRound", i.draftRound);
            sp.ParameterSet.Parameters.AddWithValue("dropOpenPassTrait", i.dropOpenPassTrait);
            sp.ParameterSet.Parameters.AddWithValue("durabilityGrade", i.durabilityGrade);
            sp.ParameterSet.Parameters.AddWithValue("experiencePoints", i.experiencePoints);
            sp.ParameterSet.Parameters.AddWithValue("feetInBoundsTrait", i.feetInBoundsTrait);
            sp.ParameterSet.Parameters.AddWithValue("fightForYardsTrait", i.fightForYardsTrait);
            sp.ParameterSet.Parameters.AddWithValue("finesseMovesRating", i.finesseMovesRating);
            sp.ParameterSet.Parameters.AddWithValue("firstName", i.firstName);
            sp.ParameterSet.Parameters.AddWithValue("forcePassTrait", i.forcePassTrait);
            sp.ParameterSet.Parameters.AddWithValue("height", i.height);
            sp.ParameterSet.Parameters.AddWithValue("highMotorTrait", i.highMotorTrait);
            sp.ParameterSet.Parameters.AddWithValue("hitPowerRating", i.hitPowerRating);
            sp.ParameterSet.Parameters.AddWithValue("hPCatchTrait", i.hPCatchTrait);
            sp.ParameterSet.Parameters.AddWithValue("impactBlockRating", i.impactBlockRating);
            sp.ParameterSet.Parameters.AddWithValue("injuryLength", i.injuryLength);
            sp.ParameterSet.Parameters.AddWithValue("injuryRating", i.injuryRating);
            sp.ParameterSet.Parameters.AddWithValue("injuryType", i.injuryType);
            sp.ParameterSet.Parameters.AddWithValue("intangibleGrade", i.intangibleGrade);
            sp.ParameterSet.Parameters.AddWithValue("isActive", i.isActive);
            sp.ParameterSet.Parameters.AddWithValue("isFreeAgent", i.isFreeAgent);
            sp.ParameterSet.Parameters.AddWithValue("isOnIR", i.isOnIR);
            sp.ParameterSet.Parameters.AddWithValue("isOnPracticeSquad", i.isOnPracticeSquad);
            sp.ParameterSet.Parameters.AddWithValue("jerseyNum", i.jerseyNum);
            sp.ParameterSet.Parameters.AddWithValue("jukeMoveRating", i.jukeMoveRating);
            sp.ParameterSet.Parameters.AddWithValue("jumpRating", i.jumpRating);
            sp.ParameterSet.Parameters.AddWithValue("kickAccRating", i.kickAccRating);
            sp.ParameterSet.Parameters.AddWithValue("kickPowerRating", i.kickPowerRating);
            sp.ParameterSet.Parameters.AddWithValue("kickRetRating", i.kickRetRating);
            sp.ParameterSet.Parameters.AddWithValue("lastName", i.lastName);
            sp.ParameterSet.Parameters.AddWithValue("lBStyleTrait", i.lBStyleTrait);
            sp.ParameterSet.Parameters.AddWithValue("leadBlockRating", i.leadBlockRating);
            sp.ParameterSet.Parameters.AddWithValue("legacyScore", i.legacyScore);
            sp.ParameterSet.Parameters.AddWithValue("manCoverRating", i.manCoverRating);
            sp.ParameterSet.Parameters.AddWithValue("passBlockFinesseRating", i.passBlockFinesseRating);
            sp.ParameterSet.Parameters.AddWithValue("passBlockPowerRating", i.passBlockPowerRating);
            sp.ParameterSet.Parameters.AddWithValue("passBlockRating", i.passBlockRating);
            sp.ParameterSet.Parameters.AddWithValue("penaltyTrait", i.penaltyTrait);
            sp.ParameterSet.Parameters.AddWithValue("physicalGrade", i.physicalGrade);
            sp.ParameterSet.Parameters.AddWithValue("playActionRating", i.playActionRating);
            sp.ParameterSet.Parameters.AddWithValue("playBallTrait", i.playBallTrait);
            sp.ParameterSet.Parameters.AddWithValue("playerBestOvr", i.playerBestOvr);
            sp.ParameterSet.Parameters.AddWithValue("playerSchemeOvr", i.playerSchemeOvr);
            sp.ParameterSet.Parameters.AddWithValue("playRecRating", i.playRecRating);
            sp.ParameterSet.Parameters.AddWithValue("portraitId", i.portraitId);
            sp.ParameterSet.Parameters.AddWithValue("posCatchTrait", i.posCatchTrait);
            sp.ParameterSet.Parameters.AddWithValue("position", i.position);
            sp.ParameterSet.Parameters.AddWithValue("powerMovesRating", i.powerMovesRating);
            sp.ParameterSet.Parameters.AddWithValue("predictTrait", i.predictTrait);
            sp.ParameterSet.Parameters.AddWithValue("presentationId", i.presentationId);
            sp.ParameterSet.Parameters.AddWithValue("pressRating", i.pressRating);
            sp.ParameterSet.Parameters.AddWithValue("productionGrade", i.productionGrade);
            sp.ParameterSet.Parameters.AddWithValue("pursuitRating", i.pursuitRating);
            sp.ParameterSet.Parameters.AddWithValue("qBStyleTrait", i.qBStyleTrait);
            sp.ParameterSet.Parameters.AddWithValue("releaseRating", i.releaseRating);
            sp.ParameterSet.Parameters.AddWithValue("reSignStatus", i.reSignStatus);
            sp.ParameterSet.Parameters.AddWithValue("rookieYear", i.rookieYear);
            sp.ParameterSet.Parameters.AddWithValue("rosterId", i.rosterId);
            sp.ParameterSet.Parameters.AddWithValue("routeRunDeepRating", i.routeRunDeepRating);
            sp.ParameterSet.Parameters.AddWithValue("routeRunMedRating", i.routeRunMedRating);
            sp.ParameterSet.Parameters.AddWithValue("routeRunShortRating", i.routeRunShortRating);
            sp.ParameterSet.Parameters.AddWithValue("runBlockFinesseRating", i.runBlockFinesseRating);
            sp.ParameterSet.Parameters.AddWithValue("runBlockPowerRating", i.runBlockPowerRating);
            sp.ParameterSet.Parameters.AddWithValue("runBlockRating", i.runBlockRating);
            sp.ParameterSet.Parameters.AddWithValue("runStyle", i.runStyle);
            sp.ParameterSet.Parameters.AddWithValue("scheme", i.scheme);
            sp.ParameterSet.Parameters.AddWithValue("sensePressureTrait", i.sensePressureTrait);
            sp.ParameterSet.Parameters.AddWithValue("sizeGrade", i.sizeGrade);
            sp.ParameterSet.Parameters.AddWithValue("skillPoints", i.skillPoints);
            sp.ParameterSet.Parameters.AddWithValue("specCatchRating", i.specCatchRating);
            sp.ParameterSet.Parameters.AddWithValue("speedRating", i.speedRating);
            sp.ParameterSet.Parameters.AddWithValue("spinMoveRating", i.spinMoveRating);
            sp.ParameterSet.Parameters.AddWithValue("staminaRating", i.staminaRating);
            sp.ParameterSet.Parameters.AddWithValue("stiffArmRating", i.stiffArmRating);
            sp.ParameterSet.Parameters.AddWithValue("strengthRating", i.strengthRating);
            sp.ParameterSet.Parameters.AddWithValue("stripBallTrait", i.stripBallTrait);
            sp.ParameterSet.Parameters.AddWithValue("tackleRating", i.tackleRating);
            sp.ParameterSet.Parameters.AddWithValue("teamId", i.teamId);
            sp.ParameterSet.Parameters.AddWithValue("teamSchemeOvr", i.teamSchemeOvr);
            sp.ParameterSet.Parameters.AddWithValue("throwAccDeepRating", i.throwAccDeepRating);
            sp.ParameterSet.Parameters.AddWithValue("throwAccMidRating", i.throwAccMidRating);
            sp.ParameterSet.Parameters.AddWithValue("throwAccRating", i.throwAccRating);
            sp.ParameterSet.Parameters.AddWithValue("throwAccShortRating", i.throwAccShortRating);
            sp.ParameterSet.Parameters.AddWithValue("throwAwayTrait", i.throwAwayTrait);
            sp.ParameterSet.Parameters.AddWithValue("throwOnRunRating", i.throwOnRunRating);
            sp.ParameterSet.Parameters.AddWithValue("throwPowerRating", i.throwPowerRating);
            sp.ParameterSet.Parameters.AddWithValue("throwUnderPressureRating", i.throwUnderPressureRating);
            sp.ParameterSet.Parameters.AddWithValue("tightSpiralTrait", i.tightSpiralTrait);
            sp.ParameterSet.Parameters.AddWithValue("toughRating", i.toughRating);
            sp.ParameterSet.Parameters.AddWithValue("truckRating", i.truckRating);
            sp.ParameterSet.Parameters.AddWithValue("weight", i.weight);
            sp.ParameterSet.Parameters.AddWithValue("yACCatchTrait", i.yACCatchTrait);
            sp.ParameterSet.Parameters.AddWithValue("yearsPro", i.yearsPro);
            sp.ParameterSet.Parameters.AddWithValue("zoneCoverRating", i.zoneCoverRating);


            return StoredProc.NonQuery(sp);
        }

        public void UpdatePlayerAbility(int leagueId, int rosterId, JSONRosters.Signatureslotlist ability)
        {
            var sp = new StoredProc
            {
                Name = "PlayerAbility_update", DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, ParameterSet = new SqlCommand()
            };

            sp.ParameterSet.Parameters.AddWithValue("@leagueId", leagueId);
            sp.ParameterSet.Parameters.AddWithValue("@rosterId", rosterId);

            sp.ParameterSet.Parameters.AddWithValue("@signatureLogoId", ability.signatureAbility.signatureLogoId);
            sp.ParameterSet.Parameters.AddWithValue("@isEmpty", ability.isEmpty);
            sp.ParameterSet.Parameters.AddWithValue("@isLocked", ability.locked);
            sp.ParameterSet.Parameters.AddWithValue("@ovrThreshold", ability.ovrThreshold);

            var status = StoredProc.NonQuery(sp);
        }

        public bool UpdatePlayerContracts(int leagueId, JSONRosters.Rosterinfolist i)
        {
            var sp = new StoredProc
            {
                Name = "PlayerContract_update", DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, ParameterSet = new SqlCommand()
            };

            sp.ParameterSet.Parameters.AddWithValue("@leagueId", leagueId);
            sp.ParameterSet.Parameters.AddWithValue("@rosterId", i.rosterId);

            sp.ParameterSet.Parameters.AddWithValue("@capHit", i.capHit);
            sp.ParameterSet.Parameters.AddWithValue("@capReleaseNetSavings", i.capReleaseNetSavings);
            sp.ParameterSet.Parameters.AddWithValue("@capReleasePenalty", i.capReleasePenalty);
            sp.ParameterSet.Parameters.AddWithValue("@contractBonus", i.contractBonus);
            sp.ParameterSet.Parameters.AddWithValue("@contractLength", i.contractLength);
            sp.ParameterSet.Parameters.AddWithValue("@contractSalary", i.contractSalary);
            sp.ParameterSet.Parameters.AddWithValue("@contractYearsLeft", i.contractYearsLeft);
            sp.ParameterSet.Parameters.AddWithValue("@desiredBonus", i.desiredBonus);
            sp.ParameterSet.Parameters.AddWithValue("@desiredLength", i.desiredLength);
            sp.ParameterSet.Parameters.AddWithValue("@desiredSalary", i.desiredSalary);
            sp.ParameterSet.Parameters.AddWithValue("@reSignStatus", i.reSignStatus);

            return StoredProc.NonQuery(sp);
        }

        public bool UpdatePlayerGrades(int leagueId, JSONRosters.Rosterinfolist i)
        {
            var sp = new StoredProc
            {
                Name = "PlayerGrades_update", DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, ParameterSet = new SqlCommand()
            };

            sp.ParameterSet.Parameters.AddWithValue("@leagueId", leagueId);
            sp.ParameterSet.Parameters.AddWithValue("@rosterId", i.rosterId);

            sp.ParameterSet.Parameters.AddWithValue("@durabilityGrade", i.durabilityGrade);
            sp.ParameterSet.Parameters.AddWithValue("@intangibleGrade", i.intangibleGrade);
            sp.ParameterSet.Parameters.AddWithValue("@physicalGrade", i.physicalGrade);
            sp.ParameterSet.Parameters.AddWithValue("@productionGrade", i.productionGrade);
            sp.ParameterSet.Parameters.AddWithValue("@sizeGrade", i.sizeGrade);

            return StoredProc.NonQuery(sp);
        }

        public bool UpdatePlayerProfile(int leagueId, JSONRosters.Rosterinfolist i)
        {
            var sp = new StoredProc
            {
                Name = "PlayerProfile_update", DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, ParameterSet = new SqlCommand()
            };

            sp.ParameterSet.Parameters.AddWithValue("@leagueId", leagueId);
            sp.ParameterSet.Parameters.AddWithValue("@rosterId", i.rosterId);
            sp.ParameterSet.Parameters.AddWithValue("@teamId", i.teamId);
            sp.ParameterSet.Parameters.AddWithValue("@portraitId", i.portraitId);
            sp.ParameterSet.Parameters.AddWithValue("@presentationId", i.presentationId);

            sp.ParameterSet.Parameters.AddWithValue("@firstName", i.firstName);
            sp.ParameterSet.Parameters.AddWithValue("@lastName", i.lastName);

            sp.ParameterSet.Parameters.AddWithValue("@position", i.position);
            sp.ParameterSet.Parameters.AddWithValue("@jerseyNum", i.jerseyNum);

            sp.ParameterSet.Parameters.AddWithValue("@weight", i.weight);
            sp.ParameterSet.Parameters.AddWithValue("@height", i.height);

            sp.ParameterSet.Parameters.AddWithValue("@yearsPro", i.yearsPro);

            sp.ParameterSet.Parameters.AddWithValue("@age", i.age);
            sp.ParameterSet.Parameters.AddWithValue("@birthDay", i.birthDay);
            sp.ParameterSet.Parameters.AddWithValue("@birthMonth", i.birthMonth);
            sp.ParameterSet.Parameters.AddWithValue("@birthYear", i.birthYear);

            sp.ParameterSet.Parameters.AddWithValue("@college", i.college);
            sp.ParameterSet.Parameters.AddWithValue("@draftPick", i.draftPick);
            sp.ParameterSet.Parameters.AddWithValue("@draftRound", i.draftRound);
            sp.ParameterSet.Parameters.AddWithValue("@rookieYear", i.rookieYear);

            sp.ParameterSet.Parameters.AddWithValue("@isActive", i.isActive);
            sp.ParameterSet.Parameters.AddWithValue("@isFreeAgent", i.isFreeAgent);
            sp.ParameterSet.Parameters.AddWithValue("@isOnPracticeSquad", i.isOnPracticeSquad);

            sp.ParameterSet.Parameters.AddWithValue("@isOnIR", i.isOnIR);
            sp.ParameterSet.Parameters.AddWithValue("@injuryLength", i.injuryLength);
            sp.ParameterSet.Parameters.AddWithValue("@injuryType", i.injuryType);

            sp.ParameterSet.Parameters.AddWithValue("@playerBestOvr", i.playerBestOvr);

            sp.ParameterSet.Parameters.AddWithValue("@runStyle", i.runStyle);
            sp.ParameterSet.Parameters.AddWithValue("@scheme", i.scheme);

            sp.ParameterSet.Parameters.AddWithValue("@skillPoints", i.skillPoints);
            sp.ParameterSet.Parameters.AddWithValue("@experiencePoints", i.experiencePoints);
            sp.ParameterSet.Parameters.AddWithValue("@legacyScore", i.legacyScore);

            return StoredProc.NonQuery(sp);
        }

        public bool UpdatePlayerRatings(int leagueId, JSONRosters.Rosterinfolist i)
        {
            var sp = new StoredProc
            {
                Name = "PlayerRatings_update", DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, ParameterSet = new SqlCommand()
            };

            sp.ParameterSet.Parameters.AddWithValue("@leagueId", leagueId);
            sp.ParameterSet.Parameters.AddWithValue("@rosterId", i.rosterId);

            sp.ParameterSet.Parameters.AddWithValue("@teamSchemeOvr", i.teamSchemeOvr);
            sp.ParameterSet.Parameters.AddWithValue("@playerBestOvr", i.playerBestOvr);
            sp.ParameterSet.Parameters.AddWithValue("@playerSchemeOvr", i.playerSchemeOvr);

            sp.ParameterSet.Parameters.AddWithValue("@accelRating", i.accelRating);
            sp.ParameterSet.Parameters.AddWithValue("@agilityRating", i.agilityRating);
            sp.ParameterSet.Parameters.AddWithValue("@awareRating", i.awareRating);
            sp.ParameterSet.Parameters.AddWithValue("@bCVRating", i.bCVRating);
            sp.ParameterSet.Parameters.AddWithValue("@blockShedRating", i.blockShedRating);
            sp.ParameterSet.Parameters.AddWithValue("@carryRating", i.carryRating);
            sp.ParameterSet.Parameters.AddWithValue("@catchRating", i.catchRating);
            sp.ParameterSet.Parameters.AddWithValue("@cITRating", i.cITRating);
            sp.ParameterSet.Parameters.AddWithValue("@confRating", i.confRating);
            sp.ParameterSet.Parameters.AddWithValue("@changeOfDirectionRating", i.changeOfDirectionRating);
            sp.ParameterSet.Parameters.AddWithValue("@finesseMovesRating", i.finesseMovesRating);
            sp.ParameterSet.Parameters.AddWithValue("@hitPowerRating", i.hitPowerRating);
            sp.ParameterSet.Parameters.AddWithValue("@impactBlockRating", i.impactBlockRating);
            sp.ParameterSet.Parameters.AddWithValue("@injuryRating", i.injuryRating);
            sp.ParameterSet.Parameters.AddWithValue("@jukeMoveRating", i.jukeMoveRating);
            sp.ParameterSet.Parameters.AddWithValue("@jumpRating", i.jumpRating);
            sp.ParameterSet.Parameters.AddWithValue("@kickAccRating", i.kickAccRating);
            sp.ParameterSet.Parameters.AddWithValue("@kickPowerRating", i.kickPowerRating);
            sp.ParameterSet.Parameters.AddWithValue("@kickRetRating", i.kickRetRating);
            sp.ParameterSet.Parameters.AddWithValue("@manCoverRating", i.manCoverRating);
            sp.ParameterSet.Parameters.AddWithValue("@passBlockRating", i.passBlockRating);
            sp.ParameterSet.Parameters.AddWithValue("@playActionRating", i.playActionRating);
            sp.ParameterSet.Parameters.AddWithValue("@playRecRating", i.playRecRating);
            sp.ParameterSet.Parameters.AddWithValue("@powerMovesRating", i.powerMovesRating);
            sp.ParameterSet.Parameters.AddWithValue("@pressRating", i.pressRating);
            sp.ParameterSet.Parameters.AddWithValue("@pursuitRating", i.pursuitRating);
            sp.ParameterSet.Parameters.AddWithValue("@releaseRating", i.releaseRating);
            sp.ParameterSet.Parameters.AddWithValue("@runBlockRating", i.runBlockRating);
            sp.ParameterSet.Parameters.AddWithValue("@routeRunRating", i.routeRunRating);
            sp.ParameterSet.Parameters.AddWithValue("@specCatchRating", i.specCatchRating);
            sp.ParameterSet.Parameters.AddWithValue("@speedRating", i.speedRating);
            sp.ParameterSet.Parameters.AddWithValue("@spinMoveRating", i.spinMoveRating);
            sp.ParameterSet.Parameters.AddWithValue("@staminaRating", i.staminaRating);
            sp.ParameterSet.Parameters.AddWithValue("@stiffArmRating", i.stiffArmRating);
            sp.ParameterSet.Parameters.AddWithValue("@strengthRating", i.strengthRating);
            sp.ParameterSet.Parameters.AddWithValue("@tackleRating", i.tackleRating);
            sp.ParameterSet.Parameters.AddWithValue("@throwAccDeepRating", i.throwAccDeepRating);
            sp.ParameterSet.Parameters.AddWithValue("@throwAccMidRating", i.throwAccMidRating);
            sp.ParameterSet.Parameters.AddWithValue("@throwAccRating", i.throwAccRating);
            sp.ParameterSet.Parameters.AddWithValue("@throwAccShortRating", i.throwAccShortRating);
            sp.ParameterSet.Parameters.AddWithValue("@throwOnRunRating", i.throwOnRunRating);
            sp.ParameterSet.Parameters.AddWithValue("@throwPowerRating", i.throwPowerRating);
            sp.ParameterSet.Parameters.AddWithValue("@toughRating", i.toughRating);
            sp.ParameterSet.Parameters.AddWithValue("@truckRating", i.truckRating);
            sp.ParameterSet.Parameters.AddWithValue("@zoneCoverRating", i.zoneCoverRating);
            sp.ParameterSet.Parameters.AddWithValue("@breakSackRating", i.breakSackRating);
            sp.ParameterSet.Parameters.AddWithValue("@breakTackleRating", i.breakTackleRating);
            sp.ParameterSet.Parameters.AddWithValue("@leadBlockRating", i.leadBlockRating);
            sp.ParameterSet.Parameters.AddWithValue("@passBlockFinesseRating", i.passBlockFinesseRating);
            sp.ParameterSet.Parameters.AddWithValue("@passBlockPowerRating", i.passBlockPowerRating);
            sp.ParameterSet.Parameters.AddWithValue("@routeRunDeepRating", i.routeRunDeepRating);
            sp.ParameterSet.Parameters.AddWithValue("@routeRunMedRating", i.routeRunMedRating);
            sp.ParameterSet.Parameters.AddWithValue("@routeRunShortRating", i.routeRunShortRating);
            sp.ParameterSet.Parameters.AddWithValue("@runBlockFinesseRating", i.runBlockFinesseRating);
            sp.ParameterSet.Parameters.AddWithValue("@runBlockPowerRating", i.runBlockPowerRating);
            sp.ParameterSet.Parameters.AddWithValue("@throwUnderPressureRating", i.throwUnderPressureRating);

            return StoredProc.NonQuery(sp);
        }

        public bool UpdatePlayerTraits(int leagueId, JSONRosters.Rosterinfolist i)
        {
            var sp = new StoredProc
            {
                Name = "PlayerTraits_update", DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, ParameterSet = new SqlCommand()
            };

            sp.ParameterSet.Parameters.AddWithValue("@leagueId", leagueId);
            sp.ParameterSet.Parameters.AddWithValue("@rosterId", i.rosterId);

            sp.ParameterSet.Parameters.AddWithValue("@bigHitTrait", i.bigHitTrait);
            sp.ParameterSet.Parameters.AddWithValue("@clutchTrait", i.clutchTrait);
            sp.ParameterSet.Parameters.AddWithValue("@coverBallTrait", i.coverBallTrait);
            sp.ParameterSet.Parameters.AddWithValue("@devTrait", i.devTrait);
            sp.ParameterSet.Parameters.AddWithValue("@dLBullRushTrait", i.dLBullRushTrait);
            sp.ParameterSet.Parameters.AddWithValue("@dLSpinTrait", i.dLSpinTrait);
            sp.ParameterSet.Parameters.AddWithValue("@dLSwimTrait", i.dLSwimTrait);
            sp.ParameterSet.Parameters.AddWithValue("@dropOpenPassTrait", i.dropOpenPassTrait);
            sp.ParameterSet.Parameters.AddWithValue("@feetInBoundsTrait", i.feetInBoundsTrait);
            sp.ParameterSet.Parameters.AddWithValue("@fightForYardsTrait", i.fightForYardsTrait);
            sp.ParameterSet.Parameters.AddWithValue("@forcePassTrait", i.forcePassTrait);
            sp.ParameterSet.Parameters.AddWithValue("@highMotorTrait", i.highMotorTrait);
            sp.ParameterSet.Parameters.AddWithValue("@hPCatchTrait", i.hPCatchTrait);
            sp.ParameterSet.Parameters.AddWithValue("@lBStyleTrait", i.lBStyleTrait);
            sp.ParameterSet.Parameters.AddWithValue("@penaltyTrait", i.penaltyTrait);
            sp.ParameterSet.Parameters.AddWithValue("@playBallTrait", i.playBallTrait);
            sp.ParameterSet.Parameters.AddWithValue("@posCatchTrait", i.posCatchTrait);
            sp.ParameterSet.Parameters.AddWithValue("@predictTrait", i.predictTrait);
            sp.ParameterSet.Parameters.AddWithValue("@qBStyleTrait", i.qBStyleTrait);
            sp.ParameterSet.Parameters.AddWithValue("@sensePressureTrait", i.sensePressureTrait);
            sp.ParameterSet.Parameters.AddWithValue("@stripBallTrait", i.stripBallTrait);
            sp.ParameterSet.Parameters.AddWithValue("@throwAwayTrait", i.throwAwayTrait);
            sp.ParameterSet.Parameters.AddWithValue("@tightSpiralTrait", i.tightSpiralTrait);
            sp.ParameterSet.Parameters.AddWithValue("@yACCatchTrait", i.yACCatchTrait);

            return StoredProc.NonQuery(sp);
        }
    }
}