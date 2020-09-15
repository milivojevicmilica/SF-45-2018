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
    /// Interaction logic for AdminProzorUstanova.xaml
    /// </summary>
    public partial class AdminProzorUstanova : Window
    {
        ICollectionView viewUstanove;
        string searchFilter = "Active";
        public AdminProzorUstanova()
        {
            InitializeComponent();
            dataGridUstanove.IsReadOnly = true;
            if (PodaciKorisnik.AktivniKorisnik == null)  //ako izaberemo "nastavi kao gost".
            {
                buttonAdd.Visibility = Visibility.Hidden;
                buttonDelete.Visibility = Visibility.Hidden;
                buttonEdit.Visibility = Visibility.Hidden;
                buttonUcionice.Visibility = Visibility.Hidden;
            }

            OsveziProzor();

        } 

        private bool CustomFilter(object obj)
        {
            Ustanova ustanova = obj as Ustanova;
            if (searchFilter.Equals("SEARCH") && String.IsNullOrEmpty(textBoxSifra.Text))
            {
                return (ustanova.Active && ustanova.Naziv.ToLower().Contains(textBoxNaziv.Text.ToLower()))
                       && (ustanova.Active && ustanova.Lokacija.ToLower().Contains(textBoxLokacija.Text.ToLower()));
            }
            if (searchFilter.Equals("SEARCH"))
                return (ustanova.Active && ustanova.Naziv.ToLower().Contains(textBoxNaziv.Text.ToLower()))
                    && (ustanova.Active && ustanova.Lokacija.ToLower().Contains(textBoxLokacija.Text.ToLower()))
                    && (ustanova.Active && ustanova.SifraUstanove.Equals(Convert.ToInt32(textBoxSifra.Text)));
            return ustanova.Active;
        }

        public void OsveziProzor()
        {
            viewUstanove = CollectionViewSource.GetDefaultView(PodaciUstanova.listaUstanova);
            dataGridUstanove.ItemsSource = viewUstanove;
            viewUstanove.Filter = CustomFilter;

            try
            {
                if (PodaciKorisnik.AktivniKorisnik.Equals(null))
                {
                    buttonDelete.Visibility = Visibility.Collapsed;
                    buttonAdd.Visibility = Visibility.Collapsed;
                    buttonEdit.Visibility = Visibility.Collapsed;
                }
            }
            catch (NullReferenceException)
            {

                buttonDelete.Visibility = Visibility.Collapsed;
                buttonAdd.Visibility = Visibility.Collapsed;
                buttonEdit.Visibility = Visibility.Collapsed;

            }
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            Ustanova ustanova = new Ustanova();
            UstanovaProzor ustanovaProzor = new UstanovaProzor(ustanova);
            if (ustanovaProzor.ShowDialog() == true)
            {
                OsveziProzor();
                viewUstanove.Refresh();
                searchFilter = "Active";
            }
        }

        private void buttonEdit_Click(object sender, RoutedEventArgs e)
        {
            Ustanova ustanova = dataGridUstanove.SelectedItem as Ustanova;

            if (ustanova == null)
            {
                MessageBox.Show("Ni jedno polje nije obelezeno", "Upozorenje", MessageBoxButton.OK);
            }
            else
            {
                Ustanova staraUstanova = ustanova.Clone();
                UstanovaProzor ustanovaProzor = new UstanovaProzor(ustanova);

                if (ustanovaProzor.ShowDialog() == false)
                { 
                    int indeks = PodaciUstanova.listaUstanova.IndexOf(PodaciUstanova.listaUstanova.Where(X => X.SifraUstanove.Equals(staraUstanova.SifraUstanove)).FirstOrDefault());
                    staraUstanova.Active = true;
                    PodaciUstanova.listaUstanova[indeks] = staraUstanova;
                }
                viewUstanove.Refresh();
                searchFilter = "Active";
            }

        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            Ustanova ustanova = dataGridUstanove.SelectedValue as Ustanova;

            if (ustanova != null)
            {
                Ustanova izbrisanaUstanova = PodaciUstanova.PretraziPoSifri(ustanova.SifraUstanove);
                PodaciUstanova.IzbrisiUstanovu(ustanova);
                izbrisanaUstanova.Active = false;
                searchFilter = "Active";
                viewUstanove.Refresh();
            }
            else
            {
                MessageBox.Show("Ni jedno polje nije obelezeno", "Upozorenje", MessageBoxButton.OK);
            }

        }

        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {
            searchFilter = "SEARCH";
            viewUstanove.Refresh();
        }

        private void buttonUcionice_Click(object sender, RoutedEventArgs e)
        {
            Ustanova izabranaUstanova = dataGridUstanove.SelectedItem as Ustanova;
            if (izabranaUstanova != null)
            {
                AdminProzorUcionica admin = new AdminProzorUcionica(izabranaUstanova);
                admin.ShowDialog();
            }
            else
            {
                MessageBox.Show("Izaberite jednu od stavki", "Greska", MessageBoxButton.OK);
            }
        }

        private void buttonZaposlenaLica_Click(object sender, RoutedEventArgs e)
        {
            Ustanova izabranaUstanova = dataGridUstanove.SelectedItem as Ustanova;


            if (izabranaUstanova != null)
            {
                ProzorZaposlenaLica zlProzor = new ProzorZaposlenaLica(izabranaUstanova);
                zlProzor.ShowDialog();

            }
            else
            {
                MessageBox.Show("Izaberite jednu od stavki", "Greska", MessageBoxButton.OK);
            }
        }

        private void buttonRaspored_Click(object sender, RoutedEventArgs e)
        {
            Ustanova odabranaUstanova = dataGridUstanove.SelectedItem as Ustanova;
            if (odabranaUstanova != null)
            {
                AdminProzorTermin terminProzor = new AdminProzorTermin(odabranaUstanova);
                terminProzor.ShowDialog();
            }
            else
            {
                MessageBox.Show("Izaberite jednu od stavki", "Greska", MessageBoxButton.OK);
            }
        }
    
        
    

        private void dataGridUstanove_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ( e.PropertyName.Equals("Termini") || e.PropertyName.Equals("ListaUcionica") || e.PropertyName.Equals("ZaposenaLica")|| e.PropertyName.Equals("Error"))
                e.Column.Visibility = Visibility.Collapsed;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            textBoxLokacija.Clear();
            textBoxNaziv.Clear();
            textBoxSifra.Clear();
            searchFilter = "SEARCH";
            viewUstanove.Refresh();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            dataGridUstanove.Columns[0].Header = "Sifra";
            dataGridUstanove.Columns[7].Header = "Max Br Ucionica";

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (radioButtonId.IsChecked == true)
            {
                List<Ustanova> sortiranaLista = PodaciUstanova.listaUstanova.OrderBy(u => u.SifraUstanove).ToList();
                viewUstanove = CollectionViewSource.GetDefaultView(sortiranaLista);
                dataGridUstanove.ItemsSource = viewUstanove;
                viewUstanove.Filter = CustomFilter;
            }
            if (radioButtonNaziv.IsChecked == true)
            {
                List<Ustanova> sortiranaLista = PodaciUstanova.listaUstanova.OrderBy(u => u.Naziv).ToList();
                viewUstanove = CollectionViewSource.GetDefaultView(sortiranaLista);
                dataGridUstanove.ItemsSource = viewUstanove;
                viewUstanove.Filter = CustomFilter;
            }
            if (radioButtonLokacija.IsChecked == true)
            {
                List<Ustanova> sortiranaLista = PodaciUstanova.listaUstanova.OrderBy(u => u.Lokacija).ToList();
                viewUstanove = CollectionViewSource.GetDefaultView(sortiranaLista);
                dataGridUstanove.ItemsSource = viewUstanove;
                viewUstanove.Filter = CustomFilter;
            }
            if (radioButtonBrUcionica.IsChecked == true)
            {
                List<Ustanova> sortiranaLista = PodaciUstanova.listaUstanova.OrderBy(u => u.MaksimalanBrojUcionica).ToList();
                viewUstanove = CollectionViewSource.GetDefaultView(sortiranaLista);
                dataGridUstanove.ItemsSource = viewUstanove;
                viewUstanove.Filter = CustomFilter;
            }
        }
    }
}
