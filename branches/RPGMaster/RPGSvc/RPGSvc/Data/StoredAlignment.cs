using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using RPGSvc.Entities;
using System.Data.SqlClient;

namespace RPGSvc.Data
{
    public class StoredAlignment
    {
        public Alignment GetPlayerAlignment(int id)
        {
            SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RPGMasterDb"].ConnectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "GetAlignmentByPlayerID";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter playerID = new SqlParameter("@PlayerID", SqlDbType.Int);

            playerID.Value = id;
            command.Parameters.Add(playerID);

            connection.Open();
            SqlDataReader dr;
            dr = command.ExecuteReader();

            var alignment = new Alignment();

            if (dr.HasRows)
            {
                dr.Read();
                alignment.Name = dr.GetString(0);
                if (dr.IsDBNull(1)) {
                    alignment.ImgSrc = "DefaultAlignment.png";
                }
                else {
                    alignment.ImgSrc = dr.GetString(1);
                }
                alignment.Description = dr.GetString(2);
            }
            connection.Close();
            dr.Close();

            return alignment;
        }

        public List<Alignment> GetAlignments()
        {

            SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RPGMasterDb"].ConnectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "GetAlignments";
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            SqlDataReader dr;
            dr = command.ExecuteReader();

            var alignmentList = new List<Alignment>();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    var alignment = new Alignment();
                    alignment.Id = dr.GetInt32(0);
                    alignment.Name = dr.GetString(1);
                    alignment.Description = dr.GetString(2);

                    alignmentList.Add(alignment);
                }
            }
            connection.Close();
            dr.Close();
            return alignmentList;
        }
    }
}