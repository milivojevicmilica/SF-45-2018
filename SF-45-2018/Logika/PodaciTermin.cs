using SF_45_2018.Entiteti;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF_45_2018.Logika
{
    class PodaciTermin
    {
        public static ObservableCollection<Termin> listaTermina { get; set; }

     

        public static void UcitajTermine()
        {
            listaTermina = new ObservableCollection<Termin>();

            using (SqlConnection conn = new SqlConnection(MainWindow.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"select * from termini";
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = command;
                da.Fill(ds, "Termini");
                foreach (DataRow row in ds.Tables["Termini"].Rows)
                {
                    int sifra = (int)row["IdTermin"];
                    DateTime vremeOd = (DateTime)row["VremeZauzecaPocetak"];
                    DateTime vremeDo = (DateTime)row["VremeZauzecaKraj"];
                    string daniuNedelji = (string)row["DaniUNedelji"];
                    string tipNastave = (string)row["TipNastave"];
                    int predavac = (int)row["ZaduzeniPredavacId"];
                    int ustanova = (int)row["UstanovaId"];
                    int ucionica = (int)row["UcionicaId"];
                    bool active = (bool)row["Active"];



                    if (tipNastave.ToLower().Equals(ETipNastave.Predavanja.ToString().ToLower()))
                    {
                        Termin termin = new Termin(sifra, vremeOd, vremeDo, ustanova, ucionica);
                        termin.TipNastave = ETipNastave.Predavanja;
                        termin.Active = active;
                        termin.ZaduzeniPredavacId = predavac;

                        if (daniuNedelji.ToLower() == EDaniUnedelji.PONEDELJAK.ToString().ToLower())
                        {
                            termin.DaniUNedelji = EDaniUnedelji.PONEDELJAK;
                            listaTermina.Add(termin);
                        }
                        else if (daniuNedelji.ToLower() == EDaniUnedelji.UTORAK.ToString().ToLower())
                        {
                            termin.DaniUNedelji = EDaniUnedelji.UTORAK;
                            listaTermina.Add(termin);
                        }
                        else if (daniuNedelji.ToLower() == EDaniUnedelji.SREDA.ToString().ToLower())
                        {
                            termin.DaniUNedelji = EDaniUnedelji.SREDA;
                            listaTermina.Add(termin);
                        }
                        else if (daniuNedelji.ToLower() == EDaniUnedelji.CETVRTAK.ToString().ToLower())
                        {
                            termin.DaniUNedelji = EDaniUnedelji.CETVRTAK;
                            listaTermina.Add(termin);
                        }
                        else if (daniuNedelji.ToLower() == EDaniUnedelji.PETAK.ToString().ToLower())
                        {
                            termin.DaniUNedelji = EDaniUnedelji.PETAK;
                            listaTermina.Add(termin);
                        }
                        else if (daniuNedelji.ToLower() == EDaniUnedelji.SUBOTA.ToString().ToLower())
                        {
                            termin.DaniUNedelji = EDaniUnedelji.SUBOTA;
                            listaTermina.Add(termin);
                        }
                        else if (daniuNedelji.ToLower() == EDaniUnedelji.NEDELJA.ToString().ToLower())
                        {
                            termin.DaniUNedelji = EDaniUnedelji.NEDELJA;
                            listaTermina.Add(termin);
                        }
                    }

                    if (tipNastave.ToLower().Equals(ETipNastave.Vezbe.ToString().ToLower()))
                    {
                        Termin termin = new Termin(sifra, vremeOd, vremeDo, ustanova, ucionica);
                        termin.TipNastave = ETipNastave.Vezbe;
                        termin.Active = active;
                        termin.ZaduzeniPredavacId = predavac;

                        if (daniuNedelji.ToLower() == EDaniUnedelji.PONEDELJAK.ToString().ToLower())
                        {
                            termin.DaniUNedelji = EDaniUnedelji.PONEDELJAK;
                            listaTermina.Add(termin);
                        }
                        else if (daniuNedelji.ToLower() == EDaniUnedelji.UTORAK.ToString().ToLower())
                        {
                            termin.DaniUNedelji = EDaniUnedelji.UTORAK;
                            listaTermina.Add(termin);
                        }
                        else if (daniuNedelji.ToLower() == EDaniUnedelji.SREDA.ToString().ToLower())
                        {
                            termin.DaniUNedelji = EDaniUnedelji.SREDA;
                            listaTermina.Add(termin);
                        }
                        else if (daniuNedelji.ToLower() == EDaniUnedelji.CETVRTAK.ToString().ToLower())
                        {
                            termin.DaniUNedelji = EDaniUnedelji.CETVRTAK;
                            listaTermina.Add(termin);
                        }
                        else if (daniuNedelji.ToLower() == EDaniUnedelji.PETAK.ToString().ToLower())
                        {
                            termin.DaniUNedelji = EDaniUnedelji.PETAK;
                            listaTermina.Add(termin);
                        }
                        else if (daniuNedelji.ToLower() == EDaniUnedelji.SUBOTA.ToString().ToLower())
                        {
                            termin.DaniUNedelji = EDaniUnedelji.SUBOTA;
                            listaTermina.Add(termin);
                        }
                        else if (daniuNedelji.ToLower() == EDaniUnedelji.NEDELJA.ToString().ToLower())
                        {
                            termin.DaniUNedelji = EDaniUnedelji.NEDELJA;
                            listaTermina.Add(termin);
                        }


                    }

                    foreach (var ust in PodaciUstanova.listaUstanova)
                    {
                        foreach (var termin in listaTermina)
                        {
                            if (ust.SifraUstanove.Equals(termin.UstanovaId))
                            {
                                termin.Ustanova = ust;
                            }
                        }
                    }

                    foreach (var pred in PodaciKorisnik.listaKorisnika)
                    {
                        foreach (var termin in listaTermina)
                        {
                            if (pred.Id.Equals(termin.ZaduzeniPredavacId))
                            {
                                termin.ZaduzeniPredavac = pred;
                            }
                        }
                    }

                    foreach (var uciona in PodaciUcionica.listaUcionica)
                    {
                        foreach (var termin in listaTermina)
                        {
                            if (uciona.IdUcionice.Equals(termin.UcionicaId))
                            {
                                termin.Ucionica = uciona;
                            }
                        }
                    }


                }
            }
        }

        public static void DodajTermine(Termin t)
        {
            using (SqlConnection connection = new SqlConnection(MainWindow.CONNECTION_STRING))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();

                command.CommandText =
                    @"INSERT INTO TERMINI(VremeZauzecaPocetak, VremeZauzecaKraj, DaniUNedelji, TipNastave, ZaduzeniPredavacId, UstanovaId, UcionicaId, Active) 
                    VALUES(@Pocetak, @Kraj, @Dan, @TipNastave, @ZaduzeniPredavacId, @UstanovaId, @UcionicaId, @Active) ";

                command.Parameters.Add(new SqlParameter("@Pocetak", t.VremeZauzecaPocetak));
                command.Parameters.Add(new SqlParameter("@Kraj", t.VremeZauzecaKraj));
                command.Parameters.Add(new SqlParameter("@Dan", t.DaniUNedelji.ToString()));
                command.Parameters.Add(new SqlParameter("@TipNastave", t.TipNastave.ToString()));
                command.Parameters.Add(new SqlParameter("@ZaduzeniPredavacId", t.ZaduzeniPredavacId));
                command.Parameters.Add(new SqlParameter("@UstanovaId", t.UstanovaId));
                command.Parameters.Add(new SqlParameter("@UcionicaId", t.UcionicaId));
                command.Parameters.Add(new SqlParameter("@Active", t.Active));


                command.ExecuteNonQuery();
            }
        }

        public static void IzmeniTermin(Termin t)
        {
            using (SqlConnection conn = new SqlConnection(MainWindow.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();

                command.CommandText =
                    @"UPDATE TERMINI SET VremeZauzecaPocetak=@Pocetak, VremeZauzecaKraj=@Kraj, DaniUNedelji=@Dan, TipNastave=@TipNastave, 
                        ZaduzeniPredavacId=@ZaduzeniPredavacId, UstanovaId=@UstanovaId, UcionicaId=@UcionicaId WHERE IdTermin=@IdTermin";

                command.Parameters.Add(new SqlParameter("@Pocetak", t.VremeZauzecaPocetak));
                command.Parameters.Add(new SqlParameter("@Kraj", t.VremeZauzecaKraj));
                command.Parameters.Add(new SqlParameter("@Dan", t.DaniUNedelji.ToString()));
                command.Parameters.Add(new SqlParameter("@TipNastave", t.TipNastave.ToString()));
                command.Parameters.Add(new SqlParameter("@ZaduzeniPredavacId", t.ZaduzeniPredavacId));
                command.Parameters.Add(new SqlParameter("@UstanovaId", t.UstanovaId));
                command.Parameters.Add(new SqlParameter("@UcionicaId", t.UcionicaId));
                command.Parameters.Add(new SqlParameter("@Active", t.Active));
                command.Parameters.Add(new SqlParameter("IdTermin", t.IdTermin));

                command.ExecuteNonQuery();
            }
        }

        public static void IzbrisiTermin(Termin t)
        {
            using (SqlConnection connection = new SqlConnection(MainWindow.CONNECTION_STRING))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();

                command.CommandText = @"UPDATE TERMINI SET Active=0 WHERE IdTermin=@IdTermin";

                command.Parameters.Add(new SqlParameter("@Active", t.Active));
                command.Parameters.Add(new SqlParameter("@IdTermin", t.IdTermin));


                command.ExecuteNonQuery();
            }
        }

        public static Termin PretraziPoSifri(int id)
        {
            foreach (var termin in listaTermina)
            {
                if (termin.IdTermin.Equals(id))
                    return termin;
            }

            return null;
        }

        public static Termin PretraziPoIdu(int Id)
        {
            foreach (var termin in listaTermina)
            {
                if (termin.IdTermin.Equals(Id))
                    return termin;
            }

            return null;
        }

        public static TimeSpan IzracunajTrajanje(DateTime a, DateTime b)
        {
            TimeSpan vremeTrajanjaTermina = a - b;
            return vremeTrajanjaTermina;
        }
    }
}
