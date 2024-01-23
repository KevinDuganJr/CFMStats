using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using CFMStats.Classes;
using CFMStats.Controls;
using CFMStats.Services;

namespace CFMStats
{
    public partial class BoxScore : Page
    {
        public void getAwayDefense(int teamID)
        {
            var uc = (UserControl)Page.LoadControl("~/Controls/ucDefenseStats.ascx");

            var ClientId = uc.ClientID;
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            var myUsercontrol = FindControl(ClientId) as ucDefenseStats;

            myUsercontrol.iStageIndex = Helper.IntegerNull(ddlSeasonType.SelectedItem.Value);
            myUsercontrol.iSeason = Helper.IntegerNull(ddlSeason.SelectedItem.Value);
            myUsercontrol.iWeek = Helper.IntegerNull(ddlWeek.SelectedItem.Value);
            myUsercontrol.iTop = 20;
            myUsercontrol.isFull = false;
            myUsercontrol.iTeamID = teamID;
            myUsercontrol.iLeagueId = Helper.IntegerNull(Session["leagueId"]);

            phAwayDefense.Controls.Add(uc);
        }

        public void getAwayKicking(int teamID)
        {
            var uc = (UserControl)Page.LoadControl("~/Controls/ucKickingStats.ascx");

            var ClientId = uc.ClientID;
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            var myUsercontrol = FindControl(ClientId) as ucKickingStats;

            myUsercontrol.iStageIndex = Helper.IntegerNull(ddlSeasonType.SelectedItem.Value);
            myUsercontrol.iSeason = Helper.IntegerNull(ddlSeason.SelectedItem.Value);
            myUsercontrol.iWeek = Helper.IntegerNull(ddlWeek.SelectedItem.Value);
            myUsercontrol.iTop = 20;
            myUsercontrol.isFull = false;
            myUsercontrol.iTeamID = teamID;
            myUsercontrol.iLeagueId = Helper.IntegerNull(Session["leagueId"]);

            phAwayKicking.Controls.Add(uc);
        }

        public void getAwayPassing(int teamID)
        {
            var uc = (UserControl)Page.LoadControl("~/Controls/ucPassingStats.ascx");

            var ClientId = uc.ClientID;
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            var myUsercontrol = FindControl(ClientId) as ucPassingStats;

            myUsercontrol.StageIndex = Helper.IntegerNull(ddlSeasonType.SelectedItem.Value);
            myUsercontrol.Season = Helper.IntegerNull(ddlSeason.SelectedItem.Value);
            myUsercontrol.Week = Helper.IntegerNull(ddlWeek.SelectedItem.Value);
            myUsercontrol.Top = 20;
            myUsercontrol.isFull = false;
            myUsercontrol.iTeamID = teamID;
            myUsercontrol.iLeagueId = Helper.IntegerNull(Session["leagueId"]);

            phAwayPassing.Controls.Add(uc);
        }

        public void getAwayPunting(int teamID)
        {
            var uc = (UserControl)Page.LoadControl("~/Controls/ucPuntingStats.ascx");

            var ClientId = uc.ClientID;
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            var myUsercontrol = FindControl(ClientId) as ucPuntingStats;

            myUsercontrol.iStageIndex = Helper.IntegerNull(ddlSeasonType.SelectedItem.Value);
            myUsercontrol.iSeason = Helper.IntegerNull(ddlSeason.SelectedItem.Value);
            myUsercontrol.iWeek = Helper.IntegerNull(ddlWeek.SelectedItem.Value);
            myUsercontrol.iTop = 20;
            myUsercontrol.isFull = false;
            myUsercontrol.iTeamID = teamID;
            myUsercontrol.iLeagueId = Helper.IntegerNull(Session["leagueId"]);

            phAwayPunting.Controls.Add(uc);
        }

