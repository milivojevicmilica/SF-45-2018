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
    /// Interaction logic for RasporedProzor.xaml
    /// </summary>
    public partial class RasporedProzor : Window
    {
        private ICollectionView viewRaspored;
        private Ustanova selektovanaUstanova;
       

        public RasporedProzor()
        {
            InitializeComponent();
            dataGrid.IsReadOnly = true;
            OsveziProzorBezParametara();
        }

        public bool CustomFilter(object obj)
        {
            Termin termin = obj as Termin;

            return termin.Active;

        }

        public void OsveziProzorBezParametara() //da se vide svi rasporedi za sve ustanove
        {

            viewRaspored = CollectionViewSource.GetDefaultView(PodaciTermin.listaTermina);
            dataGrid.ItemsSource = viewRaspored;
            viewRaspored.Filter = CustomFilter;
        }

        public void OsveziProzorSaParametrima() 
        {

            List<Termin> odabraniTermin = new List<Termin>();
            foreach (var termin in PodaciTermin.listaTermina)
            {
                if (selektovanaUstanova.SifraUstanove.Equals(termin.UstanovaId))
                {
                    odabraniTermin.Add(termin);
                }
            }

            viewRaspored = CollectionViewSource.GetDefaultView(odabraniTermin);
            dataGrid.ItemsSource = viewRaspored;
            viewRaspored.Filter = CustomFilter;

        }

        public RasporedProzor(Ustanova ustanova)
        {
            InitializeComponent();

            selektovanaUstanova = ustanova;
            OsveziProzorSaParametrima();

        }

        private void dataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {

            if (e.PropertyName.Equals("Active") || e.PropertyName.Equals("ZaduzeniPredavacId") || e.PropertyName.Equals("UstanovaId") || e.PropertyName.Equals("UcionicaId") || e.PropertyName.Equals("IdTermin"))
                e.Column.Visibility = Visibility.Collapsed;


            if (e.PropertyType == typeof(System.DateTime))
                (e.Column as DataGridTextColumn).Binding.StringFormat = "HH:mm";
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            dataGrid.Columns[1].Header = "Pocetak Termina";
            dataGrid.Columns[2].Header = "Kraj Termina";
            dataGrid.Columns[3].Header = "Dan";
            dataGrid.Columns[4].Header = "Tip Nastave";
            dataGrid.Columns[6].Header = "Zaduzeni Predavac";

        }
    }
}
