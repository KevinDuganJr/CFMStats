using System.Collections.Generic;
using System.Configuration;
using System.Web.UI.WebControls;
using CFMStats.Classes.JSON;

namespace CFMStats.Classes
{
    public class oStatsReceiving : Dictionary<int, JSONReceivingStats.Playerreceivingstatinfolist>
    {

        public bool updateReceivingStats(JSONReceivingStats.Playerreceivingstatinfolist i, int iLeagueId)
        {
            StoredProc SP = new StoredProc();
            SP.Name = "StatsReceiving_update";
            SP.DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SP.ParameterSet = new System.Data.SqlClient.SqlCommand();

            SP.ParameterSet.Parameters.AddWithValue("@leagueId", iLeagueId);
            SP.ParameterSet.Parameters.AddWithValue("@fullName", i.fullName);
            SP.ParameterSet.Parameters.AddWithValue("@recCatchPct", i.recCatchPct);
            SP.ParameterSet.Parameters.AddWithValue("@recCatches", i.recCatches);
            SP.ParameterSet.Parameters.AddWithValue("@recDrops", i.recDrops);
            SP.ParameterSet.Parameters.AddWithValue("@recLongest", i.recLongest);
            SP.ParameterSet.Parameters.AddWithValue("@recPts", i.recPts);
            SP.ParameterSet.Parameters.AddWithValue("@recTDs", i.recTDs);
            SP.ParameterSet.Parameters.AddWithValue("@recToPct", i.recToPct);
            SP.ParameterSet.Parameters.AddWithValue("@recYacPerCatch", i.recYacPerCatch);
            SP.ParameterSet.Parameters.AddWithValue("@recYds", i.recYds);
            SP.ParameterSet.Parameters.AddWithValue("@recYdsAfterCatch", i.recYdsAfterCatch);
            SP.ParameterSet.Parameters.AddWithValue("@recYdsPerCatch", i.recYdsPerCatch);
            SP.ParameterSet.Parameters.AddWithValue("@recYdsPerGame", i.recYdsPerGame);
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