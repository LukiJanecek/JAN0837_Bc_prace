namespace Bc_prace
{
    partial class CarWashSelectionForm
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
            label1 = new Label();
            btnPerfectPolish = new Button();
            btnPerfectWash = new Button();
            btnEnd = new Button();
            statusStripCarWashSelection = new StatusStrip();
            panel1 = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            Timer_read_actual = new System.Windows.Forms.Timer(components);
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(11, 9);
            label1.Name = "label1";
            label1.Size = new Size(226, 28);
            label1.TabIndex = 0;
            label1.Text = "Select washing program:";
            // 
            // btnPerfectPolish
            // 
            btnPerfectPolish.Location = new Point(11, 5);
            btnPerfectPolish.Name = "btnPerfectPolish";
            btnPerfectPolish.Size = new Size(94, 61);
            btnPerfectPolish.TabIndex = 1;
            btnPerfectPolish.Text = "Perfect polish";
            btnPerfectPolish.UseVisualStyleBackColor = true;
            btnPerfectPolish.Click += btnPerfectPolish_Click;
            // 
            // btnPerfectWash
            // 
            btnPerfectWash.Location = new Point(111, 3);
            btnPerfectWash.Name = "btnPerfectWash";
            btnPerfectWash.Size = new Size(94, 61);
            btnPerfectWash.TabIndex = 2;
            btnPerfectWash.Text = "Perfect wash";
            btnPerfectWash.UseVisualStyleBackColor = true;
            btnPerfectWash.Click += btnPerfectWash_Click;
            // 
            // btnEnd
            // 
            btnEnd.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnEnd.Location = new Point(249, 13);
            btnEnd.Name = "btnEnd";
            btnEnd.Size = new Size(94, 61);
            btnEnd.TabIndex = 5;
            btnEnd.Text = "End/close";
            btnEnd.UseVisualStyleBackColor = true;
            btnEnd.Click += btnEnd_Click;
            // 
            // statusStripCarWashSelection
            // 
            statusStripCarWashSelection.ImageScalingSize = new Size(20, 20);
            statusStripCarWashSelection.Location = new Point(0, 241);
            statusStripCarWashSelection.Name = "statusStripCarWashSelection";
            statusStripCarWashSelection.Padding = new Padding(1, 0, 16, 0);
            statusStripCarWashSelection.Size = new Size(346, 22);
            statusStripCarWashSelection.TabIndex = 6;
            statusStripCarWashSelection.Text = "statusStrip1";
            // 
            // panel1
            // 
            panel1.Controls.Add(btnEnd);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 164);
            panel1.Name = "panel1";
            panel1.Size = new Size(346, 77);
            panel1.TabIndex = 9;
            // 
            // panel2
            // 
            panel2.Controls.Add(label1);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(346, 45);
            panel2.TabIndex = 10;
            // 
            // panel3
            // 
            panel3.Controls.Add(btnPerfectWash);
            panel3.Controls.Add(btnPerfectPolish);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(0, 45);
            panel3.Name = "panel3";
            panel3.Size = new Size(346, 119);
            panel3.TabIndex = 11;
            // 
            // Timer_read_actual
            // 
            Timer_read_actual.Tick += Timer_read_actual_Tick;
            // 
            // CarWashSelectionForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(346, 263);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(statusStripCarWashSelection);
            DoubleBuffered = true;
            Name = "CarWashSelectionForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Select washing program";
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button btnPerfectPolish;
        private Button btnPerfectWash;
        private Button btnEnd;
        private StatusStrip statusStripCarWashSelection;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private System.Windows.Forms.Timer Timer_read_actual;
    }
}