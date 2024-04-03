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
            btnElevator = new Button();
            btnCarWash = new Button();
            btnCrossroad = new Button();
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
            // btnElevator
            // 
            btnElevator.Location = new Point(22, 67);
            btnElevator.Name = "btnElevator";
            btnElevator.Size = new Size(94, 52);
            btnElevator.TabIndex = 0;
            btnElevator.Text = "Elevator";
            btnElevator.UseVisualStyleBackColor = true;
            btnElevator.Click += btnProgram1_Click;
            // 
            // btnCarWash
            // 
            btnCarWash.Location = new Point(122, 67);
            btnCarWash.Name = "btnCarWash";
            btnCarWash.Size = new Size(94, 52);
            btnCarWash.TabIndex = 1;
            btnCarWash.Text = "CarWash";
            btnCarWash.UseVisualStyleBackColor = true;
            btnCarWash.Click += btnProgram2_Click;
            // 
            // btnCrossroad
            // 
            btnCrossroad.Location = new Point(222, 67);
            btnCrossroad.Name = "btnCrossroad";
            btnCrossroad.Size = new Size(94, 52);
            btnCrossroad.TabIndex = 2;
            btnCrossroad.Text = "Crossroad";
            btnCrossroad.UseVisualStyleBackColor = true;
            btnCrossroad.Click += btnProgram3_Click;
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(11, 71);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(233, 49);
            btnConnect.TabIndex = 3;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // txtBoxPLCIP
            // 
            txtBoxPLCIP.Location = new Point(11, 35);
            txtBoxPLCIP.Name = "txtBoxPLCIP";
            txtBoxPLCIP.Size = new Size(233, 27);
            txtBoxPLCIP.TabIndex = 4;
            txtBoxPLCIP.Text = "192.168.0.1";
            // 
            // lblTypePLCIP
            // 
            lblTypePLCIP.AutoSize = true;
            lblTypePLCIP.Location = new Point(11, 11);
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
            // statusStripChooseOption
            // 
            statusStripChooseOption.ImageScalingSize = new Size(20, 20);
            statusStripChooseOption.Location = new Point(0, 326);
            statusStripChooseOption.Name = "statusStripChooseOption";
            statusStripChooseOption.Size = new Size(496, 22);
            statusStripChooseOption.TabIndex = 8;
            statusStripChooseOption.Text = "statusStrip1";
            // 
            // btnEnd
            // 
            btnEnd.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnEnd.Location = new Point(13, 118);
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
            panel2.Size = new Size(496, 135);
            panel2.TabIndex = 11;
            // 
            // panel3
            // 
            panel3.Controls.Add(btnElevator);
            panel3.Controls.Add(btnCarWash);
            panel3.Controls.Add(btnCrossroad);
            panel3.Controls.Add(lblChooseSIM);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(0, 135);
            panel3.Name = "panel3";
            panel3.Size = new Size(496, 191);
            panel3.TabIndex = 12;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnEnd);
            panel1.Dock = DockStyle.Right;
            panel1.Location = new Point(335, 135);
            panel1.Name = "panel1";
            panel1.Size = new Size(161, 191);
            panel1.TabIndex = 13;
            // 
            // Timer_read_from_PLC
            // 
            Timer_read_from_PLC.Tick += Timer_read_from_PLC_Tick;
            // 
            // ChooseOptionForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(496, 348);
            Controls.Add(panel1);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(statusStripChooseOption);
            DoubleBuffered = true;
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

        private Button btnElevator;
        private Button btnCarWash;
        private Button btnCrossroad;
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