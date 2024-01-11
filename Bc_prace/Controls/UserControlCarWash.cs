using Microsoft.Win32;
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
    public partial class UserControlCarWash : UserControl
    {
        //Variables 
        #region Variables 

        private Label label;

        //beggining points of drawing
        private float x = 15;
        private float y = 15;
        private float door1X;
        private float door1Y;
        private float door2X;
        private float door2Y;

        //basic parametres
        private float length = 100;
        private float door_width = 20;
        private float door_height = 100; //should be equal to length

        #endregion
        public UserControlCarWash()
        {
            InitializeComponent();
            DoubleBuffered = true;
            Paint += UserControl1_Paint;
        }

        private void UserControl1_Paint(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            //background
            g.Clear(Color.White);

            //pen color
            Pen BlackPen = new Pen(Color.Black, 2);
            Pen WhitePen = new Pen(Color.White, 2);
            Pen RedPen = new Pen(Color.Red, 2);
            Pen GreenPen = new Pen(Color.Green, 2);
            Pen YellowPen = new Pen(Color.Yellow, 2);

            //CarWash constructon
            #region CarWash construction

            //basic line => floor
            g.DrawLine(BlackPen, x, y + length * 5, x + length * 10, y + length * 5);

            //doors
            #region Doors

            door1X = x + length * 2;
            door1Y = y + length * 4;
            door2X = x + length * 8 - door_width;
            door2Y = y + length * 4;

            //front door
            #region Front door 
            g.DrawRectangle(BlackPen, door1X, door1Y, door_width, door_height);
            #endregion

            //back door 
            #region Back door 
            g.DrawRectangle(BlackPen, door2X, door2Y, door_width, door_height);
            #endregion

            #endregion

            //edges 
            #region Edges 
            //left wall 
            g.DrawLine(BlackPen, x + length * 2, y + length * 5, x + length * 2, y + length * 2);

            //roof 
            g.DrawLine(BlackPen, x + length * 2, y + length * 2, x + length * 8, y + length * 2);

            //right wall 
            g.DrawLine(BlackPen, x + length * 8, y + length * 2, x + length * 8, y + length * 5);

            #endregion

            #endregion

            //Inner signalization 
            #region Inner cyclus signalization 
            
            //label in UserControl
            Font labelFont = new Font("Arial", 9); // Přizpůsobte podle potřeby
            SolidBrush labelBrush = new SolidBrush(Color.Black); // Přizpůsobte podle potřeby

            //Wax
            string labelWax = "Wax";
            float WaxX = x;
            float WaxY = y;
            g.DrawString(labelWax, labelFont, labelBrush, WaxX, WaxY);

            //Brushes 
            string labelBrushes = "Brushes";
            float BrushesX = x;
            float BrushesY = y;
            g.DrawString(labelBrushes, labelFont, labelBrush, BrushesX, BrushesY);

            //Water
            string labelWater = "Water";
            float WaterX = x;
            float WaterY = y;
            g.DrawString(labelWater, labelFont, labelBrush, WaxX, WaxY);

            //ActiveFoam
            string labelActiveFoam = "ActiveFoam";
            float ActiveFoamX = x;
            float ActiveFoamY = y;
            g.DrawString(labelActiveFoam, labelFont, labelBrush, ActiveFoamX, ActiveFoamY);

            //Drying
            string labelDrying = "Drying";
            float DryingX = x;
            float DryingY = y;
            g.DrawString(labelDrying, labelFont, labelBrush, DryingX, DryingY);

            //Soap
            string labelSoap = "Soap";
            float SoapX = x;
            float SoapY = y;
            g.DrawString(labelSoap, labelFont, labelBrush, SoapX, SoapY);

            #endregion

        }

        public void WaterSignalization()
        {

        }

        public void WaxSignalization()
        {

        }
        public void SoapSignalization()
        {

        }

        public void ActiveFoamSignalization()
        {

        }

        public void BrushesSignalization()
        {

        }

        public void DryingSignalization()
        {

        }
    }
}
