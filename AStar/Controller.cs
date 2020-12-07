using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar
{
    class Controller
    {
        private List<Feld> m_Spielfeld;
        private List<Feld> m_OpenList = new List<Feld>(); //Bekommt Felder die geprüft werden müssen
        private List<Feld> m_ClosedList = new List<Feld>(); //Bereits begangene Felder
        private Feld m_Zielfeld;
        private MainForm m_MainForm;

        public Controller(MainForm mainForm)
        {
            m_MainForm = mainForm;
            InitForm();
        }

        private void InitForm()
        {
            m_Spielfeld = ViewModelCreator.GetSpielfeld(10); //TODO: Feldgröße automatisch berechnen
            var formSize = Helper.CalculateWindowSize(Convert.ToInt32(Math.Sqrt(m_Spielfeld.Count)), 20, 25); //TODO Naming und Klasse mit Groeßsen.
            m_MainForm.SetWindowSize(formSize.Item1, formSize.Item2);
            m_MainForm.SetSpielfeld(m_Spielfeld);
            m_MainForm.SetStatusSchrittButton(false);
            ConnectEvents();
        }

        private void ConnectEvents()
        {
            m_Spielfeld.ForEach(feld => feld.Click += m_MainForm.Feld_Clicked);
            m_MainForm.FeldClicked += StatusFeldSetzen;
            m_MainForm.BtnSchrittClicked += BtnSchrittClicked;
            m_MainForm.BtnZumZiel += BtnZumZielClicked;
        }

        private void BtnSchrittClicked()//TODO: Später definitv rework
        {

        }

        private void StatusFeldSetzen(Feld currentFeld, Feldtyp currentFeldtyp)
        {
            var kopieSpielfeld = new List<Feld>(m_Spielfeld);
            m_Spielfeld = Feldwechsler.SetSpecialFeld(kopieSpielfeld, currentFeld, currentFeldtyp);
            var isStartBereit = Helper.IsNeededFelderGesetzt(kopieSpielfeld);
            m_MainForm.SetStatusSchrittButton(isStartBereit);
        }

        private void BtnZumZielClicked()
        {
            var startfeld = m_Spielfeld.First(feld => feld.Feldtyp == Feldtyp.AktuellesFeld);
            var zielfeld = m_Spielfeld.First(feld => feld.Feldtyp == Feldtyp.Zielfeld);

            //var kopieSpielfeld = new List<Feld>(m_Spielfeld);
            //var kopieOpenList = new List<Feld>(m_OpenList);
            //var kopieClosedList = new List<Feld>(m_ClosedList);

            //TODO: Wie geht es weiter ? 
            Pfadfinder.GeheKomplettenWeg(m_Spielfeld, m_OpenList, m_ClosedList, startfeld);
        }
    }
}
