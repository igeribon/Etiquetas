using API.DataTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DataAccess;
using Newtonsoft.Json.Linq;

namespace API.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class DacServiceController : ControllerBase
    {


        public static string Login(string pUsername, string pPassword)
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

                Newtonsoft.Json.Linq.JObject _Json = Newtonsoft.Json.Linq.JObject.Parse(response.Content);


                foreach (var x in _Json["data"])
                {
                    PostOffice _Office = new PostOffice();

                    _Office.Code = (int)x.SelectToken("K_Oficina");
                    _Office.Name = (string)x.SelectToken("D_Oficina");

                    _Office.Address = new Address();
                    _Office.Address.Line1 = (string)x.SelectToken("Calle");
                    _Office.Address.Phone = (string)x.SelectToken("Telefono");
                    _Office.Address.Line2 = "";
                    _Office.Address.Observation = "";

                    _Office.Address.Locality = ShippingController.GetLocalityByCourierNameCity((string)x.SelectToken("D_Barrio"), (string)x.SelectToken("D_Ciudad"), _Courier);



                    _PostOffices.Add(_Office);
                }
            }

            catch(Exception ex)
            {
                throw ex;
            }


            return _PostOffices;
        }





        //[HttpGet("localities/insert")]
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

        //[HttpGet("offices/insert")]
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

        public static Shipping CreateLabel(Shipping pShipping)
        {

            Shipping _Shipping = null;
            try
            {
                _Shipping = pShipping;
                string _IDSesionAux = Login("", "");



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
                

                JProperty _IDSesion = new JProperty("ID_Sesion", _IDSesionAux);
                JProperty _K_Cliente_Remitente = new JProperty("K_Cliente_Remitente", 1);
                JProperty _D_Cliente_Remitente = new JProperty("D_Cliente_Remitente", "Milgenial");
                JProperty _K_Cliente_Destinatario = new JProperty("K_Cliente_Destinatario", 5);
                JProperty _Cliente_Destinatario = new JProperty("Cliente_Destinatario", pShipping.Receiver.Name);
                JProperty _RUT = new JProperty("RUT", pShipping.Receiver.Passport);

                JProperty _Direccion_Destinatario;

                if (pShipping.Receiver.Address.Line1 != "")
                    _Direccion_Destinatario = new JProperty("Direccion_Destinatario", pShipping.Receiver.Address.Line1);
                else
                    _Direccion_Destinatario = new JProperty("Direccion_Destinatario", pShipping.Receiver.Address.Line2);

                JProperty _K_Barrio = new JProperty("K_Barrio", pShipping.Receiver.Address.Locality.Code);
                JProperty _K_Ciudad_Destinatario = new JProperty("K_Ciudad_Destinatario", pShipping.Receiver.Address.Locality.CityCode);
                JProperty _K_Estado_Destinatario = new JProperty("K_Estado_Destinatario", pShipping.Receiver.Address.Locality.StateCode);
                JProperty _K_Pais_Destinatario = new JProperty("K_Pais_Destinatario", 1);
                JProperty _CP_Destinatario = new JProperty("CP_Destinatario", pShipping.Receiver.Address.Locality.ZIP);
                JProperty _Telefono = new JProperty("Telefono", pShipping.Receiver.Phone);
                JProperty _K_Oficina_Destino = new JProperty("K_Oficina_Destino", 0);
                JProperty _Entrega = new JProperty("Entrega", pShipping.DeliveryType.Id);
                JProperty _Paquetes_Ampara = new JProperty("Paquetes_Ampara", pShipping.Packages.Count);
                JProperty _Chicos = new JProperty("Chicos", pShipping.Packages.Count);
                JProperty _Medianos = new JProperty("Medianos", 0);
                JProperty _Grandes = new JProperty("Grandes", 0);
                JProperty _Extragrande = new JProperty("Extragrande", 0);
                JProperty _Cartas = new JProperty("Cartas", 0);
                JProperty _Sobres = new JProperty("Sobres", 0);
                JProperty _K_Articulo = new JProperty("K_Articulo", 0);
                JProperty _Observaciones = new JProperty("Observaciones", "");
                JProperty _K_Tipo_Guia = new JProperty("K_Tipo_Guia", pShipping.GuideType.Id);

                JProperty _CostoMercaderia;
                if (pShipping.CashOnDelivery)
                    _CostoMercaderia = new JProperty("CostoMercaderia", pShipping.TotalLinesItemPrice);

                else
                    _CostoMercaderia = new JProperty("CostoMercaderia", 0);


                JProperty _Referencia_Pago = new JProperty("Referencia_Pago", "");
                JProperty _CodigoPedido = new JProperty("CodigoPedido", "Milgenial"+pShipping.OrderId);
                JProperty _Serv_DDF = new JProperty("Serv_DDF", "");

                JObject _JsonBody = new JObject(_IDSesion, _K_Cliente_Remitente, _D_Cliente_Remitente, _K_Cliente_Destinatario, _Cliente_Destinatario, _RUT, _Direccion_Destinatario, _K_Barrio, _K_Ciudad_Destinatario,
                    _K_Estado_Destinatario, _K_Pais_Destinatario, _CP_Destinatario, _Telefono, _K_Oficina_Destino, _Entrega, _Paquetes_Ampara, _Chicos, _Medianos, _Grandes, _Extragrande, _Cartas, _Sobres, _K_Articulo,
                    _Observaciones, _K_Tipo_Guia, _CostoMercaderia, _Referencia_Pago, _CodigoPedido, _Serv_DDF);


                var client = new RestClient("http://altis-web.grupoagencia.com:8082/JAgencia.asmx/wsInGuia");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                var body = _JsonBody.ToString();

                request.AddParameter("application/json", body, ParameterType.RequestBody);

                IRestResponse response = client.Execute(request);

                Newtonsoft.Json.Linq.JObject _JsonResponse = Newtonsoft.Json.Linq.JObject.Parse(response.Content);


                string _K_Guia_Resp = (string)_JsonResponse["data"].SelectToken("K_Guia");
                int _K_Oficina_Destino_Resp = (int)_JsonResponse["data"].SelectToken("K_Oficina_Destino"); 
                string _Codigo_Rastreo_Resp = (string)_JsonResponse["data"].SelectToken("Codigo_Rastreo");


                pShipping.PostOffice = DataAccess.DAShipping.GetPostOfficeByCodeCourier(_K_Oficina_Destino_Resp, pShipping.Courier);

                Label _Label = new Label();
                _Label.PostOffice = pShipping.PostOffice;
                _Label.Identifier = _K_Guia_Resp;
                _Label.TrackingNumber = _Codigo_Rastreo_Resp;

                int _Index = _Label.Identifier.IndexOf("-");

                int _K_Oficina_getPegote = Convert.ToInt32(_Label.Identifier.Substring(0, _Index));

                string _K_Guia_getPegote = _Label.Identifier.Substring(_Index + 1, (_Label.Identifier.Length-1-_Index));



                var clientPegote = new RestClient("http://altis-web.grupoagencia.com:8082/JAgencia.asmx/wsGetPegote?K_oficina="+_K_Oficina_getPegote+"&K_guia="+_K_Guia_getPegote+"&CodigoPedido=&ID_Sesion="+ _IDSesionAux);
                clientPegote.Timeout = -1;
                var requestPegote = new RestRequest(Method.GET);
                IRestResponse responsePegote = clientPegote.Execute(requestPegote);

                _JsonResponse = Newtonsoft.Json.Linq.JObject.Parse(responsePegote.Content);

                string _PegoteAux = (string)_JsonResponse["data"].SelectToken("Pegote");

                _Label.Data = Convert.FromBase64String(_PegoteAux);

                DataAccess.DAShipping.InsertLabel(_Label, _Shipping);

                _Shipping.Labels = new List<Label>();
                _Shipping.Labels.Add(_Label);
                
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return _Shipping;
        }
    }
}
