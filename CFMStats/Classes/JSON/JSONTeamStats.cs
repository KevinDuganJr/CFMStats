namespace CFMStats.Classes.JSON
{
    public class JSONTeamStats
    {
        //https://dugan-760bc.firebaseio.com/data/ps3/5105855/week/reg/1/teamstats/.json

        public class Rootobject
        {
            public Teamstatinfolist[] teamStatInfoList { get; set; }
        }

        public class Teamstatinfolist
        {
            public int defForcedFum { get; set; }

            public int defFumRec { get; set; }

            public int defIntsRec { get; set; }

            public int defPassYds { get; set; }

            public decimal defPtsPerGame { get; set; }

            public int defRedZoneFGs { get; set; }

            public decimal defRedZonePct { get; set; }

            public int defRedZones { get; set; }

            public int defRedZoneTDs { get; set; }

            public int defRushYds { get; set; }

            public int defSacks { get; set; }

            public int defTotalYds { get; set; }

            public int off1stDowns { get; set; }

            public int off2PtAtt { get; set; }

            public int off2PtConv { get; set; }

            public int off2PtConvPct { get; set; }

            public int off3rdDownAtt { get; set; }

            public int off3rdDownConv { get; set; }

            public decimal off3rdDownConvPct { get; set; }

            public int off4thDownAtt { get; set; }

            public int off4thDownConv { get; set; }

            public decimal off4thDownConvPct { get; set; }

            public int offFumLost { get; set; }

            public int offIntsLost { get; set; }

            public int offPassTDs { get; set; }

            public int offPassYds { get; set; }

            public decimal offPtsPerGame { get; set; }

            public int offRedZoneFGs { get; set; }

            public decimal offRedZonePct { get; set; }

            public int offRedZones { get; set; }

            public int offRedZoneTDs { get; set; }

            public int offRushTDs { get; set; }

            public int offRushYds { get; set; }

            public int offSacks { get; set; }

            public int offTotalYds { get; set; }

            public int offTotalYdsGained { get; set; }

            public int penalties { get; set; }

            public int penaltyYds { get; set; }

            public int scheduleId { get; set; }

            public int seasonIndex { get; set; }

            public int seed { get; set; }

            public int stageIndex { get; set; }

            public int statId { get; set; }

            public int teamId { get; set; }

            public int tODiff { get; set; }

            public int tOGiveaways { get; set; }

            public int tOTakeaways { get; set; }

            public int totalLosses { get; set; }

            public int totalTies { get; set; }

            public int totalWins { get; set; }

            public int weekIndex { get; set; }
        }
    }
}