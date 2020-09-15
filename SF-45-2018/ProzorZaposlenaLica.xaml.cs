using SF_45_2018.Entiteti;
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
    /// Interaction logic for ProzorZaposlenaLica.xaml 
    /// </summary>
    public partial class ProzorZaposlenaLica : Window
    {
        ICollectionView viewZaposleni;
        string SearchFilter = "Active";
        Ustanova ustanova;

        public ProzorZaposlenaLica(Ustanova ustanova)
        {
            InitializeComponent();
            this.ustanova = ustanova;
            OsveziProzor();
        }

        public void OsveziProzor()
        {
            
            viewZaposleni = CollectionViewSource.GetDefaultView(ustanova.ZaposenaLica);
            dataGridZaposlenaLica.ItemsSource = viewZaposleni;
        }

        private void dataGridZaposlenaLica_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Error") || e.PropertyName.Equals("Id") || e.PropertyName.Equals("Lozinka"))
                e.Column.Visibility = Visibility.Collapsed;
        }

        private void ZaposlenaLicaProzor_ContentRendered(object sender, EventArgs e)
        {
            dataGridZaposlenaLica.Columns[3].Header = "Korisnicko Ime";
            dataGridZaposlenaLica.Columns[4].Header = "E-Mail";
            dataGridZaposlenaLica.Columns[5].Header = "Tip Korisnika";
        }
    }
}
