<%@ Page Title="Player Profile" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="CFMStats.Profile" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .tablesorter thead .disabled {
            display: none
        }

        .playerName {
            text-transform: uppercase;
            font-weight: bold;
            font-size: 2.3em;
            color: #808080;
        }

        .teamName {
            color: #e4e4e4;
            font-weight: bold;
            text-transform: uppercase;
            font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif;
            line-height: 20px;
        }

        .cityName {
            color: #808080;
            text-transform: uppercase;
            font-family: 'Gill Sans', 'Gill Sans MT', Calibri, 'Trebuchet MS', sans-serif;
            line-height: 20px;
        }

        .position {
            font-size: 1.9em;
            color: #e6e6e6;
            font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
        }

        .jersey {
            font-size: 1.2em;
            color: #100101;
        }

        .traitName {
            text-align: right;
            font-weight: bold;
        }

        .traitValue {
            text-align: left;
        }
    </style>

    <div class="col-sm-12">
        <div id="tableProfile" runat="server" visible="true"></div>
    </div>
    <asp:Button ID="btnGetAbilities" runat="server" Text="" OnClick="btnGetAbilities_OnClick" Style="display: none;" />
    <asp:Button ID="btnGetTraits" runat="server" Text="" OnClick="btnGetTraits_Click" Style="display: none;" />
    <asp:Button ID="btnGetStats" runat="server" Text="" OnClick="btnGetStats_Click" Style="display: none;" />
    <asp:Button ID="btnGetWeekStats" runat="server" Text="" OnClick="btnGetWeekStats_Click" Style="display: none;" />

    <!-- Nav tabs -->
    <ul class="nav nav-tabs" role="tablist">
        <li role="presentation" class="active"><a href="#home" onclick="getStats();" aria-controls="home" role="tab" data-toggle="tab">Season</a></li>
        <li role="presentation"><a href="#profile" onclick="getWeekStats();" aria-controls="profile" role="tab" data-toggle="tab">Games</a></li>
        <li role="presentation"><a href="#abilities" onclick="getAbilities();" aria-controls="abilities"  role="tab" data-toggle="tab">Abilities</a></li>
        <li role="presentation"><a href="#settings" onclick="getTraits();" aria-controls="settings" role="tab" data-toggle="tab">Traits</a></li>
    </ul>

    <!-- Tab panes -->
    <div class="tab-content">
        <div role="tabpanel" class="tab-pane active" id="home">
            <div class="row">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <div style="text-align: center;">
                            <label class="label label-warning">... LOADING ...</label>
                            <label class="label label-danger">... LOADING ...</label>
                            <label class="label label-success">... LOADING ...</label><br />
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
                        <asp:AsyncPostBackTrigger ControlID="btnGetStats" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>

            </div>


        </div>
        <div role="tabpanel" class="tab-pane" id="profile">
            <div class="row">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                    <ProgressTemplate>
                        <div style="text-align: center;">
                            <label class="label label-warning">... LOADING ...</label>
                            <label class="label label-danger">... LOADING ...</label>
                            <label class="label label-success">... LOADING ...</label><br />
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
                        <asp:AsyncPostBackTrigger ControlID="btnGetWeekStats" EventName="Click" />
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
                    <asp:AsyncPostBackTrigger ControlID="btnGetAbilities" EventName="Click" />
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
                    <asp:AsyncPostBackTrigger ControlID="btnGetTraits" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>



    <%-- tablesorter --%>
    <link href="../Content/tablesorter/theme.ice.min.css" rel="stylesheet" />
    <script src="../Scripts/jquery.tablesorter.min.js"></script>
    <script src="../Scripts/jquery.tablesorter.widgets.min.js"></script>
    <link href="../Content/tablesorter/jquery.tablesorter.pager.min.css" rel="stylesheet" />
    <script src="../Scripts/jquery.tablesorter.pager.min.js"></script>
    <%--<script src="../Scripts/jquery.stickytableheaders.min.js"></script>--%>


    <script>

        function getStats() {
            document.getElementById('<%= btnGetStats.ClientID%>').click();
        }

        function getAbilities() {
            document.getElementById('<%= btnGetAbilities.ClientID%>').click();
        }

        function getTraits() {
            document.getElementById('<%= btnGetTraits.ClientID%>').click();
        }

        function getWeekStats() {
            document.getElementById('<%= btnGetWeekStats.ClientID%>').click();
        }



        //Initial bind
        $(document).ready(function () {
            BindControlEvents();
        });

        //Re-bind for callbacks
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        prm.add_endRequest(function () {
            BindControlEvents();
        });


        function BindControlEvents() {
            $(function () {
                $('[data-toggle="tooltip"]').tooltip();
            })

            //var offset = $('.navbar').height();
            //$("html:not(.legacy) table").stickyTableHeaders({ fixedOffset: offset });

            $("table").tablesorter({
                theme: 'ice',

                // fix the column widths
                widthFixed: true,

                // Show an indeterminate timer icon in the header when the table
                // is sorted or filtered
                showProcessing: true,


                // *** FUNCTIONALITY ***
                // prevent text selection in header
                cancelSelection: true,



                // The key used to select more than one column for multi-column
                // sorting.
                sortMultiSortKey: "shiftKey",

                // key used to remove sorting on a column
                sortResetKey: 'ctrlKey',

                // false for German "1.234.567,89" or French "1 234 567,89"
                usNumberFormat: true,

                // If true, parsing of all table cell data will be delayed
                // until the user initializes a sort
                // delayInit: true,

                // if true, server-side sorting should be performed because
                // client-side sorting will be disabled, but the ui and events
                // will still be used.
                serverSideSorting: false,

                // *** SORT OPTIONS ***
                // These are detected by default,
                // but you can change or disable them
                // these can also be set using data-attributes or class names
                headers: {
                    // set "sorter : false" (no quotes) to disable the column
                    0: {
                        sorter: "text"
                    },
                    1: {
                        sorter: "digit"
                    },
                    2: {
                        sorter: "text"
                    },
                    3: {
                        sorter: "url"
                    }
                },

                // ignore case while sorting
                ignoreCase: true,

                // forces the user to have this/these column(s) sorted first
                sortForce: null,
                // initial sort order of the columns, example sortList: [[0,0],[1,0]],
                // [[columnIndex, sortDirection], ... ]
                //sortList: [
                //    [5, 1]
                //],
                // default sort that is added to the end of the users sort
                // selection.
                sortAppend: null,

                // starting sort direction "asc" or "desc"
                sortInitialOrder: "desc",

                // Replace equivalent character (accented characters) to allow
                // for alphanumeric sorting
                sortLocaleCompare: false,

                // third click on the header will reset column to default - unsorted
                sortReset: false,

                // restart sort to "sortInitialOrder" when clicking on previously
                // unsorted columns
                sortRestart: false,

                // sort empty cell to bottom, top, none, zero
                emptyTo: "bottom",

                // sort strings in numerical column as max, min, top, bottom, zero
                stringTo: "max",




                // use custom text sorter
                // function(a,b){ return a.sort(b); } // basic sort
                textSorter: null,

                // *** WIDGETS ***

                // apply widgets on tablesorter initialization
                initWidgets: true,

                // include zebra and any other widgets, options:
                // 'columns', 'filter', 'stickyHeaders' & 'resizable'
                // 'uitheme' is another widget, but requires loading
                // a different skin and a jQuery UI theme.
                widgets: ['zebra', 'filter', 'saveSort', 'resizeable'],

                widgetOptions: {

                    // zebra widget: adding zebra striping, using content and
                    // default styles - the ui css removes the background
                    // from default even and odd class names included for this
                    // demo to allow switching themes
                    // [ "even", "odd" ]
                    zebra: [
                        "ui-widget-content even",
                        "ui-state-default odd"],

                    // uitheme widget: * Updated! in tablesorter v2.4 **
                    // Instead of the array of icon class names, this option now
                    // contains the name of the theme. Currently jQuery UI ("jui")
                    // and Bootstrap ("bootstrap") themes are supported. To modify
                    // the class names used, extend from the themes variable
                    // look for the "$.extend($.tablesorter.themes.jui" code below
                    //  uitheme: 'jui',

                    // columns widget: change the default column class names
                    // primary is the 1st column sorted, secondary is the 2nd, etc
                    columns: [
                        "primary",
                        "secondary",
                        "tertiary"],

                    // filter widget: If true, a filter will be added to the top of
                    // each table column.
                    filter_columnFilters: true,

                    // filter widget: css class applied to the table row containing the
                    // filters & the inputs within that row
                    filter_cssFilter: "form-control",

                    // filter widget: Customize the filter widget by adding a select
                    // dropdown with content, custom options or custom filter functions
                    // see http://goo.gl/HQQLW for more details
                    filter_functions: null,

                    // filter widget: Set this option to true to hide the filter row
                    // initially. The rows is revealed by hovering over the filter
                    // row or giving any filter input/select focus.
                    filter_hideFilters: false,

                    // filter widget: Set this option to false to keep the searches
                    // case sensitive
                    filter_ignoreCase: true,

                    // filter widget: jQuery selector string of an element used to
                    // reset the filters.
                    filter_reset: null,

                    // Delay in milliseconds before the filter widget starts searching;
                    // This option prevents searching for every character while typing
                    // and should make searching large tables faster.
                    filter_searchDelay: 300,

                    // Set this option to true if filtering is performed on the server-side.
                    filter_serversideFiltering: false,

                    // filter widget: Set this option to true to use the filter to find
                    // text from the start of the column. So typing in "a" will find
                    // "albert" but not "frank", both have a's; default is false
                    filter_startsWith: false,

                    // filter widget: If true, ALL filter searches will only use parsed
                    // data. To only use parsed data in specific columns, set this option
                    // to false and add class name "filter-parsed" to the header
                    filter_useParsedData: false,


                    // saveSort widget: If this option is set to false, new sorts will
                    // not be saved. Any previous saved sort will be restored on page
                    // reload.
                    saveSort: true

                },

                // *** CALLBACKS ***
                // function called after tablesorter has completed initialization
                initialized: function (table) { },

                // *** CSS CLASS NAMES ***
                tableClass: 'tablesorter',
                cssAsc: "tablesorter-headerSortUp",
                cssDesc: "tablesorter-headerSortDown",
                cssHeader: "tablesorter-header",
                cssHeaderRow: "tablesorter-headerRow",
                cssIcon: "tablesorter-icon",
                cssChildRow: "tablesorter-childRow",
                cssInfoBlock: "tablesorter-infoOnly",
                cssProcessing: "tablesorter-processing",

                // *** SELECTORS ***
                // jQuery selectors used to find the header cells.
                selectorHeaders: '> thead th, > thead td',

                // jQuery selector of content within selectorHeaders
                // that is clickable to trigger a sort.
                selectorSort: "th, td",

                // rows with this class name will be removed automatically
                // before updating the table cache - used by "update",
                // "addRows" and "appendCache"
                selectorRemove: "tr.remove-me",

                // *** DEBUGING ***
                // send messages to console
                debug: false

            }).tablesorterPager({

                // target the pager markup - see the HTML block below
                container: $(".pager"),

                // use this url format "http:/mydatabase.com?page={page}&size={size}" 
                ajaxUrl: null,

                // process ajax so that the data object is returned along with the
                // total number of rows; example:
                // {
                //   "data" : [{ "ID": 1, "Name": "Foo", "Last": "Bar" }],
                //   "total_rows" : 100 
                // } 
                ajaxProcessing: function (ajax) {
                    if (ajax && ajax.hasOwnProperty('data')) {
                        // return [ "data", "total_rows" ]; 
                        return [ajax.data, ajax.total_rows];
                    }
                },

                // output string - default is '{page}/{totalPages}';
                // possible variables:
                // {page}, {totalPages}, {startRow}, {endRow} and {totalRows}
                output: '{startRow} to {endRow} ({totalRows})',

                // apply disabled classname to the pager arrows when the rows at
                // either extreme is visible - default is true
                updateArrows: true,

                // starting page of the pager (zero based index)
                page: 0,

                // Number of visible rows - default is 10
                size: 25,

                // if true, the table will remain the same height no matter how many
                // records are displayed. The space is made up by an empty 
                // table row set to a height to compensate; default is false 
                fixedHeight: false,

                // remove rows from the table to speed up the sort of large tables.
                // setting this to false, only hides the non-visible rows; needed
                // if you plan to add/remove rows with the pager enabled.
                removeRows: true,

                // css class names of pager arrows
                // next page arrow
                cssNext: '.next',
                // previous page arrow
                cssPrev: '.prev',
                // go to first page arrow
                cssFirst: '.first',
                // go to last page arrow
                cssLast: '.last',
                // select dropdown to allow choosing a page
                cssGoto: '.gotoPage',
                // location of where the "output" is displayed
                cssPageDisplay: '.pagedisplay',
                // dropdown that sets the "size" option
                cssPageSize: '.pagesize',
                // class added to arrows when at the extremes 
                // (i.e. prev/first arrows are "disabled" when on the first page)
                // Note there is no period "." in front of this class name
                cssDisabled: 'disabled'

            });



        }
    </script>
</asp:Content>



