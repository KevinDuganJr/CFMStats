using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;
using CFMStats.Classes;
using CFMStats.Controls;

namespace CFMStats
{
    public partial class Profile : Page
    {
        protected void btnGetAbilities_OnClick(object sender, EventArgs e)
        {
            var abilities = new PlayerAbilityRepository();

            var playerId = Helper.IntegerNull(Request.QueryString["id"]);
            if (playerId > 0)
            {
                abilities = abilities.GetPlayerAbilities(playerId);
            }

            var sbTable = new StringBuilder();

            var abilitiesCount = 0;
            var isEmptyCount = 0;
            var isLockedCount = 0;

            foreach (var ability in abilities.Values)
            {
                abilitiesCount++;

                if (ability.IsEmpty)
                {
                    isEmptyCount++;
                }

                if (ability.IsLocked)
                {
                    isLockedCount++;
                }

                sbTable.Append("<div class='col-sm-12'>");

                sbTable.Append("<div class='panel panel-default'>");

                sbTable.Append(ability.Title.Length > 0 ? $"<div class='panel-heading'><strong>{ability.Title}</strong></div>" : "<div class='panel-heading'>LOCKED</div>");

                sbTable.Append("<div class='panel-body'>");
                sbTable.Append(ability.Description.Length > 0 ? $"{ability.Description}<br />" : $"Ability is locked until player reaches {ability.OvrThreshold} overall rating.<br />");
                sbTable.Append($"<span class='label label-success'>{ability.ActivationDescription}</span> <span class='label label-danger'>{ability.DeactivationDescription}</span> ");

                sbTable.Append("</div>"); // end panel body

                sbTable.Append($"<div class='panel-footer'>Rating Threshold: {ability.OvrThreshold} </div>");

                sbTable.Append("</div>"); // end panel

                sbTable.Append("</div>"); // end col   
            }

            var sb = new StringBuilder();
            sb.Append($"<br /><strong>{abilitiesCount}</strong> Signature Abilities | <strong>{isLockedCount}</strong> Locked | <strong>{isEmptyCount}</strong> Empty <br /> <br />");

            sb.Append(sbTable);

            divAbilities.InnerHtml = sb.ToString();
        }

        protected void btnGetStats_Click(object sender, EventArgs e)
        {
            var playerId = Helper.IntegerNull(Request.QueryString["id"]);
            var iLeagueId = Helper.IntegerNull(Session["leagueID"]);

            GetPassingStats(1, playerId, iLeagueId);
            GetRushingStats(1, playerId, iLeagueId);
            GetReceivingStats(1, playerId, iLeagueId);
            GetDefenseStats(1, playerId, iLeagueId);
            GetKickingStats(1, playerId, iLeagueId);
            GetPuntingStats(1, playerId, iLeagueId);
        }

