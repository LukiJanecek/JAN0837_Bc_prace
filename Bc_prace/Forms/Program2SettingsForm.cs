using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bc_prace.Extensions;
using Bc_prace.Settings;
using Sharp7;

namespace Bc_prace
{
    public partial class Program2SettingsForm : Form
    {
        public Program2SettingsForm()
        {
            InitializeComponent();
            DialogResult = DialogResult.Cancel;
            propertyGridCarWash.SelectedObject = Program.AppSettings.Data.DeepClone();
        }

        private void Program2Settings_Load(object sender, EventArgs e)
        {

        }

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
                statusStripCarWashSettings.Items.Add(lblStatus1);

                Console.WriteLine("Tia didn't respond. BE doesn't work properly. Data from PLC weren't read!!!");
            }
            else
            {
                //data přečtena 
                //všechny moje proměnné:

            }
        }

        #endregion

        //btn End 
        #region Close window 
        private void btnEnd_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        //Emergency + system error 
        #region Emergency + system error 
        private void btnEmergencySim_Click(object sender, EventArgs e)
        {
            /*
            statusStripCarWash.Items.Clear();
            ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Emergency mode activated");
            statusStripCarWash.Items.Add(lblStatus);
            */
        }
        #endregion

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
