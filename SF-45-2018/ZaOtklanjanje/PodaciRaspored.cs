using SF_45_2018.Entiteti;
using SF_45_2018.Logika;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF_45_2018.ZaOtklanjanje
{
    class PodaciRaspored
    {
        public static ObservableCollection<Raspored> listaRasporeda;



        public static void UcitajRaspored()
        {
            listaRasporeda = new ObservableCollection<Raspored>();

            using (SqlConnection conn = new SqlConnection(MainWindow.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"select * from raspored";
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = command;

                da.Fill(ds, "Raspored");

                foreach (DataRow row in ds.Tables["Raspored"].Rows)
                {
                    int rasporedId = (int)row["IdRaspored"];
                    int ustanovaId = (int)row["UstanovaId"];
                    int terminId = (int)row["TerminId"];
                    int ucionicaId = (int)row["UcionicaId"];
                    int korisnikId = (int)row["KorisnikId"];
                    bool active = (bool)row["Active"];

                    Raspored raspored = new Raspored(rasporedId, ustanovaId, terminId, ucionicaId, korisnikId);
                    listaRasporeda.Add(raspored);

                    foreach (var ras in listaRasporeda)
                    {
                        if (ras.OdabranaUstanovaId.Equals(ustanovaId))
                        {
                            Ustanova ustanova = PodaciUstanova.PretraziPoSifri(ras.OdabranaUstanovaId);
                            ras.OdabranaUstanova = ustanova;

                        }

                        if (ras.OdabranaUcionicaId.Equals(ucionicaId))
                        {
                            Ucionica ucionica = PodaciUcionica.PretraziPoIdu(ras.OdabranaUcionicaId);
                            ras.OdabranaUcionica = ucionica;

                        }

                        if (ras.PredavacId.Equals(korisnikId))
                        {
                            Korisnik korisnik = PodaciKorisnik.PretraziPoIDu(ras.PredavacId);
                            ras.Predavac = korisnik;
                        }

                        if (ras.ZakazaniTreminId.Equals(terminId))
                        {
                            Termin termin = PodaciTermin.PretraziPoIdu(ras.ZakazaniTreminId);
                            ras.ZakazaniTremin = termin;
                        }
                    }
                }
            }

        }

        /*public static void DodajRaspored(Raspored r)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();

                command.CommandText = "@INSERT INTO RASPORED(UstanovaId, TerminId, UcionicaId, KorisnikId, Active " +
                                      "VALUES())";

                command.Parameters.Add(new SqlParameter("@Ustanova" , r.OdabranaUstanovaId));
                command.Parameters.Add(new SqlParameter("@Termin" , r.ZakazaniTreminId));
                command.Parameters.Add(new SqlParameter("@Ucionica", r.OdabranaUcionicaId));
                command.Parameters.Add(new SqlParameter("Korisnik", r.PredavacId));
                command.Parameters.Add(new SqlParameter("Active", r.Active));

                command.ExecuteNonQuery();


            }
        } */
    }
}
