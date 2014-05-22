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

        public Player GetPlayer(int id)
        {
            //Get player,player skills and player stats
            var player = new StoredPlayer().GetPlayerByPlayerID(id);
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