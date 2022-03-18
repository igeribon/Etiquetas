using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using API.DataTypes;
using System.Data;


namespace API.DataAccess
{
    public class DAShipping
    {
        //INSERTS 
        #region INSERTS

        public static void InsertLocality(Locality pLocality)
        {
            SqlConnection _Cnn = new SqlConnection();

            try
            {
                _Cnn = Connection.Instancia.SetConnection();

                SqlCommand _Cmd = new SqlCommand("spInsertLocality", _Cnn);
                _Cmd.CommandType = CommandType.StoredProcedure;

                _Cmd.Parameters.Add("@LocalityName", SqlDbType.VarChar).Value = pLocality.Name;
                _Cmd.Parameters.Add("@LocalityCity", SqlDbType.VarChar).Value = pLocality.City;
                _Cmd.Parameters.Add("@LocalityState", SqlDbType.VarChar).Value = pLocality.State;
                _Cmd.Parameters.Add("@LocalityZIP", SqlDbType.VarChar).Value = pLocality.ZIP;
                _Cmd.Parameters.Add("@LocalityCourierId", SqlDbType.Int).Value = pLocality.Courier.Id;
                _Cmd.Parameters.Add("@LocalityCode", SqlDbType.VarChar).Value = pLocality.Code;
                _Cmd.Parameters.Add("@LocalityCityCode", SqlDbType.VarChar).Value = pLocality.CityCode;
                _Cmd.Parameters.Add("@LocalityStateCode", SqlDbType.VarChar).Value = pLocality.StateCode;


                _Cnn.Open();
                _Cmd.ExecuteNonQuery();


            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                _Cnn.Close();
            }

        }


        public static int InsertAddress(Address pAddress)
        {
            SqlConnection _Cnn = new SqlConnection();

            try
            {
                _Cnn = Connection.Instancia.SetConnection();

                SqlCommand _Cmd = new SqlCommand("spInsertAddress", _Cnn);
                _Cmd.CommandType = CommandType.StoredProcedure;


                if (pAddress.Locality != null && pAddress.Locality.Id!=0)
                    _Cmd.Parameters.Add("@AddressLocalityId", SqlDbType.Int).Value = pAddress.Locality.Id;
                else
                    _Cmd.Parameters.Add("@AddressLocalityId", SqlDbType.Int).Value = DBNull.Value;


                _Cmd.Parameters.Add("@AddressLine1", SqlDbType.VarChar).Value = pAddress.Line1;

                _Cmd.Parameters.Add("@AddressLine2", SqlDbType.VarChar).Value = pAddress.Line2;
                _Cmd.Parameters.Add("@AddressPhone", SqlDbType.VarChar).Value = pAddress.Phone;
                _Cmd.Parameters.Add("@AddressObservation", SqlDbType.VarChar).Value = pAddress.Observation;

                var _ReturnParameter = _Cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
                _ReturnParameter.Direction = ParameterDirection.ReturnValue;



                _Cnn.Open();
                _Cmd.ExecuteNonQuery();

                pAddress.Id= Convert.ToInt32(_ReturnParameter.Value);

            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                _Cnn.Close();
            }

            return pAddress.Id;

        }


        
        public static void InsertPostOffice(PostOffice pPostOffice)
        {
            SqlConnection _Cnn = new SqlConnection();

            try
            {

                pPostOffice.Address.Id = InsertAddress(pPostOffice.Address);

                _Cnn = Connection.Instancia.SetConnection();

                SqlCommand _Cmd = new SqlCommand("spInsertPostOffice", _Cnn);
                _Cmd.CommandType = CommandType.StoredProcedure;

                _Cmd.Parameters.Add("@PostOfficeCourierId", SqlDbType.Int).Value = pPostOffice.Courier.Id;
                _Cmd.Parameters.Add("@PostOfficeAddressId", SqlDbType.Int).Value = pPostOffice.Address.Id;
                _Cmd.Parameters.Add("@PostOfficeName", SqlDbType.VarChar).Value = pPostOffice.Name;
                _Cmd.Parameters.Add("@PostOfficeCode", SqlDbType.Int).Value = pPostOffice.Code;

                _Cnn.Open();
                _Cmd.ExecuteNonQuery();


            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                _Cnn.Close();
            }

        }



        public static int InsertReceiver(Receiver pReceiver)
        {
            SqlConnection _Cnn = new SqlConnection();



            try
            {
                pReceiver.Address.Id = InsertAddress(pReceiver.Address);


                _Cnn = Connection.Instancia.SetConnection();

                SqlCommand _Cmd = new SqlCommand("spInsertReceiver", _Cnn);
                _Cmd.CommandType = CommandType.StoredProcedure;

                _Cmd.Parameters.Add("@ReceiverAddressId", SqlDbType.Int).Value = pReceiver.Address.Id;
                _Cmd.Parameters.Add("@ReceiverName", SqlDbType.VarChar).Value = pReceiver.Name;
                _Cmd.Parameters.Add("@ReceiverLastname", SqlDbType.VarChar).Value = pReceiver.Lastname;


                _Cmd.Parameters.Add("@ReceiverEmail", SqlDbType.VarChar).Value = pReceiver.Email;
                _Cmd.Parameters.Add("@ReceiverPhone", SqlDbType.VarChar).Value = pReceiver.Phone;
                _Cmd.Parameters.Add("@ReceiverPassport", SqlDbType.VarChar).Value = pReceiver.Passport;

                var _ReturnParameter = _Cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
                _ReturnParameter.Direction = ParameterDirection.ReturnValue;



                _Cnn.Open();
                _Cmd.ExecuteNonQuery();


                pReceiver.Id = Convert.ToInt32(_ReturnParameter.Value);


            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                _Cnn.Close();
            }

            return pReceiver.Id;
        }


        public static void InsertPackage(Package pPackage, Shipping pShipping)
        {
            SqlConnection _Cnn = new SqlConnection();

            try
            {

                _Cnn = Connection.Instancia.SetConnection();

                SqlCommand _Cmd = new SqlCommand("spInsertPackage", _Cnn);
                _Cmd.CommandType = CommandType.StoredProcedure;

                _Cmd.Parameters.Add("@PackageShippingId", SqlDbType.Int).Value = pShipping.Id;
                _Cmd.Parameters.Add("@PackageWeight", SqlDbType.Decimal).Value = pPackage.Weight;
                _Cmd.Parameters.Add("@PackageHeight", SqlDbType.Decimal).Value = pPackage.Height;
                _Cmd.Parameters.Add("@PackageWidth", SqlDbType.Decimal).Value = pPackage.Width;
                _Cmd.Parameters.Add("@PackageDepth", SqlDbType.Decimal).Value = pPackage.Depth;
                _Cmd.Parameters.Add("@PackageReference", SqlDbType.VarChar).Value = pPackage.Reference;


                _Cnn.Open();
                _Cmd.ExecuteNonQuery();


            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                _Cnn.Close();
            }

        }


