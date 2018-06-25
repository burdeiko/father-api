using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using byTourSearch.Services.Model;
using byTourSearch.PresentationLayer.Models;

namespace byTourSearch.PresentationLayer.ModelMappings
{
    public static class ModelMappings
    {
        #region Country Mappings
        public static MvcCountry ToMvcCountry(this Country country)
        {
            if (country == null)
                return null;
            return new MvcCountry()
            {
                Id = country.Id,
                Name = country.Name
            };
        }

        public static Country ToSvcCountry(this MvcCountry country)
        {
            if (country == null)
                return null;
            return new Country()
            {
                Id = country.Id,
                Name = country.Name
            };
        }
        #endregion
        #region TravelAgency Mappings
        public static MvcTravelAgency ToMvcTravelAgency(this TravelAgency agency)
        {
            if (agency == null)
                return null;
            return new MvcTravelAgency()
            {
                Id = agency.Id,
                Name = agency.Name
            };
        }

        public static TravelAgency ToSvcTravelAgency(this MvcTravelAgency agency)
        {
            if (agency == null)
                return null;
            return new TravelAgency()
            {
                Id = agency.Id,
                Name = agency.Name
            };
        }
        #endregion
        #region Hotel Mappings
        public static MvcHotel ToMvcHotel(this Hotel hotel)
        {
            if (hotel == null)
                return null;
            return new MvcHotel()
            {
                Id = hotel.Id,
                Name = hotel.Name,
                Country = hotel.Country.ToMvcCountry()
            };
        }

        public static Hotel ToSvcHotel(this MvcHotel hotel)
        {
            if (hotel == null)
                return null;
            return new Hotel()
            {
                Id = hotel.Id,
                Name = hotel.Name,
                Country = hotel.Country.ToSvcCountry()
            };
        }
        #endregion
        #region HotelMapping Mappings
        public static MvcHotelMapping ToMvcHotelMapping(this HotelMapping mapping)
        {
            if (mapping == null)
                return null;
            return new MvcHotelMapping()
            {
                Agency = mapping.Agency.ToMvcTravelAgency(),
                EntryId = mapping.EntryId,
                ExternalName = mapping.ExternalName,
                InternalHotel = mapping.InternalHotel.ToMvcHotel(),
                Country = mapping.Country.ToMvcCountry()
            };
        }

        public static HotelMapping ToSvcHotelMapping(this MvcHotelMapping mapping)
        {
            if (mapping == null)
                return null;
            return new HotelMapping()
            {
                Agency = mapping.Agency.ToSvcTravelAgency(),
                EntryId = mapping.EntryId,
                ExternalName = mapping.ExternalName,
                InternalHotel = mapping.InternalHotel.ToSvcHotel(),
                Country = mapping.Country.ToSvcCountry()
            };
        }
        #endregion
        #region Tour Mappings
        public static MvcTour ToMvcTour(this Tour tour)
        {
            if (tour == null)
                return null;
            return new MvcTour()
            {
                AdultsCount = tour.AdultsCount,
                CheckInDate = tour.CheckInDate,
                duration = tour.duration,
                MealType = tour.MealType,
                Prices = tour.Prices,
                RoomType = tour.RoomType,
                Hotel = tour.Hotel.ToMvcHotel()
            };
        }
        public static Tour ToSvcTour(this MvcTour tour)
        {
            if (tour == null)
                return null;
            return new Tour()
            {
                AdultsCount = tour.AdultsCount,
                CheckInDate = tour.CheckInDate,
                duration = tour.duration,
                MealType = tour.MealType,
                Prices = tour.Prices,
                RoomType = tour.RoomType,
                Hotel = tour.Hotel.ToSvcHotel()
            };
        }
        #endregion
    }
}
