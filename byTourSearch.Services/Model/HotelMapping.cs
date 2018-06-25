using System;
using System.Collections.Generic;
using System.Text;

namespace byTourSearch.Services.Model
{
    public class HotelMapping
    {
        public int EntryId { get; set; }
        public string ExternalName { get; set; }
        public Country Country { get; set; }
        public TravelAgency Agency { get; set; }
        public Hotel InternalHotel { get; set; }
    }
}
