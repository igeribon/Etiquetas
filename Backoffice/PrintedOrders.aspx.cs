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
                    DateTime _From = new DateTime(2021, 1, 1);
                    DateTime _To = DateTime.Now;

                    txtFrom.Text = _From.ToString("dd/MM/yyyy");
                    txtTo.Text = _To.ToString("dd/MM/yyyy");


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

                    List<Shipping> _Shippings = JsonConvert.DeserializeObject<List<Shipping>>(response.Content);
                    grdShippings.DataSource = _Shippings;
                    grdShippings.DataBind();
                    


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

            }

            catch
            {


            }
        }

        public void LoadShippings()
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


                if (response.StatusCode.ToString() == "OK")
                {

                    List<Shipping> _Shippings = JsonConvert.DeserializeObject<List<Shipping>>(response.Content);
                    grdShippings.DataSource = _Shippings;
                    grdShippings.DataBind();

                if (response.StatusCode.ToString() == "OK")
                {

                    Shipping _Shipping = JsonConvert.DeserializeObject<Shipping>(response.Content);

                    List<Receiver> _ReceiverAux = new List<Receiver>();
                    _ReceiverAux.Add(_Shipping.Receiver);

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