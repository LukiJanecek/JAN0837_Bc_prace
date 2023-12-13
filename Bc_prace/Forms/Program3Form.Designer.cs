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
            statusStripCrossroad = new StatusStrip();
            btnSettings = new Button();
            btnEmergency = new Button();
            btnEnd = new Button();
            rBtnCrossroadBasic = new RadioButton();
            panel1 = new Panel();
            rBtnCrossroadExtension3 = new RadioButton();
            rBtnCrossroadExtension2 = new RadioButton();
            rBtnCrossroadExtension1 = new RadioButton();
            btnPedestrian1 = new Button();
            btnPedestrian2 = new Button();
            userControlCrossroad1 = new Controls.UserControlCrossroad();
            panel2 = new Panel();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // statusStripCrossroad
            // 
            statusStripCrossroad.ImageScalingSize = new Size(20, 20);
            statusStripCrossroad.Location = new Point(0, 862);
            statusStripCrossroad.Name = "statusStripCrossroad";
            statusStripCrossroad.Padding = new Padding(1, 0, 16, 0);
            statusStripCrossroad.Size = new Size(1651, 22);
            statusStripCrossroad.TabIndex = 0;
            statusStripCrossroad.Text = "statusStrip1";
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
            btnEnd.Location = new Point(3, 781);
            btnEnd.Name = "btnEnd";
            btnEnd.Size = new Size(177, 79);
            btnEnd.TabIndex = 27;
            btnEnd.Text = "End/close";
            btnEnd.UseVisualStyleBackColor = true;
            btnEnd.Click += btnEnd_Click;
            // 
            // rBtnCrossroadBasic
            // 
            rBtnCrossroadBasic.AutoSize = true;
            rBtnCrossroadBasic.Location = new Point(6, 339);
            rBtnCrossroadBasic.Name = "rBtnCrossroadBasic";
            rBtnCrossroadBasic.Size = new Size(132, 24);
            rBtnCrossroadBasic.TabIndex = 28;
            rBtnCrossroadBasic.TabStop = true;
            rBtnCrossroadBasic.Text = "Basic crossroad";
            rBtnCrossroadBasic.UseVisualStyleBackColor = true;
            rBtnCrossroadBasic.CheckedChanged += radioButton1_CheckedChanged;
            // 
            // panel1
            // 
            panel1.Controls.Add(rBtnCrossroadExtension3);
            panel1.Controls.Add(rBtnCrossroadExtension2);
            panel1.Controls.Add(rBtnCrossroadExtension1);
            panel1.Controls.Add(btnEnd);
            panel1.Controls.Add(btnPedestrian1);
            panel1.Controls.Add(rBtnCrossroadBasic);
            panel1.Controls.Add(btnPedestrian2);
            panel1.Controls.Add(btnEmergency);
            panel1.Controls.Add(btnSettings);
            panel1.Dock = DockStyle.Right;
            panel1.Location = new Point(1462, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(189, 862);
            panel1.TabIndex = 29;
            // 
            // rBtnCrossroadExtension3
            // 
            rBtnCrossroadExtension3.AutoSize = true;
            rBtnCrossroadExtension3.Location = new Point(6, 429);
            rBtnCrossroadExtension3.Name = "rBtnCrossroadExtension3";
            rBtnCrossroadExtension3.Size = new Size(175, 24);
            rBtnCrossroadExtension3.TabIndex = 34;
            rBtnCrossroadExtension3.TabStop = true;
            rBtnCrossroadExtension3.Text = "Crossroad extension 3";
            rBtnCrossroadExtension3.UseVisualStyleBackColor = true;
            rBtnCrossroadExtension3.CheckedChanged += rBtnCrossroadExtension3_CheckedChanged;
            // 
            // rBtnCrossroadExtension2
            // 
            rBtnCrossroadExtension2.AutoSize = true;
            rBtnCrossroadExtension2.Location = new Point(6, 399);
            rBtnCrossroadExtension2.Name = "rBtnCrossroadExtension2";
            rBtnCrossroadExtension2.Size = new Size(175, 24);
            rBtnCrossroadExtension2.TabIndex = 33;
            rBtnCrossroadExtension2.TabStop = true;
            rBtnCrossroadExtension2.Text = "Crossroad extension 2";
            rBtnCrossroadExtension2.UseVisualStyleBackColor = true;
            rBtnCrossroadExtension2.CheckedChanged += rBtnCrossroadExtension2_CheckedChanged;
            // 
            // rBtnCrossroadExtension1
            // 
            rBtnCrossroadExtension1.AutoSize = true;
            rBtnCrossroadExtension1.Location = new Point(6, 369);
            rBtnCrossroadExtension1.Name = "rBtnCrossroadExtension1";
            rBtnCrossroadExtension1.Size = new Size(175, 24);
            rBtnCrossroadExtension1.TabIndex = 32;
            rBtnCrossroadExtension1.TabStop = true;
            rBtnCrossroadExtension1.Text = "Crossroad extension 1";
            rBtnCrossroadExtension1.UseVisualStyleBackColor = true;
            rBtnCrossroadExtension1.CheckedChanged += rBtnCrossroadExtension1_CheckedChanged;
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
            userControlCrossroad1.Dock = DockStyle.Fill;
            userControlCrossroad1.Location = new Point(0, 0);
            userControlCrossroad1.Name = "userControlCrossroad1";
            userControlCrossroad1.Size = new Size(1462, 862);
            userControlCrossroad1.TabIndex = 32;
            // 
            // panel2
            // 
            panel2.Controls.Add(userControlCrossroad1);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 0);
            panel2.Margin = new Padding(3, 4, 3, 4);
            panel2.Name = "panel2";
            panel2.Size = new Size(1462, 862);
            panel2.TabIndex = 33;
            // 
            // Program3Form
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1651, 884);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(statusStripCrossroad);
            Margin = new Padding(3, 4, 3, 4);
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

        private StatusStrip statusStripCrossroad;
        private Button btnSettings;
        private Button btnEmergency;
        private Button btnEnd;
        private RadioButton rBtnCrossroadBasic;
        private Panel panel1;
        private Button btnPedestrian1;
        private Button btnPedestrian2;
        private Controls.UserControlCrossroad userControlCrossroad1;
        private Panel panel2;
        private RadioButton rBtnCrossroadExtension3;
        private RadioButton rBtnCrossroadExtension2;
        private RadioButton rBtnCrossroadExtension1;
    }
}