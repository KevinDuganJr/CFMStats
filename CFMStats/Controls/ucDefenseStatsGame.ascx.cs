using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using CFMStats.Classes;

namespace CFMStats.Controls
{
    public partial class ucDefenseStatsGame : System.Web.UI.UserControl
    {
        #region Properties
        private int _iStageIndex;
        public int iStageIndex
        {
            get { return _iStageIndex; }
            set { _iStageIndex = value; }
        }

        private int _iRosterId;
        public int iRosterId
        {
            get { return _iRosterId; }
            set { _iRosterId = value; }
        }

        private int _iLeagueId;
        public int iLeagueId
        {
            get { return _iLeagueId; }
            set { _iLeagueId = value; }
        }

        #endregion

        protected void Page_PreRender(object sender, EventArgs e)
        {
            BuildTable(iStageIndex, iRosterId);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BuildTable(int stageIndex, int intPlayerID)
        {
            tableDefenseStats.InnerHtml = string.Empty;

            StoredProc SP = new StoredProc();
            SP.Name = "StatsDefenseByWeek_select";
            SP.DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SP.ParameterSet = new System.Data.SqlClient.SqlCommand();

            SP.ParameterSet.Parameters.AddWithValue("@stageIndex", iStageIndex);
            SP.ParameterSet.Parameters.AddWithValue("@leagueId", iLeagueId);
            SP.ParameterSet.Parameters.AddWithValue("@playerid", intPlayerID);


            DataSet ds = StoredProc.ShowMeTheData(SP);

            if (ds.Tables[0].Rows.Count == 0)
            {
                panelDefense.Visible = false;
                return;
            }

            System.Text.StringBuilder sbTable = new System.Text.StringBuilder();
            sbTable.Append("<table id='tableDefenseStats' class='tableDefenseStats tablesorter' >");
            sbTable.Append("<thead>");
            sbTable.Append("<tr>");
            sbTable.Append("<th class='filter-select' data-placeholder='All'>Team</th>");

            sbTable.Append("<th class='filter-select' data-placeholder='All'>Away</th>");
            sbTable.Append("<th class='filter-select' data-placeholder='All'>Home</th>");
            sbTable.Append("<th class='filter-select' data-placeholder='All'>Season</th>");

            sbTable.Append("<th data-sorter='true'>Week</th>");

            sbTable.Append("<th data-sorter='true'>Sacks</th>");
            sbTable.Append("<th data-sorter='true'>Tackles</th>");
            sbTable.Append("<th data-sorter='true'>FF</th>");
            sbTable.Append("<th data-sorter='true'>FR</th>");
            sbTable.Append("<th data-sorter='true'>Safety</th>");
            sbTable.Append("<th data-sorter='true'>TD's</th>");
            sbTable.Append("<th data-sorter='true'>INT</th>");
            sbTable.Append("<th data-sorter='true'>INT Yds</th>");
            sbTable.Append("<th data-sorter='true'>Catches Allowed</th>");
            sbTable.Append("<th data-sorter='true'>Deflections</th>");

            sbTable.Append("</tr>");
            sbTable.Append("</thead>");

            sbTable.Append("<tbody>");


            foreach (DataRow item in ds.Tables[0].Rows)
            {
                sbTable.Append("<tr>");
                sbTable.Append(string.Format("<td class='c{0}'><div style='display:none;'>{0}</div></td>", item.Field<string>("teamName").Replace(" ", string.Empty).Replace(" ", string.Empty)));
                sbTable.Append(string.Format("<td>{0}</td>", item.Field<string>("away")));
                sbTable.Append(string.Format("<td>{0}</td>", item.Field<string>("home")));
                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("seasonIndex") ));
                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("week") + 1));

                sbTable.Append(string.Format("<td>{0:0.#}</td>", item.Field<decimal>("sacks")));
                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("tackles")));
                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("ForcedFumble")));
                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("FumbleRecovery")));

                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("Safety")));
                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("Touchdowns")));

                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("Interceptions")));
                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("INTReturnYards")));
                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("CatchesAllowed")));
                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("Deflections")));

                sbTable.Append("</tr>");
            }
            sbTable.Append("</tbody>");
            sbTable.Append("</table>");


            tableDefenseStats.InnerHtml = sbTable.ToString();
        }


        protected void UpdatePanel1_Unload(object sender, EventArgs e)
        {
            MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
            methodInfo.Invoke(ScriptManager.GetCurrent(Page), new object[] { sender as UpdatePanel });
        }


    }
}