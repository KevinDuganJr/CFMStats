using System.Collections.Generic;
using System.Configuration;
using System.Web.UI.WebControls;
using CFMStats.Classes.JSON;

namespace CFMStats.Classes
{
    public class oStatsPunting : Dictionary<int, JSONPuntingStats.Playerpuntingstatinfolist>
    {
        public bool updatePuntingStats(JSONPuntingStats.Playerpuntingstatinfolist i, int iLeagueId)
        {
            StoredProc SP = new StoredProc();
            SP.Name = "StatsPunting_update";
            SP.DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SP.ParameterSet = new System.Data.SqlClient.SqlCommand();

            SP.ParameterSet.Parameters.AddWithValue("@leagueId", iLeagueId);
            SP.ParameterSet.Parameters.AddWithValue("@fullName", i.fullName);
            SP.ParameterSet.Parameters.AddWithValue("@puntAtt", i.puntAtt);
            SP.ParameterSet.Parameters.AddWithValue("@puntLongest", i.puntLongest);
            SP.ParameterSet.Parameters.AddWithValue("@puntNetYds", i.puntNetYds);
            SP.ParameterSet.Parameters.AddWithValue("@puntTBs", i.puntTBs);
            SP.ParameterSet.Parameters.AddWithValue("@puntYds", i.puntYds);
            SP.ParameterSet.Parameters.AddWithValue("@puntsBlocked", i.puntsBlocked);
            SP.ParameterSet.Parameters.AddWithValue("@puntsIn20", i.puntsIn20);
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