using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using RPGSvc.Entities;
using System.Data.SqlClient;

namespace RPGSvc.Data
{
    public class StoredStat
    {
        //
        public List<Stat> GetStatsByPlayerID(int id)
        {

            SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RPGMasterDb"].ConnectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "GetStatsByPlayerID";
            command.CommandType = CommandType.StoredProcedure;


            SqlParameter playerID = new SqlParameter("@PlayerID", SqlDbType.Int);

            playerID.Value = id;
            command.Parameters.Add(playerID);

            connection.Open();
            SqlDataReader dr;
            dr = command.ExecuteReader();

            var statList = new List<Stat>();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    var stat = new Stat();
                    stat.Id = dr.GetInt32(0);
                    stat.Name = dr.GetString(1);
                    stat.Description = dr.GetString(2);
                    stat.Value = dr.GetDouble(3);

                    statList.Add(stat);
                }
            }
            connection.Close();
            return statList;
        }
    }
}