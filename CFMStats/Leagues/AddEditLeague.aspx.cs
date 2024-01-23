using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using CFMStats.Classes;
using Microsoft.AspNet.Identity;

namespace CFMStats.Leagues
{
    public partial class AddEditLeague : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if(!IsPostBack)
            {
                var leagueId = Helper.IntegerNull(Request.QueryString["leagueId"]);

                if (leagueId > 0)
                {
                    SetLeagueInformation(leagueId);
                }
            }

        }

        private void SetLeagueInformation(int leagueId)
        {
            var league = GetLeague(leagueId);

            txtLeagueId.Text = league.Id.ToString();
            txtLeagueName.Text = league.Name;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtLeagueName.Text.Length < 5)
            {
                ShowAlert("danger alert-dismissible", "x", "WARNING", "Your league name must contain more than five characters.");
                return;
            }

            if (txtLeagueId.Text.Length < 1)
            {
                var exportId = GenerateLeagueExportId();

                var isUniqueExportId = false;
                var iCountLoops = 0;

                do
                {
                    isUniqueExportId = IsExportIdUnique(exportId);

                    iCountLoops++;
                    if (iCountLoops > 19)
                    {
                        break;
                    }
                } while (isUniqueExportId == false);

                if (InsertLeague(exportId))
                {
                    ShowAlert("success alert-dismissible", "x", "SUCCESS", $"Your league {txtLeagueName.Text.Trim()} has been created!");
                    Response.Redirect("~/Leagues/MyLeagues");
                }
            }
            else
            {
                var league = GetLeague(Helper.IntegerNull(txtLeagueId.Text));
                league.Name = txtLeagueName.Text.Trim();
                var result = UpdateLeague(league);

                if (result)
                {
                    ShowAlert("success alert-dismissible", "x", "SUCCESS", $"Your league {txtLeagueName.Text.Trim()} has been updated!");
                    Response.Redirect("~/Leagues/MyLeagues");
                }
            }
        }

        /// <summary>
        ///     We need the SQL League ID
        /// </summary>
        public League GetLeague(int leagueId)
        {
            var SP = new StoredProc
            {
                Name = "League_select",
                DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString,
                ParameterSet = new SqlCommand()
            };

            SP.ParameterSet.Parameters.AddWithValue("@ownerUserId", DBNull.Value);
            SP.ParameterSet.Parameters.AddWithValue("@leagueId", leagueId);

            var ds = StoredProc.ShowMeTheData(SP);

            if (ds.Tables.Count == 0)
            {
                return null;
            }

            var leagueDetails = new League();

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                leagueDetails.Id = Helper.IntegerNull(item["ID"]);

                leagueDetails.Name = Helper.StringNull(item["Name"]);
            }

            return leagueDetails;
        }

        /// <summary>
        ///     create/insert the new league 
        /// </summary>
        private bool InsertLeague(string exportId)
        {
            var SP = new StoredProc
            {
                Name = "League_insert",
                DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString,
                ParameterSet = new SqlCommand()
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


        private bool UpdateLeague(League league)
        {
            var sql = new StringBuilder();

            sql.Append($"UPDATE tblLeague SET Name = '{league.Name}' WHERE Id = {league.Id};");

            var SP = new StoredProc
            {
                Name = sql.ToString(),
                DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString,
                IsSqlCommand = true,
                ParameterSet = new SqlCommand()
            };

            return StoredProc.NonQuery(SP);
        }

        /// <summary>
        ///     Generate export ID
        /// </summary>
        public string GenerateLeagueExportId()
        {
            var randomizeMe = new Random(Environment.TickCount);

            const string characters = "abcdefghijklmnopqrstuvwxyz0123456789";
            var builder = new StringBuilder(5);

            for (var i = 0; i < 5; ++i)
            {
                builder.Append(characters[randomizeMe.Next(characters.Length)]);
            }

            return builder.ToString();
        }

        public bool IsExportIdUnique(string exportId)
        {
            var SP = new StoredProc
            {
                Name = "LeagueId_select",
                DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString,
                ParameterSet = new SqlCommand()
            };

            SP.ParameterSet.Parameters.AddWithValue("@exportId", exportId);

            var ds = StoredProc.ShowMeTheData(SP);

            return ds.Tables.Count == 0;
        }


        /// <summary>
        /// Display Alert Message
        /// </summary>
        private void ShowAlert(string css, string image, string header, string details)
        {
            //alert alert-warning alert-dismissible
            pnlMessage.Visible = true;
            pnlMessage.CssClass = $"alert alert-{css}";
            if (image == "x")
            {
                lblAlert.Text = $"<strong>{header}</strong> {details}";
            }
            else
            {
                lblAlert.Text = $"<img src=\"/images/{image}.png\"> <strong>{header}</strong> {details}";
            }
        }
    }
}