using System.Collections.Generic;

namespace CFMStats.Classes
{
    public class oWeek
    {
        public int Value { get; set; }
        public string Text { get; set; }
    }

    public class oWeeks : Dictionary<int, oWeek>
    {
        public oWeeks Pre()
        {
            oWeeks wks = new oWeeks();
            oWeek wk = new oWeek();
            wk.Value = 0; wk.Text = "1"; wks.Add(wk.Value, wk);
            wk = new oWeek(); wk.Value = 1; wk.Text = "2"; wks.Add(wk.Value, wk);
            wk = new oWeek(); wk.Value = 2; wk.Text = "3"; wks.Add(wk.Value, wk);
            wk = new oWeek(); wk.Value = 3; wk.Text = "4"; wks.Add(wk.Value, wk);

            return wks;
        }

        public oWeeks Regular()
        {
            oWeeks wks = new oWeeks();
            oWeek wk = new oWeek(); wk = new oWeek(); wk.Value = 0; wk.Text = "1"; wks.Add(wk.Value, wk);
            wk = new oWeek(); wk.Value = 1; wk.Text = "2"; wks.Add(wk.Value, wk);
            wk = new oWeek(); wk.Value = 2; wk.Text = "3"; wks.Add(wk.Value, wk);
            wk = new oWeek(); wk.Value = 3; wk.Text = "4"; wks.Add(wk.Value, wk);
            wk = new oWeek(); wk.Value = 4; wk.Text = "5"; wks.Add(wk.Value, wk);
            wk = new oWeek(); wk.Value = 5; wk.Text = "6"; wks.Add(wk.Value, wk);
            wk = new oWeek(); wk.Value = 6; wk.Text = "7"; wks.Add(wk.Value, wk);
            wk = new oWeek(); wk.Value = 7; wk.Text = "8"; wks.Add(wk.Value, wk);
            wk = new oWeek(); wk.Value = 8; wk.Text = "9"; wks.Add(wk.Value, wk);
            wk = new oWeek(); wk.Value = 9; wk.Text = "10"; wks.Add(wk.Value, wk);
            wk = new oWeek(); wk.Value = 10; wk.Text = "11"; wks.Add(wk.Value, wk);
            wk = new oWeek(); wk.Value = 11; wk.Text = "12"; wks.Add(wk.Value, wk);
            wk = new oWeek(); wk.Value = 12; wk.Text = "13"; wks.Add(wk.Value, wk);
            wk = new oWeek(); wk.Value = 13; wk.Text = "14"; wks.Add(wk.Value, wk);
            wk = new oWeek(); wk.Value = 14; wk.Text = "15"; wks.Add(wk.Value, wk);
            wk = new oWeek(); wk.Value = 15; wk.Text = "16"; wks.Add(wk.Value, wk);
            wk = new oWeek(); wk.Value = 16; wk.Text = "17"; wks.Add(wk.Value, wk);
            wk = new oWeek(); wk.Value = 17; wk.Text = "Wild Card"; wks.Add(wk.Value, wk);
            wk = new oWeek(); wk.Value = 18; wk.Text = "Division"; wks.Add(wk.Value, wk);
            wk = new oWeek(); wk.Value = 19; wk.Text = "Conference"; wks.Add(wk.Value, wk);
            wk = new oWeek(); wk.Value = 21; wk.Text = "Super Bowl"; wks.Add(wk.Value, wk);

            return wks;
        }

    }
}