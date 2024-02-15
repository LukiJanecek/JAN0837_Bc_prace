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

namespace Bc_prace
{
    public partial class Program3Form : Form
    {
        public Program3Form()
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

        bool Option3;

        #endregion

        //Tia variables
        #region Tia variables

        public S7Client client = new S7Client();

        //DB11 => Maintain_DB -> 1 struct -> 3 variables -> size 0.2
        private int DBNumber_DB11 = 11;
        private byte[] read_buffer_DB11 = new byte[1]; //1
        private byte[] send_buffer_DB11 = new byte[1]; //1

        //DB14 => Crossroad_DB -> 11 structs -> x variables -> size 110.0 
        private int DBNumber_DB14 = 14;
        //first struct -> Input -> 5 variables -> size 0.4
        private byte[] read_buffer_DB14_Input = new byte[1024]; //110 
        private byte[] send_buffer_DB14_Input = new byte[1024]; //110
        //second struct -> Output -> 1 variable -> size 2.0
        private byte[] read_buffer_DB14_Output = new byte[1024]; //110 
        private byte[] send_buffer_DB14_Output = new byte[1024]; //110
        //other structs are Timers 

        //DB1 => Crossroad_1_DB -> Crossroad 1 -> 2 structs -> 25 variables -> size 6.3
        private int DBNumber_DB1 = 1;
        //first struct -> Input -> 4 variables -> size 0.3
        private byte[] read_buffer_DB1_Input = new byte[1024]; //6 
        private byte[] send_buffer_DB1_Input = new byte[1024]; //6
        //second struct -> Output -> 21 variables -> size 6.3 
        private byte[] read_buffer_DB1_Output = new byte[1024]; //6 
        private byte[] send_buffer_DB1_Output = new byte[1024]; //6

        //DB19 => Crossroad_2_DB -> Crossroad 2 -> 2 structs -> 25 variables -> size 6.3  
        private int DBNumber_DB19 = 19;
        //first struct -> Input -> 4 variables -> size 0.3
        private byte[] read_buffer_DB19_Input = new byte[1024]; //6 
        private byte[] send_buffer_DB19_Input = new byte[1024]; //6
        //second struct -> Output -> 21 variables -> size 6.3  
        private byte[] read_buffer_DB19_Output = new byte[1024]; //6 
        private byte[] send_buffer_DB19_Output = new byte[1024]; //6

        //DB20 => Crossroad_LeftT_DB - Left T -> 2 structs -> 16 variables -> size 5.4 
        private int DBNumber_DB20 = 20;
        //first struct -> Input -> 2 variables -> size 0.1
        private byte[] read_buffer_DB20_Input = new byte[1024]; //5
        private byte[] send_buffer_DB20_Input = new byte[1024]; //5
        //second struct -> Output -> 14 variables -> size 5.4
        private byte[] read_buffer_DB20_Output = new byte[1024]; //5
        private byte[] send_buffer_DB20_Output = new byte[1024]; //5

        //DB21 => Crossroad_RightT_DB - Right T -> 2 structs -> 16 variables -> size 5.4 
        private int DBNumber_DB21 = 21;
        //first struct -> Input -> 2 variables -> size 0.1
        private byte[] read_buffer_DB21_Input = new byte[1024]; //5
        private byte[] send_buffer_DB21_Input = new byte[1024]; //5
        //second struct -> Output -> 14 variables -> size 5.4
        private byte[] read_buffer_DB21_Output = new byte[1024]; //5
        private byte[] send_buffer_DB21_Output = new byte[1024]; //5

        //input 
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
        bool Crossroad2TopCrosswalkBTN1;
        bool Crossroad2TopCrosswalkBTN2;

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

        //output
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

        #endregion

        #endregion

        //Tia connection
        #region Tia connection

        private void Timer_read_from_PLC_Tick(object sender, EventArgs e)
        {
            try
            {
                //Trying read variable with other method
                #region Multi read -> MultiVar

                S7MultiVar reader = new S7MultiVar(client);

                //DB14 => Crossroad_DB - modes and timers
                #region Reading from DB14 Crossroad_DB

                //DB14 => Crossroad_DB -> 11 structs -> x variables -> size 110.0
                //first struct -> Input -> 5 variables -> size 0.4
                reader.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, DBNumber_DB14, 0, 0, ref read_buffer_DB14_Input); //read_buffer_DB14_Input.Length
                //second struct -> Output -> 1 variable -> size 2.0
                reader.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, DBNumber_DB14, 0, 0, ref read_buffer_DB14_Output); //read_buffer_DB14_Output.Length
                //other structs are Timers 

