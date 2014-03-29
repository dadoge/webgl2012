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
            //search C# datareader
            //called storage procedure c#
            //values being passed into new Skill would be from database
            //var skill1 = new Skill("1", "Stealth", "Ability to sneak around", 18);
            //var skill2 = new Skill("2", "Swim", "How well you can swim", 8);
            //var skill3 = new Skill("3", "Run", "How well you can run", 12);

            //var skillList = new List<Skill>();
            //skillList.Add(skill1);
            //skillList.Add(skill2);
            //skillList.Add(skill3);

            //return skillList;
            SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RPGMasterDb"].ConnectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "GetSkillsByPlayerID";
            command.CommandType = CommandType.StoredProcedure;

 
            SqlParameter playerID = new SqlParameter("@PlayerID", SqlDbType.Int);
            playerID.Value = 1;
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
                    skill.Value = dr.GetDouble(2);
                }
            }
            connection.Close();
            return skillList;
        }

    }
}