        public static void InsertShipping(Shipping pShipping)
        {
            SqlConnection _Cnn = new SqlConnection();

            try
            {
                 
                pShipping.Receiver.Id = InsertReceiver(pShipping.Receiver);


                _Cnn = Connection.Instancia.SetConnection();

                SqlCommand _Cmd = new SqlCommand("spInsertShipping", _Cnn);
                _Cmd.CommandType = CommandType.StoredProcedure;

                _Cmd.Parameters.Add("@ShippingCreatedAt", SqlDbType.DateTime).Value = pShipping.CreatedAt;


                if(pShipping.Courier!=null)
                    _Cmd.Parameters.Add("@ShippingCourierId", SqlDbType.Int).Value = pShipping.Courier.Id;

                else
                    _Cmd.Parameters.Add("@ShippingCourierId", SqlDbType.Int).Value = DBNull.Value;



                _Cmd.Parameters.Add("@ShippingReceiverId", SqlDbType.Int).Value = pShipping.Receiver.Id;

                if (pShipping.PostOffice != null)
                    _Cmd.Parameters.Add("@ShippingPostOfficeId", SqlDbType.Int).Value = pShipping.PostOffice.Id;

                else
                    _Cmd.Parameters.Add("@ShippingPostOfficeId", SqlDbType.Int).Value = DBNull.Value;


                _Cmd.Parameters.Add("@ShippingCashOnDelivery", SqlDbType.Bit).Value = pShipping.CashOnDelivery;

                if (pShipping.ReferenceId != null)
                    _Cmd.Parameters.Add("@ShippingReferenceId", SqlDbType.VarChar).Value = pShipping.ReferenceId;

                else
                    _Cmd.Parameters.Add("@ShippingReferenceId", SqlDbType.VarChar).Value = DBNull.Value;

                _Cmd.Parameters.Add("@ShippingOrderId", SqlDbType.VarChar).Value = pShipping.OrderId;

                _Cmd.Parameters.Add("@ShippingFinancialStatus", SqlDbType.VarChar).Value = pShipping.FinancialStatus;


                _Cmd.Parameters.Add("@ShippingTotalLinesItemPrice", SqlDbType.Decimal).Value = pShipping.TotalLinesItemPrice;

                
                if(pShipping.Info!=null)    
                    _Cmd.Parameters.Add("@ShippingInfo", SqlDbType.VarChar).Value = pShipping.Info;
                
                else
                    _Cmd.Parameters.Add("@ShippingInfo", SqlDbType.VarChar).Value = DBNull.Value;


                if (pShipping.Note != null)
                    _Cmd.Parameters.Add("@ShippingNote", SqlDbType.VarChar).Value = pShipping.Note;
                else
                    _Cmd.Parameters.Add("@ShippingNote", SqlDbType.VarChar).Value = DBNull.Value;


                if (pShipping.GuideType != null)
                    _Cmd.Parameters.Add("@ShippingGuideTypeId", SqlDbType.Int).Value = pShipping.GuideType.Id;
                else
                    _Cmd.Parameters.Add("@ShippingGuideTypeId", SqlDbType.Int).Value = DBNull.Value;


                if (pShipping.FulfillmentId != null)
                    _Cmd.Parameters.Add("@ShippingFulfillmentId", SqlDbType.VarChar).Value = pShipping.FulfillmentId;
                else
                    _Cmd.Parameters.Add("@ShippingFulfillmentId", SqlDbType.VarChar).Value = DBNull.Value;


                _Cmd.Parameters.Add("@ShippingShopifyId", SqlDbType.BigInt).Value = pShipping.ShopifyId;



                var _ReturnParameter = _Cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
                _ReturnParameter.Direction = ParameterDirection.ReturnValue;



                _Cnn.Open();
                _Cmd.ExecuteNonQuery();


                pShipping.Id = Convert.ToInt32(_ReturnParameter.Value);



                foreach (Package _Package in pShipping.Packages)
                {
                    InsertPackage(_Package, pShipping);
                }



            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                _Cnn.Close();
            }

        }

        public static void InsertLabel(Label pLabel, Shipping pShipping)
        {
            SqlConnection _Cnn = new SqlConnection();

            try
            {
                _Cnn = Connection.Instancia.SetConnection();

                SqlCommand _Cmd = new SqlCommand("spInsertLabel", _Cnn);
                _Cmd.CommandType = CommandType.StoredProcedure;

                _Cmd.Parameters.Add("@LabelShippingId", SqlDbType.Int).Value = pShipping.Id;
                _Cmd.Parameters.Add("@LabelIdentifier", SqlDbType.VarChar).Value = pLabel.Identifier;
                _Cmd.Parameters.Add("@LabelTrackingNumber", SqlDbType.VarChar).Value = pLabel.TrackingNumber;
                _Cmd.Parameters.Add("@LabelData", SqlDbType.Binary).Value = pLabel.Data;

                //ESTE CHECKEO SE PUSO CUANDO HABIA PROBLEMA CON LAS ETIQUETAS DE ROCHA, NO HAY OFICINA APARENTEMENTE
                if (pLabel.PostOffice != null)
                {
                    _Cmd.Parameters.Add("@LabelPostoFficeId", SqlDbType.Int).Value = pLabel.PostOffice.Id;
                }

                else
                {
                    _Cmd.Parameters.Add("@LabelPostoFficeId", SqlDbType.Int).Value = DBNull.Value;
                }

                _Cnn.Open();
                _Cmd.ExecuteNonQuery();


            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                _Cnn.Close();
            }

        }
 

        #endregion

        //GETS
        #region GETS
        public static Locality GetLocalityByCourierNameState(string pName, string pState, Courier pCourier)
        {
            Locality _Locality = null;

            SqlConnection _Cnn = new SqlConnection();

            try
            {


                _Cnn = Connection.Instancia.SetConnection();
                SqlDataReader _Dr = null;
                SqlCommand _Cmd = new SqlCommand("spLocalityGetByCourierNameState", _Cnn);
                _Cmd.Parameters.Add("@LocalityName", SqlDbType.VarChar).Value = pName;
                _Cmd.Parameters.Add("@LocalityState", SqlDbType.VarChar).Value = pState;
                _Cmd.Parameters.Add("@LocalityCourierId", SqlDbType.Int).Value = pCourier.Id;


                _Cmd.CommandType = CommandType.StoredProcedure;

                _Cnn.Open();

                _Dr = _Cmd.ExecuteReader();

                while (_Dr.Read())
                {
                    _Locality = new Locality();

                    _Locality.Id = Convert.ToInt32(_Dr["LocalityId"]);
                    _Locality.Name= Convert.ToString(_Dr["LocalityName"]);
                    _Locality.City = Convert.ToString(_Dr["LocalityCity"]);
                    _Locality.State = Convert.ToString(_Dr["LocalityState"]);
                    _Locality.ZIP = Convert.ToString(_Dr["LocalityZIP"]);

                    _Locality.Code = Convert.ToString(_Dr["LocalityCode"]);
                    _Locality.CityCode = Convert.ToString(_Dr["LocalityCityCode"]);
                    _Locality.StateCode = Convert.ToString(_Dr["LocalityStateCode"]);

                }

            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                _Cnn.Close();
            }

            return _Locality;
        }


        public static Locality GetLocalityByCourierNameCity(string pName, string pCity, Courier pCourier)
        {
            Locality _Locality = null;

            SqlConnection _Cnn = new SqlConnection();

            try
            {


                _Cnn = Connection.Instancia.SetConnection();
                SqlDataReader _Dr = null;
                SqlCommand _Cmd = new SqlCommand("spLocalityGetByCourierNameCity", _Cnn);
                _Cmd.Parameters.Add("@LocalityName", SqlDbType.VarChar).Value = pName;
                _Cmd.Parameters.Add("@LocalityCity", SqlDbType.VarChar).Value = pCity;
                _Cmd.Parameters.Add("@LocalityCourierId", SqlDbType.Int).Value = pCourier.Id;


                _Cmd.CommandType = CommandType.StoredProcedure;

                _Cnn.Open();

                _Dr = _Cmd.ExecuteReader();

                while (_Dr.Read())
                {
                    _Locality = new Locality();

                    _Locality.Id = Convert.ToInt32(_Dr["LocalityId"]);
                    _Locality.Name = Convert.ToString(_Dr["LocalityName"]);
                    _Locality.City = Convert.ToString(_Dr["LocalityCity"]);
                    _Locality.State = Convert.ToString(_Dr["LocalityState"]);
                    _Locality.ZIP = Convert.ToString(_Dr["LocalityZIP"]);

                    _Locality.Code = Convert.ToString(_Dr["LocalityCode"]);
                    _Locality.CityCode = Convert.ToString(_Dr["LocalityCityCode"]);
                    _Locality.StateCode = Convert.ToString(_Dr["LocalityStateCode"]);

                }

            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                _Cnn.Close();
            }

            return _Locality;
        }

