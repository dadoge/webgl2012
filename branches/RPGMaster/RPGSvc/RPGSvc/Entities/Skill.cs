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
        public int SelectedSkill;
        public string ImgSrc;
        public int KeyStatID;
        public int Trained;
        public int ACPenalty;
        public int Retry;
        public int OpposingSkillID;
        public string Special;
        
        public Skill(int id,string name, string description, decimal value, int selectedSkill)
        {
            Id = id;
            Name = name;
            Description = description;
            Value = value;
            SelectedSkill = selectedSkill;
        }

        public Skill() 
        {
        }
    }
}