namespace Bc_prace.Forms
{
    partial class TestForm
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
            panel1 = new Panel();
            btnOpenReactWithASPNET = new Button();
            btnShowWebApp2 = new Button();
            btnShowWebApp1 = new Button();
            btnShowExceptionMessageBox = new Button();
            btnShowErrorMessageBox = new Button();
            btnEnd = new Button();
            btnEmergency = new Button();
            userControlSablona1 = new JAN0837_BP.Controls.UserControlSablona();
            btnSendToPLC = new Button();
            listBoxJSON = new ListBox();
            listBoxPath = new ListBox();
            textBoxInt1 = new TextBox();
            textBoxBool2 = new TextBox();
            textBoxInt2 = new TextBox();
            textBoxTime1 = new TextBox();
            textBoxBool1 = new TextBox();
            textBoxTime2 = new TextBox();
            lblInt1 = new Label();
            lblTime2 = new Label();
            lblBool2 = new Label();
            lblInt2 = new Label();
            lblTime1 = new Label();
            lblBool1 = new Label();
            btnReadFromPLC = new Button();
            btnReadJSON = new Button();
            btnSendToJSON = new Button();
            listBoxJSONVariables = new ListBox();
            Periodic_Function = new System.Windows.Forms.Timer(components);
            btnShowJson = new Button();
            comboBoxFileChoice = new ComboBox();
            statusStripTestForm = new StatusStrip();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(btnOpenReactWithASPNET);
            panel1.Controls.Add(btnShowWebApp2);
            panel1.Controls.Add(btnShowWebApp1);
            panel1.Controls.Add(btnShowExceptionMessageBox);
            panel1.Controls.Add(btnShowErrorMessageBox);
            panel1.Controls.Add(btnEnd);
            panel1.Controls.Add(btnEmergency);
            panel1.Dock = DockStyle.Right;
            panel1.Location = new Point(1007, 0);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(187, 799);
            panel1.TabIndex = 0;
            // 
            // btnOpenReactWithASPNET
            // 
            btnOpenReactWithASPNET.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnOpenReactWithASPNET.Location = new Point(7, 412);
            btnOpenReactWithASPNET.Name = "btnOpenReactWithASPNET";
            btnOpenReactWithASPNET.Size = new Size(177, 75);
            btnOpenReactWithASPNET.TabIndex = 6;
            btnOpenReactWithASPNET.Text = "Open ReactFE with ASP.NET BE";
            btnOpenReactWithASPNET.UseVisualStyleBackColor = true;
            btnOpenReactWithASPNET.Click += btnOpenReactWithASPNET_Click;
            // 
            // btnShowWebApp2
            // 
            btnShowWebApp2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnShowWebApp2.Location = new Point(7, 330);
            btnShowWebApp2.Margin = new Padding(3, 4, 3, 4);
            btnShowWebApp2.Name = "btnShowWebApp2";
            btnShowWebApp2.Size = new Size(177, 75);
            btnShowWebApp2.TabIndex = 5;
            btnShowWebApp2.Text = "Show WebApp 2";
            btnShowWebApp2.UseVisualStyleBackColor = true;
            btnShowWebApp2.Click += btnShowWebApp2_Click;
            // 
            // btnShowWebApp1
            // 
            btnShowWebApp1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnShowWebApp1.Location = new Point(7, 247);
            btnShowWebApp1.Margin = new Padding(3, 4, 3, 4);
            btnShowWebApp1.Name = "btnShowWebApp1";
            btnShowWebApp1.Size = new Size(177, 75);
            btnShowWebApp1.TabIndex = 4;
            btnShowWebApp1.Text = "Show WebApp 1";
            btnShowWebApp1.UseVisualStyleBackColor = true;
            btnShowWebApp1.Click += btnShowWebApp_Click;
            // 
            // btnShowExceptionMessageBox
            // 
            btnShowExceptionMessageBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnShowExceptionMessageBox.Location = new Point(7, 165);
            btnShowExceptionMessageBox.Name = "btnShowExceptionMessageBox";
            btnShowExceptionMessageBox.Size = new Size(177, 75);
            btnShowExceptionMessageBox.TabIndex = 3;
            btnShowExceptionMessageBox.Text = "Show ExceptionMessageBox";
            btnShowExceptionMessageBox.UseVisualStyleBackColor = true;
            btnShowExceptionMessageBox.Click += btnShowExceptionMessageBox_Click;
            // 
            // btnShowErrorMessageBox
            // 
            btnShowErrorMessageBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnShowErrorMessageBox.Location = new Point(7, 85);
            btnShowErrorMessageBox.Name = "btnShowErrorMessageBox";
            btnShowErrorMessageBox.Size = new Size(177, 75);
            btnShowErrorMessageBox.TabIndex = 2;
            btnShowErrorMessageBox.Text = "Show ErrorMessageBox";
            btnShowErrorMessageBox.UseVisualStyleBackColor = true;
            btnShowErrorMessageBox.Click += btnShowErrorMessageBox_Click;
            // 
            // btnEnd
            // 
            btnEnd.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnEnd.Location = new Point(7, 716);
            btnEnd.Margin = new Padding(3, 4, 3, 4);
            btnEnd.Name = "btnEnd";
            btnEnd.Size = new Size(177, 79);
            btnEnd.TabIndex = 1;
            btnEnd.Text = "Close";
            btnEnd.UseVisualStyleBackColor = true;
            btnEnd.Click += btnEnd_Click;
            // 
            // btnEmergency
            // 
            btnEmergency.Location = new Point(7, 4);
            btnEmergency.Margin = new Padding(3, 4, 3, 4);
            btnEmergency.Name = "btnEmergency";
            btnEmergency.Size = new Size(177, 75);
            btnEmergency.TabIndex = 0;
            btnEmergency.Text = "Emergency BTN";
            btnEmergency.UseVisualStyleBackColor = true;
            btnEmergency.Click += btnEmergency_Click;
            // 
            // userControlSablona1
            // 
            userControlSablona1.Dock = DockStyle.Fill;
            userControlSablona1.Location = new Point(0, 0);
            userControlSablona1.Name = "userControlSablona1";
            userControlSablona1.Size = new Size(1007, 799);
            userControlSablona1.TabIndex = 1;
            // 
            // btnSendToPLC
            // 
            btnSendToPLC.Location = new Point(658, 331);
            btnSendToPLC.Margin = new Padding(3, 4, 3, 4);
            btnSendToPLC.Name = "btnSendToPLC";
            btnSendToPLC.Size = new Size(114, 75);
            btnSendToPLC.TabIndex = 2;
            btnSendToPLC.Text = "Send to PLC";
            btnSendToPLC.UseVisualStyleBackColor = true;
            btnSendToPLC.Click += btnSendToPLC_Click;
            // 
            // listBoxJSON
            // 
            listBoxJSON.FormattingEnabled = true;
            listBoxJSON.HorizontalScrollbar = true;
            listBoxJSON.ItemHeight = 20;
            listBoxJSON.Location = new Point(11, 13);
            listBoxJSON.Margin = new Padding(3, 4, 3, 4);
            listBoxJSON.Name = "listBoxJSON";
            listBoxJSON.Size = new Size(254, 404);
            listBoxJSON.TabIndex = 3;
            // 
            // listBoxPath
            // 
            listBoxPath.FormattingEnabled = true;
            listBoxPath.ItemHeight = 20;
            listBoxPath.Location = new Point(272, 13);
            listBoxPath.Margin = new Padding(3, 4, 3, 4);
            listBoxPath.Name = "listBoxPath";
            listBoxPath.Size = new Size(254, 404);
            listBoxPath.TabIndex = 5;
            // 
            // textBoxInt1
            // 
            textBoxInt1.Location = new Point(537, 99);
            textBoxInt1.Margin = new Padding(3, 4, 3, 4);
            textBoxInt1.Name = "textBoxInt1";
            textBoxInt1.Size = new Size(114, 27);
            textBoxInt1.TabIndex = 6;
            // 
            // textBoxBool2
            // 
            textBoxBool2.Location = new Point(537, 253);
            textBoxBool2.Margin = new Padding(3, 4, 3, 4);
            textBoxBool2.Name = "textBoxBool2";
            textBoxBool2.Size = new Size(114, 27);
            textBoxBool2.TabIndex = 7;
            // 
            // textBoxInt2
            // 
            textBoxInt2.Location = new Point(537, 215);
            textBoxInt2.Margin = new Padding(3, 4, 3, 4);
            textBoxInt2.Name = "textBoxInt2";
            textBoxInt2.Size = new Size(114, 27);
            textBoxInt2.TabIndex = 8;
            // 
            // textBoxTime1
            // 
            textBoxTime1.Location = new Point(537, 176);
            textBoxTime1.Margin = new Padding(3, 4, 3, 4);
            textBoxTime1.Name = "textBoxTime1";
            textBoxTime1.Size = new Size(114, 27);
            textBoxTime1.TabIndex = 9;
            // 
            // textBoxBool1
            // 
            textBoxBool1.Location = new Point(537, 137);
            textBoxBool1.Margin = new Padding(3, 4, 3, 4);
            textBoxBool1.Name = "textBoxBool1";
            textBoxBool1.Size = new Size(114, 27);
            textBoxBool1.TabIndex = 10;
            // 
            // textBoxTime2
            // 
            textBoxTime2.Location = new Point(537, 292);
            textBoxTime2.Margin = new Padding(3, 4, 3, 4);
            textBoxTime2.Name = "textBoxTime2";
            textBoxTime2.Size = new Size(114, 27);
            textBoxTime2.TabIndex = 11;
            // 
            // lblInt1
            // 
            lblInt1.AutoSize = true;
            lblInt1.Location = new Point(658, 103);
            lblInt1.Name = "lblInt1";
            lblInt1.Size = new Size(34, 20);
            lblInt1.TabIndex = 12;
            lblInt1.Text = "Int1";
            // 
            // lblTime2
            // 
            lblTime2.AutoSize = true;
            lblTime2.Location = new Point(658, 296);
            lblTime2.Name = "lblTime2";
            lblTime2.Size = new Size(50, 20);
            lblTime2.TabIndex = 13;
            lblTime2.Text = "Time2";
            // 
            // lblBool2
            // 
            lblBool2.AutoSize = true;
            lblBool2.Location = new Point(658, 257);
            lblBool2.Name = "lblBool2";
            lblBool2.Size = new Size(48, 20);
            lblBool2.TabIndex = 14;
            lblBool2.Text = "Bool2";
            // 
            // lblInt2
            // 
            lblInt2.AutoSize = true;
            lblInt2.Location = new Point(658, 219);
            lblInt2.Name = "lblInt2";
            lblInt2.Size = new Size(34, 20);
            lblInt2.TabIndex = 15;
            lblInt2.Text = "Int2";
            // 
            // lblTime1
            // 
            lblTime1.AutoSize = true;
            lblTime1.Location = new Point(658, 180);
            lblTime1.Name = "lblTime1";
            lblTime1.Size = new Size(50, 20);
            lblTime1.TabIndex = 16;
            lblTime1.Text = "Time1";
            // 
            // lblBool1
            // 
            lblBool1.AutoSize = true;
            lblBool1.Location = new Point(658, 141);
            lblBool1.Name = "lblBool1";
            lblBool1.Size = new Size(48, 20);
            lblBool1.TabIndex = 17;
            lblBool1.Text = "Bool1";
            // 
            // btnReadFromPLC
            // 
            btnReadFromPLC.Location = new Point(133, 425);
            btnReadFromPLC.Margin = new Padding(3, 4, 3, 4);
            btnReadFromPLC.Name = "btnReadFromPLC";
            btnReadFromPLC.Size = new Size(114, 75);
            btnReadFromPLC.TabIndex = 18;
            btnReadFromPLC.Text = "Read from PLC";
            btnReadFromPLC.UseVisualStyleBackColor = true;
            btnReadFromPLC.Click += btnReadFromPLC_Click;
            // 
            // btnReadJSON
            // 
            btnReadJSON.Location = new Point(11, 425);
            btnReadJSON.Margin = new Padding(3, 4, 3, 4);
            btnReadJSON.Name = "btnReadJSON";
            btnReadJSON.Size = new Size(114, 75);
            btnReadJSON.TabIndex = 19;
            btnReadJSON.Text = "Read JSON";
            btnReadJSON.UseVisualStyleBackColor = true;
            btnReadJSON.Click += btnReadJSON_Click;
            // 
            // btnSendToJSON
            // 
            btnSendToJSON.Location = new Point(537, 331);
            btnSendToJSON.Margin = new Padding(3, 4, 3, 4);
            btnSendToJSON.Name = "btnSendToJSON";
            btnSendToJSON.Size = new Size(114, 75);
            btnSendToJSON.TabIndex = 20;
            btnSendToJSON.Text = "Send to JSON";
            btnSendToJSON.UseVisualStyleBackColor = true;
            btnSendToJSON.Click += btnSendToJSON_Click;
            // 
            // listBoxJSONVariables
            // 
            listBoxJSONVariables.FormattingEnabled = true;
            listBoxJSONVariables.ItemHeight = 20;
            listBoxJSONVariables.Location = new Point(11, 508);
            listBoxJSONVariables.Margin = new Padding(3, 4, 3, 4);
            listBoxJSONVariables.Name = "listBoxJSONVariables";
            listBoxJSONVariables.Size = new Size(254, 164);
            listBoxJSONVariables.TabIndex = 21;
            // 
            // Periodic_Function
            // 
            Periodic_Function.Tick += Periodic_Function_Tick;
            // 
            // btnShowJson
            // 
            btnShowJson.Location = new Point(537, 448);
            btnShowJson.Margin = new Padding(3, 4, 3, 4);
            btnShowJson.Name = "btnShowJson";
            btnShowJson.Size = new Size(114, 75);
            btnShowJson.TabIndex = 22;
            btnShowJson.Text = "Show JSON";
            btnShowJson.UseVisualStyleBackColor = true;
            btnShowJson.Click += btnShowJson_Click;
            // 
            // comboBoxFileChoice
            // 
            comboBoxFileChoice.FormattingEnabled = true;
            comboBoxFileChoice.Location = new Point(537, 413);
            comboBoxFileChoice.Name = "comboBoxFileChoice";
            comboBoxFileChoice.Size = new Size(235, 28);
            comboBoxFileChoice.TabIndex = 23;
            // 
            // statusStripTestForm
            // 
            statusStripTestForm.ImageScalingSize = new Size(20, 20);
            statusStripTestForm.Location = new Point(0, 775);
            statusStripTestForm.Name = "statusStripTestForm";
            statusStripTestForm.Size = new Size(1007, 24);
            statusStripTestForm.TabIndex = 24;
            statusStripTestForm.Text = "statusStrip1";
            // 
            // TestForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1194, 799);
            Controls.Add(statusStripTestForm);
            Controls.Add(comboBoxFileChoice);
            Controls.Add(btnShowJson);
            Controls.Add(listBoxJSONVariables);
            Controls.Add(btnSendToJSON);
            Controls.Add(btnReadJSON);
            Controls.Add(btnReadFromPLC);
            Controls.Add(lblBool1);
            Controls.Add(lblTime1);
            Controls.Add(lblInt2);
            Controls.Add(lblBool2);
            Controls.Add(lblTime2);
            Controls.Add(lblInt1);
            Controls.Add(textBoxTime2);
            Controls.Add(textBoxBool1);
            Controls.Add(textBoxTime1);
            Controls.Add(textBoxInt2);
            Controls.Add(textBoxBool2);
            Controls.Add(textBoxInt1);
            Controls.Add(listBoxPath);
            Controls.Add(listBoxJSON);
            Controls.Add(btnSendToPLC);
            Controls.Add(userControlSablona1);
            Controls.Add(panel1);
            DoubleBuffered = true;
            Margin = new Padding(3, 4, 3, 4);
            Name = "TestForm";
            Text = "Test";
            Load += Test_Load;
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Button btnEmergency;
        private System.Windows.Forms.Timer Periodic_Function;
        private Button btnEnd;
        private JAN0837_BP.Controls.UserControlSablona userControlSablona1;
        private Button btnSendToPLC;
        private ListBox listBoxJSON;
        private ListBox listBoxPath;
        private TextBox textBoxInt1;
        private TextBox textBoxBool2;
        private TextBox textBoxInt2;
        private TextBox textBoxTime1;
        private TextBox textBoxBool1;
        private TextBox textBoxTime2;
        private Label lblInt1;
        private Label lblTime2;
        private Label lblBool2;
        private Label lblInt2;
        private Label lblTime1;
        private Label lblBool1;
        private Button btnReadFromPLC;
        private Button btnReadJSON;
        private Button btnSendToJSON;
        private ListBox listBoxJSONVariables;
        private Button btnShowJson;
        private ComboBox comboBoxFileChoice;
        private Button btnShowExceptionMessageBox;
        private Button btnShowErrorMessageBox;
        private Button btnShowWebApp1;
        private Button btnShowWebApp2;
        private Button btnOpenReactWithASPNET;
        private StatusStrip statusStripTestForm;
    }
}