        public static Locality GetLocalityByCourierZIPState(string pZIP, string pState, Courier pCourier)
        {
            Locality _Locality = null;

            SqlConnection _Cnn = new SqlConnection();

            try
            {


                _Cnn = Connection.Instancia.SetConnection();
                SqlDataReader _Dr = null;
                SqlCommand _Cmd = new SqlCommand("spLocalityGetByCourierZIPState", _Cnn);
                _Cmd.Parameters.Add("@LocalityZIP", SqlDbType.VarChar).Value = pZIP;
                _Cmd.Parameters.Add("@LocalityState", SqlDbType.VarChar).Value = pState;
                _Cmd.Parameters.Add("@LocalityCourierId", SqlDbType.Int).Value = pCourier.Id;


                _Cmd.CommandType = CommandType.StoredProcedure;

                _Cnn.Open();

                _Dr = _Cmd.ExecuteReader();

                while (_Dr.Read())
                {
                    _Locality = new Locality();

                    _Locality.Id = Convert.ToInt32(_Dr["LocalityId"]);
                    _Locality.Name = Convert.ToString(_Dr["LocalityName"]);
                    _Locality.City = Convert.ToString(_Dr["LocalityCity"]);
                    _Locality.State = Convert.ToString(_Dr["LocalityState"]);
                    _Locality.ZIP = Convert.ToString(_Dr["LocalityZIP"]);

                    _Locality.Code = Convert.ToString(_Dr["LocalityCode"]);
                    _Locality.CityCode = Convert.ToString(_Dr["LocalityCityCode"]);
                    _Locality.StateCode = Convert.ToString(_Dr["LocalityStateCode"]);

                }

            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                _Cnn.Close();
            }

            return _Locality;
        }


        //PARA PUT DESDE BACKOFFICE
        public static Locality GetLocalityByCourierNameCityState(string pName, string pCity, string pState, Courier pCourier)
        {
            Locality _Locality = null;

            SqlConnection _Cnn = new SqlConnection();

            try
            {


                _Cnn = Connection.Instancia.SetConnection();
                SqlDataReader _Dr = null;
                SqlCommand _Cmd = new SqlCommand("spLocalityGetByCourierNameCityState", _Cnn);
                _Cmd.Parameters.Add("@LocalityName", SqlDbType.VarChar).Value = pName;
                _Cmd.Parameters.Add("@LocalityState", SqlDbType.VarChar).Value = pState;
                _Cmd.Parameters.Add("@LocalityCity", SqlDbType.VarChar).Value = pCity;
                _Cmd.Parameters.Add("@LocalityCourierId", SqlDbType.Int).Value = pCourier.Id;


                _Cmd.CommandType = CommandType.StoredProcedure;

                _Cnn.Open();

                _Dr = _Cmd.ExecuteReader();

                while (_Dr.Read())
                {
                    _Locality = new Locality();

                    _Locality.Id = Convert.ToInt32(_Dr["LocalityId"]);
                    _Locality.Name = Convert.ToString(_Dr["LocalityName"]);
                    _Locality.City = Convert.ToString(_Dr["LocalityCity"]);
                    _Locality.State = Convert.ToString(_Dr["LocalityState"]);
                    _Locality.ZIP = Convert.ToString(_Dr["LocalityZIP"]);

                    _Locality.Code = Convert.ToString(_Dr["LocalityCode"]);
                    _Locality.CityCode = Convert.ToString(_Dr["LocalityCityCode"]);
                    _Locality.StateCode = Convert.ToString(_Dr["LocalityStateCode"]);

                }

            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                _Cnn.Close();
            }

            return _Locality;
        }

        public static Locality GetLocalityById(int pLocalityId)
        {
            Locality _Locality = null;

            SqlConnection _Cnn = new SqlConnection();

            try
            {


                _Cnn = Connection.Instancia.SetConnection();
                SqlDataReader _Dr = null;
                SqlCommand _Cmd = new SqlCommand("spLocalityGetById", _Cnn);
                _Cmd.Parameters.Add("@LocalityId", SqlDbType.Int).Value = pLocalityId;



                _Cmd.CommandType = CommandType.StoredProcedure;

                _Cnn.Open();

                _Dr = _Cmd.ExecuteReader();

                while (_Dr.Read())
                {
                    _Locality = new Locality();

                    _Locality.Id = Convert.ToInt32(_Dr["LocalityId"]);
                    _Locality.Name = Convert.ToString(_Dr["LocalityName"]).Trim();
                    _Locality.City = Convert.ToString(_Dr["LocalityCity"]).Trim();
                    _Locality.State = Convert.ToString(_Dr["LocalityState"]).Trim();
                    _Locality.ZIP = Convert.ToString(_Dr["LocalityZIP"]).Trim();

                    _Locality.Code = Convert.ToString(_Dr["LocalityCode"]).Trim();
                    _Locality.CityCode = Convert.ToString(_Dr["LocalityCityCode"]).Trim();
                    _Locality.StateCode = Convert.ToString(_Dr["LocalityStateCode"]).Trim();

                }

            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                _Cnn.Close();
            }

            return _Locality;
        }


