using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CFMStats.Classes.JSON
{
    public class JsonPlayer
    {

        public class Rootobject
        {
            public int accelRating { get; set; }
            public int age { get; set; }
            public int agilityRating { get; set; }
            public int awareRating { get; set; }
            public int bCVRating { get; set; }
            public int bigHitTrait { get; set; }
            public int birthDay { get; set; }
            public int birthMonth { get; set; }
            public int birthYear { get; set; }
            public int blockShedRating { get; set; }
            public int breakSackRating { get; set; }
            public int breakTackleRating { get; set; }
            public int cITRating { get; set; }
            public int capHit { get; set; }
            public int capReleaseNetSavings { get; set; }
            public int capReleasePenalty { get; set; }
            public int carryRating { get; set; }
            public int catchRating { get; set; }
            public int clutchTrait { get; set; }
            public string college { get; set; }
            public int confRating { get; set; }
            public int contractBonus { get; set; }
            public int contractLength { get; set; }
            public int contractSalary { get; set; }
            public int contractYearsLeft { get; set; }
            public int coverBallTrait { get; set; }
            public int dLBullRushTrait { get; set; }
            public int dLSpinTrait { get; set; }
            public int dLSwimTrait { get; set; }
            public int desiredBonus { get; set; }
            public int desiredLength { get; set; }
            public int desiredSalary { get; set; }
            public int devTrait { get; set; }
            public int draftPick { get; set; }
            public int draftRound { get; set; }
            public int dropOpenPassTrait { get; set; }
            public int durabilityGrade { get; set; }
            public int changeOfDirectionRating { get; set; }
            public int experiencePoints { get; set; }
            public int feetInBoundsTrait { get; set; }
            public int fightForYardsTrait { get; set; }
            public int finesseMovesRating { get; set; }
            public string firstName { get; set; }
            public int forcePassTrait { get; set; }
            public int hPCatchTrait { get; set; }
            public int height { get; set; }
            public int highMotorTrait { get; set; }
            public int hitPowerRating { get; set; }
            public int homeState { get; set; }
            public string homeTown { get; set; }
            public int impactBlockRating { get; set; }
            public int injuryLength { get; set; }
            public int injuryRating { get; set; }
            public int injuryType { get; set; }
            public int intangibleGrade { get; set; }
            public bool isActive { get; set; }
            public bool isFreeAgent { get; set; }
            public bool isOnIR { get; set; }
            public bool isOnPracticeSquad { get; set; }
            public int jerseyNum { get; set; }
            public int jukeMoveRating { get; set; }
            public int jumpRating { get; set; }
            public int kickAccRating { get; set; }
            public int kickPowerRating { get; set; }
            public int kickRetRating { get; set; }
            public int lBStyleTrait { get; set; }
            public string lastName { get; set; }
            public int leadBlockRating { get; set; }
            public int legacyScore { get; set; }
            public int manCoverRating { get; set; }
            public int passBlockFinesseRating { get; set; }
            public int passBlockPowerRating { get; set; }
            public int passBlockRating { get; set; }
            public int penaltyTrait { get; set; }
            public int physicalGrade { get; set; }
            public int playActionRating { get; set; }
            public int playBallTrait { get; set; }
            public int playRecRating { get; set; }
            public int playerBestOvr { get; set; }
            public int playerSchemeOvr { get; set; }
            public int portraitId { get; set; }
            public int posCatchTrait { get; set; }
            public string position { get; set; }
            public int powerMovesRating { get; set; }
            public int predictTrait { get; set; }
            public int presentationId { get; set; }
            public int pressRating { get; set; }
            public int productionGrade { get; set; }
            public int pursuitRating { get; set; }
            public int qBStyleTrait { get; set; }
            public int reSignStatus { get; set; }
            public int releaseRating { get; set; }
            public int rookieYear { get; set; }
            public int rosterId { get; set; }
            public int routeRunDeepRating { get; set; }
            public int routeRunMedRating { get; set; }
            public int routeRunShortRating { get; set; }
            public int runBlockFinesseRating { get; set; }
            public int runBlockPowerRating { get; set; }
            public int runBlockRating { get; set; }
            public int runStyle { get; set; }
            public int scheme { get; set; }
            public int sensePressureTrait { get; set; }
            public Signatureslotlist[] signatureSlotList { get; set; }
            public int sizeGrade { get; set; }
            public int skillPoints { get; set; }
            public int specCatchRating { get; set; }
            public int speedRating { get; set; }
            public int spinMoveRating { get; set; }
            public int staminaRating { get; set; }
            public int stiffArmRating { get; set; }
            public int strengthRating { get; set; }
            public int stripBallTrait { get; set; }
            public int tackleRating { get; set; }
            public int teamId { get; set; }
            public int teamSchemeOvr { get; set; }
            public int throwAccDeepRating { get; set; }
            public int throwAccMidRating { get; set; }
            public int throwAccRating { get; set; }
            public int throwAccShortRating { get; set; }
            public int throwAwayTrait { get; set; }
            public int throwOnRunRating { get; set; }
            public int throwPowerRating { get; set; }
            public int throwUnderPressureRating { get; set; }
            public int tightSpiralTrait { get; set; }
            public int toughRating { get; set; }
            public int truckRating { get; set; }
            public int weight { get; set; }
            public int yACCatchTrait { get; set; }
            public int yearsPro { get; set; }
            public int zoneCoverRating { get; set; }
        }

        public class Signatureslotlist
        {
            public bool isEmpty { get; set; }
            public bool locked { get; set; }
            public int ovrThreshold { get; set; }
            public Signatureability signatureAbility { get; set; }
        }

        public class Signatureability
        {
            public string signatureActivationDescription { get; set; }
            public string signatureDeactivationDescription { get; set; }
            public string signatureDescription { get; set; }
            public int signatureLogoId { get; set; }
            public string signatureTitle { get; set; }
        }

    }
}