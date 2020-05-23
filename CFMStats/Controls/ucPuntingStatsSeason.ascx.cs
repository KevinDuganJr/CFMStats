using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using CFMStats.Classes;

namespace CFMStats.Controls
{
    public partial class ucPuntingStatsSeason : System.Web.UI.UserControl
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
            tablePuntingStats.InnerHtml = string.Empty;

            StoredProc SP = new StoredProc();
            SP.Name = "StatsPuntingBySeason_select";
            SP.DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SP.ParameterSet = new System.Data.SqlClient.SqlCommand();

            
            SP.ParameterSet.Parameters.AddWithValue("@stageIndex", iStageIndex);
            SP.ParameterSet.Parameters.AddWithValue("@leagueId", iLeagueId);
            SP.ParameterSet.Parameters.AddWithValue("@playerid", intPlayerID);

            DataSet ds = StoredProc.ShowMeTheData(SP);

            if (ds.Tables[0].Rows.Count == 0)
            {
                panelPunting.Visible = false;
                return;
            }


            System.Text.StringBuilder sbTable = new System.Text.StringBuilder();
            sbTable.Append("<table id='tablePuntingStats' class='tablePuntingStats tablesorter' >");
            sbTable.Append("<thead>");
            sbTable.Append("<tr>");
            //sbTable.Append("<th data-sorter='true' data-toggle='tooltip' data-html='true' data-container='body' data-placement='top' title='Team Name'>Team</th>");
            sbTable.Append("<th data-filter='false' data-sorter='true'>Team</th>");
            sbTable.Append("<th data-filter='false' data-sorter='true'>Season</th>");
            sbTable.Append("<th data-filter='false' data-sorter='true'>Games</th>");
            sbTable.Append("<th data-filter='false' data-sorter='true'>Punts</th>");
            sbTable.Append("<th data-filter='false' data-sorter='true'>Yards</th>");
            sbTable.Append("<th data-filter='false' data-sorter='true'>Yards/Punt</th>");
            sbTable.Append("<th data-filter='false' data-sorter='true'>Net Yards</th>");
            sbTable.Append("<th data-filter='false' data-sorter='true'>Net Yards/Punt</th>");
            sbTable.Append("<th data-filter='false' data-sorter='true'>Blocked</th>");
            sbTable.Append("<th data-filter='false' data-sorter='true'>In 20</th>");
            sbTable.Append("<th data-filter='false' data-sorter='true'>Long</th>");


            sbTable.Append("</tr>");
            sbTable.Append("</thead>");

            sbTable.Append("<tbody>");




            foreach (DataRow item in ds.Tables[0].Rows)
            {
                sbTable.Append("<tr>");
                sbTable.Append(string.Format("<td class='c{0}'><div style='display:none;'>{0}</div></td>", item.Field<string>("displayName").Replace(" ", string.Empty)));
                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("seasonIndex") ));
                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("games")));
                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("punt")));
                sbTable.Append(string.Format("<td>{0:n0}</td>", item.Field<int>("yards")));
                sbTable.Append(string.Format("<td>{0}</td>", Helper.GetAverage(item.Field<int>("yards"), item.Field<int>("punt"))));

                sbTable.Append(string.Format("<td>{0:n0}</td>", item.Field<int>("netyards")));
                sbTable.Append(string.Format("<td>{0}</td>", Helper.GetAverage(item.Field<int>("netyards"), item.Field<int>("punt"))));

                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("blocked")));

                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("in20")));

                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("Longest")));



                sbTable.Append("</tr>");
            }
            sbTable.Append("</tbody>");
            sbTable.Append("</table>");


            tablePuntingStats.InnerHtml = sbTable.ToString();
        }



        protected void UpdatePanel1_Unload(object sender, EventArgs e)
        {
            MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
            methodInfo.Invoke(ScriptManager.GetCurrent(Page), new object[] { sender as UpdatePanel });
        }

    }
}