        public static Shipping GetShippingByOrderId(string pOrderId)
        {
            Shipping _Shipping = new Shipping();

            SqlConnection _Cnn = new SqlConnection();

            try
            {


                _Cnn = Connection.Instancia.SetConnection();
                SqlDataReader _Dr = null;
                SqlCommand _Cmd = new SqlCommand("spShippingGetByOrderId", _Cnn);
                _Cmd.Parameters.Add("@ShippingOrderId", SqlDbType.VarChar).Value = pOrderId;


                _Cmd.CommandType = CommandType.StoredProcedure;

                _Cnn.Open();

                _Dr = _Cmd.ExecuteReader();

                while (_Dr.Read())
                {
       
                    //DATOS SHIPPING
                    _Shipping.Id = Convert.ToInt32(_Dr["ShippingId"]);
                    _Shipping.OrderId = Convert.ToString(_Dr["ShippingOrderId"]);

                    _Shipping.FinancialStatus = Convert.ToString(_Dr["ShippingFinancialStatus"]);

                    _Shipping.CreatedAt = Convert.ToDateTime(_Dr["ShippingCreatedAt"]);

                    _Shipping.TotalLinesItemPrice = Convert.ToDouble(_Dr["ShippingTotalLinesItemPrice"]);

                    _Shipping.CashOnDelivery = Convert.ToBoolean(_Dr["ShippingCashOnDelivery"]);

                    if(_Dr["ShippingInfo"]!=DBNull.Value)
                        _Shipping.Info = Convert.ToString(_Dr["ShippingInfo"]);

                    if (_Dr["ShippingNote"] != DBNull.Value)
                        _Shipping.Note = Convert.ToString(_Dr["ShippingNote"]);


                    if (_Dr["ShippingFulfillmentId"] != DBNull.Value)
                        _Shipping.FulfillmentId = Convert.ToString(_Dr["ShippingFulfillmentId"]);



                    //DATOS COURIER


                    if (_Dr["CourierId"] != DBNull.Value)
                    {
                        _Shipping.Courier = new Courier();

                        _Shipping.Courier.Id = Convert.ToInt32(_Dr["CourierId"]);
                        _Shipping.Courier.Name = Convert.ToString(_Dr["CourierName"]);
                        _Shipping.Courier.Country = Convert.ToString(_Dr["CourierCountry"]);
                    }


                    //DATOS RECEIVER
                    _Shipping.Receiver = new Receiver();
                    _Shipping.Receiver.Id = Convert.ToInt32(_Dr["ReceiverId"]);
                    _Shipping.Receiver.Name = Convert.ToString(_Dr["ReceiverName"]);
                    _Shipping.Receiver.Lastname = Convert.ToString(_Dr["ReceiverLastName"]);
                    _Shipping.Receiver.Email = Convert.ToString(_Dr["ReceiverEmail"]);
                    _Shipping.Receiver.Phone = Convert.ToString(_Dr["ReceiverPhone"]);

                    _Shipping.Receiver.Address = GetAddressById(Convert.ToInt32(_Dr["ReceiverAddressId"]));


                    //DATOS POST OFFICE
                    _Shipping.PostOffice = new PostOffice();

                    if (_Dr["PostOfficeId"] != DBNull.Value)
                    {
                        _Shipping.PostOffice.Id = Convert.ToInt32(_Dr["PostOfficeId"]);
                        _Shipping.PostOffice.Name = Convert.ToString(_Dr["PostOfficeName"]).Trim();
                        _Shipping.PostOffice.Courier = _Shipping.Courier;

                        //SE CAMBIO PARA DESACOPLAR DE TABLA ADDRESS LAS POST OFFICES, AHORA POSTOFFICE SE RELACIONA CON LOCALITY, DE TODAS FORMAS
                        //CONSERVAMOS EL DISEÑO DE CLASES DONDE LA LOCALITY VA ADENTRO DE LA ADDRESS

                        //_Shipping.PostOffice.Address = GetAddressById(Convert.ToInt32(_Dr["ReceiverAddressId"]));

                        _Shipping.PostOffice.Address = new Address();
                        _Shipping.PostOffice.Address.Locality = GetLocalityById(Convert.ToInt32(_Dr["PostOfficeLocalityId"]));


                        _Shipping.PostOffice.Code = Convert.ToInt32(_Dr["PostOfficeCode"]);
                    }

               

                    _Shipping.Labels = GetLabelsByShippingId(_Shipping.Id);
                    _Shipping.Packages = GetPackagesByShippingId(_Shipping.Id);



                    if (_Dr["ShippingDeliveryTypeId"] != DBNull.Value)
                    {

                        _Shipping.DeliveryType = Controllers.DacServiceController.GetDeliveryTypes().Find(x => x.Id == Convert.ToInt32(_Dr["ShippingDeliveryTypeId"]));
                    }

                    if (_Dr["ShippingGuideTypeId"] != DBNull.Value)
                    {
                        _Shipping.GuideType = (GuideType)Controllers.DacServiceController.GetGuideTypes().Find(x => x.Id == Convert.ToInt32(_Dr["ShippingGuideTypeId"]));
                    }


                    if (_Dr["ShippingShopifyId"] != DBNull.Value)
                        _Shipping.ShopifyId = Convert.ToInt64(_Dr["ShippingShopifyId"]);


                }

            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                _Cnn.Close();
            }

            return _Shipping;
        }

        public static List<Shipping> GetShippingsByReceiverNameLastname(string pNameLastname)
        {
            List<Shipping> _Shippings = new List<Shipping>();

            SqlConnection _Cnn = new SqlConnection();

            try
            {


                _Cnn = Connection.Instancia.SetConnection();
                SqlDataReader _Dr = null;
                SqlCommand _Cmd = new SqlCommand("spShippingGetByReceiverNameLastname", _Cnn);
                _Cmd.Parameters.Add("@NameLastname", SqlDbType.VarChar).Value = pNameLastname;


                _Cmd.CommandType = CommandType.StoredProcedure;

                _Cnn.Open();

                _Dr = _Cmd.ExecuteReader();

                while (_Dr.Read())
                {
                    Shipping _Shipping = new Shipping();

                    //DATOS SHIPPING
                    _Shipping.Id = Convert.ToInt32(_Dr["ShippingId"]);
                    _Shipping.OrderId = Convert.ToString(_Dr["ShippingOrderId"]);

                    _Shipping.FinancialStatus = Convert.ToString(_Dr["ShippingFinancialStatus"]);

                    _Shipping.CreatedAt = Convert.ToDateTime(_Dr["ShippingCreatedAt"]);

                    _Shipping.TotalLinesItemPrice = Convert.ToDouble(_Dr["ShippingTotalLinesItemPrice"]);

                    _Shipping.CashOnDelivery = Convert.ToBoolean(_Dr["ShippingCashOnDelivery"]);

                    if (_Dr["ShippingInfo"] != DBNull.Value)
                        _Shipping.Info = Convert.ToString(_Dr["ShippingInfo"]);


                    if (_Dr["ShippingNote"] != DBNull.Value)
                        _Shipping.Note = Convert.ToString(_Dr["ShippingNote"]);



                    if (_Dr["ShippingFulfillmentId"] != DBNull.Value)
                        _Shipping.FulfillmentId = Convert.ToString(_Dr["ShippingFulfillmentId"]);




                    //DATOS COURIER


                    if (_Dr["CourierId"] != DBNull.Value)
                    {
                        _Shipping.Courier = new Courier();

                        _Shipping.Courier.Id = Convert.ToInt32(_Dr["CourierId"]);
                        _Shipping.Courier.Name = Convert.ToString(_Dr["CourierName"]);
                        _Shipping.Courier.Country = Convert.ToString(_Dr["CourierCountry"]);
                    }


                    //DATOS RECEIVER
                    _Shipping.Receiver = new Receiver();
                    _Shipping.Receiver.Id = Convert.ToInt32(_Dr["ReceiverId"]);
                    _Shipping.Receiver.Name = Convert.ToString(_Dr["ReceiverName"]);
                    _Shipping.Receiver.Lastname = Convert.ToString(_Dr["ReceiverLastName"]);
                    _Shipping.Receiver.Email = Convert.ToString(_Dr["ReceiverEmail"]);
                    _Shipping.Receiver.Phone = Convert.ToString(_Dr["ReceiverPhone"]);

                    _Shipping.Receiver.Address = GetAddressById(Convert.ToInt32(_Dr["ReceiverAddressId"]));


                    //DATOS POST OFFICE
                    _Shipping.PostOffice = new PostOffice();

                    if (_Dr["PostOfficeId"] != DBNull.Value)
                    {
                        _Shipping.PostOffice.Id = Convert.ToInt32(_Dr["PostOfficeId"]);
                        _Shipping.PostOffice.Name = Convert.ToString(_Dr["PostOfficeName"]);
                        _Shipping.PostOffice.Courier = _Shipping.Courier;
                        _Shipping.PostOffice.Address = GetAddressById(Convert.ToInt32(_Dr["ReceiverAddressId"]));
                        _Shipping.PostOffice.Code = Convert.ToInt32(_Dr["PostOfficeCode"]);
                    }

          
                    //_Shipping.Labels = GetLabelsByShippingId(_Shipping.Id);
                    //_Shipping.Packages = GetPackagesByShippingId(_Shipping.Id);



                    if (_Dr["ShippingDeliveryTypeId"] != DBNull.Value)
                    {

                        _Shipping.DeliveryType = Controllers.DacServiceController.GetDeliveryTypes().Find(x => x.Id == Convert.ToInt32(_Dr["ShippingDeliveryTypeId"]));
                    }

                    if (_Dr["ShippingGuideTypeId"] != DBNull.Value)
                    {
                        _Shipping.GuideType = (GuideType)Controllers.DacServiceController.GetGuideTypes().Find(x => x.Id == Convert.ToInt32(_Dr["ShippingGuideTypeId"]));
                    }

                    _Shippings.Add(_Shipping);
                }

            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                _Cnn.Close();
            }

            return _Shippings;
        }


