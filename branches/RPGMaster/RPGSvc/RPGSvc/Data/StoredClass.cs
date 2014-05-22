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

                    classList.Add(class_);
                }
            }
            connection.Close();
            return classList;
        }
    }
}