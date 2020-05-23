<%@ Page Title="Sync" Language="C#" MasterPageFile="~/Site.Master" Async="false" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Sync.aspx.cs" Inherits="CFMStats.Sync" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Button ID="btnUpdate" runat="server" Style="display: none;" OnClick="btnUpdate_Click" Text="Update?"/>


    <div class="row">
        <div class="col-xs-3"></div>
        <div class="col-xs-6">
            <div id="divLeague" class="leagueName" runat="server" visible="true"></div>
        </div>
        <div class="col-xs-3"></div>
    </div>
    <br/>

    <div class="row">
        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
            <div class="form-horizontal" role="form">
                <div class="form-group">
                    <label class="control-label col-xs-6">Export Database:</label>
                    <div class="col-xs-6">
                        <asp:Button ID="btnDeleteDatabase" runat="server" CssClass="btn btn-danger btn-sm" OnClick="btnDeleteDatabase_OnClick" Text="Delete"/>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
            <div class="form-horizontal" role="form">
                <div class="form-group">
                    <label class="control-label col-xs-6">League Info:</label>
                    <div class="col-xs-6">
                        <asp:Button ID="btnTeamStandingsUpdate" runat="server" CssClass="btn btn-success btn-sm" OnClick="btnTeamStandingsUpdate_OnClick" Text="Update!"/>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
            <div class="form-horizontal" role="form">
                <div class="form-group">
                    <label class="control-label col-xs-6">Rosters:</label>
                    <div class="col-xs-6">
                        <asp:Button ID="btnRosterUpdate" runat="server" CssClass="btn btn-success btn-sm" OnClick="btnRosterUpdate_OnClick" Text="Update!"/>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
            <div class="form-horizontal" role="form">
                <div class="form-group">
                    <label class="control-label col-xs-6">Schedule / Team Stats:</label>
                    <div class="col-xs-6">
                        <asp:Button ID="btnScheduleUpdate" runat="server" CssClass="btn btn-success btn-sm" OnClick="btnScheduleUpdate_OnClick" Text="Update!"/>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
            <div class="form-horizontal" role="form">
                <div class="form-group">
                    <label class="control-label col-sm-4">Player Stats:</label>
                    <div class="col-sm-8">
                        <div class="input-group">
                            <asp:DropDownList ID="ddlWeek" runat="server" CssClass="form-control input-sm"></asp:DropDownList>

                            <span class="input-group-btn">
                                <asp:Button ID="btnStatsUpdate" runat="server" class="btn btn-success btn-sm" OnClick="btnStatsUpdate_OnClick" Text="Update!"/>
                            </span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <br/>

    <div class="row">
        <div class="col-xs-12">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">

                <ContentTemplate>
                    <asp:HiddenField ID="hdnWeek" runat="server"/>
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                        <ProgressTemplate>
                            <div style="text-align: center;" class="img-responsive">
                                <img src="images/uploading.gif" alt=""/>
                            </div>
                            <br/>
                        </ProgressTemplate>
                    </asp:UpdateProgress>

                    <div class="table-responsive">
                        <div id="tblResults" runat="server" visible="true"></div>
                    </div>
                </ContentTemplate>

                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnUpdate" EventName="Click"/>
                    <asp:AsyncPostBackTrigger ControlID="btnStatsUpdate" EventName="Click"/>
                    <asp:AsyncPostBackTrigger ControlID="btnRosterUpdate" EventName="Click"/>
                    <asp:AsyncPostBackTrigger ControlID="btnScheduleUpdate" EventName="Click"/>
                </Triggers>

            </asp:UpdatePanel>
        </div>

    </div>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-xs-6">
                    <asp:PlaceHolder ID="phUrlHolder" runat="server"></asp:PlaceHolder>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>