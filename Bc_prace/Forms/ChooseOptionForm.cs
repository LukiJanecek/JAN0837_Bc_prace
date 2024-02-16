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

        //DB11 => Maintain_DB -> 1 struct -> 3 variables -> size 0.2
        private int DBNumber_DB11 = 11;
        private byte[] read_buffer_DB11 = new byte[1]; //1
        private byte[] send_buffer_DB11 = new byte[1]; //1

        //DB4 => Elevator_DB -> 2 structs -> 46 variables -> size 26
        private int DBNumber_DB4 = 4;
        //first struct -> Input -> 14 variables -> size 1.5 
        private byte[] read_buffer_DB4_Input = new byte[2]; //26 
        private byte[] send_buffer_DB4_Input = new byte[2]; //26
        //second struct -> Output -> 32 variables -> size 26
        private byte[] read_buffer_DB4_Output = new byte[1024]; //26
        private byte[] send_buffer_DB4_Output = new byte[1024]; //26
        
        //DB5 => CarWash_DB -> 2 structs -> 23 variables -> size 3.7
        private int DBNumber_DB5 = 5;
        //first struct -> Input -> 7 variables -> 0.6 size 
        private byte[] read_buffer_DB5_Input = new byte[1024]; //3
        private byte[] send_buffer_DB5_Input = new byte[1024]; //3
        //second struct -> Output -> 16 variables -> 3.7 size
        private byte[] read_buffer_DB5_Output = new byte[1024]; //3
        private byte[] send_buffer_DB5_Output = new byte[1024]; //3

        //MaintainDB variables
        bool Option1 = false;
        bool Option2 = false;
        bool Option3 = false;

        //ElevatorDB variables 
        #region ElevatorDB variables 

        //inputs
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

        //outputs
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

        #endregion

        //CarWashDB variables 
        #region CarWashDB variables

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

        bool CarWashPositionShower = false;
        bool CarWashPositionCar = false;
        bool CarWashGreenLight = false;
        bool CarWashRedLight = false;
        bool CarWashYellowLight = false;
        bool CarWashDoor1UP = false;
        bool CarWashDoor1DOWN = false;
        bool CarWashDoor2UP = false;
        bool CarWashDoor2DOWN = false;
        bool CarWashWater = false;
        bool CarWashWashingChemicalsFRONT = false;
        bool CarWashWashingChemicalsSIDES = false;
        bool CarWashWashingChemicalsBACK = false;
        bool CarWashWax = false;
        bool CarWashVarnishProtection = false;
        bool CarWashDry = false;
        bool CarWashSoap = false;
        bool CarWashActiveFoam = false;
        bool CarWashBrushes = false;

        #endregion

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

                //DB11 => Maintain_DB - modes and timers
                reader.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, DBNumber_DB11, 0, read_buffer_DB11.Length, ref read_buffer_DB11); 

                int readResultDB11 = reader.Read();

                if (readResultDB11 == 0)
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
                        MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data from PLC weren't read from DB11! \n\n" +
                            $"Error message: {readResultDB11} \n", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }

                //DB4 => Elevator_DB -> 2 structs -> 46 variables -> size 2
                //first struct -> Input -> 14 variables -> size 1.5 
                reader.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, DBNumber_DB4, 0, 2, ref read_buffer_DB4_Input);
                //second struct -> Output -> 32 variables -> size 26
                //reader.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, DBNumber_DB4, 2, 32, ref read_buffer_DB4_Output); 

                int readResultDB4 = reader.Read();

                if (readResultDB4 == 0)
                {
                    //read OK
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
                else
                {
                    //error
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

                /*
                //Reading variables with DBRead method
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
                S7.SetBitAt(send_buffer_DB11, 0, 0, true);
                                
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
                S7.SetBitAt(send_buffer_DB11, 0, 1, Option2);

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
            S7.SetBitAt(send_buffer_DB11, 0, 0, Option1);
            //Option2 = false
            Option2 = false;
            S7.SetBitAt(send_buffer_DB11, 0, 1, Option2);
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
            }

            //Tia disconnect
            client.Disconnect();

            //close program
            this.Close();
        }
        #endregion
    }
}
