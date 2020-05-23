using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using CFMStats.Classes;

namespace CFMStats.Controls
{
    public partial class ucReceivingStatsGame : System.Web.UI.UserControl
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

            tableReceivingStats.InnerHtml = string.Empty;

            StoredProc SP = new StoredProc();
            SP.Name = "StatsReceivingByWeek_select";
            SP.DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SP.ParameterSet = new System.Data.SqlClient.SqlCommand();

            SP.ParameterSet.Parameters.AddWithValue("@stageIndex", iStageIndex);
            SP.ParameterSet.Parameters.AddWithValue("@leagueId", iLeagueId);
            SP.ParameterSet.Parameters.AddWithValue("@playerid", intPlayerID);
            
            DataSet ds = StoredProc.ShowMeTheData(SP);

            if (ds.Tables[0].Rows.Count == 0)
            {
                panelReceiving.Visible = false;
                return;
            }


            System.Text.StringBuilder sbTable = new System.Text.StringBuilder();
            sbTable.Append("<table id='tableReceivingStats' class='tableReceivingStats tablesorter' >");
            sbTable.Append("<thead>");
            sbTable.Append("<tr>");
            sbTable.Append("<th class='filter-select' data-placeholder='All'>Team</th>");

            sbTable.Append("<th class='filter-select' data-placeholder='All'>Away</th>");
            sbTable.Append("<th class='filter-select' data-placeholder='All'>Home</th>");
            sbTable.Append("<th class='filter-select' data-placeholder='All'>Season</th>");
            sbTable.Append("<th>Week</th>");
            sbTable.Append("<th data-sorter='true'>Targets</th>");
            sbTable.Append("<th data-sorter='true'>Receptions</th>");
            sbTable.Append("<th data-sorter='true'>Yards</th>");
            sbTable.Append("<th data-sorter='true'>Yds/Rec</th>");
          
            sbTable.Append("<th data-sorter='true'>TD's</th>");
            sbTable.Append("<th data-sorter='true'>YAC</th>");
          
            sbTable.Append("<th data-sorter='true'>Long</th>");
            sbTable.Append("<th data-sorter='true'>Catch %</th>");
            sbTable.Append("<th data-sorter='true'>Drops</th>");
            sbTable.Append("<th data-sorter='true'>Drop %</th>");

            sbTable.Append("</tr>");
            sbTable.Append("</thead>");

            sbTable.Append("<tbody>");


            foreach (DataRow item in ds.Tables[0].Rows)
            {
                sbTable.Append("<tr>");
                sbTable.Append(string.Format("<td class='c{0}'><div style='display:none;'>{0}</div></td>", item.Field<string>("teamName").Replace(" ", string.Empty)));
                sbTable.Append(string.Format("<td>{0}</td>", item.Field<string>("away")));
                sbTable.Append(string.Format("<td>{0}</td>", item.Field<string>("home")));
                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("seasonIndex") ));
                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("week") + 1));
                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("targets")));
                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("receptions")));
                sbTable.Append(string.Format("<td>{0:n0}</td>", item.Field<int>("yards")));
                sbTable.Append(string.Format("<td>{0}</td>", Helper.GetAverage(item.Field<int>("yards"), item.Field<int>("receptions"))));

                 

                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("touchdowns")));
                sbTable.Append(string.Format("<td>{0:n0}</td>", item.Field<int>("YardsAfterCatch")));
             

                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("Longest")));
                sbTable.Append(string.Format("<td>{0}</td>", Helper.GetPercent(item.Field<int>("targets"), item.Field<int>("receptions"))));

                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("drops")));
                sbTable.Append(string.Format("<td>{0}</td>", Helper.GetPercent(item.Field<int>("targets"), item.Field<int>("drops"))));


                sbTable.Append("</tr>");
            }
            sbTable.Append("</tbody>");
            sbTable.Append("</table>");

            tableReceivingStats.InnerHtml = sbTable.ToString();

        }



        protected void UpdatePanel1_Unload(object sender, EventArgs e)
        {
            MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
            methodInfo.Invoke(ScriptManager.GetCurrent(Page), new object[] { sender as UpdatePanel });
        }
    }
}
