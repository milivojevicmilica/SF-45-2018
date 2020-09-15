using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF_45_2018.Entiteti
{
    public abstract class Korisnik : INotifyPropertyChanged/* IDataErrorInfo*/
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _ime;
        public string Ime
        {
            get { return _ime; }
            set { _ime = value; OnPropertyChanged("Ime"); }
        }

        private string _prezime;
        public string Prezime
        {
            get { return _prezime; }
            set { _prezime = value; OnPropertyChanged("Prezime"); }
        }

        private string _korisnickoIme;
        public string KorisnickoIme
        {
            get { return _korisnickoIme; }
            set { _korisnickoIme = value; OnPropertyChanged("KorisnickoIme"); }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged("Email"); }
        }

        private ETipKorisnika _tipKorisnika;
        public ETipKorisnika TipKorisnika
        {
            get { return _tipKorisnika; }
            set { _tipKorisnika = value; OnPropertyChanged("TipKorisnika"); }
        }

        private string _lozinka;
        public string Lozinka
        {
            get { return _lozinka; }
            set { _lozinka = value; OnPropertyChanged("Lozinka"); }
        }


        private bool _active = true;
        public bool Active
        {
            get { return _active; }
            set
            { _active = value; OnPropertyChanged("Active"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }


        public Korisnik(string ime, string prezime, string korIme, string email)
        {
            this.Ime = ime;
            this.Prezime = prezime;
            this.KorisnickoIme = korIme;
            this.Email = email;
        }

        public Korisnik(string ime, string prezime, string korIme, string email, string lozinka)
        {
            this.Ime = ime;
            this.Prezime = prezime;
            this.KorisnickoIme = korIme;
            this.Email = email;
            this.Lozinka = lozinka;
        }

        public Korisnik(int id, string ime, string prezime, string korIme, string email, string lozinka)
        {
            this.Id = id;
            this.Ime = ime;
            this.Prezime = prezime;
            this.KorisnickoIme = korIme;
            this.Email = email;
            this.Lozinka = lozinka;
        }

        public Korisnik()
        {
            this.KorisnickoIme = "";
        }


        /**public string this[string propertyName]
        {
            get
            {
                switch (propertyName)
                {
                    case "Ime":
                        if (Ime.Equals(string.Empty))
                        
            return "Ovo polje je obavezno!";
                        break;
                    case "Prezime":
                        if (Prezime.Equals(string.Empty))
                            return "Ovo polje je obavezno!";
                        break;
                    case "KorisnickoIme":
                        if (KorisnickoIme.Equals(string.Empty))
                            return "Ovo polje je obavezno!";
                        break;
                    case "Lozinka":
                        if (Lozinka.Equals(string.Empty))
                            return "Ovo polje je obavezno!";
                        break;
                }
                return null;

            }

        }*/

        public string Error
        {
            get { return "Neispravni Podaci"; }
        }

        public virtual Korisnik Clone()
        {
            return null;
        }


    }
}
