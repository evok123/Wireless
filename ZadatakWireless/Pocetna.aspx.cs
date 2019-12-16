using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZadatakWireless
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void gvProizvodi_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (gvProizvodi.SelectedDataKey != null)
            {
                var selectedId = gvProizvodi.SelectedDataKey.Value;
                Response.Redirect("Add.aspx?ID=" + selectedId + "&S=DB");
            }
        }
        protected void gvProizvodiJson_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (gvProizvodiJson.SelectedDataKey != null)
            {
                var selectedId = gvProizvodiJson.SelectedDataKey.Value;
                Response.Redirect("Add.aspx?ID=" + selectedId +"&S=JSON");
            }
        }
    }
}