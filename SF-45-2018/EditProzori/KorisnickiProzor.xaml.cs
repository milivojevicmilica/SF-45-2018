using SF_45_2018.Entiteti;
using SF_45_2018.Logika;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace SF_45_2018.EditProzori
{
    /// <summary>
    /// Interaction logic for KorisnickiProzor.xaml
    /// </summary>
    public partial class KorisnickiProzor : Window
    {
        enum Status { ADD, EDIT }
        private Status _status;
        private Korisnik selectedUser;
        ICollectionView viewUstanove;
        
        private Ustanova ustanova;
        private List<Profesor> listaProfesora;

        public KorisnickiProzor(Korisnik korisnik)
        {
            InitializeComponent();
            OsveziPrikaz();
           

            if (korisnik is Profesor)
            {
                comboBox.ItemsSource = new List<ETipKorisnika>() { ETipKorisnika.PROFESOR };
                //vidljivost za dodeljenog profesora
                comboBoxProfa.Visibility = Visibility.Hidden;
                //combobox za dodeljenog profesora labela
                label1.Visibility = Visibility.Hidden;
                
            }
            else if (korisnik is Administrator)
            {
                comboBox.ItemsSource = new List<ETipKorisnika>() { ETipKorisnika.ADMIN };
                //mesto zaposlenja sakrivamo labelu i listu celu
                label.Visibility = Visibility.Hidden;
                listBox.Visibility = Visibility.Hidden;
                //combobox za dodeljenog profesora i labela
                comboBoxProfa.Visibility = Visibility.Hidden;
                label1.Visibility = Visibility.Hidden;
            }
            else if (korisnik is Asistent)
            {
                comboBox.ItemsSource = new List<ETipKorisnika>() { ETipKorisnika.ASISTENT };

                listaProfesora = new List<Profesor>();

                foreach (var kor in PodaciKorisnik.listaKorisnika)
                {
                    if (kor.TipKorisnika.Equals(ETipKorisnika.PROFESOR))
                    {
                        listaProfesora.Add(kor as Profesor);
                    }
                }

                comboBoxProfa.ItemsSource = listaProfesora;

            }

            if (korisnik.KorisnickoIme.Equals(""))
            {
                this._status = Status.ADD;
            }
            else
            {
                this._status = Status.EDIT;

                textBoxKorIme.IsReadOnly = true;
            }

            selectedUser = korisnik;
            this.DataContext = korisnik;

        }

        public void OsveziPrikaz()
        {
            viewUstanove = CollectionViewSource.GetDefaultView(PodaciUstanova.listaUstanova);
            listBox.ItemsSource = viewUstanove;
            


        }

        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
            Korisnik kor = PodaciKorisnik.PretraziPoKorImenu(textBoxKorIme.Text);
            if (kor != null && _status.Equals(Status.ADD))
            {
                MessageBox.Show($"Korisnik sa korisnickim imenom {kor.KorisnickoIme} vec postoji", "Upozorenje",
                    MessageBoxButton.OK);
                return;
            }

            if ((String.IsNullOrWhiteSpace(textBoxKorIme.Text) ||
                                               String.IsNullOrWhiteSpace(textBoxIme.Text) ||
                                               String.IsNullOrWhiteSpace(textBoxEmail.Text) ||
                                               String.IsNullOrWhiteSpace(textBoxPrezime.Text) ||
                                               String.IsNullOrWhiteSpace(textPassword.Text)))
            {
                MessageBox.Show("Niste popunili sva polja.", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!ValidacionaPravila.Email.regex.IsMatch(textBoxEmail.Text))
            {
                MessageBox.Show("Pogresan E-mail format.", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_status.Equals(Status.ADD))
            {

                ustanova = new Ustanova();
                ustanova = listBox.SelectedItem as Ustanova;
                

                if (comboBox.SelectedItem.Equals(ETipKorisnika.PROFESOR))
                {
                    if (listBox.SelectedIndex < 0)
                    {
                        MessageBox.Show("Niste selektovali ustanovu zaposlenja.", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    Profesor profesor = selectedUser as Profesor;
                    Ustanova ustanova = profesor.UstanovaZaposlenja;
                    profesor.UstanovaZaposlenja = ustanova;
                    PodaciKorisnik.DodajKorisnika(profesor);

                    
               
                    


                    PodaciKorisnik.listaKorisnika.Clear();
                    PodaciKorisnik.UcitajKorisnike();

                    string korIme = profesor.KorisnickoIme;


                    Profesor p = PodaciKorisnik.PretraziPoKorImenu(korIme) as Profesor;

                    int idUstanova = ustanova.SifraUstanove;
                    p.UstanovaZaposlenja = ustanova;
                    p.UstanovaZaposlenjaId = idUstanova;

                    PodaciKorisnik.DodajDodatnaSvojstvaZaZaposlene(p);

                    PodaciKorisnik.listaKorisnika.Clear();
                    PodaciKorisnik.UcitajKorisnike();

                }
                
                else if (comboBox.SelectedItem.Equals(ETipKorisnika.ADMIN))
                {
                    Administrator admin = selectedUser as Administrator;
                    PodaciKorisnik.DodajKorisnika(admin);

                    PodaciKorisnik.listaKorisnika.Clear();
                    PodaciKorisnik.UcitajKorisnike();

                    string korIme = admin.KorisnickoIme;

                    Administrator a = PodaciKorisnik.PretraziPoKorImenu(korIme) as Administrator;

                    PodaciKorisnik.DodajDodatnaSvojstvaZaZaposlene(a);

                    PodaciKorisnik.listaKorisnika.Clear();
                    PodaciKorisnik.UcitajKorisnike();
                }
                else if (comboBox.SelectedItem.Equals(ETipKorisnika.ASISTENT))

                {
                    if (comboBoxProfa.SelectedIndex < 0 || listBox.SelectedIndex < 0)
                    {
                        MessageBox.Show("Niste selektovali sva polja.", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    Asistent asistent = selectedUser as Asistent;

                    PodaciKorisnik.DodajKorisnika(asistent);

                    Profesor profesor = asistent.DodeljeniProfesor;
                    Ustanova ustanova = asistent.UstanovaZaposlenja;

                    PodaciKorisnik.listaKorisnika.Clear();
                    PodaciKorisnik.UcitajKorisnike();

                    string korIme = asistent.KorisnickoIme;


                    Asistent a = PodaciKorisnik.PretraziPoKorImenu(korIme) as Asistent;

                    int idProfesora = profesor.Id;
                    int idUstanova = ustanova.SifraUstanove;
                    a.DodeljeniProfesor = profesor;
                    a.IdDodeljenogProfesora = idProfesora;
                    a.UstanovaZaposlenja = ustanova;
                    a.UstanovaZaposlenjaId = idUstanova;

                    PodaciKorisnik.DodajDodatnaSvojstvaZaZaposlene(a);

                    PodaciKorisnik.listaKorisnika.Clear();
                    PodaciKorisnik.UcitajKorisnike();
                }
                viewUstanove.Refresh();
                

            }
            if (_status.Equals(Status.EDIT))
            {
                PodaciKorisnik.IzmeniKorisnika(selectedUser);

            }


            this.DialogResult = true;
            this.Close();
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void textBoxIme_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = App.IsTextAllowed(e.Text);
        }


        private void textBoxPrezime_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = App.IsTextAllowed(e.Text);
        }
    }
}
