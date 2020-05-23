<%@ Page Title="Player Records" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="PlayerRecords.aspx.cs" Inherits="CFMStats.PlayerRecords" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <div class="row">

        <div class="col-xs-6 col-sm-3">
            <div class="form-horizontal" role="form">
                <div class="form-group form-group-sm">
                    <label class="control-label col-sm-3">Player</label>
                    <div class="col-sm-9">
                        <asp:DropDownList ID="ddlStatSelector" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlStatSelector_OnSelectedIndexChanged">
                            <asp:ListItem Text="Passing Stats" Value="passing"></asp:ListItem>
                            <asp:ListItem Text="Rushing Stats" Value="rushing"></asp:ListItem>
                            <asp:ListItem Text="Receiving Stats" Value="receiving"></asp:ListItem>
                            <asp:ListItem Text="Defense Stats" Value="defense"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-xs-6 col-sm-3">
            <div class="form-horizontal" role="form">
                <div class="form-group form-group-sm">
                    <label class="control-label col-sm-3">Duration</label>
                    <div class="col-sm-9">
                        <asp:DropDownList ID="ddlDuration" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDuration_OnSelectedIndexChanged">
                            <asp:ListItem Text="Game" Value="game"></asp:ListItem>
                            <asp:ListItem Text="Season" Value="season"></asp:ListItem>
                            <asp:ListItem Text="Career" Value="career"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>

    </div>


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


            <asp:PlaceHolder ID="phStatHolder" runat="server"></asp:PlaceHolder>


        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlStatSelector" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlDuration" EventName="SelectedIndexChanged" />
        </Triggers>

    </asp:UpdatePanel>



    <%-- tablesorter --%>
    <link href="Content/tablesorter/theme.ice.min.css" rel="stylesheet" />
    <script src="Scripts/jquery.tablesorter.min.js"></script>

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



    </script>
</asp:Content>
