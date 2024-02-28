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
using static System.Windows.Forms.Design.AxImporter;
using Sharp7;
using System.Security.Cryptography;

namespace Bc_prace
{
    public partial class Program1Form : Form
    {
        private ChooseOptionForm chooseOptionFormInstance;

        S7Client client;

        private bool errorMessageBoxShown = false;

        //DB11 => Maintain_DB -> 1 struct -> 3 variables -> size 0.2
        private int DBNumber_DB11 = 11;
        byte[] read_buffer_DB11;
        public byte[] PreviousBufferHash_DB11;
        byte[] send_buffer_DB11;

        //MaintainDB variables
        bool Option1 = false;

        //DB4 => Elevator_DB -> 2 structs -> 46 variables -> size 26
        private int DBNumber_DB4 = 4;
        //first struct -> Input -> 14 variables -> size 1.5 
        byte[] read_buffer_DB4_Input;
        public byte[] previous_buffer_DB4_Input; 
        public byte[] PreviousBufferHash_DB4_Input;
        byte[] send_buffer_DB4_Input;
        //second struct -> Output -> 32 variables -> size 26
        byte[] read_buffer_DB4_Output;
        public byte[] PreviousBufferHash_DB4_Output;
        byte[] send_buffer_DB4_Output;

        //Input variables
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

        //Output variables
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
        int ElevatorTimeDoorSQOPEN; //time
        int ElevatroTimeDoorSQCLOSE; //time
        bool ElevatorDoorClOSE;
        bool ElevatorDoorOPEN;
        int ElevatorCabinSpeed;
        bool ElevatorInactivity;
        int ElevatorTimeToGetDown; //time

        #endregion

        public Program1Form(ChooseOptionForm chooseOptionFormInstance)
        {
            InitializeComponent();

            this.chooseOptionFormInstance = chooseOptionFormInstance;

            client = chooseOptionFormInstance.client;

            //buffers
            //DB11 => Maintain_DB
            read_buffer_DB11 = chooseOptionFormInstance.read_buffer_DB11;
            send_buffer_DB11 = chooseOptionFormInstance.send_buffer_DB11;
            //DB4 => Elevator_DB
            read_buffer_DB4_Input = chooseOptionFormInstance.read_buffer_DB4_Input;
            send_buffer_DB4_Input = chooseOptionFormInstance.send_buffer_DB4_Input;
            read_buffer_DB4_Output = chooseOptionFormInstance.read_buffer_DB4_Output;
            send_buffer_DB4_Output = chooseOptionFormInstance.send_buffer_DB4_Output;

            //Cabin door position
            panelDoorLeftX = panelDoorLeft.Location.X;
            panelDoorLeftY = panelDoorLeft.Location.Y;
            panelDoorRightX = panelDoorRight.Location.X;
            panelDoorRightY = panelDoorRight.Location.Y;
            panelCabinX = panelCabin.Location.X;
            panelCabinY = panelCabin.Location.Y;
            lblElevatorFloorX = lblElevatorFloor.Location.X;
            lblElevatorFloorY = lblElevatorFloor.Location.Y;

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
            //Set start position for cabin doors (CLOSE) 
            //Left door
            panelDoorLeftX = 410;
            panelDoorLeftY = 120;
            panelDoorLeft.Location = new System.Drawing.Point(panelDoorLeftX, panelDoorLeftY);
            //Right door 
            panelDoorRightX = 560;
            panelDoorRightY = 120;
            panelDoorRight.Location = new System.Drawing.Point(panelDoorRightX, panelDoorRightY);
            //Cabin
            panelCabinX = 35;
            panelCabinY = 15;
            lblElevatorFloorX = 40;
            lblElevatorFloorY = 53;
        }
                
        //Variables
        #region Variables

        //C# variables
        #region C# variables

        //private bool DoorOPEN = false;
        //private bool DoorCLOSE = true;

        //Variables for panels (cabin door + cabin movement)
        private int panelDoorLeftX, panelDoorLeftY;
        private int panelDoorRightX, panelDoorRightY;
        private int panelCabinX, panelCabinY;
        private int lblElevatorFloorX, lblElevatorFloorY;

        //Variables DoorSEQ time 
        public int CabinDoorClosingTime = 500;
        public int CabinDoorOpenningTime = 1000;

        //Variables cabin movement step 
        public int ElevatorStep = 10;
        public int ElevatorMovementTime = 500;
        private int currentFloor = 5;
        private int floorHeight = 120;

        public int ActualFloor;

        int ElevatorSpeedValue, InactivityTimeValue, TimeDoorOPENValue, TimeDoorCLOSEValue;

