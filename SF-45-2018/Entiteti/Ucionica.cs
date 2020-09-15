using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF_45_2018.Entiteti
{
    public class Ucionica : INotifyPropertyChanged
    {

        /* private int _idUcionice;

        public int IdUcionice
        {
            get { return _idUcionice; }
            set { _idUcionice = value; }
        }*/
        private int _idUcionice;

        public int IdUcionice
        {
            get { return _idUcionice; }
            set
            {
                _idUcionice = value;
                OnPropertyChanged("IdUcionice");
            }
        }

        private string _brojUcionice;

        public string BrojUcionice
        {
            get { return _brojUcionice; }
            set
            {
                _brojUcionice = value;
                OnPropertyChanged("BrojUcionice");
            }
        }

        private int _brojMesta;

        public int BrojMesta
        {
            get { return _brojMesta; }
            set
            {
                _brojMesta = value;
                OnPropertyChanged("BrojMesta");
            }
        }

        private ETipUcionice _tipUcionice;

        public ETipUcionice TipUcionice
        {
            get { return _tipUcionice; }
            set
            {
                _tipUcionice = value;
                OnPropertyChanged("TipUcionice");
            }
        }

        private bool _active = true;

        public bool Active
        {
            get { return _active; }
            set
            {
                _active = value;
                OnPropertyChanged("Active");
            }
        }

        private Ustanova _ustanovaGdeSeNalazi;

        public Ustanova UstanovaGdeSeNalazi
        {
            get { return _ustanovaGdeSeNalazi; }
            set
            {
                _ustanovaGdeSeNalazi = value;
                OnPropertyChanged("UstanovaGdeSeNalazi");
            }
        }

        private int _ustanovaGdeSeNalaziId;

        public int UstanovaGdeSeNalaziId
        {
            get { return _ustanovaGdeSeNalaziId; }
            set
            {
                _ustanovaGdeSeNalaziId = value;
                OnPropertyChanged("UstanovaGdeSeNalaziId");
            }
        }

        public Ucionica(int id, string brojUcionice, int brojMesta, ETipUcionice tipUcionice)
        {
            this.IdUcionice = id;
            this.BrojUcionice = brojUcionice;
            this.BrojMesta = brojMesta;
            this.TipUcionice = tipUcionice;
        }

        public Ucionica(int id, string brojUcionice, int brojMesta, ETipUcionice tipUcionice, bool active)
        {
            this.IdUcionice = id;
            this.BrojUcionice = brojUcionice;
            this.BrojMesta = brojMesta;
            this.TipUcionice = tipUcionice;
            this.Active = active;
        }

        public Ucionica()
        {
            this.IdUcionice = 0;
        }

        public Ucionica Clone()
        {
            Ucionica ucionica = new Ucionica(IdUcionice, BrojUcionice, BrojMesta, TipUcionice, Active);
            this.Active = true;
            return ucionica;
        }

        public override string ToString()
        {
            return "Ucionica " + this.BrojUcionice;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Error
        {
            get { return null; }
        }

        public string this[string propertyName]
        {
            get
            {
                switch (propertyName)
                {
                    case "BrojUcionice":
                        if (BrojUcionice.Equals(string.Empty))
                            return "Ovo polje je obavezno!";
                        break;
                }
                return null;
            }
        }
    }
}
