using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RPGSvc.Data;

namespace RPGSvc.Entities
{
    public class Skill
    {
        public string Id;
        public string Name;
        public string Description;
        public double Value;
        
        public Skill(string id,string name, string description, double value)
        {
            Id = id;
            Name = name;
            Description = description;
            Value = value;
        }

        public Skill() 
        {
            Id="1";
            Name="Sucking";
            Description="The ability to suck";
            Value=9001;
        }
    }
}