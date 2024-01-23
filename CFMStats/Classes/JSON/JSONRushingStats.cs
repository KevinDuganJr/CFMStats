namespace CFMStats.Classes.JSON
{
    public class JSONRushingStats
    {
        public class Rootobject
        {
            public Playerrushingstatinfolist[] playerRushingStatInfoList { get; set; }
        }

        public class Playerrushingstatinfolist
        {
            public string fullName { get; set; }

            public int rosterId { get; set; }

            public int rush20PlusYds { get; set; }

            public int rushAtt { get; set; }

            public int rushBrokenTackles { get; set; }

            public int rushFum { get; set; }

            public int rushLongest { get; set; }

            public int rushPts { get; set; }

            public int rushTDs { get; set; }

            public int rushToPct { get; set; }

            public int rushYds { get; set; }

            public int rushYdsAfterContact { get; set; }

            public decimal rushYdsPerAtt { get; set; }

            public decimal rushYdsPerGame { get; set; }

            public int scheduleId { get; set; }

            public int seasonIndex { get; set; }

            public int stageIndex { get; set; }

            public int statId { get; set; }

            public int teamId { get; set; }

            public int weekIndex { get; set; }
        }
    }
}