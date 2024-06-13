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
using System.Diagnostics.Eventing.Reader;
using OxyPlot.Series;
using Microsoft.VisualBasic.FileIO;

public delegate void CarWashPositionCar(bool state);

namespace Bc_prace.Controls
{
    public partial class UserControlCarWash : UserControl
    {
        public event CarWashPositionCar OnCarWashPositionCar;

        //Drawing variables
        #region Drawing variables 

        //private Label label;

        //beggining points of drawing
        private float x = 15;
        private float y = 15;

        //door coordinates
        private float door1X;
        private float door1Y;
        private float door2X;
        private float door2Y;

        //door parameters 
        private float aDoor1;
        private float bDoor1;
        private float cDoor1;
        private float dDoor1;
        private float aDoor2;
        private float bDoor2;
        private float cDoor2;
        private float dDoor2;

        //Shower coordinates 
        private float ShowerX = 705;
        private float ShowerY;

        //images of car
        private PictureBox pictureBoxCar;
        private int picture = 1;
        //image coordinates 
        private float pictureX;
        private float pictureY;

        //Arrows coordinates 
        private float leftArrowX;
        private float leftArrowY;
        private float rightArrowX;
        private float rightArrowY;

        //basic parametres
        private float arrowLength = 20;
        private float length = 100;
        private float door_width = 20;
        private float door_height = 100; //should be equal to length
        private float signalizationCircle_diameter = 10;
        private float shower_width = 20;
        private float shower_height = 100;
        private float step = 10;
        private float TimeMovement = 200;

        private SolidBrush white = new SolidBrush(Color.White); //default
        private SolidBrush green = new SolidBrush(Color.Green); //soap
        private SolidBrush yellow = new SolidBrush(Color.Yellow); //wax
        private SolidBrush red = new SolidBrush(Color.Red); //brushes
        private SolidBrush blue = new SolidBrush(Color.Blue); //water
        private SolidBrush brown = new SolidBrush(Color.Brown); //drying
        private SolidBrush purple = new SolidBrush(Color.Purple); //activefoam

        #endregion

        //CarWash function variables 
        #region CarWash function variables 

        //Functions coordinates 
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
        private float VarnishProtectionX;
        private float VarnishProtectionY;
        private float CarSignalizationX;
        private float CarSignalizationY;
        private float PreWashX;
        private float PreWashY;

        //Inner cyklus signalization
        #region Inner cyklus signalization
                
        //PreWash
        private bool preWash;

