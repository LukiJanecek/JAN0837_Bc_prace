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
            components = new System.ComponentModel.Container();
            statusStripCrossroad = new StatusStrip();
            btnEmergency = new Button();
            btnEnd = new Button();
            rBtnCrossroadBasic = new RadioButton();
            panel1 = new Panel();
            btnOffMode = new Button();
            btnNightMode = new Button();
            btnDayMode = new Button();
            btnTest = new Button();
            rBtnCrossroadExtension3 = new RadioButton();
            rBtnCrossroadExtension2 = new RadioButton();
            rBtnCrossroadExtension1 = new RadioButton();
            Timer_read_actual = new System.Windows.Forms.Timer(components);
            panel2 = new Panel();
            //userControlCrossroad1 = new Controls.UserControlCrossroad(this);
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // statusStripCrossroad
            // 
            statusStripCrossroad.ImageScalingSize = new Size(20, 20);
            statusStripCrossroad.Location = new Point(0, 612);
            statusStripCrossroad.Name = "statusStripCrossroad";
            statusStripCrossroad.Size = new Size(1348, 22);
            statusStripCrossroad.TabIndex = 0;
            statusStripCrossroad.Text = "statusStrip1";
            // 
            // btnEmergency
            // 
            btnEmergency.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnEmergency.Location = new Point(5, 2);
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
            btnEnd.Location = new Point(3, 552);
            btnEnd.Margin = new Padding(3, 2, 3, 2);
            btnEnd.Name = "btnEnd";
            btnEnd.Size = new Size(155, 59);
            btnEnd.TabIndex = 27;
            btnEnd.Text = "End/close";
            btnEnd.UseVisualStyleBackColor = true;
            btnEnd.Click += btnEnd_Click;
            // 
            // rBtnCrossroadBasic
            // 
            rBtnCrossroadBasic.AutoSize = true;
            rBtnCrossroadBasic.Location = new Point(5, 257);
            rBtnCrossroadBasic.Margin = new Padding(3, 2, 3, 2);
            rBtnCrossroadBasic.Name = "rBtnCrossroadBasic";
            rBtnCrossroadBasic.Size = new Size(106, 19);
            rBtnCrossroadBasic.TabIndex = 28;
            rBtnCrossroadBasic.TabStop = true;
            rBtnCrossroadBasic.Text = "Basic crossroad";
            rBtnCrossroadBasic.UseVisualStyleBackColor = true;
            rBtnCrossroadBasic.CheckedChanged += radioButton1_CheckedChanged;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnOffMode);
            panel1.Controls.Add(btnNightMode);
            panel1.Controls.Add(btnDayMode);
            panel1.Controls.Add(btnTest);
            panel1.Controls.Add(rBtnCrossroadExtension3);
            panel1.Controls.Add(rBtnCrossroadExtension2);
            panel1.Controls.Add(rBtnCrossroadExtension1);
            panel1.Controls.Add(btnEnd);
            panel1.Controls.Add(rBtnCrossroadBasic);
            panel1.Controls.Add(btnEmergency);
            panel1.Dock = DockStyle.Right;
            panel1.Location = new Point(1183, 0);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(165, 612);
            panel1.TabIndex = 29;
            // 
            // btnOffMode
            // 
            btnOffMode.Location = new Point(5, 194);
            btnOffMode.Margin = new Padding(3, 2, 3, 2);
            btnOffMode.Name = "btnOffMode";
            btnOffMode.Size = new Size(155, 59);
            btnOffMode.TabIndex = 38;
            btnOffMode.Text = "Off mode";
            btnOffMode.UseVisualStyleBackColor = true;
            btnOffMode.Click += btnOffMode_Click;
            // 
            // btnNightMode
            // 
            btnNightMode.Location = new Point(5, 130);
            btnNightMode.Margin = new Padding(3, 2, 3, 2);
            btnNightMode.Name = "btnNightMode";
            btnNightMode.Size = new Size(155, 59);
            btnNightMode.TabIndex = 37;
            btnNightMode.Text = "Night mode";
            btnNightMode.UseVisualStyleBackColor = true;
            btnNightMode.Click += btnNightMode_Click;
            // 
            // btnDayMode
            // 
            btnDayMode.Location = new Point(5, 66);
            btnDayMode.Margin = new Padding(3, 2, 3, 2);
            btnDayMode.Name = "btnDayMode";
            btnDayMode.Size = new Size(155, 59);
            btnDayMode.TabIndex = 36;
            btnDayMode.Text = "Day mode";
            btnDayMode.UseVisualStyleBackColor = true;
            btnDayMode.Click += btnDayMode_Click;
            // 
            // btnTest
            // 
            btnTest.Location = new Point(34, 391);
            btnTest.Name = "btnTest";
            btnTest.Size = new Size(75, 23);
            btnTest.TabIndex = 35;
            btnTest.Text = "Test";
            btnTest.UseVisualStyleBackColor = true;
            btnTest.Click += btnTest_Click;
            // 
            // rBtnCrossroadExtension3
            // 
            rBtnCrossroadExtension3.AutoSize = true;
            rBtnCrossroadExtension3.Location = new Point(5, 325);
            rBtnCrossroadExtension3.Margin = new Padding(3, 2, 3, 2);
            rBtnCrossroadExtension3.Name = "rBtnCrossroadExtension3";
            rBtnCrossroadExtension3.Size = new Size(141, 19);
            rBtnCrossroadExtension3.TabIndex = 34;
            rBtnCrossroadExtension3.TabStop = true;
            rBtnCrossroadExtension3.Text = "Crossroad extension 3";
            rBtnCrossroadExtension3.UseVisualStyleBackColor = true;
            rBtnCrossroadExtension3.CheckedChanged += rBtnCrossroadExtension3_CheckedChanged;
            // 
            // rBtnCrossroadExtension2
            // 
            rBtnCrossroadExtension2.AutoSize = true;
            rBtnCrossroadExtension2.Location = new Point(5, 302);
            rBtnCrossroadExtension2.Margin = new Padding(3, 2, 3, 2);
            rBtnCrossroadExtension2.Name = "rBtnCrossroadExtension2";
            rBtnCrossroadExtension2.Size = new Size(141, 19);
            rBtnCrossroadExtension2.TabIndex = 33;
            rBtnCrossroadExtension2.TabStop = true;
            rBtnCrossroadExtension2.Text = "Crossroad extension 2";
            rBtnCrossroadExtension2.UseVisualStyleBackColor = true;
            rBtnCrossroadExtension2.CheckedChanged += rBtnCrossroadExtension2_CheckedChanged;
            // 
            // rBtnCrossroadExtension1
            // 
            rBtnCrossroadExtension1.AutoSize = true;
            rBtnCrossroadExtension1.Location = new Point(5, 280);
            rBtnCrossroadExtension1.Margin = new Padding(3, 2, 3, 2);
            rBtnCrossroadExtension1.Name = "rBtnCrossroadExtension1";
            rBtnCrossroadExtension1.Size = new Size(141, 19);
            rBtnCrossroadExtension1.TabIndex = 32;
            rBtnCrossroadExtension1.TabStop = true;
            rBtnCrossroadExtension1.Text = "Crossroad extension 1";
            rBtnCrossroadExtension1.UseVisualStyleBackColor = true;
            rBtnCrossroadExtension1.CheckedChanged += rBtnCrossroadExtension1_CheckedChanged;
            // 
            // Timer_read_actual
            // 
            Timer_read_actual.Tick += Timer_read_actual_Tick;
            // 
            // panel2
            // 
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1183, 612);
            panel2.TabIndex = 33;
            // 
            // Program3Form
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1348, 634);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(statusStripCrossroad);
            Name = "Program3Form";
            Text = "Program3";
            Load += Program3_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            //Controls.Add(this.userControlCrossroad1);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip statusStripCrossroad;
        private Button btnEmergency;
        private Button btnEnd;
        private RadioButton rBtnCrossroadBasic;
        private Panel panel1;
        private Controls.UserControlCrossroad userControlCrossroad1;
        private RadioButton rBtnCrossroadExtension3;
        private RadioButton rBtnCrossroadExtension2;
        private RadioButton rBtnCrossroadExtension1;
        private System.Windows.Forms.Timer Timer_read_actual;
        private Button btnTest;
        private Button btnOffMode;
        private Button btnNightMode;
        private Button btnDayMode;
        private Panel panel2;
    }
}