using CFMStats.Controls.Records;
using System;
using System.Web.UI;
using CFMStats.Classes;

namespace CFMStats
{
    public partial class PlayerRecords : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Helper.StringNull(Request.QueryString["leagueId"]).Length == 0)
            {
                // no league, go back to start
                Response.Redirect("~/");
            }

            Session["leagueId"] = Helper.StringNull(Request.QueryString["leagueId"]);

        }

        protected void ddlDuration_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            FetchRecords();
        }


        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlStatSelector_OnSelectedIndexChanged(null, null);
            }
        }

        protected void ddlStatSelector_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            FetchRecords();
        }

        private void FetchRecords()
        {
            if (phStatHolder.Controls.Count > 0) { phStatHolder.Controls.Clear(); }

            var duration = Helper.StringNull(ddlDuration.SelectedItem.Value);

            switch (ddlStatSelector.SelectedItem.Value)
            {

                case "passing":
                    Passing(duration);
                    break;

                case "rushing":
                    Rushing(duration);
                    break;

                case "receiving":
                    Receptions(duration);
                    break;

                case "defense":
                    Defense(duration);
                    break;


                default:
                    break;
            }
        }


        private void Passing(string duration)
        {
            UserControl uc = (UserControl)Page.LoadControl("~/Controls/Records/ucRecordPassingStats.ascx");

            string ClientId = uc.ClientID; // string.Format("{0}{1}", uc.ClientID, DateTime.Now.ToString("fff"));
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            var myUsercontrol = FindControl(ClientId) as ucRecordPassingStats;
            myUsercontrol.Duration = duration;
            myUsercontrol.LeagueId = Helper.IntegerNull(Session["leagueId"]);

            phStatHolder.Controls.Add(uc);

        }

        private void Rushing(string duration)
        {
            UserControl uc = (UserControl)Page.LoadControl("~/Controls/Records/ucRecordRushingStats.ascx");

            string ClientId = uc.ClientID; // string.Format("{0}{1}", uc.ClientID, DateTime.Now.ToString("fff"));
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            var myUsercontrol = FindControl(ClientId) as ucRecordRushingStats;
            myUsercontrol.Duration = duration;
            myUsercontrol.LeagueId = Helper.IntegerNull(Session["leagueId"]);

            phStatHolder.Controls.Add(uc);

        }

        private void Receptions(string duration)
        {
            UserControl uc = (UserControl)Page.LoadControl("~/Controls/Records/ucRecordReceivingStats.ascx");

            string ClientId = uc.ClientID; // string.Format("{0}{1}", uc.ClientID, DateTime.Now.ToString("fff"));
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            var myUsercontrol = FindControl(ClientId) as ucRecordReceivingStats;
            myUsercontrol.Duration = duration;
            myUsercontrol.LeagueId = Helper.IntegerNull(Session["leagueId"]);

            phStatHolder.Controls.Add(uc);

        }

        private void Defense(string duration)
        {
            UserControl uc = (UserControl)Page.LoadControl("~/Controls/Records/ucRecordDefenseStats.ascx");

            string ClientId = uc.ClientID; // string.Format("{0}{1}", uc.ClientID, DateTime.Now.ToString("fff"));
            Page.Controls.Add(uc);
            ClientId = uc.ClientID;

            var myUsercontrol = FindControl(ClientId) as ucRecordDefenseStats;
            myUsercontrol.Duration = duration;
            myUsercontrol.LeagueId = Helper.IntegerNull(Session["leagueId"]);

            phStatHolder.Controls.Add(uc);

        }

    }
}