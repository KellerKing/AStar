using AStar.Konstanten;
using AStar.Transferobjekte;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace AStar
{
  class Pfadfinder
  {
    public static async Task<ZielSucheDTO> GeheKomplettenWeg(List<Feld> spielfeld, List<Feld> openList, List<Feld> closedList, Feld startfeld, Feld zielfeld)
    {
      var currentFeld = startfeld;
      openList.Add(currentFeld);
      var zielgefundenPruefergebnis = AStar.CheckIfZielGefunden(openList, zielfeld);

      while (zielgefundenPruefergebnis.ZielsuchErgebnis == ZielsuchErgebnis.NichtGefunden)//TODO: Next STep
      {
        currentFeld = GetNeuesAktuellesFeld(openList);
        var grenzendeFelder = AStar.GetBetretbareUmliegendeFelder(spielfeld, currentFeld);
        var grenzendeFelderAußerhalbClosedList = EntferneGrenzendeFelderDieBereitsInClosedListSind(closedList, grenzendeFelder);
 
        BerechnePfadKostenUndSetzeVorgaenger(grenzendeFelderAußerhalbClosedList, currentFeld, zielfeld);
        FuegeFelderAusserhalbOpenListDieserHinzu(openList, grenzendeFelderAußerhalbClosedList);
        AddCurrentFeldZurClosedListUndEntferneVonOpenList(closedList, openList);

        zielgefundenPruefergebnis = AStar.CheckIfZielGefunden(openList, zielfeld);

      }
      var derFinalePfad = GetFinalenPfad(currentFeld);
      FeldFormatierer.FormatiereFinalenPfad(derFinalePfad);
      //FeldFormatierer.FormatiereBerechneteFelder(openList);

      return zielgefundenPruefergebnis;
    }

    private static List<Feld> GetFinalenPfad(Feld lastFeld)
    {
      List<Feld> output = new List<Feld>();

      while(lastFeld.Vorgaenger != null)
      {
        output.Add(lastFeld);
        lastFeld = lastFeld.Vorgaenger;
      }
      return output;
    }

    private static void FuegeFelderAusserhalbOpenListDieserHinzu(List<Feld> openList, List<Feld> grenzendeFelder)
    {

      var eintraegeAusserhalbOpenList = new List<Feld>();

      grenzendeFelder.ForEach(x => { if (!openList.Contains(x)) eintraegeAusserhalbOpenList.Add(x); });
      openList.AddRange(eintraegeAusserhalbOpenList);
    }

    private static List<Feld> EntferneGrenzendeFelderDieBereitsInClosedListSind(List<Feld> closedList, List<Feld> grenzendeFelder)
    {
      var ouptut = new List<Feld>();

      if (closedList.Count == 0)
        return grenzendeFelder;

      foreach (var feld in grenzendeFelder)
      {
        if (!closedList.Contains(feld))
          ouptut.Add(feld);
      }
      return ouptut;
    }

    public static void BerechnePfadKostenUndSetzeVorgaenger(List<Feld> grenzendeFelder, Feld currentFeld, Feld zielFeld) //TODO: Ich glaube hier ist der FEHLER !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!1
    {
      foreach (var grenzendesFeld in grenzendeFelder)
      {
        if (grenzendesFeld.Vorgaenger == null)
        {
          AStar.SetVorgaenger(grenzendesFeld, currentFeld);
          var pfadkosten = AStar.CalculateSinglePathScore(zielFeld, grenzendesFeld);
          grenzendesFeld.G = pfadkosten.G;
          grenzendesFeld.H = pfadkosten.H;
          grenzendesFeld.F = pfadkosten.F;
        }
        else
        {
          var neuerGWert = AStar.CalculateSinglePathScore(zielFeld, grenzendesFeld).G;

          if (neuerGWert < grenzendesFeld.G)
          {
            AStar.SetVorgaenger(grenzendesFeld, currentFeld);
            grenzendesFeld.G = neuerGWert;
            grenzendesFeld.F = grenzendesFeld.G + grenzendesFeld.H;
          }
        }
      }
    }

    public static void AddCurrentFeldZurClosedListUndEntferneVonOpenList(List<Feld> closedList, List<Feld> openList)
    {

      var currentFeld = openList.First(f => f.Feldtyp == Feldtyp.AktuellesFeld);
      currentFeld.Feldtyp = Feldtyp.Normal;
      closedList.Add(currentFeld);
      openList.Remove(currentFeld);
    }

    public static Feld GetNeuesAktuellesFeld(List<Feld> openList)
    {
      var nextBestFeld = openList.OrderBy(feld => feld.F).FirstOrDefault();
      
      if(nextBestFeld != null)
        nextBestFeld.Feldtyp = Feldtyp.AktuellesFeld;
      
      return nextBestFeld;
    }
  }
}
