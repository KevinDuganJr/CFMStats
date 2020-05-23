using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using CFMStats.Classes;

namespace CFMStats.Controls
{
    public partial class ucReceivingStats : System.Web.UI.UserControl
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
            tableReceivingStats.InnerHtml = string.Empty;

            StoredProc SP = new StoredProc();
            SP.Name = "StatsReceiving_select";
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

            sbTable.Append("<th data-sorter='true'>Tgts</th>");
            sbTable.Append("<th data-sorter='true'>Rec</th>");

            sbTable.Append("<th data-sorter='true'>Yds</th>");
            sbTable.Append("<th data-sorter='true'>Yds/Rec</th>");

            if (isFull == true) { sbTable.Append("<th data-sorter='true'>Yds/Gms</th>"); }

            sbTable.Append("<th data-sorter='true'>TD's</th>");

            sbTable.Append("<th data-sorter='true'>YAC</th>");
            if (isFull == true) { sbTable.Append("<th data-sorter='true'>YAC/Gm</th>"); }

            sbTable.Append("<th data-sorter='true'>Long</th>");

            sbTable.Append("<th data-sorter='true'>Rec %</th>");

            sbTable.Append("<th data-sorter='true'>Drps</th>");
            sbTable.Append("<th data-sorter='true'>Drp %</th>");

            if (isFull == true) { sbTable.Append("<th data-sorter='true'>Games</th>"); }

            sbTable.Append("</tr>");
            sbTable.Append("</thead>");

            sbTable.Append("<tbody>");




            foreach (DataRow item in ds.Tables[0].Rows)
            {
                sbTable.Append("<tr>");
                sbTable.Append(string.Format("<td class='c{0}'><div style='display:none;'>{0}</div></td>", item.Field<string>("teamName").Replace(" ", string.Empty)));
                sbTable.Append(string.Format("<td>{0}</td>", item.Field<string>("position")));

                if (Helper.StringNull(item["firstName"]).Length == 0)
                {
                    sbTable.Append($"<td style='text-align:left;'>{item.Field<string>("fullName")}</td>");
                }
                else
                {
                    sbTable.Append(string.Format("<td style='text-align:left;'><a  target='_blank' href='/profile?id={2}'>{0} {1}</a></td>", item.Field<string>("firstName"), item.Field<string>("lastName"), item.Field<int>("playerId")));
                }

                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("targets")));
                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("receptions")));
                sbTable.Append(string.Format("<td>{0:n0}</td>", item.Field<int>("yards")));
                sbTable.Append(string.Format("<td>{0}</td>", Helper.GetAverage(item.Field<int>("yards"), item.Field<int>("receptions"))));

                if (isFull == true) { sbTable.Append(string.Format("<td>{0}</td>", Helper.GetAverage(item.Field<int>("yards"), item.Field<int>("games")))); }

                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("touchdowns")));
                sbTable.Append(string.Format("<td>{0:n0}</td>", item.Field<int>("YardsAfterCatch")));
                if (isFull == true) { sbTable.Append(string.Format("<td>{0}</td>", Helper.GetAverage(item.Field<int>("YardsAfterCatch"), item.Field<int>("games")))); }

                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("Longest")));
                sbTable.Append(string.Format("<td>{0}</td>", Helper.GetPercent(item.Field<int>("targets"), item.Field<int>("receptions"))));

                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("drops")));
                sbTable.Append(string.Format("<td>{0}</td>", Helper.GetPercent(item.Field<int>("targets"), item.Field<int>("drops"))));
                if (isFull == true) { sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("games"))); }

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