        public static List<Shipping> GetShippingsByCreatedAtFromTo(DateTime pFrom, DateTime pTo, int pHasLabel, int pLimit, string pOrder)
        {
            List<Shipping> _Shippings = new List<Shipping>();

            SqlConnection _Cnn = new SqlConnection();

            try
            {


                _Cnn = Connection.Instancia.SetConnection();
                SqlDataReader _Dr = null;
                SqlCommand _Cmd = new SqlCommand("spShippingGetByCreatedAtFromTo", _Cnn);
                _Cmd.Parameters.Add("@From", SqlDbType.DateTime).Value = pFrom;
                _Cmd.Parameters.Add("@To", SqlDbType.DateTime).Value = pTo;
                _Cmd.Parameters.Add("@HasLabel", SqlDbType.Int).Value = pHasLabel;
                _Cmd.Parameters.Add("@Limit", SqlDbType.Int).Value = pLimit;
                _Cmd.Parameters.Add("@Order", SqlDbType.VarChar).Value = pOrder;

                _Cmd.CommandType = CommandType.StoredProcedure;

                _Cnn.Open();

                _Dr = _Cmd.ExecuteReader();

                while (_Dr.Read())
                {

                    Shipping _Shipping = new Shipping();

                    //DATOS SHIPPING
                    _Shipping.Id = Convert.ToInt32(_Dr["ShippingId"]);
                    _Shipping.OrderId = Convert.ToString(_Dr["ShippingOrderId"]);

                    _Shipping.FinancialStatus = Convert.ToString(_Dr["ShippingFinancialStatus"]);

                    _Shipping.CreatedAt = Convert.ToDateTime(_Dr["ShippingCreatedAt"]);

                    _Shipping.TotalLinesItemPrice = Convert.ToDouble(_Dr["ShippingTotalLinesItemPrice"]);

                    _Shipping.CashOnDelivery = Convert.ToBoolean(_Dr["ShippingCashOnDelivery"]);

                    if (_Dr["ShippingInfo"] != DBNull.Value)
                        _Shipping.Info = Convert.ToString(_Dr["ShippingInfo"]);



                    if (_Dr["ShippingNote"] != DBNull.Value)
                        _Shipping.Note = Convert.ToString(_Dr["ShippingNote"]);


                    if (_Dr["ShippingFulfillmentId"] != DBNull.Value)
                        _Shipping.FulfillmentId = Convert.ToString(_Dr["ShippingFulfillmentId"]);




                    //DATOS COURIER

                    _Shipping.Courier = new Courier();

                    if (_Dr["CourierId"] != DBNull.Value)
                    {
                        _Shipping.Courier.Id = Convert.ToInt32(_Dr["CourierId"]);
                        _Shipping.Courier.Name = Convert.ToString(_Dr["CourierName"]);
                        _Shipping.Courier.Country = Convert.ToString(_Dr["CourierCountry"]);
                    }


                    //DATOS RECEIVER
                    _Shipping.Receiver = new Receiver();
                    _Shipping.Receiver.Id = Convert.ToInt32(_Dr["ReceiverId"]);
                    _Shipping.Receiver.Name = Convert.ToString(_Dr["ReceiverName"]);
                    _Shipping.Receiver.Lastname = Convert.ToString(_Dr["ReceiverLastName"]);
                    _Shipping.Receiver.Email = Convert.ToString(_Dr["ReceiverEmail"]);
                    _Shipping.Receiver.Phone = Convert.ToString(_Dr["ReceiverPhone"]);

                    _Shipping.Receiver.Address = GetAddressById(Convert.ToInt32(_Dr["ReceiverAddressId"]));

                    //DATOS POST OFFICE

                    _Shipping.PostOffice = new PostOffice();

                    if (_Dr["PostOfficeId"] != DBNull.Value)
                    {
                        _Shipping.PostOffice.Id = Convert.ToInt32(_Dr["PostOfficeId"]);
                        _Shipping.PostOffice.Name = Convert.ToString(_Dr["PostOfficeName"]);
                        _Shipping.PostOffice.Courier = _Shipping.Courier;
                        _Shipping.PostOffice.Address = GetAddressById(Convert.ToInt32(_Dr["ReceiverAddressId"]));
                    }


                    //_Shipping.Labels = GetLabelsByShippingId(_Shipping.Id);
                    //_Shipping.Packages = GetPackagesByShippingId(_Shipping.Id);


                    //if (_Dr["PackageId"] != DBNull.Value)
                    //{

                    //    _Shipping.Packages = new List<Package>();

                    //    Package _Package = new Package();

                    //    _Package.Id = Convert.ToInt32(_Dr["PackageId"]);
                    //    _Package.Weight = Convert.ToDouble(_Dr["PackageWeight"]);
                    //    _Package.Height = Convert.ToDouble(_Dr["PackageHeight"]);
                    //    _Package.Width = Convert.ToDouble(_Dr["PackageWidth"]);

                    //    _Package.Depth = Convert.ToDouble(_Dr["PackageDepth"]);
                    //    _Package.Reference = Convert.ToString(_Dr["PackageReference"]);

                    //    _Shipping.Packages.Add(_Package);

                    //}

                    if (_Dr["LabelId"] != DBNull.Value)
                    {
                        _Shipping.Labels = new List<Label>();

                        Label _Label = new Label();

                        _Label.Id = Convert.ToInt32(_Dr["LabelId"]);
                        _Label.Identifier = Convert.ToString(_Dr["LabelIdentifier"]);
                        _Label.TrackingNumber = Convert.ToString(_Dr["LabelTrackingNumber"]);

                        _Label.Data = (byte[])(_Dr["LabelData"]);

                        _Shipping.Labels.Add(_Label);
                    }
                    
                    if (_Dr["ShippingDeliveryTypeId"] != DBNull.Value)
                    {

                        _Shipping.DeliveryType = Controllers.DacServiceController.GetDeliveryTypes().Find(x => x.Id == Convert.ToInt32(_Dr["ShippingDeliveryTypeId"]));
                    }

                    if (_Dr["ShippingGuideTypeId"] != DBNull.Value)
                    {
                        _Shipping.GuideType = (GuideType)Controllers.DacServiceController.GetGuideTypes().Find(x => x.Id == Convert.ToInt32(_Dr["ShippingGuideTypeId"]));
                    }



                    _Shippings.Add(_Shipping);
                }

            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                _Cnn.Close();
            }

            return _Shippings;
        }

