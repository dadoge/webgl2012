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
            SqlTransaction transaction;

            connection.Open();

            // Start a local transaction.
            transaction = connection.BeginTransaction("AddPlayerTransaction");

            // Must assign both transaction object and connection 
            // to Command object for a pending local transaction
            command.Transaction = transaction;
            command.Connection = connection;

            try
            {
                command.CommandText = "AddPlayer";
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter Name = new SqlParameter("@Name", SqlDbType.NVarChar);
                SqlParameter UserName = new SqlParameter("@UserName", SqlDbType.NVarChar);
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

                SqlParameter PlayerID = command.Parameters.Add("@PlayerID", SqlDbType.Int);
                PlayerID.Direction = ParameterDirection.ReturnValue;

                Name.Value = player.Name;
                UserName.Value = player.UserName;
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
                command.Parameters.Add(UserName);
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
                
                command.ExecuteNonQuery();

                int NewPlayerID = (int)command.Parameters["@PlayerID"].Value;

                // Prepare for Inserting Skills
                command.Parameters.Clear();

                SqlParameter SkillID = new SqlParameter("@SkillID", SqlDbType.Int);
                SqlParameter SkillValue = new SqlParameter("@SkillValue", SqlDbType.Int);
                PlayerID.Direction = ParameterDirection.Input;
                command.CommandText = "AddPlayerSkill";
                int i_count;

                for (i_count = 0; i_count < player.Skills.Count; i_count++)
                {
                    SkillID.Value = player.Skills[i_count].Id;
                    command.Parameters.Add(SkillID);
                    SkillValue.Value = player.Skills[i_count].Value;
                    command.Parameters.Add(SkillValue);
                    PlayerID.Value = NewPlayerID;
                    command.Parameters.Add(PlayerID);

                    command.ExecuteNonQuery();

                    command.Parameters.Clear();
                }
                // End Inserting Skills
                
                // Prepare for Inserting Stats

                SqlParameter StatID = new SqlParameter("@StatID", SqlDbType.Int);
                SqlParameter StatValue = new SqlParameter("@StatValue", SqlDbType.Int);
                command.CommandText = "AddPlayerStat";

                for (i_count = 0; i_count < player.Stats.Count; i_count++)
                {
                    StatID.Value = player.Stats[i_count].Id;
                    command.Parameters.Add(StatID);
                    StatValue.Value = player.Stats[i_count].Value;
                    command.Parameters.Add(StatValue);
                    PlayerID.Value = NewPlayerID;
                    command.Parameters.Add(PlayerID);

                    command.ExecuteNonQuery();

                    command.Parameters.Clear();
                }
                // End Inserting Stats


                // Prepare for Inserting Feats

                SqlParameter FeatID = new SqlParameter("@FeatID", SqlDbType.Int);
                command.CommandText = "AddPlayerFeat";

                for (i_count = 0; i_count < player.Feats.Count; i_count++)
                {
                    FeatID.Value = player.Feats[i_count].Id;
                    command.Parameters.Add(FeatID);
                    PlayerID.Value = NewPlayerID;
                    command.Parameters.Add(PlayerID);

                    command.ExecuteNonQuery();

                    command.Parameters.Clear();
                }
                // End Inserting Stats

                // Attempt to commit the transaction.
                transaction.Commit();
                Console.WriteLine("Transaction Successful.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                Console.WriteLine("  Message: {0}", ex.Message);

                // Attempt to roll back the transaction. 
                try
                {
                    transaction.Rollback();
                }
                catch (Exception ex2)
                {
                    // This catch block will handle any errors that may have occurred 
                    // on the server that would cause the rollback to fail, such as 
                    // a closed connection.
                    Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                    Console.WriteLine("  Message: {0}", ex2.Message);
                }
            }
            
            connection.Close();
        }
    }
}