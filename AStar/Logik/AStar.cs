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


    public static List<Feld> FuegeFelderDerClosedListHinzu(Feld feldZumHinzufügen, List<Feld> closedList)
    {
      closedList.Add(feldZumHinzufügen);
      return closedList;
    }


    //public static bool CheckIfZielGefunden(List<Feld> openList, Feld zielfeld)
    //{
    //  var result = openList.Any(feld => feld.X == zielfeld.X && feld.Y == zielfeld.Y);
    //  return result;
    //} //TODO: Machen !

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



    public static IEnumerable<Feld> GetBetretbareUmliegendeFelder(List<Feld> spielfeld, Feld currentFeld)
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
                                      f.Feldtyp != Feldtyp.AktuellesFeld);
    }

    public static Feld SetVorgaenger(Feld currentFeld, Feld vorgaengerFeld)
    {
      currentFeld.Vorgaenger = vorgaengerFeld;
      return currentFeld;
    }

    public static List<Feld> CalulatePathScores(List<Feld> openList, Feld zielfed)
    {
      var kopieOpenList = new List<Feld>(openList);
      foreach (var item in kopieOpenList)
      {
        item.G = CalculateGScore(item);
        item.H = CalculateHScore(item, zielfed);
        item.F = item.G + item.H;
      }
      return kopieOpenList;
    }

    public static PathScoreDTO CalculateSinglePathScore(Feld zielfeld, Feld aktuellesFeld)
    {
      var myG = CalculateGScore(aktuellesFeld);
      var myH = CalculateHScore(aktuellesFeld, zielfeld);

      return new PathScoreDTO
      {
        G = myG,
        H = myH,
        F = myH + myG
      };
    }

    private static int CalculateGScore(Feld feld)
    {
      const int kostenHorizontalVertikal = 10;
      const int kostenDiagonal = 14;

      return Helper.IsFeldDiagonalZumVorgaenger(feld) ? kostenDiagonal + feld.Vorgaenger.G : kostenHorizontalVertikal + feld.Vorgaenger.G;
    }

    private static int CalculateHScore(Feld feld, Feld zielFeld) //TODO: Was anders als Manhattan nehmen
    {
      return 10 * (Math.Abs(zielFeld.X - feld.X) +
                  Math.Abs(zielFeld.Y - feld.Y));
    }



  }
}
