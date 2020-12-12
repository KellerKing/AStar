using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AStar
{
  class FeldFormatierer
  {
    public static List<Feld> SetSpecialFeld(List<Feld> spielfeld, Feld currentFeld, Feldtyp currentFeldtyp)
    {
      if (currentFeldtyp == Feldtyp.Hindernis)
        return FormatiereAlsHindernis(spielfeld, currentFeld);
      else if (currentFeldtyp == Feldtyp.Normal)
        return FormatiereAlsStandardfeld(spielfeld, currentFeld);

      var oldSpecialFeld = spielfeld.FirstOrDefault(feld => feld.Feldtyp == currentFeldtyp);

      return oldSpecialFeld != null ?
          FormatiereStartOderZielfeld(FormatiereAlsStandardfeld(spielfeld, oldSpecialFeld), currentFeld, currentFeldtyp) :
          FormatiereStartOderZielfeld(spielfeld, currentFeld, currentFeldtyp);
    }

    public static List<Feld> FormatiereStartOderZielfeld(List<Feld> spielfeld, Feld currentFeld, Feldtyp currentFeldtyp) //TODO: Komplettes Rework 
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

    public static List<Feld> FormatiereAlsHindernis(List<Feld> spielfeld, Feld currentFeld)
    {
      currentFeld.BackColor = Color.Gray;
      currentFeld.Feldtyp = Feldtyp.Hindernis;
      return spielfeld;
    }

    public static List<Feld> FormatiereAlsStandardfeld(List<Feld> spielfeld, Feld currentFeld)
    {
      currentFeld.BackColor = Color.AliceBlue;
      currentFeld.Feldtyp = Feldtyp.Normal;
      return spielfeld;
    }

    public static void FormatiereFinalenPfad(List<Feld> derPfad)
    {
      derPfad.ForEach(feld =>
      {
        feld.BackColor = Color.Blue;
        feld.Feldtyp = Feldtyp.Pfad;
      });
    }

    //public static void FormatiereBerechneteFelder(List<Feld> openList)
    //{
    //  openList.Where(feld => feld.Feldtyp != Feldtyp.Zielfeld).ToList().ForEach(feld => feld.BackColor = Color.Yellow);
    //}
  }
}
