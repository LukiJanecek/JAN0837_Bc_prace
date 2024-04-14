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
using Bc_prace.Controls;
using Bc_prace.Settings;
using Sharp7;
using Microsoft.VisualBasic.ApplicationServices;
using System.Reflection.Metadata;
using static System.Windows.Forms.Design.AxImporter;
using System.Security.Cryptography;
using System.Runtime.CompilerServices;

namespace Bc_prace
{
    public partial class CarWashForm : Form
    {
        private ChooseOptionForm chooseOptionFormInstance;

        public S7Client client;

        //MessageBox control
        private bool errorMessageBoxShown = false;

        private int carCurrentPoint = 1;

        //Buffers variables 
        #region Buffers variables

        //DB11 => Maintain_DB -> 1 struct -> 3 variables -> size 0.2
        private int DBNumber_DB11 = 11;
        byte[] read_buffer_DB11;
        byte[] send_buffer_DB11;

        //DB5 => CarWash_DB -> 2 structs -> 23 variables -> size 3.7
        private int DBNumber_DB5 = 5;
        byte[] read_buffer_DB5;
        public byte[] previous_buffer_DB5;
        public byte[] PreviousBufferHash_DB5;
        byte[] send_buffer_DB5;

        #endregion

        //MaintainDB variables
        public bool Option2 = false;

        //Input variables
        #region Input variables

        public bool CarWashEmergencySTOP;
        public bool CarWashErrorSystem;
        public bool CarWashStartCarWash;
        public bool CarWashWaitingForIncomingCar;
        public bool CarWashWaitingForOutgoingCar;
        public bool CarWashPerfectWash;
        public bool CarWashPerfectPolish;

        #endregion

        //Output variables 
        #region Output variables 

        public bool CarWashPositionShower;
        public bool CarWashPositionCar;
        public bool CarWashGreenLight;
        public bool CarWashRedLight;
        public bool CarWashYellowLight;
        public bool CarWashDoor1UP;
        public bool CarWashDoor1DOWN;
        public bool CarWashDoor2UP;
        public bool CarWashDoor2DOWN;
        public bool CarWashWater;
        public bool CarWashWashingChemicalsFRONT;
        public bool CarWashWashingChemicalsSIDES;
        public bool CarWashWashingChemicalsBACK;
        public bool CarWashWax;
        public bool CarWashVarnishProtection;
        public bool CarWashDry;
        public bool CarWashSoap;
        public bool CarWashActiveFoam;
        public bool CarWashBrushes;
        public bool CarWashPrewash;
        public int CarWashTimeDoorMovement; //time

        #endregion

        //MEM variables 
        #region MEM variables 

        public bool CarWashMEMDoor;
        public bool CarWashMEMDoorTrig;
        public bool CarWashMEMDoorCloseTrig;

        #endregion

