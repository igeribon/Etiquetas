using API.DataTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

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
            

            try
            {
                string _OrderId = pShipping.name.Replace("#", "").Trim();
                //DATOS SHIPPING

                _Shipping = GetShippingByOrderId(_OrderId);

                _Shipping.OrderId = pShipping.name.Replace("#", "").Trim();

                _Shipping.FinancialStatus = pShipping.financial_status;

                _Shipping.CreatedAt = pShipping.created_at;

                _Shipping.TotalLinesItemPrice = Convert.ToDouble(pShipping.total_line_items_price.Replace(".", ","));

             

                //DATOS RECEIVER
                //_Shipping.Receiver = new Receiver();
                _Shipping.Receiver.Name = pShipping.customer.first_name;
                _Shipping.Receiver.Lastname = pShipping.customer.last_name;
                _Shipping.Receiver.Email = Convert.ToString(pShipping.customer.email);

                if (pShipping.customer.phone != "" && pShipping.customer.phone != null)
                    _Shipping.Receiver.Phone = pShipping.customer.phone;

                else if (pShipping.customer.default_address.phone != "" && pShipping.customer.default_address.phone != null)
                    _Shipping.Receiver.Phone = pShipping.customer.default_address.phone;


                //DATOS RECEIVER ADDRESS
                //_Shipping.Receiver.Address = new Address();
                _Shipping.Receiver.Address.Line1 = pShipping.shipping_address.address1;



                //DATOS COURIER

               

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

                        break;
                    }

                    else
                    {
                        _Shipping.CashOnDelivery = false;
                        _Shipping.Info = _ShippingLine.title;
                        break;
                    }
                }



                if (_Shipping.Courier != null)
                {
                    //DATOS RECEIVER ADDRESS LOCALITY
                    _Shipping.Receiver.Address.Locality = ShippingController.GetLocalityByCourierNameCity(pShipping.shipping_address.zip, pShipping.shipping_address.city, _Shipping.Courier);
                }

                    
                

                //CREO PACKAGES
                //_Shipping.Packages = new List<Package>();

                foreach (LineItem _LineItem in pShipping.line_items)
                {
                    Package _Package = new Package();
                    _Package.Depth = 15;
                    _Package.Height = 15;
                    _Package.Weight = _LineItem.grams;
                    _Package.Width = 15;
                    _Package.Reference = _LineItem.name;

                    _Shipping.Packages.Add(_Package);

                }


                if (_Shipping.Id == 0)
                {
                    ShippingController.InsertShipping(_Shipping);
                }

                else 
                {

                    ShippingController.UpdateShipping(_Shipping);
                }

            }

            catch (Exception)
            {

                return StatusCode(500);
            }





            return Ok(_Shipping);
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
        public Shipping CreateLabel(string pId)
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
                throw ex;
            }

            return _Shipping;
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

    }
}
