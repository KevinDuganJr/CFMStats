using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using CFMStats.Classes;

namespace CFMStats.Controls
{
    public partial class ucKickingStatsSeason : System.Web.UI.UserControl
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
            tableKickingStats.InnerHtml = string.Empty;

            StoredProc SP = new StoredProc();
            SP.Name = "StatsKickingBySeason_select";
            SP.DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SP.ParameterSet = new System.Data.SqlClient.SqlCommand();

            SP.ParameterSet.Parameters.AddWithValue("@stageIndex", iStageIndex);
            SP.ParameterSet.Parameters.AddWithValue("@leagueId", iLeagueId);
            SP.ParameterSet.Parameters.AddWithValue("@playerid", intPlayerID);


            DataSet ds = StoredProc.ShowMeTheData(SP);

            if (ds.Tables[0].Rows.Count == 0)
            {
                panelKicking.Visible = false;
                return;
            }

            System.Text.StringBuilder sbTable = new System.Text.StringBuilder();
            sbTable.Append("<table id='tableKickingStats' class='tableKickingStats tablesorter' >");
            sbTable.Append("<thead>");
            sbTable.Append("<tr>");
            
            sbTable.Append("<th data-filter='false' data-sorter='true'>Team</th>");
            sbTable.Append("<th data-filter='false' data-sorter='true'>Season</th>");
            sbTable.Append("<th data-filter='false' data-sorter='true'>Games</th>"); 
            sbTable.Append("<th data-filter='false' data-sorter='true'>FGA</th>");
            sbTable.Append("<th data-filter='false' data-sorter='true'>FGM</th>");
            sbTable.Append("<th data-filter='false' data-sorter='true'>FG %</th>");
                                
            sbTable.Append("<th data-filter='false' data-sorter='true'>50+ FGA</th>");
            sbTable.Append("<th data-filter='false' data-sorter='true'>50+ FGM</th>");
            sbTable.Append("<th data-filter='false' data-sorter='true'>50+ FG %</th>");
                                
            sbTable.Append("<th data-filter='false' data-sorter='true'>FG Long</th>");
                                
            sbTable.Append("<th data-filter='false' data-sorter='true'>XPA</th>");
            sbTable.Append("<th data-filter='false' data-sorter='true'>XPM</th>");
            sbTable.Append("<th data-filter='false' data-sorter='true'>XP %</th>");
                                
            sbTable.Append("<th data-filter='false' data-sorter='true'>Kickoffs</th>");
            sbTable.Append("<th data-filter='false' data-sorter='true'>Touchbacks</th>");


            sbTable.Append("</tr>");
            sbTable.Append("</thead>");

            sbTable.Append("<tbody>");


            foreach (DataRow item in ds.Tables[0].Rows)
            {
                sbTable.Append("<tr>");
                sbTable.Append(string.Format("<td class='c{0}'><div style='display:none;'>{0}</div></td>", item.Field<string>("displayName").Replace(" ", string.Empty)));
                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("seasonIndex") ));
                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("games")));
                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("fgAtt")));
                sbTable.Append(string.Format("<td>{0:n0}</td>", item.Field<int>("fgMade")));
                sbTable.Append(string.Format("<td>{0}</td>", Helper.GetPercent(item.Field<int>("fgAtt"), item.Field<int>("fgMade"))));

                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("fg50plusattempt")));
                sbTable.Append(string.Format("<td>{0:n0}</td>", item.Field<int>("fg50plusmade")));
                sbTable.Append(string.Format("<td>{0}</td>", Helper.GetPercent(item.Field<int>("fg50plusattempt"), item.Field<int>("fg50plusmade"))));

                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("Longest")));

                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("xpAttempt")));
                sbTable.Append(string.Format("<td>{0:n0}</td>", item.Field<int>("xpMade")));
                sbTable.Append(string.Format("<td>{0}</td>", Helper.GetPercent(item.Field<int>("xpAttempt"), item.Field<int>("xpMade"))));

                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("kickoff")));
                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("touchback")));

               


                sbTable.Append("</tr>");
            }
            sbTable.Append("</tbody>");
            sbTable.Append("</table>");

         
            tableKickingStats.InnerHtml = sbTable.ToString();
        }



        protected void UpdatePanel1_Unload(object sender, EventArgs e)
        {
            MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
            methodInfo.Invoke(ScriptManager.GetCurrent(Page), new object[] { sender as UpdatePanel });
        }

    }
}