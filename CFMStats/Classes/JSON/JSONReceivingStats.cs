namespace CFMStats.Classes.JSON
{
    public class JSONReceivingStats
    {

        public class Rootobject
        {
            public Playerreceivingstatinfolist[] playerReceivingStatInfoList { get; set; }
        }

        public class Playerreceivingstatinfolist
        {
            public string fullName { get; set; }
            public float recCatchPct { get; set; }
            public int recCatches { get; set; }
            public int recDrops { get; set; }
            public int recLongest { get; set; }
            public int recPts { get; set; }
            public int recTDs { get; set; }
            public int recToPct { get; set; }
            public float recYacPerCatch { get; set; }
            public int recYds { get; set; }
            public int recYdsAfterCatch { get; set; }
            public float recYdsPerCatch { get; set; }
            public float recYdsPerGame { get; set; }
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