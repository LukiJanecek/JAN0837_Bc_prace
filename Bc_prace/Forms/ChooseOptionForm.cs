using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.XPath;
using Bc_prace.Controls.MyGraphControl.Entities;
using Microsoft.VisualBasic;
using Bc_prace.Settings;
using Sharp7;

namespace Bc_prace
{
    public partial class ChooseOptionForm : Form
    {
        //Variables
        #region Variables 

        public class DataSource
        {
            //zde jsou vsechny promenne z Tia
            public bool cislo { get; set; }

        }

        //C# variables
        #region C# variables

        private static bool program1Opened = false;
        private static bool program2Opened = false;
        private static bool program3Opened = false;

        private bool errorMessageBoxShown;

        Program1Form Program1 = null;
        Program2Form Program2 = null;
        Program3Form Program3 = null;

        #endregion

        //Tia variables
        #region Tia variables

        public S7Client client = new S7Client();
        public S7MultiVar s7MultiVar;

        //DB11 => Maintain_DB 0.2
        private byte[] read_buffer_DB11 = new byte[1024];
        private byte[] send_buffer_DB11 = new byte[1024];

        //DB4 => Elevator_DB 31
        private byte[] read_buffer_DB4 = new byte[1024]; //32
        private byte[] send_buffer_DB4 = new byte[1024]; //32

        //DB5 => CarWash_DB 3.7
        private byte[] read_buffer_DB5 = new byte[1024]; //3
        private byte[] send_buffer_DB5 = new byte[1024]; //3

        //DB14 => Crossroad_DB 110.0
        private byte[] read_buffer_DB14 = new byte[1024]; //110
        private byte[] send_buffer_DB14 = new byte[1024]; //110

        //DB1 => Crossroad_1_DB - Crossroad 1 6.3
        private byte[] read_buffer_DB1 = new byte[1024]; //6
        private byte[] send_buffer_DB1 = new byte[1024]; //6

        //DB19 => Crossroad_2_DB - Crossroad 2 6.3
        private byte[] read_buffer_DB19 = new byte[1024]; //6
        private byte[] send_buffer_DB19 = new byte[1024]; //6

        //DB20 => Crossroad_LeftT_DB - Left T 5.4 
        private byte[] read_buffer_DB20 = new byte[1024]; //5
        private byte[] send_buffer_DB20 = new byte[1024]; //5 

        //DB21 => Crossroad_RightT_DB - Right T 5.4
        private byte[] read_buffer_DB21 = new byte[1024];
        private byte[] send_buffer_DB21 = new byte[1024];

        bool Option1 = false;
        bool Option2 = false;
        bool Option3 = false;

        #endregion

        #endregion

        //Tia variables
        #region Tia connection

