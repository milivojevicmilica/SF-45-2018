using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF_45_2018.Entiteti
{
    public class Administrator : Korisnik
    {
        private int _idAmin;

        public int IdAdmin
        {
            get { return _idAmin; }
            set { _idAmin = value; }
        }
        public Administrator(string ime, string prezime, string korIme, string email) : base(ime, prezime, korIme, email)
        {
            this.TipKorisnika = ETipKorisnika.ADMIN;
            Active = true;
        }

        public Administrator(string ime, string prezime, string korIme, string email, string lozinka) : base(ime, prezime, korIme, email, lozinka)
        {
            this.TipKorisnika = ETipKorisnika.ADMIN;
            Active = true;
        }

        public Administrator()
        {
            this.KorisnickoIme = "";
            this.TipKorisnika = ETipKorisnika.ADMIN;
            Active = true;
        }

        public Administrator(int id, string ime, string prezime, string korIme, string email, string lozinka) : base(id, ime, prezime, korIme, email, lozinka)
        {
            this.TipKorisnika = ETipKorisnika.ADMIN;
            Active = true;
        }

        public override Korisnik Clone()
        {
            Administrator admin = new Administrator(Id, Ime, Prezime, KorisnickoIme, Email, Lozinka);
            return admin;
        }

        public override string ToString()
        {
            return this.Ime + " " + this.Prezime;
        }

    }
}
