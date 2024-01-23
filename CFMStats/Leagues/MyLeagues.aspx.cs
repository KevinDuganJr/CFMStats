using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;
using CFMStats.Classes;
using Microsoft.AspNet.Identity;

namespace CFMStats.Leagues
{
    public partial class MyLeagues : Page
    {
        protected void GetLeagues()
        {
            tblLeagues.InnerHtml = "No Leagues Created";

            var sp = new StoredProc
            {
                Name = "League_select",
                DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString,
                ParameterSet = new SqlCommand()
            };

            var ds = StoredProc.ShowMeTheData(sp);

            if (ds.Tables.Count == 0)
            {
                return;
            }

            var userId = string.Empty;
            if (Request.IsAuthenticated)
            {
                userId = User.Identity.GetUserId();
            }

            var sbTable = new StringBuilder();
            sbTable.Append("<table class='table table-dark table-striped table-condensed'>");
            //<th data-sorter='true' data-toggle='tooltip' data-html='true' data-container='body' data-placement='top' title='Request number'
            sbTable.Append("<thead>");
            sbTable.Append("<tr>");
            sbTable.Append("<th>League Name</th>");
            sbTable.Append("<th>Export URL</th>");
            sbTable.Append("<th></th>");
            sbTable.Append("</tr>");
            sbTable.Append("</thead>");

            sbTable.Append("<tbody>");
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                if (userId == item.Field<string>("ownerUserID"))
                {
                    sbTable.Append("<tr>");
                    sbTable.Append($"<td style='padding-top: 16px;' class='leagueName'>{item["Name"]}</td>");

                    var url = $"{ConfigurationManager.AppSettings["localFirebaseURL"]}/{item["exportID"]}";
                    sbTable.Append($"<td class='exportURL'>{url}</td>");

                    sbTable.Append($"<td><a href='/Leagues/AddEditLeague?leagueId={item["Id"]}' class='btn btn-warning'>Edit</a></td>");
                    sbTable.Append("</tr>");
                }
            }

            sbTable.Append("</tbody>");
            sbTable.Append("</table>");

            tblLeagues.InnerHtml = sbTable.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                if (!IsPostBack)
                {
                    GetLeagues();
                }
            }
            else
            {
                Response.Redirect("~/");
            }
        }
    }
}