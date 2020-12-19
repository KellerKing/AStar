using AStar.Konstanten;

namespace AStar.Transferobjekte
{
 public class ZielSucheDTO
  {
    public string Ausgabetext { get; set; }
    public ZielsuchErgebnis ZielsuchErgebnis { get; set; }
    public Feld letzesBekanntesFeld { get; set; }
  }
}
