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
using System.Security.Cryptography;

namespace Bc_prace
{
    public partial class ChooseOptionForm : Form
    {
        public S7Client client = new S7Client();

        //MessageBox control
        private bool errorMessageBoxShown;

        ElevatorForm Program1 = null;
        CarWashForm Program2 = null;
        CrossroadForm Program3 = null;

        private static bool program1Opened = false;
        private static bool program2Opened = false;
        private static bool program3Opened = false;

        //Buffers variables 
        #region Buffers variables

        //DB11 => Maintain_DB 
        private int DBNumber_DB11 = 11;
        public byte[] read_buffer_DB11 = new byte[1]; 
        public byte[] previous_buffer_DB11;
        public byte[] PreviousBufferHash_DB11;
        public byte[] send_buffer_DB11 = new byte[1]; 

        //DB4 => Elevator_DB 
        private int DBNumber_DB4 = 4;
        public byte[] read_buffer_DB4 = new byte[32];
        public byte[] previous_buffer_DB4;
        public byte[] PreviousBufferHash_DB4;
        public byte[] send_buffer_DB4 = new byte[32];

        //DB5 => CarWash_DB 
        private int DBNumber_DB5 = 5;
        public byte[] read_buffer_DB5 = new byte[11]; 
        public byte[] previous_buffer_DB5;
        public byte[] PreviousBufferHash_DB5;
        public byte[] send_buffer_DB5 = new byte[11]; 

        //DB14 => Crossroad_DB 
        private int DBNumber_DB14 = 14;
        public byte[] read_buffer_DB14 = new byte[4]; 
        public byte[] previous_buffer_DB14;
        public byte[] PreviousBufferHash_DB14;
        public byte[] send_buffer_DB14 = new byte[4]; 
        //+ other structs are Timers 

        //DB1 => Crossroad_1_DB 
        private int DBNumber_DB1 = 1;
        public byte[] read_buffer_DB1 = new byte[7]; 
        public byte[] previous_buffer_DB1;
        public byte[] PreviousBufferHash_DB1;
        public byte[] send_buffer_DB1 = new byte[7]; 

        //DB19 => Crossroad_2_DB 
        private int DBNumber_DB19 = 19;
        public byte[] read_buffer_DB19 = new byte[7]; 
        public byte[] previous_buffer_DB19;
        public byte[] PreviousBufferHash_DB19;
        public byte[] send_buffer_DB19 = new byte[7]; 

        //DB20 => Crossroad_LeftT_DB 
        private int DBNumber_DB20 = 20;
        public byte[] read_buffer_DB20 = new byte[6]; 
        public byte[] previous_buffer_DB20;
        public byte[] PreviousBufferHash_DB20;
        public byte[] send_buffer_DB20 = new byte[6]; 

        //DB21 => Crossroad_RightT_DB 
        private int DBNumber_DB21 = 21;
        public byte[] read_buffer_DB21 = new byte[6]; 
        public byte[] previous_buffer_DB21;
        public byte[] PreviousBufferHash_DB21;
        public byte[] send_buffer_DB21 = new byte[6]; 

        #endregion

        //MaintainDB variables
        public bool Option1 = false;
        public bool Option2 = false;
        public bool Option3 = false;

        //ElevatorDB variables 
        #region ElevatorDB variables 

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

        //MEM variables 
        #region MEM variables

        public bool ElevatorMEMDoor;
        public bool ElevatorMEMDoorTrig;
        public bool ElevatorMEMDoorCloseTrig;
        public bool ElevatorMEMMovingtrig;
        public bool ElevatorMEMEndMovingTrig;
        public bool ElevatorMEMBTNFloor1;
        public bool ElevatorMEMBTNFloor2;
        public bool ElevatorMEMBTNFloor3;
        public bool ElevatorMEMBTNFloor4;
        public bool ElevatorMEMBTNFloor5;

        #endregion

        #endregion

        //CarWashDB variables 
        #region CarWashDB variables

        //Input variables
        #region Input variables

        public bool CarWashEmergencySTOP;
        public bool CarWashErrorSystem;
        public bool CarWashStartCarWash;
        public bool CarWashWaitingForIncomingCar;
        public bool CarWashWaitingForOutgoingCar;
        public bool CarWashPerfetWash;
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
        public bool CarWashPreWash;
        public bool CarWashBrushes;
        public bool CarWashSoap;
        public bool CarWashActiveFoam;
        public int CarWashTimeDoorMovement; //time

        #endregion

        //MEM variables 
        #region MEM variables 

        public bool CarWashMEMDoor;
        public bool CarWashMEMDoorTrig;
        public bool CarWashMEMDoorCloseTrig;

        #endregion

        #endregion

        //CrossroadDB variables 
        #region CrossroadDB variables

        //Input variables 
        #region Input variables 

        //Crossroad_DB DB14
        #region Crossroad_DB DB14

        public bool CrossroadModeOFF;
        public bool CrossroadModeNIGHT;
        public bool CrossroadModeDAY;
        public bool CrossroadEmergencySTOP;
        public bool CrossroadErrorSystem;

        #endregion

        //Crossroad_1_DB DB1
        #region Crossroad_1_DB DB1

