using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

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
          output.Add(CreateEinzelfeld(x, y, size, padding));
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
      };
    }


    public static List<Button> GetButtons(int locationX, int locationY, int padding)
    {
      return new List<Button>
      {
        new Button
        {
          Name = "btZumZiel",
          Text = "Zum Ziel",
          Location = new Point(locationX, locationY + padding),
          Size = new Size(80,25),
          TextAlign = ContentAlignment.MiddleCenter
        },
        new Button
        {
          Name = "btnClear",
          Text = "Zurücksetzen",
          Location = new Point(locationX, locationY + padding + 25),
          Size = new Size(80,25),
          TextAlign = ContentAlignment.MiddleCenter
        }
      };
    }

    public static List<meinRadioButton> GetRadioButtons(int lastButtonXValue, int yValue, int padding)
    {
      var output = new List<meinRadioButton>();
      string[] name = new string[] { "Startfeld", "Zielfeld", "Hindernis", "Normal" };
      Feldtyp[] feldtyp = new Feldtyp[] { Feldtyp.AktuellesFeld, Feldtyp.Zielfeld, Feldtyp.Hindernis, Feldtyp.Normal };

      for (int i = 0; i < name.Length; i++)
        output.Add(CreateRadioButton(lastButtonXValue + padding, yValue + (i * padding), name[i], feldtyp[i]));

      return output;
    }

    private static meinRadioButton CreateRadioButton(int x, int y, string name, Feldtyp feldtyp)
    {
      return new meinRadioButton
      {
        Name = "rb" + name,
        Location = new Point(x, y),
        Text = name,
        Feldtyp = feldtyp
      };
    }
  }
}
