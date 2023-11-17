namespace Bc_prace
{
    partial class Program3SettingsForm
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
            statusStripGarageSettings = new StatusStrip();
            btnEnd = new Button();
            panel1 = new Panel();
            panel2 = new Panel();
            propertyGrid1 = new PropertyGrid();
            btnLoadData = new Button();
            btnSetData = new Button();
            btnCancel = new Button();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // statusStripGarageSettings
            // 
            statusStripGarageSettings.ImageScalingSize = new Size(20, 20);
            statusStripGarageSettings.Location = new Point(0, 428);
            statusStripGarageSettings.Name = "statusStripGarageSettings";
            statusStripGarageSettings.Size = new Size(797, 22);
            statusStripGarageSettings.TabIndex = 0;
            statusStripGarageSettings.Text = "statusStrip1";
            // 
            // btnEnd
            // 
            btnEnd.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnEnd.Location = new Point(6, 375);
            btnEnd.Name = "btnEnd";
            btnEnd.Size = new Size(94, 50);
            btnEnd.TabIndex = 1;
            btnEnd.Text = "End/close";
            btnEnd.UseVisualStyleBackColor = true;
            btnEnd.Click += btnEnd_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnEnd);
            panel1.Dock = DockStyle.Right;
            panel1.Location = new Point(686, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(111, 428);
            panel1.TabIndex = 2;
            // 
            // panel2
            // 
            panel2.Controls.Add(btnCancel);
            panel2.Controls.Add(btnLoadData);
            panel2.Controls.Add(btnSetData);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 368);
            panel2.Name = "panel2";
            panel2.Size = new Size(686, 60);
            panel2.TabIndex = 3;
            // 
            // propertyGrid1
            // 
            propertyGrid1.Dock = DockStyle.Fill;
            propertyGrid1.Location = new Point(0, 0);
            propertyGrid1.Name = "propertyGrid1";
            propertyGrid1.Size = new Size(686, 368);
            propertyGrid1.TabIndex = 4;
            // 
            // btnLoadData
            // 
            btnLoadData.Location = new Point(3, 3);
            btnLoadData.Name = "btnLoadData";
            btnLoadData.Size = new Size(94, 50);
            btnLoadData.TabIndex = 0;
            btnLoadData.Text = "Load data";
            btnLoadData.UseVisualStyleBackColor = true;
            // 
            // btnSetData
            // 
            btnSetData.Location = new Point(103, 3);
            btnSetData.Name = "btnSetData";
            btnSetData.Size = new Size(94, 50);
            btnSetData.TabIndex = 1;
            btnSetData.Text = "Set data";
            btnSetData.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(203, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(94, 50);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // Program3SettingsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(797, 450);
            Controls.Add(propertyGrid1);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(statusStripGarageSettings);
            Name = "Program3SettingsForm";
            Text = "Program3 -> Settings";
            Load += Program3Settings_Load;
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip statusStripGarageSettings;
        private Button btnEnd;
        private Panel panel1;
        private Panel panel2;
        private Button btnCancel;
        private Button btnLoadData;
        private Button btnSetData;
        private PropertyGrid propertyGrid1;
    }
}