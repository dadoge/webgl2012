using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RPGSvc.Entities
{
    public class Spell
    {
        
        public int Id;
        public string Name;
        public string Description;
        public decimal Value;
        
        public Spell(int id,string name, string description, decimal value)
        {
            Id = id;
            Name = name;
            Description = description;
            Value = value;
        }

        public Spell() 
        {
        }
    }
}