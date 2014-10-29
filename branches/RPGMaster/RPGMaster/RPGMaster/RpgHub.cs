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
        public void FirstLogon(string name)
        {
            Clients.All.broadcastFirstLogon(name);
        }

        public void SendMessage(string name, string message)
        {
            //var user = Clients.Caller.user;
            Clients.All.broadcastMessage(name, message);
        }
        public void UpdateTile(int tileId)
        {
            Clients.All.broadcastUpdateTile(tileId);
        }
        public void LoadMap(int mapID)
        {
            Clients.All.broadcastLoadMap(mapID);
        }
    }
}