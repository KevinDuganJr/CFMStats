using System.Configuration;
using System.Data.SqlClient;
using CFMStats.Classes.JSON;

namespace CFMStats.Classes
{
    public class ScheduleRepository
    {
        public bool UpdateSchedule(JSONSchedule.Gamescheduleinfolist i, int leagueId)
        {
            var sp = new StoredProc
            {
                Name = "Schedule_update", DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, ParameterSet = new SqlCommand()
            };

            sp.ParameterSet.Parameters.AddWithValue("@leagueId", leagueId);
            sp.ParameterSet.Parameters.AddWithValue("@scheduleId", i.scheduleId);
            sp.ParameterSet.Parameters.AddWithValue("@seasonIndex", i.seasonIndex);
            sp.ParameterSet.Parameters.AddWithValue("@weekIndex", i.weekIndex);
            sp.ParameterSet.Parameters.AddWithValue("@awayScore", i.awayScore);
            sp.ParameterSet.Parameters.AddWithValue("@awayTeamID", i.awayTeamId);
            sp.ParameterSet.Parameters.AddWithValue("@homeScore", i.homeScore);
            sp.ParameterSet.Parameters.AddWithValue("@homeTeamId", i.homeTeamId);
            sp.ParameterSet.Parameters.AddWithValue("@isGameOfTheWeek", i.isGameOfTheWeek);
            sp.ParameterSet.Parameters.AddWithValue("@status", i.status);
            sp.ParameterSet.Parameters.AddWithValue("@stageIndex", i.stageIndex);

            var status = StoredProc.NonQuery(sp);

            return status;
        }
    }
}