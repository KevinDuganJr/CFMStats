namespace CFMStats.Classes.JSON
{
    public class JSONPuntingStats
    {

        public class Rootobject
        {
            public Playerpuntingstatinfolist[] playerPuntingStatInfoList { get; set; }
        }

        public class Playerpuntingstatinfolist
        {
            public string fullName { get; set; }
            public int puntAtt { get; set; }
            public int puntLongest { get; set; }
            public int puntNetYds { get; set; }
            public string puntNetYdsPerAtt { get; set; }
            public int puntTBs { get; set; }
            public int puntYds { get; set; }
            public string puntYdsPerAtt { get; set; }
            public int puntsBlocked { get; set; }
            public int puntsIn20 { get; set; }
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