using Sharp7;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bc_prace.Controls
{
    public delegate void ElevatorFloorBTNClick(object sender, string id);
    public delegate void ElevatorFloorSENS(int floor); //object sender

    public partial class UserControlElevatorCabin : UserControl
    {
        //private Program1Form program1FormInstance = null;

        //private S7Client client;

        //MessageBox control
        private bool errorMessageBoxShown;

        public event ElevatorFloorBTNClick OnElevatorFloorBTNClick;
        public event ElevatorFloorSENS OnElevatorFloorSENS;

        //BTNs define 
        #region BTNs define 

        private Button btnElevatorFloor1 = new Button();
        private Button btnElevatorFloor2 = new Button();
        private Button btnElevatorFloor3 = new Button();
        private Button btnElevatorFloor4 = new Button();
        private Button btnElevatorFloor5 = new Button();

        #endregion

        //Drawing variables
        #region Drawing variables

        private float x = 0; //x coordinate
        private float y = 0; //y coordinate
        private float xCabin = 50; //x cabin coordinate
        private float yCabin = 20; //y cabin coordinate
        private float widthCabin = 50; //cabin width 
        private float heightCabin = 80; //cabin length 
        private float widthButton = 50; //button width
        private float heightButton = 28; //button height
        public float Step = 10;
        private float length = 100;
        private float signalizationCircle_diameter = 10;

        private SolidBrush white = new SolidBrush(Color.White); //default
        private SolidBrush green = new SolidBrush(Color.Green); //ActualFloorLED color

        #endregion

        //Elevator Actual Floor LED signalization
        #region Elevator Actual Floor LED signalization

        //ElevatorActualFloorLED1
        private bool elevatorActualFloorLED1;

        public bool ElevatorActualFloorLED1
        {
            get { return elevatorActualFloorLED1; }

            set
            {
                elevatorActualFloorLED1 = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //ElevatorActualFloorLED2
        private bool elevatorActualFloorLED2;

        public bool ElevatorActualFloorLED2
        {
            get { return elevatorActualFloorLED2; }

            set
            {
                elevatorActualFloorLED2 = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //ElevatorActualFloorLED3
        private bool elevatorActualFloorLED3;

        public bool ElevatorActualFloorLED3
        {
            get { return elevatorActualFloorLED3; }

            set
            {
                elevatorActualFloorLED3 = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //ElevatorActualFloorLED4
        private bool elevatorActualFloorLED4;

        public bool ElevatorActualFloorLED4
        {
            get { return elevatorActualFloorLED4; }

            set
            {
                elevatorActualFloorLED4 = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //ElevatorActualFloorLED5
        private bool elevatorActualFloorLED5;

        public bool ElevatorActualFloorLED5
        {
            get { return elevatorActualFloorLED5; }

            set
            {
                elevatorActualFloorLED5 = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        #endregion

        public UserControlElevatorCabin() // Program1Form program1FormInstance
        {
            InitializeComponent();
            DoubleBuffered = true;
            Paint += UserControlElevatorCabin_Paint;

            // if (DesignMode == true ) { }
        }

        public void SetControl(Program1Form program1FormInstance)
        {
            InitializeButtons();

            //this.program1FormInstance = program1FormInstance;

            //client = program1FormInstance.client;
        }


        private void UserControlElevatorCabin_Paint(object sender, PaintEventArgs e)
        {
            /*
            if (program1FormInstance == null)
                return;
            */
            var g = e.Graphics;

            //backgroud
            g.Clear(Color.White);

            Draw(g);

            //pen color
            Pen BlackPen = new Pen(Color.Black);

            //text parameters
            Font labelFont = new Font("Arial", 9);
            SolidBrush labelBrush = new SolidBrush(Color.Black);

            //ElevatorCabin -> starts on the top floor
            g.DrawRectangle(BlackPen, x + xCabin, y + yCabin, widthCabin, heightCabin);

            //Floors and labels
            #region Floors and labels

            //1st floor
            g.DrawLine(BlackPen, x, y + length * 5, x + length * 3, y + length * 5);
            string lblFirstFloor = "1st floor";
            g.DrawString(lblFirstFloor, labelFont, labelBrush, x, y + length * 5 - length / 2);
            //ActualFloorLEDSig
            g.DrawEllipse(BlackPen, x + 5, y + length * 5 - length * 3 / 4, signalizationCircle_diameter, signalizationCircle_diameter);

            //2nd floor
            g.DrawLine(BlackPen, x, y + length * 4, x + length * 3, y + length * 4);
            string lblSecondFloor = "2nd floor";
            g.DrawString(lblSecondFloor, labelFont, labelBrush, x, y + length * 4 - length / 2);
            //ActualFloorLEDSig
            g.DrawEllipse(BlackPen, x + 5, y + length * 4 - length * 3 / 4, signalizationCircle_diameter, signalizationCircle_diameter);

            //3rd floor
            g.DrawLine(BlackPen, x, y + length * 3, x + length * 3, y + length * 3);
            string lblThirdFloor = "3rd floor";
            g.DrawString(lblThirdFloor, labelFont, labelBrush, x, y + length * 3 - length / 2);
            //ActualFloorLEDSig
            g.DrawEllipse(BlackPen, x + 5, y + length * 3 - length * 3 / 4, signalizationCircle_diameter, signalizationCircle_diameter);

            //4th floor
            g.DrawLine(BlackPen, x, y + length * 2, x + length * 3, y + length * 2);
            string lblFourthFloor = "4th floor";
            g.DrawString(lblFourthFloor, labelFont, labelBrush, x, y + length * 2 - length / 2);
            //ActualFloorLEDSig
            g.DrawEllipse(BlackPen, x + 5, y + length * 2 - length * 3 / 4, signalizationCircle_diameter, signalizationCircle_diameter);

            //5th floor
            g.DrawLine(BlackPen, x, y + length, x + length * 3, y + length);
            string lblFifthFloor = "5th floor";
            g.DrawString(lblFifthFloor, labelFont, labelBrush, x, y + length - length / 2);
            //ActualFloorLEDSig
            g.DrawEllipse(BlackPen, x + 5, y + length - length * 3 / 4, signalizationCircle_diameter, signalizationCircle_diameter);

            #endregion

            //Conditions based ond object position -> ElevatorActualFloorSENS
            #region Conditions based ond object position -> ElevatorActualFloorSENS

            //Cabin is on 1st floor
            if (yCabin == (y + length * 5 - heightCabin))
            {
                if (OnElevatorFloorSENS != null)
                    OnElevatorFloorSENS(1);   
            }
            
            //Cabin is on 2st floor
            if (yCabin == (y + length * 4 - heightCabin))
            {
                if (OnElevatorFloorSENS != null)
                    OnElevatorFloorSENS(2);
            } 
            
            //Cabin is on 3rd floor
            if (yCabin == (y + length * 3 - heightCabin))
            {
                if (OnElevatorFloorSENS != null)
                    OnElevatorFloorSENS(3);
            }
            
            //Cabin is on 4th floor
            if (yCabin == (y + length * 2 - heightCabin))
            {
                if (OnElevatorFloorSENS != null)
                    OnElevatorFloorSENS(4);  
            }
            
            //Cabin is on 5th floor
            if (yCabin == (y + length - heightCabin))
            {
                if (OnElevatorFloorSENS != null)
                    OnElevatorFloorSENS(5);
            }
            
            #endregion

            //ActualFloorLED signalization based on value
            #region ActualFloorLED signalization based on value

            //ElevatorActualFloorLED1
            if (ElevatorActualFloorLED1)
            {
                g.FillEllipse(green, x + 5 - 15, y + length * 5 - length * 3 / 4, signalizationCircle_diameter, signalizationCircle_diameter);
                btnElevatorFloor1.FlatAppearance.BorderColor = Color.Blue;
            }
            else
            {
                g.FillEllipse(white, x + 5 - 15, y + length * 5 - length * 3 / 4, signalizationCircle_diameter, signalizationCircle_diameter);
                btnElevatorFloor1.FlatAppearance.BorderColor = Color.Gray;
            }

            //ElevatorActualFloorLED2
            if (ElevatorActualFloorLED2)
            {
                g.FillEllipse(green, x + 5, y + length * 4 - length * 3 / 4, signalizationCircle_diameter, signalizationCircle_diameter);
                btnElevatorFloor2.FlatAppearance.BorderColor = Color.Blue;
            }
            else
            {
                g.FillEllipse(white, x + 5, y + length * 4 - length * 3 / 4, signalizationCircle_diameter, signalizationCircle_diameter);
                btnElevatorFloor2.FlatAppearance.BorderColor = Color.Gray;
            }

            //ElevatorActualFloorLED3
            if (ElevatorActualFloorLED3)
            {
                g.FillEllipse(green, x + 5, y + length * 3 - length * 3 / 4, signalizationCircle_diameter, signalizationCircle_diameter);
                btnElevatorFloor3.FlatAppearance.BorderColor = Color.Blue;
            }
            else
            {
                g.FillEllipse(white, x + 5, y + length * 3 - length * 3 / 4, signalizationCircle_diameter, signalizationCircle_diameter);
                btnElevatorFloor3.FlatAppearance.BorderColor = Color.Gray;
            }

            //ElevatorActualFloorLED4
            if (ElevatorActualFloorLED4)
            {
                g.FillEllipse(green, x + 5, y + length * 2 - length * 3 / 4, signalizationCircle_diameter, signalizationCircle_diameter);
                btnElevatorFloor4.FlatAppearance.BorderColor = Color.Blue;
            }
            else
            {
                g.FillEllipse(white, x + 5, y + length * 2 - length * 3 / 4, signalizationCircle_diameter, signalizationCircle_diameter);
                btnElevatorFloor4.FlatAppearance.BorderColor = Color.Gray; 
            }

            //ElevatorActualFloorLED5
            if (ElevatorActualFloorLED5)
            {
                g.FillEllipse(green, x + 5, y + length - length * 3 / 4, signalizationCircle_diameter, signalizationCircle_diameter);
                btnElevatorFloor2.FlatAppearance.BorderColor = Color.Blue;
            }
            else
            {
                g.FillEllipse(white, x + 5, y + length - length * 3 / 4, signalizationCircle_diameter, signalizationCircle_diameter);
                btnElevatorFloor2.FlatAppearance.BorderColor = Color.Gray;
            }

            #endregion
        }

        private void Draw(Graphics g)
        {
            try
            {

            }
            catch
            {

            }
        }

        //InitializeButtons
        #region InitializeButtons

        private void InitializeButtons()
        {
            //btnElevatorFloor1
            btnElevatorFloor1.Text = "1";
            btnElevatorFloor1.BackColor = Color.White;
            btnElevatorFloor1.Visible = true;
            btnElevatorFloor1.Enabled = true;
            btnElevatorFloor1.Location = new System.Drawing.Point(Convert.ToInt32(length + length / 2), Convert.ToInt32(length / 2 + length * 4)); //cannot invert float to int 
            btnElevatorFloor1.Size = new Size(Convert.ToInt32(widthButton), Convert.ToInt32(heightButton)); //cannot invert float to int 
            btnElevatorFloor1.Click += btnElevatorFloor1_Click;
            Controls.Add(btnElevatorFloor1);
            //btnElevatorFloor2
            btnElevatorFloor2.Text = "2";
            btnElevatorFloor2.BackColor = Color.White;
            btnElevatorFloor2.Visible = true;
            btnElevatorFloor2.Enabled = true;
            btnElevatorFloor2.Location = new System.Drawing.Point(Convert.ToInt32(length + length / 2), Convert.ToInt32(length / 2 + length * 3)); //cannot invert float to int 
            btnElevatorFloor2.Size = new Size(Convert.ToInt32(widthButton), Convert.ToInt32(heightButton)); //cannot invert float to int 
            btnElevatorFloor2.Click += btnElevatorFloor2_Click;
            Controls.Add(btnElevatorFloor2);
            //btnElevatorFloor3
            btnElevatorFloor3.Text = "3";
            btnElevatorFloor3.BackColor = Color.White;
            btnElevatorFloor3.Visible = true;
            btnElevatorFloor3.Enabled = true;
            btnElevatorFloor3.Location = new System.Drawing.Point(Convert.ToInt32(length + length / 2), Convert.ToInt32(length / 2 + length * 2)); //cannot invert float to int 
            btnElevatorFloor3.Size = new Size(Convert.ToInt32(widthButton), Convert.ToInt32(heightButton)); //cannot invert float to int 
            btnElevatorFloor3.Click += btnElevatorFloor3_Click;
            Controls.Add(btnElevatorFloor3);
            //btnElevatorFloor4
            btnElevatorFloor4.Text = "4";
            btnElevatorFloor4.BackColor = Color.White;
            btnElevatorFloor4.Visible = true;
            btnElevatorFloor4.Enabled = true;

            btnElevatorFloor4.Location = new System.Drawing.Point(Convert.ToInt32(length + length / 2), Convert.ToInt32(length / 2 + length)); //cannot invert float to int 
            btnElevatorFloor4.Size = new Size(Convert.ToInt32(widthButton), Convert.ToInt32(heightButton)); //cannot invert float to int 
            btnElevatorFloor4.Click += btnElevatorFloor4_Click;
            Controls.Add(btnElevatorFloor4);
            //btnElevatorFloor¨5
            btnElevatorFloor5.Text = "5";
            btnElevatorFloor5.BackColor = Color.White;
            btnElevatorFloor5.Visible = true;
            btnElevatorFloor5.Enabled = true;
            btnElevatorFloor5.Location = new System.Drawing.Point(Convert.ToInt32(length + length / 2), Convert.ToInt32(length / 2)); //cannot invert float to int 
            btnElevatorFloor5.Size = new Size(Convert.ToInt32(widthButton), Convert.ToInt32(heightButton)); //cannot invert float to int 
            btnElevatorFloor5.Click += btnElevatorFloor5_Click;
            Controls.Add(btnElevatorFloor5);
        }

        #endregion

        //Methods for Cabin and Floor BTN action
        #region Methods for Floor BTN action

        private void btnElevatorFloor1_Click(object sender, EventArgs e)
        {
            if (OnElevatorFloorBTNClick != null)
                OnElevatorFloorBTNClick(sender, ((Button)sender).Text);
        }

        private void btnElevatorFloor2_Click(object sender, EventArgs e)
        {
            if (OnElevatorFloorBTNClick != null)
                OnElevatorFloorBTNClick(sender, ((Button)sender).Text);
        }

        private void btnElevatorFloor3_Click(object sender, EventArgs e)
        {
            if (OnElevatorFloorBTNClick != null)
                OnElevatorFloorBTNClick(sender, ((Button)sender).Text);
        }

        private void btnElevatorFloor4_Click(object sender, EventArgs e)
        {
            if (OnElevatorFloorBTNClick != null)
                OnElevatorFloorBTNClick(sender, ((Button)sender).Text);
        }

        private void btnElevatorFloor5_Click(object sender, EventArgs e)
        {
            if (OnElevatorFloorBTNClick != null)
                OnElevatorFloorBTNClick(sender, ((Button)sender).Text);
        }

        #endregion

        //Cabin movement + parameters
        #region Cabin movement + parameters

        public async void CabinMoveOnSystemInitialising(float targetY)
        {
            targetY = targetY * length - heightCabin;

            while (yCabin < targetY)
            {
                yCabin += Step;
                this.Refresh();

                //zde mohu přidat podmínky pro zobrazení SENS

                //Conditions based ond object position -> ElevatorActualFloorSENS
                #region Conditions based ond object position -> ElevatorActualFloorSENS

                if (yCabin == (y + length * 5 - heightCabin)) //Cabin is on 1st floor
                {
                    if (OnElevatorFloorSENS != null)
                        OnElevatorFloorSENS(1);
                }
                else if (yCabin == (y + length * 4 - heightCabin)) //Cabin is on 2st floor
                {
                    if (OnElevatorFloorSENS != null)
                        OnElevatorFloorSENS(2);
                }
                else if (yCabin == (y + length * 3 - heightCabin)) //Cabin is on 3rd floor
                {
                    if (OnElevatorFloorSENS != null)
                        OnElevatorFloorSENS(3);
                }
                else if (yCabin == (y + length * 2 - heightCabin)) //Cabin is on 4th floor
                {
                    if (OnElevatorFloorSENS != null)
                        OnElevatorFloorSENS(4);
                }
                else if (yCabin == (y + length - heightCabin)) //Cabin is on 5th floor
                {
                    if (OnElevatorFloorSENS != null)
                        OnElevatorFloorSENS(5);
                }
                else //no SENS active
                {
                    if (OnElevatorFloorSENS != null)
                        OnElevatorFloorSENS(6);
                }

                #endregion
                                
                await Task.Delay(50); //program1FormInstance.ElevatorCabinSpeed * 10
            }
        }

        public async void CabinMoveToFloorUP(float targetY)
        {
            targetY = targetY * length;
            
            while (yCabin > targetY)
            {
                yCabin -= Step;

                //Conditions based ond object position -> ElevatorActualFloorSENS
                #region Conditions based ond object position -> ElevatorActualFloorSENS

                //Cabin is on 1st floor
                if (yCabin == (y + length * 5 - heightCabin))
                {
                    if (OnElevatorFloorSENS != null)
                        OnElevatorFloorSENS(1);
                }

                //Cabin is on 2st floor
                if (yCabin == (y + length * 4 - heightCabin))
                {
                    if (OnElevatorFloorSENS != null)
                        OnElevatorFloorSENS(2);
                }

                //Cabin is on 3rd floor
                if (yCabin == (y + length * 3 - heightCabin))
                {
                    if (OnElevatorFloorSENS != null)
                        OnElevatorFloorSENS(3);
                }

                //Cabin is on 4th floor
                if (yCabin == (y + length * 2 - heightCabin))
                {
                    if (OnElevatorFloorSENS != null)
                        OnElevatorFloorSENS(4);
                }

                //Cabin is on 5th floor
                if (yCabin == (y + length - heightCabin))
                {
                    if (OnElevatorFloorSENS != null)
                        OnElevatorFloorSENS(5);
                }

                #endregion

                this.Refresh();
                await Task.Delay(50); //program1FormInstance.ElevatorCabinSpeed * 10
            }
        }

        public async void CabinMoveToFloorDOWN(float targetY)
        {
            targetY = targetY * length;

            while (yCabin < targetY)
            {
                yCabin += Step;

                //Conditions based ond object position -> ElevatorActualFloorSENS
                #region Conditions based ond object position -> ElevatorActualFloorSENS

                //Cabin is on 1st floor
                if (yCabin == (y + length * 5 - heightCabin))
                {
                    if (OnElevatorFloorSENS != null)
                        OnElevatorFloorSENS(1);
                }

                //Cabin is on 2st floor
                if (yCabin == (y + length * 4 - heightCabin))
                {
                    if (OnElevatorFloorSENS != null)
                        OnElevatorFloorSENS(2);
                }

                //Cabin is on 3rd floor
                if (yCabin == (y + length * 3 - heightCabin))
                {
                    if (OnElevatorFloorSENS != null)
                        OnElevatorFloorSENS(3);
                }

                //Cabin is on 4th floor
                if (yCabin == (y + length * 2 - heightCabin))
                {
                    if (OnElevatorFloorSENS != null)
                        OnElevatorFloorSENS(4);
                }

                //Cabin is on 5th floor
                if (yCabin == (y + length - heightCabin))
                {
                    if (OnElevatorFloorSENS != null)
                        OnElevatorFloorSENS(5);
                }

                #endregion

                this.Refresh();
                await Task.Delay(50); //program1FormInstance.ElevatorCabinSpeed * 10
            }
        }
        
        public void MoveRight()
        {
            xCabin += Step;
            this.Refresh();
        }

        public void MoveLeft()
        {
            xCabin -= Step;
            this.Refresh();
        }

        public void MoveUp()
        {
            yCabin -= Step;
            this.Refresh();
        }

        public void MoveDown()
        {
            yCabin += Step;
            this.Refresh();
        }

        public void WidthBigger()
        {
            widthCabin += Step;
            this.Refresh();
        }

        public void WidthSmaller()
        {
            widthCabin -= Step;
            this.Refresh();
        }

        public void LengthBigger()
        {
            heightCabin += Step;
            this.Refresh();
        }

        public void LengthSmaller()
        {
            heightCabin -= Step;
            this.Refresh();
        }
        #endregion

        //ActualFloorSig -> old
        #region ActualFloorSignalization

        public void ElevatorActualFloorLED1Signalization(bool state) //LED and BTN light ON
        {
            //var g = e.Graphics;

            if (state)
            {
                //zapnout
                //g.FillEllipse(green, x + 5 - 15, y + length * 5 - length * 3 / 4, signalizationCircle_diameter, signalizationCircle_diameter);
                btnElevatorFloor1.FlatAppearance.BorderColor = Color.Blue;
            }
            else
            {
                //vypnout
                //g.FillEllipse(white, x + 5 - 15, y + length * 5 - length * 3 / 4, signalizationCircle_diameter, signalizationCircle_diameter);
                btnElevatorFloor1.FlatAppearance.BorderColor = Color.Gray;
            }

            this.Refresh();
        }
        public void ElevatorActualFloorLED2Signalization(bool state) //LED and BTN light ON
        {
            //var g = e.Graphics;

            if (state)
            {
                //zapnout
                //g.FillEllipse(green, x + 5, y + length * 4 - length * 3 / 4, signalizationCircle_diameter, signalizationCircle_diameter);
                btnElevatorFloor2.FlatAppearance.BorderColor = Color.Blue;
            }
            else
            {
                //vypnout
                //g.FillEllipse(white, x + 5, y + length * 4 - length * 3 / 4, signalizationCircle_diameter, signalizationCircle_diameter);
                btnElevatorFloor2.FlatAppearance.BorderColor = Color.Gray;
            }

            this.Refresh();
        }
        public void ElevatorActualFloorLED3Signalization(bool state) //LED and BTN light ON
        {
            //var g = e.Graphics;

            if (state)
            {
                //zapnout
                //g.FillEllipse(green, x + 5, y + length * 3 - length * 3 / 4, signalizationCircle_diameter, signalizationCircle_diameter);
                btnElevatorFloor3.FlatAppearance.BorderColor = Color.Blue;    
            }
            else
            {
                //vypnout
                //g.FillEllipse(white, x + 5, y + length * 3 - length * 3 / 4, signalizationCircle_diameter, signalizationCircle_diameter);
                btnElevatorFloor3.FlatAppearance.BorderColor = Color.Gray;
            }

            this.Refresh();
        }
        public void ElevatorActualFloorLED4Signalization(bool state) //LED and BTN light ON
        {
            //var g = e.Graphics;

            if (state)
            {
                //zapnout
                //g.FillEllipse(green, x + 5, y + length * 2 - length * 3 / 4, signalizationCircle_diameter, signalizationCircle_diameter);
                btnElevatorFloor4.FlatAppearance.BorderColor = Color.Blue;
            }
            else
            {
                //vypnout 
                //g.FillEllipse(white, x + 5, y + length * 2 - length * 3 / 4, signalizationCircle_diameter, signalizationCircle_diameter);
                btnElevatorFloor4.FlatAppearance.BorderColor = Color.Gray;
            }

            this.Refresh();
        }
        public void ElevatorActualFloorLED5Signalization(bool state) //LED and BTN light ON
        {
            //var g = e.Graphics;

            if (state)
            {
                //zapnout
                //g.FillEllipse(green, x + 5, y + length - length * 3 / 4, signalizationCircle_diameter, signalizationCircle_diameter);
                btnElevatorFloor2.FlatAppearance.BorderColor = Color.Blue;
            }
            else
            {
                //vypnout
                //g.FillEllipse(white, x + 5, y + length - length * 3 / 4, signalizationCircle_diameter, signalizationCircle_diameter);
                btnElevatorFloor2.FlatAppearance.BorderColor = Color.Gray;
            }
            
            this.Refresh();
        }

        #endregion
    }
}
