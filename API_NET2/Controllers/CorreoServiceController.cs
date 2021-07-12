using API.DataTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class CorreoServiceController : ControllerBase
    {
        [HttpPost]
        public Shipping CreateShipping(Shipping pShipping)
        {
            Shipping _Shipping = new Shipping();

            return _Shipping;
        }


    }
}
