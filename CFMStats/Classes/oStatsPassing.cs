using System.Collections.Generic;
using System.Configuration;
using System.Web.UI.WebControls;
using CFMStats.Classes.JSON;

namespace CFMStats.Classes
{
    public class oStatsPassing : Dictionary<int, JSONPassingStats.Playerpassingstatinfolist>
    {
        public bool updatePassingStats(JSONPassingStats.Playerpassingstatinfolist i, int iLeagueId)
        {
            StoredProc SP = new StoredProc();
            SP.Name = "StatsPassing_update";
            SP.DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SP.ParameterSet = new System.Data.SqlClient.SqlCommand();

            SP.ParameterSet.Parameters.AddWithValue("@leagueId", iLeagueId);
            SP.ParameterSet.Parameters.AddWithValue("@fullName", i.fullName);
            SP.ParameterSet.Parameters.AddWithValue("@passAtt", i.passAtt);
            SP.ParameterSet.Parameters.AddWithValue("@passComp", i.passComp);
            SP.ParameterSet.Parameters.AddWithValue("@passInts", i.passInts);
            SP.ParameterSet.Parameters.AddWithValue("@passLongest", i.passLongest);
            SP.ParameterSet.Parameters.AddWithValue("@passPts", i.passPts);
            SP.ParameterSet.Parameters.AddWithValue("@passSacks", i.passSacks);
            SP.ParameterSet.Parameters.AddWithValue("@passTDs", i.passTDs);
            SP.ParameterSet.Parameters.AddWithValue("@passYds", i.passYds);
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