        public CarWashForm(ChooseOptionForm chooseOptionFormInstance)
        {
            InitializeComponent();
            InitializeButton(); // there is probably no need for this function
            btnSignalization.Enabled = false;
            this.MinimumSize = new Size(1530, 500);
            userControlCarWash1.OnCarWashPositionCar += UserControlCarWash1_OnCarWashPositionCar;

            this.chooseOptionFormInstance = chooseOptionFormInstance;

            client = chooseOptionFormInstance.client;

            //Buffers initialize
            #region Buffers initialize

            //DB11 => Maintain_DB
            read_buffer_DB11 = chooseOptionFormInstance.read_buffer_DB11;
            send_buffer_DB11 = chooseOptionFormInstance.send_buffer_DB11;
            //DB5 => CarWash_DB
            read_buffer_DB5 = chooseOptionFormInstance.read_buffer_DB5;
            send_buffer_DB5 = chooseOptionFormInstance.send_buffer_DB5;

            #endregion

            if (client.Connected)
            {
                //start timer
                Timer_read_actual.Start();
                //set time interval (ms)
                Timer_read_actual.Interval = 100;
            }

            CarWashWaitingForIncomingCar = true;
            S7.SetBitAt(send_buffer_DB5, 0, 3, CarWashWaitingForIncomingCar);

            //write to PLC
            int writeResultDB5_CarWashWaitingForIncomingCar = client.DBWrite(DBNumber_DB5, 0, send_buffer_DB5.Length, send_buffer_DB5);
            if (writeResultDB5_CarWashWaitingForIncomingCar != 0)
            {
                //write error
                if (!errorMessageBoxShown)
                {
                    errorMessageBoxShown = true;

                    //MessageBox
                    MessageBox.Show("BE doesn't work properly. Data could´t be written to DB5!!! \n\n" +
                        $"Error message: writeResultDB5_CarWashWaitingForIncomingCar = {writeResultDB5_CarWashWaitingForIncomingCar} \n", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
            else
            {
                //write was successful
                statusStripCarWash.Items.Clear();
                ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Waiting for incoming car. Press button: Move car to next point");
                statusStripCarWash.Items.Add(lblStatus);
            }

        }

        private void UserControlCarWash1_OnCarWashPositionCar(bool state)
        {
            if (state)
            {
                statusStripCarWash.Items.Clear();
                ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Car is on position.");
                statusStripCarWash.Items.Add(lblStatus);

                CarWashPositionCar = true;
                S7.SetBitAt(send_buffer_DB5, 2, 1, CarWashPositionCar);

                //write to PLC
                int writeResultDB5_PositionCar = client.DBWrite(DBNumber_DB5, 0, send_buffer_DB5.Length, send_buffer_DB5);
                if (writeResultDB5_PositionCar != 0)
                {
                    //write error
                    if (!errorMessageBoxShown)
                    {
                        errorMessageBoxShown = true;

                        //MessageBox
                        MessageBox.Show("BE doesn't work properly. Data could´t be written to DB5!!! \n\n" +
                            $"Error message: writeResultDB5_PositionCar = {writeResultDB5_PositionCar} \n", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }
                else
                {
                    //write was successful
                    /*
                    statusStripCarWash.Items.Clear();
                    ToolStripStatusLabel lblStatus = new ToolStripStatusLabel(" Press this button to continue: Move car to next point");
                    statusStripCarWash.Items.Add(lblStatus);
                    */
                }
            }
            else
            {
                CarWashPositionCar = false;
                S7.SetBitAt(send_buffer_DB5, 2, 1, CarWashPositionCar);

                //write to PLC
                int writeResultDB5_PositionCar = client.DBWrite(DBNumber_DB5, 0, send_buffer_DB5.Length, send_buffer_DB5);
                if (writeResultDB5_PositionCar != 0)
                {
                    //write error
                    if (!errorMessageBoxShown)
                    {
                        errorMessageBoxShown = true;

                        //MessageBox
                        MessageBox.Show("BE doesn't work properly. Data could´t be written to DB5!!! \n\n" +
                            $"Error message: writeResultDB5_PositionCar = {writeResultDB5_PositionCar} \n", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }
                else
                {
                    //write was successful
                    /*
                    statusStripCarWash.Items.Clear();
                    ToolStripStatusLabel lblStatus = new ToolStripStatusLabel(" Press this button to continue: Move car to next point");
                    statusStripCarWash.Items.Add(lblStatus);
                    */
                }
            }
        }

        //Reading variables + actions on variable value
        #region Reading variables + actions on variable value

        private async void Timer_read_actual_Tick(object sender, EventArgs e)
        {
            try
            {
                Option2 = chooseOptionFormInstance.Option2;

                //Input variables 
                #region Input variables 

                CarWashEmergencySTOP = chooseOptionFormInstance.CarWashEmergencySTOP;
                CarWashErrorSystem = chooseOptionFormInstance.CarWashErrorSystem;
                CarWashStartCarWash = chooseOptionFormInstance.CarWashStartCarWash; //no need to make a condition if true
                CarWashWaitingForIncomingCar = chooseOptionFormInstance.CarWashWaitingForIncomingCar; //? 
                CarWashWaitingForOutgoingCar = chooseOptionFormInstance.CarWashWaitingForOutgoingCar; //?
                CarWashPerfectWash = chooseOptionFormInstance.CarWashPerfetWash; //no need to make a condition if true
                CarWashPerfectPolish = chooseOptionFormInstance.CarWashPerfectPolish; //no need to make a condition if true

                #endregion

                //Output variables
                #region Output variables 

                CarWashPositionShower = chooseOptionFormInstance.CarWashPositionShower; //?
                CarWashPositionCar = chooseOptionFormInstance.CarWashPositionCar; //?
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
                CarWashPrewash = chooseOptionFormInstance.CarWashPreWash;
                CarWashTimeDoorMovement = chooseOptionFormInstance.CarWashTimeDoorMovement;

                #endregion

                //MEM variables 
                #region 

                CarWashMEMDoor = chooseOptionFormInstance.CarWashMEMDoor;
                CarWashMEMDoorTrig = chooseOptionFormInstance.CarWashMEMDoorTrig;
                CarWashMEMDoorCloseTrig = chooseOptionFormInstance.CarWashMEMDoorCloseTrig;

                #endregion

                //Reading variables with MultiVar method
                /*
                #region Multi read -> MultiVar

                S7MultiVar reader = new S7MultiVar(client);

                //DB5 => CarWash_DB -> 2 structs -> 23 variables -> size 3.7
                if (previous_buffer_DB5_Input == null)
                {
                    previous_buffer_DB5_Input = new byte[read_buffer_DB5_Input.Length];
                    Array.Copy(read_buffer_DB5_Input, previous_buffer_DB5_Input, read_buffer_DB5_Input.Length);

                    // Inicializace hashe při prvním spuštění
                    PreviousBufferHash_DB5_Input = ComputeHash(read_buffer_DB5_Input);
                }

                //first struct -> Input -> 7 variables -> 0.6 size 
                reader.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, DBNumber_DB5, 0, 0, ref read_buffer_DB5_Input); //read_buffer_DB5_Input.Length
                //second struct -> Output -> 16 variables -> 3.7 size
                reader.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, DBNumber_DB5, 0, 0, ref read_buffer_DB5_Output); //read_buffer_DB5_Output.Length

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
                    statusStripCarWash.Items.Clear();
                    ToolStripStatusLabel lblStatus1 = new ToolStripStatusLabel("Variables were not read.");
                    statusStripCarWash.Items.Add(lblStatus1);

                    if (!errorMessageBoxShown)
                    {
                        errorMessageBoxShown = true;

                        //MessageBox
                        MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data weren't read from DB5!!! \n\n" +
                            $"Error message {readResultDB5} \n", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }

                #endregion
                */

                //Reading variables with DBRead method
                /*
                #region DBRead

                //DB5 => CarWash_DB -> 2 structs -> 23 variables -> size 3.7
                int readResult = client.DBRead(DBNumber_DB5, 0, read_buffer_DB5.Length, read_buffer_DB5);
                if (readResult != 0)
                {
                    //error
                    statusStripCarWash.Items.Clear();
                    ToolStripStatusLabel lblStatus1 = new ToolStripStatusLabel("Variables were not read.");
                    statusStripCarWash.Items.Add(lblStatus1);

                    if (!errorMessageBoxShown)
                    {
                        errorMessageBoxShown = true;

                        //MessageBox
                        MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data weren't read from DB5!!! \n\n" +
                            $"Error message {readResultDB5} \n", "Error",
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

                //Action on variable value
                #region Action on variable value

                if (CarWashEmergencySTOP)
                {
                    statusStripCarWash.Items.Clear();
                    ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Emergency mode activated");
                    statusStripCarWash.Items.Add(lblStatus);

                    //write emergency status 
                    if (!errorMessageBoxShown)
                    {
                        errorMessageBoxShown = true;

                        //MessageBox
                        MessageBox.Show("Emergency mode activated. \r\n \n\n", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }

                if (CarWashErrorSystem)
                {
                    statusStripCarWash.Items.Clear();
                    ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Error system");
                    statusStripCarWash.Items.Add(lblStatus);

                    //write error
                    if (!errorMessageBoxShown)
                    {
                        errorMessageBoxShown = true;

                        //MessageBox
                        MessageBox.Show("Error system is true. There is an error in the process. \r\n \n\n", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1); ;
                    }
                }

                if (CarWashWaitingForIncomingCar)
                {
                    statusStripCarWash.Items.Clear();
                    ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Waiting for incoming car.");
                    statusStripCarWash.Items.Add(lblStatus);
                }

                if (CarWashWaitingForOutgoingCar)
                {
                    statusStripCarWash.Items.Clear();
                    ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Waiting for outgoing car.");
                    statusStripCarWash.Items.Add(lblStatus);
                }

                //Red light
                if (CarWashRedLight)
                {
                    userControlCarWash1.CarWashRedLight = true;
                    userControlCarWash1.CarWashYellowLight = false;
                    userControlCarWash1.CarWashGreenLight = false;

                    btnSignalization.BackColor = System.Drawing.Color.Red;
                    btnSignalization.Text = "Stop";
                }

                //Yellow light
                if (CarWashYellowLight)
                {
                    userControlCarWash1.CarWashRedLight = false;
                    userControlCarWash1.CarWashYellowLight = true;
                    userControlCarWash1.CarWashGreenLight = false;

                    btnSignalization.BackColor = System.Drawing.Color.Yellow;
                    btnSignalization.Text = "Error";
                }

                //Green light
                if (CarWashGreenLight)
                {
                    userControlCarWash1.CarWashRedLight = false;
                    userControlCarWash1.CarWashYellowLight = false;
                    userControlCarWash1.CarWashGreenLight = true;

                    btnSignalization.BackColor = System.Drawing.Color.Green;
                    btnSignalization.Text = "Go";
                }

                //Door1 UP
                if (CarWashDoor1UP)
                {
                    statusStripCarWash.Items.Clear();
                    ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Door1UP");
                    statusStripCarWash.Items.Add(lblStatus);

                    //true
                    userControlCarWash1.door1UP(CarWashTimeDoorMovement);

                    //toto možná nebude dělat dobrotu
                    S7.SetBitAt(send_buffer_DB5, 2, 5, true);

                    //write to PLC
                    int writeResultDB5_Door1UP = client.DBWrite(DBNumber_DB5, 0, send_buffer_DB5.Length, send_buffer_DB5);
                    if (writeResultDB5_Door1UP != 0)
                    {
                        //write error
                        if (!errorMessageBoxShown)
                        {
                            errorMessageBoxShown = true;

                            //MessageBox
                            MessageBox.Show("BE doesn't work properly. Data could´t be written to DB5!!! \n\n" +
                                $"Error message: writeResultDB5_Door1UP = {writeResultDB5_Door1UP} \n", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                        }
                    }
                    else
                    {
                        //write was successful
                    }
                }
                else
                {
                    //false
                }

                //Door1 DOWN
                if (CarWashDoor1DOWN)
                {
                    statusStripCarWash.Items.Clear();
                    ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Door1DOWN");
                    statusStripCarWash.Items.Add(lblStatus);

                    //true
                    userControlCarWash1.door1DOWN(CarWashTimeDoorMovement);

                    //toto možná nebude dělat dobrotu
                    S7.SetBitAt(send_buffer_DB5, 2, 6, true);

                    //write to PLC
                    int writeResultDB5_Door1DOWN = client.DBWrite(DBNumber_DB5, 0, send_buffer_DB5.Length, send_buffer_DB5);
                    if (writeResultDB5_Door1DOWN != 0)
                    {
                        //write error
                        if (!errorMessageBoxShown)
                        {
                            errorMessageBoxShown = true;

                            //MessageBox
                            MessageBox.Show("BE doesn't work properly. Data could´t be written to DB5!!! \n\n" +
                                $"Error message: writeResultDB5_Door1DOWN = {writeResultDB5_Door1DOWN} \n", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                        }
                    }
                    else
                    {
                        //write was successful
                    }
                }
                else
                {
                    //false
                }

                //Door2 UP
                if (CarWashDoor2UP)
                {
                    statusStripCarWash.Items.Clear();
                    ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Door2UP");
                    statusStripCarWash.Items.Add(lblStatus);

                    //true 
                    userControlCarWash1.door2UP(CarWashTimeDoorMovement);

                    //toto možná nebude dělat dobrotu
                    S7.SetBitAt(send_buffer_DB5, 2, 7, true);

                    //write to PLC
                    int writeResultDB5_Door2UP = client.DBWrite(DBNumber_DB5, 0, send_buffer_DB5.Length, send_buffer_DB5);
                    if (writeResultDB5_Door2UP != 0)
                    {
                        //write error
                        if (!errorMessageBoxShown)
                        {
                            errorMessageBoxShown = true;

                            //MessageBox
                            MessageBox.Show("BE doesn't work properly. Data could´t be written to DB5!!! \n\n" +
                                $"Error message: writeResultDB5_Door2UP = {writeResultDB5_Door2UP} \n", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                        }
                    }
                    else
                    {
                        //write was successful
                    }
                }
                else
                {
                    //false
                }

                //door2 DOWN
                if (CarWashDoor2DOWN)
                {
                    statusStripCarWash.Items.Clear();
                    ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Door2DOWN");
                    statusStripCarWash.Items.Add(lblStatus);

                    //true
                    userControlCarWash1.door2DOWN(CarWashTimeDoorMovement);

                    //toto možná nebude dělat dobrotu
                    S7.SetBitAt(send_buffer_DB5, 3, 0, true);

                    //write to PLC
                    int writeResultDB5_Door2DOWN = client.DBWrite(DBNumber_DB5, 0, send_buffer_DB5.Length, send_buffer_DB5);
                    if (writeResultDB5_Door2DOWN != 0)
                    {
                        //write error
                        if (!errorMessageBoxShown)
                        {
                            errorMessageBoxShown = true;

                            //MessageBox
                            MessageBox.Show("BE doesn't work properly. Data could´t be written to DB5!!! \n\n" +
                                $"Error message: writeResultDB5_Door2DOWN = {writeResultDB5_Door2DOWN} \n", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                        }
                    }
                    else
                    {
                        //write was successful
                    }
                }
                else
                {
                    //false
                }

                //Water
                if (CarWashWater)
                {
                    statusStripCarWash.Items.Clear();
                    ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Water ON");
                    statusStripCarWash.Items.Add(lblStatus);

                    //userControlCarWash1.WaterSignalization(true);
                    userControlCarWash1.Water = true;
                    userControlCarWash1.InitializeCarImage(3);
                    await ShowerMovemenet();
                }
                else
                {
                    statusStripCarWash.Items.Clear();
                    ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Water OFF");
                    statusStripCarWash.Items.Add(lblStatus);

                    //userControlCarWash1.WaterSignalization(false);
                    userControlCarWash1.Water = false;
                }

                //Wax
                if (CarWashWax)
                {
                    statusStripCarWash.Items.Clear();
                    ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Wax ON");
                    statusStripCarWash.Items.Add(lblStatus);

                    //userControlCarWash1.WaxSignalization(true);
                    userControlCarWash1.Wax = true;
                    userControlCarWash1.InitializeCarImage(3);
                    await ShowerMovemenet();
                }
                else
                {
                    statusStripCarWash.Items.Clear();
                    ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Wax OFF");
                    statusStripCarWash.Items.Add(lblStatus);

                    //userControlCarWash1.WaxSignalization(false);
                    userControlCarWash1.Wax = false;
                }

                //ActiveFoam
                if (CarWashActiveFoam)
                {
                    statusStripCarWash.Items.Clear();
                    ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("ActiveFoam ON");
                    statusStripCarWash.Items.Add(lblStatus);

                    //userControlCarWash1.ActiveFoamSignalization(true);
                    userControlCarWash1.ActiveFoam = true;
                    userControlCarWash1.InitializeCarImage(3);
                    await ShowerMovemenet();
                }
                else
                {
                    statusStripCarWash.Items.Clear();
                    ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("ActiveFoam OFF");
                    statusStripCarWash.Items.Add(lblStatus);

                    //userControlCarWash1.ActiveFoamSignalization(true);
                    userControlCarWash1.ActiveFoam = false;
                }

                //Soap 
                if (CarWashSoap)
                {
                    statusStripCarWash.Items.Clear();
                    ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Soap ON");
                    statusStripCarWash.Items.Add(lblStatus);

                    //userControlCarWash1.SoapSignalization(true);
                    userControlCarWash1.Soap = true;
                    userControlCarWash1.InitializeCarImage(3);
                    await ShowerMovemenet();
                }
                else
                {
                    statusStripCarWash.Items.Clear();
                    ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Soap OFF");
                    statusStripCarWash.Items.Add(lblStatus);

                    //userControlCarWash1.SoapSignalization(false);
                    userControlCarWash1.Soap = false;
                }

                //Brushes
                if (CarWashBrushes)
                {
                    statusStripCarWash.Items.Clear();
                    ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Brushes ON");
                    statusStripCarWash.Items.Add(lblStatus);

                    //userControlCarWash1.BrushesSignalization(true);
                    userControlCarWash1.Brushes = true;
                    userControlCarWash1.InitializeCarImage(2);
                    await ShowerMovemenet();
                }
                else
                {
                    statusStripCarWash.Items.Clear();
                    ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Brushes OFF");
                    statusStripCarWash.Items.Add(lblStatus);

                    //userControlCarWash1.BrushesSignalization(false);
                    userControlCarWash1.Brushes = false;
                }

                //Drying
                if (CarWashDry)
                {
                    statusStripCarWash.Items.Clear();
                    ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Drying ON");
                    statusStripCarWash.Items.Add(lblStatus);

                    //userControlCarWash1.DryingSignalization(true);
                    userControlCarWash1.InitializeCarImage(4);
                    userControlCarWash1.Drying = true;
                    await ShowerMovemenet();
                }
                else
                {
                    statusStripCarWash.Items.Clear();
                    ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Drying OFF");
                    statusStripCarWash.Items.Add(lblStatus);

                    //userControlCarWash1.DryingSignalization(false);
                    userControlCarWash1.Drying = false;
                }

                //PreWash
                if (CarWashPrewash)
                {
                    statusStripCarWash.Items.Clear();
                    ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("PreWash ON");
                    statusStripCarWash.Items.Add(lblStatus);

                    //userControlCarWash1.PreWashSignalization(true);
                    userControlCarWash1.PreWash = true;
                }
                else
                {
                    statusStripCarWash.Items.Clear();
                    ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("PreWash OFF");
                    statusStripCarWash.Items.Add(lblStatus);

                    //userControlCarWash1.PreWashSignalization(false);
                    userControlCarWash1.PreWash = false;
                }

                //Varnish protectin
                if (CarWashVarnishProtection)
                {
                    statusStripCarWash.Items.Clear();
                    ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Varnich protection ON");
                    statusStripCarWash.Items.Add(lblStatus);

                    userControlCarWash1.VarnishProtection = true;
                    await ShowerMovemenet();
                }
                else
                {
                    statusStripCarWash.Items.Clear();
                    ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Varnish protection OFF");
                    statusStripCarWash.Items.Add(lblStatus);

                    userControlCarWash1.VarnishProtection = false;
                }

                #endregion

                errorMessageBoxShown = false;
            }
            catch (Exception ex)
            {
                ErrorSystem();

                if (!errorMessageBoxShown)
                {
                    errorMessageBoxShown = true;

                    MessageBox.Show($"Error: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
        }

        #endregion

        //Start CarWash
        #region Start CarWash
        private void btnStartCarWash_Click(object sender, EventArgs e)
        {
            CarWashStartCarWash = true;
            S7.SetBitAt(send_buffer_DB5, 0, 2, CarWashStartCarWash);

            //write to PLC
            int writeResultDB5_Input = client.DBWrite(DBNumber_DB5, 0, send_buffer_DB5.Length, send_buffer_DB5);
            if (writeResultDB5_Input != 0)
            {
                //write error
                if (!errorMessageBoxShown)
                {
                    errorMessageBoxShown = true;

                    //MessageBox
                    MessageBox.Show("BE doesn't work properly. Data could´t be written to DB5!!! \n\n" +
                        $"Error message: {writeResultDB5_Input} \n", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
            else
            {
                //write was successful
                //Selection form activated
                CarWashSelectionForm Selection = new CarWashSelectionForm(chooseOptionFormInstance);
                Selection.ShowDialog(this);

                statusStripCarWash.Items.Clear();
                ToolStripStatusLabel lblStatus = new ToolStripStatusLabel(" Press this button to continue: Move car to next point");
                statusStripCarWash.Items.Add(lblStatus);
            }
        }
        #endregion

        //MoveCarToNextPoint
        #region MoveCarToNextPoint

        private void btnMoveCarToNextPoint_Click(object sender, EventArgs e)
        {
            ToolStripStatusLabel lblStatus;

            userControlCarWash1.MoveCarToNextPoint(carCurrentPoint);

            switch (carCurrentPoint)
            {
                case 1:

                    statusStripCarWash.Items.Clear();
                    lblStatus = new ToolStripStatusLabel("Start CarWash by click on button Start washing.");
                    statusStripCarWash.Items.Add(lblStatus);

                    break;
                case 2:

                    //zavolat PreWash
                    statusStripCarWash.Items.Clear();
                    lblStatus = new ToolStripStatusLabel("PreWash is free to use. When are you ready press button to continue.");
                    statusStripCarWash.Items.Add(lblStatus);

                    break;

                case 3:

                    //set true CarPosition
                    statusStripCarWash.Items.Clear();
                    lblStatus = new ToolStripStatusLabel("Car is on CarPosition point.");
                    statusStripCarWash.Items.Add(lblStatus);

                    break;

                case 4:

                    statusStripCarWash.Items.Clear();
                    lblStatus = new ToolStripStatusLabel("Car is leaving CarWash. This is end of the program.");
                    statusStripCarWash.Items.Add(lblStatus);

                    break;

                case 5:

                    //reset obrázku na pozici 1 + waiting oc incoming car = true

                    break;
            }

            carCurrentPoint++;
        }

        #endregion

        //Emergency + system error 
        #region Emergency + system error 
        private void btnEmergency_Click(object sender, EventArgs e)
        {
            CarWashEmergencySTOP = true;
            S7.SetBitAt(send_buffer_DB5, 0, 0, CarWashEmergencySTOP);

            //write to PLC
            int writeResultDB5_Emergency = client.DBWrite(DBNumber_DB5, 0, send_buffer_DB5.Length, send_buffer_DB5);
            if (writeResultDB5_Emergency != 0)
            {
                //write error
                if (!errorMessageBoxShown)
                {
                    errorMessageBoxShown = true;

                    //MessageBox
                    MessageBox.Show("BE doesn't work properly. Data could´t be written to DB5!!! \n\n" +
                        $"Error message: writeResultDB5_Emergency = {writeResultDB5_Emergency} \n", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
            else
            {
                //write was successful
            }
        }

        private void ErrorSystem()
        {
            CarWashErrorSystem = true;
            S7.SetBitAt(send_buffer_DB5, 0, 1, CarWashErrorSystem);

            //write to PLC
            int writeResultDB5_ErrorSystem = client.DBWrite(DBNumber_DB5, 0, send_buffer_DB5.Length, send_buffer_DB5);
            if (writeResultDB5_ErrorSystem != 0)
            {
                //write error
                if (!errorMessageBoxShown)
                {
                    errorMessageBoxShown = true;

                    //MessageBox
                    MessageBox.Show("BE doesn't work properly. Data could´t be written to DB5!!! \n\n" +
                        $"Error message: writeResultDB5_ErrorSystem = {writeResultDB5_ErrorSystem} \n", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
            else
            {
                //write was successful
            }
        }

        #endregion

        //Shower movement
        #region Shower movemnt 

        public async Task ShowerMovemenet()
        {
            await userControlCarWash1.ShowerMovement(705, 915, 2000);
            await userControlCarWash1.ShowerMovement(915, 515, 2000);
            await userControlCarWash1.ShowerMovement(515, 705, 2000);
        }

        #endregion

        //BTN End 
        #region Close window 
        private void btnEnd_Click(object sender, EventArgs e)
        {
            //Option2 = false
            Option2 = false;
            S7.SetBitAt(send_buffer_DB11, 0, 1, Option2);

            //write to PLC
            int writeResultDB11_btnEnd = client.DBWrite(DBNumber_DB11, 0, send_buffer_DB11.Length, send_buffer_DB11);
            if (writeResultDB11_btnEnd != 0)
            {
                //write error
                if (!errorMessageBoxShown)
                {
                    errorMessageBoxShown = true;

                    //MessageBox
                    MessageBox.Show("BE doesn't work properly. Data could´t be written to DB11!!! \n\n" +
                        $"Error message: writeResultDB11_btnEnd = {writeResultDB11_btnEnd} \n", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
            else
            {
                //write was successful
                this.Close();
            }
        }
        #endregion

        //Car signalization
        #region Car signalization

        private void InitializeButton()
        {
            //setting default background color and text 
            btnSignalization.BackColor = System.Drawing.Color.Green;
            btnSignalization.Text = "Go";

            statusStripCarWash.Items.Clear();
            ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Car signalization: GO!");
            statusStripCarWash.Items.Add(lblStatus);
        }

        #endregion

        //Manual car movement
        #region Manual Car movement
        private void btnCarMoveLEFT_Click(object sender, EventArgs e)
        {
            userControlCarWash1.ManualMovePictureLEFT();
        }

        private void btnCarMoveRIGHT_Click(object sender, EventArgs e)
        {
            userControlCarWash1.ManualMovePictureRIGHT();
        }

        #endregion

        private void btnSignalization_Click(object sender, EventArgs e)
        {

        }

        private async void btnTest1_Click(object sender, EventArgs e)
        {
            userControlCarWash1.VarnishProtection = true;
            await ShowerMovemenet();
        }

        private async void btnTest2_Click_1(object sender, EventArgs e)
        {
            userControlCarWash1.ActiveFoam = true;
            userControlCarWash1.InitializeCarImage(3);
            await ShowerMovemenet();
        }
    }
}
