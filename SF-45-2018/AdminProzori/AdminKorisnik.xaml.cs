using SF_45_2018.EditProzori;
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

namespace SF_45_2018.AdminProzori
{
    /// <summary>
    /// Interaction logic for AdminKorisnik.xaml
    /// </summary>
    public partial class AdminKorisnik : Window
    {
        ICollectionView viewKorisnici;
        string searchFilter = "Active";
        public AdminKorisnik()
        {
            InitializeComponent();
            comboBoxTip.ItemsSource = new List<ETipKorisnika>
                {ETipKorisnika.ADMIN, ETipKorisnika.ASISTENT, ETipKorisnika.PROFESOR};
            dataGridKorisnici.IsReadOnly = true;

            OsveziProzor();
        }
        private bool CustomFilter(object obj)
        {
            Korisnik korisnik = obj as Korisnik;
            if (searchFilter.Equals("SEARCH") && comboBoxTip.SelectedIndex > -1)
                return ((korisnik.Active &&
                         korisnik.KorisnickoIme.ToLower().Contains(textBoxSearchKorIme.Text.ToLower())) &&
                        korisnik.Prezime.ToLower().Contains(textBoxSearchPrezime.Text.ToLower()) &&
                        korisnik.Ime.ToLower().Contains(textBoxSearchIme.Text.ToLower()) &&
                        korisnik.Email.ToLower().Contains(textBoxSearchEmail.Text.ToLower()) &&
                        korisnik.TipKorisnika.ToString().ToLower().Equals(comboBoxTip.SelectedItem.ToString().ToLower()));
            else if (searchFilter.Equals("SEARCH"))
                return ((korisnik.Active &&
                  korisnik.KorisnickoIme.ToLower().Contains(textBoxSearchKorIme.Text.ToLower())) &&
                        korisnik.Ime.ToLower().Contains(textBoxSearchIme.Text.ToLower()) &&
                 korisnik.Prezime.ToLower().Contains(textBoxSearchPrezime.Text.ToLower()) &&
                 korisnik.Email.ToLower().Contains(textBoxSearchEmail.Text.ToLower()));
            else
                return korisnik.Active;
        }

