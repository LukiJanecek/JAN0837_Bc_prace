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
using Bc_prace.Controls.MyGraphControl.Entities;
using System.Windows.Forms;
using Sharp7;

namespace Bc_prace.Controls
{
    public partial class UserControlCarWash : UserControl
    {
        //Drawing variables
        #region Drawing variables 

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
        private float CarSignalizationX;
        private float CarSignalizationY;
        private float PreWashX;
        private float PreWashY;

        private float ShowerX;
        private float ShowerY;

        private float timeDoor = 200;

        //images of car
        private PictureBox pictureBoxCar;

        private float pictureX;
        private float pictureY;

        private float leftArrowX;
        private float leftArrowY;
        private float rightArrowX;
        private float rightArrowY;

        private int picture = 1;

        //basic parametres
        private float arrowLength = 20;
        private float length = 100;
        private float door_width = 20;
        private float door_height = 100; //should be equal to length
        private float signalizationCircle_diameter = 10;
        private float shower_width = 20;
        private float shower_height = 100;

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
            Paint += UserControlCarWash_Paint;
            InitializeCarImage();
        }

        private void UserControlCarWash_Paint(object? sender, PaintEventArgs e)
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

            //label in UserControl
            Font labelFont = new Font("Arial", 9);
            SolidBrush labelBrush = new SolidBrush(Color.Black);

            //CarWash constructon
            #region CarWash construction

            //basic line => floor
            g.DrawLine(BlackPen, x, y + length * 4, x + length * 12, y + length * 4);

            //ChooseVariant
            g.DrawLine(BlackPen, x + length * 2, y + length * 4, x + length * 2, y + length * 4 - length / 2);
            g.DrawLine(BlackPen, x + length * 2, y + length * 4 - length / 2, x + length * 2 + 20, y + length * 4 - length / 2);
            g.DrawLine(BlackPen, x + length * 2 + 20, y + length * 4, x + length * 2 + 20, y + length * 4 - length / 2);
            g.DrawRectangle(BlackPen, x + length * 2 + 5, y + length * 4 - length / 2 + 10, 10, 5);

            //PreWash
            #region PreWash

            string lblPreWash = "PreWash";
            PreWashX = x + length * 3;
            PreWashY = y + length * 3;
            g.DrawString(lblPreWash, labelFont, labelBrush, PreWashX, PreWashY);
            g.DrawEllipse(BlackPen, PreWashX - 15, PreWashY, signalizationCircle_diameter, signalizationCircle_diameter);
            g.DrawLine(BlackPen, x + length * 3 + length / 2, y + length * 4, x + length * 3 + length / 2, y + length * 4 - length / 2);
            g.DrawLine(BlackPen, x + length * 3 + length / 2, y + length * 4 - length / 2, x + length * 3 + length / 2 + 20, y + length * 4 - length / 2);
            g.DrawLine(BlackPen, x + length * 3 + length / 2 + 20, y + length * 4, x + length * 3 + length / 2 + 20, y + length * 4 - length / 2);
            //side
            g.DrawLine(BlackPen, x + length * 3 + length / 2 + 20, y + length * 4 - length / 2 + length / 4, x + length * 3 + length / 2 + 30, y + length * 4 - length / 2 + length / 4);
            g.DrawLine(BlackPen, x + length * 3 + length / 2 + 30, y + length * 4 - 10, x + length * 3 + length / 2 + 30, y + length * 4 - length / 2 + 10);

            #endregion

            //Doors
            #region Doors

            door1X = x + length * 4;
            door1Y = y + length * 3;
            door2X = x + length * 10 - door_width;
            door2Y = y + length * 3;

            //front door
            g.DrawRectangle(BlackPen, door1X + aDoor1, door1Y + bDoor1, door_width + cDoor1, door_height + dDoor1);

            //back door 
            g.DrawRectangle(BlackPen, door2X + aDoor2, door2Y + bDoor2, door_width + cDoor2, door_height + dDoor2);

            #endregion

            //CarWash edges 
            #region CarWash edges 
            //left wall 
            g.DrawLine(BlackPen, x + length * 4, y + length * 3, x + length * 4, y + length);

