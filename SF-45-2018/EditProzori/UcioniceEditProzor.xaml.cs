using SF_45_2018.AdminProzori;
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
    /// Interaction logic for UcioniceEditProzor.xaml
    /// </summary>
    public partial class UcioniceEditProzor : Window
    {
        enum Status { ADD, EDIT }
        private Status _status;
        private Ucionica selectedUcionica;

        public UcioniceEditProzor(Ucionica ucionica)
        {
            InitializeComponent();

            comboBoxTipUcionice.ItemsSource = new List<ETipUcionice>() { ETipUcionice.BEZRACUNARA, ETipUcionice.SARACUNARIMA };

            if (ucionica.IdUcionice.Equals(0))
                this._status = Status.ADD;
            else
            {
                this._status = Status.EDIT;
            }
            selectedUcionica = ucionica;
            this.DataContext = ucionica;
        }

        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {

            if (String.IsNullOrWhiteSpace(textBoxBrojUcionice.Text) || String.IsNullOrWhiteSpace(textBoxBrojMesta.Text) || comboBoxTipUcionice.SelectedIndex < 0)
            {
                MessageBox.Show("Niste popunili sva polja!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (textBoxBrojMesta.Text.Equals("0"))
            {
                MessageBox.Show("Broj mesta mora biti veci od 0.", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            if (_status.Equals(Status.ADD))
            {
                selectedUcionica.UstanovaGdeSeNalaziId = AdminProzorUcionica.ustanova.SifraUstanove;


                foreach (var ucionica in PodaciUcionica.listaUcionica)
                {

                    if (AdminProzorUcionica.ustanova.SifraUstanove.Equals(ucionica.UstanovaGdeSeNalaziId))
                    {
                        if (ucionica.BrojUcionice.Trim() == textBoxBrojUcionice.Text.Trim())
                        {
                            MessageBox.Show("Ucionica sa ovim brojem vec postoji.", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                    }
                }


                PodaciUcionica.DodajUcionicu(selectedUcionica);
                PodaciUcionica.listaUcionica.Clear();
                PodaciUcionica.UcitajUcionice();


            }

            if (_status.Equals(Status.EDIT))
            {
                PodaciUcionica.IzmeniUcionicu(selectedUcionica);
            }
            this.DialogResult = true;
            this.Close();
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void textBoxBrojMesta_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !App.IsTextAllowed(e.Text);
        }
    }
}