        #endregion

        #endregion

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

                ElevatorMotorON = chooseOptionFormInstance.ElevatorMotorON;
                ElevatorMotorDOWN = chooseOptionFormInstance.ElevatorMotorDOWN;
                ElevatorMotorUP = chooseOptionFormInstance.ElevatorMotorUP;
                ElevatroHoming = chooseOptionFormInstance.ElevatroHoming;
                ElevatorSystemReady = chooseOptionFormInstance.ElevatorSystemReady;
                ElevatorActualFloor = chooseOptionFormInstance.ElevatorActualFloor;
                ElevatorMoving = chooseOptionFormInstance.ElevatorMoving;
                ElevatorSystemWorking = chooseOptionFormInstance.ElevatorSystemWorking;
                ElevatorGoToFloor = chooseOptionFormInstance.ElevatorGoToFloor;
                ElevatorDirection = chooseOptionFormInstance.ElevatorDirection;
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
                ElevatorTimeDoorSQOPEN = chooseOptionFormInstance.ElevatorTimeDoorSQOPEN;
                ElevatroTimeDoorSQCLOSE = chooseOptionFormInstance.ElevatroTimeDoorSQCLOSE;
                ElevatorDoorClOSE = chooseOptionFormInstance.ElevatorDoorClOSE;
                ElevatorDoorOPEN = chooseOptionFormInstance.ElevatorDoorOPEN;
                ElevatorCabinSpeed = chooseOptionFormInstance.ElevatorCabinSpeed;
                ElevatorInactivity = chooseOptionFormInstance.ElevatorInactivity;
                ElevatorTimeToGetDown = chooseOptionFormInstance.ElevatorTimeToGetDown;

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

                errorMessageBoxShown = false;
            }
            catch (Exception ex)
            {
                ErrorSystem();

                if (!errorMessageBoxShown)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
        }
               
        #endregion

        //Work with Tia variables
        #region Work with Tia variables
        private void btnneco1_Click(object sender, EventArgs e)
        {

        }

        private void btnneco2_Click(object sender, EventArgs e)
        {

        }

        #endregion

        //movement to specific floor 
        #region Movement to specific floor 
        //btn cabin 
        #region btn cabin
        private void btnCabinFloor1_Click(object sender, EventArgs e)
        {
            Floor1movement();
        }

        private void btnCabinFloor2_Click(object sender, EventArgs e)
        {
            Floor2movement();
        }

        private void btnCabinFloor3_Click(object sender, EventArgs e)
        {
            Floor3movement();
        }

        private void btnCabinFloor4_Click(object sender, EventArgs e)
        {
            Floor4movement();
        }

        private void btnCabinFloor5_Click(object sender, EventArgs e)
        {
            Floor5movement();
        }
        #endregion

        //btn Floor
        #region btn floor => cabin movement 

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

        #endregion

        //Move to Floor 
        #region Move to floor

        #region Move to floor 1
        private async void Floor1movement()
        {
            if (ActualFloor == 1)
            {
                //Opendoor;
            }
            else if (ActualFloor == 2)
            {
                //posun o určitý počet pixelů 
                for (int i = 0; i < 6; i++)
                {
                    userControlElevatorCabin1.MoveDown();
                    await Task.Delay(500);
                }
            }
            else if (ActualFloor == 3)
            {
                //posun o určitý počet pixelů 
                for (int i = 0; i < 6; i++)
                {
                    userControlElevatorCabin1.MoveDown();
                    await Task.Delay(500);
                }
            }
            else if (ActualFloor == 4)
            {
                //posun o určitý počet pixelů 
                for (int i = 0; i < 6; i++)
                {
                    userControlElevatorCabin1.MoveDown();
                    await Task.Delay(500);
                }
            }
            else if (ActualFloor == 5)
            {
                //posun o určitý počet pixelů 
                for (int i = 0; i < 6; i++)
                {
                    userControlElevatorCabin1.MoveDown();
                    await Task.Delay(500);
                }
            }
        }

        #endregion

