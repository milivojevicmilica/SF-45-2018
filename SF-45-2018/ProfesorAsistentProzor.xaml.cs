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
    /// Interaction logic for ProfesorAsistentProzor.xaml
    /// </summary>
    public partial class ProfesorAsistentProzor : Window
    {
        ICollectionView viewListaAsistenata;
        ICollectionView viewListaProfesora;
        private ICollectionView viewRaspored;
        public static Profesor profesor;
        public static Asistent asistent;
        private ObservableCollection<Termin> rasporedZaOdabranogProfesora;
        private ObservableCollection<Termin> rasporedZaOdabranogAsistenta;
        public ProfesorAsistentProzor(Korisnik korisnik)
        {
            InitializeComponent();

            dataGrid.IsReadOnly = true;
            rasporedZaOdabranogProfesora = new ObservableCollection<Termin>();
            rasporedZaOdabranogAsistenta=new ObservableCollection<Termin>();
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
            if (PodaciKorisnik.AktivniKorisnik.Equals(ETipKorisnika.ASISTENT))
            {

                tabItemAsistenti.Visibility = Visibility.Hidden;
                Asistent a =PodaciKorisnik.PretraziPoIDu(asistent.Id) as Asistent;

                viewListaProfesora = CollectionViewSource.GetDefaultView(a.DodeljeniProfesor.ListaAsistenata);
                dataGridListaProfesora.ItemsSource = viewListaProfesora;
                viewListaProfesora.Filter = CustomFilter;

                textBlockIme.Text = asistent.Ime;
                textBlockPrezime.Text = asistent.Prezime;
                textBlockKorIme.Text = asistent.KorisnickoIme;
                textBlockEmail.Text = asistent.Email;
                textBlockTip.Text = asistent.TipKorisnika.ToString();
            }

            if (PodaciKorisnik.AktivniKorisnik.TipKorisnika.Equals(ETipKorisnika.PROFESOR))
            {
                tabItemProfesori.Visibility = Visibility.Hidden;
                Profesor p = PodaciKorisnik.PretraziPoIDu(profesor.Id) as Profesor;

                viewListaAsistenata = CollectionViewSource.GetDefaultView(p.ListaAsistenata);
                dataGridListaAsistenata.ItemsSource = viewListaAsistenata;
                viewListaAsistenata.Filter = CustomFilter;

                textBlockIme.Text = profesor.Ime;
                textBlockPrezime.Text = profesor.Prezime;
                textBlockKorIme.Text = profesor.KorisnickoIme;
                textBlockEmail.Text = profesor.Email;
                textBlockTip.Text = profesor.TipKorisnika.ToString();

            }


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

        private void buttonDodaj_Click(object sender, RoutedEventArgs e)
        {
            Asistent asistent = new Asistent();

            DodajAsistentaProzor asistentProzor = new DodajAsistentaProzor(asistent);
            asistentProzor.ShowDialog();

            if (asistentProzor.DialogResult == true)
            {
                OsveziProzor();
                viewListaAsistenata.Refresh();

            }

        }

        private void buttonUkloni_Click(object sender, RoutedEventArgs e)
        {
            Korisnik korisnik = dataGridListaAsistenata.SelectedValue as Korisnik;
            if (korisnik != null)
            {
                Korisnik izbrisanKorisnik = PodaciKorisnik.PretraziPoKorImenu(korisnik.KorisnickoIme);
                izbrisanKorisnik.Active = false;

                int indeks = profesor.ListaAsistenata.IndexOf(profesor.ListaAsistenata.Where(u => u.KorisnickoIme.Equals(izbrisanKorisnik.KorisnickoIme)).FirstOrDefault());
                profesor.ListaAsistenata[indeks].Active = false;

               


                viewListaAsistenata.Refresh();
            }
            else
            {
                MessageBox.Show("Ni jedno polje nije obelezeno", "Upozorenje", MessageBoxButton.OK);
            }
        }

        private void buttonTermin_Click(object sender, RoutedEventArgs e)
        {
            if (PodaciKorisnik.AktivniKorisnik.TipKorisnika.Equals(ETipKorisnika.PROFESOR)) {

                profesor = PodaciKorisnik.AktivniKorisnik as Profesor;
                Termin termin = new Termin();
                termin.ZaduzeniPredavac = PodaciKorisnik.AktivniKorisnik;
                termin.Ustanova = profesor.UstanovaZaposlenja;
                TerminiEditProzor terminProzor = new TerminiEditProzor(termin);
                terminProzor.ShowDialog();
                if (terminProzor.DialogResult == true)
                {
                    rasporedZaOdabranogProfesora.Clear();
                    OsveziProzor();
                    viewRaspored.Refresh();
                }
            }
            
            if (PodaciKorisnik.AktivniKorisnik.TipKorisnika.Equals(ETipKorisnika.ASISTENT)) {
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





        }

        private void dataGridListaAsistenata_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Error") || e.PropertyName.Equals("Active") || e.PropertyName.Equals("ListaProfesora") || e.PropertyName.Equals("IdDodeljenogProfesora") || e.PropertyName.Equals("TipKorisnika")
                || e.PropertyName.Equals("Termini") || e.PropertyName.Equals("UstanovaZaposlenjaId") || e.PropertyName.Equals("Id") || e.PropertyName.Equals("Lozinka"))
                e.Column.Visibility = Visibility.Collapsed;
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

        private void tabItemAsistenti_GotFocus(object sender, RoutedEventArgs e)
        {
            
            dataGridListaAsistenata.Columns[3].Header = "Ustanova";
            dataGridListaAsistenata.Columns[8].Header = "Korisnicko Ime";
            dataGridListaAsistenata.Columns[9].Header = "E-Mail";
            dataGridListaAsistenata.Columns[10].Header = "Tip Korisnika";
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