        protected void btnGetTraits_Click(object sender, EventArgs e)
        {
            var roster = new oRosters();

            var playerId = Helper.IntegerNull(Request.QueryString["id"]);
            if (playerId > 0)
            {
                roster = roster.GetPlayerTraits(playerId);
            }

            var sbTable = new StringBuilder();

            sbTable.Append("<table  class='table-responsive table-striped'>");
            sbTable.Append("<thead>");
            sbTable.Append("<tr><th data-filter='true' data-sorter='true'>Trait</th><th data-filter='true' data-sorter='true'>Value</th></tr>");
            sbTable.Append("</thead>");
            sbTable.Append("<tbody>");

            foreach (var item in roster.Values)
            {
                var devStatus = "Slow";
                switch (item.devTrait)
                {
                    case 1:
                        devStatus = "Normal";
                        break;

                    case 2:
                        devStatus = "Quick";
                        break;

                    case 3:
                        devStatus = "Superstar";
                        break;
                }
                //sbTable.Append(string.Format("<strong><small>Development</small></strong>: {0}<br />", devStatus));

                sbTable.Append($"<tr><td class=\'traitName\'>Development</td> <td class=\'traitValue\'>{devStatus}</td></tr>");

                var playBallInAir = "Conservative";
                switch (item.playBallTrait)
                {
                    case 1:
                        playBallInAir = "Balanced";
                        break;
                    case 2:
                        playBallInAir = "Aggressive";
                        break;
                }

                sbTable.Append(string.Format("<tr><td class='traitName'>{0}</td> <td class='traitValue'>{1}</td></tr>", "Plays Ball In The Air", playBallInAir));

                // Covers The Ball
                var coversTheBall = "Never";
                switch (item.coverBallTrait)
                {
                    case 2:
                        coversTheBall = "On Big Hits";
                        break;

                    case 3:
                        coversTheBall = "On Medium Hits";
                        break;

                    case 4:
                        coversTheBall = "For All Hits";
                        break;
                }

                sbTable.Append(string.Format("<tr><td class='traitName'>{0}</td> <td class='traitValue'>{1}</td></tr>", "Covers The Ball", coversTheBall));

                // LB Style
                var lbStyle = "Cover LB";
                switch (item.lBStyleTrait)
                {
                    case 1:
                        lbStyle = "Balanced";
                        break;
                    case 2:
                        lbStyle = "Pass Rush";
                        break;
                }

                // if (item.position.Contains("LB"))
                // {
                sbTable.Append($"<tr><td class='traitName'>{"LB Style"}</td> <td class='traitValue'>{lbStyle}</td></tr>");
                //}

                #region - QB -

                //if (item.position == "QB")
                //{
                // QB Style
                var qbStyle = "Scrambling";
                switch (item.qBStyleTrait)
                {
                    case 1:
                        qbStyle = "Balanced";
                        break;
                    case 2:
                        qbStyle = "Pocket";
                        break;
                }

                sbTable.Append($"<tr><td class='traitName'>{"QB Style"}</td> <td class='traitValue'>{qbStyle}</td></tr>");

                var qbSensePressure = "Paranoid";
                switch (item.sensePressureTrait)
                {
                    case 1:
                        qbSensePressure = "Trigger Happy";
                        break;
                    case 2:
                        qbSensePressure = "Ideal";
                        break;
                    case 3:
                        qbSensePressure = "Average";
                        break;
                    case 4:
                        qbSensePressure = "Tell Kevin";
                        break;
                }

                sbTable.Append($"<tr><td class='traitName'>{"Senses Pressure"}</td> <td class='traitValue'>{qbSensePressure}</td></tr>");

                var forcePass = "Conservative";
                switch (item.forcePassTrait)
                {
                    case 1:
                        forcePass = "Ideal";
                        break;
                    case 2:
                        forcePass = "Balanced";
                        break;
                }

                //sbTable.Append(string.Format("<strong><small>Forces Passes</small></strong>: {0}<br />", forcePass));
                sbTable.Append(string.Format("<tr><td class='traitName'>{0}</td> <td class='traitValue'>{1}</td></tr>", "Forces Passes", forcePass));

                if (item.tightSpiralTrait == 1)
                {
                    //sbTable.Append("Throws Tight Spiral<br />");
                    sbTable.Append(string.Format("<tr><td class='traitName'>{0}</td> <td class='traitValue'>{1}</td></tr>", "Throws Tight Spiral", "Yes"));
                }
                else
                {
                    sbTable.Append(string.Format("<tr><td class='traitName'>{0}</td> <td class='traitValue'>{1}</td></tr>", "Throws Tight Spiral", "No"));
                }

                if (item.throwAwayTrait == 1)
                {
                    //sbTable.Append("Throws Ball Away<br />");
                    sbTable.Append(string.Format("<tr><td class='traitName'>{0}</td> <td class='traitValue'>{1}</td></tr>", "Throws Ball Away", "Yes"));
                }
                else
                {
                    sbTable.Append(string.Format("<tr><td class='traitName'>{0}</td> <td class='traitValue'>{1}</td></tr>", "Throws Ball Away", "No"));
                }

                // }

                #endregion

                var penaltyTrait = string.Empty;
                switch (item.penaltyTrait)
                {
                    case 0:
                        penaltyTrait = "Undisciplined";
                        break;

                    case 1:
                        penaltyTrait = "Normal";
                        break;

                    case 2:
                        penaltyTrait = "Disciplined";
                        break;
                }

                sbTable.Append(string.Format("<tr><td class='traitName'>{0}</td> <td class='traitValue'>{1}</td></tr>", "Penalty", penaltyTrait));

                // clutch
                if (item.clutchTrait == 1)
                {
                    sbTable.Append(string.Format("<tr><td class='traitName'>{0}</td> <td class='traitValue'>{1}</td></tr>", "Clutch", "Yes"));
                }
                else
                {
                    sbTable.Append(string.Format("<tr><td class='traitName'>{0}</td> <td class='traitValue'>{1}</td></tr>", "Clutch", "No"));
                }

                // big hitter
                if (item.bigHitTrait == 1)
                {
                    sbTable.Append(string.Format("<tr><td class='traitName'>{0}</td> <td class='traitValue'>{1}</td></tr>", "Big Hitter", "Yes"));
                }
                else
                {
                    sbTable.Append(string.Format("<tr><td class='traitName'>{0}</td> <td class='traitValue'>{1}</td></tr>", "Big Hitter", "No"));
                }

                // catch in traffic
                if (item.cITRating == 1)
                {
                    sbTable.Append(string.Format("<tr><td class='traitName'>{0}</td> <td class='traitValue'>{1}</td></tr>", "Catches In Traffic", "Yes"));
                }
                else
                {
                    sbTable.Append(string.Format("<tr><td class='traitName'>{0}</td> <td class='traitValue'>{1}</td></tr>", "Catches In Traffic", "No"));
                }

                // feet in bounds
                if (item.feetInBoundsTrait == 1)
                {
                    sbTable.Append(string.Format("<tr><td class='traitName'>{0}</td> <td class='traitValue'>{1}</td></tr>", "Makes Sideline Catches", "Yes"));
                }
                else
                {
                    sbTable.Append(string.Format("<tr><td class='traitName'>{0}</td> <td class='traitValue'>{1}</td></tr>", "Makes Sideline Catches", "No"));
                }

                // fight for yards
                if (item.fightForYardsTrait == 1)
                {
                    sbTable.Append(string.Format("<tr><td class='traitName'>{0}</td> <td class='traitValue'>{1}</td></tr>", "Fights For Extra Yards", "Yes"));
                }
                else
                {
                    sbTable.Append(string.Format("<tr><td class='traitName'>{0}</td> <td class='traitValue'>{1}</td></tr>", "Fights For Extra Yards", "No"));
                }

                // high motor
                if (item.highMotorTrait == 1)
                {
                    sbTable.Append(string.Format("<tr><td class='traitName'>{0}</td> <td class='traitValue'>{1}</td></tr>", "High Motor", "Yes"));
                }
                else
                {
                    sbTable.Append(string.Format("<tr><td class='traitName'>{0}</td> <td class='traitValue'>{1}</td></tr>", "High Motor", "No"));
                }

                // predict contract
                if (item.predictTrait == 1)
                {
                    sbTable.Append(string.Format("<tr><td class='traitName'>{0}</td> <td class='traitValue'>{1}</td></tr>", "Contract Predictable", "Yes"));
                }
                else
                {
                    sbTable.Append(string.Format("<tr><td class='traitName'>{0}</td> <td class='traitValue'>{1}</td></tr>", "Contract Predictable", "No"));
                }

                // spin
                if (item.dLSpinTrait == 1)
                {
                    sbTable.Append(string.Format("<tr><td class='traitName'>{0}</td> <td class='traitValue'>{1}</td></tr>", "Utilizes Spin Move", "Yes"));
                }
                else
                {
                    sbTable.Append(string.Format("<tr><td class='traitName'>{0}</td> <td class='traitValue'>{1}</td></tr>", "Utilizes Spin Move", "No"));
                }

                // swim
                if (item.dLSwimTrait == 1)
                {
                    sbTable.Append($"<tr><td class='traitName'>{"Utilizes Swim Move"}</td> <td class='traitValue'>{"Yes"}</td></tr>");
                }
                else
                {
                    sbTable.Append($"<tr><td class='traitName'>{"Utilizes Swim Move"}</td> <td class='traitValue'>{"No"}</td></tr>");
                }

                // bull rush
                if (item.dLBullRushTrait == 1)
                {
                    sbTable.Append(string.Format("<tr><td class='traitName'>{0}</td> <td class='traitValue'>{1}</td></tr>", "Bull Rush Move", "Yes"));
                }
                else
                {
                    sbTable.Append(string.Format("<tr><td class='traitName'>{0}</td> <td class='traitValue'>{1}</td></tr>", "Bull Rush Move", "No"));
                }

                // drop open passes
                if (item.dropOpenPassTrait == 1)
                {
                    sbTable.Append(string.Format("<tr><td class='traitName'>{0}</td> <td class='traitValue'>{1}</td></tr>", "Drop Open Passes", "Yes"));
                }
                else
                {
                    sbTable.Append(string.Format("<tr><td class='traitName'>{0}</td> <td class='traitValue'>{1}</td></tr>", "Drop Open Passes", "No"));
                }

                // aggressive catch
                if (item.hPCatchTrait == 1)
                {
                    sbTable.Append(string.Format("<tr><td class='traitName'>{0}</td> <td class='traitValue'>{1}</td></tr>", "Aggressive Catch", "Yes"));
                }
                else
                {
                    sbTable.Append(string.Format("<tr><td class='traitName'>{0}</td> <td class='traitValue'>{1}</td></tr>", "Aggressive Catch", "No"));
                }

                // possesion catch
                if (item.posCatchTrait == 1)
                {
                    sbTable.Append(string.Format("<tr><td class='traitName'>{0}</td> <td class='traitValue'>{1}</td></tr>", "Possesssion Catch", "Yes"));
                }
                else
                {
                    sbTable.Append(string.Format("<tr><td class='traitName'>{0}</td> <td class='traitValue'>{1}</td></tr>", "Possesssion Catch", "No"));
                }

                // run after catch
                if (item.yACCatchTrait == 1)
                {
                    sbTable.Append(string.Format("<tr><td class='traitName'>{0}</td> <td class='traitValue'>{1}</td></tr>", "Run After Catch", "Yes"));
                }
                else
                {
                    sbTable.Append(string.Format("<tr><td class='traitName'>{0}</td> <td class='traitValue'>{1}</td></tr>", "Run After Catch", "No"));
                }

                // strip ball
                if (item.stripBallTrait == 1)
                {
                    sbTable.Append(string.Format("<tr><td class='traitName'>{0}</td> <td class='traitValue'>{1}</td></tr>", "Strips Ball", "Yes"));
                }
                else
                {
                    sbTable.Append(string.Format("<tr><td class='traitName'>{0}</td> <td class='traitValue'>{1}</td></tr>", "Strips Ball", "No"));
                }
            }

            sbTable.Append("</tbody></table>");
            tableTraits.InnerHtml = sbTable.ToString();
        }

