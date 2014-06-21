using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using RPGSvc.Entities;
using System.Data.SqlClient;

namespace RPGSvc.Data
{
    public class StoredFeat
    {
        //
        public List<Feat> GetFeats()
        {
            SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RPGMasterDb"].ConnectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "GetFeats";
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            SqlDataReader dr;
            dr = command.ExecuteReader();

            var featList = new List<Feat>();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    var feat = new Feat();
                    feat.Id = dr.GetInt32(0);
                    feat.Name = dr.GetString(1);
                    feat.Description = dr.GetString(2);

                    featList.Add(feat);
                }
            }
            connection.Close();
            dr.Close();
            return featList;
        }

        public List<Feat> GetFeatsByPlayerID(int id)
        {

            SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RPGMasterDb"].ConnectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "GetFeatsByPlayerID";
            command.CommandType = CommandType.StoredProcedure;

 
            SqlParameter playerID = new SqlParameter("@PlayerID", SqlDbType.Int);

            playerID.Value = id;
            command.Parameters.Add(playerID);

            connection.Open();
            SqlDataReader dr;
            dr = command.ExecuteReader();

            var featList = new List<Feat>();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    var feat = new Feat();
                    feat.Id = dr.GetInt32(0);
                    feat.Name = dr.GetString(1);
                    feat.Description = dr.GetString(2);

                    featList.Add(feat);
                }
            }
            connection.Close();
            dr.Close();
            return featList;
        }

    }
}