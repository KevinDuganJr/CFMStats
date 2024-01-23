<%@ Page Title="League Settings" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddEditLeague.aspx.cs" Inherits="CFMStats.Leagues.AddEditLeague" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <asp:Panel ID="pnlMessage" runat="server" Visible="false">
            <button type="button" class="close" data-dismiss="alert"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
            <asp:Label ID="lblAlert" runat="server"></asp:Label>
        </asp:Panel>

        <div class="row">
            <div class="col-sm-1">League Id
                <asp:TextBox ID="txtLeagueId" CssClass="form-control bg-light center" runat="server" ReadOnly></asp:TextBox>
            </div>
            <div class="col-sm-6">League Name
                <asp:TextBox ID="txtLeagueName" MaxLength="24" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-sm-3 mt-4">
                <asp:Button ID="btnSave" runat="server" Text="Save" Class="btn btn-success" OnClick="btnSave_Click" />
            </div>
        </div>
    </div>

</asp:Content>