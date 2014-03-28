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

        public Player GetPlayer(string id)
        {
            //throw new NotImplementedException();
            //Call Data
            var player = new StoredPlayer().GetPlayerByID(id);
            player.Skills = new StoredSkill().GetSkillsByPlayerID(id);
            player.Stats = new StoredStat().GetStatsByPlayerID(id);

            return player;
        }

        public List<Player> GetAllPlayers()
        {
            throw new NotImplementedException();

        }
    }
}