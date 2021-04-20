using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DataTypes
{
    public class Package
    {
        private double _Weight;
        private double _Height;
        private double _Width;
        private double _Depth;
        private string _Reference;

        public double Weight { get => _Weight; set => _Weight = value; }
        public double Height { get => _Height; set => _Height = value; }
        public double Width { get => _Width; set => _Width = value; }
        public double Depth { get => _Depth; set => _Depth = value; }
        public string Reference { get => _Reference; set => _Reference = value; }

        public Package()
        { 
        
        }
    }
}
