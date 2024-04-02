using Bc_prace.Controls;

namespace Bc_prace
{
    partial class Program1Form
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
            statusStripElevator = new StatusStrip();
            btnCabinFloor5 = new Button();
            btnCabinFloor4 = new Button();
            btnCabinFloor3 = new Button();
            btnCabinFloor2 = new Button();
            btnCabinFloor1 = new Button();
            btnCabinEmergency = new Button();
            btnCabinDoorOPENCLOSE = new Button();
            btnEnd = new Button();
            panelCabinBtn = new Panel();
            panel1 = new Panel();
            btnTest2 = new Button();
            btnTest = new Button();
            btnGlobalEmergency = new Button();
            btnCabinLengthBigger = new Button();
            btnCabinLengthSmaller = new Button();
            btnCabinWidthBigger = new Button();
            btnCabinWidthSmaller = new Button();
            btnCabinMoveToDown = new Button();
            btnCabinMoveUp = new Button();
            btnCabinMoveLeft = new Button();
            btnCabinMoveRight = new Button();
            Timer_read_actual = new System.Windows.Forms.Timer(components);
            panelElevatorCabin = new Panel();
            userControlElevatorCabin1 = new UserControlElevatorCabin();
            panelElevatorDoor = new Panel();
            userControlElevatorDoor1 = new UserControlElevatorDoor();
            panelCabinBtn.SuspendLayout();
            panel1.SuspendLayout();
            panelElevatorCabin.SuspendLayout();
            panelElevatorDoor.SuspendLayout();
            SuspendLayout();
            // 
            // statusStripElevator
            // 
            statusStripElevator.ImageScalingSize = new Size(20, 20);
            statusStripElevator.Location = new Point(0, 590);
            statusStripElevator.Name = "statusStripElevator";
            statusStripElevator.Size = new Size(1200, 22);
            statusStripElevator.TabIndex = 0;
            statusStripElevator.Text = "statusStrip1";
            // 
            // btnCabinFloor5
            // 
            btnCabinFloor5.Location = new Point(32, 20);
            btnCabinFloor5.Name = "btnCabinFloor5";
            btnCabinFloor5.Size = new Size(57, 35);
            btnCabinFloor5.TabIndex = 1;
            btnCabinFloor5.Text = "5";
            btnCabinFloor5.UseVisualStyleBackColor = true;
            btnCabinFloor5.Click += btnCabinFloor5_Click;
            // 
            // btnCabinFloor4
            // 
            btnCabinFloor4.Location = new Point(32, 60);
            btnCabinFloor4.Name = "btnCabinFloor4";
            btnCabinFloor4.Size = new Size(57, 35);
            btnCabinFloor4.TabIndex = 2;
            btnCabinFloor4.Text = "4";
            btnCabinFloor4.UseVisualStyleBackColor = true;
            btnCabinFloor4.Click += btnCabinFloor4_Click;
            // 
            // btnCabinFloor3
            // 
            btnCabinFloor3.Location = new Point(32, 100);
            btnCabinFloor3.Name = "btnCabinFloor3";
            btnCabinFloor3.Size = new Size(57, 35);
            btnCabinFloor3.TabIndex = 3;
            btnCabinFloor3.Text = "3";
            btnCabinFloor3.UseVisualStyleBackColor = true;
            btnCabinFloor3.Click += btnCabinFloor3_Click;
            // 
            // btnCabinFloor2
            // 
            btnCabinFloor2.Location = new Point(32, 140);
            btnCabinFloor2.Name = "btnCabinFloor2";
            btnCabinFloor2.Size = new Size(57, 35);
            btnCabinFloor2.TabIndex = 4;
            btnCabinFloor2.Text = "2";
            btnCabinFloor2.UseVisualStyleBackColor = true;
            btnCabinFloor2.Click += btnCabinFloor2_Click;
            // 
            // btnCabinFloor1
            // 
            btnCabinFloor1.Location = new Point(32, 180);
            btnCabinFloor1.Name = "btnCabinFloor1";
            btnCabinFloor1.Size = new Size(57, 35);
            btnCabinFloor1.TabIndex = 5;
            btnCabinFloor1.Text = "1";
            btnCabinFloor1.UseVisualStyleBackColor = true;
            btnCabinFloor1.Click += btnCabinFloor1_Click;
            // 
            // btnCabinEmergency
            // 
            btnCabinEmergency.Location = new Point(5, 283);
            btnCabinEmergency.Name = "btnCabinEmergency";
            btnCabinEmergency.Size = new Size(112, 52);
            btnCabinEmergency.TabIndex = 7;
            btnCabinEmergency.Text = "Emergency BTN";
            btnCabinEmergency.UseVisualStyleBackColor = true;
            btnCabinEmergency.Click += btnCabinEmergency_Click;
            // 
            // btnCabinDoorOPENCLOSE
            // 
            btnCabinDoorOPENCLOSE.Location = new Point(5, 221);
            btnCabinDoorOPENCLOSE.Name = "btnCabinDoorOPENCLOSE";
            btnCabinDoorOPENCLOSE.Size = new Size(112, 52);
            btnCabinDoorOPENCLOSE.TabIndex = 8;
            btnCabinDoorOPENCLOSE.Text = "Open/Close door ";
            btnCabinDoorOPENCLOSE.UseVisualStyleBackColor = true;
            btnCabinDoorOPENCLOSE.Click += btnCabinDoorOPENCLOSE_Click;
            // 
            // btnEnd
            // 
            btnEnd.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnEnd.Location = new Point(3, 507);
            btnEnd.Name = "btnEnd";
            btnEnd.Size = new Size(177, 79);
            btnEnd.TabIndex = 38;
            btnEnd.Text = "End/Close";
            btnEnd.UseVisualStyleBackColor = true;
            btnEnd.Click += btnEnd_Click;
            // 
            // panelCabinBtn
            // 
            panelCabinBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            panelCabinBtn.BackColor = Color.White;
            panelCabinBtn.BorderStyle = BorderStyle.FixedSingle;
            panelCabinBtn.Controls.Add(btnCabinEmergency);
            panelCabinBtn.Controls.Add(btnCabinDoorOPENCLOSE);
            panelCabinBtn.Controls.Add(btnCabinFloor1);
            panelCabinBtn.Controls.Add(btnCabinFloor2);
            panelCabinBtn.Controls.Add(btnCabinFloor3);
            panelCabinBtn.Controls.Add(btnCabinFloor4);
            panelCabinBtn.Controls.Add(btnCabinFloor5);
            panelCabinBtn.Location = new Point(888, 236);
            panelCabinBtn.Name = "panelCabinBtn";
            panelCabinBtn.Size = new Size(121, 346);
            panelCabinBtn.TabIndex = 39;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnTest2);
            panel1.Controls.Add(btnTest);
            panel1.Controls.Add(btnGlobalEmergency);
            panel1.Controls.Add(btnCabinLengthBigger);
            panel1.Controls.Add(btnCabinLengthSmaller);
            panel1.Controls.Add(btnCabinWidthBigger);
            panel1.Controls.Add(btnCabinWidthSmaller);
            panel1.Controls.Add(btnCabinMoveToDown);
            panel1.Controls.Add(btnCabinMoveUp);
            panel1.Controls.Add(btnCabinMoveLeft);
            panel1.Controls.Add(btnCabinMoveRight);
            panel1.Controls.Add(btnEnd);
            panel1.Dock = DockStyle.Right;
            panel1.Location = new Point(1015, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(185, 590);
            panel1.TabIndex = 42;
            // 
            // btnTest2
            // 
            btnTest2.Location = new Point(99, 472);
            btnTest2.Name = "btnTest2";
            btnTest2.Size = new Size(83, 29);
            btnTest2.TabIndex = 49;
            btnTest2.Text = "test2";
            btnTest2.UseVisualStyleBackColor = true;
            btnTest2.Click += btnTest2_Click;
            // 
            // btnTest
            // 
            btnTest.Location = new Point(3, 472);
            btnTest.Name = "btnTest";
            btnTest.Size = new Size(81, 29);
            btnTest.TabIndex = 48;
            btnTest.Text = "test";
            btnTest.UseVisualStyleBackColor = true;
            btnTest.Click += btnTest_Click;
            // 
            // btnGlobalEmergency
            // 
            btnGlobalEmergency.Location = new Point(3, 12);
            btnGlobalEmergency.Name = "btnGlobalEmergency";
            btnGlobalEmergency.Size = new Size(177, 79);
            btnGlobalEmergency.TabIndex = 47;
            btnGlobalEmergency.Text = "Emergency BTN";
            btnGlobalEmergency.UseVisualStyleBackColor = true;
            btnGlobalEmergency.Click += btnGlobalEmergency_Click;
            // 
            // btnCabinLengthBigger
            // 
            btnCabinLengthBigger.Location = new Point(99, 355);
            btnCabinLengthBigger.Name = "btnCabinLengthBigger";
            btnCabinLengthBigger.Size = new Size(81, 29);
            btnCabinLengthBigger.TabIndex = 46;
            btnCabinLengthBigger.Text = "Length bigger";
            btnCabinLengthBigger.UseVisualStyleBackColor = true;
            btnCabinLengthBigger.Click += btnCabinLengthBigger_Click;
            // 
            // btnCabinLengthSmaller
            // 
            btnCabinLengthSmaller.Location = new Point(99, 320);
            btnCabinLengthSmaller.Name = "btnCabinLengthSmaller";
            btnCabinLengthSmaller.Size = new Size(81, 29);
            btnCabinLengthSmaller.TabIndex = 45;
            btnCabinLengthSmaller.Text = "Length smaller";
            btnCabinLengthSmaller.UseVisualStyleBackColor = true;
            btnCabinLengthSmaller.Click += btnCabinLengthSmaller_Click;
            // 
            // btnCabinWidthBigger
            // 
            btnCabinWidthBigger.Location = new Point(99, 285);
            btnCabinWidthBigger.Name = "btnCabinWidthBigger";
            btnCabinWidthBigger.Size = new Size(81, 29);
            btnCabinWidthBigger.TabIndex = 44;
            btnCabinWidthBigger.Text = "Width bigger";
            btnCabinWidthBigger.UseVisualStyleBackColor = true;
            btnCabinWidthBigger.Click += btnCabinWidthBigger_Click;
            // 
            // btnCabinWidthSmaller
            // 
            btnCabinWidthSmaller.Location = new Point(99, 251);
            btnCabinWidthSmaller.Name = "btnCabinWidthSmaller";
            btnCabinWidthSmaller.Size = new Size(81, 29);
            btnCabinWidthSmaller.TabIndex = 43;
            btnCabinWidthSmaller.Text = "Width smaller";
            btnCabinWidthSmaller.UseVisualStyleBackColor = true;
            btnCabinWidthSmaller.Click += btnCabinWidthSmaller_Click;
            // 
            // btnCabinMoveToDown
            // 
            btnCabinMoveToDown.Location = new Point(3, 355);
            btnCabinMoveToDown.Name = "btnCabinMoveToDown";
            btnCabinMoveToDown.Size = new Size(81, 29);
            btnCabinMoveToDown.TabIndex = 42;
            btnCabinMoveToDown.Text = "Down";
            btnCabinMoveToDown.UseVisualStyleBackColor = true;
            btnCabinMoveToDown.Click += btnCabinMoveDown_Click;
            // 
            // btnCabinMoveUp
            // 
            btnCabinMoveUp.Location = new Point(3, 320);
            btnCabinMoveUp.Name = "btnCabinMoveUp";
            btnCabinMoveUp.Size = new Size(81, 29);
            btnCabinMoveUp.TabIndex = 41;
            btnCabinMoveUp.Text = "Up";
            btnCabinMoveUp.UseVisualStyleBackColor = true;
            btnCabinMoveUp.Click += btnCabinMoveUp_Click;
            // 
            // btnCabinMoveLeft
            // 
            btnCabinMoveLeft.Location = new Point(3, 285);
            btnCabinMoveLeft.Name = "btnCabinMoveLeft";
            btnCabinMoveLeft.Size = new Size(81, 29);
            btnCabinMoveLeft.TabIndex = 40;
            btnCabinMoveLeft.Text = "Left";
            btnCabinMoveLeft.UseVisualStyleBackColor = true;
            btnCabinMoveLeft.Click += btnCabinMoveLeft_Click;
            // 
            // btnCabinMoveRight
            // 
            btnCabinMoveRight.Location = new Point(3, 251);
            btnCabinMoveRight.Name = "btnCabinMoveRight";
            btnCabinMoveRight.Size = new Size(81, 29);
            btnCabinMoveRight.TabIndex = 39;
            btnCabinMoveRight.Text = "Right";
            btnCabinMoveRight.UseVisualStyleBackColor = true;
            btnCabinMoveRight.Click += btnCabinMoveRight_Click;
            // 
            // Timer_read_actual
            // 
            Timer_read_actual.Tick += Timer_read_actual_Tick;
            // 
            // panelElevatorCabin
            // 
            panelElevatorCabin.Controls.Add(userControlElevatorCabin1);
            panelElevatorCabin.Dock = DockStyle.Left;
            panelElevatorCabin.Location = new Point(0, 0);
            panelElevatorCabin.Margin = new Padding(3, 4, 3, 4);
            panelElevatorCabin.Name = "panelElevatorCabin";
            panelElevatorCabin.Size = new Size(449, 590);
            panelElevatorCabin.TabIndex = 43;
            // 
            // userControlElevatorCabin1
            // 
            userControlElevatorCabin1.Dock = DockStyle.Fill;
            userControlElevatorCabin1.ElevatorActualFloorLED1 = false;
            userControlElevatorCabin1.ElevatorActualFloorLED2 = false;
            userControlElevatorCabin1.ElevatorActualFloorLED3 = false;
            userControlElevatorCabin1.ElevatorActualFloorLED4 = false;
            userControlElevatorCabin1.ElevatorActualFloorLED5 = false;
            userControlElevatorCabin1.Location = new Point(0, 0);
            userControlElevatorCabin1.Name = "userControlElevatorCabin1";
            userControlElevatorCabin1.Size = new Size(449, 590);
            userControlElevatorCabin1.TabIndex = 45;
            // 
            // panelElevatorDoor
            // 
            panelElevatorDoor.Controls.Add(userControlElevatorDoor1);
            panelElevatorDoor.Dock = DockStyle.Left;
            panelElevatorDoor.Location = new Point(449, 0);
            panelElevatorDoor.Margin = new Padding(3, 4, 3, 4);
            panelElevatorDoor.Name = "panelElevatorDoor";
            panelElevatorDoor.Size = new Size(341, 590);
            panelElevatorDoor.TabIndex = 44;
            // 
            // userControlElevatorDoor1
            // 
            userControlElevatorDoor1.Dock = DockStyle.Fill;
            userControlElevatorDoor1.Location = new Point(0, 0);
            userControlElevatorDoor1.Name = "userControlElevatorDoor1";
            userControlElevatorDoor1.Size = new Size(341, 590);
            userControlElevatorDoor1.TabIndex = 0;
            // 
            // Program1Form
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 612);
            Controls.Add(panelElevatorDoor);
            Controls.Add(panelElevatorCabin);
            Controls.Add(panel1);
            Controls.Add(statusStripElevator);
            Controls.Add(panelCabinBtn);
            DoubleBuffered = true;
            Name = "Program1Form";
            Text = "Program1";
            Load += Program1_Load;
            panelCabinBtn.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panelElevatorCabin.ResumeLayout(false);
            panelElevatorDoor.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip statusStripElevator;
        private Button btnCabinFloor5;
        private Button btnCabinFloor4;
        private Button btnCabinFloor3;
        private Button btnCabinFloor2;
        private Button btnCabinFloor1;
        private Button btnCabinEmergency;
        private Button btnCabinDoorOPENCLOSE;
        private Button btnEnd;
        private Panel panelCabinBtn;
        private Panel panel1;
        private Button btnCabinWidthSmaller;
        private Button btnCabinMoveToDown;
        private Button btnCabinMoveUp;
        private Button btnCabinMoveLeft;
        private Button btnCabinMoveRight;
        private Button btnCabinLengthBigger;
        private Button btnCabinLengthSmaller;
        private Button btnCabinWidthBigger;
        private System.Windows.Forms.Timer Timer_read_actual;
        private Panel panelElevatorCabin;
        private UserControlElevatorCabin userControlElevatorCabin1;
        private Panel panelElevatorDoor;
        private UserControlElevatorDoor userControlElevatorDoor1;
        private Button btnGlobalEmergency;
        private Button btnTest;
        private Button btnTest2;
    }
}