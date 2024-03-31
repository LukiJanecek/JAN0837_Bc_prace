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
        private float xLeftDoor = 0;
        private float yLeftDoor = 0;
        private float widthLeftDoor = 0;
        private float heightLeftDoor = 0;
        //RightDoor
        private float xRightDoor = 0;
        private float yRightDoor = 0;
        private float widthRightDoor = 0;
        private float heightRightDoor = 0;
        //DoorBackground
        private float xDoor = 0;
        private float yDoor = 0;
        private float widthDoor = 0;
        private float heightDoor = 0;

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

            //backgroud
            g.Clear(Color.White);

            Draw(g);

            //pen color
            Pen BlackPen = new Pen(Color.Black);

            //text parameters
            Font labelFont = new Font("Arial", 9);
            SolidBrush labelBrush = new SolidBrush(Color.Black);
            string lblElevatorDoorState = "Elevator state";
            g.DrawString(lblElevatorDoorState, labelFont, labelBrush, x + 10, y + length);

            //DoorBackground
            g.DrawRectangle(BlackPen, xDoor + 10, yDoor + length * 2, widthDoor + 160, heightDoor + 100);
            g.FillRectangle(yellow, xDoor + 10, yDoor + length * 2, widthDoor + 160, heightDoor + 100);

            //LeftDoor
            g.DrawRectangle(BlackPen, xLeftDoor + 10, yLeftDoor + length * 2, widthLeftDoor + 80, heightLeftDoor + 100);
            g.FillRectangle(gray, xLeftDoor + 10, yLeftDoor + length * 2, widthLeftDoor + 80, heightLeftDoor + 100);

            //RightDoor
            g.DrawRectangle(BlackPen, xRightDoor + 90, yRightDoor + length * 2, widthRightDoor + 80, heightRightDoor + 100);
            g.FillRectangle(gray, xRightDoor + 90, yRightDoor + length * 2, widthRightDoor + 80, heightRightDoor + 100);
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

        //Methods for door movement
        #region Methods for door movement

        public async void OpenningDoor(int time)
        {
            int realTime = 2000;

            int totalSteps = realTime / Convert.ToInt32(Step);
            int delayBetweenSteps = realTime / totalSteps;

            for (int i = 0; i < totalSteps; i++)
            {
                LeftDoorMoveLeft();
                RightDoorMoveRight();
                await Task.Delay(delayBetweenSteps);
            }

            this.Refresh(); //mozna
        }

        public async void ClosingDoor(int time)
        {
            int realTime = 2000;

            int totalSteps = realTime / Convert.ToInt32(Step);
            int delayBetweenSteps = realTime / totalSteps;

            for (int i = 0; i < totalSteps; i++)
            {
                LeftDoorMoveRight();
                RightDoorMoveLeft();
                await Task.Delay(delayBetweenSteps);
            }

            this.Refresh(); //mozna
        }

        public void LeftDoorMoveLeft()
        {
            xLeftDoor -= Step;
            //widthLeftDoor -= Step; //asi mi jeblo nebo tak něco
            this.Refresh();
        }

        public void LeftDoorMoveRight()
        {
            xLeftDoor += Step;
            this.Refresh();
        }

        public void RightDoorMoveLeft()
        {
            xRightDoor -= Step;
            this.Refresh();
        }

        public void RightDoorMoveRight()
        {
            xRightDoor += Step;
            //widthRightDoor -= Step; //asi mi jeblo nebo tak něco
            this.Refresh();
        }

        #endregion
    }
}
