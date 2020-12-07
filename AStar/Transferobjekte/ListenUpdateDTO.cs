using System.Collections.Generic;

namespace AStar
{
    public class ListenUpdateDTO
    {
        public List<Feld> spielfeld{ get; set; }
        public List<Feld> OpenList { get; set; }
        public List<Feld> ClosedList { get; set; }
        public bool ZielGefunden { get; set; }
    }
}
