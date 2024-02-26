using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bc_prace.Controls;
using Bc_prace.Controls.MyGraphControl.Entities;
using Bc_prace.Settings;
using Sharp7;
using static System.Windows.Forms.Design.AxImporter;
using System.Security.Cryptography;

namespace Bc_prace
{
    public partial class Program3Form : Form
    {
        private ChooseOptionForm chooseOptionFormInstance;
        private UserControlCrossroad userControlCrossroad;

        public S7Client client;

        private bool errorMessageBoxShown = false;

        //DB11 => Maintain_DB -> 1 struct -> 3 variables -> size 0.2
        private int DBNumber_DB11 = 11;
        byte[] read_buffer_DB11;
        byte[] send_buffer_DB11;

        //MaintainDB variables
        bool Option3 = false;

        //DB14 => Crossroad_DB -> 11 structs -> x variables -> size 110.0 
        private int DBNumber_DB14 = 14;
        //first struct -> Input -> 5 variables -> size 0.4
        public byte[] read_buffer_DB14_Input;
        public byte[] previous_buffer_DB14_Input;
        public byte[] PreviousBufferHash_DB14_Input;
        public byte[] send_buffer_DB14_Input;
        //second struct -> Output -> 1 variable -> size 2.0
        public byte[] read_buffer_DB14_Output;
        public byte[] send_buffer_DB14_Output;
        //other structs are Timers 

        //DB1 => Crossroad_1_DB -> Crossroad 1 -> 2 structs -> 25 variables -> size 6.3
        private int DBNumber_DB1 = 1;
        //first struct -> Input -> 4 variables -> size 0.3
        public byte[] read_buffer_DB1_Input;
        public byte[] previous_buffer_DB1_Input;
        public byte[] PreviousBufferHash_DB1_Input;
        public byte[] send_buffer_DB1_Input;
        //second struct -> Output -> 21 variables -> size 6.3 
        public byte[] read_buffer_DB1_Output;
        public byte[] send_buffer_DB1_Output;

        //DB19 => Crossroad_2_DB -> Crossroad 2 -> 2 structs -> 25 variables -> size 6.3  
        private int DBNumber_DB19 = 19;
        //first struct -> Input -> 4 variables -> size 0.3
        public byte[] read_buffer_DB19_Input;
        public byte[] previous_buffer_DB19_Input;
        public byte[] PreviousBufferHash_DB19_Input;
        public byte[] send_buffer_DB19_Input;
        //second struct -> Output -> 21 variables -> size 6.3  
        public byte[] read_buffer_DB19_Output;
        public byte[] send_buffer_DB19_Output;

        //DB20 => Crossroad_LeftT_DB - Left T -> 2 structs -> 16 variables -> size 5.4 
        private int DBNumber_DB20 = 20;
        //first struct -> Input -> 2 variables -> size 0.1
        public byte[] read_buffer_DB20_Input;
        public byte[] previous_buffer_DB20_Input;
        public byte[] PreviousBufferHash_DB20_Input;
        public byte[] send_buffer_DB20_Input;
        //second struct -> Output -> 14 variables -> size 5.4
        public byte[] read_buffer_DB20_Output;
        public byte[] send_buffer_DB20_Output;

        //DB21 => Crossroad_RightT_DB - Right T -> 2 structs -> 16 variables -> size 5.4 
        private int DBNumber_DB21 = 21;
        //first struct -> Input -> 2 variables -> size 0.1
        public byte[] read_buffer_DB21_Input;
        public byte[] previous_buffer_DB21_Input;
        public byte[] PreviousBufferHash_DB21_Input;
        public byte[] send_buffer_DB21_Input;
        //second struct -> Output -> 14 variables -> size 5.4
        public byte[] read_buffer_DB21_Output;
        public byte[] send_buffer_DB21_Output;

        //Input variables
        #region Input variables 

        //Crossroad_DB DB14
        #region Crossroad_DB DB14

        bool CrossroadModeOFF;
        bool CrossroadModeNIGHT;
        bool CrossroadModeDAY;
        bool CrossroadEmergencySTOP;
        bool CrossroadErrorSystem;

        #endregion

        //Crossroad_1_DB DB1
        #region Crossroad_1_DB DB1

        bool Crossroad1LeftCrosswalkBTN1;
        bool Crossroad1LeftCrosswalkBTN2;
        bool Crossroad1TopCrosswalkBTN1;
        bool Crossroad1TopCrosswalkBTN2;

        #endregion

        //Crossroad_2_DB DB19
        #region Crossroad_2_DB DB19

        bool Crossroad2LeftCrosswalkBTN1;
        bool Crossroad2LeftCrosswalkBTN2;
        bool Crossroad2RightCrosswalkBTN1;
        bool Crossroad2RightCrosswalkBTN2;

        #endregion

        //Crossroad_LeftT_DB DB20
        #region Crossroad_LeftT_DB DB20

        bool CrossroadLeftTLeftCrosswalkBTN1;
        bool CrossroadLeftTLeftCrosswalkBTN2;

        #endregion

        //Crossroad_RightT_DB DB21
        #region Crossroad_RightT_DB DB21

        bool CrossroadRightTTopCrosswalkBTN1;
        bool CrossroadRightTTopCrosswalkBTN2;

        #endregion

        #endregion

        //Output variables
        #region Output variables 

        //Crossroad_DB DB14
        #region Crossroad_DB DB14

        int TrafficLightsSQ;

        #endregion

        //Crossroad_1_DB DB1
        #region Crossroad_1_DB DB1

        int Crossroad1CrosswalkSQ;

        bool Crossroad1TopRED;
        bool Crossroad1TopGREEN;
        bool Crossroad1TopYELLOW;
        bool Crossroad1LeftRED;
        bool Crossroad1LeftGREEN;
        bool Crossroad1LeftYELLOW;
        bool Crossroad1RightRED;
        bool Crossroad1RightGREEN;
        bool Crossroad1RightYELLOW;
        bool Crossroad1BottomRED;
        bool Crossroad1BottomGREEN;
        bool Crossroad1BottomYELLOW;

        bool Crossroad1TopCrosswalkRED1;
        bool Crossroad1TopCrosswalkRED2;
        bool Crossroad1TopCrosswalkGREEN1;
        bool Crossroad1TopCrosswalkGREEN2;
        bool Crossroad1LeftCrosswalkRED1;
        bool Crossroad1LeftCrosswalkRED2;
        bool Crossroad1LeftCrosswalkGREEN1;
        bool Crossroad1LeftCrosswalkGREEN2;

        #endregion

        //Crossroad_2_DB DB19
        #region Crossroad_2_DB DB19

        int Crossroad2CrosswalkSQ;

        bool Crossroad2TopRED;
        bool Crossroad2TopGREEN;
        bool Crossroad2TopYellow;
        bool Crossroad2LeftRED;
        bool Crossroad2LeftGREEN;
        bool Crossroad2LeftYellow;
        bool Crossroad2RightRED;
        bool Crossroad2RightGREEN;
        bool Crossroad2RightYellow;
        bool Crossroad2BottomRED;
        bool Crossroad2BottomGREEN;
        bool Crossroad2BottomYellow;

        bool Crossroad2LeftCrosswalkRED1;
        bool Crossroad2LeftCrosswalkRED2;
        bool Crossroad2LeftCrosswalkGREEN1;
        bool Crossroad2LeftCrosswalkGREEN2;
        bool Crossroad2RightCrosswalkRED1;
        bool Crossroad2RightCrosswalkRED2;
        bool Crossroad2RightCrosswalkGREEN1;
        bool Crossroad2RightCrosswalkGREEN2;

        #endregion

        //Crossroad_LeftT_DB DB20
        #region Crossroad_LeftT_DB DB20

        int CrossroadLeftTCrosswalkSQ;

        bool CrossroadLeftTTopRED;
        bool CrossroadLeftTTopGREEN;
        bool CrossroadLeftTTopYellow;
        bool CrossroadLeftTLeftRED;
        bool CrossroadLeftTLeftGREEN;
        bool CrossroadLeftTLeftYellow;
        bool CrossroadLeftTRightRED;
        bool CrossroadLeftTRightGREEN;
        bool CrossroadLeftTRightYellow;

        bool CrossroadLeftTLeftCrosswalkRED1;
        bool CrossroadLeftTLeftCrosswalkRED2;
        bool CrossroadLeftTLeftCrosswalkGREEN1;
        bool CrossroadLeftTLeftCrosswalkGREEN2;

        #endregion

        //Crossroad_RightT_DB DB21
        #region Crossroad_RightT_DB DB21

        int CrossroadRightTCrosswalkSQ;

        bool CrossroadRightTTopRED;
        bool CrossroadRightTTopGREEN;
        bool CrossroadRightTTopYellow;
        bool CrossroadRightTLeftRED;
        bool CrossroadRightTLeftGREEN;
        bool CrossroadRightTLeftYellow;
        bool CrossroadRightTRightRED;
        bool CrossroadRightTRightGREEN;
        bool CrossroadRightTRightYellow;

        bool CrossroadRightTTopCrosswalkRED1;
        bool CrossroadRightTTopCrosswalkRED2;
        bool CrossroadRightTTopCrosswalkGREEN1;
        bool CrossroadRightTTopCrosswalkGREEN2;

        #endregion

        #endregion

