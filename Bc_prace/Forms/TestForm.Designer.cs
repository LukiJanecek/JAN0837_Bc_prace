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
            btnShowExceptionMessageBox = new Button();
            btnShowErrorMessageBox = new Button();
            btnEnd = new Button();
            btnEmergency = new Button();
            userControlSablona1 = new JAN0837_BP.Controls.UserControlSablona();
            btnSendToPLC = new Button();
            listBoxJSON = new ListBox();
            statusStripTestForm = new StatusStrip();
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
            btnShowWebApp = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(btnShowWebApp);
            panel1.Controls.Add(btnShowExceptionMessageBox);
            panel1.Controls.Add(btnShowErrorMessageBox);
            panel1.Controls.Add(btnEnd);
            panel1.Controls.Add(btnEmergency);
            panel1.Dock = DockStyle.Right;
            panel1.Location = new Point(881, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(164, 599);
            panel1.TabIndex = 0;
            // 
            // btnShowExceptionMessageBox
            // 
            btnShowExceptionMessageBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnShowExceptionMessageBox.Location = new Point(6, 124);
            btnShowExceptionMessageBox.Margin = new Padding(3, 2, 3, 2);
            btnShowExceptionMessageBox.Name = "btnShowExceptionMessageBox";
            btnShowExceptionMessageBox.Size = new Size(155, 56);
            btnShowExceptionMessageBox.TabIndex = 3;
            btnShowExceptionMessageBox.Text = "Show ExceptionMessageBox";
            btnShowExceptionMessageBox.UseVisualStyleBackColor = true;
            btnShowExceptionMessageBox.Click += btnShowExceptionMessageBox_Click;
            // 
            // btnShowErrorMessageBox
            // 
            btnShowErrorMessageBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnShowErrorMessageBox.Location = new Point(6, 64);
            btnShowErrorMessageBox.Margin = new Padding(3, 2, 3, 2);
            btnShowErrorMessageBox.Name = "btnShowErrorMessageBox";
            btnShowErrorMessageBox.Size = new Size(155, 56);
            btnShowErrorMessageBox.TabIndex = 2;
            btnShowErrorMessageBox.Text = "Show ErrorMessageBox";
            btnShowErrorMessageBox.UseVisualStyleBackColor = true;
            btnShowErrorMessageBox.Click += btnShowErrorMessageBox_Click;
            // 
            // btnEnd
            // 
            btnEnd.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnEnd.Location = new Point(6, 537);
            btnEnd.Name = "btnEnd";
            btnEnd.Size = new Size(155, 59);
            btnEnd.TabIndex = 1;
            btnEnd.Text = "Close";
            btnEnd.UseVisualStyleBackColor = true;
            btnEnd.Click += btnEnd_Click;
            // 
            // btnEmergency
            // 
            btnEmergency.Location = new Point(6, 3);
            btnEmergency.Name = "btnEmergency";
            btnEmergency.Size = new Size(155, 56);
            btnEmergency.TabIndex = 0;
            btnEmergency.Text = "Emergency BTN";
            btnEmergency.UseVisualStyleBackColor = true;
            btnEmergency.Click += btnEmergency_Click;
            // 
            // userControlSablona1
            // 
            userControlSablona1.Dock = DockStyle.Fill;
            userControlSablona1.Location = new Point(0, 0);
            userControlSablona1.Margin = new Padding(3, 2, 3, 2);
            userControlSablona1.Name = "userControlSablona1";
            userControlSablona1.Size = new Size(881, 599);
            userControlSablona1.TabIndex = 1;
            // 
            // btnSendToPLC
            // 
            btnSendToPLC.Location = new Point(576, 248);
            btnSendToPLC.Name = "btnSendToPLC";
            btnSendToPLC.Size = new Size(100, 56);
            btnSendToPLC.TabIndex = 2;
            btnSendToPLC.Text = "Send to PLC";
            btnSendToPLC.UseVisualStyleBackColor = true;
            btnSendToPLC.Click += btnSendToPLC_Click;
            // 
            // listBoxJSON
            // 
            listBoxJSON.FormattingEnabled = true;
            listBoxJSON.HorizontalScrollbar = true;
            listBoxJSON.ItemHeight = 15;
            listBoxJSON.Location = new Point(10, 10);
            listBoxJSON.Name = "listBoxJSON";
            listBoxJSON.Size = new Size(223, 304);
            listBoxJSON.TabIndex = 3;
            // 
            // statusStripTestForm
            // 
            statusStripTestForm.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            statusStripTestForm.Dock = DockStyle.None;
            statusStripTestForm.ImageScalingSize = new Size(20, 20);
            statusStripTestForm.Location = new Point(0, 577);
            statusStripTestForm.Name = "statusStripTestForm";
            statusStripTestForm.Size = new Size(192, 22);
            statusStripTestForm.TabIndex = 4;
            statusStripTestForm.Text = "statusStrip1";
            // 
            // listBoxPath
            // 
            listBoxPath.FormattingEnabled = true;
            listBoxPath.ItemHeight = 15;
            listBoxPath.Location = new Point(238, 10);
            listBoxPath.Name = "listBoxPath";
            listBoxPath.Size = new Size(223, 304);
            listBoxPath.TabIndex = 5;
            // 
            // textBoxInt1
            // 
            textBoxInt1.Location = new Point(470, 74);
            textBoxInt1.Name = "textBoxInt1";
            textBoxInt1.Size = new Size(100, 23);
            textBoxInt1.TabIndex = 6;
            // 
            // textBoxBool2
            // 
            textBoxBool2.Location = new Point(470, 190);
            textBoxBool2.Name = "textBoxBool2";
            textBoxBool2.Size = new Size(100, 23);
            textBoxBool2.TabIndex = 7;
            // 
            // textBoxInt2
            // 
            textBoxInt2.Location = new Point(470, 161);
            textBoxInt2.Name = "textBoxInt2";
            textBoxInt2.Size = new Size(100, 23);
            textBoxInt2.TabIndex = 8;
            // 
            // textBoxTime1
            // 
            textBoxTime1.Location = new Point(470, 132);
            textBoxTime1.Name = "textBoxTime1";
            textBoxTime1.Size = new Size(100, 23);
            textBoxTime1.TabIndex = 9;
            // 
            // textBoxBool1
            // 
            textBoxBool1.Location = new Point(470, 103);
            textBoxBool1.Name = "textBoxBool1";
            textBoxBool1.Size = new Size(100, 23);
            textBoxBool1.TabIndex = 10;
            // 
            // textBoxTime2
            // 
            textBoxTime2.Location = new Point(470, 219);
            textBoxTime2.Name = "textBoxTime2";
            textBoxTime2.Size = new Size(100, 23);
            textBoxTime2.TabIndex = 11;
            // 
            // lblInt1
            // 
            lblInt1.AutoSize = true;
            lblInt1.Location = new Point(576, 77);
            lblInt1.Name = "lblInt1";
            lblInt1.Size = new Size(27, 15);
            lblInt1.TabIndex = 12;
            lblInt1.Text = "Int1";
            // 
            // lblTime2
            // 
            lblTime2.AutoSize = true;
            lblTime2.Location = new Point(576, 222);
            lblTime2.Name = "lblTime2";
            lblTime2.Size = new Size(39, 15);
            lblTime2.TabIndex = 13;
            lblTime2.Text = "Time2";
            // 
            // lblBool2
            // 
            lblBool2.AutoSize = true;
            lblBool2.Location = new Point(576, 193);
            lblBool2.Name = "lblBool2";
            lblBool2.Size = new Size(37, 15);
            lblBool2.TabIndex = 14;
            lblBool2.Text = "Bool2";
            // 
            // lblInt2
            // 
            lblInt2.AutoSize = true;
            lblInt2.Location = new Point(576, 164);
            lblInt2.Name = "lblInt2";
            lblInt2.Size = new Size(27, 15);
            lblInt2.TabIndex = 15;
            lblInt2.Text = "Int2";
            // 
            // lblTime1
            // 
            lblTime1.AutoSize = true;
            lblTime1.Location = new Point(576, 135);
            lblTime1.Name = "lblTime1";
            lblTime1.Size = new Size(39, 15);
            lblTime1.TabIndex = 16;
            lblTime1.Text = "Time1";
            // 
            // lblBool1
            // 
            lblBool1.AutoSize = true;
            lblBool1.Location = new Point(576, 106);
            lblBool1.Name = "lblBool1";
            lblBool1.Size = new Size(37, 15);
            lblBool1.TabIndex = 17;
            lblBool1.Text = "Bool1";
            // 
            // btnReadFromPLC
            // 
            btnReadFromPLC.Location = new Point(116, 319);
            btnReadFromPLC.Name = "btnReadFromPLC";
            btnReadFromPLC.Size = new Size(100, 56);
            btnReadFromPLC.TabIndex = 18;
            btnReadFromPLC.Text = "Read from PLC";
            btnReadFromPLC.UseVisualStyleBackColor = true;
            btnReadFromPLC.Click += btnReadFromPLC_Click;
            // 
            // btnReadJSON
            // 
            btnReadJSON.Location = new Point(10, 319);
            btnReadJSON.Name = "btnReadJSON";
            btnReadJSON.Size = new Size(100, 56);
            btnReadJSON.TabIndex = 19;
            btnReadJSON.Text = "Read JSON";
            btnReadJSON.UseVisualStyleBackColor = true;
            btnReadJSON.Click += btnReadJSON_Click;
            // 
            // btnSendToJSON
            // 
            btnSendToJSON.Location = new Point(470, 248);
            btnSendToJSON.Name = "btnSendToJSON";
            btnSendToJSON.Size = new Size(100, 56);
            btnSendToJSON.TabIndex = 20;
            btnSendToJSON.Text = "Send to JSON";
            btnSendToJSON.UseVisualStyleBackColor = true;
            btnSendToJSON.Click += btnSendToJSON_Click;
            // 
            // listBoxJSONVariables
            // 
            listBoxJSONVariables.FormattingEnabled = true;
            listBoxJSONVariables.ItemHeight = 15;
            listBoxJSONVariables.Location = new Point(10, 381);
            listBoxJSONVariables.Name = "listBoxJSONVariables";
            listBoxJSONVariables.Size = new Size(223, 124);
            listBoxJSONVariables.TabIndex = 21;
            // 
            // Periodic_Function
            // 
            Periodic_Function.Tick += Periodic_Function_Tick;
            // 
            // btnShowJson
            // 
            btnShowJson.Location = new Point(470, 336);
            btnShowJson.Name = "btnShowJson";
            btnShowJson.Size = new Size(100, 56);
            btnShowJson.TabIndex = 22;
            btnShowJson.Text = "Show JSON";
            btnShowJson.UseVisualStyleBackColor = true;
            btnShowJson.Click += btnShowJson_Click;
            // 
            // comboBoxFileChoice
            // 
            comboBoxFileChoice.FormattingEnabled = true;
            comboBoxFileChoice.Location = new Point(470, 310);
            comboBoxFileChoice.Margin = new Padding(3, 2, 3, 2);
            comboBoxFileChoice.Name = "comboBoxFileChoice";
            comboBoxFileChoice.Size = new Size(206, 23);
            comboBoxFileChoice.TabIndex = 23;
            // 
            // btnShowWebApp
            // 
            btnShowWebApp.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnShowWebApp.Location = new Point(6, 185);
            btnShowWebApp.Name = "btnShowWebApp";
            btnShowWebApp.Size = new Size(155, 56);
            btnShowWebApp.TabIndex = 4;
            btnShowWebApp.Text = "Show WebApp";
            btnShowWebApp.UseVisualStyleBackColor = true;
            btnShowWebApp.Click += btnShowWebApp_Click;
            // 
            // TestForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1045, 599);
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
            Controls.Add(statusStripTestForm);
            Controls.Add(listBoxJSON);
            Controls.Add(btnSendToPLC);
            Controls.Add(userControlSablona1);
            Controls.Add(panel1);
            DoubleBuffered = true;
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
        private StatusStrip statusStripTestForm;
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
        private Button btnShowWebApp;
    }
}