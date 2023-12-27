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

        //inputs
        #region Input variables
        bool ElevatorBTNCabin1;
        bool ElevatorBTNCabin2;
        bool ElevatorBTNCabin3;
        bool ElevatorBTNCabin4;
        bool ElevatorBTNCabin5;
        bool ElevatorBTNFloor1;
        bool ElevatorBTNFloor2;
        bool ElevatorBTNFloor3;
        bool ElevatorBTNFloor4;
        bool ElevatorBTNFloor5;
        bool ElevatorDoorSEQ;
        bool ElevatorBTNOPENCLOSE;
        bool ElevatorEmergencySTOP;
        bool ElevatorErrorSystem;
        #endregion

        //outputs
        #region Output variables
        bool ElevatorMotorON;
        bool ElevatorMotorDOWN;
        bool ElevatorMotorUP;
        bool ElevatroHoming;
        bool ElevatorSystemReady;
        int ElevatorActualFloor;
        bool ElevatorMoving;
        bool ElevatorSystemWorking;
        int ElevatorGoToFloor;
        bool ElevatorDirection;
        bool ElevatorActualFloorLED1;
        bool ElevatorActualFloorLED2;
        bool ElevatorActualFloorLED3;
        bool ElevatorActualFloorLED4;
        bool ElevatorActualFloorLED5;
        bool ElevatorActualFloorCabinLED1;
        bool ElevatorActualFloorCabinLED2;
        bool ElevatorActualFloorCabinLED3;
        bool ElevatorActualFloorCabinLED4;
        bool ElevatorActualFloorCabinLED5;
        bool ElevatorActualFloorSENS1;
        bool ElevatorActualFloorSENS2;
        bool ElevatorActualFloorSENS3;
        bool ElevatorActualFloorSENS4;
        bool ElevatorActualFloorSENS5;
        string ElevatorTimeDoorSQOPEN;
        string ElevatroTimeDoorSQCLOSE;
        bool ElevatorDoorClOSE;
        bool ElevatorDoorOPEN;
        int ElevatorCabinSpeed;
        bool ElevatorInactivity;
        string ElevatorTimeToGetDown;
        #endregion

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

                //inputs
                #region Input variables 
                /*
                ElevatorBTNCabin1 = S7.GetBitAt(read_buffer, ,);
                ElevatorBTNCabin2 = S7.GetBitAt(read_buffer, ,);
                ElevatorBTNCabin3 = S7.GetBitAt(read_buffer, ,);
                ElevatorBTNCabin4 = S7.GetBitAt(read_buffer, ,);
                ElevatorBTNCabin5 = S7.GetBitAt(read_buffer, ,);
                ElevatorBTNFloor1 = S7.GetBitAt(read_buffer, ,);
                ElevatorBTNFloor2 = S7.GetBitAt(read_buffer, ,);
                ElevatorBTNFloor3 = S7.GetBitAt(read_buffer, ,);
                ElevatorBTNFloor4 = S7.GetBitAt(read_buffer, ,);
                ElevatorBTNFloor5 = S7.GetBitAt(read_buffer, ,);
                ElevatorDoorSEQ = S7.GetBitAt(read_buffer, ,);
                ElevatorBTNOPENCLOSE = S7.GetBitAt(read_buffer, ,);
                ElevatorEmergencySTOP = S7.GetBitAt(read_buffer, ,);
                ElevatorErrorSystem = S7.GetBitAt(read_buffer, ,);
                */
                #endregion

                //outputs
                #region Output variables
                /*
                ElevatorMotorON = S7.GetBitAt(read_buffer, ,); ;
                ElevatorMotorDOWN = S7.GetBitAt(read_buffer, ,); ;
                ElevatorMotorUP = S7.GetBitAt(read_buffer, ,); ;
                ElevatroHoming = S7.GetBitAt(read_buffer, ,); ;
                ElevatorSystemReady = S7.GetBitAt(read_buffer, ,); ;
                ElevatorActualFloor = S7.GetIntAt(read_buffer, ,);
                ElevatorMoving = S7.GetBitAt(read_buffer, ,); ;
                ElevatorSystemWorking = S7.GetBitAt(read_buffer, ,); ;
                ElevatorGoToFloor = S7.GetIntAt(read_buffer, ,);
                ElevatorDirection = S7.GetBitAt(read_buffer, ,); ;
                ElevatorActualFloorLED1 = S7.GetBitAt(read_buffer, ,); ;
                ElevatorActualFloorLED2 = S7.GetBitAt(read_buffer, ,); ;
                ElevatorActualFloorLED3 = S7.GetBitAt(read_buffer, ,); ;
                ElevatorActualFloorLED4 = S7.GetBitAt(read_buffer, ,); ;
                ElevatorActualFloorLED5 = S7.GetBitAt(read_buffer, ,); ;
                ElevatorActualFloorCabinLED1 = S7.GetBitAt(read_buffer, ,); ;
                ElevatorActualFloorCabinLED2 = S7.GetBitAt(read_buffer, ,); ;
                ElevatorActualFloorCabinLED3 = S7.GetBitAt(read_buffer, ,); ;
                ElevatorActualFloorCabinLED4 = S7.GetBitAt(read_buffer, ,); ;
                ElevatorActualFloorCabinLED5 = S7.GetBitAt(read_buffer, ,); ;
                ElevatorActualFloorSENS1 = S7.GetBitAt(read_buffer, ,); ;
                ElevatorActualFloorSENS2 = S7.GetBitAt(read_buffer, ,); ;
                ElevatorActualFloorSENS3 = S7.GetBitAt(read_buffer, ,); ;
                ElevatorActualFloorSENS4 = S7.GetBitAt(read_buffer, ,); ;
                ElevatorActualFloorSENS5 = S7.GetBitAt(read_buffer, ,); ;
                ElevatorTimeDoorSQOPEN = S7.;
                ElevatroTimeDoorSQCLOSE = S7.;
                ElevatorDoorClOSE = S7.GetBitAt(read_buffer, ,); ;
                ElevatorDoorOPEN = S7.GetBitAt(read_buffer, ,); ;
                ElevatorCabinSpeed = S7.GetIntAt(read_buffer, ,);
                ElevatorInactivity = S7.GetBitAt(read_buffer, ,); ;
                ElevatorTimeToGetDown = S7.;
                */
                #endregion
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
