<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucRecordPassingStats.ascx.cs" Inherits="CFMStats.Controls.Records.ucRecordPassingStats" %>


<div class="row">
    <div class="col-sm-6">
        <strong>Most Yards</strong>
        <div class="table-responsive table-bordered-curved">
            <div id="tableYards" runat="server" visible="true"></div>
        </div>
    </div>
    <div class="col-sm-6">
        <strong>Most Touchdowns</strong>
        <div class="table-responsive table-bordered-curved">
            <div id="tableTouchdowns" runat="server" visible="true"></div>
        </div>
    </div>

</div>

<div class="hidden-xs">
    <br />
</div>

<div class="row">
    <div class="col-sm-6">
        <strong>Most Interceptions</strong>
        <div class="table-responsive table-bordered-curved">
            <div id="tableInterceptions" runat="server" visible="true"></div>
        </div>
    </div>
    <div class="col-sm-6">
        <strong>Longest Pass</strong>
        <div class="table-responsive table-bordered-curved">
            <div id="tableLongest" runat="server" visible="true"></div>
        </div>
    </div>

</div>
 

