<%@ Page Title="Madden Leagues" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MaddenLeagues.aspx.cs" Inherits="CFMStats.MaddenLeagues" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:HiddenField ID="hdnLeagueID" runat="server" />
    <asp:HiddenField ID="hdnLeagueName" runat="server" />
    <asp:HiddenField ID="hdnExportID" runat="server" />

    <asp:Button ID="btnSetDefault" runat="server" Style="display: none;" OnClick="btnSetDefault_Click" />

    <div class="">
        <%--<div class="alert alert-primary" role="alert">
            Having troubles? Want something added? Feature request? Provide feedback via <a href="/Contact" class="alert-link">Contact Us</a>. 
        </div>--%>

        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div id="tblLeagues" runat="server" visible="true"></div>
            </ContentTemplate>
            <Triggers>
            </Triggers>
        </asp:UpdatePanel>
    </div>

    
    <script src="Scripts/moment.js/moment.js"></script>

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

        $('.date-time,input[type=datetime],input[type=datetime-local]').each(function (i, e) {
            var $element = $(e);
            var value;

            this.displayDateTimeDayOfWeekFormat = 'YYYY-MM-DD HH:mm A';

            if ($element.val()) {
                value = $element.val();

                var localDateTimeForInput = moment.utc(value).local().format(self.displayDateTimeDayOfWeekFormat);
                var formattedLocalDateTimeForInput = moment(localDateTimeForInput).format('MM-DD-YYYY @ h:mm A'); // YYYY-MM-DDTk:mm

                $element.val(formattedLocalDateTimeForInput);
            } else {
                value = $element.text();

                var localDateTimeForText = moment.utc(value).local().format(self.displayDateTimeDayOfWeekFormat);
                var formattedLocalDateTimeForText = moment(localDateTimeForText).format('MM-DD-YYYY @ h:mm A'); // YYYY-MM-DDTk:mm

                $element.text(formattedLocalDateTimeForText);
            }
        });

    </script>

</asp:Content>
