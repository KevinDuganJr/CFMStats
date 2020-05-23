using System;
using System.Linq;
using System.Reflection;
using System.Web.UI;

namespace CFMStats.Controls
{
    public partial class ucExportDelete : System.Web.UI.UserControl
    {
        public string SExportId { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }



        protected void Page_PreRender(object sender, EventArgs e)
        {
            string url =
                $"<iframe id='ifm' src='http://kjd.herokuapp.com/Delete/{SExportId}' width='100%' height='920px' frameborder='0'></iframe>";

            urlDelete.InnerHtml = url;
        }

        protected void UpdatePanel1_Unload(object sender, EventArgs e)
        {
            MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).First(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel"));
            methodInfo.Invoke(ScriptManager.GetCurrent(Page), new object[] { sender as UpdatePanel });
        }
    }
}