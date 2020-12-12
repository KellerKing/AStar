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
        currentFeld = GetGuengstigstesFeld(openList);
        currentFeld.Feldtyp = Feldtyp.AktuellesFeld;
        var grenzendeFelder = AStar.GetBetretbareUmliegendeFelder(spielfeld, currentFeld).ToList();
        var grenzendeFelderAußerhalbClosedList = EntferneGrenzendeFelderDieBereitsInClosedListSind(closedList, grenzendeFelder);
        BerechnePfadKostenUndSetzeVorgaenger(grenzendeFelderAußerhalbClosedList, currentFeld, zielfeld);
        openList = FuegeFelderAusserhalbOpenListDieserHinzu(openList, grenzendeFelderAußerhalbClosedList);

        var neueListen = AddCurrentFeldZurClosedListUndEntferneVonOpenList(closedList, openList);
        openList = neueListen.OpenList;
        closedList = neueListen.ClosedList;

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

    private static List<Feld> FuegeFelderAusserhalbOpenListDieserHinzu(List<Feld> openList, List<Feld> grenzendeFelder)
    {

      var eintraegeAusserhalbOpenList = new List<Feld>();

      grenzendeFelder.ForEach(x => { if (!openList.Contains(x)) eintraegeAusserhalbOpenList.Add(x); });
      openList.AddRange(eintraegeAusserhalbOpenList);
      return openList;

      var kopieListe = new List<Feld>(openList);

      for (int i = 0; i < kopieListe.Count; i++)
      {
        var eintrag = openList.Where(f => kopieListe[i] == f);

        if (eintrag.ToList().Count > 1)
          openList.Remove(eintrag.First());
      }

      return openList;
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

    public static List<Feld> GetVorhandeneFelderInOpenList(List<Feld> openList, List<Feld> felderZuDursuchen)
    {
      List<Feld> output = new List<Feld>();

      felderZuDursuchen.ForEach(feld =>
      {
        if (openList.Any(list => list.X == feld.X && list.Y == feld.Y))
          output.Add(feld);
      });

      return output;

    }

    public static ListenUpdateDTO AddCurrentFeldZurClosedListUndEntferneVonOpenList(List<Feld> closedList, List<Feld> openList)
    {

      var currentFeld = openList.First(f => f.Feldtyp == Feldtyp.AktuellesFeld);
      currentFeld.Feldtyp = Feldtyp.Normal;
      closedList.Add(currentFeld);
      openList.Remove(currentFeld);

      return new ListenUpdateDTO
      {
        ClosedList = closedList,
        OpenList = openList
      };
    }

    public static Feld GetGuengstigstesFeld(List<Feld> openList)
    {
      return openList.OrderBy(feld => feld.F).FirstOrDefault();
    }
  }
}
