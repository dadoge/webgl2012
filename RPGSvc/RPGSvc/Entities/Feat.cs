using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RPGSvc.Data;

namespace RPGSvc.Entities
{
    public class Feat
    {
        public int Id;
        public string Name;
        public string Description;
        public int Selected;
        public string PrereqMod;
        public string PrereqType;
        public string BenefitMod;
        public string BenefitType;
        
        public Feat(int id,string name, string description, int selected)
        {
            Id = id;
            Name = name;
            Description = description;
            Selected = selected;
        }

        public Feat() 
        {
        }
    }
}