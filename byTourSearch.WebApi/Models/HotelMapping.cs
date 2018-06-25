using System;
using System.Collections.Generic;
using System.Text;

namespace byTourSearch.PresentationLayer.Models
{
    public class MvcHotelMapping
    {
        public int EntryId { get; set; }
        public string ExternalName { get; set; }
        public MvcTravelAgency Agency { get; set; }
        public MvcHotel InternalHotel { get; set; }
        public MvcCountry Country { get; set; }
    }
}
