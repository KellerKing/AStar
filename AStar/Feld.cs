using System;
using System.Windows.Forms;
using System.Reflection;

namespace AStar
{
    public class Feld : Button
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Feldtyp Feldtyp { get; set; }

        public int G { get; set; }
        public int H { get; set; }
        public int F { get; set; }

        public Feld Vorgaenger { get; set; }



        public static Feld Copy(Feld sourceFeld)
        {
            var copiedFeld = new Feld
            {
                X = sourceFeld.X,
                Y = sourceFeld.Y,
                Feldtyp = sourceFeld.Feldtyp,
                G = sourceFeld.G,
                H = sourceFeld.H,
                F = sourceFeld.F,
                Vorgaenger = sourceFeld.Vorgaenger
            };

            return copiedFeld;
        }
    }
}
