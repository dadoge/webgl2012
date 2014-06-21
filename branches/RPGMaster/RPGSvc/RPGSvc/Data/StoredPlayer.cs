using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using RPGSvc.Entities;
using System.Data.SqlClient;

namespace RPGSvc.Data
{
    public class StoredPlayer
    {
        //
        public Player GetPlayerByPlayerID(int id)
        {
            SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RPGMasterDb"].ConnectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "GetPlayerByPlayerID";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter playerID = new SqlParameter("@PlayerID", SqlDbType.Int);

            playerID.Value = id;
            command.Parameters.Add(playerID);

            connection.Open();
            SqlDataReader dr;
            dr = command.ExecuteReader();

            var player = new Player();

            if (dr.HasRows)
            {
                dr.Read();
                player.Id = dr.GetInt32(0);
                player.Name = dr.GetString(1);
                player.ImgSrc = dr.GetString(2);
                player.History = dr.GetString(3);
                player.Level = dr.GetInt16(4);
                player.Age = dr.GetInt16(5);
            }
            connection.Close();
            dr.Close();
            return player;
        }
    }
}