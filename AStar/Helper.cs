using System;
using System.Collections.Generic;
using System.Linq;

namespace AStar
{
  public class Helper
  {
    public static Tuple<int, int> CalculateWindowSize(int anzahlFelderProAchse, int padding, int feldSize)
    {
      var neededHeight = anzahlFelderProAchse * feldSize + (2 * padding) + 40;
      var neededWidht = anzahlFelderProAchse * feldSize + (2 * padding) + 120;

      return Tuple.Create(neededWidht, neededHeight);
    }

    public static bool IsFeldDiagonalZumVorgaenger(Feld feld)
    {
      var vorgaengerX = feld.Vorgaenger.myPosX;
      var vorgaengerY = feld.Vorgaenger.myPosY;

      if (vorgaengerX == feld.myPosX - 1 && vorgaengerY == feld.myPosY - 1) return true;
      else if (vorgaengerX == feld.myPosX + 1 && vorgaengerY == feld.myPosY - 1) return true;
      else if (vorgaengerX == feld.myPosX + 1 && vorgaengerY == feld.myPosY + 1) return true;
      else if (vorgaengerX == feld.myPosX - 1 && vorgaengerY == feld.myPosY + 1) return true;
      else return false;
    }


    public static List<Feld> GetBetretbareUmliegendeFelder(List<Feld> spielfeld, Feld currentFeld)
    {
      var currentFieldX = currentFeld.myPosX;
      var currentFieldY = currentFeld.myPosY;

      var maxX = Math.Min(spielfeld.Max(f => f.myPosX), currentFieldX + 1);
      var minX = Math.Max(currentFieldX - 1, 0);

      var maxY = Math.Min(spielfeld.Max(f => f.myPosY), currentFieldY + 1);
      var minY = Math.Max(currentFieldY - 1, 0);

      var nearestFields = spielfeld.Where(f => f.myPosX >= minX && f.myPosX <= maxX &&
                                          f.myPosY >= minY && f.myPosY <= maxY).ToList();

      return nearestFields.Where(f => f.Feldtyp != Feldtyp.Hindernis &&
                                      f.Feldtyp != Feldtyp.AktuellesFeld).ToList();
    }

    public static void SetVorgaenger(Feld currentFeld, Feld vorgaengerFeld)
    {
      currentFeld.Vorgaenger = vorgaengerFeld;
    }


    public static List<Feld> GetFinalenPfad(Feld lastFeld)
    {
      List<Feld> output = new List<Feld>();

      while (lastFeld.Vorgaenger != null)
      {
        output.Add(lastFeld);
        lastFeld = lastFeld.Vorgaenger;
      }
      return output;
    }

  }
}