        public static Address GetAddressById(int pId)
        {
            Address _Address = new Address();

            SqlConnection _Cnn = new SqlConnection();

            try
            {


                _Cnn = Connection.Instancia.SetConnection();
                SqlDataReader _Dr = null;
                SqlCommand _Cmd = new SqlCommand("spAddressGetById", _Cnn);
                _Cmd.Parameters.Add("@AddressId", SqlDbType.VarChar).Value = pId;


                _Cmd.CommandType = CommandType.StoredProcedure;

                _Cnn.Open();

                _Dr = _Cmd.ExecuteReader();

                while (_Dr.Read())
                {


                    //DATOS SHIPPING
                    _Address.Id = Convert.ToInt32(_Dr["AddressId"]);
                    _Address.Line1 = Convert.ToString(_Dr["AddressLine1"]);
                    _Address.Line2 = Convert.ToString(_Dr["AddressLine2"]);
                    _Address.Phone = Convert.ToString(_Dr["AddressPhone"]);
                    _Address.Observation = Convert.ToString(_Dr["AddressObservation"]);

                    if (_Dr["LocalityId"] != DBNull.Value)
                    {
                        _Address.Locality = new Locality();
                        _Address.Locality.Id = Convert.ToInt32(_Dr["LocalityId"]);
                        _Address.Locality.Name = Convert.ToString(_Dr["LocalityName"]);
                        _Address.Locality.City = Convert.ToString(_Dr["LocalityCity"]);
                        _Address.Locality.State = Convert.ToString(_Dr["LocalityState"]);
                        _Address.Locality.ZIP = Convert.ToString(_Dr["LocalityZIP"]);

                        _Address.Locality.Code = Convert.ToString(_Dr["LocalityCode"]);
                        _Address.Locality.CityCode = Convert.ToString(_Dr["LocalityCityCode"]);
                        _Address.Locality.StateCode = Convert.ToString(_Dr["LocalityStateCode"]);
                    }
                }

            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                _Cnn.Close();
            }

            return _Address;
        }

        public static List<Package> GetPackagesByShippingId(int pId)
        {
            List<Package> _Packages = new List<Package>();

            SqlConnection _Cnn = new SqlConnection();

            try
            {


                _Cnn = Connection.Instancia.SetConnection();
                SqlDataReader _Dr = null;
                SqlCommand _Cmd = new SqlCommand("spPackagesGetByShippingId", _Cnn);
                _Cmd.Parameters.Add("@ShippingId", SqlDbType.VarChar).Value = pId;


                _Cmd.CommandType = CommandType.StoredProcedure;

                _Cnn.Open();

                _Dr = _Cmd.ExecuteReader();

                while (_Dr.Read())
                {

                    Package _Package = new Package();

                    _Package.Id = Convert.ToInt32(_Dr["PackageId"]);
                    _Package.Weight = Convert.ToDouble(_Dr["PackageWeight"]);
                    _Package.Height = Convert.ToDouble(_Dr["PackageHeight"]);
                    _Package.Width = Convert.ToDouble(_Dr["PackageWidth"]);

                    _Package.Depth = Convert.ToDouble(_Dr["PackageDepth"]);
                    _Package.Reference = Convert.ToString(_Dr["PackageReference"]);

                    _Packages.Add(_Package);
                }

            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                _Cnn.Close();
            }

            return _Packages;

        }


        public static List<Label> GetLabelsByShippingId(int pId)
        {
            List<Label> _Labels = new List<Label>();

            SqlConnection _Cnn = new SqlConnection();

            try
            {


                _Cnn = Connection.Instancia.SetConnection();
                SqlDataReader _Dr = null;
                SqlCommand _Cmd = new SqlCommand("spLabelsGetByShipping", _Cnn);
                _Cmd.Parameters.Add("@ShippingId", SqlDbType.VarChar).Value = pId;


                _Cmd.CommandType = CommandType.StoredProcedure;

                _Cnn.Open();

                _Dr = _Cmd.ExecuteReader();

                while (_Dr.Read())
                {

                    Label _Label = new Label();
                   
                    _Label.Id = Convert.ToInt32(_Dr["LabelId"]);
                    _Label.Identifier = Convert.ToString(_Dr["LabelIdentifier"]);
                    _Label.TrackingNumber = Convert.ToString(_Dr["LabelTrackingNumber"]);
                    _Label.Data = (byte[])(_Dr["LabelData"]);


                    if (_Dr["PostOfficeId"] != DBNull.Value)
                    {
                        _Label.PostOffice = new PostOffice();
                        _Label.PostOffice.Id = Convert.ToInt32(_Dr["PostOfficeId"]);
                        _Label.PostOffice.Name = Convert.ToString(_Dr["PostOfficeName"]);
                        _Label.PostOffice.Code = Convert.ToInt32(_Dr["PostOfficeCode"]);

                        if (_Dr["CourierId"] != DBNull.Value)
                        {
                            _Label.PostOffice.Courier = new Courier();
                            _Label.PostOffice.Courier.Id = Convert.ToInt32(_Dr["CourierId"]);
                            _Label.PostOffice.Courier.Name = Convert.ToString(_Dr["CourierName"]);
                            _Label.PostOffice.Courier.Country = Convert.ToString(_Dr["CourierCountry"]);
                        }


                        _Label.PostOffice.Address = GetAddressById(Convert.ToInt32(_Dr["PostOfficeAddressId"]));
                    }

                    _Labels.Add(_Label);
                }

            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                _Cnn.Close();
            }

            return _Labels;

        }


        public static PostOffice GetPostOfficeByCodeCourier(int pCode, Courier pCourier)
        {
            PostOffice _Office = null;

            SqlConnection _Cnn = new SqlConnection();

            try
            {


                _Cnn = Connection.Instancia.SetConnection();
                SqlDataReader _Dr = null;
                SqlCommand _Cmd = new SqlCommand("spPostOfficeGetByCodeCourierId", _Cnn);
                _Cmd.Parameters.Add("@PostOfficeCode", SqlDbType.Int).Value = pCode;
                _Cmd.Parameters.Add("@CourierId", SqlDbType.Int).Value = pCourier.Id;


                _Cmd.CommandType = CommandType.StoredProcedure;

                _Cnn.Open();

                _Dr = _Cmd.ExecuteReader();

                while (_Dr.Read())
                {
                    _Office = new PostOffice();
                    _Office.Id= Convert.ToInt32(_Dr["PostOfficeId"]);
                    _Office.Name = Convert.ToString(_Dr["PostOfficeName"]);
                    _Office.Code = Convert.ToInt32(_Dr["PostOfficeCode"]);
                    _Office.Courier = pCourier;

                    _Office.Address = new Address();
                    _Office.Address.Id = 0;
                    _Office.Address.Line1 = Convert.ToString(_Dr["PostOfficeAddressLine1"]);
                    //_Office.Address.Line2 = Convert.ToString(_Dr["AddressLine2"]);
                    _Office.Address.Phone = Convert.ToString(_Dr["PostOfficePhone"]);
                    //_Office.Address.Observation = Convert.ToString(_Dr["AddressObservation"]);

                    _Office.Address.Locality = new Locality();
                    _Office.Address.Locality.Id = Convert.ToInt32(_Dr["LocalityId"]);
                    _Office.Address.Locality.Name = Convert.ToString(_Dr["LocalityName"]);
                    _Office.Address.Locality.City = Convert.ToString(_Dr["LocalityCity"]);
                    _Office.Address.Locality.State = Convert.ToString(_Dr["LocalityState"]);
                    _Office.Address.Locality.ZIP = Convert.ToString(_Dr["LocalityZIP"]);
                    _Office.Address.Locality.Code = Convert.ToString(_Dr["LocalityCode"]);
                    _Office.Address.Locality.CityCode = Convert.ToString(_Dr["LocalityCityCode"]);
                    _Office.Address.Locality.StateCode = Convert.ToString(_Dr["LocalityStateCode"]);



                }

            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                _Cnn.Close();
            }

            return _Office;
        }


