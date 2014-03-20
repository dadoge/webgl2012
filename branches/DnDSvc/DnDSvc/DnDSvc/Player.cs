using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DnDSvc
{
    // TODO: Edit the SampleItem class
    public class Player
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Health { get; set; }
        public int Intelligence { get; set;}

        public Player(string id)
        {
            Id = id;
            Name = "Iargalon";
            Health = 28;
            Intelligence = 18;
        }
        public Player()
        {
            Id = "0";
            Name = "PlayerDefault";
            Health = 0;
            Intelligence = 0;
        }
    }
}
