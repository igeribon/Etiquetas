using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DataTypes
{
    public class DeliveryType
    {
        private int _Id;
        private string _Name;


        public int Id { get => _Id; set => _Id = value; }
        public string Name { get => _Name; set => _Name = value; }

        public DeliveryType()
        {

        }

    }
}
