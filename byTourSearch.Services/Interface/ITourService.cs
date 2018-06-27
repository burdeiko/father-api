using System;
using System.Collections.Generic;
using System.Text;
using byTourSearch.Services.Model;

namespace byTourSearch.Services.Interface
{
    public interface ITourService
    {
        IEnumerable<Tour> GetTours(int internalHotelId);
		IEnumerable<Tour> Search(SearchOptions options);
    }
}