        public void getAwayReceiving(int teamID)
        {
            var uc = (UserControl)Page.LoadControl("~/Controls/ucReceivingStats.ascx");

            var ClientId = uc.ClientID;
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            var myUsercontrol = FindControl(ClientId) as ucReceivingStats;

            myUsercontrol.iStageIndex = Helper.IntegerNull(ddlSeasonType.SelectedItem.Value);
            myUsercontrol.iSeason = Helper.IntegerNull(ddlSeason.SelectedItem.Value);
            myUsercontrol.iWeek = Helper.IntegerNull(ddlWeek.SelectedItem.Value);
            myUsercontrol.iTop = 20;
            myUsercontrol.isFull = false;
            myUsercontrol.iTeamID = teamID;
            myUsercontrol.iLeagueId = Helper.IntegerNull(Session["leagueId"]);

            phAwayReceiving.Controls.Add(uc);
        }

        public void getAwayRushing(int teamID)
        {
            var uc = (UserControl)Page.LoadControl("~/Controls/ucRushingStats.ascx");

            var ClientId = uc.ClientID;
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            var myUsercontrol = FindControl(ClientId) as ucRushingStats;

            myUsercontrol.iStageIndex = Helper.IntegerNull(ddlSeasonType.SelectedItem.Value);
            myUsercontrol.iSeason = Helper.IntegerNull(ddlSeason.SelectedItem.Value);
            myUsercontrol.iWeek = Helper.IntegerNull(ddlWeek.SelectedItem.Value);
            myUsercontrol.iTop = 20;
            myUsercontrol.isFull = false;
            myUsercontrol.iTeamID = teamID;
            myUsercontrol.iLeagueId = Helper.IntegerNull(Session["leagueId"]);

            phAwayRushing.Controls.Add(uc);
        }

        public void getHomeDefense(int teamID)
        {
            var uc = (UserControl)Page.LoadControl("~/Controls/ucDefenseStats.ascx");

            var ClientId = uc.ClientID;
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            var myUsercontrol = FindControl(ClientId) as ucDefenseStats;

            myUsercontrol.iStageIndex = Helper.IntegerNull(ddlSeasonType.SelectedItem.Value);
            myUsercontrol.iSeason = Helper.IntegerNull(ddlSeason.SelectedItem.Value);
            myUsercontrol.iWeek = Helper.IntegerNull(ddlWeek.SelectedItem.Value);
            myUsercontrol.iTop = 20;
            myUsercontrol.isFull = false;
            myUsercontrol.iTeamID = teamID;
            myUsercontrol.iLeagueId = Helper.IntegerNull(Session["leagueId"]);

            phHomeDefense.Controls.Add(uc);
        }

        public void getHomeKicking(int teamID)
        {
            var uc = (UserControl)Page.LoadControl("~/Controls/ucKickingStats.ascx");

            var ClientId = uc.ClientID;
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            var myUsercontrol = FindControl(ClientId) as ucKickingStats;

            myUsercontrol.iStageIndex = Helper.IntegerNull(ddlSeasonType.SelectedItem.Value);
            myUsercontrol.iSeason = Helper.IntegerNull(ddlSeason.SelectedItem.Value);
            myUsercontrol.iWeek = Helper.IntegerNull(ddlWeek.SelectedItem.Value);
            myUsercontrol.iTop = 20;
            myUsercontrol.isFull = false;
            myUsercontrol.iTeamID = teamID;
            myUsercontrol.iLeagueId = Helper.IntegerNull(Session["leagueId"]);

            phHomeKicking.Controls.Add(uc);
        }

        public void getHomePassing(int teamID)
        {
            var uc = (UserControl)Page.LoadControl("~/Controls/ucPassingStats.ascx");

            var ClientId = uc.ClientID;
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            var myUsercontrol = FindControl(ClientId) as ucPassingStats;
            myUsercontrol.StageIndex = Helper.IntegerNull(ddlSeasonType.SelectedItem.Value);
            myUsercontrol.Season = Helper.IntegerNull(ddlSeason.SelectedItem.Value);
            myUsercontrol.Week = Helper.IntegerNull(ddlWeek.SelectedItem.Value);
            myUsercontrol.Top = 20;
            myUsercontrol.isFull = false;
            myUsercontrol.iTeamID = teamID;
            myUsercontrol.iLeagueId = Helper.IntegerNull(Session["leagueId"]);

            phHomePassing.Controls.Add(uc);
        }

