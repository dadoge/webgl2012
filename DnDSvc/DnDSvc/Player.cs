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
        public int Id { get; set; }
        public string StringValue { get; set; }

        public Player(int id)
        {
            Id = id;
            StringValue = "Player " + Id + "is Gay";
        }
        public Player()
        {
            Id = 0;
            StringValue = "Player " + Id + "is Gay";
        }
    }
}
