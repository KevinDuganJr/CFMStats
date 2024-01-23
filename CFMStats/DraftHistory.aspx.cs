using CFMStats.Classes;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using CFMStats.Services;

namespace CFMStats
{
    public partial class DraftHistory : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Helper.StringNull(Request.QueryString["leagueId"]).Length == 0)
                {
                    Response.Redirect("~/");
                }

                Session["leagueId"] = Helper.StringNull(Request.QueryString["leagueId"]);

                BuildSeasonList();
                BuildDevelopmentList();
                BuildLeagueTeamList();
                BuildPositionGroupList();
            }
        }

        protected void ddlPositionGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetDraftedPlayers();
        }
        public void BuildDevelopmentList()
        {
            ddlDevelopment.Items.Clear();

            var traits = new DevelopmentTraitService();
            traits = traits.GetDevelopmentTraits();

            foreach (var trait in traits.Values)
            {
                ddlDevelopment.Items.Add(new ListItem(trait.Name, trait.Id.ToString()));
            }
        }

        private void BuildLeagueTeamList()
        {
            var sp = new StoredProc
            {
                Name = "TeamInfo_select",
                DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString,
                ParameterSet = new SqlCommand()
            };
            var leagueId = Helper.IntegerNull(Session["leagueId"]);
            sp.ParameterSet.Parameters.AddWithValue("@leagueId", leagueId);

            var ds = StoredProc.ShowMeTheData(sp);

            if (ds.Tables[0].Rows.Count == 0)
            {
                Response.Redirect("~/");
            }

            ddlLeagueTeams.Items.Clear();

            ddlLeagueTeams.Items.Add(new ListItem("All Teams", "99"));

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                ddlLeagueTeams.Items.Add(new ListItem(Helper.StringNull(item["displayName"]), Helper.StringNull(item["teamID"])));
            }
        }

        protected void BuildSeasonList()
        {
            var season = new oSeasons();
            season = season.getSeasons(Helper.IntegerNull(Session["leagueID"]));

            ddlDraftYear.Items.Add(new ListItem("All Seasons", "99"));

            foreach (var item in season.Values)
            {
                ddlDraftYear.Items.Add(new ListItem(item.Year.ToString(), item.ID.ToString()));
            }

            ddlDraftYear.SelectedIndex = 1;
        }

        private void BuildPositionGroupList()
        {
            var sp = new StoredProc
            {
                Name = "PositionGroup_select",
                DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString,
                ParameterSet = new SqlCommand()
            };

            var ds = StoredProc.ShowMeTheData(sp);

            if (ds.Tables.Count == 0)
            {
                return;
            }

            ddlPositionGroup.Items.Clear();

            ddlPositionGroup.Items.Add(new ListItem("All Positions", "99"));

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                ddlPositionGroup.Items.Add(new ListItem(Helper.StringNull(item["GroupName"]), Helper.StringNull(item["positionGroupID"])));
            }

            ddlPositionGroup_SelectedIndexChanged(null, null);
        }


        private void GetDraftedPlayers()
        {
            var traits = new DevelopmentTraitService();
            traits = traits.GetDevelopmentTraits();
            
            var iPositionGroup = Helper.IntegerNull(ddlPositionGroup.SelectedItem.Value);

            tablePlayers.InnerHtml = "";

            var sbTable = new StringBuilder();
            sbTable.Append("<table id='sumtable' class='sum_table tablesorter' >");

            sbTable.Append("<thead>");
            sbTable.Append("<tr>");

            sbTable.Append(string.Format("<th data-sorter='true' class='filter-select' data-placeholder='All' title='{1}'>{0}</th>", "Team", "Player Team"));
            sbTable.Append(string.Format("<th data-sorter='true' class='filter-select' data-placeholder='All' title='{1}'>{0}</th>", "Pos", "Player Position"));
            sbTable.Append(string.Format("<th data-sorter='true' title='{1}'>{0}</th>", "Name", "Player Name"));
            sbTable.Append(string.Format("<th data-sorter='true' title='{1}'>{0}</th>", "Age", "Age"));
            
            sbTable.Append(string.Format("<th data-sorter='true' class='filter-select' data-placeholder='All'  title='{1}'>{0}</th>", "DEV", "Development Status"));
            
            sbTable.Append(string.Format("<th data-sorter='true' title='{1}'>{0}</th>", "Pro", "Years Pro"));

            sbTable.Append(string.Format("<th data-sorter='true' class='filter-select' data-placeholder='All'  title='{1}'>{0}</th>", "Year", "Year Drafted"));
            sbTable.Append(string.Format("<th data-sorter='true' class='filter-select' data-placeholder='All'  title='{1}'>{0}</th>", "Round", "Round Drafted"));
            sbTable.Append(string.Format("<th data-sorter='true' title='{1}'>{0}</th>", "Pick", "Pick"));

            sbTable.Append(string.Format("<th data-sorter='true' title='{1}'>{0}</th>", "Rookie Rating", "Rookie Rating Overall"));

            sbTable.Append(string.Format("<th data-sorter='true' title='{1}'>{0}</th>", "Best Ovr", "Player Best Overall"));
            sbTable.Append(string.Format("<th data-sorter='true' title='{1}'>{0}</th>", "Scheme Ovr", "Player Scheme Overall"));
            sbTable.Append(string.Format("<th data-sorter='true' title='{1}'>{0}</th>", "Team Scheme Ovr", "Team Scheme Overall"));
            
            sbTable.Append(string.Format("<th data-sorter='true' title='{1}'>{0}</th>", "Change", "Change"));
            

            sbTable.Append("</tr>");
            sbTable.Append("</thead>");

            sbTable.Append("<tbody>");

            // using SQL
            var teamRosters = new oRosters();
            teamRosters = teamRosters.GetDraftHistory(Helper.IntegerNull(ddlDraftYear.SelectedItem.Value),
                Helper.IntegerNull(ddlLeagueTeams.SelectedItem.Value),
                Helper.IntegerNull(ddlPositionGroup.SelectedItem.Value),
                Helper.IntegerNull(ddlDevelopment.SelectedItem.Value),
                Helper.IntegerNull(Session["leagueId"])
            );

            foreach (var item in teamRosters.Values)
            {
                sbTable.Append("<tr>");
                //sbTable.Append(string.Format("<td>{0}</td>", itemTeam.abbrName));

                if (item.teamName.Length < 2)
                {
                    item.teamName = "Z Free Agent";
                }

                var cssPlayerStyle = new StringBuilder();

                if (item.isOnPracticeSquad)
                {
                    cssPlayerStyle.Append("psquad ");
                }

                sbTable.Append(string.Format("<td class='c{0}'><div style='display:none;'>{0}</div></td>", item.teamName.Replace(" ", string.Empty), item.teamId));
                sbTable.Append($"<td>{item.position}</td>");
                sbTable.Append($"<td style='text-align:left;' class='{cssPlayerStyle}' ><a  target='_blank' href='/profile?id={item.playerId}'>{item.firstName} {item.lastName}</a></td>");
                sbTable.Append($"<td>{item.age}</td>");

                sbTable.Append($"<td class='dev{traits[item.devTrait].Name}'><div style='display:none;'>{traits[item.devTrait].Name}</div></td>");

                sbTable.Append($"<td>{item.yearsPro}</td>");

                sbTable.Append($"<td>{item.rookieYear}</td>");
                sbTable.Append($"<td>{item.draftRound}</td>");
                sbTable.Append($"<td>{item.draftPick}</td>");
                
                sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.rookieRating, Helper.RatingLevel(item.rookieRating)));

                sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.playerBestOvr, Helper.RatingLevel(item.playerBestOvr)));
                sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.playerSchemeOvr, Helper.RatingLevel(item.playerSchemeOvr)));
                sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.teamSchemeOvr, Helper.RatingLevel(item.teamSchemeOvr)));

                sbTable.Append($"<td>{item.playerBestOvr - item.rookieRating}</td>");

                sbTable.Append("</tr>");
            }

            sbTable.Append("</tbody>");
            sbTable.Append("</table>");

            tablePlayers.InnerHtml = sbTable.ToString();
        }
    }
}