using System;
using System.Collections.Generic;
using System.Text;
using byTourSearch.Services.Model;

namespace byTourSearch.Services.Interface
{
    public interface ITravelAgencyService
    {
        IEnumerable<TravelAgency> GetTravelAgencies();
    }
}
