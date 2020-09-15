using SF_45_2018.AdminProzori;
using SF_45_2018.Entiteti;
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

namespace SF_45_2018
{
    /// <summary>
    /// Interaction logic for ProzorIzbor.xaml
    /// </summary>
    public partial class ProzorIzbor : Window
    {
        public ETipKorisnika tipKorisnika;

        public ProzorIzbor()
        {

            InitializeComponent();
            tipKorisnika = new ETipKorisnika();
        }

        private void buttonKorisnici_Click(object sender, RoutedEventArgs e)
        {
           
            if (tipKorisnika == ETipKorisnika.ADMIN)
            {
                AdminKorisnik adminProzor = new AdminKorisnik();
                adminProzor.ShowDialog();
            }
        }

        private void buttonUstanove_Click(object sender, RoutedEventArgs e)
        {
            if (tipKorisnika == ETipKorisnika.ADMIN)
            {
                AdminProzorUstanova adminProzorUstanova = new AdminProzorUstanova();
                adminProzorUstanova.ShowDialog();
            }
        }

        private void buttonUcionice_Click(object sender, RoutedEventArgs e)
        {
            AdminProzorTermin prozorTermin = new AdminProzorTermin();
            prozorTermin.ShowDialog();
        }

        private void buttonRaspored_Click(object sender, RoutedEventArgs e)
        {
            RasporedProzor rasporedProzor = new RasporedProzor();
            rasporedProzor.ShowDialog();
        }
    }
}