        public bool Crossroad1LeftCrosswalkBTN1;
        public bool Crossroad1LeftCrosswalkBTN2;
        public bool Crossroad1TopCrosswalkBTN1;
        public bool Crossroad1TopCrosswalkBTN2;

        #endregion

        //Crossroad_2_DB DB19
        #region Crossroad_2_DB DB19

        public bool Crossroad2LeftCrosswalkBTN1;
        public bool Crossroad2LeftCrosswalkBTN2;
        public bool Crossroad2RightCrosswalkBTN1;
        public bool Crossroad2RightCrosswalkBTN2;

        #endregion

        //Crossroad_LeftT_DB DB20
        #region Crossroad_LeftT_DB DB20

        public bool CrossroadLeftTLeftCrosswalkBTN1;
        public bool CrossroadLeftTLeftCrosswalkBTN2;

        #endregion

        //Crossroad_RightT_DB DB21
        #region Crossroad_RightT_DB DB21

        public bool CrossroadRightTTopCrosswalkBTN1;
        public bool CrossroadRightTTopCrosswalkBTN2;

        #endregion

        #endregion

        //Output variables 
        #region Output variables 

        //Crossroad_DB DB14
        #region Crossroad_DB DB14

        public int TrafficLightsSQ;

        #endregion

        //Crossroad_1_DB DB1
        #region Crossroad_1_DB DB1

        public int Crossroad1CrosswalkSQ;

        public bool Crossroad1TopRED;
        public bool Crossroad1TopGREEN;
        public bool Crossroad1TopYELLOW;
        public bool Crossroad1LeftRED;
        public bool Crossroad1LeftGREEN;
        public bool Crossroad1LeftYELLOW;
        public bool Crossroad1RightRED;
        public bool Crossroad1RightGREEN;
        public bool Crossroad1RightYELLOW;
        public bool Crossroad1BottomRED;
        public bool Crossroad1BottomGREEN;
        public bool Crossroad1BottomYELLOW;

        public bool Crossroad1TopCrosswalkRED1;
        public bool Crossroad1TopCrosswalkRED2;
        public bool Crossroad1TopCrosswalkGREEN1;
        public bool Crossroad1TopCrosswalkGREEN2;
        public bool Crossroad1LeftCrosswalkRED1;
        public bool Crossroad1LeftCrosswalkRED2;
        public bool Crossroad1LeftCrosswalkGREEN1;
        public bool Crossroad1LeftCrosswalkGREEN2;

        #endregion

        //Crossroad_2_DB DB19
        #region Crossroad_2_DB DB19

        public int Crossroad2CrosswalkSQ;

        public bool Crossroad2TopRED;
        public bool Crossroad2TopGREEN;
        public bool Crossroad2TopYellow;
        public bool Crossroad2LeftRED;
        public bool Crossroad2LeftGREEN;
        public bool Crossroad2LeftYellow;
        public bool Crossroad2RightRED;
        public bool Crossroad2RightGREEN;
        public bool Crossroad2RightYellow;
        public bool Crossroad2BottomRED;
        public bool Crossroad2BottomGREEN;
        public bool Crossroad2BottomYellow;

        public bool Crossroad2LeftCrosswalkRED1;
        public bool Crossroad2LeftCrosswalkRED2;
        public bool Crossroad2LeftCrosswalkGREEN1;
        public bool Crossroad2LeftCrosswalkGREEN2;
        public bool Crossroad2RightCrosswalkRED1;
        public bool Crossroad2RightCrosswalkRED2;
        public bool Crossroad2RightCrosswalkGREEN1;
        public bool Crossroad2RightCrosswalkGREEN2;

        #endregion

        //Crossroad_LeftT_DB DB20
        #region Crossroad_LeftT_DB DB20

        public int CrossroadLeftTCrosswalkSQ;

        public bool CrossroadLeftTTopRED;
        public bool CrossroadLeftTTopGREEN;
        public bool CrossroadLeftTTopYellow;
        public bool CrossroadLeftTLeftRED;
        public bool CrossroadLeftTLeftGREEN;
        public bool CrossroadLeftTLeftYellow;
        public bool CrossroadLeftTRightRED;
        public bool CrossroadLeftTRightGREEN;
        public bool CrossroadLeftTRightYellow;

        public bool CrossroadLeftTLeftCrosswalkRED1;
        public bool CrossroadLeftTLeftCrosswalkRED2;
        public bool CrossroadLeftTLeftCrosswalkGREEN1;
        public bool CrossroadLeftTLeftCrosswalkGREEN2;

        #endregion

        //Crossroad_RightT_DB DB21
        #region Crossroad_RightT_DB DB21

        public int CrossroadRightTCrosswalkSQ;

        public bool CrossroadRightTTopRED;
        public bool CrossroadRightTTopGREEN;
        public bool CrossroadRightTTopYellow;
        public bool CrossroadRightTLeftRED;
        public bool CrossroadRightTLeftGREEN;
        public bool CrossroadRightTLeftYellow;
        public bool CrossroadRightTRightRED;
        public bool CrossroadRightTRightGREEN;
        public bool CrossroadRightTRightYellow;

