using System.Collections.Generic;

namespace CFMStats.Classes
{
    public class oWeek
    {
        public int StageIndex { get; set; }
        public int WeekIndex { get; set; }
        public string Text { get; set; }
    }

    public class oWeeks : Dictionary<int, oWeek>
    {
        public oWeeks Regular()
        {
            oWeeks wks = new oWeeks();
            oWeek wk = new oWeek();
            //wk = new oWeek(); wk.StageIndex = 0; wk.WeekIndex = 0; wk.Text = "1"; wks.Add(wk.WeekIndex, wk);
            //wk = new oWeek(); wk.StageIndex = 0; wk.WeekIndex = 1; wk.Text = "2"; wks.Add(wk.WeekIndex, wk);
            //wk = new oWeek(); wk.StageIndex = 0; wk.WeekIndex = 2; wk.Text = "3"; wks.Add(wk.WeekIndex, wk);
            //wk = new oWeek(); wk.StageIndex = 0; wk.WeekIndex = 3; wk.Text = "BYE"; wks.Add(wk.WeekIndex, wk);

            wk = new oWeek(); wk.StageIndex = 1; wk.WeekIndex = 0; wk.Text = "1"; wks.Add(wk.WeekIndex, wk);
            wk = new oWeek(); wk.StageIndex = 1; wk.WeekIndex = 1; wk.Text = "2"; wks.Add(wk.WeekIndex, wk);
            wk = new oWeek(); wk.StageIndex = 1; wk.WeekIndex = 2; wk.Text = "3"; wks.Add(wk.WeekIndex, wk);
            wk = new oWeek(); wk.StageIndex = 1; wk.WeekIndex = 3; wk.Text = "4"; wks.Add(wk.WeekIndex, wk);
            wk = new oWeek(); wk.StageIndex = 1; wk.WeekIndex = 4; wk.Text = "5"; wks.Add(wk.WeekIndex, wk);
            wk = new oWeek(); wk.StageIndex = 1; wk.WeekIndex = 5; wk.Text = "6"; wks.Add(wk.WeekIndex, wk);
            wk = new oWeek(); wk.StageIndex = 1; wk.WeekIndex = 6; wk.Text = "7"; wks.Add(wk.WeekIndex, wk);
            wk = new oWeek(); wk.StageIndex = 1; wk.WeekIndex = 7; wk.Text = "8"; wks.Add(wk.WeekIndex, wk);
            wk = new oWeek(); wk.StageIndex = 1; wk.WeekIndex = 8; wk.Text = "9"; wks.Add(wk.WeekIndex, wk);
            wk = new oWeek(); wk.StageIndex = 1; wk.WeekIndex = 9; wk.Text = "10"; wks.Add(wk.WeekIndex, wk);
            wk = new oWeek(); wk.StageIndex = 1; wk.WeekIndex = 10; wk.Text = "11"; wks.Add(wk.WeekIndex, wk);
            wk = new oWeek(); wk.StageIndex = 1; wk.WeekIndex = 11; wk.Text = "12"; wks.Add(wk.WeekIndex, wk);
            wk = new oWeek(); wk.StageIndex = 1; wk.WeekIndex = 12; wk.Text = "13"; wks.Add(wk.WeekIndex, wk);
            wk = new oWeek(); wk.StageIndex = 1; wk.WeekIndex = 13; wk.Text = "14"; wks.Add(wk.WeekIndex, wk);
            wk = new oWeek(); wk.StageIndex = 1; wk.WeekIndex = 14; wk.Text = "15"; wks.Add(wk.WeekIndex, wk);
            wk = new oWeek(); wk.StageIndex = 1; wk.WeekIndex = 15; wk.Text = "16"; wks.Add(wk.WeekIndex, wk);
            wk = new oWeek(); wk.StageIndex = 1; wk.WeekIndex = 16; wk.Text = "17"; wks.Add(wk.WeekIndex, wk);
            wk = new oWeek(); wk.StageIndex = 1; wk.WeekIndex = 17; wk.Text = "18"; wks.Add(wk.WeekIndex, wk);
            wk = new oWeek(); wk.StageIndex = 1; wk.WeekIndex = 18; wk.Text = "Wild Card"; wks.Add(wk.WeekIndex, wk);
            wk = new oWeek(); wk.StageIndex = 1; wk.WeekIndex = 19; wk.Text = "Division"; wks.Add(wk.WeekIndex, wk);
            wk = new oWeek(); wk.StageIndex = 1; wk.WeekIndex = 20; wk.Text = "Conference"; wks.Add(wk.WeekIndex, wk);
            wk = new oWeek(); wk.StageIndex = 1; wk.WeekIndex = 21; wk.Text = "Pro Bowl"; wks.Add(wk.WeekIndex, wk);
            wk = new oWeek(); wk.StageIndex = 1; wk.WeekIndex = 22; wk.Text = "Super Bowl"; wks.Add(wk.WeekIndex, wk);

            //wk = new oWeek(); wk.StageIndex = 2; wk.WeekIndex = 0; wk.Text = "Staff Week"; wks.Add(wk.WeekIndex, wk);
            //wk = new oWeek(); wk.StageIndex = 2; wk.WeekIndex = 1; wk.Text = "Re-Sign Players"; wks.Add(wk.WeekIndex, wk);
            //wk = new oWeek(); wk.StageIndex = 2; wk.WeekIndex = 2; wk.Text = "Free Agency 1"; wks.Add(wk.WeekIndex, wk);
            //wk = new oWeek(); wk.StageIndex = 2; wk.WeekIndex = 4; wk.Text = "Free Agency 2"; wks.Add(wk.WeekIndex, wk);
            //wk = new oWeek(); wk.StageIndex = 2; wk.WeekIndex = 6; wk.Text = "Free Agency 3"; wks.Add(wk.WeekIndex, wk);
            //wk = new oWeek(); wk.StageIndex = 2; wk.WeekIndex = 7; wk.Text = "Free Agency Recap"; wks.Add(wk.WeekIndex, wk);
            //wk = new oWeek(); wk.StageIndex = 2; wk.WeekIndex = 11; wk.Text = "Draft"; wks.Add(wk.WeekIndex, wk);
            //wk = new oWeek(); wk.StageIndex = 2; wk.WeekIndex = 15; wk.Text = "Draft Recap"; wks.Add(wk.WeekIndex, wk);

            return wks;
        }



    }
}