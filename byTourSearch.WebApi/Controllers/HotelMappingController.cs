using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using byTourSearch.PresentationLayer.Models;
using byTourSearch.PresentationLayer.ModelMappings;
using byTourSearch.Services.Interface;

namespace byTourSearch.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    public class HotelMappingController : Controller
    {
        [HttpGet("[action]")]
        public IEnumerable<MvcHotelMapping> HotelMappings([FromServices] IHotelMappingService service)
        {
            return service.GetAllMappings().Select((mapping) => mapping.ToMvcHotelMapping());
        }

        [HttpPost("[action]")]
        public void HotelMappings([FromServices] IHotelMappingService service, [FromBody]HotelMappingToGet newMapping)
        {
            service.MapExternalHotel(newMapping.externalHotelId, newMapping.internalHotelName, newMapping.countryId);
        }

        public class HotelMappingToGet
        {
            public int externalHotelId;
            public string internalHotelName;
            public int countryId;
        }
    }
}
