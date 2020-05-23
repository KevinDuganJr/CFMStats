using System.Collections.Generic;
using System.Configuration;
using System.Web.UI.WebControls;
using CFMStats.Classes.JSON;

namespace CFMStats.Classes
{
    public class oStatsDefense : Dictionary<int, JSONDefenseStats.Playerdefensivestatinfolist>
    {

        public bool updateDefenseStats(JSONDefenseStats.Playerdefensivestatinfolist i, int iLeagueId)
        {
            StoredProc SP = new StoredProc();
            SP.Name = "StatsDefense_update";
            SP.DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SP.ParameterSet = new System.Data.SqlClient.SqlCommand();

            SP.ParameterSet.Parameters.AddWithValue("@leagueId", iLeagueId);
            SP.ParameterSet.Parameters.AddWithValue("@defCatchAllowed", i.defCatchAllowed);
            SP.ParameterSet.Parameters.AddWithValue("@defDeflections", i.defDeflections);
            SP.ParameterSet.Parameters.AddWithValue("@defForcedFum", i.defForcedFum);
            SP.ParameterSet.Parameters.AddWithValue("@defFumRec", i.defFumRec);
            SP.ParameterSet.Parameters.AddWithValue("@defIntReturnYds", i.defIntReturnYds);
            SP.ParameterSet.Parameters.AddWithValue("@defInts", i.defInts);
            SP.ParameterSet.Parameters.AddWithValue("@defPts", i.defPts);
            SP.ParameterSet.Parameters.AddWithValue("@defSacks", i.defSacks);
            SP.ParameterSet.Parameters.AddWithValue("@defSafeties", i.defSafeties);
            SP.ParameterSet.Parameters.AddWithValue("@defTDs", i.defTDs);
            SP.ParameterSet.Parameters.AddWithValue("@defTotalTackles", i.defTotalTackles);
            SP.ParameterSet.Parameters.AddWithValue("@fullName", i.fullName);
            SP.ParameterSet.Parameters.AddWithValue("@rosterId", i.rosterId);
            SP.ParameterSet.Parameters.AddWithValue("@scheduleId", i.scheduleId);
            SP.ParameterSet.Parameters.AddWithValue("@seasonIndex", i.seasonIndex);
            SP.ParameterSet.Parameters.AddWithValue("@stageIndex", i.stageIndex);
            SP.ParameterSet.Parameters.AddWithValue("@statId", i.statId);
            SP.ParameterSet.Parameters.AddWithValue("@teamId", i.teamId);
            SP.ParameterSet.Parameters.AddWithValue("@weekIndex", i.weekIndex);

            var status = StoredProc.NonQuery(SP);

            return status;
        }
    }
}