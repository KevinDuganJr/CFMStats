using System.Collections.Generic;
using System.Configuration;
using System.Web.UI.WebControls;
using CFMStats.Classes.JSON;

namespace CFMStats.Classes
{
    public class oStatsRushing : Dictionary<int, JSONRushingStats.Playerrushingstatinfolist>
    {
        public bool updateRushingStats(JSONRushingStats.Playerrushingstatinfolist i, int iLeagueId)
        {
            StoredProc SP = new StoredProc();
            SP.Name = "StatsRushing_update";
            SP.DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SP.ParameterSet = new System.Data.SqlClient.SqlCommand();

            SP.ParameterSet.Parameters.AddWithValue("@leagueId", iLeagueId);
            SP.ParameterSet.Parameters.AddWithValue("@fullName", i.fullName);
            SP.ParameterSet.Parameters.AddWithValue("@rosterId", i.rosterId);
            SP.ParameterSet.Parameters.AddWithValue("@rush20PlusYds", i.rush20PlusYds);
            SP.ParameterSet.Parameters.AddWithValue("@rushAtt", i.rushAtt);
            SP.ParameterSet.Parameters.AddWithValue("@rushBrokenTackles", i.rushBrokenTackles);
            SP.ParameterSet.Parameters.AddWithValue("@rushFum", i.rushFum);
            SP.ParameterSet.Parameters.AddWithValue("@rushLongest", i.rushLongest);
            SP.ParameterSet.Parameters.AddWithValue("@rushPts", i.rushPts);
            SP.ParameterSet.Parameters.AddWithValue("@rushTDs", i.rushTDs);
            SP.ParameterSet.Parameters.AddWithValue("@rushYds", i.rushYds);
            SP.ParameterSet.Parameters.AddWithValue("@rushYdsAfterContact", i.rushYdsAfterContact);
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