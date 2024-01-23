<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LeagueHome.aspx.cs" Inherits="CFMStats.LeagueHome" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    
    <!-- Nav tabs -->
    <ul class="nav nav-tabs" role="tablist">
        <li class="nav-item" role="presentation"><a class="nav-link active"href="#season" onclick="getStats();" aria-controls="season" role="tab" data-bs-toggle="tab">Home</a></li>
        <li class="nav-item" role="presentation"><a class="nav-link" href="#games" onclick="getWeekStats();" aria-controls="games" role="tab" data-bs-toggle="tab">Schedule</a></li>
        <li class="nav-item" role="presentation"><a class="nav-link" href="#abilities" onclick="getAbilities();" aria-controls="abilities"  role="tab" data-bs-toggle="tab">Week Leaders</a></li>
        <li class="nav-item" role="presentation"><a class="nav-link" href="#settings" onclick="getTraits();" aria-controls="settings" role="tab" data-bs-toggle="tab">Rankings</a></li>
        <li class="nav-item" role="presentation"><a class="nav-link" href="#stats" onclick="getTraits();" aria-controls="stats" role="tab" data-bs-toggle="tab">Stats</a></li>
    </ul>

    <!-- Tab panes -->
    <div class="tab-content">
        <div role="tabpanel" class="tab-pane active" id="season">
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

                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>

                        <div class="col-sm-12">
                            <br />
                            <asp:PlaceHolder ID="phPassing" runat="server"></asp:PlaceHolder>

                            <asp:PlaceHolder ID="phRushing" runat="server"></asp:PlaceHolder>

                            <asp:PlaceHolder ID="phReceiving" runat="server"></asp:PlaceHolder>

                            <asp:PlaceHolder ID="phDefense" runat="server"></asp:PlaceHolder>

                            <asp:PlaceHolder ID="phKicking" runat="server"></asp:PlaceHolder>

                            <asp:PlaceHolder ID="phPunting" runat="server"></asp:PlaceHolder>
                        </div>


                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
                </asp:UpdatePanel>

            </div>


        </div>
        
   

        <div role="tabpanel" class="tab-pane" id="games">
            <div class="row">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server">
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

                        <div class="col-sm-12">
                            <br />
                            <asp:PlaceHolder ID="phPassingWeek" runat="server"></asp:PlaceHolder>

                            <asp:PlaceHolder ID="phRushingWeek" runat="server"></asp:PlaceHolder>

                            <asp:PlaceHolder ID="phReceivingWeek" runat="server"></asp:PlaceHolder>

                            <asp:PlaceHolder ID="phDefenseWeek" runat="server"></asp:PlaceHolder>

                            <asp:PlaceHolder ID="phKickingWeek" runat="server"></asp:PlaceHolder>

                            <asp:PlaceHolder ID="phPuntingWeek" runat="server"></asp:PlaceHolder>
                        </div>

                    </ContentTemplate>
                    <Triggers>
                        <%--<asp:AsyncPostBackTrigger ControlID="btnGetWeekStats" EventName="Click" />--%>
                    </Triggers>
                </asp:UpdatePanel>

            </div>
        </div>
        <div role="tabpanel" class="tab-pane" id="abilities">
            <asp:UpdatePanel ID="UpdatePanelAbilities" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-sm-4">
                            <div id="divAbilities" runat="server"></div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                </Triggers>
            </asp:UpdatePanel>
        </div>

        <div role="tabpanel" class="tab-pane" id="settings">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-sm-4">
                            <div id="tableTraits" runat="server"></div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>




</asp:Content>