        public bool CrossroadRightTTopCrosswalkRED1;
        public bool CrossroadRightTTopCrosswalkRED2;
        public bool CrossroadRightTTopCrosswalkGREEN1;
        public bool CrossroadRightTTopCrosswalkGREEN2;

        #endregion

        #endregion

        #endregion

        //Tia variables
        #region Tia connection

        private void Timer_read_from_PLC_Tick(object sender, EventArgs e)
        {
            try
            {
                //Reading variables with MultiVar method
                #region Multi read -> MultiVar

                S7MultiVar reader = new S7MultiVar(client);

                //DB11 => Maintain_DB
                #region Reading from DB11 Maintain_DB
                //DB11 => Maintain_DB 
                if (previous_buffer_DB11 == null)
                {
                    previous_buffer_DB11 = new byte[read_buffer_DB11.Length];
                    Array.Copy(read_buffer_DB11, previous_buffer_DB11, read_buffer_DB11.Length);

                    PreviousBufferHash_DB11 = ComputeHash(read_buffer_DB11);
                }

                reader.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, DBNumber_DB11, 0, read_buffer_DB11.Length, ref read_buffer_DB11);

                int readResultDB11 = reader.Read();

                if (readResultDB11 == 0)
                {
                    byte[] currentHashDB11 = ComputeHash(read_buffer_DB11);

                    if (!ArraysAreEqual(currentHashDB11, PreviousBufferHash_DB11))
                    {
                        Array.Copy(read_buffer_DB11, previous_buffer_DB11, read_buffer_DB11.Length);
                        PreviousBufferHash_DB11 = currentHashDB11;

                        Option1 = S7.GetBitAt(read_buffer_DB11, 0, 0);
                        Option2 = S7.GetBitAt(read_buffer_DB11, 0, 1);
                        Option3 = S7.GetBitAt(read_buffer_DB11, 0, 2);

                        errorMessageBoxShown = false;
                    }

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
                        MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data from PLC weren't read from DB11! \n\n" +
                            $"Error message: {readResultDB11} \n", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }

                #endregion

                //DB4 => Elevator_DB
                #region Reading from DB4 Elevator_DB
                //DB4 => Elevator_DB 
                if (previous_buffer_DB4 == null)
                {
                    previous_buffer_DB4 = new byte[read_buffer_DB4.Length];
                    Array.Copy(read_buffer_DB4, previous_buffer_DB4, read_buffer_DB4.Length);

                    PreviousBufferHash_DB4 = ComputeHash(read_buffer_DB4);
                }

                reader.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, DBNumber_DB4, 0, read_buffer_DB4.Length, ref read_buffer_DB4);
                
                int readResultDB4 = reader.Read();

                if (readResultDB4 == 0)
                {
                    byte[] currentHashDB4_Input = ComputeHash(read_buffer_DB4);

                    if (!ArraysAreEqual(currentHashDB4_Input, PreviousBufferHash_DB4))
                    {
                        Array.Copy(read_buffer_DB4, previous_buffer_DB4, read_buffer_DB4.Length);
                        PreviousBufferHash_DB4 = currentHashDB4_Input;

                        //Input variables
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

                        //Output variables
                        #region Output variables

                        ElevatorMotorON = S7.GetBitAt(read_buffer_DB4, 2, 0);
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
                        ElevatroTimeDoorSQCLOSE = S7.GetDIntAt(read_buffer_DB4, 16); //Time
                        ElevatorDoorClOSE = S7.GetBitAt(read_buffer_DB4, 20, 0);
                        ElevatorDoorOPEN = S7.GetBitAt(read_buffer_DB4, 20, 1);
                        ElevatorCabinSpeed = S7.GetIntAt(read_buffer_DB4, 22);
                        ElevatorInactivity = S7.GetBitAt(read_buffer_DB4, 24, 0);
                        ElevatorTimeToGetDown = S7.GetDIntAt(read_buffer_DB4, 26); //Time

                        #endregion

                        //MEM variables 
                        #region MEM varialbes 

                        ElevatorMEMDoor = S7.GetBitAt(read_buffer_DB4, 30, 0); 
                        ElevatorMEMDoorTrig = S7.GetBitAt(read_buffer_DB4, 30, 1); 
                        ElevatorMEMDoorCloseTrig = S7.GetBitAt(read_buffer_DB4, 30, 2); 
                        ElevatorMEMMovingtrig = S7.GetBitAt(read_buffer_DB4, 30, 3); 
                        ElevatorMEMEndMovingTrig = S7.GetBitAt(read_buffer_DB4, 30, 4); 
                        ElevatorMEMBTNFloor1 = S7.GetBitAt(read_buffer_DB4, 30, 5); 
                        ElevatorMEMBTNFloor2 = S7.GetBitAt(read_buffer_DB4, 30, 6); 
                        ElevatorMEMBTNFloor3 = S7.GetBitAt(read_buffer_DB4, 30, 7); 
                        ElevatorMEMBTNFloor4 = S7.GetBitAt(read_buffer_DB4, 31, 0); 
                        ElevatorMEMBTNFloor5 = S7.GetBitAt(read_buffer_DB4, 31, 1); 

                        #endregion

                        errorMessageBoxShown = false;
                    }

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
                        MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data weren't read from DB4!!! \n\n" +
                            $"Error message {readResultDB4} \n", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }

