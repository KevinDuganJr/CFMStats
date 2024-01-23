<%@ Page Title="Change Log" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChangeLog.aspx.cs" Inherits="CFMStats.ChangeLog" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .form-horizontal .control-label {
            display: inline-block;
            float: none;
            vertical-align: middle;
        }

        .form-horizontal .controls {
            display: inline-block;
            margin-left: 20px;
        }
    </style>


    <div class="container">
        <h2>v2023.8.16.1</h2>
        <ul>
            <li><span class="badge bg-info">Enhancement</span> Added Season/Stage/Week to League Listing</li>
            <li><span class="badge bg-danger">Bug</span> Sort by Height Works</li>
            <li><span class="badge bg-info">Enhancement</span> Roster/Free Agent Players Update Faster</li>
            <li><span class="badge bg-info">Enhancement</span> Added Scheme to League > Standings</li>
        </ul>

        <h2>v2023.8.4.1</h2>
        <ul>
            <li><span class="badge bg-info">Enhancement</span> Added How Built to Team Menu</li>
            <li><span class="badge bg-info">Enhancement</span> Added Draft History to Team Menu</li>
        </ul>

        <h2>v2023.3.23.1</h2>
        <ul>
            <li><span class="badge bg-info">Enhancement</span> QB now shows Change of Direction rating</li>
        </ul>

        <h2>v2023.3.17.1</h2>
        <ul>
            <li><span class="badge bg-info">Enhancement</span> League Names can be Updated</li>
        </ul>

        <h2>v2022.11.30.1</h2>
        <ul>
            <li><span class="badge bg-info">Enhancement</span> League Name Character Limit</li>
            <li><span class="badge bg-info">Enhancement</span> Upgraded to Font Awesome 6.2</li>
        </ul>

        <h2>v2022.11.12.1</h2>
        <ul>
            <li><span class="badge bg-info">Enhancement</span> Upgraded to Bootstrap 5.2</li>
            <li><span class="badge bg-info">Enhancement</span> Upgraded to Font Awesome 6.2</li>
        </ul>

        <h2>v2022.4.6.1</h2>
        <ul>
            <li><span class="badge bg-info">Enhancement</span> Week 17 Changes</li>
        </ul>


        <h2>v2021.10.8.1</h2>
        <ul>
            <li><span class="badge bg-info">Enhancement</span> Reorganized Leagues Landing Page</li>
        </ul>
        
        <h2>v2021.7.16.1</h2>
        <ul>
            <li><span class="badge bg-info">Enhancement</span> Display Leagues Count</li>
            <li><span class="badge bg-info">Enhancement</span> Tweaked Login UI</li>
        </ul>


        <h2>v2021.7.14.1</h2>
        <ul>
            <li><span class="badge bg-info">Enhancement</span> Grid Layout for Leagues</li>
            <li><span class="badge bg-info">Enhancement</span> Show My Leagues in Navbar</li>
            <li><span class="badge bg-info">Enhancement</span> Tweaked Leagues Layout</li>
        </ul>

        <h2>v2021.7.2.2</h2>
        <ul>
            <li><span class="badge bg-info">Enhancement</span> Set Week Leaders and Schedule to Current Season Type</li>
        </ul>

        <h2>v2021.7.2.1</h2>
        <ul>
            <li><span class="badge bg-danger">Bug</span> Login Expired Prematurely</li>
            <li><span class="badge bg-info">Enhancement</span> Replaced Elusive Rating with Change of Direction Rating (Madden 21)</li>
            <li><span class="badge bg-info">Enhancement</span> Using Table for Leagues Instead of Grid</li>
            <li><span class="badge bg-info">Enhancement</span> Color of Jersey Number on Profile was hard to read</li>
            <li><span class="badge bg-info">Enhancement</span> Updated HerokuApp for HTTPS</li>
            <li><span class="badge bg-info">Enhancement</span> Cleanup Login Page</li>
        </ul>
    </div>

</asp:Content>
