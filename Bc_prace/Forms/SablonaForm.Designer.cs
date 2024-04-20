namespace Bc_prace.Forms
{
    partial class SablonaForm
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
            panel1 = new Panel();
            btnEmergency = new Button();
            btnEnd = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(btnEnd);
            panel1.Controls.Add(btnEmergency);
            panel1.Dock = DockStyle.Right;
            panel1.Location = new Point(881, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(164, 599);
            panel1.TabIndex = 0;
            // 
            // btnEmergency
            // 
            btnEmergency.Location = new Point(6, 3);
            btnEmergency.Name = "btnEmergency";
            btnEmergency.Size = new Size(155, 59);
            btnEmergency.TabIndex = 0;
            btnEmergency.Text = "Emergency BTN";
            btnEmergency.UseVisualStyleBackColor = true;
            btnEmergency.Click += btnEmergency_Click;
            // 
            // btnEnd
            // 
            btnEnd.Location = new Point(6, 537);
            btnEnd.Name = "btnEnd";
            btnEnd.Size = new Size(155, 59);
            btnEnd.TabIndex = 1;
            btnEnd.Text = "End/Close";
            btnEnd.UseVisualStyleBackColor = true;
            btnEnd.Click += btnEnd_Click;
            // 
            // Sablona
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1045, 599);
            Controls.Add(panel1);
            Name = "Sablona";
            Text = "Sablona";
            Load += Sablona_Load;
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button btnEmergency;
        private System.Windows.Forms.Timer Timer_read_actual;
        private Button btnEnd;
    }
}