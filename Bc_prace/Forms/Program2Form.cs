using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bc_prace.Controls.MyGraphControl.Entities;
using Bc_prace.Settings;
using Sharp7;

namespace Bc_prace
{
    public partial class Program2Form : Form
    {

        public Program2Form()
        {
            InitializeComponent();
            InitializeButton();
        }

        //Varialbes
        #region Variables

        //Count
        int countWax = 0;
        int countSoap = 0;
        int countActiveFoam = 0;

        int SignalizationCount = 0;

        #endregion

        //Tia connection
        #region Tia connection

        //zde vypisu vsechny promenne
        public S7Client client = new S7Client();
        public byte[] send_buffer = new byte[5u];
        public byte[] read_buffer = new byte[6u];

        private void Timer_read_from_PLC_Tick(object sender, EventArgs e)
        {
            int readResult = client.DBRead(11, 0, read_buffer.Length, read_buffer);
            if (readResult != 0)
            {
                //možná raději přidat label 
                ToolStripStatusLabel lblStatus1 = new ToolStripStatusLabel("Variables were not read.");
                statusStripCarWash.Items.Add(lblStatus1);

                Console.WriteLine("Tia didn't respond. BE doesn't work properly. Data from PLC weren't read!!!");
            }
            else
            {
                //data přečtena 
                //všechny moje proměnné:

            }
        }

        #endregion

        //Start CarWash
        #region Start CarWash
        private void btnStartCarWash_Click(object sender, EventArgs e)
        {
            Program2SelectionForm Selection = new Program2SelectionForm();
            Selection.ShowDialog(this);
        }
        #endregion

        //Settings
        #region Settings 
        private void btnSettings_Click(object sender, EventArgs e)
        {
            statusStripCarWash.Items.Clear();
            ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Running Settings");
            statusStripCarWash.Items.Add(lblStatus);

            Program2SettingsForm Settings = new Program2SettingsForm();
            Settings.ShowDialog(this);
        }
        #endregion

        private void Program2_Load(object sender, EventArgs e)
        {



        }

        //Emergency + system error 
        #region Emergency + system error 
        private void btnEmergency_Click(object sender, EventArgs e)
        {
            statusStripCarWash.Items.Clear();
            ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Emergency mode activated");
            statusStripCarWash.Items.Add(lblStatus);
        }
        #endregion

        //btn End 
        #region Close window 
        private void btnEnd_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion



        //Car signalization
        #region Car signalization

        private void InitializeButton()
        {
            // Nastavení výchozí barvy a textu tlačítka
            btnSignalization.BackColor = System.Drawing.Color.Green;
            btnSignalization.Text = "Go";

            btnSignalization.Click += btnSignalization_Click;

        }

        private void btnSignalization_Click(object sender, EventArgs e)
        {
            // Inkrementace počítadla kliknutí
            SignalizationCount++;

            //SignalizationCount = SignalizationCount % 4 + 1;

            // Podle počtu kliknutí změň barvu a text tlačítka
            switch (SignalizationCount % 4)
            {
                case 1:
                    btnSignalization.BackColor = System.Drawing.Color.Green;
                    btnSignalization.Text = "Go";
                    /*
                    statusStripCarWash.Items.Clear();
                    ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Start");
                    statusStripCarWash.Items.Add(lblStatus);
                    */
                    break;

                case 2:
                    btnSignalization.BackColor = System.Drawing.Color.Yellow;
                    btnSignalization.Text = "Error";
                    /*
                    statusStripCarWash.Items.Clear();
                    ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Wait");
                    statusStripCarWash.Items.Add(lblStatus);
                    */
                    break;

                case 3:
                    btnSignalization.BackColor = System.Drawing.Color.Red;
                    btnSignalization.Text = "Stop";
                    /*
                    statusStripCarWash.Items.Clear();
                    ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Stop");
                    statusStripCarWash.Items.Add(lblStatus);
                    */
                    break;
            }
        }

        #endregion
    }
}
