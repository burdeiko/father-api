using System;
using System.Collections.Generic;
using System.Text;

namespace byTourSearch.PresentationLayer.Models
{
    public class MvcHotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public MvcCountry Country { get; set; }
    }
}
