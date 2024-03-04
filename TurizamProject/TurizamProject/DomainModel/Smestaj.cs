using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turizam;
using Neo4jClient;
using Neo4jClient.Cypher;

namespace TurizamProject.DomainModel
{
    public class Smestaj
    {
        public String naziv { get; set; }
        public String opis { get; set; }

        public Smestaj(String op, String n)
        {
            opis = op;
            naziv = n;
        }
        public Smestaj()
        {

        }
    }
}
