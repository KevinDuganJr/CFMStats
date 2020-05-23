using System.Collections.Generic;
using System.Configuration;
using System.Web.UI.WebControls;

namespace CFMStats.Classes
{
    public class oTeams : Dictionary<int, MaddenTeamRankings.Teamstandinginfolist>
    {
        public bool updateTeam(MaddenTeamRankings.Teamstandinginfolist t, int iLeagueId)
        {
            StoredProc SP = new StoredProc();
            SP.Name = "TeamStandings_update";
            SP.DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SP.ParameterSet = new System.Data.SqlClient.SqlCommand();

            SP.ParameterSet.Parameters.AddWithValue("@leagueId", iLeagueId);
            SP.ParameterSet.Parameters.AddWithValue("@teamId", t.teamId);
            SP.ParameterSet.Parameters.AddWithValue("@teamName", t.teamName);
            SP.ParameterSet.Parameters.AddWithValue("@teamOvr", t.teamOvr);
            SP.ParameterSet.Parameters.AddWithValue("@calendarYear", t.calendarYear);
            SP.ParameterSet.Parameters.AddWithValue("@weekIndex", t.weekIndex);
            SP.ParameterSet.Parameters.AddWithValue("@seasonIndex", t.seasonIndex);
            SP.ParameterSet.Parameters.AddWithValue("@capRoom", t.capRoom);
            SP.ParameterSet.Parameters.AddWithValue("@capSpent", t.capSpent);
            SP.ParameterSet.Parameters.AddWithValue("@conferenceId", t.conferenceId);
            SP.ParameterSet.Parameters.AddWithValue("@conferenceName", t.conferenceName);
            SP.ParameterSet.Parameters.AddWithValue("@defPassYds", t.defPassYds);
            SP.ParameterSet.Parameters.AddWithValue("@defPassYdsRank", t.defPassYdsRank);
            SP.ParameterSet.Parameters.AddWithValue("@defRushYds", t.defRushYds);
            SP.ParameterSet.Parameters.AddWithValue("@defRushYdsRank", t.defRushYdsRank);
            SP.ParameterSet.Parameters.AddWithValue("@defTotalYds", t.defTotalYds);
            SP.ParameterSet.Parameters.AddWithValue("@defTotalYdsRank", t.defTotalYdsRank);
            SP.ParameterSet.Parameters.AddWithValue("@divisionId", t.divisionId);
            SP.ParameterSet.Parameters.AddWithValue("@divisionName", t.divisionName);
            SP.ParameterSet.Parameters.AddWithValue("@netPts", t.netPts);
            SP.ParameterSet.Parameters.AddWithValue("@offPassYds", t.offPassYds);
            SP.ParameterSet.Parameters.AddWithValue("@offRushYds", t.offRushYds);
            SP.ParameterSet.Parameters.AddWithValue("@offTotalYds", t.offTotalYds);
            SP.ParameterSet.Parameters.AddWithValue("@playoffStatus", t.playoffStatus);
            SP.ParameterSet.Parameters.AddWithValue("@prevRank", t.prevRank);
            SP.ParameterSet.Parameters.AddWithValue("@ptsAgainst", t.ptsAgainst);
            SP.ParameterSet.Parameters.AddWithValue("@ptsFor", t.ptsFor);
            SP.ParameterSet.Parameters.AddWithValue("@rank", t.rank);
            SP.ParameterSet.Parameters.AddWithValue("@seed", t.seed);
            SP.ParameterSet.Parameters.AddWithValue("@stageIndex", t.stageIndex);
            SP.ParameterSet.Parameters.AddWithValue("@tODiff", t.tODiff);
            SP.ParameterSet.Parameters.AddWithValue("@totalLosses", t.totalLosses);
            SP.ParameterSet.Parameters.AddWithValue("@totalTies", t.totalTies);
            SP.ParameterSet.Parameters.AddWithValue("@totalWins", t.totalWins);
            SP.ParameterSet.Parameters.AddWithValue("@winLossStreak", t.winLossStreak);
            SP.ParameterSet.Parameters.AddWithValue("@winPct", t.winPct);
            
            var status = StoredProc.NonQuery(SP);

            return status;
        }

    }
}