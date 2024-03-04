using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neo4jClient;
using Neo4jClient.Cypher;
using System.Windows.Forms;

namespace Turizam
{
    public class DataLayer
    {
        private static GraphClient client;

        public static GraphClient getClient()
        {


            if (client == null)
            {
                client = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "andjelina");
                try
                {
                    client.ConnectAsync();
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
            return client;
       


        }
    }
}