        public static List<PostOffice> GetPostOfficeByStateCourier(string pState, Courier pCourier)
        {
            List<PostOffice> _Offices = new List<PostOffice>();

            SqlConnection _Cnn = new SqlConnection();

            try
            {


                _Cnn = Connection.Instancia.SetConnection();
                SqlDataReader _Dr = null;
                SqlCommand _Cmd = new SqlCommand("spPostOfficeGetByStateCourierId", _Cnn);
                _Cmd.Parameters.Add("@LocalityState", SqlDbType.VarChar).Value = pState;
                _Cmd.Parameters.Add("@CourierId", SqlDbType.Int).Value = pCourier.Id;


                _Cmd.CommandType = CommandType.StoredProcedure;

                _Cnn.Open();

                _Dr = _Cmd.ExecuteReader();

                while (_Dr.Read())
                {
                    PostOffice _Office = new PostOffice();

                    _Office = new PostOffice();
                    _Office.Id = Convert.ToInt32(_Dr["PostOfficeId"]);
                    _Office.Name = Convert.ToString(_Dr["PostOfficeName"]);
                    _Office.Code = Convert.ToInt32(_Dr["PostOfficeCode"]);
                    _Office.Courier = pCourier;

                    _Office.Address = new Address();
                    _Office.Address.Id = 0;
                    _Office.Address.Line1 = Convert.ToString(_Dr["PostOfficeAddressLine1"]);
                    //_Office.Address.Line2 = Convert.ToString(_Dr["AddressLine2"]);
                    _Office.Address.Phone = Convert.ToString(_Dr["PostOfficePhone"]);
                    //_Office.Address.Observation = Convert.ToString(_Dr["AddressObservation"]);

                    _Office.Address.Locality = new Locality();
                    _Office.Address.Locality.Id = Convert.ToInt32(_Dr["LocalityId"]);
                    _Office.Address.Locality.Name = Convert.ToString(_Dr["LocalityName"]);
                    _Office.Address.Locality.City = Convert.ToString(_Dr["LocalityCity"]);
                    _Office.Address.Locality.State = Convert.ToString(_Dr["LocalityState"]);
                    _Office.Address.Locality.ZIP = Convert.ToString(_Dr["LocalityZIP"]);
                    _Office.Address.Locality.Code = Convert.ToString(_Dr["LocalityCode"]);
                    _Office.Address.Locality.CityCode = Convert.ToString(_Dr["LocalityCityCode"]);
                    _Office.Address.Locality.StateCode = Convert.ToString(_Dr["LocalityStateCode"]);


                    _Offices.Add(_Office);
                }

            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                _Cnn.Close();
            }

            return _Offices;
        }


        public static List<string> GetStatesByCourier(int  pCourierId)
        {
            List<string> _States=new List<string>();

            SqlConnection _Cnn = new SqlConnection();

            try
            {


                _Cnn = Connection.Instancia.SetConnection();
                SqlDataReader _Dr = null;
                SqlCommand _Cmd = new SqlCommand("spLocalityStatesGetByCourier", _Cnn);
                _Cmd.Parameters.Add("@LocalityCourierId", SqlDbType.Int).Value = pCourierId;


                _Cmd.CommandType = CommandType.StoredProcedure;

                _Cnn.Open();

                _Dr = _Cmd.ExecuteReader();

                while (_Dr.Read())
                {
                    string _State = "";

                    _State = Convert.ToString(_Dr["LocalityState"]);

                    _States.Add(_State);
                }

            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                _Cnn.Close();
            }

            return _States;
        }


        public static List<string> GetCitiesByCourierState(int pCourierId, string pState)
        {
            List<string> _Cities = new List<string>();

            SqlConnection _Cnn = new SqlConnection();

            try
            {


                _Cnn = Connection.Instancia.SetConnection();
                SqlDataReader _Dr = null;
                SqlCommand _Cmd = new SqlCommand("spLocalityCitiesGetByCourierState", _Cnn);
                _Cmd.Parameters.Add("@LocalityCourierId", SqlDbType.Int).Value = pCourierId;
                _Cmd.Parameters.Add("@LocalityState", SqlDbType.VarChar).Value = pState;

                _Cmd.CommandType = CommandType.StoredProcedure;

                _Cnn.Open();

                _Dr = _Cmd.ExecuteReader();

                while (_Dr.Read())
                {
                    string _State = "";

                    _State = Convert.ToString(_Dr["LocalityCity"]);

                    _Cities.Add(_State);
                }

            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                _Cnn.Close();
            }

            return _Cities;
        }



        public static List<string> GetLocalitiesByCourierStateCity(int pCourierId, string pState, string pCity)
        {
            List<string> _Localities = new List<string>();

            SqlConnection _Cnn = new SqlConnection();

            try
            {


                _Cnn = Connection.Instancia.SetConnection();
                SqlDataReader _Dr = null;
                SqlCommand _Cmd = new SqlCommand("spLocalitiesGetByCourierStateCity", _Cnn);
                _Cmd.Parameters.Add("@LocalityCourierId", SqlDbType.Int).Value = pCourierId;
                _Cmd.Parameters.Add("@LocalityState", SqlDbType.VarChar).Value = pState;
                _Cmd.Parameters.Add("@LocalityCity", SqlDbType.VarChar).Value = pCity;

                _Cmd.CommandType = CommandType.StoredProcedure;

                _Cnn.Open();

                _Dr = _Cmd.ExecuteReader();

                while (_Dr.Read())
                {
                    string _State = "";

                    _State = Convert.ToString(_Dr["LocalityName"]);

                    _Localities.Add(_State);
                }

            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                _Cnn.Close();
            }

            return _Localities;
        }


        #endregion

        //UPDATES
        #region UPDATES
        public static void UpdateAddress(Address pAddress)
        {
            SqlConnection _Cnn = new SqlConnection();

            try
            {
                _Cnn = Connection.Instancia.SetConnection();

                SqlCommand _Cmd = new SqlCommand("spUpdateAddress", _Cnn);
                _Cmd.CommandType = CommandType.StoredProcedure;


                _Cmd.Parameters.Add("@AddressId", SqlDbType.Int).Value = pAddress.Id;



                if(pAddress.Locality!=null && pAddress.Locality.Id!=0)
                    _Cmd.Parameters.Add("@AddressLocalityId", SqlDbType.Int).Value = pAddress.Locality.Id;
                else 
                    _Cmd.Parameters.Add("@AddressLocalityId", SqlDbType.Int).Value = DBNull.Value;



                _Cmd.Parameters.Add("@AddressLine1", SqlDbType.VarChar).Value = pAddress.Line1;

                _Cmd.Parameters.Add("@AddressLine2", SqlDbType.VarChar).Value = pAddress.Line2;
                _Cmd.Parameters.Add("@AddressPhone", SqlDbType.VarChar).Value = pAddress.Phone;
                _Cmd.Parameters.Add("@AddressObservation", SqlDbType.VarChar).Value = pAddress.Observation;



                _Cnn.Open();
                _Cmd.ExecuteNonQuery();



            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                _Cnn.Close();
            }

        
        }





        public static void UpdateReceiver(Receiver pReceiver)
        {
            SqlConnection _Cnn = new SqlConnection();



            try
            {
                UpdateAddress(pReceiver.Address);


                _Cnn = Connection.Instancia.SetConnection();

                SqlCommand _Cmd = new SqlCommand("spUpdateReceiver", _Cnn);
                _Cmd.CommandType = CommandType.StoredProcedure;


                _Cmd.Parameters.Add("@ReceiverId", SqlDbType.Int).Value = pReceiver.Id;
                _Cmd.Parameters.Add("@ReceiverAddressId", SqlDbType.Int).Value = pReceiver.Address.Id;
                _Cmd.Parameters.Add("@ReceiverName", SqlDbType.VarChar).Value = pReceiver.Name;
                _Cmd.Parameters.Add("@ReceiverLastname", SqlDbType.VarChar).Value = pReceiver.Lastname;
                _Cmd.Parameters.Add("@ReceiverEmail", SqlDbType.VarChar).Value = pReceiver.Email;
                _Cmd.Parameters.Add("@ReceiverPhone", SqlDbType.VarChar).Value = pReceiver.Phone;
                _Cmd.Parameters.Add("@ReceiverPassport", SqlDbType.VarChar).Value = pReceiver.Passport;

      

                _Cnn.Open();
                _Cmd.ExecuteNonQuery();


          
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                _Cnn.Close();
            }

        }


