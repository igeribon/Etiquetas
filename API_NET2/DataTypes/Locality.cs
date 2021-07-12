using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DataTypes
{
    public class Locality
    {
        private int _Id;
        private Courier _Courier;
        private string _Name;
        private string _City;
        private string _State;
        private string _ZIP;

        private string _Code;
        private string _CityCode;
        private string _StateCode;
           
        public int Id { get => _Id; set => _Id = value; }
        public Courier Courier { get => _Courier; set => _Courier = value; }
        public string Name { get => _Name; set => _Name = value; }
        public string City { get => _City; set => _City = value; }
        public string State { get => _State; set => _State = value; }
        public string ZIP { get => _ZIP; set => _ZIP = value; }
        public string Code { get => _Code; set => _Code = value; }
        public string CityCode { get => _CityCode; set => _CityCode = value; }
        public string StateCode { get => _StateCode; set => _StateCode = value; }

        public Locality()
        { 
        
        
        }
    }
}
