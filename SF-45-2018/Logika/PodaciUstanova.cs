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
    class PodaciUstanova
    {
        public static ObservableCollection<Ustanova> listaUstanova { get; set; }

        

        public static void UcitajUstanove()
        {
            listaUstanova = new ObservableCollection<Ustanova>();

            using (SqlConnection conn = new SqlConnection(MainWindow.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"select * from ustanove";
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = command;
                da.Fill(ds, "Ustanove");

                foreach (DataRow row in ds.Tables["Ustanove"].Rows)
                {
                    int id = (int)row["IdUstanova"];
                    string naziv = (string)row["Naziv"];
                    string lokacija = (string)row["Lokacija"];
                    bool active = (bool)row["Active"];
                    int maksBrUcionica = (int)row["MaksimalanBrojUcionica"];


                    Ustanova ustanova = new Ustanova(id, naziv, lokacija);
                    ustanova.MaksimalanBrojUcionica = maksBrUcionica;
                    ustanova.Active = active;
                    listaUstanova.Add(ustanova);


                }
            }
        }

        public static void DodajUstanovu(Ustanova u)
        {
            using (SqlConnection conn = new SqlConnection(MainWindow.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();

                command.CommandText =
                    @"INSERT INTO USTANOVE(Naziv, Lokacija, Active, MaksimalanBrojUcionica) VALUES(@Naziv, @Lokacija, @Active, @MaksBrUcionica)";

                command.Parameters.Add(new SqlParameter("@IdUstanova", u.SifraUstanove));
                command.Parameters.Add(new SqlParameter("@Naziv", u.Naziv));
                command.Parameters.Add(new SqlParameter("@Lokacija", u.Lokacija));
                command.Parameters.Add(new SqlParameter("@Active", u.Active));
                command.Parameters.Add(new SqlParameter("@MaksBrUcionica", u.MaksimalanBrojUcionica));

                command.ExecuteNonQuery();
            }
        }

        public static void IzmeniUstanovu(Ustanova u)
        {
            using (SqlConnection conn = new SqlConnection(MainWindow.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();

                command.CommandText =
                    @"UPDATE USTANOVE SET Naziv=@Naziv, Lokacija=@Lokacija, Active=@Active, MaksimalanBrojUcionica=@MaksBrUcionica WHERE IdUstanova = @IdUstanova";

                command.Parameters.Add(new SqlParameter("@IdUstanova", u.SifraUstanove));
                command.Parameters.Add(new SqlParameter("@Naziv", u.Naziv));
                command.Parameters.Add(new SqlParameter("@Lokacija", u.Lokacija));
                command.Parameters.Add(new SqlParameter("@Active", u.Active));
                command.Parameters.Add(new SqlParameter("@MaksBrUcionica", u.MaksimalanBrojUcionica));

                command.ExecuteNonQuery();
            }
        }

        public static void IzbrisiUstanovu(Ustanova u)
        {
            using (SqlConnection connection = new SqlConnection(MainWindow.CONNECTION_STRING))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();

                command.CommandText = @"UPDATE USTANOVE SET Active=0 WHERE IdUstanova=@IdUstanova";

                command.Parameters.Add(new SqlParameter("@IdUstanova", u.SifraUstanove));

                command.ExecuteNonQuery();
            }
        }

        public static Ustanova PretraziPoSifri(int sifra)
        {
            foreach (var ustanova in listaUstanova)
            {
                if (sifra.Equals(ustanova.SifraUstanove))
                    return ustanova;
            }
            return null;
        }
        public static Ustanova PretraziPoLokaciji(string lokacija)
        {
            foreach(var ustanova in listaUstanova)
            {
                if (ustanova.Lokacija.Equals(lokacija))
                    return ustanova;
            }
            return null;
        }
        public static Ustanova PretraziPoNazivu(string naziv)
        {
            foreach (var ustanova in listaUstanova)
            {
                if (ustanova.Naziv.Equals(naziv))
                    return ustanova;
            }
            return null;
        }


    }
    
}
