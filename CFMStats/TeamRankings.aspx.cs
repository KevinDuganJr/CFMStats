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
    public partial class TeamRankings : Page
    {
        protected void BuildSeasonList()
        {
            var season = new oSeasons();
            season = season.getSeasons(Helper.IntegerNull(Session["leagueID"]));
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
            var stageIndex = Helper.IntegerNull(ddlSeasonType.SelectedItem.Value);
            var seasonIndex = Helper.IntegerNull(ddlSeason.SelectedItem.Value);
            var leagueId = Helper.IntegerNull(Session["leagueID"]);

            BuildTeamRankings(seasonIndex, stageIndex, leagueId);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Helper.IntegerNull(Session["leagueId"]) == 0)
                {
                    Response.Redirect("~/");
                }

                BuildSeasonList();
                ddlWeek_SelectedIndexChanged(null, null);
            }
        }

        private void BuildTeamRankings(int seasonIndex, int stageIndex, int leagueId)
        {
            tableTeamRankings.InnerHtml = "";

            var sp = new StoredProc
            {
                Name = "TeamStandings_select", DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, ParameterSet = new SqlCommand()
            };

            sp.ParameterSet.Parameters.AddWithValue("@leagueId", leagueId);
            sp.ParameterSet.Parameters.AddWithValue("@stageIndex", stageIndex);
            sp.ParameterSet.Parameters.AddWithValue("@seasonIndex", seasonIndex);

            var ds = StoredProc.ShowMeTheData(sp);

            var sbTable = new StringBuilder();
            sbTable.Append("<table id='fixed-columns-table' class='tablesorter' >");
            sbTable.Append("<thead>");
            sbTable.Append("<tr>");

            sbTable.Append("<th data-sorter='true'>Team</th>");
            sbTable.Append("<th data-sorter='true'>Division</th>");
            sbTable.Append("<th data-sorter='true'>OVR</th>");

            sbTable.Append("<th data-sorter='true'>Rank</th>");
            sbTable.Append("<th data-sorter='true'>Seed</th>");

            sbTable.Append("<th data-sorter='true'>W</th>");
            sbTable.Append("<th data-sorter='true'>L</th>");
            sbTable.Append("<th data-sorter='true'>T</th>");

            sbTable.Append("<th data-sorter='true'>Win %</th>");

            sbTable.Append("<th data-sorter='true'>PF Avg</th>");
            sbTable.Append("<th data-sorter='true'>PA Avg</th>");
            sbTable.Append("<th data-sorter='true'>Net Pts</th>");

            sbTable.Append("<th data-sorter='true'>TO Diff</th>");
            sbTable.Append("<th data-sorter='true'>Injuries</th>");

            sbTable.Append("<th data-sorter='true'>Avg Age</th>");
            sbTable.Append("<th data-sorter='true'>Avg Ovr</th>");

            sbTable.Append("<th data-sorter='true'>Cap Space</th>");

            sbTable.Append("<th data-sorter='true'>Off Scheme</th>");
            sbTable.Append("<th data-sorter='true'>Def Scheme</th>");

            sbTable.Append("</tr>");
            sbTable.Append("</thead>");

            sbTable.Append("<tbody>");

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                sbTable.Append("<tr>");
                sbTable.Append(string.Format("<td class='c{0}'><div style='display:none;'>{0}</div></td>",
                    item.Field<string>("teamName").Replace(" ", string.Empty)));

                sbTable.Append($"<td>{item.Field<string>("divisionName")}</td>");
                sbTable.Append($"<td>{item.Field<int>("teamOvr")}</td>");

                sbTable.Append($"<td>{item.Field<int>("rank")}</td>");
                sbTable.Append($"<td>{item.Field<int>("seed")}</td>");

                sbTable.Append($"<td>{Helper.IntegerNull(item["totalWins"])}</td>");
                sbTable.Append($"<td>{Helper.IntegerNull(item["totalLosses"])}</td>");
                sbTable.Append($"<td>{Helper.IntegerNull(item["totalTies"])}</td>");

                var totalGamesPlayed = Helper.IntegerNull(item["totalWins"]) +
                                       Helper.IntegerNull(item["totalLosses"]) +
                                       Helper.IntegerNull(item["totalTies"]);

                sbTable.Append($"<td>{CalculateWin(totalGamesPlayed, Helper.IntegerNull(item["totalWins"]), Helper.IntegerNull(item["totalTies"]))}</td>");

                var pointsFor = Helper.IntegerNull(item["PointsFor"]);
                sbTable.Append($"<td>{Helper.GetAverage(pointsFor, totalGamesPlayed)}</td>");

                var pointsAgainst = Helper.IntegerNull(item["PointsAgainst"]);
                sbTable.Append($"<td>{Helper.GetAverage(pointsAgainst, totalGamesPlayed)}</td>");

                sbTable.Append($"<td>{item.Field<int>("netPts")}</td>");

                sbTable.Append($"<td>{item.Field<int>("tODiff")}</td>");
                sbTable.Append($"<td>{Helper.IntegerNull(item["injuryCount"])}</td>");

                sbTable.Append($"<td>{item["AverageAge"]:0.##}</td>");
                sbTable.Append($"<td>{item["AverageOvr"]:0.##}</td>");

                sbTable.Append($"<td style='text-align:right;'>${item.Field<int>("capRoom") - item.Field<int>("capSpent"):n0}</td>");

                sbTable.Append($"<td>{item.Field<string>("OffensiveScheme")}</td>");
                sbTable.Append($"<td>{item.Field<string>("DefensiveScheme")}</td>");

                sbTable.Append("</tr>");
            }

            sbTable.Append("</tbody>");
            sbTable.Append("</table>");

            tableTeamRankings.InnerHtml = sbTable.ToString();
        }

        private string CalculateWin(int totalGames, double wins, int ties)
        {
            var sValue = string.Empty;

            if (ties > 0)
            {
                var halfWins = ties * .5;
                wins = wins + halfWins;
            }

            var percent = wins / totalGames;

            if (wins == 0)
            {
                return "0.000";
            }

            if (totalGames == wins)
            {
                return "1.000";
            }

            return totalGames + wins > 0 ? $"{percent:0.###}".PadRight(5, '0') : sValue;
        }
    }
}