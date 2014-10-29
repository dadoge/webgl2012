using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RPGSvc.Entities;
using RPGSvc.Data;

namespace RPGSvc.Repositories
{
    public class PlayerRepository
    {
        public PlayerRepository()
        {

            //initialize various data objects here
        }


        public void DeleteUserPlayer(int id)
        {
            new StoredPlayer().DeleteUserPlayer(id);
        }

        public List<Player> GetUserPlayers(string username)
        {
            var PlayerList = new StoredPlayer().GetUserPlayers(username);

            return PlayerList;
        }

        public Player GetPlayer(int id)
        {
            //Get player,player skills and player stats
            var player = new StoredPlayer().GetPlayerByPlayerID(id);
            player.Skills = new StoredSkill().GetSkillsByPlayerID(id);
            player.Feats = new StoredFeat().GetFeatsByPlayerID(id);
            player.Stats = new StoredStat().GetStatsByPlayerID(id);
            player.Race = new StoredRaces().GetPlayerRace(id);
            player.Class = new StoredClass().GetPlayerClass(id);
            player.Alignment = new StoredAlignment().GetPlayerAlignment(id);
            player.Gender = new StoredGender().GetPlayerGender(id);
            player.Inventory = new StoredInventory().GetPlayerInventory(id);

            return player;
        }

        public List<Player> GetAllPlayers()
        {
            throw new NotImplementedException();
        }

        /*Inventory and Item functions*/
        public List<Item> PlayerInventory(int id)
        {
            var item = new StoredInventory().GetPlayerInventory(id);

            return item;
        }
        public List<Item> AllItems()
        {
            var item = new StoredInventory().GetAllItems();

            return item;
        }
        public ItemType ItemTypes()
        {
            var itemType = new StoredInventory().GetItemTypes();

            return itemType;
        }
        public void AddToPlayerInventory(List<Inventory> inventory)
        {
            var si = new StoredInventory();
            si.AddToPlayerInventory(inventory);
        }
        public void UpdatePlayerInventory(List<Inventory> inventory)
        {
            var si = new StoredInventory();
            si.UpdatePlayerInventory(inventory);
        }
        public void AddItem(Item item)
        {
            var si = new StoredInventory();
            si.AddItem(item);
        }
    }
}