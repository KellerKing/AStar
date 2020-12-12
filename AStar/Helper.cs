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
    public static Tuple<int, int> CalculateWindowSize(int anzahlFelderProAchse, int padding, int feldSize)
    {
      var neededHeight = anzahlFelderProAchse * feldSize + (2 * padding) + 40;
      var neededWidht = anzahlFelderProAchse * feldSize + (2 * padding) + 120;

      return Tuple.Create(neededWidht, neededHeight);
    }

    public static bool IsFeldDiagonalZumVorgaenger(Feld feld)
    {
      var vorgaengerX = feld.Vorgaenger.X;
      var vorgaengerY = feld.Vorgaenger.Y;

      if (vorgaengerX == feld.X - 1 && vorgaengerY == feld.Y - 1) return true;
      else if (vorgaengerX == feld.X + 1 && vorgaengerY == feld.Y - 1) return true;
      else if (vorgaengerX == feld.X + 1 && vorgaengerY == feld.Y + 1) return true;
      else if (vorgaengerX == feld.X - 1 && vorgaengerY == feld.Y + 1) return true;
      else return false;
    }

    public static void ResetSpielfeld(List<Feld> m_Spielfeld) //TODO: Muss ich das wirklich mit Return machen, oder mache ich alles mit byRef
    {
      foreach (var feld in m_Spielfeld)
      {
        feld.BackColor = Color.AliceBlue;
        feld.Feldtyp = Feldtyp.Normal;
      }
    }
  }
}
