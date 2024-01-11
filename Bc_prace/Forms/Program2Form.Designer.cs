namespace Bc_prace
{
    partial class Program2Form
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
            statusStripCarWash = new StatusStrip();
            btnSettings = new Button();
            btnEmergency = new Button();
            btnEnd = new Button();
            btnStartCarWash = new Button();
            panel1 = new Panel();
            btnSignalization = new Button();
            panel2 = new Panel();
            userControlCarWash1 = new Controls.UserControlCarWash();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // statusStripCarWash
            // 
            statusStripCarWash.ImageScalingSize = new Size(20, 20);
            statusStripCarWash.Location = new Point(0, 578);
            statusStripCarWash.Name = "statusStripCarWash";
            statusStripCarWash.Padding = new Padding(1, 0, 16, 0);
            statusStripCarWash.Size = new Size(1388, 22);
            statusStripCarWash.TabIndex = 0;
            statusStripCarWash.Text = "statusStrip1";
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
            btnEmergency.TabIndex = 37;
            btnEmergency.Text = "Emergency button";
            btnEmergency.UseVisualStyleBackColor = true;
            btnEmergency.Click += btnEmergency_Click;
            // 
            // btnEnd
            // 
            btnEnd.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnEnd.Location = new Point(3, 496);
            btnEnd.Name = "btnEnd";
            btnEnd.Size = new Size(177, 79);
            btnEnd.TabIndex = 38;
            btnEnd.Text = "End/close";
            btnEnd.UseVisualStyleBackColor = true;
            btnEnd.Click += btnEnd_Click;
            // 
            // btnStartCarWash
            // 
            btnStartCarWash.Location = new Point(12, 12);
            btnStartCarWash.Name = "btnStartCarWash";
            btnStartCarWash.Size = new Size(129, 89);
            btnStartCarWash.TabIndex = 39;
            btnStartCarWash.Text = "Start washing";
            btnStartCarWash.UseVisualStyleBackColor = true;
            btnStartCarWash.Click += btnStartCarWash_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnEnd);
            panel1.Controls.Add(btnSettings);
            panel1.Controls.Add(btnEmergency);
            panel1.Dock = DockStyle.Right;
            panel1.Location = new Point(1200, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(188, 578);
            panel1.TabIndex = 40;
            // 
            // btnSignalization
            // 
            btnSignalization.Location = new Point(12, 107);
            btnSignalization.Name = "btnSignalization";
            btnSignalization.Size = new Size(129, 89);
            btnSignalization.TabIndex = 41;
            btnSignalization.Text = "Signalization";
            btnSignalization.UseVisualStyleBackColor = true;
            btnSignalization.Click += btnSignalization_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(btnSignalization);
            panel2.Controls.Add(btnStartCarWash);
            panel2.Dock = DockStyle.Left;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(155, 578);
            panel2.TabIndex = 42;
            // 
            // userControlCarWash1
            // 
            userControlCarWash1.Dock = DockStyle.Fill;
            userControlCarWash1.Location = new Point(155, 0);
            userControlCarWash1.Name = "userControlCarWash1";
            userControlCarWash1.Size = new Size(1045, 578);
            userControlCarWash1.TabIndex = 43;
            // 
            // Program2Form
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1388, 600);
            Controls.Add(userControlCarWash1);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(statusStripCarWash);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Program2Form";
            Text = "Program2";
            Load += Program2_Load;
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip statusStripCarWash;
        private Button btnSettings;
        private Button btnEmergency;
        private Button btnEnd;
        private Button btnStartCarWash;
        private Panel panel1;
        private Button btnSignalization;
        private System.Windows.Forms.Timer Timer_read_from_PLC;
        private Panel panel2;
        private Controls.UserControlCarWash userControlCarWash1;
    }
}