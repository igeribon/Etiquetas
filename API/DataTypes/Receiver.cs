using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DataTypes
{
    public class Receiver
    {
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

        public Receiver()
        { 
        
        }
    }
}
