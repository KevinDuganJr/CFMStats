using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using CFMStats.Classes;

namespace CFMStats.Controls
{
    public partial class ucPuntingStats : System.Web.UI.UserControl
    {

        #region Properties
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

        private int _iTop;
        public int iTop
        {
            get { return _iTop; }
            set { _iTop = value; }
        }

        private bool _isFull;
        public bool isFull
        {
            get { return _isFull; }
            set { _isFull = value; }
        }

        private int _iTeamID;
        public int iTeamID
        {
            get { return _iTeamID; }
            set { _iTeamID = value; }
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
            tablePuntingStats.InnerHtml = string.Empty;

            StoredProc SP = new StoredProc();
            SP.Name = "StatsPunting_select";
            SP.DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SP.ParameterSet = new System.Data.SqlClient.SqlCommand();

            SP.ParameterSet.Parameters.AddWithValue("@stageIndex", iStageIndex);
            SP.ParameterSet.Parameters.AddWithValue("@leagueId", iLeagueId);
            SP.ParameterSet.Parameters.AddWithValue("@seasonIndex", iSeason);

            if (iWeek == 99)
                SP.ParameterSet.Parameters.AddWithValue("@weekIndex", DBNull.Value);
            else
                SP.ParameterSet.Parameters.AddWithValue("@weekIndex", iWeek);

            if (iTop == 0)
                SP.ParameterSet.Parameters.AddWithValue("@top", DBNull.Value);
            else
                SP.ParameterSet.Parameters.AddWithValue("@top", iTop);

            if (iTeamID == 0)
                SP.ParameterSet.Parameters.AddWithValue("@teamID", DBNull.Value);
            else
                SP.ParameterSet.Parameters.AddWithValue("@teamID", iTeamID);

            DataSet ds = StoredProc.ShowMeTheData(SP);

            //            if (ds.Tables.Count == 0) { return collection; }


            System.Text.StringBuilder sbTable = new System.Text.StringBuilder();
            
            if (isFull == true)
                sbTable.Append("<table id='tableStats' class='tableDefenseStats tablesorter' >");
            else
                sbTable.Append("<table id='tableStats' class='tableDefenseStats table table-condensed table-bordered tablesorter' >");


            sbTable.Append("<thead>");
            sbTable.Append("<tr>");
            //sbTable.Append("<th data-sorter='true' data-toggle='tooltip' data-html='true' data-container='body' data-placement='top' title='Team Name'>Team</th>");
            sbTable.Append("<th data-sorter='true' class='filter-select' data-placeholder='All'>Team</th>");
            sbTable.Append("<th data-sorter='true' class='filter-select' data-placeholder='All'>Pos</th>");
            sbTable.Append("<th data-sorter='true'>Player</th>");

            sbTable.Append("<th data-sorter='true'>Punts</th>");
            sbTable.Append("<th data-sorter='true'>Yards</th>");
            sbTable.Append("<th data-sorter='true'>Yards/Punt</th>");

            sbTable.Append("<th data-sorter='true'>Net Yards</th>");
            sbTable.Append("<th data-sorter='true'>Net Yards/Punt</th>");

            sbTable.Append("<th data-sorter='true'>Blocked</th>");

            sbTable.Append("<th data-sorter='true'>In 20</th>");

            sbTable.Append("<th data-sorter='true'>Long</th>");
            if (isFull == true) { sbTable.Append("<th data-sorter='true'>Games</th>"); }

            sbTable.Append("</tr>");
            sbTable.Append("</thead>");

            sbTable.Append("<tbody>");




            foreach (DataRow item in ds.Tables[0].Rows)
            {
                sbTable.Append("<tr>");
                sbTable.Append(string.Format("<td class='c{0}'><div style='display:none;'>{0}</div></td>", item.Field<string>("teamName").Replace(" ", string.Empty)));
                sbTable.Append(string.Format("<td>{0}</td>", item.Field<string>("position")));
                //if (item.Field<int>("injuryType") != 97 && item.Field<bool>("isActive") == false)
                //{
                //    sbTable.Append(string.Format("<td class='injured' style='text-align:left;'>{0} {1}</td>", item.Field<string>("firstName"), item.Field<string>("lastName")));
                //}
                //else
                //{
                sbTable.Append(string.Format("<td style='text-align:left;'><a  target='_blank' href='/profile?id={2}'>{0} {1}</a></td>", item.Field<string>("firstName"), item.Field<string>("lastName"), item.Field<int>("playerId")));
                //}




                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("punt")));
                sbTable.Append(string.Format("<td>{0:n0}</td>", item.Field<int>("yards")));
                sbTable.Append(string.Format("<td>{0}</td>", Helper.GetAverage(item.Field<int>("yards"), item.Field<int>("punt"))));

                sbTable.Append(string.Format("<td>{0:n0}</td>", item.Field<int>("netyards")));
                sbTable.Append(string.Format("<td>{0}</td>", Helper.GetAverage(item.Field<int>("netyards"), item.Field<int>("punt"))));

                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("blocked")));

                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("in20")));

                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("Longest")));
                if (isFull == true) { sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("games"))); }


                sbTable.Append("</tr>");
            }
            sbTable.Append("</tbody>");
            sbTable.Append("</table>");

            ////< !--pager-- >
            //sbTable.Append("<div id='pager' class='pager'>");
            //sbTable.Append("<form>");
            //sbTable.Append("<img src='Content\\tablesorter\\images\\first.png' class='first'/>");
            //sbTable.Append("<img src='Content\\tablesorter\\images\\prev.png' class='prev'/>");
            ////<!-- the 'pagedisplay' can be any element, including an input -->
            //sbTable.Append("<span class='pagedisplay' data-pager-output-filtered='{startRow:input} &ndash; {endRow} / {filteredRows} of {totalRows} total rows'></span>");
            //sbTable.Append("<img src='Content\\tablesorter\\images\\next.png' class='next'/>");
            //sbTable.Append("<img src='Content\\tablesorter\\images\\last.png' class='last'/>");
            //sbTable.Append("<select class='pagesize'>");
            //sbTable.Append("<option value='15'>15</ option >");
            //sbTable.Append("<option value='50'>50</option>");
            //sbTable.Append("<option value='100'>100</ option >");
            //sbTable.Append("<option value='all'>All Rows</option>");
            //sbTable.Append("</select>");
            //sbTable.Append("</form>");
            //sbTable.Append("</div>");

            tablePuntingStats.InnerHtml = sbTable.ToString();
        }



        protected void UpdatePanel1_Unload(object sender, EventArgs e)
        {
            MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
            methodInfo.Invoke(ScriptManager.GetCurrent(Page), new object[] { sender as UpdatePanel });
        }


    }
}