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
    public class HotelsController : Controller
    {
        [HttpGet("[action]")]
        public IEnumerable<MvcHotel> Hotels([FromServices] IHotelService service, int countryId)
        {
            return service.GetHotelByCountry(countryId).Select((hotel) => hotel.ToMvcHotel());
        }
    }
}