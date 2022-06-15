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

        static List<PostOffice> _Offices;

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

                    var clientState = new RestClient("https://api.enviosmilgenial.com/statesByCourier/" + _Shipping.Courier.Id);
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

                if (_Shipping.GuideType != null)
                {
                    if (_Shipping.GuideType.Id == 2)
                    {
                        rbtCuentaCorriente.Checked = true;
                    }

                    else if (_Shipping.GuideType.Id == 6)
                    {
                        rbtContrarembolso.Checked = true;
                    }

                    else if (_Shipping.GuideType.Id == 4)
                    {
                        rbtFleteDestino.Checked = true;
                    }

                }

                //chkCashOnDelivery.Checked = _Shipping.CashOnDelivery;

                if (_Shipping.DeliveryType == null || _Shipping.DeliveryType.Id == 0 || _Shipping.DeliveryType.Id == 2)
                {
                    //rbtADomicilio.Checked = true;


                    if (_Shipping.Receiver.Address.Locality != null && _Shipping.Receiver.Address.Locality.Id != 0)
                    {
                        drpState.Text = _Shipping.Receiver.Address.Locality.State;


                        var clientCity = new RestClient("https://api.enviosmilgenial.com/citiesByCourierState/Courier=" + _Shipping.Courier.Id + "&State=" + _Shipping.Receiver.Address.Locality.State);
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



                        var clientLocality = new RestClient("https://api.enviosmilgenial.com/localitiesByCourierStateCity/Courier=" + _Shipping.Courier.Id + "&State=" + _Shipping.Receiver.Address.Locality.State + "&City=" + _Shipping.Receiver.Address.Locality.City);
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
                }

                else if (_Shipping.DeliveryType.Id == 1)
               {
                    //ACA VA EL CODIGO PARA MOSTRAR UN ENVIO A OFICINA

                    //rbtOficina.Checked = true;
                    //rbtOficina.AutoPostBack = true;

                    string _State = "Seleccionar...";

                    if (_Shipping.PostOffice != null && _Shipping.PostOffice.Id!=0)
                    {
                        _State = _Shipping.PostOffice.Address.Locality.State;
                    }

                    else if (_Shipping.Receiver.Address.Locality != null)
                    {
                        _State = _Shipping.Receiver.Address.Locality.State;
                    }

                    drpState.SelectedValue = _State;

                    drpPostOffices.Items.Clear();

                    _Courier = _Couriers.Find(x => x.Name.Trim().Equals(drpCourier.Text.Trim()));


                    var clientOffices = new RestClient("https://api.enviosmilgenial.com/PostOffices/State=" + _State + "&Courier=" + _Courier.Id);
                    clientOffices.Timeout = -1;
                    var requestOffices = new RestRequest(Method.GET);
                    IRestResponse responseOffices = clientOffices.Execute(requestOffices);

                    _Offices = JsonConvert.DeserializeObject<List<PostOffice>>(responseOffices.Content);

                    drpPostOffices.Items.Add("Seleccionar...");

                    if (_Offices != null)
                    {
                        foreach (PostOffice _Office in _Offices)
                        {
                            drpPostOffices.Items.Add(_Office.Name);
                        }
                    }


                    drpPostOffices.Text = _Shipping.PostOffice.Name;
                }

                


                grdPackages.DataSource = _Shipping.Packages;
                grdPackages.AutoGenerateColumns = false;
                grdPackages.DataBind();

                if (_Shipping.Labels.Count > 0)
                {
                    //btnGenerarEtiqueta.Enabled = false;

                    File.WriteAllBytes(Server.MapPath("~/Label/Label.pdf"), _Shipping.Labels[0].Data);


                    pdfiframe.Visible = true;
                    //pdfiframe.Src = "../Label/Label.pdf";
                }

                else
                {

                    pdfiframe.Visible = false;
                }


                //ESTE CODIGO SE PUSO PARA MOSTRAR COMBOS SEGUN EL DELIVERYTYPE
                if (_Shipping.DeliveryType == null || _Shipping.DeliveryType.Id == 0 || _Shipping.DeliveryType.Id == 2)
                {
                    rbtADomicilio.Checked = true;


                    lblOficina.Visible = false;
                    drpPostOffices.Visible = false;

                    lblCiudad.Visible = true;
                    drpCity.Visible = true;
                    lblLocalidad.Visible = true;
                    drpLocality.Visible = true;
                }

                else if(_Shipping.DeliveryType.Id==1)
                {
                    rbtOficina.Checked = true;

                    lblOficina.Visible = true;
                    drpPostOffices.Visible = true;

                    lblCiudad.Visible = false;
                    drpCity.Visible = false;
                    lblLocalidad.Visible = false;
                    drpLocality.Visible = false;
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




                _Shipping.DeliveryType = new DeliveryType();


                if (rbtADomicilio.Checked)
                {
                    if (_Shipping.Receiver.Address.Locality == null)
                        _Shipping.Receiver.Address.Locality = new Locality();


                    _Shipping.Receiver.Address.Locality.State = drpState.Text.Trim();

                    _Shipping.Receiver.Address.Locality.City = drpCity.Text.Trim();

                    _Shipping.Receiver.Address.Locality.Name = drpLocality.Text.Trim();

                    _Shipping.Courier = _Couriers[drpCourier.SelectedIndex - 1];



                    _Shipping.DeliveryType.Id = 2;
                    _Shipping.DeliveryType.Name = "SERVICIO A DOMICILIO";


                }

                else if(rbtOficina.Checked)
                {
                    if (drpPostOffices.SelectedValue != "" && drpPostOffices.SelectedValue != "Seleccionar...")
                    {
                        _Shipping.PostOffice = _Offices.Where(x => x.Name.Equals(drpPostOffices.SelectedValue.Trim())).Single();
                    }

                    else
                    {
                        _Shipping.PostOffice = null;
                    }

                    _Shipping.DeliveryType.Id = 1;
                    _Shipping.DeliveryType.Name = "AGENCIA";

                }



                //PARA CONTEMPLAR EL TIPO DE ENVIO DE LA MISMA FORMA QUE CON EL TIPO DE GUIA, ESTO VA A CAMBIAR CUANDO INTEGREMOS EL CORREO

  


                _Shipping.GuideType = new GuideType();

                if (rbtCuentaCorriente.Checked)
                {
                    _Shipping.GuideType.Id = 2;
                    _Shipping.CashOnDelivery = false;
                }

                else if (rbtContrarembolso.Checked)
                {
                    _Shipping.GuideType.Id = 6;
                    _Shipping.CashOnDelivery = true;
                }

                else if (rbtFleteDestino.Checked)
                {
                    _Shipping.CashOnDelivery = false;
                    _Shipping.GuideType.Id=4;
                }

                
                //_Shipping.CashOnDelivery = chkCashOnDelivery.Checked;


                var client = new RestClient("https://api.enviosmilgenial.com/shippings");
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

                    var clientState = new RestClient("https://api.enviosmilgenial.com/statesByCourier/" + _Courier.Id);
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
                    if (rbtADomicilio.Checked)
                    {
                        drpCity.Items.Clear();

                        Courier _Courier = _Couriers.Find(x => x.Name.Trim().Equals(drpCourier.Text.Trim()));

                        string _State = drpState.Text.Trim();

                        var clientCity = new RestClient("https://api.enviosmilgenial.com/citiesByCourierState/Courier=" + _Courier.Id + "&State=" + _State);
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

                    else if (rbtOficina.Checked)
                    {
                        //ACA VA EL CODIGO PARA BUSCAR LAS OFICINAS POR COURIER Y STATE

                        string _State = drpState.Text.Trim();

                        if (_State != "Seleccionar...")
                        {
                            drpPostOffices.Items.Clear();

                            Courier _Courier = _Couriers.Find(x => x.Name.Trim().Equals(drpCourier.Text.Trim()));


                            var clientOffices = new RestClient("https://api.enviosmilgenial.com/PostOffices/State=" + _State + "&Courier=" + _Courier.Id);
                            clientOffices.Timeout = -1;
                            var requestOffices = new RestRequest(Method.GET);
                            IRestResponse responseOffices = clientOffices.Execute(requestOffices);

                            _Offices = JsonConvert.DeserializeObject<List<PostOffice>>(responseOffices.Content);

                            drpPostOffices.Items.Add("Seleccionar...");

                            foreach (PostOffice _Office in _Offices)
                            {
                                drpPostOffices.Items.Add(_Office.Name);
                            }

                        }

                    }
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

                    var clientLocality = new RestClient("https://api.enviosmilgenial.com/localitiesByCourierStateCity/Courier=" + _Courier.Id + "&State=" + _State + "&City=" + _City);
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
                    //_Shipping.Receiver.Address.Locality.State = drpState.Text.Trim();
                    //_Shipping.Receiver.Address.Locality.City = drpCity.Text.Trim();
                    //_Shipping.Receiver.Address.Locality.Name = drpLocality.Text.Trim();




                    if (rbtADomicilio.Checked)
                    {
                        if (_Shipping.Receiver.Address.Locality == null)
                            _Shipping.Receiver.Address.Locality = new Locality();


                        _Shipping.Receiver.Address.Locality.State = drpState.Text.Trim();

                        _Shipping.Receiver.Address.Locality.City = drpCity.Text.Trim();

                        _Shipping.Receiver.Address.Locality.Name = drpLocality.Text.Trim();

                        _Shipping.Courier = _Couriers[drpCourier.SelectedIndex - 1];
                    }

                    else if (rbtOficina.Checked)
                    {
                        if (drpPostOffices.SelectedValue != "" && drpPostOffices.SelectedValue != "Seleccionar...")
                        {
                            _Shipping.PostOffice = _Offices.Where(x => x.Name.Equals(drpPostOffices.SelectedValue.Trim())).Single();
                        }
                    }






                    _Shipping.Courier = _Couriers[drpCourier.SelectedIndex - 1];

                    _Shipping.GuideType = new GuideType();

                    if (rbtCuentaCorriente.Checked)
                    {
                        _Shipping.GuideType.Id = 2;
                        _Shipping.CashOnDelivery = false;
                    }

                    else if (rbtContrarembolso.Checked)
                    {
                        _Shipping.GuideType.Id = 6;
                        _Shipping.CashOnDelivery = true;
                    }

                    else if (rbtFleteDestino.Checked)
                    {
                        _Shipping.CashOnDelivery = false;
                        _Shipping.GuideType.Id = 4;
                    }

                    //PARA CONTEMPLAR EL TIPO DE ENVIO DE LA MISMA FORMA QUE CON EL TIPO DE GUIA, ESTO VA A CAMBIAR CUANDO INTEGREMOS EL CORREO

                    _Shipping.DeliveryType = new DeliveryType();

                    if (rbtADomicilio.Checked)
                    {
                        _Shipping.DeliveryType.Id = 2;
                        _Shipping.DeliveryType.Name = "SERVICIO A DOMICILIO";
                    }

                    else if(rbtOficina.Checked)
                    {
                        _Shipping.DeliveryType.Id = 1;
                        _Shipping.DeliveryType.Name = "AGENCIA";
                    }


                    var client = new RestClient("https://api.enviosmilgenial.com/shippings");
                    client.Timeout = -1;
                    var request = new RestRequest(Method.PUT);
                    request.AddHeader("Content-Type", "application/json");


                    var body = JsonConvert.SerializeObject(_Shipping);

                    request.AddParameter("application/json", body, ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);


                    //OJO CON ESTE COMMENT, se comentó porque el endpoint esta dando internal server error, hay que revisar

                    _Shipping = JsonConvert.DeserializeObject<Shipping>(response.Content); 


                }

                catch(Exception ex)
                {
                    throw new Exception("Hay un prublema con los datos del cliente.");
                }

                //_Shipping.CashOnDelivery = chkCashOnDelivery.Checked;




                if (_Shipping.FinancialStatus.Trim() == "paid"||_Shipping.CashOnDelivery)
                {
                    if ((_Shipping.Receiver.Address.Locality != null &&_Shipping.Receiver.Address.Locality.Id != 0 ) || (_Shipping.PostOffice!=null && _Shipping.PostOffice.Id!=0))
                    {
                        var clientLabel = new RestClient("https://api.enviosmilgenial.com/shippings/" + _Shipping.OrderId + "/labels");
                        clientLabel.Timeout = -1;
                        var requestLabel = new RestRequest(Method.POST);
                        IRestResponse responseLabel = clientLabel.Execute(requestLabel);


                        if (responseLabel.StatusCode == System.Net.HttpStatusCode.OK)
                        {

                            _Shipping = JsonConvert.DeserializeObject<Shipping>(responseLabel.Content);

                            File.WriteAllBytes(Server.MapPath("~/Label/Label.pdf"), _Shipping.Labels[0].Data);

                            //Response.Write("<script>");
                            //Response.Write("window.open('ShowPDF.aspx','_blank')");

                            //Response.Write("</script>");

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

                            Newtonsoft.Json.Linq.JObject _JsonResponse = Newtonsoft.Json.Linq.JObject.Parse(responseLabel.Content);

                            string _Message = (string)_JsonResponse.SelectToken("data");


                            lblError.Text = "Error: "+_Message;
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

        protected void rbtOficina_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtOficina.Checked)
            {

                string _State = drpState.Text.Trim();

                if (_State != "Seleccionar...")
                {
                    drpPostOffices.Items.Clear();

                    Courier _Courier = _Couriers.Find(x => x.Name.Trim().Equals(drpCourier.Text.Trim()));


                    var clientOffices = new RestClient("https://api.enviosmilgenial.com/PostOffices/State=" + _State + "&Courier=" + _Courier.Id);
                    clientOffices.Timeout = -1;
                    var requestOffices = new RestRequest(Method.GET);
                    IRestResponse responseOffices = clientOffices.Execute(requestOffices);

                    _Offices = JsonConvert.DeserializeObject<List<PostOffice>>(responseOffices.Content);

                    drpPostOffices.Items.Add("Seleccionar...");

                    foreach (PostOffice _Office in _Offices)
                    {
                        drpPostOffices.Items.Add(_Office.Name);
                    }

                }

                if (_Shipping.PostOffice != null && _Shipping.PostOffice.Id != 0 && _Shipping.PostOffice.Address.Locality.State.Trim() == _State.Trim())
                    drpPostOffices.SelectedValue = _Shipping.PostOffice.Name;

                lblOficina.Visible = true;
                drpPostOffices.Visible = true;

                lblCiudad.Visible = false;
                drpCity.Visible = false;
                lblLocalidad.Visible = false;
                drpLocality.Visible = false;

            }

        }

        protected void rbtADomicilio_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtADomicilio.Checked)
            {
                if (_Shipping.Receiver.Address.Locality != null && _Shipping.Receiver.Address.Locality.Id != 0)
                {
                    drpCity.Items.Clear();
                    drpLocality.Items.Clear();

                    drpState.Text = _Shipping.Receiver.Address.Locality.State;


                    var clientCity = new RestClient("https://api.enviosmilgenial.com/citiesByCourierState/Courier=" + _Shipping.Courier.Id + "&State=" + _Shipping.Receiver.Address.Locality.State);
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



                    var clientLocality = new RestClient("https://api.enviosmilgenial.com/localitiesByCourierStateCity/Courier=" + _Shipping.Courier.Id + "&State=" + _Shipping.Receiver.Address.Locality.State + "&City=" + _Shipping.Receiver.Address.Locality.City);
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

                lblOficina.Visible = false;
                drpPostOffices.Visible = false;

                lblCiudad.Visible = true;
                drpCity.Visible = true;
                lblLocalidad.Visible = true;
                drpLocality.Visible = true;
            }

        }
    }
}