        public void getHomePunting(int teamID)
        {
            var uc = (UserControl)Page.LoadControl("~/Controls/ucPuntingStats.ascx");

            var ClientId = uc.ClientID;
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            var myUsercontrol = FindControl(ClientId) as ucPuntingStats;

            myUsercontrol.iStageIndex = Helper.IntegerNull(ddlSeasonType.SelectedItem.Value);
            myUsercontrol.iSeason = Helper.IntegerNull(ddlSeason.SelectedItem.Value);
            myUsercontrol.iWeek = Helper.IntegerNull(ddlWeek.SelectedItem.Value);
            myUsercontrol.iTop = 20;
            myUsercontrol.isFull = false;
            myUsercontrol.iTeamID = teamID;
            myUsercontrol.iLeagueId = Helper.IntegerNull(Session["leagueId"]);

            phHomePunting.Controls.Add(uc);
        }

        public void getHomeReceiving(int teamID)
        {
            var uc = (UserControl)Page.LoadControl("~/Controls/ucReceivingStats.ascx");

            var ClientId = uc.ClientID;
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            var myUsercontrol = FindControl(ClientId) as ucReceivingStats;

            myUsercontrol.iStageIndex = Helper.IntegerNull(ddlSeasonType.SelectedItem.Value);
            myUsercontrol.iSeason = Helper.IntegerNull(ddlSeason.SelectedItem.Value);
            myUsercontrol.iWeek = Helper.IntegerNull(ddlWeek.SelectedItem.Value);
            myUsercontrol.iTop = 20;
            myUsercontrol.isFull = false;
            myUsercontrol.iTeamID = teamID;
            myUsercontrol.iLeagueId = Helper.IntegerNull(Session["leagueId"]);

            phHomeReceiving.Controls.Add(uc);
        }

        public void getHomeRushing(int teamID)
        {
            var uc = (UserControl)Page.LoadControl("~/Controls/ucRushingStats.ascx");

            var ClientId = uc.ClientID;
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            var myUsercontrol = FindControl(ClientId) as ucRushingStats;

            myUsercontrol.iStageIndex = Helper.IntegerNull(ddlSeasonType.SelectedItem.Value);
            myUsercontrol.iSeason = Helper.IntegerNull(ddlSeason.SelectedItem.Value);
            myUsercontrol.iWeek = Helper.IntegerNull(ddlWeek.SelectedItem.Value);
            myUsercontrol.iTop = 20;
            myUsercontrol.isFull = false;
            myUsercontrol.iTeamID = teamID;
            myUsercontrol.iLeagueId = Helper.IntegerNull(Session["leagueId"]);

            phHomeRushing.Controls.Add(uc);
        }

        protected void btnGetPlayerStats_Click(object sender, EventArgs e)
        {
            GetPlayerStats();
        }

        protected void btnGetTeamStats_Click(object sender, EventArgs e)
        {
            var stageIndex = Helper.IntegerNull(Request.QueryString["type"]);
            var season = Helper.IntegerNull(Request.QueryString["season"]);
            var week = Helper.IntegerNull(Request.QueryString["week"]);
            var teamId = Helper.IntegerNull(Request.QueryString["id"]);
            var leagueId = Helper.IntegerNull(Session["leagueId"]);

            var items = GetScheduleId(season, week, teamId, stageIndex, leagueId);

            GetTeamStats(items, season, week, stageIndex, leagueId);
        }

        protected void BuildLeagueTeamList(int leagueId)
        {
            var SP = new StoredProc
            {
                Name = "TeamInfo_select", DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, ParameterSet = new SqlCommand()
            };

            SP.ParameterSet.Parameters.AddWithValue("@leagueId", leagueId);

            var ds = StoredProc.ShowMeTheData(SP);

            if(ds.Tables.Count == 0)
            {
                return;
            }

            ddlLeagueTeams.Items.Clear();

            foreach(DataRow item in ds.Tables[0].Rows)
            {
                ddlLeagueTeams.Items.Add(new ListItem(Helper.StringNull(item["displayName"]),
                    Helper.StringNull(item["teamID"])));
            }
        }

        protected void BuildSeasonList(int leagueId)
        {
            var season = new oSeasons();
            season = season.getSeasons(leagueId);

            foreach(var item in season.Values)
            {
                ddlSeason.Items.Add(new ListItem(item.Year.ToString(), item.ID.ToString()));
            }
        }

