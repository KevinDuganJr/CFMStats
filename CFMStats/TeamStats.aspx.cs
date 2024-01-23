using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using CFMStats.Classes;
using CFMStats.Controls;

namespace CFMStats
{
    public partial class TeamStats : Page
    {
        public void getConversionStats(int stageIndex)
        {
            var uc = (UserControl)Page.LoadControl("~/Controls/ucTeamConversion.ascx");

            var ClientId = uc.ClientID;
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            var myUsercontrol = FindControl(ClientId) as ucTeamConversion;
            myUsercontrol.iStageIndex = stageIndex;
            myUsercontrol.iSeason = Helper.IntegerNull(ddlSeason.SelectedItem.Value);
            myUsercontrol.iWeek = Helper.IntegerNull(ddlWeek.SelectedItem.Value);
            myUsercontrol.iLeagueId = Helper.IntegerNull(Session["leagueID"]);

            phStatHolder.Controls.Add(uc);
        }

        public void getDefenseStats(int stageIndex)
        {
            var uc = (UserControl)Page.LoadControl("~/Controls/ucTeamDefense.ascx");

            var ClientId = uc.ClientID;
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            var myUsercontrol = FindControl(ClientId) as ucTeamDefense;
            myUsercontrol.iStageIndex = stageIndex;
            myUsercontrol.iSeason = Helper.IntegerNull(ddlSeason.SelectedItem.Value);
            myUsercontrol.iWeek = Helper.IntegerNull(ddlWeek.SelectedItem.Value);
            myUsercontrol.iLeagueId = Helper.IntegerNull(Session["leagueID"]);

            phStatHolder.Controls.Add(uc);
        }

        public void getOffenseStats(int stageIndex)
        {
            var uc = (UserControl)Page.LoadControl("~/Controls/ucTeamOffense.ascx");

            var ClientId = uc.ClientID;
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            var myUsercontrol = FindControl(ClientId) as ucTeamOffense;
            myUsercontrol.iStageIndex = stageIndex;
            myUsercontrol.iSeason = Helper.IntegerNull(ddlSeason.SelectedItem.Value);
            myUsercontrol.iWeek = Helper.IntegerNull(ddlWeek.SelectedItem.Value);
            myUsercontrol.iLeagueId = Helper.IntegerNull(Session["leagueID"]);

            phStatHolder.Controls.Add(uc);
        }

        public void getPenaltyStats(int stageIndex)
        {
            var uc = (UserControl)Page.LoadControl("~/Controls/ucTeamPenalty.ascx");

            var ClientId = uc.ClientID;
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            var myUsercontrol = FindControl(ClientId) as ucTeamPenalty;
            myUsercontrol.iStageIndex = stageIndex;
            myUsercontrol.iSeason = Helper.IntegerNull(ddlSeason.SelectedItem.Value);
            myUsercontrol.iWeek = Helper.IntegerNull(ddlWeek.SelectedItem.Value);
            myUsercontrol.iLeagueId = Helper.IntegerNull(Session["leagueID"]);

            phStatHolder.Controls.Add(uc);
        }

        public void getRedZoneStats(int stageIndex)
        {
            var uc = (UserControl)Page.LoadControl("~/Controls/ucTeamRedZone.ascx");

            var ClientId = uc.ClientID;
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            var myUsercontrol = FindControl(ClientId) as ucTeamRedZone;
            myUsercontrol.iStageIndex = stageIndex;
            myUsercontrol.iSeason = Helper.IntegerNull(ddlSeason.SelectedItem.Value);
            myUsercontrol.iWeek = Helper.IntegerNull(ddlWeek.SelectedItem.Value);
            myUsercontrol.iLeagueId = Helper.IntegerNull(Session["leagueID"]);

            phStatHolder.Controls.Add(uc);
        }

        public void getTurnoverStats(int stageIndex)
        {
            var uc = (UserControl)Page.LoadControl("~/Controls/ucTeamTurnovers.ascx");

            var ClientId = uc.ClientID;
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            var myUsercontrol = FindControl(ClientId) as ucTeamTurnovers;
            myUsercontrol.iStageIndex = stageIndex;
            myUsercontrol.iSeason = Helper.IntegerNull(ddlSeason.SelectedItem.Value);
            myUsercontrol.iWeek = Helper.IntegerNull(ddlWeek.SelectedItem.Value);
            myUsercontrol.iLeagueId = Helper.IntegerNull(Session["leagueID"]);

            phStatHolder.Controls.Add(uc);
        }

        protected void ddlWeek_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(phStatHolder.Controls.Count > 0)
            {
                phStatHolder.Controls.Clear();
            }

            var stageIndex = Helper.IntegerNull(ddlSeasonType.SelectedItem.Value);

            switch(ddlStatSelector.SelectedItem.Value)
            {
                case "offense":
                    getOffenseStats(stageIndex);
                    break;

                case "defense":
                    getDefenseStats(stageIndex);
                    break;

                case "conversion":
                    getConversionStats(stageIndex);
                    break;

                case "turnovers":
                    getTurnoverStats(stageIndex);
                    break;

                case "redzone":
                    getRedZoneStats(stageIndex);
                    break;

                case "penalty":
                    getPenaltyStats(stageIndex);
                    break;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(IsPostBack)
            {
                return;
            }

            if(Helper.StringNull(Request.QueryString["leagueId"]).Length == 0)
            {
                // no league, go back to start
                Response.Redirect("~/");
            }

            Session["leagueId"] = Helper.StringNull(Request.QueryString["leagueId"]);

            SetWeek();
            SetSeasonList();
            ddlWeek_SelectedIndexChanged(null, null);
        }

        protected void SetSeasonList()
        {
            var season = new oSeasons();
            season = season.getSeasons(Helper.IntegerNull(Session["leagueId"]));

            if(season.Count == 0)
            {
                Response.Redirect("~/");
            }

            foreach(var item in season.Values)
            {
                ddlSeason.Items.Add(new ListItem(item.Year.ToString(), item.ID.ToString()));
            }
        }

        protected void SetWeek()
        {
            var wks = new oWeeks();
            wks = wks.Regular();

            ddlWeek.Items.Clear();

            ddlWeek.Items.Add(new ListItem("All", "99"));

            foreach(var item in wks.Values)
            {
                ddlWeek.Items.Add(new ListItem(item.Text, item.WeekIndex.ToString()));
            }
        }
    }
}