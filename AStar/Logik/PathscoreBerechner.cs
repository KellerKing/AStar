using System;

namespace AStar.Logik
{
  class PathscoreBerechner
  {
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

      return Helper.IsFeldDiagonalZumVorgaenger(currentFeld) ?
        kostenDiagonal + currentFeld.Vorgaenger.G :
        kostenHorizontalVertikal + currentFeld.Vorgaenger.G;
    }

    private static int CalculateHScore(Feld currentFeld, Feld zielFeld)
    {
      return 10 * (Math.Abs(zielFeld.myPosX - currentFeld.myPosX) +
                  Math.Abs(zielFeld.myPosY - currentFeld.myPosY));
    }
  }
}
