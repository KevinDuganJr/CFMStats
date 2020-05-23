<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="CFMStats.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">

        <div class="panel panel-default">
            <div class="panel-heading">
                <h4 class="panel-title">Madden Companion App - Exporting Data
                </h4>
            </div>

            <div class="panel-body">
                <div class="row">
                    <div class="col-sm-9">
                        <h4>What is happening</h4>
                        <ul>
                            <li><strong>League Info</strong>
                                <ul>
                                    <li>Exports data for League Info (e.g. teams and nicknames) and Team Standings.</li>
                                </ul>
                            </li>
                            <li><strong>Rosters</strong>
                                <ul>
                                    <li>Exports data for Rosters on each team and Free Agents.</li>
                                </ul>
                            </li>
                            <li><strong>Weekly Stats</strong>
                                <ul>
                                    <li>Exports data for Schedules, Team Stats, and Player Stats.</li>
                                </ul>
                            </li>
                        </ul>

                        <hr />

                        <h4>Export data in this order</h4>
                        <ol type="1">
                            <li><strong>League Info</strong></li>

                            <li><strong>Weekly Stats</strong> (select the week that was just completed)
                                <ul>
                                    <li>Select "All Weeks" to build the schedule for a new league or if you started a new season.</li>
                                    <li>During the Playoffs: Export last week completed, and then export current week.
                                        <ul>
                                            <li>E.g. If you last advanced Wild Card round, you have to export Divisional Round to get the schedule.</li>
                                        </ul>
                                    </li>
                                </ul>
                            </li>

                            <li><strong>Rosters</strong></li>
                        </ol>
                    </div>

                    <div class="col-sm-3">
                        <img class="img-responsive img-thumbnail" src="images/madden20app.png" />
                    </div>
                </div>
            </div>
        </div>

        <div class="panel panel-default">
            <div class="panel-heading">
                <h4 class="panel-title">CFM Stats - Sync Data
                </h4>
            </div>

            <div class="panel-body">
                <div class="row">
                    <div class="col-sm-9">
                        <h4>What is happening</h4>
                        <ul>
                            <li><strong>League Info</strong>
                                <ul>
                                    <li>Updates Team > Standings.</li>
                                </ul>
                            </li>
                            <li><strong>Rosters</strong>
                                <ul>
                                    <li>Updates Player > Ratings.</li>
                                </ul>
                            </li>
                            <li><strong>Schedule / Team Stats</strong>
                                <ul>
                                    <li>Updates Team > Stats and Schedule.</li>
                                </ul>
                            </li>

                            <li><strong>Player Stats</strong>
                                <ul>
                                    <li>Updates Player > Stats.</li>
                                </ul>
                            </li>
                        </ul>

                        <hr />

                        <h4>Sync data in this order</h4>

                        <ol type="1">
                            <li><strong>League Info</strong>
                                <ul>
                                    <li>Takes a second to update.</li>
                                </ul>
                            </li>
                            <li><strong>Rosters</strong>
                                <ul>
                                    <li>Takes ~2-3 minutes to update.</li>
                                </ul>
                            </li>
                            <li><strong>Schedule / Team Stats</strong>
                                <ul>
                                    <li>Takes ~15 seconds per week to update.</li>
                                </ul>
                            </li>
                            <li><strong>Player Stats</strong>
                                <ul>
                                    <li>Takes ~1-2 minutes to update.</li>
                                </ul>
                            </li>
                            <li><strong>Delete Export Database</strong>
                                <ul>
                                    <li>Takes ~15 seconds to delete.</li>
                                </ul>
                            </li>
                        </ol>


                    </div>
                    <div class="col-sm-3">
                        <img class="img-responsive img-thumbnail" src="images/cfmsync001.png" />
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>


<%--    <a href="https://itunes.apple.com/us/app/madden-nfl-18-companion/id1258904557?mt=8"><img src="images/iOSiTunes.png" /></a>
    <br />
    <a href="https://play.google.com/store/apps/details?id=com.ea.gp.madden18companionapp&hl=en"><img src="images/Android.png" /></a>

--%>

<%--    https://www.easports.com/madden-nfl/news/2016/madden-17-companion-app



    Madden 17 
    https://play.google.com/store/apps/details?id=com.ea.gp.maddenmca--%>