        public void OsveziProzor()
        {


            viewKorisnici = CollectionViewSource.GetDefaultView(PodaciKorisnik.listaKorisnika);
            dataGridKorisnici.ItemsSource = viewKorisnici;
            viewKorisnici.Filter = CustomFilter;
         

        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (radioButtonAdmin.IsChecked == false && radioButtonAsistent.IsChecked == false && radioButtonProfesor.IsChecked == false)
                MessageBox.Show("Selektujte tip za korisnika koga zelite dodati", "Greska", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            if (radioButtonAdmin.IsChecked == true)
            {
                Administrator admin = new Administrator();

                KorisnickiProzor korProz = new KorisnickiProzor(admin);
                if (korProz.ShowDialog() == true)
                {
                    OsveziProzor();
                    
                    viewKorisnici.Refresh();
                    searchFilter = "Active";
                }
            }
            else if (radioButtonAsistent.IsChecked == true)
            {
                Asistent asistent = new Asistent();

                KorisnickiProzor korProz = new KorisnickiProzor(asistent);
                if (korProz.ShowDialog() == true)
                {
                    OsveziProzor();
                    viewKorisnici.Refresh();
                    searchFilter = "Active";
                }
            }
            else if (radioButtonProfesor.IsChecked == true)
            {
                Profesor profesor = new Profesor();
                KorisnickiProzor korProz = new KorisnickiProzor(profesor);
                if (korProz.ShowDialog() == true)
                {
                    OsveziProzor();
                    viewKorisnici.Refresh();
                    searchFilter = "Active";
                }
            }
        }

        private void buttonEdit_Click(object sender, RoutedEventArgs e)
        {

            Korisnik korisnik = dataGridKorisnici.SelectedItem as Korisnik;
            

            if (korisnik == null)
            {
                MessageBox.Show("Ni jedno polje nije obelezeno", "Upozorenje", MessageBoxButton.OK);
            }
            else
            {
                Korisnik stariKorisnik = korisnik.Clone();

                Korisnik selektovaniKor = dataGridKorisnici.SelectedItem as Korisnik;

                KorisnickiProzor kor = new KorisnickiProzor(korisnik);
                if (kor.ShowDialog() == false)
                {
                    int indeks = PodaciKorisnik.listaKorisnika.IndexOf(PodaciKorisnik.listaKorisnika
                        .Where(u => u.KorisnickoIme.Equals(stariKorisnik.KorisnickoIme)).FirstOrDefault());

                    stariKorisnik.Active = true;
                    PodaciKorisnik.listaKorisnika[indeks] = stariKorisnik;

                    if (stariKorisnik.TipKorisnika.ToString().ToLower() == ETipKorisnika.ASISTENT.ToString().ToLower())
                    {
                        Asistent stariKorisnikAs = stariKorisnik as Asistent;
                        Asistent asistent = korisnik as Asistent;
                        stariKorisnikAs.DodeljeniProfesor = asistent.DodeljeniProfesor;
                        stariKorisnikAs.IdDodeljenogProfesora = asistent.IdDodeljenogProfesora;
                        stariKorisnikAs.UstanovaZaposlenjaId = asistent.UstanovaZaposlenjaId;
                        stariKorisnikAs.UstanovaZaposlenja = asistent.UstanovaZaposlenja;
                    }


                    if (stariKorisnik.TipKorisnika.ToString().ToLower() == ETipKorisnika.PROFESOR.ToString().ToLower())
                    {
                        Profesor stariKorisnikPr = stariKorisnik as Profesor;
                        Profesor profesor = korisnik as Profesor;
                        stariKorisnikPr.UstanovaZaposlenjaId = profesor.UstanovaZaposlenjaId;
                        stariKorisnikPr.UstanovaZaposlenja = profesor.UstanovaZaposlenja;
                    }


                }
            }

            viewKorisnici.Refresh();
            searchFilter = "Active";
        }
       

        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {

            searchFilter = "SEARCH";
            viewKorisnici.Refresh();

        }


        private void dataGridKorisnici_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Error") || e.PropertyName.Equals("Lozinka") || e.PropertyName.Equals("Id"))
                e.Column.Visibility = Visibility.Collapsed;
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            Korisnik korisnik = dataGridKorisnici.SelectedValue as Korisnik;
            if (korisnik != null)
            {
                Korisnik IzbrisanKorisnik = PodaciKorisnik.PretraziPoKorImenu(korisnik.KorisnickoIme);
                IzbrisanKorisnik.Active = false;
                PodaciKorisnik.IzbrisiKorisnika(korisnik);
                searchFilter = "Active";
                viewKorisnici.Refresh();
            }
            else
            {
                MessageBox.Show("Ni jedno polje nije obelezeno", "Upozorenje", MessageBoxButton.OK);
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            textBoxSearchEmail.Clear();
            textBoxSearchIme.Clear();
            textBoxSearchKorIme.Clear();
            textBoxSearchPrezime.Clear();
            comboBoxTip.SelectedIndex = -1;
            searchFilter = "SEARCH";
            viewKorisnici.Refresh();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            dataGridKorisnici.Columns[3].Header = "Korisnicko Ime";
            dataGridKorisnici.Columns[4].Header = "E-Mail";
            dataGridKorisnici.Columns[5].Header = "Tip Korisnika";
        }


        private void button_sort_Click(object sender, RoutedEventArgs e)
        {
            if (radioButtonIme.IsChecked == true)
            {
                List<Korisnik> sortiranaLista = PodaciKorisnik.listaKorisnika.OrderBy(k => k.Ime).ToList();
                viewKorisnici = CollectionViewSource.GetDefaultView(sortiranaLista);
                dataGridKorisnici.ItemsSource = viewKorisnici;
                viewKorisnici.Filter = CustomFilter;
            }

            if (radioButton_Prezime.IsChecked == true)
            {
                List<Korisnik> sortiranaLista = PodaciKorisnik.listaKorisnika.OrderBy(k => k.Prezime).ToList();
                viewKorisnici = CollectionViewSource.GetDefaultView(sortiranaLista);
                dataGridKorisnici.ItemsSource = viewKorisnici;
                viewKorisnici.Filter = CustomFilter;
            }

            if (radioButtonkorIme.IsChecked == true)
            {
                List<Korisnik> sortiranaLista = PodaciKorisnik.listaKorisnika.OrderBy(k => k.KorisnickoIme).ToList();
                viewKorisnici = CollectionViewSource.GetDefaultView(sortiranaLista);
                dataGridKorisnici.ItemsSource = viewKorisnici;
                viewKorisnici.Filter = CustomFilter;
            }

            if (radioButtonEmail.IsChecked == true)
            {
                List<Korisnik> sortiranaLista = PodaciKorisnik.listaKorisnika.OrderBy(k => k.Email).ToList();
                viewKorisnici = CollectionViewSource.GetDefaultView(sortiranaLista);
                dataGridKorisnici.ItemsSource = viewKorisnici;
                viewKorisnici.Filter = CustomFilter;
            }

            if (radioButtonTipKor.IsChecked == true)
            {
                List<Korisnik> sortiranaLista = PodaciKorisnik.listaKorisnika.OrderBy(k => k.TipKorisnika.ToString()).ToList();
                viewKorisnici = CollectionViewSource.GetDefaultView(sortiranaLista);
                dataGridKorisnici.ItemsSource = viewKorisnici;
                viewKorisnici.Filter = CustomFilter;
            }

        }
    
}
}
