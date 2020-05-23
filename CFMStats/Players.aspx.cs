using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using CFMStats.Classes;

namespace CFMStats
{
    public partial class Players : Page
    {

        protected void ddlPositionGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetPlayers();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Helper.StringNull(Request.QueryString["leagueId"]).Length == 0)
                {
                    Response.Redirect("~/");
                }

                Session["leagueId"] = Helper.StringNull(Request.QueryString["leagueId"]);


                BuildDevelopmentList();
                BuildLeagueTeamList();
                BuildPositionGroupList();
            }
        }

        /// <summary>
        ///     Build the development status list
        /// </summary>
        public void BuildDevelopmentList()
        {
            ddlDevelopment.Items.Clear();

            var traits = new DevelopmentTraitService();
            traits = traits.GetDevelopmentTraits();

            foreach (var trait in traits.Values)
            {
                ddlDevelopment.Items.Add(new ListItem(trait.Name, trait.Id.ToString()));
            }
        }

        private void BuildLeagueTeamList()
        {
            var sp = new StoredProc
            {
                Name = "TeamInfo_select", DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, ParameterSet = new SqlCommand()
            };
            var leagueId = Helper.IntegerNull(Session["leagueId"]);
            sp.ParameterSet.Parameters.AddWithValue("@leagueId", leagueId);

            var ds = StoredProc.ShowMeTheData(sp);

            if (ds.Tables[0].Rows.Count == 0)
            {
                Response.Redirect("~/");
            }

            ddlLeagueTeams.Items.Clear();

            ddlLeagueTeams.Items.Add(new ListItem("All Teams", "99"));

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                ddlLeagueTeams.Items.Add(new ListItem(Helper.StringNull(item["displayName"]), Helper.StringNull(item["teamID"])));
            }
        }

        private void BuildPositionGroupList()
        {
            var sp = new StoredProc
            {
                Name = "PositionGroup_select", DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, ParameterSet = new SqlCommand()
            };

            var ds = StoredProc.ShowMeTheData(sp);

            if (ds.Tables.Count == 0)
            {
                return;
            }

            ddlPositionGroup.Items.Clear();

            ddlPositionGroup.Items.Add(new ListItem("All Positions", "99"));

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                ddlPositionGroup.Items.Add(new ListItem(Helper.StringNull(item["GroupName"]), Helper.StringNull(item["positionGroupID"])));
            }

            ddlPositionGroup.SelectedIndex = 1;

            ddlPositionGroup_SelectedIndexChanged(null, null);
        }

       
        private void GetPlayers()
        {
            var traits = new DevelopmentTraitService();
            traits = traits.GetDevelopmentTraits();


            var iPositionGroup = Helper.IntegerNull(ddlPositionGroup.SelectedItem.Value);

            tablePlayers.InnerHtml = "";

            var sbTable = new StringBuilder();
            sbTable.Append("<table id='sumtable' class='sum_table tablesorter' >");

            sbTable.Append("<thead>");
            sbTable.Append("<tr>");

            sbTable.Append(string.Format("<th data-sorter='true' class='filter-select' data-placeholder='All'  title='{1}'>{0}</th>", "Team", "Player Team"));
            sbTable.Append(string.Format("<th data-sorter='true' class='filter-select' data-placeholder='All'  title='{1}'>{0}</th>", "Pos", "Player Position"));
            sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "Name", "Player Name"));
            sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "Age", "Age"));
            sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "Height", "Height"));
            sbTable.Append(string.Format("<th data-sorter='true' class='filter-select' data-placeholder='All'  title='{1}'>{0}</th>", "DEV", "Development Status"));
            sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "Pro", "Years Pro"));

            sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "OVR", "Overall"));

            sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "AWR", "Awareness"));
            sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "SPD", "Speed"));
            sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "ACC", "Acceleration"));
            sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "AGI", "Agility"));
            sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "STR", "Strength"));

            switch (iPositionGroup)
            {
                case 1: // QB
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "THP", "Throw Power"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "THA", "Throw Accuracy"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "SAC", "Short Throw Accuracy"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "MAC", "Medium Throw Accuracy"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "DAC", "Deep Throw Accuracy"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "PAC", "Play Action"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "RUN", "Throw On Run"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "TUP", "Throw Under Pressure"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "BSK", "Break Sack"));
                    break;

                case 2: // HB
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "BCV", "Ball Carrier Vision"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "ELU", "Elusiveness"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "TRK", "Trucking"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "BTK", "Break Tackle"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "CAR", "Carrying"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "SPM", "Spin Move"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "CTH", "Catching"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "RTE", "Route Running"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "SRR", "Short Route Running"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "MRR", "Medium Route Running"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "DRR", "Deep Route Running"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "JKM", "Juke Move"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "SFA", "Stiff Arm"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "JMP", "Jumping"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "RET", "Kick Return"));
                    break;

                case 3: // FB
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "IBL", "Impact Blocking"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "RBK", "Run Block"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "LB", "Lead Block"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "PBK", "Pass Block"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "CAR", "Carrying"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "TRK", "Trucking"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "BTK", "Break Tackle"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "CTH", "Catching"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "SFA", "Stiff Arm"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "BCV", "Ball Carrier Vision"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "TGH", "Toughness"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "RTE", "Route Running"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "SRR", "Short Route Running"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "MRR", "Medium Route Running"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "DRR", "Deep Route Running"));
                    break;

                case 4: // WR
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "RTE", "Route Running"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "SRR", "Short Route Running"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "MRR", "Medium Route Running"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "DRR", "Deep Route Running"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "CTH", "Catching"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "CIT", "Catch In Traffic"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "RLS", "Release"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "SPC", "Spectacular Catch"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "JMP", "Jumping"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "CAR", "Carrying"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "ELU", "Elusiveness"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "BTK", "Break Tackle"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "BCV", "Ball Carrier Vision"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "RET", "Kick Return"));
                    break;

                case 5: // TE
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "RBK", "Run Block"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "RBF", "Run Block Finesse"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "RBP", "Run Block Power"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "PBK", "Pass Block"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "PBF", "Pass Block Finesse"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "PBP", "Pass Block Power"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "CTH", "Catching"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "SPC", "Spectacular Catch"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "CIT", "Catch In Traffic"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "RTE", "Route Running"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "SRR", "Short Route Running"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "MRR", "Medium Route Running"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "DRR", "Deep Route Running"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "IBL", "Impact Blocking"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "JMP", "Jumping"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "TRK", "Trucking"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "BTK", "Break Tackle"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "SFA", "Stiff Arm"));
                    break;

                case 6: // LT LG C RG RT
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "LB", "Lead Block"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "RBK", "Run Block"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "RBF", "Run Block Finesse"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "RBP", "Run Block Power"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "PBK", "Pass Block"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "PBF", "Pass Block Finesse"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "PBP", "Pass Block Power"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "IBL", "Impact Blocking"));
                    break;

                case 7: // RE DT LE
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "PMV", "Power Moves"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "FMV", "Finesse Moves"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "PRC", "Play Recognition"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "TAK", "Tackling"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "BSH", "Block Shedding"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "PUR", "Pursuit"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "POW", "Hit Power"));
                    break;

                case 8: // ROLB MLB LOLB
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "ZCV", "Zone Coverage"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "TAK", "Tackling"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "PUR", "Pursuit"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "PMV", "Power Moves"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "PRC", "Play Recognition"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "MCV", "Man Coverage"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "POW", "Hit Power"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "FMV", "Finesse Moves"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "BSH", "Block Shedding"));
                    break;

                case 9: // CB
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "MCV", "Man Coverage"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "ZCV", "Zone Coverage"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "PRC", "Play Recognition"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "TAK", "Tackling"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "JMP", "Jumping"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "PRS", "Press"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "CTH", "Catching"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "PUR", "Pursuit"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "POW", "Hit Power"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "BSH", "Block Shedding"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "RET", "Kick Return"));
                    break;

                case 10: // FS SS
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "PRC", "Play Recognition"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "ZCV", "Zone Coverage"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "POW", "Hit Power"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "TAK", "Tackling"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "PUR", "Pursuit"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "MCV", "Man Coverage"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "JMP", "Jumping"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "BSH", "Block Shedding"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "CTH", "Catching"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "PRS", "Press"));
                    break;

                case 11: // K P 
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "KPW", "Kick Power"));
                    sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "KAC", "Kick Accuracy"));
                    break;
            }

            sbTable.Append(string.Format("<th data-sorter='true'  title='{1}'>{0}</th>", "SP", "Skill Points"));

            sbTable.Append("</tr>");
            sbTable.Append("</thead>");

            sbTable.Append("<tbody>");

            // using SQL
            var teamRosters = new oRosters();
            teamRosters = teamRosters.GetPlayerRatings(Helper.IntegerNull(ddlPositionGroup.SelectedItem.Value),
                Helper.IntegerNull(ddlLeagueTeams.SelectedItem.Value),
                Helper.IntegerNull(ddlDevelopment.SelectedItem.Value),
                0,
                chkTwentyFiveAndUnder.Checked,
                Helper.IntegerNull(Session["leagueId"])
                );

            foreach (var item in teamRosters.Values)
            {
                sbTable.Append("<tr>");
                //sbTable.Append(string.Format("<td>{0}</td>", itemTeam.abbrName));

                if (item.teamName.Length < 2)
                {
                    item.teamName = "Z Free Agent";
                }

                var cssPlayerStyle = new StringBuilder();

                sbTable.Append(string.Format("<td class='c{0}'><div style='display:none;'>{0}</div></td>",
                    item.teamName.Replace(" ", string.Empty), item.teamId));

                if (item.injuryType != 97 && item.isActive == false)
                {
                    cssPlayerStyle.Append("injured ");
                }

                if (item.isOnPracticeSquad)
                {
                    cssPlayerStyle.Append("psquad ");
                }

                if (item.isRetired)
                {
                    cssPlayerStyle.Append("isretired ");
                }

                sbTable.Append($"<td>{item.position}</td>");

                sbTable.Append($"<td style='text-align:left;' class='{cssPlayerStyle}' ><a  target='_blank' href='/profile?id={item.playerId}'>{item.firstName} {item.lastName}</a></td>");

                sbTable.Append($"<td>{item.age}</td>");
                sbTable.Append($"<td data-value={item.height}>{PlayersHeightFromInches(item.height)}</td>");
                sbTable.Append($"<td><small>{traits[item.devTrait].Name}</small></td>");
                sbTable.Append($"<td>{item.yearsPro}</td>");

                // ratings
                sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.playerSchemeOvr, Helper.RatingLevel(item.playerSchemeOvr)));
                sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.awareRating, Helper.RatingLevel(item.awareRating)));
                sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.speedRating, Helper.RatingLevel(item.speedRating)));
                sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.accelRating, Helper.RatingLevel(item.accelRating)));
                sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.agilityRating, Helper.RatingLevel(item.agilityRating)));
                sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.strengthRating, Helper.RatingLevel(item.strengthRating)));

                switch (iPositionGroup)
                {
                    case 1: // QB
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.throwPowerRating, Helper.RatingLevel(item.throwPowerRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.throwAccRating, Helper.RatingLevel(item.throwAccRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.throwAccShortRating, Helper.RatingLevel(item.throwAccShortRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.throwAccMidRating, Helper.RatingLevel(item.throwAccMidRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.throwAccDeepRating, Helper.RatingLevel(item.throwAccDeepRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.playActionRating, Helper.RatingLevel(item.playActionRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.throwOnRunRating, Helper.RatingLevel(item.throwOnRunRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.throwUnderPressureRating, Helper.RatingLevel(item.throwUnderPressureRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.breakSackRating, Helper.RatingLevel(item.breakSackRating)));
                        break;

                    case 2: // HB
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.bCVRating, Helper.RatingLevel(item.bCVRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.elusiveRating, Helper.RatingLevel(item.elusiveRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.truckRating, Helper.RatingLevel(item.truckRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.breakTackleRating, Helper.RatingLevel(item.breakTackleRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.carryRating, Helper.RatingLevel(item.carryRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.spinMoveRating, Helper.RatingLevel(item.spinMoveRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.catchRating, Helper.RatingLevel(item.catchRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.routeRunRating, Helper.RatingLevel(item.routeRunRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.routeRunShortRating, Helper.RatingLevel(item.routeRunShortRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.routeRunMedRating, Helper.RatingLevel(item.routeRunMedRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.routeRunDeepRating, Helper.RatingLevel(item.routeRunDeepRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.jukeMoveRating, Helper.RatingLevel(item.jukeMoveRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.stiffArmRating, Helper.RatingLevel(item.stiffArmRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.jumpRating, Helper.RatingLevel(item.jumpRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.kickRetRating, Helper.RatingLevel(item.kickRetRating)));
                        break;

                    case 3: // FB
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.impactBlockRating, Helper.RatingLevel(item.impactBlockRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.runBlockRating, Helper.RatingLevel(item.runBlockRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.leadBlockRating, Helper.RatingLevel(item.leadBlockRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.passBlockRating, Helper.RatingLevel(item.passBlockRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.carryRating, Helper.RatingLevel(item.carryRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.truckRating, Helper.RatingLevel(item.truckRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.breakTackleRating, Helper.RatingLevel(item.breakTackleRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.catchRating, Helper.RatingLevel(item.catchRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.stiffArmRating, Helper.RatingLevel(item.stiffArmRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.bCVRating, Helper.RatingLevel(item.bCVRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.toughRating, Helper.RatingLevel(item.toughRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.routeRunRating, Helper.RatingLevel(item.routeRunRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.routeRunShortRating, Helper.RatingLevel(item.routeRunShortRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.routeRunMedRating, Helper.RatingLevel(item.routeRunMedRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.routeRunDeepRating, Helper.RatingLevel(item.routeRunDeepRating)));

                        break;

                    case 4: // WR
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.routeRunRating, Helper.RatingLevel(item.routeRunRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.routeRunShortRating, Helper.RatingLevel(item.routeRunShortRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.routeRunMedRating, Helper.RatingLevel(item.routeRunMedRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.routeRunDeepRating, Helper.RatingLevel(item.routeRunDeepRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.catchRating, Helper.RatingLevel(item.catchRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.cITRating, Helper.RatingLevel(item.cITRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.releaseRating, Helper.RatingLevel(item.releaseRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.specCatchRating, Helper.RatingLevel(item.specCatchRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.jumpRating, Helper.RatingLevel(item.jumpRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.carryRating, Helper.RatingLevel(item.carryRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.elusiveRating, Helper.RatingLevel(item.elusiveRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.breakTackleRating, Helper.RatingLevel(item.breakTackleRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.bCVRating, Helper.RatingLevel(item.bCVRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.kickRetRating, Helper.RatingLevel(item.kickRetRating)));

                        break;

                    case 5: // TE
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.runBlockRating, Helper.RatingLevel(item.runBlockRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.runBlockFinesseRating, Helper.RatingLevel(item.runBlockFinesseRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.runBlockPowerRating, Helper.RatingLevel(item.runBlockPowerRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.passBlockRating, Helper.RatingLevel(item.passBlockRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.passBlockFinesseRating, Helper.RatingLevel(item.passBlockFinesseRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.passBlockPowerRating, Helper.RatingLevel(item.passBlockPowerRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.catchRating, Helper.RatingLevel(item.catchRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.specCatchRating, Helper.RatingLevel(item.specCatchRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.cITRating, Helper.RatingLevel(item.cITRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.routeRunRating, Helper.RatingLevel(item.routeRunRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.routeRunShortRating, Helper.RatingLevel(item.routeRunShortRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.routeRunMedRating, Helper.RatingLevel(item.routeRunMedRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.routeRunDeepRating, Helper.RatingLevel(item.routeRunDeepRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.impactBlockRating, Helper.RatingLevel(item.impactBlockRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.jumpRating, Helper.RatingLevel(item.jumpRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.truckRating, Helper.RatingLevel(item.truckRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.breakTackleRating, Helper.RatingLevel(item.breakTackleRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.stiffArmRating, Helper.RatingLevel(item.stiffArmRating)));

                        break;

                    case 6: // LT LG C RG RT
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.leadBlockRating, Helper.RatingLevel(item.leadBlockRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.runBlockRating, Helper.RatingLevel(item.runBlockRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.runBlockFinesseRating, Helper.RatingLevel(item.runBlockFinesseRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.runBlockPowerRating, Helper.RatingLevel(item.runBlockPowerRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.passBlockRating, Helper.RatingLevel(item.passBlockRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.passBlockFinesseRating, Helper.RatingLevel(item.passBlockFinesseRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.passBlockPowerRating, Helper.RatingLevel(item.passBlockPowerRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.impactBlockRating, Helper.RatingLevel(item.impactBlockRating)));
                        break;

                    case 7: // RE DT LE
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.powerMovesRating, Helper.RatingLevel(item.powerMovesRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.finesseMovesRating, Helper.RatingLevel(item.finesseMovesRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.playRecRating, Helper.RatingLevel(item.playRecRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.tackleRating, Helper.RatingLevel(item.tackleRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.blockShedRating, Helper.RatingLevel(item.blockShedRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.pursuitRating, Helper.RatingLevel(item.pursuitRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.hitPowerRating, Helper.RatingLevel(item.hitPowerRating)));
                        break;

                    case 8: // ROLB MLB LOLB
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.zoneCoverRating, Helper.RatingLevel(item.zoneCoverRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.tackleRating, Helper.RatingLevel(item.tackleRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.pursuitRating, Helper.RatingLevel(item.pursuitRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.powerMovesRating, Helper.RatingLevel(item.powerMovesRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.playRecRating, Helper.RatingLevel(item.playRecRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.manCoverRating, Helper.RatingLevel(item.manCoverRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.hitPowerRating, Helper.RatingLevel(item.hitPowerRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.finesseMovesRating, Helper.RatingLevel(item.finesseMovesRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.blockShedRating, Helper.RatingLevel(item.blockShedRating)));
                        break;

                    case 9: // CB
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.manCoverRating, Helper.RatingLevel(item.manCoverRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.zoneCoverRating, Helper.RatingLevel(item.zoneCoverRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.playRecRating, Helper.RatingLevel(item.playRecRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.tackleRating, Helper.RatingLevel(item.tackleRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.jumpRating, Helper.RatingLevel(item.jumpRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.pressRating, Helper.RatingLevel(item.pressRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.catchRating, Helper.RatingLevel(item.catchRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.pursuitRating, Helper.RatingLevel(item.pursuitRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.hitPowerRating, Helper.RatingLevel(item.hitPowerRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.blockShedRating, Helper.RatingLevel(item.blockShedRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.kickRetRating, Helper.RatingLevel(item.kickRetRating)));
                        break;

                    case 10: // FS SS
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.playRecRating, Helper.RatingLevel(item.playRecRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.zoneCoverRating, Helper.RatingLevel(item.zoneCoverRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.hitPowerRating, Helper.RatingLevel(item.hitPowerRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.tackleRating, Helper.RatingLevel(item.tackleRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.pursuitRating, Helper.RatingLevel(item.pursuitRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.manCoverRating, Helper.RatingLevel(item.manCoverRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.jumpRating, Helper.RatingLevel(item.jumpRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.blockShedRating, Helper.RatingLevel(item.blockShedRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.catchRating, Helper.RatingLevel(item.catchRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.pressRating, Helper.RatingLevel(item.pressRating)));
                        break;

                    case 11: // K P 
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.kickPowerRating, Helper.RatingLevel(item.kickPowerRating)));
                        sbTable.Append(string.Format("<td class='{1}'>{0}</td>", item.kickAccRating, Helper.RatingLevel(item.kickAccRating)));
                        break;
                }

                sbTable.Append($"<td><small>{item.skillPoints:n0}</small></td>");

                sbTable.Append("</tr>");
            }

            sbTable.Append("</tbody>");
            sbTable.Append("</table>");

            tablePlayers.InnerHtml = sbTable.ToString();
        }

        private string PlayersHeightFromInches(int inches)
        {
            var feet = inches / 12;
            var inchesLeft = inches % 12;
            return$"{feet}' {inchesLeft}\"";
        }
    }
}