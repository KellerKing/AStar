using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AStar
{
    class Feldwechsler
    {
        public static List<Feld> SetSpecialFeld(List<Feld> spielfeld, Feld currentFeld, Feldtyp currentFeldtyp)
        {
            if (currentFeldtyp == Feldtyp.Hindernis)
                return AddHindernis(spielfeld, currentFeld);
            else if (currentFeldtyp == Feldtyp.Normal)
                return ResetFeld(spielfeld, currentFeld);

            var oldSpecialFeld = spielfeld.FirstOrDefault(feld => feld.Feldtyp == currentFeldtyp);

            return oldSpecialFeld != null ?
                FormatSpecialFeld(ResetFeld(spielfeld, oldSpecialFeld), currentFeld, currentFeldtyp) :
                FormatSpecialFeld(spielfeld, currentFeld, currentFeldtyp);
        }

        private static List<Feld> FormatSpecialFeld(List<Feld> spielfeld, Feld currentFeld, Feldtyp currentFeldtyp) //TODO: Komplettes Rework 
        {
            currentFeld.Feldtyp = currentFeldtyp;

            switch (currentFeldtyp)
            {
                case Feldtyp.Zielfeld:
                    currentFeld.BackColor = Color.Red;
                    break;
                case Feldtyp.AktuellesFeld:
                    currentFeld.BackColor = Color.Green;
                    break;
            }
            return spielfeld;
        }

        private static List<Feld> AddHindernis(List<Feld> spielfeld, Feld currentFeld)
        {
            currentFeld.BackColor = Color.Gray;
            currentFeld.Feldtyp = Feldtyp.Hindernis;
            return spielfeld;
        }

        private static List<Feld> ResetFeld(List<Feld> spielfeld, Feld currentFeld)
        {
            currentFeld.BackColor = Color.AliceBlue;
            currentFeld.Feldtyp = Feldtyp.Normal;
            return spielfeld;
        }

    }
}
