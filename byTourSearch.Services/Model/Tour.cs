using System;
using System.Collections.Generic;
using System.Text;

namespace byTourSearch.Services.Model
{
    public class Tour
    {
        public Hotel Hotel { get; set; }
        public DateTime CheckInDate { get; set; }
        public int duration { get; set; }
        public string MealType { get; set; }
        public int AdultsCount { get; set; }
        public string RoomType { get; set; }
        /// <summary>
        /// The key is a TravelAgency id
        /// </summary>
        public Dictionary<int, int?> Prices { get; set; }
    }
}
