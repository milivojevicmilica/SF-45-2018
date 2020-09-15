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
    /// Interaction logic for UstanovaProzor.xaml
    /// </summary>
    public partial class UstanovaProzor : Window
    {
        enum Status { ADD, EDIT }
        private Status _status;

        private Ustanova selectedUstanova;

        public UstanovaProzor(Ustanova ustanova)
        {
            InitializeComponent();

            if (ustanova.Naziv.Equals(""))
            {
                this._status = Status.ADD;
            }
            else
            {
                this._status = Status.EDIT;
            }

            selectedUstanova = ustanova;
            this.DataContext = ustanova;
        }

        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
           
            Ustanova ustanova = PodaciUstanova.PretraziPoNazivu(textBoxNaziv.Text);
            if (ustanova != null && _status.Equals(Status.ADD))
            {
                MessageBox.Show($"Ustanova sa ovim nazivom {ustanova.Naziv} vec postoji", "Upozorenje",
                    MessageBoxButton.OK);
                return;
            }
            Ustanova ustLokacija = PodaciUstanova.PretraziPoLokaciji(textBoxLokacija.Text);
            if (ustLokacija != null && _status.Equals(Status.ADD))
            {
                MessageBox.Show($"Ustanova sa ovom lokacijom {ustLokacija.Lokacija} vec postoji", "Upozorenje",
                    MessageBoxButton.OK);
                return;
            }
            if (String.IsNullOrWhiteSpace(textBoxNaziv.Text) || String.IsNullOrWhiteSpace(textBoxLokacija.Text) || String.IsNullOrWhiteSpace(textBoxbrUcionica.Text))
            {
                MessageBox.Show("Niste popunili sva polja.", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (textBoxbrUcionica.Text.Equals("0"))
            {
                MessageBox.Show("Broj ucionica mora biti veci od 0.", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_status.Equals(Status.ADD))
            {
                PodaciUstanova.DodajUstanovu(selectedUstanova);
                PodaciUstanova.listaUstanova.Clear();
                PodaciUstanova.UcitajUstanove();

            }

            this.DialogResult = true;
            this.Close();

            if (_status.Equals(Status.EDIT))
            {
                PodaciUstanova.IzmeniUstanovu(selectedUstanova);
            }

        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void textBoxbrUcionica_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !App.IsTextAllowed(e.Text);
        }

        private void textBoxNaziv_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = App.IsTextAllowed(e.Text);
        }
    }
}
