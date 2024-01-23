using CFMStats.Classes.JSON;
using CFMStats.Classes;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Data;

namespace CFMStats.Services
{
    public class ImportDataService
    {
        private readonly string _localFireBaseData = "data";

        private readonly string _localFireBaseUrl = "https://dugan-760bc.firebaseio.com";

        private readonly JsonToObjectService _jsonToObjectService = new JsonToObjectService();

        public List<string> CheckForExportIds()
        {
            var exportList = new List<string>();
            var url = $"{_localFireBaseUrl}";

            var content = UrlDataReaderService.GetDataFromUrl($"{url}/.json?shallow=true");

            if (content == "null")
            {
                // no data found
                return exportList;
            }

            var seasonTypes = JObject.Parse(content);

            foreach (var x in seasonTypes)
            {
                exportList.Add(x.Key);
            }

            return exportList;
        }

        public DataTable CheckFireBaseForWeeklyStats(string exportId)
        {
            var weeksDataTable = new DataTable();

            var url = $"{_localFireBaseUrl}/{exportId}/{_localFireBaseData}/week/.json";
            var content = UrlDataReaderService.GetDataFromUrl($"{url}?shallow=true");

            if (content == "null")
            {
                // no data found
                return weeksDataTable;
            }

            var table = new DataTable();
            table.Columns.Add("SeasonType", typeof(string));
            table.Columns.Add("Week", typeof(int));
            table.Columns.Add("HasStats", typeof(bool));

            var seasonTypes = JObject.Parse(content);

            foreach (var x in seasonTypes)
            {
                var stageIndexType = x.Key;

                var content2 = UrlDataReaderService.GetDataFromUrl($"{_localFireBaseUrl}/{exportId}/{_localFireBaseData}/week/{stageIndexType}/.json?shallow=true");

                var weeks = JObject.Parse(content2);

                foreach (var y in weeks)
                {
                    var week = Helper.IntegerNull(y.Key);

                    var statsUrl = $"{_localFireBaseUrl}/{exportId}/{_localFireBaseData}/week/{stageIndexType}/{week}/defense/.json";
                    var statsContent = UrlDataReaderService.GetDataFromUrl($"{statsUrl}?shallow=true");
                    table.Rows.Add(x.Key, y.Key, statsContent != "null");
                }
            }

            var dv = table.DefaultView;
            dv.Sort = "SeasonType DESC, Week ASC";
            table = dv.ToTable();

            return table;
        }

        public void ProcessPlayers(string exportId, int leagueId)
        {
            // get the list of teams, and then update each team roster
            UpdatePlayers(exportId, leagueId);

            // update free agents
            UpdateFreeAgents(exportId, leagueId);
        }

        public void ProcessPlayerStats(string exportId, int leagueId, string stage, int week)
        {
            UpdatePassingStats(exportId, leagueId, stage, week);
            UpdateReceivingStats(exportId, leagueId, stage, week);
            UpdateRushingStats(exportId, leagueId, stage, week);
            UpdateDefenseStats(exportId, leagueId, stage, week);
            UpdatePuntingStats(exportId, leagueId, stage, week);
            UpdateKickingStats(exportId, leagueId, stage, week);
        }

        public void ProcessTeamInfo(string exportId, int leagueId)
        {
            var url = $"{_localFireBaseUrl}/{exportId}/{_localFireBaseData}/leagueteams/.json";
            var content = UrlDataReaderService.GetDataFromUrl($"{url}?shallow=true");
            if (content == "null") { return; } // no data found

            var team = _jsonToObjectService.ReturnJsonObject<JSONLeagueTeamInfo.Rootobject>(url);

            if (team == null) { return; }

            var up = new JSONLeagueTeamInfo();
            foreach (var item in team.leagueTeamInfoList)
            {
                up.updateLeagueTeamInfo(item, leagueId);
            }
        }

        public void ProcessTeamStandings(string exportId, int leagueId)
        {
            var url = $"{_localFireBaseUrl}/{exportId}/{_localFireBaseData}/standings/.json";
            var content = UrlDataReaderService.GetDataFromUrl($"{url}?shallow=true");
            if (content == "null")
            {
                return;
            } // no data found

            var teams = _jsonToObjectService.ReturnJsonObject<MaddenTeamRankings.Rootobject>(url);

            var update = new oTeams();
            foreach (var item in teams.teamStandingInfoList)
            {
                update.updateTeam(item, leagueId);
            }
        }