            //roof 
            g.DrawLine(BlackPen, x + length * 4, y + length, x + length * 10, y + length);

            //right wall 
            g.DrawLine(BlackPen, x + length * 10, y + length, x + length * 10, y + length * 3);

            #endregion

            //Shower
            g.DrawRectangle(BlackPen, x + ShowerX + length * 7 - (shower_width / 2), y + ShowerY + length * 3, shower_width, shower_height);

            #endregion

            //Inner signalization 
            #region Inner cyclus signalization 

            //car position
            //g.DrawEllipse(BlackPen, x, y + length * 3 + length / 2, signalizationCircle_diameter, signalizationCircle_diameter);

            //position line  
            g.DrawLine(BlackPen, x + length * 5, y + length * 2, x + length * 9, y + length * 2);
            g.DrawEllipse(BlackPen, x + ShowerX + length * 7 - (signalizationCircle_diameter / 2), y + ShowerY + length * 2 - (signalizationCircle_diameter / 2), signalizationCircle_diameter, signalizationCircle_diameter);

            //car signalization => // this doesnt make sense anymore
            string labelCarSignalization = "GO!"; //this doesnt make sense anymore
            CarSignalizationX = x + length * 7 + 20;
            CarSignalizationY = y + length * 2 + length / 2;
            g.DrawString(labelCarSignalization, labelFont, labelBrush, CarSignalizationX, CarSignalizationY); // this doesnt make sense anymore
            g.DrawEllipse(BlackPen, CarSignalizationX - 15, CarSignalizationY, signalizationCircle_diameter, signalizationCircle_diameter); // this doesnt make sense anymore

            //Water
            string labelWater = "Water";
            WaterX = x + length * 4 + 20;
            WaterY = y + length + 10;
            g.DrawString(labelWater, labelFont, labelBrush, WaterX, WaterY);
            g.DrawEllipse(BlackPen, WaterX - 15, WaterY, signalizationCircle_diameter, signalizationCircle_diameter);

            //Drying
            string labelDrying = "Drying";
            DryingX = x + length * 5 + 20;
            DryingY = y + length + 10;
            g.DrawString(labelDrying, labelFont, labelBrush, DryingX, DryingY);
            g.DrawEllipse(BlackPen, DryingX - 15, DryingY, signalizationCircle_diameter, signalizationCircle_diameter);

            //Soap
            string labelSoap = "Soap";
            SoapX = x + length * 6 + 20;
            SoapY = y + length + 10;
            g.DrawString(labelSoap, labelFont, labelBrush, SoapX, SoapY);
            g.DrawEllipse(BlackPen, SoapX - 15, SoapY, signalizationCircle_diameter, signalizationCircle_diameter);

            //ActiveFoam
            string labelActiveFoam = "ActiveFoam";
            ActiveFoamX = x + length * 7 + 20;
            ActiveFoamY = y + length + 10;
            g.DrawString(labelActiveFoam, labelFont, labelBrush, ActiveFoamX, ActiveFoamY);
            g.DrawEllipse(BlackPen, ActiveFoamX - 15, ActiveFoamY, signalizationCircle_diameter, signalizationCircle_diameter);

            //Wax
            string labelWax = "Wax";
            WaxX = x + length * 8 + 20;
            WaxY = y + length + 10;
            g.DrawString(labelWax, labelFont, labelBrush, WaxX, WaxY);
            g.DrawEllipse(BlackPen, WaxX - 15, WaxY, signalizationCircle_diameter, signalizationCircle_diameter);

            //Brushes 
            string labelBrushes = "Brushes";
            BrushesX = x + length * 9 + 20;
            BrushesY = y + length + 10;
            g.DrawString(labelBrushes, labelFont, labelBrush, BrushesX, BrushesY);
            g.DrawEllipse(BlackPen, BrushesX - 15, BrushesY, signalizationCircle_diameter, signalizationCircle_diameter);

            #endregion

            //Arrows
            #region Arrows

            leftArrowX = x + length;
            leftArrowY = y + length * 2;
            rightArrowX = x + length * 11;
            rightArrowY = y + length * 2;