        private void Timer_read_from_PLC_Tick(object sender, EventArgs e)
        {
            try
            {
                s7MultiVar = new S7MultiVar(client);

                //Maintain_DB 
                #region Maintain_DB 
                
                //DB11 => Crossroad_DB - modes and timers
                s7MultiVar.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, 11, 0, read_buffer_DB11.Length, ref read_buffer_DB11);

                int multivarResultDB11 = s7MultiVar.Read();

                if (multivarResultDB11 == 0)
                {
                    Option1 = S7.GetBitAt(read_buffer_DB11, 0, 0);
                    Option2 = S7.GetBitAt(read_buffer_DB11, 0, 1);
                    Option3 = S7.GetBitAt(read_buffer_DB11, 0, 2);

                    errorMessageBoxShown = false;
                }
                else
                {
                    //error
                    statusStripChooseOption.Items.Clear();
                    ToolStripStatusLabel lblStatus1 = new ToolStripStatusLabel("Variables were not read.");
                    statusStripChooseOption.Items.Add(lblStatus1);

                    if (!errorMessageBoxShown)
                    {
                        errorMessageBoxShown = true;

                        //MessageBox
                        MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data from PLC weren't read from DB14!", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }

                #endregion

                //Program1
                #region Program 1 

                //DB4 
                #region DB4 

                //DB4 => Crossroad_DB - modes and timers
                s7MultiVar.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, 4, 0, read_buffer_DB4.Length, ref read_buffer_DB4); // read_buffer_DB4.Length

                int multivarResult = s7MultiVar.Read();

                if (multivarResult == 0)
                {
                    //input variables
                    #region Input variables

                    ElevatorBTNCabin1 = S7.GetBitAt(read_buffer_DB4, 0, 0);
                    ElevatorBTNCabin2 = S7.GetBitAt(read_buffer_DB4, 0, 1);
                    ElevatorBTNCabin3 = S7.GetBitAt(read_buffer_DB4, 0, 2);
                    ElevatorBTNCabin4 = S7.GetBitAt(read_buffer_DB4, 0, 3);
                    ElevatorBTNCabin5 = S7.GetBitAt(read_buffer_DB4, 0, 4);
                    ElevatorBTNFloor1 = S7.GetBitAt(read_buffer_DB4, 0, 5);
                    ElevatorBTNFloor2 = S7.GetBitAt(read_buffer_DB4, 0, 6);
                    ElevatorBTNFloor3 = S7.GetBitAt(read_buffer_DB4, 0, 7);
                    ElevatorBTNFloor4 = S7.GetBitAt(read_buffer_DB4, 1, 0);
                    ElevatorBTNFloor5 = S7.GetBitAt(read_buffer_DB4, 1, 1);
                    ElevatorDoorSEQ = S7.GetBitAt(read_buffer_DB4, 1, 2);
                    ElevatorBTNOPENCLOSE = S7.GetBitAt(read_buffer_DB4, 1, 3);
                    ElevatorEmergencySTOP = S7.GetBitAt(read_buffer_DB4, 1, 4);
                    ElevatorErrorSystem = S7.GetBitAt(read_buffer_DB4, 1, 5);

                    #endregion

                    //output variables
                    #region Output variables

                    ElevatorMotorON = S7.GetBitAt(read_buffer_DB4, 2, 0); ;
                    ElevatorMotorDOWN = S7.GetBitAt(read_buffer_DB4, 2, 1);
                    ElevatorMotorUP = S7.GetBitAt(read_buffer_DB4, 2, 2);
                    ElevatroHoming = S7.GetBitAt(read_buffer_DB4, 2, 3);
                    ElevatorSystemReady = S7.GetBitAt(read_buffer_DB4, 2, 4);
                    ElevatorActualFloor = S7.GetIntAt(read_buffer_DB4, 4);
                    ElevatorMoving = S7.GetBitAt(read_buffer_DB4, 6, 0);
                    ElevatorSystemWorking = S7.GetBitAt(read_buffer_DB4, 6, 1);
                    ElevatorGoToFloor = S7.GetIntAt(read_buffer_DB4, 8);
                    ElevatorDirection = S7.GetBitAt(read_buffer_DB4, 10, 0);
                    ElevatorActualFloorLED1 = S7.GetBitAt(read_buffer_DB4, 10, 1);
                    ElevatorActualFloorLED2 = S7.GetBitAt(read_buffer_DB4, 10, 2);
                    ElevatorActualFloorLED3 = S7.GetBitAt(read_buffer_DB4, 10, 3);
                    ElevatorActualFloorLED4 = S7.GetBitAt(read_buffer_DB4, 10, 4);
                    ElevatorActualFloorLED5 = S7.GetBitAt(read_buffer_DB4, 10, 5);
                    ElevatorActualFloorCabinLED1 = S7.GetBitAt(read_buffer_DB4, 10, 6);
                    ElevatorActualFloorCabinLED2 = S7.GetBitAt(read_buffer_DB4, 10, 7);
                    ElevatorActualFloorCabinLED3 = S7.GetBitAt(read_buffer_DB4, 11, 0);
                    ElevatorActualFloorCabinLED4 = S7.GetBitAt(read_buffer_DB4, 11, 1);
                    ElevatorActualFloorCabinLED5 = S7.GetBitAt(read_buffer_DB4, 11, 2);
                    ElevatorActualFloorSENS1 = S7.GetBitAt(read_buffer_DB4, 11, 3);
                    ElevatorActualFloorSENS2 = S7.GetBitAt(read_buffer_DB4, 11, 4);
                    ElevatorActualFloorSENS3 = S7.GetBitAt(read_buffer_DB4, 11, 5);
                    ElevatorActualFloorSENS4 = S7.GetBitAt(read_buffer_DB4, 11, 6);
                    ElevatorActualFloorSENS5 = S7.GetBitAt(read_buffer_DB4, 11, 7);
                    ElevatorTimeDoorSQOPEN = S7.GetDIntAt(read_buffer_DB4, 12); //Time
                    ElevatroTimeDoorSQCLOSE = S7.GetDIntAt(read_buffer_DB4, 12); //Time
                    ElevatorDoorClOSE = S7.GetBitAt(read_buffer_DB4, 20, 0);
                    ElevatorDoorOPEN = S7.GetBitAt(read_buffer_DB4, 20, 1);
                    ElevatorCabinSpeed = S7.GetIntAt(read_buffer_DB4, 22);
                    ElevatorInactivity = S7.GetBitAt(read_buffer_DB4, 24, 0);
                    ElevatorTimeToGetDown = S7.GetDIntAt(read_buffer_DB4, 26); //Time

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
                        MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data weren't read from DB4!!!", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }

                #endregion

                #endregion

                //Program2
                #region Program2 

                //DB5
                #region DB5

                //DB5 => Crossroad_DB - modes and timers
                s7MultiVar.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, 5, 0, read_buffer_DB5.Length, ref read_buffer_DB5);

                int multivarResult = s7MultiVar.Read();

                if (multivarResult == 0)
                {
                    //input
                    #region Input variables

                    CarWashEmergencySTOP = S7.GetBitAt(read_buffer_DB5, 0, 0);
                    CarWashErrorSystem = S7.GetBitAt(read_buffer_DB5, 0, 1);
                    CarWashStartCarWash = S7.GetBitAt(read_buffer_DB5, 0, 2);
                    CarWashWaitingForIncomingCar = S7.GetBitAt(read_buffer_DB5, 0, 3);
                    CarWashWaitingForOutgoingCar = S7.GetBitAt(read_buffer_DB5, 0, 4);
                    CarWashPerfetWash = S7.GetBitAt(read_buffer_DB5, 0, 5);
                    CarWashPerfectPolish = S7.GetBitAt(read_buffer_DB5, 0, 6);

                    #endregion

                    //output
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
                        MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data weren't read from DB14!!!", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }

                #endregion

                #endregion

                //Program3
                #region Program3 

                //DB14 => Crossroad_DB - modes and timers
                s7MultiVar.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, 14, 0, read_buffer_DB14.Length, ref read_buffer_DB14);

                //DB1 => Crossroad_1_DB - Crossroad 1
                s7MultiVar.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, 1, 0, read_buffer_DB1.Length, ref read_buffer_DB1);

                //DB19 => Crossroad_2_DB - Crossroad 2 
                s7MultiVar.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, 19, 0, read_buffer_DB19.Length, ref read_buffer_DB19);

