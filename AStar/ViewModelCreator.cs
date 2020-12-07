using System.Collections.Generic;
using System.Drawing;

namespace AStar
{
    class ViewModelCreator
    {
        public static List<Feld> GetSpielfeld(int fieldSizeProAchse)
        {
            var size = 25;
            var padding = 20;
            var output = new List<Feld>();

            for (int x = 0; x < fieldSizeProAchse; x++)
            {
                for (int y = 0; y < fieldSizeProAchse; y++)
                {
                    output.Add(CreateEinzelfeld(x,y,size,padding));
                }
            }
            return output;
        }

        private static Feld CreateEinzelfeld(int xInArray, int yInArray, int size, int padding)
        {
            return new Feld
            {
                BackColor = Color.AliceBlue,
                X = xInArray,
                Y = yInArray,
                Location = new Point(xInArray * size + padding, yInArray * size + padding),
                Size = new Size(size, size),
                Feldtyp = Feldtyp.Normal,
                Text = $"{xInArray} : {yInArray}"
            };
        }
    }
}
