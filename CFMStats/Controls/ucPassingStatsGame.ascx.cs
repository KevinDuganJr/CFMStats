using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using CFMStats.Classes;

namespace CFMStats.Controls
{
    public partial class ucPassingStatsGame : System.Web.UI.UserControl
    {
        #region  Properties 

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
            tablePassingStatsWeek.InnerHtml = string.Empty;

            StoredProc SP = new StoredProc();
            SP.Name = "StatsPassingByWeek_select";
            SP.DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SP.ParameterSet = new System.Data.SqlClient.SqlCommand();

            SP.ParameterSet.Parameters.AddWithValue("@stageIndex", iStageIndex);
            SP.ParameterSet.Parameters.AddWithValue("@leagueId", iLeagueId);
            SP.ParameterSet.Parameters.AddWithValue("@playerid", intPlayerID);
 

            DataSet ds = StoredProc.ShowMeTheData(SP);


            if (ds.Tables[0].Rows.Count == 0)
            {
                panelPassingWeek.Visible = false;
                return;
            }

            System.Text.StringBuilder sbTable = new System.Text.StringBuilder();
            sbTable.Append("<table id='tablePassingStatsGame' class=' tablesorter' >");
            sbTable.Append("<thead>");
            sbTable.Append("<tr>");
            
            sbTable.Append("<th class='filter-select' data-placeholder='All'>Team</th>");

            sbTable.Append("<th class='filter-select' data-placeholder='All'>Away</th>");
            sbTable.Append("<th class='filter-select' data-placeholder='All'>Home</th>");
            sbTable.Append("<th class='filter-select' data-placeholder='All'>Season</th>");
            sbTable.Append("<th>Week</th>");

            sbTable.Append("<th>ATT</th>");
            sbTable.Append("<th>CMP</th>");
            sbTable.Append("<th>CMP %</th>");

            sbTable.Append("<th>Yards</th>");

            sbTable.Append("<th>TD</th>");

            sbTable.Append("<th>INT</th>");
            sbTable.Append("<th>Sack</th>");

            sbTable.Append("<th>Long</th>");
            sbTable.Append("<th>QBR</th>");

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
                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("week")+1));

                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("attempt")));
                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("completion")));
                sbTable.Append(string.Format("<td>{0}</td>", Helper.GetPercent(item.Field<int>("attempt"), item.Field<int>("completion"))));

                // double attempts, double completions, double touchdowns, double interceptions, double yards/

                sbTable.Append(string.Format("<td>{0:n0}</td>", item.Field<int>("yards")));
          //      sbTable.Append(string.Format("<td>{0}</td>", Helper.GetAverage(item.Field<int>("yards"), item.Field<int>("games")))); 


                //sbTable.Append(string.Format("<td>{0}</td>", Helper.CalculatePercent(item.Field<int>("targets"), item.Field<int>("drops"))));

                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("touchdown")));

                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("interception")));
                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("sack")));

                sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("Longest")));

                sbTable.Append(string.Format("<td>{0}</td>", Helper.CalculateQbRating(item.Field<int>("attempt"),
                                                                                item.Field<int>("completion"),
                                                                                item.Field<int>("touchdown"),
                                                                                item.Field<int>("interception"),
                                                                                item.Field<int>("yards")))); // rating
          //      sbTable.Append(string.Format("<td>{0}</td>", item.Field<int>("games"))); 

                sbTable.Append("</tr>");
            }
            sbTable.Append("</tbody>");
            sbTable.Append("</table>");



            tablePassingStatsWeek.InnerHtml = sbTable.ToString();
        }

        protected void UpdatePanel1_Unload(object sender, EventArgs e)
        {
            MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
            methodInfo.Invoke(ScriptManager.GetCurrent(Page), new object[] { sender as UpdatePanel });
        }


    }
}