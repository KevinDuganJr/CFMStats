using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using CFMStats.Classes;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;

namespace CFMStats
{
    public partial class _Default : Page
    {
        private string LocalFirebaseDataVar { get; set; }

        private string LocalFirebaseUrl { get; set; }

        protected void btnSetDefault_Click(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated) // user not signed in
            {
                Session["leagueID"] = hdnLeagueID.Value;
                Session["leagueName"] = hdnLeagueName.Value;

                Response.Redirect("/Schedule");
            }

            var SP = new StoredProc
            {
                Name = "LeagueDefault_update", DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, ParameterSet = new SqlCommand()
            };

            SP.ParameterSet.Parameters.AddWithValue("@ownerUserID", User.Identity.GetUserId());
            SP.ParameterSet.Parameters.AddWithValue("@leagueID", Helper.IntegerNull(hdnLeagueID.Value));

            var result = StoredProc.NonQuery(SP);
            if (result)
            {
                Session.Remove("leagueID");
                Session.Remove("leagueName");

                SetLeagueSession();
                
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "displayAlert('" + result + "');", true);
            }
        }

        protected void btnSetUpdate_Click(object sender, EventArgs e)
        {
            Session["exportID"] = hdnExportID.Value;
            Response.Redirect("~/Sync");
        }

        private void GetAllLeagues()
        {
            tblLeagues.InnerHtml = "No Leagues Created";

            var sp = new StoredProc
            {
                Name = "League_select", DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, ParameterSet = new SqlCommand()
            };

            sp.ParameterSet.Parameters.AddWithValue("@ownerUserID", DBNull.Value);
            sp.ParameterSet.Parameters.AddWithValue("@leagueID", DBNull.Value);

            var ds = StoredProc.ShowMeTheData(sp);

            if (ds.Tables.Count == 0)
            {
                return;
            }

            var sbTable = new StringBuilder();

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                var leagueId = Helper.IntegerNull(item["ID"]);

                var lastUpdated = Helper.DatetimeNull(item["lastUpdatedOn"]);

                if (lastUpdated.Year > 2000)
                {
                    lastUpdated = lastUpdated.AddHours(-6);
                }

                sbTable.Append("<div class='col-sm-3'>");
                //
                sbTable.Append("<div class='panel panel-primary'>");
                sbTable.Append($"<div class='panel-heading'><a href='Schedule/?leagueId={leagueId}' onclick=setCookie('LeagueId',{leagueId}); class='btn btn-info btn-sm'>Go To</a>&nbsp;&nbsp;{item.Field<string>("Name")}</div>");

                sbTable.Append("<div class='panel-body'>");
                sbTable.Append("<div class='col-xs-2'><strong>Users</strong></div>");
                sbTable.Append($"<div class='col-xs-10'>{BuildUsersList(item.Field<string>("Users"))}</div>");
                sbTable.Append("</div>");

                sbTable.Append($"<div class='panel-footer'><a href='/Sync?league={leagueId}' class='btn btn-success btn-sm'>Sync</a>&nbsp;&nbsp;<strong>Updated</strong> : <small>{lastUpdated:yyyy MMM dd @ h:mm:ss tt} CT</small></div>");
                sbTable.Append("</div>"); // end panel

                sbTable.Append("</div>"); // end col
            }

            tblLeagues.InnerHtml = sbTable.ToString();
        }

        /// <summary>
        ///     check firebase db for data collections
        /// </summary>
        protected Dictionary<int, string> GetDatabaseCollections()
        {
            var leagues = new Dictionary<int, string>();
            var count = 0;

            var content = GetUrlContents($"{LocalFirebaseUrl}/.json?shallow=true");

            if (content == "null")
            {
                // no data found
                leagues.Add(count, "");
                return leagues;
            }

            var o = JObject.Parse(content);

            foreach (var x in o)
            {
                var exportId = x.Key;
                leagues.Add(count++, x.Key);
            }

            return leagues;
        }

        /// <summary>
        ///     Returns JSON results
        /// </summary>
        private string GetUrlContents(string fileName)
        {
            string sContents;

            try
            {
                if (fileName.ToLower().IndexOf("https:", StringComparison.Ordinal) > -1)
                {
                    var wc = new WebClient();
                    var response = wc.DownloadData(fileName);

                    sContents = Encoding.ASCII.GetString(response);
                }
                else
                {
                    var sr = new StreamReader(fileName);
                    sContents = sr.ReadToEnd();
                    sr.Close();
                }
            }
            catch
            {
                sContents = "unable to connect to server ";
            }

            return sContents;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LocalFirebaseUrl = ConfigurationManager.AppSettings["localFirebaseURL"];
                LocalFirebaseDataVar = ConfigurationManager.AppSettings["localFirebaseDataVar"];

                GetAllLeagues();
            }
        }

        private void SetLeagueSession()
        {
            var sp = new StoredProc
            {
                Name = "LeagueDefault_select", DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, ParameterSet = new SqlCommand()
            };

            var userId = string.Empty;
            if (Request.IsAuthenticated)
            {
                userId = Context.User.Identity.GetUserId();
            }

            sp.ParameterSet.Parameters.AddWithValue("@ownerUserID", userId);

            var ds = StoredProc.ShowMeTheData(sp);

            if (ds.Tables.Count == 0)
            {
                return;
            }

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                Session["leagueID"] = Helper.IntegerNull(item["ID"]);
                Session["leagueName"] = Helper.StringNull(item["Name"]);
            }

            Response.Redirect($"/Schedule");
        }

        private string BuildUsersList(string users)
        {
            if (users == null)
            {
                return"No Users Found";
            }

            var selectUsers = new StringBuilder();
            var usersList = users.Split(',');

            selectUsers.Append("<select class='form-control input-xs'>");
            foreach (var user in usersList)
            {
                selectUsers.Append($"<option value='{user}'>{user}</option>");
            }

            selectUsers.Append("</select>");

            return selectUsers.ToString();
        }
    }
}