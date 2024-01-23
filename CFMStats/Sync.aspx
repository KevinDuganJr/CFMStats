<%@ Page Title="Sync" Language="C#" MasterPageFile="~/Site.Master" Async="false" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Sync.aspx.cs" Inherits="CFMStats.Sync" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Button ID="btnUpdate" runat="server" Style="display: none;" OnClick="btnUpdate_Click" Text="Update?" />

    <div class="container">
        <div class="alert alert-primary">
            <div id="divLeague" class="text-center" runat="server" visible="true"></div>
        </div>

        <br />
        <br />

        <div class="input-group mb-4">
            <button type="button" class="btn btn-info" data-bs-toggle="tooltip" data-bs-placement="top" data-bs-title="Updates all the NFL teams.">
                <i class="fas fa-question-circle"></i>
            </button>
            <span class="form-control text-white bg-secondary" id="inputGroup1">League Info</span>
            <asp:Button ID="btnTeamStandingsUpdate" runat="server" CssClass="btn btn-success" OnClick="btnTeamStandingsUpdate_OnClick" Text="Update!" />
        </div>

        <div class="input-group mb-4">
            <button type="button" class="btn btn-info" data-bs-toggle="tooltip" data-bs-placement="top" data-bs-title="Updates all NFL players.">
                <i class="fas fa-question-circle"></i>
            </button>
            <span class="form-control text-white bg-secondary" id="inputGroup2">Rosters / Free Agents</span>
            <asp:Button ID="btnRosterUpdate" runat="server" CssClass="btn btn-success" OnClick="btnRosterUpdate_OnClick" Text="Update!" />
        </div>

        <div class="input-group mb-4">
            <button type="button" class="btn btn-info" data-bs-toggle="tooltip" data-bs-placement="top" data-bs-title="Updates all NFL team rankings, team stats and team schedule.">
                <i class="fas fa-question-circle"></i>
            </button>
            <span class="form-control text-white bg-secondary" id="inputGroup3">Schedule / Team Stats</span>
            <asp:Button ID="btnScheduleUpdate" runat="server" CssClass="btn btn-success" OnClick="btnScheduleUpdate_OnClick" Text="Update!" />
        </div>

        <div class="input-group">
            <button type="button" class="btn btn-info" data-bs-toggle="tooltip" data-bs-placement="top" data-bs-title="Updates stats for the week selected.">
                <i class="fas fa-question-circle"></i>
            </button>
            <span class="form-control text-white bg-secondary" id="inputGroup-sizing-sm">Player Stats</span>
        </div>
        <div class="input-group mb-4">
            <asp:DropDownList ID="ddlWeek" runat="server" CssClass="form-control form-select"></asp:DropDownList>
            <asp:Button ID="btnStatsUpdate" runat="server" class="btn btn-success btn-sm" OnClick="btnStatsUpdate_OnClick" Text="Update!" />
        </div>

        <br />
        <br />

        <div class="input-group mb-5 w-100">
            <button type="button" class="btn btn-info" data-bs-toggle="tooltip" data-bs-placement="top" data-bs-title="This will delete the staged data from the Madden Companion App.">
                <i class="fas fa-question-circle"></i>
            </button>
            <span class="form-control text-white bg-secondary" id="inputGroup0">Exported Data</span>
            <asp:Button ID="btnDeleteDatabase" runat="server" CssClass="btn btn-danger" OnClick="btnDeleteDatabase_OnClick" Text="Delete" />
        </div>



        <div class="row">
            <div class="col-xs-12">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">

                    <ContentTemplate>
                        <asp:HiddenField ID="hdnWeek" runat="server" />
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                            <ProgressTemplate>
                                <div style="text-align: center;" class="img-responsive">
                                    <i class="fas fa-spinner fa-spin fa-7x"></i>
                                </div>
                                <br />
                            </ProgressTemplate>
                        </asp:UpdateProgress>

                        <div class="table-responsive">
                            <div id="tblResults" runat="server" visible="true"></div>
                        </div>
                    </ContentTemplate>

                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnUpdate" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnStatsUpdate" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnRosterUpdate" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnScheduleUpdate" EventName="Click" />
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
    </div>
    <script>
        $(function () {
            $('[data-bs-toggle="tooltip"]').tooltip();
        });
    </script>
</asp:Content>


