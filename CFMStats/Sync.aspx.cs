using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using CFMStats.Classes;
using CFMStats.Classes.JSON;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CFMStats
{
    public partial class Sync : Page
    {
        private string localFirebaseDataVar { get; set; } = ConfigurationManager.AppSettings["localFirebaseDataVar"];

        private string localFirebaseURL { get; set; } = ConfigurationManager.AppSettings["localFirebaseURL"];

        protected void btnDeleteDatabase_OnClick(object sender, EventArgs e)
        {
            var exportId = GetExportId(Helper.IntegerNull(Request.QueryString["league"]));
            var url = $"{ConfigurationManager.AppSettings["localFirebaseURL"]}/delete/{exportId}";

            Response.Redirect(url);
        }

        protected void btnRosterUpdate_OnClick(object sender, EventArgs e)
        {
            var exportId = GetExportId(Helper.IntegerNull(Request.QueryString["league"]));
            var leagueId = Helper.IntegerNull(Request.QueryString["league"]);

            ProcessPlayers(exportId, leagueId);
        }

        protected void btnScheduleUpdate_OnClick(object sender, EventArgs e)
        {
            var exportId = GetExportId(Helper.IntegerNull(Request.QueryString["league"]));
            var leagueId = Helper.IntegerNull(Request.QueryString["league"]);

            ProcessSchedule(exportId, leagueId);

            // update team stats separate -- this updates the total 
            var selectedWeek = Helper.StringNull(ddlWeek.SelectedItem.Value);
            var values = selectedWeek.Split('^');

            var seasonType = Helper.StringNull(values[0]);
            var week = Helper.IntegerNull(values[1]);

            UpdateTeamStatsInfo(exportId, leagueId, seasonType, week);
        }

        protected void btnStatsUpdate_OnClick(object sender, EventArgs e)
        {
            var selectedWeek = Helper.StringNull(ddlWeek.SelectedItem.Value);
            var values = selectedWeek.Split('^');

            var exportId = GetExportId(Helper.IntegerNull(Request.QueryString["league"]));
            var leagueId = Helper.IntegerNull(Request.QueryString["league"]);
            var seasonType = Helper.StringNull(values[0]);
            var week = Helper.IntegerNull(values[1]);

            ProcessPlayerStats(exportId, leagueId, seasonType, week);
        }

        protected void btnTeamStandingsUpdate_OnClick(object sender, EventArgs e)
        {
            tblResults.InnerHtml = string.Empty;

            var exportId = GetExportId(Helper.IntegerNull(Request.QueryString["league"]));
            var leagueId = Helper.IntegerNull(Request.QueryString["league"]);

            var sb = new StringBuilder();

            sb.Append("<table class='table table-condensed table-striped tablesorter'>");
            sb.Append("<thead>");
            sb.Append("<tr>");
            sb.Append("<th>Update Task</th>");
            sb.Append("<th>Completed in</th>");
            sb.Append("</thead>");

            sb.Append("<tbody>");

            var startTime = DateTime.UtcNow;
            ProcessTeamInfo(exportId, leagueId); // update team information 
            sb.Append($"<tr><td>Team Information</td><td>{Helper.TimeElapsed(startTime)}</td></tr>");

            startTime = DateTime.UtcNow;
            ProcessTeamStandings(exportId, leagueId); // update team standings
            sb.Append($"<tr><td>Team Standings</td><td>{Helper.TimeElapsed(startTime)}</td></tr>");

            sb.Append("</tbody>");
            sb.Append("</table>");

            tblResults.InnerHtml = sb.ToString();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (phUrlHolder.Controls.Count > 0)
            {
                phUrlHolder.Controls.Clear();
            }

            var exportId = GetExportId(Helper.IntegerNull(Request.QueryString["league"]));

            try
            {
                // ProcessSyncFromJsonToSql(exportId);
                //   DeleteFirebase(exportId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var scriptMan = ScriptManager.GetCurrent(this);
            if (scriptMan != null)
            {
                scriptMan.AsyncPostBackTimeout = 36000;
            }

            if (IsPostBack)
            {
                return;
            }

            if (Helper.StringNull(Request.QueryString["league"]).Length == 0)
            {
                // no league, go back to start
                Response.Redirect("~/");
            }

            var exportId = GetExportId(Helper.IntegerNull(Request.QueryString["league"]));

            DisplayLeagueName(Helper.IntegerNull(Request.QueryString["league"]));

            CheckFirebaseForWeeklyStats(exportId);

            CheckFirebaseForRosters(exportId);

            CheckFirebaseForTeamInfo(exportId);
        }

        private void CheckFirebaseForRosters(string exportId)
        {
            var url = $"{localFirebaseURL}/{exportId}/{localFirebaseDataVar}/team/.json";
            var content = GetContentsFromUrl($"{url}?shallow=true");

            if (content == "null")
            {
                btnRosterUpdate.Visible = false;
                return;
            } // no data found

            btnRosterUpdate.Visible = true;
        }

        private void CheckFirebaseForTeamInfo(string exportId)
        {
            var url = $"{localFirebaseURL}/{exportId}/{localFirebaseDataVar}/standings/.json";
            var content = GetContentsFromUrl($"{url}?shallow=true");

            if (content == "null")
            {
                btnTeamStandingsUpdate.Visible = false;
                return;
            } // no data found

            btnTeamStandingsUpdate.Visible = true;
        }

        // check the exported Firebase DB 

        private void CheckFirebaseForWeeklyStats(string exportId)
        {
            var url = $"{localFirebaseURL}/{exportId}/{localFirebaseDataVar}/week/.json";
            var content = GetContentsFromUrl($"{url}?shallow=true");

            if (content == "null")
            {
                ddlWeek.Visible = false;
                btnStatsUpdate.Visible = false;
                btnScheduleUpdate.Visible = false;
                // no data found
                return;
            }

            ddlWeek.Visible = true;
            btnStatsUpdate.Visible = true;
            btnScheduleUpdate.Visible = true;

            var table = new DataTable();
            table.Columns.Add("SeasonType", typeof(string));
            table.Columns.Add("Week", typeof(int));

            var seasonTypes = JObject.Parse(content);

            foreach (var x in seasonTypes)
            {
                var stageIndexType = x.Key;

                var content2 = GetContentsFromUrl($"{localFirebaseURL}/{exportId}/{localFirebaseDataVar}/week/{stageIndexType}/.json?shallow=true");

                var weeks = JObject.Parse(content2);

                foreach (var y in weeks)
                {
                    var week = Helper.IntegerNull(y.Key);

                    var statsUrl = $"{localFirebaseURL}/{exportId}/{localFirebaseDataVar}/week/{stageIndexType}/{week}/passing/.json";
                    var statsContent = GetContentsFromUrl($"{statsUrl}?shallow=true");
                    if (statsContent != "null")
                    {
                        table.Rows.Add(x.Key, y.Key);
                    }
                }
            }

            var dv = table.DefaultView;
            dv.Sort = "SeasonType DESC, Week DESC";
            table = dv.ToTable();
            var seasonType = string.Empty;

            foreach (DataRow row in table.Rows)
            {
                var stageIndexType = Helper.StringNull(row["SeasonType"]);
                var seasonWeek = Helper.IntegerNull(row["Week"]);

                if (seasonWeek > 17)
                {
                    var postSeason = string.Empty;
                    switch (seasonWeek)
                    {
                        case 18:
                            postSeason = "Wild Card";
                            break;
                        case 19:
                            postSeason = "Divisional";
                            break;
                        case 20:
                            postSeason = "Conference";
                            break;
                        case 22:
                            postSeason = "Super Bowl";
                            break;
                    }

                    ddlWeek.Items.Add(new ListItem($"{postSeason}", $"{stageIndexType}^{seasonWeek}"));
                }
                else
                {
                    if (Helper.StringNull(row["SeasonType"]) == "pre")
                    {
                        seasonType = "Pre";
                    }

                    if (Helper.StringNull(row["SeasonType"]) == "reg")
                    {
                        seasonType = "Reg";
                    }

                    ddlWeek.Items.Add(new ListItem($"{seasonType} Week {seasonWeek}", $"{stageIndexType}^{seasonWeek}"));
                }
            }
        }

        private static T ConvertJsonToClass<T>(string URL)
        {
            var json_data = string.Empty;

            using (var webClient = new WebClient())
            {
                json_data = webClient.DownloadString(URL);
            }

            return JsonConvert.DeserializeObject<T>(json_data);
        }

        private static T ConvertJsonToRotoObject<T>(string url) where T : new()
        {
            // https://www.codeproject.com/Tips/397574/Use-Csharp-to-get-JSON-Data-from-the-Web-and-Map-i
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

        // misc 

        private void DisplayLeagueName(int leagueId)
        {
            var sp = new StoredProc
            {
                Name = "League_select",
                DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"]
                    .ConnectionString,
                ParameterSet = new SqlCommand()
            };

            sp.ParameterSet.Parameters.AddWithValue("@ownerUserId", DBNull.Value);
            sp.ParameterSet.Parameters.AddWithValue("@leagueId", leagueId);

            var ds = StoredProc.ShowMeTheData(sp);

            if (ds.Tables.Count == 0)
            {
                return;
            }

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                divLeague.InnerHtml = Helper.StringNull(item["Name"]);
            }
        }

        // deserializing JSON

        private string GetContentsFromUrl(string url)
        {

            //var consumeJson = System.IO.File.ReadAllText(url);
            //var consumedData = JsonConvert.DeserializeObject<JSONDefenseStats>(consumeJson);
            

            string sContents;

            try
            {
                if (url.ToLower().IndexOf("https:", StringComparison.Ordinal) > -1)
                {
                    var wc = new WebClient();
                    var response = wc.DownloadData(url);

                    sContents = Encoding.ASCII.GetString(response);
                }
                else
                {
                    var sr = new StreamReader(url);
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

        private string GetExportId(int leagueId)
        {
            var export = "";
            var sp = new StoredProc
            {
                Name = "League_select",
                DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString,
                ParameterSet = new SqlCommand()
            };

            var userId = string.Empty;
            if (Request.IsAuthenticated)
            {
                userId = User.Identity.GetUserId();
            }

            sp.ParameterSet.Parameters.AddWithValue("@ownerUserID", userId);

            var ds = StoredProc.ShowMeTheData(sp);

            if (ds.Tables.Count == 0)
            {
                return export;
            }

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                if (leagueId == item.Field<int>("ID"))
                {
                    export = item.Field<string>("exportId");
                    break;
                }
            }

            return export.ToLower();
        }

        private static async Task<object> PostCallAPI(string url, object jsonObject)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(url, content);
                    if (response != null)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<object>(jsonString);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

            return null;
        }

        //  display table of what was completed

        private void ProcessPlayers(string exportId, int leagueId)
        {
            var sb = new StringBuilder();

            sb.Append("<table class='table table-condensed table-striped tablesorter'>");
            sb.Append("<thead>");
            sb.Append("<tr>");
            sb.Append("<th>Update Task</th>");
            sb.Append("<th>Completed in</th>");
            sb.Append("</thead>");

            sb.Append("<tbody>");

            var startTime = DateTime.UtcNow;
            UpdatePlayers(exportId, leagueId); // get the list of teams, and then update each team roster
            sb.Append($"<tr><td>Roster Players</td><td>{Helper.TimeElapsed(startTime)}</td></tr>");

            startTime = DateTime.UtcNow;
            UpdateFreeAgents(exportId, leagueId); // update the free agents
            sb.Append($"<tr><td>Free Agents</td><td>{Helper.TimeElapsed(startTime)}</td></tr>");

            sb.Append("</tbody>");
            sb.Append("</table>");

            tblResults.InnerHtml = sb.ToString();
        }

        private void ProcessPlayerStats(string exportId, int leagueId, string stage, int week)
        {
            tblResults.InnerHtml = string.Empty;

            var sb = new StringBuilder();

            sb.Append("<table class='table table-condensed table-striped tablesorter'>");
            sb.Append("<thead>");
            sb.Append("<tr>");
            sb.Append("<th>Update Task</th>");
            sb.Append("<th>Completed in</th>");
            sb.Append("</thead>");

            sb.Append("<tbody>");

            var startTime = DateTime.UtcNow;

            UpdatePassingStats(exportId, leagueId, stage, week);
            UpdateReceivingStats(exportId, leagueId, stage, week);
            UpdateRushingStats(exportId, leagueId, stage, week);
            UpdateDefenseStats(exportId, leagueId, stage, week);
            UpdatePuntingStats(exportId, leagueId, stage, week);
            UpdateKickingStats(exportId, leagueId, stage, week);

            sb.Append($"<tr><td>Player Stats (Week {week})</td><td>{Helper.TimeElapsed(startTime)}</td></tr>");

            sb.Append("</tbody>");
            sb.Append("</table>");

            tblResults.InnerHtml = sb.ToString();
        }

        private void ProcessSchedule(string exportId, int leagueId)
        {
            tblResults.InnerHtml = string.Empty;

            var sb = new StringBuilder();

            sb.Append("<table class='table table-condensed table-striped tablesorter'>");
            sb.Append("<thead>");
            sb.Append("<tr>");
            sb.Append("<th>Update Task</th>");
            sb.Append("<th>Completed in</th>");
            sb.Append("</thead>");

            sb.Append("<tbody>");

            var startTime = DateTime.UtcNow;

            for (var i = 1; i < 5; i++)
            {
                UpdateSchedule(exportId, leagueId, "pre", i);
                UpdateTeamStatsInfo(exportId, leagueId, "pre", i);
            }

            for (var i = 1; i < 23; i++)
            {
                UpdateSchedule(exportId, leagueId, "reg", i);
                UpdateTeamStatsInfo(exportId, leagueId, "reg", i);
            }

            sb.Append($"<tr><td>Schedule</td><td>{Helper.TimeElapsed(startTime)}</td></tr>");

            sb.Append("</tbody>");
            sb.Append("</table>");

            tblResults.InnerHtml = sb.ToString();
        }

        private void ProcessTeamInfo(string exportId, int leagueId)
        {
            var url = $"{localFirebaseURL}/{exportId}/{localFirebaseDataVar}/leagueteams/.json";
            var content = GetContentsFromUrl($"{url}?shallow=true");
            if (content == "null")
            {
                return;
            } // no data found

            var team = ConvertJsonToRotoObject<JSONLeagueTeamInfo.Rootobject>(url);

            if (team == null)
            {
                return;
            }

            var up = new JSONLeagueTeamInfo();
            foreach (var item in team.leagueTeamInfoList)
            {
                up.updateLeagueTeamInfo(item, leagueId);
            }
        }

        private void ProcessTeamStandings(string exportId, int leagueId)
        {
            var url = $"{localFirebaseURL}/{exportId}/{localFirebaseDataVar}/standings/.json";
            var content = GetContentsFromUrl($"{url}?shallow=true");
            if (content == "null")
            {
                return;
            } // no data found

            var teams = ConvertJsonToRotoObject<MaddenTeamRankings.Rootobject>(url);

            var update = new oTeams();
            foreach (var item in teams.teamStandingInfoList)
            {
                update.updateTeam(item, leagueId);
            }
        }

        private void UpdateDefenseStats(string exportId, int leagueId, string seasonType, int week)
        {
            var url = $"{localFirebaseURL}/{exportId}/{localFirebaseDataVar}/week/{seasonType}/{week}/defense/.json";
            var content = GetContentsFromUrl($"{url}?shallow=true");
            if (content == "null")
            {
                return;
            } // no data found

            var stats = ConvertJsonToRotoObject<JSONDefenseStats.Rootobject>(url);
            if (stats == null)
            {
                return;
            }

            var up = new oStatsDefense();
            foreach (var item in stats.playerDefensiveStatInfoList)
            {
                if (item.defInts > 0)
                {
                    // madden uses 255 for -1 (?)
                    if (item.defIntReturnYds > 225)
                    {
                        item.defIntReturnYds = item.defIntReturnYds - 256;
                    }
                }

                up.updateDefenseStats(item, leagueId);
            }
        }

        private void UpdateFreeAgents(string exportId, int leagueId)
        {
            var url = $"{localFirebaseURL}/{exportId}/{localFirebaseDataVar}/freeagents/.json";
            var content = GetContentsFromUrl($"{url}?shallow=true");
            if (content == "null")
            {
                return;
            } // no data found

            var players = ConvertJsonToRotoObject<JSONRosters.Rootobject>(url);

            foreach (var player in players.rosterInfoList)
            {
                DoMyPlayerUpdate(leagueId, player);
            }
        }

        private void UpdateKickingStats(string exportId, int leagueId, string seasonType, int week)
        {
            var url = $"{localFirebaseURL}/{exportId}/{localFirebaseDataVar}/week/{seasonType}/{week}/kicking/.json";
            var content = GetContentsFromUrl($"{url}?shallow=true");
            if (content == "null")
            {
                return;
            } // no data found

            var stats = ConvertJsonToRotoObject<JSONKickingStats.Rootobject>(url);
            if (stats == null)
            {
                return;
            }

            var up = new oStatsKicking();
            foreach (var item in stats.playerKickingStatInfoList)
            {
                up.updateKickingStats(item, leagueId);
            }
        }

        // execute updating SQL from Firebase

        private void UpdatePassingStats(string exportId, int leagueId, string seasonType, int week)
        {
            var url = $"{localFirebaseURL}/{exportId}/{localFirebaseDataVar}/week/{seasonType}/{week}/passing/.json";
            var content = GetContentsFromUrl($"{url}?shallow=true");
            if (content == "null")
            {
                return;
            } // no data found

            var stats = ConvertJsonToRotoObject<JSONPassingStats.Rootobject>(url);
            if (stats == null)
            {
                return;
            }

            var up = new oStatsPassing();
            foreach (var item in stats.playerPassingStatInfoList)
            {
                up.updatePassingStats(item, leagueId);
            }
        }

        private void UpdatePlayers(string exportId, int leagueId)
        {
            var content = GetContentsFromUrl($"{localFirebaseURL}/{exportId}/{localFirebaseDataVar}/team/.json?shallow=true");

            if (content == "null")
            {
                return;
            } // no data found

            // set all players to retired and as free agents
            var defaultPlayers = new oRosters();
            defaultPlayers.SetToFreeAgentAndRetired(leagueId);

            var o = JObject.Parse(content);

            var allTeams = new List<JSONRosters.Rootobject>();

            foreach (var x in o)
            {
                var teamId = x.Key;
                var URL = $"{localFirebaseURL}/{exportId}/{localFirebaseDataVar}/team/{teamId}/.json";
                var players = ConvertJsonToClass<JSONRosters.Rootobject>(URL);
                allTeams.Add(players);
            }

            foreach (var team in allTeams)
            {
                foreach (var player in team.rosterInfoList)
                {
                    DoMyPlayerUpdate(leagueId, player);
                }
                
            }

            
        }


        private void DoMyPlayerUpdate(int leagueId, JSONRosters.Rosterinfolist player)
        {
            var up = new oRosters();

            var recordSaved = up.UpdatePlayerProfile(leagueId, player);

            if (recordSaved)
            {
                up.UpdatePlayerContracts(leagueId, player);
                up.UpdatePlayerGrades(leagueId, player);
                up.UpdatePlayerRatings(leagueId, player);
                up.UpdatePlayerTraits(leagueId, player);

                if (player.signatureSlotList != null)
                {
                    UpdateSignatureAbility(leagueId, player.rosterId, player.signatureSlotList);
                }
            }
            
        }

        private void UpdateSignatureAbility(int leagueId, int rosterId, JSONRosters.Signatureslotlist[] abilities)
        {
            foreach (var ability in abilities)
            {
                var up = new oRosters();
                up.UpdateAbility(leagueId, ability);
                up.UpdatePlayerAbility(leagueId, rosterId, ability);
            }
        }

        private void UpdatePuntingStats(string exportId, int leagueId, string seasonType, int week)
        {
            var url = $"{localFirebaseURL}/{exportId}/{localFirebaseDataVar}/week/{seasonType}/{week}/punting/.json";
            var content = GetContentsFromUrl($"{url}?shallow=true");
            if (content == "null")
            {
                return;
            } // no data found

            var stats = ConvertJsonToRotoObject<JSONPuntingStats.Rootobject>(url);
            if (stats == null)
            {
                return;
            }

            var up = new oStatsPunting();
            foreach (var item in stats.playerPuntingStatInfoList)
            {
                up.updatePuntingStats(item, leagueId);
            }
        }

        private void UpdateReceivingStats(string exportId, int leagueId, string seasonType, int week)
        {
            var url = $"{localFirebaseURL}/{exportId}/{localFirebaseDataVar}/week/{seasonType}/{week}/receiving/.json";
            var content = GetContentsFromUrl($"{url}?shallow=true");
            if (content == "null")
            {
                return;
            } // no data found

            var stats = ConvertJsonToRotoObject<JSONReceivingStats.Rootobject>(url);
            if (stats == null)
            {
                return;
            }

            var up = new oStatsReceiving();
            foreach (var item in stats.playerReceivingStatInfoList)
            {
                up.updateReceivingStats(item, leagueId);
            }
        }

        private void UpdateRushingStats(string exportId, int leagueId, string seasonType, int week)
        {
            var url = $"{localFirebaseURL}/{exportId}/{localFirebaseDataVar}/week/{seasonType}/{week}/rushing/.json";
            var content = GetContentsFromUrl($"{url}?shallow=true");
            if (content == "null")
            {
                return;
            } // no data found

            var stats = ConvertJsonToRotoObject<JSONRushingStats.Rootobject>(url);
            if (stats == null)
            {
                return;
            }

            var up = new oStatsRushing();
            foreach (var item in stats.playerRushingStatInfoList)
            {
                up.updateRushingStats(item, leagueId);
            }
        }

        private void UpdateSchedule(string exportId, int leagueId, string seasonType, int week)
        {
            var url = $"{localFirebaseURL}/{exportId}/{localFirebaseDataVar}/week/{seasonType}/{week}/schedules/.json";
            var content = GetContentsFromUrl($"{url}?shallow=true");
            if (content == "null")
            {
                return;
            } // no data found

            var schedule = ConvertJsonToRotoObject<JSONSchedule.Rootobject>(url);
            if (schedule == null)
            {
                return;
            }

            var up = new ScheduleRepository();
            foreach (var item in schedule.gameScheduleInfoList)
            {
                up.UpdateSchedule(item, leagueId);
            }
        }

        private void UpdateTeamRosters(string exportId, int leagueId, string teamId)
        {
            var URL = $"{localFirebaseURL}/{exportId}/{localFirebaseDataVar}/team/{teamId}/.json";
            var content = GetContentsFromUrl($"{URL}?shallow=true");
            if (content == "null")
            {
                return;
            } // no data found

            var players = ConvertJsonToClass<JSONRosters.Rootobject>(URL);

            foreach (var player in players.rosterInfoList)
            {
                DoMyPlayerUpdate(leagueId, player);   
            }
        }

        private void UpdateTeamStatsInfo(string exportId, int leagueId, string seasonType, int week)
        {
            var url = $"{localFirebaseURL}/{exportId}/{localFirebaseDataVar}/week/{seasonType}/{week}/teamstats/.json";
            var content = GetContentsFromUrl($"{url}?shallow=true");
            if (content == "null")
            {
                return;
            } // no data found

            var stats = ConvertJsonToRotoObject<JSONTeamStats.Rootobject>(url);
            if (stats == null)
            {
                return;
            }

            var up = new oStatsTeamStatsInfo();
            foreach (var item in stats.teamStatInfoList)
            {
                up.UpdateTeamStatsInfo(item, leagueId);
            }
        }


    }
}


