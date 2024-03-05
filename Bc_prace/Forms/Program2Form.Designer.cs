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
            components = new System.ComponentModel.Container();
            statusStripCarWash = new StatusStrip();
            btnEmergency = new Button();
            btnEnd = new Button();
            btnStartCarWash = new Button();
            panel1 = new Panel();
            btnTest2 = new Button();
            btnTest = new Button();
            btnSignalization = new Button();
            panel2 = new Panel();
            btnCarMoveRIGHT = new Button();
            btnCarMoveLEFT = new Button();
            userControlCarWash1 = new Controls.UserControlCarWash();
            Timer_read_actual = new System.Windows.Forms.Timer(components);
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // statusStripCarWash
            // 
            statusStripCarWash.ImageScalingSize = new Size(20, 20);
            statusStripCarWash.Location = new Point(0, 582);
            statusStripCarWash.Name = "statusStripCarWash";
            statusStripCarWash.Padding = new Padding(1, 0, 16, 0);
            statusStripCarWash.Size = new Size(1525, 22);
            statusStripCarWash.TabIndex = 0;
            statusStripCarWash.Text = "statusStrip1";
            // 
            // btnEmergency
            // 
            btnEmergency.Location = new Point(6, 3);
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
            btnEnd.Location = new Point(3, 499);
            btnEnd.Name = "btnEnd";
            btnEnd.Size = new Size(177, 79);
            btnEnd.TabIndex = 38;
            btnEnd.Text = "End/close";
            btnEnd.UseVisualStyleBackColor = true;
            btnEnd.Click += btnEnd_Click;
            // 
            // btnStartCarWash
            // 
            btnStartCarWash.Location = new Point(11, 12);
            btnStartCarWash.Name = "btnStartCarWash";
            btnStartCarWash.Size = new Size(130, 91);
            btnStartCarWash.TabIndex = 39;
            btnStartCarWash.Text = "Start washing";
            btnStartCarWash.UseVisualStyleBackColor = true;
            btnStartCarWash.Click += btnStartCarWash_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnTest2);
            panel1.Controls.Add(btnTest);
            panel1.Controls.Add(btnEnd);
            panel1.Controls.Add(btnEmergency);
            panel1.Dock = DockStyle.Right;
            panel1.Location = new Point(1338, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(187, 582);
            panel1.TabIndex = 40;
            // 
            // btnTest2
            // 
            btnTest2.Location = new Point(6, 461);
            btnTest2.Margin = new Padding(3, 4, 3, 4);
            btnTest2.Name = "btnTest2";
            btnTest2.Size = new Size(86, 31);
            btnTest2.TabIndex = 40;
            btnTest2.Text = "Test2";
            btnTest2.UseVisualStyleBackColor = true;
            btnTest2.Click += btnTest2_Click;
            // 
            // btnTest
            // 
            btnTest.Location = new Point(94, 461);
            btnTest.Margin = new Padding(3, 4, 3, 4);
            btnTest.Name = "btnTest";
            btnTest.Size = new Size(86, 31);
            btnTest.TabIndex = 39;
            btnTest.Text = "Test";
            btnTest.UseVisualStyleBackColor = true;
            btnTest.Click += btnTest_Click;
            // 
            // btnSignalization
            // 
            btnSignalization.Enabled = false;
            btnSignalization.Location = new Point(11, 107);
            btnSignalization.Name = "btnSignalization";
            btnSignalization.Size = new Size(130, 91);
            btnSignalization.TabIndex = 41;
            btnSignalization.Text = "Signalization";
            btnSignalization.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            panel2.Controls.Add(btnCarMoveRIGHT);
            panel2.Controls.Add(btnCarMoveLEFT);
            panel2.Controls.Add(btnSignalization);
            panel2.Controls.Add(btnStartCarWash);
            panel2.Dock = DockStyle.Left;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(155, 582);
            panel2.TabIndex = 42;
            // 
            // btnCarMoveRIGHT
            // 
            btnCarMoveRIGHT.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCarMoveRIGHT.Location = new Point(77, 532);
            btnCarMoveRIGHT.Name = "btnCarMoveRIGHT";
            btnCarMoveRIGHT.Size = new Size(65, 45);
            btnCarMoveRIGHT.TabIndex = 43;
            btnCarMoveRIGHT.Text = ">";
            btnCarMoveRIGHT.UseVisualStyleBackColor = true;
            btnCarMoveRIGHT.Click += btnCarMoveRIGHT_Click;
            // 
            // btnCarMoveLEFT
            // 
            btnCarMoveLEFT.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnCarMoveLEFT.Location = new Point(11, 532);
            btnCarMoveLEFT.Name = "btnCarMoveLEFT";
            btnCarMoveLEFT.Size = new Size(65, 45);
            btnCarMoveLEFT.TabIndex = 42;
            btnCarMoveLEFT.Text = "<";
            btnCarMoveLEFT.UseVisualStyleBackColor = true;
            btnCarMoveLEFT.Click += btnCarMoveLEFT_Click;
            // 
            // userControlCarWash1
            // 
            userControlCarWash1.Dock = DockStyle.Fill;
            userControlCarWash1.Location = new Point(155, 0);
            userControlCarWash1.Name = "userControlCarWash1";
            userControlCarWash1.Size = new Size(1183, 582);
            userControlCarWash1.TabIndex = 43;
            // 
            // Timer_read_actual
            // 
            Timer_read_actual.Tick += Timer_read_actual_Tick;
            // 
            // Program2Form
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1525, 604);
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
        private Button btnEmergency;
        private Button btnEnd;
        private Button btnStartCarWash;
        private Panel panel1;
        private Button btnSignalization;
        private System.Windows.Forms.Timer Timer_read_actual;
        private Panel panel2;
        private Controls.UserControlCarWash userControlCarWash1;
        private Button btnTest;
        private Button btnTest2;
        private Button btnCarMoveLEFT;
        private Button btnCarMoveRIGHT;
    }
}