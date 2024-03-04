using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neo4jClient;
using Neo4jClient.Cypher;
using Turizam;

namespace TurizamProject.DomainModel
{
    public class Destinacija
    {
        public String drzava { get; set; }
        public String mesto { get; set; }

        public Destinacija()
        {

        }
        public Destinacija(String d, String m)
        {
            drzava = d;
            mesto = m;

        }

        public static List<String> pribaviSveDrzave()
        {
            List<String> sveDrzave = new List<String>();
            GraphClient graphClient = DataLayer.getClient();

            var query = graphClient.Cypher
            .Match("(d:Destinacija)")
            .Return((d) =>
                new
                {
                    dest = d.As<Destinacija>()
                });

            var res = query.ResultsAsync.Result.ToList();

            foreach (var result in res)
                sveDrzave.Add(result.dest.drzava);

            return sveDrzave;
        }

        public static List<Destinacija> pribaviSvaMesta(String drzava)
        {
            List<Destinacija> svaMesta = new List<Destinacija>();
            GraphClient graphClient = DataLayer.getClient();

            var query = graphClient.Cypher
            .Match("(d:Destinacija)")
            .Where((Destinacija d) => d.drzava == drzava)
            .Return((d) =>
                new
                {
                    dest = d.As<Destinacija>()
                });

            var res = query.ResultsAsync.Result.ToList();

            foreach (var result in res)
                svaMesta.Add(result.dest);

            return svaMesta;
        }

        public List<Putovanje> pribaviSvaPutovanja()
        {
            GraphClient graphClient = DataLayer.getClient();

            var query = graphClient.Cypher
            .Match("(p:Putovanje)-[:DO_MESTA]->(d:Destinacija)")
            .Where((Destinacija d) => d.mesto == this.mesto)
            .Return((p) =>
                new
                {
                    putovanje = p.As<Putovanje>()
                });

            var res = query.ResultsAsync.Result.ToList();
            List<Putovanje> svaPutovanja = new List<Putovanje>();
            foreach (var result in res)
                svaPutovanja.Add(result.putovanje);
            return svaPutovanja;
        }



    }
}
