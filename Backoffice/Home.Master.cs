using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using API.DataTypes;

namespace Backoffice
{
    
    public partial class Home : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Account _Account = (Account)Session["Account"];
            lblName.Text ="Bienvenido, " +_Account.Name+"!";
        }
    }
}