        protected void ddlWeek_SelectedIndexChanged(object sender, EventArgs e)
        {
            //getBoxScore(Helper.Integer_Null(ddlSeason.SelectedItem.WeekIndex),
            //    Helper.Integer_Null(ddlWeek.SelectedItem.WeekIndex),
            //    Helper.Integer_Null(ddlLeagueTeams.SelectedItem.WeekIndex),
            //    Helper.String_Null(ddlSeasonType.SelectedItem.WeekIndex));
            var leagueId = Helper.IntegerNull(Session["leagueId"]);
            Response.Redirect($"/BoxScore?id={ddlLeagueTeams.SelectedItem.Value}&leagueId={leagueId}&season={ddlSeason.SelectedItem.Value}&week={ddlWeek.SelectedItem.Value}&type={ddlSeasonType.SelectedItem.Value}");
        }

        protected void getAwayStats(int id)
        {
            getAwayPassing(id);
            getAwayRushing(id);
            getAwayReceiving(id);
            getAwayDefense(id);
            getAwayKicking(id);
            getAwayPunting(id);
        }

        protected void getBoxScore(int season, int week, int teamid, int stageIndex, int leagueId)
        {
            var teams = new TeamService();
            teams = teams.GetLeagueTeams(leagueId);


            boxscore.InnerHtml = string.Empty;

            var sp = new StoredProc
            {
                Name = "BoxScore_select", DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, ParameterSet = new SqlCommand()
            };

            sp.ParameterSet.Parameters.AddWithValue("@stageIndex", stageIndex);
            sp.ParameterSet.Parameters.AddWithValue("@seasonIndex", season);
            sp.ParameterSet.Parameters.AddWithValue("@teamId", teamid);
            sp.ParameterSet.Parameters.AddWithValue("@weekindex", week);
            sp.ParameterSet.Parameters.AddWithValue("@leagueId", leagueId);

            var ds = StoredProc.ShowMeTheData(sp);

            if(ds.Tables.Count == 0)
            {
                return;
            }

            var homeTeamID = 0;
            var awayTeamID = 0;

            var sbTable = new StringBuilder();

            sbTable.Append("<div class='row'>");

            foreach(DataRow item in ds.Tables[0].Rows)
            {
                homeTeamID = item.Field<int>("homeID");
                awayTeamID = item.Field<int>("awayID");

                sbTable.Append("<div class='col-6'>");
                sbTable.Append("<div class='d-flex justify-content-center'>");
                sbTable.Append("<table>");
                sbTable.Append("<tr>");

                sbTable.Append("<td>");
                sbTable.Append($"<img class='awayLogo img-responsive' src='/images/team/large/{teams[awayTeamID].logoId}.png' height='80' style='padding: 0 5px;' />");
                sbTable.Append("</td>");

                sbTable.Append("<td>");
                sbTable.Append($"<div class='teamScore'>{item.Field<int>("awayScore")}</div>");
                sbTable.Append("</td>");

                sbTable.Append("</tr>");
                sbTable.Append("</table>");
                sbTable.Append("</div>");
                sbTable.Append("</div>");

                sbTable.Append("<div class='col-6'>");
                sbTable.Append("<div class='d-flex justify-content-center'>");
                sbTable.Append("<table>");
                sbTable.Append("<tr>");

                sbTable.Append("<td>");
                sbTable.Append(
                    $"<img class='homeLogo img-responsive' src='/images/team/large/{teams[homeTeamID].logoId}.png' height='80' style='padding: 0 5px;' />");
                sbTable.Append("</td>");

                sbTable.Append("<td>");
                sbTable.Append($"<div class='teamScore'>{item.Field<int>("homeScore")}</div>");
                sbTable.Append("</td>");

                sbTable.Append("</tr>");
                sbTable.Append("</table>");
                sbTable.Append("</div>");
                sbTable.Append("</div>");

                sbTable.Append("</div>");
            }

            sbTable.Append("</div>");

            boxscore.InnerHtml = sbTable.ToString();
        }

