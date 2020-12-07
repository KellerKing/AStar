 using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AStar
{
    class Controller
    {
        private List<Feld> m_Spielfeld;
        private List<Feld> m_OpenList = new List<Feld>(); //Bekommt Felder die geprüft werden müssen
        private List<Feld> m_ClosedList = new List<Feld>(); //Bereits begangene Felder
        private MainForm m_MainForm;

        public Controller(MainForm mainForm)
        {
            m_MainForm = mainForm;
            InitForm();
        }

        private void InitForm()
        {
            var felderProAchse = 16;
            m_Spielfeld = ViewModelCreator.GetSpielfeld(felderProAchse); //TODO: Feldgröße automatisch berechnen
            var lastField = m_Spielfeld.First(feld => feld.X == felderProAchse - 1 && feld.Y == felderProAchse - 1);
            var radButtons = ViewModelCreator.GetRadioButtons(lastField.Width + lastField.Location.X, lastField.Y, 20);
            var zumZielButton = ViewModelCreator.GetZumZielButton(radButtons.Last().Location.X, radButtons.Last().Location.Y, 40);
            var formSize = Helper.CalculateWindowSize(Convert.ToInt32(Math.Sqrt(m_Spielfeld.Count)), 20, 25); //TODO Naming und Klasse mit Groeßsen.

            SetzeUIFelder(zumZielButton, formSize, radButtons);
            ConnectEvents(zumZielButton);
        }

        private void SetzeUIFelder(Button zumZielButton, Tuple<int, int> formSize, List<meinRadioButton> radButtons)
        {
            m_MainForm.SetZumZielButton(zumZielButton);
            m_MainForm.SetWindowSize(formSize.Item1, formSize.Item2);
            m_MainForm.SetSpielfeld(m_Spielfeld);
            m_MainForm.SetRadioButtons(radButtons);

        }

        private void ConnectEvents(Button zumZielButton)
        {
            zumZielButton.Click += m_MainForm.btnKomplett_Click;
            m_Spielfeld.ForEach(feld => feld.Click += m_MainForm.Feld_Clicked);
            m_MainForm.FeldClicked += StatusFeldSetzen;
            m_MainForm.BtnZumZiel += BtnZumZielClicked;
        }

        private void StatusFeldSetzen(Feld currentFeld, Feldtyp currentFeldtyp)
        {
            var kopieSpielfeld = new List<Feld>(m_Spielfeld);
            m_Spielfeld = Feldwechsler.SetSpecialFeld(kopieSpielfeld, currentFeld, currentFeldtyp);
            var isStartBereit = Helper.IsNeededFelderGesetzt(kopieSpielfeld);
           // m_MainForm.SetStatusSchrittButton(isStartBereit);
        }

        private void BtnZumZielClicked()
        {
            var startfeld = m_Spielfeld.FirstOrDefault(feld => feld.Feldtyp == Feldtyp.AktuellesFeld);
            var zielfeld = m_Spielfeld.FirstOrDefault(feld => feld.Feldtyp == Feldtyp.Zielfeld);

            if (startfeld == null || zielfeld == null) return;

            //TODO: Wie geht es weiter ? 
            Task.Run(() => Pfadfinder.GeheKomplettenWeg(m_Spielfeld, m_OpenList, m_ClosedList, startfeld, zielfeld));
        }
    }
}
