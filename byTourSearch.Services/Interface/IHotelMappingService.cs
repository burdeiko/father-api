using System;
using System.Collections.Generic;
using System.Text;
using byTourSearch.Services.Model;

namespace byTourSearch.Services.Interface
{
    public interface IHotelMappingService
    {
        void MapExternalHotel(int externalHotelId, string internalHotelName, int countryId);
        IEnumerable<HotelMapping> GetAllMappings();
    }
}
