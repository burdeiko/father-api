using System;
using System.Collections.Generic;
using System.Text;
using byTourSearch.Services.Interface;
using byTourSearch.Services.Model;
using MySql.Data.MySqlClient;
using byTourSearch.Services.Infrastructure;

namespace byTourSearch.Services
{
    public class TourService : ITourService
    {
        public IEnumerable<Tour> GetTours(int internalHotelId)
        {
            using (var connection = new MySqlConnection("Server = localhost; Database = bytoursearch; Uid = time; Pwd = somepass;"))
            {
                connection.Open();

                string sql = String.Format("SELECT check_in_date, duration, meal_type, adults_count, room_type, tourfirm_hotel_id, absPrice, tezPrice, hotels.id AS hotelId, hotels.name AS hotelName, countries.id AS countryId, countries.name AS countryName FROM " +
					"((SELECT abs.check_in_date, abs.duration, abs.meal_type, abs.adults_count, abs.room_type, abs.tourfirm_hotel_id, abs.price AS absPrice, tez.price AS tezPrice " +
						"FROM (SELECT * FROM `tours` JOIN tourfirm_hotels ON tourfirm_hotel_id = entry_id where internal_hotel_id = {0} AND tourfirm_id = 1) AS tez " +
						"RIGHT JOIN (SELECT * FROM `tours` JOIN tourfirm_hotels ON tourfirm_hotel_id = entry_id where internal_hotel_id = {0} AND tourfirm_id = 2) AS abs " +
							"ON tez.check_in_date = abs.check_in_date AND abs.duration = tez.duration AND abs.meal_type = tez.meal_type " +
								"AND tez.adults_count = abs.adults_count AND tez.room_type = abs.room_type)" +
						"UNION (SELECT tez.check_in_date, tez.duration, tez.meal_type, tez.adults_count, tez.room_type, tez.tourfirm_hotel_id, abs.price AS absPrice, tez.price AS tezPrice " +
						"FROM (SELECT * FROM `tours` JOIN tourfirm_hotels ON tourfirm_hotel_id = entry_id where internal_hotel_id = {0} AND tourfirm_id = 1) AS tez " +
						"LEFT JOIN (SELECT * FROM `tours` JOIN tourfirm_hotels ON tourfirm_hotel_id = entry_id where internal_hotel_id = {0} AND tourfirm_id = 2) AS abs " +
							"ON tez.check_in_date = abs.check_in_date AND abs.duration = tez.duration AND abs.meal_type = tez.meal_type " +
								"AND tez.adults_count = abs.adults_count AND tez.room_type = abs.room_type)) AS tours " +
					"JOIN tourfirm_hotels ON entry_id = tours.tourfirm_hotel_id " +
					"JOIN hotels ON internal_hotel_id = hotels.id " +
					"JOIN countries ON hotels.country_id = countries.id", internalHotelId);
                var command = new MySqlCommand(sql, connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var prices = new Dictionary<int, int?>();
                        prices.Add(1, reader.GetInt32Safe("tezPrice"));
                        prices.Add(2, reader.GetInt32Safe("absPrice"));
						yield return new Tour()
						{
							Hotel = new Hotel()
							{
								Country = new Country()
								{
									Id = reader.GetInt32("countryId"),
									Name = reader.GetString("countryName")
								},
								Id = reader.GetInt32("hotelId"),
								Name = reader.GetString("hotelName")
							},
                            AdultsCount = reader.GetInt32("adults_count"),
                            CheckInDate = reader.GetDateTime("check_in_date"),
                            duration = reader.GetInt32("duration"),
                            MealType = reader.GetString("meal_type"),
                            RoomType = reader.GetString("room_type"),
                            Prices = prices
                        };
                    }
                }
            }
        }

		public IEnumerable<Tour> Search(SearchOptions options)
		{
			using (var connection = new MySqlConnection("Server = localhost; Database = bytoursearch; Uid = time; Pwd = somepass;"))
			{
				connection.Open();
				string sql = String.Format(
					"SELECT check_in_date, duration, meal_type, adults_count, room_type, price, tourfirm_id, hotels.id AS hotelId, hotels.name AS hotelName, countries.id AS countryId, countries.name AS countryName " +
						"FROM tours JOIN tourfirm_hotels ON tourfirm_hotel_id = entry_id " +
						"JOIN hotels ON internal_hotel_id = hotels.id " +
						"JOIN countries ON hotels.country_id = countries.id " +
							"WHERE (duration BETWEEN {0} AND {1}) AND (check_in_date BETWEEN '{2}' AND '{3}')", 
					options.durationFrom, options.durationTo, options.CheckInDateFrom, options.CheckInDateTo);
				if (options.HotelId.HasValue)
					sql += String.Format(" AND hotels.id = {0}", options.HotelId);
				if (options.HotelName != null)
					sql += String.Format(" AND hotels.name LIKE '%{0}%'", options.HotelName);
				var command = new MySqlCommand(sql, connection);
				using (var reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						var prices = new Dictionary<int, int?>();
						prices.Add(reader.GetInt32("tourfirm_id"), reader.GetInt32("price"));
						yield return new Tour()
						{
							Hotel = new Hotel()
							{
								Country = new Country()
								{
									Id = reader.GetInt32("countryId"),
									Name = reader.GetString("countryName")
								},
								Id = reader.GetInt32("hotelId"),
								Name = reader.GetString("hotelName")
							},
							AdultsCount = reader.GetInt32("adults_count"),
							CheckInDate = reader.GetDateTime("check_in_date"),
							duration = reader.GetInt32("duration"),
							MealType = reader.GetString("meal_type"),
							RoomType = reader.GetString("room_type"),
							Prices = prices
						};
					}
				}
			}
		}
	}
}
