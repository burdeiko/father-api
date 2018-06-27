using System;
using System.Collections.Generic;
using System.Text;

namespace byTourSearch.Services.Model
{
    public class SearchOptions
    {
		public int? HotelId { get; set; }
		public string CheckInDateFrom { get; set; }
		public string CheckInDateTo { get; set; }
		public int durationFrom { get; set; }
		public int durationTo { get; set; }
		public string MealType { get; set; }
		public int? AdultsCount { get; set; }
		public string RoomType { get; set; }
		public string HotelName { get; set; }
	}
}
