using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF_45_2018.Entiteti
{
    public class Ustanova : INotifyPropertyChanged /**IDataErrorInfo*/
    {
        private int _sifraUstanove;
        public int SifraUstanove
        {
            get { return _sifraUstanove; }
            set { _sifraUstanove = value; }
        }

        private string _naziv;
        public string Naziv
        {
            get { return _naziv; }
            set { _naziv = value; }
        }

        private string _lokacija;
        public string Lokacija
        {
            get { return _lokacija; }
            set { _lokacija = value; }
        }

        private bool _active = true;
        public bool Active
        {
            get { return _active; }
            set { _active = value; OnPropertyChanged("Active"); }
        }

        private ObservableCollection<Ucionica> _listaUcionica = new ObservableCollection<Ucionica>();
        public ObservableCollection<Ucionica> ListaUcionica
        {
            get { return _listaUcionica; }
            set { _listaUcionica = value; OnPropertyChanged("listUcionica"); }
        }



        private List<Korisnik> _zaposlenaLica = new List<Korisnik>();
        public List<Korisnik> ZaposenaLica
        {
            get { return _zaposlenaLica; }
            set { _zaposlenaLica = value; OnPropertyChanged("zaposlenaLica"); }

        }

        private List<Termin> _termini = new List<Termin>();
        public List<Termin> Termini
        {
            get { return _termini; }
            set { _termini = value; OnPropertyChanged("Termini"); }
        }

        private int _maksimalanBrojUcionica;

        public int MaksimalanBrojUcionica
        {
            get { return _maksimalanBrojUcionica; }
            set { _maksimalanBrojUcionica = value; OnPropertyChanged("MaksimalanBrojUcionica"); }
        }

        public Ustanova(int sifraUstanove, string naziv, string lokacija, ObservableCollection<Ucionica> listaUcionica)
        {
            this.SifraUstanove = sifraUstanove;
            this.Naziv = naziv;
            this.Lokacija = lokacija;
            this.ListaUcionica = listaUcionica;
            Active = true;
        }

        public Ustanova(int sifraUstanove, string naziv, string lokacija)
        {
            this.SifraUstanove = sifraUstanove;
            this.Naziv = naziv;
            this.Lokacija = lokacija;
            Active = true;
        }

        public Ustanova(int sifraUstanove, string naziv, string lokacija, ObservableCollection<Ucionica> listaUcionica, int brUcionica)
        {
            this.SifraUstanove = sifraUstanove;
            this.Naziv = naziv;
            this.Lokacija = lokacija;
            this.MaksimalanBrojUcionica = brUcionica;
            this.ListaUcionica = listaUcionica;
            Active = true;
        }

        public Ustanova()
        {
            this.Naziv = "";
            Active = true;

        }

        public Ustanova Clone()
        {
            Ustanova ustanova = new Ustanova(SifraUstanove, Naziv, Lokacija, ListaUcionica, MaksimalanBrojUcionica);
            return ustanova;
        }

        public override string ToString()
        {
            return String.Format("{0}, {1}", Naziv, Lokacija);
        }



        public string Error
        {
            get { return null; }
        }

        /**public string this[string propertyName]
        {
            get
            {
                switch (propertyName)
                {
                    case "Naziv":
                        if (Naziv.Equals(string.Empty))
                            return "Ovo polje je obavezno!";
                        break;
                    case "Lokacija":
                        if (Lokacija.Equals(String.Empty))
                            return "Ovo polje je obavezno!";
                        break;
                }
                return null;
            }
        }*/

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }



    }
}
