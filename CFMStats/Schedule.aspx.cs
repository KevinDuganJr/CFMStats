using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using CFMStats.Classes;

namespace CFMStats
{
    public partial class Schedule : Page
    {
        protected void BuildRows()
        {
            var stageIndex = Helper.IntegerNull(ddlSeasonType.SelectedItem.Value);
            var seasonIndex = Helper.IntegerNull(ddlSeason.SelectedItem.Value);
            var weekIndex = Helper.IntegerNull(ddlWeek.SelectedItem.Value);
            var leagueId = Helper.IntegerNull(Session["leagueID"]);

            tableSchedule.InnerHtml = string.Empty;

            var sp = new StoredProc
            {
                Name = "ScheduleWeek_select", DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, ParameterSet = new SqlCommand()
            };

            sp.ParameterSet.Parameters.AddWithValue("@leagueId", leagueId);
            sp.ParameterSet.Parameters.AddWithValue("@stageIndex", stageIndex);
            sp.ParameterSet.Parameters.AddWithValue("@seasonIndex", seasonIndex);
            sp.ParameterSet.Parameters.AddWithValue("@weekIndex", weekIndex);

            var ds = StoredProc.ShowMeTheData(sp);

            var sbTable = new StringBuilder();

            var firstColumn = "<div class='col-md-1 col-sm-2 col-xs-3'>";
            var middleColumn = "<div class='col-md-4 col-sm-3 hidden-xs' style:display: flex;align-items:center;'> ";
            var lastColumn = "<div class='col-md-1 col-sm-1 col-xs-3'>";

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                var sHomeUser = "CPU";
                var sAwayUser = "CPU";

                if (Helper.StringNull(item.Field<string>("awayUser")).Length > 0)
                {
                    sAwayUser = item.Field<string>("awayUser");
                }

                if (Helper.StringNull(item.Field<string>("homeUser")).Length > 0)
                {
                    sHomeUser = item.Field<string>("homeUser");
                }

                var iHomeScore = item.Field<int>("homeScore");
                var iAwayScore = item.Field<int>("awayScore");

                sbTable.Append("<div class='row alert alert-schedule'>");

                // Away Team
                sbTable.Append(firstColumn);
                sbTable.Append($"<a href='/BoxScore?id={item.Field<int>("awayTeamId")}&leagueId={leagueId}&season={seasonIndex}&week={weekIndex}&type={stageIndex}'><img class='awayLogo img-responsive' src='/images/team/{item.Field<string>("awayTeam").Replace(" ", string.Empty)}.png' /></a>");
                sbTable.Append("</div>");

                // Away City
                sbTable.Append(middleColumn);
                sbTable.Append($"<div class='teamCity'>{item.Field<string>("awayCity")}</div>");
                sbTable.Append($"<div class='teamName'>{item.Field<string>("awayTeam")}</div>");
                sbTable.Append($"<div class='teamUser'>{sAwayUser}</div>");
                sbTable.Append("</div>");

                // Away Score
                sbTable.Append(lastColumn);
                sbTable.Append(iAwayScore > iHomeScore
                    ? $"<div class='teamScore teamWinner'>{iAwayScore}</div>"
                    : $"<div class='teamScore'>{iAwayScore}</div>");
                sbTable.Append("</div>");

                // Home Team
                sbTable.Append(firstColumn);
                sbTable.Append(string.Format($"<a href='/BoxScore?id={item.Field<int>("homeTeamId")}&leagueId={leagueId}&season={seasonIndex}&week={weekIndex}&type={stageIndex}'><img class='homeLogo img-responsive' src='/images/team/{item.Field<string>("homeTeam").Replace(" ", string.Empty)}.png' /></a>"));
                sbTable.Append("</div>");

                // Home City
                sbTable.Append(middleColumn);
                sbTable.Append($"<div class='teamCity'>{item.Field<string>("homeCity")}</div>");
                sbTable.Append($"<div class='teamName'>{item.Field<string>("homeTeam")}</div>");
                sbTable.Append($"<div class='teamUser'>{sHomeUser}</div>");
                sbTable.Append("</div>");

                // Home Score
                sbTable.Append(lastColumn);
                sbTable.Append(iHomeScore > iAwayScore
                    ? $"<div class='teamScore teamWinner'>{iHomeScore}</div>"
                    : $"<div class='teamScore'>{iHomeScore}</div>");
                sbTable.Append("</div>");

                sbTable.Append("</div>");
            }

            tableSchedule.InnerHtml = sbTable.ToString();
        }

        protected void BuildSeasonList()
        {
            var season = new oSeasons();
            season = season.getSeasons(Helper.IntegerNull(Session["leagueID"]));

            foreach (var item in season.Values)
            {
                ddlSeason.Items.Add(new ListItem(item.Year.ToString(), item.ID.ToString()));
            }
        }

        protected void ddlSeasonType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //  setWeek();
        }

        protected void ddlWeek_SelectedIndexChanged(object sender, EventArgs e)
        {
            BuildRows();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                return;
            }

            if (Helper.StringNull(Request.QueryString["leagueId"]).Length == 0)
            {
                // no league, go back to start
                Response.Redirect("~/");
            }

            Session["leagueId"] = Helper.StringNull(Request.QueryString["leagueId"]);

            BuildSeasonList();
            GetCurrentSeasonWeek();
        }

        protected void setWeek()
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
                ddlWeek.Items.Add(new ListItem(item.Text, item.Value.ToString()));
            }

            GetCurrentSeasonWeek();
        }

        private void GetCurrentSeasonWeek()
        {
            var sp = new StoredProc
            {
                Name = "ScheduleCurrentWeek_select", DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, ParameterSet = new SqlCommand()
            };

            sp.ParameterSet.Parameters.AddWithValue("@leagueId", Helper.IntegerNull(Session["leagueID"]));

            var ds = StoredProc.ShowMeTheData(sp);

            if (ds.Tables[0].Rows.Count == 0)
            {
                Response.Redirect("~/");
            }

            if (ds.Tables[0].Rows.Count < 1)
            {
                ddlWeek.SelectedIndex = ddlWeek.Items.Count - 1;
            }

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                //ddlSeasonType.SelectedIndex = ddlSeasonType.Items.IndexOf(ddlSeasonType.Items.FindByValue(Helper.String_Null(item["seasonType"])));
                ddlSeason.SelectedIndex = ddlSeason.Items.IndexOf(ddlSeason.Items.FindByValue(Helper.StringNull(item["seasonIndex"])));

                var currentWeek = Helper.IntegerNull(item["weekIndex"]);
                // if (currentWeek > 0) { currentWeek = currentWeek - 1; }

                ddlWeek.SelectedIndex = ddlWeek.Items.IndexOf(ddlWeek.Items.FindByValue(Helper.StringNull(currentWeek)));
            }

            ddlWeek_SelectedIndexChanged(null, null);
        }
    }
}