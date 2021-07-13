using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using API.DataTypes;
using Newtonsoft.Json;
using RestSharp;

namespace Backoffice
{
    public partial class ShippingDetail : System.Web.UI.Page
    {
        static List<Courier> _Couriers;
        static Shipping _Shipping;
        static string _PreviousPage;
        static List<string> _States;
        static List<string> _Cities;
        static List<string> _Localities;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                   _PreviousPage = Request.UrlReferrer.ToString();



                    _Shipping = (Shipping)Session["Shipping"];
                    LoadDetail();
                }

              
            }

            catch (Exception ex)
            { 
            
            }
        }


        public void LoadDetail()
        {
            try
            {
                _Couriers = new List<Courier>();

                Courier _Courier = new Courier();
                _Courier.Id = 1;
                _Courier.Name = "DAC";
                _Courier.Country = "URUGUAY";
                _Couriers.Add(_Courier);


                _Courier = new Courier();
                _Courier.Id = 2;
                _Courier.Name = "CORREO";
                _Courier.Country = "URUGUAY";

                _Couriers.Add(_Courier);

                drpCourier.Items.Add("Seleccionar...");

                foreach (Courier oCourier in _Couriers)
                {
                    drpCourier.Items.Add(oCourier.Name);
                }

                if (_Shipping.Courier != null && _Shipping.Courier.Id != 0)
                {
                    //CARGO COMBOS STATE Y CITY

                    var clientState = new RestClient("http://api.enviosmilgenial.com/statesByCourier/" + _Shipping.Courier.Id);
                    clientState.Timeout = -1;
                    var requestState = new RestRequest(Method.GET);
                    IRestResponse responseState = clientState.Execute(requestState);

                    _States = JsonConvert.DeserializeObject<List<string>>(responseState.Content);

                    drpState.Items.Add("Seleccionar...");

                    foreach (string _State in _States)
                    {
                        drpState.Items.Add(_State);
                    }
                }

                txtOrderId.Text = _Shipping.OrderId;
                txtCreatedAt.Text = _Shipping.CreatedAt.ToShortDateString();

                txtInfo.Text = _Shipping.Info;

                txtNote.Text = _Shipping.Note;

                drpCourier.Text = _Shipping.CourierName;


                txtName.Text = _Shipping.Receiver.Name;
                txtLastName.Text = _Shipping.Receiver.Lastname;
                txtPhone.Text = _Shipping.Receiver.Phone;
                txtEmail.Text = _Shipping.Receiver.Email;
                txtPassport.Text = _Shipping.Receiver.Passport;


                txtLine1.Text = _Shipping.Receiver.AddressLine1;

                chkCashOnDelivery.Checked = _Shipping.CashOnDelivery;

                if (_Shipping.Receiver.Address.Locality != null && _Shipping.Receiver.Address.Locality.Id != 0)
                {
                    drpState.Text = _Shipping.Receiver.Address.Locality.State;


                    var clientCity = new RestClient("http://api.enviosmilgenial.com/citiesByCourierState/Courier=" + _Shipping.Courier.Id + "&State=" + _Shipping.Receiver.Address.Locality.State);
                    clientCity.Timeout = -1;
                    var requestCity = new RestRequest(Method.GET);
                    IRestResponse responseCity = clientCity.Execute(requestCity);

                    _Cities = JsonConvert.DeserializeObject<List<string>>(responseCity.Content);

                    drpCity.Items.Add("Seleccionar...");

                    foreach (string _City in _Cities)
                    {
                        drpCity.Items.Add(_City);
                    }

                    drpCity.Text = _Shipping.Receiver.Address.Locality.City;



                    var clientLocality = new RestClient("http://api.enviosmilgenial.com/localitiesByCourierStateCity/Courier=" + _Shipping.Courier.Id + "&State=" + _Shipping.Receiver.Address.Locality.State + "&City=" + _Shipping.Receiver.Address.Locality.City);
                    clientLocality.Timeout = -1;
                    var requestLocality = new RestRequest(Method.GET);
                    IRestResponse responseLocality = clientLocality.Execute(requestLocality);

                    _Localities = JsonConvert.DeserializeObject<List<string>>(responseLocality.Content);

                    drpLocality.Items.Add("Seleccionar...");

                    foreach (string _Locality in _Localities)
                    {
                        drpLocality.Items.Add(_Locality);
                    }

                    drpLocality.Text = _Shipping.Receiver.Address.Locality.Name;
                }

                grdPackages.DataSource = _Shipping.Packages;
                grdPackages.AutoGenerateColumns = false;
                grdPackages.DataBind();

                if (_Shipping.Labels.Count > 0)
                {
                    btnGenerarEtiqueta.Enabled = false;

                    File.WriteAllBytes(Server.MapPath("~/Label/Label.pdf"), _Shipping.Labels[0].Data);


                    pdfiframe.Visible = true;
                    pdfiframe.Src = "../Label/Label.pdf";
                }

                else
                {

                    pdfiframe.Visible = false;
                }
            }

            catch (Exception ex)
            {
                lblError.Text = "Error: " + ex.Message;
            }
        }

        protected void btnGuardar_OnClick(object sender, EventArgs e)
        {
            try
            {
                _Shipping.Receiver.Name = txtName.Text.Trim();
                _Shipping.Receiver.Lastname = txtLastName.Text.Trim();
                _Shipping.Receiver.Phone = txtPhone.Text.Trim();
                _Shipping.Receiver.Email = txtEmail.Text.Trim();
                _Shipping.Receiver.Passport = txtPassport.Text.Trim();
                _Shipping.Info = txtInfo.Text;
                _Shipping.Note = txtNote.Text;

                _Shipping.Receiver.Address.Line1 = txtLine1.Text.Trim();
                _Shipping.Receiver.Address.Locality.State = drpState.Text.Trim();
                _Shipping.Receiver.Address.Locality.City = drpCity.Text.Trim();
                _Shipping.Receiver.Address.Locality.Name = drpLocality.Text.Trim();

                _Shipping.Courier = _Couriers[drpCourier.SelectedIndex - 1];

                _Shipping.CashOnDelivery = chkCashOnDelivery.Checked;


                var client = new RestClient("http://api.enviosmilgenial.com/shippings");
                client.Timeout = -1;
                var request = new RestRequest(Method.PUT);
                request.AddHeader("Content-Type", "application/json");


                var body = JsonConvert.SerializeObject(_Shipping);

                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

                _Shipping = JsonConvert.DeserializeObject<Shipping>(response.Content);;

                //Response.Redirect(_PreviousPage,false);

                LoadDetail();

                lblError.Text = "";
            }

            catch(Exception ex)
            {


            }
        }


        protected void btnCancelar_OnClick(object sender, EventArgs e)
        {
            try
            {

                Response.Redirect(_PreviousPage, false);
            }

            catch
            {


            }
        }
        protected void drpCourier_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                if (drpCourier.Text != "Seleccionar...")
                {
                    Courier _Courier = _Couriers.Find(x => x.Name.Trim().Equals(drpCourier.Text.Trim()));

                    var clientState = new RestClient("http://api.enviosmilgenial.com/statesByCourier/" + _Courier.Id);
                    clientState.Timeout = -1;
                    var requestState = new RestRequest(Method.GET);
                    IRestResponse responseState = clientState.Execute(requestState);

                    _States = JsonConvert.DeserializeObject<List<string>>(responseState.Content);

                    drpState.Items.Clear();

                    drpState.Items.Add("Seleccionar...");

                    foreach (string _State in _States)
                    {
                        drpState.Items.Add(_State);
                    }

                    
                    drpCity.Items.Clear();
                    drpCity.Items.Add("Seleccionar...");


                    drpLocality.Items.Clear();
                    drpLocality.Items.Add("Seleccionar...");


                    drpState.Text = "Seleccionar...";


                }
            }

            catch
            {

            }
        }

        protected void drpState_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpState.Text != "Seleccionar...")
                {
                    drpCity.Items.Clear();

                    Courier _Courier = _Couriers.Find(x => x.Name.Trim().Equals(drpCourier.Text.Trim()));

                    string _State = drpState.Text.Trim();

                    var clientCity = new RestClient("http://api.enviosmilgenial.com/citiesByCourierState/Courier=" + _Courier.Id + "&State=" + _State);
                    clientCity.Timeout = -1;
                    var requestCity = new RestRequest(Method.GET);
                    IRestResponse responseCity = clientCity.Execute(requestCity);

                    _Cities = JsonConvert.DeserializeObject<List<string>>(responseCity.Content);

                    drpCity.Items.Add("Seleccionar...");

                    foreach (string _City in _Cities)
                    {
                        drpCity.Items.Add(_City);
                    }

                    drpLocality.Items.Clear();
                    drpLocality.Items.Add("Seleccionar...");

                }
            }

            catch
            {

            }
        }

        protected void drpCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpCity.Text != "Seleccionar...")
                {
                    drpLocality.Items.Clear();


                    Courier _Courier = _Couriers.Find(x => x.Name.Trim().Equals(drpCourier.Text.Trim()));

                    string _State = drpState.Text.Trim();

                    string _City = drpCity.Text.Trim();

                    var clientLocality = new RestClient("http://api.enviosmilgenial.com/localitiesByCourierStateCity/Courier=" + _Courier.Id + "&State=" + _State + "&City=" + _City);
                    clientLocality.Timeout = -1;
                    var requestLocality = new RestRequest(Method.GET);
                    IRestResponse responseLocality = clientLocality.Execute(requestLocality);

                    _Localities = JsonConvert.DeserializeObject<List<string>>(responseLocality.Content);

                    drpLocality.Items.Add("Seleccionar...");

                    foreach (string _Locality in _Localities)
                    {
                        drpLocality.Items.Add(_Locality);
                    }
                }
            }

            catch
            {

            }
        }

        protected void btnGenerarEtiqueta_OnClick(object sender, EventArgs e)
        {
            try
            {

                if (_Shipping.FinancialStatus.Trim() == "paid")
                {
                    if (_Shipping.Receiver.Address.Locality.Id != 0)
                    {
                        var client = new RestClient("http://api.enviosmilgenial.com/shippings/" + _Shipping.OrderId + "/labels");
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

                            lblError.Text = "";

                            LoadDetail();
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
                lblError.Text = "Error: " + ex.Message;
            }

        }
    }
}