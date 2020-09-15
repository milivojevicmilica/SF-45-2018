using SF_45_2018.AdminProzori;
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

namespace SF_45_2018
{
    /// <summary>
    /// Interaction logic for NastaviKaoGost.xaml
    /// </summary>
    public partial class NastaviKaoGost : Window
    {
        ICollectionView viewUstanove;
        string searchFilter = "Active";
        
        public NastaviKaoGost()
        {
            InitializeComponent();
            dataGridUstanove.IsReadOnly = true;
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

           
        }

        

        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {
            searchFilter = "SEARCH";
            viewUstanove.Refresh();
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
            if (e.PropertyName.Equals("Termini") || e.PropertyName.Equals("ListaUcionica") || e.PropertyName.Equals("ZaposenaLica") || e.PropertyName.Equals("Error"))
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