        public Program3Form(ChooseOptionForm chooseOptionFormInstance)
        {
            InitializeComponent();

            //Adding UserControlCrossroad
            userControlCrossroad = new UserControlCrossroad(this);
            Controls.Add(userControlCrossroad);
            userControlCrossroad.ButtonClicked += UserControlCrossroad_ButtonClicked;
            
            this.chooseOptionFormInstance = chooseOptionFormInstance;

            client = chooseOptionFormInstance.client;

            //buffers 
            //DB11 => Maintain_DB
            read_buffer_DB11 = chooseOptionFormInstance.read_buffer_DB11;
            send_buffer_DB11 = chooseOptionFormInstance.send_buffer_DB11;
            //DB14 => Crossroad_DB
            read_buffer_DB14_Input = chooseOptionFormInstance.read_buffer_DB14_Input;
            send_buffer_DB14_Input = chooseOptionFormInstance.send_buffer_DB14_Input;
            read_buffer_DB14_Output = chooseOptionFormInstance.read_buffer_DB14_Output;
            send_buffer_DB14_Output = chooseOptionFormInstance.send_buffer_DB14_Output;
            //DB1 => Crossroad_1_DB
            read_buffer_DB1_Input = chooseOptionFormInstance.read_buffer_DB1_Input;
            send_buffer_DB1_Input = chooseOptionFormInstance.send_buffer_DB1_Input;
            read_buffer_DB1_Output = chooseOptionFormInstance.read_buffer_DB1_Output;
            send_buffer_DB1_Output = chooseOptionFormInstance.send_buffer_DB1_Output;
            //DB19 => Crossroad_2_DB
            read_buffer_DB19_Input = chooseOptionFormInstance.read_buffer_DB19_Input;
            send_buffer_DB19_Input = chooseOptionFormInstance.send_buffer_DB19_Input;
            read_buffer_DB19_Output = chooseOptionFormInstance.read_buffer_DB19_Output;
            send_buffer_DB19_Output = chooseOptionFormInstance.send_buffer_DB19_Output;
            //DB20 => Crossroad_LeftT_DB
            read_buffer_DB20_Input = chooseOptionFormInstance.read_buffer_DB20_Input;
            send_buffer_DB20_Input = chooseOptionFormInstance.send_buffer_DB20_Input;
            read_buffer_DB20_Output = chooseOptionFormInstance.read_buffer_DB20_Output;
            send_buffer_DB20_Output = chooseOptionFormInstance.send_buffer_DB20_Output;
            //DB21 => Crossroad_RightT_DB
            read_buffer_DB21_Input = chooseOptionFormInstance.read_buffer_DB21_Input;
            send_buffer_DB21_Input = chooseOptionFormInstance.send_buffer_DB21_Input;
            read_buffer_DB21_Output = chooseOptionFormInstance.read_buffer_DB21_Output;
            send_buffer_DB21_Output = chooseOptionFormInstance.send_buffer_DB21_Output;
                        
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
                Option3 = chooseOptionFormInstance.Option3;

                //Input variables
                #region Input variables 

                //Crossroad_DB DB14
                #region Crossroad_DB DB14

                CrossroadModeOFF = chooseOptionFormInstance.CrossroadModeOFF;
                CrossroadModeNIGHT = chooseOptionFormInstance.CrossroadModeNIGHT;
                CrossroadModeDAY = chooseOptionFormInstance.CrossroadModeDAY;
                CrossroadEmergencySTOP = chooseOptionFormInstance.CrossroadEmergencySTOP;
                CrossroadErrorSystem = chooseOptionFormInstance.CrossroadErrorSystem;

                #endregion

                //Crossroad_1_DB DB1
                #region Crossroad_1_DB DB1

                Crossroad1LeftCrosswalkBTN1 = chooseOptionFormInstance.Crossroad1LeftCrosswalkBTN1;
                Crossroad1LeftCrosswalkBTN2 = chooseOptionFormInstance.Crossroad1LeftCrosswalkBTN2;
                Crossroad1TopCrosswalkBTN1 = chooseOptionFormInstance.Crossroad1TopCrosswalkBTN1;
                Crossroad1TopCrosswalkBTN2 = chooseOptionFormInstance.Crossroad1TopCrosswalkBTN2;

                #endregion

                //Crossroad_2_DB DB19
                #region Crossroad_2_DB DB19

                Crossroad2LeftCrosswalkBTN1 = chooseOptionFormInstance.Crossroad2LeftCrosswalkBTN1;
                Crossroad2LeftCrosswalkBTN2 = chooseOptionFormInstance.Crossroad2LeftCrosswalkBTN2;
                Crossroad2RightCrosswalkBTN1 = chooseOptionFormInstance.Crossroad2RightCrosswalkBTN1;
                Crossroad2RightCrosswalkBTN2 = chooseOptionFormInstance.Crossroad2RightCrosswalkBTN2;

                #endregion

                //Crossroad_LeftT_DB DB20
                #region Crossroad_LeftT_DB DB20

                CrossroadLeftTLeftCrosswalkBTN1 = chooseOptionFormInstance.CrossroadLeftTLeftCrosswalkBTN1;
                CrossroadLeftTLeftCrosswalkBTN2 = chooseOptionFormInstance.CrossroadLeftTLeftCrosswalkBTN2;

                #endregion

                //Crossroad_RightT_DB DB21
                #region Crossroad_RightT_DB DB21

                CrossroadRightTTopCrosswalkBTN1 = chooseOptionFormInstance.CrossroadRightTTopCrosswalkBTN1;
                CrossroadRightTTopCrosswalkBTN2 = chooseOptionFormInstance.CrossroadRightTTopCrosswalkBTN2;

                #endregion

                #endregion

                //Output variables
                #region Output variables 

                //Crossroad_DB DB14
                #region Crossroad_DB DB14

                TrafficLightsSQ = chooseOptionFormInstance.TrafficLightsSQ;

                #endregion

                //Crossroad_1_DB DB1
                #region Crossroad_1_DB DB1

                Crossroad1CrosswalkSQ = chooseOptionFormInstance.Crossroad1CrosswalkSQ;

                Crossroad1TopRED = chooseOptionFormInstance.Crossroad1TopRED;
                Crossroad1TopGREEN = chooseOptionFormInstance.Crossroad1TopGREEN;
                Crossroad1TopYELLOW = chooseOptionFormInstance.Crossroad1TopYELLOW;
                Crossroad1LeftRED = chooseOptionFormInstance.Crossroad1LeftRED;
                Crossroad1LeftGREEN = chooseOptionFormInstance.Crossroad1LeftGREEN;
                Crossroad1LeftYELLOW = chooseOptionFormInstance.Crossroad1LeftYELLOW;
                Crossroad1RightRED = chooseOptionFormInstance.Crossroad1RightRED;
                Crossroad1RightGREEN = chooseOptionFormInstance.Crossroad1RightGREEN;
                Crossroad1RightYELLOW = chooseOptionFormInstance.Crossroad1RightYELLOW;
                Crossroad1BottomRED = chooseOptionFormInstance.Crossroad1BottomRED;
                Crossroad1BottomGREEN = chooseOptionFormInstance.Crossroad1BottomGREEN;
                Crossroad1BottomYELLOW = chooseOptionFormInstance.Crossroad1BottomYELLOW;

                Crossroad1TopCrosswalkRED1 = chooseOptionFormInstance.Crossroad1TopCrosswalkRED1;
                Crossroad1TopCrosswalkRED2 = chooseOptionFormInstance.Crossroad1TopCrosswalkRED2;
                Crossroad1TopCrosswalkGREEN1 = chooseOptionFormInstance.Crossroad1TopCrosswalkGREEN1;
                Crossroad1TopCrosswalkGREEN2 = chooseOptionFormInstance.Crossroad1TopCrosswalkGREEN2;
                Crossroad1LeftCrosswalkRED1 = chooseOptionFormInstance.Crossroad1LeftCrosswalkRED1;
                Crossroad1LeftCrosswalkRED2 = chooseOptionFormInstance.Crossroad1LeftCrosswalkRED2;
                Crossroad1LeftCrosswalkGREEN1 = chooseOptionFormInstance.Crossroad1LeftCrosswalkGREEN1;
                Crossroad1LeftCrosswalkGREEN2 = chooseOptionFormInstance.Crossroad1LeftCrosswalkGREEN2;

                #endregion

                //Crossroad_2_DB DB19
                #region Crossroad_2_DB DB19

                Crossroad2CrosswalkSQ = chooseOptionFormInstance.Crossroad2CrosswalkSQ;

                Crossroad2TopRED = chooseOptionFormInstance.Crossroad2TopRED;
                Crossroad2TopGREEN = chooseOptionFormInstance.Crossroad2TopGREEN;
                Crossroad2TopYellow = chooseOptionFormInstance.Crossroad2TopYellow;
                Crossroad2LeftRED = chooseOptionFormInstance.Crossroad2LeftRED;
                Crossroad2LeftGREEN = chooseOptionFormInstance.Crossroad2LeftGREEN;
                Crossroad2LeftYellow = chooseOptionFormInstance.Crossroad2LeftYellow;
                Crossroad2RightRED = chooseOptionFormInstance.Crossroad2RightRED;
                Crossroad2RightGREEN = chooseOptionFormInstance.Crossroad2RightGREEN;
                Crossroad2RightYellow = chooseOptionFormInstance.Crossroad2RightYellow;
                Crossroad2BottomRED = chooseOptionFormInstance.Crossroad2BottomRED;
                Crossroad2BottomGREEN = chooseOptionFormInstance.Crossroad2BottomGREEN;
                Crossroad2BottomYellow = chooseOptionFormInstance.Crossroad2BottomYellow;

                Crossroad2LeftCrosswalkRED1 = chooseOptionFormInstance.Crossroad2LeftCrosswalkRED1;
                Crossroad2LeftCrosswalkRED2 = chooseOptionFormInstance.Crossroad2LeftCrosswalkRED2;
                Crossroad2LeftCrosswalkGREEN1 = chooseOptionFormInstance.Crossroad2LeftCrosswalkGREEN1;
                Crossroad2LeftCrosswalkGREEN2 = chooseOptionFormInstance.Crossroad2LeftCrosswalkGREEN2;
                Crossroad2RightCrosswalkRED1 = chooseOptionFormInstance.Crossroad2RightCrosswalkRED1;
                Crossroad2RightCrosswalkRED2 = chooseOptionFormInstance.Crossroad2RightCrosswalkRED2;
                Crossroad2RightCrosswalkGREEN1 = chooseOptionFormInstance.Crossroad2RightCrosswalkGREEN1;
                Crossroad2RightCrosswalkGREEN2 = chooseOptionFormInstance.Crossroad2RightCrosswalkGREEN2;

                #endregion

                //Crossroad_LeftT_DB DB20
                #region Crossroad_LeftT_DB DB20

                CrossroadLeftTCrosswalkSQ = chooseOptionFormInstance.CrossroadLeftTCrosswalkSQ;

                CrossroadLeftTTopRED = chooseOptionFormInstance.CrossroadLeftTTopRED;
                CrossroadLeftTTopGREEN = chooseOptionFormInstance.CrossroadLeftTTopGREEN;
                CrossroadLeftTTopYellow = chooseOptionFormInstance.CrossroadLeftTTopYellow;
                CrossroadLeftTLeftRED = chooseOptionFormInstance.CrossroadLeftTLeftRED;
                CrossroadLeftTLeftGREEN = chooseOptionFormInstance.CrossroadLeftTLeftGREEN;
                CrossroadLeftTLeftYellow = chooseOptionFormInstance.CrossroadLeftTLeftYellow;
                CrossroadLeftTRightRED = chooseOptionFormInstance.CrossroadLeftTRightRED;
                CrossroadLeftTRightGREEN = chooseOptionFormInstance.CrossroadLeftTRightGREEN;
                CrossroadLeftTRightYellow = chooseOptionFormInstance.CrossroadLeftTRightYellow;

                CrossroadLeftTLeftCrosswalkRED1 = chooseOptionFormInstance.CrossroadLeftTLeftCrosswalkRED1;
                CrossroadLeftTLeftCrosswalkRED2 = chooseOptionFormInstance.CrossroadLeftTLeftCrosswalkRED2;
                CrossroadLeftTLeftCrosswalkGREEN1 = chooseOptionFormInstance.CrossroadLeftTLeftCrosswalkGREEN1;
                CrossroadLeftTLeftCrosswalkGREEN2 = chooseOptionFormInstance.CrossroadLeftTLeftCrosswalkGREEN2;

                #endregion

                //Crossroad_RightT_DB DB21
                #region Crossroad_RightT_DB DB21

                CrossroadRightTCrosswalkSQ = chooseOptionFormInstance.CrossroadRightTCrosswalkSQ;

                CrossroadRightTTopRED = chooseOptionFormInstance.CrossroadRightTTopRED;
                CrossroadRightTTopGREEN = chooseOptionFormInstance.CrossroadRightTTopGREEN;
                CrossroadRightTTopYellow = chooseOptionFormInstance.CrossroadRightTTopYellow;
                CrossroadRightTLeftRED = chooseOptionFormInstance.CrossroadRightTLeftRED;
                CrossroadRightTLeftGREEN = chooseOptionFormInstance.CrossroadRightTLeftGREEN;
                CrossroadRightTLeftYellow = chooseOptionFormInstance.CrossroadRightTLeftYellow;
                CrossroadRightTRightRED = chooseOptionFormInstance.CrossroadRightTRightRED;
                CrossroadRightTRightGREEN = chooseOptionFormInstance.CrossroadRightTRightGREEN;
                CrossroadRightTRightYellow = chooseOptionFormInstance.CrossroadRightTRightYellow;

                CrossroadRightTTopCrosswalkRED1 = chooseOptionFormInstance.CrossroadRightTTopCrosswalkRED1;
                CrossroadRightTTopCrosswalkRED2 = chooseOptionFormInstance.CrossroadRightTTopCrosswalkRED2;
                CrossroadRightTTopCrosswalkGREEN1 = chooseOptionFormInstance.CrossroadRightTTopCrosswalkGREEN1;
                CrossroadRightTTopCrosswalkGREEN2 = chooseOptionFormInstance.CrossroadRightTTopCrosswalkGREEN2;

                #endregion

                #endregion

                //Reading variables with MultiVar method
                /*
                #region Multi read -> MultiVar

                S7MultiVar reader = new S7MultiVar(client);

                //DB14 => Crossroad_DB - modes and timers
                #region Reading from DB14 Crossroad_DB
                //DB14 => Crossroad_DB -> 11 structs -> x variables -> size 110.0
                if (previous_buffer_DB14_Input == null)
                {
                    previous_buffer_DB14_Input = new byte[read_buffer_DB14_Input.Length];
                    Array.Copy(read_buffer_DB14_Input, previous_buffer_DB14_Input, read_buffer_DB14_Input.Length);

                    // Inicializace hashe při prvním spuštění
                    PreviousBufferHash_DB14_Input = ComputeHash(read_buffer_DB14_Input);
                }

                //first struct -> Input -> 5 variables -> size 0.4
                reader.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, DBNumber_DB14, 0, 0, ref read_buffer_DB14_Input); //read_buffer_DB14_Input.Length
                //second struct -> Output -> 1 variable -> size 2.0
                reader.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, DBNumber_DB14, 0, 0, ref read_buffer_DB14_Output); //read_buffer_DB14_Output.Length
                //other structs are Timers 

                int readResultDB14 = reader.Read();

                if (readResultDB14 == 0)
                {
                    byte[] currentHashDB14_Input = ComputeHash(read_buffer_DB14_Input);

                    // Porovnání hashe s předchozím hashem
                    if (!ArraysAreEqual(currentHashDB14_Input, PreviousBufferHash_DB14_Input))
                    {
                        // Aktualizace předchozího bufferu a hashe
                        Array.Copy(read_buffer_DB14_Input, previous_buffer_DB14_Input, read_buffer_DB14_Input.Length);
                        PreviousBufferHash_DB14_Input = currentHashDB14_Input;

                        // Aktualizace proměnných na základě nových dat

                        //Input variables
                        #region Input variables

                        CrossroadModeOFF = S7.GetBitAt(read_buffer_DB14_Input, 0, 0);
                        CrossroadModeNIGHT = S7.GetBitAt(read_buffer_DB14_Input, 0, 1);
                        CrossroadModeDAY = S7.GetBitAt(read_buffer_DB14_Input, 0, 2);
                        CrossroadEmergencySTOP = S7.GetBitAt(read_buffer_DB14_Input, 0, 3);
                        CrossroadErrorSystem = S7.GetBitAt(read_buffer_DB14_Input, 0, 4);

                        #endregion

                        //Output variables
                        #region Output variables 

                        TrafficLightsSQ = S7.GetIntAt(read_buffer_DB14_Output, 2);

                        #endregion

                        errorMessageBoxShown = false;
                    }

                    //Input variables
                    #region Input variables

                    CrossroadModeOFF = S7.GetBitAt(read_buffer_DB14_Input, 0, 0);
                    CrossroadModeNIGHT = S7.GetBitAt(read_buffer_DB14_Input, 0, 1);
                    CrossroadModeDAY = S7.GetBitAt(read_buffer_DB14_Input, 0, 2);
                    CrossroadEmergencySTOP = S7.GetBitAt(read_buffer_DB14_Input, 0, 3);
                    CrossroadErrorSystem = S7.GetBitAt(read_buffer_DB14_Input, 0, 4);

                    #endregion

                    //Output variables
                    #region Output variables 

                    TrafficLightsSQ = S7.GetIntAt(read_buffer_DB14_Output, 2);

                    #endregion

                    //other structs are Timers

                    errorMessageBoxShown = false;
                }
                else
                {
                    //error
                    statusStripCrossroad.Items.Clear();
                    ToolStripStatusLabel lblStatus1 = new ToolStripStatusLabel("Variables were not read.");
                    statusStripCrossroad.Items.Add(lblStatus1);

                    if (!errorMessageBoxShown)
                    {
                        errorMessageBoxShown = true;    

                        //MessageBox
                        MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data weren't read from DB14!!! \n\n" +
                            $"Error message {readResultDB14} \n", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }

                #endregion

                //DB1 => Crossroad_1_DB - Crossroad 1
                #region Reading from DB1 Crossroad_1_DB
                //DB1 => Crossroad_1_DB -> Crossroad 1 -> 2 structs -> 25 variables -> size 6.3
                if (previous_buffer_DB1_Input == null)
                {
                    previous_buffer_DB1_Input = new byte[read_buffer_DB1_Input.Length];
                    Array.Copy(read_buffer_DB1_Input, previous_buffer_DB1_Input, read_buffer_DB1_Input.Length);

                    // Inicializace hashe při prvním spuštění
                    PreviousBufferHash_DB1_Input = ComputeHash(read_buffer_DB1_Input);
                }

                //first struct -> Input -> 4 variables -> size 0.3
                reader.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, DBNumber_DB1, 0, 0, ref read_buffer_DB1_Input); //read_buffer_DB1_Input.Length
                //second struct -> Output -> 21 variables -> size 6.3 
                reader.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, DBNumber_DB1, 0, 0, ref read_buffer_DB1_Output); //read_buffer_DB1_Output.Length

                int readResultDB1 = reader.Read();

                if (readResultDB1 == 0)
                {
                    byte[] currentHashDB1_Input = ComputeHash(read_buffer_DB1_Input);

                    // Porovnání hashe s předchozím hashem
                    if (!ArraysAreEqual(currentHashDB1_Input, PreviousBufferHash_DB1_Input))
                    {
                        // Aktualizace předchozího bufferu a hashe
                        Array.Copy(read_buffer_DB1_Input, previous_buffer_DB1_Input, read_buffer_DB1_Input.Length);
                        PreviousBufferHash_DB1_Input = currentHashDB1_Input;

                        // Aktualizace proměnných na základě nových dat

                        //Input variables
                        #region Input variables

                        Crossroad1LeftCrosswalkBTN1 = S7.GetBitAt(read_buffer_DB1_Input, 0, 0);
                        Crossroad1LeftCrosswalkBTN2 = S7.GetBitAt(read_buffer_DB1_Input, 0, 1);
                        Crossroad1TopCrosswalkBTN1 = S7.GetBitAt(read_buffer_DB1_Input, 0, 2);
                        Crossroad1TopCrosswalkBTN2 = S7.GetBitAt(read_buffer_DB1_Input, 0, 3);

                        #endregion

                        //Output variables
                        #region Output variables

                        Crossroad1CrosswalkSQ = S7.GetIntAt(read_buffer_DB1_Output, 2);
                        Crossroad1TopRED = S7.GetBitAt(read_buffer_DB1_Output, 4, 0);
                        Crossroad1TopGREEN = S7.GetBitAt(read_buffer_DB1_Output, 4, 1);
                        Crossroad1TopYELLOW = S7.GetBitAt(read_buffer_DB1_Output, 4, 2);
                        Crossroad1LeftRED = S7.GetBitAt(read_buffer_DB1_Output, 4, 3);
                        Crossroad1LeftGREEN = S7.GetBitAt(read_buffer_DB1_Output, 4, 4);
                        Crossroad1LeftYELLOW = S7.GetBitAt(read_buffer_DB1_Output, 4, 5);
                        Crossroad1RightRED = S7.GetBitAt(read_buffer_DB1_Output, 4, 6);
                        Crossroad1RightGREEN = S7.GetBitAt(read_buffer_DB1_Output, 4, 7);
                        Crossroad1RightYELLOW = S7.GetBitAt(read_buffer_DB1_Output, 5, 0);
                        Crossroad1BottomRED = S7.GetBitAt(read_buffer_DB1_Output, 5, 1);
                        Crossroad1BottomGREEN = S7.GetBitAt(read_buffer_DB1_Output, 5, 2);
                        Crossroad1BottomYELLOW = S7.GetBitAt(read_buffer_DB1_Output, 5, 3);
                        Crossroad1TopCrosswalkRED1 = S7.GetBitAt(read_buffer_DB1_Output, 5, 4);
                        Crossroad1TopCrosswalkRED2 = S7.GetBitAt(read_buffer_DB1_Output, 5, 5);
                        Crossroad1TopCrosswalkGREEN1 = S7.GetBitAt(read_buffer_DB1_Output, 5, 6);
                        Crossroad1TopCrosswalkGREEN2 = S7.GetBitAt(read_buffer_DB1_Output, 5, 7);
                        Crossroad1LeftCrosswalkRED1 = S7.GetBitAt(read_buffer_DB1_Output, 6, 0);
                        Crossroad1LeftCrosswalkRED2 = S7.GetBitAt(read_buffer_DB1_Output, 6, 1);
                        Crossroad1LeftCrosswalkGREEN1 = S7.GetBitAt(read_buffer_DB1_Output, 6, 2);
                        Crossroad1LeftCrosswalkGREEN2 = S7.GetBitAt(read_buffer_DB1_Output, 6, 3);

                        #endregion

                        errorMessageBoxShown = false;
                    }

                    //Input variables
                    #region Input variables

                    Crossroad1LeftCrosswalkBTN1 = S7.GetBitAt(read_buffer_DB1_Input, 0, 0);
                    Crossroad1LeftCrosswalkBTN2 = S7.GetBitAt(read_buffer_DB1_Input, 0, 1);
                    Crossroad1TopCrosswalkBTN1 = S7.GetBitAt(read_buffer_DB1_Input, 0, 2);
                    Crossroad1TopCrosswalkBTN2 = S7.GetBitAt(read_buffer_DB1_Input, 0, 3);

                    #endregion

                    //Output variables
                    #region Output variables

                    Crossroad1CrosswalkSQ = S7.GetIntAt(read_buffer_DB1_Output, 2);
                    Crossroad1TopRED = S7.GetBitAt(read_buffer_DB1_Output, 4, 0);
                    Crossroad1TopGREEN = S7.GetBitAt(read_buffer_DB1_Output, 4, 1);
                    Crossroad1TopYELLOW = S7.GetBitAt(read_buffer_DB1_Output, 4, 2);
                    Crossroad1LeftRED = S7.GetBitAt(read_buffer_DB1_Output, 4, 3);
                    Crossroad1LeftGREEN = S7.GetBitAt(read_buffer_DB1_Output, 4, 4);
                    Crossroad1LeftYELLOW = S7.GetBitAt(read_buffer_DB1_Output, 4, 5);
                    Crossroad1RightRED = S7.GetBitAt(read_buffer_DB1_Output, 4, 6);
                    Crossroad1RightGREEN = S7.GetBitAt(read_buffer_DB1_Output, 4, 7);
                    Crossroad1RightYELLOW = S7.GetBitAt(read_buffer_DB1_Output, 5, 0);
                    Crossroad1BottomRED = S7.GetBitAt(read_buffer_DB1_Output, 5, 1);
                    Crossroad1BottomGREEN = S7.GetBitAt(read_buffer_DB1_Output, 5, 2);
                    Crossroad1BottomYELLOW = S7.GetBitAt(read_buffer_DB1_Output, 5, 3);
                    Crossroad1TopCrosswalkRED1 = S7.GetBitAt(read_buffer_DB1_Output, 5, 4);
                    Crossroad1TopCrosswalkRED2 = S7.GetBitAt(read_buffer_DB1_Output, 5, 5);
                    Crossroad1TopCrosswalkGREEN1 = S7.GetBitAt(read_buffer_DB1_Output, 5, 6);
                    Crossroad1TopCrosswalkGREEN2 = S7.GetBitAt(read_buffer_DB1_Output, 5, 7);
                    Crossroad1LeftCrosswalkRED1 = S7.GetBitAt(read_buffer_DB1_Output, 6, 0);
                    Crossroad1LeftCrosswalkRED2 = S7.GetBitAt(read_buffer_DB1_Output, 6, 1);
                    Crossroad1LeftCrosswalkGREEN1 = S7.GetBitAt(read_buffer_DB1_Output, 6, 2);
                    Crossroad1LeftCrosswalkGREEN2 = S7.GetBitAt(read_buffer_DB1_Output, 6, 3);

                    #endregion                    

                    errorMessageBoxShown = false;

                }
                else
                {
                    //error
                    statusStripCrossroad.Items.Clear();
                    ToolStripStatusLabel lblStatus1 = new ToolStripStatusLabel("Variables were not read.");
                    statusStripCrossroad.Items.Add(lblStatus1);

                    if (!errorMessageBoxShown)
                    {
                        errorMessageBoxShown = true;

                        //MessageBox
                        MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data weren't read from DB1!!! \n\n" +
                            $"Error message {readResultDB1} \n", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }

                #endregion

                //DB19 => Crossroad_2_DB - Crossroad 2 
                #region Reading from DB19 Crossroad_2_DB
                //DB19 => Crossroad_2_DB -> Crossroad 2 -> 2 structs -> 25 variables -> size 6.3  
                if (previous_buffer_DB19_Input == null)
                {
                    previous_buffer_DB19_Input = new byte[read_buffer_DB19_Input.Length];
                    Array.Copy(read_buffer_DB19_Input, previous_buffer_DB19_Input, read_buffer_DB19_Input.Length);

                    // Inicializace hashe při prvním spuštění
                    PreviousBufferHash_DB19_Input = ComputeHash(read_buffer_DB19_Input);
                }

                //first struct -> Input -> 4 variables -> size 0.3
                reader.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, DBNumber_DB19, 0, 0, ref read_buffer_DB19_Input); //read_buffer_DB19_Input.Length
                //second struct -> Output -> 21 variables -> size 6.3
                reader.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, DBNumber_DB19, 0, 0, ref read_buffer_DB19_Output); //read_buffer_DB19_Input.Length

                int readResultDB19 = reader.Read();

                if (readResultDB19 == 0)
                {
                    byte[] currentHashDB19_Input = ComputeHash(read_buffer_DB19_Input);

                    // Porovnání hashe s předchozím hashem
                    if (!ArraysAreEqual(currentHashDB19_Input, PreviousBufferHash_DB19_Input))
                    {
                        // Aktualizace předchozího bufferu a hashe
                        Array.Copy(read_buffer_DB19_Input, previous_buffer_DB19_Input, read_buffer_DB19_Input.Length);
                        PreviousBufferHash_DB19_Input = currentHashDB19_Input;

                        // Aktualizace proměnných na základě nových dat

                        //Input variables
                        #region Input variables

                        Crossroad2LeftCrosswalkBTN1 = S7.GetBitAt(read_buffer_DB19_Input, 0, 0);
                        Crossroad2LeftCrosswalkBTN2 = S7.GetBitAt(read_buffer_DB19_Input, 0, 1);
                        Crossroad2RightCrosswalkBTN1 = S7.GetBitAt(read_buffer_DB19_Input, 0, 2);
                        Crossroad2RightCrosswalkBTN2 = S7.GetBitAt(read_buffer_DB19_Input, 0, 3);

                        #endregion

                        //Output variables
                        #region Output variables

                        Crossroad2CrosswalkSQ = S7.GetIntAt(read_buffer_DB19_Output, 2);
                        Crossroad2TopRED = S7.GetBitAt(read_buffer_DB19_Output, 4, 0);
                        Crossroad2TopGREEN = S7.GetBitAt(read_buffer_DB19_Output, 4, 1);
                        Crossroad2TopYellow = S7.GetBitAt(read_buffer_DB19_Output, 4, 2);
                        Crossroad2LeftRED = S7.GetBitAt(read_buffer_DB19_Output, 4, 3);
                        Crossroad2LeftGREEN = S7.GetBitAt(read_buffer_DB19_Output, 4, 4);
                        Crossroad2LeftYellow = S7.GetBitAt(read_buffer_DB19_Output, 4, 5);
                        Crossroad2RightRED = S7.GetBitAt(read_buffer_DB19_Output, 4, 6);
                        Crossroad2RightGREEN = S7.GetBitAt(read_buffer_DB19_Output, 4, 7);
                        Crossroad2RightYellow = S7.GetBitAt(read_buffer_DB19_Output, 5, 0);
                        Crossroad2BottomRED = S7.GetBitAt(read_buffer_DB19_Output, 5, 1);
                        Crossroad2BottomGREEN = S7.GetBitAt(read_buffer_DB19_Output, 5, 2);
                        Crossroad2BottomYellow = S7.GetBitAt(read_buffer_DB19_Output, 5, 3);
                        Crossroad2RightCrosswalkRED1 = S7.GetBitAt(read_buffer_DB19_Output, 5, 4);
                        Crossroad2RightCrosswalkRED2 = S7.GetBitAt(read_buffer_DB19_Output, 5, 5);
                        Crossroad2RightCrosswalkGREEN1 = S7.GetBitAt(read_buffer_DB19_Output, 5, 6);
                        Crossroad2RightCrosswalkGREEN2 = S7.GetBitAt(read_buffer_DB19_Output, 5, 7);
                        Crossroad2LeftCrosswalkRED1 = S7.GetBitAt(read_buffer_DB19_Output, 6, 0);
                        Crossroad2LeftCrosswalkRED2 = S7.GetBitAt(read_buffer_DB19_Output, 6, 1);
                        Crossroad2LeftCrosswalkGREEN1 = S7.GetBitAt(read_buffer_DB19_Output, 6, 2);
                        Crossroad2LeftCrosswalkGREEN2 = S7.GetBitAt(read_buffer_DB19_Output, 6, 3);

                        #endregion

                        errorMessageBoxShown = false;
                    }

                    //Input variables
                    #region Input variables

                    Crossroad2LeftCrosswalkBTN1 = S7.GetBitAt(read_buffer_DB19_Input, 0, 0);
                    Crossroad2LeftCrosswalkBTN2 = S7.GetBitAt(read_buffer_DB19_Input, 0, 1);
                    Crossroad2RightCrosswalkBTN1 = S7.GetBitAt(read_buffer_DB19_Input, 0, 2);
                    Crossroad2RightCrosswalkBTN2 = S7.GetBitAt(read_buffer_DB19_Input, 0, 3);

                    #endregion

                    //Output variables
                    #region Output variables

                    Crossroad2CrosswalkSQ = S7.GetIntAt(read_buffer_DB19_Output, 2);
                    Crossroad2TopRED = S7.GetBitAt(read_buffer_DB19_Output, 4, 0);
                    Crossroad2TopGREEN = S7.GetBitAt(read_buffer_DB19_Output, 4, 1);
                    Crossroad2TopYellow = S7.GetBitAt(read_buffer_DB19_Output, 4, 2);
                    Crossroad2LeftRED = S7.GetBitAt(read_buffer_DB19_Output, 4, 3);
                    Crossroad2LeftGREEN = S7.GetBitAt(read_buffer_DB19_Output, 4, 4);
                    Crossroad2LeftYellow = S7.GetBitAt(read_buffer_DB19_Output, 4, 5);
                    Crossroad2RightRED = S7.GetBitAt(read_buffer_DB19_Output, 4, 6);
                    Crossroad2RightGREEN = S7.GetBitAt(read_buffer_DB19_Output, 4, 7);
                    Crossroad2RightYellow = S7.GetBitAt(read_buffer_DB19_Output, 5, 0);
                    Crossroad2BottomRED = S7.GetBitAt(read_buffer_DB19_Output, 5, 1);
                    Crossroad2BottomGREEN = S7.GetBitAt(read_buffer_DB19_Output, 5, 2);
                    Crossroad2BottomYellow = S7.GetBitAt(read_buffer_DB19_Output, 5, 3);
                    Crossroad2RightCrosswalkRED1 = S7.GetBitAt(read_buffer_DB19_Output, 5, 4);
                    Crossroad2RightCrosswalkRED2 = S7.GetBitAt(read_buffer_DB19_Output, 5, 5);
                    Crossroad2RightCrosswalkGREEN1 = S7.GetBitAt(read_buffer_DB19_Output, 5, 6);
                    Crossroad2RightCrosswalkGREEN2 = S7.GetBitAt(read_buffer_DB19_Output, 5, 7);
                    Crossroad2LeftCrosswalkRED1 = S7.GetBitAt(read_buffer_DB19_Output, 6, 0);
                    Crossroad2LeftCrosswalkRED2 = S7.GetBitAt(read_buffer_DB19_Output, 6, 1);
                    Crossroad2LeftCrosswalkGREEN1 = S7.GetBitAt(read_buffer_DB19_Output, 6, 2);
                    Crossroad2LeftCrosswalkGREEN2 = S7.GetBitAt(read_buffer_DB19_Output, 6, 3);

                    #endregion

                    errorMessageBoxShown = false;

                }
                else
                {
                    //error
                    statusStripCrossroad.Items.Clear();
                    ToolStripStatusLabel lblStatus1 = new ToolStripStatusLabel("Variables were not read.");
                    statusStripCrossroad.Items.Add(lblStatus1);

                    if (!errorMessageBoxShown)
                    {
                        errorMessageBoxShown = true;

                        //MessageBox
                        MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data weren't read from DB19!!! \n\n" +
                            $"Error message {readResultDB19} \n", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }

                #endregion

                //DB20 => Crossroad_LeftT_DB - Left T 
                #region Reading from DB20 Crossroad_LeftT_DB
                //DB20 => Crossroad_LeftT_DB - Left T -> 2 structs -> 16 variables -> size 5.4 
                if (previous_buffer_DB20_Input == null)
                {
                    previous_buffer_DB20_Input = new byte[read_buffer_DB20_Input.Length];
                    Array.Copy(read_buffer_DB20_Input, previous_buffer_DB20_Input, read_buffer_DB20_Input.Length);

                    // Inicializace hashe při prvním spuštění
                    PreviousBufferHash_DB20_Input = ComputeHash(read_buffer_DB20_Input);
                }

                //first struct -> Input -> 2 variables -> size 0.1
                reader.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, DBNumber_DB20, 0, 0, ref read_buffer_DB20_Input); //read_buffer_DB20_Input.Length
                //second struct -> Output -> 14 variables -> size 5.
                reader.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, DBNumber_DB20, 0, 0, ref read_buffer_DB20_Output); //read_buffer_DB20_Output.Length

                int readResultDB20 = reader.Read();

                if (readResultDB20 == 0)
                {
                    byte[] currentHashDB20_Input = ComputeHash(read_buffer_DB20_Input);

                    // Porovnání hashe s předchozím hashem
                    if (!ArraysAreEqual(currentHashDB20_Input, PreviousBufferHash_DB20_Input))
                    {
                        // Aktualizace předchozího bufferu a hashe
                        Array.Copy(read_buffer_DB20_Input, previous_buffer_DB20_Input, read_buffer_DB20_Input.Length);
                        PreviousBufferHash_DB20_Input = currentHashDB20_Input;

                        // Aktualizace proměnných na základě nových dat

                        //Input variables
                        #region Input variables

                        CrossroadLeftTLeftCrosswalkBTN1 = S7.GetBitAt(read_buffer_DB20_Input, 0, 0);
                        CrossroadLeftTLeftCrosswalkBTN2 = S7.GetBitAt(read_buffer_DB20_Input, 0, 1);

                        #endregion

                        //Output variables
                        #region Output variables

                        CrossroadLeftTCrosswalkSQ = S7.GetIntAt(read_buffer_DB20_Output, 2);
                        CrossroadLeftTTopRED = S7.GetBitAt(read_buffer_DB20_Output, 4, 0);
                        CrossroadLeftTTopGREEN = S7.GetBitAt(read_buffer_DB20_Output, 4, 1);
                        CrossroadLeftTTopYellow = S7.GetBitAt(read_buffer_DB20_Output, 4, 2);
                        CrossroadLeftTLeftRED = S7.GetBitAt(read_buffer_DB20_Output, 4, 3);
                        CrossroadLeftTLeftGREEN = S7.GetBitAt(read_buffer_DB20_Output, 4, 4);
                        CrossroadLeftTLeftYellow = S7.GetBitAt(read_buffer_DB20_Output, 4, 5);
                        CrossroadLeftTRightRED = S7.GetBitAt(read_buffer_DB20_Output, 4, 6);
                        CrossroadLeftTRightGREEN = S7.GetBitAt(read_buffer_DB20_Output, 4, 7);
                        CrossroadLeftTRightYellow = S7.GetBitAt(read_buffer_DB20_Output, 5, 0);
                        CrossroadLeftTLeftCrosswalkRED1 = S7.GetBitAt(read_buffer_DB20_Output, 5, 1);
                        CrossroadLeftTLeftCrosswalkRED2 = S7.GetBitAt(read_buffer_DB20_Output, 5, 2);
                        CrossroadLeftTLeftCrosswalkGREEN1 = S7.GetBitAt(read_buffer_DB20_Output, 5, 3);
                        CrossroadLeftTLeftCrosswalkGREEN2 = S7.GetBitAt(read_buffer_DB20_Output, 5, 4);

                        #endregion

                        errorMessageBoxShown = false;
                    }

                    //Input variables
                    #region Input variables

                    CrossroadLeftTLeftCrosswalkBTN1 = S7.GetBitAt(read_buffer_DB20_Input, 0, 0);
                    CrossroadLeftTLeftCrosswalkBTN2 = S7.GetBitAt(read_buffer_DB20_Input, 0, 1);

                    #endregion

                    //Output variables
                    #region Output variables

                    CrossroadLeftTCrosswalkSQ = S7.GetIntAt(read_buffer_DB20_Output, 2);
                    CrossroadLeftTTopRED = S7.GetBitAt(read_buffer_DB20_Output, 4, 0);
                    CrossroadLeftTTopGREEN = S7.GetBitAt(read_buffer_DB20_Output, 4, 1);
                    CrossroadLeftTTopYellow = S7.GetBitAt(read_buffer_DB20_Output, 4, 2);
                    CrossroadLeftTLeftRED = S7.GetBitAt(read_buffer_DB20_Output, 4, 3);
                    CrossroadLeftTLeftGREEN = S7.GetBitAt(read_buffer_DB20_Output, 4, 4);
                    CrossroadLeftTLeftYellow = S7.GetBitAt(read_buffer_DB20_Output, 4, 5);
                    CrossroadLeftTRightRED = S7.GetBitAt(read_buffer_DB20_Output, 4, 6);
                    CrossroadLeftTRightGREEN = S7.GetBitAt(read_buffer_DB20_Output, 4, 7);
                    CrossroadLeftTRightYellow = S7.GetBitAt(read_buffer_DB20_Output, 5, 0);
                    CrossroadLeftTLeftCrosswalkRED1 = S7.GetBitAt(read_buffer_DB20_Output, 5, 1);
                    CrossroadLeftTLeftCrosswalkRED2 = S7.GetBitAt(read_buffer_DB20_Output, 5, 2);
                    CrossroadLeftTLeftCrosswalkGREEN1 = S7.GetBitAt(read_buffer_DB20_Output, 5, 3);
                    CrossroadLeftTLeftCrosswalkGREEN2 = S7.GetBitAt(read_buffer_DB20_Output, 5, 4);

                    #endregion

                    errorMessageBoxShown = false;
                }
                else
                {
                    //error
                    statusStripCrossroad.Items.Clear();
                    ToolStripStatusLabel lblStatus1 = new ToolStripStatusLabel("Variables were not read.");
                    statusStripCrossroad.Items.Add(lblStatus1);

                    if (!errorMessageBoxShown)
                    {
                        errorMessageBoxShown = true;

                        //MessageBox
                        MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data weren't read from DB20!!! \n\n" +
                            $"Error message {readResultDB20} \n", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }

                #endregion

                //DB21 => Crossroad_RightT_DB - Right T
                #region Reading from DB21 Crossroad_RightT_DB
                //DB21 => Crossroad_RightT_DB - Right T -> 2 structs -> 16 variables -> size 5.4 
                if (previous_buffer_DB21_Input == null)
                {
                    previous_buffer_DB21_Input = new byte[read_buffer_DB21_Input.Length];
                    Array.Copy(read_buffer_DB21_Input, previous_buffer_DB21_Input, read_buffer_DB21_Input.Length);

                    // Inicializace hashe při prvním spuštění
                    PreviousBufferHash_DB21_Input = ComputeHash(read_buffer_DB21_Input);
                }

                //first struct -> Input -> 2 variables -> size 0.1
                reader.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, DBNumber_DB21, 0, 0, ref read_buffer_DB21_Input); //read_buffer_DB21_Input.Length
                //second struct -> Output -> 14 variables -> size 5.4
                reader.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, DBNumber_DB21, 0, 0, ref read_buffer_DB21_Output); //read_buffer_DB21_Output.Length

                int readResultDB21 = reader.Read();

                if (readResultDB21 == 0)
                {
                    byte[] currentHashDB21_Input = ComputeHash(read_buffer_DB21_Input);

                    // Porovnání hashe s předchozím hashem
                    if (!ArraysAreEqual(currentHashDB21_Input, PreviousBufferHash_DB21_Input))
                    {
                        // Aktualizace předchozího bufferu a hashe
                        Array.Copy(read_buffer_DB21_Input, previous_buffer_DB21_Input, read_buffer_DB21_Input.Length);
                        PreviousBufferHash_DB21_Input = currentHashDB21_Input;

                        // Aktualizace proměnných na základě nových dat

                        //Input variables
                        #region Input variables

                        CrossroadRightTTopCrosswalkBTN1 = S7.GetBitAt(read_buffer_DB21_Input, 0, 0);
                        CrossroadRightTTopCrosswalkBTN2 = S7.GetBitAt(read_buffer_DB21_Input, 0, 1);

                        #endregion

                        //Output variables
                        #region Output variables

                        CrossroadRightTCrosswalkSQ = S7.GetIntAt(read_buffer_DB21_Output, 2);
                        CrossroadRightTTopRED = S7.GetBitAt(read_buffer_DB21_Output, 4, 0);
                        CrossroadRightTTopGREEN = S7.GetBitAt(read_buffer_DB21_Output, 4, 1);
                        CrossroadRightTTopYellow = S7.GetBitAt(read_buffer_DB21_Output, 4, 2);
                        CrossroadRightTLeftRED = S7.GetBitAt(read_buffer_DB21_Output, 4, 3);
                        CrossroadRightTLeftGREEN = S7.GetBitAt(read_buffer_DB21_Output, 4, 4);
                        CrossroadRightTLeftYellow = S7.GetBitAt(read_buffer_DB21_Output, 4, 5);
                        CrossroadRightTRightRED = S7.GetBitAt(read_buffer_DB21_Output, 4, 6);
                        CrossroadRightTRightGREEN = S7.GetBitAt(read_buffer_DB21_Output, 4, 7);
                        CrossroadRightTRightYellow = S7.GetBitAt(read_buffer_DB21_Output, 5, 0);
                        CrossroadRightTTopCrosswalkRED1 = S7.GetBitAt(read_buffer_DB21_Output, 5, 1);
                        CrossroadRightTTopCrosswalkRED2 = S7.GetBitAt(read_buffer_DB21_Output, 5, 2);
                        CrossroadRightTTopCrosswalkGREEN1 = S7.GetBitAt(read_buffer_DB21_Output, 5, 3);
                        CrossroadRightTTopCrosswalkGREEN2 = S7.GetBitAt(read_buffer_DB21_Output, 5, 4);

                        #endregion

                        errorMessageBoxShown = false;
                    }

                    //Input variables
                    #region Input variables

                    CrossroadRightTTopCrosswalkBTN1 = S7.GetBitAt(read_buffer_DB21_Input, 0, 0);
                    CrossroadRightTTopCrosswalkBTN2 = S7.GetBitAt(read_buffer_DB21_Input, 0, 1);

                    #endregion

                    //Output variables
                    #region Output variables

                    CrossroadRightTCrosswalkSQ = S7.GetIntAt(read_buffer_DB21_Output, 2);
                    CrossroadRightTTopRED = S7.GetBitAt(read_buffer_DB21_Output, 4, 0);
                    CrossroadRightTTopGREEN = S7.GetBitAt(read_buffer_DB21_Output, 4, 1);
                    CrossroadRightTTopYellow = S7.GetBitAt(read_buffer_DB21_Output, 4, 2);
                    CrossroadRightTLeftRED = S7.GetBitAt(read_buffer_DB21_Output, 4, 3);
                    CrossroadRightTLeftGREEN = S7.GetBitAt(read_buffer_DB21_Output, 4, 4);
                    CrossroadRightTLeftYellow = S7.GetBitAt(read_buffer_DB21_Output, 4, 5);
                    CrossroadRightTRightRED = S7.GetBitAt(read_buffer_DB21_Output, 4, 6);
                    CrossroadRightTRightGREEN = S7.GetBitAt(read_buffer_DB21_Output, 4, 7);
                    CrossroadRightTRightYellow = S7.GetBitAt(read_buffer_DB21_Output, 5, 0);
                    CrossroadRightTTopCrosswalkRED1 = S7.GetBitAt(read_buffer_DB21_Output, 5, 1);
                    CrossroadRightTTopCrosswalkRED2 = S7.GetBitAt(read_buffer_DB21_Output, 5, 2);
                    CrossroadRightTTopCrosswalkGREEN1 = S7.GetBitAt(read_buffer_DB21_Output, 5, 3);
                    CrossroadRightTTopCrosswalkGREEN2 = S7.GetBitAt(read_buffer_DB21_Output, 5, 4);

                    #endregion

                    errorMessageBoxShown = false;

                }
                else
                {
                    //error
                    statusStripCrossroad.Items.Clear();
                    ToolStripStatusLabel lblStatus1 = new ToolStripStatusLabel("Variables were not read.");
                    statusStripCrossroad.Items.Add(lblStatus1);

                    if (!errorMessageBoxShown)
                    {
                        errorMessageBoxShown = true;

                        //MessageBox
                        MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data weren't read from DB21!!! \n\n" +
                            $"Error message {readResultDB21} \n", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }

                #endregion

                #endregion
                */

                //Reading variables with DBRead method
                /*
                #region DBRead

                //DB14 => Crossroad_DB - modes and timers
                #region Reading from DB14 Crossroad_DB

                //DB14 => Crossroad_DB -> 11 structs -> x variables -> size 110.0
                int readResultDB14 = client.DBRead(DBNumber_DB14, 0, read_buffer_DB14.Length, read_buffer_DB14);
                if (readResultDB14 != 0)
                {
                    //error
                    statusStripCrossroad.Items.Clear();
                    ToolStripStatusLabel lblStatus1 = new ToolStripStatusLabel("Variables were not read.");
                    statusStripCrossroad.Items.Add(lblStatus1);

                    if (!errorMessageBoxShown)
                    {
                        errorMessageBoxShown = true;

                        //MessageBox
                        MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data weren't read from DB14!!! \n\n" +
                            $"Error message {readResultDB14} \n", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }
                else
                {
                    //Input variables
                    #region Input variables
                    
                    CrossroadModeOFF = S7.GetBitAt(read_buffer_DB14, 0, 0);
                    CrossroadModeNIGHT = S7.GetBitAt(read_buffer_DB14, 0, 1);
                    CrossroadModeDAY = S7.GetBitAt(read_buffer_DB14, 0, 2);
                    CrossroadEmergencySTOP = S7.GetBitAt(read_buffer_DB14, 0, 3);
                    CrossroadErrorSystem = S7.GetBitAt(read_buffer_DB14, 0, 4);

                    #endregion

                    //Output variables
                    #region Output variables 

                    TrafficLightsSQ = S7.GetIntAt(read_buffer_DB14_Output, 2);

                    #endregion

                    //Timers
                    #region Timers

                    #endregion

                }

                #endregion

                //DB1 => Crossroad_1_DB - Crossroad 1
                #region Reading from DB1 Crossroad_1_DB

                //DB1 => Crossroad_1_DB -> Crossroad 1 -> 2 structs -> 25 variables -> size 6.3
                int readResultDB1 = client.DBRead(DBNumber_DB1, 0, read_buffer_DB1.Length, read_buffer_DB1);
                if (readResultDB1 != 0)
                {
                    //error
                    statusStripCrossroad.Items.Clear();
                    ToolStripStatusLabel lblStatus1 = new ToolStripStatusLabel("Variables were not read.");
                    statusStripCrossroad.Items.Add(lblStatus1);

                    if (!errorMessageBoxShown)
                    {
                        errorMessageBoxShown = true;

                        //MessageBox
                        MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data weren't read from DB1!!! \n\n" +
                            $"Error message {readResultDB1} \n", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }
                else
                {
                    //Input variable
                    #region Input variables

                    Crossroad1LeftCrosswalkBTN1 = S7.GetBitAt(read_buffer_DB1, 0, 0); 
                    Crossroad1LeftCrosswalkBTN2 = S7.GetBitAt(read_buffer_DB1, 0, 1);
                    Crossroad1TopCrosswalkBTN1 = S7.GetBitAt(read_buffer_DB1, 0, 2);
                    Crossroad1TopCrosswalkBTN2 = S7.GetBitAt(read_buffer_DB1, 0, 3);

                    #endregion

                    //Output variables
                    #region Output variables

                    Crossroad1CrosswalkSQ = S7.GetIntAt(read_buffer_DB1_Output, 2);
                    Crossroad1TopRED = S7.GetBitAt(read_buffer_DB1_Output, 4, 0); 
                    Crossroad1TopGREEN = S7.GetBitAt(read_buffer_DB1_Output, 4, 1);
                    Crossroad1TopYELLOW = S7.GetBitAt(read_buffer_DB1_Output, 4, 2);
                    Crossroad1LeftRED = S7.GetBitAt(read_buffer_DB1_Output, 4, 3);
                    Crossroad1LeftGREEN = S7.GetBitAt(read_buffer_DB1_Output, 4, 4);
                    Crossroad1LeftYELLOW = S7.GetBitAt(read_buffer_DB1_Output, 4, 5);
                    Crossroad1RightRED = S7.GetBitAt(read_buffer_DB1_Output, 4, 6);
                    Crossroad1RightGREEN = S7.GetBitAt(read_buffer_DB1_Output, 4, 7);
                    Crossroad1RightYELLOW = S7.GetBitAt(read_buffer_DB1_Output, 5, 0);
                    Crossroad1BottomRED = S7.GetBitAt(read_buffer_DB1_Output, 5, 1);
                    Crossroad1BottomGREEN = S7.GetBitAt(read_buffer_DB1_Output, 5, 2);
                    Crossroad1BottomYELLOW = S7.GetBitAt(read_buffer_DB1_Output, 5, 3);
                    Crossroad1TopCrosswalkRED1 = S7.GetBitAt(read_buffer_DB1_Output, 5, 4);
                    Crossroad1TopCrosswalkRED2 = S7.GetBitAt(read_buffer_DB1_Output, 5, 5);
                    Crossroad1TopCrosswalkGREEN1 = S7.GetBitAt(read_buffer_DB1_Output, 5, 6);
                    Crossroad1TopCrosswalkGREEN2 = S7.GetBitAt(read_buffer_DB1_Output, 5, 7);
                    Crossroad1LeftCrosswalkRED1 = S7.GetBitAt(read_buffer_DB1_Output, 6, 0);
                    Crossroad1LeftCrosswalkRED2 = S7.GetBitAt(read_buffer_DB1_Output, 6, 1);
                    Crossroad1LeftCrosswalkGREEN1 = S7.GetBitAt(read_buffer_DB1_Output, 6, 2);
                    Crossroad1LeftCrosswalkGREEN2 = S7.GetBitAt(read_buffer_DB1_Output, 6, 3);

                    #endregion
                }

                #endregion

                //DB19 => Crossroad_2_DB - Crossroad 2 
                #region Reading from DB19 Crossroad_2_DB

                //DB19 => Crossroad_2_DB -> Crossroad 2 -> 2 structs -> 25 variables -> size 6.3 
                int readResultDB19 = client.DBRead(DBNumber_DB19, 0, read_buffer_DB19.Length, read_buffer_DB19);
                if (readResultDB19 != 0)
                {
                    //error
                    statusStripCrossroad.Items.Clear();
                    ToolStripStatusLabel lblStatus1 = new ToolStripStatusLabel("Variables were not read.");
                    statusStripCrossroad.Items.Add(lblStatus1);

                    if (!errorMessageBoxShown)
                    {
                        errorMessageBoxShown = true;

                        //MessageBox
                        MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data weren't read from DB19!!! \n\n" +
                            $"Error message {readResultDB19} \n", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }
                else
                {
                    //Input variable
                    #region Input variables

                    Crossroad2LeftCrosswalkBTN1 = S7.GetBitAt(read_buffer_DB19_Input, 0, 0);
                    Crossroad2LeftCrosswalkBTN2 = S7.GetBitAt(read_buffer_DB19_Input, 0, 1);
                    Crossroad2TopCrosswalkBTN1 = S7.GetBitAt(read_buffer_DB19_Input, 0, 2);
                    Crossroad2TopCrosswalkBTN2 = S7.GetBitAt(read_buffer_DB19_Input, 0, 3);

                    #endregion

                    //Output variables
                    #region Output variables

                    Crossroad2CrosswalkSQ = S7.GetIntAt(read_buffer_DB19_Output, 2);
                    Crossroad2TopRED = S7.GetBitAt(read_buffer_DB19_Output, 4, 0);
                    Crossroad2TopGREEN = S7.GetBitAt(read_buffer_DB19_Output, 4, 1);
                    Crossroad2TopYellow = S7.GetBitAt(read_buffer_DB19_Output, 4, 2);
                    Crossroad2LeftRED = S7.GetBitAt(read_buffer_DB19_Output, 4, 3);
                    Crossroad2LeftGREEN = S7.GetBitAt(read_buffer_DB19_Output, 4, 4);
                    Crossroad2LeftYellow = S7.GetBitAt(read_buffer_DB19_Output, 4, 5);
                    Crossroad2RightRED = S7.GetBitAt(read_buffer_DB19_Output, 4, 6);
                    Crossroad2RightGREEN = S7.GetBitAt(read_buffer_DB19_Output, 4, 7);
                    Crossroad2RightYellow = S7.GetBitAt(read_buffer_DB19_Output, 5, 0);
                    Crossroad2BottomRED = S7.GetBitAt(read_buffer_DB19_Output, 5, 1);
                    Crossroad2BottomGREEN = S7.GetBitAt(read_buffer_DB19_Output, 5, 2);
                    Crossroad2BottomYellow = S7.GetBitAt(read_buffer_DB19_Output, 5, 3);
                    Crossroad2RightCrosswalkRED1 = S7.GetBitAt(read_buffer_DB19_Output, 5, 4);
                    Crossroad2RightCrosswalkRED2 = S7.GetBitAt(read_buffer_DB19_Output, 5, 5);
                    Crossroad2RightCrosswalkGREEN1 = S7.GetBitAt(read_buffer_DB19_Output, 5, 6);
                    Crossroad2RightCrosswalkGREEN2 = S7.GetBitAt(read_buffer_DB19_Output, 5, 7);
                    Crossroad2LeftCrosswalkRED1 = S7.GetBitAt(read_buffer_DB19_Output, 6, 0);
                    Crossroad2LeftCrosswalkRED2 = S7.GetBitAt(read_buffer_DB19_Output, 6, 1);
                    Crossroad2LeftCrosswalkGREEN1 = S7.GetBitAt(read_buffer_DB19_Output, 6, 2);
                    Crossroad2LeftCrosswalkGREEN2 = S7.GetBitAt(read_buffer_DB19_Output, 6, 3);
                                        
                    #endregion
                }

                #endregion

                //DB20 => Crossroad_LeftT_DB - Left T 
                #region Reading from DB20 Crossroad_LeftT_DB

                //DB20 => Crossroad_LeftT_DB - Left T -> 2 structs -> 16 variables -> size 5.4 
                int readResultDB20 = client.DBRead(DBNumber_DB20, 0, read_buffer_DB20.Length, read_buffer_DB20);
                if (readResultDB20 != 0)
                {
                    //error
                    statusStripCrossroad.Items.Clear();
                    ToolStripStatusLabel lblStatus1 = new ToolStripStatusLabel("Variables were not read.");
                    statusStripCrossroad.Items.Add(lblStatus1);

                    if (!errorMessageBoxShown)
                    {
                        errorMessageBoxShown = true;

                        //MessageBox
                        MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data weren't read from DB20!!! \n\n" +
                            $"Error message {readResultDB20} \n", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }
                else
                {
                    //Input variable
                    #region Input variables

                    CrossroadLeftTLeftCrosswalkBTN1 = S7.GetBitAt(read_buffer_DB20_Input, 0, 0);
                    CrossroadLeftTLeftCrosswalkBTN2= S7.GetBitAt(read_buffer_DB20_Input, 0, 1);

                    #endregion

                    //Output variables
                    #region Output variables

                    CrossroadLeftTCrosswalkSQ = S7.GetIntAt(read_buffer_DB20_Output, 2);
                    CrossroadLeftTTopRED = S7.GetBitAt(read_buffer_DB20_Output, 4, 0);
                    CrossroadLeftTTopGREEN = S7.GetBitAt(read_buffer_DB20_Output, 4, 1);
                    CrossroadLeftTTopYellow = S7.GetBitAt(read_buffer_DB20_Output, 4, 2);
                    CrossroadLeftTLeftRED = S7.GetBitAt(read_buffer_DB20_Output, 4, 3);
                    CrossroadLeftTLeftGREEN = S7.GetBitAt(read_buffer_DB20_Output, 4, 4);
                    CrossroadLeftTLeftYellow = S7.GetBitAt(read_buffer_DB20_Output, 4, 5);
                    CrossroadLeftTRightRED = S7.GetBitAt(read_buffer_DB20_Output, 4, 6);
                    CrossroadLeftTRightGREEN = S7.GetBitAt(read_buffer_DB20_Output, 4, 7);
                    CrossroadLeftTRightYellow = S7.GetBitAt(read_buffer_DB20_Output, 5, 0);
                    CrossroadLeftTLeftCrosswalkRED1 = S7.GetBitAt(read_buffer_DB20_Output, 5, 1);
                    CrossroadLeftTLeftCrosswalkRED2 = S7.GetBitAt(read_buffer_DB20_Output, 5, 2);
                    CrossroadLeftTLeftCrosswalkGREEN1 = S7.GetBitAt(read_buffer_DB20_Output, 5, 3);
                    CrossroadLeftTLeftCrosswalkGREEN2 = S7.GetBitAt(read_buffer_DB20_Output, 5, 4);

                    #endregion
                }

                #endregion

                //DB21 => Crossroad_RightT_DB - Right T
                #region Reading from DB21 Crossroad_RightT_DB

                //DB21 => Crossroad_RightT_DB - Right T -> 2 structs -> 16 variables -> size 5.4
                int readResultDB21 = client.DBRead(DBNumber_DB21, 0, read_buffer_DB21.Length, read_buffer_DB21);
                if (readResultDB21 != 0)
                {
                    //error
                    statusStripCrossroad.Items.Clear();
                    ToolStripStatusLabel lblStatus1 = new ToolStripStatusLabel("Variables were not read.");
                    statusStripCrossroad.Items.Add(lblStatus1);

                    if (!errorMessageBoxShown)
                    {
                        errorMessageBoxShown = true;

                        //MessageBox
                        MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data weren't read from DB21!!! \n\n" +
                            $"Error message {readResultDB21} \n", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }
                else
                {
                    //Input variable
                    #region Input variables

                    CrossroadRightTTopCrosswalkBTN1 = S7.GetBitAt(read_buffer_DB21_Input, 0, 0); 
                    CrossroadRightTTopCrosswalkBTN2 = S7.GetBitAt(read_buffer_DB21_Input, 0, 1);

                    #endregion

                    //Output variables
                    #region Output variables

                    CrossroadRightTCrosswalkSQ = S7.GetIntAt(read_buffer_DB21_Output, 2);
                    CrossroadRightTTopRED = S7.GetBitAt(read_buffer_DB21_Output, 4, 0);
                    CrossroadRightTTopGREEN = S7.GetBitAt(read_buffer_DB21_Output, 4, 1);
                    CrossroadRightTTopYellow = S7.GetBitAt(read_buffer_DB21_Output, 4, 2);
                    CrossroadRightTLeftRED = S7.GetBitAt(read_buffer_DB21_Output, 4, 3);
                    CrossroadRightTLeftGREEN = S7.GetBitAt(read_buffer_DB21_Output, 4, 4);
                    CrossroadRightTLeftYellow = S7.GetBitAt(read_buffer_DB21_Output, 4, 5);
                    CrossroadRightTRightRED = S7.GetBitAt(read_buffer_DB21_Output, 4, 6);
                    CrossroadRightTRightGREEN = S7.GetBitAt(read_buffer_DB21_Output, 4, 7);
                    CrossroadRightTRightYellow = S7.GetBitAt(read_buffer_DB21_Output, 5, 0);
                    CrossroadRightTTopCrosswalkRED1 = S7.GetBitAt(read_buffer_DB21_Output, 5, 1);
                    CrossroadRightTTopCrosswalkRED2 = S7.GetBitAt(read_buffer_DB21_Output, 5, 2);
                    CrossroadRightTTopCrosswalkGREEN1 = S7.GetBitAt(read_buffer_DB21_Output, 5, 3);
                    CrossroadRightTTopCrosswalkGREEN2 = S7.GetBitAt(read_buffer_DB21_Output, 5, 4);

                    #endregion
                }

                #endregion

                #endregion

                */

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

        private void Program3_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            rBtnCrossroadBasic.Checked = false;
            rBtnCrossroadExtension1.Checked = false;
            rBtnCrossroadExtension2.Checked = false;
            rBtnCrossroadExtension3.Checked = false;

            //Draw();

        }

