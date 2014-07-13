using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using RPGSvc.Entities;
using System.Data.SqlClient;

namespace RPGSvc.Data
{
    public class StoredClass
    {
        public Class GetPlayerClass(int id)
        {
            SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RPGMasterDb"].ConnectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "GetClassByPlayerID";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter playerID = new SqlParameter("@PlayerID", SqlDbType.Int);

            playerID.Value = id;
            command.Parameters.Add(playerID);

            connection.Open();
            SqlDataReader dr;
            dr = command.ExecuteReader();

            var pclass = new Class();

            if (dr.HasRows)
            {
                dr.Read();
                pclass.Name = dr.GetString(0);
                if (dr.IsDBNull(1))   {
                    pclass.ImgSrc = "DefaultClass.png";
                }
                else  {
                    pclass.ImgSrc = dr.GetString(1);
                }
                pclass.Description = dr.GetString(2);
            }
            connection.Close();
            dr.Close();

            return pclass;
        }

        public List<Class> GetClasses()
        {

            SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RPGMasterDb"].ConnectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "GetClasses";
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            SqlDataReader dr;
            dr = command.ExecuteReader();

            var classList = new List<Class>();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    var class_ = new Class();
                    class_.Id = dr.GetInt32(0);
                    class_.Name = dr.GetString(1);
                    class_.Description = dr.GetString(2);
                    if (dr.IsDBNull(3))
                    {
                        class_.ImgSrc = "DefaultClass.png";
                    }
                    else
                    {
                        class_.ImgSrc = dr.GetString(3);
                    }

                    classList.Add(class_);
                }
            }
            connection.Close();
            dr.Close();
            return classList;
        }
    }
}