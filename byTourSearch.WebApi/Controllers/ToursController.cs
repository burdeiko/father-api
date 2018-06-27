using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using byTourSearch.Services.Model;
using byTourSearch.Services.Interface;
using byTourSearch.PresentationLayer.ModelMappings;

namespace byTourSearch.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    public class ToursController : Controller
    {
        [HttpGet("[action]")]
        public IEnumerable<MvcTour> Tours([FromServices] ITourService service, int hotelId)
        {
            return service.GetTours(hotelId).Select((tour) => tour.ToMvcTour());
        }

        [HttpGet("[action]")]
        public IEnumerable<MvcTour> Search([FromServices] ITourService service, SearchOptions options)
        {
            return service.Search(options).Select((tour) => tour.ToMvcTour());
        }
    }
}