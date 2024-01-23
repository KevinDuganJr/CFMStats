using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using CFMStats.Classes;
using CFMStats.Services;

namespace CFMStats
{
    public partial class Schedule : Page
    {
        protected void BuildRows()
        {
            var stageIndex = Helper.IntegerNull(ddlSeasonType.SelectedItem.Value);
            var seasonIndex = Helper.IntegerNull(ddlSeason.SelectedItem.Value);
            var weekIndex = Helper.IntegerNull(ddlWeek.SelectedItem.Value);
            var leagueId = Helper.IntegerNull(Session["leagueID"]);

            var teams = new TeamService();
            teams = teams.GetLeagueTeams(leagueId);


            tableSchedule.InnerHtml = string.Empty;

            var sp = new StoredProc
            {
                Name = "ScheduleWeek_select", DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, ParameterSet = new SqlCommand()
            };

            sp.ParameterSet.Parameters.AddWithValue("@leagueId", leagueId);
            sp.ParameterSet.Parameters.AddWithValue("@stageIndex", stageIndex);
            sp.ParameterSet.Parameters.AddWithValue("@seasonIndex", seasonIndex);
            sp.ParameterSet.Parameters.AddWithValue("@weekIndex", weekIndex);

            var ds = StoredProc.ShowMeTheData(sp);

            var sbTable = new StringBuilder();

            var firstColumn = "<div class='col'>";
            var middleColumn = "<div class='col d-none d-sm-block' style:display: flex;align-items:center;'> ";
            var lastColumn = "<div class='col'>";

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                var sHomeUser = "CPU";
                var sAwayUser = "CPU";

                if (Helper.StringNull(item.Field<string>("awayUser")).Length > 0)
                {
                    sAwayUser = item.Field<string>("awayUser");
                }

                if (Helper.StringNull(item.Field<string>("homeUser")).Length > 0)
                {
                    sHomeUser = item.Field<string>("homeUser");
                }

                var iHomeScore = item.Field<int>("homeScore");
                var iAwayScore = item.Field<int>("awayScore");

                sbTable.Append("<div class='row alert alert-schedule'>");

                // Away Team
                sbTable.Append(firstColumn);
                sbTable.Append($"<a href='/BoxScore?id={item.Field<int>("awayTeamId")}&leagueId={leagueId}&season={seasonIndex}&week={weekIndex}&type={stageIndex}'><img class='img-fluid img-thumbnail bg-secondary awayLogo' src='/images/team/large/{teams[item.Field<int>("awayTeamId")].logoId}.png' /></a>");
                sbTable.Append("</div>");

                // Away City
                sbTable.Append(middleColumn);
                var awayPrimaryColor = DecimalToHex(Helper.IntegerNull(item["awayPrimaryColor"]));
                var awaySecondaryColor = DecimalToHex(Helper.IntegerNull(item["awaySecondaryColor"]));
                sbTable.Append($"<div style='color:#{awayPrimaryColor};' class='teamCity'>{item.Field<string>("awayCity")}</div>");
                sbTable.Append($"<div style='color: #{awaySecondaryColor};' class='teamName'>{item.Field<string>("awayTeam")}</div>");
                sbTable.Append($"<div class='teamUser'>{sAwayUser}</div>");
                sbTable.Append("</div>");

                // Away Score
                sbTable.Append(lastColumn);
                sbTable.Append(iAwayScore > iHomeScore
                    ? $"<div class='teamScore teamWinner'>{iAwayScore}</div>"
                    : $"<div class='teamScore'>{iAwayScore}</div>");
                sbTable.Append("</div>");

                // Home Team
                sbTable.Append(firstColumn);
                sbTable.Append($"<a href='/BoxScore?id={item.Field<int>("homeTeamId")}&leagueId={leagueId}&season={seasonIndex}&week={weekIndex}&type={stageIndex}'><img class='img-fluid img-thumbnail bg-secondary homeLogo' src='/images/team/large/{teams[item.Field<int>("homeTeamId")].logoId}.png' /></a>");
                sbTable.Append("</div>");

                // Home City
                sbTable.Append(middleColumn);
                var homePrimaryColor = DecimalToHex(Helper.IntegerNull(item["homePrimaryColor"]));
                var homeSecondaryColor = DecimalToHex(Helper.IntegerNull(item["homeSecondaryColor"]));
                sbTable.Append($"<div style='color:#{homePrimaryColor};' class='teamCity'>{item.Field<string>("homeCity")}</div>");
                sbTable.Append($"<div style='color:#{homeSecondaryColor};' class='teamName'>{item.Field<string>("homeTeam")}</div>");
                sbTable.Append($"<div class='teamUser'>{sHomeUser}</div>");
                sbTable.Append("</div>");

                // Home Score
                sbTable.Append(lastColumn);
                sbTable.Append(iHomeScore > iAwayScore
                    ? $"<div class='teamScore teamWinner'>{iHomeScore}</div>"
                    : $"<div class='teamScore'>{iHomeScore}</div>");
                sbTable.Append("</div>");

                sbTable.Append("</div>");
            }

