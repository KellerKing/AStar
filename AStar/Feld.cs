using System;
using System.Windows.Forms;

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
    }
}
