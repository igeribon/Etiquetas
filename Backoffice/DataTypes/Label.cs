using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DataTypes
{
    public class Label
    {
        private int _Id;
        private string _Identifier;
        private PostOffice _PostOffice;
        private string _TrackingNumber;
        private byte[] _Data;

        public int Id { get => _Id; set => _Id = value; }
        public string Identifier { get => _Identifier; set => _Identifier = value; }
        public PostOffice PostOffice { get => _PostOffice; set => _PostOffice = value; }
        public string TrackingNumber { get => _TrackingNumber; set => _TrackingNumber = value; }
        public byte[] Data { get => _Data; set => _Data = value; }


        public Label()
        { 
        
        }
    }
}