            tableSchedule.InnerHtml = sbTable.ToString();
        }

        protected void BuildSeasonList()
        {
            var season = new oSeasons();
            season = season.getSeasons(Helper.IntegerNull(Session["leagueID"]));

            foreach (var item in season.Values)
            {
                ddlSeason.Items.Add(new ListItem(item.Year.ToString(), item.ID.ToString()));
            }
        }

        protected void ddlSeasonType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //  SetWeek();
        }

        protected void ddlWeek_SelectedIndexChanged(object sender, EventArgs e)
        {
            BuildRows();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                return;
            }

            if (Helper.StringNull(Request.QueryString["leagueId"]).Length == 0)
            {
                // no league, go back to start
                Response.Redirect("~/");
            }

            Session["leagueId"] = Helper.StringNull(Request.QueryString["leagueId"]);

            SetWeek();
            BuildSeasonList();
            GetCurrentSeasonWeek();
        }

        protected void SetWeek()
        {
            var wks = new oWeeks();
            //switch (ddlSeasonType.SelectedIndex)
            //{
            //    case 1:
            //        wks = wks.pre();
            //        break;

            //    case 0:
            wks = wks.Regular();
            //        break;
            //}

            ddlWeek.Items.Clear();

            foreach (var item in wks.Values)
            {
                ddlWeek.Items.Add(new ListItem(item.Text, item.WeekIndex.ToString()));
            }

         //   GetCurrentSeasonWeek();
        }

        private void GetCurrentSeasonWeek()
        {
            var sp = new StoredProc
            {
                Name = "ScheduleCurrentWeek_select", DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, ParameterSet = new SqlCommand()
            };

            sp.ParameterSet.Parameters.AddWithValue("@leagueId", Helper.IntegerNull(Session["leagueID"]));

            var ds = StoredProc.ShowMeTheData(sp);

            if (ds.Tables[0].Rows.Count == 0)
            {
                Response.Redirect("~/");
            }

            if (ds.Tables[0].Rows.Count < 1)
            {
                ddlWeek.SelectedIndex = ddlWeek.Items.Count - 1;
            }

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                ddlSeason.SelectedIndex = ddlSeason.Items.IndexOf(ddlSeason.Items.FindByValue(Helper.StringNull(item["seasonIndex"])));

                var currentWeek = Helper.IntegerNull(item["weekIndex"]);
                // if (currentWeek > 0) { currentWeek = currentWeek - 1; }

                ddlWeek.SelectedIndex = ddlWeek.Items.IndexOf(ddlWeek.Items.FindByValue(Helper.StringNull(currentWeek)));

                int stageIndex = Helper.IntegerNull(item["stageIndex"]);
                ddlSeasonType.SelectedIndex = ddlSeasonType.Items.IndexOf(ddlSeasonType.Items.FindByValue(Helper.StringNull(stageIndex)));
            }

            ddlWeek_SelectedIndexChanged(null, null);
        }

        public static string HexToDec(string hex)
        {
            if (hex.Length % 2 == 1)
            {
                hex = "0" + hex;
            }

            byte[] raw = new byte[hex.Length / 2];
            decimal d = 0;
            for (int i = 0; i < raw.Length; i++)
            {
                raw[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
                d += (decimal) (Math.Pow(256, (raw.Length - 1 - i)) * raw[i]);
            }
            return d.ToString();
            
        }

        public string DecimalToHex(int colorValue)
        {
            int decValue = colorValue;

            // Convert integer 182 as a hex in a string variable
            string hexValue = decValue.ToString("X");

            // Convert the hex string back to the number
            int decAgain = int.Parse(hexValue, System.Globalization.NumberStyles.HexNumber);

            return hexValue;
        }
    }
}