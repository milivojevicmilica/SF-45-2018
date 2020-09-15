using SF_45_2018.Entiteti;
using SF_45_2018.Logika;
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
using System.Windows.Shapes;

namespace SF_45_2018.EditProzori
{
    /// <summary>
    /// Interaction logic for DodajAsistentaProzor.xaml
    /// </summary>
    public partial class DodajAsistentaProzor : Window
    {
        private Asistent selektovaniAsistent;
        enum Status { ADD, EDIT }
        private Status _status;
        public DodajAsistentaProzor(Asistent asistent)
        {
            InitializeComponent();
            OsveziProzor();


            if (asistent.KorisnickoIme.Equals(""))
            {
                this._status = Status.ADD;
            }
            else
            {
                this._status = Status.EDIT;

                textBoxKorIme.IsReadOnly = true;
            }

            selektovaniAsistent = asistent;
            DataContext = asistent;
        }

        public void OsveziProzor()
        {
            
            comboBoxTipKorisnika.ItemsSource = new List<ETipKorisnika> { ETipKorisnika.ASISTENT };
            comboBoxUstanovaZaposlenja.ItemsSource = PodaciUstanova.listaUstanova;
            
            try
            {
                
                Profesor dodeljeniProfa = PodaciKorisnik.PretraziPoKorImenu(ProfesorAsistentProzor.profesor.KorisnickoIme) as Profesor;
                if (dodeljeniProfa != null)
                {
                    comboBoxProfa.ItemsSource = new List<Profesor> { dodeljeniProfa };
                }
                else if (dodeljeniProfa == null)
                {
                    List<Profesor> listaProfesora = new List<Profesor>();

                    foreach (var kor in PodaciKorisnik.listaKorisnika)
                    {
                        if (kor.TipKorisnika.Equals(ETipKorisnika.PROFESOR))
                        {
                            listaProfesora.Add(kor as Profesor);
                        }
                    }

                    comboBoxProfa.ItemsSource = listaProfesora;
                    comboBoxProfa.SelectedIndex = 0;
                }


            }
            catch (NullReferenceException)
            {

                
            }
        }

        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
            Korisnik kor = PodaciKorisnik.PretraziPoKorImenu(textBoxKorIme.Text);
            if (kor != null && _status.Equals(Status.ADD))
            {
                MessageBox.Show($"Korisnik sa korisnickim imenom {kor.KorisnickoIme} vec postoji", "Upozorenje", MessageBoxButton.OK);
                return;
            }

            if (String.IsNullOrWhiteSpace(textBoxKorIme.Text) ||
                                               String.IsNullOrWhiteSpace(textBoxIme.Text) ||
                                               String.IsNullOrWhiteSpace(textBoxEmail.Text) ||
                                               String.IsNullOrWhiteSpace(textBoxPrezime.Text) ||
                                               String.IsNullOrWhiteSpace(textPassword.Text) || comboBoxProfa.SelectedIndex < 0 || comboBoxUstanovaZaposlenja.SelectedIndex < 0 || comboBoxTipKorisnika.SelectedIndex < 0)
            {
                MessageBox.Show("Niste popunili sva polja!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!ValidacionaPravila.Email.regex.IsMatch(textBoxEmail.Text))
            {
                MessageBox.Show("Pogresan E-mail format.", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            else if (_status.Equals(Status.ADD))
            {
                Asistent asistent = selektovaniAsistent as Asistent;

                PodaciKorisnik.DodajKorisnika(selektovaniAsistent);

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
                profesor.ListaAsistenata.Clear();
                PodaciKorisnik.UcitajKorisnike();

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
