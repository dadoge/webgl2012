using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using RPGSvc.Entities;

namespace RPGSvc.Data
{
    public class StoredPlayer
    {
        //
        public Player GetPlayerByID(string id)
        {
            //search C# datareader
            //called storage procedure c#
            //values being passed into new Skill would be from database
            return new Player("1");
        }

    }
}