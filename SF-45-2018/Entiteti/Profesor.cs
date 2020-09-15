using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF_45_2018.Entiteti
{
    public class Profesor : Korisnik
    {
        private ObservableCollection<Asistent> _listaAsistenata = new ObservableCollection<Asistent>();
        public ObservableCollection<Asistent> ListaAsistenata
        {
            get { return _listaAsistenata; }
            set { _listaAsistenata = value; }
        }

        private List<Termin> _termini = new List<Termin>();
        public List<Termin> Termini
        {
            get { return _termini; }
            set { _termini = value; }
        }





        private Ustanova _ustanovaZaposlenja;
        public Ustanova UstanovaZaposlenja
        {
            get { return _ustanovaZaposlenja; }
            set { _ustanovaZaposlenja = value; }
        }

        private int _ustanovaZaposlenjaId;
        public int UstanovaZaposlenjaId
        {
            get { return _ustanovaZaposlenjaId; }
            set { _ustanovaZaposlenjaId = value; }
        }

        public Profesor(string ime, string prezime, string korIme, string email) : base(ime, prezime, korIme, email)
        {
            TipKorisnika = ETipKorisnika.PROFESOR;
            Active = true;
        }

        public Profesor(string ime, string prezime, string korIme, string email, string lozinka) : base(ime, prezime, korIme, email, lozinka)
        {
            TipKorisnika = ETipKorisnika.PROFESOR;
            Active = true;
        }

        public Profesor()
        {
            KorisnickoIme = "";
            TipKorisnika = ETipKorisnika.PROFESOR;
            Active = true;
        }

        public Profesor(int id, string ime, string prezime, string korIme, string email, string lozinka) : base(id, ime, prezime, korIme, email, lozinka)
        {
            TipKorisnika = ETipKorisnika.PROFESOR;
            Active = true;
        }

        public override Korisnik Clone()
        {
            Profesor profa = new Profesor(Id, Ime, Prezime, KorisnickoIme, Email, Lozinka);
            return profa;

        }

        public override string ToString()
        {
            return this.Ime + " " + this.Prezime;
        }
    }
}
