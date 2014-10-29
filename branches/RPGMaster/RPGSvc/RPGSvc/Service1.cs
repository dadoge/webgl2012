using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Script.Serialization;
using RPGSvc.Entities;
using RPGSvc.Repositories;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace RPGSvc
{
    // Start the service and browse to http://<machine_name>:<port>/Service1/help to view the service's generated help page
    // NOTE: By default, a new instance of the service is created for each call; change the InstanceContextMode to Single if you want
    // a single instance of the service to process all calls.	
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    // NOTE: If the service is renamed, remember to update the global.asax.cs file
    public class Service1
    {
        // TODO: Implement the collection resource that will contain the SampleItem instances

        [WebGet(UriTemplate = "GetPlayers")]
        public List<Player> GetCollection()
        {
            // TODO: Replace the current implementation to return a collection of SampleItem instances
            return new List<Player>() { new Player() { Id = 1, Name = "Hello" } };
        }

        [WebInvoke(UriTemplate = "AddPlayer", Method = "POST")]
        public string AddPlayer(Player newPlayer)
        {
            var newchar = new NewCharacterRepository();
            newchar.AddNewCharacter(newPlayer);
            var newCharacterID = 1;
            return "{'NewCharacterID': " + newCharacterID + "}";
        }

        [WebInvoke(UriTemplate = "DeleteUserPlayer", Method = "POST")]
        public string DeleteUserPlayer(string id)
        {
            int Id = -1;
            //make sure id is an int not string
            // ToInt32 can throw FormatException or OverflowException. 
            try
            {
                Id = Convert.ToInt32(id);
            }
            catch (FormatException e)
            {
                Console.WriteLine("String ID is not a sequence of digits.");
            }
            catch (OverflowException e)
            {
                Console.WriteLine("The string ID number cannot fit in an Int32.");
            }
            var newchar = new PlayerRepository();
            newchar.DeleteUserPlayer(Id);
            var Success = 1;
            return "{'PlayerDeleted': " + Success + "}";
        }

        [WebInvoke(UriTemplate = "UpdateDefaultPlayer", Method = "POST")]
        public string UpdateDefaultPlayer(User user)
        {
            var updateUser = new UserRepository();
            updateUser.UpdateDefaultPlayer(user);
            var Success = user.ActivePlayer;
            return "{'UpdatedPlayerID': " + Success + "}";
        }
        
        [WebGet(UriTemplate = "GetUserPlayers/{username}", ResponseFormat = WebMessageFormat.Json)]
        public List<Player> getUserPlayers(string username)
        {
            return new PlayerRepository().GetUserPlayers(username);
        }

        [WebGet(UriTemplate = "GetPlayer/{id}", ResponseFormat = WebMessageFormat.Json)]
        public Player Get(string id)
        {
            int Id=-1;
            var pr = new PlayerRepository();
            //make sure id is an int not string
            // ToInt32 can throw FormatException or OverflowException. 
            try
            {
                Id = Convert.ToInt32(id);
            }
            catch (FormatException e)
            {
                Console.WriteLine("String ID is not a sequence of digits.");
            }
            catch (OverflowException e)
            {
                Console.WriteLine("The string ID number cannot fit in an Int32.");
            }
            return pr.GetPlayer(Id);
            //return new Player(id);
            //var p = new PlayerRepository();
            //return p.GetPlayer(id);
        }

        [WebGet(UriTemplate = "GetUserInfo/{id}", ResponseFormat = WebMessageFormat.Json)]
        public User GetUserInfo(string id)
        {
            var ncr = new UserRepository();
            return ncr.GetUser(id);
        }

        [WebGet(UriTemplate = "CreateNewCharacter/", ResponseFormat = WebMessageFormat.Json)]
        public NewCharacter Gets()
        {
            var ncr = new NewCharacterRepository();
            return ncr.GetNewCharacter();
        }

        [WebInvoke(UriTemplate = "SaveMap", Method = "POST")]
        public string SaveMap(GameMap gameMap)
        {
            var MapRepo = new MapRepository();
            MapRepo.SaveStoredGameMap(gameMap);
            return "{'Result': '" + gameMap.Name + " Map Saved.'}";
        }

        [WebGet(UriTemplate = "LoadMap/{mapID}", ResponseFormat = WebMessageFormat.Json)]
        public GameMap LoadMap(string mapID)
        {
            int Id = -1;
            try
            {
                Id = Convert.ToInt32(mapID);
            }
            catch (FormatException e)
            {
                Console.WriteLine("String ID is not a sequence of digits.");
            }
            catch (OverflowException e)
            {
                Console.WriteLine("The string ID number cannot fit in an Int32.");
            }

            var MapRepo = new MapRepository();
            return MapRepo.LoadStoredGameMap(Id);
        }

        [WebGet(UriTemplate = "GetUserMaps/{username}", ResponseFormat = WebMessageFormat.Json)]
        public UserGameMap UserMaps(string username)
        {
            var MapRepo = new MapRepository();
            return MapRepo.GetUserStoredGameMap(username);
        }

        [WebGet(UriTemplate = "GetPlayerInventory/{playerid}", ResponseFormat = WebMessageFormat.Json)]
        public List<Item> GetPlayerInventory(string playerid)
        {
            int Id = -1;
            try
            {
                Id = Convert.ToInt32(playerid);
            }
            catch (FormatException e)
            {
                Console.WriteLine("String ID is not a sequence of digits.");
            }
            catch (OverflowException e)
            {
                Console.WriteLine("The string ID number cannot fit in an Int32.");
            }
            var PlayerRepo = new PlayerRepository();
            return PlayerRepo.PlayerInventory(Id);
        }

        [WebGet(UriTemplate = "GetAllItems", ResponseFormat = WebMessageFormat.Json)]
        public List<Item> GetAllItems()
        {
            var PlayerRepo = new PlayerRepository();
            return PlayerRepo.AllItems();
        }

        [WebGet(UriTemplate = "GetItemTypes", ResponseFormat = WebMessageFormat.Json)]
        public ItemType GetItemTypes()
        {
            var PlayerRepo = new PlayerRepository();
            return PlayerRepo.ItemTypes();
        }

        [WebInvoke(UriTemplate = "AddToPlayerInventory", Method = "POST")]
        public string AddToPlayerInventory(List<Inventory> inventory)
        {
            var PlayerRepo = new PlayerRepository();
            PlayerRepo.AddToPlayerInventory(inventory);
            return "{'ReturnString':'Added to Player Inventory'}";
        }

        [WebInvoke(UriTemplate = "UpdatePlayerInventory", Method = "POST")]
        public string UpdatePlayerInventory(List<Inventory> inventory)
        {
            var PlayerRepo = new PlayerRepository();
            PlayerRepo.UpdatePlayerInventory(inventory);
            return "{'ReturnString':'Updated Player Inventory'}";
        }

        [WebInvoke(UriTemplate = "AddItem", Method = "POST")]
        public string AddItem(Item item)
        {
            var PlayerRepo = new PlayerRepository();
            PlayerRepo.AddItem(item);
            return "{'ReturnString':'Item has been created'}";
        }

        [WebInvoke(UriTemplate = "{id}", Method = "PUT")]
        public Player Update(string id, Player instance)
        {
            // TODO: Update the given instance of SampleItem in the collection
            throw new NotImplementedException();
        }

        [WebInvoke(UriTemplate = "{id}", Method = "DELETE")]
        public void Delete(string id)
        {
            // TODO: Remove the instance of SampleItem with the given id from the collection
            throw new NotImplementedException();
        }

    }
}
