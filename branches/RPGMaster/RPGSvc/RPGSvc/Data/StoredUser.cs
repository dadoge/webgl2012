using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using RPGSvc.Entities;
using System.Data.SqlClient;

namespace RPGSvc.Data
{
    public class StoredUser
    {
        public User GetUser(string id)
        {
            SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RPGMasterDb"].ConnectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "GetUserInfo";
            command.CommandType = CommandType.StoredProcedure;


            SqlParameter userID = new SqlParameter("@UserName", SqlDbType.NVarChar);

            userID.Value = id;
            command.Parameters.Add(userID);

            connection.Open();
            SqlDataReader dr;
            dr = command.ExecuteReader();

            var user = new User();

            if (dr.HasRows)
            {
                dr.Read();
                user.Id = dr.GetString(0);
                if (dr.IsDBNull(1)==true)
                {
                    user.ChatName = "User";
                }
                else if (dr.GetString(1) == "")
                {
                    user.ChatName = "User";
                }
                else {
                    user.ChatName = dr.GetString(1);
                }
                if (dr.IsDBNull(2) == true)
                {
                    user.ActivePlayer = -1;
                }
                else
                {
                    user.ActivePlayer = dr.GetInt32(2);
                }
            }
            connection.Close();
            dr.Close();
            return user;
        }

        public void UpdateDefaultPlayer(User user)
        {
            SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RPGMasterDb"].ConnectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "UpdateDefaultPlayer";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter UserName = new SqlParameter("@UserName", SqlDbType.NVarChar);
            SqlParameter PlayerID = new SqlParameter("@PlayerID", SqlDbType.Int);
            UserName.Value = user.UserName;
            PlayerID.Value = user.ActivePlayer;
            command.Parameters.Add(UserName);
            command.Parameters.Add(PlayerID);

            connection.Open();
            command.ExecuteNonQuery();

            connection.Close();
        }
    }
}