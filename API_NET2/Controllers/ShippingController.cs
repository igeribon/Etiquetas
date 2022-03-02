using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DataAccess;
using API.DataTypes;
using System.IO;
using RestSharp;

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



        public static List<Shipping> GetShippingsByReceiverNameLastName(string pNameLastname)
        {
            List<Shipping> _Shippings = new List<Shipping>();


            try
            {


                _Shippings = DataAccess.DAShipping.GetShippingsByReceiverNameLastname(pNameLastname);

            }

            catch (Exception ex)
            {
                throw ex;
            }



            return _Shippings;
        }


        public static List<Shipping> GetShippingsByCreatedAtFromTo(DateTime pFrom, DateTime pTo, int pHasLabel, int pLimit, string pOrder)
        {
            List<Shipping> _Shippings = new List<Shipping>();


            try
            {


                _Shippings = DataAccess.DAShipping.GetShippingsByCreatedAtFromTo(pFrom, pTo, pHasLabel, pLimit, pOrder);

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

        public static string Test()
        {
            string _Test = "";
            try
            {
                _Test = "ESTO ES UNA PRUEBA, LLEGAMOS AL SHIPPING CONTROLLER";
            }

            catch(Exception ex)
            {
                throw ex;
            }

            return _Test;
        

        }

        public static Locality GetLocalityByCourierNameCity(string pName, string pCity, Courier pCourier)
        {
            Locality _Locality = new Locality();

            try
            {
                bool _IsZIP;

                try
                {
                    Convert.ToInt64(pName);
                    _IsZIP = true;
                }

                catch
                {
                    _IsZIP = false;
                }



                if (_IsZIP)
                {
                    _Locality = DataAccess.DAShipping.GetLocalityByCourierZIPState(pName, pCity, pCourier);
                }

                else
                {
                    _Locality = DataAccess.DAShipping.GetLocalityByCourierNameCity(pName, pCity, pCourier);

                    if(_Locality==null)
                        _Locality = DataAccess.DAShipping.GetLocalityByCourierNameState(pName, pCity, pCourier);
                }


            }

            catch (Exception ex)
            {
                throw ex;
            }


            return _Locality;
        }

        //PARA PUT DESDE BACKOFFICE
        public static Locality GetLocalityByCourierNameCityState(string pName, string pCity, string pState, Courier pCourier)
        {
            Locality _Locality = new Locality();

            try
            {
             


             
                    _Locality = DataAccess.DAShipping.GetLocalityByCourierNameCityState(pName, pCity, pState, pCourier);
                


            }

            catch (Exception ex)
            {
                throw ex;
            }


            return _Locality;
        }

        public static List<string> GetStatesByCourier(int pCourierId)
        {
            List<string> _States;

            try
            {
                _States = DataAccess.DAShipping.GetStatesByCourier(pCourierId);


            }

            catch (Exception ex)
            {
                throw ex;
            }



            return _States;

        }


        public static List<string> GetCitiesByCourierState(int pCourierId, string pState)
        {
            List<string> _Cities;

            try
            {
                _Cities = DataAccess.DAShipping.GetCitiesByCourierState(pCourierId, pState);


            }

            catch (Exception ex)
            {
                throw ex;
            }



            return _Cities;


        }



        public static List<string> GetLocalitiesByCourierStateCity(int pCourierId, string pState, string pCity)
        {
            List<string> _Localities;

            try
            {
                _Localities = DataAccess.DAShipping.GetLocalitiesByCourierStateCity(pCourierId, pState, pCity);


            }

            catch (Exception ex)
            {
                throw ex;
            }



            return _Localities;


        }


        public static void SaveLog(string pLog, string pFileName)
        {
            
            try
            {
                //string fullPath = Path.Combine(Directory.GetCurrentDirectory(), pFileName.Replace("/","-").Replace(" ","_").Replace(":","_"));
                //string fullPathAux = fullPath.Replace("\\", "\");
                string _CurrentFolder = Path.Combine(Directory.GetCurrentDirectory());

                File.WriteAllText(_CurrentFolder + "\\APIRequestLogs\\" +pFileName.Replace("/", "-").Replace(" ", "_").Replace(":", "_"), pLog);

            }

            catch (Exception ex)
            {
                throw ex;
            }



        }


        public static void UpdateShopifyTrackingNumberWithoutFulfillment(Shipping pShipping)
        {

            try
            {
                string _api_key = "2a69d0274a99072efb7f9835117fb708";
                string _api_password = "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";
                string _store_name = "mil-genial-uruguay";
                string _api_version = "2021-07";


                string _Url = "https://" + _api_key + ":" + _api_password + "@" + _store_name + ".myshopify.com/admin/api/" + _api_version + "/orders/" + pShipping.ShopifyId + "/fulfillments.json";
                //string _Url = "https://" + _api_key + ":" + _api_password + "@" + _store_name + ".myshopify.com/admin/api/" + _api_version + "/orders/3960236114098/fulfillments.json";

                var client = new RestClient(_Url);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("X-Shopify-Access-Token", _api_password);

                var body = "{\"fulfillment\": {\r\n    \"location_id\": 49644961951,\r\n    \"tracking_company\" : \"DAC\",\r\n    \"tracking_number\": \"" + pShipping.LabelTrackingNumber+ "\",\r\n    \"tracking_urls\": [\r\n      \"https://www.dac.com.uy/envios/rastrear\"\r\n    ],\r\n    \"notify_customer\": true\r\n  }\r\n}";


                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }



        public static void UpdateShopifyTrackingNumberWithFulfillment(Shipping pShipping)
        {

            try
            {
                string _api_key = "2a69d0274a99072efb7f9835117fb708";
                string _api_password = "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";
                string _store_name = "mil-genial-uruguay";
                string _api_version = "2021-07";


                string _Url = "https://" + _api_key + ":" + _api_password + "@" + _store_name + ".myshopify.com/admin/api/" + _api_version + "/fulfillments/" + pShipping.FulfillmentId + "/update_tracking.json";

                var client = new RestClient(_Url);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("X-Shopify-Access-Token", _api_password);

                var body = "{" +
" \"fulfillment\": {" +
"\"notify_customer\": true," +
"\"tracking_info\": {" +
                    "\"number\": \"" + pShipping.LabelTrackingNumber + "\"," +
"\"url\": \"https://www.dac.com.uy/envios/rastrear\"," +
"\"company\": \"DAC\"" +
"}" +
            "}" +
        "}";

                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }




        //PARA PROBAR ENDPOINT

        public static List<PostOffice> GetGetPostOfficeByStateCourier(string pState, Courier pCourier)
        {
            List<PostOffice> _Officess = new List<PostOffice>();


            try
            {


                _Officess = DataAccess.DAShipping.GetPostOfficeByStateCourier(pState,pCourier);

            }

            catch (Exception ex)
            {
                throw ex;
            }



            return _Officess;
        }




    }
}
