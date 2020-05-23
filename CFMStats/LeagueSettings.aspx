<%@ Page Title="League Settings" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LeagueSettings.aspx.cs" Inherits="CFMStats.LeagueSettings" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Button ID="btnCreateLeague" runat="server" Style="display: none;" OnClick="btnCreateLeague_Click"/>
    <asp:Button ID="btnLoadLeagues" runat="server" Style="display: none;" OnClick="btnLoadLeagues_Click"/>
    
    <div class="row">

        <div class="col-sm-2"></div>

        <div class="col-sm-8">

            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="table-responsive table-bordered-curved">
                        <div id="tblLeagues" runat="server" visible="true"></div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnLoadLeagues" EventName="Click"/>
                </Triggers>
            </asp:UpdatePanel>
        </div>

        <div class="col-sm-2"></div>
    </div>

    <br/>

    <div class="row">
        <div class="col-sm-5"></div>
        <div class="col-sm-2">
            <button type="button" class="btn btn-success" data-toggle="modal" data-target="#myModalCreate">Create New League</button>
        </div>
        <div class="col-sm-5"></div>
    </div>


    <div class="modal fade" id="myModalCreate" tabindex="-1" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <h3 class="modal-title">Create New League</h3>
                </div>
                <div class="modal-body">

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            League Name
                            <asp:TextBox ID="txtLeagueName" CssClass="form-control" runat="server"></asp:TextBox>

                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnCreateLeague" EventName="Click"/>
                        </Triggers>
                    </asp:UpdatePanel>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                    <button type="button" class="btn btn-success" data-dismiss="modal" onclick="CreateLeague();">Create</button>
                </div>
            </div>
        </div>
    </div>


    <%-- tablesorter --%>

    <link href="Content/tablesorter/theme.ice.min.css" rel="stylesheet"/>
    <script src="Scripts/jquery.tablesorter.min.js"></script>
    <script src="Scripts/jquery.tablesorter.widgets.min.js"></script>


    <script>
        function CreateLeague() {
            document.getElementById('<%= btnCreateLeague.ClientID %>').click();
        }

        function displayAlert(message) {
            alert(message);
        }



        //Initial bind
        $(document).ready(function() {
            BindControlEvents();
        });

        //Re-bind for callbacks
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        prm.add_endRequest(function() {
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