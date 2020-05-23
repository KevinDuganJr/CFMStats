<%@ Page Title="Teams" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Teams.aspx.cs" Inherits="CFMStats.LeagueTeams" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

       <br />
    <div class="row">
        <div class="col-sm-3">
            <asp:Button ID="btnGetTeams" runat="server" CssClass="btn btn-default" Text="Teams" OnClick="btnGetTeams_Click" />
        </div>

        <div class="col-sm-3"></div>

        <div class="col-sm-3"></div>

        <div class="col-sm-3"></div>
    </div>



    <div class="row">
        <div class="col-sm-12">

            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>... loading teams...</ProgressTemplate>
            </asp:UpdateProgress>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                      <div id="tableFreeAgents" runat="server" visible="true"></div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnGetTeams" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>

     <%-- tablesorter --%>
    <link href="../Content/tablesorter/theme.bootstrap.min.css" rel="stylesheet" />
    <script src="../Scripts/jquery.tablesorter.js"></script>
    <script src="../Scripts/jquery.tablesorter.widgets.js"></script>
    <link href="../Content/tablesorter/jquery.tablesorter.pager.min.css" rel="stylesheet" />
    <script src="../Scripts/jquery.tablesorter.pager.min.js"></script>
    <script src="../Scripts/jquery.stickytableheaders.min.js"></script>

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
            var offset = $('.navbar').height();
            $("html:not(.legacy) table").stickyTableHeaders({ fixedOffset: offset });





            // NOTE: $.tablesorter.theme.bootstrap is ALREADY INCLUDED in the jquery.tablesorter.widgets.js
            // file; it is included here to show how you can modify the default classes
            //$.tablesorter.themes.bootstrap = {
            //    // these classes are added to the table. To see other table classes available,
            //    // look here: http://getbootstrap.com/css/#tables
            //    table: 'table table-bordered table-striped',
            //    caption: 'caption',
            //    // header class names
            //    header: 'bootstrap-header', // give the header a gradient background (theme.bootstrap_2.css)
            //    sortNone: '',
            //    sortAsc: '',
            //    sortDesc: '',
            //    active: '', // applied when column is sorted
            //    hover: '', // custom css required - a defined bootstrap style may not override other classes
            //    // icon class names
            //    icons: '', // add "icon-white" to make them white; this icon class is added to the <i> in the header
            //    iconSortNone: 'bootstrap-icon-unsorted', // class name added to icon when column is not sorted
            //    iconSortAsc: 'glyphicon glyphicon-chevron-up', // class name added to icon when column has ascending sort
            //    iconSortDesc: 'glyphicon glyphicon-chevron-down', // class name added to icon when column has descending sort
            //    filterRow: '', // filter row class; use widgetOptions.filter_cssFilter for the input/select element
            //    footerRow: '',
            //    footerCells: '',
            //    even: '', // even row zebra striping
            //    odd: ''  // odd row zebra striping
            //};

            // call the tablesorter plugin and apply the uitheme widget


            $("table").tablesorter({
                // this will apply the bootstrap theme if "uitheme" widget is included
                // the widgetOptions.uitheme is no longer required to be set
                theme: "bootstrap",
                widthFixed: true,
                headerTemplate: '{content} {icon}', // new in v2.7. Needed to add the bootstrap icon!

                // widget code contained in the jquery.tablesorter.widgets.js file
                // use the zebra stripe widget if you plan on hiding any rows (filter widget)
                widgets: ["uitheme", "resizable", "zebra", "stickyHeaders", "filter"],

                widgetOptions: {
                    // using the default zebra striping class name, so it actually isn't included in the theme variable above
                    // this is ONLY needed for bootstrap theming if you are using the filter widget, because rows are hidden
                    zebra: ["even", "odd"],
                    storage_useSessionStorage: true,
                    resizable_addLastColumn: false,
                    // reset filters button
                    filter_reset: ".reset",
                    filter_saveFilters: false,

                    //cssStickyHeaders_addCaption: true,
                    //cssStickyHeaders_offset        : 500,
                    //// jQuery selector or object to attach sticky header to
                    //cssStickyHeaders_attachTo      : null,
                    //cssStickyHeaders_filteredToTop : true,
                    //cssStickyHeaders_zIndex: 10,

                    // extra css class name (string or array) added to the filter element (input or select)
                    filter_cssFilter: "form-control",
                    filter_columnFilters: true
                }
            });

            //.tablesorterPager({

            //     // target the pager markup - see the HTML block below
            //     container: $(".ts-pager"),

            //     // target the pager page select dropdown - choose a page
            //     cssGoto: ".pagenum",

            //     // remove rows from the table to speed up the sort of large tables.
            //     // setting this to false, only hides the non-visible rows; needed if you plan to add/remove rows with the pager enabled.
            //     removeRows: false,

            //     // output string - default is '{page}/{totalPages}';
            //     // possible variables: {page}, {totalPages}, {filteredPages}, {startRow}, {endRow}, {filteredRows} and {totalRows}
            //     output: '{startRow} - {endRow} / {filteredRows} ({totalRows})'
            // }); 


        }


    </script>
</asp:Content>
