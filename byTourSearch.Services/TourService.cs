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
                string sql = String.Format("(SELECT abs.check_in_date, abs.duration, abs.meal_type, abs.adults_count, abs.room_type, abs.price AS absPrice, tez.price AS tezPrice " +
                    "FROM (SELECT * FROM `tours` JOIN tourfirm_hotels ON tourfirm_hotel_id = entry_id where internal_hotel_id = {0} AND tourfirm_id = 1) AS tez " +
                    "RIGHT JOIN (SELECT * FROM `tours` JOIN tourfirm_hotels ON tourfirm_hotel_id = entry_id where internal_hotel_id = {0} AND tourfirm_id = 2) AS abs " +
                        "ON tez.check_in_date = abs.check_in_date AND abs.duration = tez.duration AND abs.meal_type = tez.meal_type " +
                            "AND tez.adults_count = abs.adults_count AND tez.room_type = abs.room_type)" +
                    "UNION (SELECT tez.check_in_date, tez.duration, tez.meal_type, tez.adults_count, tez.room_type, abs.price AS absPrice, tez.price AS tezPrice " +
                    "FROM (SELECT * FROM `tours` JOIN tourfirm_hotels ON tourfirm_hotel_id = entry_id where internal_hotel_id = {0} AND tourfirm_id = 1) AS tez " +
                    "LEFT JOIN (SELECT * FROM `tours` JOIN tourfirm_hotels ON tourfirm_hotel_id = entry_id where internal_hotel_id = {0} AND tourfirm_id = 2) AS abs " +
                        "ON tez.check_in_date = abs.check_in_date AND abs.duration = tez.duration AND abs.meal_type = tez.meal_type " +
                            "AND tez.adults_count = abs.adults_count AND tez.room_type = abs.room_type)", internalHotelId);
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
                            Hotel = null,
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

        public IEnumerable<Tour> GetNonMatchedTours(int internalHotelId)
        {
            using (var connection = new MySqlConnection("Server = localhost; Database = bytoursearch; Uid = time; Pwd = somepass;"))
            {
                connection.Open();
                string sql = String.Format("SELECT tourfirm_id, check_in_date, duration, meal_type, adults_count, room_type, price " +
                    "FROM `tours` JOIN tourfirm_hotels ON tourfirm_hotel_id = entry_id where internal_hotel_id = {0}", internalHotelId);
                var command = new MySqlCommand(sql, connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return new Tour()
                        {
                            Hotel = null,
                            AdultsCount = reader.GetInt32("adults_count"),
                            CheckInDate = reader.GetDateTime("check_in_date"),
                            duration = reader.GetInt32("duration"),
                            MealType = reader.GetString("meal_type"),
                            RoomType = reader.GetString("room_type"),
                            Prices = new Dictionary<int, int?>()
                            {
                                { reader.GetInt32("tourfirm_id"), reader.GetInt32("price") }
                            }
                        };
                    }
                }
            }
        }
    }
}
