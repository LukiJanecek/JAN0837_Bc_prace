using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bc_prace.Controls.MyGraphControl.Entities;
using Bc_prace.Settings;
//using static System.Windows.Forms.Design.AxImporter;
using Sharp7;
using System.Security.Cryptography;

namespace Bc_prace
{
    public partial class Program1Form : Form
    {
        private ChooseOptionForm chooseOptionFormInstance;

        public S7Client client;

        //MessageBox control
        private bool errorMessageBoxShown = false;

        //MaintainDB variables
        bool Option1 = false;

        //Buffers variables 
        #region Buffers variables

        //DB11 => Maintain_DB -> 1 struct -> 3 variables -> size 0.2
        public int DBNumber_DB11 = 11;
        byte[] read_buffer_DB11;
        public byte[] PreviousBufferHash_DB11;
        byte[] send_buffer_DB11;

        //DB4 => Elevator_DB -> 2 structs -> 46 variables -> size 26
        public int DBNumber_DB4 = 4;
        public byte[] read_buffer_DB4;
        public byte[] previous_buffer_DB4;
        public byte[] PreviousBufferHash_DB4;
        public byte[] send_buffer_DB4;

        #endregion

        //Input variables
        #region Input variables 

        public bool ElevatorBTNCabin1;
        public bool ElevatorBTNCabin2;
        public bool ElevatorBTNCabin3;
        public bool ElevatorBTNCabin4;
        public bool ElevatorBTNCabin5;
        public bool ElevatorBTNFloor1;
        public bool ElevatorBTNFloor2;
        public bool ElevatorBTNFloor3;
        public bool ElevatorBTNFloor4;
        public bool ElevatorBTNFloor5;
        public bool ElevatorDoorSEQ;
        public bool ElevatorBTNOPENCLOSE;
        public bool ElevatorEmergencySTOP;
        public bool ElevatorErrorSystem;

        #endregion

        //Output variables
        #region Output variables

        public bool ElevatorMotorON;
        public bool ElevatorMotorDOWN;
        public bool ElevatorMotorUP;
        public bool ElevatroHoming;
        public bool ElevatorSystemReady;
        public int ElevatorActualFloor;
        public bool ElevatorMoving;
        public bool ElevatorSystemWorking;
        public int ElevatorGoToFloor;
        public bool ElevatorDirection;
        public bool ElevatorActualFloorLED1;
        public bool ElevatorActualFloorLED2;
        public bool ElevatorActualFloorLED3;
        public bool ElevatorActualFloorLED4;
        public bool ElevatorActualFloorLED5;
        public bool ElevatorActualFloorCabinLED1;
        public bool ElevatorActualFloorCabinLED2;
        public bool ElevatorActualFloorCabinLED3;
        public bool ElevatorActualFloorCabinLED4;
        public bool ElevatorActualFloorCabinLED5;
        public bool ElevatorActualFloorSENS1;
        public bool ElevatorActualFloorSENS2;
        public bool ElevatorActualFloorSENS3;
        public bool ElevatorActualFloorSENS4;
        public bool ElevatorActualFloorSENS5;
        public int ElevatorTimeDoorSQOPEN; //time
        public int ElevatroTimeDoorSQCLOSE; //time
        public bool ElevatorDoorClOSE;
        public bool ElevatorDoorOPEN;
        public int ElevatorCabinSpeed;
        public bool ElevatorInactivity;
        public int ElevatorTimeToGetDown; //time

        #endregion
                
        //Variables cabin movement step 
        public int ElevatorStep = 10;

        //int ElevatorSpeedValue, InactivityTimeValue, TimeDoorOPENValue, TimeDoorCLOSEValue;

        public Program1Form(ChooseOptionForm chooseOptionFormInstance)
        {
            InitializeComponent();
            this.MinimumSize = new Size(1150, 650);
            userControlElevatorCabin1.OnElevatorFloorBTNClick += UserControlElevatorCabin1_OnElevatorFloorBTNClick;

            this.chooseOptionFormInstance = chooseOptionFormInstance;

            client = chooseOptionFormInstance.client;

            //Buffers initialize
            #region Buffers initialize

            //DB11 => Maintain_DB
            read_buffer_DB11 = chooseOptionFormInstance.read_buffer_DB11;
            send_buffer_DB11 = chooseOptionFormInstance.send_buffer_DB11;
            //DB4 => Elevator_DB
            read_buffer_DB4 = chooseOptionFormInstance.read_buffer_DB4;
            send_buffer_DB4 = chooseOptionFormInstance.send_buffer_DB4;

            #endregion

            if (client.Connected)
            {
                //start timer
                Timer_read_actual.Start();
                //set time interval (ms)
                Timer_read_actual.Interval = 100;
            }
        }

        private void Program1_Load(object sender, EventArgs e)
        {
            userControlElevatorCabin1.SetControl(this);
        }

