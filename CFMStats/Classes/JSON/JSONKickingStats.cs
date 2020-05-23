namespace CFMStats.Classes.JSON
{
    public class JSONKickingStats
    {

        public class Rootobject
        {
            public Playerkickingstatinfolist[] playerKickingStatInfoList { get; set; }
        }

        public class Playerkickingstatinfolist
        {
            public int fG50PlusAtt { get; set; }
            public int fG50PlusMade { get; set; }
            public int fGAtt { get; set; }
            public string fGCompPct { get; set; }
            public int fGLongest { get; set; }
            public int fGMade { get; set; }
            public string fullName { get; set; }
            public int kickPts { get; set; }
            public int kickoffAtt { get; set; }
            public int kickoffTBs { get; set; }
            public int rosterId { get; set; }
            public int scheduleId { get; set; }
            public int seasonIndex { get; set; }
            public int stageIndex { get; set; }
            public int statId { get; set; }
            public int teamId { get; set; }
            public int weekIndex { get; set; }
            public int xPAtt { get; set; }
            public string xPCompPct { get; set; }
            public int xPMade { get; set; }
        }

    }
}