        public void UpdateDefenseStats(string exportId, int leagueId, string seasonType, int week)
        {
            var url = $"{_localFireBaseUrl}/{exportId}/{_localFireBaseData}/week/{seasonType}/{week}/defense/.json";
            var content = UrlDataReaderService.GetDataFromUrl($"{url}?shallow=true");
            if (content == "null")
            {
                return;
            } // no data found

            var stats = _jsonToObjectService.ReturnJsonObject<JSONDefenseStats.Rootobject>(url);
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

        public void UpdateFreeAgents(string exportId, int leagueId)
        {
            var url = $"{_localFireBaseUrl}/{exportId}/{_localFireBaseData}/freeagents/.json";
            var content = UrlDataReaderService.GetDataFromUrl($"{url}?shallow=true");
            if (content == "null")
            {
                return;
            } // no data found

            var players = _jsonToObjectService.ReturnJsonObject<JSONRosters.Rootobject>(url);

            foreach (var player in players.rosterInfoList)
            {
                DoMyPlayerUpdate(leagueId, player);
            }
        }

        public void UpdateKickingStats(string exportId, int leagueId, string seasonType, int week)
        {
            var url = $"{_localFireBaseUrl}/{exportId}/{_localFireBaseData}/week/{seasonType}/{week}/kicking/.json";
            var content = UrlDataReaderService.GetDataFromUrl($"{url}?shallow=true");
            if (content == "null")
            {
                return;
            } // no data found

            var stats = _jsonToObjectService.ReturnJsonObject<JSONKickingStats.Rootobject>(url);
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

        public void UpdatePassingStats(string exportId, int leagueId, string seasonType, int week)
        {
            var url = $"{_localFireBaseUrl}/{exportId}/{_localFireBaseData}/week/{seasonType}/{week}/passing/.json";
            var content = UrlDataReaderService.GetDataFromUrl($"{url}?shallow=true");
            if (content == "null")
            {
                return;
            } // no data found

            var stats = _jsonToObjectService.ReturnJsonObject<JSONPassingStats.Rootobject>(url);
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

        private void DoMyPlayerUpdate(int leagueId, JSONRosters.Rosterinfolist player)
        {
            var up = new oRosters();

            var recordSaved = up.UpdatePlayer(leagueId, player);

            if (recordSaved)
            {
                if (player.signatureSlotList != null)
                {
                    UpdateSignatureAbility(leagueId, player.rosterId, player.signatureSlotList);
                }
            }

        }
        public void UpdatePlayers(string exportId, int leagueId)
        {
            var content = UrlDataReaderService.GetDataFromUrl($"{_localFireBaseUrl}/{exportId}/{_localFireBaseData}/team/.json?shallow=true");

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
                var URL = $"{_localFireBaseUrl}/{exportId}/{_localFireBaseData}/team/{teamId}/.json";
                var players = _jsonToObjectService.ReturnJsonObject<JSONRosters.Rootobject>(URL);
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

        public void UpdatePuntingStats(string exportId, int leagueId, string seasonType, int week)
        {
            var url = $"{_localFireBaseUrl}/{exportId}/{_localFireBaseData}/week/{seasonType}/{week}/punting/.json";
            var content = UrlDataReaderService.GetDataFromUrl($"{url}?shallow=true");
            if (content == "null")
            {
                return;
            } // no data found

            var stats = _jsonToObjectService.ReturnJsonObject<JSONPuntingStats.Rootobject>(url);
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

        public void UpdateReceivingStats(string exportId, int leagueId, string seasonType, int week)
        {
            var url = $"{_localFireBaseUrl}/{exportId}/{_localFireBaseData}/week/{seasonType}/{week}/receiving/.json";
            var content = UrlDataReaderService.GetDataFromUrl($"{url}?shallow=true");
            if (content == "null")
            {
                return;
            } // no data found

            var stats = _jsonToObjectService.ReturnJsonObject<JSONReceivingStats.Rootobject>(url);
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

        public void UpdateRushingStats(string exportId, int leagueId, string seasonType, int week)
        {
            var url = $"{_localFireBaseUrl}/{exportId}/{_localFireBaseData}/week/{seasonType}/{week}/rushing/.json";
            var content = UrlDataReaderService.GetDataFromUrl($"{url}?shallow=true");
            if (content == "null")
            {
                return;
            } // no data found

            var stats = _jsonToObjectService.ReturnJsonObject<JSONRushingStats.Rootobject>(url);
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

        public void UpdateSchedule(string exportId, int leagueId, string seasonType, int week)
        {
            var url = $"{_localFireBaseUrl}/{exportId}/{_localFireBaseData}/week/{seasonType}/{week}/schedules/.json";
            var content = UrlDataReaderService.GetDataFromUrl($"{url}?shallow=true");
            if (content == "null")
            {
                return;
            } // no data found

            var schedule = _jsonToObjectService.ReturnJsonObject<JSONSchedule.Rootobject>(url);
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

        public void UpdateTeamRosters(string exportId, int leagueId, string teamId)
        {
            var URL = $"{_localFireBaseUrl}/{exportId}/{_localFireBaseData}/team/{teamId}/.json";
            var content = UrlDataReaderService.GetDataFromUrl($"{URL}?shallow=true");
            if (content == "null")
            {
                return;
            } // no data found

            var players = _jsonToObjectService.ReturnJsonObject<JSONRosters.Rootobject>(URL);

            foreach (var player in players.rosterInfoList)
            {
                var up = new oRosters();

                var recordSaved = up.UpdatePlayer(leagueId, player);

                if (recordSaved)
                {
                    if (player.signatureSlotList != null)
                    {
                        UpdateSignatureAbility(leagueId, player.rosterId, player.signatureSlotList);
                    }
                }
            }
        }

        public void UpdateTeamStatsInfo(string exportId, int leagueId, string seasonType, int week)
        {
            var url = $"{_localFireBaseUrl}/{exportId}/{_localFireBaseData}/week/{seasonType}/{week}/teamstats/.json";
            var content = UrlDataReaderService.GetDataFromUrl($"{url}?shallow=true");
            if (content == "null")
            {
                return;
            } // no data found

            var stats = _jsonToObjectService.ReturnJsonObject<JSONTeamStats.Rootobject>(url);
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

        private void UpdateSignatureAbility(int leagueId, int rosterId, JSONRosters.Signatureslotlist[] abilities)
        {
            foreach (var ability in abilities)
            {
                var up = new oRosters();
                up.UpdateAbility(leagueId, ability);
                up.UpdatePlayerAbility(leagueId, rosterId, ability);
            }
        }
    }


}