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
        public void DeleteUserPlayer(int id)
        {
            SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RPGMasterDb"].ConnectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "DeleteUserPlayer";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter PlayerID = new SqlParameter("@PlayerID", SqlDbType.Int);
            PlayerID.Value = id;
            command.Parameters.Add(PlayerID);

            connection.Open();
            command.ExecuteNonQuery();

            connection.Close();
        }

        public List<Player> GetUserPlayers(string userName)
        {
            SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RPGMasterDb"].ConnectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "GetUserPlayers";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter UserName = new SqlParameter("@UserName", SqlDbType.NVarChar);
            UserName.Value = userName;
            command.Parameters.Add(UserName);

            connection.Open();
            SqlDataReader dr;
            dr = command.ExecuteReader();

            var playerList = new List<Player>();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    var player = new Player();
                    player.Id = dr.GetInt32(0);
                    player.Name = dr.GetString(1);
                    if (dr.IsDBNull(2))
                    {
                        player.ImgSrc = "PlayerDefault_Image.png";
                    }
                    else
                    {
                        if (dr.GetString(2) == "")
                        {
                            player.ImgSrc = "PlayerDefault_Image.png";
                        }
                        else
                        {
                            player.ImgSrc = dr.GetString(2);
                        }
                    }

                    playerList.Add(player);
                }
            }
            connection.Close();
            dr.Close();
            return playerList;
        }

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
                if (dr.IsDBNull(2))
                {
                    player.ImgSrc = "PlayerDefault_Image.png";
                }
                else
                {
                    if (dr.GetString(2) == "")
                    {
                        player.ImgSrc = "PlayerDefault_Image.png";
                    }
                    else
                    {
                        player.ImgSrc = dr.GetString(2);
                    }
                }
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