        protected void getHomeStats(int id)
        {
            getHomePassing(id);
            getHomeRushing(id);
            getHomeReceiving(id);
            getHomeDefense(id);
            getHomeKicking(id);
            getHomePunting(id);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if(Helper.StringNull(Request.QueryString["leagueId"]).Length == 0)
                {
                    Response.Redirect("~/");
                }

                Session["leagueId"] = Helper.StringNull(Request.QueryString["leagueId"]);

                var iLeagueId = Helper.IntegerNull(Session["leagueId"]);

                SetWeek();
                BuildSeasonTypeList();
                BuildSeasonList(iLeagueId);
                BuildLeagueTeamList(iLeagueId);

                getStarted();
            }
        }

        protected void SetWeek()
        {
            var wks = new oWeeks();
            wks = wks.Regular();

            ddlWeek.Items.Clear();

            foreach(var item in wks.Values)
            {
                ddlWeek.Items.Add(new ListItem(item.Text, item.WeekIndex.ToString()));
            }
            
        }

        private void BuildSeasonTypeList()
        {
            ddlSeasonType.Items.Clear();
            ddlSeasonType.Items.Add(new ListItem("Pre", "0"));
            ddlSeasonType.Items.Add(new ListItem("Reg", "1"));
        }

        private void GetPlayerStats()
        {
            var stageIndex = Helper.IntegerNull(Request.QueryString["type"]);
            var season = Helper.IntegerNull(Request.QueryString["season"]);
            var week = Helper.IntegerNull(Request.QueryString["week"]);
            var teamId = Helper.IntegerNull(Request.QueryString["id"]);
            var leagueId = Helper.IntegerNull(Session["leagueId"]);

            var items = GetScheduleId(season, week, teamId, stageIndex, leagueId);

            var homeTeamId = 0;
            var awayTeamId = 0;

            foreach(var i in items)
            {
                if(i.Key == "homeTeamId")
                {
                    homeTeamId = i.Value;
                }

                if(i.Key == "awayTeamId")
                {
                    awayTeamId = i.Value;
                }
            }

            GetPlayerStats(awayTeamId, homeTeamId);
        }

        private void GetPlayerStats(int awayTeamId, int homeTeamId)
        {
            try
            {
                getAwayStats(awayTeamId);
            }
            catch(Exception ex)
            {
                ShowAlert("danger", "Away", ex.Message);
            }

            try
            {
                getHomeStats(homeTeamId);
            }
            catch(Exception ex)
            {
                ShowAlert("danger", "Home", ex.Message);
            }
        }

        private Dictionary<string, int> GetScheduleId
        (
            int seasonIndex,
            int weekIndex,
            int teamid,
            int stageIndex,
            int leagueId
        )
        {
            var sp = new StoredProc
            {
                Name = "ScheduleId_select", DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, ParameterSet = new SqlCommand()
            };

            sp.ParameterSet.Parameters.AddWithValue("@stageIndex", stageIndex);
            sp.ParameterSet.Parameters.AddWithValue("@seasonIndex", seasonIndex);
            sp.ParameterSet.Parameters.AddWithValue("@teamId", teamid);
            sp.ParameterSet.Parameters.AddWithValue("@weekindex", weekIndex);
            sp.ParameterSet.Parameters.AddWithValue("@leagueId", leagueId);

            var ds = StoredProc.ShowMeTheData(sp);

            if(ds.Tables.Count == 0)
            {
                return null;
            }

            var dict = new Dictionary<string, int>();

            foreach(DataRow row in ds.Tables[0].Rows)
            {
                dict.Add("scheduleId", Helper.IntegerNull(row["scheduleId"]));
                dict.Add("homeTeamId", Helper.IntegerNull(row["homeTeamId"]));
                dict.Add("awayTeamId", Helper.IntegerNull(row["awayTeamId"]));
                break;
            }

            return dict;
        }

