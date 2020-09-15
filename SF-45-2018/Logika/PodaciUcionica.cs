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
    class PodaciUcionica
    {

        public static ObservableCollection<Ucionica> listaUcionica;

      


        public static void UcitajUcionice()
        {
            listaUcionica = new ObservableCollection<Ucionica>();

            using (SqlConnection conn = new SqlConnection(MainWindow.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"select * from ucionice";
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = command;

                da.Fill(ds, "Ucionice");

                foreach (DataRow row in ds.Tables["Ucionice"].Rows)
                {
                    int id = (int)row["IdUcionica"];
                    string brUcionice = (string)row["BrojUcionice"];
                    int brojMesta = (int)row["BrojMesta"];
                    string tipUcionice = (string)row["TipUcionice"];
                    int idUstanove = (int)row["UstanovaGdeSeNalaziId"];
                    bool active = (bool)row["Active"];

                    if (tipUcionice.Equals("SARACUNARIMA"))
                    {
                        Ucionica ucionica = new Ucionica(id, brUcionice, brojMesta,
                            ETipUcionice.SARACUNARIMA);
                        ucionica.IdUcionice = id;
                        ucionica.UstanovaGdeSeNalaziId = idUstanove;


                        listaUcionica.Add(ucionica);

                    }
                    else if (tipUcionice.Equals("BEZRACUNARA"))
                    {
                        Ucionica ucionica = new Ucionica(id, brUcionice, brojMesta,
                            ETipUcionice.BEZRACUNARA);
                        ucionica.IdUcionice = id;
                        ucionica.UstanovaGdeSeNalaziId = idUstanove;
                        listaUcionica.Add(ucionica);

                    }

                }


                foreach (var ustanova in PodaciUstanova.listaUstanova)
                {
                    foreach (var ucionica in listaUcionica)
                    {
                        if (ucionica.UstanovaGdeSeNalaziId.Equals(ustanova.SifraUstanove))
                        {
                            ucionica.UstanovaGdeSeNalazi = ustanova;
                            ucionica.UstanovaGdeSeNalaziId = ustanova.SifraUstanove;
                        }
                    }
                }

            }
        }

        public static void DodajUcioniceOdredjenojUstanovi()
        {
            foreach (var ustanova in PodaciUstanova.listaUstanova)
            {
                foreach (var ucionica in listaUcionica)
                {
                    if (ucionica.UstanovaGdeSeNalaziId.Equals(ustanova.SifraUstanove))
                    {
                        ustanova.ListaUcionica.Add(ucionica);

                    }

                }

            }
        }


        public static void DodajUcionicu(Ucionica u)
        {
            using (SqlConnection connection = new SqlConnection(MainWindow.CONNECTION_STRING))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();

                command.CommandText =
                    @"INSERT INTO UCIONICE(BrojUcionice,BrojMesta,TipUcionice,UstanovaGdeSeNalaziId,Active)
                    VALUES( @BrojUcionice, @BrojMesta, @TipUcionice, @UstanovaId, @Active)";

                // command.Parameters.Add(new SqlParameter("@IdUcionica", u.IdUcionice));
                command.Parameters.Add(new SqlParameter("@BrojUcionice", u.BrojUcionice));
                command.Parameters.Add(new SqlParameter("@BrojMesta", u.BrojMesta));
                command.Parameters.Add(new SqlParameter("@TipUcionice", u.TipUcionice.ToString()));
                command.Parameters.Add(new SqlParameter("@UstanovaId", u.UstanovaGdeSeNalaziId));
                command.Parameters.Add(new SqlParameter("@Active", u.Active));
                command.Parameters.Add(new SqlParameter("@Sifra", u.IdUcionice));

                command.ExecuteNonQuery();

            }
        }

        public static void IzmeniUcionicu(Ucionica u)
        {
            using (SqlConnection connection = new SqlConnection(MainWindow.CONNECTION_STRING))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();

                command.CommandText =
                    @"UPDATE UCIONICE SET BrojUcionice=@BrojUcionice, BrojMesta=@BrojMesta, TipUcionice=@TipUcionice, 
            UstanovaGdeSeNalaziId=@UstanovaId, Active=@Active WHERE IdUcionica=@IdUcionice";

                command.Parameters.Add(new SqlParameter("@IdUcionice", u.IdUcionice));
                command.Parameters.Add(new SqlParameter("@BrojUcionice", u.BrojUcionice));
                command.Parameters.Add(new SqlParameter("@BrojMesta", u.BrojMesta));
                command.Parameters.Add(new SqlParameter("@TipUcionice", u.TipUcionice.ToString()));
                command.Parameters.Add(new SqlParameter("@UstanovaId", u.UstanovaGdeSeNalaziId));
                command.Parameters.Add(new SqlParameter("@Active", u.Active));
                command.Parameters.Add(new SqlParameter("@Sifra", u.IdUcionice));

                command.ExecuteNonQuery();
            }

        }

        public static void IzbrisiUcionicu(Ucionica u)
        {
            using (SqlConnection connection = new SqlConnection(MainWindow.CONNECTION_STRING))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();

                command.CommandText = @"UPDATE UCIONICE SET Active=0 WHERE IdUcionica=@IdUcionica";
                ;
                command.Parameters.Add(new SqlParameter("@IdUcionica", u.IdUcionice));

                command.ExecuteNonQuery();
            }
        }

        public static Ucionica PretraziPoSifri(string sifra)
        {
            foreach (var ucionica in listaUcionica)
            {
                if (sifra.Equals(ucionica.IdUcionice))
                    return ucionica;
            }

            return null;
        }

        public static Ucionica PretraziPoIdu(int id)
        {
            foreach (var ucionica in listaUcionica)
            {
                if (id.Equals(ucionica.IdUcionice))
                    return ucionica;
            }

            return null;
        }



    }
}
