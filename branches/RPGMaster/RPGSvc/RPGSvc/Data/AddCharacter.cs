using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using RPGSvc.Entities;
using System.Data.SqlClient;

namespace RPGSvc.Data
{
    public class AddCharacter
    {
        public void AddNewCharacter(Player player)
        {
            SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RPGMasterDb"].ConnectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "AddPlayer";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter Name = new SqlParameter("@Name", SqlDbType.NVarChar);
            SqlParameter ImgSrc = new SqlParameter("@ImgSrc", SqlDbType.NVarChar);
            SqlParameter PlayerTypeID = new SqlParameter("@PlayerTypeID", SqlDbType.Int);
            SqlParameter ClassID = new SqlParameter("@ClassID", SqlDbType.Int);
            SqlParameter RaceID = new SqlParameter("@RaceID", SqlDbType.Int);
            SqlParameter GenderID = new SqlParameter("@GenderID", SqlDbType.Int);
            SqlParameter AlignmentID = new SqlParameter("@AlignmentID", SqlDbType.Int);
            SqlParameter Level = new SqlParameter("@Level", SqlDbType.Int);
            SqlParameter Age = new SqlParameter("@Age", SqlDbType.Int);
            SqlParameter History = new SqlParameter("@History", SqlDbType.NVarChar);
            SqlParameter Height = new SqlParameter("@Height", SqlDbType.NVarChar);
            SqlParameter Weight = new SqlParameter("@Weight", SqlDbType.NVarChar);
            SqlParameter Experience = new SqlParameter("@Experience", SqlDbType.Int);
            SqlParameter Money = new SqlParameter("@Money", SqlDbType.NVarChar);
            SqlParameter MaxHitPoints = new SqlParameter("@MaxHitPoints", SqlDbType.Int);

            Name.Value = player.Name;
            ImgSrc.Value = player.ImgSrc;
            PlayerTypeID.Value = player.PlayerTypeID;
            ClassID.Value = player.Class.Id;
            RaceID.Value = player.Race.Id;
            GenderID.Value = player.Gender.Id;
            AlignmentID.Value = player.Alignment.Id;
            Level.Value = player.Level;
            Age.Value = player.Age;
            History.Value = player.History;
            Height.Value = player.Height;
            Weight.Value = player.Weight;
            Experience.Value = player.Experience;
            Money.Value = player.Money;
            MaxHitPoints.Value = player.MaxHitPoints;

            command.Parameters.Add(Name);
            command.Parameters.Add(ImgSrc);
            command.Parameters.Add(PlayerTypeID);
            command.Parameters.Add(ClassID);
            command.Parameters.Add(RaceID);
            command.Parameters.Add(GenderID);
            command.Parameters.Add(AlignmentID);
            command.Parameters.Add(Level);
            command.Parameters.Add(Age);
            command.Parameters.Add(History);
            command.Parameters.Add(Height);
            command.Parameters.Add(Weight);
            command.Parameters.Add(Experience);
            command.Parameters.Add(Money);
            command.Parameters.Add(MaxHitPoints);
            
            connection.Open();
            //SqlDataReader dr;
            //dr = command.ExecuteReader();

            command.ExecuteNonQuery();

            connection.Close();
        }
    }
}