using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using RPGSvc.Entities;
using System.Data.SqlClient;

namespace RPGSvc.Data
{
    public class StoredRaces
    {
        public List<Race> GetRaces()
        {

            SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RPGMasterDb"].ConnectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "GetRaces";
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            SqlDataReader dr;
            dr = command.ExecuteReader();

            var raceList = new List<Race>();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    var race = new Race();
                    race.Id = dr.GetInt32(0);
                    race.Name = dr.GetString(1);
                    race.Description = dr.GetString(2);

                    raceList.Add(race);
                }
            }
            connection.Close();
            return raceList;
        }
    }
}