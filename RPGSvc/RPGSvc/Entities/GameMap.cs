using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RPGSvc.Entities
{
    public class GameMap
    {
        public string Name;
        public string UserName;
        public int CreatedByPlayerID;
        public string TilesData;
        public List<Tile> Tiles;
        public string DateCreated;
        public int isActive;
    }
}