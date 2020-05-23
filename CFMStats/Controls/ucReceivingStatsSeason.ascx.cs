using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using CFMStats.Classes;

namespace CFMStats.Controls
{
    public partial class ucReceivingStatsSeason : System.Web.UI.UserControl
    {
        #region Properties
        public int StageIndex { get; set; }  // todo this break things?

        public int RosterId { get; set; }
        
        public int LeagueId { get; set; }

        #endregion

        protected void Page_PreRender(object sender, EventArgs e)
        {
            BuildTable(StageIndex, RosterId);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BuildTable(int stageIndex, int intPlayerID)
        {

            tableReceivingStats.InnerHtml = string.Empty;

            StoredProc sp = new StoredProc
            {
                Name = "StatsReceivingBySeason_select",
                DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString,
                ParameterSet = new System.Data.SqlClient.SqlCommand()
            };

            sp.ParameterSet.Parameters.AddWithValue("@stageIndex", this.StageIndex);
            sp.ParameterSet.Parameters.AddWithValue("@playerid", intPlayerID);
            sp.ParameterSet.Parameters.AddWithValue("@leagueId", LeagueId);

            DataSet ds = StoredProc.ShowMeTheData(sp);

            if (ds.Tables[0].Rows.Count == 0)
            {
                panelReceiving.Visible = false;
                return;
            }


            System.Text.StringBuilder sbTable = new System.Text.StringBuilder();
            sbTable.Append("<table id='tableReceivingStats' class='tableReceivingStats tablesorter' >");
            sbTable.Append("<thead>");
            sbTable.Append("<tr>");
            //sbTable.Append("<th data-sorter='true' data-toggle='tooltip' data-html='true' data-container='body' data-placement='top' title='Team Name'>Team</th>");
            sbTable.Append("<th data-filter='false' data-sorter='true'>Team</th>");
            sbTable.Append("<th data-filter='false' data-sorter='true'>Season</th>");
            sbTable.Append("<th data-filter='false' data-sorter='true'>Games</th>");
            sbTable.Append("<th data-filter='false' data-sorter='true'>Targets</th>");
            sbTable.Append("<th data-filter='false' data-sorter='true'>Receptions</th>");
            sbTable.Append("<th data-filter='false' data-sorter='true'>Yards</th>");
            sbTable.Append("<th data-filter='false' data-sorter='true'>Yds/Rec</th>");
            sbTable.Append("<th data-filter='false' data-sorter='true'>Yds/Game</th>"); 
            sbTable.Append("<th data-filter='false' data-sorter='true'>TD's</th>");
            sbTable.Append("<th data-filter='false' data-sorter='true'>YAC</th>");
            sbTable.Append("<th data-filter='false' data-sorter='true'>YAC/Game</th>"); 
            sbTable.Append("<th data-filter='false' data-sorter='true'>Long</th>");
            sbTable.Append("<th data-filter='false' data-sorter='true'>Catch %</th>");
            sbTable.Append("<th data-filter='false' data-sorter='true'>Drops</th>");
            sbTable.Append("<th data-filter='false' data-sorter='true'>Drop %</th>");

           

            sbTable.Append("</tr>");
            sbTable.Append("</thead>");

            sbTable.Append("<tbody>");




            foreach (DataRow item in ds.Tables[0].Rows)
            {
                sbTable.Append("<tr>");
                sbTable.Append(string.Format("<td class='c{0}'><div style='display:none;'>{0}</div></td>", item.Field<string>("displayName").Replace(" ", string.Empty)));
                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("seasonIndex") ));
                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("games")));
                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("targets")));
                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("receptions")));
                sbTable.Append(string.Format("<td>{0:n0}</td>", item.Field<int>("yards")));
                sbTable.Append(string.Format("<td>{0}</td>", Helper.GetAverage(item.Field<int>("yards"), item.Field<int>("receptions"))));

                 sbTable.Append(string.Format("<td>{0}</td>", Helper.GetAverage(item.Field<int>("yards"), item.Field<int>("games")))); 

                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("touchdowns")));
                sbTable.Append(string.Format("<td>{0:n0}</td>", item.Field<int>("YardsAfterCatch")));
                sbTable.Append(string.Format("<td>{0}</td>", Helper.GetAverage(item.Field<int>("YardsAfterCatch"), item.Field<int>("games")))); 

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