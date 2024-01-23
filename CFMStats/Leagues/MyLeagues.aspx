<%@ Page Title="League Settings" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MyLeagues.aspx.cs" Inherits="CFMStats.Leagues.MyLeagues" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-sm-10"></div>
            <div class="col-sm-2 mb-3">
                <a href="AddEditLeague.aspx" class="btn btn-success btn-sm">Create New League</a>
            </div>
        </div>
        <div class="row">
            <div class="col">
                <div class="table-responsive table-bordered-curved">
                    <div id="tblLeagues" runat="server" visible="true"></div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>