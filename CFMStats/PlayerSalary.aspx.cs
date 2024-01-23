using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using CFMStats.Classes;
using CFMStats.Services;

namespace CFMStats
{
    public partial class PlayerSalary : Page
    {
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

            ddlLeagueTeams.Items.Add(new ListItem("All Teams", "99"));

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                ddlLeagueTeams.Items.Add(new ListItem(Helper.StringNull(item["displayName"]), Helper.StringNull(item["teamID"])));
            }
        }

        private void BuildPositionGroupList()
        {
            var sp = new StoredProc
            {
                Name = "PositionGroup_select", DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, ParameterSet = new SqlCommand()
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

            ddlPositionGroup.SelectedIndex = 1;

            ddlPositionGroup_SelectedIndexChanged(null, null);
        }

        protected void ddlPositionGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetPlayers();
        }

        private void GetPlayers()
        {
            var traits = new DevelopmentTraitService();
            traits = traits.GetDevelopmentTraits();


            tablePlayers.InnerHtml = "";

            var sbTable = new StringBuilder();
            sbTable.Append("<table id='sumtable' class='sum_table tablesorter' >");

            sbTable.Append("<thead>");
            sbTable.Append("<tr>");

            sbTable.Append(string.Format("<th data-sorter='true' class='filter-select' data-placeholder='All' data-toggle='tooltip' data-html='true' data-container='body' data-placement='top' title='{1}'>{0}</th>", "Team", "Player Team"));
            sbTable.Append(string.Format("<th data-sorter='true' class='filter-select' data-placeholder='All' data-toggle='tooltip' data-html='true' data-container='body' data-placement='top' title='{1}'>{0}</th>", "Pos", "Player Position"));
            sbTable.Append(string.Format("<th data-sorter='true' data-toggle='tooltip' data-html='true' data-container='body' data-placement='top' title='{1}'>{0}</th>", "Name", "Player Name"));
            sbTable.Append(string.Format("<th data-sorter='true' data-toggle='tooltip' data-html='true' data-container='body' data-placement='top' title='{1}'>{0}</th>", "Age", "Age"));
            sbTable.Append(string.Format("<th data-sorter='true' data-toggle='tooltip' data-html='true' data-container='body' data-placement='top' title='{1}'>{0}</th>", "DEV", "Development"));
            sbTable.Append(string.Format("<th data-sorter='true' data-toggle='tooltip' data-html='true' data-container='body' data-placement='top' title='{1}'>{0}</th>", "Pro", "Years Pro"));
            
            sbTable.Append(string.Format("<th data-sorter='true' data-toggle='tooltip' data-html='true' data-container='body' data-placement='top' title='{1}'>{0}</th>", "OVR", "Overall"));

            sbTable.Append(string.Format("<th data-sorter='true' data-toggle='tooltip' data-html='true' data-container='body' data-placement='top' title='{1}'>{0}</th>", "Cap Hit", "Cap Hit"));
            sbTable.Append(string.Format("<th data-sorter='true' data-toggle='tooltip' data-html='true' data-container='body' data-placement='top' title='{1}'>{0}</th>", "Savings", "Cap Net Savings"));
            sbTable.Append(string.Format("<th data-sorter='true' data-toggle='tooltip' data-html='true' data-container='body' data-placement='top' title='{1}'>{0}</th>", "Penalty", "Cap Penalty"));
            sbTable.Append(string.Format("<th data-sorter='true' data-toggle='tooltip' data-html='true' data-container='body' data-placement='top' title='{1}'>{0}</th>", "Bonus", "Contract Bonus"));
            sbTable.Append(string.Format("<th data-sorter='true' data-toggle='tooltip' data-html='true' data-container='body' data-placement='top' title='{1}'>{0}</th>", "Length", "Contract Length"));
            sbTable.Append(string.Format("<th data-sorter='true' data-toggle='tooltip' data-html='true' data-container='body' data-placement='top' title='{1}'>{0}</th>", "Salary", "Contract Salary"));
            sbTable.Append(string.Format("<th data-sorter='true' data-toggle='tooltip' data-html='true' data-container='body' data-placement='top' title='{1}'>{0}</th>", "Yrs Left", "Years Left on Contract"));
            sbTable.Append(string.Format("<th data-sorter='true' data-toggle='tooltip' data-html='true' data-container='body' data-placement='top' title='{1}'>{0}</th>", "DesiredLength", "Desired Contract Length"));
            sbTable.Append(string.Format("<th data-sorter='true' data-toggle='tooltip' data-html='true' data-container='body' data-placement='top' title='{1}'>{0}</th>", "DesiredSalary", "Desired Contract Salary"));
            sbTable.Append(string.Format("<th data-sorter='true' data-toggle='tooltip' data-html='true' data-container='body' data-placement='top' title='{1}'>{0}</th>", "DesiredBonus", "Desired Signing Bonus"));

            sbTable.Append(string.Format("<th data-sorter='true' data-toggle='tooltip' data-html='true' data-container='body' data-placement='top' title='{1}'>{0}</th>", "SP", "Skill Points"));
            
            sbTable.Append("</tr>");
            sbTable.Append("</thead>");

            sbTable.Append("<tbody>");

            // using SQL
            var teamRosters = new oRosters();
            teamRosters = teamRosters.GetPlayerSalaries(Helper.IntegerNull(ddlPositionGroup.SelectedItem.Value),
                Helper.IntegerNull(ddlLeagueTeams.SelectedItem.Value),
                Helper.IntegerNull(Session["leagueID"]));

            foreach (var item in teamRosters.Values)
            {
                sbTable.Append("<tr>");
      
                if (item.teamName.Length < 2)
                {
                    item.teamName = "Z Free Agent";
                }

                var cssPlayerStyle = new StringBuilder();

                sbTable.Append($"<td class='c{item.teamName.Replace(" ", string.Empty)}'><div style='display:none;'>{item.teamName.Replace(" ", string.Empty)}</div></td>");

                sbTable.Append($"<td>{item.position}</td>");

                sbTable.Append($"<td style='text-align:left;' class='{cssPlayerStyle}' ><a  target='_blank' href='/profile?id={item.playerId}'>{item.firstName} {item.lastName}</a></td>");

                sbTable.Append($"<td>{item.age}</td>");
                sbTable.Append($"<td class='dev{traits[item.devTrait].Name}'><div style='display:none;'>{traits[item.devTrait].Name}</div></td>");
                sbTable.Append($"<td>{item.yearsPro}</td>");

                // ratings

                sbTable.Append($"<td class='{Helper.RatingLevel(item.playerSchemeOvr)}'>{item.playerSchemeOvr}</td>");

                sbTable.Append($"<td style='text-align:right;'>${item.capHit:n0}</td>");
                sbTable.Append($"<td style='text-align:right;'>${item.capReleaseNetSavings:n0}</td>");
                sbTable.Append($"<td style='text-align:right;'>${item.capReleasePenalty:n0}</td>");
                sbTable.Append($"<td style='text-align:right;'>${item.contractBonus:n0}</td>");
                sbTable.Append($"<td>{item.contractLength}</td>");
                sbTable.Append($"<td style='text-align:right;'>${item.contractSalary:n0}</td>");
                sbTable.Append($"<td>{item.contractYearsLeft}</td>");
                sbTable.Append($"<td>{item.desiredLength}</td>");
                sbTable.Append($"<td style='text-align:right;'>${item.desiredSalary:n0}</td>");
                sbTable.Append($"<td style='text-align:right;'>${item.desiredBonus:n0}</td>");

                sbTable.Append($"<td>{item.skillPoints}</td>");

                sbTable.Append("</tr>");

                lblCapSpace.Text = ddlLeagueTeams.SelectedIndex == 0 ? "No Team Selected" : $"<td>${item.capRoom - item.capSpent:n0}</td>";
            }

            sbTable.Append("</tbody>");
            sbTable.Append("</table>");

            tablePlayers.InnerHtml = sbTable.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Helper.StringNull(Request.QueryString["leagueId"]).Length == 0)
                {
                    Response.Redirect("~/");
                }

                Session["leagueId"] = Helper.StringNull(Request.QueryString["leagueId"]);

                BuildLeagueTeamList();
                BuildPositionGroupList();
            }
        }
        
    }
}