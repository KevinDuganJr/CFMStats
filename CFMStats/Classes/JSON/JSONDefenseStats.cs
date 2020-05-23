namespace CFMStats.Classes.JSON
{
    public class JSONDefenseStats
    {

        public class Rootobject
        {
            public Playerdefensivestatinfolist[] playerDefensiveStatInfoList { get; set; }
        }

        public class Playerdefensivestatinfolist
        {
            public int defCatchAllowed { get; set; }
            public int defDeflections { get; set; }
            public int defForcedFum { get; set; }
            public int defFumRec { get; set; }
            public int defIntReturnYds { get; set; }
            public int defInts { get; set; }
            public int defPts { get; set; }
            public string defSacks { get; set; }
            public int defSafeties { get; set; }
            public int defTDs { get; set; }
            public int defTotalTackles { get; set; }
            public string fullName { get; set; }
            public int rosterId { get; set; }
            public int scheduleId { get; set; }
            public int seasonIndex { get; set; }
            public int stageIndex { get; set; }
            public int statId { get; set; }
            public int teamId { get; set; }
            public int weekIndex { get; set; }
        }

    }
}