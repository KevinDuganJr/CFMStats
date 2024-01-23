using CFMStats.Classes;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI.WebControls;

namespace CFMStats
{
    public partial class LeagueHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
             // var ds =  GetTopPerformers();
               // PassingStats(ds.Tables[0]);
            }
        }

        private void PassingStats(DataTable stats)
        {
            var sbTable = new StringBuilder();

            sbTable.Append("<table class='table table-striped table-hover table-sm'>");

            sbTable.Append("<thead>");
            sbTable.Append("<tr class='table-light'>");
            sbTable.Append("<th>Player</th>");
            sbTable.Append("<th>Att</th>");
            sbTable.Append("<th>Comp</th>");
            sbTable.Append("<th>Yards</th>");
            sbTable.Append("<th>TDs</th>");
            sbTable.Append("<th>INTs</th>");
            sbTable.Append("</tr>");
            sbTable.Append("</thead>");

            sbTable.Append("<tbody>");
            foreach (DataRow item in stats.Rows)
            {
                sbTable.Append($"<tr class='table-secondary'>");

                sbTable.Append($"<td>{Helper.StringNull(item["fullName"])}</td>");
                sbTable.Append($"<td>{Helper.IntegerNull(item["passAtt"])}</td>");
                sbTable.Append($"<td>{Helper.IntegerNull(item["passComp"])}</td>");
                sbTable.Append($"<td>{Helper.IntegerNull(item["passYds"])}</td>");
                sbTable.Append($"<td>{Helper.IntegerNull(item["passTDs"])}</td>");
                sbTable.Append($"<td>{Helper.IntegerNull(item["passINTs"])}</td>");

                sbTable.Append($"</tr>");
            }
            sbTable.Append("</tbody>");

            sbTable.Append("</table>");

            //tblAfcPassing.InnerHtml = sbTable.ToString();
        }



        private DataSet GetTopPerformers()
        {
            var sp = new StoredProc
            {
                Name = "[LeagueTopPerformers_select]",
                DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString,
                ParameterSet = new SqlCommand()
            };

            sp.ParameterSet.Parameters.AddWithValue("@stageIndex", 1);
            sp.ParameterSet.Parameters.AddWithValue("@seasonIndex", 2024);
            sp.ParameterSet.Parameters.AddWithValue("@weekIndex", 0);
            sp.ParameterSet.Parameters.AddWithValue("@leagueID", 1136);
            sp.ParameterSet.Parameters.AddWithValue("@conference", "AFC");

            var ds = StoredProc.ShowMeTheData(sp);

            if (ds.Tables.Count == 0)
            {
                return ds;
            }

            return ds;
        }

    }
}