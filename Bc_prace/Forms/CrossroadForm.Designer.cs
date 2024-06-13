using Bc_prace.Controls;

namespace Bc_prace
{
    partial class CrossroadForm
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
            rBtnCrossroadExtension3 = new RadioButton();
            rBtnCrossroadExtension2 = new RadioButton();
            rBtnCrossroadExtension1 = new RadioButton();
            Timer_read_actual = new System.Windows.Forms.Timer(components);
            panel2 = new Panel();
            userControlCrossroad1 = new UserControlCrossroad();
            groupBoxCrossroadVariant = new GroupBox();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            groupBoxCrossroadVariant.SuspendLayout();
            SuspendLayout();
            // 
            // statusStripCrossroad
            // 
            statusStripCrossroad.ImageScalingSize = new Size(20, 20);
            statusStripCrossroad.Location = new Point(0, 823);
            statusStripCrossroad.Name = "statusStripCrossroad";
            statusStripCrossroad.Padding = new Padding(1, 0, 16, 0);
            statusStripCrossroad.Size = new Size(1541, 22);
            statusStripCrossroad.TabIndex = 0;
            statusStripCrossroad.Text = "statusStrip1";
            // 
            // btnEmergency
            // 
            btnEmergency.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnEmergency.Location = new Point(6, 3);
            btnEmergency.Name = "btnEmergency";
            btnEmergency.Size = new Size(202, 79);
            btnEmergency.TabIndex = 26;
            btnEmergency.Text = "Emergency BTN";
            btnEmergency.UseVisualStyleBackColor = true;
            btnEmergency.Click += btnEmergency_Click;
            // 
            // btnEnd
            // 
            btnEnd.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnEnd.Location = new Point(3, 743);
            btnEnd.Name = "btnEnd";
            btnEnd.Size = new Size(205, 79);
            btnEnd.TabIndex = 27;
            btnEnd.Text = "Close";
            btnEnd.UseVisualStyleBackColor = true;
            btnEnd.Click += btnEnd_Click;
            // 
            // rBtnCrossroadBasic
            // 
            rBtnCrossroadBasic.AutoSize = true;
            rBtnCrossroadBasic.Location = new Point(26, 26);
            rBtnCrossroadBasic.Name = "rBtnCrossroadBasic";
            rBtnCrossroadBasic.Size = new Size(132, 24);
            rBtnCrossroadBasic.TabIndex = 28;
            rBtnCrossroadBasic.TabStop = true;
            rBtnCrossroadBasic.Text = "Basic crossroad";
            rBtnCrossroadBasic.UseVisualStyleBackColor = true;
            rBtnCrossroadBasic.CheckedChanged += rBtnCrossroadBasic_CheckedChanged;
            // 
            // panel1
            // 
            panel1.Controls.Add(groupBoxCrossroadVariant);
            panel1.Controls.Add(btnOffMode);
            panel1.Controls.Add(btnNightMode);
            panel1.Controls.Add(btnDayMode);
            panel1.Controls.Add(btnEnd);
            panel1.Controls.Add(btnEmergency);
            panel1.Dock = DockStyle.Right;
            panel1.Location = new Point(1327, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(214, 823);
            panel1.TabIndex = 29;
            // 
            // btnOffMode
            // 
            btnOffMode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnOffMode.Location = new Point(6, 259);
            btnOffMode.Name = "btnOffMode";
            btnOffMode.Size = new Size(201, 79);
            btnOffMode.TabIndex = 38;
            btnOffMode.Text = "Off mode";
            btnOffMode.UseVisualStyleBackColor = true;
            btnOffMode.Click += btnOffMode_Click;
            // 
            // btnNightMode
            // 
            btnNightMode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnNightMode.Location = new Point(6, 173);
            btnNightMode.Name = "btnNightMode";
            btnNightMode.Size = new Size(201, 79);
            btnNightMode.TabIndex = 37;
            btnNightMode.Text = "Night mode";
            btnNightMode.UseVisualStyleBackColor = true;
            btnNightMode.Click += btnNightMode_Click;
            // 
            // btnDayMode
            // 
            btnDayMode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnDayMode.Location = new Point(6, 88);
            btnDayMode.Name = "btnDayMode";
            btnDayMode.Size = new Size(202, 79);
            btnDayMode.TabIndex = 36;
            btnDayMode.Text = "Day mode";
            btnDayMode.UseVisualStyleBackColor = true;
            btnDayMode.Click += btnDayMode_Click;
            // 
            // rBtnCrossroadExtension3
            // 
            rBtnCrossroadExtension3.AutoSize = true;
            rBtnCrossroadExtension3.Location = new Point(26, 113);
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
            rBtnCrossroadExtension2.Location = new Point(26, 86);
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
            rBtnCrossroadExtension1.Location = new Point(26, 56);
            rBtnCrossroadExtension1.Name = "rBtnCrossroadExtension1";
            rBtnCrossroadExtension1.Size = new Size(175, 24);
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
            panel2.Controls.Add(userControlCrossroad1);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1327, 823);
            panel2.TabIndex = 30;
            // 
            // userControlCrossroad1
            // 
            userControlCrossroad1.Crossroad1BottomGREEN = false;
            userControlCrossroad1.Crossroad1BottomRED = false;
            userControlCrossroad1.Crossroad1BottomYELLOW = false;
            userControlCrossroad1.Crossroad1LeftCrosswalkGREEN1 = false;
            userControlCrossroad1.Crossroad1LeftCrosswalkGREEN2 = false;
            userControlCrossroad1.Crossroad1LeftCrosswalkRED1 = false;
            userControlCrossroad1.Crossroad1LeftCrosswalkRED2 = false;
            userControlCrossroad1.Crossroad1LeftGREEN = false;
            userControlCrossroad1.Crossroad1LeftRED = false;
            userControlCrossroad1.Crossroad1LeftYELLOW = false;
            userControlCrossroad1.Crossroad1RightGREEN = false;
            userControlCrossroad1.Crossroad1RightRED = false;
            userControlCrossroad1.Crossroad1RightYELLOW = false;
            userControlCrossroad1.Crossroad1TopCrosswalkGREEN1 = false;
            userControlCrossroad1.Crossroad1TopCrosswalkGREEN2 = false;
            userControlCrossroad1.Crossroad1TopCrosswalkRED1 = false;
            userControlCrossroad1.Crossroad1TopCrosswalkRED2 = false;
            userControlCrossroad1.Crossroad1TopGREEN = false;
            userControlCrossroad1.Crossroad1TopRED = false;
            userControlCrossroad1.Crossroad1TopYELLOW = false;
            userControlCrossroad1.Crossroad2BottomGREEN = false;
            userControlCrossroad1.Crossroad2BottomRED = false;
            userControlCrossroad1.Crossroad2BottomYELLOW = false;
            userControlCrossroad1.Crossroad2LeftCrosswalkGREEN1 = false;
            userControlCrossroad1.Crossroad2LeftCrosswalkGREEN2 = false;
            userControlCrossroad1.Crossroad2LeftCrosswalkRED1 = false;
            userControlCrossroad1.Crossroad2LeftCrosswalkRED2 = false;
            userControlCrossroad1.Crossroad2LeftGREEN = false;
            userControlCrossroad1.Crossroad2LeftRED = false;
            userControlCrossroad1.Crossroad2LeftYELLOW = false;
            userControlCrossroad1.Crossroad2RightCrosswalkGREEN1 = false;
            userControlCrossroad1.Crossroad2RightCrosswalkGREEN2 = false;
            userControlCrossroad1.Crossroad2RightCrosswalkRED1 = false;
            userControlCrossroad1.Crossroad2RightCrosswalkRED2 = false;
            userControlCrossroad1.Crossroad2RightGREEN = false;
            userControlCrossroad1.Crossroad2RightRED = false;
            userControlCrossroad1.Crossroad2RightYELLOW = false;
            userControlCrossroad1.Crossroad2TopGREEN = false;
            userControlCrossroad1.Crossroad2TopRED = false;
            userControlCrossroad1.Crossroad2TopYELLOW = false;
            userControlCrossroad1.CrossroadLeftTLeftCrosswalkGREEN1 = false;
            userControlCrossroad1.CrossroadLeftTLeftCrosswalkGREEN2 = false;
            userControlCrossroad1.CrossroadLeftTLeftCrosswalkRED1 = false;
            userControlCrossroad1.CrossroadLeftTLeftCrosswalkRED2 = false;
            userControlCrossroad1.CrossroadLeftTLeftGREEN = false;
            userControlCrossroad1.CrossroadLeftTLeftRED = false;
            userControlCrossroad1.CrossroadLeftTLeftYELLOW = false;
            userControlCrossroad1.CrossroadLeftTRightGREEN = false;
            userControlCrossroad1.CrossroadLeftTRightRED = false;
            userControlCrossroad1.CrossroadLeftTRightYELLOW = false;
            userControlCrossroad1.CrossroadLeftTTopGREEN = false;
            userControlCrossroad1.CrossroadLeftTTopRED = false;
            userControlCrossroad1.CrossroadLeftTTopYELLOW = false;
            userControlCrossroad1.CrossroadRightTLeftGREEN = false;
            userControlCrossroad1.CrossroadRightTLeftRED = false;
            userControlCrossroad1.CrossroadRightTLeftYELLOW = false;
            userControlCrossroad1.CrossroadRightTRightGREEN = false;
            userControlCrossroad1.CrossroadRightTRightRED = false;
            userControlCrossroad1.CrossroadRightTRightYELLOW = false;
            userControlCrossroad1.CrossroadRightTTopCrosswalkGREEN1 = false;
            userControlCrossroad1.CrossroadRightTTopCrosswalkGREEN2 = false;
            userControlCrossroad1.CrossroadRightTTopCrosswalkRED1 = false;
            userControlCrossroad1.CrossroadRightTTopCrosswalkRED2 = false;
            userControlCrossroad1.CrossroadRightTTopGREEN = false;
            userControlCrossroad1.CrossroadRightTTopRED = false;
            userControlCrossroad1.CrossroadRightTTopYELLOW = false;
            userControlCrossroad1.Dock = DockStyle.Fill;
            userControlCrossroad1.DrawBasicCrossroad = false;
            userControlCrossroad1.DrawCrossroadExtension1 = false;
            userControlCrossroad1.DrawCrossroadExtension2 = false;
            userControlCrossroad1.DrawCrossroadExtension3 = false;
            userControlCrossroad1.Location = new Point(0, 0);
            userControlCrossroad1.Name = "userControlCrossroad1";
            userControlCrossroad1.Size = new Size(1327, 823);
            userControlCrossroad1.TabIndex = 0;
            // 
            // groupBoxCrossroadVariant
            // 
            groupBoxCrossroadVariant.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBoxCrossroadVariant.Controls.Add(rBtnCrossroadBasic);
            groupBoxCrossroadVariant.Controls.Add(rBtnCrossroadExtension1);
            groupBoxCrossroadVariant.Controls.Add(rBtnCrossroadExtension2);
            groupBoxCrossroadVariant.Controls.Add(rBtnCrossroadExtension3);
            groupBoxCrossroadVariant.Location = new Point(6, 344);
            groupBoxCrossroadVariant.Name = "groupBoxCrossroadVariant";
            groupBoxCrossroadVariant.Size = new Size(250, 143);
            groupBoxCrossroadVariant.TabIndex = 40;
            groupBoxCrossroadVariant.TabStop = false;
            groupBoxCrossroadVariant.Text = "Choose crossroad variant:";
            // 
            // CrossroadForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1541, 845);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(statusStripCrossroad);
            DoubleBuffered = true;
            Margin = new Padding(3, 4, 3, 4);
            Name = "CrossroadForm";
            Text = "Crossroad";
            Load += Program3_Load;
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            groupBoxCrossroadVariant.ResumeLayout(false);
            groupBoxCrossroadVariant.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip statusStripCrossroad;
        private Button btnEmergency;
        private Button btnEnd;
        private RadioButton rBtnCrossroadBasic;
        private Panel panel1;
        private RadioButton rBtnCrossroadExtension3;
        private RadioButton rBtnCrossroadExtension2;
        private RadioButton rBtnCrossroadExtension1;
        private System.Windows.Forms.Timer Timer_read_actual;
        private Button btnOffMode;
        private Button btnNightMode;
        private Button btnDayMode;
        private Panel panel2;
        private UserControlCrossroad userControlCrossroad1;
        private GroupBox groupBoxCrossroadVariant;
    }
}