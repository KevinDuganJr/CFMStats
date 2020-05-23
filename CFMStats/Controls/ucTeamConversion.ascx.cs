using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using CFMStats.Classes;

namespace CFMStats.Controls
{
    public partial class ucTeamConversion : System.Web.UI.UserControl
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
            tblConversionStats.InnerHtml = string.Empty;

            StoredProc SP = new StoredProc();
            SP.Name = "TeamConversion_select";
            SP.DataConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SP.ParameterSet = new System.Data.SqlClient.SqlCommand();

            SP.ParameterSet.Parameters.AddWithValue("@stageIndex", iStageIndex);
            SP.ParameterSet.Parameters.AddWithValue("@leagueId", iLeagueId);
            SP.ParameterSet.Parameters.AddWithValue("@seasonIndex", iSeason);

            if (iWeek == 99)
                SP.ParameterSet.Parameters.AddWithValue("@weekIndex", DBNull.Value);
            else
                SP.ParameterSet.Parameters.AddWithValue("@weekIndex", iWeek);


            System.Data.DataSet ds = StoredProc.ShowMeTheData(SP);


            System.Text.StringBuilder sbTable = new System.Text.StringBuilder();

            sbTable.Append("<table id='tableStats' class='tableConversionStats tablesorter' >");

            sbTable.Append("<thead>");
            sbTable.Append("<tr>");
            //sbTable.Append("<th data-sorter='true' data-toggle='tooltip' data-html='true' data-container='body' data-placement='top' title='Team Name'>Team</th>");
            sbTable.Append("<th data-sorter='true' class='filter-select' data-placeholder='All'>Team</th>");
            sbTable.Append("<th data-sorter='true'>Division</th>");

            sbTable.Append("<th data-sorter='true'>3rd Att</th>");
            sbTable.Append("<th data-sorter='true'>3rd Conv</th>");
            sbTable.Append("<th data-sorter='true'>3rd %</th>");

            sbTable.Append("<th data-sorter='true'>4th Att</th>");
            sbTable.Append("<th data-sorter='true'>4th Conv</th>");
            sbTable.Append("<th data-sorter='true'>4th %</th>");

            sbTable.Append("<th data-sorter='true'>2PT Att</th>");
            sbTable.Append("<th data-sorter='true'>2PT Conv</th>");
            sbTable.Append("<th data-sorter='true'>2PT %</th>");

            sbTable.Append("<th data-sorter='true'>Games</th>");

            sbTable.Append("</tr>");
            sbTable.Append("</thead>");
            sbTable.Append("<tbody>");




            foreach (System.Data.DataRow item in ds.Tables[0].Rows)
            {
                sbTable.Append("<tr>");

                sbTable.Append(string.Format("<td class='c{0}'><div style='display:none;'>{0}</div></td>", item.Field<string>("teamName").Replace(" ", string.Empty)));
                sbTable.Append(string.Format("<td>{0}</td>", item.Field<string>("divName")));

                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("off3rdDownAtt")));
                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("off3rdDownConv")));
                sbTable.Append(string.Format("<td>{0}</td>", Helper.GetPercent(item.Field<int>("off3rdDownAtt"), item.Field<int>("off3rdDownConv"))));

                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("off4thDownAtt")));
                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("off4thDownConv")));
                sbTable.Append(string.Format("<td>{0}</td>", Helper.GetPercent(item.Field<int>("off4thDownAtt"), item.Field<int>("off4thDownConv"))));

                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("off2ptAtt")));
                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("off2PtConv")));
                sbTable.Append(string.Format("<td>{0}</td>", Helper.GetPercent(item.Field<int>("off2PtAtt"), item.Field<int>("off2ptConv"))));


                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("games")));


                sbTable.Append("</tr>");
            }
            sbTable.Append("</tbody>");
            sbTable.Append("</table>");


            tblConversionStats.InnerHtml = sbTable.ToString();
        }


        protected void UpdatePanel1_Unload(object sender, EventArgs e)
        {
            System.Reflection.MethodInfo methodInfo = typeof(ScriptManager).GetMethods(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
            methodInfo.Invoke(ScriptManager.GetCurrent(Page), new object[] { sender as UpdatePanel });
        }



    }
}