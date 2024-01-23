<%@ Page Title="Team Rankings" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TeamRankings.aspx.cs" Inherits="CFMStats.TeamRankings" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <div class="row">
        <div class="col-xs-6 col-sm-3">
            <div class="input-group input-group-sm mb-3">
                <span class="input-group-text bg-secondary" id="basic-addon1">Season</span>
                <asp:DropDownList ID="ddlSeason" runat="server" CssClass="form-control form-select form-select-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlWeek_SelectedIndexChanged"></asp:DropDownList>
            </div>
        </div>

        <div class="col-xs-6 col-sm-3">
            <div class="input-group input-group-sm mb-3">
                <span class="input-group-text bg-secondary" id="basic-addon2">Type</span>
                <asp:DropDownList ID="ddlSeasonType" runat="server" CssClass="form-control form-select form-select-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlWeek_SelectedIndexChanged">
                    <asp:ListItem Text="Regular" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Pre" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
    </div>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>
                    <div style="text-align: center;">
                        <label class="badge bg-warning">... LOADING ...</label>
                        <label class="badge bg-danger">... LOADING ...</label>
                        <label class="badge bg-success">... LOADING ...</label><br/>
                    </div>
                    <br/>
                </ProgressTemplate>
            </asp:UpdateProgress>

            <div class="table-responsive table-bordered-curved">
                <div id="tableTeamRankings" runat="server" visible="true"></div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlSeason" EventName="SelectedIndexChanged"/>
            <asp:AsyncPostBackTrigger ControlID="ddlSeasonType" EventName="SelectedIndexChanged"/>
        </Triggers>
    </asp:UpdatePanel>


    <%--<link href="https://code.jquery.com/ui/1.12.0/themes/smoothness/jquery-ui.css" rel="stylesheet" />--%>

    <%-- tablesorter --%>
    <link href="../Content/tablesorter/theme.ice.min.css" rel="stylesheet"/>
    <script src="../Scripts/jquery.tablesorter.min.js"></script>
    <script src="../Scripts/jquery.tablesorter.widgets.min.js"></script>
    <%--<script src="https://mottie.github.io/tablesorter/js/jquery.tablesorter.widgets.js"></script>--%>
    <link href="../Content/tablesorter/jquery.tablesorter.pager.min.css" rel="stylesheet"/>
    <script src="../Scripts/jquery.tablesorter.pager.min.js"></script>
    <%--<script src="https://rawgit.com/Mottie/tablesorter/master/js/widgets/widget-scroller.js"></script>--%>

    <%--<script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>    --%>

    <script>


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
                $('[data-toggle="tooltip"]').tooltip()
            })

            $("table").tablesorter({

                theme: 'ice',

                // fix the column widths
                widthFixed: true,

                // Show an indeterminate timer icon in the header when the table
                // is sorted or filtered
                showProcessing: true,

                // header layout template (HTML ok); {content} = innerHTML,
                // {icon} = <i/> (class from cssIcon)
                headerTemplate: '{content}',

                // return the modified template string
                onRenderTemplate: null, // function(index, template){ return template; },

                // called after each header cell is rendered, use index to target the column
                // customize header HTML
                onRenderHeader: function (index) {
                    // the span wrapper is added by default
                    $(this).find('div.tablesorter-header-inner').addClass('roundedCorners');
                },

                // *** FUNCTIONALITY ***
                // prevent text selection in header
                cancelSelection: true,

                // other options: "ddmmyyyy" & "yyyymmdd"
                dateFormat: "mmddyyyy",

                // The key used to select more than one column for multi-column
                // sorting.
                sortMultiSortKey: "shiftKey",

                // key used to remove sorting on a column
                sortResetKey: 'ctrlKey',

                // false for German "1.234.567,89" or French "1 234 567,89"
                usNumberFormat: true,

                // If true, parsing of all table cell data will be delayed
                // until the user initializes a sort
                delayInit: true,

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

                // extract text from the table - this is how is
                // it done by default
                textExtraction: {
                    0: function (node) {
                        return $(node).text();
                    },
                    1: function (node) {
                        return $(node).text();
                    }
                },

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
                widgets: ['zebra', 'columns', 'filter', 'saveSort', 'resizeable', 'scroller'],

                widgetOptions: {

                    //scroller_fixedColumns: 1,
                    //scroller_height: 560,
                    //// scroll tbody to top after sorting
                    //scroller_upAfterSort: true,
                    //// pop table header into view while scrolling up the page
                    //scroller_jumpToHeader: true,

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
                    saveSort: false

                }
            });

        }


    </script>

</asp:Content>