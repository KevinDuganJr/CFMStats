using System.Collections.Generic;
using System.Configuration;
using System.Web.UI.WebControls;
using CFMStats.Classes.JSON;

namespace CFMStats.Classes
{
    public class oStatsKicking : Dictionary<int, JSONKickingStats.Playerkickingstatinfolist>
    {
        public bool updateKickingStats(JSONKickingStats.Playerkickingstatinfolist i, int iLeagueId)
        {
            StoredProc SP = new StoredProc();
            SP.Name = "StatsKicking_update";
            SP.DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SP.ParameterSet = new System.Data.SqlClient.SqlCommand();

            SP.ParameterSet.Parameters.AddWithValue("@leagueId", iLeagueId);
            SP.ParameterSet.Parameters.AddWithValue("@fG50PlusAtt", i.fG50PlusAtt);
            SP.ParameterSet.Parameters.AddWithValue("@fG50PlusMade", i.fG50PlusMade);
            SP.ParameterSet.Parameters.AddWithValue("@fGAtt", i.fGAtt);
            SP.ParameterSet.Parameters.AddWithValue("@fGLongest", i.fGLongest);
            SP.ParameterSet.Parameters.AddWithValue("@fGMade", i.fGMade);
            SP.ParameterSet.Parameters.AddWithValue("@fullName", i.fullName);
            SP.ParameterSet.Parameters.AddWithValue("@kickPts", i.kickPts);
            SP.ParameterSet.Parameters.AddWithValue("@kickoffAtt", i.kickoffAtt);
            SP.ParameterSet.Parameters.AddWithValue("@kickoffTBs", i.kickoffTBs);
            SP.ParameterSet.Parameters.AddWithValue("@rosterId", i.rosterId);
            SP.ParameterSet.Parameters.AddWithValue("@scheduleId", i.scheduleId);
            SP.ParameterSet.Parameters.AddWithValue("@seasonIndex", i.seasonIndex);
            SP.ParameterSet.Parameters.AddWithValue("@stageIndex", i.stageIndex);
            SP.ParameterSet.Parameters.AddWithValue("@statId", i.statId);
            SP.ParameterSet.Parameters.AddWithValue("@teamId", i.teamId);
            SP.ParameterSet.Parameters.AddWithValue("@weekIndex", i.weekIndex);
            SP.ParameterSet.Parameters.AddWithValue("@xPAtt", i.xPAtt);
            SP.ParameterSet.Parameters.AddWithValue("@xPMade", i.xPMade);

            var status = StoredProc.NonQuery(SP);

            return status;
        }
    }
}