        private void getStarted()
        {
            var stageIndex = Helper.IntegerNull(Request.QueryString["type"]);
            var season = Helper.IntegerNull(Request.QueryString["season"]);
            var week = Helper.IntegerNull(Request.QueryString["week"]);
            var teamId = Helper.IntegerNull(Request.QueryString["id"]);
            var leagueId = Helper.IntegerNull(Session["leagueId"]);

            if(teamId > 0)
            {
                ddlSeason.SelectedIndex = ddlSeason.Items.IndexOf(ddlSeason.Items.FindByValue(Helper.StringNull(season)));

                ddlWeek.SelectedIndex = ddlWeek.Items.IndexOf(ddlWeek.Items.FindByValue(Helper.StringNull(week)));

                ddlLeagueTeams.SelectedIndex = ddlLeagueTeams.Items.IndexOf(ddlLeagueTeams.Items.FindByValue(Helper.StringNull(teamId)));

                ddlSeasonType.SelectedIndex = ddlSeasonType.Items.IndexOf(ddlSeasonType.Items.FindByValue(Helper.StringNull(stageIndex)));

                getBoxScore(season, week, teamId, stageIndex, leagueId);

                var items = GetScheduleId(season, week, teamId, stageIndex, leagueId);

                GetTeamStats(items, season, week, stageIndex, leagueId);

                //GetPlayerStats();
            }
        }

