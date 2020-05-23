using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using CFMStats.Classes;
using CFMStats.Controls;

namespace CFMStats
{
    public partial class PlayerStats : Page
    {
        public void GetDefenseStats(int stageIndex)
        {
            var uc = (UserControl) Page.LoadControl("~/Controls/ucDefenseStats.ascx");

            var ClientId = uc.ClientID;
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            var myUsercontrol = FindControl(ClientId) as ucDefenseStats;
            myUsercontrol.iStageIndex = stageIndex;
            myUsercontrol.iSeason = Helper.IntegerNull(ddlSeason.SelectedItem.Value);
            myUsercontrol.iWeek = Helper.IntegerNull(ddlWeek.SelectedItem.Value);
            myUsercontrol.iTop = 0;
            myUsercontrol.isFull = true;
            myUsercontrol.iLeagueId = Helper.IntegerNull(Session["leagueId"]);

            phStatHolder.Controls.Add(uc);
        }

        public void GetKickingStats(int stageIndex)
        {
            var uc = (UserControl) Page.LoadControl("~/Controls/ucKickingStats.ascx");

            var ClientId = uc.ClientID;
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            var myUsercontrol = FindControl(ClientId) as ucKickingStats;
            myUsercontrol.iStageIndex = stageIndex;
            myUsercontrol.iSeason = Helper.IntegerNull(ddlSeason.SelectedItem.Value);
            myUsercontrol.iWeek = Helper.IntegerNull(ddlWeek.SelectedItem.Value);
            myUsercontrol.iTop = 0;
            myUsercontrol.isFull = true;
            myUsercontrol.iLeagueId = Helper.IntegerNull(Session["leagueId"]);

            phStatHolder.Controls.Add(uc);
        }

        public void GetPassingStats(int stageIndex)
        {
            var uc = (UserControl) Page.LoadControl("~/Controls/ucPassingStats.ascx");

            var ClientId = uc.ClientID;
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            var myUsercontrol = FindControl(ClientId) as ucPassingStats;
            myUsercontrol.StageIndex = stageIndex;
            myUsercontrol.Season = Helper.IntegerNull(ddlSeason.SelectedItem.Value);
            myUsercontrol.Week = Helper.IntegerNull(ddlWeek.SelectedItem.Value);
            myUsercontrol.Top = 0;
            myUsercontrol.isFull = true;
            myUsercontrol.iLeagueId = Helper.IntegerNull(Session["leagueId"]);

            phStatHolder.Controls.Add(uc);
        }

        public void GetPuntingStats(int stageIndex)
        {
            var uc = (UserControl) Page.LoadControl("~/Controls/ucPuntingStats.ascx");

            var ClientId = uc.ClientID;
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            var myUsercontrol = FindControl(ClientId) as ucPuntingStats;
            myUsercontrol.iStageIndex = stageIndex;
            myUsercontrol.iSeason = Helper.IntegerNull(ddlSeason.SelectedItem.Value);
            myUsercontrol.iWeek = Helper.IntegerNull(ddlWeek.SelectedItem.Value);
            myUsercontrol.iTop = 0;
            myUsercontrol.isFull = true;
            myUsercontrol.iLeagueId = Helper.IntegerNull(Session["leagueId"]);

            phStatHolder.Controls.Add(uc);
        }

        public void GetReceivingStats(int stageIndex)
        {
            var uc = (UserControl) Page.LoadControl("~/Controls/ucReceivingStats.ascx");

            var ClientId = uc.ClientID; // string.Format("{0}{1}", uc.ClientID, DateTime.Now.ToString("fff"));
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            var myUsercontrol = FindControl(ClientId) as ucReceivingStats;
            myUsercontrol.iStageIndex = stageIndex;
            myUsercontrol.iSeason = Helper.IntegerNull(ddlSeason.SelectedItem.Value);
            myUsercontrol.iWeek = Helper.IntegerNull(ddlWeek.SelectedItem.Value);
            myUsercontrol.iTop = 0;
            myUsercontrol.isFull = true;
            myUsercontrol.iLeagueId = Helper.IntegerNull(Session["leagueId"]);

            phStatHolder.Controls.Add(uc);
        }

        public void GetRushingStats(int stageIndex)
        {
            var uc = (UserControl) Page.LoadControl("~/Controls/ucRushingStats.ascx");

            var ClientId = uc.ClientID;
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            var myUsercontrol = FindControl(ClientId) as ucRushingStats;
            myUsercontrol.iStageIndex = stageIndex;
            myUsercontrol.iSeason = Helper.IntegerNull(ddlSeason.SelectedItem.Value);
            myUsercontrol.iWeek = Helper.IntegerNull(ddlWeek.SelectedItem.Value);
            myUsercontrol.iTop = 0;
            myUsercontrol.isFull = true;
            myUsercontrol.iLeagueId = Helper.IntegerNull(Session["leagueId"]);

            phStatHolder.Controls.Add(uc);
        }

        protected void ddlWeek_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (phStatHolder.Controls.Count > 0)
            {
                phStatHolder.Controls.Clear();
            }

            var stageIndex = Helper.IntegerNull(ddlSeasonType.SelectedItem.Value);

            switch (ddlStatSelector.SelectedItem.Value)
            {
                case"defense":
                    GetDefenseStats(stageIndex);
                    break;

                case"passing":
                    GetPassingStats(stageIndex);
                    break;

                case"receiving":
                    GetReceivingStats(stageIndex);
                    break;

                case"rushing":
                    GetRushingStats(stageIndex);
                    break;

                case"kicking":
                    GetKickingStats(stageIndex);
                    break;

                case"punting":
                    GetPuntingStats(stageIndex);
                    break;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Helper.StringNull(Request.QueryString["leagueId"]).Length == 0)
            {
                Response.Redirect("~/");
            }

            Session["leagueId"] = Helper.StringNull(Request.QueryString["leagueId"]);
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                return;
            }

            SetSeasonList();
            ddlWeek_SelectedIndexChanged(null, null);
        }

        protected void SetSeasonList()
        {
            var season = new oSeasons();
            season = season.getSeasons(Helper.IntegerNull(Session["leagueId"]));

            if (season.Count == 0)
            {
                Response.Redirect("~/");
            }

            foreach (var item in season.Values)
            {
                ddlSeason.Items.Add(new ListItem(item.Year.ToString(), item.ID.ToString()));
            }
        }
    }
}