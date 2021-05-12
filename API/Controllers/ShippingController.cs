using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DataAccess;
using API.DataTypes;

namespace API.Controllers
{
    public class ShippingController 
    {

        public static void InsertShipping(Shipping pShipping)
        {
            try
            {
                DataAccess.DAShipping.InsertShipping(pShipping);
            }

            catch (Exception ex)
            {
                throw ex;
            }

 
        }

        public static void UpdateShipping(Shipping pShipping)
        {

            try
            {

                DataAccess.DAShipping.UpdateShipping(pShipping);
            }

            catch (Exception ex)
            {
                throw ex;
            }


        }


        public static Shipping GetShippingByOrderId(string pOrderId)
        {
            Shipping _Shipping = new Shipping();

          
            try
            {
                _Shipping = DataAccess.DAShipping.GetShippingByOrderId(pOrderId);


            }

            catch (Exception ex)
            {
                throw ex;
            }

     

            return _Shipping;
        }


        public static List<Shipping> GetShippingsByCreatedAtFromTo(DateTime pFrom, DateTime pTo)
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


        public static Account Login(string pUserName, string pPassword)
        {
            Account _Account = null;

         
            try
            {
                _Account = DataAccess.DAShipping.Login(pUserName, pPassword);

            }

            catch (Exception ex)
            {
                throw ex;
            }


            return _Account;

        }
    }
}
