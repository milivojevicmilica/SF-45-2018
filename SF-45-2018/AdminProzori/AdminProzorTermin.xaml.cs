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
    /// Interaction logic for AdminProzorTermin.xaml
    /// </summary>
    public partial class AdminProzorTermin : Window
    {

        ICollectionView viewTermini;
        private Ustanova selektovanaUstanova;
        string searchFilter = "Active";

        public AdminProzorTermin()
        {
            InitializeComponent();
            comboBox1.ItemsSource = new List<EDaniUnedelji>
            {
                EDaniUnedelji.PONEDELJAK, EDaniUnedelji.UTORAK, EDaniUnedelji.SREDA, EDaniUnedelji.CETVRTAK,
                EDaniUnedelji.PETAK, EDaniUnedelji.SUBOTA, EDaniUnedelji.NEDELJA
            };
            comboBox.ItemsSource = new List<ETipNastave> { ETipNastave.Predavanja, ETipNastave.Vezbe };

            dataGridTermini.IsReadOnly = true;
            OsveziProzor();
        }

        public AdminProzorTermin(Ustanova ustanova)
        {
            InitializeComponent();

            comboBox1.ItemsSource = new List<EDaniUnedelji>
            {
                EDaniUnedelji.PONEDELJAK, EDaniUnedelji.UTORAK, EDaniUnedelji.SREDA, EDaniUnedelji.CETVRTAK,
                EDaniUnedelji.PETAK, EDaniUnedelji.SUBOTA, EDaniUnedelji.NEDELJA
            };

            comboBox.ItemsSource = new List<ETipNastave> { ETipNastave.Predavanja, ETipNastave.Vezbe };
            dataGridTermini.IsReadOnly = true;

            selektovanaUstanova = ustanova;
            OsveziProzorSaPar();
        }

        public void OsveziProzorSaPar()
        {
            List<Termin> odabraniTermin = new List<Termin>();
            foreach (var termin in PodaciTermin.listaTermina)
            {
                if (selektovanaUstanova.SifraUstanove.Equals(termin.UstanovaId))
                {
                    odabraniTermin.Add(termin);
                }
            }

            viewTermini = CollectionViewSource.GetDefaultView(odabraniTermin);
            dataGridTermini.ItemsSource = viewTermini;
            viewTermini.Filter = CustomFilter;

            try
            {
                if (PodaciKorisnik.AktivniKorisnik.Equals(null))
                {
                    buttonAdd.Visibility = Visibility.Collapsed;
                    buttonEdit.Visibility = Visibility.Collapsed;
                    buttonRemove.Visibility = Visibility.Collapsed;
                }
            }
            catch (NullReferenceException)
            {
                buttonAdd.Visibility = Visibility.Collapsed;
                buttonEdit.Visibility = Visibility.Collapsed;
                buttonRemove.Visibility = Visibility.Collapsed;
            }

        }

        public bool CustomFilter(object obj)
        {
            Termin termin = obj as Termin;


            if (searchFilter.Equals("SEARCH") && String.IsNullOrEmpty(textBoxSearch.Text))
            {
                return (termin.Active && termin.DaniUNedelji.ToString().ToLower().Contains(comboBox1.Text.ToLower())
                                      && (termin.Active && termin.TipNastave.ToString().ToLower()
                                              .Contains(comboBox.Text.ToLower())));
            }

            else if (searchFilter.Equals("SEARCH"))
            {
                return (termin.Active && termin.DaniUNedelji.ToString().ToLower().Contains(comboBox1.Text.ToLower()))
                       && (termin.Active && termin.TipNastave.ToString().ToLower().Contains(comboBox.Text.ToLower()))
                       && (termin.Active && termin.IdTermin.Equals(Convert.ToInt32(textBoxSearch.Text)));
            }

            else
            {
                return termin.Active;
            }
        }

        public void OsveziProzor()
        {
            viewTermini = CollectionViewSource.GetDefaultView(PodaciTermin.listaTermina);
            dataGridTermini.ItemsSource = viewTermini;
            viewTermini.Filter = CustomFilter;

            try
            {
                if (PodaciKorisnik.AktivniKorisnik.Equals(null))
                {
                    buttonAdd.Visibility = Visibility.Collapsed;
                    buttonEdit.Visibility = Visibility.Collapsed;
                    buttonRemove.Visibility = Visibility.Collapsed;
                }
            }
            catch (NullReferenceException)
            {
                buttonAdd.Visibility = Visibility.Collapsed;
                buttonEdit.Visibility = Visibility.Collapsed;
                buttonRemove.Visibility = Visibility.Collapsed;
            }
        }

        private void dataGridTermini_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(System.DateTime))
                (e.Column as DataGridTextColumn).Binding.StringFormat = "HH:mm";

            if (e.PropertyName.Equals("Active") || e.PropertyName.Equals("ZaduzeniPredavacId") || e.PropertyName.Equals("UstanovaId") || e.PropertyName.Equals("UcionicaId"))
                e.Column.Visibility = Visibility.Collapsed;
        }

        private void buttonEdit_Click(object sender, RoutedEventArgs e)
        {
            Termin termin = dataGridTermini.SelectedItem as Termin;

            if (termin == null)
                MessageBox.Show("Ni jedno polje nije obelezeno", "Upozorenje", MessageBoxButton.OK);
            else
            {
                Termin stariTermin = termin.Clone();

                Termin selektovaniTermin = dataGridTermini.SelectedItem as Termin;

                TerminiEditProzor terminEditProzor = new TerminiEditProzor(termin);

                if (terminEditProzor.ShowDialog() == false)
                {
                    int indeks = PodaciTermin.listaTermina.IndexOf(PodaciTermin.listaTermina.Where(t => t.IdTermin.Equals(stariTermin.IdTermin)).FirstOrDefault());

                    stariTermin.Active = true;
                    stariTermin.ZaduzeniPredavacId = termin.ZaduzeniPredavacId;
                    stariTermin.UcionicaId = termin.UcionicaId;
                    stariTermin.UstanovaId = termin.UstanovaId;
                    stariTermin.ZaduzeniPredavac = termin.ZaduzeniPredavac;
                    stariTermin.Ustanova = termin.Ustanova;
                    stariTermin.Ucionica = termin.Ucionica;
                    PodaciTermin.listaTermina[indeks] = stariTermin;

                }
                viewTermini.Refresh();
            }
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            Termin termin = new Termin();

            TerminiEditProzor terEditProzor = new TerminiEditProzor(termin);

            if (terEditProzor.ShowDialog() == true)
            {
                OsveziProzor();
                viewTermini.Refresh();
                searchFilter = "Active";

            }
        }

        private void buttonRemove_Click(object sender, RoutedEventArgs e)
        {
            Termin termin = dataGridTermini.SelectedItem as Termin;

            if (termin != null)
            {
                Termin izbrisaniTermin = PodaciTermin.PretraziPoSifri(Convert.ToInt32(termin.IdTermin));
                izbrisaniTermin.Active = false;
                PodaciTermin.IzbrisiTermin(termin);
                searchFilter = "Active";
                viewTermini.Refresh();
            }
            else
            {
                MessageBox.Show("Ni jedno polje nije obelezeno", "Upozorenje", MessageBoxButton.OK);
            }
        }

        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {
            searchFilter = "SEARCH";
            viewTermini.Refresh();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            textBoxSearch.Clear();
            comboBox.SelectedIndex = -1;
            comboBox1.SelectedIndex = -1;
            searchFilter = "SEARCH";
            viewTermini.Refresh();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            dataGridTermini.Columns[0].Header = "Sifra";
            dataGridTermini.Columns[1].Header = "Pocetak Termina";
            dataGridTermini.Columns[2].Header = "Kraj Termina";
            dataGridTermini.Columns[3].Header = "Dan";
            dataGridTermini.Columns[4].Header = "Tip Nastave";
            dataGridTermini.Columns[6].Header = "Predavac";

        }
    }
}
