using System;
using System.Collections.Generic;
using System.Text;
using byTourSearch.Services.Interface;
using byTourSearch.Services.Model;
using MySql.Data.MySqlClient;

namespace byTourSearch.Services
{
    public class TravelAgencyService : ITravelAgencyService
    {
        public IEnumerable<TravelAgency> GetTravelAgencies()
        {
            using (var connection = new MySqlConnection("Server = localhost; Database = bytoursearch; Uid = time; Pwd = somepass;"))
            {
                connection.Open();
                string sql = "SELECT * FROM tourfirms";
                var command = new MySqlCommand(sql, connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return new TravelAgency()
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
