﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using API.DataTypes;
using System.Data;

namespace API.DataAccess
{
    public class DataAccess
    {
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

             

                _Cmd.Parameters.Add("@AddressLocalityId", SqlDbType.Int).Value = pAddress.Locality.Id;
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



        public static Locality GetLocalityByCourierNameState(string pName, string pState, Courier pCourier)
        {
            Locality _Locality = new Locality() ;

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
            Locality _Locality = new Locality();

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


        public static void InsertPostOffice(PostOffice pPostOffice)
        {
            SqlConnection _Cnn = new SqlConnection();

            try
            {

                pPostOffice.Address.Id=InsertAddress(pPostOffice.Address);

                _Cnn = Connection.Instancia.SetConnection();

                SqlCommand _Cmd = new SqlCommand("spInsertPostOffice", _Cnn);
                _Cmd.CommandType = CommandType.StoredProcedure;

                _Cmd.Parameters.Add("@PostOfficeCourierId", SqlDbType.Int).Value = pPostOffice.Courier.Id;
                _Cmd.Parameters.Add("@PostOfficeAddressId", SqlDbType.Int).Value = pPostOffice.Address.Id;
                _Cmd.Parameters.Add("@PostOfficeName", SqlDbType.VarChar).Value = pPostOffice.Name;

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

                pShipping.Receiver.Id=InsertReceiver(pShipping.Receiver);
                

                _Cnn = Connection.Instancia.SetConnection();

                SqlCommand _Cmd = new SqlCommand("spInsertShipping", _Cnn);
                _Cmd.CommandType = CommandType.StoredProcedure;

                _Cmd.Parameters.Add("@ShippingCreatedAt", SqlDbType.DateTime).Value = pShipping.CreatedAt;
                _Cmd.Parameters.Add("@ShippingCourierId", SqlDbType.Int).Value = pShipping.Courier.Id;
                _Cmd.Parameters.Add("@ShippingReceiverId", SqlDbType.Int).Value = pShipping.Receiver.Id;

                if(pShipping.PostOffice!=null)
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

                    _Shipping.Labels = GetLabelsByShippingId(_Shipping.Id);
                    _Shipping.Packages = GetPackagesByShippingId(_Shipping.Id);


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

                    //LEVANTAR DATA DE LABELS

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
    
}
}