        #region Move to floor 2
        private async void Floor2movement()
        {
            if (ActualFloor == 1)
            {
                //Opendoor;
            }
            else if (ActualFloor == 2)
            {
                //posun o určitý počet pixelů 
                for (int i = 0; i < 6; i++)
                {
                    userControlElevatorCabin1.MoveDown();
                    await Task.Delay(500);
                }
            }
            else if (ActualFloor == 3)
            {
                //posun o určitý počet pixelů 
                for (int i = 0; i < 6; i++)
                {
                    userControlElevatorCabin1.MoveDown();
                    await Task.Delay(500);
                }
            }
            else if (ActualFloor == 4)
            {
                //posun o určitý počet pixelů 
                for (int i = 0; i < 6; i++)
                {
                    userControlElevatorCabin1.MoveDown();
                    await Task.Delay(500);
                }
            }
            else if (ActualFloor == 5)
            {
                //posun o určitý počet pixelů 
                for (int i = 0; i < 6; i++)
                {
                    userControlElevatorCabin1.MoveDown();
                    await Task.Delay(500);
                }
            }
        }
        #endregion

        #region Move to floor 3
        private async void Floor3movement()
        {
            if (ActualFloor == 1)
            {
                //Opendoor;
            }
            else if (ActualFloor == 2)
            {
                //posun o určitý počet pixelů 
                for (int i = 0; i < 6; i++)
                {
                    userControlElevatorCabin1.MoveDown();
                    await Task.Delay(500);
                }
            }
            else if (ActualFloor == 3)
            {
                //posun o určitý počet pixelů 
                for (int i = 0; i < 6; i++)
                {
                    userControlElevatorCabin1.MoveDown();
                    await Task.Delay(500);
                }
            }
            else if (ActualFloor == 4)
            {
                //posun o určitý počet pixelů 
                for (int i = 0; i < 6; i++)
                {
                    userControlElevatorCabin1.MoveDown();
                    await Task.Delay(500);
                }
            }
            else if (ActualFloor == 5)
            {
                //posun o určitý počet pixelů 
                for (int i = 0; i < 6; i++)
                {
                    userControlElevatorCabin1.MoveDown();
                    await Task.Delay(500);
                }
            }
        }
        #endregion

        #region Move to floor 4
        private async void Floor4movement()
        {
            if (ActualFloor == 1)
            {
                //Opendoor;
            }
            else if (ActualFloor == 2)
            {
                //posun o určitý počet pixelů 
                for (int i = 0; i < 6; i++)
                {
                    userControlElevatorCabin1.MoveDown();
                    await Task.Delay(500);
                }
            }
            else if (ActualFloor == 3)
            {
                //posun o určitý počet pixelů 
                for (int i = 0; i < 6; i++)
                {
                    userControlElevatorCabin1.MoveDown();
                    await Task.Delay(500);
                }
            }
            else if (ActualFloor == 4)
            {
                //posun o určitý počet pixelů 
                for (int i = 0; i < 6; i++)
                {
                    userControlElevatorCabin1.MoveDown();
                    await Task.Delay(500);
                }
            }
            else if (ActualFloor == 5)
            {
                //posun o určitý počet pixelů 
                for (int i = 0; i < 6; i++)
                {
                    userControlElevatorCabin1.MoveDown();
                    await Task.Delay(500);
                }
            }
        }
        #endregion

        #region Move to floor 5
        private async void Floor5movement()
        {
            if (ActualFloor == 1)
            {
                //Opendoor;
            }
            else if (ActualFloor == 2)
            {
                //posun o určitý počet pixelů 
                for (int i = 0; i < 6; i++)
                {
                    userControlElevatorCabin1.MoveDown();
                    await Task.Delay(500);
                }
            }
            else if (ActualFloor == 3)
            {
                //posun o určitý počet pixelů 
                for (int i = 0; i < 6; i++)
                {
                    userControlElevatorCabin1.MoveDown();
                    await Task.Delay(500);
                }
            }
            else if (ActualFloor == 4)
            {
                //posun o určitý počet pixelů 
                for (int i = 0; i < 6; i++)
                {
                    userControlElevatorCabin1.MoveDown();
                    await Task.Delay(500);
                }
            }
            else if (ActualFloor == 5)
            {
                //posun o určitý počet pixelů 
                for (int i = 0; i < 6; i++)
                {
                    userControlElevatorCabin1.MoveDown();
                    await Task.Delay(500);
                }
            }
        }
        #endregion

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
            lblElevatorCabin.Text = "Closing door";
            statusStripElevator.Items.Clear();
            ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Closing door");
            statusStripElevator.Items.Add(lblStatus);

            //panelDoorLeft.Left = panelDoorLeft.Width;
            //panelDoorRight.Right = panelDoorRight.Width;

