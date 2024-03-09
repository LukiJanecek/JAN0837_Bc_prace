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

        #endregion

        public UserControlElevatorDoor()
        {
            InitializeComponent();

            DoubleBuffered = true;
            Paint += UserControlElevatorDoor_Paint;
        }

        private void UserControlElevatorDoor_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            //backgroud
            g.Clear(Color.White);

            //pen color
            Pen BlackPen = new Pen(Color.Black);

            //text parameters
            Font labelFont = new Font("Arial", 9);
            SolidBrush labelBrush = new SolidBrush(Color.Black);

            //LeftDoor
            g.DrawRectangle(BlackPen, xLeftDoor + 10, yLeftDoor + 200, widthLeftDoor + 80, heightLeftDoor + 100);

            //RightDoor
            g.DrawRectangle(BlackPen, xRightDoor + 80, yRightDoor + 200, widthRightDoor + 80, heightRightDoor + 100);

            //DoorBackground
            g.DrawRectangle(BlackPen, xDoor + 10, yDoor + 200, widthDoor + 160, heightDoor + 100);
            g.FillRectangle(yellow, xDoor + 10, yDoor + 200, widthDoor + 160, heightDoor + 100);
        }

        //Methods for door movement
        #region Methods for door movement

        private void LeftDoorMoveLeft()
        {
            widthLeftDoor -= Step;
            this.Refresh();
        }

        private void RightDoorMoveRight()
        {
            xRightDoor += Step;
            widthRightDoor -= Step;
            this.Refresh();
        }

        #endregion
    }
}
