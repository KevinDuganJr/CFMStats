<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucRecordReceivingStats.ascx.cs" Inherits="CFMStats.Controls.Records.ucRecordReceivingStats" %>


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
        <strong>Most Receptions</strong>
        <div class="table-responsive table-bordered-curved">
            <div id="tableReceptions" runat="server" visible="true"></div>
        </div>
    </div>
    <div class="col-sm-6">
        <strong>Most Drops</strong>
        <div class="table-responsive table-bordered-curved">
            <div id="tableDrops" runat="server" visible="true"></div>
        </div>
    </div>

</div>

<div class="hidden-xs">
    <br />
</div>

<div class="row">
    <div class="col-sm-6">
        <strong>Longest Reception</strong>
        <div class="table-responsive table-bordered-curved">
            <div id="tableLongest" runat="server" visible="true"></div>
        </div>
    </div>

    <div class="col-sm-6">
    </div>

</div>
