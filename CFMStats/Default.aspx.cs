using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.Providers.Entities;
using System.Web.UI;
using CFMStats.Classes;
using Microsoft.AspNet.Identity;

namespace CFMStats
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetAllLeagues();
            }
        }
        private void GetAllLeagues()
        {
            tblLeagues.InnerHtml = "No Leagues Created";

            var sp = new StoredProc
            {
                Name = "League_select",
                DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString,
                ParameterSet = new SqlCommand()
            };

            sp.ParameterSet.Parameters.AddWithValue("@ownerUserID", DBNull.Value);
            sp.ParameterSet.Parameters.AddWithValue("@leagueID", DBNull.Value);

            var ds = StoredProc.ShowMeTheData(sp);

            if (ds.Tables.Count == 0)
            {
                return;
            }

            if (!Request.IsAuthenticated) // user not signed in
            {
                tblLeagues.InnerHtml = "<div class='alert alert-warning' role='alert'>Click <a href='/Account/Login'>HERE</a> to login and view your leagues.</div>";
            } 
            else
            {
                tblLeagues.InnerHtml = LeaguesGrid(ds.Tables[0]);
            }
            
            tblAllLeagues.InnerHtml = TableOfLeagues(ds.Tables[0]);
        }

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
                Name = "LeagueDefault_update",
                DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString,
                ParameterSet = new SqlCommand()
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
        private void SetLeagueSession()
        {
            var sp = new StoredProc
            {
                Name = "LeagueDefault_select",
                DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString,
                ParameterSet = new SqlCommand()
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

            Response.Redirect("/Schedule");
        }
        private int GetLeagueUserCount(string users)
        {
            if (users == null)
            {
                return 0;
            }

            var selectUsers = new StringBuilder();
            var usersList = users.Split(',');

            return usersList.Length;
        }

        private string LeaguesGrid(DataTable dataTable)
        {
            var stages = new Stages();
            stages = stages.AllStages();

            var sbTable = new StringBuilder();

            sbTable.Append("<div class='row'>");

            var gridCount = 0;

            foreach (DataRow item in dataTable.Rows)
            {
                gridCount++;
                var userId = "";// "79bda6aa-74d4-4512-b91c-39f079e35bd4-kjd";

                if (Request.IsAuthenticated)
                {
                    userId = User.Identity.GetUserId();
                }

                if (userId == item.Field<string>("ownerUserID"))
                {
                    var leagueId = Helper.IntegerNull(item["ID"]);

                    var lastUpdated = Helper.DatetimeNull(item["lastUpdatedOn"]);

                    sbTable.Append("<div class='col-xs-12 col-sm-6 col-md-4 col-lg-4 col-xl-4 pb-3'>");
                    sbTable.Append("<div class='card text-center'>");

                    sbTable.Append($"<div class='card-header'><a href='Schedule/?leagueId={leagueId}' class='btn btn-success btn-sm' onclick=setCookie('LeagueId',{leagueId})>{item.Field<string>("Name")}</a></div>");

                    sbTable.Append("<div class='card-body'>");
                    var seasonType = stages[Helper.IntegerNull(item["Stage"])].SeasonType;
                    if (Helper.IntegerNull(item["WeekIndex"]) > 17)
                    {
                        seasonType = "Post Season";
                    }

                    sbTable.Append($"<span class='badge text-bg-secondary'>{Helper.IntegerNull(item["Season"])}</span>");
                    sbTable.Append($"&nbsp&nbsp<span class='badge text-bg-secondary'>{seasonType}</span>");
                    sbTable.Append($"&nbsp&nbsp<span class='badge text-bg-secondary'>{Helper.StringNull(item["Description"])}</span>");

                    sbTable.Append("<p></p>");

                    var teams = Helper.StringNull(item["Teams"]);
                    var teamsList = teams.Split(',');

                    if (teams.Length > 0)
                    {
                        foreach (var team in teamsList)
                        {
                            sbTable.Append($"<img src='/images/team/small/{team.Replace(" ", "")}.png' class='img-fluid' width='35' alt='{team}'/>");
                        }
                    }

                    sbTable.Append("</div>");

                    sbTable.Append($"<div class='card-footer'><a href='/Sync?league={leagueId}' class='fas fa-refresh fa-xl'></a>&nbsp;&nbsp;<small>{Helper.RelativeTime(lastUpdated)}</small></div>");
                    sbTable.Append("</div>");

                    sbTable.Append("</div>");
 
                }
            }

            sbTable.Append("</div>");

            return sbTable.ToString();
        }

        private string TableOfLeagues(DataTable leagues)
        {
            var stages = new Stages();
            stages = stages.AllStages();

            var sbTable = new StringBuilder();
            sbTable.Append("<table class='leaguesTable'>");

            sbTable.Append("<thead>");
            sbTable.Append("<tr>");
            sbTable.Append("<th>Name</th>");
            //sbTable.Append("<th>Members</th>");
            sbTable.Append("<th>Season</th>");
            sbTable.Append("<th>Stage</th>");
            sbTable.Append("<th>Week</th>");
            sbTable.Append("<th>Updated On</th>");
            sbTable.Append("</tr>");
            sbTable.Append("</thead>");

            sbTable.Append("<tbody>");

            foreach (DataRow item in leagues.Rows)
            {
                var leagueId = Helper.IntegerNull(item["ID"]);

                var lastUpdated = Helper.DatetimeNull(item["lastUpdatedOn"]);
                var members = GetLeagueUserCount(item.Field<string>("Users"));


                sbTable.Append("<tr>");
                sbTable.Append($"<td class='leagueTitle' style='text-align:left;vertical-align: middle;'><a href='Schedule/?leagueId={leagueId}' onclick=setCookie('LeagueId',{leagueId}); class='fas fa-external-link fa-2xl'></a>&nbsp;&nbsp;{item.Field<string>("Name")}</td>");
                //sbTable.Append($"<td style='text-align:center;vertical-align: middle;'>{members}</td>");

                sbTable.Append($"<td style='text-align:center;vertical-align: middle;'>{Helper.IntegerNull(item["Season"])}</td>");

                var seasonType = stages[Helper.IntegerNull(item["Stage"])].SeasonType;
                if (Helper.IntegerNull(item["WeekIndex"]) > 17)
                {
                    seasonType = "Post Season";
                }

                sbTable.Append($"<td style='text-align:center;vertical-align: middle;'>{seasonType}</td>");

                sbTable.Append($"<td style='text-align:center;vertical-align: middle;'>{Helper.StringNull(item["Description"])}</td>");

                sbTable.Append($"<td style='text-align:left; vertical-align: middle;'><a href='/Sync?league={leagueId}' class='fas fa-refresh fa-xl'></a>&nbsp;&nbsp;<span class='date-time'><small>{Helper.RelativeTime(lastUpdated)}</small></span></td>");
                sbTable.Append("</tr>");
            }

            sbTable.Append("</tbody>");
            sbTable.Append("</table>");

            return sbTable.ToString();
        }
    }
}