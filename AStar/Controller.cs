using AStar.Logik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AStar
{
  class Controller
  {
    private List<Feld> m_Spielfeld;
    private List<Feld> m_OpenList = new List<Feld>();
    private List<Feld> m_ClosedList = new List<Feld>(); 
    private MainForm m_MainForm;

    public Controller(MainForm mainForm)
    {
      m_MainForm = mainForm;
      InitForm();
    }

    private void InitForm()
    {
      var felderProAchse = 16;
      m_Spielfeld = ViewModelCreator.GetSpielfeld(felderProAchse); 
      var lastField = m_Spielfeld.First(feld => feld.myPosX == felderProAchse - 1 && feld.myPosY == felderProAchse - 1);
      var radButtons = ViewModelCreator.GetRadioButtons(lastField.Width + lastField.Location.X, lastField.myPosY, 20);
      var uiButtons = ViewModelCreator.GetButtons(radButtons.Last().Location.X, radButtons.Last().Location.Y, 40);

      var formSize = Helper.CalculateWindowSize(Convert.ToInt32(Math.Sqrt(m_Spielfeld.Count)), 20, 25); 

      SetzeUIFelder(uiButtons, formSize, radButtons);
      ConnectEvents(uiButtons);
    }

    private void SetzeUIFelder(List<Button> uiButtons, Tuple<int, int> formSize, List<MeinRadioButton> radButtons)
    {
      m_MainForm.SetButtons(uiButtons);
      m_MainForm.SetWindowSize(formSize.Item1, formSize.Item2);
      m_MainForm.SetSpielfeld(m_Spielfeld);
      m_MainForm.SetRadioButtons(radButtons);

    }

    private void ConnectEvents(List<Button> uiButtons)
    {
      uiButtons[0].Click += m_MainForm.btnKomplett_Click;
      uiButtons[1].Click += m_MainForm.btnClear_Click;
      uiButtons[2].Click += m_MainForm.btnZufaelligesSpielfed_Click;
      m_Spielfeld.ForEach(feld => feld.Click += m_MainForm.Feld_Clicked);

      m_MainForm.FeldClicked += StatusFeldSetzen;
      m_MainForm.ZumZielButtonClicked += BtnZumZielClicked;
      m_MainForm.ClearButtonClicked += BtnClearClicked;
      m_MainForm.BtnZufaelligesSpielfedClicked += SetZufaelligesFeld;
    }

    private void SetZufaelligesFeld() => RandomMapGenerator.GenerateRandomMap(m_Spielfeld, 50);

    private void StatusFeldSetzen(Feld currentFeld, Feldtyp currentFeldtyp) => FeldFormatierer.SetSpecialFeld(m_Spielfeld, currentFeld, currentFeldtyp);
    
    private void BtnClearClicked() => Application.Restart();


    private async void BtnZumZielClicked()
    {
      var startfeld = m_Spielfeld.FirstOrDefault(feld => feld.Feldtyp == Feldtyp.AktuellesFeld);
      var zielfeld = m_Spielfeld.FirstOrDefault(feld => feld.Feldtyp == Feldtyp.Zielfeld);

      if (startfeld == null || zielfeld == null) return;

      var zielsucheErgebnis = await Task.Run(() => Pfadfinder.FindeWegZumZiel(m_Spielfeld, m_OpenList, m_ClosedList, startfeld, zielfeld));

      await Task.Run(() => ZielChecker.FormatiereFallsPfadGefunden(zielsucheErgebnis));

      var neustartAntwort = m_MainForm.ZeigeSpielBeendenDialog(zielsucheErgebnis.Ausgabetext);

      if (neustartAntwort == DialogResult.Yes)
        Application.Exit();
      else
        Application.Restart();
    }
  }
}
