<%@ Page Title="Schedule" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Schedule.aspx.cs" Inherits="CFMStats.Schedule" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .teamCity {
            font-family: 'Gill Sans', 'Gill Sans MT', Calibri, 'Trebuchet MS', sans-serif;
            font-size: 1.2em;
            line-height: 20px;
            /*color: #808080;*/
            text-transform: uppercase;
            text-shadow: 1px 1px #938585;
        }

        .teamName {
            font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif;
            font-size: 2em;
            /* color: #100101;*/
            font-weight: bold;
            line-height: 20px;
            text-transform: uppercase;
            text-shadow: 1px 1px #938585;
        }

        .teamUser {
            color: #dcdcdc;
            font-style: italic;
            font-weight: bold;
            line-height: 20px;
        }


        .awayLogo {
            height: auto;
            max-height: 62.5px;
            max-width: 75px;
            width: auto;
        }

        .homeLogo {
            height: auto;
            max-height: 62.5px;
            max-width: 75px;
            width: auto;
        }

        .teamWinner {
            font-weight: bold;
            text-decoration: none;
        }

        .teamNickname {
            font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif;
            font-size: 2em;
            font-weight: bold;
            line-height: 20px;
            text-transform: uppercase;
            text-shadow: 1px 1px #938585;
        }

        .teamRecord {
            font-family: 'Gill Sans', 'Gill Sans MT', Calibri, 'Trebuchet MS', sans-serif;
            font-size: 1.2em;
            line-height: 20px;
            text-transform: uppercase;
            text-shadow: 1px 1px #938585;
        }

        .score {
            font-family: Consolas;
            font-size: 1.75em;
            color: #dcdcdc;
            font-weight: bold;
            line-height: 20px;
        }

        .green_background {
            background-image: url(/images/team/large/19.png);
            background-size: 100% 100%;
        }
    </style>

    
    <%--<link rel="stylesheet" href="https://mymadden.com/css/app.css">--%>
  <%--  <div class="row alert alert-schedule">

        <div class="row">
            <div class="col-lg-5 justify-content-end">
                <table>
                    <tr>
                        <td><span class="teamRecord">(8-4)</span></td>
                        <td><span class="teamNickname">49ers</span></td>
                        <td>
                            <img class="img-fluid awayLogo" src="/images/team/large/14.png"></td>
                        <td><span class="score">13</span></td>
                    </tr>
                </table>
            </div>
            <div class="col-lg-2 justify-content-center">
                <h3>@</h3>
            </div>

            <div class="col-lg-5">
                <td><span class="score">63</span></td>
                <td>
                    <img class="img-fluid homeLogo" src="/images/team/large/19.png"></td>
                <td><span class="teamNickname">Packers</span></td>
                <td><span class="teamRecord">(12-0)</span></td>
            </div>

        </div>





        <div class="card-header w-full text-center flex flex-row z-0 font-medium glossy" style="align-items: center;">
            <div class="flex flex-shrink">
                <!--v-if-->
            </div>
            <div class="flex flex-grow justify-center" style=""><span class="w-full"><a href="https://mymadden.com/lg/shnrz/games/129" class="w-full">GAMECENTER </a></span></div>
        </div>--%>



        <%-- 
        <div class="row">
            <div class="col">
                <table>
                    <tr>
                        <td rowspan="2">
                           <sup>78</sup></td>
                        <td>
                            <span style="color: #EEAD1E;" class="teamName">PACKERS</span> <small><i>(11 - 0)</i></small><br />
                        </td>
                        <td rowspan="2" class="teamScore">42</td>
                    </tr>

                </table>
            </div>

            <div class="col">
                @
            </div>

            <div class="col">
                <table>
                    <tr>
                        <td rowspan="2">
                            <img class="img-fluid img-thumbnail homeLogo" src="/images/team/large/14.png"><sup>97</sup></td>
                        <td>

                            <span style="color: #AF925D;" class="teamName">49ers</span> <small><i>(4 - 8)</i></small><br />
                        </td>
                        <td rowspan="2" class="teamScore">0</td>
                    </tr>

                </table>
            </div>


        </div>--%>
    <%--</div>
    <div class="img-fluid" style="background: url(/images/team/large/14.png); background-size: 100% 100%;">Where is Logo?</div>
    
    <div class="card">
        <div class="card-header">
            GAME CENTER BOX SCORE
        </div>
        <div class="card-body">
            <div class="d-flex row w-100">
                <div class="w-45 img-fluid" style="background-image: url(/images/team/large/14.png); background-size: 100% 100%; background-color: #AF925D;">
                    <table class="table-auto w-100">
                    </table>
                    Hello<br /><br /><br /><br /><br /><br />
                </div>
                <div class="w-10 h-auto">&nbsp; </div>
                <div class="w-45" style="background-image: url(/images/team/large/19.png); background-size: 100% 100%; background-color: #EEAD1E;">
                    Hello
                </div>
            </div>
        </div>
    </div>

    <div class="panel flex flex-col w-full overflow-y-visible rounded my-2 mx-auto p-1 shadow-sm bg-gray-200 dark:bg-gray-800 game" style=""><div class="panel-header w-full text-center flex flex-row z-0 font-medium glossy" style="align-items: center;"><div class="flex flex-shrink"><!--v-if--></div><div class="flex flex-grow justify-center" style=""> <span class="w-full"><a href="https://mymadden.com/lg/shnrz/games/130" class="w-full"> GAMECENTER </a></span></div></div><div class="panel-body flex overflow-x-auto overflow-y-visible w-full z-10 bg-gray-100 dark:bg-gray-900 text-white" style=""><div class="flex flex-row w-full text-center text-white text-shadow font-medium" style="background: linear-gradient(to right, rgb(166, 174, 176), rgb(166, 174, 176) 45%, rgb(227, 24, 55) 55%, rgb(227, 24, 55));"><div class="flex flex-row w-full"><div class="w-[45%]" style="background: url(&quot;/storage/teamlogos/256/22.png&quot;) center center / cover rgb(166, 174, 176); background-blend-mode: soft-light;"><table class="table-auto w-full"><tbody><tr><td class="w-full team-name text-lg text-shadow md:text-2xl lg:text-4xl"><a href="https://mymadden.com/lg/shnrz/teams/LV"> Raiders </a></td></tr><tr><td class="h-6 sm:h-12 md:h-28 lg:h-40 score text-4xl sm:text-5xl md:text-6xl lg:text-7xl xl:text-8xl font-bold my-auto text-right"> 22 </td></tr><tr><td class="text-shadow text-lg font-medium"> (3-3-0) </td></tr></tbody></table></div><div class="w-[10%] h-auto"> &nbsp; </div><div class="w-[45%]" style="background: url(&quot;/storage/teamlogos/256/8.png&quot;) center center / cover rgb(227, 24, 55); background-blend-mode: soft-light;"><table class="table-auto w-full"><tbody><tr><td class="w-full team-name text-lg text-shadow sm:text-lg md:text-2xl lg:text-4xl"><a href="https://mymadden.com/lg/shnrz/teams/KC"> Chiefs </a></td></tr><tr><td class="h-6 sm:h-12 md:h-28 lg:h-40 score text-4xl sm:text-5xl md:text-6xl lg:text-7xl xl:text-8xl font-bold my-auto text-left"> 35 </td></tr><tr><td class="text-shadow text-lg font-medium"> (4-2-0) </td></tr></tbody></table></div></div></div></div><div class="panel-footer flex w-full text-center" style=""></div></div>
    --%>
    <div class="container">
        <div class="row">
            <div class="col-xs-6 col-sm-4">
                <div class="input-group input-group-sm mb-3">
                    <span class="input-group-text bg-secondary" id="basic-addon1">Season</span>
                    <asp:DropDownList ID="ddlSeason" runat="server" CssClass="form-control form-select form-select-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlWeek_SelectedIndexChanged"></asp:DropDownList>
                </div>
            </div>

            <div class="col-xs-6 col-sm-4">
                <div class="input-group input-group-sm mb-3">
                    <span class="input-group-text bg-secondary" id="basic-addon2">Type</span>
                    <asp:DropDownList ID="ddlSeasonType" runat="server" CssClass="form-control form-select form-select-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlWeek_SelectedIndexChanged">
                        <asp:ListItem Text="Regular" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Pre" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>

            <div class="col-xs-12 col-sm-4">
                <div class="input-group input-group-sm mb-3">
                    <span class="input-group-text bg-secondary" id="basic-addon3">Week</span>

                    <asp:DropDownList ID="ddlWeek" runat="server" CssClass="form-control form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlWeek_SelectedIndexChanged"></asp:DropDownList>

                    <button type="button" name="btnPrevStatus" value="Previous" class="btn btn-primary" onclick="Previous(this,'<%= ddlWeek.ClientID %>');" id="btnPrevWeek">
                        <i class="fas fa-chevron-left"></i>
                    </button>

                    <button type="button" class="btn btn-primary" name="btnNextStatus" value="Next" onclick="Next(this,'<%= ddlWeek.ClientID %>');" id="btnNextWeek">
                        <i class="fas fa-chevron-right"></i>
                    </button>
                </div>
            </div>
        </div>

        <div class="row">
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>
                    <div style="text-align: center;">
                        <label class="badge bg-warning">... LOADING ...</label>
                        <label class="badge bg-danger">... LOADING ...</label>
                        <label class="badge bg-success">... LOADING ...</label><br />
                    </div>
                    <br />
                </ProgressTemplate>
            </asp:UpdateProgress>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div id="tableSchedule" runat="server" visible="true"></div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlSeason" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlWeek" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>

        </div>
    </div>



    <%--        
        <div class="col">
            <a href="/BoxScore?id=788267008&amp;leagueId=1136&amp;season=2026&amp;week=8&amp;type=1">
                <img class="img-fluid img-thumbnail bg-secondary awayLogo" src="/images/team/large/14.png"></a>
        </div>

        <div class="col d-none d-sm-block">
            <div style="color: #AA0000;" class="teamCity">San Francisco</div>
            <div style="color: #AF925D;" class="teamName">49ers</div>
            <div class="teamUser">CPU</div>
        </div>
        <div class="col">
            <div class="teamScore">0</div>
        </div>
        
        <div class="col">
            <a href="/BoxScore?id=788267033&amp;leagueId=1136&amp;season=2026&amp;week=8&amp;type=1">
                <img class="img-fluid img-thumbnail bg-secondary homeLogo" src="/images/team/large/19.png"></a>
        </div>
        <div class="col d-none d-sm-block">
            <div style="color: #1C2D25;" class="teamCity">Green Bay</div>
            <div style="color: #EEAD1E;" class="teamName">Packers</div>
            <div class="teamUser">SlickVisionZero</div>
        </div>
        <div class="col">
            <div class="teamScore">0</div>
        </div>
    </div>--%>

    <script>


        //Initial bind
        $(document).ready(function () {
            BindControlEvents();
        });

        //Re-bind for callbacks
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        prm.add_endRequest(function () {
            BindControlEvents();
        });


        function BindControlEvents() {
            $(function () {
                $('[data-toggle="tooltip"]').tooltip();
            });
        }

        function Next(obj, obj1) {
            var index;


            var ddlNumbers = document.getElementById(obj1);
            var options = ddlNumbers.getElementsByTagName("option");
            for (var i = 0; i < options.length; i++) {
                if (options[i].selected) {
                    index = i;
                }
            }
            index = index + 1;
            if (index >= ddlNumbers.length) {

            }
            else {
                ddlNumbers.value = ddlNumbers[index].value;
                document.getElementById(obj1).onchange();
            }
            return false;
        }

        //http://www.aspforums.net/Threads/143568/Navigate-through-DropDownList-Items-using-Next-Previous-buttons-using-JavaScript-and-jQuery/

        function Previous(obj, obj1) {
            var index;
            var ddlNumbers = document.getElementById(obj1);



            var options = ddlNumbers.getElementsByTagName("option");
            for (var i = 0; i < options.length; i++) {
                if (options[i].selected) {
                    index = i;
                }
            }
            index = index - 1;
            if (index <= -1) {
            }
            else {
                ddlNumbers.value = ddlNumbers[index].value;
                document.getElementById(obj1).onchange();
            }
            return false;
        }

    </script>

</asp:Content>