        protected void btnGetWeekStats_Click(object sender, EventArgs e)
        {
            var iRosterID = Helper.IntegerNull(Request.QueryString["id"]);
            var iLeagueId = Helper.IntegerNull(Session["leagueID"]);

            GetPassingStatsGame(1, iRosterID, iLeagueId);
            GetRushingStatsGame(1, iRosterID, iLeagueId);
            GetReceivingStatsGame(1, iRosterID, iLeagueId);
            GetDefenseStatsGame(1, iRosterID, iLeagueId);
            GetKickingStatsGame(1, iRosterID, iLeagueId);
            GetPuntingStatsGame(1, iRosterID, iLeagueId);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var playerId = Helper.IntegerNull(Request.QueryString["id"]);

                if (playerId > 0)
                {
                    var leagueId = GetLeagueIdByPlayerId(playerId);
                    Session["leagueID"] = leagueId;

                    GetPlayer(playerId);
                    btnGetStats_Click(null, null);
                }
            }
        }

        private void GetDefenseStats(int stageIndex, int playerId, int leagueId)
        {
            var uc = (UserControl) Page.LoadControl("~/Controls/ucDefenseStatsSeason.ascx");

            var ClientId = uc.ClientID;
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            var myUsercontrol = FindControl(ClientId) as ucDefenseStatsSeason;
            myUsercontrol.iStageIndex = stageIndex;

            myUsercontrol.iRosterId = playerId;
            myUsercontrol.iLeagueId = leagueId;

            phDefense.Controls.Add(uc);
        }

