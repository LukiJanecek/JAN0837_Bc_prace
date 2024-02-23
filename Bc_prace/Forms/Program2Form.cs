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
using System.Security.Cryptography;

namespace Bc_prace
{
    public partial class Program2Form : Form
    {
        private ChooseOptionForm chooseOptionFormInstance;

        S7Client client;

        private bool errorMessageBoxShown = false;

        //DB11 => Maintain_DB -> 1 struct -> 3 variables -> size 0.2
        private int DBNumber_DB11 = 11;
        byte[] read_buffer_DB11;
        byte[] send_buffer_DB11;

        //MaintainDB variables
        bool Option2 = false;

        //DB5 => CarWash_DB -> 2 structs -> 23 variables -> size 3.7
        private int DBNumber_DB5 = 5;
        //first struct -> Input -> 7 variables -> 0.6 size 
        byte[] read_buffer_DB5_Input;
        public byte[] previous_buffer_DB5_Input;
        public byte[] PreviousBufferHash_DB5_Input;
        byte[] send_buffer_DB5_Input;
        //second struct -> Output -> 16 variables -> 3.7 size
        byte[] read_buffer_DB5_Output;
        byte[] send_buffer_DB5_Output;

        //Input variables
        #region Input variables

        bool CarWashEmergencySTOP;
        bool CarWashErrorSystem;
        bool CarWashStartCarWash;
        bool CarWashWaitingForIncomingCar;
        bool CarWashWaitingForOutgoingCar;
        bool CarWashPerfetWash;
        bool CarWashPerfectPolish;

        #endregion

        //Output variables 
        #region Output variables 

        bool CarWashPositionShower;
        bool CarWashPositionCar;
        bool CarWashGreenLight;
        bool CarWashRedLight;
        bool CarWashYellowLight;
        bool CarWashDoor1UP;
        bool CarWashDoor1DOWN;
        bool CarWashDoor2UP;
        bool CarWashDoor2DOWN;
        bool CarWashWater;
        bool CarWashWashingChemicalsFRONT;
        bool CarWashWashingChemicalsSIDES;
        bool CarWashWashingChemicalsBACK;
        bool CarWashWax;
        bool CarWashVarnishProtection;
        bool CarWashDry;
        bool CarWashSoap;
        bool CarWashActiveFoam;
        bool CarWashBrushes;

        #endregion

        public Program2Form(ChooseOptionForm chooseOptionFormInstance)
        {
            InitializeComponent();
            InitializeButton();

            this.chooseOptionFormInstance = chooseOptionFormInstance;

            client = chooseOptionFormInstance.client;

            //buffers
            //DB11 => Maintain_DB
            read_buffer_DB11 = chooseOptionFormInstance.read_buffer_DB11;
            send_buffer_DB11 = chooseOptionFormInstance.send_buffer_DB11;
            //DB5 => CarWash_DB
            read_buffer_DB5_Input = chooseOptionFormInstance.read_buffer_DB5_Input;
            send_buffer_DB5_Input = chooseOptionFormInstance.send_buffer_DB5_Input;
            read_buffer_DB5_Output = chooseOptionFormInstance.read_buffer_DB5_Output;
            send_buffer_DB5_Output = chooseOptionFormInstance.send_buffer_DB5_Output;
                        
            //start timer
            Timer_read_actual.Start();
            //set time interval (ms)
            Timer_read_actual.Interval = 100;


        }

        //Variables
        #region Variables

        //C# variables
        #region C# variables

        //Count
        int countWax = 0;
        int countSoap = 0;
        int countActiveFoam = 0;

        int SignalizationCount = 0;

        #endregion
                
        #endregion

        //Tia connection
        #region Tia connection

