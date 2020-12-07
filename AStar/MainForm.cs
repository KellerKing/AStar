using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AStar
{
    public partial class MainForm : Form
    {
        public Action<Feld, Feldtyp> FeldClicked;
        public Action BtnSchrittClicked;
        public Action BtnZumZiel;

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
            Feldtyp currentFeldtyp = Feldtyp.Normal;

            if (rbStartfeld.Checked)
                currentFeldtyp = Feldtyp.AktuellesFeld;
            else if (rbZielfeld.Checked)
                currentFeldtyp = Feldtyp.Zielfeld;
            else if (rbHindernis.Checked)
                currentFeldtyp = Feldtyp.Hindernis;

            FeldClicked.Invoke(currentFeld, currentFeldtyp);
        }

        internal void SetStatusSchrittButton(bool aktivStatus)
        {
            btnSchritt.Enabled = aktivStatus;
        }

        private void btnSchritt_Click(object sender, EventArgs e)
        {
            BtnSchrittClicked.Invoke();
        }

        internal void SetzStatusRadioButtons(bool aktivStatus)
        {
            rbStartfeld.Enabled = aktivStatus;
            rbZielfeld.Enabled = aktivStatus;
            rbHindernis.Enabled = aktivStatus;
            rbNormal.Enabled = aktivStatus;
        }

        private void btnKomplett_Click(object sender, EventArgs e)
        {
            BtnZumZiel.Invoke();
        }
    }
}
