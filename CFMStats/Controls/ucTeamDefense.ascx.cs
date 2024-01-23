using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.UI;
using CFMStats.Classes;

namespace CFMStats.Controls
{
    public partial class ucTeamDefense : UserControl
    {
        public int iLeagueId { get; set; }

        public int iSeason { get; set; }

        public int iStageIndex { get; set; }

        public int iWeek { get; set; }

        protected void BuildTable(int Season, int Week)
        {
            tblDefenseStats.InnerHtml = string.Empty;

            var SP = new StoredProc();
            SP.Name = "TeamDefense_select";
            SP.DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SP.ParameterSet = new SqlCommand();

            SP.ParameterSet.Parameters.AddWithValue("@stageIndex", iStageIndex);
            SP.ParameterSet.Parameters.AddWithValue("@leagueId", iLeagueId);
            SP.ParameterSet.Parameters.AddWithValue("@seasonIndex", iSeason);

            if (iWeek == 99)
            {
                SP.ParameterSet.Parameters.AddWithValue("@weekIndex", DBNull.Value);
            }
            else
            {
                SP.ParameterSet.Parameters.AddWithValue("@weekIndex", iWeek);
            }

            var ds = StoredProc.ShowMeTheData(SP);

            var sbTable = new StringBuilder();

            sbTable.Append("<table id='tableStats' class='tableDefenseStats tablesorter' >");

            sbTable.Append("<thead>");
            sbTable.Append("<tr>");
            //sbTable.Append("<th data-sorter='true' data-toggle='tooltip' data-html='true' data-container='body' data-placement='top' title='Team Name'>Team</th>");
            sbTable.Append("<th data-sorter='true' class='filter-select' data-placeholder='All'>Team</th>");
            sbTable.Append("<th data-sorter='true'>Division</th>");

            sbTable.Append("<th data-sorter='true'>Points</th>");
            sbTable.Append("<th data-sorter='true'>Pts/Gm</th>");

            sbTable.Append("<th data-sorter='true'>Total Yds</th>");
            sbTable.Append("<th data-sorter='true'>Passing Yds</th>");
            sbTable.Append("<th data-sorter='true'>Rushing Yds</th>");

            sbTable.Append("<th data-sorter='true'>Yds/Game</th>");

            sbTable.Append("<th data-sorter='true'>Sacks</th>");
            sbTable.Append("<th data-sorter='true'>FF</th>");
            sbTable.Append("<th data-sorter='true'>FR</th>");

            sbTable.Append("<th data-sorter='true'>INT</th>");

            sbTable.Append("<th data-sorter='true'>Safety</th>");
            sbTable.Append("<th data-sorter='true'>Def TDs</th>");
            sbTable.Append("<th data-sorter='true'>Deflections</th>");
            sbTable.Append("<th data-sorter='true'>Tackles</th>");

            sbTable.Append("<th data-sorter='true'>Games</th>");

            sbTable.Append("</tr>");
            sbTable.Append("</thead>");
            sbTable.Append("<tbody>");

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                sbTable.Append("<tr>");
                sbTable.Append(string.Format("<td class='c{0}'><div style='display:none;'>{0}</div></td>", item.Field<string>("teamName").Replace(" ", string.Empty)));
                sbTable.Append($"<td>{item.Field<string>("divName")}</td>");

                var pointsAgainst = Helper.IntegerNull(item["defPoints"]);
                sbTable.Append($"<td>{pointsAgainst:n0}</td>");

                sbTable.Append($"<td>{Helper.GetAverage(pointsAgainst, item.Field<int>("games"))}</td>");

                sbTable.Append($"<td>{item.Field<int>("defTotalYds"):n0}</td>");
                sbTable.Append($"<td>{item.Field<int>("defPassYds"):n0}</td>");
                sbTable.Append($"<td>{item.Field<int>("defRushYds"):n0}</td>");

                sbTable.Append($"<td>{Helper.GetAverage(item.Field<int>("defTotalYds"), item.Field<int>("games"))}</td>");

                sbTable.Append($"<td>{item.Field<int>("defSacks")}</td>");

                sbTable.Append($"<td>{item.Field<int>("defForcedFum")}</td>");
                sbTable.Append($"<td>{item.Field<int>("defFumRec")}</td>");
                sbTable.Append($"<td>{item.Field<int>("defIntsRec")}</td>");

                sbTable.Append($"<td>{item.Field<int>("defSafeties")}</td>");
                sbTable.Append($"<td>{item.Field<int>("defTDs")}</td>");
                sbTable.Append($"<td>{item.Field<int>("defDeflections")}</td>");
                sbTable.Append($"<td>{item.Field<int>("defTotalTackles")}</td>");

                sbTable.Append($"<td>{item.Field<int>("games")}</td>");

                sbTable.Append("</tr>");
            }

            sbTable.Append("</tbody>");
            sbTable.Append("</table>");

            tblDefenseStats.InnerHtml = sbTable.ToString();
        }

        protected void Page_Load(object sender, EventArgs e) { }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            BuildTable(iSeason, iWeek);
        }

        protected void UpdatePanel1_Unload(object sender, EventArgs e)
        {
            var methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
            methodInfo.Invoke(ScriptManager.GetCurrent(Page), new object[]
            {
                sender as UpdatePanel
            });
        }
    }
}