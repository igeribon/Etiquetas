using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using API.DataTypes;
using Newtonsoft.Json;
using RestSharp;

namespace Backoffice
{
    public partial class PrintedOrders : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            List<Shipping> _List = new List<Shipping>();

            try
            {
                DateTime _From = new DateTime(2021, 1, 1);
                DateTime _To = DateTime.Now;

                var client = new RestClient("http://localhost:8080/shippings/from="+_From.ToString("yyyy-MM-dd")+"&to="+_To.ToString("yyyy-MM-dd") + "&hasLabel=1");
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);


                if (response.StatusCode.ToString() == "OK")
                {

                    List<Shipping> _Shippings = JsonConvert.DeserializeObject<List<Shipping>>(response.Content);
                    grdShippings.DataSource = _Shippings;
                    grdShippings.DataBind();
                    

                }

                else
                {
                    throw new Exception("Usuario y/o contraseña inválidos");
                }

            }

            catch (Exception ex)
            { 
            
            }
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string _OrderId = grdShippings.DataKeys[e.Row.RowIndex].Value.ToString();
                    GridView grdDetail = e.Row.FindControl("grdDetail") as GridView;

                    var client = new RestClient("http://localhost:8080/shippings/" + _OrderId);
                    client.Timeout = -1;
                    var request = new RestRequest(Method.GET);
                    IRestResponse response = client.Execute(request);



                    Shipping _Shipping = JsonConvert.DeserializeObject<Shipping>(response.Content);

                    List<Receiver> _ReceiverAux = new List<Receiver>();
                    _ReceiverAux.Add(_Shipping.Receiver);

                    grdDetail.DataSource = _ReceiverAux;
                    grdDetail.DataBind();
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}