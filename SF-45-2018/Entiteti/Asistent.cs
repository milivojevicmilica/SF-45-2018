using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF_45_2018.Entiteti
{
    public class Asistent : Korisnik
    {
        private ObservableCollection<Profesor> _listaProfesora = new ObservableCollection<Profesor>();
        public ObservableCollection<Profesor> ListaProfesora
        {
            get { return _listaProfesora; }
            set { _listaProfesora = value; }
        }
        private Profesor _dodeljeniProfesor;
        public Profesor DodeljeniProfesor
        {
            get { return _dodeljeniProfesor; }
            set { _dodeljeniProfesor = value; }
        }

        private int _idDodeljenogProfesora;

        public int IdDodeljenogProfesora
        {
            get { return _idDodeljenogProfesora; }
            set { _idDodeljenogProfesora = value; }
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

        public Asistent(string ime, string prezime, string korIme, string email) : base(ime, prezime, korIme, email)
        {
            TipKorisnika = ETipKorisnika.ASISTENT;
            Active = true;
        }

        public Asistent(string ime, string prezime, string korIme, string email, string lozinka) : base(ime, prezime, korIme, email, lozinka)
        {
            TipKorisnika = ETipKorisnika.ASISTENT;
            Active = true;
        }

        public Asistent(string ime, string prezime, string korIme, string email, string lozinka, Profesor dodeljeniProfesor, Ustanova ustanovaZaposlenja) : base(ime, prezime, korIme, email, lozinka)
        {
            TipKorisnika = ETipKorisnika.ASISTENT;
             Active = true;
            this.DodeljeniProfesor = dodeljeniProfesor;
            this.UstanovaZaposlenja = ustanovaZaposlenja;
        }


        public Asistent(int id, string ime, string prezime, string korIme, string email, string lozinka) : base(id, ime, prezime, korIme, email, lozinka)
        {
            TipKorisnika = ETipKorisnika.ASISTENT;
            Active = true;
        }
        public Asistent()
        {
            KorisnickoIme = "";
            TipKorisnika = ETipKorisnika.ASISTENT;
              Active = true;

        }

        public override Korisnik Clone()
        {
            Asistent asistent = new Asistent(Id, Ime, Prezime, KorisnickoIme, Email, Lozinka);
            return asistent;
        }

        public override string ToString()
        {
            return this.Ime + " " + this.Prezime;
        }
    }
}