            //LeftArrow
            g.DrawLine(BlackPen, leftArrowX + arrowLength, leftArrowY + arrowLength, leftArrowX + arrowLength, leftArrowY + arrowLength * 2);
            g.DrawLine(BlackPen, leftArrowX + arrowLength, leftArrowY + arrowLength, leftArrowX + arrowLength * 2, leftArrowY + arrowLength);
            g.DrawLine(BlackPen, leftArrowX + arrowLength, leftArrowY + arrowLength * 2, leftArrowX + arrowLength * 2, leftArrowY + arrowLength * 2);
            g.DrawLine(BlackPen, leftArrowX + arrowLength * 2, leftArrowY + arrowLength, leftArrowX + arrowLength * 2, leftArrowY + arrowLength - arrowLength / 2);
            g.DrawLine(BlackPen, leftArrowX + arrowLength * 2, leftArrowY + arrowLength * 2, leftArrowX + arrowLength * 2, leftArrowY + arrowLength * 2 + arrowLength / 2);
            g.DrawLine(BlackPen, leftArrowX + arrowLength * 2, leftArrowY + arrowLength - arrowLength / 2, leftArrowX + arrowLength * 3, leftArrowY + arrowLength + arrowLength / 2);
            g.DrawLine(BlackPen, leftArrowX + arrowLength * 2, leftArrowY + arrowLength * 2 + arrowLength / 2, leftArrowX + arrowLength * 3, leftArrowY + arrowLength + arrowLength / 2);

            //RightArrow
            g.DrawLine(BlackPen, rightArrowX + arrowLength, rightArrowY + arrowLength, rightArrowX + arrowLength, rightArrowY + arrowLength * 2);
            g.DrawLine(BlackPen, rightArrowX + arrowLength, rightArrowY + arrowLength, rightArrowX + arrowLength * 2, rightArrowY + arrowLength);
            g.DrawLine(BlackPen, rightArrowX + arrowLength, rightArrowY + arrowLength * 2, rightArrowX + arrowLength * 2, rightArrowY + arrowLength * 2);
            g.DrawLine(BlackPen, rightArrowX + arrowLength * 2, rightArrowY + arrowLength, rightArrowX + arrowLength * 2, rightArrowY + arrowLength - arrowLength / 2);
            g.DrawLine(BlackPen, rightArrowX + arrowLength * 2, rightArrowY + arrowLength * 2, rightArrowX + arrowLength * 2, rightArrowY + arrowLength * 2 + arrowLength / 2);
            g.DrawLine(BlackPen, rightArrowX + arrowLength * 2, rightArrowY + arrowLength - arrowLength / 2, rightArrowX + arrowLength * 3, rightArrowY + arrowLength + arrowLength / 2);
            g.DrawLine(BlackPen, rightArrowX + arrowLength * 2, rightArrowY + arrowLength * 2 + arrowLength / 2, rightArrowX + arrowLength * 3, rightArrowY + arrowLength + arrowLength / 2);

