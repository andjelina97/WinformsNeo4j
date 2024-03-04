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
    public class Rezervisao
    {
        public int broj_putnika { get; set; }
        public Putovanje putovanje { get; set; }
        public Klijent klijent { get; set; }

        public Rezervisao()
        {
        }
    }
}
