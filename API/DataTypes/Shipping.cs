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
        public List<Package> Packages { get => _Packages; set => _Packages = value; }
        public Receiver Receiver { get => _Receiver; set => _Receiver = value; }
        public Courier Courier { get => _Courier; set => _Courier = value; }
        public PostOffice PostOffice { get => _PostOffice; set => _PostOffice = value; }
        public List<Label> Labels { get => _Labels; set => _Labels = value; }
    }
}
