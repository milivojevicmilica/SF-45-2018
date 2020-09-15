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
    /// Interaction logic for AdminProzorUcionica.xaml
    /// </summary>
    public partial class AdminProzorUcionica : Window
    {
        ICollectionView viewUcionice;
        string searchFilter = "Active";
        public static Ustanova ustanova;
        public AdminProzorUcionica(Ustanova u)
        {
            ustanova = u;

            InitializeComponent();
            comboBox.ItemsSource = new List<ETipUcionice> { ETipUcionice.SARACUNARIMA, ETipUcionice.BEZRACUNARA };
            OsveziProzor();
            dataGrid.IsReadOnly = true;
        }

        public bool CustomFilter(object obj)
        {
            Ucionica ucionica = obj as Ucionica;

            if (searchFilter.Equals("SEARCH") && comboBox.SelectedIndex > -1 && !(String.IsNullOrEmpty(textBoxSearchSifra.Text)))
            {
                return ((ucionica.Active &&
                         ucionica.IdUcionice.Equals(Convert.ToInt32(textBoxSearchSifra.Text)))
                        && (ucionica.Active && ucionica.BrojMesta.ToString().ToLower()
                                .Contains(textBoxSearchBrojMesta.Text.ToLower()))
                        && (ucionica.Active && ucionica.BrojMesta.ToString().ToLower()
                                .Contains(textBoxSearchBrojMesta.Text.ToLower()))
                        && (ucionica.Active && ucionica.BrojUcionice.ToString().ToLower()
                                .Contains(textBoxSearchBroj.Text.ToLower()))
                        && (ucionica.Active && ucionica.TipUcionice.ToString().ToLower().Equals(comboBox.SelectedItem.ToString().ToLower())));
            }

            else if (searchFilter.Equals("SEARCH") && comboBox.SelectedIndex > -1)
            {
                return ((ucionica.Active && ucionica.BrojMesta.ToString().ToLower()
                                .Contains(textBoxSearchBrojMesta.Text.ToLower()))
                        && (ucionica.Active && ucionica.BrojMesta.ToString().ToLower()
                                .Contains(textBoxSearchBrojMesta.Text.ToLower()))
                        && (ucionica.Active && ucionica.BrojUcionice.ToString().ToLower()
                                .Contains(textBoxSearchBroj.Text.ToLower()))
                        && (ucionica.Active && ucionica.TipUcionice.ToString().ToLower().Equals(comboBox.SelectedItem.ToString().ToLower())));
            }

            else if (searchFilter.Equals("SEARCH") && String.IsNullOrEmpty(textBoxSearchSifra.Text))
            {
                return ((ucionica.Active && ucionica.BrojMesta.ToString().ToLower()
                             .Contains(textBoxSearchBrojMesta.Text.ToLower()))
                        && (ucionica.Active && ucionica.BrojUcionice.ToString().ToLower()
                                .Contains(textBoxSearchBroj.Text.ToLower())));
                /*&& (ucionica.Active && ucionica.TipUcionice.ToString().ToLower()
                        .Contains(textBoxSearchTip.Text.ToLower())));*/
            }

            else if (searchFilter.Equals("SEARCH"))
            {
                return ((ucionica.Active &&
                         ucionica.IdUcionice.Equals(Convert.ToInt32(textBoxSearchSifra.Text)))
                        && (ucionica.Active && ucionica.BrojMesta.ToString().ToLower()
                                .Contains(textBoxSearchBrojMesta.Text.ToLower()))
                        && (ucionica.Active && ucionica.BrojMesta.ToString().ToLower()
                                .Contains(textBoxSearchBrojMesta.Text.ToLower()))
                        && (ucionica.Active && ucionica.BrojUcionice.ToString().ToLower()
                                .Contains(textBoxSearchBroj.Text.ToLower())));
                
            }
            else
            {
                return ucionica.Active;
            }
        }

        public void OsveziProzor()
        {

            viewUcionice = CollectionViewSource.GetDefaultView(ustanova.ListaUcionica);
            dataGrid.ItemsSource = viewUcionice;
            viewUcionice.Filter = CustomFilter;

        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (ustanova.ListaUcionica.Count < ustanova.MaksimalanBrojUcionica)
            {
                Ucionica ucionica = new Ucionica();

                UcioniceEditProzor ucionicaEdit = new UcioniceEditProzor(ucionica);

                if (ucionicaEdit.ShowDialog() == true)
                {
                    ustanova.ListaUcionica.Clear();
                    PodaciUcionica.DodajUcioniceOdredjenojUstanovi();
                    OsveziProzor();
                    viewUcionice.Refresh();
                    searchFilter = "Active";
                }
            }
            if (ustanova.ListaUcionica.Count >= ustanova.MaksimalanBrojUcionica)
            {
                string text =
                    String.Format("U ovu ustanove ne mozete dodati vise ucionica. Maksimalan broj ucionica: {0}",
                        ustanova.MaksimalanBrojUcionica);
                MessageBox.Show(text, "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
                //MessageBox.Show("U ovu ustanove ne mozete dodati vise ucionica", "Greska!",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void buttonRemove_Click(object sender, RoutedEventArgs e)
        {
            Ucionica ucionica = dataGrid.SelectedValue as Ucionica;

            if (ucionica != null)
            {
                Ucionica izbrisanaUcionica = PodaciUcionica.PretraziPoIdu(ucionica.IdUcionice);
                int indeks = ustanova.ListaUcionica.IndexOf(ustanova.ListaUcionica.Where(u => u.IdUcionice.Equals(ucionica.IdUcionice)).FirstOrDefault());
                izbrisanaUcionica.Active = false;
                PodaciUcionica.IzbrisiUcionicu(ucionica);
                ustanova.ListaUcionica[indeks].Active = false;

                searchFilter = "Active";
                viewUcionice.Refresh();
            }
            else
            {
                MessageBox.Show("Ni jedno polje nije obelezeno", "Upozorenje", MessageBoxButton.OK);
            }
        }

        private void buttonEdit_Click(object sender, RoutedEventArgs e)
        {
            Ucionica ucionica = dataGrid.SelectedItem as Ucionica;

            if (ucionica == null)
            {
                MessageBox.Show("Ni jedno polje nije obelezeno", "Upozorenje", MessageBoxButton.OK);
            }
            else
            {

                Ucionica staraUcionica = ucionica.Clone();

                UcioniceEditProzor ucionicaEditProzor = new UcioniceEditProzor(ucionica);
                if (ucionicaEditProzor.ShowDialog() == false)
                {
                    int indeks = PodaciUcionica.listaUcionica.IndexOf(PodaciUcionica.listaUcionica.Where(u => u.IdUcionice.Equals(staraUcionica.IdUcionice)).FirstOrDefault());

                    staraUcionica.Active = true;
                    PodaciUcionica.listaUcionica[indeks] = staraUcionica;
                }

                viewUcionice.Refresh();
                searchFilter = "Active";
            }
        }

        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {
            searchFilter = "SEARCH";
            viewUcionice.Refresh();
        }

        private void dataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Error") || e.PropertyName.Equals("Active") || e.PropertyName.Equals("UstanovaGdeSeNalaziId"))
                e.Column.Visibility = Visibility.Collapsed;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            textBoxSearchSifra.Clear();
            textBoxSearchBroj.Clear();
            textBoxSearchBrojMesta.Clear();
            comboBox.SelectedIndex = -1;
            searchFilter = "SEARCH";
            viewUcionice.Refresh();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            dataGrid.Columns[0].Header = "Sifra";
            dataGrid.Columns[1].Header = "Broj Ucionice";
            dataGrid.Columns[2].Header = "Broj Mesta";
            dataGrid.Columns[3].Header = "Tip Ucionice";
            dataGrid.Columns[5].Header = "Ustanova";
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (radioButtonSifra.IsChecked == true)
            {
                List<Ucionica> sortiranaLista = PodaciUcionica.listaUcionica.OrderBy(u => u.IdUcionice).ToList();
                viewUcionice = CollectionViewSource.GetDefaultView(sortiranaLista);
                dataGrid.ItemsSource = viewUcionice;
                viewUcionice.Filter = CustomFilter;
            }
            if (radioButtonBrjMesta.IsChecked == true)
            {
                List<Ucionica> sortiranaLista = PodaciUcionica.listaUcionica.OrderBy(u => u.BrojMesta).ToList();
                viewUcionice = CollectionViewSource.GetDefaultView(sortiranaLista);
                dataGrid.ItemsSource = viewUcionice;
                viewUcionice.Filter = CustomFilter;
            }
            if (radioButtonBrojUcionice.IsChecked == true)
            {
                List<Ucionica> sortiranaLista = PodaciUcionica.listaUcionica.OrderBy(u => u.BrojUcionice).ToList();
                viewUcionice = CollectionViewSource.GetDefaultView(sortiranaLista);
                dataGrid.ItemsSource = viewUcionice;
                viewUcionice.Filter = CustomFilter;
            }
            if (radioButtonTipUcionice.IsChecked == true)
            {
                List<Ucionica> sortiranaLista = PodaciUcionica.listaUcionica.OrderBy(u => u.TipUcionice.ToString()).ToList();
                viewUcionice = CollectionViewSource.GetDefaultView(sortiranaLista);
                dataGrid.ItemsSource = viewUcionice;
                viewUcionice.Filter = CustomFilter;
            }
        }
    }
}
