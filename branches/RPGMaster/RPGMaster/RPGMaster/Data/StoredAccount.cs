using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace RPGMaster.Data
{
    public class StoredAccount
    {
        public void CreateNewAccount(string userID, string email)
        {

            SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RPGMasterDb"].ConnectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "CreateNewAccount";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter UserID = new SqlParameter("@UserID", SqlDbType.NVarChar);
            SqlParameter Email = new SqlParameter("@Email", SqlDbType.NVarChar);

            UserID.Value = userID;
            Email.Value = email;

            command.Parameters.Add(UserID);
            command.Parameters.Add(Email);

            connection.Open();
            //SqlDataReader dr;
            //dr = command.ExecuteReader();

            command.ExecuteNonQuery();

            connection.Close();
        }
    }
}