namespace AStar
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rbStartfeld = new System.Windows.Forms.RadioButton();
            this.rbZielfeld = new System.Windows.Forms.RadioButton();
            this.btnSchritt = new System.Windows.Forms.Button();
            this.rbHindernis = new System.Windows.Forms.RadioButton();
            this.rbNormal = new System.Windows.Forms.RadioButton();
            this.btnKomplett = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rbStartfeld
            // 
            this.rbStartfeld.AutoSize = true;
            this.rbStartfeld.Location = new System.Drawing.Point(862, 34);
            this.rbStartfeld.Name = "rbStartfeld";
            this.rbStartfeld.Size = new System.Drawing.Size(64, 17);
            this.rbStartfeld.TabIndex = 0;
            this.rbStartfeld.TabStop = true;
            this.rbStartfeld.Text = "Startfeld";
            this.rbStartfeld.UseVisualStyleBackColor = true;
            // 
            // rbZielfeld
            // 
            this.rbZielfeld.AutoSize = true;
            this.rbZielfeld.Location = new System.Drawing.Point(862, 57);
            this.rbZielfeld.Name = "rbZielfeld";
            this.rbZielfeld.Size = new System.Drawing.Size(59, 17);
            this.rbZielfeld.TabIndex = 1;
            this.rbZielfeld.TabStop = true;
            this.rbZielfeld.Text = "Zielfeld";
            this.rbZielfeld.UseVisualStyleBackColor = true;
            // 
            // btnSchritt
            // 
            this.btnSchritt.Location = new System.Drawing.Point(862, 126);
            this.btnSchritt.Name = "btnSchritt";
            this.btnSchritt.Size = new System.Drawing.Size(75, 23);
            this.btnSchritt.TabIndex = 2;
            this.btnSchritt.Text = "button1";
            this.btnSchritt.UseVisualStyleBackColor = true;
            this.btnSchritt.Click += new System.EventHandler(this.btnSchritt_Click);
            // 
            // rbHindernis
            // 
            this.rbHindernis.AutoSize = true;
            this.rbHindernis.Location = new System.Drawing.Point(862, 80);
            this.rbHindernis.Name = "rbHindernis";
            this.rbHindernis.Size = new System.Drawing.Size(69, 17);
            this.rbHindernis.TabIndex = 3;
            this.rbHindernis.TabStop = true;
            this.rbHindernis.Text = "Hindernis";
            this.rbHindernis.UseVisualStyleBackColor = true;
            // 
            // rbNormal
            // 
            this.rbNormal.AutoSize = true;
            this.rbNormal.Location = new System.Drawing.Point(862, 103);
            this.rbNormal.Name = "rbNormal";
            this.rbNormal.Size = new System.Drawing.Size(58, 17);
            this.rbNormal.TabIndex = 4;
            this.rbNormal.TabStop = true;
            this.rbNormal.Text = "Normal";
            this.rbNormal.UseVisualStyleBackColor = true;
            // 
            // btnKomplett
            // 
            this.btnKomplett.Location = new System.Drawing.Point(862, 155);
            this.btnKomplett.Name = "btnKomplett";
            this.btnKomplett.Size = new System.Drawing.Size(75, 23);
            this.btnKomplett.TabIndex = 5;
            this.btnKomplett.Text = "Zum Ziel";
            this.btnKomplett.UseVisualStyleBackColor = true;
            this.btnKomplett.Click += new System.EventHandler(this.btnKomplett_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(969, 665);
            this.Controls.Add(this.btnKomplett);
            this.Controls.Add(this.rbNormal);
            this.Controls.Add(this.rbHindernis);
            this.Controls.Add(this.btnSchritt);
            this.Controls.Add(this.rbZielfeld);
            this.Controls.Add(this.rbStartfeld);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbStartfeld;
        private System.Windows.Forms.RadioButton rbZielfeld;
        private System.Windows.Forms.Button btnSchritt;
    private System.Windows.Forms.RadioButton rbHindernis;
        private System.Windows.Forms.RadioButton rbNormal;
        private System.Windows.Forms.Button btnKomplett;
    }
}

