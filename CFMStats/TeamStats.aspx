<%@ Page Title="Team Stats" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="TeamStats.aspx.cs" Inherits="CFMStats.TeamStats" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">

        <div class="col-xs-6 col-sm-3">
            <div class="form-horizontal" role="form">
                <div class="form-group form-group-sm">
                    <label class="control-label col-sm-3">Team</label>
                    <div class="col-sm-9">
                        <asp:DropDownList ID="ddlStatSelector" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlWeek_SelectedIndexChanged">
                            <asp:ListItem Text="Offense" Value="offense"></asp:ListItem>
                            <asp:ListItem Text="Defense" Value="defense"></asp:ListItem>
                            <asp:ListItem Text="Conversion" Value="conversion"></asp:ListItem>
                            <asp:ListItem Text="Turnovers" Value="turnovers"></asp:ListItem>
                            <asp:ListItem Text="Red Zone" Value="redzone"></asp:ListItem>
                            <asp:ListItem Text="Penalty" Value="penalty"></asp:ListItem>
                        </asp:DropDownList>

               <%--         <span class="input-group-btn">
                            <button type="button" name="btnPrevStatus" value="Previous" class="btn btn-default" onclick="Previous(this,'<%= ddlStatSelector.ClientID %>');" id="btnPrevItem">
                                <span class="glyphicon glyphicon-chevron-left"></span>
                            </button>

                            <button type="button" class="btn btn-default" name="btnNextStatus" value="Next" onclick="Next(this,'<%= ddlStatSelector.ClientID %>');" id="btnNextItem">
                                <span class="glyphicon glyphicon-chevron-right"></span>
                            </button>
                        </span>--%>

                    </div>
                </div>
            </div>
        </div>


        <div class="col-xs-6 col-sm-3">
            <div class="form-horizontal" role="form">
                <div class="form-group form-group-sm">
                    <label class="control-label col-sm-3">Season</label>
                    <div class="col-sm-9">
                        <asp:DropDownList ID="ddlSeasonType" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlWeek_SelectedIndexChanged">
                            <asp:ListItem Text="Regular" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Pre" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>



        <div class="col-xs-6 col-sm-3">

            <div class="form-horizontal" role="form">
                <div class="form-group form-group-sm">
                    <label class="control-label col-sm-3">Year</label>
                    <div class="col-sm-9">
                        <asp:DropDownList ID="ddlSeason" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlWeek_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
            </div>

        </div>

        <div class="col-xs-6 col-sm-3">
            <div class="form-horizontal" role="form">
                <div class="form-group form-group-sm">
                    <label class="control-label col-sm-3">Week</label>
                    <div class="col-sm-9">
                        <div class="input-group input-group-sm">
                            <asp:DropDownList ID="ddlWeek" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlWeek_SelectedIndexChanged">
                                <asp:ListItem Text="All" Value="99"></asp:ListItem>
                                <asp:ListItem Text="1" Value="0"></asp:ListItem>
                                <asp:ListItem Text="2" Value="1"></asp:ListItem>
                                <asp:ListItem Text="3" Value="2"></asp:ListItem>
                                <asp:ListItem Text="4" Value="3"></asp:ListItem>
                                <asp:ListItem Text="5" Value="4"></asp:ListItem>
                                <asp:ListItem Text="6" Value="5"></asp:ListItem>
                                <asp:ListItem Text="7" Value="6"></asp:ListItem>
                                <asp:ListItem Text="8" Value="7"></asp:ListItem>
                                <asp:ListItem Text="9" Value="8"></asp:ListItem>
                                <asp:ListItem Text="10" Value="9"></asp:ListItem>
                                <asp:ListItem Text="11" Value="10"></asp:ListItem>
                                <asp:ListItem Text="12" Value="11"></asp:ListItem>
                                <asp:ListItem Text="13" Value="12"></asp:ListItem>
                                <asp:ListItem Text="14" Value="13"></asp:ListItem>
                                <asp:ListItem Text="15" Value="14"></asp:ListItem>
                                <asp:ListItem Text="16" Value="15"></asp:ListItem>
                                <asp:ListItem Text="17" Value="16"></asp:ListItem>
                                <asp:ListItem Text="Wild Card" Value="17"></asp:ListItem>
                                <asp:ListItem Text="Divisional" Value="18"></asp:ListItem>
                                <asp:ListItem Text="Conference" Value="19"></asp:ListItem>
                                <asp:ListItem Text="Super Bowl" Value="21"></asp:ListItem>

                            </asp:DropDownList>

                            <span class="input-group-btn">
                                <button type="button" name="btnPrevStatus" value="Previous" class="btn btn-default" onclick="Previous(this,'<%= ddlWeek.ClientID %>');" id="btnPrevWeek">
                                    <span class="glyphicon glyphicon-chevron-left"></span>
                                </button>

                                <button type="button" class="btn btn-default" name="btnNextStatus" value="Next" onclick="Next(this,'<%= ddlWeek.ClientID %>');" id="btnNextWeek">
                                    <span class="glyphicon glyphicon-chevron-right"></span>
                                </button>
                            </span>

                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-sm-1"></div>

    </div>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

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

            <div class="table-responsive table-bordered-curved">
                <asp:PlaceHolder ID="phStatHolder" runat="server"></asp:PlaceHolder>
            </div>
            <div class="pager">
                <img src='Content\\tablesorter\\images\\first.png' class='first' />
                <img src='Content\\tablesorter\\images\\prev.png' class='prev' />
                <span class='pagedisplay' data-pager-output-filtered='{startRow:input} &ndash; {endRow} / {filteredRows} of {totalRows} total rows'></span>
                <img src='Content\\tablesorter\\images\\next.png' class='next' />
                <img src='Content\\tablesorter\\images\\last.png' class='last' />
                <select class="pagesize" title="Select page size">
                    <option selected="selected" value="25">25</option>
                    <option value="50">50</option>
                    <option value="all">all</option>
                </select>
                <select class="gotoPage" title="Select page number"></select>
            </div>



        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlSeason" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlWeek" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlStatSelector" EventName="SelectedIndexChanged" />
        </Triggers>

    </asp:UpdatePanel>


    <%-- tablesorter --%>
    <link href="../Content/tablesorter/theme.ice.min.css" rel="stylesheet" />
    <script src="../Scripts/jquery.tablesorter.min.js"></script>
    <script src="../Scripts/jquery.tablesorter.widgets.min.js"></script>
    <link href="../Content/tablesorter/jquery.tablesorter.pager.min.css" rel="stylesheet" />
    <script src="../Scripts/jquery.tablesorter.pager.min.js"></script>
    <%--<script src="../Scripts/jquery.stickytableheaders.min.js"></script>--%>

    <script>
        function Next(obj, obj1) {
            var index;


            var ddlNumbers = document.getElementById(obj1);
            var options = ddlNumbers.getElementsByTagName("option")
            for (var i = 0; i < options.length; i++) {
                if (options[i].selected) {
                    index = i;
                }
            }
            index = index + 1;
            if (index >= ddlNumbers.length) {

            }
            else {
                ddlNumbers.value = ddlNumbers[index].value;
                document.getElementById(obj1).onchange();
            }
            return false;
        }

        //http://www.aspforums.net/Threads/143568/Navigate-through-DropDownList-Items-using-Next-Previous-buttons-using-JavaScript-and-jQuery/

        function Previous(obj, obj1) {
            var index;
            var ddlNumbers = document.getElementById(obj1);


            var options = ddlNumbers.getElementsByTagName("option")
            for (var i = 0; i < options.length; i++) {
                if (options[i].selected) {
                    index = i;
                }
            }
            index = index - 1;
            if (index <= -1) {
            }
            else {
                ddlNumbers.value = ddlNumbers[index].value;
                document.getElementById(obj1).onchange();
            }
            return false;
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
                $('[data-toggle="tooltip"]').tooltip()
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
                widgets: ['zebra', 'columns', 'filter', 'saveSort', 'resizeable'],

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

            });


        }
    </script>

</asp:Content>
