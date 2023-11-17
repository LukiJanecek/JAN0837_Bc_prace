namespace Bc_prace
{
    partial class ChooseOptionForm
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
            btnProgram1 = new Button();
            btnProgram2 = new Button();
            btnProgram3 = new Button();
            btnConnect = new Button();
            txtBoxPLCIP = new TextBox();
            lblTypePLCIP = new Label();
            lblChooseSIM = new Label();
            statusStrip1 = new StatusStrip();
            btnEnd = new Button();
            panel2 = new Panel();
            panel3 = new Panel();
            panel1 = new Panel();
            components = new System.ComponentModel.Container();
            Timer_read_from_PLC = new System.Windows.Forms.Timer(components);
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // btnProgram1
            // 
            btnProgram1.Location = new Point(22, 66);
            btnProgram1.Name = "btnProgram1";
            btnProgram1.Size = new Size(94, 52);
            btnProgram1.TabIndex = 0;
            btnProgram1.Text = "Program1";
            btnProgram1.UseVisualStyleBackColor = true;
            btnProgram1.Click += btnProgram1_Click;
            // 
            // btnProgram2
            // 
            btnProgram2.Location = new Point(122, 66);
            btnProgram2.Name = "btnProgram2";
            btnProgram2.Size = new Size(94, 52);
            btnProgram2.TabIndex = 1;
            btnProgram2.Text = "Program2";
            btnProgram2.UseVisualStyleBackColor = true;
            btnProgram2.Click += btnProgram2_Click;
            // 
            // btnProgram3
            // 
            btnProgram3.Location = new Point(222, 66);
            btnProgram3.Name = "btnProgram3";
            btnProgram3.Size = new Size(94, 52);
            btnProgram3.TabIndex = 2;
            btnProgram3.Text = "Program3";
            btnProgram3.UseVisualStyleBackColor = true;
            btnProgram3.Click += btnProgram3_Click;
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(12, 67);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(232, 49);
            btnConnect.TabIndex = 3;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // txtBoxPLCIP
            // 
            txtBoxPLCIP.Location = new Point(12, 34);
            txtBoxPLCIP.Name = "txtBoxPLCIP";
            txtBoxPLCIP.Size = new Size(232, 27);
            txtBoxPLCIP.TabIndex = 4;
            txtBoxPLCIP.TextChanged += txtBoxPLCIP_TextChanged;
            // 
            // lblTypePLCIP
            // 
            lblTypePLCIP.AutoSize = true;
            lblTypePLCIP.Location = new Point(12, 11);
            lblTypePLCIP.Name = "lblTypePLCIP";
            lblTypePLCIP.Size = new Size(178, 20);
            lblTypePLCIP.TabIndex = 5;
            lblTypePLCIP.Text = "Type your PLC IP address: ";
            // 
            // lblChooseSIM
            // 
            lblChooseSIM.AutoSize = true;
            lblChooseSIM.Location = new Point(89, 43);
            lblChooseSIM.Name = "lblChooseSIM";
            lblChooseSIM.Size = new Size(167, 20);
            lblChooseSIM.TabIndex = 7;
            lblChooseSIM.Text = "Choose your simulation:";
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Location = new Point(0, 371);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(697, 22);
            statusStrip1.TabIndex = 8;
            statusStrip1.Text = "statusStrip1";
            // 
            // btnEnd
            // 
            btnEnd.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnEnd.Location = new Point(13, 162);
            btnEnd.Name = "btnEnd";
            btnEnd.Size = new Size(142, 71);
            btnEnd.TabIndex = 9;
            btnEnd.Text = "End/close";
            btnEnd.UseVisualStyleBackColor = true;
            btnEnd.Click += btnEnd_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(btnConnect);
            panel2.Controls.Add(txtBoxPLCIP);
            panel2.Controls.Add(lblTypePLCIP);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(697, 135);
            panel2.TabIndex = 11;
            // 
            // panel3
            // 
            panel3.Controls.Add(btnProgram1);
            panel3.Controls.Add(btnProgram2);
            panel3.Controls.Add(btnProgram3);
            panel3.Controls.Add(lblChooseSIM);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(0, 135);
            panel3.Name = "panel3";
            panel3.Size = new Size(697, 236);
            panel3.TabIndex = 12;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnEnd);
            panel1.Dock = DockStyle.Right;
            panel1.Location = new Point(536, 135);
            panel1.Name = "panel1";
            panel1.Size = new Size(161, 236);
            panel1.TabIndex = 13;
            //
            //Timer_read_from_PLC
            //
            Timer_read_from_PLC.Tick += Timer_read_from_PLC_Tick;
            // 
            // ChooseOptionForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(697, 393);
            Controls.Add(panel1);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(statusStrip1);
            Name = "ChooseOptionForm";
            Text = "Connect your PLC and choose your simulation";
            Load += ChooseOption_Load;
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnProgram1;
        private Button btnProgram2;
        private Button btnProgram3;
        private Button btnConnect;
        private TextBox txtBoxPLCIP;
        private Label lblTypePLCIP;
        private Label lblChooseSIM;
        private StatusStrip statusStrip1;
        private Button btnEnd;
        private Panel panel2;
        private Panel panel3;
        private Panel panel1;
        private System.Windows.Forms.Timer Timer_read_from_PLC;
    }
}