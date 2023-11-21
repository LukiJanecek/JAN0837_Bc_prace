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
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // statusStripGarage
            // 
            statusStripGarage.ImageScalingSize = new Size(20, 20);
            statusStripGarage.Location = new Point(0, 578);
            statusStripGarage.Name = "statusStripGarage";
            statusStripGarage.Padding = new Padding(1, 0, 16, 0);
            statusStripGarage.Size = new Size(1045, 22);
            statusStripGarage.TabIndex = 0;
            statusStripGarage.Text = "statusStrip1";
            // 
            // btnSettings
            // 
            btnSettings.Location = new Point(3, 3);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(177, 79);
            btnSettings.TabIndex = 25;
            btnSettings.Text = "Settings";
            btnSettings.UseVisualStyleBackColor = true;
            btnSettings.Click += btnSettings_Click;
            // 
            // btnEmergency
            // 
            btnEmergency.Location = new Point(3, 88);
            btnEmergency.Name = "btnEmergency";
            btnEmergency.Size = new Size(177, 79);
            btnEmergency.TabIndex = 26;
            btnEmergency.Text = "Emergency button ";
            btnEmergency.UseVisualStyleBackColor = true;
            btnEmergency.Click += btnEmergency_Click;
            // 
            // btnEnd
            // 
            btnEnd.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnEnd.Location = new Point(3, 496);
            btnEnd.Name = "btnEnd";
            btnEnd.Size = new Size(177, 79);
            btnEnd.TabIndex = 27;
            btnEnd.Text = "End/close";
            btnEnd.UseVisualStyleBackColor = true;
            btnEnd.Click += btnEnd_Click;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(50, 358);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(117, 24);
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
            panel1.Location = new Point(856, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(189, 578);
            panel1.TabIndex = 29;
            // 
            // btnPedestrian1
            // 
            btnPedestrian1.Location = new Point(16, 283);
            btnPedestrian1.Name = "btnPedestrian1";
            btnPedestrian1.Size = new Size(94, 29);
            btnPedestrian1.TabIndex = 30;
            btnPedestrian1.Text = "Pedestrian";
            btnPedestrian1.UseVisualStyleBackColor = true;
            // 
            // btnPedestrian2
            // 
            btnPedestrian2.Location = new Point(16, 236);
            btnPedestrian2.Name = "btnPedestrian2";
            btnPedestrian2.Size = new Size(94, 29);
            btnPedestrian2.TabIndex = 31;
            btnPedestrian2.Text = "Pedestrian";
            btnPedestrian2.UseVisualStyleBackColor = true;
            // 
            // userControlCrossroad1
            // 
            userControlCrossroad1.Location = new Point(12, 12);
            userControlCrossroad1.Name = "userControlCrossroad1";
            userControlCrossroad1.Size = new Size(703, 548);
            userControlCrossroad1.TabIndex = 32;
            // 
            // Program3Form
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1045, 600);
            Controls.Add(userControlCrossroad1);
            Controls.Add(panel1);
            Controls.Add(statusStripGarage);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Program3Form";
            Text = "Program3";
            Load += Program3_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
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
    }
}