using API.DataTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase , IServiceController
    {
        

        [HttpPost("shippings")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Shipping))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateShipping(DTOShipping pShipping)
        {
            Shipping _Shipping = null;

            JObject _Log = new JObject();

            string _LogMessage = "";


            try
            {

                _Log = Newtonsoft.Json.Linq.JObject.FromObject(pShipping);

                ////SE CAMBIO pShipping.Name por pShipping.ordcer_number

                string _OrderId = pShipping.name.Replace("#", "").Trim();

                //string _OrderId = pShipping.order_number.ToString();

                //DATOS SHIPPING

                _Shipping = GetShippingByOrderId(_OrderId);

                _Shipping.OrderId = _OrderId.Replace("#", "").Trim();

                _Shipping.FinancialStatus = pShipping.financial_status;

                try
                {
                    if (pShipping.created_at != null)
                        _Shipping.CreatedAt = Convert.ToDateTime(pShipping.created_at);
                }

                catch
                {

                }


                //SE QUITO EL REEMPLAZO DE PUNTOS POR COMAS EN TotalLinesItemPrice POR LA CONFIGURACION DE IDIOMA DEL SERVIDOR
                //_Shipping.TotalLinesItemPrice = Convert.ToDouble(pShipping.total_line_items_price.Replace(".", ","));

                try
                {

                    _Shipping.TotalLinesItemPrice = Convert.ToDouble(pShipping.total_line_items_price);
                }

                catch
                {

                }

                if (pShipping.note != null)
                    _Shipping.Note = Convert.ToString(pShipping.note);

                else
                    _Shipping.Note = "";


                //DATOS RECEIVER
                if (_Shipping.Receiver == null)
                    _Shipping.Receiver = new Receiver();


                if (pShipping.customer != null)
                {
                    _Shipping.Receiver.Name = pShipping.customer.first_name;
                    _Shipping.Receiver.Lastname = pShipping.customer.last_name;
                    _Shipping.Receiver.Email = pShipping.customer.email;

                    if (_Shipping.Receiver.Email == null)
                        _Shipping.Receiver.Email = "";


                    if (pShipping.customer.phone != "" && pShipping.customer.phone != null)
                        _Shipping.Receiver.Phone = pShipping.customer.phone.ToString();

                    else if (pShipping.customer.default_address.phone != "" && pShipping.customer.default_address.phone != null)
                        _Shipping.Receiver.Phone = pShipping.customer.default_address.phone;


                    if (_Shipping.Receiver.Phone == null)
                        _Shipping.Receiver.Phone = "";
                }

                //DATOS RECEIVER ADDRESS
                //_Shipping.Receiver.Address = new Address();

                if (pShipping.shipping_address != null)
                    _Shipping.Receiver.Address.Line1 = pShipping.shipping_address.address1;



                //DATOS COURIER

                if (pShipping.shipping_lines != null)
                {
                    foreach (ShippingLine _ShippingLine in pShipping.shipping_lines)
                    {
                        if (_ShippingLine.title.ToLower().Contains("dac"))
                        {
                            _Shipping.Courier = new Courier();
                            _Shipping.Courier.Id = 1;
                            _Shipping.Courier.Name = "DAC";
                            _Shipping.Courier.Country = "URUGUAY";

                            break;
                        }

                        else if (_ShippingLine.title.ToLower().Contains("correo"))
                        {
                            _Shipping.Courier = new Courier();
                            _Shipping.Courier.Id = 2;
                            _Shipping.Courier.Name = "CORREO";
                            _Shipping.Courier.Country = "URUGUAY";

                            break;
                        }



                    }



                    foreach (ShippingLine _ShippingLine in pShipping.shipping_lines)
                    {
                        if (_ShippingLine.title.ToLower().Contains("contra"))
                        {
                            _Shipping.CashOnDelivery = true;


                            _Shipping.Info = _ShippingLine.title;

                            if (_Shipping.Info == null)
                            {
                                _Shipping.Info = "";
                            }

                            break;
                        }

                        else
                        {
                            _Shipping.CashOnDelivery = false;

                            _Shipping.Info = _ShippingLine.title;

                            if (_Shipping.Info == null)
                            {
                                _Shipping.Info = "";
                            }

                            break;
                        }
                    }

                }

                _Shipping.GuideType = new GuideType();

                if (_Shipping.CashOnDelivery)
                {
                    _Shipping.GuideType = new GuideType();
                    _Shipping.GuideType.Id = 6;
                    _Shipping.GuideType.Name = "CONTRAREMBOLSO";
                }

                else
                {
                    _Shipping.GuideType = new GuideType();
                    _Shipping.GuideType.Id = 2;
                    _Shipping.GuideType.Name = "CUENTA CORRIENTE";
                }



                if (_Shipping.Courier != null)
                {
                    //DATOS RECEIVER ADDRESS LOCALITY
                    if (pShipping.shipping_address != null)
                        _Shipping.Receiver.Address.Locality = ShippingController.GetLocalityByCourierNameCity(pShipping.shipping_address.zip, pShipping.shipping_address.city, _Shipping.Courier);
                }




                //CREO PACKAGES
                if (_Shipping.Packages == null)
                    _Shipping.Packages = new List<Package>();

                if (pShipping.line_items != null)
                {
                    foreach (LineItem _LineItem in pShipping.line_items)
                    {
                        Package _Package = new Package();
                        _Package.Depth = 15;
                        _Package.Height = 15;

                        try
                        {
                            if (_LineItem.grams != null)
                                _Package.Weight = Convert.ToDouble(_LineItem.grams);
                        }

                        catch
                        {
                            
                        }

                        _Package.Width = 15;

                        if (_LineItem.name != null)
                            _Package.Reference = _LineItem.name;
                        else
                            _Package.Reference = "";

                        _Shipping.Packages.Add(_Package);

                    }
                }



                if (_Shipping.Id == 0)
                {
                    ShippingController.InsertShipping(_Shipping);
                }

                else
                {

                    ShippingController.UpdateShipping(_Shipping);
                }

                _LogMessage = "200";

            }

            catch (Exception ex)
            {
                _LogMessage= "500"+" - "+ex.Message;
                _Log.Add("APIResponse", _LogMessage);
                ShippingController.SaveLog(_Log.ToString(), pShipping.name.Replace("#", "").Trim() + "_" + DateTime.Now.ToString() + ".json");

                return StatusCode(500);
            }

            finally
            {
               
            }


            //SE CAMBIO PARA RETORNAR VACIO
            //return Ok(_Shipping);
            return Ok();
        }


        [HttpPut("shippings")]
        public Shipping UpdateShippingBackOffice(Shipping pShipping)
        {
            Shipping _Shipping = pShipping;


            try
            {
                if (_Shipping.Courier != null)
                {
                    //DATOS RECEIVER ADDRESS LOCALITY
                    _Shipping.Receiver.Address.Locality = ShippingController.GetLocalityByCourierNameCity(pShipping.Receiver.Address.Locality.Name, pShipping.Receiver.Address.Locality.City, _Shipping.Courier);
                }

                ShippingController.UpdateShipping(_Shipping);
            }

            catch (Exception ex)
            {
                throw ex;
            }



          
            return _Shipping;
        }


        [HttpGet("shippings/{pId}")]
        public Shipping GetShippingByOrderId(string pId)
        {
            Shipping _Shipping = new Shipping();

            try
            {
                _Shipping = ShippingController.GetShippingByOrderId(pId);
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return _Shipping;
        }


        [HttpGet("shippings/ReceiverNameLastname={pNameLastname}")]
        public List<Shipping> GetShippingsByCreatedAtFromTo(string pNameLastname)
        {
            List<Shipping> _Shippings = new List<Shipping>();

            try
            {
                _Shippings = ShippingController.GetShippingsByReceiverNameLastName(pNameLastname);
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return _Shippings;
        }


        [HttpGet("shippings/from={pFrom}&to={pTo}&hasLabel={pHasLabel}&limit={pLimit}&order={pOrder}")]
        public List<Shipping> GetShippingsByCreatedAtFromTo(DateTime pFrom, DateTime pTo, int pHasLabel, int pLimit, string pOrder)
        {
            List<Shipping> _Shippings = new List<Shipping>();

            try
            {
                _Shippings = ShippingController.GetShippingsByCreatedAtFromTo(pFrom, pTo, pHasLabel, pLimit,pOrder);
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return _Shippings;
        }


        [HttpGet("login/username={pUsername}&password={pPassword}")]

        public Account Login(string pUsername, string pPassword)
        {
            Account _Account = null;

            try
            {
                _Account = ShippingController.Login(pUsername,pPassword);
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return _Account;
        }

        [HttpPost("shippings/{pId}/labels")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Shipping))]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult CreateLabel(string pId)
        {
            Shipping _Shipping = new Shipping();

            try
            {
                _Shipping = ShippingController.GetShippingByOrderId(pId);

                if (_Shipping.Courier != null && _Shipping.Courier.Id != 0)
                {
                    if (_Shipping.Courier.Name == "DAC")
                        _Shipping = DacServiceController.CreateLabel(_Shipping);

                    else if (_Shipping.Courier.Name == "CORREO")
                        throw new Exception("Aun no contamos con integración para este courier.");
                }

                else
                {
                    throw new Exception("No se indicó courier.");
                }
            }

            catch (Exception ex)
            {
                JObject _Json = new JObject();
                _Json.Add("data", ex.Message);


                Response.ContentType = "application/json";

                return Conflict(_Json);
            }

            return Ok(_Shipping);
        }


        //GETS LOCALITIES

        [HttpGet("statesByCourier/{pCourierId}")]
        public List<string> GetStatesByCourier(int pCourierId)
        {
           List<string> _States;

            try
            {
                _States = ShippingController.GetStatesByCourier(pCourierId);
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return _States;
        }




        [HttpGet("citiesByCourierState/Courier={pCourierId}&State={pState}")]
        public List<string> GetCitiesByCourierState(int pCourierId, string pState)
        {
            List<string> _Cities;

            try
            {
                _Cities = ShippingController.GetCitiesByCourierState(pCourierId, pState);
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return _Cities;
        }



        [HttpGet("localitiesByCourierStateCity/Courier={pCourierId}&State={pState}&City={pCity}")]
        public List<string> GetLocalitiesByCourierStateCity(int pCourierId, string pState, string pCity)
        {
            List<string> _Localities;

            try
            {
                _Localities = ShippingController.GetLocalitiesByCourierStateCity(pCourierId, pState, pCity);
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return _Localities;
        }


        [HttpGet("test")]
        public string GetTest()
        {
            string _test = "";
            try
            {
                _test = ShippingController.Test();
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return _test;
        }

     



    }
}