        private void UserControlElevatorCabin1_OnElevatorFloorBTNClick(object sender, string id)
        {
            switch (id)
            {
                case "1":

                    ElevatorBTNFloor1 = true;
                    S7.SetBitAt(send_buffer_DB4, 0, 5, ElevatorBTNFloor1);

                    //write to PLC
                    int writeResultDB4_btnElevatorFloor1 = client.DBWrite(DBNumber_DB4, 0, send_buffer_DB4.Length, send_buffer_DB4);
                    if (writeResultDB4_btnElevatorFloor1 != 0)
                    {
                        //write error
                        if (!errorMessageBoxShown)
                        {
                            //MessageBox
                            MessageBox.Show("BE doesn't work properly. Data could´t be written to DB4!!! \n\n" +
                                $"Error message: writeResultDB4_btnElevatorFloor1 = {writeResultDB4_btnElevatorFloor1} \n", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                            errorMessageBoxShown = true;
                        }
                    }
                    else
                    {
                        //write was successful
                    }

                    break;
                case "2":

                    ElevatorBTNFloor2 = true;
                    S7.SetBitAt(send_buffer_DB4, 0, 6, ElevatorBTNFloor2);

                    //write to PLC
                    int writeResultDB4_btnElevatorFloor2 = client.DBWrite(DBNumber_DB4, 0, send_buffer_DB4.Length, send_buffer_DB4);
                    if (writeResultDB4_btnElevatorFloor2 != 0)
                    {
                        //write error
                        if (!errorMessageBoxShown)
                        {
                            //MessageBox
                            MessageBox.Show("BE doesn't work properly. Data could´t be written to DB4!!! \n\n" +
                                $"Error message: writeResultDB4_btnElevatorFloor2 = {writeResultDB4_btnElevatorFloor2} \n", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                            errorMessageBoxShown = true;
                        }
                    }
                    else
                    {
                        //write was successful
                    }

                    break;
                case "3":

                    ElevatorBTNFloor3 = true;
                    S7.SetBitAt(send_buffer_DB4, 0, 7, ElevatorBTNFloor3);

                    //write to PLC
                    int writeResultDB4_btnElevatorFloor3 = client.DBWrite(DBNumber_DB4, 0, send_buffer_DB4.Length, send_buffer_DB4);
                    if (writeResultDB4_btnElevatorFloor3 != 0)
                    {
                        //write error
                        if (!errorMessageBoxShown)
                        {
                            //MessageBox
                            MessageBox.Show("BE doesn't work properly. Data could´t be written to DB4!!! \n\n" +
                                $"Error message: writeResultDB4_btnElevatorFloor3 = {writeResultDB4_btnElevatorFloor3} \n", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                            errorMessageBoxShown = true;
                        }
                    }
                    else
                    {
                        //write was successful
                    }

                    break;
                case "4":

                    ElevatorBTNFloor4 = true;
                    S7.SetBitAt(send_buffer_DB4, 1, 0, ElevatorBTNFloor4);

                    //write to PLC
                    int writeResultDB4_btnElevatorFloor4 = client.DBWrite(DBNumber_DB4, 0, send_buffer_DB4.Length, send_buffer_DB4);
                    if (writeResultDB4_btnElevatorFloor4 != 0)
                    {
                        //write error
                        if (!errorMessageBoxShown)
                        {
                            //MessageBox
                            MessageBox.Show("BE doesn't work properly. Data could´t be written to DB4!!! \n\n" +
                                $"Error message: writeResultDB4_btnElevatorFloor4 = {writeResultDB4_btnElevatorFloor4} \n", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                            errorMessageBoxShown = true;
                        }
                    }
                    else
                    {
                        //write was successful
                    }

                    break;
                case "5":

                    ElevatorBTNFloor5 = true;
                    S7.SetBitAt(send_buffer_DB4, 1, 1, ElevatorBTNFloor5);

                    //write to PLC
                    int writeResultDB4_btnElevatorFloor5 = client.DBWrite(DBNumber_DB4, 0, send_buffer_DB4.Length, send_buffer_DB4);
                    if (writeResultDB4_btnElevatorFloor5 != 0)
                    {
                        //write error
                        if (!errorMessageBoxShown)
                        {
                            //MessageBox
                            MessageBox.Show("BE doesn't work properly. Data could´t be written to DB5!!! \n\n" +
                                $"Error message: writeResultDB4_btnElevatorFloor5 = {writeResultDB4_btnElevatorFloor5} \n", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                            errorMessageBoxShown = true;
                        }
                    }
                    else
                    {
                        //write was successful
                    }

                    break;
            }
        }

        //Tia connection
        #region Tia connection

        private void Timer_read_actual_Tick(object sender, EventArgs e)
        {
            try
            {
                Option1 = chooseOptionFormInstance.Option1;

                //Input variables
                #region Input variables 

                ElevatorBTNCabin1 = chooseOptionFormInstance.ElevatorBTNCabin1;
                ElevatorBTNCabin2 = chooseOptionFormInstance.ElevatorBTNCabin2;
                ElevatorBTNCabin3 = chooseOptionFormInstance.ElevatorBTNCabin3;
                ElevatorBTNCabin4 = chooseOptionFormInstance.ElevatorBTNCabin4;
                ElevatorBTNCabin5 = chooseOptionFormInstance.ElevatorBTNCabin5;
                ElevatorBTNFloor1 = chooseOptionFormInstance.ElevatorBTNFloor1;
                ElevatorBTNFloor2 = chooseOptionFormInstance.ElevatorBTNFloor2;
                ElevatorBTNFloor3 = chooseOptionFormInstance.ElevatorBTNFloor3;
                ElevatorBTNFloor4 = chooseOptionFormInstance.ElevatorBTNFloor4;
                ElevatorBTNFloor5 = chooseOptionFormInstance.ElevatorBTNFloor5;
                ElevatorDoorSEQ = chooseOptionFormInstance.ElevatorDoorSEQ;
                ElevatorBTNOPENCLOSE = chooseOptionFormInstance.ElevatorBTNOPENCLOSE;
                ElevatorEmergencySTOP = chooseOptionFormInstance.ElevatorEmergencySTOP;
                ElevatorErrorSystem = chooseOptionFormInstance.ElevatorErrorSystem;

                #endregion

                //Output variables
                #region Output variables 

                ElevatorMotorON = chooseOptionFormInstance.ElevatorMotorON; //no need to make a condition if true
                ElevatorMotorDOWN = chooseOptionFormInstance.ElevatorMotorDOWN; //no need to make a condition if true
                ElevatorMotorUP = chooseOptionFormInstance.ElevatorMotorUP; //no need to make a condition if true
                ElevatroHoming = chooseOptionFormInstance.ElevatroHoming; //no need to make a condition if true
                ElevatorSystemReady = chooseOptionFormInstance.ElevatorSystemReady; //no need to make a condition if true
                ElevatorActualFloor = chooseOptionFormInstance.ElevatorActualFloor; //no need to make a condition if true
                ElevatorMoving = chooseOptionFormInstance.ElevatorMoving; //no need to make a condition if true
                ElevatorSystemWorking = chooseOptionFormInstance.ElevatorSystemWorking; //no need to make a condition if true
                ElevatorGoToFloor = chooseOptionFormInstance.ElevatorGoToFloor; //no need to make a condition if true
                ElevatorDirection = chooseOptionFormInstance.ElevatorDirection; //no need to make a condition if true
                ElevatorActualFloorLED1 = chooseOptionFormInstance.ElevatorActualFloorLED1;
                ElevatorActualFloorLED2 = chooseOptionFormInstance.ElevatorActualFloorLED2;
                ElevatorActualFloorLED3 = chooseOptionFormInstance.ElevatorActualFloorLED3;
                ElevatorActualFloorLED4 = chooseOptionFormInstance.ElevatorActualFloorLED4;
                ElevatorActualFloorLED5 = chooseOptionFormInstance.ElevatorActualFloorLED5;
                ElevatorActualFloorCabinLED1 = chooseOptionFormInstance.ElevatorActualFloorCabinLED1;
                ElevatorActualFloorCabinLED2 = chooseOptionFormInstance.ElevatorActualFloorCabinLED2;
                ElevatorActualFloorCabinLED3 = chooseOptionFormInstance.ElevatorActualFloorCabinLED3;
                ElevatorActualFloorCabinLED4 = chooseOptionFormInstance.ElevatorActualFloorCabinLED4;
                ElevatorActualFloorCabinLED5 = chooseOptionFormInstance.ElevatorActualFloorCabinLED5;
                ElevatorActualFloorSENS1 = chooseOptionFormInstance.ElevatorActualFloorSENS1;
                ElevatorActualFloorSENS2 = chooseOptionFormInstance.ElevatorActualFloorSENS2;
                ElevatorActualFloorSENS3 = chooseOptionFormInstance.ElevatorActualFloorSENS3;
                ElevatorActualFloorSENS4 = chooseOptionFormInstance.ElevatorActualFloorSENS4;
                ElevatorActualFloorSENS5 = chooseOptionFormInstance.ElevatorActualFloorSENS5;
                ElevatorTimeDoorSQOPEN = chooseOptionFormInstance.ElevatorTimeDoorSQOPEN; // time
                ElevatroTimeDoorSQCLOSE = chooseOptionFormInstance.ElevatroTimeDoorSQCLOSE; //time
                ElevatorDoorClOSE = chooseOptionFormInstance.ElevatorDoorClOSE;
                ElevatorDoorOPEN = chooseOptionFormInstance.ElevatorDoorOPEN;
                ElevatorCabinSpeed = chooseOptionFormInstance.ElevatorCabinSpeed;
                ElevatorInactivity = chooseOptionFormInstance.ElevatorInactivity; //no need to make a condition if true
                ElevatorTimeToGetDown = chooseOptionFormInstance.ElevatorTimeToGetDown; //time

                #endregion

                //Reading variables with MultiVar method
                /*
                #region Multi read -> MultiVar   

                S7MultiVar reader = new S7MultiVar(client);

                //DB4 => Elevator_DB -> 2 structs -> 46 variables -> size 2
                if (previous_buffer_DB4_Input == null)
                {
                    previous_buffer_DB4_Input = new byte[read_buffer_DB4_Input.Length];
                    Array.Copy(read_buffer_DB4_Input, previous_buffer_DB4_Input, read_buffer_DB4_Input.Length);

                    // Inicializace hashe při prvním spuštění
                    PreviousBufferHash_DB4_Input = ComputeHash(read_buffer_DB4_Input);
                }

                //first struct -> Input -> 14 variables -> size 1.5 
                reader.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, DBNumber_DB4, 0, 0, ref read_buffer_DB4_Input);
                //second struct -> Output -> 32 variables -> size 26
                reader.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, DBNumber_DB4, 2, 32, ref read_buffer_DB4_Output); 

                int readResultDB4 = reader.Read();

                if (readResultDB4 == 0)
                {
                    byte[] currentHashDB4_Input = ComputeHash(read_buffer_DB4_Input);

                    // Porovnání hashe s předchozím hashem
                    if (!ArraysAreEqual(currentHashDB4_Input, PreviousBufferHash_DB4_Input))
                    {
                        // Aktualizace předchozího bufferu a hashe
                        Array.Copy(read_buffer_DB4_Input, previous_buffer_DB4_Input, read_buffer_DB4_Input.Length);
                        PreviousBufferHash_DB4_Input = currentHashDB4_Input;

                        // Aktualizace proměnných na základě nových dat

                        //Input variables
                        #region Input variables

                        ElevatorBTNCabin1 = S7.GetBitAt(read_buffer_DB4_Input, 0, 0);
                        ElevatorBTNCabin2 = S7.GetBitAt(read_buffer_DB4_Input, 0, 1);
                        ElevatorBTNCabin3 = S7.GetBitAt(read_buffer_DB4_Input, 0, 2);
                        ElevatorBTNCabin4 = S7.GetBitAt(read_buffer_DB4_Input, 0, 3);
                        ElevatorBTNCabin5 = S7.GetBitAt(read_buffer_DB4_Input, 0, 4);
                        ElevatorBTNFloor1 = S7.GetBitAt(read_buffer_DB4_Input, 0, 5);
                        ElevatorBTNFloor2 = S7.GetBitAt(read_buffer_DB4_Input, 0, 6);
                        ElevatorBTNFloor3 = S7.GetBitAt(read_buffer_DB4_Input, 0, 7);
                        ElevatorBTNFloor4 = S7.GetBitAt(read_buffer_DB4_Input, 1, 0);
                        ElevatorBTNFloor5 = S7.GetBitAt(read_buffer_DB4_Input, 1, 1);
                        ElevatorDoorSEQ = S7.GetBitAt(read_buffer_DB4_Input, 1, 2);
                        ElevatorBTNOPENCLOSE = S7.GetBitAt(read_buffer_DB4_Input, 1, 3);
                        ElevatorEmergencySTOP = S7.GetBitAt(read_buffer_DB4_Input, 1, 4);
                        ElevatorErrorSystem = S7.GetBitAt(read_buffer_DB4_Input, 1, 5);

                        #endregion

                        //Output variables
                        #region Output variables

                        ElevatorMotorON = S7.GetBitAt(read_buffer_DB4_Input, 2, 0); ;
                        ElevatorMotorDOWN = S7.GetBitAt(read_buffer_DB4_Input, 2, 1);
                        ElevatorMotorUP = S7.GetBitAt(read_buffer_DB4_Input, 2, 2);
                        ElevatroHoming = S7.GetBitAt(read_buffer_DB4_Input, 2, 3);
                        ElevatorSystemReady = S7.GetBitAt(read_buffer_DB4_Input, 2, 4);
                        ElevatorActualFloor = S7.GetIntAt(read_buffer_DB4_Input, 4);
                        ElevatorMoving = S7.GetBitAt(read_buffer_DB4_Input, 6, 0);
                        ElevatorSystemWorking = S7.GetBitAt(read_buffer_DB4_Input, 6, 1);
                        ElevatorGoToFloor = S7.GetIntAt(read_buffer_DB4_Input, 8);
                        ElevatorDirection = S7.GetBitAt(read_buffer_DB4_Input, 10, 0);
                        ElevatorActualFloorLED1 = S7.GetBitAt(read_buffer_DB4_Input, 10, 1);
                        ElevatorActualFloorLED2 = S7.GetBitAt(read_buffer_DB4_Input, 10, 2);
                        ElevatorActualFloorLED3 = S7.GetBitAt(read_buffer_DB4_Input, 10, 3);
                        ElevatorActualFloorLED4 = S7.GetBitAt(read_buffer_DB4_Input, 10, 4);
                        ElevatorActualFloorLED5 = S7.GetBitAt(read_buffer_DB4_Input, 10, 5);
                        ElevatorActualFloorCabinLED1 = S7.GetBitAt(read_buffer_DB4_Input, 10, 6);
                        ElevatorActualFloorCabinLED2 = S7.GetBitAt(read_buffer_DB4_Input, 10, 7);
                        ElevatorActualFloorCabinLED3 = S7.GetBitAt(read_buffer_DB4_Input, 11, 0);
                        ElevatorActualFloorCabinLED4 = S7.GetBitAt(read_buffer_DB4_Input, 11, 1);
                        ElevatorActualFloorCabinLED5 = S7.GetBitAt(read_buffer_DB4_Input, 11, 2);
                        ElevatorActualFloorSENS1 = S7.GetBitAt(read_buffer_DB4_Input, 11, 3);
                        ElevatorActualFloorSENS2 = S7.GetBitAt(read_buffer_DB4_Input, 11, 4);
                        ElevatorActualFloorSENS3 = S7.GetBitAt(read_buffer_DB4_Input, 11, 5);
                        ElevatorActualFloorSENS4 = S7.GetBitAt(read_buffer_DB4_Input, 11, 6);
                        ElevatorActualFloorSENS5 = S7.GetBitAt(read_buffer_DB4_Input, 11, 7);
                        ElevatorTimeDoorSQOPEN = S7.GetDIntAt(read_buffer_DB4_Input, 12); //Time
                        ElevatroTimeDoorSQCLOSE = S7.GetDIntAt(read_buffer_DB4_Input, 12); //Time
                        ElevatorDoorClOSE = S7.GetBitAt(read_buffer_DB4_Input, 20, 0);
                        ElevatorDoorOPEN = S7.GetBitAt(read_buffer_DB4_Input, 20, 1);
                        ElevatorCabinSpeed = S7.GetIntAt(read_buffer_DB4_Input, 22);
                        ElevatorInactivity = S7.GetBitAt(read_buffer_DB4_Input, 24, 0);
                        ElevatorTimeToGetDown = S7.GetDIntAt(read_buffer_DB4_Input, 26); //Time

                        #endregion

                        errorMessageBoxShown = false;
                    }

                    //Input variables
                    #region Input variables

                    ElevatorBTNCabin1 = S7.GetBitAt(read_buffer_DB4_Input, 0, 0);
                    ElevatorBTNCabin2 = S7.GetBitAt(read_buffer_DB4_Input, 0, 1);
                    ElevatorBTNCabin3 = S7.GetBitAt(read_buffer_DB4_Input, 0, 2);
                    ElevatorBTNCabin4 = S7.GetBitAt(read_buffer_DB4_Input, 0, 3);
                    ElevatorBTNCabin5 = S7.GetBitAt(read_buffer_DB4_Input, 0, 4);
                    ElevatorBTNFloor1 = S7.GetBitAt(read_buffer_DB4_Input, 0, 5);
                    ElevatorBTNFloor2 = S7.GetBitAt(read_buffer_DB4_Input, 0, 6);
                    ElevatorBTNFloor3 = S7.GetBitAt(read_buffer_DB4_Input, 0, 7);
                    ElevatorBTNFloor4 = S7.GetBitAt(read_buffer_DB4_Input, 1, 0);
                    ElevatorBTNFloor5 = S7.GetBitAt(read_buffer_DB4_Input, 1, 1);
                    ElevatorDoorSEQ = S7.GetBitAt(read_buffer_DB4_Input, 1, 2);
                    ElevatorBTNOPENCLOSE = S7.GetBitAt(read_buffer_DB4_Input, 1, 3);
                    ElevatorEmergencySTOP = S7.GetBitAt(read_buffer_DB4_Input, 1, 4);
                    ElevatorErrorSystem = S7.GetBitAt(read_buffer_DB4_Input, 1, 5);

                    #endregion

                    //Output variables
                    #region Output variables

                    ElevatorMotorON = S7.GetBitAt(read_buffer_DB4_Output, 2, 0); ;
                    ElevatorMotorDOWN = S7.GetBitAt(read_buffer_DB4_Output, 2, 1);
                    ElevatorMotorUP = S7.GetBitAt(read_buffer_DB4_Output, 2, 2);
                    ElevatroHoming = S7.GetBitAt(read_buffer_DB4_Output, 2, 3);
                    ElevatorSystemReady = S7.GetBitAt(read_buffer_DB4_Output, 2, 4);
                    ElevatorActualFloor = S7.GetIntAt(read_buffer_DB4_Output, 4);
                    ElevatorMoving = S7.GetBitAt(read_buffer_DB4_Output, 6, 0);
                    ElevatorSystemWorking = S7.GetBitAt(read_buffer_DB4_Output, 6, 1);
                    ElevatorGoToFloor = S7.GetIntAt(read_buffer_DB4_Output, 8);
                    ElevatorDirection = S7.GetBitAt(read_buffer_DB4_Output, 10, 0);
                    ElevatorActualFloorLED1 = S7.GetBitAt(read_buffer_DB4_Output, 10, 1);
                    ElevatorActualFloorLED2 = S7.GetBitAt(read_buffer_DB4_Output, 10, 2);
                    ElevatorActualFloorLED3 = S7.GetBitAt(read_buffer_DB4_Output, 10, 3);
                    ElevatorActualFloorLED4 = S7.GetBitAt(read_buffer_DB4_Output, 10, 4);
                    ElevatorActualFloorLED5 = S7.GetBitAt(read_buffer_DB4_Output, 10, 5);
                    ElevatorActualFloorCabinLED1 = S7.GetBitAt(read_buffer_DB4_Output, 10, 6);
                    ElevatorActualFloorCabinLED2 = S7.GetBitAt(read_buffer_DB4_Output, 10, 7);
                    ElevatorActualFloorCabinLED3 = S7.GetBitAt(read_buffer_DB4_Output, 11, 0);
                    ElevatorActualFloorCabinLED4 = S7.GetBitAt(read_buffer_DB4_Output, 11, 1);
                    ElevatorActualFloorCabinLED5 = S7.GetBitAt(read_buffer_DB4_Output, 11, 2);
                    ElevatorActualFloorSENS1 = S7.GetBitAt(read_buffer_DB4_Output, 11, 3);
                    ElevatorActualFloorSENS2 = S7.GetBitAt(read_buffer_DB4_Output, 11, 4);
                    ElevatorActualFloorSENS3 = S7.GetBitAt(read_buffer_DB4_Output, 11, 5);
                    ElevatorActualFloorSENS4 = S7.GetBitAt(read_buffer_DB4_Output, 11, 6);
                    ElevatorActualFloorSENS5 = S7.GetBitAt(read_buffer_DB4_Output, 11, 7);
                    ElevatorTimeDoorSQOPEN = S7.GetDIntAt(read_buffer_DB4_Output, 12); //Time
                    ElevatroTimeDoorSQCLOSE = S7.GetDIntAt(read_buffer_DB4_Output, 12); //Time
                    ElevatorDoorClOSE = S7.GetBitAt(read_buffer_DB4_Output, 20, 0);
                    ElevatorDoorOPEN = S7.GetBitAt(read_buffer_DB4_Output, 20, 1);
                    ElevatorCabinSpeed = S7.GetIntAt(read_buffer_DB4_Output, 22);
                    ElevatorInactivity = S7.GetBitAt(read_buffer_DB4_Output, 24, 0);
                    ElevatorTimeToGetDown = S7.GetDIntAt(read_buffer_DB4_Output, 26); //Time

                    #endregion

                    errorMessageBoxShown = false;
                }
                else
                {
                    //error
                    statusStripElevator.Items.Clear();
                    ToolStripStatusLabel lblStatus1 = new ToolStripStatusLabel("Variables were not read.");
                    statusStripElevator.Items.Add(lblStatus1);

                    if (!errorMessageBoxShown)
                    {
                        errorMessageBoxShown = true;

                        //MessageBox
                        MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data weren't read from DB4!!! \n\n" +
                            $"Error message {readResultDB4} \n", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }

                #endregion
                */

                //Reading variables with DBRead method
                /*
                #region DBRead

                //DB4 => Elevator_DB -> 2 structs -> 46 variables -> size 2
                int readResultDB4 = client.DBRead(DBNumber_DB4, 0, read_buffer_DB4.Length, read_buffer_DB4);
                if (readResultDB4 != 0)
                {
                    //error
                    statusStripElevator.Items.Clear();
                    ToolStripStatusLabel lblStatus1 = new ToolStripStatusLabel("Variables were not read.");
                    statusStripElevator.Items.Add(lblStatus1);

                    if (!errorMessageBoxShown)
                    {
                        errorMessageBoxShown = true;

                        //MessageBox
                        MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data weren't read from DB4!!! \n\n" +
                            $"Error message {readResultDB4} \n", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }
                else
                {
                    //input variables
                    #region Input variables
                    
                    ElevatorBTNCabin1 = S7.GetBitAt(read_buffer_DB4_Input, 0, 0);
                    ElevatorBTNCabin2 = S7.GetBitAt(read_buffer_DB4_Input, 0, 1);
                    ElevatorBTNCabin3 = S7.GetBitAt(read_buffer_DB4_Input, 0, 2);
                    ElevatorBTNCabin4 = S7.GetBitAt(read_buffer_DB4_Input, 0, 3);
                    ElevatorBTNCabin5 = S7.GetBitAt(read_buffer_DB4_Input, 0, 4);
                    ElevatorBTNFloor1 = S7.GetBitAt(read_buffer_DB4_Input, 0, 5);
                    ElevatorBTNFloor2 = S7.GetBitAt(read_buffer_DB4_Input, 0, 6);
                    ElevatorBTNFloor3 = S7.GetBitAt(read_buffer_DB4_Input, 0, 7);
                    ElevatorBTNFloor4 = S7.GetBitAt(read_buffer_DB4_Input, 1, 0);
                    ElevatorBTNFloor5 = S7.GetBitAt(read_buffer_DB4_Input, 1, 1);
                    ElevatorDoorSEQ = S7.GetBitAt(read_buffer_DB4_Input, 1, 2);
                    ElevatorBTNOPENCLOSE = S7.GetBitAt(read_buffer_DB4_Input, 1, 3);
                    ElevatorEmergencySTOP = S7.GetBitAt(read_buffer_DB4_Input, 1, 4);
                    ElevatorErrorSystem = S7.GetBitAt(read_buffer_DB4_Input, 1, 5);
                    
                    #endregion

                    //output variables
                    #region Output variables
                    
                    ElevatorMotorON = S7.GetBitAt(read_buffer_DB4_Output, 2, 0); ;
                    ElevatorMotorDOWN = S7.GetBitAt(read_buffer_DB4_Output, 2, 1); 
                    ElevatorMotorUP = S7.GetBitAt(read_buffer_DB4_Output, 2, 2);
                    ElevatroHoming = S7.GetBitAt(read_buffer_DB4_Output, 2, 3); 
                    ElevatorSystemReady = S7.GetBitAt(read_buffer_DB4_Output, 2, 4); 
                    ElevatorActualFloor = S7.GetIntAt(read_buffer_DB4_Output, 4);
                    ElevatorMoving = S7.GetBitAt(read_buffer_DB4_Output, 6, 0); 
                    ElevatorSystemWorking = S7.GetBitAt(read_buffer_DB4_Output, 6, 1); 
                    ElevatorGoToFloor = S7.GetIntAt(read_buffer_DB4_Output, 8);
                    ElevatorDirection = S7.GetBitAt(read_buffer_DB4_Output, 10, 0); 
                    ElevatorActualFloorLED1 = S7.GetBitAt(read_buffer_DB4_Output, 10, 1); 
                    ElevatorActualFloorLED2 = S7.GetBitAt(read_buffer_DB4_Output, 10, 2); 
                    ElevatorActualFloorLED3 = S7.GetBitAt(read_buffer_DB4_Output, 10, 3); 
                    ElevatorActualFloorLED4 = S7.GetBitAt(read_buffer_DB4_Output, 10, 4); 
                    ElevatorActualFloorLED5 = S7.GetBitAt(read_buffer_DB4_Output, 10, 5); 
                    ElevatorActualFloorCabinLED1 = S7.GetBitAt(read_buffer_DB4_Output, 10, 6); 
                    ElevatorActualFloorCabinLED2 = S7.GetBitAt(read_buffer_DB4_Output, 10, 7); 
                    ElevatorActualFloorCabinLED3 = S7.GetBitAt(read_buffer_DB4_Output, 11, 0); 
                    ElevatorActualFloorCabinLED4 = S7.GetBitAt(read_buffer_DB4_Output, 11, 1); 
                    ElevatorActualFloorCabinLED5 = S7.GetBitAt(read_buffer_DB4_Output, 11, 2); 
                    ElevatorActualFloorSENS1 = S7.GetBitAt(read_buffer_DB4_Output, 11, 3); 
                    ElevatorActualFloorSENS2 = S7.GetBitAt(read_buffer_DB4_Output, 11, 4); 
                    ElevatorActualFloorSENS3 = S7.GetBitAt(read_buffer_DB4_Output, 11, 5); 
                    ElevatorActualFloorSENS4 = S7.GetBitAt(read_buffer_DB4_Output, 11, 6); 
                    ElevatorActualFloorSENS5 = S7.GetBitAt(read_buffer_DB4_Output, 11, 7); 
                    ElevatorTimeDoorSQOPEN = S7.GetDIntAt(read_buffer_DB4_Output, 12); //Time
                    ElevatroTimeDoorSQCLOSE = S7.GetDIntAt(read_buffer_DB4_Output, 12); //Time
                    ElevatorDoorClOSE = S7.GetBitAt(read_buffer_DB4_Output, 20, 0); 
                    ElevatorDoorOPEN = S7.GetBitAt(read_buffer_DB4_Output, 20, 1); 
                    ElevatorCabinSpeed = S7.GetIntAt(read_buffer_DB4_Output, 22);
                    ElevatorInactivity = S7.GetBitAt(read_buffer_DB4_Output, 24, 0); 
                    ElevatorTimeToGetDown = S7.GetDIntAt(read_buffer_DB4_Output, 26); //Time
                    
                    #endregion

                    errorMessageBoxShown = false;
                }

                #endregion

                */

                //Action on variable change (if == true)
                #region Action on variable change (if == true)

                if (ElevatorDoorSEQ)
                {

                }

                if (ElevatorBTNOPENCLOSE)
                {

                }

                if (ElevatorEmergencySTOP)
                {
                    btnCabinEmergency_Click(this, EventArgs.Empty);
                }

                if (ElevatorErrorSystem)
                {
                    //toto je blbě, musí tady být řádná chybová hláška 
                    
                    ErrorSystem();

                    //write error
                    if (!errorMessageBoxShown)
                    {
                        //MessageBox
                        MessageBox.Show("Error system is true. \n\n", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                        errorMessageBoxShown = true;
                    }
                }

                //pohyb výtahu musí být postaven na ElevatorGoToFloor a ElevatorActualFloor

                //Elevator cabin movemnt based on ElevatorActualFloor and ElevatorGotToFloor
                #region Elevator cabin movemnt based on ElevatorActualFloor and ElevatorGotToFloor

                //All combinations for 1st floor 
                #region all combinations for 1st floor 

                if (ElevatorGoToFloor == 1 && ElevatorActualFloor == 1)
                {
                    //DoorSQ => TIA do automatically 
                    ElevatorActualFloorSENS1 = true; //mozno resit v ramci pozice v userControlu
                }

                if (ElevatorGoToFloor == 1 && ElevatorActualFloor == 2)
                {
                    userControlElevatorCabin1.CabinMoveToFloorDOWN(1);
                    ElevatorActualFloorSENS1 = true; //mozno resit v ramci pozice v userControlu
                }

                if (ElevatorGoToFloor == 1 && ElevatorActualFloor == 3)
                {
                    userControlElevatorCabin1.CabinMoveToFloorDOWN(2);
                    ElevatorActualFloorSENS1 = true; //mozno resit v ramci pozice v userControlu
                }

                if (ElevatorGoToFloor == 1 && ElevatorActualFloor == 4)
                {
                    userControlElevatorCabin1.CabinMoveToFloorDOWN(3);
                    ElevatorActualFloorSENS1 = true; //mozno resit v ramci pozice v userControlu
                }

                if (ElevatorGoToFloor == 1 && ElevatorActualFloor == 5)
                {
                    userControlElevatorCabin1.CabinMoveToFloorDOWN(4);
                    ElevatorActualFloorSENS1 = true; //mozno resit v ramci pozice v userControlu
                }

                #endregion

                //All combinations for 2md floor 
                #region all combinations for 2nd floor 

                if (ElevatorGoToFloor == 2 && ElevatorActualFloor == 1)
                {
                    userControlElevatorCabin1.CabinMoveToFloorUP(1);
                    ElevatorActualFloorSENS2 = true; //mozno resit v ramci pozice v userControlu
                }

                if (ElevatorGoToFloor == 2 && ElevatorActualFloor == 2)
                {
                    //DoorSQ => TIA do automatically 
                    ElevatorActualFloorSENS2 = true; //mozno resit v ramci pozice v userControlu
                }

                if (ElevatorGoToFloor == 2 && ElevatorActualFloor == 3)
                {
                    userControlElevatorCabin1.CabinMoveToFloorDOWN(1);
                    ElevatorActualFloorSENS2 = true; //mozno resit v ramci pozice v userControlu
                }

                if (ElevatorGoToFloor == 2 && ElevatorActualFloor == 4)
                {
                    userControlElevatorCabin1.CabinMoveToFloorDOWN(2);
                    ElevatorActualFloorSENS2 = true; //mozno resit v ramci pozice v userControlu
                }

                if (ElevatorGoToFloor == 2 && ElevatorActualFloor == 5)
                {
                    userControlElevatorCabin1.CabinMoveToFloorDOWN(3);
                    ElevatorActualFloorSENS2 = true; //mozno resit v ramci pozice v userControlu
                }

                #endregion

                //All combinations for 3rd floor
                #region all combinations for 3rd floor 

                if (ElevatorGoToFloor == 3 && ElevatorActualFloor == 1)
                {
                    userControlElevatorCabin1.CabinMoveToFloorUP(2);
                    ElevatorActualFloorSENS3 = true; //mozno resit v ramci pozice v userControlu
                }

                if (ElevatorGoToFloor == 3 && ElevatorActualFloor == 2)
                {
                    userControlElevatorCabin1.CabinMoveToFloorUP(1);
                    ElevatorActualFloorSENS3 = true; //mozno resit v ramci pozice v userControlu
                }

                if (ElevatorGoToFloor == 3 && ElevatorActualFloor == 3)
                {
                    //DoorSQ => TIA do automatically 
                    ElevatorActualFloorSENS3 = true; //mozno resit v ramci pozice v userControlu
                }

                if (ElevatorGoToFloor == 3 && ElevatorActualFloor == 4)
                {
                    userControlElevatorCabin1.CabinMoveToFloorDOWN(1);
                    ElevatorActualFloorSENS3 = true; //mozno resit v ramci pozice v userControlu
                }

                if (ElevatorGoToFloor == 3 && ElevatorActualFloor == 5)
                {
                    userControlElevatorCabin1.CabinMoveToFloorDOWN(2);
                    ElevatorActualFloorSENS3 = true; //mozno resit v ramci pozice v userControlu
                }

                #endregion

                //All combinations for 4th floor 
                #region all combinations for 4th floor 

                if (ElevatorGoToFloor == 4 && ElevatorActualFloor == 1)
                {
                    userControlElevatorCabin1.CabinMoveToFloorUP(3);
                    ElevatorActualFloorSENS4 = true; //mozno resit v ramci pozice v userControlu
                }

                if (ElevatorGoToFloor == 4 && ElevatorActualFloor == 2)
                {
                    userControlElevatorCabin1.CabinMoveToFloorUP(2);
                    ElevatorActualFloorSENS4 = true; //mozno resit v ramci pozice v userControlu
                }

                if (ElevatorGoToFloor == 4 && ElevatorActualFloor == 3)
                {
                    userControlElevatorCabin1.CabinMoveToFloorUP(1);
                    ElevatorActualFloorSENS4 = true; //mozno resit v ramci pozice v userControlu
                }

                if (ElevatorGoToFloor == 4 && ElevatorActualFloor == 4)
                {
                    //DoorSQ => TIA do automatically 
                    ElevatorActualFloorSENS4 = true; //mozno resit v ramci pozice v userControlu
                }

                if (ElevatorGoToFloor == 4 && ElevatorActualFloor == 5)
                {
                    userControlElevatorCabin1.CabinMoveToFloorDOWN(1);
                    ElevatorActualFloorSENS4 = true; //mozno resit v ramci pozice v userControlu
                }

                #endregion

                //All combinations for 5th floor 
                #region all combinations for 5th floor 

                if (ElevatorGoToFloor == 5 && ElevatorActualFloor == 1)
                {
                    userControlElevatorCabin1.CabinMoveToFloorUP(4);
                    ElevatorActualFloorSENS5 = true; //mozno resit v ramci pozice v userControlu
                }

                if (ElevatorGoToFloor == 5 && ElevatorActualFloor == 2)
                {
                    userControlElevatorCabin1.CabinMoveToFloorUP(3);
                    ElevatorActualFloorSENS5 = true; //mozno resit v ramci pozice v userControlu
                }

                if (ElevatorGoToFloor == 5 && ElevatorActualFloor == 3)
                {
                    userControlElevatorCabin1.CabinMoveToFloorUP(2);
                    ElevatorActualFloorSENS5 = true; //mozno resit v ramci pozice v userControlu
                }

                if (ElevatorGoToFloor == 5 && ElevatorActualFloor == 4)
                {
                    userControlElevatorCabin1.CabinMoveToFloorUP(1);
                    ElevatorActualFloorSENS5 = true; //mozno resit v ramci pozice v userControlu
                }

                if (ElevatorGoToFloor == 5 && ElevatorActualFloor == 5)
                {
                    //DoorSQ => TIA do automatically 
                    ElevatorActualFloorSENS5 = true; //mozno resit v ramci pozice v userControlu
                }

                #endregion

                #endregion

                //ElevatorCabinBTN
                #region ElevatorCabinBTN
                /*
                if (ElevatorBTNCabin1)
                {
                    Floor1movement();
                }
                else if (ElevatorBTNCabin2)
                {
                    Floor2movement();
                }
                else if (ElevatorBTNCabin3)
                {
                    Floor3movement();
                }
                else if (ElevatorBTNCabin4)
                {
                    Floor4movement();
                }
                else if (ElevatorBTNCabin5)
                {
                    Floor5movement();
                }
                else
                {
                    //waiting for BTN push
                }
                */
                #endregion

                //ElevatorFloorBTN
                #region ElevatorFloorBTN
                /*
                if (ElevatorBTNFloor1)
                {
                    Floor1movement();
                }
                else if (ElevatorBTNFloor2)
                {
                    Floor2movement();
                }
                else if (ElevatorBTNFloor3)
                {
                    Floor3movement();
                }
                else if (ElevatorBTNFloor4)
                {
                    Floor4movement();
                }
                else if (ElevatorBTNFloor5)
                {
                    Floor5movement();
                }
                else
                {
                    //waiting for BTN push
                }
                */
                #endregion

                //ElevatorActualFloorLED
                #region ElevatorActualFloorLED

                if (ElevatorActualFloorLED1)
                {
                    //zapnout
                    userControlElevatorCabin1.ElevatorActualFloorLED1Signalization(true);
                }
                else
                {
                    //vypnout
                    userControlElevatorCabin1.ElevatorActualFloorLED1Signalization(false);
                }

                if (ElevatorActualFloorLED2)
                {
                    //zapnout
                    userControlElevatorCabin1.ElevatorActualFloorLED2Signalization(true);
                }
                else
                {
                    //vypnout
                    userControlElevatorCabin1.ElevatorActualFloorLED2Signalization(false);
                }

                if (ElevatorActualFloorLED3)
                {
                    //zapnout
                    userControlElevatorCabin1.ElevatorActualFloorLED3Signalization(true);
                }
                else
                {
                    //vypnout
                    userControlElevatorCabin1.ElevatorActualFloorLED3Signalization(false);
                }

                if (ElevatorActualFloorLED4)
                {
                    //zapnout
                    userControlElevatorCabin1.ElevatorActualFloorLED4Signalization(true);
                }
                else
                {
                    //vypnout
                    userControlElevatorCabin1.ElevatorActualFloorLED4Signalization(false);
                }

                if (ElevatorActualFloorLED5)
                {
                    //zapnout
                    userControlElevatorCabin1.ElevatorActualFloorLED5Signalization(true);
                }
                else
                {
                    //vypnout
                    userControlElevatorCabin1.ElevatorActualFloorLED5Signalization(false);
                }

                #endregion

                //ElevatorActualFloorCabinLED
                #region ElevatorActualFloorCabinLED

                if (ElevatorActualFloorCabinLED1)
                {
                    //zapnout
                    btnCabinFloor1.FlatAppearance.BorderColor = Color.Blue;
                }
                else
                {
                    //vypnout
                    btnCabinFloor1.FlatAppearance.BorderColor = Color.Gray;
                }

                if (ElevatorActualFloorCabinLED2)
                {
                    //zapnout
                    btnCabinFloor2.FlatAppearance.BorderColor = Color.Blue;
                }
                else
                {
                    //vypnout
                    btnCabinFloor2.FlatAppearance.BorderColor = Color.Gray;
                }

                if (ElevatorActualFloorCabinLED3)
                {
                    //zapnout
                    btnCabinFloor3.FlatAppearance.BorderColor = Color.Blue;
                }
                else
                {
                    //vypnout
                    btnCabinFloor3.FlatAppearance.BorderColor = Color.Gray;
                }

                if (ElevatorActualFloorCabinLED4)
                {
                    //zapnout
                    btnCabinFloor4.FlatAppearance.BorderColor = Color.Blue;
                }
                else
                {
                    //vypnout
                    btnCabinFloor4.FlatAppearance.BorderColor = Color.Gray;
                }

                if (ElevatorActualFloorCabinLED5)
                {
                    //zapnout
                    btnCabinFloor5.FlatAppearance.BorderColor = Color.Blue;
                }
                else
                {
                    //vypnout
                    btnCabinFloor5.FlatAppearance.BorderColor = Color.Gray;
                }

                #endregion

                if (ElevatorDoorClOSE)
                {

                }

                if (ElevatorDoorOPEN)
                {

                }

                #endregion

                errorMessageBoxShown = false;
            }
            catch (Exception ex)
            {
                ErrorSystem();

                if (!errorMessageBoxShown)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                    errorMessageBoxShown = true;
                }

            }
        }

        #endregion

        //Movement to specific floor 
        #region Movement to specific floor 
        
        //BTN cabin 
        #region BTN cabin
        private void btnCabinFloor1_Click(object sender, EventArgs e)
        {
            //Floor1movement(); //this must go => Tia needs to activate this 

            ElevatorBTNCabin1 = true;
            S7.SetBitAt(send_buffer_DB4, 0, 0, ElevatorBTNCabin1);

            //write to PLC
            int writeResultDB4_btnCabinFloor1 = client.DBWrite(DBNumber_DB4, 0, send_buffer_DB4.Length, send_buffer_DB4);
            if (writeResultDB4_btnCabinFloor1 != 0)
            {
                //write error
                if (!errorMessageBoxShown)
                {
                    //MessageBox
                    MessageBox.Show("BE doesn't work properly. Data could´t be written to DB4!!! \n\n" +
                        $"Error message: writeResultDB4_btnCabinFloor1 = {writeResultDB4_btnCabinFloor1} \n", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                    errorMessageBoxShown = true;
                }
            }
            else
            {
                //write was successful
            }
        }

        private void btnCabinFloor2_Click(object sender, EventArgs e)
        {
            //Floor2movement(); //this must go => Tia needs to activate this 

            ElevatorBTNCabin2 = true;
            S7.SetBitAt(send_buffer_DB4, 0, 1, ElevatorBTNCabin2);

            //write to PLC
            int writeResultDB4_btnCabinFloor2 = client.DBWrite(DBNumber_DB4, 0, send_buffer_DB4.Length, send_buffer_DB4);
            if (writeResultDB4_btnCabinFloor2 != 0)
            {
                //write error
                if (!errorMessageBoxShown)
                {
                    //MessageBox
                    MessageBox.Show("BE doesn't work properly. Data could´t be written to DB4!!! \n\n" +
                        $"Error message: writeResultDB4_btnCabinFloor2 = {writeResultDB4_btnCabinFloor2} \n", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                    errorMessageBoxShown = true;
                }
            }
            else
            {
                //write was successful
            }
        }

        private void btnCabinFloor3_Click(object sender, EventArgs e)
        {
            //Floor3movement(); //this must go => Tia needs to activate this 

            ElevatorBTNCabin3 = true;
            S7.SetBitAt(send_buffer_DB4, 0, 2, ElevatorBTNCabin3);

            //write to PLC
            int writeResultDB4_btnCabinFloor3 = client.DBWrite(DBNumber_DB4, 0, send_buffer_DB4.Length, send_buffer_DB4);
            if (writeResultDB4_btnCabinFloor3 != 0)
            {
                //write error
                if (!errorMessageBoxShown)
                {
                    //MessageBox
                    MessageBox.Show("BE doesn't work properly. Data could´t be written to DB4!!! \n\n" +
                        $"Error message: writeResultDB4_btnCabinFloor3 = {writeResultDB4_btnCabinFloor3} \n", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                    errorMessageBoxShown = true;
                }
            }
            else
            {
                //write was successful
            }
        }

        private void btnCabinFloor4_Click(object sender, EventArgs e)
        {
            //Floor4movement(); //this must go => Tia needs to activate this 

            ElevatorBTNCabin4 = true;
            S7.SetBitAt(send_buffer_DB4, 0, 3, ElevatorBTNCabin4);

            //write to PLC
            int writeResultDB4_btnCabinFloor4 = client.DBWrite(DBNumber_DB4, 0, send_buffer_DB4.Length, send_buffer_DB4);
            if (writeResultDB4_btnCabinFloor4 != 0)
            {
                //write error
                if (!errorMessageBoxShown)
                {
                    //MessageBox
                    MessageBox.Show("BE doesn't work properly. Data could´t be written to DB4!!! \n\n" +
                        $"Error message: writeResultDB4_btnCabinFloor4 = {writeResultDB4_btnCabinFloor4} \n", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                    errorMessageBoxShown = true;
                }
            }
            else
            {
                //write was successful
            }
        }

        private void btnCabinFloor5_Click(object sender, EventArgs e)
        {
            //Floor5movement(); //this must go => Tia needs to activate this 

            ElevatorBTNCabin5 = true;
            S7.SetBitAt(send_buffer_DB4, 0, 3, ElevatorBTNCabin5);

            //write to PLC
            int writeResultDB4_btnCabinFloor5 = client.DBWrite(DBNumber_DB4, 0, send_buffer_DB4.Length, send_buffer_DB4);
            if (writeResultDB4_btnCabinFloor5 != 0)
            {
                //write error
                if (!errorMessageBoxShown)
                {
                    //MessageBox
                    MessageBox.Show("BE doesn't work properly. Data could´t be written to DB4!!! \n\n" +
                        $"Error message: writeResultDB4_btnCabinFloor5 = {writeResultDB4_btnCabinFloor5} \n", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                    errorMessageBoxShown = true;
                }
            }
            else
            {
                //write was successful
            }
        }
        #endregion

        //btn Floor
        #region btn floor => cabin movement 
        /*
        private void btnFloor1_Click(object sender, EventArgs e)
        {
            Floor1movement();
        }

        private void btnFloor2_Click(object sender, EventArgs e)
        {
            Floor2movement();
        }

        private void btnFloor3_Click(object sender, EventArgs e)
        {
            Floor3movement();
        }

        private void btnFloor4_Click(object sender, EventArgs e)
        {
            Floor4movement();
        }

        private void btnFloor5_Click(object sender, EventArgs e)
        {
            Floor5movement();
        }
        */
        #endregion

        //Move to Floor 
        #region Move to floor 

        /*

        #region Move to floor 1
        private async void Floor1movement()
        {
            if (ElevatorActualFloor == 1)
            {
                //Opendoor; => dela Tia
                //mozna ElevatorActualFloorSENS1 = true; => mozno resit v ramci pozice v userControlu
            }
            else if (ElevatorActualFloor == 2)
            {
                userControlElevatorCabin1.CabinMoveToFloorDOWN(1);
                //posun o určitý počet pixelů 
                for (int i = 0; i < 10; i++)
                {
                    userControlElevatorCabin1.MoveDown();
                    await Task.Delay(500);
                }
                //mozna ElevatorActualFloorSENS1 = true; => mozno resit v ramci pozice v userControlu
            }
            else if (ElevatorActualFloor == 3)
            {
                userControlElevatorCabin1.CabinMoveToFloorDOWN(2);
                //posun o určitý počet pixelů 
                for (int i = 0; i < 20; i++)
                {
                    userControlElevatorCabin1.MoveDown();
                    await Task.Delay(500);
                }
                //mozna ElevatorActualFloorSENS1 = true; => mozno resit v ramci pozice v userControlu
            }
            else if (ElevatorActualFloor == 4)
            {
                userControlElevatorCabin1.CabinMoveToFloorDOWN(3);
                //posun o určitý počet pixelů 
                for (int i = 0; i < 30; i++)
                {
                    userControlElevatorCabin1.MoveDown();
                    await Task.Delay(500);
                }
                //mozna ElevatorActualFloorSENS1 = true; => mozno resit v ramci pozice v userControlu
            }
            else if (ElevatorActualFloor == 5)
            {
                userControlElevatorCabin1.CabinMoveToFloorDOWN(4);
                //posun o určitý počet pixelů 
                for (int i = 0; i < 40; i++)
                {
                    userControlElevatorCabin1.MoveDown();
                    await Task.Delay(500);
                }
                //mozna ElevatorActualFloorSENS1 = true; => mozno resit v ramci pozice v userControlu
            }
            else
            {
                ErrorSystem();

                //write error
                if (!errorMessageBoxShown)
                {
                    //MessageBox
                    MessageBox.Show("Floor1Movement is not possible. \n\n", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                    errorMessageBoxShown = true;
                }
            }
        }

        #endregion

        #region Move to floor 2
        private async void Floor2movement()
        {
            if (ElevatorActualFloor == 1)
            {
                userControlElevatorCabin1.CabinMoveToFloorUP(1);
                //posun o určitý počet pixelů 
                for (int i = 0; i < 10; i++)
                {
                    userControlElevatorCabin1.MoveUp();
                    await Task.Delay(500);
                }
                //mozna ElevatorActualFloorSENS2 = true; => mozno resit v ramci pozice v userControlu
            }
            else if (ElevatorActualFloor == 2)
            {
                //Opendoor; => dela Tia
                //mozna ElevatorActualFloorSENS2 = true; => mozno resit v ramci pozice v userControl
            }
            else if (ElevatorActualFloor == 3)
            {
                userControlElevatorCabin1.CabinMoveToFloorDOWN(1);
                //posun o určitý počet pixelů 
                for (int i = 0; i < 10; i++)
                {
                    userControlElevatorCabin1.MoveDown();
                    await Task.Delay(500);
                }
                //mozna ElevatorActualFloorSENS2 = true; => mozno resit v ramci pozice v userControlu
            }
            else if (ElevatorActualFloor == 4)
            {
                userControlElevatorCabin1.CabinMoveToFloorDOWN(2);
                //posun o určitý počet pixelů 
                for (int i = 0; i < 20; i++)
                {
                    userControlElevatorCabin1.MoveDown();
                    await Task.Delay(500);
                }
                //mozna ElevatorActualFloorSENS2 = true; => mozno resit v ramci pozice v userControlu
            }
            else if (ElevatorActualFloor == 5)
            {
                userControlElevatorCabin1.CabinMoveToFloorDOWN(3);
                //posun o určitý počet pixelů 
                for (int i = 0; i < 30; i++)
                {
                    userControlElevatorCabin1.MoveDown();
                    await Task.Delay(500);
                }
                //mozna ElevatorActualFloorSENS2 = true; => mozno resit v ramci pozice v userControlu
            }
            else
            {
                ErrorSystem();

                //write error
                if (!errorMessageBoxShown)
                {
                    //MessageBox
                    MessageBox.Show("Floor2Movement is not possible. \n\n", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                    errorMessageBoxShown = true;
                }
            }
        }
        #endregion

        #region Move to floor 3
        private async void Floor3movement()
        {
            if (ElevatorActualFloor == 1)
            {
                userControlElevatorCabin1.CabinMoveToFloorUP(2);
                //posun o určitý počet pixelů 
                for (int i = 0; i < 20; i++)
                {
                    userControlElevatorCabin1.MoveUp();
                    await Task.Delay(500);
                }
                //mozna ElevatorActualFloorSENS3 = true; => mozno resit v ramci pozice v userControlu
            }
            else if (ElevatorActualFloor == 2)
            {
                userControlElevatorCabin1.CabinMoveToFloorUP(1);
                //posun o určitý počet pixelů 
                for (int i = 0; i < 10; i++)
                {
                    userControlElevatorCabin1.MoveUp();
                    await Task.Delay(500);
                }
                //mozna ElevatorActualFloorSENS3 = true; => mozno resit v ramci pozice v userControlu
            }
            else if (ElevatorActualFloor == 3)
            {
                //Opendoor; => dela Tia
                //mozna ElevatorActualFloorSENS3 = true; => mozno resit v ramci pozice v userControlu
            }
            else if (ElevatorActualFloor == 4)
            {
                userControlElevatorCabin1.CabinMoveToFloorDOWN(1);
                //posun o určitý počet pixelů 
                for (int i = 0; i < 10; i++)
                {
                    userControlElevatorCabin1.MoveDown();
                    await Task.Delay(500);
                }
                //mozna ElevatorActualFloorSENS3 = true; => mozno resit v ramci pozice v userControlu
            }
            else if (ElevatorActualFloor == 5)
            {
                userControlElevatorCabin1.CabinMoveToFloorDOWN(2);
                //posun o určitý počet pixelů 
                for (int i = 0; i < 20; i++)
                {
                    userControlElevatorCabin1.MoveDown();
                    await Task.Delay(500);
                }
                //mozna ElevatorActualFloorSENS3 = true; => mozno resit v ramci pozice v userControlu
            }
            else
            {
                ErrorSystem();

                //write error
                if (!errorMessageBoxShown)
                {
                    //MessageBox
                    MessageBox.Show("Floor3Movement is not possible. \n\n", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                    errorMessageBoxShown = true;
                }
            }

        }
        #endregion

        #region Move to floor 4
        private async void Floor4movement()
        {
            if (ElevatorActualFloor == 1)
            {
                userControlElevatorCabin1.CabinMoveToFloorUP(3);
                //posun o určitý počet pixelů 
                for (int i = 0; i < 30; i++)
                {
                    userControlElevatorCabin1.MoveUp();
                    await Task.Delay(500);
                }
                //mozna ElevatorActualFloorSENS4 = true; => mozno resit v ramci pozice v userControlu
            }
            else if (ElevatorActualFloor == 2)
            {
                userControlElevatorCabin1.CabinMoveToFloorUP(2);
                //posun o určitý počet pixelů 
                for (int i = 0; i < 20; i++)
                {
                    userControlElevatorCabin1.MoveUp();
                    await Task.Delay(500);
                }
                //mozna ElevatorActualFloorSENS4 = true; => mozno resit v ramci pozice v userControlu
            }
            else if (ElevatorActualFloor == 3)
            {
                userControlElevatorCabin1.CabinMoveToFloorUP(1);
                //posun o určitý počet pixelů 
                for (int i = 0; i < 10; i++)
                {
                    userControlElevatorCabin1.MoveUp();
                    await Task.Delay(500);
                }
                //mozna ElevatorActualFloorSENS4 = true; => mozno resit v ramci pozice v userControlu
            }
            else if (ElevatorActualFloor == 4)
            {
                //Opendoor; => dela Tia
                //mozna ElevatorActualFloorSENS4 = true; => mozno resit v ramci pozice v userControlu
            }
            else if (ElevatorActualFloor == 5)
            {
                userControlElevatorCabin1.CabinMoveToFloorDOWN(1);
                //posun o určitý počet pixelů 
                for (int i = 0; i < 30; i++)
                {
                    userControlElevatorCabin1.MoveDown();
                    await Task.Delay(500);
                }
                //mozna ElevatorActualFloorSENS4 = true; => mozno resit v ramci pozice v userControlu
            }
            else
            {
                ErrorSystem();

                //write error
                if (!errorMessageBoxShown)
                {
                    //MessageBox
                    MessageBox.Show("Floor4Movement is not possible. \n\n", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                    errorMessageBoxShown = true;
                }
            }
        }
        #endregion

        #region Move to floor 5
        private async void Floor5movement()
        {
            if (ElevatorActualFloor == 1)
            {
                userControlElevatorCabin1.CabinMoveToFloorUP(4);
                //posun o určitý počet pixelů 
                for (int i = 0; i < 40; i++)
                {
                    userControlElevatorCabin1.MoveUp();
                    await Task.Delay(500);
                }
                //mozna ElevatorActualFloorSENS5 = true; => mozno resit v ramci pozice v userControlu
            }
            else if (ElevatorActualFloor == 2)
            {
                userControlElevatorCabin1.CabinMoveToFloorUP(3);
                //posun o určitý počet pixelů 
                for (int i = 0; i < 30; i++)
                {
                    userControlElevatorCabin1.MoveUp();
                    await Task.Delay(500);
                }
                //mozna ElevatorActualFloorSENS5 = true; => mozno resit v ramci pozice v userControlu
            }
            else if (ElevatorActualFloor == 3)
            {
                userControlElevatorCabin1.CabinMoveToFloorUP(2);
                //posun o určitý počet pixelů 
                for (int i = 0; i < 20; i++)
                {
                    userControlElevatorCabin1.MoveUp();
                    await Task.Delay(500);
                }
                //mozna ElevatorActualFloorSENS5 = true; => mozno resit v ramci pozice v userControlu
            }
            else if (ElevatorActualFloor == 4)
            {
                userControlElevatorCabin1.CabinMoveToFloorUP(1);
                //posun o určitý počet pixelů 
                for (int i = 0; i < 10; i++)
                {
                    userControlElevatorCabin1.MoveUp();
                    await Task.Delay(500);
                }
                //mozna ElevatorActualFloorSENS5 = true; => mozno resit v ramci pozice v userControlu
            }
            else if (ElevatorActualFloor == 5)
            {
                //Opendoor; => dela Tia
                //mozna ElevatorActualFloorSENS5 = true; => mozno resit v ramci pozice v userControlu
            }
            else
            {
                ErrorSystem();

                //write error
                if (!errorMessageBoxShown)
                {
                    //MessageBox
                    MessageBox.Show("Floor5Movement is not possible. \n\n", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                    errorMessageBoxShown = true;
                }
            }
        }
        #endregion

        */

        #endregion

        #endregion

        //Cabin door movement 
        #region Cabin doors movement
        private async void btnCabinDoorOPENCLOSE_Click(object sender, EventArgs e)
        {
            OpenDOOR();
            await Task.Delay(5000);
            CloseDOOR();
        }

        private async void CloseDOOR()
        {
            ToolStripStatusLabel lblStatus;

            statusStripElevator.Items.Clear();
            lblStatus = new ToolStripStatusLabel("Closing door");
            statusStripElevator.Items.Add(lblStatus);

            statusStripElevator.Items.Clear();
            lblStatus = new ToolStripStatusLabel("Door closed");
            statusStripElevator.Items.Add(lblStatus);
        }

        private async void OpenDOOR()
        {
            ToolStripStatusLabel lblStatus;

            statusStripElevator.Items.Clear();
            lblStatus = new ToolStripStatusLabel("Openning door");
            statusStripElevator.Items.Add(lblStatus);

            statusStripElevator.Items.Clear();
            lblStatus = new ToolStripStatusLabel("Door open");
            statusStripElevator.Items.Add(lblStatus);
        }
        #endregion

        //Emergency + system error 
        #region Emergency + system error
        private void btnCabinEmergency_Click(object sender, EventArgs e)
        {
            statusStripElevator.Items.Clear();
            ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Emergency mode activated");
            statusStripElevator.Items.Add(lblStatus);

            ElevatorEmergencySTOP = true;
            S7.SetBitAt(send_buffer_DB4, 1, 4, ElevatorEmergencySTOP);

            //write to PLC
            int writeResultDB4_Emergency = client.DBWrite(DBNumber_DB4, 0, send_buffer_DB4.Length, send_buffer_DB4);
            if (writeResultDB4_Emergency != 0)
            {
                //write error
                if (!errorMessageBoxShown)
                {
                    //MessageBox
                    MessageBox.Show("BE doesn't work properly. Data could´t be written to DB4!!! \n\n" +
                        $"Error message: writeResultDB4_Emergency = {writeResultDB4_Emergency} \n", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                    errorMessageBoxShown = true;
                }
            }
            else
            {
                //write was successful
            }
        }

        private void ErrorSystem()
        {
            statusStripElevator.Items.Clear();
            ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Error system");
            statusStripElevator.Items.Add(lblStatus);

            ElevatorErrorSystem = true;
            S7.SetBitAt(send_buffer_DB4, 1, 5, ElevatorErrorSystem);

            //write to PLC
            int writeResultDB4_ErrorSystem = client.DBWrite(DBNumber_DB4, 0, send_buffer_DB4.Length, send_buffer_DB4);
            if (writeResultDB4_ErrorSystem != 0)
            {
                //write error
                if (!errorMessageBoxShown)
                {
                    //MessageBox
                    MessageBox.Show("BE doesn't work properly. Data could´t be written to DB4!!! \n\n" +
                        $"Error message: writeResultDB4_ErrorSystem = {writeResultDB4_ErrorSystem} \n", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                    errorMessageBoxShown = true;
                }
            }
            else
            {
                //write was successful
            }
        }

        #endregion

        //btn End 
        #region Close window 
        private void btnEnd_Click(object sender, EventArgs e)
        {
            //Option1 = false
            Option1 = false;
            S7.SetBitAt(send_buffer_DB11, 0, 0, Option1);

            //write to PLC
            int writeResultDB11_btnEnd = client.DBWrite(DBNumber_DB11, 0, send_buffer_DB11.Length, send_buffer_DB11);
            if (writeResultDB11_btnEnd != 0)
            {
                //write error
                if (!errorMessageBoxShown)
                {
                    //MessageBox
                    MessageBox.Show("BE doesn't work properly. Data could´t be written to DB11!!! \n\n" +
                        $"Error message: writeResultDB11_btnEnd = {writeResultDB11_btnEnd} \n", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                    errorMessageBoxShown = true;
                }
            }
            else
            {
                //write was successful
                this.Close();
            }
        }
        #endregion

        //ElevatorCabin - cabin size parameters and position
        #region ElevatorCabin - cabin size parameters and position

        //Movement
        #region Movement
        private void btnCabinMoveToRight_Click(object sender, EventArgs e)
        {
            userControlElevatorCabin1.MoveRight();
        }

        private void btnCabinMoveToLeft_Click(object sender, EventArgs e)
        {
            userControlElevatorCabin1.MoveLeft();
        }

        private void btnCabinMoveToUp_Click(object sender, EventArgs e)
        {
            userControlElevatorCabin1.MoveUp();
        }

        private void btnCabinMoveToDown_Click(object sender, EventArgs e)
        {
            userControlElevatorCabin1.MoveDown();
        }
        #endregion

        //Cabin size parameters
        #region Cabin size parameters
        private void btnCabinWidthSmaller_Click(object sender, EventArgs e)
        {
            userControlElevatorCabin1.WidthSmaller();
        }

        private void btnCabinWidthBigger_Click(object sender, EventArgs e)
        {
            userControlElevatorCabin1.WidthBigger();
        }

        private void btnCabinLengthSmaller_Click(object sender, EventArgs e)
        {
            userControlElevatorCabin1.LengthSmaller();
        }

        private void btnCabinLengthBigger_Click(object sender, EventArgs e)
        {
            userControlElevatorCabin1.LengthBigger();
        }

        #endregion

        #endregion

    }
}
