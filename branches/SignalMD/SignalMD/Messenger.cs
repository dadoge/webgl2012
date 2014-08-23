using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SignalR.Hubs;

namespace SignalMD
{
    public class Messenger : Hub
    {
        public void SendChat(string message)
        {
            // Call the addMessage method on all clients
            Clients.updateChat(message);
          
        }
        public void SendDrawing(string drawing)
        {
            // Call the addMessage method on all clients
            Clients.updateDrawing(drawing);

        }
        public void RemoveDrawing()
        {
            // Call the addMessage method on all clients
            Clients.clearDrawing();

        }
    }
}