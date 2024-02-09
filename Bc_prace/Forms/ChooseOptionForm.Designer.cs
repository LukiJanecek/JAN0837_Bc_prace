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
            components = new System.ComponentModel.Container();
            btnProgram1 = new Button();
            btnProgram2 = new Button();
            btnProgram3 = new Button();
            btnConnect = new Button();
            txtBoxPLCIP = new TextBox();
            lblTypePLCIP = new Label();
            lblChooseSIM = new Label();
            statusStripChooseOption = new StatusStrip();
            btnEnd = new Button();
            panel2 = new Panel();
            panel3 = new Panel();
            panel1 = new Panel();
            Timer_read_from_PLC = new System.Windows.Forms.Timer(components);
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // btnProgram1
            // 
            btnProgram1.Location = new Point(19, 50);
            btnProgram1.Margin = new Padding(3, 2, 3, 2);
            btnProgram1.Name = "btnProgram1";
            btnProgram1.Size = new Size(82, 39);
            btnProgram1.TabIndex = 0;
            btnProgram1.Text = "Program1";
            btnProgram1.UseVisualStyleBackColor = true;
            btnProgram1.Click += btnProgram1_Click;
            // 
            // btnProgram2
            // 
            btnProgram2.Location = new Point(107, 50);
            btnProgram2.Margin = new Padding(3, 2, 3, 2);
            btnProgram2.Name = "btnProgram2";
            btnProgram2.Size = new Size(82, 39);
            btnProgram2.TabIndex = 1;
            btnProgram2.Text = "Program2";
            btnProgram2.UseVisualStyleBackColor = true;
            btnProgram2.Click += btnProgram2_Click;
            // 
            // btnProgram3
            // 
            btnProgram3.Location = new Point(194, 50);
            btnProgram3.Margin = new Padding(3, 2, 3, 2);
            btnProgram3.Name = "btnProgram3";
            btnProgram3.Size = new Size(82, 39);
            btnProgram3.TabIndex = 2;
            btnProgram3.Text = "Program3";
            btnProgram3.UseVisualStyleBackColor = true;
            btnProgram3.Click += btnProgram3_Click;
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(10, 50);
            btnConnect.Margin = new Padding(3, 2, 3, 2);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(203, 37);
            btnConnect.TabIndex = 3;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // txtBoxPLCIP
            // 
            txtBoxPLCIP.Location = new Point(10, 26);
            txtBoxPLCIP.Margin = new Padding(3, 2, 3, 2);
            txtBoxPLCIP.Name = "txtBoxPLCIP";
            txtBoxPLCIP.Size = new Size(204, 23);
            txtBoxPLCIP.TabIndex = 4;
            txtBoxPLCIP.Text = "192.168.0.1";
            txtBoxPLCIP.TextChanged += txtBoxPLCIP_TextChanged;
            // 
            // lblTypePLCIP
            // 
            lblTypePLCIP.AutoSize = true;
            lblTypePLCIP.Location = new Point(10, 8);
            lblTypePLCIP.Name = "lblTypePLCIP";
            lblTypePLCIP.Size = new Size(144, 15);
            lblTypePLCIP.TabIndex = 5;
            lblTypePLCIP.Text = "Type your PLC IP address: ";
            // 
            // lblChooseSIM
            // 
            lblChooseSIM.AutoSize = true;
            lblChooseSIM.Location = new Point(78, 32);
            lblChooseSIM.Name = "lblChooseSIM";
            lblChooseSIM.Size = new Size(136, 15);
            lblChooseSIM.TabIndex = 7;
            lblChooseSIM.Text = "Choose your simulation:";
            // 
            // statusStripChooseOption
            // 
            statusStripChooseOption.ImageScalingSize = new Size(20, 20);
            statusStripChooseOption.Location = new Point(0, 273);
            statusStripChooseOption.Name = "statusStripChooseOption";
            statusStripChooseOption.Padding = new Padding(1, 0, 12, 0);
            statusStripChooseOption.Size = new Size(610, 22);
            statusStripChooseOption.TabIndex = 8;
            statusStripChooseOption.Text = "statusStrip1";
            // 
            // btnEnd
            // 
            btnEnd.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnEnd.Location = new Point(11, 117);
            btnEnd.Margin = new Padding(3, 2, 3, 2);
            btnEnd.Name = "btnEnd";
            btnEnd.Size = new Size(124, 53);
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
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(610, 101);
            panel2.TabIndex = 11;
            // 
            // panel3
            // 
            panel3.Controls.Add(btnProgram1);
            panel3.Controls.Add(btnProgram2);
            panel3.Controls.Add(btnProgram3);
            panel3.Controls.Add(lblChooseSIM);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(0, 101);
            panel3.Margin = new Padding(3, 2, 3, 2);
            panel3.Name = "panel3";
            panel3.Size = new Size(610, 172);
            panel3.TabIndex = 12;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnEnd);
            panel1.Dock = DockStyle.Right;
            panel1.Location = new Point(469, 101);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(141, 172);
            panel1.TabIndex = 13;
            // 
            // Timer_read_from_PLC
            // 
            Timer_read_from_PLC.Tick += Timer_read_from_PLC_Tick;
            // 
            // ChooseOptionForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(610, 295);
            Controls.Add(panel1);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(statusStripChooseOption);
            Margin = new Padding(3, 2, 3, 2);
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
        private StatusStrip statusStripChooseOption;
        private Button btnEnd;
        private Panel panel2;
        private Panel panel3;
        private Panel panel1;
        private System.Windows.Forms.Timer Timer_read_from_PLC;
    }
}