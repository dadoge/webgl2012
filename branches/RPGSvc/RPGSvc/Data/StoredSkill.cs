using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using RPGSvc.Entities;
using System.Data.SqlClient;

namespace RPGSvc.Data
{
    public class StoredSkill
    {
        //
        public List<Skill> GetSkillsByPlayerID(string id)
        {

            SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RPGMasterDb"].ConnectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "GetSkillsByPlayerID";
            command.CommandType = CommandType.StoredProcedure;

 
            SqlParameter playerID = new SqlParameter("@PlayerID", SqlDbType.Int);

            //TODO:
            //I really shouldnt be parsing the int32 here, please pull this up into a higher layer, probably Service1.cs
            //Return json error if an integer is not passed in, handle elegantly clientside.
            playerID.Value = Int32.Parse(id);
            command.Parameters.Add(playerID);

            connection.Open();
            SqlDataReader dr;
            dr = command.ExecuteReader();

            var skillList = new List<Skill>();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    var skill = new Skill();
                    skill.Id = dr.GetInt32(0).ToString();
                    skill.Name = dr.GetString(1);
                    skill.Value = dr.GetDecimal(2);

                    skillList.Add(skill);
                }
            }
            connection.Close();
            return skillList;
        }

    }
}