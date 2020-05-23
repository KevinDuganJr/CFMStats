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
    public partial class ucPassingStats : UserControl
    {
        public int iLeagueId { get; set; }

        public bool isFull { get; set; }

        public int iTeamID { get; set; }

        public int Season { get; set; }

        public int StageIndex { get; set; }

        public int Top { get; set; }

        public int Week { get; set; }

        private void BuildTable(int Season, int Week)
        {
            tablePassingStats.InnerHtml = string.Empty;

            var sp = new StoredProc
            {
                Name = "StatsPassing_select", DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, ParameterSet = new SqlCommand()
            };

            sp.ParameterSet.Parameters.AddWithValue("@stageIndex", StageIndex);
            sp.ParameterSet.Parameters.AddWithValue("@leagueId", iLeagueId);
            sp.ParameterSet.Parameters.AddWithValue("@seasonIndex", this.Season);

            if (this.Week == 99)
            {
                sp.ParameterSet.Parameters.AddWithValue("@weekIndex", DBNull.Value);
            }
            else
            {
                sp.ParameterSet.Parameters.AddWithValue("@weekIndex", this.Week);
            }

            if (Top == 0)
            {
                sp.ParameterSet.Parameters.AddWithValue("@top", DBNull.Value);
            }
            else
            {
                sp.ParameterSet.Parameters.AddWithValue("@top", Top);
            }

            if (iTeamID == 0)
            {
                sp.ParameterSet.Parameters.AddWithValue("@teamID", DBNull.Value);
            }
            else
            {
                sp.ParameterSet.Parameters.AddWithValue("@teamID", iTeamID);
            }

            var ds = StoredProc.ShowMeTheData(sp);

            // if (ds.Tables.Count == 0) { return collection; }

            var sbTable = new StringBuilder();
            sbTable.Append(isFull ? "<table id='tableStats' class='tablesorter' >" : "<table id='tableStats' class='table table-condensed table-bordered tablesorter' >");

            sbTable.Append("<thead>");
            sbTable.Append("<tr>");

            sbTable.Append("<th class='filter-select' data-placeholder='All'>Team</th>");
            sbTable.Append("<th class='filter-select' data-placeholder='All'>Pos</th>");
            sbTable.Append("<th>Player</th>");

            sbTable.Append("<th>ATT</th>");
            sbTable.Append("<th>CMP</th>");
            sbTable.Append("<th>CMP %</th>");

            sbTable.Append("<th>Yards</th>");
            if (isFull)
            {
                sbTable.Append("<th>Yds/Game</th>");
            }

            sbTable.Append("<th>TD</th>");

            sbTable.Append("<th>INT</th>");
            sbTable.Append("<th>Sack</th>");

            sbTable.Append("<th>Long</th>");
            sbTable.Append("<th>QBR</th>");
            if (isFull)
            {
                sbTable.Append("<th>Games</th>");
            }

            sbTable.Append("</tr>");
            sbTable.Append("</thead>");

            sbTable.Append("<tbody>");

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                sbTable.Append("<tr>");
                sbTable.Append(string.Format("<td class='c{0}'><div style='display:none;'>{0}</div></td>", item.Field<string>("teamName").Replace(" ", string.Empty)));
                sbTable.Append($"<td>{item.Field<string>("position")}</td>");

                if (Helper.StringNull(item["firstName"]).Length == 0)
                {
                    sbTable.Append($"<td style='text-align:left;'>{item.Field<string>("fullName")}</td>");
                }
                else
                {
                    sbTable.Append(string.Format("<td style='text-align:left;'><a  target='_blank' href='/profile?id={2}'>{0} {1}</a></td>", item.Field<string>("firstName"), item.Field<string>("lastName"), item.Field<int>("playerId")));
                }

                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("attempt")));
                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("completion")));
                sbTable.Append(string.Format("<td>{0}</td>", Helper.GetPercent(item.Field<int>("attempt"), item.Field<int>("completion"))));

                // double attempts, double completions, double touchdowns, double interceptions, double yards/

                sbTable.Append(string.Format("<td>{0:n0}</td>", item.Field<int>("yards")));
                if (isFull)
                {
                    sbTable.Append(string.Format("<td>{0}</td>", Helper.GetAverage(item.Field<int>("yards"), item.Field<int>("games"))));
                }

                //sbTable.Append(string.Format("<td>{0}</td>", Helper.CalculatePercent(item.Field<int>("targets"), item.Field<int>("drops"))));

                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("touchdown")));

                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("interception")));
                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("sack")));

                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("Longest")));

                sbTable.Append(string.Format("<td>{0}</td>", Helper.CalculateQbRating(item.Field<int>("attempt"),
                    item.Field<int>("completion"),
                    item.Field<int>("touchdown"),
                    item.Field<int>("interception"),
                    item.Field<int>("yards")))); // rating
                if (isFull)
                {
                    sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("games")));
                }

                sbTable.Append("</tr>");
            }

            sbTable.Append("</tbody>");
            sbTable.Append("</table>");

            tablePassingStats.InnerHtml = sbTable.ToString();
        }

        protected void Page_Load(object sender, EventArgs e) { }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            BuildTable(Season, Week);
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