        public bool PreWash
        {
            get { return preWash; } 
            
            set
            {
                preWash = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //Water
        private bool water;

        public bool Water
        {
            get { return water; }

            set
            {
                water = value;

                Invalidate(); //toto tu asi být nemusí
            }
        }

        //Wax
        private bool wax;

        public bool Wax
        {
            get { return wax; }

            set
            {
                wax = value;

                Invalidate(); //toto tu asi být nemusí
            }
        }

        //ActiveFoam
        private bool activeFoam;

        public bool ActiveFoam
        {
            get { return activeFoam; }

            set
            {
                activeFoam = value;

                Invalidate(); //toto tu asi být nemusí
            }
        }

        //Soap 
        private bool soap;

        public bool Soap
        {
            get { return soap; }

            set
            {
                soap = value;

                Invalidate(); //toto tu asi být nemusí
            }
        }

        //Drying 
        private bool drying;

        public bool Drying
        {
            get { return drying; }

            set
            {
                drying = value;

                Invalidate(); //toto tu asi být nemusí
            }
        }

        //Brushes 
        private bool brushes;

        public bool Brushes
        {
            get { return brushes; }

            set
            {
                brushes = value;

                Invalidate(); //toto tu asi být nemusí
            }
        }

        //Varnish protection 
        private bool varnishProtection;

        public bool VarnishProtection
        {
            get { return  varnishProtection; } 

            set
            {
                varnishProtection = value;

                Invalidate(); //toto tu asi být nemusí
            }
        }

        //CarWash Red Light
        private bool carWashRedLight;

        public bool CarWashRedLight
        {
            get { return carWashRedLight; }

            set
            {
                carWashRedLight = value;

                Invalidate(); //toto tu asi být nemusí
            }
        }

        //CarWash Yellow light
        private bool carWashYellowLight;

        public bool CarWashYellowLight
        {
            get { return carWashYellowLight; }

            set
            {
                carWashYellowLight = value;

                Invalidate(); //toto tu asi být nemusí
            }
        }

        //CarWash Green light 
        private bool carWashGreenLight;

        public bool CarWashGreenLight
        {
            get { return carWashGreenLight; }

            set
            {
                carWashGreenLight = value;

                Invalidate(); //toto tu asi být nemusí
            }
        }

        #endregion

        #endregion

        public UserControlCarWash()
        {
            InitializeComponent();
            DoubleBuffered = true; 
            Paint += UserControlCarWash_Paint;
            InitializeCarImage(1);
        }

        private void UserControlCarWash_Paint(object? sender, PaintEventArgs e)
        {
            /*
            if (program2FormInstance == null)
                return;
            */
            var g = e.Graphics;

            //background color
            g.Clear(Color.White);

            Draw(g); 

            //Invalidate(); 
        }

        private void Draw(Graphics g)
        {
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

            //front door -> Door1
            g.DrawRectangle(BlackPen, door1X + aDoor1, door1Y + bDoor1, door_width + cDoor1, door_height + dDoor1);

            //back door -> Door2
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
            g.DrawRectangle(BlackPen, ShowerX, y + ShowerY + length * 3, shower_width, shower_height);

            #endregion

            //Inner signalization 
            #region Inner cyclus signalization 

            //position line  
            g.DrawLine(BlackPen, x + length * 5, y + length * 2, x + length * 9, y + length * 2);
            g.DrawEllipse(BlackPen, ShowerX, y + ShowerY + length * 2 - (signalizationCircle_diameter / 2), signalizationCircle_diameter, signalizationCircle_diameter);

            //car signalization 
            string labelCarSignalization = ""; 
            CarSignalizationX = x + length * 7 + 20;
            CarSignalizationY = y + length * 2 + length / 2;
            g.DrawString(labelCarSignalization, labelFont, labelBrush, CarSignalizationX, CarSignalizationY); 
            g.DrawEllipse(BlackPen, CarSignalizationX - 15, CarSignalizationY, signalizationCircle_diameter, signalizationCircle_diameter); 

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

            //Varnish protection 
            string labelVarnishProtection = "Varnish protection";
            VarnishProtectionX = x + length * 4 + 20;
            VarnishProtectionY = y + length + 30;
            g.DrawString(labelVarnishProtection, labelFont, labelBrush, VarnishProtectionX, VarnishProtectionY);
            g.DrawEllipse(BlackPen, VarnishProtectionX - 15, VarnishProtectionY, signalizationCircle_diameter, signalizationCircle_diameter);

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

            //Draw signalization based on value 
            #region Draw signalization based on value

            //Car Signalization Lights and texts 
            #region Car Signalization Lights and texts 
            if (CarWashRedLight)
            {
                g.DrawString("", labelFont, labelBrush, CarSignalizationX, CarSignalizationY);
                labelCarSignalization = "STOP!";
                g.DrawString(labelCarSignalization, labelFont, labelBrush, CarSignalizationX, CarSignalizationY);
                g.FillEllipse(red, CarSignalizationX - 15, CarSignalizationY, signalizationCircle_diameter, signalizationCircle_diameter);
            }
            else
            {
                g.DrawString("", labelFont, labelBrush, CarSignalizationX, CarSignalizationY);
                g.FillEllipse(white, CarSignalizationX - 15, CarSignalizationY, signalizationCircle_diameter, signalizationCircle_diameter);
            }

            if (CarWashYellowLight)
            {
                g.DrawString("", labelFont, labelBrush, CarSignalizationX, CarSignalizationY);
                labelCarSignalization = "ERROR!";
                g.DrawString(labelCarSignalization, labelFont, labelBrush, CarSignalizationX, CarSignalizationY);
                g.FillEllipse(yellow, CarSignalizationX - 15, CarSignalizationY, signalizationCircle_diameter, signalizationCircle_diameter);
            }
            else
            {
                g.DrawString("", labelFont, labelBrush, CarSignalizationX, CarSignalizationY);
                g.FillEllipse(white, CarSignalizationX - 15, CarSignalizationY, signalizationCircle_diameter, signalizationCircle_diameter);
            }

            if (CarWashGreenLight)
            {
                g.DrawString("", labelFont, labelBrush, CarSignalizationX, CarSignalizationY);
                labelCarSignalization = "GO!";
                g.DrawString(labelCarSignalization, labelFont, labelBrush, CarSignalizationX, CarSignalizationY);
                g.FillEllipse(green, CarSignalizationX - 15, CarSignalizationY, signalizationCircle_diameter, signalizationCircle_diameter);
            }
            else
            {
                g.DrawString("", labelFont, labelBrush, CarSignalizationX, CarSignalizationY);
                g.FillEllipse(white, CarSignalizationX - 15, CarSignalizationY, signalizationCircle_diameter, signalizationCircle_diameter);
            }

            #endregion

            //draw PreWash signalization
            if (PreWash)
            {
                g.FillEllipse(blue, PreWashX - 15, PreWashY, signalizationCircle_diameter, signalizationCircle_diameter);
            }
            else
            {
                g.FillEllipse(white, PreWashX - 15, PreWashY, signalizationCircle_diameter, signalizationCircle_diameter);
            }

            //draw Water signalization
            if (Water)
            {
                g.FillEllipse(blue, WaterX - 15, WaterY, signalizationCircle_diameter, signalizationCircle_diameter);
            }
            else
            {
                g.FillEllipse(white, WaterX - 15, WaterY, signalizationCircle_diameter, signalizationCircle_diameter);
            }

            //draw Wax signalization
            if (Wax)
            {
                g.FillEllipse(yellow, WaxX - 15, WaxY, signalizationCircle_diameter, signalizationCircle_diameter);
            }
            else
            {
                g.FillEllipse(white, WaxX - 15, WaxY, signalizationCircle_diameter, signalizationCircle_diameter);
            }

            //draw ActiveFoam signalization
            if (ActiveFoam)
            {
                g.FillEllipse(purple, ActiveFoamX - 15, ActiveFoamY, signalizationCircle_diameter, signalizationCircle_diameter);
            }
            else
            {
                g.FillEllipse(white, ActiveFoamX - 15, ActiveFoamY, signalizationCircle_diameter, signalizationCircle_diameter);
            }

            //draw Soap signalization
            if (Soap)
            {
                g.FillEllipse(green, SoapX - 15, SoapY, signalizationCircle_diameter, signalizationCircle_diameter);
            }
            else
            {
                g.FillEllipse(white, SoapX - 15, SoapY, signalizationCircle_diameter, signalizationCircle_diameter);
            }

            //draw Drying signalization
            if (Drying)
            {
                g.FillEllipse(brown, DryingX - 15, DryingY, signalizationCircle_diameter, signalizationCircle_diameter);
            }
            else
            {
                g.FillEllipse(white, DryingX - 15, DryingY, signalizationCircle_diameter, signalizationCircle_diameter);
            }

            //draw Brushes signalization
            if (Brushes)
            {
                g.FillEllipse(red, BrushesX - 15, BrushesY, signalizationCircle_diameter, signalizationCircle_diameter);
            }
            else
            {
                g.FillEllipse(white, BrushesX - 15, BrushesY, signalizationCircle_diameter, signalizationCircle_diameter);
            }

            //draw VarnishProtection signalization
            if (VarnishProtection)
            {
                g.FillEllipse(purple, VarnishProtectionX - 15, VarnishProtectionY, signalizationCircle_diameter, signalizationCircle_diameter);
            }
            else
            {
                g.FillEllipse(white, VarnishProtectionX - 15, VarnishProtectionY, signalizationCircle_diameter, signalizationCircle_diameter);
            }

            #endregion            
        }

        //Methods for reaction on Tia variable change 
        #region Methods for reaction on Tia variable change 

        //Door movement
        #region Door movement
        public async void door1UP(int time) 
        {
            int realTime = 4000;
            
            int totalSteps = Convert.ToInt32(length / step);
            int delayBetweenSteps = realTime / totalSteps;

            for (int i = 0; i < totalSteps; i++)
            {
                if (bDoor1 >= -90)
                {
                    bDoor1 -= step;
                    this.Refresh();
                    await Task.Delay(delayBetweenSteps);
                }
                else
                {
                    break;
                }
            }
        }

        public async void door1DOWN(int time)
        {
            int realTime = 4000;

            int totalSteps = Convert.ToInt32(length / step);
            int delayBetweenSteps = realTime / totalSteps;

            for (int i = 0; i < totalSteps; i++)
            {
                if (bDoor1 <= -10)
                {
                    bDoor1 += step;
                    await Task.Delay(delayBetweenSteps);
                    this.Refresh();
                }
                else
                {
                    break;
                }
            }
        }

        public async void door2UP(int time)
        {
            int realTime = 4000;

            int totalSteps = Convert.ToInt32(length / step);
            int delayBetweenSteps = realTime / totalSteps;
                        
            for (int i = 0; i < totalSteps; i++)
            {
                
                if (bDoor2 >= -90)
                {
                    bDoor2 -= step;
                    this.Refresh();
                    await Task.Delay(delayBetweenSteps);
                }
                else
                {
                    break;
                }
            }           
        }

        public async void door2DOWN(int time)
        { 
            int realTime = 4000;

            int totalSteps = Convert.ToInt32(length / step);
            int delayBetweenSteps = realTime / totalSteps;

            for (int i = 0; i < totalSteps; i++)
            {
                if (bDoor2 <= -10)
                {
                    bDoor2 += step;
                    await Task.Delay(delayBetweenSteps);
                    this.Refresh();
                }
                else
                {
                    break;
                }
            }
        }

        #endregion

        //Shower movement
        #region Shower movement

        //await userControlCarWash1.ShowerMovement(705, 915, 2000);
        //await userControlCarWash1.ShowerMovement(915, 515, 2000);
        //await userControlCarWash1.ShowerMovement(515, 705, 2000);

        public async Task ShowerMovement(int startX, int endX, int duration)
        {
            int distance = Math.Abs(startX - endX);

            int stepSize = 5;

            int steps = distance / stepSize;

            int stepDelay = distance / steps; 

            bool moveRight = endX > startX;

            for (int i = 0; i < steps; i++)
            {
                if ((moveRight && ShowerX >= endX) || (!moveRight && ShowerX <= endX))
                {
                    break;
                }
                else
                {
                    if (moveRight)
                    {
                        ShowerX += stepSize;
                        await Task.Delay(stepDelay);
                        this.Refresh();
                    }
                    else
                    {
                        ShowerX -= stepSize;
                        await Task.Delay(stepDelay);
                        this.Refresh();
                    }                   
                }
            }
        }

        //ready for manual control

        public async void ShowerMoveLeft() 
        {
            for (int i = 0; i <= Convert.ToInt32(length * 2); i += Convert.ToInt32(length) / 10)
            {
                ShowerX -= step;
                this.Refresh();
                await Task.Delay(Convert.ToInt32(TimeMovement));
            }
        }

        public async void ShowerMoveRight()
        {
            for (int i = 0; i <= Convert.ToInt32(length * 2); i += Convert.ToInt32(length) / 10)
            {
                ShowerX += step;
                this.Refresh();
                await Task.Delay(Convert.ToInt32(TimeMovement));
            }
        }

        #endregion

        //Car picture
        #region Car picture

        public void InitializeCarImage(int index)
        {
            if (pictureBoxCar != null && !pictureBoxCar.IsDisposed)
            {
                Controls.Remove(pictureBoxCar);
                pictureBoxCar.Dispose();
            }

            pictureBoxCar = new PictureBox();

            //moving car 
            if (picture == 1)
            {
                pictureBoxCar.Image = Image.FromFile("C:\\Users\\lukas\\OneDrive\\Dokumenty\\VŠ\\Bc_prace\\Bc\\C#\\final\\JAN0837_Bc_prace\\Bc_prace\\Resources\\car_64.png");
            }
            //brushes
            else if (picture == 2)
            {
                pictureBoxCar.Image = Image.FromFile("C:\\Users\\lukas\\OneDrive\\Dokumenty\\VŠ\\Bc_prace\\Bc\\C#\\final\\JAN0837_Bc_prace\\Bc_prace\\Resources\\car_brushes_64.png");
            }
            //washing
            else if (picture == 3)
            {
                pictureBoxCar.Image = Image.FromFile("C:\\Users\\lukas\\OneDrive\\Dokumenty\\VŠ\\Bc_prace\\Bc\\C#\\final\\JAN0837_Bc_prace\\Bc_prace\\Resources\\car_washing_64.png");
            }
            //shinny car 
            else if (picture == 4)
            {  
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

        public async void MoveCarToNextPoint(int point) 
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
                        await Task.Delay(Convert.ToInt32(TimeMovement));
                    }

                    break;
                case 2: // move to PreWash

                    targetX = length * 3 - 10;

                    while (pictureBoxCar.Location.X < targetX)
                    {
                        pictureX += 10;
                        pictureBoxCar.Location = new Point(Convert.ToInt32(x + pictureX), Convert.ToInt32(length * 3 + (length / 2) + pictureY));
                        this.Refresh();
                        await Task.Delay(Convert.ToInt32(TimeMovement));
                    }

                    break;

                case 3: // move to CarPosition from PreWash

                    targetX = length * 7 - 64;

                    while (pictureBoxCar.Location.X < targetX)
                    {
                        pictureX += 10;
                        pictureBoxCar.Location = new Point(Convert.ToInt32(x + pictureX), Convert.ToInt32(length * 3 + (length / 2) + pictureY));
                        this.Refresh();
                        await Task.Delay(Convert.ToInt32(TimeMovement));
                    }

                    if (OnCarWashPositionCar != null)
                    {
                        OnCarWashPositionCar(true);
                    }

                    break;
                case 4: // move out 

                    targetX = length * 12 - 64;

                    while (pictureBoxCar.Location.X < targetX)
                    {
                        pictureX += 10;
                        pictureBoxCar.Location = new Point(Convert.ToInt32(x + pictureX), Convert.ToInt32(length * 3 + (length / 2) + pictureY));
                        this.Refresh();
                        await Task.Delay(Convert.ToInt32(TimeMovement));
                    }

                    break;
            }
        }

        public void ManualMovePictureLEFT()
        {
            pictureX -= 10;
            pictureBoxCar.Location = new Point(Convert.ToInt32(x + pictureX), Convert.ToInt32(length * 3 + (length / 2) + pictureY));
            this.Refresh();
            InitializeCarImage(1);
        }

        public void ManualMovePictureRIGHT()
        {
            pictureX += 10;
            pictureBoxCar.Location = new Point(Convert.ToInt32(x + pictureX), Convert.ToInt32(length * 3 + (length / 2) + pictureY));
            this.Refresh();
            InitializeCarImage(1);
        }

        #endregion

        #endregion
    }
}