                //DB20 => Crossroad_LeftT_DB - Left T
                s7MultiVar.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, 20, 0, read_buffer_DB20.Length, ref read_buffer_DB20);

                //DB21 => Crossroad_RightT_DB - Right T
                s7MultiVar.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, 21, 0, read_buffer_DB21.Length, ref read_buffer_DB21);

                int multivarResult = s7MultiVar.Read();

                if (multivarResult == 0)
                {
                    //Read was successful

                    //toto je stejné jako při jiném čtení

                    //DB14 => Crossroad_DB - modes and timers
                    #region Reading from DB14 Crossroad_DB

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

                    #endregion

                    //DB1 => Crossroad_1_DB - Crossroad 1
                    #region Reading from DB1 Crossroad_1_DB

                    //Inpit variable
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

                    #endregion

                    //DB19 => Crossroad_2_DB - Crossroad 2 
                    #region Reading from DB19 Crossroad_2_DB

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

                    #endregion

                    //DB20 => Crossroad_LeftT_DB - Left T 
                    #region Reading from DB20 Crossroad_LeftT_DB

                    //Input variable
                    #region Input variables

                    CrossroadLeftTLeftCrosswalkBTN1 = S7.GetBitAt(read_buffer_DB20, 0, 0);
                    CrossroadLeftTLeftCrosswalkBTN2 = S7.GetBitAt(read_buffer_DB20, 0, 1);

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

                    #endregion

                    //DB21 => Crossroad_RightT_DB - Right T
                    #region Reading from DB21 Crossroad_RightT_DB

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
                        MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data weren't read from DB14!!!", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }

                #endregion




                /*
                //DB11 => Maintain_DB
                int readResult = client.DBRead(11, 0, read_buffer_DB11.Length, read_buffer_DB11);
                //pokud je readResult roven 0, tak čtení bylo úspěšné
                if (readResult != 0)
                {
                    statusStrip1.Items.Clear();
                    ToolStripStatusLabel lblStatus1 = new ToolStripStatusLabel("Tia didn't respond. BE doesn't work properly. Data from PLC weren't read!!!");
                    statusStrip1.Items.Add(lblStatus1);

                    if (!errorMessageBoxShown)
                    {
                        errorMessageBoxShown = true;

                        //MessageBox
                        MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data from PLC weren't read from DB11!!!", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }
                else
                {
                    Option1 = S7.GetBitAt(read_buffer_DB11, 0, 0);
                    Option2 = S7.GetBitAt(read_buffer_DB11, 0, 1);
                    Option3 = S7.GetBitAt(read_buffer_DB11, 0, 2);

                    errorMessageBoxShown = false;
                }
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

        public ChooseOptionForm()
        {
            InitializeComponent();
        }

        //Choices and messages 
        #region Choose your simulation
        private void btnProgram1_Click(object sender, EventArgs e)
        {
            if (!program1Opened)
            {
                statusStripChooseOption.Items.Clear();
                ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Running Program 1");
                statusStripChooseOption.Items.Add(lblStatus);

                Option1 = true;
                S7.SetBitAt(ref send_buffer_DB11, 0, 0, true);

                //write to PLC
                s7MultiVar = new S7MultiVar(client);
                //writeResult = s7MultiVar.Write();

                int writeResult = client.DBWrite(11, 0, send_buffer_DB11.Length, send_buffer_DB11);
                if (writeResult != 0)
                {
                    //write error
                    if (!errorMessageBoxShown)
                    {
                        //MessageBox
                        MessageBox.Show("BE doesn't work properly. Data couldt be written to PLC!!!", "Error",
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

                Program1 = new Program1Form();
                Program1.Show();
                program1Opened = true;

                Program1.FormClosed += (sender, e) => { program1Opened = false; };
            }
            else
            {
                statusStripChooseOption.Items.Clear();
                ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Program 1 is already running.");
                statusStripChooseOption.Items.Add(lblStatus);

                if (Program1 != null && !Program1.IsDisposed)
                {
                    Program1.BringToFront();
                }


            }
        }

        private void btnProgram2_Click(object sender, EventArgs e)
        {
            if (!program2Opened)
            {
                statusStripChooseOption.Items.Clear();
                ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Running Program 2");
                statusStripChooseOption.Items.Add(lblStatus);

                Option2 = true;
                S7.SetBitAt(ref send_buffer_DB11, 0, 1, Option2);
                int writeResult = client.DBWrite(11, 0, send_buffer_DB11.Length, send_buffer_DB11);

                //write to PLC
                if (writeResult != 0)
                {
                    //write error
                    if (!errorMessageBoxShown)
                    {
                        //MessageBox
                        MessageBox.Show("BE doesn't work properly. Data couldt be written to PLC!!!", "Error",
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

                Program2 = new Program2Form();
                Program2.Show();
                program2Opened = true;

                Program2.FormClosed += (sender, e) => { program1Opened = false; };
            }
            else
            {
                statusStripChooseOption.Items.Clear();
                ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Program 2 is already running");
                statusStripChooseOption.Items.Add(lblStatus);

                if (Program2 != null && !Program2.IsDisposed)
                {
                    Program2.BringToFront();
                }

            }

        }

        private void btnProgram3_Click(object sender, EventArgs e)
        {
            if (!program3Opened)
            {
                statusStripChooseOption.Items.Clear();
                ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Running Program 3");
                statusStripChooseOption.Items.Add(lblStatus);

                Option3 = true;
                S7.SetBitAt(ref send_buffer_DB11, 0, 2, Option3);
                int writeResult = client.DBWrite(11, 0, send_buffer_DB11.Length, send_buffer_DB11);

                //write to PLC
                if (writeResult != 0)
                {
                    //write error
                    if (!errorMessageBoxShown)
                    {
                        //MessageBox
                        MessageBox.Show("BE doesn't work properly. Data couldt be written to PLC!!!", "Error",
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

                Program3 = new Program3Form();
                Program3.Show();
                program3Opened = true;

                Program3.FormClosed += (sender, e) => { program1Opened = false; };
            }
            else
            {
                statusStripChooseOption.Items.Clear();
                ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Program 3 is already running");
                statusStripChooseOption.Items.Add(lblStatus);


                if (Program3 != null && !Program3.IsDisposed)
                {
                    Program3.BringToFront();
                }

            }

        }
        #endregion

        //Connection + messages
        #region Connecting to PLC 
        private void btnConnect_Click(object sender, EventArgs e)
        {
            btnConnect.Text = "Connecting...";
            statusStripChooseOption.Items.Clear();
            ToolStripStatusLabel lblStat = new ToolStripStatusLabel("Connecting to " + txtBoxPLCIP.Text);
            statusStripChooseOption.Items.Add(lblStat);

            int plc = client.ConnectTo(txtBoxPLCIP.Text, 0, 1);

            if (plc == 0)
            {
                statusStripChooseOption.Items.Clear();
                ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Connected to " + txtBoxPLCIP.Text);
                statusStripChooseOption.Items.Add(lblStatus);
                btnConnect.Text = "Connected";

                //start timer
                Timer_read_from_PLC.Start();
                //set time interval (ms)
                Timer_read_from_PLC.Interval = 100;
            }
            else
            {
                statusStripChooseOption.Items.Clear();
                ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Connecting to " + txtBoxPLCIP.Text + "FAILED! Please, chech your IP address or PLC itself.");
                statusStripChooseOption.Items.Add(lblStatus);
                btnConnect.Text = "Connect";
            }
        }
        #endregion


        private void txtBoxPLCIP_TextChanged(object sender, EventArgs e)
        {

        }


        private void ChooseOption_Load(object sender, EventArgs e)
        {

        }

        //btn End 
        #region Close window 
        private void btnEnd_Click(object sender, EventArgs e)
        {
            //Option1 = false
            Option1 = false;
            S7.SetBitAt(ref send_buffer_DB11, 0, 0, Option1);
            //Option2 = false
            Option2 = false;
            S7.SetBitAt(ref send_buffer_DB11, 0, 1, Option2);
            //Option3 = false
            Option3 = false;
            S7.SetBitAt(ref send_buffer_DB11, 0, 2, Option3);

            //write to PLC
            int writeResult = client.DBWrite(11, 0, send_buffer_DB11.Length, send_buffer_DB11);
            if (writeResult != 0)
            {
                //write error
                if (!errorMessageBoxShown)
                {
                    //MessageBox
                    MessageBox.Show("BE doesn't work properly. Data couldt be written to PLC!!!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                    errorMessageBoxShown = true;
                }
            }
            else
            {
                //write was successful
            }

            //Tia disconnect
            client.Disconnect();

            //close program
            this.Close();
        }
        #endregion
    }
}
