using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using RPGSvc.Entities;

namespace RPGSvc.Data
{
    public class StoredSkill
    {
        //
        public List<Skill> GetSkillsByPlayerID(string id)
        {
            //search C# datareader
            //called storage procedure c#
            //values being passed into new Skill would be from database
            var skill1 = new Skill("1", "Stealth", "Ability to sneak around", 18);
            var skill2 = new Skill("2", "Swim", "How well you can swim", 8);
            var skill3 = new Skill("3", "Run", "How well you can run", 12);

            var skillList = new List<Skill>();
            skillList.Add(skill1);
            skillList.Add(skill2);
            skillList.Add(skill3);

            return skillList;
        }

    }
}