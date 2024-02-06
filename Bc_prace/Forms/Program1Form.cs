using Sharp7;
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

namespace Bc_prace
{
    public partial class Program1Form : Form
    {
        //Variables
        #region Variables

        //C# variables
        #region C# variables

        private bool errorMessageBoxShown = false;

        bool Option1;

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

        //Tia variables 
        #region Tia variables 

        public S7Client client = new S7Client();

        //ChooseOptionForm
        //we need to read/write 3 bits (3 times bool) -> 1 byte
        private byte[] read_buffer_ChooseOptionForm = new byte[1];
        private byte[] send_buffer_ChooseOptionForm = new byte[1];

        //Form1
        private byte[] read_buffer_Form1 = new byte[4];
        private byte[] send_buffer_Form1 = new byte[4];

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
        string ElevatorTimeDoorSQOPEN;
        string ElevatroTimeDoorSQCLOSE;
        bool ElevatorDoorClOSE;
        bool ElevatorDoorOPEN;
        int ElevatorCabinSpeed;
        bool ElevatorInactivity;
        string ElevatorTimeToGetDown;

        #endregion

        #endregion

        #endregion

        public Program1Form()
        {
            InitializeComponent();

            //Cabin door position
            panelDoorLeftX = panelDoorLeft.Location.X;
            panelDoorLeftY = panelDoorLeft.Location.Y;
            panelDoorRightX = panelDoorRight.Location.X;
            panelDoorRightY = panelDoorRight.Location.Y;
            panelCabinX = panelCabin.Location.X;
            panelCabinY = panelCabin.Location.Y;
            lblElevatorFloorX = lblElevatorFloor.Location.X;
            lblElevatorFloorY = lblElevatorFloor.Location.Y;

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

        //Tia connection
        #region Tia connection
        
        private void Timer_read_from_PLC_Tick(object sender, EventArgs e)
        {
            try
            {
                int readResult = client.DBRead(11, 0, read_buffer_Form1.Length, read_buffer_Form1);
                if (readResult != 0)
                {
                    statusStripElevator.Items.Clear();
                    ToolStripStatusLabel lblStatus1 = new ToolStripStatusLabel("Variables were not read.");
                    statusStripElevator.Items.Add(lblStatus1);

                    if (!errorMessageBoxShown)
                    {
                        //MessageBox
                        MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data from PLC weren't read!!!", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                        errorMessageBoxShown = true;
                    }
                }
                else
                {
                    //input variables
                    #region Input variables
                    /*
                    ElevatorBTNCabin1 = S7.GetBitAt(read_buffer, ,);
                    ElevatorBTNCabin2 = S7.GetBitAt(read_buffer, ,);
                    ElevatorBTNCabin3 = S7.GetBitAt(read_buffer, ,);
                    ElevatorBTNCabin4 = S7.GetBitAt(read_buffer, ,);
                    ElevatorBTNCabin5 = S7.GetBitAt(read_buffer, ,);
                    ElevatorBTNFloor1 = S7.GetBitAt(read_buffer, ,);
                    ElevatorBTNFloor2 = S7.GetBitAt(read_buffer, ,);
                    ElevatorBTNFloor3 = S7.GetBitAt(read_buffer, ,);
                    ElevatorBTNFloor4 = S7.GetBitAt(read_buffer, ,);
                    ElevatorBTNFloor5 = S7.GetBitAt(read_buffer, ,);
                    ElevatorDoorSEQ = S7.GetBitAt(read_buffer, ,);
                    ElevatorBTNOPENCLOSE = S7.GetBitAt(read_buffer, ,);
                    ElevatorEmergencySTOP = S7.GetBitAt(read_buffer, ,);
                    ElevatorErrorSystem = S7.GetBitAt(read_buffer, ,);
                    */
                    #endregion

                    //output variables
                    #region Output variables
                    /*
                    ElevatorMotorON = S7.GetBitAt(read_buffer, ,); ;
                    ElevatorMotorDOWN = S7.GetBitAt(read_buffer, ,); ;
                    ElevatorMotorUP = S7.GetBitAt(read_buffer, ,); ;
                    ElevatroHoming = S7.GetBitAt(read_buffer, ,); ;
                    ElevatorSystemReady = S7.GetBitAt(read_buffer, ,); ;
                    ElevatorActualFloor = S7.GetIntAt(read_buffer, ,);
                    ElevatorMoving = S7.GetBitAt(read_buffer, ,); ;
                    ElevatorSystemWorking = S7.GetBitAt(read_buffer, ,); ;
                    ElevatorGoToFloor = S7.GetIntAt(read_buffer, ,);
                    ElevatorDirection = S7.GetBitAt(read_buffer, ,); ;
                    ElevatorActualFloorLED1 = S7.GetBitAt(read_buffer, ,); ;
                    ElevatorActualFloorLED2 = S7.GetBitAt(read_buffer, ,); ;
                    ElevatorActualFloorLED3 = S7.GetBitAt(read_buffer, ,); ;
                    ElevatorActualFloorLED4 = S7.GetBitAt(read_buffer, ,); ;
                    ElevatorActualFloorLED5 = S7.GetBitAt(read_buffer, ,); ;
                    ElevatorActualFloorCabinLED1 = S7.GetBitAt(read_buffer, ,); ;
                    ElevatorActualFloorCabinLED2 = S7.GetBitAt(read_buffer, ,); ;
                    ElevatorActualFloorCabinLED3 = S7.GetBitAt(read_buffer, ,); ;
                    ElevatorActualFloorCabinLED4 = S7.GetBitAt(read_buffer, ,); ;
                    ElevatorActualFloorCabinLED5 = S7.GetBitAt(read_buffer, ,); ;
                    ElevatorActualFloorSENS1 = S7.GetBitAt(read_buffer, ,); ;
                    ElevatorActualFloorSENS2 = S7.GetBitAt(read_buffer, ,); ;
                    ElevatorActualFloorSENS3 = S7.GetBitAt(read_buffer, ,); ;
                    ElevatorActualFloorSENS4 = S7.GetBitAt(read_buffer, ,); ;
                    ElevatorActualFloorSENS5 = S7.GetBitAt(read_buffer, ,); ;
                    ElevatorTimeDoorSQOPEN = S7.;
                    ElevatroTimeDoorSQCLOSE = S7.;
                    ElevatorDoorClOSE = S7.GetBitAt(read_buffer, ,); ;
                    ElevatorDoorOPEN = S7.GetBitAt(read_buffer, ,); ;
                    ElevatorCabinSpeed = S7.GetIntAt(read_buffer, ,);
                    ElevatorInactivity = S7.GetBitAt(read_buffer, ,); ;
                    ElevatorTimeToGetDown = S7.;
                    */
                    #endregion

                    errorMessageBoxShown = false;
                }
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
            ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Closong door");
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
        }
        #endregion

        //btn End 
        #region Close window 
        private void btnEnd_Click(object sender, EventArgs e)
        {
            //Option1 = false
            Option1 = false;
            S7.SetBitAt(ref send_buffer_ChooseOptionForm, 0, 0, Option1);
            
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        //ElevatorCabin - parametets and position
        #region ElevatorCabin - parametets and position

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
