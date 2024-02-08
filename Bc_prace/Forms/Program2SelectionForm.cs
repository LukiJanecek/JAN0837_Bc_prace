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

        //DB5 => CarWash_DB 3.7
        private byte[] read_buffer_DB5 = new byte[4];
        private byte[] send_buffer_DB5 = new byte[4];

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
                //DB5 => CarWash_DB
                int readResult = client.DBRead(5, 0, read_buffer_DB5.Length, read_buffer_DB5);
                if (readResult != 0)
                {
                    statusStripCarWashSelection.Items.Clear();
                    ToolStripStatusLabel lblStatus1 = new ToolStripStatusLabel("Variables were not read.");
                    statusStripCarWashSelection.Items.Add(lblStatus1);

                    if (!errorMessageBoxShown)
                    {
                        errorMessageBoxShown = true;

                        //MessageBox
                        MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data from PLC weren't read from DB5!!!", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }
                else
                {
                    //input variables
                    #region Input variables

                    CarWashEmergencySTOP = S7.GetBitAt(read_buffer_DB5, 0, 0);
                    CarWashErrorSystem = S7.GetBitAt(read_buffer_DB5, 0, 1);
                    CarWashStartCarWash = S7.GetBitAt(read_buffer_DB5, 0, 2);
                    CarWashWaitingForIncomingCar = S7.GetBitAt(read_buffer_DB5, 0, 3);
                    CarWashWaitingForOutgoingCar = S7.GetBitAt(read_buffer_DB5, 0, 4);
                    CarWashPerfetWash = S7.GetBitAt(read_buffer_DB5, 0, 5);
                    CarWashPerfectPolish = S7.GetBitAt(read_buffer_DB5, 0, 6);

                    #endregion

                    //output variables
                    #region Output variables 

                    CarWashPositionShower = S7.GetBitAt(read_buffer_DB5, 2, 0);
                    CarWashPositionCar = S7.GetBitAt(read_buffer_DB5, 2, 1);
                    CarWashGreenLight = S7.GetBitAt(read_buffer_DB5, 2, 2);
                    CarWashRedLight = S7.GetBitAt(read_buffer_DB5, 2, 3);
                    CarWashYellowLight = S7.GetBitAt(read_buffer_DB5, 2, 4);
                    CarWashDoor1UP = S7.GetBitAt(read_buffer_DB5, 2, 5);
                    CarWashDoor1DOWN = S7.GetBitAt(read_buffer_DB5, 2, 6);
                    CarWashDoor2UP = S7.GetBitAt(read_buffer_DB5, 2, 7);
                    CarWashDoor2DOWN = S7.GetBitAt(read_buffer_DB5, 3, 0);
                    CarWashWater = S7.GetBitAt(read_buffer_DB5, 3, 1);
                    CarWashWashingChemicalsFRONT = S7.GetBitAt(read_buffer_DB5, 3, 2);
                    CarWashWashingChemicalsSIDES = S7.GetBitAt(read_buffer_DB5, 3, 3);
                    CarWashWashingChemicalsBACK = S7.GetBitAt(read_buffer_DB5, 3, 4);
                    CarWashWax = S7.GetBitAt(read_buffer_DB5, 3, 5);
                    CarWashVarnishProtection = S7.GetBitAt(read_buffer_DB5, 3, 6);
                    CarWashDry = S7.GetBitAt(read_buffer_DB5, 3, 7);

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

