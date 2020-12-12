using AStar.Konstanten;
using AStar.Transferobjekte;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AStar
{
  public class AStar
  {
    public static ZielSucheDTO CheckIfZielGefunden(List<Feld> openList, Feld zielfeld)
    {
      var zielsuchErgebnis = ZielsuchErgebnis.NichtGefunden;
      var ausgabetext = "";

      if (!openList.Any())
      {
        zielsuchErgebnis = ZielsuchErgebnis.KeinWegMoeglich;
        ausgabetext = "Ups, leider kann das Ziel nicht erreicht werden";
      }
      else if (openList.Any(feld => feld.X == zielfeld.X && feld.Y == zielfeld.Y))
      {
        zielsuchErgebnis = ZielsuchErgebnis.Gefunden;
        ausgabetext = "Supi, das Ziel wurde gefunden !";
      }
      return new ZielSucheDTO
      {
        ZielsuchErgebnis = zielsuchErgebnis,
        Ausgabetext = ausgabetext
      };
    } //TODO: Machen !



    public static List<Feld> GetBetretbareUmliegendeFelder(List<Feld> spielfeld, Feld currentFeld)
    {
      var currentFieldX = currentFeld.X;
      var currentFieldY = currentFeld.Y;

      var maxX = Math.Min(spielfeld.Max(f => f.X), currentFieldX + 1);
      var minX = Math.Max(currentFieldX - 1, 0);

      var maxY = Math.Min(spielfeld.Max(f => f.Y), currentFieldY + 1);
      var minY = Math.Max(currentFieldY - 1, 0);

      var nearestFields = spielfeld.Where(f => f.X >= minX && f.X <= maxX &&
                                          f.Y >= minY && f.Y <= maxY).ToList();

      return nearestFields.Where(f => f.Feldtyp != Feldtyp.Hindernis &&
                                      f.Feldtyp != Feldtyp.AktuellesFeld).ToList();
    }

    //TODO: Muss das wirklich eine Eigene Methode sein ? 
    public static void SetVorgaenger(Feld currentFeld, Feld vorgaengerFeld)
    {
      currentFeld.Vorgaenger = vorgaengerFeld;
    }

    public static PathScoreDTO CalculateSinglePathScore(Feld zielFeld, Feld currentFeld)
    {
      var myG = CalculateGScore(currentFeld);
      var myH = CalculateHScore(currentFeld, zielFeld);

      return new PathScoreDTO
      {
        G = myG,
        H = myH,
        F = myH + myG
      };
    }

    private static int CalculateGScore(Feld currentFeld)
    {
      const int kostenHorizontalVertikal = 10;
      const int kostenDiagonal = 14;

      return Helper.IsFeldDiagonalZumVorgaenger(currentFeld) ? kostenDiagonal + currentFeld.Vorgaenger.G : kostenHorizontalVertikal + currentFeld.Vorgaenger.G;
    }

    private static int CalculateHScore(Feld currentFeld, Feld zielFeld)
    {
      return 10 * (Math.Abs(zielFeld.X - currentFeld.X) +
                  Math.Abs(zielFeld.Y - currentFeld.Y));
    }



  }
}
