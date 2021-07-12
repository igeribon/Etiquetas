using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DataTypes
{
    public class PostOffice
    {
        private int _Id;
        private string _Name;
        private Address _Address;
        private Courier _Courier;
        private int _Code;

        public int Id { get => _Id; set => _Id = value; }

        public Courier Courier { get => _Courier; set => _Courier = value; }
        public int Code { get => _Code; set => _Code = value; }

        public string Name { get => _Name; set => _Name = value; }
        public Address Address { get => _Address; set => _Address = value; }
   
      
        public PostOffice()
        { 
        
        }
    }
}
