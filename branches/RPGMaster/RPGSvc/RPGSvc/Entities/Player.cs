using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace RPGSvc.Entities
{
    public class Player
    {
        public int Id;
        public string UserName;
        public string Name;
        public int PlayerTypeID;
        public List<Skill> Skills;
        public List<Stat> Stats;
        public Alignment Alignment;
        public List<Feat> Feats;
        public Race Race;
        public Class Class;
        public Gender Gender;
        public string ImgSrc;
        public int Age;
        public string Height;
        public string Weight;
        public int Level;
        public Int32 Experience;
        public Int64 Money;
        public string History;
        public int MaxHitPoints;
        public List<Item> Inventory;


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
