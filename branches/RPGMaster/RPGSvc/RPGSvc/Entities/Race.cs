using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RPGSvc.Entities
{
    public class Race
    {
        public int Id;
        public string Name;
        public string Description;
        public string ImgSrc;
        
        public Race(int id,string name, string description, string imgSrc)
        {
            Id = id;
            Name = name;
            Description = description;
            ImgSrc = imgSrc;
        }

        public Race() 
        {
        }
    }
}