        private void Draw()
        {
            if (rBtnCrossroadBasic.Checked)
            {
                // Vykreslete čáry pro křižovatku
                userControlCrossroad1.BasicCrossroad();
            }
            else if (rBtnCrossroadExtension1.Checked)
            {
                // Vykreslete čáry pro volbu 1
                userControlCrossroad1.CrossroadExtension1();
            }
            else if (rBtnCrossroadExtension2.Checked)
            {
                // Vykreslete čáry pro volbu 2
                userControlCrossroad1.CrossroadExtension2();

            }
            else if (rBtnCrossroadExtension3.Checked)
            {
                // Vykreslete čáry pro volbu 3
                userControlCrossroad1.CrossroadExtension3();

            }
            else
            {

            }
        }

        //Radiobutton clicked
        #region Radiobutton clicked
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            statusStripCrossroad.Items.Clear();
            ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Basic crossroad");
            statusStripCrossroad.Items.Add(lblStatus);

            userControlCrossroad1.BasicCrossroad();
        }

        private void rBtnCrossroadExtension1_CheckedChanged(object sender, EventArgs e)
        {
            statusStripCrossroad.Items.Clear();
            ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Crossroad extension 1");
            statusStripCrossroad.Items.Add(lblStatus);

            userControlCrossroad1.CrossroadExtension1();
        }

