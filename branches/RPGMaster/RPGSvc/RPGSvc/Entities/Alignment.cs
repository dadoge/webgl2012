using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RPGSvc.Entities
{
    public class Alignment
    {
        public int Id;
        public string Name;
        public string Description;
        public string ImgSrc;

        public Alignment(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public Alignment() 
        {
        }
    }
}