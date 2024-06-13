using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bc_prace.Controls
{
    public partial class UserControlElevatorDoor : UserControl
    {
        //MessageBox control
        private bool errorMessageBoxShown;

        //Drawing variables
        #region Drawing variables

        private float x = 0; //x coordinate
        private float y = 0; //y coordinate
        private float length = 100;
        private float Step = 10;
        //LeftDoor 
        private float xLeftDoor = 70;
        private float yLeftDoor = 0;
        private float widthLeftDoor = 80;
        private float heightLeftDoor = 100;
        //RightDoor
        private float xRightDoor = 70;
        private float yRightDoor = 0;
        private float widthRightDoor = 80;
        private float heightRightDoor = 100;
        //DoorBackground
        private float xDoor = 70;
        private float yDoor = 0;
        private float widthDoor = 160;
        private float heightDoor = 100;

        public string lblElevatorDoorState = "Elevator state";

        private SolidBrush white = new SolidBrush(Color.White); //default
        private SolidBrush yellow = new SolidBrush(Color.Yellow); //DoorBackground color
        private SolidBrush gray = new SolidBrush(Color.Gray); //Door color

        #endregion

        public UserControlElevatorDoor()
        {
            InitializeComponent();

            DoubleBuffered = true;
            Paint += UserControlElevatorDoor_Paint;
        }

        private void UserControlElevatorDoor_Paint(object sender, PaintEventArgs e)
        {
            /*
            if (program1FormInstance == null)
                return;
            */
            var g = e.Graphics;

            //backgroud color
            g.Clear(Color.White);

            Draw(g);

            //Invalidate(); 
        }

        private void Draw(Graphics g)
        {
            //pen color
            Pen BlackPen = new Pen(Color.Black);

            //text parameters
            Font labelFont = new Font("Arial", 9);
            SolidBrush labelBrush = new SolidBrush(Color.Black);
            //lblElevatorDoorState = "Elevator state";
            g.DrawString(lblElevatorDoorState, labelFont, labelBrush, x + 30, y + length);

            //DoorBackground
            g.DrawRectangle(BlackPen, xDoor, yDoor + length * 2, widthDoor, heightDoor);
            g.FillRectangle(yellow, xDoor, yDoor + length * 2, widthDoor, heightDoor);

            //LeftDoor
            g.DrawRectangle(BlackPen, xLeftDoor, yLeftDoor + length * 2, widthLeftDoor, heightLeftDoor);
            g.FillRectangle(gray, xLeftDoor, yLeftDoor + length * 2, widthLeftDoor, heightLeftDoor);

            //RightDoor
            g.DrawRectangle(BlackPen, xRightDoor + 80, yRightDoor + length * 2, widthRightDoor, heightRightDoor);
            g.FillRectangle(gray, xRightDoor + 80, yRightDoor + length * 2, widthRightDoor, heightRightDoor);
        }

        //Methods for door movement
        #region Methods for door movement

        public async void OpenningDoor(int time) 
        {
            int realTime = 2000;

            int totalSteps = 80 / Convert.ToInt32(Step);
            int delayBetweenSteps = realTime / totalSteps;

            lblElevatorDoorState = "Door openning";

            for (int i = 0; i < totalSteps; i++)
            {
                if (widthLeftDoor > 0 && widthRightDoor > 0)
                {
                    LeftDoorMoveLeft();
                    RightDoorMoveRight();
                    await Task.Delay(delayBetweenSteps);
                }
                else
                {
                    lblElevatorDoorState = "Door open";
                    this.Refresh();
                    break;
                }
            }
            //this.Refresh(); //maybe yes maybe no, I dont know
        }

        public async void ClosingDoor(int time)
        {
            int realTime = 2000;

            int totalSteps = 80 / Convert.ToInt32(Step);
            int delayBetweenSteps = realTime / totalSteps;

            lblElevatorDoorState = "Door closing";

            for (int i = 0; i < totalSteps; i++)
            {
                if (widthLeftDoor < 80 && widthRightDoor < 80)
                {
                    LeftDoorMoveRight();
                    RightDoorMoveLeft();
                    await Task.Delay(delayBetweenSteps);
                }
                else
                {
                    lblElevatorDoorState = "Door close";
                    this.Refresh();
                    break;
                }
            }
            //this.Refresh(); //maybe yes maybe no, I dont know
        }

        public void LeftDoorMoveLeft()
        {
            if (widthLeftDoor <= 0)
            {
                return;
            }
            else 
            {
                widthLeftDoor -= Step;
                
                this.Refresh();
            }  
        }

        public void LeftDoorMoveRight()
        {
            if (widthLeftDoor >= 80)
            {
                return;
            }
            else
            {
                widthLeftDoor += Step;
                
                this.Refresh();
            }
        }

        public void RightDoorMoveLeft()
        {
            if (xRightDoor >= 230)
            {
                return;
            }
            else
            {
                xRightDoor -= Step;
                widthRightDoor += Step;

                if (xRightDoor <= 70)
                {
                    xRightDoor = 70;
                }

                this.Refresh();
            } 
        }

        public void RightDoorMoveRight()
        {
            if (widthRightDoor <= 0)
            {
                return;
            }
            else
            {
                xRightDoor += Step;
                widthRightDoor -= Step;
                
                if (xRightDoor >= 230)
                {
                    xRightDoor = 230;
                }

                this.Refresh();
            }   
        }

        #endregion
    }
}
