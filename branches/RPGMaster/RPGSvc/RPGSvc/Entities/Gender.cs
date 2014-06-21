using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RPGSvc.Entities
{
    public class Gender
    {
        public int Id;
        public string Name;
        public string ImgSrc;
        
        public Gender(int id,string name)
        {
            Id = id;
            Name = name;
        }

        public Gender() 
        {
        }
    }
}