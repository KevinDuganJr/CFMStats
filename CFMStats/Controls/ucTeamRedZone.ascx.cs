using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using CFMStats.Classes;

namespace CFMStats.Controls
{
    public partial class ucTeamRedZone : System.Web.UI.UserControl
    {

        #region - Properties -
        private int _iStageIndex;
        public int iStageIndex
        {
            get { return _iStageIndex; }
            set { _iStageIndex = value; }
        }

        private int _iSeason;
        public int iSeason
        {
            get { return _iSeason; }
            set { _iSeason = value; }
        }

        private int _iWeek;
        public int iWeek
        {
            get { return _iWeek; }
            set { _iWeek = value; }
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
            BuildTable(iSeason, iWeek);
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BuildTable(int Season, int Week)
        {
            tblRedZonStats.InnerHtml = string.Empty;

            StoredProc SP = new StoredProc();
            SP.Name = "TeamRedZone_select";
            SP.DataConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SP.ParameterSet = new System.Data.SqlClient.SqlCommand();

            SP.ParameterSet.Parameters.AddWithValue("@stageIndex", iStageIndex);
            SP.ParameterSet.Parameters.AddWithValue("@leagueId", iLeagueId);
            SP.ParameterSet.Parameters.AddWithValue("@seasonIndex", iSeason);

            if (iWeek == 99)
                SP.ParameterSet.Parameters.AddWithValue("@weekIndex", DBNull.Value);
            else
                SP.ParameterSet.Parameters.AddWithValue("@weekIndex", iWeek);


            DataSet ds = StoredProc.ShowMeTheData(SP);


            System.Text.StringBuilder sbTable = new System.Text.StringBuilder();

            sbTable.Append("<table id='tableStats' class='tableRedZoneStats tablesorter' >");

            sbTable.Append("<thead>");
            sbTable.Append("<tr>");
            //sbTable.Append("<th data-sorter='true' data-toggle='tooltip' data-html='true' data-container='body' data-placement='top' title='Team Name'>Team</th>");
            sbTable.Append("<th data-sorter='true' class='filter-select' data-placeholder='All'>Team</th>");
            sbTable.Append("<th data-sorter='true'>Division</th>");

            sbTable.Append("<th data-sorter='true'>Red Zone Att</th>");
            sbTable.Append("<th data-sorter='true'>Red Zone FG's</th>");
            sbTable.Append("<th data-sorter='true'>Red Zone TD's</th>");
            sbTable.Append("<th data-sorter='true'>Red Zone %</th>");

            sbTable.Append("<th data-sorter='true'>Opp Red Zone Att</th>");
            sbTable.Append("<th data-sorter='true'>Opp Red Zone FG's</th>");
            sbTable.Append("<th data-sorter='true'>Opp Red Zone TD's</th>");
            sbTable.Append("<th data-sorter='true'>Opp Red Zone %</th>");

            sbTable.Append("<th data-sorter='true'>Games</th>");

            sbTable.Append("</tr>");
            sbTable.Append("</thead>");
            sbTable.Append("<tbody>");


            foreach (DataRow item in ds.Tables[0].Rows)
            {
                sbTable.Append("<tr>");

                sbTable.Append(string.Format("<td class='c{0}'><div style='display:none;'>{0}</div></td>", item.Field<string>("teamName").Replace(" ", string.Empty)));
                sbTable.Append(string.Format("<td>{0}</td>", item.Field<string>("divName")));

                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("offRedZones")));
                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("offRedZoneFGs")));
                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("offRedZoneTDs")));
                sbTable.Append(string.Format("<td>{0}</td>", Helper.GetPercent(item.Field<int>("offRedZones"), item.Field<int>("offRedZoneFGs") + item.Field<int>("offRedZoneTDs"))));


                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("defRedZones")));
                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("defRedZoneFGs")));
                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("defRedZoneTDs")));
                sbTable.Append(string.Format("<td>{0}</td>", Helper.GetPercent(item.Field<int>("defRedZones"), item.Field<int>("defRedZoneFGs") + item.Field<int>("defRedZoneTDs"))));
            
                
                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("games")));


                sbTable.Append("</tr>");
            }
            sbTable.Append("</tbody>");
            sbTable.Append("</table>");


            tblRedZonStats.InnerHtml = sbTable.ToString();
        }


        protected void UpdatePanel1_Unload(object sender, EventArgs e)
        {
            System.Reflection.MethodInfo methodInfo = typeof(ScriptManager).GetMethods(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
            methodInfo.Invoke(ScriptManager.GetCurrent(Page), new object[] { sender as UpdatePanel });
        }
    }
}