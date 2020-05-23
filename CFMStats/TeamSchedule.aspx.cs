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
    public partial class TeamSchedule : Page
    {
        protected void ddlLeagueTeams_SelectedIndexChanged(object sender, EventArgs e)
        {
            var stageIndex = Helper.IntegerNull(ddlSeasonType.SelectedItem.Value);
            var seasonIndex = Helper.IntegerNull(ddlSeason.SelectedItem.Value);
            var leagueId = Helper.IntegerNull(Session["leagueID"]);
            var teamId = Helper.IntegerNull(ddlLeagueTeams.SelectedItem.Value);

            BuildTeamSchedule(seasonIndex, stageIndex, leagueId, teamId);
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
                BuildLeagueTeamList();
                ddlLeagueTeams_SelectedIndexChanged(null, null);
            }
        }

        private void BuildLeagueTeamList()
        {
            var sp = new StoredProc
            {
                Name = "TeamInfo_select", DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, ParameterSet = new SqlCommand()
            };
            var leagueId = Helper.IntegerNull(Session["leagueId"]);
            sp.ParameterSet.Parameters.AddWithValue("@leagueId", leagueId);

            var ds = StoredProc.ShowMeTheData(sp);

            if (ds.Tables[0].Rows.Count == 0)
            {
                Response.Redirect("~/");
            }

            ddlLeagueTeams.Items.Clear();

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                ddlLeagueTeams.Items.Add(new ListItem(Helper.StringNull(item["displayName"]), Helper.StringNull(item["teamID"])));
            }
        }

        private void BuildSeasonList()
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

        private void BuildTeamSchedule(int seasonIndex, int stageIndex, int leagueId, int teamId)
        {
            tblSchedule.InnerHtml = "";

            var sp = new StoredProc
            {
                Name = "TeamScheduleSeason_select", DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, ParameterSet = new SqlCommand()
            };

            sp.ParameterSet.Parameters.AddWithValue("@leagueId", leagueId);
            sp.ParameterSet.Parameters.AddWithValue("@stageIndex", stageIndex);
            sp.ParameterSet.Parameters.AddWithValue("@seasonIndex", seasonIndex);
            sp.ParameterSet.Parameters.AddWithValue("@teamId", teamId);

            var ds = StoredProc.ShowMeTheData(sp);

            var sbTable = new StringBuilder();
            sbTable.Append("<table id='fixed-columns-table' class='tablesorter' >");
            sbTable.Append("<thead>");
            sbTable.Append("<tr>");

            sbTable.Append("<th data-sorter='true'>Week</th>");

            sbTable.Append("<th data-sorter='true'>Team Logo</th>");
            sbTable.Append("<th data-sorter='true'>Team</th>");
            sbTable.Append("<th data-sorter='true'>Team Score</th>");

            sbTable.Append("<th data-sorter='true'>Opponent Logo</th>");
            sbTable.Append("<th data-sorter='true'>Opponent</th>");
            sbTable.Append("<th data-sorter='true'>Opponent Score</th>");

            sbTable.Append("<th data-sorter='true'>Margin</th>");

            sbTable.Append("<th data-sorter='true'></th>");

            sbTable.Append("</tr>");
            sbTable.Append("</thead>");

            sbTable.Append("<tbody>");

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                sbTable.Append("<tr>");

                sbTable.Append($"<td>{item.Field<int>("weekIndex") + 1}</td>");

                if (item.Field<int>("homeTeamId") == teamId)
                {
                    sbTable.Append(string.Format("<td class='c{0}'><div style='display:none;'>{0}</div></td>", item.Field<string>("homeTeam").Replace(" ", string.Empty)));
                    sbTable.Append($"<td>{item.Field<string>("homeTeam")}</td>");
                    sbTable.Append($"<td>{item.Field<int>("homeScore")}</td>");

                    sbTable.Append(string.Format("<td class='c{0}'><div style='display:none;'>{0}</div></td>", item.Field<string>("awayTeam").Replace(" ", string.Empty)));
                    sbTable.Append($"<td>{item.Field<string>("awayTeam")}</td>");
                    sbTable.Append($"<td>{item.Field<int>("awayScore")}</td>");

                    sbTable.Append($"<td>{item.Field<int>("homeScore") - item.Field<int>("awayScore")}</td>");

                    sbTable.Append(Helper.IntegerNull(item.Field<int>("status")) > 1 ? string.Format($"<td><a href='/BoxScore?id={item.Field<int>("awayTeamID")}&season={item.Field<int>("seasonIndex")}&week={item.Field<int>("weekIndex")}&type={item.Field<int>("stageIndex")}&leagueId={leagueId}'>Box Score</a></td>") : "<td>Not Played</td>");
                }
                else
                {
                    sbTable.Append(string.Format("<td class='c{0}'><div style='display:none;'>{0}</div></td>", item.Field<string>("awayTeam").Replace(" ", string.Empty)));
                    sbTable.Append($"<td>{item.Field<string>("awayTeam")}</td>");
                    sbTable.Append($"<td>{item.Field<int>("awayScore")}</td>");

                    sbTable.Append(string.Format("<td class='c{0}'><div style='display:none;'>{0}</div></td>", item.Field<string>("homeTeam").Replace(" ", string.Empty)));
                    sbTable.Append($"<td>@{item.Field<string>("homeTeam")}</td>");
                    sbTable.Append($"<td>{item.Field<int>("homeScore")}</td>");

                    sbTable.Append($"<td>{item.Field<int>("awayScore") - item.Field<int>("homeScore")}</td>");

                    sbTable.Append(Helper.IntegerNull(item.Field<int>("status")) > 1 ? string.Format($"<td><a href='/BoxScore?id={item.Field<int>("homeTeamId")}&season={item.Field<int>("seasonIndex")}&week={item.Field<int>("weekIndex")}&type={item.Field<int>("stageIndex")}&leagueId={leagueId}'>Box Score</a></td>") : "<td>Not Played</td>");
                }

                sbTable.Append("</tr>");
            }

            sbTable.Append("</tbody>");
            sbTable.Append("</table>");

            tblSchedule.InnerHtml = sbTable.ToString();
        }
    }
}