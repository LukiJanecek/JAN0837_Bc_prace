using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bc_prace.Settings;
using Sharp7;

namespace Bc_prace
{
    public partial class Program2SelectionForm : Form
    {
        public Program2SelectionForm()
        {
            InitializeComponent();

            //start timer
            Timer_read_from_PLC.Start();
            //set time interval (ms)
            Timer_read_from_PLC.Interval = 100;
        }

        //Variables
        #region Variables

        //C# variables
        #region C# variables

        private bool errorMessageBoxShown = false;

        #endregion

        //Tia variables
        #region Tia variables

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

        #endregion

        #endregion

        //Tia connection
        #region Tia connection

        private void Timer_read_from_PLC_Tick(object sender, EventArgs e)
        {
            try
            {
                int readResult = client.DBRead(11, 0, read_buffer.Length, read_buffer);
                if (readResult != 0)
                {
                    statusStripCarWashSelection.Items.Clear();
                    ToolStripStatusLabel lblStatus1 = new ToolStripStatusLabel("Variables were not read.");
                    statusStripCarWashSelection.Items.Add(lblStatus1);

                    if (!errorMessageBoxShown)
                    {
                        //MessageBox
                        MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data from PLC weren't read!!!", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                        errorMessageBoxShown = true;
                    }
                }
                else
                {
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
            catch (Exception ex) 
            {
                if (!errorMessageBoxShown)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
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
    }
}

