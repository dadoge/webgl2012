using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RPGSvc.Data;

namespace RPGSvc.Entities
{
    public class Stat
    {
        public string Id;
        public string Name;
        public string Description;
        public double Value;
        
        public Stat(string id,string name, string description, double value)
        {
            Id = id;
            Name = name;
            Description = description;
            Value = value;
        }

        public Stat() 
        {
            Id="0";
            Name="Strength";
            Description="How hard your muscles are";
            Value=9001;
        }
    }
}