namespace Bc_prace
{
    partial class Program3Form
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
            statusStripGarage = new StatusStrip();
            btnSettings = new Button();
            btnEmergency = new Button();
            btnEnd = new Button();
            radioButton1 = new RadioButton();
            panel1 = new Panel();
            btnPedestrian1 = new Button();
            btnPedestrian2 = new Button();
            userControlCrossroad1 = new Controls.UserControlCrossroad();
            panel2 = new Panel();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // statusStripGarage
            // 
            statusStripGarage.ImageScalingSize = new Size(20, 20);
            statusStripGarage.Location = new Point(0, 528);
            statusStripGarage.Name = "statusStripGarage";
            statusStripGarage.Size = new Size(1092, 22);
            statusStripGarage.TabIndex = 0;
            statusStripGarage.Text = "statusStrip1";
            // 
            // btnSettings
            // 
            btnSettings.Location = new Point(3, 2);
            btnSettings.Margin = new Padding(3, 2, 3, 2);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(155, 59);
            btnSettings.TabIndex = 25;
            btnSettings.Text = "Settings";
            btnSettings.UseVisualStyleBackColor = true;
            btnSettings.Click += btnSettings_Click;
            // 
            // btnEmergency
            // 
            btnEmergency.Location = new Point(3, 66);
            btnEmergency.Margin = new Padding(3, 2, 3, 2);
            btnEmergency.Name = "btnEmergency";
            btnEmergency.Size = new Size(155, 59);
            btnEmergency.TabIndex = 26;
            btnEmergency.Text = "Emergency button ";
            btnEmergency.UseVisualStyleBackColor = true;
            btnEmergency.Click += btnEmergency_Click;
            // 
            // btnEnd
            // 
            btnEnd.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnEnd.Location = new Point(3, 467);
            btnEnd.Margin = new Padding(3, 2, 3, 2);
            btnEnd.Name = "btnEnd";
            btnEnd.Size = new Size(155, 59);
            btnEnd.TabIndex = 27;
            btnEnd.Text = "End/close";
            btnEnd.UseVisualStyleBackColor = true;
            btnEnd.Click += btnEnd_Click;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(44, 268);
            radioButton1.Margin = new Padding(3, 2, 3, 2);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(94, 19);
            radioButton1.TabIndex = 28;
            radioButton1.TabStop = true;
            radioButton1.Text = "radioButton1";
            radioButton1.UseVisualStyleBackColor = true;
            radioButton1.CheckedChanged += radioButton1_CheckedChanged;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnEnd);
            panel1.Controls.Add(btnPedestrian1);
            panel1.Controls.Add(radioButton1);
            panel1.Controls.Add(btnPedestrian2);
            panel1.Controls.Add(btnEmergency);
            panel1.Controls.Add(btnSettings);
            panel1.Dock = DockStyle.Right;
            panel1.Location = new Point(927, 0);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(165, 528);
            panel1.TabIndex = 29;
            // 
            // btnPedestrian1
            // 
            btnPedestrian1.Location = new Point(14, 212);
            btnPedestrian1.Margin = new Padding(3, 2, 3, 2);
            btnPedestrian1.Name = "btnPedestrian1";
            btnPedestrian1.Size = new Size(82, 22);
            btnPedestrian1.TabIndex = 30;
            btnPedestrian1.Text = "Pedestrian";
            btnPedestrian1.UseVisualStyleBackColor = true;
            // 
            // btnPedestrian2
            // 
            btnPedestrian2.Location = new Point(14, 177);
            btnPedestrian2.Margin = new Padding(3, 2, 3, 2);
            btnPedestrian2.Name = "btnPedestrian2";
            btnPedestrian2.Size = new Size(82, 22);
            btnPedestrian2.TabIndex = 31;
            btnPedestrian2.Text = "Pedestrian";
            btnPedestrian2.UseVisualStyleBackColor = true;
            // 
            // userControlCrossroad1
            // 
            userControlCrossroad1.Dock = DockStyle.Fill;
            userControlCrossroad1.Location = new Point(0, 0);
            userControlCrossroad1.Margin = new Padding(3, 2, 3, 2);
            userControlCrossroad1.Name = "userControlCrossroad1";
            userControlCrossroad1.Size = new Size(927, 528);
            userControlCrossroad1.TabIndex = 32;
            // 
            // panel2
            // 
            panel2.Controls.Add(userControlCrossroad1);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(927, 528);
            panel2.TabIndex = 33;
            // 
            // Program3Form
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1092, 550);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(statusStripGarage);
            Name = "Program3Form";
            Text = "Program3";
            Load += Program3_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip statusStripGarage;
        private Button btnSettings;
        private Button btnEmergency;
        private Button btnEnd;
        private RadioButton radioButton1;
        private Panel panel1;
        private Button btnPedestrian1;
        private Button btnPedestrian2;
        private Controls.UserControlCrossroad userControlCrossroad1;
        private Panel panel2;
    }
}