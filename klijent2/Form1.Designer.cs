namespace klijent2
{
    partial class Form1
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
            this.uiOutputPrimljenaPoruka = new System.Windows.Forms.TextBox();
            this.uiActionPosalji = new System.Windows.Forms.Button();
            this.uiInputTekstZaSlanje = new System.Windows.Forms.TextBox();
            this.uiActionKreirajKljuceve = new System.Windows.Forms.Button();
            this.uiActionProvjeraPoruke = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // uiOutputPrimljenaPoruka
            // 
            this.uiOutputPrimljenaPoruka.Location = new System.Drawing.Point(20, 29);
            this.uiOutputPrimljenaPoruka.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.uiOutputPrimljenaPoruka.Multiline = true;
            this.uiOutputPrimljenaPoruka.Name = "uiOutputPrimljenaPoruka";
            this.uiOutputPrimljenaPoruka.ReadOnly = true;
            this.uiOutputPrimljenaPoruka.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.uiOutputPrimljenaPoruka.Size = new System.Drawing.Size(424, 175);
            this.uiOutputPrimljenaPoruka.TabIndex = 0;
            // 
            // uiActionPosalji
            // 
            this.uiActionPosalji.Location = new System.Drawing.Point(20, 283);
            this.uiActionPosalji.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.uiActionPosalji.Name = "uiActionPosalji";
            this.uiActionPosalji.Size = new System.Drawing.Size(424, 28);
            this.uiActionPosalji.TabIndex = 1;
            this.uiActionPosalji.Text = "Pošalji";
            this.uiActionPosalji.UseVisualStyleBackColor = true;
            this.uiActionPosalji.Click += new System.EventHandler(this.uiActionPosalji_Click);
            // 
            // uiInputTekstZaSlanje
            // 
            this.uiInputTekstZaSlanje.Location = new System.Drawing.Point(20, 254);
            this.uiInputTekstZaSlanje.Multiline = true;
            this.uiInputTekstZaSlanje.Name = "uiInputTekstZaSlanje";
            this.uiInputTekstZaSlanje.Size = new System.Drawing.Size(424, 22);
            this.uiInputTekstZaSlanje.TabIndex = 2;
            // 
            // uiActionKreirajKljuceve
            // 
            this.uiActionKreirajKljuceve.Location = new System.Drawing.Point(476, 29);
            this.uiActionKreirajKljuceve.Name = "uiActionKreirajKljuceve";
            this.uiActionKreirajKljuceve.Size = new System.Drawing.Size(165, 23);
            this.uiActionKreirajKljuceve.TabIndex = 3;
            this.uiActionKreirajKljuceve.Text = "Kreiraj ključeve";
            this.uiActionKreirajKljuceve.UseVisualStyleBackColor = true;
            this.uiActionKreirajKljuceve.Click += new System.EventHandler(this.uiActionKreirajKljuceve_Click);
            // 
            // uiActionProvjeraPoruke
            // 
            this.uiActionProvjeraPoruke.Location = new System.Drawing.Point(476, 58);
            this.uiActionProvjeraPoruke.Name = "uiActionProvjeraPoruke";
            this.uiActionProvjeraPoruke.Size = new System.Drawing.Size(165, 23);
            this.uiActionProvjeraPoruke.TabIndex = 4;
            this.uiActionProvjeraPoruke.Text = "Provjera poruke";
            this.uiActionProvjeraPoruke.UseVisualStyleBackColor = true;
            this.uiActionProvjeraPoruke.Click += new System.EventHandler(this.uiActionProvjeraPoruke_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Primljena poruka";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 234);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Napišite poruku";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 341);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.uiActionProvjeraPoruke);
            this.Controls.Add(this.uiActionKreirajKljuceve);
            this.Controls.Add(this.uiInputTekstZaSlanje);
            this.Controls.Add(this.uiActionPosalji);
            this.Controls.Add(this.uiOutputPrimljenaPoruka);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Klijent";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox uiOutputPrimljenaPoruka;
        private System.Windows.Forms.Button uiActionPosalji;
        private System.Windows.Forms.TextBox uiInputTekstZaSlanje;
        private System.Windows.Forms.Button uiActionKreirajKljuceve;
        private System.Windows.Forms.Button uiActionProvjeraPoruke;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

