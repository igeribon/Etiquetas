using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DataTypes;

namespace API.Controllers
{
    public interface IServiceController
    {
        Shipping CreateShipping(DTOShipping pShipping);

        Shipping GetShippingByOrderId(string pId);
       
    }
}
