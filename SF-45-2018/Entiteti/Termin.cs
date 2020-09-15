using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF_45_2018.Entiteti
{
    public class Termin /*: INotifyPropertyChanged*/
    {
        /*private int _idTermin;

        public int IdTermin
        {
            get { return _idTermin; }
            set { _idTermin = value; }
        }*/
        private int _idTermin;
        public int IdTermin
        {
            get { return _idTermin; }
            set { _idTermin = value; } /*OnPropertyChanged("IdTermin"); */
        }

        private DateTime _vremeZauzecaPocetak;
        public DateTime VremeZauzecaPocetak
        {
            get { return _vremeZauzecaPocetak; }
            set { _vremeZauzecaPocetak = value; /*OnPropertyChanged("VremeZauzecaPocetak"); */}
        }

        private DateTime _vremeZauzecaKraj;
        public DateTime VremeZauzecaKraj
        {
            get { return _vremeZauzecaKraj; }
            set { _vremeZauzecaKraj = value; /*OnPropertyChanged("VrijemeZauzecaPocetak");*/}
        }

        /*private TimeSpan _trajanjeTermina;
        public TimeSpan TrajanjeTermina
        {
            get { return _trajanjeTermina; }
            set { _trajanjeTermina = value; OnPropertyChanged("TrajanjeTermina"); }
        }*/

        private EDaniUnedelji _daniUNedelji;
        public EDaniUnedelji DaniUNedelji
        {
            get { return _daniUNedelji; }
            set { _daniUNedelji = value; /*OnPropertyChanged("DaniUNedelji");*/}
        }

        private ETipNastave _tipNastave;
        public ETipNastave TipNastave
        {
            get { return _tipNastave; }
            set { _tipNastave = value; /*OnPropertyChanged("TipNastave");*/ }
        }

        private bool _active = true;
        public bool Active
        {
            get { return _active; }
            set { _active = value; /*OnPropertyChanged("Active");*/}
        }

        private Korisnik _zaduzeniPredavac;
        public Korisnik ZaduzeniPredavac
        {
            get { return _zaduzeniPredavac; }
            set { _zaduzeniPredavac = value; /*OnPropertyChanged("ZaduzeniPredavac");*/}
        }

        private int _zaduzeniPredavacId;
        public int ZaduzeniPredavacId
        {
            get { return _zaduzeniPredavacId; }
            set { _zaduzeniPredavacId = value; /*OnPropertyChanged("ZaduzeniPredavac");*/}
        }

        private Ustanova _ustanova;

        public Ustanova Ustanova
        {
            get { return _ustanova; }
            set { _ustanova = value; /*OnPropertyChanged("Ustanova");*/}
        }

        private int _ustanovaId;

        public int UstanovaId
        {
            get { return _ustanovaId; }
            set { _ustanovaId = value; /*OnPropertyChanged("Ustanova");*/}
        }

        private Ucionica _ucnionica;

        public Ucionica Ucionica
        {
            get { return _ucnionica; }
            set { _ucnionica = value; /*OnPropertyChanged("Ucionica");*/}
        }

        private int _ucnionicaId;

        public int UcionicaId
        {
            get { return _ucnionicaId; }
            set { _ucnionicaId = value; /*OnPropertyChanged("Ucionica");*/}
        }

        public Termin(int sifra, DateTime VremeOd, DateTime VremeDo, EDaniUnedelji dani, ETipNastave tipNastave, Ustanova ustanova, Ucionica ucionica)
        {
            this.IdTermin = sifra;
            this.VremeZauzecaPocetak = VremeOd;
            this.VremeZauzecaKraj = VremeDo;
            this.DaniUNedelji = dani;
            this.TipNastave = tipNastave;
            this.Ustanova = ustanova;
            this.Ucionica = ucionica;
        }

        public Termin(int sifra, DateTime VremeOd, DateTime VremeDo, int ustanovaID, int ucionicaID)
        {
            this.IdTermin = sifra;
            this.VremeZauzecaPocetak = VremeOd;
            this.VremeZauzecaKraj = VremeDo;
            this.UstanovaId = ustanovaID;
            this.UcionicaId = ucionicaID;
        }

        public Termin(int sifra, DateTime VremeOd, DateTime VremeDo, EDaniUnedelji dani, ETipNastave tipNastave)
        {
            this.IdTermin = sifra;
            this.VremeZauzecaPocetak = VremeOd;
            this.VremeZauzecaKraj = VremeDo;
            this.DaniUNedelji = dani;
            this.TipNastave = tipNastave;
        }

        public Termin(int sifra, DateTime VremeOd, DateTime VremeDo, EDaniUnedelji dani, ETipNastave tipNastave, bool active)
        {
            this.IdTermin = sifra;
            this.VremeZauzecaPocetak = VremeOd;
            this.VremeZauzecaKraj = VremeDo;
            this.DaniUNedelji = dani;
            this.TipNastave = tipNastave;
            this.Active = active;
        }

        public Termin()
        {
            this.IdTermin = 0;
        }

        public virtual Termin Clone()
        {
            Termin termin = new Termin(IdTermin, VremeZauzecaPocetak, VremeZauzecaKraj, DaniUNedelji, TipNastave, Active);
            return termin;
        }

        public override string ToString()
        {
            return VremeZauzecaPocetak.ToShortTimeString() + " - " + VremeZauzecaKraj.ToShortTimeString();
        }

        public string this[string propertyName]
        {
            get
            {
                switch (propertyName)
                {
                    case "VremeZauzecaPocetak":
                        if (Convert.ToString(VremeZauzecaPocetak).Equals(string.Empty))
                            return "Ovo polje je obavezno!";
                        break;
                }
                return null;
            }
        }
        /*public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }*/
    }
}
