using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using RPGSvc.Entities;
using System.Data.SqlClient;

namespace RPGSvc.Data
{
    public class StoredGameMap
    {
        public void SaveGameMap(GameMap gameMap)
        {
            SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RPGMasterDb"].ConnectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SaveGameMap";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter Name = new SqlParameter("@Name", SqlDbType.NVarChar);
            SqlParameter UserName = new SqlParameter("@UserName", SqlDbType.NVarChar);
            SqlParameter TilesData = new SqlParameter("@TilesData", SqlDbType.NVarChar);
            SqlParameter isActive = new SqlParameter("@isActive", SqlDbType.NVarChar);

            Name.Value = gameMap.Name;
            UserName.Value = gameMap.UserName;
            TilesData.Value = gameMap.TilesData;
            isActive.Value = 1;

            command.Parameters.Add(Name);
            command.Parameters.Add(UserName);
            command.Parameters.Add(TilesData);
            command.Parameters.Add(isActive);

            connection.Open();
            command.ExecuteNonQuery();

            connection.Close();
        }
        public GameMap LoadGameMap(int mapID)
        {
            SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RPGMasterDb"].ConnectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "LoadGameMap";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter MapID = new SqlParameter("@MapID", SqlDbType.Int);

            MapID.Value = mapID;
            command.Parameters.Add(MapID);

            connection.Open();
            SqlDataReader dr;
            dr = command.ExecuteReader();

            var gameMap = new GameMap();

            if (dr.HasRows)
            {
                dr.Read();
                gameMap.Name = dr.GetString(0);
                gameMap.TilesData = dr.GetString(1);
            }
            connection.Close();
            dr.Close();
            
            return gameMap;
        }
        public UserGameMap GetUserGameMaps(string username)
        {
            SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RPGMasterDb"].ConnectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "GetPlayerGameMaps";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter UserName = new SqlParameter("@Username", SqlDbType.NVarChar);

            UserName.Value = username;
            command.Parameters.Add(UserName);

            connection.Open();
            SqlDataReader dr;
            dr = command.ExecuteReader();

            var userGameMaps = new UserGameMap();
            userGameMaps.MapID = new List<int>();
            userGameMaps.Name = new List<string>();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    userGameMaps.MapID.Add(dr.GetInt32(0));
                    userGameMaps.Name.Add(dr.GetString(1));
                }
            }
            connection.Close();
            dr.Close();

            return userGameMaps;
        }
    }
}