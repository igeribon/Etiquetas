using API.DataTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DataAccess;

namespace API.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class DacServiceController : ControllerBase
    {
        //METODOS INTERFAZ

        
        public Shipping CreateShipping(Shipping pShipping)
        {
            Shipping _Shipping = pShipping;

            return _Shipping;
        }


        //MÉTODOS COURIER


        public string Login(string pUsername, string pPassword)
        {

            string _IDSession = "";

            try
            {
                pUsername = "99090";
                pPassword = "99090";

                var client = new RestClient("http://altis-web.grupoagencia.com:8082/JAgencia.asmx/wsLogin?Login=" + pUsername + "&Contrasenia=" + pPassword);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);

                string _Respuesta = response.Content;


                Newtonsoft.Json.Linq.JObject _Json = Newtonsoft.Json.Linq.JObject.Parse(_Respuesta);


                _IDSession = (string)_Json["data"][0]["ID_Session"];
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return _IDSession;

        }


        public List<Locality> GetLocalitiesDAC(string pLocalityName)
        {

            List<Locality> _Localities = new List<Locality>();

            Courier _Courier = new Courier();

            _Courier.Id = 1;
            _Courier.Name = "DAC";
            _Courier.Country = "Uruguay";

            try
            {
                string _IDSession = Login("", "");



                var client = new RestClient("http://altis-web.grupoagencia.com:8082/JAgencia.asmx/wsBarrio?ID_Sesion=" + _IDSession + "&Barrrio=" + pLocalityName);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);

                Newtonsoft.Json.Linq.JObject _Json = Newtonsoft.Json.Linq.JObject.Parse(response.Content);


                foreach (var x in _Json["data"])
                {
                    Locality _Locality = new Locality();
                    _Locality.Courier = _Courier;
                    _Locality.City = (string)x.SelectToken("D_Ciudad");
                    _Locality.Name = (string)x.SelectToken("D_Barrio");
                    _Locality.State = (string)x.SelectToken("D_Estado");
                    _Locality.ZIP = (string)x.SelectToken("Codigo_Postal");
                    _Locality.Code = (string)x.SelectToken("K_Barrio");
                    _Locality.CityCode = (string)x.SelectToken("K_Ciudad");
                    _Locality.StateCode = (string)x.SelectToken("K_Estado");

                    _Localities.Add(_Locality);
                }
            }

            catch
            {

            }


            return _Localities;
        }




        
        public List<PostOffice> GetOfficesDAC(int pId)
        {

            List<PostOffice> _PostOffices = new List<PostOffice>();
            Courier _Courier = new Courier();
            _Courier.Id = 1;
            _Courier.Name = "DAC";
            _Courier.Country = "URUGUAY";

            try
            {
                string _IDSession = Login("", "");


                var client = new RestClient("http://altis-web.grupoagencia.com:8082/JAgencia.asmx/wsOficina?ID_Sesion="+_IDSession+"&K_Oficina="+pId);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);

                Newtonsoft.Json.Linq.JObject _Json = Newtonsoft.Json.Linq.JObject.Parse(response.Content);


                foreach (var x in _Json["data"])
                {
                    PostOffice _Office = new PostOffice();

                    _Office.Id = (int)x.SelectToken("K_Oficina");
                    _Office.Name = (string)x.SelectToken("D_Oficina");

                    _Office.Address = new Address();
                    _Office.Address.Line1 = (string)x.SelectToken("Calle");
                    _Office.Address.Phone = (string)x.SelectToken("Telefono");
                    _Office.Address.Line2 = "";
                    _Office.Address.Observation = "";

                    _Office.Address.Locality = GetLocalityByCourierNameCity((string)x.SelectToken("D_Barrio"), (string)x.SelectToken("D_Ciudad"), _Courier);



                    _PostOffices.Add(_Office);
                }
            }

            catch(Exception ex)
            {
                throw ex;
            }


            return _PostOffices;
        }





       // [HttpGet("localities/insert")]
        public string InsertLocalities()
        {
            string _Retorno="";

            List<Locality> _Localities = new List<Locality>();

            try
            {
                _Localities = GetLocalitiesDAC("");

                foreach (Locality _Locality in _Localities)
                {
                    DataAccess.DAShipping.InsertLocality(_Locality);
                }

                _Retorno = "INSERCIÓN COMPLETA!!!";

            }

            catch(Exception ex)
            {
                _Retorno = ex.Message;
            }


            return _Retorno;
        }

      //  [HttpGet("offices/insert")]
        public string InsertOffices()
        {

            List<PostOffice> _PostOffices = new List<PostOffice>();
            string _Retorno = "";


            try
            {
                Courier _Courier = new Courier();
                _Courier.Id = 1;
                _Courier.Name = "DAC";
                _Courier.Country = "Uruguay";


                _PostOffices = GetOfficesDAC(0);

                foreach (PostOffice _PostOffice in _PostOffices)
                {
                    _PostOffice.Courier = _Courier;
                    DataAccess.DAShipping.InsertPostOffice(_PostOffice);
                }

                _Retorno = "INSERCIÓN COMPLETA!!!";

            }

            catch (Exception ex)
            {
                _Retorno = ex.Message;
            }

            return _Retorno;

        }


        public static Locality GetLocalityByCourierNameCity(string pName, string pCity, Courier pCourier)
        {
            Locality _Locality=new Locality();

           try
            {
                _Locality = DataAccess.DAShipping.GetLocalityByCourierNameCity(pName, pCity, pCourier);

                if(_Locality==null)
                    _Locality = DataAccess.DAShipping.GetLocalityByCourierNameState(pName, pCity, pCourier);


            }

            catch (Exception ex)
            {
                throw ex;
            }

     
            return _Locality;
        }


  
        public static List<DeliveryType> GetDeliveryTypes()
        {
            List<DeliveryType> _DeliveryTypes = new List<DeliveryType>();


            DeliveryType _DeliveryType = new DeliveryType();
            _DeliveryType.Id = 1;
            _DeliveryType.Name = "AGENCIA";

            _DeliveryTypes.Add(_DeliveryType);


            _DeliveryType = new DeliveryType();
            _DeliveryType.Id = 2;
            _DeliveryType.Name = "SERVICIO A DOMICILIO";

            _DeliveryTypes.Add(_DeliveryType);


            return _DeliveryTypes;
        }


     
        public static List<GuideType> GetGuideTypes()
        {
            List<GuideType> _TiposGuia = new List<GuideType>();


            GuideType _GuideType = new GuideType();
            _GuideType.Id = 1;
            _GuideType.Name = "FLETE PAGADO";

            _TiposGuia.Add(_GuideType);


            _GuideType = new GuideType();
            _GuideType.Id = 2;
            _GuideType.Name = "CUENTA CORRIENTE";

            _TiposGuia.Add(_GuideType);



            _GuideType = new GuideType();
            _GuideType.Id = 4;
            _GuideType.Name = "FLETE DESTINO";

            _TiposGuia.Add(_GuideType);



            _GuideType = new GuideType();
            _GuideType.Id = 6;
            _GuideType.Name = "CONTRAREMBOLSO";

            _TiposGuia.Add(_GuideType);


            return _TiposGuia;

        }
    }
}