        private void Timer_read_actual_Tick(object sender, EventArgs e)
        {
            try
            { 
                Option2 = chooseOptionFormInstance.Option2;

                //Input variables 
                #region Input variables 

                CarWashEmergencySTOP = chooseOptionFormInstance.CarWashEmergencySTOP;
                CarWashErrorSystem = chooseOptionFormInstance.CarWashErrorSystem;
                CarWashStartCarWash = chooseOptionFormInstance.CarWashStartCarWash;
                CarWashWaitingForIncomingCar = chooseOptionFormInstance.CarWashWaitingForIncomingCar;
                CarWashWaitingForOutgoingCar = chooseOptionFormInstance.CarWashWaitingForOutgoingCar;
                CarWashPerfetWash = chooseOptionFormInstance.CarWashPerfetWash;
                CarWashPerfectPolish = chooseOptionFormInstance.CarWashPerfectPolish;

                #endregion

                //Output variables
                #region Output variables 

                CarWashPositionShower = chooseOptionFormInstance.CarWashPositionShower;
                CarWashPositionCar = chooseOptionFormInstance.CarWashPositionCar;
                CarWashGreenLight = chooseOptionFormInstance.CarWashGreenLight;
                CarWashRedLight = chooseOptionFormInstance.CarWashRedLight;
                CarWashYellowLight = chooseOptionFormInstance.CarWashYellowLight;
                CarWashDoor1UP = chooseOptionFormInstance.CarWashDoor1UP;
                CarWashDoor1DOWN = chooseOptionFormInstance.CarWashDoor1DOWN;
                CarWashDoor2UP = chooseOptionFormInstance.CarWashDoor2UP;
                CarWashDoor2DOWN = chooseOptionFormInstance.CarWashDoor2DOWN;
                CarWashWater = chooseOptionFormInstance.CarWashWater;
                CarWashWashingChemicalsFRONT = chooseOptionFormInstance.CarWashWashingChemicalsFRONT;
                CarWashWashingChemicalsSIDES = chooseOptionFormInstance.CarWashWashingChemicalsSIDES;
                CarWashWashingChemicalsBACK = chooseOptionFormInstance.CarWashWashingChemicalsBACK;
                CarWashWax = chooseOptionFormInstance.CarWashWax;
                CarWashVarnishProtection = chooseOptionFormInstance.CarWashVarnishProtection;
                CarWashDry = chooseOptionFormInstance.CarWashDry;
                CarWashSoap = chooseOptionFormInstance.CarWashSoap;
                CarWashActiveFoam = chooseOptionFormInstance.CarWashActiveFoam;
                CarWashBrushes = chooseOptionFormInstance.CarWashBrushes;

                #endregion

                //Reading variables with MultiVar method
                /*
                #region Multi read -> MultiVar

                S7MultiVar reader = new S7MultiVar(client);

                //DB5 => CarWash_DB -> 2 structs -> 23 variables -> size 3.7
                if (previous_buffer_DB5_Input == null)
                {
                    previous_buffer_DB5_Input = new byte[read_buffer_DB5_Input.Length];
                    Array.Copy(read_buffer_DB5_Input, previous_buffer_DB5_Input, read_buffer_DB5_Input.Length);

                    // Inicializace hashe při prvním spuštění
                    PreviousBufferHash_DB5_Input = ComputeHash(read_buffer_DB5_Input);
                }

                //first struct -> Input -> 7 variables -> 0.6 size 
                reader.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, DBNumber_DB5, 0, 0, ref read_buffer_DB5_Input); //read_buffer_DB5_Input.Length
                //second struct -> Output -> 16 variables -> 3.7 size
                reader.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, DBNumber_DB5, 0, 0, ref read_buffer_DB5_Output); //read_buffer_DB5_Output.Length

                int readResultDB5 = reader.Read();

                if (readResultDB5 == 0)
                {
                    byte[] currentHashDB5_Input = ComputeHash(read_buffer_DB5_Input);

                    // Porovnání hashe s předchozím hashem
                    if (!ArraysAreEqual(currentHashDB5_Input, PreviousBufferHash_DB5_Input))
                    {
                        // Aktualizace předchozího bufferu a hashe
                        Array.Copy(read_buffer_DB5_Input, previous_buffer_DB5_Input, read_buffer_DB5_Input.Length);
                        PreviousBufferHash_DB5_Input = currentHashDB5_Input;

                        // Aktualizace proměnných na základě nových dat
                        
                        //Input variables
                        #region Input variables

                        CarWashEmergencySTOP = S7.GetBitAt(read_buffer_DB5_Input, 0, 0);
                        CarWashErrorSystem = S7.GetBitAt(read_buffer_DB5_Input, 0, 1);
                        CarWashStartCarWash = S7.GetBitAt(read_buffer_DB5_Input, 0, 2);
                        CarWashWaitingForIncomingCar = S7.GetBitAt(read_buffer_DB5_Input, 0, 3);
                        CarWashWaitingForOutgoingCar = S7.GetBitAt(read_buffer_DB5_Input, 0, 4);
                        CarWashPerfetWash = S7.GetBitAt(read_buffer_DB5_Input, 0, 5);
                        CarWashPerfectPolish = S7.GetBitAt(read_buffer_DB5_Input, 0, 6);

                        #endregion

                        //Output variables
                        #region Output variables 

                        CarWashPositionShower = S7.GetBitAt(read_buffer_DB5_Output, 2, 0);
                        CarWashPositionCar = S7.GetBitAt(read_buffer_DB5_Output, 2, 1);
                        CarWashGreenLight = S7.GetBitAt(read_buffer_DB5_Output, 2, 2);
                        CarWashRedLight = S7.GetBitAt(read_buffer_DB5_Output, 2, 3);
                        CarWashYellowLight = S7.GetBitAt(read_buffer_DB5_Output, 2, 4);
                        CarWashDoor1UP = S7.GetBitAt(read_buffer_DB5_Output, 2, 5);
                        CarWashDoor1DOWN = S7.GetBitAt(read_buffer_DB5_Output, 2, 6);
                        CarWashDoor2UP = S7.GetBitAt(read_buffer_DB5_Output, 2, 7);
                        CarWashDoor2DOWN = S7.GetBitAt(read_buffer_DB5_Output, 3, 0);
                        CarWashWater = S7.GetBitAt(read_buffer_DB5_Output, 3, 1);
                        CarWashWashingChemicalsFRONT = S7.GetBitAt(read_buffer_DB5_Output, 3, 2);
                        CarWashWashingChemicalsSIDES = S7.GetBitAt(read_buffer_DB5_Output, 3, 3);
                        CarWashWashingChemicalsBACK = S7.GetBitAt(read_buffer_DB5_Output, 3, 4);
                        CarWashWax = S7.GetBitAt(read_buffer_DB5_Output, 3, 5);
                        CarWashVarnishProtection = S7.GetBitAt(read_buffer_DB5_Output, 3, 6);
                        CarWashDry = S7.GetBitAt(read_buffer_DB5_Output, 3, 7);

                        #endregion

                        errorMessageBoxShown = false;
                    }

                    //Input variables
                    #region Input variables

                    CarWashEmergencySTOP = S7.GetBitAt(read_buffer_DB5_Input, 0, 0);
                    CarWashErrorSystem = S7.GetBitAt(read_buffer_DB5_Input, 0, 1);
                    CarWashStartCarWash = S7.GetBitAt(read_buffer_DB5_Input, 0, 2);
                    CarWashWaitingForIncomingCar = S7.GetBitAt(read_buffer_DB5_Input, 0, 3);
                    CarWashWaitingForOutgoingCar = S7.GetBitAt(read_buffer_DB5_Input, 0, 4);
                    CarWashPerfetWash = S7.GetBitAt(read_buffer_DB5_Input, 0, 5);
                    CarWashPerfectPolish = S7.GetBitAt(read_buffer_DB5_Input, 0, 6);

                    #endregion

                    //Output variables
                    #region Output variables 

                    CarWashPositionShower = S7.GetBitAt(read_buffer_DB5_Output, 2, 0);
                    CarWashPositionCar = S7.GetBitAt(read_buffer_DB5_Output, 2, 1);
                    CarWashGreenLight = S7.GetBitAt(read_buffer_DB5_Output, 2, 2);
                    CarWashRedLight = S7.GetBitAt(read_buffer_DB5_Output, 2, 3);
                    CarWashYellowLight = S7.GetBitAt(read_buffer_DB5_Output, 2, 4);
                    CarWashDoor1UP = S7.GetBitAt(read_buffer_DB5_Output, 2, 5);
                    CarWashDoor1DOWN = S7.GetBitAt(read_buffer_DB5_Output, 2, 6);
                    CarWashDoor2UP = S7.GetBitAt(read_buffer_DB5_Output, 2, 7);
                    CarWashDoor2DOWN = S7.GetBitAt(read_buffer_DB5_Output, 3, 0);
                    CarWashWater = S7.GetBitAt(read_buffer_DB5_Output, 3, 1);
                    CarWashWashingChemicalsFRONT = S7.GetBitAt(read_buffer_DB5_Output, 3, 2);
                    CarWashWashingChemicalsSIDES = S7.GetBitAt(read_buffer_DB5_Output, 3, 3);
                    CarWashWashingChemicalsBACK = S7.GetBitAt(read_buffer_DB5_Output, 3, 4);
                    CarWashWax = S7.GetBitAt(read_buffer_DB5_Output, 3, 5);
                    CarWashVarnishProtection = S7.GetBitAt(read_buffer_DB5_Output, 3, 6);
                    CarWashDry = S7.GetBitAt(read_buffer_DB5_Output, 3, 7);

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
                        MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data weren't read from DB5!!! \n\n" +
                            $"Error message {readResultDB5} \n", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }

                #endregion
                */
                
                //Reading variables with DBRead method
                /*
                #region DBRead

                //DB5 => CarWash_DB -> 2 structs -> 23 variables -> size 3.7
                int readResult = client.DBRead(DBNumber_DB5, 0, read_buffer_DB5.Length, read_buffer_DB5);
                if (readResult != 0)
                {
                    //error
                    statusStripCarWash.Items.Clear();
                    ToolStripStatusLabel lblStatus1 = new ToolStripStatusLabel("Variables were not read.");
                    statusStripCarWash.Items.Add(lblStatus1);

                    if (!errorMessageBoxShown)
                    {
                        errorMessageBoxShown = true;

                        //MessageBox
                        MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data weren't read from DB5!!! \n\n" +
                            $"Error message {readResultDB5} \n", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }
                else
                {
                    //input
                    #region Input variables
                    
                    CarWashEmergencySTOP = S7.GetBitAt(read_buffer_DB5_Input, 0, 0);
                    CarWashErrorSystem = S7.GetBitAt(read_buffer_DB5_Input, 0, 1);
                    CarWashStartCarWash = S7.GetBitAt(read_buffer_DB5_Input, 0, 2);
                    CarWashWaitingForIncomingCar = S7.GetBitAt(read_buffer_DB5_Input, 0, 3);
                    CarWashWaitingForOutgoingCar = S7.GetBitAt(read_buffer_DB5_Input, 0, 4);
                    CarWashPerfetWash = S7.GetBitAt(read_buffer_DB5_Input, 0, 5);
                    CarWashPerfectPolish = S7.GetBitAt(read_buffer_DB5_Input, 0, 6);
                    
                    #endregion

                    //output
                    #region Output variables 
                    
                    CarWashPositionShower = S7.GetBitAt(read_buffer_DB5_Output, 2, 0);
                    CarWashPositionCar = S7.GetBitAt(read_buffer_DB5_Output, 2, 1);
                    CarWashGreenLight = S7.GetBitAt(read_buffer_DB5_Output, 2, 2);
                    CarWashRedLight = S7.GetBitAt(read_buffer_DB5_Output, 2, 3);
                    CarWashYellowLight = S7.GetBitAt(read_buffer_DB5_Output, 2, 4);
                    CarWashDoor1UP = S7.GetBitAt(read_buffer_DB5_Output, 2, 5);
                    CarWashDoor1DOWN = S7.GetBitAt(read_buffer_DB5_Output, 2, 6);
                    CarWashDoor2UP = S7.GetBitAt(read_buffer_DB5_Output, 2, 7);
                    CarWashDoor2DOWN = S7.GetBitAt(read_buffer_DB5_Output, 3, 0);
                    CarWashWater = S7.GetBitAt(read_buffer_DB5_Output, 3, 1);
                    CarWashWashingChemicalsFRONT = S7.GetBitAt(read_buffer_DB5_Output, 3, 2);
                    CarWashWashingChemicalsSIDES = S7.GetBitAt(read_buffer_DB5_Output, 3, 3);
                    CarWashWashingChemicalsBACK = S7.GetBitAt(read_buffer_DB5_Output, 3, 4);
                    CarWashWax = S7.GetBitAt(read_buffer_DB5_Output, 3, 5);
                    CarWashVarnishProtection = S7.GetBitAt(read_buffer_DB5_Output, 3, 6);
                    CarWashDry = S7.GetBitAt(read_buffer_DB5_Output, 3, 7);
                    
                    #endregion
                                      
                    errorMessageBoxShown = false;
                }

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

        private bool ArraysAreEqual(byte[] array1, byte[] array2)
        {
            // Porovnání dvou polí bytů
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

        //Start CarWash
        #region Start CarWash
        private void btnStartCarWash_Click(object sender, EventArgs e)
        {
            Program2SelectionForm Selection = new Program2SelectionForm(chooseOptionFormInstance);
            Selection.ShowDialog(this);
        }
        #endregion
                
        private void Program2_Load(object sender, EventArgs e)
        {

        }
                
        //Emergency + system error 
        #region Emergency + system error 
        private void btnEmergency_Click(object sender, EventArgs e)
        {
            statusStripCarWash.Items.Clear();
            ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Emergency mode activated");
            statusStripCarWash.Items.Add(lblStatus);

            CarWashEmergencySTOP = true;
            S7.SetBitAt(send_buffer_DB5_Input, 0, 0, CarWashEmergencySTOP);

            //write to PLC
            int writeResultDB5_Input = client.DBWrite(DBNumber_DB5, 0, send_buffer_DB5_Input.Length, send_buffer_DB5_Input);
            if (writeResultDB5_Input != 0)
            {
                //write error
                if (!errorMessageBoxShown)
                {
                    //MessageBox
                    MessageBox.Show("BE doesn't work properly. Data could´t be written to DB5!!! \n\n" +
                        $"Error message: {writeResultDB5_Input} \n", "Error",
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
            statusStripCarWash.Items.Clear();
            ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Error system");
            statusStripCarWash.Items.Add(lblStatus);

            CarWashErrorSystem = true;
            S7.SetBitAt(send_buffer_DB5_Input, 0, 1, CarWashErrorSystem);

            //write to PLC
            int writeResultDB5_Input = client.DBWrite(DBNumber_DB5, 0, send_buffer_DB5_Input.Length, send_buffer_DB5_Input);
            if (writeResultDB5_Input != 0)
            {
                //write error
                if (!errorMessageBoxShown)
                {
                    //MessageBox
                    MessageBox.Show("BE doesn't work properly. Data could´t be written to DB5!!! \n\n" +
                        $"Error message: {writeResultDB5_Input} \n", "Error",
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
            //Option2 = false
            Option2 = false;
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
                this.Close();
            }

            //stop timer
            //Timer_read_from_PLC.Stop();            
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
