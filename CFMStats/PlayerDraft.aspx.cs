using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CFMStats.Classes;

namespace CFMStats
{
    public partial class PlayerDraft : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            FixDraft();

            if (!IsPostBack)
            {
                if (Helper.StringNull(Request.QueryString["leagueId"]).Length == 0)
                {
                    Response.Redirect("~/");
                }

                Session["leagueId"] = Helper.StringNull(Request.QueryString["leagueId"]);
                
            }
        }

       


        protected void ddlPositionGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetPlayers();
        }






        private string DevelopmentStatus(int devTrait)
        {
            switch (devTrait)
            {
                case 0:
                    return "Normal";

                case 1:
                    return "Star";

                case 2:
                    return "Superstar";

                case 3:
                    return "Superstar X Factor";

                default:
                    return "";
            }
        }

        private void GetPlayers()
        {
            tablePlayers.InnerHtml = "";

            var sbTable = new StringBuilder();
            sbTable.Append("<table id='sumtable' class='sum_table tablesorter' >");

            sbTable.Append("<thead>");
            sbTable.Append("<tr>");

            sbTable.Append(string.Format("<th data-sorter='true' class='filter-select' data-placeholder='All'  title='{1}'>{0}</th>", "Team", "Player Team"));
            sbTable.Append(string.Format("<th data-sorter='true' class='filter-select' data-placeholder='All'  title='{1}'>{0}</th>", "Pos", "Player Position"));
            sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "Name", "Player Name"));
            sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "Age", "Age"));
            sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "Height", "Height"));
            sbTable.Append(string.Format("<th data-sorter='true' class='filter-select' data-placeholder='All'  title='{1}'>{0}</th>", "DEV", "Development Status"));

            sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "OVR", "Overall"));

            sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "AWR", "Awareness"));
            sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "SPD", "Speed"));
            sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "ACC", "Acceleration"));
            sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "AGI", "Agility"));
            sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "STR", "Strength"));


            sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "XP", "Experience Points"));
            sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "SP", "Skill Points"));

            sbTable.Append("</tr>");
            sbTable.Append("</thead>");

            sbTable.Append("<tbody>");

            // using SQL
            var teamRosters = new oRosters();
            teamRosters = teamRosters.GetPlayerRatings(Helper.IntegerNull(ddlPositionGroup.SelectedItem.Value),
                Helper.IntegerNull(ddlLeagueTeams.SelectedItem.Value),
                Helper.IntegerNull(ddlDevelopment.SelectedItem.Value),
                0,
                chkTwentyFiveAndUnder.Checked,
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

                sbTable.Append(string.Format("<td class='c{0}'><div style='display:none;'>{0}</div></td>",
                    item.teamName.Replace(" ", string.Empty), item.teamId));

                if (item.injuryType != 97 && item.isActive == false)
                {
                    cssPlayerStyle.Append("injured ");
                }

                if (item.isOnPracticeSquad)
                {
                    cssPlayerStyle.Append("psquad ");
                }

                if (item.isRetired)
                {
                    cssPlayerStyle.Append("isretired ");
                }

                sbTable.Append($"<td>{item.position}</td>");

                sbTable.Append($"<td style='text-align:left;' class='{cssPlayerStyle}' ><a  target='_blank' href='/profile?id={item.playerId}'>{item.firstName} {item.lastName}</a></td>");

                sbTable.Append($"<td>{item.age}</td>");
                sbTable.Append($"<td data-value={item.height}>{PlayersHeightFromInches(item.height)}</td>");
                sbTable.Append($"<td><small>{DevelopmentStatus(item.devTrait)}</small></td>");
                sbTable.Append($"<td>{item.yearsPro}</td>");

                // ratings
                sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.playerSchemeOvr, Helper.RatingLevel(item.playerSchemeOvr)));
                sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.awareRating, Helper.RatingLevel(item.awareRating)));
                sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.speedRating, Helper.RatingLevel(item.speedRating)));
                sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.accelRating, Helper.RatingLevel(item.accelRating)));
                sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.agilityRating, Helper.RatingLevel(item.agilityRating)));
                sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.strengthRating, Helper.RatingLevel(item.strengthRating)));

                

                
                

                sbTable.Append("</tr>");
            }

            sbTable.Append("</tbody>");
            sbTable.Append("</table>");

            tablePlayers.InnerHtml = sbTable.ToString();
        }

        private string PlayersHeightFromInches(int inches)
        {
            var feet = inches / 12;
            var inchesLeft = inches % 12;
            return $"{feet}' {inchesLeft}\"";
        }



        private void FixDraft()
        {
            StoredProc sp = new StoredProc
            {
                Name = "SELECT rookieYear, presentationId, portraitId, rosterId FROM tblPlayerProfile WHERE leagueId = 20", IsSqlCommand = true, DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString
            };


            var ds = StoredProc.ShowMeTheData(sp);

            if (ds.Tables.Count == 0) { return; }

            foreach (DataRow item in ds.Tables[0].Rows)
            {

                var updateString = $"UPDATE tblPlayerProfile SET ModifiedOn = GetUTCDate(), rookieYear = {item.Field<int>("rookieYear")} WHERE leagueId = 14 AND presentationId = {item.Field<int>("presentationId")} AND portraitId = {item.Field<int>("portraitId")} AND rosterId = {item.Field<int>("rosterid")}";

                var sp2 = new StoredProc
                {
                    Name = updateString,
                    IsSqlCommand = true,
                    DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString
                    
            };

                //sp2.ParameterSet = new System.Data.SqlClient.SqlCommand();
                //sp2.ParameterSet.Parameters.AddWithValue("@rookieYear", item.Field<int>("rookieYear"));
                //sp2.ParameterSet.Parameters.AddWithValue("@birthDay", item.Field<int>("birthDay"));
                //sp2.ParameterSet.Parameters.AddWithValue("@birthMonth", item.Field<int>("birthMonth"));
                //sp2.ParameterSet.Parameters.AddWithValue("@rosterId", item.Field<int>("rosterId"));

                var status = StoredProc.NonQuery(sp2);

                Console.WriteLine(status);
 
            }




        }
    }
}