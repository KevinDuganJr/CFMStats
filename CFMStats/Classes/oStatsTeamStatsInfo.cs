using System.Collections.Generic;
using System.Configuration;
using CFMStats.Classes.JSON;

namespace CFMStats.Classes
{
    public class oStatsTeamStatsInfo : Dictionary<int, JSONTeamStats.Teamstatinfolist>
    {
        public bool UpdateTeamStatsInfo(JSONTeamStats.Teamstatinfolist i, int iLeagueId)
        {
            var sp = new StoredProc
            {
                Name = "StatsTeam_update",
                DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString,
                ParameterSet = new System.Data.SqlClient.SqlCommand()
            };

            sp.ParameterSet.Parameters.AddWithValue("@leagueId", iLeagueId);
            sp.ParameterSet.Parameters.AddWithValue("@weekIndex", i.weekIndex);
            sp.ParameterSet.Parameters.AddWithValue("@stageIndex", i.stageIndex);
            sp.ParameterSet.Parameters.AddWithValue("@statId", i.statId);
            sp.ParameterSet.Parameters.AddWithValue("@seasonIndex", i.seasonIndex);
            sp.ParameterSet.Parameters.AddWithValue("@seed", i.seed);
            sp.ParameterSet.Parameters.AddWithValue("@teamId", i.teamId);
            sp.ParameterSet.Parameters.AddWithValue("@scheduleId", i.scheduleId);
            sp.ParameterSet.Parameters.AddWithValue("@defForcedFum", i.defForcedFum);
            sp.ParameterSet.Parameters.AddWithValue("@defFumRec", i.defFumRec);
            sp.ParameterSet.Parameters.AddWithValue("@defIntsRec", i.defIntsRec);
            sp.ParameterSet.Parameters.AddWithValue("@defPassYds", i.defPassYds);
            sp.ParameterSet.Parameters.AddWithValue("@defPtsPerGame", i.defPtsPerGame);
            sp.ParameterSet.Parameters.AddWithValue("@defRedZoneFGs", i.defRedZoneFGs);
            sp.ParameterSet.Parameters.AddWithValue("@defRedZonePct", i.defRedZonePct);
            sp.ParameterSet.Parameters.AddWithValue("@defRedZoneTDs", i.defRedZoneTDs);
            sp.ParameterSet.Parameters.AddWithValue("@defRedZones", i.defRedZones);
            sp.ParameterSet.Parameters.AddWithValue("@defRushYds", i.defRushYds);
            sp.ParameterSet.Parameters.AddWithValue("@defSacks", i.defSacks);
            sp.ParameterSet.Parameters.AddWithValue("@defTotalYds", i.defTotalYds);
            sp.ParameterSet.Parameters.AddWithValue("@off1stDowns", i.off1stDowns);
            sp.ParameterSet.Parameters.AddWithValue("@off2PtAtt", i.off2PtAtt);
            sp.ParameterSet.Parameters.AddWithValue("@off2PtConv", i.off2PtConv);
            sp.ParameterSet.Parameters.AddWithValue("@off2PtConvPct", i.off2PtConvPct);
            sp.ParameterSet.Parameters.AddWithValue("@off3rdDownAtt", i.off3rdDownAtt);
            sp.ParameterSet.Parameters.AddWithValue("@off3rdDownConv", i.off3rdDownConv);
            sp.ParameterSet.Parameters.AddWithValue("@off3rdDownConvPct", i.off3rdDownConvPct);
            sp.ParameterSet.Parameters.AddWithValue("@off4thDownAtt", i.off4thDownAtt);
            sp.ParameterSet.Parameters.AddWithValue("@off4thDownConv", i.off4thDownConv);
            sp.ParameterSet.Parameters.AddWithValue("@off4thDownConvPct", i.off4thDownConvPct);
            sp.ParameterSet.Parameters.AddWithValue("@offFumLost", i.offFumLost);
            sp.ParameterSet.Parameters.AddWithValue("@offIntsLost", i.offIntsLost);
            sp.ParameterSet.Parameters.AddWithValue("@offPassTDs", i.offPassTDs);
            sp.ParameterSet.Parameters.AddWithValue("@offPassYds", i.offPassYds);
            sp.ParameterSet.Parameters.AddWithValue("@offPtsPerGame", i.offPtsPerGame);
            sp.ParameterSet.Parameters.AddWithValue("@offRedZoneFGs", i.offRedZoneFGs);
            sp.ParameterSet.Parameters.AddWithValue("@offRedZonePct", i.offRedZonePct);
            sp.ParameterSet.Parameters.AddWithValue("@offRedZoneTDs", i.offRedZoneTDs);
            sp.ParameterSet.Parameters.AddWithValue("@offRedZones", i.offRedZones);
            sp.ParameterSet.Parameters.AddWithValue("@offRushTDs", i.offRushTDs);
            sp.ParameterSet.Parameters.AddWithValue("@offRushYds", i.offRushYds);
            sp.ParameterSet.Parameters.AddWithValue("@offSacks", i.offSacks);
            sp.ParameterSet.Parameters.AddWithValue("@offTotalYds", i.offTotalYds);
            sp.ParameterSet.Parameters.AddWithValue("@offTotalYdsGained", i.offTotalYdsGained);
            sp.ParameterSet.Parameters.AddWithValue("@penalties", i.penalties);
            sp.ParameterSet.Parameters.AddWithValue("@penaltyYds", i.penaltyYds);
            sp.ParameterSet.Parameters.AddWithValue("@tODiff", i.tODiff);
            sp.ParameterSet.Parameters.AddWithValue("@tOGiveaways", i.tOGiveaways);
            sp.ParameterSet.Parameters.AddWithValue("@tOTakeaways", i.tOTakeaways);
            sp.ParameterSet.Parameters.AddWithValue("@totalLosses", i.totalLosses);
            sp.ParameterSet.Parameters.AddWithValue("@totalTies", i.totalTies);
            sp.ParameterSet.Parameters.AddWithValue("@totalWins", i.totalWins);

            var status = StoredProc.NonQuery(sp);

            return status;
        }

    }
}