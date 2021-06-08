using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DataTypes
{
    public class Shipping
    {
        private int _Id;
        private string _OrderId;
        private DateTime _CreatedAt;
        private double _TotalLinesItemPrice;
        private bool _CashOnDelivery;
        private string _FinancialStatus;
        private string _ReferenceId;
        private List<Package> _Packages;
        private Receiver _Receiver;

        private Courier _Courier;

        private PostOffice _PostOffice;
        private List<Label> _Labels;

        private GuideType _GuideType;
        private DeliveryType _DeliveryType;

        private string _Info;


        public Shipping()
        { 
        
        }

        public int Id { get => _Id; set => _Id = value; }
        public string OrderId { get => _OrderId; set => _OrderId = value; }
        public DateTime CreatedAt { get => _CreatedAt; set => _CreatedAt = value; }
        public double TotalLinesItemPrice { get => _TotalLinesItemPrice; set => _TotalLinesItemPrice = value; }
        public bool CashOnDelivery { get => _CashOnDelivery; set => _CashOnDelivery = value; }
        public string FinancialStatus { get => _FinancialStatus; set => _FinancialStatus = value; }
        public string ReferenceId { get => _ReferenceId; set => _ReferenceId = value; }
     
        public Receiver Receiver { get => _Receiver; set => _Receiver = value; }
        public Courier Courier { get => _Courier; set => _Courier = value; }

        public List<Package> Packages { get => _Packages; set => _Packages = value; }
        public PostOffice PostOffice { get => _PostOffice; set => _PostOffice = value; }


        public DeliveryType DeliveryType { get => _DeliveryType; set => _DeliveryType = value; }

        public GuideType GuideType { get => _GuideType; set => _GuideType = value; }

        public List<Label> Labels { get => _Labels; set => _Labels = value; }


        public string PackageReference { get => _Packages[0].Reference; }

        public string CourierName
        {
            get
            {
                if (_Courier != null)
                { return _Courier.Name; }
                else return "";
            }
        }

        public string LabelTrackingNumber
        {
            get
            {
                if (_Labels != null && _Labels.Count>0)
                {
                    return _Labels[0].TrackingNumber;
                }
                else
                {
                    return "";
                }
            }
        }

        public string NameLastname { get => _Receiver.Name + " " + _Receiver.Lastname; }


        public string ReceiverPassport { get => _Receiver.Passport; }
        public string Info { get => _Info; set => _Info = value; }
    }
}
