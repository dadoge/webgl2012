using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RPGSvc.Entities
{
    public class Item
    {
        public int Id;
        public string Name;
        public string Description;
        public int Type;
        public string TypeName;
        public int Cost;
        public string MaxEffect;
        public string MinEffect;
        public string CriticalEffect;
        public string OtherEffect;
        public string Range;
        public decimal Weight;
        public int OtherType;
        public string Path;
        public int ItemQuantity;
    }
}