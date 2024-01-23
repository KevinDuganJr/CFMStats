using System.Data.SqlClient;

namespace CFMStats.Classes.JSON
{
    public class JSONLeagueTeamInfo
    {
        public bool updateLeagueTeamInfo(Leagueteaminfolist i, int iLeagueId)
        {
            var SP = new StoredProc();
            SP.Name = "TeamInfo_update";

            SP.ParameterSet = new SqlCommand();

            SP.ParameterSet.Parameters.AddWithValue("@leagueId", iLeagueId);
            SP.ParameterSet.Parameters.AddWithValue("@teamId", i.teamId);
            SP.ParameterSet.Parameters.AddWithValue("@abbrName", i.abbrName);
            SP.ParameterSet.Parameters.AddWithValue("@logoId", i.logoId);
            SP.ParameterSet.Parameters.AddWithValue("@cityName", i.cityName);
            SP.ParameterSet.Parameters.AddWithValue("@displayName", i.displayName);
            SP.ParameterSet.Parameters.AddWithValue("@divName", i.divName);
            SP.ParameterSet.Parameters.AddWithValue("@offScheme", i.offScheme);
            SP.ParameterSet.Parameters.AddWithValue("@defScheme", i.defScheme);
            SP.ParameterSet.Parameters.AddWithValue("@ovrRating", i.ovrRating);
            SP.ParameterSet.Parameters.AddWithValue("@injuryCount", i.injuryCount);
            SP.ParameterSet.Parameters.AddWithValue("@primaryColor", i.primaryColor);
            SP.ParameterSet.Parameters.AddWithValue("@secondaryColor", i.secondaryColor);
            SP.ParameterSet.Parameters.AddWithValue("@userName", i.userName);

            var status = StoredProc.NonQuery(SP);

            return status;
        }

        public class Rootobject
        {
            public Leagueteaminfolist[] leagueTeamInfoList { get; set; }
        }

        public class Leagueteaminfolist
        {
            public string abbrName { get; set; }

            public string cityName { get; set; }

            public int defScheme { get; set; }

            public string displayName { get; set; }

            public string divName { get; set; }

            public int injuryCount { get; set; }

            public int logoId { get; set; }

            public string nickName { get; set; }

            public int offScheme { get; set; }

            public int ovrRating { get; set; }

            public int primaryColor { get; set; }

            public int secondaryColor { get; set; }

            public int teamId { get; set; }

            public string userName { get; set; }
        }
    }
}