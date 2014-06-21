using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace RPGSvc.Entities
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PlayerTypeID;
        public List<Skill> Skills;
        public List<Stat> Stats;
        public Alignment Alignment;
        public Race Race;
        public Class Class;
        public Gender Gender;
        public string ImgSrc;
        public int Age;
        public string Height;
        public string Weight;
        public int Level;
        public Int32 Experience;
        public string Money;
        public string History;
        public int MaxHitPoints;


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
