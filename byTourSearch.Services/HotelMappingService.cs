using System;
using System.Collections.Generic;
using System.Text;
using byTourSearch.Services.Interface;
using byTourSearch.Services.Model;
using MySql.Data.MySqlClient;
using byTourSearch.Services.Infrastructure;

namespace byTourSearch.Services
{
    public class HotelMappingService : IHotelMappingService
    {
        public IEnumerable<HotelMapping> GetAllMappings()
        {
            using (var connection = new MySqlConnection("Server = localhost; Database = bytoursearch; Uid = time; Pwd = somepass;"))
            {
                connection.Open();
                string sql = "SELECT entry_id, tourfirms.id AS tourfirmId, tourfirms.name AS tourfirmName, tourfirm_hotels.name, countries.id AS countryId, " +
                    "countries.name AS countryName, hotels.id AS hotelId, hotels.name AS hotelName " +
                    "FROM tourfirm_hotels LEFT JOIN hotels ON internal_hotel_id = hotels.id JOIN tourfirms ON tourfirm_id = tourfirms.id " +
                    "JOIN countries ON tourfirm_hotels.country_id = countries.id";
                var command = new MySqlCommand(sql, connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        bool isMapped = !reader.IsDBNull(reader.GetOrdinal("hotelId"));
                        yield return new HotelMapping()
                        {
                            EntryId = reader.GetInt32("entry_id"),
                            Agency = new TravelAgency()
                            {
                                Id = reader.GetInt32("tourfirmId"),
                                Name = reader.GetString("tourfirmName")
                            },
                            ExternalName = reader.GetString("name"),
                            Country = new Country()
                            {
                                Id = reader.GetInt32("countryId"),
                                Name = reader.GetStringSafe("countryName")
                            },
                            InternalHotel = isMapped ? new Hotel()
                            {
                                Country = new Country()
                                {
                                    Id = reader.GetInt32("countryId"),
                                    Name = reader.GetStringSafe("countryName")
                                },
                                Id = reader.GetInt32("hotelId"),
                                Name = reader.GetString("hotelName")
                            } : null
                        };
                    }
                }
            }
        }

        public void MapExternalHotel(int externalHotelId, string internalHotelName, int countryId)
        {
            using (var connection = new MySqlConnection("Server = localhost; Database = bytoursearch; Uid = time; Pwd = somepass;"))
            {
                if (internalHotelName == string.Empty)
                    internalHotelName = null;
                connection.Open();
                string sql = String.Format("SELECT id FROM hotels WHERE name = '{0}' LIMIT 1", internalHotelName);
                var command = new MySqlCommand(sql, connection);
                int? internalHotelId = (int?)command.ExecuteScalar();
                if (!internalHotelId.HasValue && internalHotelName != null)
                {
                    sql = String.Format("INSERT INTO hotels (name, country_id) VALUES('{0}', {1})", internalHotelName, countryId);
                    command = new MySqlCommand(sql, connection);
                    command.ExecuteNonQuery();
                    internalHotelId = (int)command.LastInsertedId;
                }
                sql = String.Format("UPDATE tourfirm_hotelS SET internal_hotel_id = {0} WHERE entry_id = {1}", internalHotelId.HasValue ? internalHotelId.ToString() : "NULL", externalHotelId.ToString());
                command = new MySqlCommand(sql, connection);
                command.ExecuteNonQuery();
            }
        }
    }
}
