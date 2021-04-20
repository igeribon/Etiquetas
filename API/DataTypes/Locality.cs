using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DataTypes
{
    public class Locality
    {
        private string _Name;
        private string _City;
        private string _State;
        private string _ZIP;

        public string Name { get => _Name; set => _Name = value; }
        public string City { get => _City; set => _City = value; }
        public string State { get => _State; set => _State = value; }
        public string ZIP { get => _ZIP; set => _ZIP = value; }


        public Locality()
        { 
        
        
        }
    }
}
