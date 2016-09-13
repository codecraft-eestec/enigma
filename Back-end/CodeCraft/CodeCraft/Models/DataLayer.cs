using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Neo4jClient;

namespace CodeCraft.Models
{
    public class DataLayer
    {
        private static GraphClient client;

        public static GraphClient getClient()
        {
            if (client == null)
            {
                client = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "codecraft");
                try
                {
                    client.Connect();
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return client;
        }
    }
}