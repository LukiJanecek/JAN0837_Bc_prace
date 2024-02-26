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
            components = new System.ComponentModel.Container();
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
            label1.Location = new Point(10, 7);
            label1.Name = "label1";
            label1.Size = new Size(181, 21);
            label1.TabIndex = 0;
            label1.Text = "Select washing program:";
            // 
            // btnPerfectPolish
            // 
            btnPerfectPolish.Location = new Point(10, 4);
            btnPerfectPolish.Margin = new Padding(3, 2, 3, 2);
            btnPerfectPolish.Name = "btnPerfectPolish";
            btnPerfectPolish.Size = new Size(82, 46);
            btnPerfectPolish.TabIndex = 1;
            btnPerfectPolish.Text = "Perfect polish";
            btnPerfectPolish.UseVisualStyleBackColor = true;
            btnPerfectPolish.Click += btnPerfectPolish_Click;
            // 
            // btnPerfectWash
            // 
            btnPerfectWash.Location = new Point(10, 55);
            btnPerfectWash.Margin = new Padding(3, 2, 3, 2);
            btnPerfectWash.Name = "btnPerfectWash";
            btnPerfectWash.Size = new Size(82, 46);
            btnPerfectWash.TabIndex = 2;
            btnPerfectWash.Text = "Perfect wash";
            btnPerfectWash.UseVisualStyleBackColor = true;
            btnPerfectWash.Click += btnPerfectWash_Click;
            // 
            // button3
            // 
            button3.Location = new Point(299, 4);
            button3.Margin = new Padding(3, 2, 3, 2);
            button3.Name = "button3";
            button3.Size = new Size(82, 46);
            button3.TabIndex = 3;
            button3.Text = "button3";
            button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Location = new Point(299, 55);
            button4.Margin = new Padding(3, 2, 3, 2);
            button4.Name = "button4";
            button4.Size = new Size(82, 46);
            button4.TabIndex = 4;
            button4.Text = "button4";
            button4.UseVisualStyleBackColor = true;
            // 
            // btnEnd
            // 
            btnEnd.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnEnd.Location = new Point(610, 10);
            btnEnd.Margin = new Padding(3, 2, 3, 2);
            btnEnd.Name = "btnEnd";
            btnEnd.Size = new Size(82, 46);
            btnEnd.TabIndex = 5;
            btnEnd.Text = "End/close";
            btnEnd.UseVisualStyleBackColor = true;
            btnEnd.Click += btnEnd_Click;
            // 
            // statusStripCarWashSelection
            // 
            statusStripCarWashSelection.ImageScalingSize = new Size(20, 20);
            statusStripCarWashSelection.Location = new Point(0, 229);
            statusStripCarWashSelection.Name = "statusStripCarWashSelection";
            statusStripCarWashSelection.Size = new Size(695, 22);
            statusStripCarWashSelection.TabIndex = 6;
            statusStripCarWashSelection.Text = "statusStrip1";
            // 
            // lblPerfectPolishDescription
            // 
            lblPerfectPolishDescription.AutoSize = true;
            lblPerfectPolishDescription.Location = new Point(98, 4);
            lblPerfectPolishDescription.Name = "lblPerfectPolishDescription";
            lblPerfectPolishDescription.Size = new Size(180, 30);
            lblPerfectPolishDescription.TabIndex = 7;
            lblPerfectPolishDescription.Text = "- foamy wax\r\n- washing with special chemicals\r\n";
            // 
            // lblPerfectWashDescription
            // 
            lblPerfectWashDescription.AutoSize = true;
            lblPerfectWashDescription.Location = new Point(98, 55);
            lblPerfectWashDescription.Name = "lblPerfectWashDescription";
            lblPerfectWashDescription.Size = new Size(180, 30);
            lblPerfectWashDescription.TabIndex = 8;
            lblPerfectWashDescription.Text = "- varnish protection\r\n- washing with special chemicals\r\n";
            // 
            // panel1
            // 
            panel1.Controls.Add(btnEnd);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 171);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(695, 58);
            panel1.TabIndex = 9;
            // 
            // panel2
            // 
            panel2.Controls.Add(label1);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(695, 34);
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
            panel3.Location = new Point(0, 34);
            panel3.Margin = new Padding(3, 2, 3, 2);
            panel3.Name = "panel3";
            panel3.Size = new Size(695, 137);
            panel3.TabIndex = 11;
            // 
            // Timer_read_actual
            // 
            Timer_read_actual.Tick += Timer_read_actual_Tick;
            // 
            // Program2SelectionForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(695, 251);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(statusStripCarWashSelection);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Program2SelectionForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Program2 -> Select washing program";
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
        private System.Windows.Forms.Timer Timer_read_actual;
    }
}