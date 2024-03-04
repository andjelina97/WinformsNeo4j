using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neo4jClient;
using Neo4jClient.Cypher;
using System.Windows.Forms;
using Turizam;

namespace TurizamProject.DomainModel
{
    public class Klijent
    {
        public String ime { get; set; }
        public String email { get; set; }
        public String password { get; set; }
        public String telefon { get; set; }


        public Klijent(String i, String e, String p, String t)
        {
            ime = i;
            email = e;
            password = p;
            telefon = t;

        }

        public Klijent()
        { }

        public static Klijent pribaviKlijenta(string email)
        {
            GraphClient client = DataLayer.getClient();

            var query = client.Cypher
            .Match("(n:Klijent)")
            .Where((Klijent n) => n.email == email)
            .Return((n) =>
                new
                {
                    Klijent = n.As<Klijent>(),
                });

            var res = query.ResultsAsync.Result.ToList();
            if (res.Count != 0)
                return res[0].Klijent;
            else return null;
        }

        public void sacuvajUDB()
        {
            GraphClient client = DataLayer.getClient();
            var noviKlijent = this;
            client.Cypher.Merge("(klijent:Klijent { email: {id} })")
            .OnCreate()
            .Set("klijent = {noviKlijent}")
            .WithParams(new
            {
                id = noviKlijent.email,
                noviKlijent
            })
            .ExecuteWithoutResultsAsync();
        }

        public static Klijent LogIn(string email, string pass)
        {
            Klijent pribavljen = Klijent.pribaviKlijenta(email);

            if (pribavljen != null && pribavljen.password == pass)
                return pribavljen;
            else return null;
        }

        public void posaljiZahtev(String mail)
        {

            GraphClient graphClient = DataLayer.getClient();

            graphClient.Cypher
            .Match("(n:Klijent)", "(m:Klijent)")
            .Where((Klijent n) => n.email == this.email)
            .AndWhere((Klijent m) => m.email == mail)
            .CreateUnique("(n)-[:POSLAO_ZAHTEV]->(m)")
            .ExecuteWithoutResultsAsync();

        }

        public void prihvatiZahtev(String mail)
        {
            GraphClient graphClient = DataLayer.getClient();
            obrisiZahtev(mail);

            graphClient.Cypher
            .Match("(n:Klijent)", "(m:Klijent)")
            .Where((Klijent n) => n.email == this.email)
            .AndWhere((Klijent m) => m.email == mail)
            .CreateUnique("(n)-[:PRIJATELJ]->(m)")
            .ExecuteWithoutResultsAsync();

        }

        public void obrisiZahtev(String mail)
        {
            GraphClient graphClient = DataLayer.getClient();

            graphClient.Cypher
            .Match("(m:Klijent)-[r:POSLAO_ZAHTEV]->(n:Klijent)")
            .Where((Klijent n) => n.email == this.email)
            .AndWhere((Klijent m) => m.email == mail)
            .Delete("r")
            .ExecuteWithoutResultsAsync();
        }

        public List<Klijent> pribaviZahteve()
        {
            GraphClient graphClient = DataLayer.getClient();

            var query = graphClient.Cypher
            .Match("(n:Klijent)-[:POSLAO_ZAHTEV]->(m:Klijent)")
            .Where((Klijent m) => m.email == this.email)
            .Return((n) =>
                new
                {
                    Klijenti = n.As<Klijent>(),
                });

            var res = query.ResultsAsync.Result.ToList();
            List<Klijent> sviKlijenti = new List<Klijent>();
            foreach (var result in res)
                sviKlijenti.Add(result.Klijenti);
            return sviKlijenti;
        }

        public List<Klijent> pribaviPrijatelje()
        {
            GraphClient graphClient = DataLayer.getClient();

            var query = graphClient.Cypher
            .Match("(n:Klijent)-[:PRIJATELJ]-(m:Klijent)")
            .Where((Klijent n) => n.email == this.email)
            .Return((m) =>
                new
                {
                    Klijenti = m.As<Klijent>(),
                });

            var res = query.ResultsAsync.Result.ToList();
            List<Klijent> sviKlijenti = new List<Klijent>();
            foreach (var result in res)
                sviKlijenti.Add(result.Klijenti);
            return sviKlijenti;
        }

        public List<Rezervisao> pribaviRezervacije()
        {
            GraphClient graphClient = DataLayer.getClient();

            var query = graphClient.Cypher
            .Match("(k:Klijent)-[r:REZERVISAO]->(p:Putovanje)")
            .Where((Klijent k) => k.email == this.email)
            .Return((r, p) =>
                new
                {
                    Rezervacija = r.As<Rezervisao>(),
                    Putovanje = p.As<Putovanje>()
                });

            var res = query.ResultsAsync.Result.ToList();
            List<Rezervisao> sveRezervacije = new List<Rezervisao>();
            foreach (var result in res)
            {
                result.Rezervacija.putovanje = result.Putovanje;
                sveRezervacije.Add(result.Rezervacija);
            }
            return sveRezervacije;
        }

        public List<Putovanje> pribaviInteresovanja()
        {
            GraphClient graphClient = DataLayer.getClient();

            var query = graphClient.Cypher
            .Match("(k:Klijent)-[:ZAINTERESOVAN]->(p:Putovanje)")
            .Where((Klijent k) => k.email == this.email)
            .Return((p) =>
                new
                {
                    Put = p.As<Putovanje>(),
                });

            var res = query.ResultsAsync.Result.ToList();
            List<Putovanje> svaInteresovanja = new List<Putovanje>();
            foreach (var result in res)
                svaInteresovanja.Add(result.Put);
            return svaInteresovanja;
        }

