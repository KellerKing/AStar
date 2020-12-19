using AStar.Konstanten;
using AStar.Logik;
using AStar.Transferobjekte;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AStar
{
  class Pfadfinder
  {
    public static async Task<ZielSucheDTO> FindeWegZumZiel(List<Feld> spielfeld, List<Feld> openList, List<Feld> closedList, Feld startfeld, Feld zielfeld)
    {
      var currentFeld = startfeld;
      openList.Add(currentFeld);
      var zielgefundenPruefergebnis = ZielChecker.CheckIfZielGefunden(openList, zielfeld, currentFeld);

      while (zielgefundenPruefergebnis.ZielsuchErgebnis == ZielsuchErgebnis.NichtGefunden)
      {
        currentFeld = GetNeuesAktuellesFeld(openList);
        MacheSchritt(spielfeld, openList, closedList, currentFeld, zielfeld);

        zielgefundenPruefergebnis = ZielChecker.CheckIfZielGefunden(openList, zielfeld, currentFeld);

      }
      return zielgefundenPruefergebnis;
    }


    private static void MacheSchritt(List<Feld> spielfeld, List<Feld> openList, List<Feld> closedList, Feld currentFeld, Feld zielfeld)
    {
      var grenzendeFelder = Helper.GetBetretbareUmliegendeFelder(spielfeld, currentFeld);
      var grenzendeFelderAußerhalbClosedList = EntferneGrenzendeFelderDieBereitsInClosedListSind(closedList, grenzendeFelder);
      BerechnePfadKostenUndSetzeVorgaenger(grenzendeFelderAußerhalbClosedList, currentFeld, zielfeld);
      FuegeFelderAusserhalbOpenListDieserHinzu(openList, grenzendeFelderAußerhalbClosedList);
      AddCurrentFeldZurClosedListUndEntferneVonOpenList(closedList, openList);
    }


    private static void FuegeFelderAusserhalbOpenListDieserHinzu(List<Feld> openList, List<Feld> grenzendeFelder)
    {

      var eintraegeAusserhalbOpenList = new List<Feld>();

      grenzendeFelder.ForEach(feld => { 
        if (!openList.Contains(feld)) 
          eintraegeAusserhalbOpenList.Add(feld); 
      });

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

    public static void BerechnePfadKostenUndSetzeVorgaenger(List<Feld> grenzendeFelder, Feld currentFeld, Feld zielFeld) 
    {
      foreach (var grenzendesFeld in grenzendeFelder)
      {
        if (grenzendesFeld.Vorgaenger == null)
        {
          Helper.SetVorgaenger(grenzendesFeld, currentFeld);
          var pfadkosten = PathscoreBerechner.CalculateSinglePathScore(zielFeld, grenzendesFeld);
          grenzendesFeld.G = pfadkosten.G;
          grenzendesFeld.H = pfadkosten.H;
          grenzendesFeld.F = pfadkosten.F;
        }
        else
        {
          var neuerGWert = PathscoreBerechner.CalculateSinglePathScore(zielFeld, grenzendesFeld).G;

          if (neuerGWert < grenzendesFeld.G)
          {
            Helper.SetVorgaenger(grenzendesFeld, currentFeld);
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
