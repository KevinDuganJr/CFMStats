using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using CFMStats.Classes;

namespace CFMStats.Controls
{
    public partial class ucTeamTurnovers : System.Web.UI.UserControl
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
            tblTurnoversStats.InnerHtml = string.Empty;

            StoredProc SP = new StoredProc();
            SP.Name = "TeamTurnovers_select";
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

            sbTable.Append("<table id='tableStats' class='tableTurnoversStats tablesorter' >");

            sbTable.Append("<thead>");
            sbTable.Append("<tr>");
            //sbTable.Append("<th data-sorter='true' data-toggle='tooltip' data-html='true' data-container='body' data-placement='top' title='Team Name'>Team</th>");
            sbTable.Append("<th data-sorter='true' class='filter-select' data-placeholder='All'>Team</th>");
            sbTable.Append("<th data-sorter='true'>Division</th>");

            sbTable.Append("<th data-sorter='true'>TO Diff</th>");

            sbTable.Append("<th data-sorter='true'>Giveaway</th>");
            sbTable.Append("<th data-sorter='true'>Off INT</th>");
            sbTable.Append("<th data-sorter='true'>Off Fum</th>");

            sbTable.Append("<th data-sorter='true'>Takeaway</th>");
            sbTable.Append("<th data-sorter='true'>Def INT</th>");
            sbTable.Append("<th data-sorter='true'>Def Fum</th>");

            sbTable.Append("<th data-sorter='true'>Games</th>");

            sbTable.Append("</tr>");
            sbTable.Append("</thead>");
            sbTable.Append("<tbody>");


            foreach (DataRow item in ds.Tables[0].Rows)
            {
                sbTable.Append("<tr>");

                sbTable.Append(string.Format("<td class='c{0}'><div style='display:none;'>{0}</div></td>", item.Field<string>("teamName").Replace(" ", string.Empty)));
                sbTable.Append(string.Format("<td>{0}</td>", item.Field<string>("divName")));

                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("tODiff")));

                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("tOGiveaways")));
                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("offIntsLost")));
                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("offFumLost")));

                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("tOTakeaways")));
                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("defIntsRec")));
                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("defFumRec")));

                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("games")));


                sbTable.Append("</tr>");
            }
            sbTable.Append("</tbody>");
            sbTable.Append("</table>");


            tblTurnoversStats.InnerHtml = sbTable.ToString();
        }


        protected void UpdatePanel1_Unload(object sender, EventArgs e)
        {
            System.Reflection.MethodInfo methodInfo = typeof(ScriptManager).GetMethods(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
            methodInfo.Invoke(ScriptManager.GetCurrent(Page), new object[] { sender as UpdatePanel });
        }

    }
}