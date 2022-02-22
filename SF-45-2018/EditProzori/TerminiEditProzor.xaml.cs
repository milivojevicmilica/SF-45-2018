using SF_45_2018.Entiteti;
using SF_45_2018.Logika;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SF_45_2018.EditProzori
{
    /// <summary>
    /// Interaction logic for TerminiEditProzor.xaml
    /// </summary>
    public partial class TerminiEditProzor : Window
    {
        enum Status
        {
            ADD,
            EDIT
        }

        private Status _status;
        private Termin selectedTermin;
        private List<Korisnik> listaPredavaca;
        private List<Ucionica> ucioniceUOdabranojUstanovi;
        private Termin selectedTerminCopy;
        private List<Termin> listaTerminaCopy;
        

        public TerminiEditProzor(Termin termin)
        {
            InitializeComponent();

            OsveziProzor();
            opisVanrednog.Visibility = Visibility.Hidden;

            listaTerminaCopy = new List<Termin>();
            listaTerminaCopy.AddRange(PodaciTermin.listaTermina);

            if (termin.IdTermin.Equals(0))
                this._status = Status.ADD;
            else
            {
                this._status = Status.EDIT;
            }

        
            selectedTermin = termin;
            this.DataContext = termin;
           

            selectedTerminCopy = selectedTermin.Clone();
        }

        public void OsveziProzor()
        {
            listaPredavaca = new List<Korisnik>();
            
            comboBoxDanUNed.ItemsSource = new List<EDaniUnedelji>()
            {
                EDaniUnedelji.PONEDELJAK, EDaniUnedelji.UTORAK,
                EDaniUnedelji.SREDA, EDaniUnedelji.CETVRTAK, EDaniUnedelji.PETAK,
                EDaniUnedelji.SUBOTA, EDaniUnedelji.NEDELJA
            };

            comboBoxTipNastave.ItemsSource = new List<ETipNastave>() { ETipNastave.Predavanja, ETipNastave.Vezbe };
            if (checkBoxVanredno.IsChecked == true)
            {
                opisVanrednog.Visibility = Visibility.Visible;

            }
            if (PodaciKorisnik.AktivniKorisnik.TipKorisnika.Equals(ETipKorisnika.ADMIN))
            {
                

                comboBoxUstanova.ItemsSource = PodaciUstanova.listaUstanova;


                foreach (var predavac in PodaciKorisnik.listaKorisnika)
                {
                    if (predavac.TipKorisnika.Equals(ETipKorisnika.PROFESOR) ||
                        predavac.TipKorisnika.Equals(ETipKorisnika.ASISTENT))
                        listaPredavaca.Add(predavac);
                }

                comboBoxZaduzenaOsoba.ItemsSource = listaPredavaca;
                
            }

            else if (PodaciKorisnik.AktivniKorisnik.TipKorisnika.Equals(ETipKorisnika.PROFESOR))
            {
                Profesor profesor = PodaciKorisnik.AktivniKorisnik as Profesor;
                comboBoxZaduzenaOsoba.ItemsSource = new List<Profesor> { profesor };
                comboBoxZaduzenaOsoba.SelectedIndex = 0;
                comboBoxUstanova.ItemsSource = new List<Ustanova> { profesor.UstanovaZaposlenja };
                comboBoxUstanova.SelectedIndex = 0;
            }
            else if (PodaciKorisnik.AktivniKorisnik.TipKorisnika.Equals(ETipKorisnika.ASISTENT))
            {
                Asistent asistent = PodaciKorisnik.AktivniKorisnik as Asistent;
                comboBoxZaduzenaOsoba.ItemsSource = new List<Asistent> { asistent };
                comboBoxZaduzenaOsoba.SelectedIndex = 0;
                comboBoxUstanova.ItemsSource = new List<Ustanova> { asistent.UstanovaZaposlenja };
                comboBoxUstanova.SelectedIndex = 0;
            }

            if (!(comboBoxUstanova.SelectedIndex == -1))
            {
                ucioniceUOdabranojUstanovi = new List<Ucionica>();

                Ustanova ustanova = comboBoxUstanova.SelectedItem as Ustanova;

                int indeks = PodaciUstanova.listaUstanova.IndexOf(PodaciUstanova.listaUstanova
                    .Where(u => u.SifraUstanove.Equals(ustanova.SifraUstanove)).FirstOrDefault());

                foreach (var ucionica in PodaciUstanova.listaUstanova[indeks].ListaUcionica)
                {
                    ucioniceUOdabranojUstanovi.Add(ucionica);
                }

                comboBoxUcionica.ItemsSource = ucioniceUOdabranojUstanovi;
            }
        }

        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
            /*Termin termin = PodaciTermin.PretraziPoIdu(Convert.ToInt32(textBoxSifra.Text));


            if (termin != null && _status.Equals(Status.ADD))
            {
                MessageBox.Show($"Termin sa sifrom {termin.IdTermin} vec postoji", "Upozorenje",
                    MessageBoxButton.OK);
                return;
            }*/
            

            if (comboBoxDanUNed.SelectedIndex < 0 || comboBoxTipNastave.SelectedIndex < 0 ||
                comboBoxUcionica.SelectedIndex < 0 || comboBoxUstanova.SelectedIndex < 0 ||
                comboBoxZaduzenaOsoba.SelectedIndex < 0)
            {
                MessageBox.Show("Niste popunili sva polja!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!(ValidacionaPravila.Termin.regex.IsMatch(TextBoxVremePocetka
                    .Text)) || !(ValidacionaPravila.Termin.regex.IsMatch(TextBoxVremeKraja.Text)))
            {
                MessageBox.Show("Format za vreme staviti kao hh:mm AM/PM", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_status.Equals(Status.ADD))
            {
                DateTime pocetakRadnogVremena =
                    new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 07, 00, 00);
                DateTime krajRadnogVremena =
                    new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 20, 00, 00);

                DateTime vremeOd = Convert.ToDateTime(TextBoxVremePocetka.Text);
                DateTime vremeDo = Convert.ToDateTime(TextBoxVremeKraja.Text);

                if (vremeOd.TimeOfDay >= pocetakRadnogVremena.TimeOfDay &&
                    vremeDo.TimeOfDay <= krajRadnogVremena.TimeOfDay)
                {
                    if ((comboBoxDanUNed.SelectedItem.ToString().ToLower() ==
                         EDaniUnedelji.SUBOTA.ToString().ToLower() && checkBoxVanredno.IsChecked == false)
                        || (comboBoxDanUNed.SelectedItem.ToString().ToLower() ==
                            EDaniUnedelji.NEDELJA.ToString().ToLower() && checkBoxVanredno.IsChecked == false))
                    {
                       
                        MessageBox.Show(
                            "Ukoliko zelite da zauzmete termin subotom ili nedeljom, obelezite polje 'Vanredni Termin' ",
                            "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    int result = DateTime.Compare(vremeOd, vremeDo);

                    TimeSpan vremeTrajanjaTermina = PodaciTermin.IzracunajTrajanje(vremeDo, vremeOd);



                    foreach (var t in PodaciTermin.listaTermina)
                    {


                        if (t.Active.Equals(true) && vremeOd.Ticks >= t.VremeZauzecaPocetak.Ticks &&
                            vremeDo.Ticks <= t.VremeZauzecaKraj.Ticks &&
                            t.DaniUNedelji.ToString().ToLower() == comboBoxDanUNed.SelectedValue.ToString().ToLower()
                            && vremeOd.Ticks < vremeDo.Ticks)
                        {
                            MessageBox.Show("Termin u tom vremenskom razdoblju vec postoji", "Greska",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        if (t.Active.Equals(true) && comboBoxDanUNed.SelectedItem.ToString().ToLower() == t.DaniUNedelji.ToString().ToLower() &&
                            vremeOd.Ticks <= t.VremeZauzecaPocetak.Ticks &&
                            vremeDo.Ticks >= t.VremeZauzecaPocetak.Ticks &&
                             vremeDo.Ticks <= t.VremeZauzecaKraj.Ticks)
                        {
                            MessageBox.Show("Termin u tom vremenskom razdoblju vec postoji", "Greska",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        if (t.Active.Equals(true) && comboBoxDanUNed.SelectedItem.ToString().ToLower() == t.DaniUNedelji.ToString().ToLower() &&
                            vremeOd.Ticks >= t.VremeZauzecaPocetak.Ticks &&
                            vremeOd.Ticks <= t.VremeZauzecaKraj.Ticks &&
                            vremeDo.Ticks >= t.VremeZauzecaKraj.Ticks)
                        {
                            MessageBox.Show("Termin u tom vremenskom razdoblju vec postoji", "Greska",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        if (t.Active.Equals(true) && comboBoxDanUNed.SelectedItem.ToString().ToLower() == t.DaniUNedelji.ToString().ToLower() &&
                            vremeOd.Ticks <= t.VremeZauzecaPocetak.Ticks &&
                            vremeDo.Ticks >= t.VremeZauzecaKraj.Ticks)
                        {
                            MessageBox.Show("Termin u tom vremenskom razdoblju vec postoji", "Greska",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }



                    if (result < 0)
                    {

                        BindingExpression dan = comboBoxDanUNed.GetBindingExpression(Selector.SelectedItemProperty);
                        dan.UpdateSource();


                        selectedTermin.VremeZauzecaPocetak = vremeOd;
                        selectedTermin.VremeZauzecaKraj = vremeDo;

                        Ucionica ucionica = comboBoxUcionica.SelectedValue as Ucionica;
                        selectedTermin.UcionicaId = ucionica.IdUcionice;

                        Ustanova ustanova = comboBoxUstanova.SelectedValue as Ustanova;
                        selectedTermin.UstanovaId = ustanova.SifraUstanove;

                        Korisnik predavac = comboBoxZaduzenaOsoba.SelectedValue as Korisnik;
                        selectedTermin.ZaduzeniPredavacId = predavac.Id;

                        PodaciTermin.DodajTermine(selectedTermin);
                        PodaciTermin.listaTermina.Clear();
                        PodaciTermin.UcitajTermine();

                        this.DialogResult = true;
                        this.Close();
                    }

                    else if (result > 0 || result == 0)
                    {
                        MessageBox.Show("Pocetak termina mora biti pre zavrsetka!", "Greska!", MessageBoxButton.OK,
                            MessageBoxImage.Error);
                        return;
                    }
                }

                else
                {
                    MessageBox.Show(
                        "Radno vreme je definisano od 07:00-20:00, molimo vas zauzmite termine u tom opsegu",
                        "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }
            else if (_status.Equals(Status.EDIT))
            {

                Debug.WriteLine("Pocetak originalnog termina: {0}", selectedTermin.VremeZauzecaPocetak);
                Debug.WriteLine("Pocetak kopije: {0}", selectedTerminCopy.VremeZauzecaPocetak);
                Debug.WriteLine("Kraj originalnog termina: {0}", selectedTermin.VremeZauzecaKraj);
                Debug.WriteLine("Kraj kopije: {0}", selectedTerminCopy.VremeZauzecaKraj);

                DateTime pocetakRadnogVremena =
                    new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 07, 00, 00);
                DateTime KrajRadnogVremena =
                    new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 20, 00, 00);

                DateTime vremeOd = Convert.ToDateTime(TextBoxVremePocetka.Text);
                DateTime vremeDo = Convert.ToDateTime(TextBoxVremeKraja.Text);

                if (vremeOd.TimeOfDay >= pocetakRadnogVremena.TimeOfDay &&
                    vremeDo.TimeOfDay <= KrajRadnogVremena.TimeOfDay)
                {
                    if ((comboBoxDanUNed.SelectedItem.ToString().ToLower() ==
                         EDaniUnedelji.SUBOTA.ToString().ToLower() && checkBoxVanredno.IsChecked == false)
                        || (comboBoxDanUNed.SelectedItem.ToString().ToLower() ==
                            EDaniUnedelji.NEDELJA.ToString().ToLower() && checkBoxVanredno.IsChecked == false))
                    {
                        MessageBox.Show(
                            "Ukoliko zelite da zauzmete termin subotom ili nedeljom, obelezite polje 'Vanredni Termin' ",
                            "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    int result = DateTime.Compare(vremeOd, vremeDo);

                    TimeSpan vremeTrajanjaTermina = PodaciTermin.IzracunajTrajanje(vremeDo, vremeOd);

                    if (result > 0 || result == 0)
                    {
                        MessageBox.Show("Pocetak termina mora biti pre zavrsetka!", "Greska!", MessageBoxButton.OK,
                            MessageBoxImage.Error);
                        return;
                    }

                    if (vremeOd.TimeOfDay >= selectedTerminCopy.VremeZauzecaPocetak.TimeOfDay &&
                        vremeDo.TimeOfDay <= selectedTerminCopy.VremeZauzecaKraj.TimeOfDay)
                    {
                        this.DialogResult = true;
                        this.Close();
                    }

                    else
                    {
                        foreach (var t in listaTerminaCopy)
                        {

                            if (t.Active.Equals(true) && vremeOd.Ticks >= t.VremeZauzecaPocetak.Ticks && vremeDo.Ticks <=
                                t.VremeZauzecaKraj.Ticks
                                && t.DaniUNedelji.ToString()
                                    .ToLower() ==
                                comboBoxDanUNed.SelectedValue
                                    .ToString()
                                    .ToLower() &&
                                vremeOd.Ticks < vremeDo.Ticks)
                            {
                                MessageBox.Show("Termin u tom vremenskom razdoblju vec postoji", "Greska",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }

                            if (t.Active.Equals(true) && comboBoxDanUNed.SelectedItem.ToString().ToLower() ==
                                t.DaniUNedelji.ToString().ToLower() &&
                                vremeOd.Ticks <= t.VremeZauzecaPocetak.Ticks &&
                                (vremeDo.Ticks >= t.VremeZauzecaPocetak.Ticks &&
                                 vremeDo.Ticks <= t.VremeZauzecaKraj.Ticks))
                            {
                                MessageBox.Show("Termin u tom vremenskom razdoblju vec postoji", "Greska",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }

                            if ((t.Active.Equals(true) && comboBoxDanUNed.SelectedItem.ToString().ToLower() == t.DaniUNedelji.ToString().ToLower() &&
                                 vremeOd.Ticks >= t.VremeZauzecaPocetak.Ticks &&
                                 vremeOd.Ticks <= t.VremeZauzecaKraj.Ticks) &&
                                vremeDo.Ticks >= t.VremeZauzecaKraj.Ticks)
                            {
                                MessageBox.Show("Termin u tom vremenskom razdoblju vec postoji", "Greska",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                            if (t.Active.Equals(true) && comboBoxDanUNed.SelectedItem.ToString().ToLower() == t.DaniUNedelji.ToString().ToLower() &&
                                vremeOd.Ticks <= t.VremeZauzecaPocetak.Ticks &&
                                vremeDo.Ticks >= t.VremeZauzecaKraj.Ticks)
                            {
                                MessageBox.Show(" Termin u tom vremenskom razdoblju vec postoji", "Greska",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                        }

                        if (result < 0)
                        {
                            BindingExpression pocetak =
                                TextBoxVremePocetka.GetBindingExpression(TextBox.TextProperty);
                            pocetak.UpdateSource();

                            BindingExpression kraj = TextBoxVremeKraja.GetBindingExpression(TextBox.TextProperty);
                            kraj.UpdateSource();

                            BindingExpression dan = comboBoxDanUNed.GetBindingExpression(Selector.SelectedItemProperty);
                            dan.UpdateSource();


                            selectedTermin.VremeZauzecaPocetak = vremeOd;
                            selectedTermin.VremeZauzecaKraj = vremeDo;

                            Ucionica ucionica = comboBoxUcionica.SelectedValue as Ucionica;
                            selectedTermin.UcionicaId = ucionica.IdUcionice;

                            Ustanova ustanova = comboBoxUstanova.SelectedValue as Ustanova;
                            selectedTermin.UstanovaId = ustanova.SifraUstanove;

                            Korisnik korisnik = comboBoxZaduzenaOsoba.SelectedValue as Korisnik;
                           

                            selectedTermin.ZaduzeniPredavacId = korisnik.Id;


                            PodaciTermin.IzmeniTermin(selectedTermin);

                            //PodaciTermin.listaTermina.Add(selectedTermin);
                            this.DialogResult = true;
                            this.Close();
                        }

                     
                    }




                
                }

                else
                {
                    MessageBox.Show(
                        "Radno vreme je definisano od 07:00-20:00, molimo vas zauzmite termine u tom opsegu",
                        "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }

        }
        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void comboBoxUstanova_DropDownClosed_1(object sender, EventArgs e)
        {
            if (!(comboBoxUstanova.SelectedIndex == -1))
            {
                ucioniceUOdabranojUstanovi = new List<Ucionica>();

                Ustanova ustanova = comboBoxUstanova.SelectedItem as Ustanova;

                int indeks = PodaciUstanova.listaUstanova.IndexOf(PodaciUstanova.listaUstanova
                    .Where(u => u.SifraUstanove.Equals(ustanova.SifraUstanove)).FirstOrDefault());

                foreach (var ucionica in PodaciUstanova.listaUstanova[indeks].ListaUcionica)
                {
                    ucioniceUOdabranojUstanovi.Add(ucionica);
                }

                comboBoxUcionica.ItemsSource = ucioniceUOdabranojUstanovi;
            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            if (!(comboBoxUstanova.SelectedIndex == -1))
            {
                ucioniceUOdabranojUstanovi = new List<Ucionica>();

                Ustanova ustanova = comboBoxUstanova.SelectedItem as Ustanova;

                int indeks = PodaciUstanova.listaUstanova.IndexOf(PodaciUstanova.listaUstanova
                    .Where(u => u.SifraUstanove.Equals(ustanova.SifraUstanove)).FirstOrDefault());

                foreach (var ucionica in PodaciUstanova.listaUstanova[indeks].ListaUcionica)
                {
                    ucioniceUOdabranojUstanovi.Add(ucionica);
                }

                comboBoxUcionica.ItemsSource = ucioniceUOdabranojUstanovi;
            }
        }
    }
}

