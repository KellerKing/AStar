using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar
{
  class RandomMapGenerator
  {
    public static void GenerateRandomMap(List<Feld> spielfeld, int hindernisrateInProzent)
    {
      var r = new Random();

      SetRandomStartOderZielfeld(spielfeld, r, Feldtyp.AktuellesFeld);
      SetRandomStartOderZielfeld(spielfeld, r, Feldtyp.Zielfeld);
      SetRandomHindernisse(spielfeld, hindernisrateInProzent, r);
    }

    private static void SetRandomStartOderZielfeld(List<Feld> spielfeld, Random r, Feldtyp feldtyp)
    {
      Feld zuFormatierendesFeld;

      do
      {
        var maxNumberImSpielfed = spielfeld.Count;
        var IndexStartfeld = r.Next(0, maxNumberImSpielfed);
        zuFormatierendesFeld = spielfeld[IndexStartfeld];

      } while (zuFormatierendesFeld.Feldtyp != Feldtyp.Normal);

      FeldFormatierer.SetSpecialFeld(spielfeld, zuFormatierendesFeld, feldtyp);
    }

    private static void SetRandomHindernisse(List<Feld> spielfeld, int hindernisrateInProzent, Random r)
    {
      var anzahlDerHindenisse = Math.Round(((double)spielfeld.Count / 100) * hindernisrateInProzent);
      Feld zuFormatierendesFeld;

      for (int i = 0; i < anzahlDerHindenisse; i++)
      {
        do
        {
          var maxNumberImSpielfed = spielfeld.Count;
          var IndexStartfeld = r.Next(0, maxNumberImSpielfed);
          zuFormatierendesFeld = spielfeld[IndexStartfeld];

        } while (zuFormatierendesFeld.Feldtyp != Feldtyp.Normal);

        FeldFormatierer.FormatiereAlsHindernis(zuFormatierendesFeld);
      }
    }
  }
}
