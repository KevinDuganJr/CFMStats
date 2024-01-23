<%@ Page Title="Week Leaders" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WeekLeaders.aspx.cs" Inherits="CFMStats.WeekLeaders" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
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
                        <label class="badge bg-success">... LOADING ...</label><br/>
                    </div>
                    <br/>
                </ProgressTemplate>
            </asp:UpdateProgress>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

                    <strong>Passing</strong>
                    <div class="table-responsive table-bordered-curved">
                        <asp:PlaceHolder ID="phPassing" runat="server"></asp:PlaceHolder>
                    </div>

                    <br/>

                    <strong>Rushing</strong>
                    <div class="table-responsive table-bordered-curved">
                        <asp:PlaceHolder ID="phRushing" runat="server"></asp:PlaceHolder>
                    </div>

                    <br/>

                    <strong>Receiving</strong>
                    <div class="table-responsive table-bordered-curved">
                        <asp:PlaceHolder ID="phReceiving" runat="server"></asp:PlaceHolder>
                    </div>

                    <br/>

                    <strong>Defense</strong>
                    <div class="table-responsive table-bordered-curved">
                        <asp:PlaceHolder ID="phDefense" runat="server"></asp:PlaceHolder>
                    </div>

                    <br/>

                    <strong>Kicking</strong>
                    <div class="table-responsive table-bordered-curved">
                        <asp:PlaceHolder ID="phKicking" runat="server"></asp:PlaceHolder>
                    </div>

                    <br/>

                    <strong>Punting</strong>
                    <div class="table-responsive table-bordered-curved">
                        <asp:PlaceHolder ID="phPunting" runat="server"></asp:PlaceHolder>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlSeason" EventName="SelectedIndexChanged"/>
                    <asp:AsyncPostBackTrigger ControlID="ddlWeek" EventName="SelectedIndexChanged"/>
                    <%--<asp:AsyncPostBackTrigger ControlID="ddlSeasonType" EventName="SelectedIndexChanged" />--%>
                </Triggers>
            </asp:UpdatePanel>


        </div>


    </div>


    <%-- tablesorter --%>

    <link href="Content/tablesorter/theme.ice.min.css" rel="stylesheet"/>
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