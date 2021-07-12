using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DataTypes
{
    public class Courier
    {
        private int _Id;
        private string _Name;
        private string _Country;

        public string Name { get => _Name; set => _Name = value; }
        public string Country { get => _Country; set => _Country = value; }
        public int Id { get => _Id; set => _Id = value; }

        public Courier()
        { 
        
        }
    }
}
