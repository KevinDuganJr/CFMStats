<%@ Page Title="Box Score" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BoxScore.aspx.cs" Inherits="CFMStats.BoxScore" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .stat {
            font-family: cursive;
            font-size: 1.0em;
            text-align: center;
        }

        .statName {
            font-family: fantasy;
            font-size: 1.2em;
            text-align: center;
        }
    </style>

    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>

            <asp:Panel ID="pnlMessage" runat="server">
                <div id="alertMessage" runat="server">
                    <asp:Label ID="lblAlert" runat="server"></asp:Label>
                </div>
            </asp:Panel>

        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>


    <div class="row">

        <div class="col-xs-6 col-sm-3">
            <asp:DropDownList ID="ddlSeasonType" runat="server" CssClass="form-control input-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlWeek_SelectedIndexChanged"></asp:DropDownList>
        </div>

        <div class="col-xs-6 col-sm-3">
            <asp:DropDownList ID="ddlSeason" runat="server" CssClass="form-control input-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlWeek_SelectedIndexChanged"></asp:DropDownList>
        </div>

        <div class="col-xs-12 col-sm-3">
            <div class="visible-xs">
                <br />
            </div>
            <div class="input-group input-group-sm">
                <asp:DropDownList ID="ddlWeek" runat="server" CssClass="form-control input-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlWeek_SelectedIndexChanged">
                    <asp:ListItem Text="1" Value="0"></asp:ListItem>
                    <asp:ListItem Text="2" Value="1"></asp:ListItem>
                    <asp:ListItem Text="3" Value="2"></asp:ListItem>
                    <asp:ListItem Text="4" Value="3"></asp:ListItem>
                    <asp:ListItem Text="5" Value="4"></asp:ListItem>
                    <asp:ListItem Text="6" Value="5"></asp:ListItem>
                    <asp:ListItem Text="7" Value="6"></asp:ListItem>
                    <asp:ListItem Text="8" Value="7"></asp:ListItem>
                    <asp:ListItem Text="9" Value="8"></asp:ListItem>
                    <asp:ListItem Text="10" Value="9"></asp:ListItem>
                    <asp:ListItem Text="11" Value="10"></asp:ListItem>
                    <asp:ListItem Text="12" Value="11"></asp:ListItem>
                    <asp:ListItem Text="13" Value="12"></asp:ListItem>
                    <asp:ListItem Text="14" Value="13"></asp:ListItem>
                    <asp:ListItem Text="15" Value="14"></asp:ListItem>
                    <asp:ListItem Text="16" Value="15"></asp:ListItem>
                    <asp:ListItem Text="17" Value="16"></asp:ListItem>
                    <asp:ListItem Text="Wild Card" Value="17"></asp:ListItem>
                    <asp:ListItem Text="Divisional" Value="18"></asp:ListItem>
                    <asp:ListItem Text="Conference" Value="19"></asp:ListItem>
                    <asp:ListItem Text="Super Bowl" Value="21"></asp:ListItem>
                </asp:DropDownList>

                <span class="input-group-btn">
                    <button type="button" name="btnPrevStatus" value="Previous" class="btn btn-default" onclick="Previous(this, '<%= ddlWeek.ClientID %>');" id="btnPrevWeek">
                        <span class="glyphicon glyphicon-chevron-left"></span>
                    </button>

                    <button type="button" class="btn btn-default" name="btnNextStatus" value="Next" onclick="Next(this, '<%= ddlWeek.ClientID %>');" id="btnNextWeek">
                        <span class="glyphicon glyphicon-chevron-right"></span>
                    </button>
                </span>

            </div>
        </div>


        <div class="col-xs-12 col-sm-3">
            <div class="visible-xs">
                <br />
            </div>
            <div class="input-group">
                <asp:DropDownList ID="ddlLeagueTeams" runat="server" CssClass="form-control input-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlWeek_SelectedIndexChanged"></asp:DropDownList>

                <span class="input-group-btn">
                    <button type="button" name="btnPrevStatus" value="Previous" class="btn btn-default btn-sm" onclick="Previous(this, '<%= ddlLeagueTeams.ClientID %>');" id="btnPrevTeam">
                        <span class="glyphicon glyphicon-chevron-left"></span>
                    </button>

                    <button type="button" class="btn btn-default btn-sm" name="btnNextStatus" value="Next" onclick="Next(this, '<%= ddlLeagueTeams.ClientID %>');" id="btnNextTeam">
                        <span class="glyphicon glyphicon-chevron-right"></span>
                    </button>
                </span>
            </div>
        </div>
    </div>

    <asp:Button ID="btnGetTeamStats" runat="server" Text="" OnClick="btnGetTeamStats_Click" Style="display: none;" />
    <asp:Button ID="btnGetPlayerStats" runat="server" Text="" OnClick="btnGetPlayerStats_Click" Style="display: none;" />

    <div class="container">

        <div id="boxscore" runat="server"></div>
        
        <div class="row">
            <div class="col-md-4">
                
            </div>
            <div class="col-md-8">
                      
            </div>

        </div>
        
        

        <!-- Nav tabs -->
        <ul class="nav nav-tabs" role="tablist">
            <li role="presentation" class="active"><a href="#teamStats" onclick="getTeamStats();" aria-controls="teamStats" role="tab" data-toggle="tab">Team Stats</a></li>
            <li role="presentation"><a href="#playerStats" onclick="getPlayerStats();" aria-controls="playerStats" role="tab" data-toggle="tab">Player Stats</a></li>
        </ul>

        <!-- Tab panes -->
        <div class="tab-content">
            <div role="tabpanel" class="tab-pane active" id="teamStats">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div id="tableTeamStats" runat="server"></div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnGetTeamStats" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="ddlSeasonType" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlLeagueTeams" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlSeason" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlWeek" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>

            </div>


            <div role="tabpanel" class="tab-pane" id="playerStats">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>

                        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                            <ProgressTemplate>
                                <div style="text-align: center;">
                                    <label class="label label-warning">... LOADING ...</label>
                                    <label class="label label-danger">... LOADING ...</label>
                                    <label class="label label-success">... LOADING ...</label><br />
                                </div>
                                <br />
                            </ProgressTemplate>
                        </asp:UpdateProgress>


                                     <div class="row">
                            <div class="col-sm-6">
                                <strong><small>PASSING</small></strong>
                                <div class="table-responsive">
                                    <asp:PlaceHolder ID="phAwayPassing" runat="server"></asp:PlaceHolder>
                                </div>
                            </div>

                            <div class="col-sm-6">
                                <strong><small>PASSING</small></strong>
                                <div class="table-responsive">
                                    <asp:PlaceHolder ID="phHomePassing" runat="server"></asp:PlaceHolder>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-6">
                                <strong><small>RUSHING</small></strong>
                                <div class="table-responsive">
                                    <asp:PlaceHolder ID="phAwayRushing" runat="server"></asp:PlaceHolder>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <strong><small>RUSHING</small></strong>
                                <div class="table-responsive">
                                    <asp:PlaceHolder ID="phHomeRushing" runat="server"></asp:PlaceHolder>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-6">
                                <strong><small>RECEIVING</small></strong>
                                <div class="table-responsive">
                                    <asp:PlaceHolder ID="phAwayReceiving" runat="server"></asp:PlaceHolder>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <strong><small>RECEIVING</small></strong>
                                <div class="table-responsive">
                                    <asp:PlaceHolder ID="phHomeReceiving" runat="server"></asp:PlaceHolder>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-6">
                                <strong><small>DEFENSE</small></strong>
                                <div class="table-responsive">
                                    <asp:PlaceHolder ID="phAwayDefense" runat="server"></asp:PlaceHolder>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <strong><small>DEFENSE</small></strong>
                                <div class="table-responsive">
                                    <asp:PlaceHolder ID="phHomeDefense" runat="server"></asp:PlaceHolder>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-6">
                                <strong><small>KICKING</small></strong>
                                <div class="table-responsive">
                                    <asp:PlaceHolder ID="phAwayKicking" runat="server"></asp:PlaceHolder>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <strong><small>KICKING</small></strong>
                                <div class="table-responsive">
                                    <asp:PlaceHolder ID="phHomeKicking" runat="server"></asp:PlaceHolder>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-6">
                                <strong><small>PUNTING</small></strong>
                                <div class="table-responsive">
                                    <asp:PlaceHolder ID="phAwayPunting" runat="server"></asp:PlaceHolder>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <strong><small>PUNTING</small></strong>
                                <div class="table-responsive">
                                    <asp:PlaceHolder ID="phHomePunting" runat="server"></asp:PlaceHolder>
                                </div>
                            </div>
                        </div>
     

                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnGetPlayerStats" EventName="Click" />
                    </Triggers>

                </asp:UpdatePanel>
            </div>
        </div>

    </div>
    <%-- tablesorter --%>

    <link href="Content/tablesorter/theme.ice.min.css" rel="stylesheet" />
    <script src="Scripts/jquery.tablesorter.min.js"></script>
    <script src="Scripts/jquery.tablesorter.widgets.min.js"></script>

    <script>

        function getTeamStats() {
            document.getElementById('<%= btnGetTeamStats.ClientID%>').click();
        }

        function getPlayerStats() {
            document.getElementById('<%= btnGetPlayerStats.ClientID%>').click();
        }

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
                $('table').tablesorter({
                    theme: 'ice',
                    widthFixed: true
                });
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



            var options = ddlNumbers.getElementsByTagName("option")
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
