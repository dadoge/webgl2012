using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RPGSvc.Entities;
using RPGSvc.Data;

namespace RPGSvc.Repositories
{
    public class UserRepository
    {
        public UserRepository()
        {
            //initialize various data objects here
        }

        public User GetUser(string id)
        {
            //Get player,player skills and player stats
            var user = new StoredUser().GetUser(id);

            return user;
        }
        public void UpdateDefaultPlayer(User user)
        {
            new StoredUser().UpdateDefaultPlayer(user);
        }

    }
}