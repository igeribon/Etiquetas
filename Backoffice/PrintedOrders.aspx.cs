using System;
using System.Collections.Generic;
using System.Globalization;
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
        List<Shipping> _Shippings;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    DateTime _From = DateTime.Now;
                    DateTime _To = DateTime.Now;

                    txtFrom.Text = _From.ToString("yyyy-MM-dd");
                    txtTo.Text = _To.ToString("yyyy-MM-dd");


                    LoadShippings();
                }

                catch (Exception ex)
                {

                }
            }
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try { 
            //{
            //    if (e.Row.RowType == DataControlRowType.DataRow)
            //    {
            //        string _OrderId = grdShippings.DataKeys[e.Row.RowIndex].Value.ToString();
            //        GridView grdDetail = e.Row.FindControl("grdDetail") as GridView;

            //        var client = new RestClient("http://localhost:8080/shippings/" + _OrderId);
            //        client.Timeout = -1;
            //        var request = new RestRequest(Method.GET);
            //        IRestResponse response = client.Execute(request);



            //        Shipping _Shipping = JsonConvert.DeserializeObject<Shipping>(response.Content);

            //        List<Receiver> _ReceiverAux = new List<Receiver>();
            //        _ReceiverAux.Add(_Shipping.Receiver);

            //        grdDetail.DataSource = _ReceiverAux;
            //        grdDetail.DataBind();
            //    }
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
                //lblError.Text = "";
                _Shippings = new List<Shipping>();

                DateTime _From = DateTime.ParseExact(txtFrom.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                DateTime _To = DateTime.ParseExact(txtTo.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture).AddDays(1);

                string _Order = "asc";

                if (rbtDescendente.Checked)
                    _Order = "desc";


                var client = new RestClient("http://localhost:8080/shippings/from=" + _From.ToString("yyyy-MM-dd") + "&to=" + _To.ToString("yyyy-MM-dd") + "&hasLabel=1" + "&limit=300" + "&order=" + _Order);
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
                    throw new Exception("");
                }

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