using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DataTypes;

namespace API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class ShippingController : ControllerBase
    {

        [HttpGet("shippings/{from}/{to}")]
        public List<Shipping> Get(DateTime from, DateTime to)
        {
            //Obtiene los shippings entre dos fechas para listarlos

            List<Shipping> _Shippings = new List<Shipping>();
                return _Shippings;
        }


        [HttpGet("shippings/{id}")]
        public Shipping Get(int id)
        {

            //Obtiene la información de un shipping para su detalle

            Shipping _Shipping = new Shipping();

            return _Shipping;
        }


        [HttpPost("shippings")]
        public bool Post(Shipping shipping)
        {
            //Genera/actualiza un registro en la tabla de shipping a partir de la información que nos llega 

            bool _Commited = false;


            return _Commited;

        }


        [HttpPost("shippings/{id}/labels")]
        public Label Post(int id)
        {
            //Genera una etiqueta para ese envío


            Label _Label = new Label();

            return _Label;
        }


    }
}
