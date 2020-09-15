using SF_45_2018.EditProzori;
using SF_45_2018.Entiteti;
using SF_45_2018.Logika;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for AsistentProzor.xaml
    /// </summary>
    public partial class AsistentProzor : Window
    {
            
            ICollectionView viewListaProfesora;
            private ICollectionView viewRaspored;
            public static Profesor profesor;
            public static Asistent asistent;
            private ObservableCollection<Termin> rasporedZaOdabranogProfesora;
            private ObservableCollection<Termin> rasporedZaOdabranogAsistenta;
            public AsistentProzor(Korisnik korisnik)
            {
                InitializeComponent();

                dataGrid.IsReadOnly = true;
                rasporedZaOdabranogProfesora = new ObservableCollection<Termin>();
                rasporedZaOdabranogAsistenta = new ObservableCollection<Termin>();
                profesor = korisnik as Profesor;
                asistent = korisnik as Asistent;
                OsveziProzor();

            }

            private bool CustomFilter(object obj)
            {
                Korisnik korisnik = obj as Korisnik;
                return korisnik.Active;
            }

            public void OsveziProzor()
            {
            
                
                Asistent a = PodaciKorisnik.PretraziPoIDu(asistent.Id) as Asistent;

                viewListaProfesora = CollectionViewSource.GetDefaultView(a.DodeljeniProfesor.ListaAsistenata);
                dataGridListaProfesora.ItemsSource = viewListaProfesora;
                viewListaProfesora.Filter = CustomFilter;

                textBlockIme.Text = asistent.Ime;
                textBlockPrezime.Text = asistent.Prezime;
                textBlockKorIme.Text = asistent.KorisnickoIme;
                textBlockEmail.Text = asistent.Email;
                textBlockTip.Text = asistent.TipKorisnika.ToString();

            

                foreach (var raspored in PodaciTermin.listaTermina)
                {
                    if (PodaciKorisnik.AktivniKorisnik.Id.Equals(raspored.ZaduzeniPredavacId))
                    {
                        rasporedZaOdabranogProfesora.Add(raspored);
                    }
                }

                viewRaspored = CollectionViewSource.GetDefaultView(rasporedZaOdabranogProfesora);
                dataGrid.ItemsSource = viewRaspored;

            }

            

            private void buttonTermin_Click(object sender, RoutedEventArgs e)
            {
                

                
                    asistent = PodaciKorisnik.AktivniKorisnik as Asistent;
                    Termin termin = new Termin();
                    termin.ZaduzeniPredavac = PodaciKorisnik.AktivniKorisnik;
                    termin.Ustanova = asistent.UstanovaZaposlenja;
                    TerminiEditProzor terminiProzor = new TerminiEditProzor(termin);
                    terminiProzor.ShowDialog();
                    if (terminiProzor.DialogResult == true)
                    {
                        rasporedZaOdabranogAsistenta.Clear();
                        OsveziProzor();
                        viewRaspored.Refresh();

                    }


                





            }

            
            private void dataGridListaProfesora_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
            {
                if (e.PropertyName.Equals("Error") || e.PropertyName.Equals("ListaProfesora") || e.PropertyName.Equals("Active") || e.PropertyName.Equals("IdDodeljenogProfesora") || e.PropertyName.Equals("TipKorisnika")
                    || e.PropertyName.Equals("Termini") || e.PropertyName.Equals("UstanovaZaposlenjaId") || e.PropertyName.Equals("Id") || e.PropertyName.Equals("Lozinka"))
                    e.Column.Visibility = Visibility.Collapsed;
            }
            private void dataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
            {
                if (e.PropertyType == typeof(System.DateTime))
                    (e.Column as DataGridTextColumn).Binding.StringFormat = "HH:mm";

                if (e.PropertyName.Equals("Error") || e.PropertyName.Equals("Active") || e.PropertyName.Equals("ZaduzeniPredavacId") || e.PropertyName.Equals("UstanovaId") || e.PropertyName.Equals("UcionicaId"))
                    e.Column.Visibility = Visibility.Collapsed;
            }

            private void Window_ContentRendered(object sender, EventArgs e)
            {
                /*dataGrid.Columns[0].Header = "Sifra";
                dataGrid.Columns[1].Header = "Pocetak Termina";
                dataGrid.Columns[2].Header = "Kraj Termina";
                dataGrid.Columns[3].Header = "Dan";
                dataGrid.Columns[4].Header = "Tip Nastave";
                dataGrid.Columns[5].Header = "Predavac";

                dataGridListaAsistenata.Columns[0].Header = "Nadlezni Profesor";
                dataGridListaAsistenata.Columns[3].Header = "Ustanova";
                dataGridListaAsistenata.Columns[8].Header = "Korisnicko Ime";
                dataGridListaAsistenata.Columns[9].Header = "E-Mail";
                dataGridListaAsistenata.Columns[10].Header = "Tip Korisnika";*/
            }

            private void tabRaspored_GotFocus(object sender, RoutedEventArgs e)
            {
                dataGrid.Columns[0].Header = "Sifra";
                dataGrid.Columns[1].Header = "Pocetak Termina";
                dataGrid.Columns[2].Header = "Kraj Termina";
                dataGrid.Columns[3].Header = "Dan";
                dataGrid.Columns[4].Header = "Tip Nastave";
                dataGrid.Columns[6].Header = "Predavac";
            }

            

            private void tabItemProfesori_GotFocus(object sender, RoutedEventArgs e)
            {

                dataGridListaProfesora.Columns[3].Header = "Ustanova";
                dataGridListaProfesora.Columns[8].Header = "Korisnicko Ime";
                dataGridListaProfesora.Columns[9].Header = "E-Mail";
                dataGridListaProfesora.Columns[10].Header = "Tip Korisnika";


            }
        }
    }
