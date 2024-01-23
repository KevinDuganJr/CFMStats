<%@ Page Title="Box Score" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BoxScore.aspx.cs" Inherits="CFMStats.BoxScore" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .stat {
            font-family: Calibri;
            font-size: 1.0em;
            text-align: center;
        }

        .statName {
            font-family: Verdana;
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
        <div class="col-md-3 col-xs-12 col-sm-6">
            <div class="input-group input-group-sm mb-3">
                <span class="input-group-text bg-secondary" id="basic-addon1">Season</span>
                <asp:DropDownList ID="ddlSeason" runat="server" CssClass="form-control form-select form-select-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlWeek_SelectedIndexChanged"></asp:DropDownList>
            </div>
        </div>

        <div class="col-md-3 col-xs-12 col-sm-6">
            <div class="input-group input-group-sm mb-3">
                <span class="input-group-text bg-secondary" id="basic-addon2">Type</span>
                <asp:DropDownList ID="ddlSeasonType" runat="server" CssClass="form-control form-select form-select-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlWeek_SelectedIndexChanged">
                    <asp:ListItem Text="Regular" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Pre" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>

        <div class="col-md-3 col-xs-12 col-sm-6">
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

        <div class="col-md-3 col-xs-12 col-sm-6">
            <div class="input-group input-group-sm mb-3">
                <span class="input-group-text bg-secondary" id="basic-addon4">Team</span>
                <asp:DropDownList ID="ddlLeagueTeams" runat="server" CssClass="form-control form-select-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlWeek_SelectedIndexChanged"></asp:DropDownList>

                <button type="button" name="btnPrevStatus" value="Previous" class="btn btn-primary btn-sm" onclick="Previous(this, '<%= ddlLeagueTeams.ClientID %>');" id="btnPrevTeam">
                    <span class="fas fa-chevron-left"></span>
                </button>

                <button type="button" class="btn btn-primary btn-sm" name="btnNextStatus" value="Next" onclick="Next(this, '<%= ddlLeagueTeams.ClientID %>');" id="btnNextTeam">
                    <span class="fas fa-chevron-right"></span>
                </button>
            </div>
        </div>
    </div>

    <asp:Button ID="btnGetTeamStats" runat="server" Text="" OnClick="btnGetTeamStats_Click" Style="display: none;" />
    <asp:Button ID="btnGetPlayerStats" runat="server" Text="" OnClick="btnGetPlayerStats_Click" Style="display: none;" />

    <div class="container">

        <div id="boxscore" runat="server"></div>

        <!-- Nav tabs -->
        <ul class="nav nav-tabs" role="tablist">
            <li class="nav-item" role="presentation"><a class="nav-link active" href="#teamStats" onclick="getTeamStats();" aria-controls="teamStats" role="tab" data-bs-toggle="tab">Team Stats</a></li>
            <li class="nav-item" role="presentation"><a class="nav-link" href="#playerStats" onclick="getPlayerStats();" aria-controls="playerStats" role="tab" data-bs-toggle="tab">Player Stats</a></li>
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
                                    <label class="badge bg-warning">... LOADING ...</label>
                                    <label class="badge bg-danger">... LOADING ...</label>
                                    <label class="badge bg-success">... LOADING ...</label><br />
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
