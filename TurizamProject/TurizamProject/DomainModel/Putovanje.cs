using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neo4jClient.Cypher;
using Turizam;

namespace TurizamProject.DomainModel
{
    public class Putovanje
    {
        public String vremePolaska { get; set; }      //samo datum
        public String vremeDolaska { get; set; }      //samo datum
        public String mestoIVremePolaska { get; set; }    //mesto i vreme
        public String program { get; set; }
        public int cena { get; set; }
        public String kratakOpis { get; set; }

        public Putovanje(String vremeP, String vremeD, String mestoVremeP, String prog, int cn, String opis)
        {
            vremePolaska = vremeP;
            vremeDolaska = vremeD;
            mestoIVremePolaska = mestoVremeP;
            program = prog;
            cena = cn;
            kratakOpis = opis;
        }

        public Putovanje()
        {

        }

        public Smestaj pribaviSmestaj()
        {
            GraphClient graphClient = DataLayer.getClient();

            var query = graphClient.Cypher
            .Match("(p:Putovanje)-[:UKLJUCUJE]->(s:Smestaj)")
            .Where((Putovanje p) => p.vremePolaska == this.vremePolaska)
            .AndWhere((Putovanje p) => p.vremeDolaska == this.vremeDolaska)
            .AndWhere((Putovanje p) => p.mestoIVremePolaska == this.mestoIVremePolaska)
            .AndWhere((Putovanje p) => p.cena == this.cena)
            .Return((s) =>
                new
                {
                    smestaj = s.As<Smestaj>(),
                });

            var res = query.ResultsAsync.Result.ToList();
            if (res.Count != 0)
                return res[0].smestaj;
            else return null;
        }

        public Destinacija pribaviDestinaciju()
        {
            GraphClient graphClient = DataLayer.getClient();

            var query = graphClient.Cypher
            .Match("(p:Putovanje)-[:DO_MESTA]->(d:Destinacija)")
            .Where((Putovanje p) => p.vremePolaska == this.vremePolaska)
            .AndWhere((Putovanje p) => p.vremeDolaska == this.vremeDolaska)
            .AndWhere((Putovanje p) => p.mestoIVremePolaska == this.mestoIVremePolaska)
            .AndWhere((Putovanje p) => p.cena == this.cena)
            .Return((d) =>
                new
                {
                    dest = d.As<Destinacija>()
                });

            var res = query.ResultsAsync.Result.ToList();
            if (res.Count != 0)
                return res[0].dest;
            else return null;
        }

        public List<Klijent> pribaviZainteresovane()
        {
            GraphClient graphClient = DataLayer.getClient();

            var query = graphClient.Cypher
            .Match("(p:Putovanje)<-[:ZAINTERESOVAN]-(k:Klijent)")
            .Where((Putovanje p) => p.vremePolaska == this.vremePolaska)
            .AndWhere((Putovanje p) => p.vremeDolaska == this.vremeDolaska)
            .AndWhere((Putovanje p) => p.mestoIVremePolaska == this.mestoIVremePolaska)
            .AndWhere((Putovanje p) => p.cena == this.cena)
            .Return((k) =>
                new
                {
                    zainteresovan = k.As<Klijent>()
                });

            var res = query.ResultsAsync.Result.ToList();
            List<Klijent> sviZainteresovani = new List<Klijent>();
            foreach (var result in res)
                sviZainteresovani.Add(result.zainteresovan);
            return sviZainteresovani;

        }

        public List<Rezervisao> pribaviRezervacije()
        {
            GraphClient graphClient = DataLayer.getClient();

            var query = graphClient.Cypher
            .Match("(p:Putovanje)<-[r:REZERVISAO]-(k:Klijent)")
            .Where((Putovanje p) => p.vremePolaska == this.vremePolaska)
            .AndWhere((Putovanje p) => p.vremeDolaska == this.vremeDolaska)
            .AndWhere((Putovanje p) => p.mestoIVremePolaska == this.mestoIVremePolaska)
            .AndWhere((Putovanje p) => p.cena == this.cena)
            .Return((k, r) =>
                new
                {
                    klijent = k.As<Klijent>(),
                    rezervacija = r.As<Rezervisao>(),
                });

            var res = query.ResultsAsync.Result.ToList();
            List<Rezervisao> sveRezervacije = new List<Rezervisao>();
            foreach (var result in res)
            {
                result.rezervacija.klijent = result.klijent;
                sveRezervacije.Add(result.rezervacija);
            }
            return sveRezervacije;
        }

        public List<Ocenio> pribaviOcene()
        {
            GraphClient graphClient = DataLayer.getClient();

            var query = graphClient.Cypher
            .Match("(p:Putovanje)<-[r:OCENIO]-(k:Klijent)")
            .Where((Putovanje p) => p.vremePolaska == this.vremePolaska)
            .AndWhere((Putovanje p) => p.vremeDolaska == this.vremeDolaska)
            .AndWhere((Putovanje p) => p.mestoIVremePolaska == this.mestoIVremePolaska)
            .AndWhere((Putovanje p) => p.cena == this.cena)
            .Return((r) =>
                new
                {
                    ocena = r.As<Ocenio>(),
                });

            var res = query.ResultsAsync.Result.ToList();
            List<Ocenio> sveOcene = new List<Ocenio>();
            foreach (var result in res)
                sveOcene.Add(result.ocena);
            return sveOcene;
        }

        public float prosecnaOcena()
        {
            List<Ocenio> sveOcene = this.pribaviOcene();
            if (sveOcene.Count != 0)
            {
                float suma = 0;
                foreach (Ocenio o in sveOcene)
                    suma += o.ocena;
                float prOc = suma / (float)sveOcene.Count;
                return prOc;
            }
            return 0;
        }

        public static List<Putovanje> pribaviPutovanjaPremaDrzavi(String drzava)
        {
            GraphClient graphClient = DataLayer.getClient();

            var query = graphClient.Cypher
            .Match("(p:Putovanje)-[:DO_MESTA]->(d:Destinacija)")
            .Where((Destinacija d) => d.drzava == drzava)
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

        public static List<Putovanje> pribaviPutovanjaPremaMestu(String mesto)
        {
            GraphClient graphClient = DataLayer.getClient();

            var query = graphClient.Cypher
            .Match("(p:Putovanje)-[:DO_MESTA]->(d:Destinacija)")
            .Where((Destinacija d) => d.mesto == mesto)
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
