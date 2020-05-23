using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CFMStats.Classes.JSON;

namespace CFMStats
{
    public partial class LeagueTeams : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) { btnGetTeams_Click(null, null); }
        }

        protected void btnGetTeams_Click(object sender, EventArgs e)
        {
            tableFreeAgents.InnerHtml = "";

            var url = "https://dugan-760bc.firebaseio.com/data/ps3/218182/leagueteams/.json";
            var leagueTeams = getjson<JSONLeagueTeamInfo.Rootobject>(url);

            System.Text.StringBuilder sbTable = new System.Text.StringBuilder();
            sbTable.Append("<table id='sumtable' class='sum_table  tablesorter' >");

            sbTable.Append("<thead>");
            sbTable.Append("<tr>");
            sbTable.Append("<th data-sorter='true'>Display Name</th>");
            sbTable.Append("<th data-sorter='true'>Overall Rating</th>");
            sbTable.Append("<th data-sorter='true'>Division</th>");
            sbTable.Append("<th data-sorter='true'>Injury Count</th>");
            sbTable.Append("<th data-sorter='true'>OFF Scheme</th>");
            sbTable.Append("<th data-sorter='true'>DEF Scheme</th>");
            sbTable.Append("</tr>");
            sbTable.Append("</thead>");

            sbTable.Append("<tbody>");

            foreach (var item in leagueTeams.leagueTeamInfoList)
            {
                sbTable.Append("<tr>");
                sbTable.Append(string.Format("<td>{0}</td>", item.displayName));
                sbTable.Append(string.Format("<td>{0}</td>", item.ovrRating));
                sbTable.Append(string.Format("<td>{0}</td>", item.cityName));
                sbTable.Append(string.Format("<td>{0}</td>", item.divName));
                sbTable.Append(string.Format("<td>{0}</td>", item.injuryCount));
                sbTable.Append(string.Format("<td>{0}</td>", item.offScheme));
                sbTable.Append(string.Format("<td>{0}</td>", item.defScheme));
                sbTable.Append("</tr>");
            }
            sbTable.Append("</tbody>");
            sbTable.Append("</table>");
            tableFreeAgents.InnerHtml = sbTable.ToString();
        }


        /// <summary>
        /// Converts JSON to Custom Class
        /// </summary>
        private static T getjson<T>(string url) where T : new()
        {
            using (var w = new WebClient())
            {
                var json_data = string.Empty;
                // attempt to download JSON data as a string
                try
                {
                    json_data = w.DownloadString(url);
                }
                catch (Exception) { }

                // if string with JSON data is not empty, deserialize it to class and return its instance 
                return !string.IsNullOrEmpty(json_data) ? JsonConvert.DeserializeObject<T>(json_data) : new T();
            }
        }


    }
}