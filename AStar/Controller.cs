﻿using System;
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
      var felderProAchse = 12;
      m_Spielfeld = ViewModelCreator.GetSpielfeld(felderProAchse); //TODO: Feldgröße automatisch berechnen
      var lastField = m_Spielfeld.First(feld => feld.X == felderProAchse - 1 && feld.Y == felderProAchse - 1);
      var radButtons = ViewModelCreator.GetRadioButtons(lastField.Width + lastField.Location.X, lastField.Y, 20);
      var uiButtons = ViewModelCreator.GetButtons(radButtons.Last().Location.X, radButtons.Last().Location.Y, 40);

      var formSize = Helper.CalculateWindowSize(Convert.ToInt32(Math.Sqrt(m_Spielfeld.Count)), 20, 25); //TODO Naming und Klasse mit Groeßsen.

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

    private void SetZufaelligesFeld()
    {
      m_Spielfeld = Helper.ResetSpielfeld(new List<Feld>(m_Spielfeld));
      m_Spielfeld = RandomMapGenerator.GenerateRandomMap(new List<Feld>(m_Spielfeld), 50);
    }

    private void StatusFeldSetzen(Feld currentFeld, Feldtyp currentFeldtyp)
    {
      FeldFormatierer.SetSpecialFeld(m_Spielfeld, currentFeld, currentFeldtyp);
    }
    private void BtnClearClicked()
    {
      Application.Restart();
    }

    private async void BtnZumZielClicked()
    {
      var startfeld = m_Spielfeld.FirstOrDefault(feld => feld.Feldtyp == Feldtyp.AktuellesFeld);
      var zielfeld = m_Spielfeld.FirstOrDefault(feld => feld.Feldtyp == Feldtyp.Zielfeld);

      if (startfeld == null || zielfeld == null) return;

      //TODO: Wie geht es weiter ? 
      var zielsucheErgebnis = await Task.Run(() => Pfadfinder.GeheKomplettenWeg(m_Spielfeld, m_OpenList, m_ClosedList, startfeld, zielfeld));

      MessageBox.Show(zielsucheErgebnis.Ausgabetext); //TODO: Hier das dann auslagern
      Application.Restart();
    }
  }
}
