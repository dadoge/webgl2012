using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using RPGSvc.Entities;
using System.Data.SqlClient;

namespace RPGSvc.Data
{
    public class StoredGender
    {
        public Gender GetPlayerGender(int id)
        {
            SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RPGMasterDb"].ConnectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "GetGenderByPlayerID";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter playerID = new SqlParameter("@PlayerID", SqlDbType.Int);

            playerID.Value = id;
            command.Parameters.Add(playerID);

            connection.Open();
            SqlDataReader dr;
            dr = command.ExecuteReader();

            var gender = new Gender();

            if (dr.HasRows)
            {
                dr.Read();
                gender.Name = dr.GetString(0);
                if (dr.IsDBNull(1)) {
                    gender.ImgSrc = "DefaultGender.png";
                }
                else {
                    gender.ImgSrc = dr.GetString(1);
                }
            }
            connection.Close();
            dr.Close();

            return gender;
        }

        public List<Gender> GetGenders()
        {

            SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RPGMasterDb"].ConnectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "GetGenders";
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            SqlDataReader dr;
            dr = command.ExecuteReader();

            var genderList = new List<Gender>();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    var gender = new Gender();
                    gender.Id = dr.GetInt32(0);
                    gender.Name = dr.GetString(1);

                    genderList.Add(gender);
                }
            }
            connection.Close();
            dr.Close();
            return genderList;
        }
    }
}