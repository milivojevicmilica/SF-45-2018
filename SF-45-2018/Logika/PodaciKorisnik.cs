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
    class PodaciKorisnik
    {
        public static Korisnik AktivniKorisnik;

        public static ObservableCollection<Korisnik> listaKorisnika { get; set; }

        public static void UcitajKorisnike()
        {
            listaKorisnika = new ObservableCollection<Korisnik>();

            using (SqlConnection conn = new SqlConnection(MainWindow.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"select * from korisnici";
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = command;

                da.Fill(ds, "Korisnici");

                foreach (DataRow row in ds.Tables["Korisnici"].Rows)
                {
                    int id = (int)row["IdKorisnik"];
                    string ime = (string)row["Ime"];
                    string prezime = (string)row["prezime"];
                    string korIme = (string)row["KorisnickoIme"];
                    string email = (string)row["Email"];
                    string lozinka = (string)row["Lozinka"];
                    bool active = (bool)row["Active"];

                    if (row["TipKorisnika"].Equals(ETipKorisnika.ADMIN.ToString()))
                    {
                        Administrator admin = new Administrator(ime, prezime, korIme, email, lozinka);
                        admin.Active = active;
                        admin.Id = id;
                        listaKorisnika.Add(admin);

                    }

                    if (row["TipKorisnika"].Equals((ETipKorisnika.PROFESOR.ToString())))
                    {
                        Profesor profesor = new Profesor(ime, prezime, korIme, email, lozinka);
                        profesor.Active = active;
                        profesor.Id = id;


                        listaKorisnika.Add(profesor);

                    }
                    else if (row["TipKorisnika"].Equals(ETipKorisnika.ASISTENT.ToString()))
                    {
                        Asistent asistent = new Asistent(ime, prezime, korIme, email, lozinka);
                        asistent.Active = active;
                        asistent.Id = id;
                        listaKorisnika.Add(asistent);


                    }
                }
            }

            using (SqlConnection conn = new SqlConnection(MainWindow.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand commandA = conn.CreateCommand();
                commandA.CommandText = @"select * from Asistenti";
                DataSet dsA = new DataSet();
                SqlDataAdapter daA = new SqlDataAdapter();
                daA.SelectCommand = commandA;

                daA.Fill(dsA, "Asistenti");

                foreach (DataRow row in dsA.Tables["Asistenti"].Rows)
                {
                     int idAsistenta = (int)row["IdAsistent"];
                    int idProfesora = (int)row["DodeljeniProfesor"];
                    int idUstanova = (int)row["UstanovaId"];

                    foreach (var asis in listaKorisnika)
                    {
                        if (idAsistenta.Equals(asis.Id))
                        {
                            Asistent asistent = asis as Asistent;
                            asistent.IdDodeljenogProfesora = idProfesora;
                            asistent.UstanovaZaposlenjaId = idUstanova;

                            Profesor dodeljeniProfa = PretraziPoIDu(idProfesora) as Profesor;
                            asistent.DodeljeniProfesor = dodeljeniProfa;

                            Ustanova ustanovaZaposlenja = PodaciUstanova.PretraziPoSifri(idUstanova);
                            asistent.UstanovaZaposlenja = ustanovaZaposlenja;
                        }
                    }

                    foreach (var profa in listaKorisnika)
                    {
                        if (idProfesora.Equals(profa.Id))
                        {

                            Profesor profesor = profa as Profesor;

                            foreach (var asistent in listaKorisnika)
                            {
                                if (idAsistenta.Equals(asistent.Id))
                                {
                                    profesor.ListaAsistenata.Add(asistent as Asistent);
                                }

                            }


                        }
                    }
                }


                foreach (var ustanova in PodaciUstanova.listaUstanova)
                {

                    foreach (var korisnik in listaKorisnika)
                    {
                        if (korisnik.TipKorisnika.ToString().ToLower() ==
                            ETipKorisnika.ASISTENT.ToString().ToLower())
                        {

                            Asistent a = korisnik as Asistent;

                            if (a.UstanovaZaposlenjaId.Equals(ustanova.SifraUstanove))
                            {
                                ustanova.ZaposenaLica.Add(a);
                            }
                        }
                    }
                }


            }

            using (SqlConnection conn = new SqlConnection(MainWindow.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"SELECT * FROM PROFESORI";

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = command;
                da.Fill(ds, "Profesori");

                foreach (DataRow row in ds.Tables["Profesori"].Rows)
                {
                    int idProfesor = (int)row["IdProfesor"];
                    int ustanovaId = (int)row["UstanovaId"];

                    foreach (var kor in listaKorisnika)
                    {
                        if (idProfesor.Equals(kor.Id))
                        {
                            Profesor profesor = kor as Profesor;

                            profesor.UstanovaZaposlenjaId = ustanovaId;

                            Ustanova ustanovaZaposlenja = PodaciUstanova.PretraziPoSifri(ustanovaId);
                            profesor.UstanovaZaposlenja = ustanovaZaposlenja;
                        }
                    }
                }
                foreach (var ustanova in PodaciUstanova.listaUstanova)
                {

                    foreach (var korisnik in listaKorisnika)
                    {
                        if (korisnik.TipKorisnika.ToString().ToLower() ==
                            ETipKorisnika.PROFESOR.ToString().ToLower())
                        {

                            Profesor p = korisnik as Profesor;

                            if (p.UstanovaZaposlenjaId.Equals(ustanova.SifraUstanove))
                            {
                                ustanova.ZaposenaLica.Add(p);
                            }
                        }
                    }
                }


            }
        }

        public static void DodajKorisnika(Korisnik k)
        {
            using (SqlConnection connection = new SqlConnection(MainWindow.CONNECTION_STRING))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText =
                    @"INSERT INTO KORISNICI(Ime, Prezime, KorisnickoIme, Email, TipKorisnika, Lozinka, Active) VALUES 
(@Ime, @Prezime, @KorisnickoIme, @Email, @TipKOrisnika, @Lozinka, @Active)";

                command.Parameters.Add(new SqlParameter("@Ime", k.Ime));
                command.Parameters.Add(new SqlParameter("@Prezime", k.Prezime));
                command.Parameters.Add(new SqlParameter("@KorisnickoIme", k.KorisnickoIme));
                command.Parameters.Add(new SqlParameter("@Email", k.Email));
                command.Parameters.Add(new SqlParameter("@TipKorisnika", k.TipKorisnika.ToString()));
                command.Parameters.Add(new SqlParameter("@Lozinka", k.Lozinka));
                command.Parameters.Add(new SqlParameter("@Active", k.Active));

                command.ExecuteNonQuery();
            }



        }

        public static void DodajDodatnaSvojstvaZaZaposlene(Korisnik k)
        {

            using (SqlConnection connection = new SqlConnection(MainWindow.CONNECTION_STRING))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();

                if (k.TipKorisnika.ToString().ToLower() == ETipKorisnika.ASISTENT.ToString().ToLower())
                {

                    command.CommandText =
                        @"INSERT INTO ASISTENTI(IdAsistent, DodeljeniProfesor, UstanovaId) VALUES (@Id, @DodeljeniProfesorId, @Ustanova)";

                    Asistent a = k as Asistent;

                    command.Parameters.Add(new SqlParameter("@DodeljeniProfesorId", a.IdDodeljenogProfesora));
                    command.Parameters.Add(new SqlParameter("@Id", a.Id));
                    command.Parameters.Add(new SqlParameter("@Ustanova", a.UstanovaZaposlenjaId));

                    command.ExecuteNonQuery();

                }

                if (k.TipKorisnika.ToString().ToLower() == ETipKorisnika.PROFESOR.ToString().ToLower())
                {
                    command.CommandText = @"INSERT INTO PROFESORI(IdProfesor, UstanovaId) VALUES (@Id, @Ustanova)";

                    Profesor p = k as Profesor;

                    command.Parameters.Add(new SqlParameter("@Id", p.Id));
                    command.Parameters.Add(new SqlParameter("@Ustanova", p.UstanovaZaposlenjaId));

                    command.ExecuteNonQuery();
                }

                if (k.TipKorisnika.ToString().ToLower() == ETipKorisnika.ADMIN.ToString().ToLower())
                {
                    command.CommandText = @"INSERT INTO ADMINISTRATORI(IdAdmin) VALUES (@Id)";

                    Administrator a = k as Administrator;

                    command.Parameters.Add(new SqlParameter("@Id", a.Id));

                    command.ExecuteNonQuery();
                }


            }
        }


        public static void IzmeniKorisnika(Korisnik k)
        {
            using (SqlConnection connection = new SqlConnection(MainWindow.CONNECTION_STRING))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();

                command.CommandText = @"UPDATE KORISNICI SET Ime=@Ime, Prezime=@PRezime,
KorisnickoIme=@KorisnickoIme, Email=@Email, TipKorisnika=@TipKorisnika, Lozinka=@Lozinka, Active=@Active WHERE KorisnickoIme=@KorisnickoIme";

                command.Parameters.Add(new SqlParameter("@Ime", k.Ime));
                command.Parameters.Add(new SqlParameter("@Prezime", k.Prezime));
                command.Parameters.Add(new SqlParameter("@KorisnickoIme", k.KorisnickoIme));
                command.Parameters.Add(new SqlParameter("@Email", k.Email));
                command.Parameters.Add(new SqlParameter("@TipKorisnika", k.TipKorisnika.ToString()));
                command.Parameters.Add(new SqlParameter("@Lozinka", k.Lozinka));
                command.Parameters.Add(new SqlParameter("@Active", k.Active));

                command.ExecuteNonQuery();

            }
        }

        public static void IzbrisiKorisnika(Korisnik k)
        {
            using (SqlConnection connection = new SqlConnection(MainWindow.CONNECTION_STRING))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();

                command.CommandText = @"UPDATE KORISNICI SET Active=0 WHERE KorisnickoIme=@KorisnickoIme";

                command.Parameters.Add(new SqlParameter("@Active", k.Active));
                command.Parameters.Add(new SqlParameter("@KorisnickoIme", k.KorisnickoIme));

                command.ExecuteNonQuery();
            }
        }


        public static Korisnik PretraziPoKorImenu(string korIme)
        {

            foreach (Korisnik kor in listaKorisnika)
            {
                if (kor.KorisnickoIme.Equals(korIme))
                    return kor;
            }

            return null;
        }


        public static bool ValidirajKorisnika(string username, string password)
        {
            bool result = false;
            

            foreach (Korisnik kor in listaKorisnika)
            {
                if (kor.KorisnickoIme.Equals(username) && kor.Lozinka.Equals(password) )
                {
                    result = true;
                   
                }
            }

            return result;
        }

        public static Korisnik PretraziPoIDu(int Id)
        {
            foreach (var korisnik in listaKorisnika)
            {
                if (Id.Equals(korisnik.Id))
                {
                    return korisnik;
                }

            }
            return null;
        }
    }
}
