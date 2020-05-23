using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CFMStats.Classes;
using CFMStats.Controls;

namespace CFMStats
{
    public partial class WeekLeaders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                int ileagueId = Helper.IntegerNull(Session["leagueId"]);
                if (ileagueId == 0) { Response.Redirect("~/"); }

                buildSeasonList(ileagueId);
                getCurrentSeasonWeek(ileagueId);
            }
        }


        protected void ddlWeek_SelectedIndexChanged(object sender, EventArgs e)
        {
            int iStageIndex = Helper.IntegerNull(ddlSeasonType.SelectedItem.Value);
            

            getPassingStats(iStageIndex);
            getRushingStats(iStageIndex);
            getReceivingStats(iStageIndex);
            getDefenseStats(iStageIndex);
            getKickingStats(iStageIndex);
            getPuntingStats(iStageIndex);

        }


        protected void getCurrentSeasonWeek(int leagueId)
        {
            StoredProc SP = new StoredProc();
            SP.Name = "ScheduleCurrentWeek_select";
            SP.DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SP.ParameterSet = new System.Data.SqlClient.SqlCommand();

            SP.ParameterSet.Parameters.AddWithValue("@leagueId", leagueId);

            DataSet ds = StoredProc.ShowMeTheData(SP);

            if (ds.Tables[0].Rows.Count < 1)
            {
                ddlWeek.SelectedIndex = 16;
            }

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                ddlSeason.SelectedIndex = ddlSeason.Items.IndexOf(ddlSeason.Items.FindByValue(Helper.StringNull(item["seasonIndex"])));

                int currentWeek = Helper.IntegerNull(item["weekIndex"]);
                if(currentWeek > 0) { currentWeek = currentWeek - 1; }

                ddlWeek.SelectedIndex = ddlWeek.Items.IndexOf(ddlWeek.Items.FindByValue(Helper.StringNull(currentWeek)));


            }

            ddlWeek_SelectedIndexChanged(null, null);
        }


        public void getPassingStats(int stageIndex)
        {
            UserControl uc = (UserControl)Page.LoadControl("~/Controls/ucPassingStats.ascx");

            string ClientId = uc.ClientID;
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            ucPassingStats myUsercontrol = FindControl(ClientId) as ucPassingStats;
            myUsercontrol.StageIndex = stageIndex;
            myUsercontrol.Season = Helper.IntegerNull(ddlSeason.SelectedItem.Value);
            myUsercontrol.Week = Helper.IntegerNull(ddlWeek.SelectedItem.Value);
            myUsercontrol.Top = 5;
            myUsercontrol.iLeagueId = Helper.IntegerNull(Session["leagueID"]);
            myUsercontrol.isFull = false;

            phPassing.Controls.Add(uc);
        }





        public void getRushingStats(int stageIndex)
        {
            UserControl uc = (UserControl)Page.LoadControl("~/Controls/ucRushingStats.ascx");

            string ClientId = uc.ClientID;
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            ucRushingStats myUsercontrol = FindControl(ClientId) as ucRushingStats;
            myUsercontrol.iStageIndex = stageIndex;
            myUsercontrol.iSeason = Helper.IntegerNull(ddlSeason.SelectedItem.Value);
            myUsercontrol.iWeek = Helper.IntegerNull(ddlWeek.SelectedItem.Value);
            myUsercontrol.iTop = 5;
            myUsercontrol.iLeagueId = Helper.IntegerNull(Session["leagueID"]);
            myUsercontrol.isFull = false;

            phRushing.Controls.Add(uc);

        }



        public void getReceivingStats(int stageIndex)
        {
            UserControl uc = (UserControl)Page.LoadControl("~/Controls/ucReceivingStats.ascx");

            string ClientId = uc.ClientID; // string.Format("{0}{1}", uc.ClientID, DateTime.Now.ToString("fff"));
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            ucReceivingStats myUsercontrol = FindControl(ClientId) as ucReceivingStats;
            myUsercontrol.iStageIndex = stageIndex;
            myUsercontrol.iSeason = Helper.IntegerNull(ddlSeason.SelectedItem.Value);
            myUsercontrol.iWeek = Helper.IntegerNull(ddlWeek.SelectedItem.Value);
            myUsercontrol.iTop = 5;
            myUsercontrol.iLeagueId = Helper.IntegerNull(Session["leagueID"]);
            myUsercontrol.isFull = false;

            phReceiving.Controls.Add(uc);

        }

        public void getDefenseStats(int stageIndex)
        {
            UserControl uc = (UserControl)Page.LoadControl("~/Controls/ucDefenseStats.ascx");

            string ClientId = uc.ClientID;
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            ucDefenseStats myUsercontrol = FindControl(ClientId) as ucDefenseStats;
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
            UserControl uc = (UserControl)Page.LoadControl("~/Controls/ucKickingStats.ascx");

            string ClientId = uc.ClientID;
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            ucKickingStats myUsercontrol = FindControl(ClientId) as ucKickingStats;
            myUsercontrol.iStageIndex = stageIndex;
            myUsercontrol.iSeason = Helper.IntegerNull(ddlSeason.SelectedItem.Value);
            myUsercontrol.iWeek = Helper.IntegerNull(ddlWeek.SelectedItem.Value);
            myUsercontrol.iTop = 5;
            myUsercontrol.iLeagueId = Helper.IntegerNull(Session["leagueID"]);
            myUsercontrol.isFull = false;

            phKicking.Controls.Add(uc);
        }


        public void getPuntingStats(int stageIndex)
        {
            UserControl uc = (UserControl)Page.LoadControl("~/Controls/ucPuntingStats.ascx");

            string ClientId = uc.ClientID;
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            ucPuntingStats myUsercontrol = FindControl(ClientId) as ucPuntingStats;
            myUsercontrol.iStageIndex = stageIndex;
            myUsercontrol.iSeason = Helper.IntegerNull(ddlSeason.SelectedItem.Value);
            myUsercontrol.iWeek = Helper.IntegerNull(ddlWeek.SelectedItem.Value);
            myUsercontrol.iTop = 5;
            myUsercontrol.iLeagueId = Helper.IntegerNull(Session["leagueID"]);
            myUsercontrol.isFull = false;

            phPunting.Controls.Add(uc);
        }

        protected void buildSeasonList(int leagueId)
        {
            oSeasons season = new oSeasons();
            season = season.getSeasons(leagueId);

            if(season.Count == 0) { Response.Redirect("~/"); }

            foreach (oSeason item in season.Values)
                ddlSeason.Items.Add(new ListItem(item.Year.ToString(), item.ID.ToString()));

        }

    }
}