        private void rBtnCrossroadExtension2_CheckedChanged(object sender, EventArgs e)
        {
            statusStripCrossroad.Items.Clear();
            ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Crossroad extension 2");
            statusStripCrossroad.Items.Add(lblStatus);

            userControlCrossroad1.CrossroadExtension2();
        }

        private void rBtnCrossroadExtension3_CheckedChanged(object sender, EventArgs e)
        {
            statusStripCrossroad.Items.Clear();
            ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Crossroad extension 3");
            statusStripCrossroad.Items.Add(lblStatus);

            userControlCrossroad1.CrossroadExtension3();
        }

        #endregion

        //Buttons in UserControlCrossroad clicked event
        #region Buttons in UserControlCrossroad clicked event
        private void UserControlCrossroad_ButtonClicked(object? sender, string buttonIdentifier)
        {
            if (sender != null)
            {
                switch (buttonIdentifier)
                {
                    case "btnCrossroad1TopCrosswalkLEFT":
                        Crossroad1TopCrosswalkBTN1 = true;
                        S7.SetBitAt(send_buffer_DB1_Input, 0, 0, Crossroad1TopCrosswalkBTN1);

                        //write to PLC
                        int writeResultDB1_Crossroad1TopCrosswalkBTN1 = client.DBWrite(DBNumber_DB1, 0, send_buffer_DB1_Input.Length, send_buffer_DB1_Input);
                        if (writeResultDB1_Crossroad1TopCrosswalkBTN1 != 0)
                        {
                            //write error
                            if (!errorMessageBoxShown)
                            {
                                //MessageBox
                                MessageBox.Show("BE doesn't work properly. Data could´t be written to DB1!!! \n\n" +
                                    $"Error message: {writeResultDB1_Crossroad1TopCrosswalkBTN1} \n", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                                errorMessageBoxShown = true;
                            }
                        }
                        else
                        {
                            //write was successful
                        }
                        break;

                    case "btnCrossroad1TopCrosswalkRIGHT":
                        Crossroad1TopCrosswalkBTN2 = true;
                        S7.SetBitAt(send_buffer_DB1_Input, 0, 1, Crossroad1TopCrosswalkBTN2);

                        //write to PLC
                        int writeResultDB1_Crossroad1TopCrosswalkBTN2 = client.DBWrite(DBNumber_DB1, 0, send_buffer_DB1_Input.Length, send_buffer_DB1_Input);
                        if (writeResultDB1_Crossroad1TopCrosswalkBTN2 != 0)
                        {
                            //write error
                            if (!errorMessageBoxShown)
                            {
                                //MessageBox
                                MessageBox.Show("BE doesn't work properly. Data could´t be written to DB1!!! \n\n" +
                                    $"Error message: {writeResultDB1_Crossroad1TopCrosswalkBTN2} \n", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                                errorMessageBoxShown = true;
                            }
                        }
                        else
                        {
                            //write was successful
                        }
                        break;

                    case "btnCrossroad1LeftCrosswalkTOP":
                        Crossroad1LeftCrosswalkBTN1 = true;
                        S7.SetBitAt(send_buffer_DB1_Input, 0, 2, Crossroad1LeftCrosswalkBTN1);

                        //write to PLC
                        int writeResultDB1_Crossroad1LeftCrosswalkBTN1 = client.DBWrite(DBNumber_DB1, 0, send_buffer_DB1_Input.Length, send_buffer_DB1_Input);
                        if (writeResultDB1_Crossroad1LeftCrosswalkBTN1 != 0)
                        {
                            //write error
                            if (!errorMessageBoxShown)
                            {
                                //MessageBox
                                MessageBox.Show("BE doesn't work properly. Data could´t be written to DB1!!! \n\n" +
                                    $"Error message: {writeResultDB1_Crossroad1LeftCrosswalkBTN1} \n", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                                errorMessageBoxShown = true;
                            }
                        }
                        else
                        {
                            //write was successful
                        }
                        break;

                    case "btnCrossroad1LeftCrosswalkBOTTOM":
                        Crossroad1LeftCrosswalkBTN2 = true;
                        S7.SetBitAt(send_buffer_DB1_Input, 0, 3, Crossroad1LeftCrosswalkBTN2);

                        //write to PLC
                        int writeResultDB1_Crossroad1LeftCrosswalkBTN2 = client.DBWrite(DBNumber_DB1, 0, send_buffer_DB1_Input.Length, send_buffer_DB1_Input);
                        if (writeResultDB1_Crossroad1LeftCrosswalkBTN2 != 0)
                        {
                            //write error
                            if (!errorMessageBoxShown)
                            {
                                //MessageBox
                                MessageBox.Show("BE doesn't work properly. Data could´t be written to DB1!!! \n\n" +
                                    $"Error message: {writeResultDB1_Crossroad1LeftCrosswalkBTN2} \n", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                                errorMessageBoxShown = true;
                            }
                        }
                        else
                        {
                            //write was successful
                        }
                        break;

                    case "btnCrossroad2LeftCrosswalkTOP":
                        Crossroad2LeftCrosswalkBTN1 = true;
                        S7.SetBitAt(send_buffer_DB19_Input, 0, 0, Crossroad2LeftCrosswalkBTN1);

                        //write to PLC
                        int writeResultDB19_Crossroad2LeftCrosswalkBTN1 = client.DBWrite(DBNumber_DB19, 0, send_buffer_DB19_Input.Length, send_buffer_DB19_Input);
                        if (writeResultDB19_Crossroad2LeftCrosswalkBTN1 != 0)
                        {
                            //write error
                            if (!errorMessageBoxShown)
                            {
                                //MessageBox
                                MessageBox.Show("BE doesn't work properly. Data could´t be written to DB19!!! \n\n" +
                                    $"Error message: {writeResultDB19_Crossroad2LeftCrosswalkBTN1} \n", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                                errorMessageBoxShown = true;
                            }
                        }
                        else
                        {
                            //write was successful
                        }
                        break;

                    case "btnCrossroad2LeftCrosswalkBOTTOM":
                        Crossroad2LeftCrosswalkBTN2 = true;
                        S7.SetBitAt(send_buffer_DB19_Input, 0, 1, Crossroad2LeftCrosswalkBTN2);

                        //write to PLC
                        int writeResultDB19_Crossroad2LeftCrosswalkBTN2 = client.DBWrite(DBNumber_DB19, 0, send_buffer_DB19_Input.Length, send_buffer_DB19_Input);
                        if (writeResultDB19_Crossroad2LeftCrosswalkBTN2 != 0)
                        {
                            //write error
                            if (!errorMessageBoxShown)
                            {
                                //MessageBox
                                MessageBox.Show("BE doesn't work properly. Data could´t be written to DB19!!! \n\n" +
                                    $"Error message: {writeResultDB19_Crossroad2LeftCrosswalkBTN2} \n", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                                errorMessageBoxShown = true;
                            }
                        }
                        else
                        {
                            //write was successful
                        }
                        break;

                    case "btnCrossroad2RightCrosswalkTOP":
                        Crossroad2RightCrosswalkBTN1 = true;
                        S7.SetBitAt(send_buffer_DB19_Input, 0, 2, Crossroad2RightCrosswalkBTN1);

                        //write to PLC
                        int writeResultDB19_Crossroad2RightCrosswalkBTN1 = client.DBWrite(DBNumber_DB19, 0, send_buffer_DB19_Input.Length, send_buffer_DB19_Input);
                        if (writeResultDB19_Crossroad2RightCrosswalkBTN1 != 0)
                        {
                            //write error
                            if (!errorMessageBoxShown)
                            {
                                //MessageBox
                                MessageBox.Show("BE doesn't work properly. Data could´t be written to DB19!!! \n\n" +
                                    $"Error message: {writeResultDB19_Crossroad2RightCrosswalkBTN1} \n", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                                errorMessageBoxShown = true;
                            }
                        }
                        else
                        {
                            //write was successful
                        }
                        break;

                    case "btnCrossroad2RightCrosswalkBOTTOM":
                        Crossroad2RightCrosswalkBTN2 = true;
                        S7.SetBitAt(send_buffer_DB19_Input, 0, 3, Crossroad2RightCrosswalkBTN2);

                        //write to PLC
                        int writeResultDB19_Crossroad2RightCrosswalkBTN2 = client.DBWrite(DBNumber_DB19, 0, send_buffer_DB19_Input.Length, send_buffer_DB19_Input);
                        if (writeResultDB19_Crossroad2RightCrosswalkBTN2 != 0)
                        {
                            //write error
                            if (!errorMessageBoxShown)
                            {
                                //MessageBox
                                MessageBox.Show("BE doesn't work properly. Data could´t be written to DB19!!! \n\n" +
                                    $"Error message: {writeResultDB19_Crossroad2RightCrosswalkBTN2} \n", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                                errorMessageBoxShown = true;
                            }
                        }
                        else
                        {
                            //write was successful
                        }
                        break;

                    case "btnLeftTLeftCrosswalkTOP":
                        CrossroadLeftTLeftCrosswalkBTN1 = true;
                        S7.SetBitAt(send_buffer_DB20_Input, 0, 0, CrossroadLeftTLeftCrosswalkBTN1);

                        //write to PLC
                        int writeResultDB20_CrossroadLeftTLeftCrosswalkBTN1 = client.DBWrite(DBNumber_DB20, 0, send_buffer_DB20_Input.Length, send_buffer_DB20_Input);
                        if (writeResultDB20_CrossroadLeftTLeftCrosswalkBTN1 != 0)
                        {
                            //write error
                            if (!errorMessageBoxShown)
                            {
                                //MessageBox
                                MessageBox.Show("BE doesn't work properly. Data could´t be written to DB20!!! \n\n" +
                                    $"Error message: {writeResultDB20_CrossroadLeftTLeftCrosswalkBTN1} \n", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                                errorMessageBoxShown = true;
                            }
                        }
                        else
                        {
                            //write was successful
                        }
                        break;

                    case "btnLeftTLeftCrosswalkBOTTOM":
                        CrossroadLeftTLeftCrosswalkBTN2 = true;
                        S7.SetBitAt(send_buffer_DB20_Input, 0, 1, Crossroad2RightCrosswalkBTN2);

                        //write to PLC
                        int writeResultDB20_CrossroadLeftTLeftCrosswalkBTN2 = client.DBWrite(DBNumber_DB20, 0, send_buffer_DB20_Input.Length, send_buffer_DB20_Input);
                        if (writeResultDB20_CrossroadLeftTLeftCrosswalkBTN2 != 0)
                        {
                            //write error
                            if (!errorMessageBoxShown)
                            {
                                //MessageBox
                                MessageBox.Show("BE doesn't work properly. Data could´t be written to DB20!!! \n\n" +
                                    $"Error message: {writeResultDB20_CrossroadLeftTLeftCrosswalkBTN2} \n", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                                errorMessageBoxShown = true;
                            }
                        }
                        else
                        {
                            //write was successful
                        }
                        break;

                    case "btnRightTTopCrosswalkLEFT":
                        CrossroadRightTTopCrosswalkBTN1 = true;
                        S7.SetBitAt(send_buffer_DB21_Input, 0, 0, CrossroadRightTTopCrosswalkBTN1);

                        //write to PLC
                        int writeResultDB21_CrossroadRightTTopCrosswalkBTN1 = client.DBWrite(DBNumber_DB21, 0, send_buffer_DB21_Input.Length, send_buffer_DB21_Input);
                        if (writeResultDB21_CrossroadRightTTopCrosswalkBTN1 != 0)
                        {
                            //write error
                            if (!errorMessageBoxShown)
                            {
                                //MessageBox
                                MessageBox.Show("BE doesn't work properly. Data could´t be written to DB21!!! \n\n" +
                                    $"Error message: {writeResultDB21_CrossroadRightTTopCrosswalkBTN1} \n", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                                errorMessageBoxShown = true;
                            }
                        }
                        else
                        {
                            //write was successful
                        }
                        break;

                    case "btnRightTTopCrosswalkRIGHT":
                        CrossroadRightTTopCrosswalkBTN2 = true;
                        S7.SetBitAt(send_buffer_DB21_Input, 0, 0, CrossroadRightTTopCrosswalkBTN2);

                        //write to PLC
                        int writeResultDB21_CrossroadRightTTopCrosswalkBTN2 = client.DBWrite(DBNumber_DB21, 0, send_buffer_DB21_Input.Length, send_buffer_DB21_Input);
                        if (writeResultDB21_CrossroadRightTTopCrosswalkBTN2 != 0)
                        {
                            //write error
                            if (!errorMessageBoxShown)
                            {
                                //MessageBox
                                MessageBox.Show("BE doesn't work properly. Data could´t be written to DB21!!! \n\n" +
                                    $"Error message: {writeResultDB21_CrossroadRightTTopCrosswalkBTN2} \n", "Error",
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
            else
            {
                //write error
                if (!errorMessageBoxShown)
                {
                    //MessageBox
                    MessageBox.Show("BE doesn't work properly. Button sender is null!!! \n\n" +
                        $"Error message: {sender} \n", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                    errorMessageBoxShown = true;
                }
            }

            /*
            if (sender is UserControlCrossroad userControl)
            {
                //something
            }
            */
        }

        #endregion

        //Emergency + system error 
        #region Emergency + system error 
        private void btnEmergency_Click(object sender, EventArgs e)
        {
            statusStripCrossroad.Items.Clear();
            ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Emergency error");
            statusStripCrossroad.Items.Add(lblStatus);

            CrossroadEmergencySTOP = true;
            S7.SetBitAt(send_buffer_DB14_Input, 0, 3, CrossroadEmergencySTOP);

            //write to PLC
            int writeResultDB14_Input = client.DBWrite(DBNumber_DB14, 0, send_buffer_DB14_Input.Length, send_buffer_DB14_Input);
            if (writeResultDB14_Input != 0)
            {
                //write error
                if (!errorMessageBoxShown)
                {
                    //MessageBox
                    MessageBox.Show("BE doesn't work properly. Data could´t be written to DB14!!! \n\n" +
                        $"Error message: {writeResultDB14_Input} \n", "Error",
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
            statusStripCrossroad.Items.Clear();
            ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Error system");
            statusStripCrossroad.Items.Add(lblStatus);

            CrossroadErrorSystem = true;
            S7.SetBitAt(send_buffer_DB14_Input, 0, 4, CrossroadErrorSystem);

            //write to PLC
            int writeResultDB14_Input = client.DBWrite(DBNumber_DB14, 0, send_buffer_DB14_Input.Length, send_buffer_DB14_Input);
            if (writeResultDB14_Input != 0)
            {
                //write error
                if (!errorMessageBoxShown)
                {
                    //MessageBox
                    MessageBox.Show("BE doesn't work properly. Data could´t be written to DB14!!! \n\n" +
                        $"Error message: {writeResultDB14_Input} \n", "Error",
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
            //Option3 = false
            Option3 = false;
            S7.SetBitAt(send_buffer_DB11, 0, 2, Option3);

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

        private void btnTest_Click(object sender, EventArgs e)
        {
            
        }
    }
}