        public static void UpdatePackage(Package pPackage, Shipping pShipping)
        {
            SqlConnection _Cnn = new SqlConnection();

            try
            {

                _Cnn = Connection.Instancia.SetConnection();

                SqlCommand _Cmd = new SqlCommand("spUpdatePackage", _Cnn);
                _Cmd.CommandType = CommandType.StoredProcedure;


                _Cmd.Parameters.Add("@PackageId", SqlDbType.Int).Value = pPackage.Id;
                _Cmd.Parameters.Add("@PackageShippingId", SqlDbType.Int).Value = pShipping.Id;
                _Cmd.Parameters.Add("@PackageWeight", SqlDbType.Decimal).Value = pPackage.Weight;
                _Cmd.Parameters.Add("@PackageHeight", SqlDbType.Decimal).Value = pPackage.Height;
                _Cmd.Parameters.Add("@PackageWidth", SqlDbType.Decimal).Value = pPackage.Width;
                _Cmd.Parameters.Add("@PackageDepth", SqlDbType.Decimal).Value = pPackage.Depth;
                _Cmd.Parameters.Add("@PackageReference", SqlDbType.VarChar).Value = pPackage.Reference;


                _Cnn.Open();
                _Cmd.ExecuteNonQuery();


            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                _Cnn.Close();
            }

        }


        public static void UpdateShipping(Shipping pShipping)
        {
            SqlConnection _Cnn = new SqlConnection();

            try
            {

                UpdateReceiver(pShipping.Receiver);


                _Cnn = Connection.Instancia.SetConnection();

                SqlCommand _Cmd = new SqlCommand("spUpdateShipping", _Cnn);
                _Cmd.CommandType = CommandType.StoredProcedure;

                _Cmd.Parameters.Add("@ShippingId", SqlDbType.Int).Value = pShipping.Id;
                _Cmd.Parameters.Add("@ShippingCreatedAt", SqlDbType.DateTime).Value = pShipping.CreatedAt;

                if(pShipping.Courier!=null)
                    _Cmd.Parameters.Add("@ShippingCourierId", SqlDbType.Int).Value = pShipping.Courier.Id;

                else
                    _Cmd.Parameters.Add("@ShippingCourierId", SqlDbType.Int).Value = DBNull.Value;


                _Cmd.Parameters.Add("@ShippingReceiverId", SqlDbType.Int).Value = pShipping.Receiver.Id;

                if (pShipping.PostOffice != null&&pShipping.PostOffice.Id!=0)
                    _Cmd.Parameters.Add("@ShippingPostOfficeId", SqlDbType.Int).Value = pShipping.PostOffice.Id;

                else
                    _Cmd.Parameters.Add("@ShippingPostOfficeId", SqlDbType.Int).Value = DBNull.Value;

                if (pShipping.CashOnDelivery)
                {

                    _Cmd.Parameters.Add("@ShippingCashOnDelivery", SqlDbType.Bit).Value = 1;
                }


                else
                {
                    _Cmd.Parameters.Add("@ShippingCashOnDelivery", SqlDbType.Bit).Value = 0;
                }

                if (pShipping.ReferenceId != null)
                    _Cmd.Parameters.Add("@ShippingReferenceId", SqlDbType.VarChar).Value = pShipping.ReferenceId;

                else
                    _Cmd.Parameters.Add("@ShippingReferenceId", SqlDbType.VarChar).Value = DBNull.Value;

                _Cmd.Parameters.Add("@ShippingOrderId", SqlDbType.VarChar).Value = pShipping.OrderId;

                _Cmd.Parameters.Add("@ShippingFinancialStatus", SqlDbType.VarChar).Value = pShipping.FinancialStatus;


                _Cmd.Parameters.Add("@ShippingTotalLinesItemPrice", SqlDbType.Decimal).Value = pShipping.TotalLinesItemPrice;


                if(pShipping.GuideType!=null)
                    _Cmd.Parameters.Add("@ShippingGuideTypeId", SqlDbType.Int).Value = pShipping.GuideType.Id;
                else
                    _Cmd.Parameters.Add("@ShippingGuideTypeId", SqlDbType.Int).Value = DBNull.Value;


                if (pShipping.DeliveryType != null)
                    _Cmd.Parameters.Add("@ShippingDeliveryTypeId", SqlDbType.Int).Value = pShipping.DeliveryType.Id;
                else
                    _Cmd.Parameters.Add("@ShippingDeliveryTypeId", SqlDbType.Int).Value = DBNull.Value;



                if (pShipping.Info != null)
                    _Cmd.Parameters.Add("@ShippingInfo", SqlDbType.VarChar).Value = pShipping.Info;

                else
                    _Cmd.Parameters.Add("@ShippingInfo", SqlDbType.VarChar).Value = DBNull.Value;


                if (pShipping.Note != null)
                    _Cmd.Parameters.Add("@ShippingNote", SqlDbType.VarChar).Value = pShipping.Note;
                else
                    _Cmd.Parameters.Add("@ShippingNote", SqlDbType.VarChar).Value = DBNull.Value;


                if (pShipping.FulfillmentId != null)
                    _Cmd.Parameters.Add("@ShippingFulfillmentId", SqlDbType.VarChar).Value = pShipping.FulfillmentId;
                else
                    _Cmd.Parameters.Add("@ShippingFulfillmentId", SqlDbType.VarChar).Value = DBNull.Value;

                _Cmd.Parameters.Add("@ShippingShopifyId", SqlDbType.BigInt).Value = pShipping.ShopifyId;




                _Cnn.Open();
                _Cmd.ExecuteNonQuery();



                foreach (Package _Package in pShipping.Packages)
                {
                    UpdatePackage(_Package,pShipping);
                }



            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                _Cnn.Close();
            }

        }

        #endregion

        //LOGIN
        public static Account Login(string pUserName, string pPassword)
        {
            Account _Account = null;

            SqlConnection _Cnn = new SqlConnection();

            try
            {

                _Cnn = Connection.Instancia.SetConnection();
                SqlDataReader _Dr = null;
                SqlCommand _Cmd = new SqlCommand("spAccountLogin", _Cnn);
                _Cmd.Parameters.Add("@AccountUsername", SqlDbType.VarChar).Value = pUserName;
                _Cmd.Parameters.Add("@AccountPassword", SqlDbType.VarChar).Value = pPassword;


                _Cmd.CommandType = CommandType.StoredProcedure;

                _Cnn.Open();

                _Dr = _Cmd.ExecuteReader();

                while (_Dr.Read())
                {
                    _Account = new Account();
                    _Account.Id = Convert.ToInt32(_Dr["AccountId"]);
                    _Account.Name = Convert.ToString(_Dr["AccountName"]);
                    _Account.Username = Convert.ToString(_Dr["AccountUsername"]);
                    _Account.Password = Convert.ToString(_Dr["AccountPassword"]);
                }

            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                _Cnn.Close();
            }


            return _Account;
        }
    }
}
