using API.DataTypes;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Backoffice
{
    public partial class PendingOrders : System.Web.UI.Page
    {
        static List<Shipping> _Shippings;

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
            try
            {
                //if (e.Row.RowType == DataControlRowType.DataRow)
                //{
                //    string _OrderId = grdShippings.DataKeys[e.Row.RowIndex].Value.ToString();
                //    GridView grdDetail = e.Row.FindControl("grdDetail") as GridView;

                //    var client = new RestClient("https://api.enviosmilgenial.com/shippings/" + _OrderId);
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

                string _OrderId= Convert.ToString(grdShippings.DataKeys[row.RowIndex].Value);

              
                    var client = new RestClient("https://api.enviosmilgenial.com/shippings/" + _OrderId);
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

        protected void btnGenerateLabel_Click(object sender, EventArgs e)
        {
            try
            {
   
                Button lbtn = (Button)sender;
                GridViewRow row = (GridViewRow)lbtn.NamingContainer;

                string _OrderId = Convert.ToString(grdShippings.DataKeys[row.RowIndex].Value);

                Shipping _Shipping = _Shippings.Find(x => x.OrderId.Trim().Equals(_OrderId.Trim()));

                if (_Shipping.FinancialStatus.Trim() == "paid" || _Shipping.CashOnDelivery)
                {
                    if (_Shipping.Receiver.Address.Locality.Id != 0)
                    {
                        var client = new RestClient("https://api.enviosmilgenial.com/shippings/" + _OrderId + "/labels");
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        IRestResponse response = client.Execute(request);


                        if (response.StatusCode != System.Net.HttpStatusCode.InternalServerError)
                        {
                          
                            _Shipping = JsonConvert.DeserializeObject<Shipping>(response.Content);

                            File.WriteAllBytes(Server.MapPath("~/Label/Label.pdf"), _Shipping.Labels[0].Data);

                            Response.Write("<script>");
                            Response.Write("window.open('ShowPDF.aspx','_blank')");

                            Response.Write("</script>");

                            //Process printjob = new Process();

                            //printjob.StartInfo.FileName = Server.MapPath("~/Label/Label.pdf");//path of your file;

                            //printjob.StartInfo.Verb = "Print";

                            //printjob.StartInfo.CreateNoWindow = true;

                            //printjob.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

                            //PrinterSettings setting = new PrinterSettings();

                            //setting.DefaultPageSettings.Landscape = true;

                            //printjob.Start();


                            LoadShippings();

                            lblError.Text = "";
                        }

                        else
                        {
                            lblError.Text = "Error: Hubo un error al generar la etiqueta.";                        
                        }
                    }

                    else
                    {
                        lblError.Text = "Error: La órden no tiene una empresa y/o localidad asignada.";
                    }
                }

                else
                {
                    lblError.Text = "Error: La órden no está paga.";
                }

                
            }

            catch (Exception ex)
            {
                lblError.Text = "Error: "+ex.Message;
            }

        }


        public void LoadShippings()
        {
            try
            {
                lblError.Text = "";
                _Shippings = new List<Shipping>();

                DateTime _From = DateTime.ParseExact(txtFrom.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                DateTime _To = DateTime.ParseExact(txtTo.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture).AddDays(1);

                string _Order = "asc";

                if (rbtDescendente.Checked)
                    _Order = "desc";
              
                var client = new RestClient("https://api.enviosmilgenial.com/shippings/from=" + _From.ToString("yyyy-MM-dd") + "&to=" + _To.ToString("yyyy-MM-dd") + "&hasLabel=0" + "&limit=300" + "&order=" + _Order);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);


                if (response.StatusCode.ToString() == "OK")
                {

                    _Shippings = JsonConvert.DeserializeObject<List<Shipping>>(response.Content);
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

        protected void rbtDescendente_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}