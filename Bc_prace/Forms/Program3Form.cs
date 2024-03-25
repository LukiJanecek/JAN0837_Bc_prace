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
using Newtonsoft.Json.Bson;

namespace Bc_prace
{
    public partial class Program3Form : Form
    {
        private ChooseOptionForm chooseOptionFormInstance;

        public S7Client client;

        //MessageBox control
        private bool errorMessageBoxShown = false;

        //MaintainDB variables
        bool Option3 = false;

        public bool crossroadBasic;
        public bool crossroadExtension1;
        public bool crossroadExtension2;
        public bool crossroadExtension3;

        //Buffers variables 
        #region Buffers variables

        //DB11 => Maintain_DB -> 1 struct -> 3 variables -> size 0.2
        private int DBNumber_DB11 = 11;
        byte[] read_buffer_DB11;
        byte[] send_buffer_DB11;

        //DB14 => Crossroad_DB -> 11 structs -> x variables -> size 110.0 
        private int DBNumber_DB14 = 14;
        public byte[] read_buffer_DB14;
        public byte[] previous_buffer_DB14;
        public byte[] PreviousBufferHash_DB14;
        public byte[] send_buffer_DB14;
        //+ other structs are Timers 

        //DB1 => Crossroad_1_DB -> Crossroad 1 -> 2 structs -> 25 variables -> size 6.3
        private int DBNumber_DB1 = 1;
        public byte[] read_buffer_DB1;
        public byte[] previous_buffer_DB1;
        public byte[] PreviousBufferHash_DB1;
        public byte[] send_buffer_DB1;

        //DB19 => Crossroad_2_DB -> Crossroad 2 -> 2 structs -> 25 variables -> size 6.3  
        private int DBNumber_DB19 = 19;
        public byte[] read_buffer_DB19;
        public byte[] previous_buffer_DB19;
        public byte[] PreviousBufferHash_DB19;
        public byte[] send_buffer_DB19;

        //DB20 => Crossroad_LeftT_DB - Left T -> 2 structs -> 16 variables -> size 5.4 
        private int DBNumber_DB20 = 20;
        public byte[] read_buffer_DB20;
        public byte[] previous_buffer_DB20t;
        public byte[] PreviousBufferHash_DB20;
        public byte[] send_buffer_DB20;

        //DB21 => Crossroad_RightT_DB - Right T -> 2 structs -> 16 variables -> size 5.4 
        private int DBNumber_DB21 = 21;
        public byte[] read_buffer_DB21;
        public byte[] previous_buffer_DB21;
        public byte[] PreviousBufferHash_DB21;
        public byte[] send_buffer_DB21;

        #endregion

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

        int Crossroad2CrosswalkSQ;

        public bool Crossroad2TopRED;
        public bool Crossroad2TopGREEN;
        public bool Crossroad2TopYELLOW;
        public bool Crossroad2LeftRED;
        public bool Crossroad2LeftGREEN;
        public bool Crossroad2LeftYELLOW;
        public bool Crossroad2RightRED;
        public bool Crossroad2RightGREEN;
        public bool Crossroad2RightYELLOW;
        public bool Crossroad2BottomRED;
        public bool Crossroad2BottomGREEN;
        public bool Crossroad2BottomYELLOW;

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
        public bool CrossroadLeftTTopYELLOW;
        public bool CrossroadLeftTLeftRED;
        public bool CrossroadLeftTLeftGREEN;
        public bool CrossroadLeftTLeftYELLOW;
        public bool CrossroadLeftTRightRED;
        public bool CrossroadLeftTRightGREEN;
        public bool CrossroadLeftTRightYELLOW;

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
        public bool CrossroadRightTTopYELLOW;
        public bool CrossroadRightTLeftRED;
        public bool CrossroadRightTLeftGREEN;
        public bool CrossroadRightTLeftYELLOW;
        public bool CrossroadRightTRightRED;
        public bool CrossroadRightTRightGREEN;
        public bool CrossroadRightTRightYELLOW;

        public bool CrossroadRightTTopCrosswalkRED1;
        public bool CrossroadRightTTopCrosswalkRED2;
        public bool CrossroadRightTTopCrosswalkGREEN1;
        public bool CrossroadRightTTopCrosswalkGREEN2;

        #endregion

        #endregion

        public Program3Form(ChooseOptionForm chooseOptionFormInstance)
        {
            InitializeComponent();
            this.MinimumSize = new Size(1000, 700); //crossroad1 + btns
            userControlCrossroad1.OnCrossroadBTNClick += UserControlCrossroad1_OnCrossroadBTNClick;

            this.chooseOptionFormInstance = chooseOptionFormInstance;

            client = chooseOptionFormInstance.client;

            //Buffers initialize
            #region Buffers initialize

            //DB11 => Maintain_DB
            read_buffer_DB11 = chooseOptionFormInstance.read_buffer_DB11;
            send_buffer_DB11 = chooseOptionFormInstance.send_buffer_DB11;
            //DB14 => Crossroad_DB
            read_buffer_DB14 = chooseOptionFormInstance.read_buffer_DB14;
            send_buffer_DB14 = chooseOptionFormInstance.send_buffer_DB14;
            //DB1 => Crossroad_1_DB
            read_buffer_DB1 = chooseOptionFormInstance.read_buffer_DB1;
            send_buffer_DB1 = chooseOptionFormInstance.send_buffer_DB1;
            //DB19 => Crossroad_2_DB
            read_buffer_DB19 = chooseOptionFormInstance.read_buffer_DB19;
            send_buffer_DB19 = chooseOptionFormInstance.send_buffer_DB19;
            //DB20 => Crossroad_LeftT_DB
            read_buffer_DB20 = chooseOptionFormInstance.read_buffer_DB20;
            send_buffer_DB20 = chooseOptionFormInstance.send_buffer_DB20;
            //DB21 => Crossroad_RightT_DB
            read_buffer_DB21 = chooseOptionFormInstance.read_buffer_DB21;
            send_buffer_DB21 = chooseOptionFormInstance.send_buffer_DB21;

            #endregion

            if (client.Connected)
            {
                //start timer
                Timer_read_actual.Start();
                //set time interval (ms)
                Timer_read_actual.Interval = 100;
            }
        }

        private void Program3_Load(object sender, EventArgs e)
        {
            userControlCrossroad1.SetControl(this);

            this.WindowState = FormWindowState.Maximized;

            rBtnCrossroadBasic.Checked = false;
            rBtnCrossroadExtension1.Checked = false;
            rBtnCrossroadExtension2.Checked = false;
            rBtnCrossroadExtension3.Checked = false;
        }

