using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;
using CFMStats.Classes;
using Microsoft.AspNet.Identity;

namespace CFMStats
{
    public partial class LeagueSettings : Page
    {
        /// <summary>
        ///     Generate export ID
        /// </summary>
        public string GenerateLeagueExportId()
        {
            var randomizeMe = new Random(Environment.TickCount);

            var chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            var builder = new StringBuilder(5);

            for (var i = 0; i < 5; ++i)
            {
                builder.Append(chars[randomizeMe.Next(chars.Length)]);
            }

            return builder.ToString();
        }

        protected void btnCreateLeague_Click(object sender, EventArgs e)
        {
            if (txtLeagueName.Text.Length < 5)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "displayAlert('Please enter a longer league name and try again.');", true);

                return;
            }

            var exportID = GenerateLeagueExportId();
            var leagueID = 0;
            var iCountLoops = 0;

            do
            {
                leagueID = GetLeagueId(exportID);

                iCountLoops++;
                if (iCountLoops > 19)
                {
                    break;
                } // lets not create infinite loop hell, k thanks
            } while (leagueID > 1);

            if (InsertLeague(exportID))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "displayAlert('League added!');", true);
                GetLeagues();
            }
        }

        protected void btnLoadLeagues_Click(object sender, EventArgs e)
        {
            GetLeagues();
        }

        protected void GetLeagues()
        {
            tblLeagues.InnerHtml = "No Leagues Created";

            var sp = new StoredProc
            {
                Name = "League_select", DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, ParameterSet = new SqlCommand()
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
            sbTable.Append("<table id='sumtable' class='table tablesorter' >");
            //<th data-sorter='true' data-toggle='tooltip' data-html='true' data-container='body' data-placement='top' title='Request number'
            sbTable.Append("<thead>");
            sbTable.Append("<tr>");
            sbTable.Append("<th>League Name</th>");
            sbTable.Append("<th>Export URL</th>");
            sbTable.Append("</tr>");
            sbTable.Append("</thead>");

            sbTable.Append("<tbody>");
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                if (userId == item.Field<string>("ownerUserID"))
                {
                    sbTable.Append("<tr>");
                    sbTable.Append($"<td class='leagueName'>{item.Field<string>("Name")}</td>");
                    sbTable.Append($"<td class='exportURL'>{ConfigurationManager.AppSettings["exportURL"]}/{item.Field<string>("exportID").ToLower()}</td>");
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

        /// <summary>
        ///     We need the SQL League ID
        /// </summary>
        private int GetLeagueId(string exportId)
        {
            var leagueID = 0;
            var SP = new StoredProc
            {
                Name = "LeagueID_select", DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, ParameterSet = new SqlCommand()
            };

            SP.ParameterSet.Parameters.AddWithValue("@exportID", exportId);

            var ds = StoredProc.ShowMeTheData(SP);

            if (ds.Tables.Count == 0)
            {
                return 0;
            }

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                leagueID = Helper.IntegerNull(item["ID"]);
            }

            return leagueID;
        }

        /// <summary>
        ///     create/insert the new league  woohooo
        /// </summary>
        private bool InsertLeague(string exportId)
        {
            var SP = new StoredProc
            {
                Name = "League_insert", DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, ParameterSet = new SqlCommand()
            };

            // don't let greater than 50 characters in there
            if (txtLeagueName.Text.Length > 50)
            {
                txtLeagueName.Text = txtLeagueName.Text.Substring(0, 50);
            }

            SP.ParameterSet.Parameters.AddWithValue("@leagueName", txtLeagueName.Text); // TODO: USE REGEX ELIMINATE BAD DIGITS
            SP.ParameterSet.Parameters.AddWithValue("@exportID", exportId);
            SP.ParameterSet.Parameters.AddWithValue("@ownerUserID", User.Identity.GetUserId());

            return StoredProc.NonQuery(SP);
        }
    }
}