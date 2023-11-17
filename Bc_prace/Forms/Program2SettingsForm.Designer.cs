namespace Bc_prace
{
    partial class Program2SettingsForm
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
            btnDoor1OPEN = new Button();
            btnDoor1CLOSE = new Button();
            btnDoor2OPEN = new Button();
            btnDoor2CLOSE = new Button();
            btnEmergencySim = new Button();
            statusStripCarWashSettings = new StatusStrip();
            btnEnd = new Button();
            btnSelfCleaning = new Button();
            btnFillWax = new Button();
            btnFillSoap = new Button();
            btnFillActiveFoam = new Button();
            lblActiveFoamState = new Label();
            lblWaxState = new Label();
            lblSoapState = new Label();
            propertyGridCarWash = new PropertyGrid();
            panel1 = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            btnLoadData = new Button();
            btnSetData = new Button();
            btnCancel = new Button();
            panel4 = new Panel();
            panel5 = new Panel();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            SuspendLayout();
            // 
            // btnDoor1OPEN
            // 
            btnDoor1OPEN.Location = new Point(19, 10);
            btnDoor1OPEN.Name = "btnDoor1OPEN";
            btnDoor1OPEN.Size = new Size(107, 29);
            btnDoor1OPEN.TabIndex = 0;
            btnDoor1OPEN.Text = "Door1 OPEN";
            btnDoor1OPEN.UseVisualStyleBackColor = true;
            // 
            // btnDoor1CLOSE
            // 
            btnDoor1CLOSE.Location = new Point(19, 44);
            btnDoor1CLOSE.Name = "btnDoor1CLOSE";
            btnDoor1CLOSE.Size = new Size(107, 29);
            btnDoor1CLOSE.TabIndex = 1;
            btnDoor1CLOSE.Text = "Door1 CLOSE";
            btnDoor1CLOSE.UseVisualStyleBackColor = true;
            // 
            // btnDoor2OPEN
            // 
            btnDoor2OPEN.Location = new Point(19, 80);
            btnDoor2OPEN.Name = "btnDoor2OPEN";
            btnDoor2OPEN.Size = new Size(107, 29);
            btnDoor2OPEN.TabIndex = 2;
            btnDoor2OPEN.Text = "Door2 OPEN";
            btnDoor2OPEN.UseVisualStyleBackColor = true;
            // 
            // btnDoor2CLOSE
            // 
            btnDoor2CLOSE.Location = new Point(19, 115);
            btnDoor2CLOSE.Name = "btnDoor2CLOSE";
            btnDoor2CLOSE.Size = new Size(107, 29);
            btnDoor2CLOSE.TabIndex = 3;
            btnDoor2CLOSE.Text = "Door2 CLOSE";
            btnDoor2CLOSE.UseVisualStyleBackColor = true;
            // 
            // btnEmergencySim
            // 
            btnEmergencySim.Location = new Point(10, 3);
            btnEmergencySim.Name = "btnEmergencySim";
            btnEmergencySim.Size = new Size(107, 64);
            btnEmergencySim.TabIndex = 4;
            btnEmergencySim.Text = "Emergency simulation";
            btnEmergencySim.UseVisualStyleBackColor = true;
            btnEmergencySim.Click += btnEmergencySim_Click;
            // 
            // statusStripCarWashSettings
            // 
            statusStripCarWashSettings.ImageScalingSize = new Size(20, 20);
            statusStripCarWashSettings.Location = new Point(0, 381);
            statusStripCarWashSettings.Name = "statusStripCarWashSettings";
            statusStripCarWashSettings.Size = new Size(818, 22);
            statusStripCarWashSettings.TabIndex = 8;
            statusStripCarWashSettings.Text = "statusStrip1";
            // 
            // btnEnd
            // 
            btnEnd.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnEnd.Location = new Point(10, 314);
            btnEnd.Name = "btnEnd";
            btnEnd.Size = new Size(107, 64);
            btnEnd.TabIndex = 9;
            btnEnd.Text = "End/close";
            btnEnd.UseVisualStyleBackColor = true;
            btnEnd.Click += btnEnd_Click;
            // 
            // btnSelfCleaning
            // 
            btnSelfCleaning.Location = new Point(3, 314);
            btnSelfCleaning.Name = "btnSelfCleaning";
            btnSelfCleaning.Size = new Size(107, 64);
            btnSelfCleaning.TabIndex = 10;
            btnSelfCleaning.Text = "Self-cleaning";
            btnSelfCleaning.UseVisualStyleBackColor = true;
            // 
            // btnFillWax
            // 
            btnFillWax.Location = new Point(3, 133);
            btnFillWax.Name = "btnFillWax";
            btnFillWax.Size = new Size(107, 64);
            btnFillWax.TabIndex = 11;
            btnFillWax.Text = "Fill wax";
            btnFillWax.UseVisualStyleBackColor = true;
            // 
            // btnFillSoap
            // 
            btnFillSoap.Location = new Point(3, 43);
            btnFillSoap.Name = "btnFillSoap";
            btnFillSoap.Size = new Size(107, 64);
            btnFillSoap.TabIndex = 12;
            btnFillSoap.Text = "Fill soap";
            btnFillSoap.UseVisualStyleBackColor = true;
            // 
            // btnFillActiveFoam
            // 
            btnFillActiveFoam.Location = new Point(3, 224);
            btnFillActiveFoam.Name = "btnFillActiveFoam";
            btnFillActiveFoam.Size = new Size(107, 64);
            btnFillActiveFoam.TabIndex = 13;
            btnFillActiveFoam.Text = "Fill active foam";
            btnFillActiveFoam.UseVisualStyleBackColor = true;
            // 
            // lblActiveFoamState
            // 
            lblActiveFoamState.AutoSize = true;
            lblActiveFoamState.Location = new Point(3, 202);
            lblActiveFoamState.Name = "lblActiveFoamState";
            lblActiveFoamState.Size = new Size(128, 20);
            lblActiveFoamState.TabIndex = 14;
            lblActiveFoamState.Text = "Active foam state:";
            // 
            // lblWaxState
            // 
            lblWaxState.AutoSize = true;
            lblWaxState.Location = new Point(3, 110);
            lblWaxState.Name = "lblWaxState";
            lblWaxState.Size = new Size(76, 20);
            lblWaxState.TabIndex = 15;
            lblWaxState.Text = "Wax state:";
            // 
            // lblSoapState
            // 
            lblSoapState.AutoSize = true;
            lblSoapState.Location = new Point(3, 19);
            lblSoapState.Name = "lblSoapState";
            lblSoapState.Size = new Size(82, 20);
            lblSoapState.TabIndex = 16;
            lblSoapState.Text = "Soap state:";
            // 
            // propertyGridCarWash
            // 
            propertyGridCarWash.Dock = DockStyle.Fill;
            propertyGridCarWash.Location = new Point(0, 0);
            propertyGridCarWash.Margin = new Padding(3, 4, 3, 4);
            propertyGridCarWash.Name = "propertyGridCarWash";
            propertyGridCarWash.Size = new Size(379, 381);
            propertyGridCarWash.TabIndex = 17;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnEnd);
            panel1.Controls.Add(btnEmergencySim);
            panel1.Dock = DockStyle.Right;
            panel1.Location = new Point(698, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(120, 381);
            panel1.TabIndex = 18;
            // 
            // panel2
            // 
            panel2.Controls.Add(btnFillWax);
            panel2.Controls.Add(btnSelfCleaning);
            panel2.Controls.Add(btnFillSoap);
            panel2.Controls.Add(lblSoapState);
            panel2.Controls.Add(btnFillActiveFoam);
            panel2.Controls.Add(lblWaxState);
            panel2.Controls.Add(lblActiveFoamState);
            panel2.Dock = DockStyle.Left;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(175, 381);
            panel2.TabIndex = 19;
            panel2.Paint += panel2_Paint;
            // 
            // panel3
            // 
            panel3.Controls.Add(btnDoor1CLOSE);
            panel3.Controls.Add(btnDoor1OPEN);
            panel3.Controls.Add(btnDoor2OPEN);
            panel3.Controls.Add(btnDoor2CLOSE);
            panel3.Dock = DockStyle.Left;
            panel3.Location = new Point(175, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(144, 381);
            panel3.TabIndex = 20;
            // 
            // btnLoadData
            // 
            btnLoadData.Location = new Point(6, 18);
            btnLoadData.Name = "btnLoadData";
            btnLoadData.Size = new Size(107, 64);
            btnLoadData.TabIndex = 4;
            btnLoadData.Text = "Load data";
            btnLoadData.UseVisualStyleBackColor = true;
            // 
            // btnSetData
            // 
            btnSetData.Location = new Point(119, 18);
            btnSetData.Name = "btnSetData";
            btnSetData.Size = new Size(107, 64);
            btnSetData.TabIndex = 5;
            btnSetData.Text = "Set data";
            btnSetData.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(232, 18);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(107, 64);
            btnCancel.TabIndex = 6;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            panel4.Controls.Add(propertyGridCarWash);
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(319, 0);
            panel4.Name = "panel4";
            panel4.Size = new Size(379, 381);
            panel4.TabIndex = 21;
            // 
            // panel5
            // 
            panel5.Controls.Add(btnCancel);
            panel5.Controls.Add(btnLoadData);
            panel5.Controls.Add(btnSetData);
            panel5.Dock = DockStyle.Bottom;
            panel5.Location = new Point(319, 296);
            panel5.Name = "panel5";
            panel5.Size = new Size(379, 85);
            panel5.TabIndex = 22;
            // 
            // Program2SettingsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(818, 403);
            Controls.Add(panel5);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(statusStripCarWashSettings);
            Name = "Program2SettingsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Program2 -> Settings";
            Load += Program2Settings_Load;
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel5.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnDoor1OPEN;
        private Button btnDoor1CLOSE;
        private Button btnDoor2OPEN;
        private Button btnDoor2CLOSE;
        private Button btnEmergencySim;
        private StatusStrip statusStripCarWashSettings;
        private Button btnEnd;
        private Button btnSelfCleaning;
        private Button btnFillWax;
        private Button btnFillSoap;
        private Button btnFillActiveFoam;
        private Label lblActiveFoamState;
        private Label lblWaxState;
        private Label lblSoapState;
        private PropertyGrid propertyGridCarWash;
        private Panel panel1;
        private Panel panel2;
        private Button btnSetData;
        private Panel panel3;
        private Button btnCancel;
        private Button btnLoadData;
        private Panel panel4;
        private Panel panel5;
    }
}