using System;
using System.Collections.Generic;
using System.Text;
using byTourSearch.Services.Interface;
using byTourSearch.Services.Model;
using MySql.Data.MySqlClient;

namespace byTourSearch.Services
{
    public class HotelService : IHotelService
    {
        public IEnumerable<Hotel> GetHotelByCountry(int countryId)
        {
            using (var connection = new MySqlConnection("Server = localhost; Database = bytoursearch; Uid = time; Pwd = somepass;"))
            {
                connection.Open();
                string sql = String.Format("SELECT id, name FROM hotels WHERE country_id = {0}", countryId.ToString());
                var command = new MySqlCommand(sql, connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return new Hotel()
                        {
                            Id = reader.GetInt32("id"),
                            Name = reader.GetString("name")
                        };
                    }
                }
            }
        }
    }
}