                #endregion

                //DB5 => CarWash_DB
                #region Reading from DB5 CarWash_DB
                //DB5 => CarWash_DB 
                if (previous_buffer_DB5 == null)
                {
                    previous_buffer_DB5 = new byte[read_buffer_DB5.Length];
                    Array.Copy(read_buffer_DB5, previous_buffer_DB5, read_buffer_DB5.Length);

                    PreviousBufferHash_DB5 = ComputeHash(read_buffer_DB5);
                }

                reader.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, DBNumber_DB5, 0, read_buffer_DB5.Length, ref read_buffer_DB5);

                int readResultDB5 = reader.Read();

                if (readResultDB5 == 0)
                {
                    byte[] currentHashDB5_Input = ComputeHash(read_buffer_DB5);

                    if (!ArraysAreEqual(currentHashDB5_Input, PreviousBufferHash_DB5))
                    {
                        Array.Copy(read_buffer_DB5, previous_buffer_DB5, read_buffer_DB5.Length);
                        PreviousBufferHash_DB5 = currentHashDB5_Input;

                        //Input variables
                        #region Input variables

                        CarWashEmergencySTOP = S7.GetBitAt(read_buffer_DB5, 0, 0);
                        CarWashErrorSystem = S7.GetBitAt(read_buffer_DB5, 0, 1);
                        CarWashStartCarWash = S7.GetBitAt(read_buffer_DB5, 0, 2);
                        CarWashWaitingForIncomingCar = S7.GetBitAt(read_buffer_DB5, 0, 3);
                        CarWashWaitingForOutgoingCar = S7.GetBitAt(read_buffer_DB5, 0, 4);
                        CarWashPerfetWash = S7.GetBitAt(read_buffer_DB5, 0, 5);
                        CarWashPerfectPolish = S7.GetBitAt(read_buffer_DB5, 0, 6);

                        #endregion

                        //Output variables
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
                        CarWashPreWash = S7.GetBitAt(read_buffer_DB5, 4, 0);
                        CarWashBrushes = S7.GetBitAt(read_buffer_DB5, 4, 1); 
                        CarWashSoap = S7.GetBitAt(read_buffer_DB5, 4, 2); ;
                        CarWashActiveFoam = S7.GetBitAt(read_buffer_DB5, 4, 3); 
                        CarWashTimeDoorMovement = S7.GetDIntAt(read_buffer_DB5, 6);
                        CarWashMEMDoor = S7.GetBitAt(read_buffer_DB5, 10, 0); 
                        CarWashMEMDoorTrig = S7.GetBitAt(read_buffer_DB5, 10, 1);
                        CarWashMEMDoorCloseTrig = S7.GetBitAt(read_buffer_DB5, 10, 2);

                        #endregion

                        errorMessageBoxShown = false;
                    }

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
                        MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data weren't read from DB5!!! \n\n" +
                            $"Error message {readResultDB5} \n", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }

                #endregion

                //DB14 => Crossroad_DB 
                #region Reading from DB14 Crossroad_DB
                //DB14 => Crossroad_DB 
                if (previous_buffer_DB14 == null)
                {
                    previous_buffer_DB14 = new byte[read_buffer_DB14.Length];
                    Array.Copy(read_buffer_DB14, previous_buffer_DB14, read_buffer_DB14.Length);

                    PreviousBufferHash_DB14 = ComputeHash(read_buffer_DB14);
                }

