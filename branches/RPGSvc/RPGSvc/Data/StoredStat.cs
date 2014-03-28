using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using RPGSvc.Entities;

namespace RPGSvc.Data
{
    public class StoredStat
    {
        //
        public List<Stat> GetStatsByPlayerID(string id)
        {
            //search C# datareader
            //called storage procedure c#
            //values being passed into new stat would be from database
            var stat1 = new Stat("1", "Strength", "How hard your muscles are", 18.76);
            var stat2 = new Stat("2", "Wisdom", "An old man once told me...", 18);
            var stat3 = new Stat("3", "Dexterity", "Not what Ghost does", 14);

            var statList = new List<Stat>();
            statList.Add(stat1);
            statList.Add(stat2);
            statList.Add(stat3);

            return statList;
        }

    }
}