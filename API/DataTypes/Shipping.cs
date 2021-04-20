using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DataTypes
{
    public class Shipping
    {
        private List<Package> _Packages;
        private Receiver _Receiver;
        private bool _CashOnDelivery;
        private Courier _Courier;
        private string _ReferenceId;
        private PostOffice _PostOffice;
        private List<Label> _Labels;
        private string _OrderId;

        public List<Package> Packages { get => _Packages; set => _Packages = value; }
        public Receiver Receiver { get => _Receiver; set => _Receiver = value; }
        public bool CashOnDelivery { get => _CashOnDelivery; set => _CashOnDelivery = value; }
        public Courier Courier { get => _Courier; set => _Courier = value; }
        public string ReferenceId { get => _ReferenceId; set => _ReferenceId = value; }
        public PostOffice PostOffice { get => _PostOffice; set => _PostOffice = value; }
        public List<Label> Labels { get => _Labels; set => _Labels = value; }
        public string OrderId { get => _OrderId; set => _OrderId = value; }

        public Shipping()
        { 
        
        }
    }
}
