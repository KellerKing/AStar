using AStar.Konstanten;
using AStar.Transferobjekte;
using System.Collections.Generic;
using System.Linq;

namespace AStar.Logik
{
  public class ZielChecker
  {
    public static ZielSucheDTO CheckIfZielGefunden(List<Feld> openList, Feld zielfeld, Feld currentFeld)
    {
      var zielsuchErgebnis = ZielsuchErgebnis.NichtGefunden;
      var ausgabetext = "";

      if (!openList.Any())
      {
        zielsuchErgebnis = ZielsuchErgebnis.KeinWegMoeglich;
        ausgabetext = "Ups, leider kann das Ziel nicht erreicht werden." + "\r\n" + "Beenden ? ";
      }
      else if (openList.Any(feld => feld.Feldtyp == Feldtyp.Zielfeld))
      {
        zielsuchErgebnis = ZielsuchErgebnis.Gefunden;
        ausgabetext = "Supi, das Ziel wurde gefunden !" + "\r\n" + "Beenden ? ";
      }
      return new ZielSucheDTO
      {
        ZielsuchErgebnis = zielsuchErgebnis,
        Ausgabetext = ausgabetext,
        letzesBekanntesFeld = currentFeld
      };
    }

    public static void FormatiereFallsPfadGefunden(ZielSucheDTO pruefergebnis)
    {
      if (pruefergebnis.ZielsuchErgebnis == ZielsuchErgebnis.KeinWegMoeglich)
        return;

      var derFinalePfad = Helper.GetFinalenPfad(pruefergebnis.letzesBekanntesFeld);
      FeldFormatierer.FormatiereFinalenPfad(derFinalePfad);

    }
  }
}
