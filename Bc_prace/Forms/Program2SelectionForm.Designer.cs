namespace Bc_prace
{
    partial class Program2SelectionForm
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
            label1 = new Label();
            btnPerfectPolish = new Button();
            btnPerfectWash = new Button();
            button3 = new Button();
            button4 = new Button();
            btnEnd = new Button();
            statusStripCarWashSelection = new StatusStrip();
            lblPerfectPolishDescription = new Label();
            lblPerfectWashDescription = new Label();
            panel1 = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(226, 28);
            label1.TabIndex = 0;
            label1.Text = "Select washing program:";
            // 
            // btnPerfectPolish
            // 
            btnPerfectPolish.Location = new Point(12, 6);
            btnPerfectPolish.Name = "btnPerfectPolish";
            btnPerfectPolish.Size = new Size(94, 61);
            btnPerfectPolish.TabIndex = 1;
            btnPerfectPolish.Text = "Perfect polish";
            btnPerfectPolish.UseVisualStyleBackColor = true;
            // 
            // btnPerfectWash
            // 
            btnPerfectWash.Location = new Point(12, 73);
            btnPerfectWash.Name = "btnPerfectWash";
            btnPerfectWash.Size = new Size(94, 61);
            btnPerfectWash.TabIndex = 2;
            btnPerfectWash.Text = "Perfect wash";
            btnPerfectWash.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(342, 6);
            button3.Name = "button3";
            button3.Size = new Size(94, 61);
            button3.TabIndex = 3;
            button3.Text = "button3";
            button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Location = new Point(342, 73);
            button4.Name = "button4";
            button4.Size = new Size(94, 61);
            button4.TabIndex = 4;
            button4.Text = "button4";
            button4.UseVisualStyleBackColor = true;
            // 
            // btnEnd
            // 
            btnEnd.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnEnd.Location = new Point(697, 13);
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
            statusStripCarWashSelection.Location = new Point(0, 311);
            statusStripCarWashSelection.Name = "statusStripCarWashSelection";
            statusStripCarWashSelection.Padding = new Padding(1, 0, 16, 0);
            statusStripCarWashSelection.Size = new Size(794, 24);
            statusStripCarWashSelection.TabIndex = 6;
            statusStripCarWashSelection.Text = "statusStrip1";
            // 
            // lblPerfectPolishDescription
            // 
            lblPerfectPolishDescription.AutoSize = true;
            lblPerfectPolishDescription.Location = new Point(112, 6);
            lblPerfectPolishDescription.Name = "lblPerfectPolishDescription";
            lblPerfectPolishDescription.Size = new Size(224, 40);
            lblPerfectPolishDescription.TabIndex = 7;
            lblPerfectPolishDescription.Text = "- foamy wax\r\n- washing with special chemicals\r\n";
            // 
            // lblPerfectWashDescription
            // 
            lblPerfectWashDescription.AutoSize = true;
            lblPerfectWashDescription.Location = new Point(112, 73);
            lblPerfectWashDescription.Name = "lblPerfectWashDescription";
            lblPerfectWashDescription.Size = new Size(224, 40);
            lblPerfectWashDescription.TabIndex = 8;
            lblPerfectWashDescription.Text = "- varnish protection\r\n- washing with special chemicals\r\n";
            // 
            // panel1
            // 
            panel1.Controls.Add(btnEnd);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 234);
            panel1.Name = "panel1";
            panel1.Size = new Size(794, 77);
            panel1.TabIndex = 9;
            // 
            // panel2
            // 
            panel2.Controls.Add(label1);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(794, 45);
            panel2.TabIndex = 10;
            // 
            // panel3
            // 
            panel3.Controls.Add(btnPerfectWash);
            panel3.Controls.Add(btnPerfectPolish);
            panel3.Controls.Add(button3);
            panel3.Controls.Add(lblPerfectWashDescription);
            panel3.Controls.Add(button4);
            panel3.Controls.Add(lblPerfectPolishDescription);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(0, 45);
            panel3.Name = "panel3";
            panel3.Size = new Size(794, 189);
            panel3.TabIndex = 11;
            //
            //Timer_read_from_PLC
            //
            Timer_read_from_PLC.Tick += Timer_read_from_PLC_Tick;
            // 
            // Program2SelectionForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(794, 335);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(statusStripCarWashSelection);
            Name = "Program2SelectionForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Program2 -> Selection";
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button btnPerfectPolish;
        private Button btnPerfectWash;
        private Button button3;
        private Button button4;
        private Button btnEnd;
        private StatusStrip statusStripCarWashSelection;
        private Label lblPerfectPolishDescription;
        private Label lblPerfectWashDescription;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private System.Windows.Forms.Timer Timer_read_from_PLC;
    }
}