using System;
using System.Windows.Forms;
using System.Reflection;

namespace AStar
{
    public class Feld : Button
    {
        public int myPosX { get; set; }
        public int myPosY { get; set; }

        public Feldtyp Feldtyp { get; set; }

        public int G { get; set; }
        public int H { get; set; }
        public int F { get; set; }

        public Feld Vorgaenger { get; set; }

    }
}