            #endregion
        }

        //Methods for reaction on Tia variable change 
        #region Methods for reaction on Tia variable change 

        //Door movement
        #region Door movement
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
            for (int i = 0; i <= Convert.ToInt32(length); i += Convert.ToInt32(length) / 10)
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

        #endregion

        //Shower movement
        #region Shower movement

        public async void ShowerMoveLeft()
        {
            for (int i = 0; i <= Convert.ToInt32(length * 2); i += Convert.ToInt32(length) / 10)
            {
                ShowerX -= 10;
                this.Refresh();
                await Task.Delay(Convert.ToInt32(timeDoor));
            }
        }

        public async void ShowerMoveRight()
        {
            for (int i = 0; i <= Convert.ToInt32(length * 2); i += Convert.ToInt32(length) / 10)
            {
                ShowerX += 10;
                this.Refresh();
                await Task.Delay(Convert.ToInt32(timeDoor));
            }
        }

        #endregion

        //Inner cycle signalization
        #region Inner cycle signalization
        public void PreWashSignalization(bool state)
        {
            var g = this.CreateGraphics(); //this doesnt work properly

            if (state)
            {
                g.FillEllipse(blue, PreWashX - 15, PreWashY, signalizationCircle_diameter, signalizationCircle_diameter);
            }
            else
            {
                g.FillEllipse(blue, PreWashX - 15, PreWashY, signalizationCircle_diameter, signalizationCircle_diameter);
            }

            this.Refresh();
        }
        
        public void WaterSignalization(bool state)
        {
            var g = this.CreateGraphics(); //this doesnt work properly

            if (state)
            {
                g.FillEllipse(blue, WaterX - 15, WaterY, signalizationCircle_diameter, signalizationCircle_diameter);
            }
            else
            {
                g.FillEllipse(white, WaterX - 15, WaterY, signalizationCircle_diameter, signalizationCircle_diameter);
            }

            this.Refresh();
        }
        
        public void WaxSignalization(bool state)
        { 
            var g = this.CreateGraphics(); //this doesnt work properly

            if (state)
            {
                g.FillEllipse(yellow, WaxX - 15, WaxY, signalizationCircle_diameter, signalizationCircle_diameter);
            }
            else
            {
                g.FillEllipse(white, WaxX - 15, WaxY, signalizationCircle_diameter, signalizationCircle_diameter);
            }

            this.Refresh();
        }
        
        public void SoapSignalization(bool state)
        {
            var g = this.CreateGraphics(); //this doesnt work properly

            if (state)
            {
                g.FillEllipse(green, SoapX - 15, SoapY, signalizationCircle_diameter, signalizationCircle_diameter);
            }
            else
            {
                g.FillEllipse(white, SoapX - 15, SoapY, signalizationCircle_diameter, signalizationCircle_diameter);
            }

            this.Refresh();
        }
        

        public void ActiveFoamSignalization(bool state)
        {
            var g = this.CreateGraphics(); //this doesnt work properly

            if (state)
            {
                g.FillEllipse(purple, ActiveFoamX - 15, ActiveFoamY, signalizationCircle_diameter, signalizationCircle_diameter);
            }
            else 
            {
                g.FillEllipse(white, ActiveFoamX - 15, ActiveFoamY, signalizationCircle_diameter, signalizationCircle_diameter);
            }

            this.Refresh();
        }
        
        public void BrushesSignalization(bool state)
        {
            var g = this.CreateGraphics(); //this doesnt work properly

            if (state)
            {
                g.FillEllipse(red, BrushesX - 15, BrushesY, signalizationCircle_diameter, signalizationCircle_diameter);
            }
            else
            {
                g.FillEllipse(white, BrushesX - 15, BrushesY, signalizationCircle_diameter, signalizationCircle_diameter);
            }

            this.Refresh();
        }
        

        public void DryingSignalization(bool state)
        {
            var g = this.CreateGraphics(); //this doesnt work properly

            if (state)
            {
                g.FillEllipse(brown, DryingX - 15, DryingY, signalizationCircle_diameter, signalizationCircle_diameter);
            }
            else
            {
                g.FillEllipse(white, DryingX - 15, DryingY, signalizationCircle_diameter, signalizationCircle_diameter);
            }

            this.Refresh();
        }
        
        #endregion

        //Car picture
        #region Car picture

        public void InitializeCarImage()
        {
            if (pictureBoxCar != null && !pictureBoxCar.IsDisposed)
            {
                Controls.Remove(pictureBoxCar);
                pictureBoxCar.Dispose();
            }

            pictureBoxCar = new PictureBox();

            if (picture == 1)
            {
                //Controls.Remove(pictureBoxCar);
                //this.Refresh();
                //pictureBoxCar = new PictureBox();
                pictureBoxCar.Image = Image.FromFile("C:\\Users\\lukas\\OneDrive\\Dokumenty\\VŠ\\Bc_prace\\Bc\\C#\\final\\JAN0837_Bc_prace\\Bc_prace\\Resources\\car_64.png");
            }
            else if (picture == 2)
            {
                //Controls.Remove(pictureBoxCar);
                //this.Refresh();
                //pictureBoxCar = new PictureBox();
                pictureBoxCar.Image = Image.FromFile("C:\\Users\\lukas\\OneDrive\\Dokumenty\\VŠ\\Bc_prace\\Bc\\C#\\final\\JAN0837_Bc_prace\\Bc_prace\\Resources\\car_brushes_64.png");
            }
            else if (picture == 3)
            {
                //Controls.Remove(pictureBoxCar);
                //this.Refresh();
                //pictureBoxCar = new PictureBox();
                pictureBoxCar.Image = Image.FromFile("C:\\Users\\lukas\\OneDrive\\Dokumenty\\VŠ\\Bc_prace\\Bc\\C#\\final\\JAN0837_Bc_prace\\Bc_prace\\Resources\\car_washing_64.png");
            }
            else if (picture == 4)
            {
                //Controls.Remove(pictureBoxCar);
                //this.Refresh();
                //pictureBoxCar = new PictureBox();
                pictureBoxCar.Image = Image.FromFile("C:\\Users\\lukas\\OneDrive\\Dokumenty\\VŠ\\Bc_prace\\Bc\\C#\\final\\JAN0837_Bc_prace\\Bc_prace\\Resources\\car_done_64.png");
            }
            else
            {
                picture = 1;
            }

            pictureBoxCar.Location = new Point(Convert.ToInt32(x + pictureX), Convert.ToInt32(length * 3 + (length / 2) + pictureY));
            pictureBoxCar.Size = new Size(64, 64); //all pictures are for 64 px 
            Controls.Add(pictureBoxCar);
        }

        public async void MoveCarToNextPoint(int point) //možná budu muset přidat krok o kolik se má posunout
        {
            float targetX;

            switch (point)
            {
                case 1: // move to select washing method

                    targetX = length * 2 - 64;

                    while (pictureBoxCar.Location.X < targetX)
                    {
                        pictureX += 10;
                        pictureBoxCar.Location = new Point(Convert.ToInt32(x + pictureX), Convert.ToInt32(length * 3 + (length / 2) + pictureY));
                        this.Refresh();
                        await Task.Delay(Convert.ToInt32(timeDoor));
                    }

                    break;
                case 2: // move to PreWash

                    targetX = length * 3 - 10;

                    while (pictureBoxCar.Location.X < targetX)
                    {
                        pictureX += 10;
                        pictureBoxCar.Location = new Point(Convert.ToInt32(x + pictureX), Convert.ToInt32(length * 3 + (length / 2) + pictureY));
                        this.Refresh();
                        await Task.Delay(Convert.ToInt32(timeDoor));
                    }

                    break;

                case 3: // move to CarPosition from PreWash

                    targetX = length * 7 - 64;

                    while (pictureBoxCar.Location.X < targetX)
                    {
                        pictureX += 10;
                        pictureBoxCar.Location = new Point(Convert.ToInt32(x + pictureX), Convert.ToInt32(length * 3 + (length / 2) + pictureY));
                        this.Refresh();
                        await Task.Delay(Convert.ToInt32(timeDoor));
                    }

                    break;
                case 4: // move out 

                    targetX = length * 12 - 64;

                    while (pictureBoxCar.Location.X < targetX)
                    {
                        pictureX += 10;
                        pictureBoxCar.Location = new Point(Convert.ToInt32(x + pictureX), Convert.ToInt32(length * 3 + (length / 2) + pictureY));
                        this.Refresh();
                        await Task.Delay(Convert.ToInt32(timeDoor));
                    }

                    break;
            }
        }

        public void ManualMovePictureLEFT()
        {
            pictureX -= 10;
            pictureBoxCar.Location = new Point(Convert.ToInt32(x + pictureX), Convert.ToInt32(length * 3 + (length / 2) + pictureY));
            this.Refresh();
            InitializeCarImage();
        }

        public void ManualMovePictureRIGHT()
        {
            pictureX += 10;
            pictureBoxCar.Location = new Point(Convert.ToInt32(x + pictureX), Convert.ToInt32(length * 3 + (length / 2) + pictureY));
            this.Refresh();
            InitializeCarImage();
        }

        #endregion

        #endregion
    }
}