        private void GetTeamStats
        (
            Dictionary<string, int> things,
            int seasonIndex,
            int weekIndex,
            int stageIndex,
            int leagueId
        )
        {
            tableTeamStats.InnerHtml = string.Empty;

            var awayTeamId = 0;
            var homeTeamId = 0;
            var scheduleId = 0;

            foreach(var i in things)
            {
                if(i.Key == "homeTeamId")
                {
                    homeTeamId = i.Value;
                }

                if(i.Key == "awayTeamId")
                {
                    awayTeamId = i.Value;
                }

                if(i.Key == "scheduleId")
                {
                    scheduleId = i.Value;
                }
            }

            var sp = new StoredProc
            {
                Name = "BoxScoreTeamStats_select", DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, ParameterSet = new SqlCommand()
            };

            sp.ParameterSet.Parameters.AddWithValue("@stageIndex", stageIndex);
            sp.ParameterSet.Parameters.AddWithValue("@seasonIndex", seasonIndex);
            sp.ParameterSet.Parameters.AddWithValue("@weekindex", weekIndex);
            sp.ParameterSet.Parameters.AddWithValue("@scheduleId", scheduleId);
            sp.ParameterSet.Parameters.AddWithValue("@leagueId", leagueId);

            var ds = StoredProc.ShowMeTheData(sp);

            if(ds.Tables.Count == 0)
            {
                return;
            }

            var stats = new BoxScoreTeamStats();

            foreach(DataRow row in ds.Tables[0].Rows)
            {
                if(Helper.IntegerNull(row["teamId"]) == homeTeamId)
                {
                    stats.HomeTotalYards = Helper.IntegerNull(row["offTotalYds"]);
                    stats.HomeRushYards = Helper.IntegerNull(row["offRushYds"]);
                    stats.HomePassYards = Helper.IntegerNull(row["offPassYds"]);
                    stats.HomeTotalYardsGained = Helper.IntegerNull(row["offTotalYdsGained"]);

                    stats.HomeFirstDowns = Helper.IntegerNull(row["off1stDowns"]);

                    stats.HomeTODifferential =
                        $"{Helper.IntegerNull(row["tOGiveaways"])} ({Helper.IntegerNull(row["tODiff"])})";

                    var off3RdDownAtt = Helper.IntegerNull(row["off3rdDownAtt"]);
                    var off3RdDownConv = Helper.IntegerNull(row["off3rdDownConv"]);
                    stats.HomeThirdDowns =
                        $"{off3RdDownConv} - {off3RdDownAtt} ({Helper.GetPercent(off3RdDownAtt, off3RdDownConv)})";

                    var off4ThDownAtt = Helper.IntegerNull(row["off4thDownAtt"]);
                    var off4ThDownConv = Helper.IntegerNull(row["off4thDownConv"]);
                    stats.HomeFourthDowns =
                        $"{off4ThDownConv} - {off4ThDownAtt} ({Helper.GetPercent(off4ThDownAtt, off4ThDownConv)})";

                    var offRedZoneTDs = Helper.IntegerNull(row["offRedZoneTDs"]);
                    var offRedZoneFGs = Helper.IntegerNull(row["offRedZoneFGs"]);
                    var offRedZones = Helper.IntegerNull(row["offRedZones"]);
                    stats.HomeRedZones =
                        $"{offRedZoneFGs + offRedZoneTDs} - {offRedZones} ({Helper.GetPercent(offRedZones, offRedZoneFGs + offRedZoneTDs)})";
                    stats.HomeRedZoneFGs = offRedZoneFGs;
                    stats.HomeRedZoneTDs = offRedZoneTDs;

                    stats.HomePenalties =
                        $"{Helper.IntegerNull(row["penalties"])} - {Helper.IntegerNull(row["penaltyYds"])}";

                    stats.HomeSacks = Helper.IntegerNull(row["defSacks"]);
                    stats.HomeInterceptions = Helper.IntegerNull(row["defIntsRec"]);
                    stats.HomePassTDs = Helper.IntegerNull(row["offPassTDs"]);
                    stats.HomeRushTDs = Helper.IntegerNull(row["offRushTDs"]);
                    stats.HomeTotalTDs = stats.HomeRushTDs + stats.HomePassTDs;
                }

                if(Helper.IntegerNull(row["teamId"]) == awayTeamId)
                {
                    stats.AwayTotalYards = Helper.IntegerNull(row["offTotalYds"]);
                    stats.AwayRushYards = Helper.IntegerNull(row["offRushYds"]);
                    stats.AwayPassYards = Helper.IntegerNull(row["offPassYds"]);
                    stats.AwayTotalYardsGained = Helper.IntegerNull(row["offTotalYdsGained"]);

                    stats.AwayFirstDowns = Helper.IntegerNull(row["off1stDowns"]);

                    stats.AwayTODifferential =
                        $"{Helper.IntegerNull(row["tOGiveaways"])} ({Helper.IntegerNull(row["tODiff"])})";

                    var off3RdDownAtt = Helper.IntegerNull(row["off3rdDownAtt"]);
                    var off3RdDownConv = Helper.IntegerNull(row["off3rdDownConv"]);
                    stats.AwayThirdDowns =
                        $"{off3RdDownConv} - {off3RdDownAtt} ({Helper.GetPercent(off3RdDownAtt, off3RdDownConv)})";

                    var off4ThDownAtt = Helper.IntegerNull(row["off4thDownAtt"]);
                    var off4ThDownConv = Helper.IntegerNull(row["off4thDownConv"]);
                    stats.AwayFourthDowns =
                        $"{off4ThDownConv} - {off4ThDownAtt} ({Helper.GetPercent(off4ThDownAtt, off4ThDownConv)})";

                    var offRedZoneTDs = Helper.IntegerNull(row["offRedZoneTDs"]);
                    var offRedZoneFGs = Helper.IntegerNull(row["offRedZoneFGs"]);
                    var offRedZones = Helper.IntegerNull(row["offRedZones"]);
                    stats.AwayRedZones =
                        $"{offRedZoneFGs + offRedZoneTDs} - {offRedZones} ({Helper.GetPercent(offRedZones, offRedZoneFGs + offRedZoneTDs)})";
                    stats.AwayRedZoneFGs = offRedZoneFGs;
                    stats.AwayRedZoneTDs = offRedZoneTDs;

                    stats.AwayPenalties =
                        $"{Helper.IntegerNull(row["penalties"])} - {Helper.IntegerNull(row["penaltyYds"])}";

                    stats.AwaySacks = Helper.IntegerNull(row["defSacks"]);
                    stats.AwayInterceptions = Helper.IntegerNull(row["defIntsRec"]);
                    stats.AwayPassTDs = Helper.IntegerNull(row["offPassTDs"]);
                    stats.AwayRushTDs = Helper.IntegerNull(row["offRushTDs"]);
                    stats.AwayTotalTDs = stats.AwayRushTDs + stats.AwayPassTDs;
                }
            }

            var sb = new StringBuilder();

            sb.Append("<table class='table table-striped table-hover table-bordered'><thead></thead><tbody>");

            sb.Append(
                $"<tr><td class='stat'>{stats.AwayTotalYards}</td> <td class='statName'>Total Offense Yards</td> <td class='stat'>{stats.HomeTotalYards}</td> </tr>");
            sb.Append(
                $"<tr><td class='stat'>{stats.AwayRushYards}</td> <td class='statName'>Rush Yards</td> <td class='stat'>{stats.HomeRushYards}</td> </tr>");
            sb.Append(
                $"<tr><td class='stat'>{stats.AwayPassYards}</td> <td class='statName'>Pass Yards</td> <td class='stat'>{stats.HomePassYards}</td> </tr>");
            sb.Append(
                $"<tr><td class='stat'>{stats.AwayTotalYardsGained}</td> <td class='statName'>Total Yards Gained</td> <td class='stat'>{stats.HomeTotalYardsGained}</td> </tr>");

            sb.Append(
                $"<tr><td class='stat'>{stats.AwayFirstDowns}</td> <td class='statName'>First Downs</td> <td class='stat'>{stats.HomeFirstDowns}</td> </tr>");

            sb.Append(
                $"<tr><td class='stat'>{stats.AwayTODifferential}</td> <td class='statName'>Turnover Diff</td> <td class='stat'>{stats.HomeTODifferential}</td> </tr>");

            sb.Append(
                $"<tr><td class='stat'>{stats.AwayThirdDowns}</td> <td class='statName'>3rd Down</td> <td class='stat'>{stats.HomeThirdDowns}</td> </tr>");
            sb.Append(
                $"<tr><td class='stat'>{stats.AwayFourthDowns}</td> <td class='statName'>4th Down</td> <td class='stat'>{stats.HomeFourthDowns}</td> </tr>");

            sb.Append(
                $"<tr><td class='stat'>{stats.AwayRedZones}</td> <td class='statName'>Red Zone</td> <td class='stat'>{stats.HomeRedZones}</td> </tr>");
            sb.Append(
                $"<tr><td class='stat'>{stats.AwayRedZoneFGs}</td> <td class='statName'>Red Zone FGs</td> <td class='stat'>{stats.HomeRedZoneFGs}</td> </tr>");
            sb.Append(
                $"<tr><td class='stat'>{stats.AwayRedZoneTDs}</td> <td class='statName'>Red Zone TDs</td> <td class='stat'>{stats.HomeRedZoneTDs}</td> </tr>");

            sb.Append(
                $"<tr><td class='stat'>{stats.AwayPenalties}</td> <td class='statName'>Penalties</td> <td class='stat'>{stats.HomePenalties}</td> </tr>");

            sb.Append(
                $"<tr><td class='stat'>{stats.AwaySacks}</td> <td class='statName'>Sacks</td> <td class='stat'>{stats.HomeSacks}</td> </tr>");
            sb.Append(
                $"<tr><td class='stat'>{stats.AwayInterceptions}</td> <td class='statName'>Interceptions</td> <td class='stat'>{stats.HomeInterceptions}</td> </tr>");

            sb.Append(
                $"<tr><td class='stat'>{stats.AwayRushTDs}</td> <td class='statName'>Rush TDs</td> <td class='stat'>{stats.HomeRushTDs}</td> </tr>");
            sb.Append(
                $"<tr><td class='stat'>{stats.AwayPassTDs}</td> <td class='statName'>Pass TDs</td> <td class='stat'>{stats.HomePassTDs}</td> </tr>");
            sb.Append(
                $"<tr><td class='stat'>{stats.AwayTotalTDs}</td> <td class='statName'>Total Offensive TDs</td> <td class='stat'>{stats.HomeTotalTDs}</td> </tr>");

            sb.Append("</tbody></table>");

            tableTeamStats.InnerHtml = sb.ToString();
        }

        private void ShowAlert(string css, string header, string details)
        {
            //Regex rgx = new Regex("[^a-zA-Z0-9 -]");
            //details = rgx.Replace(details, "");

            //ShowAlert("warning", "userInfo", "", "Please click the '...' button on the right to add items to your list.");
            pnlMessage.Visible = true;
            pnlMessage.CssClass = string.Format("alert alert-{0}", css);
            lblAlert.Text = string.Format("<strong>{0}</strong> {1}", header, details);

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalAlert('" + details + "','" + pnlMessage.CssClass + "');", true);
        }
    }
}