        private void UserControlCrossroad1_OnCrossroadBTNClick(object sender, string id)
        {
            switch(id)
            {
                case "Crossroad1 Top crosswalk BTN1":

                    Crossroad1TopCrosswalkBTN1 = true;
                    S7.SetBitAt(send_buffer_DB1, 0, 2, Crossroad1TopCrosswalkBTN1);

                    //write to PLC
                    int writeResultDB1_Crossroad1TopCrosswalkBTN1 = client.DBWrite(DBNumber_DB1, 0, send_buffer_DB1.Length, send_buffer_DB1);
                    if (writeResultDB1_Crossroad1TopCrosswalkBTN1 != 0)
                    {
                        //write error
                        if (!errorMessageBoxShown)
                        {
                            //MessageBox
                            MessageBox.Show("BE doesn't work properly. Data could´t be written to DB1!!! \n\n" +
                                $"Error message: writeResultDB1_Crossroad1TopCrosswalkBTN1 = {writeResultDB1_Crossroad1TopCrosswalkBTN1} \n", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                            errorMessageBoxShown = true;
                        }
                    }
                    else
                    {
                        //write was successful
                    }

                    break;
                case "Crossroad1 Top crosswalk BTN2":

                    Crossroad1TopCrosswalkBTN2 = true;
                    S7.SetBitAt(send_buffer_DB1, 0, 3, Crossroad1TopCrosswalkBTN2);

                    //write to PLC
                    int writeResultDB1_Crossroad1TopCrosswalkBTN2 = client.DBWrite(DBNumber_DB1, 0, send_buffer_DB1.Length, send_buffer_DB1);
                    if (writeResultDB1_Crossroad1TopCrosswalkBTN2 != 0)
                    {
                        //write error
                        if (!errorMessageBoxShown)
                        {
                            //MessageBox
                            MessageBox.Show("BE doesn't work properly. Data could´t be written to DB1!!! \n\n" +
                                $"Error message: writeResultDB1_Crossroad1TopCrosswalkBTN2 = {writeResultDB1_Crossroad1TopCrosswalkBTN2} \n", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                            errorMessageBoxShown = true;
                        }
                    }
                    else
                    {
                        //write was successful
                    }

                    break;
                case "Crossroad1 Left crosswalk BTN1":

                    Crossroad1LeftCrosswalkBTN1 = true;
                    S7.SetBitAt(send_buffer_DB1, 0, 0, Crossroad1LeftCrosswalkBTN1);

                    //write to PLC
                    int writeResultDB1_Crossroad1LeftCrosswalkBTN1 = client.DBWrite(DBNumber_DB1, 0, send_buffer_DB1.Length, send_buffer_DB1);
                    if (writeResultDB1_Crossroad1LeftCrosswalkBTN1 != 0)
                    {
                        //write error
                        if (!errorMessageBoxShown)
                        {
                            //MessageBox
                            MessageBox.Show("BE doesn't work properly. Data could´t be written to DB1!!! \n\n" +
                                $"Error message: writeResultDB1_Crossroad1LeftCrosswalkBTN1 = {writeResultDB1_Crossroad1LeftCrosswalkBTN1} \n", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                            errorMessageBoxShown = true;
                        }
                    }
                    else
                    {
                        //write was successful
                    }

                    break;
                case "Crossroad1 Left crosswalk BTN2":

                    Crossroad1LeftCrosswalkBTN2 = true;
                    S7.SetBitAt(send_buffer_DB1, 0, 1, Crossroad1LeftCrosswalkBTN2);

                    //write to PLC
                    int writeResultDB1_Crossroad1LeftCrosswalkBTN2 = client.DBWrite(DBNumber_DB1, 0, send_buffer_DB1.Length, send_buffer_DB1);
                    if (writeResultDB1_Crossroad1LeftCrosswalkBTN2 != 0)
                    {
                        //write error
                        if (!errorMessageBoxShown)
                        {
                            //MessageBox
                            MessageBox.Show("BE doesn't work properly. Data could´t be written to DB1!!! \n\n" +
                                $"Error message: writeResultDB1_Crossroad1LeftCrosswalkBTN2 = {writeResultDB1_Crossroad1LeftCrosswalkBTN2} \n", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                            errorMessageBoxShown = true;
                        }
                    }
                    else
                    {
                        //write was successful
                    }

                    break;
                case "Crossroad2 Left crosswalk BTN1":

                    Crossroad2LeftCrosswalkBTN1 = true;
                    S7.SetBitAt(send_buffer_DB19, 0, 0, Crossroad2LeftCrosswalkBTN1);

                    //write to PLC
                    int writeResultDB1_Crossroad2LeftCrosswalkBTN1 = client.DBWrite(DBNumber_DB19, 0, send_buffer_DB19.Length, send_buffer_DB19);
                    if (writeResultDB1_Crossroad2LeftCrosswalkBTN1 != 0)
                    {
                        //write error
                        if (!errorMessageBoxShown)
                        {
                            //MessageBox
                            MessageBox.Show("BE doesn't work properly. Data could´t be written to DB19!!! \n\n" +
                                $"Error message: writeResultDB1_Crossroad2LeftCrosswalkBTN1 = {writeResultDB1_Crossroad2LeftCrosswalkBTN1} \n", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                            errorMessageBoxShown = true;
                        }
                    }
                    else
                    {
                        //write was successful
                    }

                    break;
                case "Crossroad2 Left crosswalk BTN2":

                    Crossroad2LeftCrosswalkBTN2 = true;
                    S7.SetBitAt(send_buffer_DB19, 0, 1, Crossroad2LeftCrosswalkBTN2);

                    //write to PLC
                    int writeResultDB1_Crossroad2LeftCrosswalkBTN2 = client.DBWrite(DBNumber_DB19, 0, send_buffer_DB19.Length, send_buffer_DB19);
                    if (writeResultDB1_Crossroad2LeftCrosswalkBTN2 != 0)
                    {
                        //write error
                        if (!errorMessageBoxShown)
                        {
                            //MessageBox
                            MessageBox.Show("BE doesn't work properly. Data could´t be written to DB19!!! \n\n" +
                                $"Error message: writeResultDB1_Crossroad2LeftCrosswalkBTN2 = {writeResultDB1_Crossroad2LeftCrosswalkBTN2} \n", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                            errorMessageBoxShown = true;
                        }
                    }
                    else
                    {
                        //write was successful
                    }

                    break;
                case "Crossroad2 Right crosswalk BTN1":

                    Crossroad2RightCrosswalkBTN1 = true;
                    S7.SetBitAt(send_buffer_DB19, 0, 2, Crossroad2RightCrosswalkBTN1);

                    //write to PLC
                    int writeResultDB1_Crossroad2RightCrosswalkBTN1 = client.DBWrite(DBNumber_DB19, 0, send_buffer_DB19.Length, send_buffer_DB19);
                    if (writeResultDB1_Crossroad2RightCrosswalkBTN1 != 0)
                    {
                        //write error
                        if (!errorMessageBoxShown)
                        {
                            //MessageBox
                            MessageBox.Show("BE doesn't work properly. Data could´t be written to DB19!!! \n\n" +
                                $"Error message: writeResultDB1_Crossroad2RightCrosswalkBTN1 = {writeResultDB1_Crossroad2RightCrosswalkBTN1} \n", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                            errorMessageBoxShown = true;
                        }
                    }
                    else
                    {
                        //write was successful
                    }

                    break;
                case "Crossroad2 Right crosswalk BTN2":

                    Crossroad2RightCrosswalkBTN2 = true;
                    S7.SetBitAt(send_buffer_DB19, 0, 3, Crossroad2RightCrosswalkBTN2);

                    //write to PLC
                    int writeResultDB1_Crossroad2RightCrosswalkBTN2 = client.DBWrite(DBNumber_DB19, 0, send_buffer_DB19.Length, send_buffer_DB19);
                    if (writeResultDB1_Crossroad2RightCrosswalkBTN2 != 0)
                    {
                        //write error
                        if (!errorMessageBoxShown)
                        {
                            //MessageBox
                            MessageBox.Show("BE doesn't work properly. Data could´t be written to DB19!!! \n\n" +
                                $"Error message: writeResultDB1_Crossroad2RightCrosswalkBTN2 = {writeResultDB1_Crossroad2RightCrosswalkBTN2} \n", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                            errorMessageBoxShown = true;
                        }
                    }
                    else
                    {
                        //write was successful
                    }

                    break;
                case "LeftT\nLeft crosswalk BTN1":

                    CrossroadLeftTLeftCrosswalkBTN1 = true;
                    S7.SetBitAt(send_buffer_DB20, 0, 0, CrossroadLeftTLeftCrosswalkBTN1);

                    //write to PLC
                    int writeResultDB1_CrossroadLeftTLeftCrosswalkBTN1 = client.DBWrite(DBNumber_DB20, 0, send_buffer_DB20.Length, send_buffer_DB20);
                    if (writeResultDB1_CrossroadLeftTLeftCrosswalkBTN1 != 0)
                    {
                        //write error
                        if (!errorMessageBoxShown)
                        {
                            //MessageBox
                            MessageBox.Show("BE doesn't work properly. Data could´t be written to DB20!!! \n\n" +
                                $"Error message: writeResultDB1_CrossroadLeftTLeftCrosswalkBTN1 = {writeResultDB1_CrossroadLeftTLeftCrosswalkBTN1} \n", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                            errorMessageBoxShown = true;
                        }
                    }
                    else
                    {
                        //write was successful
                    }

                    break;
                case "LeftT\nLeft crosswalk BTN2":

                    CrossroadLeftTLeftCrosswalkBTN2 = true;
                    S7.SetBitAt(send_buffer_DB20, 0, 1, CrossroadLeftTLeftCrosswalkBTN2);

                    //write to PLC
                    int writeResultDB1_CrossroadLeftTLeftCrosswalkBTN2 = client.DBWrite(DBNumber_DB20, 0, send_buffer_DB20.Length, send_buffer_DB20);
                    if (writeResultDB1_CrossroadLeftTLeftCrosswalkBTN2 != 0)
                    {
                        //write error
                        if (!errorMessageBoxShown)
                        {
                            //MessageBox
                            MessageBox.Show("BE doesn't work properly. Data could´t be written to DB20!!! \n\n" +
                                $"Error message: writeResultDB1_CrossroadLeftTLeftCrosswalkBTN2 = {writeResultDB1_CrossroadLeftTLeftCrosswalkBTN2} \n", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                            errorMessageBoxShown = true;
                        }
                    }
                    else
                    {
                        //write was successful
                    }

                    break;
                case "RightT\nTop crosswalk BTN1":

                    CrossroadRightTTopCrosswalkBTN1 = true;
                    S7.SetBitAt(send_buffer_DB21, 0, 0, CrossroadRightTTopCrosswalkBTN1);

                    //write to PLC
                    int writeResultDB1_CrossroadRightTTopCrosswalkBTN1 = client.DBWrite(DBNumber_DB21, 0, send_buffer_DB21.Length, send_buffer_DB21);
                    if (writeResultDB1_CrossroadRightTTopCrosswalkBTN1 != 0)
                    {
                        //write error
                        if (!errorMessageBoxShown)
                        {
                            //MessageBox
                            MessageBox.Show("BE doesn't work properly. Data could´t be written to DB21!!! \n\n" +
                                $"Error message: writeResultDB1_CrossroadRightTTopCrosswalkBTN1 = {writeResultDB1_CrossroadRightTTopCrosswalkBTN1} \n", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                            errorMessageBoxShown = true;
                        }
                    }
                    else
                    {
                        //write was successful
                    }

                    break;
                case "RightT\nTop crosswalk BTN2":

                    CrossroadRightTTopCrosswalkBTN2 = true;
                    S7.SetBitAt(send_buffer_DB21, 0, 1, CrossroadRightTTopCrosswalkBTN2);

                    //write to PLC
                    int writeResultDB1_CrossroadRightTTopCrosswalkBTN2 = client.DBWrite(DBNumber_DB21, 0, send_buffer_DB21.Length, send_buffer_DB21);
                    if (writeResultDB1_CrossroadRightTTopCrosswalkBTN2 != 0)
                    {
                        //write error
                        if (!errorMessageBoxShown)
                        {
                            //MessageBox
                            MessageBox.Show("BE doesn't work properly. Data could´t be written to DB21!!! \n\n" +
                                $"Error message: writeResultDB1_CrossroadRightTTopCrosswalkBTN2 = {writeResultDB1_CrossroadRightTTopCrosswalkBTN2} \n", "Error",
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

        //Reading variables + actions on variable value
        #region Reading variables + actions on variable value

        private void Timer_read_actual_Tick(object sender, EventArgs e)
        {
            try
            {
                Option3 = chooseOptionFormInstance.Option3;

                //Input variables
                #region Input variables 

                //Crossroad_DB DB14
                #region Crossroad_DB DB14

                CrossroadModeOFF = chooseOptionFormInstance.CrossroadModeOFF; //no need to make a condition if true
                CrossroadModeNIGHT = chooseOptionFormInstance.CrossroadModeNIGHT; //no need to make a condition if true
                CrossroadModeDAY = chooseOptionFormInstance.CrossroadModeDAY; //no need to make a condition if true
                CrossroadEmergencySTOP = chooseOptionFormInstance.CrossroadEmergencySTOP;
                CrossroadErrorSystem = chooseOptionFormInstance.CrossroadErrorSystem;

                #endregion

                //Crossroad_1_DB DB1
                #region Crossroad_1_DB DB1

                Crossroad1LeftCrosswalkBTN1 = chooseOptionFormInstance.Crossroad1LeftCrosswalkBTN1; //no need to make a condition if true
                Crossroad1LeftCrosswalkBTN2 = chooseOptionFormInstance.Crossroad1LeftCrosswalkBTN2; //no need to make a condition if true
                Crossroad1TopCrosswalkBTN1 = chooseOptionFormInstance.Crossroad1TopCrosswalkBTN1; //no need to make a condition if true
                Crossroad1TopCrosswalkBTN2 = chooseOptionFormInstance.Crossroad1TopCrosswalkBTN2; //no need to make a condition if true

                #endregion

                //Crossroad_2_DB DB19
                #region Crossroad_2_DB DB19

                Crossroad2LeftCrosswalkBTN1 = chooseOptionFormInstance.Crossroad2LeftCrosswalkBTN1; //no need to make a condition if true
                Crossroad2LeftCrosswalkBTN2 = chooseOptionFormInstance.Crossroad2LeftCrosswalkBTN2; //no need to make a condition if true
                Crossroad2RightCrosswalkBTN1 = chooseOptionFormInstance.Crossroad2RightCrosswalkBTN1; //no need to make a condition if true
                Crossroad2RightCrosswalkBTN2 = chooseOptionFormInstance.Crossroad2RightCrosswalkBTN2; //no need to make a condition if true

                #endregion

                //Crossroad_LeftT_DB DB20
                #region Crossroad_LeftT_DB DB20

                CrossroadLeftTLeftCrosswalkBTN1 = chooseOptionFormInstance.CrossroadLeftTLeftCrosswalkBTN1; //no need to make a condition if true
                CrossroadLeftTLeftCrosswalkBTN2 = chooseOptionFormInstance.CrossroadLeftTLeftCrosswalkBTN2; //no need to make a condition if true

                #endregion

                //Crossroad_RightT_DB DB21
                #region Crossroad_RightT_DB DB21

                CrossroadRightTTopCrosswalkBTN1 = chooseOptionFormInstance.CrossroadRightTTopCrosswalkBTN1; //no need to make a condition if true
                CrossroadRightTTopCrosswalkBTN2 = chooseOptionFormInstance.CrossroadRightTTopCrosswalkBTN2; //no need to make a condition if true

                #endregion

                #endregion

                //Output variables
                #region Output variables 

                //Crossroad_DB DB14
                #region Crossroad_DB DB14

                TrafficLightsSQ = chooseOptionFormInstance.TrafficLightsSQ; //no need to make a condition if true

                #endregion

                //Crossroad_1_DB DB1
                #region Crossroad_1_DB DB1

                Crossroad1CrosswalkSQ = chooseOptionFormInstance.Crossroad1CrosswalkSQ; //no need to make a condition if true

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

                Crossroad2CrosswalkSQ = chooseOptionFormInstance.Crossroad2CrosswalkSQ; //no need to make a condition if true

                Crossroad2TopRED = chooseOptionFormInstance.Crossroad2TopRED;
                Crossroad2TopGREEN = chooseOptionFormInstance.Crossroad2TopGREEN;
                Crossroad2TopYELLOW = chooseOptionFormInstance.Crossroad2TopYellow;
                Crossroad2LeftRED = chooseOptionFormInstance.Crossroad2LeftRED;
                Crossroad2LeftGREEN = chooseOptionFormInstance.Crossroad2LeftGREEN;
                Crossroad2LeftYELLOW = chooseOptionFormInstance.Crossroad2LeftYellow;
                Crossroad2RightRED = chooseOptionFormInstance.Crossroad2RightRED;
                Crossroad2RightGREEN = chooseOptionFormInstance.Crossroad2RightGREEN;
                Crossroad2RightYELLOW = chooseOptionFormInstance.Crossroad2RightYellow;
                Crossroad2BottomRED = chooseOptionFormInstance.Crossroad2BottomRED;
                Crossroad2BottomGREEN = chooseOptionFormInstance.Crossroad2BottomGREEN;
                Crossroad2BottomYELLOW = chooseOptionFormInstance.Crossroad2BottomYellow;

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

                CrossroadLeftTCrosswalkSQ = chooseOptionFormInstance.CrossroadLeftTCrosswalkSQ; //no need to make a condition if true

                CrossroadLeftTTopRED = chooseOptionFormInstance.CrossroadLeftTTopRED;
                CrossroadLeftTTopGREEN = chooseOptionFormInstance.CrossroadLeftTTopGREEN;
                CrossroadLeftTTopYELLOW = chooseOptionFormInstance.CrossroadLeftTTopYellow;
                CrossroadLeftTLeftRED = chooseOptionFormInstance.CrossroadLeftTLeftRED;
                CrossroadLeftTLeftGREEN = chooseOptionFormInstance.CrossroadLeftTLeftGREEN;
                CrossroadLeftTLeftYELLOW = chooseOptionFormInstance.CrossroadLeftTLeftYellow;
                CrossroadLeftTRightRED = chooseOptionFormInstance.CrossroadLeftTRightRED;
                CrossroadLeftTRightGREEN = chooseOptionFormInstance.CrossroadLeftTRightGREEN;
                CrossroadLeftTRightYELLOW = chooseOptionFormInstance.CrossroadLeftTRightYellow;

                CrossroadLeftTLeftCrosswalkRED1 = chooseOptionFormInstance.CrossroadLeftTLeftCrosswalkRED1;
                CrossroadLeftTLeftCrosswalkRED2 = chooseOptionFormInstance.CrossroadLeftTLeftCrosswalkRED2;
                CrossroadLeftTLeftCrosswalkGREEN1 = chooseOptionFormInstance.CrossroadLeftTLeftCrosswalkGREEN1;
                CrossroadLeftTLeftCrosswalkGREEN2 = chooseOptionFormInstance.CrossroadLeftTLeftCrosswalkGREEN2;

                #endregion

                //Crossroad_RightT_DB DB21
                #region Crossroad_RightT_DB DB21

                CrossroadRightTCrosswalkSQ = chooseOptionFormInstance.CrossroadRightTCrosswalkSQ; //no need to make a condition if true

                CrossroadRightTTopRED = chooseOptionFormInstance.CrossroadRightTTopRED;
                CrossroadRightTTopGREEN = chooseOptionFormInstance.CrossroadRightTTopGREEN;
                CrossroadRightTTopYELLOW = chooseOptionFormInstance.CrossroadRightTTopYellow;
                CrossroadRightTLeftRED = chooseOptionFormInstance.CrossroadRightTLeftRED;
                CrossroadRightTLeftGREEN = chooseOptionFormInstance.CrossroadRightTLeftGREEN;
                CrossroadRightTLeftYELLOW = chooseOptionFormInstance.CrossroadRightTLeftYellow;
                CrossroadRightTRightRED = chooseOptionFormInstance.CrossroadRightTRightRED;
                CrossroadRightTRightGREEN = chooseOptionFormInstance.CrossroadRightTRightGREEN;
                CrossroadRightTRightYELLOW = chooseOptionFormInstance.CrossroadRightTRightYellow;

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

                //Action on variable value
                #region Action on variable value

                if (CrossroadEmergencySTOP)
                {
                    statusStripCrossroad.Items.Clear();
                    ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Emergency mode activated");
                    statusStripCrossroad.Items.Add(lblStatus);

                    //write emergency status 
                    if (!errorMessageBoxShown)
                    {
                        //MessageBox
                        MessageBox.Show("Emergency mode activated. \r\n \n\n", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                        errorMessageBoxShown = true;
                    }
                }

                if (CrossroadErrorSystem)
                {
                    statusStripCrossroad.Items.Clear();
                    ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Error system");
                    statusStripCrossroad.Items.Add(lblStatus);

                    //write error
                    if (!errorMessageBoxShown)
                    {
                        //MessageBox
                        MessageBox.Show("Error system is true. There is an error in the process. \r\n \n\n", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                        errorMessageBoxShown = true;
                    }
                }

                //I never get into this condition!!! -> test me pls
                //Lights change
                if (crossroadBasic) //rBtnBasicCrossroad.Checked userControlCrossroad1.DrawBasicCrossroad == true
                {
                    //userControlCrossroad1.BasicCrossroad();

                    //Crossroad1
                    #region Crossroad1 

                    userControlCrossroad1.UpdateTrafficLightsCrossroad1TOP(Crossroad1TopRED, Crossroad1TopYELLOW, Crossroad1TopGREEN);
                    userControlCrossroad1.UpdateTrafficLightsCrossroad1LEFT(Crossroad1LeftRED, Crossroad1LeftYELLOW, Crossroad1LeftGREEN);
                    userControlCrossroad1.UpdateTrafficLightsCrossroad1RIGHT(Crossroad1RightRED, Crossroad1RightYELLOW, Crossroad1RightGREEN);
                    userControlCrossroad1.UpdateTrafficLightsCrossroad1BOTTOM(Crossroad1BottomRED, Crossroad1BottomYELLOW, Crossroad1BottomGREEN);
                    userControlCrossroad1.UpdateCrosswalkLightsCrossroad1TOP(Crossroad1TopCrosswalkRED1, Crossroad1TopCrosswalkGREEN1, Crossroad1TopCrosswalkRED2, Crossroad1TopCrosswalkGREEN2);
                    userControlCrossroad1.UpdateCrosswalkLightsCrossroad1LEFT(Crossroad1LeftCrosswalkRED1, Crossroad1LeftCrosswalkGREEN1, Crossroad1LeftCrosswalkRED2, Crossroad1LeftCrosswalkGREEN2);

                    #endregion                  

                    //Edit variables => didn't work properly
                    /*
                    //Top
                    userControlCrossroad1.Crossroad1TopRED = Crossroad1TopRED;
                    userControlCrossroad1.Crossroad1TopYELLOW = Crossroad1TopYELLOW;
                    userControlCrossroad1.Crossroad1TopRED = Crossroad1TopRED;
                    //Left
                    userControlCrossroad1.Crossroad1LeftRED = Crossroad1LeftRED;
                    userControlCrossroad1.Crossroad1LeftYELLOW = Crossroad1LeftYELLOW;
                    userControlCrossroad1.Crossroad1LeftGREEN = Crossroad1LeftGREEN;
                    //Right
                    userControlCrossroad1.Crossroad1RightRED = Crossroad1RightRED;
                    userControlCrossroad1.Crossroad1RightYELLOW = Crossroad1RightYELLOW;
                    userControlCrossroad1.Crossroad1RightGREEN = Crossroad1RightGREEN;
                    //Bottom
                    userControlCrossroad1.Crossroad1BottomRED = Crossroad1BottomRED;
                    userControlCrossroad1.Crossroad1BottomYELLOW = Crossroad1BottomYELLOW;
                    userControlCrossroad1.Crossroad1BottomGREEN = Crossroad1BottomGREEN;

                    //Crosswalk
                    //Top
                    userControlCrossroad1.Crossroad1TopCrosswalkRED1 = Crossroad1TopCrosswalkRED1;
                    userControlCrossroad1.Crossroad1TopCrosswalkRED2 = Crossroad1TopCrosswalkRED2;
                    userControlCrossroad1.Crossroad1TopCrosswalkGREEN1 = Crossroad1TopCrosswalkGREEN1;
                    userControlCrossroad1.Crossroad1TopCrosswalkGREEN2 = Crossroad1TopCrosswalkGREEN2;
                    //Left
                    userControlCrossroad1.Crossroad1LeftCrosswalkRED1 = Crossroad1LeftCrosswalkRED1;
                    userControlCrossroad1.Crossroad1LeftCrosswalkRED2 = Crossroad1LeftCrosswalkRED2;
                    userControlCrossroad1.Crossroad1LeftCrosswalkGREEN1 = Crossroad1LeftCrosswalkGREEN1;
                    userControlCrossroad1.Crossroad1LeftCrosswalkGREEN2 = Crossroad1LeftCrosswalkGREEN2;

                    */

                    //Testing 
                    /*
                    #region Testing
                    //TOP
                    userControlCrossroad1.UpdateTrafficLightsCrossroad1TOP(Crossroad1TopRED, Crossroad1TopYELLOW, Crossroad1TopGREEN);
                    userControlCrossroad1.UpdateCrosswalkLightsCrossroad1TOP(Crossroad1TopCrosswalkRED1, Crossroad1TopCrosswalkGREEN1, Crossroad1TopCrosswalkRED2, Crossroad1TopCrosswalkGREEN2);
                    //LEFT
                    userControlCrossroad1.UpdateTrafficLightsCrossroad1LEFT(Crossroad1LeftRED, Crossroad1LeftYELLOW, Crossroad1LeftGREEN);
                    userControlCrossroad1.UpdateCrosswalkLightsCrossroad1LEFT(Crossroad1LeftCrosswalkRED1, Crossroad1LeftCrosswalkGREEN1, Crossroad1LeftCrosswalkRED2, Crossroad1LeftCrosswalkGREEN2);
                    //RIGHT
                    userControlCrossroad1.UpdateTrafficLightsCrossroad1RIGHT(Crossroad1RightRED, Crossroad1RightYELLOW, Crossroad1RightGREEN);
                    //BOTTOM
                    userControlCrossroad1.UpdateTrafficLightsCrossroad1BOTTOM(Crossroad1BottomRED, Crossroad1BottomYELLOW, Crossroad1BottomGREEN);
                    #endregion
                    */
                }
                else if (crossroadExtension1) //rBtnCrossroadExtension1.Checked userControlCrossroad1.DrawCrossroadExtension1 == true
                {
                    //userControlCrossroad1.CrossroadExtension1();
                    
                    //Crossroad1
                    #region Crossroad1 

                    userControlCrossroad1.UpdateTrafficLightsCrossroad1TOP(Crossroad1TopRED, Crossroad1TopYELLOW, Crossroad1TopGREEN);
                    userControlCrossroad1.UpdateTrafficLightsCrossroad1LEFT(Crossroad1LeftRED, Crossroad1LeftYELLOW, Crossroad1LeftGREEN);
                    userControlCrossroad1.UpdateTrafficLightsCrossroad1RIGHT(Crossroad1RightRED, Crossroad1RightYELLOW, Crossroad1RightGREEN);
                    userControlCrossroad1.UpdateTrafficLightsCrossroad1BOTTOM(Crossroad1BottomRED, Crossroad1BottomYELLOW, Crossroad1BottomGREEN);
                    userControlCrossroad1.UpdateCrosswalkLightsCrossroad1TOP(Crossroad1TopCrosswalkRED1, Crossroad1TopCrosswalkGREEN1, Crossroad1TopCrosswalkRED2, Crossroad1TopCrosswalkGREEN2);
                    userControlCrossroad1.UpdateCrosswalkLightsCrossroad1LEFT(Crossroad1LeftCrosswalkRED1, Crossroad1LeftCrosswalkGREEN1, Crossroad1LeftCrosswalkRED2, Crossroad1LeftCrosswalkGREEN2);

                    #endregion

                    //Crossroad2
                    #region Crossroad2

                    userControlCrossroad1.UpdateTrafficLightsCrossroad2TOP(Crossroad2TopRED, Crossroad2TopYELLOW, Crossroad2TopGREEN);
                    userControlCrossroad1.UpdateTrafficLightsCrossroad2LEFT(Crossroad2LeftRED, Crossroad2LeftYELLOW, Crossroad2LeftGREEN);
                    userControlCrossroad1.UpdateTrafficLightsCrossroad2RIGHT(Crossroad2RightRED, Crossroad2RightYELLOW, Crossroad2RightGREEN);
                    userControlCrossroad1.UpdateTrafficLightsCrossroad2BOTTOM(Crossroad2BottomRED, Crossroad2BottomYELLOW, Crossroad2BottomGREEN);
                    userControlCrossroad1.UpdateCrosswalkLightsCrossroad2LEFT(Crossroad2LeftCrosswalkRED1, Crossroad2LeftCrosswalkGREEN1, Crossroad2LeftCrosswalkRED2, Crossroad2LeftCrosswalkGREEN2);
                    userControlCrossroad1.UpdateCrosswalkLightsCrossroad2RIGHT(Crossroad2RightCrosswalkRED1, Crossroad2RightCrosswalkGREEN1, Crossroad2RightCrosswalkRED2, Crossroad2RightCrosswalkGREEN2);

                    #endregion
                                        
                    //Edit variables => didn't work properly
                    /*
                    //Crossroad1
                    #region Crossroad1

                    //Top
                    userControlCrossroad1.Crossroad1TopRED = Crossroad1TopRED;
                    userControlCrossroad1.Crossroad1TopYELLOW = Crossroad1TopYELLOW;
                    userControlCrossroad1.Crossroad1TopRED = Crossroad1TopRED;
                    //Left
                    userControlCrossroad1.Crossroad1LeftRED = Crossroad1LeftRED;
                    userControlCrossroad1.Crossroad1LeftYELLOW = Crossroad1LeftYELLOW;
                    userControlCrossroad1.Crossroad1LeftGREEN = Crossroad1LeftGREEN;
                    //Right
                    userControlCrossroad1.Crossroad1RightRED = Crossroad1RightRED;
                    userControlCrossroad1.Crossroad1RightYELLOW = Crossroad1RightYELLOW;
                    userControlCrossroad1.Crossroad1RightGREEN = Crossroad1RightGREEN;
                    //Bottom
                    userControlCrossroad1.Crossroad1BottomRED = Crossroad1BottomRED;
                    userControlCrossroad1.Crossroad1BottomYELLOW = Crossroad1BottomYELLOW;
                    userControlCrossroad1.Crossroad1BottomGREEN = Crossroad1BottomGREEN;

                    //Crosswalk
                    //Top
                    userControlCrossroad1.Crossroad1TopCrosswalkRED1 = Crossroad1TopCrosswalkRED1;
                    userControlCrossroad1.Crossroad1TopCrosswalkRED2 = Crossroad1TopCrosswalkRED2;
                    userControlCrossroad1.Crossroad1TopCrosswalkGREEN1 = Crossroad1TopCrosswalkGREEN1;
                    userControlCrossroad1.Crossroad1TopCrosswalkGREEN2 = Crossroad1TopCrosswalkGREEN2;
                    //Left
                    userControlCrossroad1.Crossroad1LeftCrosswalkRED1 = Crossroad1LeftCrosswalkRED1;
                    userControlCrossroad1.Crossroad1LeftCrosswalkRED2 = Crossroad1LeftCrosswalkRED2;
                    userControlCrossroad1.Crossroad1LeftCrosswalkGREEN1 = Crossroad1LeftCrosswalkGREEN1;
                    userControlCrossroad1.Crossroad1LeftCrosswalkGREEN2 = Crossroad1LeftCrosswalkGREEN2;

                    #endregion

                    //Crossroad2
                    #region Crossroad2

                    //Top
                    userControlCrossroad1.Crossroad2TopRED = Crossroad2TopRED;
                    userControlCrossroad1.Crossroad2TopYELLOW = Crossroad2TopYELLOW;
                    userControlCrossroad1.Crossroad2TopGREEN = Crossroad2TopGREEN;
                    //Left
                    userControlCrossroad1.Crossroad2LeftRED = Crossroad2LeftRED;
                    userControlCrossroad1.Crossroad2LeftYELLOW = Crossroad2LeftYELLOW;
                    userControlCrossroad1.Crossroad2LeftGREEN = Crossroad2LeftGREEN;
                    //Right
                    userControlCrossroad1.Crossroad2RightRED = Crossroad2RightRED;
                    userControlCrossroad1.Crossroad2RightYELLOW = Crossroad2RightYELLOW;
                    userControlCrossroad1.Crossroad2RightGREEN = Crossroad2RightGREEN;
                    //Bottom
                    userControlCrossroad1.Crossroad2BottomRED = Crossroad2BottomRED;
                    userControlCrossroad1.Crossroad2BottomYELLOW = Crossroad2BottomYELLOW;
                    userControlCrossroad1.Crossroad2BottomGREEN = Crossroad2BottomGREEN;

                    //Crosswalk
                    //Left
                    userControlCrossroad1.Crossroad2LeftCrosswalkRED1 = Crossroad1LeftCrosswalkRED1;
                    userControlCrossroad1.Crossroad2LeftCrosswalkRED2 = Crossroad2LeftCrosswalkRED2;
                    userControlCrossroad1.Crossroad2LeftCrosswalkGREEN1 = Crossroad1LeftCrosswalkGREEN1;
                    userControlCrossroad1.Crossroad2LeftCrosswalkGREEN2 = Crossroad2LeftCrosswalkGREEN2;
                    //Right
                    userControlCrossroad1.Crossroad2RightCrosswalkRED1 = Crossroad2RightCrosswalkRED1;
                    userControlCrossroad1.Crossroad2RightCrosswalkRED2 = Crossroad2RightCrosswalkRED2;
                    userControlCrossroad1.Crossroad2RightCrosswalkGREEN1 = Crossroad1LeftCrosswalkGREEN1;
                    userControlCrossroad1.Crossroad2RightCrosswalkGREEN2 = Crossroad2RightCrosswalkGREEN2;

                    #endregion

                    */

                    //Testing 
                    /*
                    #region Testing
                    //TOP
                    userControlCrossroad1.UpdateTrafficLightsCrossroad1TOP(Crossroad1TopRED, Crossroad1TopYELLOW, Crossroad1TopGREEN);
                    userControlCrossroad1.UpdateCrosswalkLightsCrossroad1TOP(Crossroad1TopCrosswalkRED1, Crossroad1TopCrosswalkGREEN1, Crossroad1TopCrosswalkRED2, Crossroad1TopCrosswalkGREEN2);
                    //LEFT
                    userControlCrossroad1.UpdateTrafficLightsCrossroad1LEFT(Crossroad1LeftRED, Crossroad1LeftYELLOW, Crossroad1LeftGREEN);
                    userControlCrossroad1.UpdateCrosswalkLightsCrossroad1LEFT(Crossroad1LeftCrosswalkRED1, Crossroad1LeftCrosswalkGREEN1, Crossroad1LeftCrosswalkRED2, Crossroad1LeftCrosswalkGREEN2);
                    //RIGHT
                    userControlCrossroad1.UpdateTrafficLightsCrossroad1RIGHT(Crossroad1RightRED, Crossroad1RightYELLOW, Crossroad1RightGREEN);
                    //BOTTOM
                    userControlCrossroad1.UpdateTrafficLightsCrossroad1BOTTOM(Crossroad1BottomRED, Crossroad1BottomYELLOW, Crossroad1BottomGREEN);


                    //TOP
                    userControlCrossroad1.UpdateTrafficLightsCrossroad2TOP(Crossroad1TopRED, Crossroad1TopYELLOW, Crossroad1TopGREEN);
                    //LEFT
                    userControlCrossroad1.UpdateTrafficLightsCrossroad2LEFT(Crossroad1LeftRED, Crossroad1LeftYELLOW, Crossroad1LeftGREEN);
                    userControlCrossroad1.UpdateCrosswalkLightsCrossroad2LEFT(Crossroad1LeftCrosswalkRED1, Crossroad1LeftCrosswalkGREEN1, Crossroad1LeftCrosswalkRED2, Crossroad1LeftCrosswalkGREEN2);
                    //RIGHT
                    userControlCrossroad1.UpdateTrafficLightsCrossroad2RIGHT(Crossroad1RightRED, Crossroad1RightYELLOW, Crossroad1RightGREEN);
                    userControlCrossroad1.UpdateCrosswalkLightsCrossroad2RIGHT(Crossroad1TopCrosswalkRED1, Crossroad1TopCrosswalkGREEN1, Crossroad1TopCrosswalkRED2, Crossroad1TopCrosswalkGREEN2);
                    //BOTTOM
                    userControlCrossroad1.UpdateTrafficLightsCrossroad2BOTTOM(Crossroad1BottomRED, Crossroad1BottomYELLOW, Crossroad1BottomGREEN);

                    #endregion
                    */
                }
                else if (crossroadExtension2) //rBtnCrossroadExtension2.Checked userControlCrossroad1.DrawCrossroadExtension2 == true
                {
                    //userControlCrossroad1.CrossroadExtension2();
                    
                    //Crossroad1
                    #region Crossroad1 

                    userControlCrossroad1.UpdateTrafficLightsCrossroad1TOP(Crossroad1TopRED, Crossroad1TopYELLOW, Crossroad1TopGREEN);
                    userControlCrossroad1.UpdateTrafficLightsCrossroad1LEFT(Crossroad1LeftRED, Crossroad1LeftYELLOW, Crossroad1LeftGREEN);
                    userControlCrossroad1.UpdateTrafficLightsCrossroad1RIGHT(Crossroad1RightRED, Crossroad1RightYELLOW, Crossroad1RightGREEN);
                    userControlCrossroad1.UpdateTrafficLightsCrossroad1BOTTOM(Crossroad1BottomRED, Crossroad1BottomYELLOW, Crossroad1BottomGREEN);
                    userControlCrossroad1.UpdateCrosswalkLightsCrossroad1TOP(Crossroad1TopCrosswalkRED1, Crossroad1TopCrosswalkGREEN1, Crossroad1TopCrosswalkRED2, Crossroad1TopCrosswalkGREEN2);
                    userControlCrossroad1.UpdateCrosswalkLightsCrossroad1LEFT(Crossroad1LeftCrosswalkRED1, Crossroad1LeftCrosswalkGREEN1, Crossroad1LeftCrosswalkRED2, Crossroad1LeftCrosswalkGREEN2);

                    #endregion

                    //Crossroad2
                    #region Crossroad2

                    userControlCrossroad1.UpdateTrafficLightsCrossroad2TOP(Crossroad2TopRED, Crossroad2TopYELLOW, Crossroad2TopGREEN);
                    userControlCrossroad1.UpdateTrafficLightsCrossroad2LEFT(Crossroad2LeftRED, Crossroad2LeftYELLOW, Crossroad2LeftGREEN);
                    userControlCrossroad1.UpdateTrafficLightsCrossroad2RIGHT(Crossroad2RightRED, Crossroad2RightYELLOW, Crossroad2RightGREEN);
                    userControlCrossroad1.UpdateTrafficLightsCrossroad2BOTTOM(Crossroad2BottomRED, Crossroad2BottomYELLOW, Crossroad2BottomGREEN);
                    userControlCrossroad1.UpdateCrosswalkLightsCrossroad2LEFT(Crossroad2LeftCrosswalkRED1, Crossroad2LeftCrosswalkGREEN1, Crossroad2LeftCrosswalkRED2, Crossroad2LeftCrosswalkGREEN2);
                    userControlCrossroad1.UpdateCrosswalkLightsCrossroad2RIGHT(Crossroad2RightCrosswalkRED1, Crossroad2RightCrosswalkGREEN1, Crossroad2RightCrosswalkRED2, Crossroad2RightCrosswalkGREEN2);

                    #endregion

                    //LeftT
                    #region LeftT

                    userControlCrossroad1.UpdateTrafficLightsCrossroadLeftTTOP(CrossroadLeftTTopRED, CrossroadLeftTTopYELLOW, CrossroadLeftTTopGREEN);
                    userControlCrossroad1.UpdateTrafficLightsCrossroadLeftTLEFT(CrossroadLeftTLeftRED, CrossroadLeftTLeftYELLOW, CrossroadLeftTLeftGREEN);
                    userControlCrossroad1.UpdateTrafficLightsCrossroadLeftTRIGHT(CrossroadLeftTRightRED, CrossroadLeftTRightYELLOW, CrossroadLeftTRightGREEN);
                    userControlCrossroad1.UpdateCrosswalkLightsCrossroadLeftTLEFT(CrossroadLeftTLeftCrosswalkRED1, CrossroadLeftTLeftCrosswalkGREEN1, CrossroadLeftTLeftCrosswalkRED2, CrossroadLeftTLeftCrosswalkGREEN2);

                    #endregion
                                        
                    //Edit variables => didn't work properly
                    /*
                    //Crossroad1
                    #region Crossroad1

                    //Top
                    userControlCrossroad1.Crossroad1TopRED = Crossroad1TopRED;
                    userControlCrossroad1.Crossroad1TopYELLOW = Crossroad1TopYELLOW;
                    userControlCrossroad1.Crossroad1TopRED = Crossroad1TopRED;
                    //Left
                    userControlCrossroad1.Crossroad1LeftRED = Crossroad1LeftRED;
                    userControlCrossroad1.Crossroad1LeftYELLOW = Crossroad1LeftYELLOW;
                    userControlCrossroad1.Crossroad1LeftGREEN = Crossroad1LeftGREEN;
                    //Right
                    userControlCrossroad1.Crossroad1RightRED = Crossroad1RightRED;
                    userControlCrossroad1.Crossroad1RightYELLOW = Crossroad1RightYELLOW;
                    userControlCrossroad1.Crossroad1RightGREEN = Crossroad1RightGREEN;
                    //Bottom
                    userControlCrossroad1.Crossroad1BottomRED = Crossroad1BottomRED;
                    userControlCrossroad1.Crossroad1BottomYELLOW = Crossroad1BottomYELLOW;
                    userControlCrossroad1.Crossroad1BottomGREEN = Crossroad1BottomGREEN;

                    //Crosswalk
                    //Top
                    userControlCrossroad1.Crossroad1TopCrosswalkRED1 = Crossroad1TopCrosswalkRED1;
                    userControlCrossroad1.Crossroad1TopCrosswalkRED2 = Crossroad1TopCrosswalkRED2;
                    userControlCrossroad1.Crossroad1TopCrosswalkGREEN1 = Crossroad1TopCrosswalkGREEN1;
                    userControlCrossroad1.Crossroad1TopCrosswalkGREEN2 = Crossroad1TopCrosswalkGREEN2;
                    //Left
                    userControlCrossroad1.Crossroad1LeftCrosswalkRED1 = Crossroad1LeftCrosswalkRED1;
                    userControlCrossroad1.Crossroad1LeftCrosswalkRED2 = Crossroad1LeftCrosswalkRED2;
                    userControlCrossroad1.Crossroad1LeftCrosswalkGREEN1 = Crossroad1LeftCrosswalkGREEN1;
                    userControlCrossroad1.Crossroad1LeftCrosswalkGREEN2 = Crossroad1LeftCrosswalkGREEN2;

                    #endregion

                    //Crossroad2
                    #region Crossroad2

                    //Top
                    userControlCrossroad1.Crossroad2TopRED = Crossroad2TopRED;
                    userControlCrossroad1.Crossroad2TopYELLOW = Crossroad2TopYELLOW;
                    userControlCrossroad1.Crossroad2TopGREEN = Crossroad2TopGREEN;
                    //Left
                    userControlCrossroad1.Crossroad2LeftRED = Crossroad2LeftRED;
                    userControlCrossroad1.Crossroad2LeftYELLOW = Crossroad2LeftYELLOW;
                    userControlCrossroad1.Crossroad2LeftGREEN = Crossroad2LeftGREEN;
                    //Right
                    userControlCrossroad1.Crossroad2RightRED = Crossroad2RightRED;
                    userControlCrossroad1.Crossroad2RightYELLOW = Crossroad2RightYELLOW;
                    userControlCrossroad1.Crossroad2RightGREEN = Crossroad2RightGREEN;
                    //Bottom
                    userControlCrossroad1.Crossroad2BottomRED = Crossroad2BottomRED;
                    userControlCrossroad1.Crossroad2BottomYELLOW = Crossroad2BottomYELLOW;
                    userControlCrossroad1.Crossroad2BottomGREEN = Crossroad2BottomGREEN;

                    //Crosswalk
                    //Left
                    userControlCrossroad1.Crossroad2LeftCrosswalkRED1 = Crossroad1LeftCrosswalkRED1;
                    userControlCrossroad1.Crossroad2LeftCrosswalkRED2 = Crossroad2LeftCrosswalkRED2;
                    userControlCrossroad1.Crossroad2LeftCrosswalkGREEN1 = Crossroad1LeftCrosswalkGREEN1;
                    userControlCrossroad1.Crossroad2LeftCrosswalkGREEN2 = Crossroad2LeftCrosswalkGREEN2;
                    //Right
                    userControlCrossroad1.Crossroad2RightCrosswalkRED1 = Crossroad2RightCrosswalkRED1;
                    userControlCrossroad1.Crossroad2RightCrosswalkRED2 = Crossroad2RightCrosswalkRED2;
                    userControlCrossroad1.Crossroad2RightCrosswalkGREEN1 = Crossroad1LeftCrosswalkGREEN1;
                    userControlCrossroad1.Crossroad2RightCrosswalkGREEN2 = Crossroad2RightCrosswalkGREEN2;

                    #endregion

                    //LeftT
                    #region LeftT
                    //Top
                    userControlCrossroad1.CrossroadLeftTTopRED = CrossroadLeftTTopRED;
                    userControlCrossroad1.CrossroadLeftTTopYELLOW = CrossroadLeftTTopYELLOW;
                    userControlCrossroad1.CrossroadLeftTTopGREEN = CrossroadLeftTTopGREEN;
                    //Left
                    userControlCrossroad1.CrossroadLeftTLeftRED = CrossroadLeftTLeftRED;
                    userControlCrossroad1.CrossroadLeftTLeftYELLOW = CrossroadLeftTLeftYELLOW;
                    userControlCrossroad1.CrossroadLeftTLeftGREEN = CrossroadLeftTLeftGREEN;
                    //Right
                    userControlCrossroad1.CrossroadLeftTRightRED = CrossroadLeftTRightRED;
                    userControlCrossroad1.CrossroadLeftTRightYELLOW = CrossroadLeftTRightYELLOW;
                    userControlCrossroad1.CrossroadLeftTRightGREEN = CrossroadLeftTRightGREEN;

                    //Crosswalk
                    //Left
                    userControlCrossroad1.CrossroadLeftTLeftCrosswalkRED1 = CrossroadLeftTLeftCrosswalkRED1;
                    userControlCrossroad1.CrossroadLeftTLeftCrosswalkRED2 = CrossroadLeftTLeftCrosswalkRED2;
                    userControlCrossroad1.CrossroadLeftTLeftCrosswalkGREEN1 = CrossroadLeftTLeftCrosswalkGREEN1;
                    userControlCrossroad1.CrossroadLeftTLeftCrosswalkGREEN2 = CrossroadLeftTLeftCrosswalkGREEN2;

                    #endregion

                    */

                    //Testing
                    /*
                    #region Testing
                    //TOP
                    userControlCrossroad1.UpdateTrafficLightsCrossroad1TOP(Crossroad1TopRED, Crossroad1TopYELLOW, Crossroad1TopGREEN);
                    userControlCrossroad1.UpdateCrosswalkLightsCrossroad1TOP(Crossroad1TopCrosswalkRED1, Crossroad1TopCrosswalkGREEN1, Crossroad1TopCrosswalkRED2, Crossroad1TopCrosswalkGREEN2);
                    //LEFT
                    userControlCrossroad1.UpdateTrafficLightsCrossroad1LEFT(Crossroad1LeftRED, Crossroad1LeftYELLOW, Crossroad1LeftGREEN);
                    userControlCrossroad1.UpdateCrosswalkLightsCrossroad1LEFT(Crossroad1LeftCrosswalkRED1, Crossroad1LeftCrosswalkGREEN1, Crossroad1LeftCrosswalkRED2, Crossroad1LeftCrosswalkGREEN2);
                    //RIGHT
                    userControlCrossroad1.UpdateTrafficLightsCrossroad1RIGHT(Crossroad1RightRED, Crossroad1RightYELLOW, Crossroad1RightGREEN);
                    //BOTTOM
                    userControlCrossroad1.UpdateTrafficLightsCrossroad1BOTTOM(Crossroad1BottomRED, Crossroad1BottomYELLOW, Crossroad1BottomGREEN);

                    //TOP
                    userControlCrossroad1.UpdateTrafficLightsCrossroad2TOP(Crossroad1TopRED, Crossroad1TopYELLOW, Crossroad1TopGREEN);
                    //LEFT
                    userControlCrossroad1.UpdateTrafficLightsCrossroad2LEFT(Crossroad1LeftRED, Crossroad1LeftYELLOW, Crossroad1LeftGREEN);
                    userControlCrossroad1.UpdateCrosswalkLightsCrossroad2LEFT(Crossroad1LeftCrosswalkRED1, Crossroad1LeftCrosswalkGREEN1, Crossroad1LeftCrosswalkRED2, Crossroad1LeftCrosswalkGREEN2);
                    //RIGHT
                    userControlCrossroad1.UpdateTrafficLightsCrossroad2RIGHT(Crossroad1RightRED, Crossroad1RightYELLOW, Crossroad1RightGREEN);
                    userControlCrossroad1.UpdateCrosswalkLightsCrossroad2RIGHT(Crossroad1TopCrosswalkRED1, Crossroad1TopCrosswalkGREEN1, Crossroad1TopCrosswalkRED2, Crossroad1TopCrosswalkGREEN2);
                    //BOTTOM
                    userControlCrossroad1.UpdateTrafficLightsCrossroad2BOTTOM(Crossroad1BottomRED, Crossroad1BottomYELLOW, Crossroad1BottomGREEN);

                    //TOP
                    userControlCrossroad1.UpdateTrafficLightsCrossroadLeftTTOP(CrossroadLeftTTopRED, CrossroadLeftTTopYELLOW, CrossroadLeftTTopGREEN);
                    //LEFT
                    userControlCrossroad1.UpdateTrafficLightsCrossroadLeftTLEFT(CrossroadLeftTLeftRED, CrossroadLeftTLeftYELLOW, CrossroadLeftTLeftGREEN);
                    userControlCrossroad1.UpdateCrosswalkLightsCrossroadLeftTLEFT(CrossroadLeftTLeftCrosswalkRED1, CrossroadLeftTLeftCrosswalkGREEN1, CrossroadLeftTLeftCrosswalkRED2, CrossroadLeftTLeftCrosswalkGREEN2);
                    //RIGHT
                    userControlCrossroad1.UpdateTrafficLightsCrossroadLeftTRIGHT(CrossroadLeftTRightRED, CrossroadLeftTRightYELLOW, CrossroadLeftTRightGREEN);

                    #endregion
                    */
                }
                else if (crossroadExtension3) //rBtnCrossroadExtension3.Checked userControlCrossroad1.DrawCrossroadExtension3 == true
                {
                    //userControlCrossroad1.CrossroadExtension3();
                    
                    //Crossroad1
                    #region Crossroad1 

                    userControlCrossroad1.UpdateTrafficLightsCrossroad1TOP(Crossroad1TopRED, Crossroad1TopYELLOW, Crossroad1TopGREEN);
                    userControlCrossroad1.UpdateTrafficLightsCrossroad1LEFT(Crossroad1LeftRED, Crossroad1LeftYELLOW, Crossroad1LeftGREEN);
                    userControlCrossroad1.UpdateTrafficLightsCrossroad1RIGHT(Crossroad1RightRED, Crossroad1RightYELLOW, Crossroad1RightGREEN);
                    userControlCrossroad1.UpdateTrafficLightsCrossroad1BOTTOM(Crossroad1BottomRED, Crossroad1BottomYELLOW, Crossroad1BottomGREEN);
                    userControlCrossroad1.UpdateCrosswalkLightsCrossroad1TOP(Crossroad1TopCrosswalkRED1, Crossroad1TopCrosswalkGREEN1, Crossroad1TopCrosswalkRED2, Crossroad1TopCrosswalkGREEN2);
                    userControlCrossroad1.UpdateCrosswalkLightsCrossroad1LEFT(Crossroad1LeftCrosswalkRED1, Crossroad1LeftCrosswalkGREEN1, Crossroad1LeftCrosswalkRED2, Crossroad1LeftCrosswalkGREEN2);

                    #endregion

                    //Crossroad2
                    #region Crossroad2

                    userControlCrossroad1.UpdateTrafficLightsCrossroad2TOP(Crossroad2TopRED, Crossroad2TopYELLOW, Crossroad2TopGREEN);
                    userControlCrossroad1.UpdateTrafficLightsCrossroad2LEFT(Crossroad2LeftRED, Crossroad2LeftYELLOW, Crossroad2LeftGREEN);
                    userControlCrossroad1.UpdateTrafficLightsCrossroad2RIGHT(Crossroad2RightRED, Crossroad2RightYELLOW, Crossroad2RightGREEN);
                    userControlCrossroad1.UpdateTrafficLightsCrossroad2BOTTOM(Crossroad2BottomRED, Crossroad2BottomYELLOW, Crossroad2BottomGREEN);
                    userControlCrossroad1.UpdateCrosswalkLightsCrossroad2LEFT(Crossroad2LeftCrosswalkRED1, Crossroad2LeftCrosswalkGREEN1, Crossroad2LeftCrosswalkRED2, Crossroad2LeftCrosswalkGREEN2);
                    userControlCrossroad1.UpdateCrosswalkLightsCrossroad2RIGHT(Crossroad2RightCrosswalkRED1, Crossroad2RightCrosswalkGREEN1, Crossroad2RightCrosswalkRED2, Crossroad2RightCrosswalkGREEN2);

                    #endregion

                    //LeftT
                    #region LeftT

                    userControlCrossroad1.UpdateTrafficLightsCrossroadLeftTTOP(CrossroadLeftTTopRED, CrossroadLeftTTopYELLOW, CrossroadLeftTTopGREEN);
                    userControlCrossroad1.UpdateTrafficLightsCrossroadLeftTLEFT(CrossroadLeftTLeftRED, CrossroadLeftTLeftYELLOW, CrossroadLeftTLeftGREEN);
                    userControlCrossroad1.UpdateTrafficLightsCrossroadLeftTRIGHT(CrossroadLeftTRightRED, CrossroadLeftTRightYELLOW, CrossroadLeftTRightGREEN);
                    userControlCrossroad1.UpdateCrosswalkLightsCrossroadLeftTLEFT(CrossroadLeftTLeftCrosswalkRED1, CrossroadLeftTLeftCrosswalkGREEN1, CrossroadLeftTLeftCrosswalkRED2, CrossroadLeftTLeftCrosswalkGREEN2);

                    #endregion

                    //RightT
                    #region RightT

                    userControlCrossroad1.UpdateTrafficLightsCrossroadRightTTOP(CrossroadRightTTopRED, CrossroadRightTTopYELLOW, CrossroadRightTTopGREEN);
                    userControlCrossroad1.UpdateTrafficLightsCrossroadRightTLEFT(CrossroadRightTLeftRED, CrossroadRightTLeftYELLOW, CrossroadRightTLeftGREEN);
                    userControlCrossroad1.UpdateTrafficLightsCrossroadRightTRIGHT(CrossroadRightTRightRED, CrossroadRightTRightYELLOW, CrossroadRightTRightGREEN);
                    userControlCrossroad1.UpdateCrosswalkLightsCrossroadRightTTOP(CrossroadRightTTopCrosswalkRED1, CrossroadRightTTopCrosswalkGREEN1, CrossroadRightTTopCrosswalkRED2, CrossroadRightTTopCrosswalkGREEN2);

                    #endregion
                                        
                    //Edit variables => didn't work properly
                    /*
                    //Crossroad1
                    #region Crossroad1

                    //Top
                    userControlCrossroad1.Crossroad1TopRED = Crossroad1TopRED;
                    userControlCrossroad1.Crossroad1TopYELLOW = Crossroad1TopYELLOW;
                    userControlCrossroad1.Crossroad1TopRED = Crossroad1TopRED;
                    //Left
                    userControlCrossroad1.Crossroad1LeftRED = Crossroad1LeftRED;
                    userControlCrossroad1.Crossroad1LeftYELLOW = Crossroad1LeftYELLOW;
                    userControlCrossroad1.Crossroad1LeftGREEN = Crossroad1LeftGREEN;
                    //Right
                    userControlCrossroad1.Crossroad1RightRED = Crossroad1RightRED;
                    userControlCrossroad1.Crossroad1RightYELLOW = Crossroad1RightYELLOW;
                    userControlCrossroad1.Crossroad1RightGREEN = Crossroad1RightGREEN;
                    //Bottom
                    userControlCrossroad1.Crossroad1BottomRED = Crossroad1BottomRED;
                    userControlCrossroad1.Crossroad1BottomYELLOW = Crossroad1BottomYELLOW;
                    userControlCrossroad1.Crossroad1BottomGREEN = Crossroad1BottomGREEN;

                    //Crosswalk
                    //Top
                    userControlCrossroad1.Crossroad1TopCrosswalkRED1 = Crossroad1TopCrosswalkRED1;
                    userControlCrossroad1.Crossroad1TopCrosswalkRED2 = Crossroad1TopCrosswalkRED2;
                    userControlCrossroad1.Crossroad1TopCrosswalkGREEN1 = Crossroad1TopCrosswalkGREEN1;
                    userControlCrossroad1.Crossroad1TopCrosswalkGREEN2 = Crossroad1TopCrosswalkGREEN2;
                    //Left
                    userControlCrossroad1.Crossroad1LeftCrosswalkRED1 = Crossroad1LeftCrosswalkRED1;
                    userControlCrossroad1.Crossroad1LeftCrosswalkRED2 = Crossroad1LeftCrosswalkRED2;
                    userControlCrossroad1.Crossroad1LeftCrosswalkGREEN1 = Crossroad1LeftCrosswalkGREEN1;
                    userControlCrossroad1.Crossroad1LeftCrosswalkGREEN2 = Crossroad1LeftCrosswalkGREEN2;

                    #endregion

                    //Crossroad2
                    #region Crossroad2

                    //Top
                    userControlCrossroad1.Crossroad2TopRED = Crossroad2TopRED;
                    userControlCrossroad1.Crossroad2TopYELLOW = Crossroad2TopYELLOW;
                    userControlCrossroad1.Crossroad2TopGREEN = Crossroad2TopGREEN;
                    //Left
                    userControlCrossroad1.Crossroad2LeftRED = Crossroad2LeftRED;
                    userControlCrossroad1.Crossroad2LeftYELLOW = Crossroad2LeftYELLOW;
                    userControlCrossroad1.Crossroad2LeftGREEN = Crossroad2LeftGREEN;
                    //Right
                    userControlCrossroad1.Crossroad2RightRED = Crossroad2RightRED;
                    userControlCrossroad1.Crossroad2RightYELLOW = Crossroad2RightYELLOW;
                    userControlCrossroad1.Crossroad2RightGREEN = Crossroad2RightGREEN;
                    //Bottom
                    userControlCrossroad1.Crossroad2BottomRED = Crossroad2BottomRED;
                    userControlCrossroad1.Crossroad2BottomYELLOW = Crossroad2BottomYELLOW;
                    userControlCrossroad1.Crossroad2BottomGREEN = Crossroad2BottomGREEN;

                    //Crosswalk
                    //Left
                    userControlCrossroad1.Crossroad2LeftCrosswalkRED1 = Crossroad1LeftCrosswalkRED1;
                    userControlCrossroad1.Crossroad2LeftCrosswalkRED2 = Crossroad2LeftCrosswalkRED2;
                    userControlCrossroad1.Crossroad2LeftCrosswalkGREEN1 = Crossroad1LeftCrosswalkGREEN1;
                    userControlCrossroad1.Crossroad2LeftCrosswalkGREEN2 = Crossroad2LeftCrosswalkGREEN2;
                    //Right
                    userControlCrossroad1.Crossroad2RightCrosswalkRED1 = Crossroad2RightCrosswalkRED1;
                    userControlCrossroad1.Crossroad2RightCrosswalkRED2 = Crossroad2RightCrosswalkRED2;
                    userControlCrossroad1.Crossroad2RightCrosswalkGREEN1 = Crossroad1LeftCrosswalkGREEN1;
                    userControlCrossroad1.Crossroad2RightCrosswalkGREEN2 = Crossroad2RightCrosswalkGREEN2;

                    #endregion

                    //LeftT
                    #region LeftT
                    //Top
                    userControlCrossroad1.CrossroadLeftTTopRED = CrossroadLeftTTopRED;
                    userControlCrossroad1.CrossroadLeftTTopYELLOW = CrossroadLeftTTopYELLOW;
                    userControlCrossroad1.CrossroadLeftTTopGREEN = CrossroadLeftTTopGREEN;
                    //Left
                    userControlCrossroad1.CrossroadLeftTLeftRED = CrossroadLeftTLeftRED;
                    userControlCrossroad1.CrossroadLeftTLeftYELLOW = CrossroadLeftTLeftYELLOW;
                    userControlCrossroad1.CrossroadLeftTLeftGREEN = CrossroadLeftTLeftGREEN;
                    //Right
                    userControlCrossroad1.CrossroadLeftTRightRED = CrossroadLeftTRightRED;
                    userControlCrossroad1.CrossroadLeftTRightYELLOW = CrossroadLeftTRightYELLOW;
                    userControlCrossroad1.CrossroadLeftTRightGREEN = CrossroadLeftTRightGREEN;

                    //Crosswalk
                    //Left
                    userControlCrossroad1.CrossroadLeftTLeftCrosswalkRED1 = CrossroadLeftTLeftCrosswalkRED1;
                    userControlCrossroad1.CrossroadLeftTLeftCrosswalkRED2 = CrossroadLeftTLeftCrosswalkRED2;
                    userControlCrossroad1.CrossroadLeftTLeftCrosswalkGREEN1 = CrossroadLeftTLeftCrosswalkGREEN1;
                    userControlCrossroad1.CrossroadLeftTLeftCrosswalkGREEN2 = CrossroadLeftTLeftCrosswalkGREEN2;

                    #endregion

                    //RightT
                    #region RightT

                    //Top
                    userControlCrossroad1.CrossroadRightTTopRED = CrossroadRightTTopRED;
                    userControlCrossroad1.CrossroadRightTTopYELLOW = CrossroadRightTTopYELLOW;
                    userControlCrossroad1.CrossroadRightTTopGREEN = CrossroadRightTTopGREEN;
                    //Left
                    userControlCrossroad1.CrossroadRightTLeftRED = CrossroadRightTLeftRED;
                    userControlCrossroad1.CrossroadRightTLeftYELLOW = CrossroadRightTLeftYELLOW;
                    userControlCrossroad1.CrossroadRightTLeftGREEN = CrossroadRightTLeftGREEN;
                    //Right
                    userControlCrossroad1.CrossroadRightTRightRED = CrossroadRightTRightRED;
                    userControlCrossroad1.CrossroadRightTRightYELLOW = CrossroadRightTRightYELLOW;
                    userControlCrossroad1.CrossroadRightTRightGREEN = CrossroadRightTRightGREEN;

                    //Crosswalk
                    //Top
                    userControlCrossroad1.CrossroadRightTTopCrosswalkRED1 = CrossroadRightTTopCrosswalkRED1;
                    userControlCrossroad1.CrossroadRightTTopCrosswalkRED2 = CrossroadRightTTopCrosswalkRED2;
                    userControlCrossroad1.CrossroadRightTTopCrosswalkGREEN1 = CrossroadRightTTopCrosswalkRED2;
                    userControlCrossroad1.CrossroadRightTTopCrosswalkGREEN2 = CrossroadRightTTopCrosswalkRED2;

                    #endregion

                    */

                    //Testing 
                    /*
                    #region Testing 
                    //TOP
                    userControlCrossroad1.UpdateTrafficLightsCrossroad1TOP(Crossroad1TopRED, Crossroad1TopYELLOW, Crossroad1TopGREEN);
                    userControlCrossroad1.UpdateCrosswalkLightsCrossroad1TOP(Crossroad1TopCrosswalkRED1, Crossroad1TopCrosswalkGREEN1, Crossroad1TopCrosswalkRED2, Crossroad1TopCrosswalkGREEN2);
                    //LEFT
                    userControlCrossroad1.UpdateTrafficLightsCrossroad1LEFT(Crossroad1LeftRED, Crossroad1LeftYELLOW, Crossroad1LeftGREEN);
                    userControlCrossroad1.UpdateCrosswalkLightsCrossroad1LEFT(Crossroad1LeftCrosswalkRED1, Crossroad1LeftCrosswalkGREEN1, Crossroad1LeftCrosswalkRED2, Crossroad1LeftCrosswalkGREEN2);
                    //RIGHT
                    userControlCrossroad1.UpdateTrafficLightsCrossroad1RIGHT(Crossroad1RightRED, Crossroad1RightYELLOW, Crossroad1RightGREEN);
                    //BOTTOM
                    userControlCrossroad1.UpdateTrafficLightsCrossroad1BOTTOM(Crossroad1BottomRED, Crossroad1BottomYELLOW, Crossroad1BottomGREEN);

                    //TOP
                    userControlCrossroad1.UpdateTrafficLightsCrossroad2TOP(Crossroad1TopRED, Crossroad1TopYELLOW, Crossroad1TopGREEN);
                    //LEFT
                    userControlCrossroad1.UpdateTrafficLightsCrossroad2LEFT(Crossroad1LeftRED, Crossroad1LeftYELLOW, Crossroad1LeftGREEN);
                    userControlCrossroad1.UpdateCrosswalkLightsCrossroad2LEFT(Crossroad1LeftCrosswalkRED1, Crossroad1LeftCrosswalkGREEN1, Crossroad1LeftCrosswalkRED2, Crossroad1LeftCrosswalkGREEN2);
                    //RIGHT
                    userControlCrossroad1.UpdateTrafficLightsCrossroad2RIGHT(Crossroad1RightRED, Crossroad1RightYELLOW, Crossroad1RightGREEN);
                    userControlCrossroad1.UpdateCrosswalkLightsCrossroad2RIGHT(Crossroad1TopCrosswalkRED1, Crossroad1TopCrosswalkGREEN1, Crossroad1TopCrosswalkRED2, Crossroad1TopCrosswalkGREEN2);
                    //BOTTOM
                    userControlCrossroad1.UpdateTrafficLightsCrossroad2BOTTOM(Crossroad1BottomRED, Crossroad1BottomYELLOW, Crossroad1BottomGREEN);

                    //TOP
                    userControlCrossroad1.UpdateTrafficLightsCrossroadLeftTTOP(CrossroadLeftTTopRED, CrossroadLeftTTopYELLOW, CrossroadLeftTTopGREEN);
                    //LEFT
                    userControlCrossroad1.UpdateTrafficLightsCrossroadLeftTLEFT(CrossroadLeftTLeftRED, CrossroadLeftTLeftYELLOW, CrossroadLeftTLeftGREEN);
                    userControlCrossroad1.UpdateCrosswalkLightsCrossroadLeftTLEFT(CrossroadLeftTLeftCrosswalkRED1, CrossroadLeftTLeftCrosswalkGREEN1, CrossroadLeftTLeftCrosswalkRED2, CrossroadLeftTLeftCrosswalkGREEN2);
                    //RIGHT
                    userControlCrossroad1.UpdateTrafficLightsCrossroadLeftTRIGHT(CrossroadLeftTRightRED, CrossroadLeftTRightYELLOW, CrossroadLeftTRightGREEN);

                    //TOP
                    userControlCrossroad1.UpdateTrafficLightsCrossroadRightTTOP(CrossroadLeftTTopRED, CrossroadLeftTTopYELLOW, CrossroadLeftTTopGREEN);
                    userControlCrossroad1.UpdateCrosswalkLightsCrossroadRightTLEFT(CrossroadRightTTopCrosswalkRED1, CrossroadRightTTopCrosswalkGREEN1, CrossroadRightTTopCrosswalkRED2, CrossroadRightTTopCrosswalkGREEN2);
                    //LEFT
                    userControlCrossroad1.UpdateTrafficLightsCrossroadRightTLEFT(CrossroadLeftTLeftRED, CrossroadLeftTLeftYELLOW, CrossroadLeftTLeftGREEN);
                    //RIGHT
                    userControlCrossroad1.UpdateTrafficLightsCrossroadRightTRIGHT(CrossroadLeftTRightRED, CrossroadLeftTRightYELLOW, CrossroadLeftTRightGREEN);

                    #endregion
                    */
                }
                else
                {
                    statusStripCrossroad.Items.Clear();
                    ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Click some radio button to choose your variand of crossroad.");
                    statusStripCrossroad.Items.Add(lblStatus);
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
                
        //Radiobutton clicked
        #region Radiobutton clicked
        private void rBtnCrossroadBasic_CheckedChanged(object sender, EventArgs e)
        {
            statusStripCrossroad.Items.Clear();
            ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Basic crossroad");
            statusStripCrossroad.Items.Add(lblStatus);

            //userControlCrossroad1.BasicCrossroad();
            
            crossroadBasic = true;
            crossroadExtension1 = false;
            crossroadExtension2 = false;
            crossroadExtension3 = false;
            
            userControlCrossroad1.DrawBasicCrossroad = true;
            userControlCrossroad1.DrawCrossroadExtension1 = false;
            userControlCrossroad1.DrawCrossroadExtension2 = false;
            userControlCrossroad1.DrawCrossroadExtension3 = false;
            
        }

        private void rBtnCrossroadExtension1_CheckedChanged(object sender, EventArgs e)
        {
            statusStripCrossroad.Items.Clear();
            ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Crossroad extension 1");
            statusStripCrossroad.Items.Add(lblStatus);

            //userControlCrossroad1.CrossroadExtension1();

            crossroadBasic = false;
            crossroadExtension1 = true;
            crossroadExtension2 = false;
            crossroadExtension3 = false;

            userControlCrossroad1.DrawBasicCrossroad = false;
            userControlCrossroad1.DrawCrossroadExtension1 = true;
            userControlCrossroad1.DrawCrossroadExtension2 = false;
            userControlCrossroad1.DrawCrossroadExtension3 = false;
            
        }

        private void rBtnCrossroadExtension2_CheckedChanged(object sender, EventArgs e)
        {
            statusStripCrossroad.Items.Clear();
            ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Crossroad extension 2");
            statusStripCrossroad.Items.Add(lblStatus);

            //userControlCrossroad1.CrossroadExtension2();

            crossroadBasic = false;
            crossroadExtension1 = false;
            crossroadExtension2 = true;
            crossroadExtension3 = false;

            
            userControlCrossroad1.DrawBasicCrossroad = false;
            userControlCrossroad1.DrawCrossroadExtension1 = false;
            userControlCrossroad1.DrawCrossroadExtension2 = true;
            userControlCrossroad1.DrawCrossroadExtension3 = false;
            
        }

        private void rBtnCrossroadExtension3_CheckedChanged(object sender, EventArgs e)
        {
            statusStripCrossroad.Items.Clear();
            ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Crossroad extension 3");
            statusStripCrossroad.Items.Add(lblStatus);

            //userControlCrossroad1.CrossroadExtension3();
       
            crossroadBasic = false;
            crossroadExtension1 = false;
            crossroadExtension2 = false;
            crossroadExtension3 = true;

            userControlCrossroad1.DrawBasicCrossroad = false;
            userControlCrossroad1.DrawCrossroadExtension1 = false;
            userControlCrossroad1.DrawCrossroadExtension2 = false;
            userControlCrossroad1.DrawCrossroadExtension3 = true;
            
        }

        #endregion

        //Buttons in UserControlCrossroad clicked event
        /*
        #region Buttons in UserControlCrossroad clicked event
        private void UserControlCrossroad_ButtonClicked(object? sender, string buttonIdentifier)
        {
            if (sender != null)
            {
                switch (buttonIdentifier)
                {
                    case "btnCrossroad1TopCrosswalkLEFT":
                        Crossroad1TopCrosswalkBTN1 = true;
                        S7.SetBitAt(send_buffer_DB1, 0, 0, Crossroad1TopCrosswalkBTN1);

                        //write to PLC
                        int writeResultDB1_Crossroad1TopCrosswalkBTN1 = client.DBWrite(DBNumber_DB1, 0, send_buffer_DB1.Length, send_buffer_DB1);
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
                        S7.SetBitAt(send_buffer_DB1, 0, 1, Crossroad1TopCrosswalkBTN2);

                        //write to PLC
                        int writeResultDB1_Crossroad1TopCrosswalkBTN2 = client.DBWrite(DBNumber_DB1, 0, send_buffer_DB1.Length, send_buffer_DB1);
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
                        S7.SetBitAt(send_buffer_DB1, 0, 2, Crossroad1LeftCrosswalkBTN1);

                        //write to PLC
                        int writeResultDB1_Crossroad1LeftCrosswalkBTN1 = client.DBWrite(DBNumber_DB1, 0, send_buffer_DB1.Length, send_buffer_DB1);
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
                        S7.SetBitAt(send_buffer_DB1, 0, 3, Crossroad1LeftCrosswalkBTN2);

                        //write to PLC
                        int writeResultDB1_Crossroad1LeftCrosswalkBTN2 = client.DBWrite(DBNumber_DB1, 0, send_buffer_DB1.Length, send_buffer_DB1);
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
                        S7.SetBitAt(send_buffer_DB19, 0, 0, Crossroad2LeftCrosswalkBTN1);

                        //write to PLC
                        int writeResultDB19_Crossroad2LeftCrosswalkBTN1 = client.DBWrite(DBNumber_DB19, 0, send_buffer_DB19.Length, send_buffer_DB19);
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
                        S7.SetBitAt(send_buffer_DB19, 0, 1, Crossroad2LeftCrosswalkBTN2);

                        //write to PLC
                        int writeResultDB19_Crossroad2LeftCrosswalkBTN2 = client.DBWrite(DBNumber_DB19, 0, send_buffer_DB19.Length, send_buffer_DB19);
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
                        S7.SetBitAt(send_buffer_DB19, 0, 2, Crossroad2RightCrosswalkBTN1);

                        //write to PLC
                        int writeResultDB19_Crossroad2RightCrosswalkBTN1 = client.DBWrite(DBNumber_DB19, 0, send_buffer_DB19.Length, send_buffer_DB19);
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
                        S7.SetBitAt(send_buffer_DB19, 0, 3, Crossroad2RightCrosswalkBTN2);

                        //write to PLC
                        int writeResultDB19_Crossroad2RightCrosswalkBTN2 = client.DBWrite(DBNumber_DB19, 0, send_buffer_DB19.Length, send_buffer_DB19);
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
                        S7.SetBitAt(send_buffer_DB20, 0, 0, CrossroadLeftTLeftCrosswalkBTN1);

                        //write to PLC
                        int writeResultDB20_CrossroadLeftTLeftCrosswalkBTN1 = client.DBWrite(DBNumber_DB20, 0, send_buffer_DB20.Length, send_buffer_DB20);
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
                        S7.SetBitAt(send_buffer_DB20, 0, 1, Crossroad2RightCrosswalkBTN2);

                        //write to PLC
                        int writeResultDB20_CrossroadLeftTLeftCrosswalkBTN2 = client.DBWrite(DBNumber_DB20, 0, send_buffer_DB20.Length, send_buffer_DB20);
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
                        S7.SetBitAt(send_buffer_DB21, 0, 0, CrossroadRightTTopCrosswalkBTN1);

                        //write to PLC
                        int writeResultDB21_CrossroadRightTTopCrosswalkBTN1 = client.DBWrite(DBNumber_DB21, 0, send_buffer_DB21.Length, send_buffer_DB21);
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
                        S7.SetBitAt(send_buffer_DB21, 0, 0, CrossroadRightTTopCrosswalkBTN2);

                        //write to PLC
                        int writeResultDB21_CrossroadRightTTopCrosswalkBTN2 = client.DBWrite(DBNumber_DB21, 0, send_buffer_DB21.Length, send_buffer_DB21);
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
        }

        #endregion
        */

        //Emergency + system error 
        #region Emergency + system error 
        private void btnEmergency_Click(object sender, EventArgs e)
        {
            CrossroadEmergencySTOP = true;
            S7.SetBitAt(send_buffer_DB14, 0, 3, CrossroadEmergencySTOP);

            //write to PLC
            int writeResultDB14_Emergency = client.DBWrite(DBNumber_DB14, 0, send_buffer_DB14.Length, send_buffer_DB14);
            if (writeResultDB14_Emergency != 0)
            {
                //write error
                if (!errorMessageBoxShown)
                {
                    //MessageBox
                    MessageBox.Show("BE doesn't work properly. Data could´t be written to DB14!!! \n\n" +
                        $"Error message: writeResultDB14_Emergency = {writeResultDB14_Emergency} \n", "Error",
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
            CrossroadErrorSystem = true;
            S7.SetBitAt(send_buffer_DB14, 0, 4, CrossroadErrorSystem);

            //write to PLC
            int writeResultDB14_ErrorSystem = client.DBWrite(DBNumber_DB14, 0, send_buffer_DB14.Length, send_buffer_DB14);
            if (writeResultDB14_ErrorSystem != 0)
            {
                //write error
                if (!errorMessageBoxShown)
                {
                    //MessageBox
                    MessageBox.Show("BE doesn't work properly. Data could´t be written to DB14!!! \n\n" +
                        $"Error message: writeResultDB14_ErrorSystem = {writeResultDB14_ErrorSystem} \n", "Error",
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

        //BTN End 
        #region Close window 
        private void btnEnd_Click(object sender, EventArgs e)
        {
            //Option3 = false
            Option3 = false;
            S7.SetBitAt(send_buffer_DB11, 0, 2, Option3);

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
                
        //BTN mode click
        #region BTN mode click
        private void btnDayMode_Click(object sender, EventArgs e)
        {
            statusStripCrossroad.Items.Clear();
            ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Day mode");
            statusStripCrossroad.Items.Add(lblStatus);

            CrossroadModeDAY = true;
            S7.SetBitAt(send_buffer_DB14, 0, 2, CrossroadModeDAY);

            //write to PLC
            int writeResultDB14_DayMode = client.DBWrite(DBNumber_DB14, 0, send_buffer_DB14.Length, send_buffer_DB14);
            if (writeResultDB14_DayMode != 0)
            {
                //write error
                if (!errorMessageBoxShown)
                {
                    //MessageBox
                    MessageBox.Show("BE doesn't work properly. Data could´t be written to DB14!!! \n\n" +
                        $"Error message: writeResultDB14_DayMode = {writeResultDB14_DayMode} \n", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                    errorMessageBoxShown = true;
                }
            }
            else
            {
                //write was successful

                //btnDayMode.BackColor = Color.LightGray;
                btnDayMode.BackColor = SystemColors.ControlDark;
                btnDayMode.Enabled = false;

                //btnNightMode.BackColor = Color.LightGray;
                btnNightMode.BackColor = SystemColors.Control;
                btnNightMode.Enabled = true;

                //btnOffMode.BackColor = Color.DarkGray;
                btnOffMode.BackColor = SystemColors.Control;
                btnOffMode.Enabled = true;
            }
        }

        private void btnNightMode_Click(object sender, EventArgs e)
        {
            statusStripCrossroad.Items.Clear();
            ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Night mode");
            statusStripCrossroad.Items.Add(lblStatus);

            CrossroadModeNIGHT = true;
            S7.SetBitAt(send_buffer_DB14, 0, 1, CrossroadModeNIGHT);

            //write to PLC
            int writeResultDB14_NightMode = client.DBWrite(DBNumber_DB14, 0, send_buffer_DB14.Length, send_buffer_DB14);
            if (writeResultDB14_NightMode != 0)
            {
                //write error
                if (!errorMessageBoxShown)
                {
                    //MessageBox
                    MessageBox.Show("BE doesn't work properly. Data could´t be written to DB14!!! \n\n" +
                        $"Error message: writeResultDB14_NightMode = {writeResultDB14_NightMode} \n", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                    errorMessageBoxShown = true;
                }
            }
            else
            {
                //write was successful

                //btnDayMode.BackColor = Color.LightGray;
                btnDayMode.BackColor = SystemColors.Control;
                btnDayMode.Enabled = true;

                //btnNightMode.BackColor = Color.LightGray;
                btnNightMode.BackColor = SystemColors.ControlDark;
                btnNightMode.Enabled = false;

                //btnOffMode.BackColor = Color.DarkGray;
                btnOffMode.BackColor = SystemColors.Control;
                btnOffMode.Enabled = true;
            }
        }

        private void btnOffMode_Click(object sender, EventArgs e)
        {
            statusStripCrossroad.Items.Clear();
            ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Off mode");
            statusStripCrossroad.Items.Add(lblStatus);

            CrossroadModeOFF = true;
            S7.SetBitAt(send_buffer_DB14, 0, 0, CrossroadModeOFF);

            //write to PLC
            int writeResultDB14_OffMode = client.DBWrite(DBNumber_DB14, 0, send_buffer_DB14.Length, send_buffer_DB14);
            if (writeResultDB14_OffMode != 0)
            {
                //write error
                if (!errorMessageBoxShown)
                {
                    //MessageBox
                    MessageBox.Show("BE doesn't work properly. Data could´t be written to DB14!!! \n\n" +
                        $"Error message: writeResultDB14_OffMode = {writeResultDB14_OffMode} \n", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                    errorMessageBoxShown = true;
                }
            }
            else
            {
                //write was successful

                //btnDayMode.BackColor = Color.LightGray;
                btnDayMode.BackColor = SystemColors.Control;
                btnDayMode.Enabled = true;

                //btnNightMode.BackColor = Color.LightGray;
                btnNightMode.BackColor = SystemColors.Control;
                btnNightMode.Enabled = true;

                //btnOffMode.BackColor = Color.DarkGray;
                btnOffMode.BackColor = SystemColors.ControlDark;
                btnOffMode.Enabled = false;

            }
        }

        #endregion

        private void btnTest_Click(object sender, EventArgs e)
        {

        }

    }
}
