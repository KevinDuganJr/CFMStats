using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using CFMStats.Classes;
using CFMStats.Controls;

namespace CFMStats
{
    public partial class WeekLeaders : Page
    {
        public void getDefenseStats(int stageIndex)
        {
            var uc = (UserControl) Page.LoadControl("~/Controls/ucDefenseStats.ascx");

            var ClientId = uc.ClientID;
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            var myUsercontrol = FindControl(ClientId) as ucDefenseStats;
            myUsercontrol.iStageIndex = stageIndex;
            myUsercontrol.iSeason = Helper.IntegerNull(ddlSeason.SelectedItem.Value);
            myUsercontrol.iWeek = Helper.IntegerNull(ddlWeek.SelectedItem.Value);
            myUsercontrol.iTop = 5;
            myUsercontrol.iLeagueId = Helper.IntegerNull(Session["leagueID"]);
            myUsercontrol.isFull = false;

            phDefense.Controls.Add(uc);
        }

        public void getKickingStats(int stageIndex)
        {
            var uc = (UserControl) Page.LoadControl("~/Controls/ucKickingStats.ascx");

            var ClientId = uc.ClientID;
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            var myUsercontrol = FindControl(ClientId) as ucKickingStats;
            myUsercontrol.iStageIndex = stageIndex;
            myUsercontrol.iSeason = Helper.IntegerNull(ddlSeason.SelectedItem.Value);
            myUsercontrol.iWeek = Helper.IntegerNull(ddlWeek.SelectedItem.Value);
            myUsercontrol.iTop = 5;
            myUsercontrol.iLeagueId = Helper.IntegerNull(Session["leagueID"]);
            myUsercontrol.isFull = false;

            phKicking.Controls.Add(uc);
        }

        public void getPassingStats(int stageIndex)
        {
            var uc = (UserControl) Page.LoadControl("~/Controls/ucPassingStats.ascx");

            var ClientId = uc.ClientID;
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            var myUsercontrol = FindControl(ClientId) as ucPassingStats;
            myUsercontrol.StageIndex = stageIndex;
            myUsercontrol.Season = Helper.IntegerNull(ddlSeason.SelectedItem.Value);
            myUsercontrol.Week = Helper.IntegerNull(ddlWeek.SelectedItem.Value);
            myUsercontrol.Top = 5;
            myUsercontrol.iLeagueId = Helper.IntegerNull(Session["leagueID"]);
            myUsercontrol.isFull = false;

            phPassing.Controls.Add(uc);
        }

        public void getPuntingStats(int stageIndex)
        {
            var uc = (UserControl) Page.LoadControl("~/Controls/ucPuntingStats.ascx");

            var ClientId = uc.ClientID;
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            var myUsercontrol = FindControl(ClientId) as ucPuntingStats;
            myUsercontrol.iStageIndex = stageIndex;
            myUsercontrol.iSeason = Helper.IntegerNull(ddlSeason.SelectedItem.Value);
            myUsercontrol.iWeek = Helper.IntegerNull(ddlWeek.SelectedItem.Value);
            myUsercontrol.iTop = 5;
            myUsercontrol.iLeagueId = Helper.IntegerNull(Session["leagueID"]);
            myUsercontrol.isFull = false;

            phPunting.Controls.Add(uc);
        }

        public void getReceivingStats(int stageIndex)
        {
            var uc = (UserControl) Page.LoadControl("~/Controls/ucReceivingStats.ascx");

            var ClientId = uc.ClientID; // string.Format("{0}{1}", uc.ClientID, DateTime.Now.ToString("fff"));
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            var myUsercontrol = FindControl(ClientId) as ucReceivingStats;
            myUsercontrol.iStageIndex = stageIndex;
            myUsercontrol.iSeason = Helper.IntegerNull(ddlSeason.SelectedItem.Value);
            myUsercontrol.iWeek = Helper.IntegerNull(ddlWeek.SelectedItem.Value);
            myUsercontrol.iTop = 5;
            myUsercontrol.iLeagueId = Helper.IntegerNull(Session["leagueID"]);
            myUsercontrol.isFull = false;

            phReceiving.Controls.Add(uc);
        }

