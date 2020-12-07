using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AStar
{
    class Pfadfinder
    {
        public static ListenUpdateDTO GeheKomplettenWeg(List<Feld> spielfeld, List<Feld> openList, List<Feld> closedList, Feld startfeld)
        {
            var currentFeld = startfeld;
            var zielfeld = spielfeld.First(x => x.Feldtyp == Feldtyp.Zielfeld);
            ListenUpdateDTO result = new ListenUpdateDTO();

            while (AStar.CheckIfZielGefunden(openList, zielfeld) == false)//TODO: Next STep
            {
                openList.Add(currentFeld);
                Helper.FormatAktuellesFeld(currentFeld);
                var grenzendeFelder = AStar.GetSurroundingFelder(spielfeld, currentFeld).ToList();
                var grenzendeFelderAußerhalbClosedList = FiltereGrenzendeFelderObInClosedListVorhanden(closedList, grenzendeFelder);
                var berechneteFelder = BerechnerPfadKostenUndSetzeVorgaenger(grenzendeFelderAußerhalbClosedList, currentFeld, openList, zielfeld);
                openList.AddRange(berechneteFelder);
 
                var neueListen = AddCurrentFeldZurClosedListUndEntferneVonOpenList(closedList, openList);
                openList = neueListen.OpenList;
                closedList = neueListen.ClosedList;
                currentFeld = GetGuengstigstesFeld(openList);
              
            } 

            return result;
        }


        private static List<Feld> FiltereGrenzendeFelderObInClosedListVorhanden(List<Feld> closedList, List<Feld> grenzendeFelder)
        {
            var ouptut = new List<Feld>();

            foreach (var item in grenzendeFelder)
            {
                if (!closedList.Contains(item))
                    ouptut.Add(item);
            }
            return ouptut;
        }

        public static List<Feld> BerechnerPfadKostenUndSetzeVorgaenger(List<Feld> grenzendeFelder, Feld currentFeld, List<Feld> openList, Feld zielFeld)
        {
            var bereitsVorhandeneFelder = GetVorhandeneFelderInOpenList(openList.Where(x => x.Feldtyp != Feldtyp.AktuellesFeld).ToList(), grenzendeFelder);
            bereitsVorhandeneFelder.ForEach(x => grenzendeFelder.Remove(x));

            if (bereitsVorhandeneFelder.Any())
            {
                foreach (var item in bereitsVorhandeneFelder)
                {
                    var pfadkosten = AStar.CalculateSinglePathScore(zielFeld, item);

                    if (pfadkosten.G < item.G)
                    {
                        AStar.SetVorgaenger(item, currentFeld);
                        item.G = pfadkosten.G;
                        item.F = item.G + item.H;
                    }
                }
            }

            foreach (var item in grenzendeFelder)
            {
                AStar.SetVorgaenger(item, currentFeld);
                var pfadkosten = AStar.CalculateSinglePathScore(zielFeld, item);
                item.G = pfadkosten.G;
                item.H = pfadkosten.H;
                item.F = pfadkosten.F;
            }

            return grenzendeFelder.Concat(bereitsVorhandeneFelder).ToList();
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
            currentFeld.Feldtyp = Feldtyp.Pfad;
            closedList.Add(currentFeld);
            openList.Remove(currentFeld);

            return new ListenUpdateDTO
            {
                ClosedList = closedList,
                OpenList = openList
            };
        }

        public static Feld GetGuengstigstesFeld(List<Feld> openList) => openList.OrderBy(feld => feld.F).First();
    }
}
