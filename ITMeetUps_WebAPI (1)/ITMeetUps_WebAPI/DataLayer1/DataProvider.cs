using Cassandra;
using CassandraDataLayer.QueryEntities;
using DataLayer1.QueryEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer1
{
    public static class DataProvider
    {
        private static bool isReturned = false;
        #region User
        public static bool DodajUsera(User u)
        {
            try
            {
                ISession session = SessionManager.GetSession();

                if (session == null)
                {
                    return false;
                }
                String inter = "";
                if (u.interesovanja != null)
                {
                    for (int i = 0; i < u.interesovanja.Count; i++)
                    {
                        if (i == u.interesovanja.Count - 1)
                        {
                            inter += "'" + u.interesovanja[i] + "'";
                            break;
                        }
                        inter += "'" + u.interesovanja[i] + "',";
                    }
                }

                RowSet entry = session.Execute("INSERT INTO \"User\" (username, ime, prezime, interesovanja) VALUES ('" + u.username + "', '" + u.ime + "','" + u.prezime + "', [" + inter + "]);");
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static IList<User> VratiSveUsere()
        {
            try
            {
                ISession session = SessionManager.GetSession();
                IList<User> returnList = new List<User>();

                if (session == null)
                    return null;

                var prijave = session.Execute("SELECT * FROM \"User\";");
                foreach (var row in prijave)
                {
                    User user = new User();
                    user.ime = row["ime"] != null ? row["ime"].ToString() : string.Empty;
                    user.prezime = row["prezime"] != null ? row["prezime"].ToString() : string.Empty;
                    user.username = row["username"] != null ? row["username"].ToString() : string.Empty;
                    string[] interesovanja = row["interesovanja"] != null ? (string[])row["interesovanja"] : null;

                    if (interesovanja != null)
                    {
                        user.interesovanja = new List<string>(interesovanja);
                    }

                    returnList.Add(user);
                }

                return returnList;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static User VratiUseraPoUsernameu(string username)
        {
            try
            {
                ISession session = SessionManager.GetSession();

                if (session == null)
                    return null;

                var prijave = session.Execute("SELECT * FROM \"User\" where username='" + username + "';");

                User user = null;
                foreach (var row in prijave)
                {
                    user = new User();
                    user.ime = row["ime"] != null ? row["ime"].ToString() : string.Empty;
                    user.prezime = row["prezime"] != null ? row["prezime"].ToString() : string.Empty;
                    user.username = row["username"] != null ? row["username"].ToString() : string.Empty;
                    string[] interesovanja = row["interesovanja"] != null ? (string[])row["interesovanja"] : null;

                    if (interesovanja != null)
                    {
                        user.interesovanja = new List<string>(interesovanja);
                    }

                }

                return user;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static bool DodajInteresovanjaUseru(string username, IList<string> interesovanja)
        {
            try
            {
                ISession session = SessionManager.GetSession();

                if (session == null)
                    return false;

                String inter = "";
                if (interesovanja != null)
                {
                    for (int i = 0; i < interesovanja.Count; i++)
                    {
                        if (i == interesovanja.Count - 1)
                        {
                            inter += "'" + interesovanja[i] + "'";
                            break;
                        }
                        inter += "'" + interesovanja[i] + "',";
                    }
                }
                RowSet entry = session.Execute("UPDATE \"User\" SET interesovanja = interesovanja + [" + inter + "] WHERE username = '" + username + "';");
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool ObrisiUsera(string username)
        {
            try
            {
                ISession session = SessionManager.GetSession();

                if (session == null)
                    return false;

                RowSet entry = session.Execute("DELETE FROM \"User\" WHERE username = '" + username + "';");
                DataProvider.ObrisiSvePrijavePoUserimaPoUsername(username);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        #endregion

        #region Firma

        public static IList<Firma> VratiSveFirme()
        {
            IList<Firma> firme = new List<Firma>();
            try
            {
                ISession session = SessionManager.GetSession();
                if (session == null)
                    return null;

                var firmeData = session.Execute("select * from \"Firma\";");
                foreach (var firma in firmeData)
                {
                    Firma novaFirma = new Firma();
                    novaFirma.pib = firma["pib"] == null ? string.Empty : firma["pib"].ToString();
                    novaFirma.naziv = firma["naziv"] == null ? string.Empty : firma["naziv"].ToString();
                    novaFirma.adresa = firma["adresa"] == null ? string.Empty : firma["adresa"].ToString();
                    firme.Add(novaFirma);
                }
                return firme;
            }
            catch (Exception e)
            {
                return firme;
            }
        }

        public static Firma VratiFirmuPIB(string pib)
        {
            Firma returnFirma = new Firma();
            try
            {
                ISession session = SessionManager.GetSession();
                if (session == null)
                    return null;

                var firmaData = session.Execute("select * from \"Firma\" where pib='" + pib + "';");
                foreach (var firma in firmaData)
                {
                    returnFirma.pib = firma["pib"] == null ? string.Empty : firma["pib"].ToString();
                    returnFirma.naziv = firma["naziv"] == null ? string.Empty : firma["naziv"].ToString();
                    returnFirma.adresa = firma["adresa"] == null ? string.Empty : firma["adresa"].ToString();
                }

                return returnFirma;
            }
            catch (Exception e)
            {
                return returnFirma;
            }
        }

        public static bool DodajFirmu(Firma firma)
        {
            try
            {
                ISession session = SessionManager.GetSession();
                if (session == null)
                    return false;

                session.Execute("insert into \"Firma\" (pib, naziv, adresa) values ('" + firma.pib + "','"
                    + firma.naziv + "','" + firma.adresa + "');");
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public static bool ObrisiFirmu(string pib)
        {
            try
            {
                ISession session = SessionManager.GetSession();

                if (session == null)
                    return false;

                RowSet entry = session.Execute("DELETE FROM \"Firma\" WHERE pib = '" + pib + "';");
                ObrisiSvePrezentacijeZaFirmu(pib);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool UpdateFirmu(string pib, string adresa)
        {
            try
            {
                ISession session = SessionManager.GetSession();

                session.Execute("update \"Firma\" set adresa='" + adresa + "' where pib='" + pib + "'");
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region Prezentacija

        public static IList<Prezentacija> VratiSvePrezentacije()
        {
            IList<Prezentacija> prezentacije = new List<Prezentacija>();
            try
            {
                ISession session = SessionManager.GetSession();
                if (session == null)
                    return null;

                var prezentacijeData = session.Execute("select * from \"Prezentacija\";");

                foreach (var prezentacija in prezentacijeData)
                {
                    Prezentacija novaPrezentacija = new Prezentacija();
                    novaPrezentacija.naziv_prezentacije = prezentacija["naziv_prezentacije"] == null ?
                        string.Empty : prezentacija["naziv_prezentacije"].ToString();
                    novaPrezentacija.datum = prezentacija["datum"] == null ?
                        string.Empty : prezentacija["datum"].ToString();
                    novaPrezentacija.predavac = prezentacija["predavac"] == null ?
                        string.Empty : prezentacija["predavac"].ToString();
                    novaPrezentacija.interesovanje = prezentacija["interesovanje"] == null ?
                        string.Empty : prezentacija["interesovanje"].ToString();
                    prezentacije.Add(novaPrezentacija);
                }
                return prezentacije;
            }
            catch (Exception e)
            {
                return prezentacije;
            }
        }

        public static IList<Prezentacija> VratiPrezentaciju(string naziv)
        {
            try
            {
                IList<Prezentacija> returnPrezentacije = new List<Prezentacija>();
                ISession session = SessionManager.GetSession();
                if (session == null)
                    return null;

                var prezentacijeData = session.Execute("select * from \"Prezentacija\" where naziv_prezentacije = '"
                    + naziv + "';");

                foreach (var el in prezentacijeData)
                {
                    Prezentacija novaPrezentacija = new Prezentacija();
                    novaPrezentacija.naziv_prezentacije = el["naziv_prezentacije"] == null ?
                        string.Empty : el["naziv_prezentacije"].ToString();
                    novaPrezentacija.datum = el["datum"] == null ?
                        string.Empty : el["datum"].ToString();
                    novaPrezentacija.predavac = el["predavac"] == null ?
                        string.Empty : el["predavac"].ToString();
                    novaPrezentacija.interesovanje = el["interesovanje"] == null ?
                        string.Empty : el["interesovanje"].ToString();
                    returnPrezentacije.Add(novaPrezentacija);
                }
                return returnPrezentacije;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static bool DodajPrezentaciju(Prezentacija prezentacija)
        {
            try
            {
                ISession session = SessionManager.GetSession();
                if (session == null)
                    return false;

                session.Execute("insert into \"Prezentacija\" (naziv_prezentacije, datum, predavac, interesovanje) " +
                    "values ('" + prezentacija.naziv_prezentacije + "','" + prezentacija.datum + "','" + prezentacija.predavac +
                    "','" + prezentacija.interesovanje + "');");
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool ObrisiPrezentaciju(string nazivPrezentacije)
        {
            try
            {
                ISession session = SessionManager.GetSession();
                if (session == null)
                    return false;

                session.Execute("delete from \"Prezentacija\" where naziv_prezentacije='" + nazivPrezentacije + "';");
                //TODO:
                //ObrisiSvePrezentacijeZaFirmuPoPrezentaciji(nazivPrezentacije);  
                //ObrisiSvePrijavePoPrezentacijamaPoPrezentaciji(nazivPrezentacije);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool UpdateInteresovanjaZaPrezentaciju(string nazivPrezentacije, string interesovanje)
        {
            try
            {
                ISession session = SessionManager.GetSession();
                if (session == null)
                    return false;

                List<Prezentacija> prezentacija = (List<Prezentacija>)VratiPrezentaciju(nazivPrezentacije);

                //TODO: nije update, dodavanje je
                session.Execute("insert into \"Prezentacija\" (naziv_prezentacije, datum, predavac, interesovanje)" +
                    " values ('" + nazivPrezentacije + "','" + prezentacija[0].datum + "','" + prezentacija[0].predavac
                    + "','" + interesovanje + "');");

                return true;

            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool UpdatePrezentaciju(string naziv, string predavac)
        {
            try
            {
                ISession session = SessionManager.GetSession();
                if (session == null)
                    return false;

                //TODO: some clustering keys are missing
                //session.Execute("update \"Prezentacija\" set predavac = '" + predavac + "' where naziv_prezentacije" +
                //"='" + naziv + "'");

                session.Execute("update \"Prezentacija\" set predavac = '" + predavac + "' where naziv_prezentacije" +
                    "='" + naziv + "' and interesovanje ='web';");

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        #endregion

        #region Prijava_Po_Userima

        public static bool DodajPrijavuPoUserima(string username, string prezentacija, bool createAnother)
        {
            try
            {
                ISession session = SessionManager.GetSession();

                if (session == null)
                {
                    return false;
                }

                RowSet entry = session.Execute("  INSERT INTO \"Prijava_po_userima\" (username,naziv_prezentacije) VALUES('" + username + "', '" + prezentacija + "'); ");
                if (createAnother)
                    DataProvider.DodajPrijavuPoPrezentacijama(username, prezentacija, false);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static IList<PrijavaPoUseru> VratiSvePrijavePoUserima()
        {
            try
            {
                ISession session = SessionManager.GetSession();
                IList<PrijavaPoUseru> returnList = new List<PrijavaPoUseru>();

                if (session == null)
                    return null;

                var prijave = session.Execute("SELECT * FROM \"Prijava_po_userima\";");
                foreach (var row in prijave)
                {
                    PrijavaPoUseru prijava = new PrijavaPoUseru();
                    prijava.naziv_prezentacije = row["naziv_prezentacije"] != null ? row["naziv_prezentacije"].ToString() : string.Empty;
                    prijava.username = row["username"] != null ? row["username"].ToString() : string.Empty;
                    returnList.Add(prijava);
                }

                return returnList;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static IList<PrijavaPoUseru> VratiSvePrijavePoUserimaPoUsername(string username)
        {
            try
            {
                ISession session = SessionManager.GetSession();
                IList<PrijavaPoUseru> returnList = new List<PrijavaPoUseru>();

                if (session == null)
                    return null;

                var prijave = session.Execute("SELECT * FROM \"Prijava_po_userima\" WHERE username='" + username + "';");
                foreach (var row in prijave)
                {
                    PrijavaPoUseru prijava = new PrijavaPoUseru();
                    prijava.naziv_prezentacije = row["naziv_prezentacije"] != null ? row["naziv_prezentacije"].ToString() : string.Empty;
                    prijava.username = row["username"] != null ? row["username"].ToString() : string.Empty;
                    returnList.Add(prijava);
                }

                return returnList;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static bool ObrisiSvePrijavePoUserimaPoUsername(string username)
        {
            try
            {
                ISession session = SessionManager.GetSession();

                if (session == null)
                    return false;

                RowSet row = session.Execute("delete from \"Prijava_po_userima\" where username='" + username + "';");
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        #endregion

        #region Prijava_po_prezentacijama
        public static bool DodajPrijavuPoPrezentacijama(string username, string prezentacija, bool createAnother)
        {
            try
            {
                ISession session = SessionManager.GetSession();

                if (session == null)
                {
                    return false;
                }

                RowSet entry = session.Execute("  INSERT INTO \"Prijava_po_prezentacijama\" (username,naziv_prezentacije) VALUES('" + username + "', '" + prezentacija + "'); ");
                if (createAnother)
                    DataProvider.DodajPrijavuPoUserima(username, prezentacija, false);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static IList<PrijavaPoPrezentaciji> VratiSvePrijavePoPrezentacijama()
        {
            try
            {
                ISession session = SessionManager.GetSession();
                IList<PrijavaPoPrezentaciji> returnList = new List<PrijavaPoPrezentaciji>();

                if (session == null)
                    return null;

                var prijave = session.Execute("SELECT * FROM \"Prijava_po_prezentacijama\";");
                foreach (var row in prijave)
                {
                    PrijavaPoPrezentaciji prijava = new PrijavaPoPrezentaciji();
                    prijava.naziv_prezentacije = row["naziv_prezentacije"] != null ? row["naziv_prezentacije"].ToString() : string.Empty;
                    prijava.username = row["username"] != null ? row["username"].ToString() : string.Empty;
                    returnList.Add(prijava);
                }

                return returnList;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static IList<PrijavaPoPrezentaciji> VratiSvePrijavePoPrezentacijamaPoPrezentaciji(string nazivPrezentacije)
        {
            try
            {
                ISession session = SessionManager.GetSession();
                IList<PrijavaPoPrezentaciji> returnList = new List<PrijavaPoPrezentaciji>();

                if (session == null)
                    return null;

                var prijave = session.Execute("SELECT * FROM \"Prijava_po_prezentacijama\" WHERE naziv_prezentacije='" + nazivPrezentacije + "';");
                foreach (var row in prijave)
                {
                    PrijavaPoPrezentaciji prijava = new PrijavaPoPrezentaciji();
                    prijava.naziv_prezentacije = row["naziv_prezentacije"] != null ? row["naziv_prezentacije"].ToString() : string.Empty;
                    prijava.username = row["username"] != null ? row["username"].ToString() : string.Empty;
                    returnList.Add(prijava);
                }

                return returnList;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static bool ObrisiSvePrijavePoPrezentacijamaPoPrezentaciji(string nazivPrezentacije)
        {
            try
            {
                ISession session = SessionManager.GetSession();

                if (session == null)
                    return false;

                RowSet row = session.Execute("delete from \"Prijava_po_prezentacijama\" where naziv_prezentacije='" + nazivPrezentacije + "';");
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        #region Prezentacije_po_firmama
        public static IList<PrezentacijePoFirmama> VratiSvePrezentacijePoFirmama()
        {
            try
            {
                ISession session = SessionManager.GetSession();
                if (session == null)
                    return null;
                IList<PrezentacijePoFirmama> prezentacije = new List<PrezentacijePoFirmama>();
                var prezentacijeData = session.Execute("select * from \"Prezentacije_po_firmama\";");

                foreach (var prezentacija in prezentacijeData)
                {
                    PrezentacijePoFirmama novaPrezentacija = new PrezentacijePoFirmama();
                    novaPrezentacija.pib = prezentacija["pib"] == null ? string.Empty : prezentacija["pib"].ToString();
                    novaPrezentacija.naziv_prezentacije = prezentacija["naziv_prezentacije"] == null
                        ? string.Empty : prezentacija["naziv_prezentacije"].ToString();
                    prezentacije.Add(novaPrezentacija);
                }
                return prezentacije;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static IList<PrezentacijePoFirmama> VratiPrezentacijeZaFirmu(string pib)
        {
            try
            {
                ISession session = SessionManager.GetSession();
                if (session == null)
                    return null;

                IList<PrezentacijePoFirmama> prezentacijePoFirmama = new List<PrezentacijePoFirmama>();

                var prezentacijeData = session.Execute("select * from \"Prezentacije_po_firmama\" where pib = '" +
                    pib + "';");

                foreach (var row in prezentacijeData)
                {
                    PrezentacijePoFirmama novaPrezentacija = new PrezentacijePoFirmama();
                    novaPrezentacija.pib = row["pib"] == null ? string.Empty : row["pib"].ToString();
                    novaPrezentacija.naziv_prezentacije = row["naziv_prezentacije"] == null
                        ? string.Empty : row["naziv_prezentacije"].ToString();
                    prezentacijePoFirmama.Add(novaPrezentacija);
                }

                return prezentacijePoFirmama;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static bool DodajPrezentacijuZaFirmu(PrezentacijePoFirmama prezentacijaZaFirmu)
        {
            try
            {
                ISession session = SessionManager.GetSession();
                if (session == null)
                    return false;
                session.Execute("insert into \"Prezentacije_po_firmama\" (pib, naziv_prezentacije) values ('"
                    + prezentacijaZaFirmu.pib + "','" + prezentacijaZaFirmu.naziv_prezentacije + "');");

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool ObrisiPrezentacijuZaFirmu(PrezentacijePoFirmama prezentacijaZaFirmu)
        {
            try
            {
                ISession session = SessionManager.GetSession();
                if (session == null)
                    return false;
                session.Execute("delete from \"Prezentacije_po_firmama\" where pib ='"
                    + prezentacijaZaFirmu.pib + "' and naziv_prezentacije ='"
                    + prezentacijaZaFirmu.naziv_prezentacije + "';");

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool ObrisiSvePrezentacijeZaFirmu(string pib)
        {
            try
            {
                ISession session = SessionManager.GetSession();
                if (session == null)
                    return false;
                session.Execute("delete from \"Prezentacije_po_firmama\" where pib ='" + pib + "';");

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool ObrisiSvePrezentacijeZaFirmuPoPrezentaciji(string naziv_prezentacije)
        {
            try
            {
                ISession session = SessionManager.GetSession();
                if (session == null)
                    return false;
                session.Execute("delete from \"Prezentacije_po_firmama\" where naziv_prezentacije ='" + naziv_prezentacije + "';");
                //TODO: some partition keys are missing: pib
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        //TODO: update?

        #endregion

        #region Komentari
        public static IList<Komentar> VratiSveKomentarePoUserima()
        {
            try
            {
                ISession session = SessionManager.GetSession();
                IList<Komentar> returnList = new List<Komentar>();

                if (session == null)
                    return null;

                var komentari = session.Execute("SELECT * FROM \"Komentari_po_userima\";");
                foreach (var row in komentari)
                {
                    Komentar kom = new Komentar();
                    kom.username = row["username"] != null ? row["username"].ToString() : string.Empty;
                    kom.nazivPrezentacije = row["naziv_prezentacije"] != null ? row["naziv_prezentacije"].ToString() : string.Empty;
                    kom.datum = row["datum"] != null ? row["datum"].ToString() : string.Empty;
                    kom.brojZvezdica = row["broj_zvezdica"] != null ? int.Parse(row["broj_zvezdica"].ToString()) : 0;
                    kom.komentar = row["komentar"] != null ? row["komentar"].ToString() : string.Empty;


                    returnList.Add(kom);
                }

                return returnList;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public static IList<Komentar> VratiSveKomentarePoPrezentacijama()
        {
            try
            {
                ISession session = SessionManager.GetSession();
                IList<Komentar> returnList = new List<Komentar>();

                if (session == null)
                    return null;

                var komentari = session.Execute("SELECT * FROM \"Komentari_po_prezentacijama\";");
                foreach (var row in komentari)
                {
                    Komentar kom = new Komentar();
                    kom.username = row["username"] != null ? row["username"].ToString() : string.Empty;
                    kom.nazivPrezentacije = row["naziv_prezentacije"] != null ? row["naziv_prezentacije"].ToString() : string.Empty;
                    kom.datum = row["datum"] != null ? row["datum"].ToString() : string.Empty;
                    kom.brojZvezdica = row["broj_zvezdica"] != null ? int.Parse(row["broj_zvezdica"].ToString()) : 0;
                    kom.komentar = row["komentar"] != null ? row["komentar"].ToString() : string.Empty;


                    returnList.Add(kom);
                }

                return returnList;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static IList<Komentar> VratiKomentareUsera(string username)
        {
            try
            {
                ISession session = SessionManager.GetSession();
                IList<Komentar> returnList = new List<Komentar>();

                if (session == null)
                    return null;

                var komentari = session.Execute("SELECT * FROM \"Komentari_po_userima\" WHERE username = '" + username + "';");

                foreach (var row in komentari)
                {
                    Komentar kom = new Komentar();
                    kom.username = row["username"] != null ? row["username"].ToString() : string.Empty;
                    kom.nazivPrezentacije = row["naziv_prezentacije"] != null ? row["naziv_prezentacije"].ToString() : string.Empty;
                    kom.datum = row["datum"] != null ? row["datum"].ToString() : string.Empty;
                    kom.brojZvezdica = row["broj_zvezdica"] != null ? int.Parse(row["broj_zvezdica"].ToString()) : 0;
                    kom.komentar = row["komentar"] != null ? row["komentar"].ToString() : string.Empty;


                    returnList.Add(kom);
                }

                return returnList;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static IList<Komentar> VratiKomentarePrezentacije(string nazivPrezentacije)
        {
            try
            {
                ISession session = SessionManager.GetSession();
                IList<Komentar> returnList = new List<Komentar>();

                if (session == null)
                    return null;

                var komentari = session.Execute("SELECT * FROM \"Komentari_po_prezentacijama\" WHERE naziv_prezentacije = '" + nazivPrezentacije + "';");

                foreach (var row in komentari)
                {
                    Komentar kom = new Komentar();
                    kom.username = row["username"] != null ? row["username"].ToString() : string.Empty;
                    kom.nazivPrezentacije = row["naziv_prezentacije"] != null ? row["naziv_prezentacije"].ToString() : string.Empty;
                    kom.datum = row["datum"] != null ? row["datum"].ToString() : string.Empty;
                    kom.brojZvezdica = row["broj_zvezdica"] != null ? int.Parse(row["broj_zvezdica"].ToString()) : 0;
                    kom.komentar = row["komentar"] != null ? row["komentar"].ToString() : string.Empty;


                    returnList.Add(kom);
                }

                return returnList;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static bool DodajKomentarUseru(Komentar komentariPoUserima)
        {
            try
            {
                ISession session = SessionManager.GetSession();
                
                if (session == null)
                    return false;
                session.Execute("insert into \"Komentari_po_userima\" (username, naziv_prezentacije, datum, broj_zvezdica, komentar) values ('"
                    + komentariPoUserima.username + "','" + komentariPoUserima.nazivPrezentacije + "','"+ komentariPoUserima.datum + "',"+komentariPoUserima.brojZvezdica
                    + ",'" + komentariPoUserima.komentar + "');");

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool DodajKomentarPrezentaciji(Komentar komentariPoPrezentacijama)
        {
            try
            {
                ISession session = SessionManager.GetSession();
                if (session == null)
                    return false;
                session.Execute("insert into \"Komentari_po_prezentacijama\" (naziv_prezentacije, username, datum, broj_zvezdica, komentar) values ('"
                    + komentariPoPrezentacijama.nazivPrezentacije + "','" + komentariPoPrezentacijama.username + "','" + komentariPoPrezentacijama.datum + "'," + komentariPoPrezentacijama.brojZvezdica
                    + ",'" + komentariPoPrezentacijama.komentar + "');");

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static bool ObrisiKomentarUsera(Komentar komentariPoUserima)
        {
            try
            {
                ISession session = SessionManager.GetSession();
                if (session == null)
                    return false;
                /*DELETE  FROM "Komentari_po_userima"
                        WHERE username='Dule123' AND naziv_prezentacije='Web programiranje 1' AND  datum='2017-05-10'
                                IF broj_zvezdica=4 AND komentar='Onako...';*/
                session.Execute("delete from \"Komentari_po_userima\" where username ='"
                    + komentariPoUserima.username + "' and naziv_prezentacije ='"
                    + komentariPoUserima.nazivPrezentacije + "' and datum='"+komentariPoUserima.datum + "' " + 
                    "if broj_zvezdica="+komentariPoUserima.brojZvezdica +" and komentar='" + komentariPoUserima.komentar +"';");

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool ObrisiKomentarPrezentacije(Komentar komentariPoPrezentacijama)
        {
            try
            {
                ISession session = SessionManager.GetSession();
                if (session == null)
                    return false;
                /*DELETE  FROM "Komentari_po_userima"
                        WHERE username='Dule123' AND naziv_prezentacije='Web programiranje 1' AND  datum='2017-05-10'
                                IF broj_zvezdica=4 AND komentar='Onako...';*/
                session.Execute("delete from \"Komentari_po_prezentacijama\" where username ='"
                    + komentariPoPrezentacijama.username + "' and naziv_prezentacije ='"
                    + komentariPoPrezentacijama.nazivPrezentacije + "' and datum='" + komentariPoPrezentacijama.datum + "' " +
                    "if broj_zvezdica=" + komentariPoPrezentacijama.brojZvezdica + " and komentar='" + komentariPoPrezentacijama.komentar + "';");

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static bool AzurirajKomentarPoPrezentacijama(Komentar komentariPoPrezentacijama)
        {
            try
            {
                ISession session = SessionManager.GetSession();
                if (session == null)
                    return false;

                /*
                  UPDATE "Komentari_po_prezentacijama" SET komentar='Onako...123', broj_zvezdica=3
                        WHERE naziv_prezentacije='Web programiranje 1' AND username='Dule123' AND datum='2017-05-10';
                 */

                session.Execute("update \"Komentari_po_prezentacijama\" set komentar = '" + komentariPoPrezentacijama.komentar +
                    "', broj_zvezdica="+komentariPoPrezentacijama.brojZvezdica+" where naziv_prezentacije ='" + komentariPoPrezentacijama.nazivPrezentacije + "' and username='" + komentariPoPrezentacijama.username +
                    "' and datum='" + komentariPoPrezentacijama.datum + "';");
                    

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static bool AzurirajKomentarPoUseru(Komentar komentariPoUserima)
        {
            try
            {
                ISession session = SessionManager.GetSession();
                if (session == null)
                    return false;

                /*
                  UPDATE "Komentari_po_useru" SET komentar='Onako...123', broj_zvezdica=3
                        WHERE naziv_prezentacije='Web programiranje 1' AND username='Dule123' AND datum='2017-05-10';
                 */

                session.Execute("update \"Komentari_po_userima\" set komentar = '" + komentariPoUserima.komentar +
                    "', broj_zvezdica=" + komentariPoUserima.brojZvezdica + " where naziv_prezentacije ='" + komentariPoUserima.nazivPrezentacije + "' and username='" + komentariPoUserima.username +
                    "' and datum='" + komentariPoUserima.datum + "';");


                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        #endregion

        #region Interesovanje_Po_Userima

        public static IList<InteresovanjePoUserima> VratiInteresovanjaPoUserima()
        {
            try
            {
                ISession sesija = SessionManager.GetSession();
                if (sesija == null)
                    return null;


                IList<InteresovanjePoUserima> interesovanja = new List<InteresovanjePoUserima>();
                var interesovanjaData = sesija.Execute("select * from \"Interesovanja_po_userima\";");

                foreach (var pr in interesovanjaData)
                {
                    InteresovanjePoUserima interesovanje = new InteresovanjePoUserima();
                    interesovanje.interesovanje = pr["interesovanje"] == null ? string.Empty : pr["interesovanje"].ToString();
                    interesovanje.username = pr["username"] == null ? string.Empty : pr["username"].ToString();

                    interesovanja.Add(interesovanje);
                }
                return interesovanja;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public static IList<InteresovanjePoUserima> VratiInteresovanjaPoUseru(string username)
        {
            try
            {
                ISession sesija = SessionManager.GetSession();
                if (sesija == null)
                    return null;
                if (username == null)
                    return null;

                IList<InteresovanjePoUserima> interesovanja = new List<InteresovanjePoUserima>();
                var interesovanjaData = sesija.Execute("select * from \"Interesovanja_po_userima\"where username = '" +
                    username + "';");

                foreach (var pr in interesovanjaData)
                {
                    InteresovanjePoUserima interesovanje = new InteresovanjePoUserima();
                    interesovanje.interesovanje = pr["interesovanje"] == null ? string.Empty : pr["interesovanje"].ToString();
                    interesovanje.username = username;

                    interesovanja.Add(interesovanje);
                }
                return interesovanja;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public static bool DodajInteresovanjePoUseru(InteresovanjePoUserima novoInt)
        {
            try
            {
                ISession sesija = SessionManager.GetSession();
                if (sesija == null)
                    return false;

                sesija.Execute("insert into \"Interesovanja_po_userima\" (username, interesovanje) values ('"
                    + novoInt.username + "','" + novoInt.interesovanje + "');");

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static bool AzurirajInteresovanjePoUseru(InteresovanjePoUserima interesovanje, string novoInteresovanje)
        {
            try
            {
                ISession sesija = SessionManager.GetSession();
                if (sesija == null)
                    return false;


                InteresovanjePoUserima novoInt = new InteresovanjePoUserima();
                novoInt.interesovanje = novoInteresovanje;
                novoInt.username = interesovanje.username;

                sesija.Execute("delete from \"Interesovanja_po_userima\" where interesovanje ='"
                    + interesovanje.interesovanje + "' and username ='"
                    + interesovanje.username + "';");
                sesija.Execute("insert into \"Interesovanja_po_userima\" (interesovanje, username) values ('"
                   + novoInt.interesovanje + "','" + novoInt.username + "');");




                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool ObrisiInteresovanjaPoUseru(InteresovanjePoUserima inter)
        {
            try
            {
                ISession sesija = SessionManager.GetSession();
                if (sesija == null)
                    return false;


                sesija.Execute("delete from \"Interesovanja_po_userima\" where interesovanje ='"
                    + inter.interesovanje + "' and username ='"
                    + inter.username + "';");

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        //TODO: brisanje svih interesovanja jednog usera

        #endregion

        #region Predavaci_Po_Prezentacijama

        public static IList<PredavaciPoPrezentacijama> VratiPredavacePoPrezentacijama()
        {
            try
            {
                ISession sesija = SessionManager.GetSession();
                if (sesija == null)
                    return null;

                IList<PredavaciPoPrezentacijama> predavaci = new List<PredavaciPoPrezentacijama>();
                var predavaciData = sesija.Execute("select * from \"Predavaci_po_prezentacijama\";");

                foreach (var novipred in predavaciData)
                {
                    PredavaciPoPrezentacijama predavac = new PredavaciPoPrezentacijama();
                    predavac.predavac = novipred["predavac"] == null ? string.Empty : novipred["predavac"].ToString();
                    predavac.prezentacija = novipred["prezentacija"] == null
                        ? string.Empty : novipred["prezentacija"].ToString();
                    predavaci.Add(predavac);
                }
                return predavaci;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static IList<PredavaciPoPrezentacijama> VratiPredavacePoPrezentaciji(string prezentacija)
        {
            try
            {
                ISession sesija = SessionManager.GetSession();
                if (sesija == null)
                    return null;
                if (prezentacija == null)
                    return null;

                IList<PredavaciPoPrezentacijama> predavaci = new List<PredavaciPoPrezentacijama>();
                var predavaciData = sesija.Execute("select * from \"Predavaci_po_prezentacijama\"where prezentacija = '" +
                    prezentacija + "';");

                foreach (var pred in predavaciData)
                {
                    PredavaciPoPrezentacijama predavac = new PredavaciPoPrezentacijama();
                    predavac.predavac = pred["predavac"] == null ? string.Empty : pred["predavac"].ToString();
                    predavac.prezentacija = prezentacija;

                    predavaci.Add(predavac);
                }
                return predavaci;
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public static bool DodajPredavacaPoPrezentaciji(PredavaciPoPrezentacijama predavacpoPrez)
        {
            try
            {
                ISession sesija = SessionManager.GetSession();
                if (sesija == null)
                    return false;
                sesija.Execute("insert into \"Predavaci_po_prezentacijama\" (predavac, prezentacija) values ('"
                    + predavacpoPrez.predavac + "','" + predavacpoPrez.prezentacija + "');");

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool ObrisiPredavacaPoPrezentaciji(PredavaciPoPrezentacijama predavac)
        {
            try
            {
                ISession sesija = SessionManager.GetSession();
                if (sesija == null)
                    return false;


                sesija.Execute("delete from \"Predavaci_po_prezentacijama\" where predavac ='"
                    + predavac.predavac + "' and prezentacija ='"
                    + predavac.prezentacija + "';");

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool AzurirajPredavacaPoPrezentaciji(PredavaciPoPrezentacijama predavac, string noviPredavac) //? da li moze samo jedan predavac
        {
            try
            {
                ISession sesija = SessionManager.GetSession();
                if (sesija == null)
                    return false;
                PredavaciPoPrezentacijama noviPred = new PredavaciPoPrezentacijama();
                noviPred.prezentacija = predavac.prezentacija;
                noviPred.predavac = noviPredavac;
                sesija.Execute("delete from \"Predavaci_po_prezentacijama\" where predavac ='"
                    + predavac.predavac + "' and prezentacija ='"
                    + predavac.prezentacija + "';");
                sesija.Execute("insert into \"Predavaci_po_prezentacijama\" (predavac, prezentacija) values ('"
                   + noviPred.predavac + "','" + noviPred.prezentacija + "');");


                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }
        //??
        #endregion

    }
}
