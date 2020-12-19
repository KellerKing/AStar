using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AStar
{
  class FeldFormatierer
  {
    public static void SetSpecialFeld(List<Feld> spielfeld, Feld currentFeld, Feldtyp currentFeldtyp)
    {
      if (currentFeldtyp == Feldtyp.Hindernis)
      {
        FormatiereAlsHindernis(currentFeld);
      }
      else if (currentFeldtyp == Feldtyp.Normal)
      {
        FormatiereAlsStandardfeld(currentFeld);
      }
      else //Wenn Startfeld oder Zielfeld gestzt werden muss geschaut werden ob es die schon gibt, und wenn ja das alte erst entfernen
      {
        var oldSpecialFeld = spielfeld.FirstOrDefault(feld => feld.Feldtyp == currentFeldtyp);

        if (oldSpecialFeld != null)
        {
          FormatiereAlsStandardfeld(oldSpecialFeld);
        }
        FormatiereStartOderZielfeld(currentFeld, currentFeldtyp);
      }
    }

    public static void FormatiereStartOderZielfeld(Feld currentFeld, Feldtyp currentFeldtyp)  
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
    }

    public static void FormatiereAlsHindernis(Feld currentFeld)
    {
      currentFeld.BackColor = Color.Gray;
      currentFeld.Feldtyp = Feldtyp.Hindernis;
    }

    public static void FormatiereAlsStandardfeld(Feld currentFeld)
    {
      currentFeld.BackColor = Color.AliceBlue;
      currentFeld.Feldtyp = Feldtyp.Normal;
    }

    public static void FormatiereFinalenPfad(List<Feld> derPfad)
    {
      derPfad.ForEach(feld =>
      {
        feld.BackColor = Color.Blue;
        feld.Feldtyp = Feldtyp.FinalerPfad;
      });
    }
  }
}
