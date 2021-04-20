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
    public class PostOfficeController : ControllerBase
    {
        [HttpGet]
        public List<PostOffice> Get(Courier)
        {

            //Obtiene las localidades para ese courier

            List<PostOffice> _PostOffices = new List<PostOffice>();

            return _PostOffices;
        }

    }
}
