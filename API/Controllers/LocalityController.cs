using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DataTypes;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalityController : ControllerBase
    {
        [HttpGet]
        public List<Locality> Get(Courier)
        {

            //Obtiene las localidades para ese courier

            List<Locality> _Localities = new List<Locality>();

            return _Localities;
        }
    }
}
