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

namespace Bc_prace
{
    public partial class Program2Form : Form
    {

        public Program2Form()
        {
            InitializeComponent();
            InitializeButton();

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

        bool Option2;

        //Count
        int countWax = 0;
        int countSoap = 0;
        int countActiveFoam = 0;

        int SignalizationCount = 0;

        #endregion

        //Tia variables
        #region Tia variables

        public S7Client client = new S7Client();

        //ChooseOptionForm
        //we need to read/write 3 bits (3 times bool) -> 1 byte
        private byte[] read_buffer_ChooseOptionForm = new byte[1];
        private byte[] send_buffer_ChooseOptionForm = new byte[1];

        //Form2
        private byte[] read_buffer_Form2 = new byte[1];
        private byte[] send_buffer_Form2 = new byte[1];

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

        //Tia connection
        #region Tia connection

        private void Timer_read_from_PLC_Tick(object sender, EventArgs e)
        {
            try
            {
                //DB5 => CarWash_DB
                int readResult = client.DBRead(5, 0, read_buffer_Form2.Length, read_buffer_Form2);
                if (readResult != 0)
                {
                    statusStripCarWash.Items.Clear();
                    ToolStripStatusLabel lblStatus1 = new ToolStripStatusLabel("Variables were not read.");
                    statusStripCarWash.Items.Add(lblStatus1);

                    if (!errorMessageBoxShown)
                    {
                        errorMessageBoxShown = true;

                        //MessageBox
                        MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data from PLC weren't read from DB5!!!", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }
                else
                {
                    //input
                    #region Input variables
                    /*
                    CarWashEmergencySTOP = S7.GetBitAt(read_buffer, ,);
                    CarWashErrorSystem = S7.GetBitAt(read_buffer, ,);
                    CarWashStartCarWash = S7.GetBitAt(read_buffer, ,);
                    CarWashWaitingForIncomingCar = S7.GetBitAt(read_buffer, ,);
                    CarWashWaitingForOutgoingCar = S7.GetBitAt(read_buffer, ,);
                    CarWashPerfetWash = S7.GetBitAt(read_buffer, ,);
                    CarWashPerfectPolish = S7.GetBitAt(read_buffer, ,);
                    */
                    #endregion

                    //output
                    #region Output variables 
                    /*
                    CarWashPositionShower = S7.GetBitAt(read_buffer, ,);
                    CarWashPositionCar = S7.GetBitAt(read_buffer, ,);
                    CarWashGreenLight = S7.GetBitAt(read_buffer, ,);
                    CarWashRedLight = S7.GetBitAt(read_buffer, ,);
                    CarWashYellowLight = S7.GetBitAt(read_buffer, ,);
                    CarWashDoor1UP = S7.GetBitAt(read_buffer, ,);
                    CarWashDoor1DOWN = S7.GetBitAt(read_buffer, ,);
                    CarWashDoor2UP = S7.GetBitAt(read_buffer, ,);
                    CarWashDoor2DOWN = S7.GetBitAt(read_buffer, ,);
                    CarWashWater = S7.GetBitAt(read_buffer, ,);
                    CarWashWashingChemicalsFRONT = S7.GetBitAt(read_buffer, ,);
                    CarWashWashingChemicalsSIDES = S7.GetBitAt(read_buffer, ,);
                    CarWashWashingChemicalsBACK = S7.GetBitAt(read_buffer, ,);
                    CarWashWax = S7.GetBitAt(read_buffer, ,);
                    CarWashVarnishProtection = S7.GetBitAt(read_buffer, ,);
                    CarWashDry = S7.GetBitAt(read_buffer, ,);
                    */
                    #endregion
                                      
                    errorMessageBoxShown = false;
                }

                //action on variable 
                #region Action on variable

                    //toto asi nebude fungvat 
                    if (CarWashGreenLight) //Green light
                    {
                        statusStripCarWash.Items.Clear();
                        ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Car signalization: GO!");
                        statusStripCarWash.Items.Add(lblStatus);

                        btnSignalization.BackColor = System.Drawing.Color.Green;
                        btnSignalization.Text = "Go";
                    }
                    else if (CarWashYellowLight) //Yellow light
                    {
                        statusStripCarWash.Items.Clear();
                        ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Car signalization: Error!");
                        statusStripCarWash.Items.Add(lblStatus);

                        btnSignalization.BackColor = System.Drawing.Color.Yellow;
                        btnSignalization.Text = "Error";
                    }
                    else if (CarWashRedLight) //Red light
                    {
                        statusStripCarWash.Items.Clear();
                        ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Car signalization: STOP!");
                        statusStripCarWash.Items.Add(lblStatus);

                        btnSignalization.BackColor = System.Drawing.Color.Red;
                        btnSignalization.Text = "Stop";
                    }
                    else
                    {
                        statusStripCarWash.Items.Clear();
                        ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Error! Car should move.");
                        statusStripCarWash.Items.Add(lblStatus);
                    }

                    //Door1 UP
                    if (CarWashDoor1UP)
                    {
                        userControlCarWash1.door1UP();
                    }

                    //Door1 DOWN
                    if (CarWashDoor1DOWN)
                    {
                        userControlCarWash1.door1DOWN();
                    }

                    //Door2 UP
                    if (CarWashDoor2UP)
                    {
                        userControlCarWash1.door2UP();
                    }

                    //door2 DOWN
                    if (CarWashDoor2DOWN)
                    {
                        userControlCarWash1.door2DOWN();
                    }

                    //Water
                    if (CarWashWater)
                    {
                        userControlCarWash1.WaterSignalizationON();
                    }
                    else
                    {
                        userControlCarWash1.WaterSignalizationOFF();

                    }

                    //Wax
                    if (CarWashWax)
                    {
                        userControlCarWash1.WaxSignalizationON();
                    }
                    else
                    {
                        userControlCarWash1.WaxSignalizationOFF();

                    }

                    //ActiveFoam
                    if (CarWashActiveFoam)
                    {
                        userControlCarWash1.DryingSignalizationON();
                    }
                    else
                    {
                        userControlCarWash1.DryingSignalizationOFF();

                    }

                    //Soap 
                    if (CarWashSoap)
                    {
                        userControlCarWash1.SoapSignalizationON();
                    }
                    else
                    {
                        userControlCarWash1.SoapSignalizationOFF();

                    }

                    //Brushes
                    if (CarWashBrushes)
                    {
                        userControlCarWash1.BrushesSignalizationON();
                    }
                    else
                    {
                        userControlCarWash1.BrushesSignalizationOFF();

                    }

                    //Drying
                    if (CarWashDry)
                    {
                        userControlCarWash1.DryingSignalizationON();
                    }
                    else
                    {
                        userControlCarWash1.DryingSignalizationOFF();

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

        //Start CarWash
        #region Start CarWash
        private void btnStartCarWash_Click(object sender, EventArgs e)
        {
            Program2SelectionForm Selection = new Program2SelectionForm();
            Selection.ShowDialog(this);
        }
        #endregion
                
        private void Program2_Load(object sender, EventArgs e)
        {

        }

        //Reaction on Tia variable change
        #region Reaction on Tia variable change




        #endregion

        //Emergency + system error 
        #region Emergency + system error 
        private void btnEmergency_Click(object sender, EventArgs e)
        {
            statusStripCarWash.Items.Clear();
            ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Emergency mode activated");
            statusStripCarWash.Items.Add(lblStatus);
        }
        #endregion

        //btn End 
        #region Close window 
        private void btnEnd_Click(object sender, EventArgs e)
        {
            //Option2 = false
            Option2 = false;
            S7.SetBitAt(ref send_buffer_ChooseOptionForm, 0, 1, Option2);

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

        private void btnTest_Click(object sender, EventArgs e)
        {
            userControlCarWash1.WaxSignalizationON();
            userControlCarWash1.WaterSignalizationON();
            userControlCarWash1.DryingSignalizationON();
            userControlCarWash1.BrushesSignalizationON();
            userControlCarWash1.SoapSignalizationON();
            userControlCarWash1.ActiveFoamSignalizationON();

            userControlCarWash1.door1UP();
            userControlCarWash1.door2UP();

        }

        private void btnTest2_Click(object sender, EventArgs e)
        {
            userControlCarWash1.WaxSignalizationOFF();
            userControlCarWash1.WaterSignalizationOFF();
            userControlCarWash1.DryingSignalizationOFF();
            userControlCarWash1.BrushesSignalizationOFF();
            userControlCarWash1.SoapSignalizationOFF();
            userControlCarWash1.ActiveFoamSignalizationOFF();

            userControlCarWash1.door1DOWN();
            userControlCarWash1.door2DOWN();

            userControlCarWash1.MovePictureRight();


        }
    }
}
