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
        public S7MultiVar s7MultiVar;

        //ChooseOptionForm
        //we need to read/write 3 bits (3 times bool) -> 1 byte
        private byte[] read_buffer_ChooseOptionForm = new byte[1];
        private byte[] send_buffer_ChooseOptionForm = new byte[1];

        //DB14 => Crossroad_DB 110.0
        private byte[] read_buffer_DB14 = new byte[111]; 
        private byte[] send_buffer_DB14 = new byte[111];

        //DB1 => Crossroad_1_DB - Crossroad 1 6.3
        private byte[] read_buffer_DB1 = new byte[7];
        private byte[] send_buffer_DB1 = new byte[7];

        //DB19 => Crossroad_2_DB - Crossroad 2 6.3
        private byte[] read_buffer_DB19 = new byte[7];
        private byte[] send_buffer_DB19 = new byte[7];

        //DB20 => Crossroad_LeftT_DB - Left T 5.4 
        private byte[] read_buffer_DB20 = new byte[6];
        private byte[] send_buffer_DB20 = new byte[6];

        //DB21 => Crossroad_RightT_DB - Right T 5.4
        private byte[] read_buffer_DB21 = new byte[6];
        private byte[] send_buffer_DB21 = new byte[6];

        //input 
        #region Input variables 

        //Crossroad_DB DB14
        #region Crossroad_DB DB14

        bool CrossroadPedestrianBTN;
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

        #endregion

        //Crossroad_2_DB DB19
        #region Crossroad_2_DB DB19

        #endregion

        //Crossroad_LeftT_DB DB20
        #region Crossroad_LeftT_DB DB20

        #endregion

        //Crossroad_RightT_DB DB21
        #region Crossroad_RightT_DB DB21

        #endregion

        //crossorad1
        #region Crossroad1

        bool Crossroad1TopRED;
        bool Crossroad1TopGREEN;
        bool Crossroad1TopYellow;
        bool Crossroad1LeftRED;
        bool Crossroad1LeftGREEN;
        bool Crossroad1LeftYellow;
        bool Crossroad1RightRED;
        bool Crossroad1RightGREEN;
        bool Crossroad1RightYellow;
        bool Crossroad1BottomRED;
        bool Crossroad1BottomGREEN;
        bool Crossroad1BottomYellow;

        bool Crossroad1TopCrosswalkRED1;
        bool Crossroad1TopCrosswalkRED2;
        bool Crossroad1TopCrosswalkGREEN1;
        bool Crossroad1TopCrosswalkGREEN2;
        bool Crossroad1LeftCrosswalkRED1;
        bool Crossroad1LeftCrosswalkRED2;
        bool Crossroad1LeftCrosswalkGREEN1;
        bool Crossroad1LeftCrosswalkGREEN2;
        bool Crossroad1RightCrosswalkRED1;
        bool Crossroad1RightCrosswalkRED2;
        bool Crossroad1RightCrosswalkGREEN1;
        bool Crossroad1RightCrosswalkGREEN2;
        bool Crossroad1BottomCrosswalkRED1;
        bool Crossroad1BottomCrosswalkRED2;
        bool Crossroad1BottomCrosswalkGREEN1;
        bool Crossroad1BottomCrosswalkGREEN2;

        #endregion

        //crossroad2
        #region Crossroad2

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

        bool Crossroad2TopCrosswalkRED1;
        bool Crossroad2TopCrosswalkRED2;
        bool Crossroad2TopCrosswalkGREEN1;
        bool Crossroad2TopCrosswalkGREEN2;
        bool Crossroad2LeftCrosswalkRED1;
        bool Crossroad2LeftCrosswalkRED2;
        bool Crossroad2LeftCrosswalkGREEN1;
        bool Crossroad2LeftCrosswalkGREEN2;
        bool Crossroad2RightCrosswalkRED1;
        bool Crossroad2RightCrosswalkRED2;
        bool Crossroad2RightCrosswalkGREEN1;
        bool Crossroad2RightCrosswalkGREEN2;
        bool Crossroad2BottomCrosswalkRED1;
        bool Crossroad2BottomCrosswalkRED2;
        bool Crossroad2BottomCrosswalkGREEN1;
        bool Crossroad2BottomCrosswalkGREEN2;

        #endregion

        //LeftT
        #region LeftT

        bool CrossroadLeftTTopRED;
        bool CrossroadLeftTTopGREEN;
        bool CrossroadLeftTTopYellow;
        bool CrossroadLeftTLeftRED;
        bool CrossroadLeftTLeftGREEN;
        bool CrossroadLeftTLeftYellow;
        bool CrossroadLeftTRightRED;
        bool CrossroadLeftTRightGREEN;
        bool CrossroadLeftTRightYellow;

        bool CrossroadLeftTTopCrosswalkRED1;
        bool CrossroadLeftTTopCrosswalkRED2;
        bool CrossroadLeftTTopCrosswalkGREEN1;
        bool CrossroadLeftTTopCrosswalkGREEN2;
        bool CrossroadLeftTLeftCrosswalkRED1;
        bool CrossroadLeftTLeftCrosswalkRED2;
        bool CrossroadLeftTLeftCrosswalkGREEN1;
        bool CrossroadLeftTLeftCrosswalkGREEN2;
        bool CrossroadLeftTRightCrosswalkRED1;
        bool CrossroadLeftTRightCrosswalkRED2;
        bool CrossroadLeftTRightCrosswalkGREEN1;
        bool CrossroadLeftTRightCrosswalkGREEN2;

        #endregion

        //RightT
        #region RightT

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
        bool CrossroadRightTLeftCrosswalkRED1;
        bool CrossroadRightTLeftCrosswalkRED2;
        bool CrossroadRightTLeftCrosswalkGREEN1;
        bool CrossroadRightTLeftCrosswalkGREEN2;
        bool CrossroadRightTRightCrosswalkRED1;
        bool CrossroadRightTRightCrosswalkRED2;
        bool CrossroadRightTRightCrosswalkGREEN1;
        bool CrossroadRightTRightCrosswalkGREEN2;

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

                s7MultiVar = new S7MultiVar(client);

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
                }
                else
                {
                    //error
                }

                #endregion

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
                    //Inpit variable
                    #region Input variables

                    Crossroad1LeftCrosswalkBTN1 = S7.GetBitAt(read_buffer_DB14, 0, 0); 
                    Crossroad1LeftCrosswalkBTN2 = S7.GetBitAt(read_buffer_DB14, 0, 1);
                    Crossroad1TopCrosswalkBTN1 = S7.GetBitAt(read_buffer_DB14, 0, 2);
                    Crossroad1TopCrosswalkBTN2 = S7.GetBitAt(read_buffer_DB14, 0, 3);

                    #endregion

                    //Output variables
                    #region Output variables

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
                    //Inpit variable
                    #region Input variables

                    Crossroad2LeftCrosswalkBTN1 = S7.GetBitAt(read_buffer_DB19, 0, 0);
                    Crossroad2LeftCrosswalkBTN2 = S7.GetBitAt(read_buffer_DB19, 0, 1);
                    Crossroad2TopCrosswalkBTN1 = S7.GetBitAt(read_buffer_DB19, 0, 2);
                    Crossroad2TopCrosswalkBTN2 = S7.GetBitAt(read_buffer_DB19, 0, 3);

                    #endregion

                    //Output variables
                    #region Output variables

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
                    //Inpit variable
                    #region Input variables

                    CrossroadLeftTLeftCrosswalkBTN1 = S7.GetBitAt(read_buffer_DB19, 0, 0);
                    CrossroadLeftTLeftCrosswalkBTN2= S7.GetBitAt(read_buffer_DB19, 0, 1);

                    #endregion

                    //Output variables
                    #region Output variables

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
                    //Inpit variable
                    #region Input variables

                    CrossroadRightTTopCrosswalkBTN1 = S7.GetBitAt(read_buffer_DB19, 0, 0); 
                    CrossroadRightTTopCrosswalkBTN2 = S7.GetBitAt(read_buffer_DB19, 0, 1);

                    #endregion

                    //Output variables
                    #region Output variables

                    #endregion
                }

                #endregion


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
            S7.SetBitAt(ref send_buffer_ChooseOptionForm, 0, 2, Option3);

            //write to PLC
            int writeResult = client.DBWrite(11, 0, send_buffer_ChooseOptionForm.Length, send_buffer_ChooseOptionForm);
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

            this.Close();
        }
        #endregion


        private void btnTest_Click(object sender, EventArgs e)
        {
            
        }
    }
}