                reader.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, DBNumber_DB14, 0, read_buffer_DB14.Length, ref read_buffer_DB14); 

                int readResultDB14 = reader.Read();

                if (readResultDB14 == 0)
                {
                    byte[] currentHashDB14_Input = ComputeHash(read_buffer_DB14);

                    if (!ArraysAreEqual(currentHashDB14_Input, PreviousBufferHash_DB14))
                    {
                        Array.Copy(read_buffer_DB14, previous_buffer_DB14, read_buffer_DB14.Length);
                        PreviousBufferHash_DB14 = currentHashDB14_Input;

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

                        errorMessageBoxShown = false;
                    }
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
                        MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data weren't read from DB14!!! \n\n" +
                            $"Error message {readResultDB14} \n", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }

                #endregion

                //DB1 => Crossroad_1_DB - Crossroad 1
                #region Reading from DB1 Crossroad_1_DB
                //DB1 => Crossroad_1_DB -> Crossroad 1 
                if (previous_buffer_DB1 == null)
                {
                    previous_buffer_DB1 = new byte[read_buffer_DB1.Length];
                    Array.Copy(read_buffer_DB1, previous_buffer_DB1, read_buffer_DB1.Length);

                    PreviousBufferHash_DB1 = ComputeHash(read_buffer_DB1);
                }

                reader.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, DBNumber_DB1, 0, read_buffer_DB1.Length, ref read_buffer_DB1); 

                int readResultDB1 = reader.Read();

                if (readResultDB1 == 0)
                {
                    byte[] currentHashDB1_Input = ComputeHash(read_buffer_DB1);

                    if (!ArraysAreEqual(currentHashDB1_Input, PreviousBufferHash_DB1))
                    {
                        Array.Copy(read_buffer_DB1, previous_buffer_DB1, read_buffer_DB1.Length);
                        PreviousBufferHash_DB1 = currentHashDB1_Input;

                        //Input variables
                        #region Input variables

                        Crossroad1LeftCrosswalkBTN1 = S7.GetBitAt(read_buffer_DB1, 0, 0);
                        Crossroad1LeftCrosswalkBTN2 = S7.GetBitAt(read_buffer_DB1, 0, 1);
                        Crossroad1TopCrosswalkBTN1 = S7.GetBitAt(read_buffer_DB1, 0, 2);
                        Crossroad1TopCrosswalkBTN2 = S7.GetBitAt(read_buffer_DB1, 0, 3);

                        #endregion

                        //Output variables
                        #region Output variables

                        Crossroad1CrosswalkSQ = S7.GetIntAt(read_buffer_DB1, 2);

                        Crossroad1TopGREEN = S7.GetBitAt(read_buffer_DB1, 4, 0);
                        Crossroad1TopYELLOW = S7.GetBitAt(read_buffer_DB1, 4, 1);
                        Crossroad1TopRED = S7.GetBitAt(read_buffer_DB1, 4, 2);
                                                                        
                        Crossroad1LeftGREEN = S7.GetBitAt(read_buffer_DB1, 4, 3);
                        Crossroad1LeftYELLOW = S7.GetBitAt(read_buffer_DB1, 4, 4);
                        Crossroad1LeftRED = S7.GetBitAt(read_buffer_DB1, 4, 5);

                        Crossroad1BottomGREEN = S7.GetBitAt(read_buffer_DB1, 4, 6);
                        Crossroad1BottomYELLOW = S7.GetBitAt(read_buffer_DB1, 4, 7);
                        Crossroad1BottomRED = S7.GetBitAt(read_buffer_DB1, 5, 0);

                        Crossroad1RightGREEN = S7.GetBitAt(read_buffer_DB1, 5, 1);
                        Crossroad1RightYELLOW = S7.GetBitAt(read_buffer_DB1, 5, 2);
                        Crossroad1RightRED = S7.GetBitAt(read_buffer_DB1, 5, 3);

                        Crossroad1TopCrosswalkRED1 = S7.GetBitAt(read_buffer_DB1, 5, 4);
                        Crossroad1TopCrosswalkRED2 = S7.GetBitAt(read_buffer_DB1, 5, 5);
                        Crossroad1TopCrosswalkGREEN1 = S7.GetBitAt(read_buffer_DB1, 5, 6);
                        Crossroad1TopCrosswalkGREEN2 = S7.GetBitAt(read_buffer_DB1, 5, 7);

                        Crossroad1LeftCrosswalkRED1 = S7.GetBitAt(read_buffer_DB1, 6, 0);
                        Crossroad1LeftCrosswalkRED2 = S7.GetBitAt(read_buffer_DB1, 6, 1);
                        Crossroad1LeftCrosswalkGREEN1 = S7.GetBitAt(read_buffer_DB1, 6, 2);
                        Crossroad1LeftCrosswalkGREEN2 = S7.GetBitAt(read_buffer_DB1, 6, 3);

                        #endregion

                        errorMessageBoxShown = false;
                    }
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
                        MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data weren't read from DB1!!! \n\n" +
                            $"Error message {readResultDB1} \n", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }

                #endregion

                //DB19 => Crossroad_2_DB - Crossroad 2 
                #region Reading from DB19 Crossroad_2_DB
                //DB19 => Crossroad_2_DB -> Crossroad 2
                if (previous_buffer_DB19 == null)
                {
                    previous_buffer_DB19 = new byte[read_buffer_DB19.Length];
                    Array.Copy(read_buffer_DB19, previous_buffer_DB19, read_buffer_DB19.Length);

                    PreviousBufferHash_DB19 = ComputeHash(read_buffer_DB19);
                }

                reader.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, DBNumber_DB19, 0, read_buffer_DB19.Length, ref read_buffer_DB19); 

                int readResultDB19 = reader.Read();

                if (readResultDB19 == 0)
                {
                    byte[] currentHashDB19_Input = ComputeHash(read_buffer_DB19);

                    if (!ArraysAreEqual(currentHashDB19_Input, PreviousBufferHash_DB19))
                    {
                        Array.Copy(read_buffer_DB19, previous_buffer_DB19, read_buffer_DB19.Length);
                        PreviousBufferHash_DB19 = currentHashDB19_Input;

                        //Input variable
                        #region Input variables

                        Crossroad2LeftCrosswalkBTN1 = S7.GetBitAt(read_buffer_DB19, 0, 0);
                        Crossroad2LeftCrosswalkBTN2 = S7.GetBitAt(read_buffer_DB19, 0, 1);
                        Crossroad2RightCrosswalkBTN1 = S7.GetBitAt(read_buffer_DB19, 0, 2);
                        Crossroad2RightCrosswalkBTN2 = S7.GetBitAt(read_buffer_DB19, 0, 3);

                        #endregion

                        //Output variables
                        #region Output variables

                        Crossroad2CrosswalkSQ = S7.GetIntAt(read_buffer_DB19, 2);
                                                
                        Crossroad2TopGREEN = S7.GetBitAt(read_buffer_DB19, 4, 0);
                        Crossroad2TopYellow = S7.GetBitAt(read_buffer_DB19, 4, 1);
                        Crossroad2TopRED = S7.GetBitAt(read_buffer_DB19, 4, 2);
                                                
                        Crossroad2LeftGREEN = S7.GetBitAt(read_buffer_DB19, 4, 3);
                        Crossroad2LeftYellow = S7.GetBitAt(read_buffer_DB19, 4, 4);
                        Crossroad2LeftRED = S7.GetBitAt(read_buffer_DB19, 4, 5);

                        Crossroad2BottomGREEN = S7.GetBitAt(read_buffer_DB19, 4, 6);
                        Crossroad2BottomYellow = S7.GetBitAt(read_buffer_DB19, 4, 7);
                        Crossroad2BottomRED = S7.GetBitAt(read_buffer_DB19, 5, 0);

                        Crossroad2RightGREEN = S7.GetBitAt(read_buffer_DB19, 5, 1);
                        Crossroad2RightYellow = S7.GetBitAt(read_buffer_DB19, 5, 2);
                        Crossroad2RightRED = S7.GetBitAt(read_buffer_DB19, 5, 3);

                        Crossroad2RightCrosswalkRED1 = S7.GetBitAt(read_buffer_DB19, 5, 4);
                        Crossroad2RightCrosswalkRED2 = S7.GetBitAt(read_buffer_DB19, 5, 5);
                        Crossroad2RightCrosswalkGREEN1 = S7.GetBitAt(read_buffer_DB19, 5, 6);
                        Crossroad2RightCrosswalkGREEN2 = S7.GetBitAt(read_buffer_DB19, 5, 7);

                        Crossroad2LeftCrosswalkRED1 = S7.GetBitAt(read_buffer_DB19, 6, 0);
                        Crossroad2LeftCrosswalkRED2 = S7.GetBitAt(read_buffer_DB19, 6, 1);
                        Crossroad2LeftCrosswalkGREEN1 = S7.GetBitAt(read_buffer_DB19, 6, 2);
                        Crossroad2LeftCrosswalkGREEN2 = S7.GetBitAt(read_buffer_DB19, 6, 3);

                        #endregion

                        errorMessageBoxShown = false;
                    }
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
                        MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data weren't read from DB19!!! \n\n" +
                            $"Error message {readResultDB19} \n", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }

                #endregion

                //DB20 => Crossroad_LeftT_DB - Left T 
                #region Reading from DB20 Crossroad_LeftT_DB
                //DB20 => Crossroad_LeftT_DB - Left T 
                if (previous_buffer_DB20 == null)
                {
                    previous_buffer_DB20 = new byte[read_buffer_DB20.Length];
                    Array.Copy(read_buffer_DB20, previous_buffer_DB20, read_buffer_DB20.Length);

                    PreviousBufferHash_DB20 = ComputeHash(read_buffer_DB20);
                }

                reader.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, DBNumber_DB20, 0, read_buffer_DB20.Length, ref read_buffer_DB20); 

                int readResultDB20 = reader.Read();

                if (readResultDB20 == 0)
                {
                    byte[] currentHashDB20_Input = ComputeHash(read_buffer_DB20);

                    if (!ArraysAreEqual(currentHashDB20_Input, PreviousBufferHash_DB20))
                    {
                        Array.Copy(read_buffer_DB20, previous_buffer_DB20, read_buffer_DB20.Length);
                        PreviousBufferHash_DB20 = currentHashDB20_Input;

                        //Input variable
                        #region Input variables

                        CrossroadLeftTLeftCrosswalkBTN1 = S7.GetBitAt(read_buffer_DB20, 0, 0);
                        CrossroadLeftTLeftCrosswalkBTN2 = S7.GetBitAt(read_buffer_DB20, 0, 1);

                        #endregion

                        //Output variables
                        #region Output variables

                        CrossroadLeftTCrosswalkSQ = S7.GetIntAt(read_buffer_DB20, 2);
                                                
                        CrossroadLeftTTopGREEN = S7.GetBitAt(read_buffer_DB20, 4, 0);
                        CrossroadLeftTTopYellow = S7.GetBitAt(read_buffer_DB20, 4, 1);
                        CrossroadLeftTTopRED = S7.GetBitAt(read_buffer_DB20, 4, 2);
                                                
                        CrossroadLeftTLeftGREEN = S7.GetBitAt(read_buffer_DB20, 4, 3);
                        CrossroadLeftTLeftYellow = S7.GetBitAt(read_buffer_DB20, 4, 4);
                        CrossroadLeftTLeftRED = S7.GetBitAt(read_buffer_DB20, 4, 5);
                                                
                        CrossroadLeftTRightGREEN = S7.GetBitAt(read_buffer_DB20, 4, 6);
                        CrossroadLeftTRightYellow = S7.GetBitAt(read_buffer_DB20, 4, 7);
                        CrossroadLeftTRightRED = S7.GetBitAt(read_buffer_DB20, 5, 0);

                        CrossroadLeftTLeftCrosswalkRED1 = S7.GetBitAt(read_buffer_DB20, 5, 1);
                        CrossroadLeftTLeftCrosswalkRED2 = S7.GetBitAt(read_buffer_DB20, 5, 2);

                        CrossroadLeftTLeftCrosswalkGREEN1 = S7.GetBitAt(read_buffer_DB20, 5, 3);
                        CrossroadLeftTLeftCrosswalkGREEN2 = S7.GetBitAt(read_buffer_DB20, 5, 4);

                        #endregion

                        errorMessageBoxShown = false;
                    }
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
                        MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data weren't read from DB20!!! \n\n" +
                            $"Error message {readResultDB20} \n", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }

                #endregion

                //DB21 => Crossroad_RightT_DB - Right T
                #region Reading from DB21 Crossroad_RightT_DB
                //DB21 => Crossroad_RightT_DB - Right T
                if (previous_buffer_DB21 == null)
                {
                    previous_buffer_DB21 = new byte[read_buffer_DB21.Length];
                    Array.Copy(read_buffer_DB21, previous_buffer_DB21, read_buffer_DB21.Length);

                    PreviousBufferHash_DB21 = ComputeHash(read_buffer_DB21);
                }

                reader.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, DBNumber_DB21, 0, read_buffer_DB21.Length, ref read_buffer_DB21); 

                int readResultDB21 = reader.Read();

                if (readResultDB21 == 0)
                {
                    byte[] currentHashDB21_Input = ComputeHash(read_buffer_DB21);

                    if (!ArraysAreEqual(currentHashDB21_Input, PreviousBufferHash_DB21))
                    {
                        Array.Copy(read_buffer_DB21, previous_buffer_DB21, read_buffer_DB21.Length);
                        PreviousBufferHash_DB21 = currentHashDB21_Input;

                        //Input variable
                        #region Input variables

                        CrossroadRightTTopCrosswalkBTN1 = S7.GetBitAt(read_buffer_DB21, 0, 0);
                        CrossroadRightTTopCrosswalkBTN2 = S7.GetBitAt(read_buffer_DB21, 0, 1);

                        #endregion

                        //Output variables
                        #region Output variables

                        CrossroadRightTCrosswalkSQ = S7.GetIntAt(read_buffer_DB21, 2);
                                                
                        CrossroadRightTTopGREEN = S7.GetBitAt(read_buffer_DB21, 4, 0);
                        CrossroadRightTTopYellow = S7.GetBitAt(read_buffer_DB21, 4, 1);
                        CrossroadRightTTopRED = S7.GetBitAt(read_buffer_DB21, 4, 2);
                                                
                        CrossroadRightTLeftGREEN = S7.GetBitAt(read_buffer_DB21, 4, 3);
                        CrossroadRightTLeftYellow = S7.GetBitAt(read_buffer_DB21, 4, 4);
                        CrossroadRightTLeftRED = S7.GetBitAt(read_buffer_DB21, 4, 5);
                                                
                        CrossroadRightTRightGREEN = S7.GetBitAt(read_buffer_DB21, 4, 6);
                        CrossroadRightTRightYellow = S7.GetBitAt(read_buffer_DB21, 4, 7);
                        CrossroadRightTRightRED = S7.GetBitAt(read_buffer_DB21, 5, 0);

                        CrossroadRightTTopCrosswalkRED1 = S7.GetBitAt(read_buffer_DB21, 5, 1);
                        CrossroadRightTTopCrosswalkRED2 = S7.GetBitAt(read_buffer_DB21, 5, 2);

                        CrossroadRightTTopCrosswalkGREEN1 = S7.GetBitAt(read_buffer_DB21, 5, 3);
                        CrossroadRightTTopCrosswalkGREEN2 = S7.GetBitAt(read_buffer_DB21, 5, 4);

                        #endregion

                        errorMessageBoxShown = false;
                    }
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
                        MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data weren't read from DB21!!! \n\n" +
                            $"Error message {readResultDB21} \n", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }

                #endregion

                #endregion

                //Reading variables with DBRead method
                /*
                #region DBRead

                //DB11 => Maintain_DB
                int readResultDB11 = client.DBRead(DBNumber_DB11, 0, read_buffer_DB11.Length, read_buffer_DB11);
                //pokud je readResult roven 0, tak čtení bylo úspěšné
                if (readResultDB11 != 0)
                {
                    //error
                    statusStripChooseOption.Items.Clear();
                    ToolStripStatusLabel lblStatus1 = new ToolStripStatusLabel("Variables were not read.");
                    statusStripChooseOption.Items.Add(lblStatus1);

                    if (!errorMessageBoxShown)
                    {
                        errorMessageBoxShown = true;

                        //MessageBox
                        MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data weren't read from DB4!!! \n\n" +
                            $"Error message {readResultDB11} \n", "Error",
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

                #endregion

                */
            }
            catch (Exception ex)
            {
                if (!errorMessageBoxShown)
                {
                    //MessageBox
                    MessageBox.Show($"Error: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
        }

        private bool ArraysAreEqual(byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length)
            {
                return false;
            }

            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                {
                    return false;
                }
            }

            return true;
        }

        private byte[] ComputeHash(byte[] data)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(data);
            }
        }

        #endregion

        public ChooseOptionForm()
        {
            InitializeComponent();
            this.MinimumSize = new Size(450, 300);
        }

        private void ChooseOption_Load(object sender, EventArgs e)
        {
            lblChooseSIM.Visible = false;
            btnElevator.Visible = false;
            btnCarWash.Visible = false;
            btnCrossroad.Visible = false;
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
                S7.SetBitAt(send_buffer_DB11, 0, 0, Option1);

                //write to PLC
                int writeResultDB11 = client.DBWrite(DBNumber_DB11, 0, send_buffer_DB11.Length, send_buffer_DB11);
                if (writeResultDB11 == 0)
                {
                    //write was successful
                }
                else
                {
                    //write error
                    if (!errorMessageBoxShown)
                    {
                        errorMessageBoxShown = true;

                        //MessageBox
                        MessageBox.Show("BE doesn't work properly. Data could´t be written to DB11!!! \n\n" +
                            $"Error message: {writeResultDB11} \n", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }

                Program1 = new ElevatorForm(this);
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
                S7.SetBitAt(send_buffer_DB11, 0, 1, Option2);

                //write to PLC
                int writeResultDB11 = client.DBWrite(DBNumber_DB11, 0, send_buffer_DB11.Length, send_buffer_DB11);
                if (writeResultDB11 == 0)
                {
                    //write was successful
                }
                else
                {
                    //write error
                    if (!errorMessageBoxShown)
                    {
                        errorMessageBoxShown = true;

                        //MessageBox
                        MessageBox.Show("BE doesn't work properly. Data could´t be written to DB11!!! \n\n" +
                            $"Error message: {writeResultDB11} \n", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }

                Program2 = new CarWashForm(this);
                Program2.Show();
                program2Opened = true;

                Program2.FormClosed += (sender, e) => { program2Opened = false; };
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
                S7.SetBitAt(send_buffer_DB11, 0, 2, Option3);

                //write to PLC
                int writeResultDB11 = client.DBWrite(DBNumber_DB11, 0, send_buffer_DB11.Length, send_buffer_DB11);
                if (writeResultDB11 == 0)
                {
                    //write was successful
                }
                else
                {
                    //write error
                    if (!errorMessageBoxShown)
                    {
                        errorMessageBoxShown = true;

                        //MessageBox
                        MessageBox.Show("BE doesn't work properly. Data could´t be written to DB11!!! \n\n" +
                            $"Error message: {writeResultDB11} \n", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }

                Program3 = new CrossroadForm(this);
                Program3.Show();
                program3Opened = true;

                Program3.FormClosed += (sender, e) => { program3Opened = false; };
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

            //0 -> MPI -> Multi Point Interface -> zde připojení nefunguje 
            //1 -> PPI -> Point to Point interface
            //2 -> OP -> Engineering point
            //3 -> S7 Basic -> S7 communication using Ethernet or Profibus
            //10 -> ISOTCP -> TCP/IP protocol -> Ethernet -> zde připojení nefunguje
            client.SetConnectionType(1);

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

                lblChooseSIM.Visible = true;
                btnElevator.Visible = true;
                btnCarWash.Visible = true;
                btnCrossroad.Visible = true;
            }
            else
            {
                statusStripChooseOption.Items.Clear();
                ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Connecting to " + txtBoxPLCIP.Text + "FAILED! Please, chech your IP address or PLC itself.");
                statusStripChooseOption.Items.Add(lblStatus);
                btnConnect.Text = "Connect";

                //MessageBox
                MessageBox.Show("PLC didn´t connected. Please, chech your IP address or PLC itself.\n\n" +
                    $"Error message: {plc} \n", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

            }
        }
        #endregion

        //btn End 
        #region Close window 
        private void btnEnd_Click(object sender, EventArgs e)
        {
            Option1 = false;
            S7.SetBitAt(send_buffer_DB11, 0, 0, Option1);
            Option2 = false;
            S7.SetBitAt(send_buffer_DB11, 0, 1, Option2);
            Option3 = false;
            S7.SetBitAt(send_buffer_DB11, 0, 2, Option3);

            if (client.Connected)
            {
                //write to PLC
                int writeResultDB11 = client.DBWrite(DBNumber_DB11, 0, send_buffer_DB11.Length, send_buffer_DB11);
                if (writeResultDB11 == 0)
                {
                    //write was successful
                }
                else
                {
                    //write error
                    if (!errorMessageBoxShown)
                    {
                        errorMessageBoxShown = true;

                        //MessageBox
                        MessageBox.Show("BE doesn't work properly. Data could´t be written to DB11!!! \n\n" +
                                $"Error message: {writeResultDB11} \n", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }

                //Tia disconnect
                client.Disconnect();
            }

            //close program
            this.Close();
        }

        #endregion
    }
}
