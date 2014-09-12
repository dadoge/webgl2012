using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace RPGMaster
{
    public class RpgHub : Hub
    {

        public void SendMessage(string name, string message)
        {
            var user = Clients.Caller.user;
            Clients.All.broadcastMessage(name, message);
        }
    }
}