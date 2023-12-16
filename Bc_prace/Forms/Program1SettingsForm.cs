using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Bc_prace.Extensions;
using Bc_prace.Settings;
using Sharp7;

namespace Bc_prace
{
    public partial class Program1SettingsForm : Form
    {
        public Program1SettingsForm()
        {
            InitializeComponent();
            DialogResult = DialogResult.Cancel;
            propertyGridElevator.SelectedObject = Program.AppSettings.Data.DeepClone();
        }

        private void Program1Settings_Load(object sender, EventArgs e)
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
                statusStripElevatorSettings.Items.Add(lblStatus1);

                Console.WriteLine("Tia didn't respond. BE doesn't work properly. Data from PLC weren't read!!!");
            }
            else
            {
                //data přečtena 
                //všechny moje proměnné:

            }
        }

        #endregion


        //What happend when you press the buttons?
        #region TIA variables

        //btn SetData
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Program.AppSettings.Data = (ElevatorSettingsData)propertyGridElevator.SelectedObject;
            Program.AppSettings.SaveSettings();
            Program.UpdateSettings();
            //this.Close(); //toto tady asi nepatri
        }

        //btn Cancel
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        //btn LoadData
        private void btnLoadData_Click(object sender, EventArgs e)
        {
            int readResult = client.DBRead(1, 0, read_buffer.Length, read_buffer);
            if (readResult != 0)
            {
                Console.WriteLine("Tia didn't respond. BE doesn't work properly.");
            }
            else
            {
                //zde budou vsechyn promenne z Tia 

            }
        }
        private void btnDoorOPEN_Click(object sender, EventArgs e)
        {

        }

        private void btnDoorCLOSE_Click(object sender, EventArgs e)
        {

        }
        #endregion

        //btn End 
        #region Close window 
        private void btnEnd_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        //Emergency simulation
        #region Emergency simulation
        public event EventHandler EmergencySimulationClicked;

        private void btnEmergencySim_Click(object sender, EventArgs e)
        {
            EmergencySimulationClicked?.Invoke(this, EventArgs.Empty);

            statusStripElevatorSettings.Items.Clear();
            ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Emergency mode activated");
            statusStripElevatorSettings.Items.Add(lblStatus);
        }
        #endregion
    }
}
