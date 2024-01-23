using CFMStats.Classes;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Providers.Entities;
using System.Web.UI;
using System.Web.UI.WebControls;
using CFMStats.Classes;
using Microsoft.AspNet.Identity;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Web.UI;

namespace CFMStats.Controls
{
    public partial class ucMyLeagues : System.Web.UI.UserControl
    {

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetAllLeagues();
            }
        }
        private void GetAllLeagues()
        {
         

        }
       
    }
}