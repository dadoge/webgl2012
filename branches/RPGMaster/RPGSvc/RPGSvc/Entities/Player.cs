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
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Skill> Skills;
        public List<Stat> Stats;

        public Player(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public Player()
        {
        }
    }
}
