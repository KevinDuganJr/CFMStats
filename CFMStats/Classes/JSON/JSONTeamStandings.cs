namespace CFMStats.Classes.JSON
{
    public class JSONTeamStandings
    {
        public class Rootobject
        {
            public Teamstandinginfolist[] teamStandingInfoList { get; set; }
        }

        public class Teamstandinginfolist
        {
            public int awayLosses { get; set; }

            public int awayTies { get; set; }

            public int awayWins { get; set; }

            public int calendarYear { get; set; }

            public int capAvailable { get; set; }

            public int capRoom { get; set; }

            public int capSpent { get; set; }

            public int conferenceId { get; set; }

            public string conferenceName { get; set; }

            public int confLosses { get; set; }

            public int confTies { get; set; }

            public int confWins { get; set; }

            public int defPassYds { get; set; }

            public int defPassYdsRank { get; set; }

            public int defRushYds { get; set; }

            public int defRushYdsRank { get; set; }

            public int defTotalYds { get; set; }

            public int defTotalYdsRank { get; set; }

            public int divisionId { get; set; }

            public string divisionName { get; set; }

            public int divLosses { get; set; }

            public int divTies { get; set; }

            public int divWins { get; set; }

            public int homeLosses { get; set; }

            public int homeTies { get; set; }

            public int homeWins { get; set; }

            public int netPts { get; set; }

            public int offPassYds { get; set; }

            public int offPassYdsRank { get; set; }

            public int offRushYds { get; set; }

            public int offRushYdsRank { get; set; }

            public int offTotalYds { get; set; }

            public int offTotalYdsRank { get; set; }

            public int playoffStatus { get; set; }

            public int prevRank { get; set; }

            public int ptsAgainst { get; set; }

            public int ptsAgainstRank { get; set; }

            public int ptsFor { get; set; }

            public int ptsForRank { get; set; }

            public int rank { get; set; }

            public int seasonIndex { get; set; }

            public int seed { get; set; }

            public int stageIndex { get; set; }

            public int teamId { get; set; }

            public string teamName { get; set; }

            public int teamOvr { get; set; }

            public int tODiff { get; set; }

            public int totalLosses { get; set; }

            public int totalTies { get; set; }

            public int totalWins { get; set; }

            public int weekIndex { get; set; }

            public int winLossStreak { get; set; }

            public float winPct { get; set; }
        }
    }
}