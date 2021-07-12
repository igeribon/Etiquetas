using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using API.DataTypes;
using Newtonsoft.Json;

namespace Backoffice
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string _Username = txtUsername.Text;
                string _Password = txtPassword.Text;
                
                if (_Username != "" && _Password != "")
                {

                    var client = new RestClient("http://api.enviosmilgenial.com/Login/Username=" + _Username + "&Password=" + _Password);
                    client.Timeout = -1;
                    var request = new RestRequest(Method.GET);
                    IRestResponse response = client.Execute(request);

               
                    if (response.StatusCode.ToString() == "OK")
                    {
                      
                        Account _Account = JsonConvert.DeserializeObject<Account>(response.Content);

                       
                        Session["Account"] = _Account;
                        Response.Redirect("Home.aspx");
                    }

                    else
                    {
                        throw new Exception("Usuario y/o contraseña inválidos");
                    }
                }

                else
                {
                    throw new Exception("Ingrese un usuario y contraseña.");
                }
            }

            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
    }
}