using API.DataTypes;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Backoffice
{
    public partial class Search : System.Web.UI.Page
    {
        static List<Shipping> _Shippings;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                try
                {
                    drpSearchType.Items.Add("Nro. Orden");
                    drpSearchType.Items.Add("Cliente");

                    drpSearchType.Text = "Nro. Orden";
                 
                }

                catch (Exception ex)
                {

                }
            }
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                //if (e.Row.RowType == DataControlRowType.DataRow)
                //{
                //    string _OrderId = grdShippings.DataKeys[e.Row.RowIndex].Value.ToString();
                //    GridView grdDetail = e.Row.FindControl("grdDetail") as GridView;

                //    var client = new RestClient("http://localhost:8080/shippings/" + _OrderId);
                //    client.Timeout = -1;
                //    var request = new RestRequest(Method.GET);
                //    IRestResponse response = client.Execute(request);



                //    Shipping _Shipping = JsonConvert.DeserializeObject<Shipping>(response.Content);

                //    List<Receiver> _ReceiverAux = new List<Receiver>();
                //    _ReceiverAux.Add(_Shipping.Receiver);

                //    grdDetail.DataSource = _ReceiverAux;
                //    grdDetail.DataBind();
                //}
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnDetail_OnClick(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton lbtn = (ImageButton)sender;
                GridViewRow row = (GridViewRow)lbtn.NamingContainer;

                string _OrderId = Convert.ToString(grdShippings.DataKeys[row.RowIndex].Value);


                var client = new RestClient("http://localhost:8080/shippings/" + _OrderId);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);



                Shipping _Shipping = JsonConvert.DeserializeObject<Shipping>(response.Content);

                Session["Shipping"] = _Shipping;

                Response.Redirect("ShippingDetail.aspx", false);


            }

            catch
            {


            }
        }



        public void LoadShippings()
        {
            try
            {

                _Shippings = new List<Shipping>();

                if (drpSearchType.Text == "Nro. Orden")
                {
                    if (txtSearch.Text != "")
                    {
                        string _OrderId = txtSearch.Text.Trim();


                        var client = new RestClient("http://localhost:8080/shippings/" + _OrderId);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.GET);
                        IRestResponse response = client.Execute(request);



                        Shipping _Shipping = JsonConvert.DeserializeObject<Shipping>(response.Content);

                        _Shippings.Add(_Shipping);
                    }

                }

                else if (drpSearchType.Text == "Cliente")
                {
                    if (txtSearch.Text != "")
                    {
                        string _Name = txtSearch.Text.Trim();


                        var client = new RestClient("http://localhost:8080/shippings/ReceiverNameLastname=" + _Name);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.GET);
                        IRestResponse response = client.Execute(request);



                         _Shippings = JsonConvert.DeserializeObject<List<Shipping>>(response.Content);


                    }
                }

                grdShippings.AutoGenerateColumns = false;
                grdShippings.DataSource = _Shippings;
                grdShippings.DataBind();

            }

            catch (Exception ex)
            {

            }
        }



        protected void btnAplicar_OnClick(object sender, EventArgs e)
        {
            try
            {
                LoadShippings();
            }

            catch (Exception ex)
            {


            }
        }


    }
}