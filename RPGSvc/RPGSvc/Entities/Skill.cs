using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RPGSvc.Data;

namespace RPGSvc.Entities
{
    public class Skill
    {
        public int Id;
        public string Name;
        public string Description;
        public decimal Value;
        
        public Skill(int id,string name, string description, decimal value)
        {
            Id = id;
            Name = name;
            Description = description;
            Value = value;
        }

        public Skill() 
        {
        }
    }
}