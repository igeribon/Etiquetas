using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DataTypes
{
    public class Address
    {
        private string street;
        private string doorNumber;
        private string line2;
        private Locality locality;
        private string phone;
        private string observation;

        public string Street { get => street; set => street = value; }
        public string DoorNumber { get => doorNumber; set => doorNumber = value; }
        public string Line2 { get => line2; set => line2 = value; }
        public Locality Locality { get => locality; set => locality = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Observation { get => observation; set => observation = value; }

        public Address()
        { 
        
        }
    }
}