        public void getRushingStats(int stageIndex)
        {
            var uc = (UserControl) Page.LoadControl("~/Controls/ucRushingStats.ascx");

            var ClientId = uc.ClientID;
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            var myUsercontrol = FindControl(ClientId) as ucRushingStats;
            myUsercontrol.iStageIndex = stageIndex;
            myUsercontrol.iSeason = Helper.IntegerNull(ddlSeason.SelectedItem.Value);
            myUsercontrol.iWeek = Helper.IntegerNull(ddlWeek.SelectedItem.Value);
            myUsercontrol.iTop = 5;
            myUsercontrol.iLeagueId = Helper.IntegerNull(Session["leagueID"]);
            myUsercontrol.isFull = false;

            phRushing.Controls.Add(uc);
        }

        protected void buildSeasonList(int leagueId)
        {
            var season = new oSeasons();
            season = season.getSeasons(leagueId);

            if (season.Count == 0)
            {
                Response.Redirect("~/");
            }

            foreach (var item in season.Values)
            {
                ddlSeason.Items.Add(new ListItem(item.Year.ToString(), item.ID.ToString()));
            }
        }

        protected void ddlWeek_SelectedIndexChanged(object sender, EventArgs e)
        {
            var iStageIndex = Helper.IntegerNull(ddlSeasonType.SelectedItem.Value);

            getPassingStats(iStageIndex);
            getRushingStats(iStageIndex);
            getReceivingStats(iStageIndex);
            getDefenseStats(iStageIndex);
            getKickingStats(iStageIndex);
            getPuntingStats(iStageIndex);
        }

        protected void getCurrentSeasonWeek(int leagueId)
        {
            var SP = new StoredProc();
            SP.Name = "ScheduleCurrentWeek_select";
            SP.DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SP.ParameterSet = new SqlCommand();

            SP.ParameterSet.Parameters.AddWithValue("@leagueId", leagueId);

            var ds = StoredProc.ShowMeTheData(SP);

            if (ds.Tables[0].Rows.Count < 1)
            {
                ddlWeek.SelectedIndex = 17;
            }

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                ddlSeason.SelectedIndex = ddlSeason.Items.IndexOf(ddlSeason.Items.FindByValue(Helper.StringNull(item["seasonIndex"])));

                var currentWeek = Helper.IntegerNull(item["weekIndex"]);

                switch (currentWeek)
                {
                    default:
                        break;
                }

                if (currentWeek > 0)
                {
                    if (currentWeek == 22) // Super Bowl
                    {
                        currentWeek = 20;
                    }
                    else
                    {
                        currentWeek -= 1;
                    }
                }

                ddlWeek.SelectedIndex = ddlWeek.Items.IndexOf(ddlWeek.Items.FindByValue(Helper.StringNull(currentWeek)));

                var stageIndex = Helper.IntegerNull(item["stageIndex"]);
                ddlSeasonType.SelectedIndex = ddlSeasonType.Items.IndexOf(ddlSeasonType.Items.FindByValue(Helper.StringNull(stageIndex)));
            }

            ddlWeek_SelectedIndexChanged(null, null);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var ileagueId = Helper.IntegerNull(Session["leagueId"]);
                if (ileagueId == 0)
                {
                    Response.Redirect("~/");
                }

                buildSeasonList(ileagueId);
                SetWeek();
                getCurrentSeasonWeek(ileagueId);
            }
        }

        protected void SetWeek()
        {
            var wks = new oWeeks();
            //switch (ddlSeasonType.SelectedIndex)
            //{
            //    case 1:
            //        wks = wks.pre();
            //        break;

            //    case 0:
            wks = wks.Regular();
            //        break;
            //}

            ddlWeek.Items.Clear();

            foreach (var item in wks.Values)
            {
                ddlWeek.Items.Add(new ListItem(item.Text, item.WeekIndex.ToString()));
            }

            //   GetCurrentSeasonWeek();
        }
    }
}