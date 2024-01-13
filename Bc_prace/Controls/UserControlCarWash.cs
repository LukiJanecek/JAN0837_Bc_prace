using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
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

        private float aDoor1;
        private float bDoor1;
        private float cDoor1;
        private float dDoor1;
        private float aDoor2;
        private float bDoor2;
        private float cDoor2;
        private float dDoor2;   
            
        private float WaxX;
        private float WaxY;
        private float WaterX;
        private float WaterY;
        private float ActiveFoamX;
        private float ActiveFoamY;
        private float SoapX;
        private float SoapY;
        private float DryingX;
        private float DryingY;
        private float BrushesX;
        private float BrushesY;

        private float timeDoor = 200;

        //basic parametres
        private float length = 100;
        private float door_width = 20;
        private float door_height = 100; //should be equal to length
        private float signalizationCircle_diameter = 10;

        private SolidBrush white = new SolidBrush(Color.White); //default
        private SolidBrush green = new SolidBrush(Color.Green); //soap
        private SolidBrush yellow = new SolidBrush(Color.Yellow); //wax
        private SolidBrush red = new SolidBrush(Color.Red); //brushes
        private SolidBrush blue = new SolidBrush(Color.Blue); //water
        private SolidBrush brown = new SolidBrush(Color.Brown); //drying
        private SolidBrush purple = new SolidBrush(Color.Purple); //activefoam

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
            g.DrawLine(BlackPen, x, y + length * 4, x + length * 10, y + length * 4);

            //doors
            #region Doors

            door1X = x + length * 2;
            door1Y = y + length * 3;
            door2X = x + length * 8 - door_width;
            door2Y = y + length * 3;

            //front door
            #region Front door 
            g.DrawRectangle(BlackPen, door1X + aDoor1, door1Y + bDoor1, door_width + cDoor1, door_height + dDoor1);
            #endregion

            //back door 
            #region Back door 
            g.DrawRectangle(BlackPen, door2X + aDoor2, door2Y + bDoor2, door_width + cDoor2, door_height + dDoor2);
            #endregion

            #endregion

            //edges 
            #region Edges 
            //left wall 
            g.DrawLine(BlackPen, x + length * 2, y + length * 4, x + length * 2, y + length);

            //roof 
            g.DrawLine(BlackPen, x + length * 2, y + length, x + length * 8, y + length);

            //right wall 
            g.DrawLine(BlackPen, x + length * 8, y + length, x + length * 8, y + length * 4);

            #endregion

            #endregion

            //Inner signalization 
            #region Inner cyclus signalization 
            
            //label in UserControl
            Font labelFont = new Font("Arial", 9); 
            SolidBrush labelBrush = new SolidBrush(Color.Black);

            //car position
            g.DrawEllipse(BlackPen, x, y + length * 3 + length / 2, signalizationCircle_diameter, signalizationCircle_diameter);

            //position line  
            g.DrawLine(BlackPen, x + length * 3, y + length * 2, x + length * 7, y + length * 2);
            g.DrawEllipse(BlackPen, x + length * 3 - (signalizationCircle_diameter / 2), y + length * 2 - (signalizationCircle_diameter / 2), signalizationCircle_diameter, signalizationCircle_diameter);

            //Wax
            string labelWax = "Wax";
            WaxX = x + length * 2 + 20;
            WaxY = y + length + 10;
            g.DrawString(labelWax, labelFont, labelBrush, WaxX, WaxY);
            g.DrawEllipse(BlackPen, WaxX - 15, WaxY, signalizationCircle_diameter, signalizationCircle_diameter);
                        
            //Water
            string labelWater = "Water";
            WaterX = x + length * 2 + 20;
            WaterY = y + length + 30;
            g.DrawString(labelWater, labelFont, labelBrush, WaterX, WaterY);
            g.DrawEllipse(BlackPen, WaterX - 15, WaterY, signalizationCircle_diameter, signalizationCircle_diameter);

            //ActiveFoam
            string labelActiveFoam = "ActiveFoam";
            ActiveFoamX = x + length * 2 + 20;
            ActiveFoamY = y + length + 50;
            g.DrawString(labelActiveFoam, labelFont, labelBrush, ActiveFoamX, ActiveFoamY);
            g.DrawEllipse(BlackPen, ActiveFoamX - 15, ActiveFoamY, signalizationCircle_diameter, signalizationCircle_diameter);
                        
            //Soap
            string labelSoap = "Soap";
            SoapX = x + length * 2 + 20;
            SoapY = y + length + 70;
            g.DrawString(labelSoap, labelFont, labelBrush, SoapX, SoapY);
            g.DrawEllipse(BlackPen, SoapX - 15, SoapY, signalizationCircle_diameter, signalizationCircle_diameter);

            //Drying
            string labelDrying = "Drying";
            DryingX = x + length * 3 + 20;
            DryingY = y + length + 10;
            g.DrawString(labelDrying, labelFont, labelBrush, DryingX, DryingY);
            g.DrawEllipse(BlackPen, DryingX - 15, DryingY, signalizationCircle_diameter, signalizationCircle_diameter);

            //Brushes 
            string labelBrushes = "Brushes";
            BrushesX = x + length * 3 + 20;
            BrushesY = y + length + 30;
            g.DrawString(labelBrushes, labelFont, labelBrush, BrushesX, BrushesY);
            g.DrawEllipse(BlackPen, BrushesX - 15, BrushesY, signalizationCircle_diameter, signalizationCircle_diameter);

            #endregion

        }

        //Methods for reaction on Tia variable change 
        #region Methods for reaction on Tia variable change 

        public async void door1UP()
        {
            for (int i = 0; i <= Convert.ToInt32(length); i += Convert.ToInt32(length) / 10)
            {
                bDoor1 -= 10;
                this.Refresh();
                await Task.Delay(Convert.ToInt32(timeDoor));
            }
        }

        public async void door1DOWN()
        {
            for (int i = 0; i <= Convert.ToInt32(length); i += Convert.ToInt32(length) / 10)
            {
                bDoor1 += 10;
                this.Refresh();
                await Task.Delay(Convert.ToInt32(timeDoor));
            }
        }

        public async void door2UP()
        {
            for (int i = 0; i <= Convert.ToInt32(length); i += Convert.ToInt32(length)/10)
            {
                bDoor2 -= 10;
                this.Refresh();
                await Task.Delay(Convert.ToInt32(timeDoor));
            }
        }

        public async void door2DOWN() 
        {
            for (int i = 0; i <= Convert.ToInt32(length); i += Convert.ToInt32(length) / 10)
            {
                bDoor2 += 10;
                this.Refresh();
                await Task.Delay(Convert.ToInt32(timeDoor));
            }
        }

        public void WaterSignalizationON()
        {
            var g = this.CreateGraphics();
                        
            g.FillEllipse(blue, WaterX - 15, WaterY, signalizationCircle_diameter, signalizationCircle_diameter);
        }
        public void WaterSignalizationOFF()
        {
            var g = this.CreateGraphics();

            g.FillEllipse(white, WaterX - 15, WaterY, signalizationCircle_diameter, signalizationCircle_diameter);
        }
        public void WaxSignalizationON()
        {
            var g = this.CreateGraphics();

            g.FillEllipse(yellow, WaxX - 15, WaxY, signalizationCircle_diameter, signalizationCircle_diameter);
        }
        public void WaxSignalizationOFF()
        {
            var g = this.CreateGraphics();

            g.FillEllipse(white, WaxX - 15, WaxY, signalizationCircle_diameter, signalizationCircle_diameter);
        }
        public void SoapSignalizationON()
        {
            var g = this.CreateGraphics();

            g.FillEllipse(green, SoapX - 15, SoapY, signalizationCircle_diameter, signalizationCircle_diameter);
        }
        public void SoapSignalizationOFF()
        {
            var g = this.CreateGraphics();

            g.FillEllipse(white, SoapX - 15, SoapY, signalizationCircle_diameter, signalizationCircle_diameter);
        }

        public void ActiveFoamSignalizationON()
        {
            var g = this.CreateGraphics();

            g.FillEllipse(purple, ActiveFoamX - 15, ActiveFoamY, signalizationCircle_diameter, signalizationCircle_diameter);
        }
        public void ActiveFoamSignalizationOFF()
        {
            var g = this.CreateGraphics();

            g.FillEllipse(white, ActiveFoamX - 15, ActiveFoamY, signalizationCircle_diameter, signalizationCircle_diameter);
        }

        public void BrushesSignalizationON()
        {
            var g = this.CreateGraphics();

            g.FillEllipse(red, BrushesX - 15, BrushesY, signalizationCircle_diameter, signalizationCircle_diameter);
        }
        public void BrushesSignalizationOFF()
        {
            var g = this.CreateGraphics();

            g.FillEllipse(white, BrushesX - 15, BrushesY, signalizationCircle_diameter, signalizationCircle_diameter);
        }

        public void DryingSignalizationON()
        {
            var g = this.CreateGraphics();

            g.FillEllipse(brown, DryingX - 15, DryingY, signalizationCircle_diameter, signalizationCircle_diameter);
        }
        public void DryingSignalizationOFF()
        {
            var g = this.CreateGraphics();

            g.FillEllipse(white, DryingX - 15, DryingY, signalizationCircle_diameter, signalizationCircle_diameter);
        }

        #endregion
    }
}