        public void oceniPutovanje(Putovanje p, int novaOcena)
        {
            GraphClient graphClient = DataLayer.getClient();

            graphClient.Cypher
           .Match("(k:Klijent)", "(put:Putovanje)")
           .Where((Klijent k) => k.email == this.email)
           .AndWhere((Putovanje put) => put.vremeDolaska == p.vremeDolaska)
           .AndWhere((Putovanje put) => put.vremePolaska == p.vremePolaska)
           .AndWhere((Putovanje put) => put.mestoIVremePolaska == p.mestoIVremePolaska)
           .AndWhere((Putovanje put) => put.cena == p.cena)
           .CreateUnique("(k)-[r:OCENIO {ocena:{ocena}}]->(put)")
           .WithParam("ocena", novaOcena)
           .ExecuteWithoutResultsAsync();

        }
        public bool ocenio(Putovanje p)
        {
            GraphClient graphClient = DataLayer.getClient();

            var query = graphClient.Cypher
           .Match("(k:Klijent)-[r:OCENIO]->(put:Putovanje)")
           .Where((Klijent k) => k.email == this.email)
           .AndWhere((Putovanje put) => put.vremeDolaska == p.vremeDolaska)
           .AndWhere((Putovanje put) => put.vremePolaska == p.vremePolaska)
           .AndWhere((Putovanje put) => put.mestoIVremePolaska == p.mestoIVremePolaska)
           .AndWhere((Putovanje put) => put.cena == p.cena)
           .Return((r) =>
                new
                {
                    ocena = r.As<Ocenio>(),
                });

            var res = query.ResultsAsync.Result.ToList();
            if (res.Count == 0)
                return false;
            else return true;
        }

        public void zainteresujSe(Putovanje p)
        {
            GraphClient graphClient = DataLayer.getClient();

            graphClient.Cypher
           .Match("(k:Klijent)", "(put:Putovanje)")
           .Where((Klijent k) => k.email == this.email)
           .AndWhere((Putovanje put) => put.vremeDolaska == p.vremeDolaska)
           .AndWhere((Putovanje put) => put.vremePolaska == p.vremePolaska)
           .AndWhere((Putovanje put) => put.mestoIVremePolaska == p.mestoIVremePolaska)
           .AndWhere((Putovanje put) => put.cena == p.cena)
           .CreateUnique("(k)-[r:ZAINTERESOVAN]->(put)")
           .ExecuteWithoutResultsAsync();
        }

        public void obrisiZainteresovanost(Putovanje p)
        {
            GraphClient graphClient = DataLayer.getClient();

            graphClient.Cypher
            .Match("(n:Klijent)-[r:ZAINTERESOVAN]->(put:Putovanje)")
            .Where((Klijent n) => n.email == this.email)
            .AndWhere((Putovanje put) => put.vremeDolaska == p.vremeDolaska)
            .AndWhere((Putovanje put) => put.vremePolaska == p.vremePolaska)
            .AndWhere((Putovanje put) => put.mestoIVremePolaska == p.mestoIVremePolaska)
            .AndWhere((Putovanje put) => put.cena == p.cena)
            .Delete("r")
            .ExecuteWithoutResultsAsync();
        }

        public void rezervisi(Putovanje p, int mesta)
        {
            GraphClient graphClient = DataLayer.getClient();
            obrisiZainteresovanost(p);

            graphClient.Cypher
           .Match("(k:Klijent)", "(put:Putovanje)")
           .Where((Klijent k) => k.email == this.email)
           .AndWhere((Putovanje put) => put.vremeDolaska == p.vremeDolaska)
           .AndWhere((Putovanje put) => put.vremePolaska == p.vremePolaska)
           .AndWhere((Putovanje put) => put.mestoIVremePolaska == p.mestoIVremePolaska)
           .AndWhere((Putovanje put) => put.cena == p.cena)
           .CreateUnique("(k)-[r:REZERVISAO {broj_putnika:{broj}}]->(put)")
           .WithParam("broj", mesta)
           .ExecuteWithoutResultsAsync();
        }

        public bool daLiJeVecOcenio(Putovanje p)
        {
            GraphClient graphClient = DataLayer.getClient();

            var query = graphClient.Cypher
            .OptionalMatch("(k:Klijent)-[o:OCENIO]->(put:Putovanje)")
            .Where((Klijent k) => k.email == this.email)
            .AndWhere((Putovanje put) => put.vremeDolaska == p.vremeDolaska)
            .AndWhere((Putovanje put) => put.vremePolaska == p.vremePolaska)
            .AndWhere((Putovanje put) => put.mestoIVremePolaska == p.mestoIVremePolaska)
            .AndWhere((Putovanje put) => put.cena == p.cena)
            .Return((o) =>
                 new
                 {
                     ocenio = o.As<Ocenio>(),
                 });

            var res = query.ResultsAsync.Result.ToList();
            if (res.Count == 0)
                return false;
            else return true;

        }

        public int pribaviBrojMesta(Putovanje putovanje)
        {
            GraphClient graphClient = DataLayer.getClient();

            var query = graphClient.Cypher
           .Match("(k:Klijent)-[r:REZERVISAO]->(put:Putovanje)")
           .Where((Klijent k) => k.email == this.email)
           .AndWhere((Putovanje put) => put.vremeDolaska == putovanje.vremeDolaska)
           .AndWhere((Putovanje put) => put.vremePolaska == putovanje.vremePolaska)
           .AndWhere((Putovanje put) => put.mestoIVremePolaska == putovanje.mestoIVremePolaska)
           .AndWhere((Putovanje put) => put.cena == putovanje.cena)
           .Return((r) =>
                new
                {
                    rezervacija = r.As<Rezervisao>()
                });

            var res = query.ResultsAsync.Result.ToList();

            return res[0].rezervacija.broj_putnika;
        }



    }
}
