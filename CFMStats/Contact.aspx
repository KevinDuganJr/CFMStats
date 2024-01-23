<%@ Page Title="Contact Us" Language="C#" AutoEventWireup="true"  MasterPageFile="~/Site.Master" CodeBehind="Contact.aspx.cs" Inherits="CFMStats.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <asp:Panel ID="pnlMessage" runat="server" Visible="false">
            <button type="button" class="close" data-dismiss="alert"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
            <asp:Label ID="lblAlert" runat="server"></asp:Label>
        </asp:Panel>
        
         <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>
                    <div style="text-align: center;">
                        <label class="badge bg-warning">... SENDING ...</label>
                        <label class="badge bg-danger">... SENDING ...</label>
                        <label class="badge bg-success">... SENDING ...</label><br/>
                    </div>
                    <br/>
                </ProgressTemplate>
            </asp:UpdateProgress>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
 
                    <asp:Panel ID="pnlSendMessage" runat="server" Visible="true">
                        <div class="mb-3">
                            <label for="exampleFormControlInput1" class="form-label">Your Email Address</label>
                            <asp:TextBox ID="txtFromEmail" runat="server" type="email" class="form-control" placeholder="name@example.com"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <label for="exampleFormControlTextarea1" class="form-label">Your Message</label>
                            <asp:TextBox ID="txtMessage" runat="server" class="form-control" TextMode="MultiLine"></asp:TextBox>
                        </div>
    
                        <br />
        
                        <asp:Button ID="btnSendMessage" runat="server" class="btn btn-success" Text="Send Message" OnClick="btnSendMessage_Click" />
                    </asp:Panel>
       
                    <asp:Panel ID="pnlMessageSent" runat="server" Visible="false">
                        Message Sent!
                    </asp:Panel>

                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSendMessage" EventName="Click"/>
                </Triggers>
            </asp:UpdatePanel>

    </div>
    
</asp:Content>