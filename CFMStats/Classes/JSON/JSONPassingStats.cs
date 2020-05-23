namespace CFMStats.Classes.JSON
{
    public class JSONPassingStats
    {

        public class Rootobject
        {
            public Playerpassingstatinfolist[] playerPassingStatInfoList { get; set; }
        }

        public class Playerpassingstatinfolist
        {
            public string fullName { get; set; }
            public int passAtt { get; set; }
            public int passComp { get; set; }
            public string passCompPct { get; set; }
            public int passInts { get; set; }
            public int passLongest { get; set; }
            public int passPts { get; set; }
            public int passSacks { get; set; }
            public int passTDs { get; set; }
            public int passYds { get; set; }
            public string passYdsPerAtt { get; set; }
            public string passYdsPerGame { get; set; }
            public string passerRating { get; set; }
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