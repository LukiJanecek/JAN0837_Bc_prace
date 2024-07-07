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
using System.Security.Cryptography;

namespace Bc_prace
{
    public partial class CarWashSelectionForm : Form
    {
        //files
        public const string backupJSONFilePath = "~/Data/backupFile.json";
        public const string ElevatroDBJSONFilePath = "~/Data/ElevatorDB.json";
        public const string CarWashDBJSONFilePath = "~/Data/CarWashDB.json";
        public const string CrossroadDBJSONFilePath = "~/Data/CrossroadDB.json";
        public const string logger_file = "~/Data/Logger_file.json";

        private ChooseOptionForm chooseOptionFormInstance;

        S7Client client;

        private bool errorMessageBoxShown = false;

        //DB11 => Maintain_DB 
        private int DBNumber_DB11 = 11;
        byte[] read_buffer_DB11;
        byte[] send_buffer_DB11;

        //DB5 => CarWash_DB 
        private int DBNumber_DB5 = 5;
        byte[] read_buffer_DB5;
        public byte[] previous_buffer_DB5;
        public byte[] PreviousBufferHash_DB;
        byte[] send_buffer_DB5;


        //Input variables
        #region Input variables

        bool CarWashEmergencySTOP;
        bool CarWashErrorSystem;
        bool CarWashStartCarWash;
        bool CarWashWaitingForIncomingCar;
        bool CarWashWaitingForOutgoingCar;
        bool CarWashPerfetWash;
        bool CarWashPerfectPolish;
        bool CarWashPositionShower;
        bool CarWashPositionCar;

        #endregion

        //Output variables 
        #region Output variables 

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
        bool CarWashSoap;
        bool CarWashActiveFoam;
        bool CarWashBrushes;

        #endregion

        public CarWashSelectionForm(ChooseOptionForm chooseOptionFormInstance)
        {
            InitializeComponent();
            this.MinimumSize = new Size(356, 310); 

            this.chooseOptionFormInstance = chooseOptionFormInstance;

            client = chooseOptionFormInstance.client;

            //buffers
            //DB11 => Maintain_DB
            read_buffer_DB11 = chooseOptionFormInstance.read_buffer_DB11;
            send_buffer_DB11 = chooseOptionFormInstance.send_buffer_DB11;
            //DB5 => CarWash_DB
            read_buffer_DB5 = chooseOptionFormInstance.read_buffer_DB5;
            send_buffer_DB5 = chooseOptionFormInstance.send_buffer_DB5;

            //start timer
            Timer_read_actual.Start();
            //set time interval (ms)
            Timer_read_actual.Interval = 100;
        }

        //Tia connection
        #region Tia connection

