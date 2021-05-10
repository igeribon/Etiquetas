﻿using API.DataTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

                //_Shipping.Courier = new Courier();

                foreach (ShippingLine _ShippingLine in pShipping.shipping_lines)
                {
                    if (_ShippingLine.title.ToLower().Contains("dac"))
                    {
                        _Shipping.Courier.Id = 1;
                        _Shipping.Courier.Name = "DAC";
                        _Shipping.Courier.Country = "URUGUAY";

                        break;
                    }

                    else if (_ShippingLine.title.ToLower().Contains("correo"))
                    {
                        _Shipping.Courier.Id = 2;
                        _Shipping.Courier.Name = "CORREO";
                        _Shipping.Courier.Country = "URUGUAY";

                        break;
                    }


                }



                //DATOS RECEIVER ADDRESS LOCALITY
                _Shipping.Receiver.Address.Locality = DacServiceController.GetLocalityByCourierNameCity(pShipping.shipping_address.zip, pShipping.shipping_address.city, _Shipping.Courier);





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
                    DataAccess.DAShipping.InsertShipping(_Shipping);
                }

                else 
                {

                    DataAccess.DAShipping.UpdateShipping(_Shipping);
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
                DataAccess.DAShipping.UpdateShipping(_Shipping);
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
                _Shipping = DataAccess.DAShipping.GetShippingByOrderId(pId);
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return _Shipping;
        }


        [HttpGet("shippings/from={pFrom}&to={pTo}")]
        public List<Shipping> GetShippingsByCreatedAtFromTo(DateTime pFrom, DateTime pTo)
        {
            List<Shipping> _Shippings = new List<Shipping>();

            try
            {
                _Shippings = DataAccess.DAShipping.GetShippingsByCreatedAtFromTo(pFrom, pTo);
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
                _Account = DataAccess.DAShipping.Login(pUsername,pPassword);
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return _Account;
        }


    }
}
