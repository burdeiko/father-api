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
    public class TravelAgencyController: Controller
    {
        [HttpGet("[action]")]
        public IEnumerable<MvcTravelAgency> TravelAgencies([FromServices] ITravelAgencyService service)
        {
            return service.GetTravelAgencies().Select((agency) => agency.ToMvcTravelAgency());
        }
    }
}
