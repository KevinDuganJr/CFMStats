using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CFMStats.Classes;
using CFMStats.Controls;

namespace CFMStats
{
    public partial class TeamStats : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Helper.IntegerNull(Session["leagueId"]) == 0) { Response.Redirect("~/"); }
        }


        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                buildSeasonList();
                ddlWeek_SelectedIndexChanged(null, null);
            }
        }

        protected void ddlWeek_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (phStatHolder.Controls.Count > 0) { phStatHolder.Controls.Clear(); }

            int stageIndex = Helper.IntegerNull(ddlSeasonType.SelectedItem.Value);

            switch (ddlStatSelector.SelectedItem.Value)
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


                default:
                    break;
            }




        }

        public void getOffenseStats(int stageIndex)
        {
            UserControl uc = (UserControl)Page.LoadControl("~/Controls/ucTeamOffense.ascx");

            string ClientId = uc.ClientID;
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            ucTeamOffense myUsercontrol = FindControl(ClientId) as ucTeamOffense;
            myUsercontrol.iStageIndex = stageIndex;
            myUsercontrol.iSeason = Helper.IntegerNull(ddlSeason.SelectedItem.Value);
            myUsercontrol.iWeek = Helper.IntegerNull(ddlWeek.SelectedItem.Value);
            myUsercontrol.iLeagueId = Helper.IntegerNull(Session["leagueID"]);

            phStatHolder.Controls.Add(uc);
        }
        

        public void getDefenseStats(int stageIndex)
        {
            UserControl uc = (UserControl)Page.LoadControl("~/Controls/ucTeamDefense.ascx");

            string ClientId = uc.ClientID;  
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            ucTeamDefense myUsercontrol = FindControl(ClientId) as ucTeamDefense;
            myUsercontrol.iStageIndex = stageIndex;
            myUsercontrol.iSeason = Helper.IntegerNull(ddlSeason.SelectedItem.Value);
            myUsercontrol.iWeek = Helper.IntegerNull(ddlWeek.SelectedItem.Value);
            myUsercontrol.iLeagueId = Helper.IntegerNull(Session["leagueID"]);

            phStatHolder.Controls.Add(uc);
        }

        public void getConversionStats(int stageIndex)
        {
            UserControl uc = (UserControl)Page.LoadControl("~/Controls/ucTeamConversion.ascx");

            string ClientId = uc.ClientID;
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            ucTeamConversion myUsercontrol = FindControl(ClientId) as ucTeamConversion;
            myUsercontrol.iStageIndex = stageIndex;
            myUsercontrol.iSeason = Helper.IntegerNull(ddlSeason.SelectedItem.Value);
            myUsercontrol.iWeek = Helper.IntegerNull(ddlWeek.SelectedItem.Value);
            myUsercontrol.iLeagueId = Helper.IntegerNull(Session["leagueID"]);

            phStatHolder.Controls.Add(uc);
        }

        public void getTurnoverStats(int stageIndex)
        {
            UserControl uc = (UserControl)Page.LoadControl("~/Controls/ucTeamTurnovers.ascx");

            string ClientId = uc.ClientID;
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            ucTeamTurnovers myUsercontrol = FindControl(ClientId) as ucTeamTurnovers;
            myUsercontrol.iStageIndex = stageIndex;
            myUsercontrol.iSeason = Helper.IntegerNull(ddlSeason.SelectedItem.Value);
            myUsercontrol.iWeek = Helper.IntegerNull(ddlWeek.SelectedItem.Value);
            myUsercontrol.iLeagueId = Helper.IntegerNull(Session["leagueID"]);

            phStatHolder.Controls.Add(uc);
        }

        public void getRedZoneStats(int stageIndex)
        {
            UserControl uc = (UserControl)Page.LoadControl("~/Controls/ucTeamRedZone.ascx");

            string ClientId = uc.ClientID;
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            ucTeamRedZone myUsercontrol = FindControl(ClientId) as ucTeamRedZone;
            myUsercontrol.iStageIndex = stageIndex;
            myUsercontrol.iSeason = Helper.IntegerNull(ddlSeason.SelectedItem.Value);
            myUsercontrol.iWeek = Helper.IntegerNull(ddlWeek.SelectedItem.Value);
            myUsercontrol.iLeagueId = Helper.IntegerNull(Session["leagueID"]);

            phStatHolder.Controls.Add(uc);
        }
        
        public void getPenaltyStats(int stageIndex)
        {
            UserControl uc = (UserControl)Page.LoadControl("~/Controls/ucTeamPenalty.ascx");

            string ClientId = uc.ClientID;
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            ucTeamPenalty myUsercontrol = FindControl(ClientId) as ucTeamPenalty;
            myUsercontrol.iStageIndex = stageIndex;
            myUsercontrol.iSeason = Helper.IntegerNull(ddlSeason.SelectedItem.Value);
            myUsercontrol.iWeek = Helper.IntegerNull(ddlWeek.SelectedItem.Value);
            myUsercontrol.iLeagueId = Helper.IntegerNull(Session["leagueID"]);

            phStatHolder.Controls.Add(uc);
        }
        
        protected void buildSeasonList()
        {
            oSeasons season = new oSeasons();
            season = season.getSeasons(Helper.IntegerNull(Session["leagueID"]));
            if (season.Count == 0) { Response.Redirect("~/"); }

            foreach (oSeason item in season.Values)
                ddlSeason.Items.Add(new ListItem(item.Year.ToString(), item.ID.ToString()));

        }
    }
}