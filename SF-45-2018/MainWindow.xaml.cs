using SF_45_2018.AdminProzori;
using SF_45_2018.Entiteti;
using SF_45_2018.Logika;
using SF_45_2018.ZaOtklanjanje;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SF_45_2018
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        

        public static string CONNECTION_STRING =
        @"Data Source = (localdb)\MSSQLLocalDB;Initial Catalog = ustanove; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public MainWindow()
        {
            InitializeComponent();
            OsveziProzor();

        }

        private void OsveziProzor()
        {
            
            PodaciUstanova.UcitajUstanove();
            PodaciUcionica.UcitajUcionice();
            PodaciUcionica.DodajUcioniceOdredjenojUstanovi();
            PodaciKorisnik.UcitajKorisnike();
            PodaciTermin.UcitajTermine();
            PodaciRaspored.UcitajRaspored();
            comboBox.ItemsSource = new List<ETipKorisnika>() { ETipKorisnika.ASISTENT, ETipKorisnika.PROFESOR, ETipKorisnika.ADMIN };
        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {



            if (!(String.IsNullOrEmpty(textBoxKorIme.Text)) && !(String.IsNullOrEmpty(passwordBox.Password)) &&  comboBox.SelectedIndex >=0 )
            {

                bool logovanjeUspesno = PodaciKorisnik.ValidirajKorisnika(textBoxKorIme.Text, passwordBox.Password,(ETipKorisnika)comboBox.SelectedItem);

                if (logovanjeUspesno == true)
                {
                    Korisnik kor = PodaciKorisnik.PretraziPoKorImenu(textBoxKorIme.Text);
                    
                    if (comboBox.SelectedItem.Equals(ETipKorisnika.ADMIN))
                    {
                        PodaciKorisnik.AktivniKorisnik = new Administrator();
                        PodaciKorisnik.AktivniKorisnik = kor as Administrator;
                        MessageBox.Show("Admin logovanje uspesno", "Uspeh");
                        ProzorIzbor prozorIzbor = new ProzorIzbor();
                        prozorIzbor.ShowDialog();

                    }
                    else if (comboBox.SelectedItem.Equals(ETipKorisnika.PROFESOR))
                    {

                        PodaciKorisnik.AktivniKorisnik = new Profesor();
                        PodaciKorisnik.AktivniKorisnik = kor as Profesor;
                        MessageBox.Show("Profesor logovanje uspesno", "Uspeh");
                        ProfesorAsistentProzor profesorAsistentProzor = new ProfesorAsistentProzor(kor);
                        profesorAsistentProzor.ShowDialog();

                    }
                    else if (comboBox.SelectedItem.Equals(ETipKorisnika.ASISTENT))
                    {
                        PodaciKorisnik.AktivniKorisnik = new Asistent();
                        PodaciKorisnik.AktivniKorisnik = kor as Asistent;
                        MessageBox.Show("Asistent logovanje Uspesno", "Uspeh");
                        // AsistentProzor asistprozor = new AsistentProzor(kor);
                        ProfesorAsistentProzor asistprozor = new ProfesorAsistentProzor(kor);
                        asistprozor.ShowDialog();

                    }
                }

                else
                {
                    MessageBox.Show("Uneli ste pogresne podatke", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

            }
            else if (String.IsNullOrEmpty(textBoxKorIme.Text) || String.IsNullOrWhiteSpace(passwordBox.Password))
            {
                MessageBox.Show("Niste popunili sva polja", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void labelNastaviKaoGost_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            NastaviKaoGost nastaviKaoGost = new NastaviKaoGost();
            nastaviKaoGost.ShowDialog();
        }

        private void textBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NastaviKaoGost nastaviKaoGost = new NastaviKaoGost();
            nastaviKaoGost.ShowDialog();
        }
    }
}
