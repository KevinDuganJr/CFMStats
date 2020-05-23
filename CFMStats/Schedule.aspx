<%@ Page Title="Schedule" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Schedule.aspx.cs" Inherits="CFMStats.Schedule" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .teamCity {
            font-size: 1.2em;
            color: #808080;
            text-transform: uppercase;
            font-family: 'Gill Sans', 'Gill Sans MT', Calibri, 'Trebuchet MS', sans-serif;
            line-height: 20px;
        }

        .teamName {
            font-size: 2em;
            color: #100101;
            font-weight: bold;
            text-transform: uppercase;
            font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif;
            line-height: 20px;
        }

        .teamUser {
            font-weight: bold;
            font-style: italic;
            line-height: 20px;
            color: #dcdcdc;
        }


        .awayLogo {
            max-width: 75px;
            max-height: 62.5px;
            width: auto;
            height: auto;
        }

        .homeLogo {
            max-width: 75px;
            max-height: 62.5px;
            width: auto;
            height: auto;
        }

        .teamWinner {
            font-weight: bold;
            text-decoration: none;
        }
    </style>

    <div class="container">
        <div class="row">

            <div class="col-xs-6 col-sm-3">
                <asp:DropDownList ID="ddlSeasonType" runat="server" CssClass="form-control input-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlWeek_SelectedIndexChanged">
                    <asp:ListItem Text="Regular" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Pre" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="col-xs-6 col-sm-6">
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
                        <button type="button" name="btnPrevStatus" value="Previous" class="btn btn-default" onclick="Previous(this,'<%= ddlWeek.ClientID %>');" id="btnPrevWeek">
                            <span class="glyphicon glyphicon-chevron-left"></span>
                        </button>

                        <button type="button" class="btn btn-default" name="btnNextStatus" value="Next" onclick="Next(this,'<%= ddlWeek.ClientID %>');" id="btnNextWeek">
                            <span class="glyphicon glyphicon-chevron-right"></span>
                        </button>
                    </span>

                </div>


            </div>

        </div>

        <br />

        <div class="row">

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
