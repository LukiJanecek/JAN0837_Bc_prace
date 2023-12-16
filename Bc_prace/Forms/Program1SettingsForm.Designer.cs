namespace Bc_prace
{
    partial class Program1SettingsForm
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
            btnEmergencySim = new Button();
            btnDoorCLOSE = new Button();
            btnDoorOPEN = new Button();
            statusStripElevatorSettings = new StatusStrip();
            btnEnd = new Button();
            propertyGridElevator = new PropertyGrid();
            btnSetData = new Button();
            btnCancel = new Button();
            panel1 = new Panel();
            panel3 = new Panel();
            btnLoadData = new Button();
            panel2 = new Panel();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // btnEmergencySim
            // 
            btnEmergencySim.Location = new Point(3, 4);
            btnEmergencySim.Name = "btnEmergencySim";
            btnEmergencySim.Size = new Size(103, 55);
            btnEmergencySim.TabIndex = 0;
            btnEmergencySim.Text = "Emergency simulation";
            btnEmergencySim.UseVisualStyleBackColor = true;
            btnEmergencySim.Click += btnEmergencySim_Click;
            // 
            // btnDoorCLOSE
            // 
            btnDoorCLOSE.Location = new Point(3, 121);
            btnDoorCLOSE.Name = "btnDoorCLOSE";
            btnDoorCLOSE.Size = new Size(103, 52);
            btnDoorCLOSE.TabIndex = 1;
            btnDoorCLOSE.Text = "Close door ";
            btnDoorCLOSE.UseVisualStyleBackColor = true;
            btnDoorCLOSE.Click += btnDoorCLOSE_Click;
            // 
            // btnDoorOPEN
            // 
            btnDoorOPEN.Location = new Point(3, 64);
            btnDoorOPEN.Name = "btnDoorOPEN";
            btnDoorOPEN.Size = new Size(103, 52);
            btnDoorOPEN.TabIndex = 2;
            btnDoorOPEN.Text = "Open door";
            btnDoorOPEN.UseVisualStyleBackColor = true;
            btnDoorOPEN.Click += btnDoorOPEN_Click;
            // 
            // statusStripElevatorSettings
            // 
            statusStripElevatorSettings.ImageScalingSize = new Size(20, 20);
            statusStripElevatorSettings.Location = new Point(0, 456);
            statusStripElevatorSettings.Name = "statusStripElevatorSettings";
            statusStripElevatorSettings.Size = new Size(590, 24);
            statusStripElevatorSettings.TabIndex = 12;
            statusStripElevatorSettings.Text = "statusStrip1";
            // 
            // btnEnd
            // 
            btnEnd.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnEnd.Location = new Point(3, 394);
            btnEnd.Name = "btnEnd";
            btnEnd.Size = new Size(110, 52);
            btnEnd.TabIndex = 13;
            btnEnd.Text = "End/close";
            btnEnd.UseVisualStyleBackColor = true;
            btnEnd.Click += btnEnd_Click;
            // 
            // propertyGridElevator
            // 
            propertyGridElevator.Dock = DockStyle.Fill;
            propertyGridElevator.Location = new Point(0, 0);
            propertyGridElevator.Margin = new Padding(3, 4, 3, 4);
            propertyGridElevator.Name = "propertyGridElevator";
            propertyGridElevator.Size = new Size(590, 456);
            propertyGridElevator.TabIndex = 14;
            // 
            // btnSetData
            // 
            btnSetData.Location = new Point(124, 4);
            btnSetData.Margin = new Padding(3, 4, 3, 4);
            btnSetData.Name = "btnSetData";
            btnSetData.Size = new Size(115, 52);
            btnSetData.TabIndex = 15;
            btnSetData.Text = "Set data";
            btnSetData.UseVisualStyleBackColor = true;
            btnSetData.Click += button1_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(245, 4);
            btnCancel.Margin = new Padding(3, 4, 3, 4);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(115, 52);
            btnCancel.TabIndex = 16;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(propertyGridElevator);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(590, 456);
            panel1.TabIndex = 17;
            // 
            // panel3
            // 
            panel3.Controls.Add(btnLoadData);
            panel3.Controls.Add(btnSetData);
            panel3.Controls.Add(btnCancel);
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(0, 379);
            panel3.Margin = new Padding(3, 4, 3, 4);
            panel3.Name = "panel3";
            panel3.Size = new Size(590, 77);
            panel3.TabIndex = 17;
            // 
            // btnLoadData
            // 
            btnLoadData.Location = new Point(3, 4);
            btnLoadData.Name = "btnLoadData";
            btnLoadData.Size = new Size(115, 52);
            btnLoadData.TabIndex = 17;
            btnLoadData.Text = "Load data";
            btnLoadData.UseVisualStyleBackColor = true;
            btnLoadData.Click += btnLoadData_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(btnEmergencySim);
            panel2.Controls.Add(btnDoorOPEN);
            panel2.Controls.Add(btnEnd);
            panel2.Controls.Add(btnDoorCLOSE);
            panel2.Dock = DockStyle.Right;
            panel2.Location = new Point(471, 0);
            panel2.Margin = new Padding(3, 4, 3, 4);
            panel2.Name = "panel2";
            panel2.Size = new Size(119, 456);
            panel2.TabIndex = 18;
            //
            //Timer_read_from_PLC
            //
            Timer_read_from_PLC.Tick += Timer_read_from_PLC_Tick;
            // 
            // Program1SettingsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(590, 480);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(statusStripElevatorSettings);
            Name = "Program1SettingsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Program1 -> Settings";
            Load += Program1Settings_Load;
            panel1.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnEmergencySim;
        private Button btnDoorCLOSE;
        private Button btnDoorOPEN;
        private StatusStrip statusStripElevatorSettings;
        private Button btnEnd;
        private PropertyGrid propertyGridElevator;
        private Button btnSetData;
        private Button btnCancel;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Button btnLoadData;
        private System.Windows.Forms.Timer Timer_read_from_PLC;
    }
}