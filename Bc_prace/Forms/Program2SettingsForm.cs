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

        //input
        #region Input variables
        bool CarWashEmergencySTOP;
        bool CarWashErrorSystem;
        bool CarWashStartCarWash;
        bool CarWashWaitingForIncomingCar;
        bool CarWashWaitingForOutgoingCar;
        bool CarWashPerfetWash;
        bool CarWashPerfectPolish;
        #endregion

        //output
        #region Output variables 
        bool CarWashPositionShower;
        bool CarWashPositionCar;
        bool CarWashGreenLight;
        bool CarWashRedLight;
        bool CarWashYellowLight;
        bool CarWashDoor1UP;
        bool CarWashDoor1DOWN;
        bool CarWashDoor2UP;
        bool CarWashDoor2DOWN;
        bool CarWashWater;
        bool CarWashWashingChemicalsFRONT;
        bool CarWashWashingChemicalsSIDES;
        bool CarWashWashingChemicalsBACK;
        bool CarWashWax;
        bool CarWashVarnishProtection;
        bool CarWashDry;
        #endregion

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

                //input
                #region Input variables
                /*
                CarWashEmergencySTOP = S7.GetBitAt(read_buffer, ,);
                CarWashErrorSystem = S7.GetBitAt(read_buffer, ,);
                CarWashStartCarWash = S7.GetBitAt(read_buffer, ,);
                CarWashWaitingForIncomingCar = S7.GetBitAt(read_buffer, ,);
                CarWashWaitingForOutgoingCar = S7.GetBitAt(read_buffer, ,);
                CarWashPerfetWash = S7.GetBitAt(read_buffer, ,);
                CarWashPerfectPolish = S7.GetBitAt(read_buffer, ,);
                */
                #endregion

                //output
                #region Output variables 
                /*
                CarWashPositionShower = S7.GetBitAt(read_buffer, ,);
                CarWashPositionCar = S7.GetBitAt(read_buffer, ,);
                CarWashGreenLight = S7.GetBitAt(read_buffer, ,);
                CarWashRedLight = S7.GetBitAt(read_buffer, ,);
                CarWashYellowLight = S7.GetBitAt(read_buffer, ,);
                CarWashDoor1UP = S7.GetBitAt(read_buffer, ,);
                CarWashDoor1DOWN = S7.GetBitAt(read_buffer, ,);
                CarWashDoor2UP = S7.GetBitAt(read_buffer, ,);
                CarWashDoor2DOWN = S7.GetBitAt(read_buffer, ,);
                CarWashWater = S7.GetBitAt(read_buffer, ,);
                CarWashWashingChemicalsFRONT = S7.GetBitAt(read_buffer, ,);
                CarWashWashingChemicalsSIDES = S7.GetBitAt(read_buffer, ,);
                CarWashWashingChemicalsBACK = S7.GetBitAt(read_buffer, ,);
                CarWashWax = S7.GetBitAt(read_buffer, ,);
                CarWashVarnishProtection = S7.GetBitAt(read_buffer, ,);
                CarWashDry = S7.GetBitAt(read_buffer, ,);
                */
                #endregion

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
