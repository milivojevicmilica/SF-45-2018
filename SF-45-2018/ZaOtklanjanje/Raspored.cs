using SF_45_2018.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF_45_2018.ZaOtklanjanje
{
    class Raspored
    {
        private int _rasporedId;

        public int RasporedId
        {
            get { return _rasporedId; }
            set { _rasporedId = value; }
        }

        private Ustanova _odabranaUstanova;
        public Ustanova OdabranaUstanova
        {
            get { return _odabranaUstanova; }
            set { _odabranaUstanova = value; }
        }

        private int _odabranaUstanovaId;
        public int OdabranaUstanovaId
        {
            get { return _odabranaUstanovaId; }
            set { _odabranaUstanovaId = value; }
        }

        private Termin _zakazaniTermin;
        public Termin ZakazaniTremin
        {
            get { return _zakazaniTermin; }
            set { _zakazaniTermin = value; }
        }

        private int _zakazaniTerminId;
        public int ZakazaniTreminId
        {
            get { return _zakazaniTerminId; }
            set { _zakazaniTerminId = value; }
        }

        private Ucionica _odabranaUcionica;

        public Ucionica OdabranaUcionica
        {
            get { return _odabranaUcionica; }
            set { _odabranaUcionica = value; }
        }

        private int _odabranaUcionicaId;

        public int OdabranaUcionicaId
        {
            get { return _odabranaUcionicaId; }
            set { _odabranaUcionicaId = value; }
        }

        private Korisnik _predavac;
        public Korisnik Predavac
        {
            get { return _predavac; }
            set { _predavac = value; }
        }

        private int _predavacId;
        public int PredavacId
        {
            get { return _predavacId; }
            set { _predavacId = value; }
        }

        private bool _active;

        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }

        public Raspored(Ustanova ustanova, Termin termin, Ucionica ucionica, Korisnik predavac)
        {
            OdabranaUstanova = ustanova;
            OdabranaUcionica = ucionica;
            ZakazaniTremin = termin;
            Predavac = predavac;
        }

        public Raspored(int idRaspored, int ustanovaId, int terminId, int ucionicaId, int predavacId)
        {
            this._rasporedId = idRaspored;
            this.OdabranaUstanovaId = ustanovaId;
            this._zakazaniTerminId = terminId;
            this.OdabranaUcionicaId = ucionicaId;
            this.PredavacId = predavacId;
        }
    }
}
