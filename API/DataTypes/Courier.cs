using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DataTypes
{
    public class Courier
    {
        private string _Nombre;
        private string _Cuntry;

        public string Nombre { get => _Nombre; set => _Nombre = value; }
        public string Cuntry { get => _Cuntry; set => _Cuntry = value; }

        public Courier()
        { 
        
        }
    }
}
