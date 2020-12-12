using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar
{
  public class Helper
  {
    public static bool IsNeededFelderGesetzt(List<Feld> spielfeld)
    {
      int anzahlGueltigerFelder = 0;

      foreach (var feld in spielfeld)
      {
        if (feld.Feldtyp == Feldtyp.Zielfeld || feld.Feldtyp == Feldtyp.AktuellesFeld)
          anzahlGueltigerFelder++;

        if (anzahlGueltigerFelder == 2)
          return true;
      }
      return false;
    }

    public static Tuple<int, int> CalculateWindowSize(int anzahlFelderProAchse, int padding, int feldSize) //TODO: Extra needed space ? 
    {
      var neededHeight = anzahlFelderProAchse * feldSize + (2 * padding) + 40;
      var neededWidht = anzahlFelderProAchse * feldSize + (2 * padding) + 120;

      return Tuple.Create(neededWidht, neededHeight);
    }

    public static bool IsFeldDiagonalZumVorgaenger(Feld feld)//TODO: Das geht bestimmt auch mit Mathe ? 
    {
      var vorgaengerX = feld.Vorgaenger.X;
      var vorgaengerY = feld.Vorgaenger.Y;

      if (vorgaengerX == feld.X - 1 && vorgaengerY == feld.Y - 1) return true;
      else if (vorgaengerX == feld.X + 1 && vorgaengerY == feld.Y - 1) return true;
      else if (vorgaengerX == feld.X + 1 && vorgaengerY == feld.Y + 1) return true;
      else if (vorgaengerX == feld.X - 1 && vorgaengerY == feld.Y + 1) return true;
      else return false;
    }

    public static List<Feld> ResetSpielfeld(List<Feld> m_Spielfeld)
    {
      foreach (var feld in m_Spielfeld)
      {
        feld.BackColor = Color.AliceBlue;
        feld.Feldtyp = Feldtyp.Normal;
      }
      return m_Spielfeld;
    }
  }
}