            await Task.Delay(CabinDoorClosingTime);
            panelDoorLeftX += 50;
            panelDoorRightX -= 50;
            panelDoorLeft.Location = new System.Drawing.Point(panelDoorLeftX, panelDoorLeftY);
            panelDoorRight.Location = new System.Drawing.Point(panelDoorRightX, panelDoorRightY);
            await Task.Delay(CabinDoorClosingTime);
            panelDoorLeftX += 50;
            panelDoorRightX -= 50;
            panelDoorLeft.Location = new System.Drawing.Point(panelDoorLeftX, panelDoorLeftY);
            panelDoorRight.Location = new System.Drawing.Point(panelDoorRightX, panelDoorRightY);
            await Task.Delay(CabinDoorClosingTime);
            panelDoorLeftX += 50;
            panelDoorRightX -= 50;
            panelDoorLeft.Location = new System.Drawing.Point(panelDoorLeftX, panelDoorLeftY);
            panelDoorRight.Location = new System.Drawing.Point(panelDoorRightX, panelDoorRightY);
            await Task.Delay(CabinDoorClosingTime);


            //DoorOPEN = false;
            lblElevatorCabin.Text = "Door closed";
            statusStripElevator.Items.Clear();
            lblStatus = new ToolStripStatusLabel("Door closed");
            statusStripElevator.Items.Add(lblStatus);
        }

        private async void OpenDOOR()
        {
            statusStripElevator.Items.Clear();
            ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Openning door");
            statusStripElevator.Items.Add(lblStatus);
            lblElevatorCabin.Text = "Openning door";

            //panelDoorLeft.Left = 0;
            //panelDoorRight.Right = 0;

            await Task.Delay(CabinDoorOpenningTime);
            panelDoorLeftX -= 50;
            panelDoorRightX += 50;
            panelDoorLeft.Location = new System.Drawing.Point(panelDoorLeftX, panelDoorLeftY);
            panelDoorRight.Location = new System.Drawing.Point(panelDoorRightX, panelDoorRightY);
            await Task.Delay(CabinDoorOpenningTime);
            panelDoorLeftX -= 50;
            panelDoorRightX += 50;
            panelDoorLeft.Location = new System.Drawing.Point(panelDoorLeftX, panelDoorLeftY);
            panelDoorRight.Location = new System.Drawing.Point(panelDoorRightX, panelDoorRightY);
            await Task.Delay(CabinDoorOpenningTime);
            panelDoorLeftX -= 50;
            panelDoorRightX += 50;
            panelDoorLeft.Location = new System.Drawing.Point(panelDoorLeftX, panelDoorLeftY);
            panelDoorRight.Location = new System.Drawing.Point(panelDoorRightX, panelDoorRightY);
            await Task.Delay(CabinDoorOpenningTime);

            //DoorOPEN = true;
            lblElevatorCabin.Text = "Door open";
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
            S7.SetBitAt(send_buffer_DB4_Input, 1, 4, ElevatorEmergencySTOP);

            //write to PLC
            int writeResultDB4_Input = client.DBWrite(DBNumber_DB4, 0, send_buffer_DB4_Input.Length, send_buffer_DB4_Input);
            if (writeResultDB4_Input != 0)
            {
                //write error
                if (!errorMessageBoxShown)
                {
                    //MessageBox
                    MessageBox.Show("BE doesn't work properly. Data could´t be written to DB4!!! \n\n" +
                        $"Error message: {writeResultDB4_Input} \n", "Error",
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
            S7.SetBitAt(send_buffer_DB4_Input, 1, 5, ElevatorErrorSystem);

            //write to PLC
            int writeResultDB4_Input = client.DBWrite(DBNumber_DB4, 0, send_buffer_DB4_Input.Length, send_buffer_DB4_Input);
            if (writeResultDB4_Input != 0)
            {
                //write error
                if (!errorMessageBoxShown)
                {
                    //MessageBox
                    MessageBox.Show("BE doesn't work properly. Data could´t be written to DB4!!! \n\n" +
                        $"Error message: {writeResultDB4_Input} \n", "Error",
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
            int writeResultDB11 = client.DBWrite(DBNumber_DB11, 0, send_buffer_DB11.Length, send_buffer_DB11);
            if (writeResultDB11 != 0)
            {
                //write error
                if (!errorMessageBoxShown)
                {
                    //MessageBox
                    MessageBox.Show("BE doesn't work properly. Data could´t be written to DB11!!! \n\n" +
                        $"Error message: {writeResultDB11} \n", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                    errorMessageBoxShown = true;
                }
            }
            else
            {
                //write was successful
                this.Close();
            }

            //stop timer
            //Timer_read_from_PLC.Stop();            
        }
        #endregion

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        //ElevatorCabin - parametets and position
        #region ElevatorCabin - parameters and position

        #region Movemnent
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

        #region Parameters
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

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
