using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace RPGSvc.Entities
{
    // TODO: Edit the SampleItem class
    public class Player
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Skill> Skills;
        public List<Stat> Stats;

        public Player(string id)
        {
            Id = id;
            Name = "Iargalon";
        }
        public Player()
        {
            Id = "0";
            Name = "PlayerDefault";
        }
    }
}