                int readResultDB14 = reader.Read();

                if (readResultDB14 == 0)
                {
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
                //first struct -> Input -> 4 variables -> size 0.3
                reader.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, DBNumber_DB1, 0, 0, ref read_buffer_DB1_Input); //read_buffer_DB1_Input.Length
                //second struct -> Output -> 21 variables -> size 6.3 
                reader.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, DBNumber_DB1, 0, 0, ref read_buffer_DB1_Output); //read_buffer_DB1_Output.Length

                int readResultDB1 = reader.Read();

                if (readResultDB1 == 0)
                {
                    //Read was successful

                    //Inpit variable
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
                //first struct -> Input -> 4 variables -> size 0.3
                reader.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, DBNumber_DB19, 0, 0, ref read_buffer_DB19_Input); //read_buffer_DB19_Input.Length
                //second struct -> Output -> 21 variables -> size 6.3
                reader.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, DBNumber_DB19, 0, 0, ref read_buffer_DB19_Output); //read_buffer_DB19_Input.Length

                int readResultDB19 = reader.Read();

                if (readResultDB19 == 0)
                {
                    //Read was successful
                                      
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
                //first struct -> Input -> 2 variables -> size 0.1
                reader.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, DBNumber_DB20, 0, 0, ref read_buffer_DB20_Input); //read_buffer_DB20_Input.Length
                //second struct -> Output -> 14 variables -> size 5.
                reader.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, DBNumber_DB20, 0, 0, ref read_buffer_DB20_Output); //read_buffer_DB20_Output.Length

                int readResultDB20 = reader.Read();

                if (readResultDB20 == 0)
                {
                    //Read was successful
                                        
                    //Input variable
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
                //first struct -> Input -> 2 variables -> size 0.1
                reader.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, DBNumber_DB21, 0, 0, ref read_buffer_DB21_Input); //read_buffer_DB21_Input.Length
                //second struct -> Output -> 14 variables -> size 5.4
                reader.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, DBNumber_DB21, 0, 0, ref read_buffer_DB21_Output); //read_buffer_DB21_Output.Length

                int readResultDB21 = reader.Read();

                if (readResultDB21 == 0)
                {
                    //Read was successful       
                    
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

                /*

                //DB14 => Crossroad_DB - modes and timers
                #region Reading from DB14 Crossroad_DB

                int readResultDB14 = client.DBRead(14, 0, read_buffer_DB14.Length, read_buffer_DB14);
                if (readResultDB14 != 0)
                {
                    statusStripCrossroad.Items.Clear();
                    ToolStripStatusLabel lblStatus1 = new ToolStripStatusLabel("Variables were not read.");
                    statusStripCrossroad.Items.Add(lblStatus1);

                    if (!errorMessageBoxShown)
                    {
                        errorMessageBoxShown = true;

                        //MessageBox
                        MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data from PLC weren't read from DB14!!!", "Error",
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

                    TrafficLightsSQ = S7.GetIntAt(read_buffer_DB14, 2);

                    #endregion

                    //Timers
                    #region Timers

                    #endregion

                }

                #endregion

                //DB1 => Crossroad_1_DB - Crossroad 1
                #region Reading from DB1 Crossroad_1_DB

                int readResultDB1 = client.DBRead(1, 0, read_buffer_DB1.Length, read_buffer_DB1);
                if (readResultDB1 != 0)
                {
                    statusStripCrossroad.Items.Clear();
                    ToolStripStatusLabel lblStatus1 = new ToolStripStatusLabel("Variables were not read.");
                    statusStripCrossroad.Items.Add(lblStatus1);

                    if (!errorMessageBoxShown)
                    {
                        errorMessageBoxShown = true;

                        //MessageBox
                        MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data from PLC weren't read from DB1!!!", "Error",
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

                    Crossroad1CrosswalkSQ = S7.GetIntAt(read_buffer_DB1, 2);
                    Crossroad1TopRED = S7.GetBitAt(read_buffer_DB1, 4, 0); 
                    Crossroad1TopGREEN = S7.GetBitAt(read_buffer_DB1, 4, 1);
                    Crossroad1TopYELLOW = S7.GetBitAt(read_buffer_DB1, 4, 2);
                    Crossroad1LeftRED = S7.GetBitAt(read_buffer_DB1, 4, 3);
                    Crossroad1LeftGREEN = S7.GetBitAt(read_buffer_DB1, 4, 4);
                    Crossroad1LeftYELLOW = S7.GetBitAt(read_buffer_DB1, 4, 5);
                    Crossroad1RightRED = S7.GetBitAt(read_buffer_DB1, 4, 6);
                    Crossroad1RightGREEN = S7.GetBitAt(read_buffer_DB1, 4, 7);
                    Crossroad1RightYELLOW = S7.GetBitAt(read_buffer_DB1, 5, 0);
                    Crossroad1BottomRED = S7.GetBitAt(read_buffer_DB1, 5, 1);
                    Crossroad1BottomGREEN = S7.GetBitAt(read_buffer_DB1, 5, 2);
                    Crossroad1BottomYELLOW = S7.GetBitAt(read_buffer_DB1, 5, 3);
                    Crossroad1TopCrosswalkRED1 = S7.GetBitAt(read_buffer_DB1, 5, 4);
                    Crossroad1TopCrosswalkRED2 = S7.GetBitAt(read_buffer_DB1, 5, 5);
                    Crossroad1TopCrosswalkGREEN1 = S7.GetBitAt(read_buffer_DB1, 5, 6);
                    Crossroad1TopCrosswalkGREEN2 = S7.GetBitAt(read_buffer_DB1, 5, 7);
                    Crossroad1LeftCrosswalkRED1 = S7.GetBitAt(read_buffer_DB1, 6, 0);
                    Crossroad1LeftCrosswalkRED2 = S7.GetBitAt(read_buffer_DB1, 6, 1);
                    Crossroad1LeftCrosswalkGREEN1 = S7.GetBitAt(read_buffer_DB1, 6, 2);
                    Crossroad1LeftCrosswalkGREEN2 = S7.GetBitAt(read_buffer_DB1, 6, 3);

                    #endregion
                }

                #endregion

                //DB19 => Crossroad_2_DB - Crossroad 2 
                #region Reading from DB19 Crossroad_2_DB

                int readResultDB19 = client.DBRead(19, 0, read_buffer_DB19.Length, read_buffer_DB19);
                if (readResultDB19 != 0)
                {
                    statusStripCrossroad.Items.Clear();
                    ToolStripStatusLabel lblStatus1 = new ToolStripStatusLabel("Variables were not read.");
                    statusStripCrossroad.Items.Add(lblStatus1);

                    if (!errorMessageBoxShown)
                    {
                        errorMessageBoxShown = true;

                        //MessageBox
                        MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data from PLC weren't read from DB19!!!", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }
                else
                {
                    //Input variable
                    #region Input variables

                    Crossroad2LeftCrosswalkBTN1 = S7.GetBitAt(read_buffer_DB19, 0, 0);
                    Crossroad2LeftCrosswalkBTN2 = S7.GetBitAt(read_buffer_DB19, 0, 1);
                    Crossroad2TopCrosswalkBTN1 = S7.GetBitAt(read_buffer_DB19, 0, 2);
                    Crossroad2TopCrosswalkBTN2 = S7.GetBitAt(read_buffer_DB19, 0, 3);

                    #endregion

                    //Output variables
                    #region Output variables

                    Crossroad2CrosswalkSQ = S7.GetIntAt(read_buffer_DB19, 2);
                    Crossroad2TopRED = S7.GetBitAt(read_buffer_DB19, 4, 0);
                    Crossroad2TopGREEN = S7.GetBitAt(read_buffer_DB19, 4, 1);
                    Crossroad2TopYellow = S7.GetBitAt(read_buffer_DB19, 4, 2);
                    Crossroad2LeftRED = S7.GetBitAt(read_buffer_DB19, 4, 3);
                    Crossroad2LeftGREEN = S7.GetBitAt(read_buffer_DB19, 4, 4);
                    Crossroad2LeftYellow = S7.GetBitAt(read_buffer_DB19, 4, 5);
                    Crossroad2RightRED = S7.GetBitAt(read_buffer_DB19, 4, 6);
                    Crossroad2RightGREEN = S7.GetBitAt(read_buffer_DB19, 4, 7);
                    Crossroad2RightYellow = S7.GetBitAt(read_buffer_DB19, 5, 0);
                    Crossroad2BottomRED = S7.GetBitAt(read_buffer_DB19, 5, 1);
                    Crossroad2BottomGREEN = S7.GetBitAt(read_buffer_DB19, 5, 2);
                    Crossroad2BottomYellow = S7.GetBitAt(read_buffer_DB19, 5, 3);
                    Crossroad2RightCrosswalkRED1 = S7.GetBitAt(read_buffer_DB19, 5, 4);
                    Crossroad2RightCrosswalkRED2 = S7.GetBitAt(read_buffer_DB19, 5, 5);
                    Crossroad2RightCrosswalkGREEN1 = S7.GetBitAt(read_buffer_DB19, 5, 6);
                    Crossroad2RightCrosswalkGREEN2 = S7.GetBitAt(read_buffer_DB19, 5, 7);
                    Crossroad2LeftCrosswalkRED1 = S7.GetBitAt(read_buffer_DB19, 6, 0);
                    Crossroad2LeftCrosswalkRED2 = S7.GetBitAt(read_buffer_DB19, 6, 1);
                    Crossroad2LeftCrosswalkGREEN1 = S7.GetBitAt(read_buffer_DB19, 6, 2);
                    Crossroad2LeftCrosswalkGREEN2 = S7.GetBitAt(read_buffer_DB19, 6, 3);
                                        
                    #endregion
                }

                #endregion

                //DB20 => Crossroad_LeftT_DB - Left T 
                #region Reading from DB20 Crossroad_LeftT_DB

                int readResultDB20 = client.DBRead(20, 0, read_buffer_DB20.Length, read_buffer_DB20);
                if (readResultDB20 != 0)
                {
                    statusStripCrossroad.Items.Clear();
                    ToolStripStatusLabel lblStatus1 = new ToolStripStatusLabel("Variables were not read.");
                    statusStripCrossroad.Items.Add(lblStatus1);

                    if (!errorMessageBoxShown)
                    {
                        errorMessageBoxShown = true;

                        //MessageBox
                        MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data from PLC weren't read from DB20!!!", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }
                else
                {
                    //Input variable
                    #region Input variables

                    CrossroadLeftTLeftCrosswalkBTN1 = S7.GetBitAt(read_buffer_DB20, 0, 0);
                    CrossroadLeftTLeftCrosswalkBTN2= S7.GetBitAt(read_buffer_DB20, 0, 1);

                    #endregion

                    //Output variables
                    #region Output variables

                    CrossroadLeftTCrosswalkSQ = S7.GetIntAt(read_buffer_DB20, 2);
                    CrossroadLeftTTopRED = S7.GetBitAt(read_buffer_DB20, 4, 0);
                    CrossroadLeftTTopGREEN = S7.GetBitAt(read_buffer_DB20, 4, 1);
                    CrossroadLeftTTopYellow = S7.GetBitAt(read_buffer_DB20, 4, 2);
                    CrossroadLeftTLeftRED = S7.GetBitAt(read_buffer_DB20, 4, 3);
                    CrossroadLeftTLeftGREEN = S7.GetBitAt(read_buffer_DB20, 4, 4);
                    CrossroadLeftTLeftYellow = S7.GetBitAt(read_buffer_DB20, 4, 5);
                    CrossroadLeftTRightRED = S7.GetBitAt(read_buffer_DB20, 4, 6);
                    CrossroadLeftTRightGREEN = S7.GetBitAt(read_buffer_DB20, 4, 7);
                    CrossroadLeftTRightYellow = S7.GetBitAt(read_buffer_DB20, 5, 0);
                    CrossroadLeftTLeftCrosswalkRED1 = S7.GetBitAt(read_buffer_DB20, 5, 1);
                    CrossroadLeftTLeftCrosswalkRED2 = S7.GetBitAt(read_buffer_DB20, 5, 2);
                    CrossroadLeftTLeftCrosswalkGREEN1 = S7.GetBitAt(read_buffer_DB20, 5, 3);
                    CrossroadLeftTLeftCrosswalkGREEN2 = S7.GetBitAt(read_buffer_DB20, 5, 4);

                    #endregion
                }

                #endregion

                //DB21 => Crossroad_RightT_DB - Right T
                #region Reading from DB21 Crossroad_RightT_DB

                int readResultDB21 = client.DBRead(21, 0, read_buffer_DB21.Length, read_buffer_DB21);
                if (readResultDB21 != 0)
                {
                    statusStripCrossroad.Items.Clear();
                    ToolStripStatusLabel lblStatus1 = new ToolStripStatusLabel("Variables were not read.");
                    statusStripCrossroad.Items.Add(lblStatus1);

                    if (!errorMessageBoxShown)
                    {
                        errorMessageBoxShown = true;

                        //MessageBox
                        MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data from PLC weren't read from DB21!!!", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }
                else
                {
                    //Input variable
                    #region Input variables

                    CrossroadRightTTopCrosswalkBTN1 = S7.GetBitAt(read_buffer_DB21, 0, 0); 
                    CrossroadRightTTopCrosswalkBTN2 = S7.GetBitAt(read_buffer_DB21, 0, 1);

                    #endregion

                    //Output variables
                    #region Output variables

                    CrossroadRightTCrosswalkSQ = S7.GetIntAt(read_buffer_DB21, 2);
                    CrossroadRightTTopRED = S7.GetBitAt(read_buffer_DB21, 4, 0);
                    CrossroadRightTTopGREEN = S7.GetBitAt(read_buffer_DB21, 4, 1);
                    CrossroadRightTTopYellow = S7.GetBitAt(read_buffer_DB21, 4, 2);
                    CrossroadRightTLeftRED = S7.GetBitAt(read_buffer_DB21, 4, 3);
                    CrossroadRightTLeftGREEN = S7.GetBitAt(read_buffer_DB21, 4, 4);
                    CrossroadRightTLeftYellow = S7.GetBitAt(read_buffer_DB21, 4, 5);
                    CrossroadRightTRightRED = S7.GetBitAt(read_buffer_DB21, 4, 6);
                    CrossroadRightTRightGREEN = S7.GetBitAt(read_buffer_DB21, 4, 7);
                    CrossroadRightTRightYellow = S7.GetBitAt(read_buffer_DB21, 5, 0);
                    CrossroadRightTTopCrosswalkRED1 = S7.GetBitAt(read_buffer_DB21, 5, 1);
                    CrossroadRightTTopCrosswalkRED2 = S7.GetBitAt(read_buffer_DB21, 5, 2);
                    CrossroadRightTTopCrosswalkGREEN1 = S7.GetBitAt(read_buffer_DB21, 5, 3);
                    CrossroadRightTTopCrosswalkGREEN2 = S7.GetBitAt(read_buffer_DB21, 5, 4);

                    #endregion
                }

                #endregion

                */

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

        //Emergency + system error 
        #region Emergency + system error 
        private void btnEmergency_Click(object sender, EventArgs e)
        {
            statusStripCrossroad.Items.Clear();
            ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Emergency error");
            statusStripCrossroad.Items.Add(lblStatus);
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
            int writeResult = client.DBWrite(DBNumber_DB11, 0, send_buffer_DB11.Length, send_buffer_DB11);
            if (writeResult != 0)
            {
                //write error
                if (!errorMessageBoxShown)
                {
                    //MessageBox
                    MessageBox.Show("BE doesn't work properly. Data could´t be written to DB11!!! \n\n" +
                        $"Error message: {writeResult} \n", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                    errorMessageBoxShown = true;
                }
            }
            else
            {
                //write was successful
            }

            //stop timer
            Timer_read_from_PLC.Stop();

            this.Close();
        }
        #endregion


        private void btnTest_Click(object sender, EventArgs e)
        {
            
        }
    }
}
