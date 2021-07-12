using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DataTypes
{
    public class Address
    {
        private int _Id;
        private string _Line1;
        private string _Line2;
        private Locality _Locality;
        private string _Phone;
        private string _Observation;

        public Address()
        {
            _Id = 0;
            _Line1 = "";
            _Line2 = "";
            _Locality = new Locality();
            _Phone = "";
            _Observation = "";
        }

        public int Id { get => _Id; set => _Id = value; }
        public string Line1 { get => _Line1; set => _Line1 = value; }
        public string Line2 { get => _Line2; set => _Line2 = value; }
        public Locality Locality { get => _Locality; set => _Locality = value; }
        public string Phone { get => _Phone; set => _Phone = value; }
        public string Observation { get => _Observation; set => _Observation = value; }
     
    }
}
