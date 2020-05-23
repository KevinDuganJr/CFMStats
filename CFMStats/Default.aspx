<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CFMStats._Default" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <asp:HiddenField ID="hdnLeagueID" runat="server"/>
    <asp:HiddenField ID="hdnLeagueName" runat="server"/>
    <asp:HiddenField ID="hdnExportID" runat="server"/>

    <asp:Button ID="btnSetDefault" runat="server" Style="display: none;" OnClick="btnSetDefault_Click"/>
    
    <div class="row">

        <div class="col-sm-12">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <%--<div class="table-responsive table-bordered-curved">
                    </div>--%>
                    <div id="tblLeagues" runat="server" visible="true"></div>
                </ContentTemplate>
                <Triggers>
                </Triggers>
            </asp:UpdatePanel>
        </div>

    </div>

    <script>

        function setDefault(leagueId) {
            $("#<%= hdnLeagueID.ClientID %>").val(leagueId);
            document.getElementById('<%= btnSetDefault.ClientID %>').click();
        }

        function setCookie(name, value) {
            var expiry = new Date();
            expiry.setMinutes(expiry.getMinutes() + 60);
            document.cookie = name + "=" + (value) + "; path=/; expires=" + expiry.toGMTString();
        }

    </script>

</asp:Content>