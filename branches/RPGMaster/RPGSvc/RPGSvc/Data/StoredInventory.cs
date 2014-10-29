using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using RPGSvc.Entities;
using System.Data.SqlClient;

namespace RPGSvc.Data
{
    public class StoredInventory
    {
        public List<Item> GetPlayerInventory(int playerID)
        {
            SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RPGMasterDb"].ConnectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "GetPlayerInventory";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter PlayerID = new SqlParameter("@PlayerID", SqlDbType.Int);

            PlayerID.Value = playerID;
            command.Parameters.Add(PlayerID);

            connection.Open();
            SqlDataReader dr;
            dr = command.ExecuteReader();

            var itemList = new List<Item>();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    var item = new Item();
                    item.Name = dr.GetString(0);
                    item.Description = dr.GetString(1);
                    item.Type = dr.GetInt32(2);
                    item.TypeName = dr.GetString(3);
                    item.Cost = dr.GetInt32(4);
                    item.MaxEffect = dr.GetString(5);
                    item.MinEffect = dr.GetString(6);
                    item.CriticalEffect = dr.GetString(7);
                    item.OtherEffect = dr.GetString(8);
                    item.Range = dr.GetString(9);
                    item.Weight = dr.GetDecimal(10);
                    item.OtherType = dr.GetInt32(11);
                    if (dr.IsDBNull(12))
                    {
                        item.Path = "";
                    }
                    else
                    {
                        item.Path = dr.GetString(12);
                    }
                    item.ItemQuantity = dr.GetInt32(13);

                    itemList.Add(item);
                }

            }
            connection.Close();
            dr.Close();

            return itemList;
        }

        public List<Item> GetAllItems()
        {
            SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RPGMasterDb"].ConnectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "GetAllItems";
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            SqlDataReader dr;
            dr = command.ExecuteReader();

            var itemList = new List<Item>();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    var item = new Item();
                    item.Id = dr.GetInt32(0);
                    item.Name = dr.GetString(1);
                    item.Description = dr.GetString(2);
                    item.Type = dr.GetInt32(3);
                    item.Cost = dr.GetInt32(4);
                    item.MaxEffect = dr.GetString(5);
                    item.MinEffect = dr.GetString(6);
                    item.CriticalEffect = dr.GetString(7);
                    item.OtherEffect = dr.GetString(8);
                    item.Range = dr.GetString(9);
                    item.Weight = dr.GetDecimal(10);
                    item.OtherType = dr.GetInt32(11);

                    itemList.Add(item);
                }

            }
            connection.Close();
            dr.Close();

            return itemList;
        }

        public ItemType GetItemTypes()
        {
            SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RPGMasterDb"].ConnectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "GetItemTypes";
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            SqlDataReader dr;
            dr = command.ExecuteReader();

            var itemTypeList = new ItemType();
            itemTypeList.TypeID = new List<int>();
            itemTypeList.Name = new List<string>();            

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    itemTypeList.TypeID.Add(dr.GetInt32(0));
                    itemTypeList.Name.Add(dr.GetString(1));
                }
            }
            connection.Close();
            dr.Close();

            return itemTypeList;
        }

        public void AddToPlayerInventory(List<Inventory> inventory)
        {
            SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RPGMasterDb"].ConnectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "AddToPlayerInventory";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter PlayerID = new SqlParameter("@PlayerID", SqlDbType.Int);
            SqlParameter ItemID = new SqlParameter("@ItemID", SqlDbType.Int);
            SqlParameter ItemQuantity = new SqlParameter("@ItemQuantity", SqlDbType.Int);

            connection.Open();
            int i_count;

            for (i_count = 0; i_count < inventory.Count; i_count++)
            {
                PlayerID.Value = inventory[i_count].PlayerID;
                command.Parameters.Add(PlayerID);
                ItemID.Value = inventory[i_count].ItemID;
                command.Parameters.Add(ItemID);
                ItemQuantity.Value = inventory[i_count].ItemQuantity;
                command.Parameters.Add(ItemQuantity);

                command.ExecuteNonQuery();

                command.Parameters.Clear();
            }

            connection.Close();
        }

        public void UpdatePlayerInventory(List<Inventory> inventory)
        {
            SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RPGMasterDb"].ConnectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "UpdatePlayerInventory";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter PlayerID = new SqlParameter("@PlayerID", SqlDbType.Int);
            SqlParameter ItemID = new SqlParameter("@ItemID", SqlDbType.Int);
            SqlParameter ItemQuantity = new SqlParameter("@ItemQuantity", SqlDbType.Int);

            connection.Open();
            int i_count;

            for (i_count = 0; i_count < inventory.Count; i_count++)
            {
                PlayerID.Value = inventory[i_count].PlayerID;
                command.Parameters.Add(PlayerID);
                ItemID.Value = inventory[i_count].ItemID;
                command.Parameters.Add(ItemID);
                ItemQuantity.Value = inventory[i_count].ItemQuantity;
                command.Parameters.Add(ItemQuantity);

                command.ExecuteNonQuery();

                command.Parameters.Clear();
            }

            connection.Close();
        }

        public void AddItem(Item item)
        {
            SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RPGMasterDb"].ConnectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "AddItem";
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();

            SqlParameter Name = new SqlParameter("@Name", SqlDbType.NVarChar);
            SqlParameter Description = new SqlParameter("@Description", SqlDbType.NVarChar);
            SqlParameter Type = new SqlParameter("@Type", SqlDbType.Int);
            SqlParameter Cost = new SqlParameter("@Cost", SqlDbType.Int);
            SqlParameter MaxEffect = new SqlParameter("@MaxEffect", SqlDbType.NVarChar);
            SqlParameter MinEffect = new SqlParameter("@MinEffect", SqlDbType.NVarChar);
            SqlParameter CriticalEffect = new SqlParameter("@CriticalEffect", SqlDbType.NVarChar);
            SqlParameter OtherEffect = new SqlParameter("@OtherEffect", SqlDbType.NVarChar);
            SqlParameter Range = new SqlParameter("@Range", SqlDbType.NVarChar);
            SqlParameter Weight = new SqlParameter("@Weight", SqlDbType.Decimal);
            SqlParameter OtherType = new SqlParameter("@OtherType", SqlDbType.Int);
            SqlParameter Path = new SqlParameter("@Path", SqlDbType.NVarChar);


            Name.Value = item.Name;
            command.Parameters.Add(Name);
            Description.Value = item.Description;
            command.Parameters.Add(Description);
            Type.Value = item.Type;
            command.Parameters.Add(Type);
            Cost.Value = item.Cost;
            command.Parameters.Add(Cost);
            MaxEffect.Value = item.MaxEffect;
            command.Parameters.Add(MaxEffect);
            MinEffect.Value = item.MinEffect;
            command.Parameters.Add(MinEffect);
            CriticalEffect.Value = item.CriticalEffect;
            command.Parameters.Add(CriticalEffect);
            OtherEffect.Value = item.OtherEffect;
            command.Parameters.Add(OtherEffect);
            Range.Value = item.Range;
            command.Parameters.Add(Range);
            Weight.Value = item.Weight;
            command.Parameters.Add(Weight);
            OtherType.Value = item.OtherType;
            command.Parameters.Add(OtherType);
            Path.Value = item.Path;
            command.Parameters.Add(Path);

            command.ExecuteNonQuery();

            connection.Close();
        }
    }
}