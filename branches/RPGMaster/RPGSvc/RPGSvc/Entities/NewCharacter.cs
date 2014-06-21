using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RPGSvc.Entities
{
    public class NewCharacter
    {
        public List<Race> Races;
        public List<Alignment> Alignments;
        public List<Gender> Genders;
        public List<Stat> Stats;
        public List<Class> Classes;
        public List<Skill> Skills;
        public List<Feat> Feats;

        public NewCharacter()
        {
        }
    }
}