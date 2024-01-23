using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using CFMStats.Classes;
using static CFMStats.Classes.JSON.JSONLeagueTeamInfo;

namespace CFMStats.Services
{
    public class TeamService : Dictionary<int, Leagueteaminfolist>
    {
        public TeamService GetLeagueTeams(int leagueId)
        {
            var collection = new TeamService();

            var sp = new StoredProc
            {
                Name = "TeamInfo_select", DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, ParameterSet = new SqlCommand()
            };

            sp.ParameterSet.Parameters.AddWithValue("@leagueId", leagueId);

            var ds = StoredProc.ShowMeTheData(sp);

            if (ds.Tables[0].Rows.Count == 0)
            {
                return collection;
            }

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                var team = new Leagueteaminfolist
                {
                    teamId = item.Field<int>("teamId"),
                    logoId = Helper.IntegerNull(item["logoId"]),
                    abbrName = item.Field<string>("abbrName"),
                    cityName = item.Field<string>("cityName"),
                    displayName = item.Field<string>("displayName"),
                    divName = item.Field<string>("divName"),
                    offScheme = item.Field<int>("offScheme"),
                    defScheme = item.Field<int>("defScheme"),
                    ovrRating = item.Field<int>("ovrRating"),
                    injuryCount = item.Field<int>("injuryCount"),
                    primaryColor = item.Field<int>("primaryColor"),
                    secondaryColor = item.Field<int>("secondaryColor"),
                    userName = Helper.StringNull(item["userName"])
                };
                collection.Add(team.teamId, team);
            }

            return collection;
        }
    }
}