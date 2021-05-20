using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DataTypes
{
    public class Receiver
    {
        private int _Id;
        private string _Name;
        private string _Lastname;
        private string _Email;
        private string _Phone;
        private string _Passport;
        private Address _Address;

        public string Name { get => _Name; set => _Name = value; }
        public string Lastname { get => _Lastname; set => _Lastname = value; }
        public string Email { get => _Email; set => _Email = value; }
        public string Phone { get => _Phone; set => _Phone = value; }
        public string Passport { get => _Passport; set => _Passport = value; }
        public Address Address { get => _Address; set => _Address = value; }
        public int Id { get => _Id; set => _Id = value; }

        public string AddressLine1 { get => _Address.Line1; }

        public string LocalityStateName { get => _Address.Locality.State; }

        public string LocalityCityName { get => _Address.Locality.City; }
        
        public string LocalityName { get => _Address.Locality.Name; }




        public Receiver()
        {
            _Id = 0;
            _Name = "";
            _Lastname = "";
            _Email = "";
            _Phone = "";
            _Passport = "";
            _Address = new Address();
        }
    }
}