        private void GetDefenseStatsGame(int iStageIndex, int playerId, int leagueId)
        {
            var uc = (UserControl) Page.LoadControl("~/Controls/ucDefenseStatsGame.ascx");

            var ClientId = uc.ClientID;
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            var myUsercontrol = FindControl(ClientId) as ucDefenseStatsGame;
            myUsercontrol.iStageIndex = iStageIndex;
            myUsercontrol.iRosterId = playerId;
            myUsercontrol.iLeagueId = leagueId;

            phDefenseWeek.Controls.Add(uc);
        }

        private void GetKickingStats(int stageIndex, int playerId, int leagueId)
        {
            var uc = (UserControl) Page.LoadControl("~/Controls/ucKickingStatsSeason.ascx");

            var ClientId = uc.ClientID;
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            var myUsercontrol = FindControl(ClientId) as ucKickingStatsSeason;
            myUsercontrol.iStageIndex = stageIndex;
            myUsercontrol.iRosterId = playerId;
            myUsercontrol.iLeagueId = leagueId;

            phKicking.Controls.Add(uc);
        }

        private void GetKickingStatsGame(int iStageIndex, int playerId, int leagueId)
        {
            var uc = (UserControl) Page.LoadControl("~/Controls/ucKickingStatsGame.ascx");

            var ClientId = uc.ClientID;
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            var myUsercontrol = FindControl(ClientId) as ucKickingStatsGame;
            myUsercontrol.iStageIndex = iStageIndex;
            myUsercontrol.iRosterId = playerId;
            myUsercontrol.iLeagueId = leagueId;

            phKickingWeek.Controls.Add(uc);
        }

