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
    public class Admin
    { 
        public String password { get; set; }
        public String username { get; set; }
        public Admin()
        {

        }

        public static void kreirajAdmina()
        {
            if (!postojiAdmin())
            {
                GraphClient graphClient = DataLayer.getClient();

                var a = new Admin { username = "admin", password = "admin" };

                graphClient.Cypher
                .Create("(admin:Admin {parametri})")
                .WithParam("parametri", a)
                .ExecuteWithoutResultsAsync();
            }

        }

       public static bool postojiAdmin()
        {
            GraphClient graphClient = DataLayer.getClient();

            var query = graphClient.Cypher
           .Match("(a:Admin)")
           .Return((a) =>
                new
                {
                    ad = a.As<Admin>(),
                });

            var res = query.ResultsAsync.Result.ToList();
            if (res.Count == 0)
                return false;
            else return true;
        } 

        public static Admin LogIn(string un, string pass)
        {
            GraphClient client = DataLayer.getClient();

            var query = client.Cypher
            .Match("(n:Admin)")
            .Where((Admin n) => n.username == un)
            .Return((n) =>
                new
                {
                    Admin = n.As<Admin>(),
                });

            var res = query.ResultsAsync.Result.ToList();
            if (res.Count != 0)
            {
                Admin a = res[0].Admin;
                if (a.password == pass)
                    return a;
                else return null;
            }
            return null;

        }

        public void promeniSifru(string pass)
        {
            GraphClient client = DataLayer.getClient();

            client.Cypher
            .Match("(n:Admin)")
            .Set("n.password = {p}")
            .WithParam("p", pass)
            .ExecuteWithoutResultsAsync();
        }

        public void promeniKorisnicko(String name)
        {
            GraphClient client = DataLayer.getClient();

            client.Cypher
            .Match("(n:Admin)")
            .Set("n.username = {un}")
            .WithParam("un", name)
            .ExecuteWithoutResultsAsync();
        }

        public List<Destinacija> pribaviDestinacije(string drzava)
        {
            GraphClient client = DataLayer.getClient();

            var query = client.Cypher
            .Match("(des:Destinacija)")
            .Where((Destinacija des) => des.drzava == drzava)
            .Return((des) =>
                new
                {
                    Dest = des.As<Destinacija>(),
                });

            var res = query.ResultsAsync.Result.ToList();
            List<Destinacija> sveDestinacije = new List<Destinacija>();
            foreach (var result in res)
                sveDestinacije.Add(result.Dest);

            return sveDestinacije;
        }



        public List<Putovanje> pribaviPutovanja()
        {
            GraphClient client = DataLayer.getClient();

            var query = client.Cypher
            .Match("(p:Putovanje)")
            .Return((p) =>
                new
                {
                    Put = p.As<Putovanje>(),
                });

            var res = query.ResultsAsync.Result.ToList();
            List<Putovanje> svaPutovanja = new List<Putovanje>();
            foreach (var result in res)
                svaPutovanja.Add(result.Put);
            return svaPutovanja;
        }

        public void dodajPutovanje(Putovanje p, Smestaj s)
        {
            GraphClient graphClient = DataLayer.getClient();

            graphClient.Cypher
                .Match("(smestaj:Smestaj)-[:SE_NALAZI_U]->(dest:Destinacija)")
                .Where((Smestaj smestaj) => smestaj.naziv == s.naziv)
                .CreateUnique("(dest)<-[:DO_MESTA]-(putovanje:Putovanje {novo})-[:UKLJUCUJE]->(smestaj)")
                .WithParam("novo", p)
                .ExecuteWithoutResultsAsync();


        }


        public void dodajDestinaciju(Destinacija d)
        {
            GraphClient graphClient = DataLayer.getClient();

            var novaDest = d;

            graphClient.Cypher
            .Merge("(des:Destinacija { mesto: {id} })")
            .OnCreate()
            .Set("des = {novaDest}")
            .WithParams(new
            {
                id = novaDest.mesto,
                novaDest
            })
            .ExecuteWithoutResultsAsync();
        }

        public void dodajSmestaj(Smestaj s, Destinacija dest)
        {
            GraphClient graphClient = DataLayer.getClient();
            graphClient.Cypher
                .Match("(destinacija:Destinacija)")
                .Where((Destinacija destinacija) => destinacija.mesto == dest.mesto)
                .CreateUnique("(smestaj:Smestaj {novi})-[:SE_NALAZI_U]->(destinacija)")
                .WithParam("novi", s)
                .ExecuteWithoutResultsAsync();


        }

        public void obrisiPutovanje(Putovanje p)
        {
            //PROVERI
            GraphClient graphClient = DataLayer.getClient();

            graphClient.Cypher
            .Match("(put:Putovanje)-[r]-()")
            .Where((Putovanje put) => put.vremeDolaska == p.vremeDolaska)
            .AndWhere((Putovanje put) => put.vremePolaska == p.vremePolaska)
            .AndWhere((Putovanje put) => put.mestoIVremePolaska == p.mestoIVremePolaska)
            .AndWhere((Putovanje put) => put.cena == p.cena)
            .Delete("r,put")
            .ExecuteWithoutResultsAsync();
        }

        public void promeniCenuPutovanja(Putovanje put, int novaCena)
        {
            GraphClient graphClient = DataLayer.getClient();

            graphClient.Cypher
            .Match("(p:Putovanje)")
            .Where((Putovanje p) => p.vremeDolaska == put.vremeDolaska)
            .AndWhere((Putovanje p) => p.vremePolaska == put.vremePolaska)
            .AndWhere((Putovanje p) => p.mestoIVremePolaska == put.mestoIVremePolaska)
            .AndWhere((Putovanje p) => p.cena == put.cena)
            .Set("p.cena = {nc}")
            .WithParam("nc", novaCena)
            .ExecuteWithoutResultsAsync();
        }

        public void promeniVremePolaska(Putovanje put, DateTime novoVreme)
        {
            GraphClient graphClient = DataLayer.getClient();

            graphClient.Cypher
            .Match("(p:Putovanje)")
            .Where((Putovanje p) => p.vremeDolaska == put.vremeDolaska)
            .AndWhere((Putovanje p) => p.vremePolaska == put.vremePolaska)
            .AndWhere((Putovanje p) => p.mestoIVremePolaska == put.mestoIVremePolaska)
            .AndWhere((Putovanje p) => p.cena == put.cena)
            .Set("p.vremePolaska = {vp}")
            .WithParam("vp", novoVreme)
            .ExecuteWithoutResultsAsync();
        }

        public void promenimestoIVremePolaska(Putovanje put, String novoMestoiVreme)
        {
            GraphClient graphClient = DataLayer.getClient();

            graphClient.Cypher
            .Match("(p:Putovanje)")
            .Where((Putovanje p) => p.vremeDolaska == put.vremeDolaska)
            .AndWhere((Putovanje p) => p.vremePolaska == put.vremePolaska)
            .AndWhere((Putovanje p) => p.mestoIVremePolaska == put.mestoIVremePolaska)
            .AndWhere((Putovanje p) => p.cena == put.cena)
            .Set("p.mestoIVremePolaska = {mp}")
            .WithParam("mp", novoMestoiVreme)
            .ExecuteWithoutResultsAsync();
        }

        public void obrisiSmestaj(Smestaj s)
        {
            GraphClient graphClient = DataLayer.getClient();

            graphClient.Cypher
            .Match("(sm:Smestaj)-[r1]-(des:Destinacija)")
            .Where((Smestaj sm) => sm.naziv == s.naziv)
            .AndWhere((Smestaj sm) => sm.opis == s.opis)
            .OptionalMatch("(sm)-[r2]-(p:Putovanje)-[r3]-()")
            .Delete("r1,sm,r2,r3,p")
            .ExecuteWithoutResultsAsync();
        }

        public void obrisiDestinaciju(Destinacija dest)
        {
            GraphClient graphClient = DataLayer.getClient();

            graphClient.Cypher
            .Match("(d:Destinacija)")
            .Where((Destinacija d) => d.drzava == dest.drzava)
            .AndWhere((Destinacija d) => d.mesto == dest.mesto)
            .OptionalMatch("(d)-[r1]-(n)-[r2]-()")
            .Delete("r2,r1,n,d")
            .ExecuteWithoutResultsAsync();
        }

        public List<Smestaj> pribaviSmestaje(Destinacija destinacija)
        {
            GraphClient client = DataLayer.getClient();

            var query = client.Cypher
            .Match("(sm:Smestaj)-[:SE_NALAZI_U]->(des:Destinacija)")
            .Where((Destinacija des) => des.mesto == destinacija.mesto)
            .Return((sm) =>
                new
                {
                    Smestaj = sm.As<Smestaj>(),
                });

            var res = query.ResultsAsync.Result.ToList();
            List<Smestaj> sviSmestaji = new List<Smestaj>();
            foreach (var result in res)
                sviSmestaji.Add(result.Smestaj);
            return sviSmestaji;
        }

        public Smestaj pribaviSmestaj(Putovanje put)
        {
            GraphClient client = DataLayer.getClient();

            var query = client.Cypher
            .Match("(sm:Smestaj)<-[:UKLJUCUJE]-(p:Putovanje)")
            .Where((Putovanje p) => p.vremeDolaska == put.vremeDolaska)
            .AndWhere((Putovanje p) => p.vremePolaska == put.vremePolaska)
            .AndWhere((Putovanje p) => p.mestoIVremePolaska == put.mestoIVremePolaska)
            .AndWhere((Putovanje p) => p.cena == put.cena)
            .Return((sm) =>
                new
                {
                    smestaj = sm.As<Smestaj>(),
                });

            var res = query.ResultsAsync.Result.ToList();

            return res[0].smestaj;
        }
    }
}
