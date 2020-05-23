namespace CFMStats.Classes.JSON
{
    public class JSONSchedule
    {

        public class Rootobject
        {
            public Gamescheduleinfolist[] gameScheduleInfoList { get; set; }
        }

        public class Gamescheduleinfolist
        {
            public int awayScore { get; set; }
            public int awayTeamId { get; set; }
            public int homeScore { get; set; }
            public int homeTeamId { get; set; }
            public bool isGameOfTheWeek { get; set; }
            public int scheduleId { get; set; }
            public int seasonIndex { get; set; }
            public int stageIndex { get; set; }
            public int status { get; set; }
            public int weekIndex { get; set; }
        }

    }
}