        private int GetLeagueIdByPlayerId(int playerId)
        {
            var leagueId = 0;
            var sp = new StoredProc
            {
                Name = "[GetLeagueIdByPlayerId]", DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, ParameterSet = new SqlCommand()
            };

            sp.ParameterSet.Parameters.AddWithValue("@playerId", playerId);

            var ds = StoredProc.ShowMeTheData(sp);

            if (ds.Tables[0].Rows.Count == 0)
            {
                Response.Redirect("~/");
            }

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                leagueId = Helper.IntegerNull(item["leagueId"]);
            }

            return leagueId;
        }

        private void GetPassingStats(int stageIndex, int playerId, int leagueId)
        {
            var uc = (UserControl) Page.LoadControl("~/Controls/ucPassingStatsSeason.ascx");

            var ClientId = uc.ClientID;
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            var myUsercontrol = FindControl(ClientId) as ucPassingStatsSeason;
            myUsercontrol.iStageIndex = stageIndex;
            myUsercontrol.iRosterId = playerId;
            myUsercontrol.iLeagueId = leagueId;

            phPassing.Controls.Add(uc);
        }

        private void GetPassingStatsGame(int iStageIndex, int playerId, int leagueId)
        {
            var uc = (UserControl) Page.LoadControl("~/Controls/ucPassingStatsGame.ascx");

            var ClientId = uc.ClientID;
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            var myUsercontrol = FindControl(ClientId) as ucPassingStatsGame;
            myUsercontrol.iStageIndex = iStageIndex;
            myUsercontrol.iRosterId = playerId;
            myUsercontrol.iLeagueId = leagueId;

            phPassingWeek.Controls.Add(uc);
        }

        private void GetPlayer(int playerId)
        {
            var sp = new StoredProc
            {
                Name = "PlayerProfile_select", DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, ParameterSet = new SqlCommand()
            };

            sp.ParameterSet.Parameters.AddWithValue("@playerId", playerId);

            var ds = StoredProc.ShowMeTheData(sp);

            if (ds.Tables[0].Rows.Count == 0)
            {
                Response.Redirect("~/");
            }

            var sbTable = new StringBuilder();

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                var feet = Helper.IntegerNull(item["height"]) / 12;
                var inchesLeft = Helper.IntegerNull(item["height"]) % 12;

                sbTable.Append($"<span class='position'>{Helper.StringNull(item["position"])}</span> | <span class='playerName'>{Helper.StringNull(item["firstName"])} {Helper.StringNull(item["lastName"])}</span> <span class='jersey'>#{Helper.IntegerNull(item["jerseyNum"])}</span>");

                sbTable.Append("<div class='row'>");

                // bio stuff left
                sbTable.Append("<div class='col-sm-5 col-xs-6'>");
                sbTable.Append($"<span class='cityName'>{Helper.StringNull(item["cityName"])}</span> <span class='teamName'>{Helper.StringNull(item["displayName"])}</span><br />");

                sbTable.Append($"{Helper.IntegerNull(item["age"])} years old<br />");
                sbTable.Append($"{feet}' {inchesLeft}\" {Helper.IntegerNull(item["weight"])} lbs <br />");

                sbTable.Append(Helper.IntegerNull(item["yearsPro"]) == 0 ? "ROOKIE<br />" : $"{Helper.IntegerNull(item["yearsPro"])} years pro<br />");

                sbTable.Append(Helper.IntegerNull(item["draftRound"]) == 64 ? "undrafted <br />" : $"Round: {Helper.IntegerNull(item["draftRound"])}, Pick: {Helper.IntegerNull(item["draftPick"])}<br />");

                sbTable.Append($"{Helper.StringNull(item["college"])}<br />");

                if (Helper.IntegerNull(item["injuryType"]) != 97 && Helper.BooleanNull(item["isActive"]) == false)
                {
                    sbTable.Append($"<span class='label label-danger'>INJURED</span> {Helper.StringNull(item["injuryName"])}<br />");
                }

                // if (Helper.Boolean_Null(item["isOnIR"]) == true) { sbTable.Append("Injured Reserve <br />"); }
                if (Helper.BooleanNull(item["isRetired"]))
                {
                    sbTable.Append("Retired <br />");
                }

                if (Helper.BooleanNull(item["isOnIR"]))
                {
                    sbTable.Append("On Injured Reserve <br />");
                }

                if (Helper.BooleanNull(item["isOnPracticeSquad"]))
                {
                    sbTable.Append("Practice Squad <br />");
                }

                sbTable.Append($"<span class='label label-success'>Contract</span> ${Helper.IntegerNull(item["contractSalary"]):n0}<br />");
                sbTable.Append($"<span class='label label-default'>Player Best OVR</span> {Helper.IntegerNull(item["playerBestOVR"])}<br />");
                sbTable.Append($"<span class='label label-default'>Player Scheme OVR</span> {Helper.IntegerNull(item["playerSchemeOvr"])}<br />");
                sbTable.Append($"<span class='label label-default'>Team Scheme OVR</span> {Helper.IntegerNull(item["teamSchemeOvr"])}<br />");

                sbTable.Append("<br /></div>");

                sbTable.Append("<div class='col-sm-5 col-xs-6'>");
                sbTable.Append($"<img src='https://madden-assets-cdn.pulse.ea.com/madden20/portraits/256/{Helper.IntegerNull(item["portraitId"])}.png' class='img-circle' alt=''>");
                sbTable.Append("</div>");

                // team logo
                sbTable.Append("<div class='col-sm-2 col-xs-6'>");
                sbTable.Append($"<div style='background-image: url(images/team/{Helper.StringNull(item["displayName"]).Replace(" ", string.Empty)}.png); background-repeat: no-repeat; height: 150px; width: 150px;'></div>");
                sbTable.Append("</div>");
            }

            tableProfile.InnerHtml = sbTable.ToString();
        }

        private void GetPuntingStats(int stageIndex, int playerId, int leagueId)
        {
            var uc = (UserControl) Page.LoadControl("~/Controls/ucPuntingStatsSeason.ascx");

            var ClientId = uc.ClientID;
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            var myUsercontrol = FindControl(ClientId) as ucPuntingStatsSeason;
            myUsercontrol.iStageIndex = stageIndex;
            myUsercontrol.iRosterId = playerId;
            myUsercontrol.iLeagueId = leagueId;

            phPunting.Controls.Add(uc);
        }

        private void GetPuntingStatsGame(int iStageIndex, int playerId, int leagueId)
        {
            var uc = (UserControl) Page.LoadControl("~/Controls/ucPuntingStatsGame.ascx");

            var ClientId = uc.ClientID;
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            var myUsercontrol = FindControl(ClientId) as ucPuntingStatsGame;
            myUsercontrol.iStageIndex = iStageIndex;
            myUsercontrol.iRosterId = playerId;
            myUsercontrol.iLeagueId = leagueId;

            phPuntingWeek.Controls.Add(uc);
        }

        private void GetReceivingStats(int stageIndex, int playerId, int leagueId)
        {
            var uc = (UserControl) Page.LoadControl("~/Controls/ucReceivingStatsSeason.ascx");

            var ClientId = uc.ClientID; // string.Format("{0}{1}", uc.ClientID, DateTime.Now.ToString("fff"));
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            var myUsercontrol = FindControl(ClientId) as ucReceivingStatsSeason;
            myUsercontrol.StageIndex = stageIndex;
            myUsercontrol.RosterId = playerId;
            myUsercontrol.LeagueId = leagueId;

            phReceiving.Controls.Add(uc);
        }

        private void GetReceivingStatsGame(int iStageIndex, int playerId, int leagueId)
        {
            var uc = (UserControl) Page.LoadControl("~/Controls/ucReceivingStatsGame.ascx");

            var ClientId = uc.ClientID; // string.Format("{0}{1}", uc.ClientID, DateTime.Now.ToString("fff"));
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            var myUsercontrol = FindControl(ClientId) as ucReceivingStatsGame;
            myUsercontrol.iStageIndex = iStageIndex;
            myUsercontrol.iRosterId = playerId;
            myUsercontrol.iLeagueId = leagueId;

            phReceivingWeek.Controls.Add(uc);
        }

        private void GetRushingStats(int stageIndex, int playerId, int leagueId)
        {
            var uc = (UserControl) Page.LoadControl("~/Controls/ucRushingStatsSeason.ascx");

            var ClientId = uc.ClientID;
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            var myUsercontrol = FindControl(ClientId) as ucRushingStatsSeason;
            myUsercontrol.iStageIndex = stageIndex;
            myUsercontrol.iRosterId = playerId;
            myUsercontrol.iLeagueId = leagueId;

            phRushing.Controls.Add(uc);
        }

        private void GetRushingStatsGame(int iStageIndex, int playerId, int leagueId)
        {
            var uc = (UserControl) Page.LoadControl("~/Controls/ucRushingStatsGame.ascx");

            var ClientId = uc.ClientID;
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            var myUsercontrol = FindControl(ClientId) as ucRushingStatsGame;
            myUsercontrol.iStageIndex = iStageIndex;
            myUsercontrol.iRosterId = playerId;
            myUsercontrol.iLeagueId = leagueId;

            phRushingWeek.Controls.Add(uc);
        }
    }
}