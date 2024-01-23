using CFMStats.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CFMStats
{
    public partial class HowBuilt : Page
    {
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

            BuildTable(Helper.IntegerNull(Request.QueryString["leagueId"]));
        }


        protected void BuildTable(int leagueId)
        {
            tableHowBuilt.InnerHtml = string.Empty;

            var sbSQL = new StringBuilder();
            sbSQL.Append("SELECT t.teamid, t.displayName, t.ovrRating, draftRound, COUNT(*) AS Picks");
            sbSQL.Append(" FROM [tblPlayerProfile] p");
            sbSQL.Append(" JOIN tblTeamInfo t ON t.teamId = p.teamid AND t.leagueid = p.leagueId");
            sbSQL.Append($" WHERE p.leagueId = {leagueId} AND p.isOnPracticeSquad = 0");
            sbSQL.Append(" GROUP BY t.teamid, t.displayName, draftRound, t.ovrRating");
            sbSQL.Append(" ORDER BY t.displayName, draftRound;");

            var SP = new StoredProc
            {
                Name = sbSQL.ToString(),
                IsSqlCommand = true,
                DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString,
                ParameterSet = new SqlCommand()
            };

            var ds = StoredProc.ShowMeTheData(SP);

            var sbTable = new StringBuilder();

            sbTable.Append("<table id='tableHowBuilt' class='tablesorter' >");

            sbTable.Append("<thead>");
            sbTable.Append("<tr>");

            sbTable.Append("<th data-filter='false' data-sorter='true'>Team</th>");
            sbTable.Append("<th data-filter='false' data-sorter='true'>Rating</th>");
            
            sbTable.Append("<th data-filter='false' data-sorter='true'>1st Round</th>");
            sbTable.Append("<th data-filter='false' data-sorter='true'>2nd Round</th>");
            sbTable.Append("<th data-filter='false' data-sorter='true'>3rd Round</th>");
            sbTable.Append("<th data-filter='false' data-sorter='true'>4th Round</th>");
            sbTable.Append("<th data-filter='false' data-sorter='true'>5th Round</th>");
            sbTable.Append("<th data-filter='false' data-sorter='true'>6th Round</th>");
            sbTable.Append("<th data-filter='false' data-sorter='true'>7th Round</th>");
            
            sbTable.Append("<th data-filter='false' data-sorter='true'>Undrafted</th>");

            sbTable.Append("</tr>");
            sbTable.Append("</thead>");
            sbTable.Append("<tbody>");

            var holdTeamId = 0;

            var teamDraftByRounds = new Dictionary<int, TeamDraftByRound>();
      

            var team = new TeamDraftByRound
            {
                
                RoundOne = 0,
                RoundTwo = 0,
                RoundThree = 0,
                RoundFour = 0,
                RoundFive = 0,
                RoundSix = 0,
                RoundSeven = 0,
                Undrafted = 0
            };


            foreach (DataRow row in ds.Tables[0].Rows)
            {
                if (Helper.IntegerNull(row["teamId"]) != holdTeamId)
                {
                    if(holdTeamId > 0)
                    {
                        if (!teamDraftByRounds.ContainsKey(holdTeamId))
                        {
                            teamDraftByRounds.Add(holdTeamId, team);
                        }
                    }

                    team = new TeamDraftByRound
                    {
                        TeamId= Helper.IntegerNull(row["teamId"]),
                        TeamName = Helper.StringNull(row["displayName"]),
                        Rating = Helper.IntegerNull(row["ovrRating"]),
                        RoundOne = 0,
                        RoundTwo = 0,
                        RoundThree = 0,
                        RoundFour = 0,
                        RoundFive = 0,
                        RoundSix = 0,
                        RoundSeven = 0,
                        Undrafted = 0
                    };
                }

                switch (Helper.IntegerNull(row["draftRound"]))
                {
                    case 1:
                        team.RoundOne = Helper.IntegerNull(row["Picks"]);
                        break;
                    case 2:
                        team.RoundTwo = Helper.IntegerNull(row["Picks"]);
                        break;
                    case 3:
                        team.RoundThree = Helper.IntegerNull(row["Picks"]);
                        break;
                    case 4:
                        team.RoundFour = Helper.IntegerNull(row["Picks"]);
                        break;
                    case 5:
                        team.RoundFive = Helper.IntegerNull(row["Picks"]);
                        break;
                    case 6:
                        team.RoundSix = Helper.IntegerNull(row["Picks"]);
                        break;
                    case 7:
                        team.RoundSeven = Helper.IntegerNull(row["Picks"]);
                        break;
                    default:
                        team.Undrafted = Helper.IntegerNull(row["Picks"]);
                        break;
                }

                holdTeamId = Helper.IntegerNull(row["teamId"]);

                if (!teamDraftByRounds.ContainsKey(holdTeamId))
                {
                    teamDraftByRounds.Add(holdTeamId, team);
                }
            }

            foreach (var item in teamDraftByRounds)
            {
                sbTable.Append("<tr>");
                
                sbTable.Append(string.Format("<td class='c{0}'><div style='display:none;'>{0}</div></td>", item.Value.TeamName.Replace(" ", string.Empty)));
                sbTable.Append($"<td>{item.Value.Rating}</td>");
                sbTable.Append($"<td>{item.Value.RoundOne}</td>");
                sbTable.Append($"<td>{item.Value.RoundTwo}</td>");
                sbTable.Append($"<td>{item.Value.RoundThree}</td>");
                sbTable.Append($"<td>{item.Value.RoundFour}</td>");
                sbTable.Append($"<td>{item.Value.RoundFive}</td>");
                sbTable.Append($"<td>{item.Value.RoundSix}</td>");
                sbTable.Append($"<td>{item.Value.RoundSeven}</td>");
                sbTable.Append($"<td>{item.Value.Undrafted}</td>");
                
                sbTable.Append("</tr>");
            }

            sbTable.Append("</tbody>");
            sbTable.Append("</table>");

            tableHowBuilt.InnerHtml = sbTable.ToString();
        }
    }
}