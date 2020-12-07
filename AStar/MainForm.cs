﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AStar
{
    public partial class MainForm : Form
    {
        public Action<Feld, Feldtyp> FeldClicked;
        public Action BtnZumZiel;

        private List<meinRadioButton> radioButtons;

        public MainForm()
        {
            InitializeComponent();
            new Controller(this);
        }

        public void SetWindowSize(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        public void SetSpielfeld(List<Feld> spielfeld)
        {
            spielfeld.ForEach(feld => this.Controls.Add(feld));
        }

        public void Feld_Clicked(object sender, EventArgs e)
        {
            var currentFeld = sender as Feld;
            Feldtyp currentFeldtyp = radioButtons.First(rbButton => rbButton.Checked).Feldtyp;

            FeldClicked.Invoke(currentFeld, currentFeldtyp);
        }

        //internal void SetStatusSchrittButton(bool aktivStatus)
        //{
        //    btnSchritt.Enabled = aktivStatus;
        //}

        internal void SetRadioButtons(List<meinRadioButton> radButtons)
        {
            this.radioButtons = radButtons;
            radioButtons.First().Checked = true;
            this.Controls.AddRange(radButtons.ToArray());
        }

        internal void SetZumZielButton(Button zumZielButton)
        {
            this.Controls.Add(zumZielButton);
        }

        internal void SetzStatusRadioButtons(bool aktivStatus)
        {
            foreach (var radButton in radioButtons)
            {
                radButton.Enabled = aktivStatus;
            }
        }

        public void btnKomplett_Click(object sender, EventArgs e)
        {
            BtnZumZiel.Invoke();
        }
    }
}