        private void Timer_read_actual_Tick(object sender, EventArgs e)
        {
            try
            {
                //Input variables 
                #region Input variables 

                CarWashEmergencySTOP = chooseOptionFormInstance.CarWashEmergencySTOP;
                CarWashErrorSystem = chooseOptionFormInstance.CarWashErrorSystem;
                CarWashStartCarWash = chooseOptionFormInstance.CarWashStartCarWash;
                CarWashWaitingForIncomingCar = chooseOptionFormInstance.CarWashWaitingForIncomingCar;
                CarWashWaitingForOutgoingCar = chooseOptionFormInstance.CarWashWaitingForOutgoingCar;
                CarWashPerfetWash = chooseOptionFormInstance.CarWashPerfetWash;
                CarWashPerfectPolish = chooseOptionFormInstance.CarWashPerfectPolish;
                CarWashPositionShower = chooseOptionFormInstance.CarWashPositionShower;
                CarWashPositionCar = chooseOptionFormInstance.CarWashPositionCar;

                #endregion

                //Output variables
                #region Output variables

                CarWashGreenLight = chooseOptionFormInstance.CarWashGreenLight;
                CarWashRedLight = chooseOptionFormInstance.CarWashRedLight;
                CarWashYellowLight = chooseOptionFormInstance.CarWashYellowLight;
                CarWashDoor1UP = chooseOptionFormInstance.CarWashDoor1UP;
                CarWashDoor1DOWN = chooseOptionFormInstance.CarWashDoor1DOWN;
                CarWashDoor2UP = chooseOptionFormInstance.CarWashDoor2UP;
                CarWashDoor2DOWN = chooseOptionFormInstance.CarWashDoor2DOWN;
                CarWashWater = chooseOptionFormInstance.CarWashWater;
                CarWashWashingChemicalsFRONT = chooseOptionFormInstance.CarWashWashingChemicalsFRONT;
                CarWashWashingChemicalsSIDES = chooseOptionFormInstance.CarWashWashingChemicalsSIDES;
                CarWashWashingChemicalsBACK = chooseOptionFormInstance.CarWashWashingChemicalsBACK;
                CarWashWax = chooseOptionFormInstance.CarWashWax;
                CarWashVarnishProtection = chooseOptionFormInstance.CarWashVarnishProtection;
                CarWashDry = chooseOptionFormInstance.CarWashDry;
                CarWashSoap = chooseOptionFormInstance.CarWashSoap;
                CarWashActiveFoam = chooseOptionFormInstance.CarWashActiveFoam;
                CarWashBrushes = chooseOptionFormInstance.CarWashBrushes;

                #endregion

                //Reading variables with MultiVar method
                /*
                #region Multi read -> MultiVar

                S7MultiVar reader = new S7MultiVar(client);

                //DB5 => CarWash_DB -> 2 structs -> 23 variables -> size 3.7
                //first struct -> Input -> 7 variables -> 0.6 size 
                reader.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, DBNumber_DB5, 0, 0, ref read_buffer_DB5_Input); //read_buffer_DB5_Input.Length
                //second struct -> Output -> 16 variables -> 3.7 size
                reader.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, DBNumber_DB5, 0, 0, ref read_buffer_DB5_Output); //read_buffer_DB5_Input.Length

                int readResultDB5 = reader.Read();

                if (readResultDB5 == 0)
                {
                    byte[] currentHashDB5_Input = ComputeHash(read_buffer_DB5_Input);

                    // Porovnání hashe s předchozím hashem
                    if (!ArraysAreEqual(currentHashDB5_Input, PreviousBufferHash_DB5_Input))
                    {
                        // Aktualizace předchozího bufferu a hashe
                        Array.Copy(read_buffer_DB5_Input, previous_buffer_DB5_Input, read_buffer_DB5_Input.Length);
                        PreviousBufferHash_DB5_Input = currentHashDB5_Input;

                        // Aktualizace proměnných na základě nových dat
                        
                        //Input variables
                        #region Input variables

                        CarWashEmergencySTOP = S7.GetBitAt(read_buffer_DB5_Input, 0, 0);
                        CarWashErrorSystem = S7.GetBitAt(read_buffer_DB5_Input, 0, 1);
                        CarWashStartCarWash = S7.GetBitAt(read_buffer_DB5_Input, 0, 2);
                        CarWashWaitingForIncomingCar = S7.GetBitAt(read_buffer_DB5_Input, 0, 3);
                        CarWashWaitingForOutgoingCar = S7.GetBitAt(read_buffer_DB5_Input, 0, 4);
                        CarWashPerfetWash = S7.GetBitAt(read_buffer_DB5_Input, 0, 5);
                        CarWashPerfectPolish = S7.GetBitAt(read_buffer_DB5_Input, 0, 6);

                        #endregion

                        //Output variables
                        #region Output variables 

                        CarWashPositionShower = S7.GetBitAt(read_buffer_DB5_Output, 2, 0);
                        CarWashPositionCar = S7.GetBitAt(read_buffer_DB5_Output, 2, 1);
                        CarWashGreenLight = S7.GetBitAt(read_buffer_DB5_Output, 2, 2);
                        CarWashRedLight = S7.GetBitAt(read_buffer_DB5_Output, 2, 3);
                        CarWashYellowLight = S7.GetBitAt(read_buffer_DB5_Output, 2, 4);
                        CarWashDoor1UP = S7.GetBitAt(read_buffer_DB5_Output, 2, 5);
                        CarWashDoor1DOWN = S7.GetBitAt(read_buffer_DB5_Output, 2, 6);
                        CarWashDoor2UP = S7.GetBitAt(read_buffer_DB5_Output, 2, 7);
                        CarWashDoor2DOWN = S7.GetBitAt(read_buffer_DB5_Output, 3, 0);
                        CarWashWater = S7.GetBitAt(read_buffer_DB5_Output, 3, 1);
                        CarWashWashingChemicalsFRONT = S7.GetBitAt(read_buffer_DB5_Output, 3, 2);
                        CarWashWashingChemicalsSIDES = S7.GetBitAt(read_buffer_DB5_Output, 3, 3);
                        CarWashWashingChemicalsBACK = S7.GetBitAt(read_buffer_DB5_Output, 3, 4);
                        CarWashWax = S7.GetBitAt(read_buffer_DB5_Output, 3, 5);
                        CarWashVarnishProtection = S7.GetBitAt(read_buffer_DB5_Output, 3, 6);
                        CarWashDry = S7.GetBitAt(read_buffer_DB5_Output, 3, 7);

                        #endregion

                        errorMessageBoxShown = false;
                    }

                    //Input variables
                    #region Input variables

                    CarWashEmergencySTOP = S7.GetBitAt(read_buffer_DB5_Input, 0, 0);
                    CarWashErrorSystem = S7.GetBitAt(read_buffer_DB5_Input, 0, 1);
                    CarWashStartCarWash = S7.GetBitAt(read_buffer_DB5_Input, 0, 2);
                    CarWashWaitingForIncomingCar = S7.GetBitAt(read_buffer_DB5_Input, 0, 3);
                    CarWashWaitingForOutgoingCar = S7.GetBitAt(read_buffer_DB5_Input, 0, 4);
                    CarWashPerfetWash = S7.GetBitAt(read_buffer_DB5_Input, 0, 5);
                    CarWashPerfectPolish = S7.GetBitAt(read_buffer_DB5_Input, 0, 6);

                    #endregion

                    //Output variables
                    #region Output variables 

                    CarWashPositionShower = S7.GetBitAt(read_buffer_DB5_Output, 2, 0);
                    CarWashPositionCar = S7.GetBitAt(read_buffer_DB5_Output, 2, 1);
                    CarWashGreenLight = S7.GetBitAt(read_buffer_DB5_Output, 2, 2);
                    CarWashRedLight = S7.GetBitAt(read_buffer_DB5_Output, 2, 3);
                    CarWashYellowLight = S7.GetBitAt(read_buffer_DB5_Output, 2, 4);
                    CarWashDoor1UP = S7.GetBitAt(read_buffer_DB5_Output, 2, 5);
                    CarWashDoor1DOWN = S7.GetBitAt(read_buffer_DB5_Output, 2, 6);
                    CarWashDoor2UP = S7.GetBitAt(read_buffer_DB5_Output, 2, 7);
                    CarWashDoor2DOWN = S7.GetBitAt(read_buffer_DB5_Output, 3, 0);
                    CarWashWater = S7.GetBitAt(read_buffer_DB5_Output, 3, 1);
                    CarWashWashingChemicalsFRONT = S7.GetBitAt(read_buffer_DB5_Output, 3, 2);
                    CarWashWashingChemicalsSIDES = S7.GetBitAt(read_buffer_DB5_Output, 3, 3);
                    CarWashWashingChemicalsBACK = S7.GetBitAt(read_buffer_DB5_Output, 3, 4);
                    CarWashWax = S7.GetBitAt(read_buffer_DB5_Output, 3, 5);
                    CarWashVarnishProtection = S7.GetBitAt(read_buffer_DB5_Output, 3, 6);
                    CarWashDry = S7.GetBitAt(read_buffer_DB5_Output, 3, 7);

                    #endregion

                    errorMessageBoxShown = false;
                }
                else
                {
                    //error
                    statusStripCarWashSelection.Items.Clear();
                    ToolStripStatusLabel lblStatus1 = new ToolStripStatusLabel("Variables were not read.");
                    statusStripCarWashSelection.Items.Add(lblStatus1);

                    if (!errorMessageBoxShown)
                    {
                        errorMessageBoxShown = true;

                        //MessageBox
                        MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data from PLC weren't read from DB5!!! \n\n" +
                            $"Error message {readResultDB5} \n", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }

                #endregion
                */

                //Reading variables with DBRead method
                /*
                #region DBRead

                //DB5 => CarWash_DB
                int readResult = client.DBRead(DBNumber_DB5, 0, read_buffer_DB5.Length, read_buffer_DB5);
                if (readResult != 0)
                {
                    statusStripCarWash.Items.Clear();
                    ToolStripStatusLabel lblStatus1 = new ToolStripStatusLabel("Variables were not read.");
                    statusStripCarWash.Items.Add(lblStatus1);

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
                    //input
                    #region Input variables
                    
                    CarWashEmergencySTOP = S7.GetBitAt(read_buffer_DB5_Input, 0, 0);
                    CarWashErrorSystem = S7.GetBitAt(read_buffer_DB5_Input, 0, 1);
                    CarWashStartCarWash = S7.GetBitAt(read_buffer_DB5_Input, 0, 2);
                    CarWashWaitingForIncomingCar = S7.GetBitAt(read_buffer_DB5_Input, 0, 3);
                    CarWashWaitingForOutgoingCar = S7.GetBitAt(read_buffer_DB5_Input, 0, 4);
                    CarWashPerfetWash = S7.GetBitAt(read_buffer_DB5_Input, 0, 5);
                    CarWashPerfectPolish = S7.GetBitAt(read_buffer_DB5_Input, 0, 6);
                    
                    #endregion

                    //output
                    #region Output variables 
                    
                    CarWashPositionShower = S7.GetBitAt(read_buffer_DB5_Output, 2, 0);
                    CarWashPositionCar = S7.GetBitAt(read_buffer_DB5_Output, 2, 1);
                    CarWashGreenLight = S7.GetBitAt(read_buffer_DB5_Output, 2, 2);
                    CarWashRedLight = S7.GetBitAt(read_buffer_DB5_Output, 2, 3);
                    CarWashYellowLight = S7.GetBitAt(read_buffer_DB5_Output, 2, 4);
                    CarWashDoor1UP = S7.GetBitAt(read_buffer_DB5_Output, 2, 5);
                    CarWashDoor1DOWN = S7.GetBitAt(read_buffer_DB5_Output, 2, 6);
                    CarWashDoor2UP = S7.GetBitAt(read_buffer_DB5_Output, 2, 7);
                    CarWashDoor2DOWN = S7.GetBitAt(read_buffer_DB5_Output, 3, 0);
                    CarWashWater = S7.GetBitAt(read_buffer_DB5_Output, 3, 1);
                    CarWashWashingChemicalsFRONT = S7.GetBitAt(read_buffer_DB5_Output, 3, 2);
                    CarWashWashingChemicalsSIDES = S7.GetBitAt(read_buffer_DB5_Output, 3, 3);
                    CarWashWashingChemicalsBACK = S7.GetBitAt(read_buffer_DB5_Output, 3, 4);
                    CarWashWax = S7.GetBitAt(read_buffer_DB5_Output, 3, 5);
                    CarWashVarnishProtection = S7.GetBitAt(read_buffer_DB5_Output, 3, 6);
                    CarWashDry = S7.GetBitAt(read_buffer_DB5_Output, 3, 7);
                    
                    #endregion
                                      
                    errorMessageBoxShown = false;
                }

                #endregion

                */

                errorMessageBoxShown = false;
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
            //closing form
            this.Close();
        }
        #endregion

        //Choosing washing variant
        #region Choosing washing variant
        private void btnPerfectPolish_Click(object sender, EventArgs e)
        {
            CarWashPerfectPolish = true;
            S7.SetBitAt(send_buffer_DB5, 0, 6, CarWashPerfectPolish);

            //write to PLC
            int writeResultDB5_Input = client.DBWrite(DBNumber_DB5, 0, send_buffer_DB5.Length, send_buffer_DB5);
            if (writeResultDB5_Input != 0)
            {
                //write error
                if (!errorMessageBoxShown)
                {
                    //MessageBox
                    MessageBox.Show("BE doesn't work properly. Data could´t be written to DB11!!! \n\n" +
                        $"Error message: {writeResultDB5_Input} \n", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                    errorMessageBoxShown = true;
                }
            }
            else
            {
                //write was successful
                //closing form
                this.Close();
            }
        }

        private void btnPerfectWash_Click(object sender, EventArgs e)
        {
            CarWashPerfetWash = true;
            S7.SetBitAt(send_buffer_DB5, 0, 5, CarWashPerfetWash);

            //write to PLC
            int writeResultDB5_Input = client.DBWrite(DBNumber_DB5, 0, send_buffer_DB5.Length, send_buffer_DB5);
            if (writeResultDB5_Input != 0)
            {
                //write error
                if (!errorMessageBoxShown)
                {
                    //MessageBox
                    MessageBox.Show("BE doesn't work properly. Data could´t be written to DB11!!! \n\n" +
                        $"Error message: {writeResultDB5_Input} \n", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                    errorMessageBoxShown = true;
                }
            }
            else
            {
                //write was successful
                //closing form
                this.Close();
            }
        }

        #endregion
    }
}

