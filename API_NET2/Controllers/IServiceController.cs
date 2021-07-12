using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DataTypes;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public interface IServiceController
    {
        IActionResult CreateShipping(DTOShipping pShipping);

        Shipping GetShippingByOrderId(string pId);
       
    }
}
