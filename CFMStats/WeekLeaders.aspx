<%@ Page Title="Week Leaders" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WeekLeaders.aspx.cs" Inherits="CFMStats.WeekLeaders" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">


        <div class="row">


            <div class="col-xs-6 col-sm-3">
                <asp:DropDownList ID="ddlSeasonType" runat="server" CssClass="form-control input-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlWeek_SelectedIndexChanged">
                    <asp:ListItem Text="Regular" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Pre" Value="0"></asp:ListItem>
                </asp:DropDownList>
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

                    <strong>Passing</strong>
                    <div class="table-responsive table-bordered-curved">
                        <asp:PlaceHolder ID="phPassing" runat="server"></asp:PlaceHolder>
                    </div>

                    <br />

                    <strong>Rushing</strong>
                    <div class="table-responsive table-bordered-curved">
                        <asp:PlaceHolder ID="phRushing" runat="server"></asp:PlaceHolder>
                    </div>

                    <br />

                    <strong>Receiving</strong>
                    <div class="table-responsive table-bordered-curved">
                        <asp:PlaceHolder ID="phReceiving" runat="server"></asp:PlaceHolder>
                    </div>

                    <br />

                    <strong>Defense</strong>
                    <div class="table-responsive table-bordered-curved">
                        <asp:PlaceHolder ID="phDefense" runat="server"></asp:PlaceHolder>
                    </div>
                    
                    <br />

                    <strong>Kicking</strong>
                    <div class="table-responsive table-bordered-curved">
                        <asp:PlaceHolder ID="phKicking" runat="server"></asp:PlaceHolder>
                    </div>
                    
                    <br />

                    <strong>Punting</strong>
                    <div class="table-responsive table-bordered-curved">
                        <asp:PlaceHolder ID="phPunting" runat="server"></asp:PlaceHolder>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlSeason" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlWeek" EventName="SelectedIndexChanged" />
                    <%--<asp:AsyncPostBackTrigger ControlID="ddlSeasonType" EventName="SelectedIndexChanged" />--%>
                </Triggers>
            </asp:UpdatePanel>




        </div>


    </div>



    <%-- tablesorter --%>

    <link href="Content/tablesorter/theme.ice.min.css" rel="stylesheet" />
    <script src="Scripts/jquery.tablesorter.min.js"></script>
    <script src="Scripts/jquery.tablesorter.widgets.min.js"></script>

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
            $('table').tablesorter({
                theme: 'ice',
                widthFixed: true
            });
        }

        function Next(obj, obj1) {
            var index;


            var ddlNumbers = document.getElementById(obj1);
            var options = ddlNumbers.getElementsByTagName("option")
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
