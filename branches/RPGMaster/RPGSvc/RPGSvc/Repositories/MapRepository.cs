using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RPGSvc.Entities;
using RPGSvc.Data;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;

namespace RPGSvc.Repositories
{
    public class MapRepository
    {
        public void SaveStoredGameMap(GameMap gameMap)
        {
            var sm = new StoredGameMap();
            sm.SaveGameMap(gameMap);
        }

        public GameMap LoadStoredGameMap(int mapID)
        {
            var sgm = new StoredGameMap();
            var gameMap = sgm.LoadGameMap(mapID);
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(List<Tile>));
            MemoryStream ms = new MemoryStream(System.Text.ASCIIEncoding.ASCII.GetBytes(gameMap.TilesData));

            gameMap.Tiles = (List<Tile>)js.ReadObject(ms);

            return gameMap;
        }

        public UserGameMap GetUserStoredGameMap(string username)
        {
            var sm = new StoredGameMap();
            return sm.GetUserGameMaps(username);
        }
    }
}