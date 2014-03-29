using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Script.Serialization;
using RPGSvc.Entities;
using RPGSvc.Repositories;

namespace RPGSvc
{
    // Start the service and browse to http://<machine_name>:<port>/Service1/help to view the service's generated help page
    // NOTE: By default, a new instance of the service is created for each call; change the InstanceContextMode to Single if you want
    // a single instance of the service to process all calls.	
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    // NOTE: If the service is renamed, remember to update the global.asax.cs file
    public class Service1
    {
        // TODO: Implement the collection resource that will contain the SampleItem instances

        [WebGet(UriTemplate = "GetPlayers")]
        public List<Player> GetCollection()
        {
            // TODO: Replace the current implementation to return a collection of SampleItem instances
            return new List<Player>() { new Player() { Id = 1, Name = "Hello" } };
        }

        [WebInvoke(UriTemplate = "", Method = "POST")]
        public Player Create(Player instance)
        {
            // TODO: Add the new instance of SampleItem to the collection
            throw new NotImplementedException();
        }

        [WebGet(UriTemplate = "GetPlayer/{id}", ResponseFormat = WebMessageFormat.Json)]
        public Player Get(string id)
        {
            int Id=-1;
            var pr = new PlayerRepository();
            var userplayer = new Player();
            //make sure id is an int not string
            // ToInt32 can throw FormatException or OverflowException. 
            try
            {
                Id = Convert.ToInt32(id);
            }
            catch (FormatException e)
            {
                Console.WriteLine("String ID is not a sequence of digits.");
            }
            catch (OverflowException e)
            {
                Console.WriteLine("The string ID number cannot fit in an Int32.");
            }
            finally
            {
                userplayer = pr.GetPlayer(Id);
            }

            return userplayer;
            //return new Player(id);
            //var p = new PlayerRepository();
            //return p.GetPlayer(id);
        }

        [WebInvoke(UriTemplate = "{id}", Method = "PUT")]
        public Player Update(string id, Player instance)
        {
            // TODO: Update the given instance of SampleItem in the collection
            throw new NotImplementedException();
        }

        [WebInvoke(UriTemplate = "{id}", Method = "DELETE")]
        public void Delete(string id)
        {
            // TODO: Remove the instance of SampleItem with the given id from the collection
            throw new NotImplementedException();
        }

    }
}
