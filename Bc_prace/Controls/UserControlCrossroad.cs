using Bc_prace.Controls.MyGraphControl.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Bc_prace.Controls
{
    public partial class UserControlCrossroad : UserControl
    {
        //Variables
        #region Variables

        //beggining points of drawing
        private float x = 15;
        private float y = 15;

        //basic parametres
        private float length = 80;
        //private float length_gap = 10; //nebudeme řešit :)
        private float length_interrupted = 20;
        private float TrafficLights_height = 60;
        private float TrafficLights_width = 20;
        private float crosswalk_width = 20;
        private float crosswalk_height = 60;
        private float TrafficLightsCrosswalk_width = 20;
        private float TrafficLightsCrosswalk_height = 40;
        private float FreeSpace = 5;

        private SolidBrush green = new SolidBrush(Color.Green);
        private SolidBrush yellow = new SolidBrush(Color.Yellow);
        private SolidBrush red = new SolidBrush(Color.Red);
        private SolidBrush white = new SolidBrush(Color.White); 
                
        #endregion


        public UserControlCrossroad()
        {
            InitializeComponent();
            DoubleBuffered = true;
            Paint += UserControl1_Paint;         

        }

        private void UserControl1_Paint(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            //background
            g.Clear(Color.Black);

            //pen color
            Pen BlackPen = new Pen(Color.Black, 2);
            Pen WhitePen = new Pen(Color.White, 2);
            Pen RedPen = new Pen(Color.Red, 2);
            Pen GreenPen = new Pen (Color.Green, 2);
            Pen YellowPen = new Pen(Color.Yellow, 2);

            #region Původní nástavba 
            /*
            //BasicCrossroad();

            //crossroad - left up corner
            //vertical line
            g.DrawLine(WhitePen, x + length * 3, y, x + length * 3, y + length);
            g.DrawLine(WhitePen, x + length * 3, y + length, x + length * 3, y + length * 2);
            g.DrawLine(WhitePen, x + length * 3, y + length * 2, x + length * 3, y + length * 3);
            //horizontal line
            g.DrawLine(WhitePen, x, y + length * 3, x + length, y + length * 3);
            g.DrawLine(WhitePen, x + length, y + length * 3, x + length * 2, y + length * 3);
            g.DrawLine(WhitePen, x + length * 2, y + length * 3, x + length * 3, y + length * 3);

            //white line - crossorad1 - left - internal
            //g.DrawLine(WhitePen, x + length * 3, y + length * 3, x + length * 3, y + length * 4); //up
            //g.DrawLine(WhitePen, x + length * 3, y + length * 4, x + length * 3, y + length * 5); //down
            //white line - crossroad1 - left - external
            //g.DrawLine(WhitePen, x + length * 2, y + length * 3, x + length * 2, y + length * 4); //up
            g.DrawLine(WhitePen, x + length * 2, y + length * 4, x + length * 2, y + length * 5); //down
                        
            //crossroad - left down corner
            //horizontal line
            g.DrawLine(WhitePen, x, y + length * 5, x + length, y + length * 5);
            g.DrawLine(WhitePen, x + length, y + length * 5, x + length * 2, y + length * 5);
            g.DrawLine(WhitePen, x + length * 2, y + length * 5, x + length * 3, y + length * 5);
            //vertical line
            g.DrawLine(WhitePen, x + length * 3, y + length * 5, x + length * 3, y + length * 6);
            g.DrawLine(WhitePen, x + length * 3, y + length * 6, x + length * 3, y + length * 7);
            g.DrawLine(WhitePen, x + length * 3, y + length * 7, x + length * 3, y + length * 8);
            g.DrawLine(WhitePen, x + length * 3, y + length * 8, x + length * 3, y + length * 9);

            //white line - crossroad1 - top
            //internal
            //g.DrawLine(WhitePen, x + length * 3, y + length * 3, x + length * 4, y + length * 3); //left
            //g.DrawLine(WhitePen, x + length * 4, y + length * 3, x + length * 5, y + length * 3); //right
            //exteral
            g.DrawLine(WhitePen, x + length * 3, y + length * 2, x + length * 4, y + length * 2); //left
            //g.DrawLine(WhitePen, x + length * 4, y + length * 2, x + length * 5, y + length * 2); //right
            
            //crossroad - mid up - left
            g.DrawLine(WhitePen, x + length * 5, y, x + length * 5, y + length);
            g.DrawLine(WhitePen, x + length * 5, y + length, x + length * 5, y + length * 2);
            g.DrawLine(WhitePen, x + length * 5, y + length * 2, x + length * 5, y + length * 3);
             
            //crossroad - mid up - mid 
            g.DrawLine(WhitePen, x + length * 5, y + length * 3, x + length * 6, y + length * 3);
            g.DrawLine(WhitePen, x + length * 6, y + length * 3, x + length * 7, y + length * 3);
            g.DrawLine(WhitePen, x + length * 7, y + length * 3, x + length * 8, y + length * 3);
            g.DrawLine(WhitePen, x + length * 8, y + length * 3, x + length * 9, y + length * 3);
            //jednoduše
            //g.DrawLine(WhitePen, x + length * 5, y + length * 3, x + length * 9, y + length * 3);

            //crossroad - mid up - right 
            g.DrawLine(WhitePen, x + length * 9, y, x + length * 9, y + length);
            g.DrawLine(WhitePen, x + length * 9, y + length, x + length * 9, y + length * 2);
            g.DrawLine(WhitePen, x + length * 9, y + length * 2, x + length * 9, y + length * 3);
                       
            //white line - crossroad1 - right
            //internal line
            //g.DrawLine(WhitePen, x + length * 5, y + length * 3, x + length * 5, y + length * 4); //up
            //g.DrawLine(WhitePen, x + length * 5, y + length * 4, x + length * 5, y + length * 5); //down
            //external line
            g.DrawLine(WhitePen, x + length * 6, y + length * 3, x + length * 6, y + length * 4); //up
            //g.DrawLine(WhitePen, x + length * 6, y + length * 4, x + length * 6, y + length * 5); //down

            //crossroad - mid down - left
            g.DrawLine(WhitePen, x + length * 5, y + length * 5, x + length * 5, y + length * 6);          
            g.DrawLine(WhitePen, x + length * 5, y + length * 6, x + length * 5, y + length * 7);
            g.DrawLine(WhitePen, x + length * 5, y + length * 7, x + length * 5, y + length * 8);
            g.DrawLine(WhitePen, x + length * 5, y + length * 8, x + length * 5, y + length * 9);

            //crossroad - mid down - mid 
            g.DrawLine(WhitePen, x + length * 5, y + length * 5, x + length * 6, y + length * 5);
            g.DrawLine(WhitePen, x + length * 6, y + length * 5, x + length * 7, y + length * 5);
            g.DrawLine(WhitePen, x + length * 7, y + length * 5, x + length * 8, y + length * 5);
            g.DrawLine(WhitePen, x + length * 8, y + length * 5, x + length * 9, y + length * 5);

            //crossroad - mid down - right
            g.DrawLine(WhitePen, x + length * 9, y + length * 5, x + length * 9, y + length * 6);
            g.DrawLine(WhitePen, x + length * 9, y + length * 6, x + length * 9, y + length * 7);
            g.DrawLine(WhitePen, x + length * 9, y + length * 7, x + length * 9, y + length * 8);
            g.DrawLine(WhitePen, x + length * 9, y + length * 8, x + length * 9, y + length * 9);

            //white line - crossroad1 - bottom
            //internal
            //g.DrawLine(WhitePen, x + length * 3, y + length * 5, x + length * 4, y + length * 5); //left
            //g.DrawLine(WhitePen, x + length * 4, y + length * 5, x + length * 5, y + length * 5); //right
            //external
            //g.DrawLine(WhitePen, x + length * 3, y + length * 6, x + length * 4, y + length * 6); //left
            g.DrawLine(WhitePen, x + length * 4, y + length * 6, x + length * 5, y + length * 6); //right

            //white line - crossroad2 - left
            //exteranl
            //g.DrawLine(WhitePen, x + length * 8, y + length * 3, x + length * 8, y + length * 4); //up
            g.DrawLine(WhitePen, x + length * 8, y + length * 4, x + length * 8, y + length * 5); //down
            //internal
            //g.DrawLine(WhitePen, x + length * 9, y + length * 3, x + length * 9, y + length * 4); //up
            //g.DrawLine(WhitePen, x + length * 9, y + length * 4, x + length * 9, y + length * 5); //down 

            //crossroad - right up corner
            //vertical line
            g.DrawLine(WhitePen, x + length * 11, y, x + length * 11, y + length);
            g.DrawLine(WhitePen, x + length * 11, y + length, x + length * 11, y + length * 2);
            g.DrawLine(WhitePen, x + length * 11, y + length * 2, x + length * 11, y + length * 3);
            //horizontal line
            g.DrawLine(WhitePen, x + length * 11, y + length * 3, x + length * 12, y + length * 3);
            g.DrawLine(WhitePen, x + length * 12, y + length * 3, x + length * 13, y + length * 3);
            g.DrawLine(WhitePen, x + length * 13, y + length * 3, x + length * 14, y + length * 3);

            //white line - crossroad2 - top
            //external
            g.DrawLine(WhitePen, x + length * 9, y + length * 2, x + length * 10, y + length * 2); //left
            //g.DrawLine(WhitePen, x + length * 10, y + length * 2, x + length * 11, y + length * 2); //right
            //internal
            //g.DrawLine(WhitePen, x + length * 9, y + length * 3, x + length * 10, y + length * 3); //left
            //g.DrawLine(WhitePen, x + length * 10, y + length * 3, x + length * 11, y + length * 3); //right

            //crossroad - right down corner
            //vertical line
            g.DrawLine(WhitePen, x + length * 11, y + length * 5, x + length * 11, y + length * 6);
            g.DrawLine(WhitePen, x + length * 11, y + length * 6, x + length * 11, y + length * 7);
            g.DrawLine(WhitePen, x + length * 11, y + length * 7, x + length * 11, y + length * 8);
            g.DrawLine(WhitePen, x + length * 11, y + length * 8, x + length * 11, y + length * 9);
            //horizontal line
            g.DrawLine(WhitePen, x + length * 11, y + length * 5, x + length * 12, y + length * 5);
            g.DrawLine(WhitePen, x + length * 12, y + length * 5, x + length * 13, y + length * 5);
            g.DrawLine(WhitePen, x + length * 13, y + length * 5, x + length * 14, y + length * 5);

            //white line - crossroad2 - bottom
            //internal
            //g.DrawLine(WhitePen, x + length * 9, y + length * 5, x + length * 10, y + length * 5); //left
            //g.DrawLine(WhitePen, x + length * 10, y + length * 5, x + length * 11, y + length * 5); //right
            //external
            //g.DrawLine(WhitePen, x + length * 9, y + length * 6, x + length * 10, y + length * 6); //left
            g.DrawLine(WhitePen, x + length * 10, y + length * 6, x + length * 11, y + length * 6); //right

            //white line - crossroad2 - right
            //internal
            //g.DrawLine(WhitePen, x + length * 11, y + length * 3, x + length * 11, y + length * 4); //up
            //g.DrawLine(WhitePen, x + length * 11, y + length * 4, x + length * 11, y + length * 5); //down
            //external
            g.DrawLine(WhitePen, x + length * 12, y + length * 3, x + length * 12, y + length * 4); //up
            //g.DrawLine(WhitePen, x + length * 12, y + length * 4, x + length * 12, y + length * 5); //down
                        
            //crossroad - T - left 
            g.DrawLine(WhitePen, x, y + length * 9, x + length, y + length * 9);
            g.DrawLine(WhitePen, x + length, y + length * 9, x + length * 2, y + length * 9);
            g.DrawLine(WhitePen, x + length * 2, y + length * 9, x + length * 3, y + length * 9);

            //crossroad - T - mid 
            g.DrawLine(WhitePen, x + length * 5, y + length * 9, x + length * 6, y + length * 9);
            g.DrawLine(WhitePen, x + length * 6, y + length * 9, x + length * 7, y + length * 9);
            g.DrawLine(WhitePen, x + length * 7, y + length * 9, x + length * 8, y + length * 9);
            g.DrawLine(WhitePen, x + length * 8, y + length * 9, x + length * 9, y + length * 9);

            //crossroad - T - bottom 
            g.DrawLine(WhitePen, x, y + length * 11, x + length * 14, y + length * 11);

            //crossroad - T - right
            g.DrawLine(WhitePen, x + length * 11, y + length * 9, x + length * 12, y + length * 9);
            g.DrawLine(WhitePen, x + length * 12, y + length * 9, x + length * 13, y + length * 9);
            g.DrawLine(WhitePen, x + length * 13, y + length * 9, x + length * 14, y + length * 9);

            //pravá spojka mezi crossroad2 a T2 - čára pro auto
            g.DrawLine(WhitePen, x + length * 9, y + length * 8, x + length * 10, y + length * 8);

            //whiteline - T
            //left
            g.DrawLine(WhitePen, x + length * 2, y + length * 10, x + length * 2, y + length * 11);
            //top
            g.DrawLine(WhitePen, x + length * 3, y + length * 8, x + length * 4, y + length * 8);
            //right
            g.DrawLine(WhitePen, x + length * 6, y + length * 9, x + length * 6, y + length * 10);

            g.DrawLine(WhitePen, x + length * 8, y + length * 10, x + length * 8, y + length * 11);

            g.DrawLine(WhitePen, x + length * 12, y + length * 9, x + length * 12, y + length * 10);

            //Center lines
            #region Center lines

            //vertical center line - left
            //top
            g.DrawLine(WhitePen, x + length * 4, y, x + length * 4, y + length_interrupted);
            g.DrawLine(WhitePen, x + length * 4, y + length_interrupted * 2, x + length * 4, y + length_interrupted * 3);
            //g.DrawLine(WhitePen, x + length * 4, y + length_interrupted * 4, x + length * 4, y + length_interrupted * 5);
            //g.DrawLine(WhitePen, x + length * 4, y + length_interrupted * 6, x + length * 4, y + length_interrupted * 7);
            g.DrawLine(WhitePen, x + length * 4, y + length, x + length * 4, y + length * 2);
            //bottom
            g.DrawLine(WhitePen, x + length * 4, y + length * 6, x + length * 4, y + length * 8);

            //vertical center line - right
            g.DrawLine(WhitePen, x + length * 10, y, x + length * 10, y + length_interrupted);
            g.DrawLine(WhitePen, x + length * 10, y + length_interrupted * 2, x + length * 10, y + length_interrupted * 3);
            //g.DrawLine(WhitePen, x + length * 4, y + length_interrupted * 4, x + length * 4, y + length_interrupted * 5);
            //g.DrawLine(WhitePen, x + length * 4, y + length_interrupted * 6, x + length * 4, y + length_interrupted * 7);
            g.DrawLine(WhitePen, x + length * 10, y + length, x + length * 10, y + length * 2);
            //bottom
            g.DrawLine(WhitePen, x + length * 10, y + length * 6, x + length * 10, y + length * 8);

            //horizontal center line - top 
            g.DrawLine(WhitePen, x, y + length * 4, x + length_interrupted, y + length * 4);
            g.DrawLine(WhitePen, x + length_interrupted * 2, y + length * 4, x + length_interrupted * 3, y + length * 4);
            g.DrawLine(WhitePen, x + length, y + length * 4, x + length * 2, y + length * 4);
            g.DrawLine(WhitePen, x + length * 6, y + length * 4, x + length * 8, y + length * 4);
            g.DrawLine(WhitePen, x + length * 12, y + length * 4, x + length * 13, y + length * 4);
            g.DrawLine(WhitePen, x + length * 13 + length_interrupted, y + length * 4, x + length * 13 + length_interrupted * 2, y + length * 4);
            g.DrawLine(WhitePen, x + length * 13 + length_interrupted * 3, y + length * 4, x + length * 13 + length_interrupted * 4, y + length * 4);

            //g.DrawLine(WhitePen, x + length_interrupted * 4, y + length * 3, x + length_interrupted * 5, y + length * 3);
            //g.DrawLine(WhitePen, x + length_interrupted * 6, y + length * 3, x + length_interrupted * 7, y + length * 3);

            g.DrawLine(WhitePen, x + length, y + length * 10, x + length * 2, y + length * 10);
            g.DrawLine(WhitePen, x + length * 6, y + length * 10, x + length * 8, y + length * 10);

            g.DrawLine(WhitePen, x + length * 12, y + length * 10, x + length * 13, y + length * 10);

            //horizontal center line - bottom 
            //left T
            g.DrawLine(WhitePen, x, y + length * 10, x + length_interrupted, y + length * 10);
            g.DrawLine(WhitePen, x + length_interrupted * 2, y + length * 10, x + length_interrupted * 3, y + length * 10);
            g.DrawLine(WhitePen, x + length, y + length * 10, x + length * 2, y + length * 10);
            
            g.DrawLine(WhitePen, x + length * 6, y + length * 10, x + length * 8, y + length * 10);

            //right T 
            g.DrawLine(WhitePen, x + length * 12, y + length * 10, x + length * 13, y + length * 10);
            g.DrawLine(WhitePen, x + length * 13 + length_interrupted, y + length * 10, x + length * 13 + length_interrupted * 2, y + length * 10);
            g.DrawLine(WhitePen, x + length * 13 + length_interrupted * 3, y + length * 10, x + length * 13 + length_interrupted * 4, y + length * 10);




            #endregion

            //Crosswalks
            #region Crosswalks

            //crossroad1 
            //crossroad1 - crosswalk - top
            g.DrawRectangle(WhitePen, x + length * 3 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 3 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 3 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 3 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 3 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 3 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 4 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 4 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 4 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);

            //crossroad1 - crosswalk - left
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 3 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 3 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 3 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 4 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 4 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 4 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);

            //crossroad1 - crosswalk - right
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 3 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 3 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 3 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 4 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 4 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 4 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);

            //crossroad1 - crosswalk - bottom
            g.DrawRectangle(WhitePen, x + length * 3 + FreeSpace, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 3 + 2 * FreeSpace + crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 3 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + FreeSpace, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + 2 * FreeSpace + crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);

            //crossroad2 
            //crossroad2 - crosswalk - top
            g.DrawRectangle(WhitePen, x + length * 9 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 9 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 9 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);

            //crossroad2 - crosswalk - bottom
            g.DrawRectangle(WhitePen, x + length * 9 + FreeSpace, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 9 + 2 * FreeSpace + crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 9 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + FreeSpace, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + 2 * FreeSpace + crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);

            //crossroad2 - crosswalk - left
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 3 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 3 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 3 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 4 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 4 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 4 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);

            //crossroad2 - crosswalk - right
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 3 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 3 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 3 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 4 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 4 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 4 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);

            //left T
            //left T - crosswalk - top
            g.DrawRectangle(WhitePen, x + length * 3 + FreeSpace, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 3 + 2 * FreeSpace + crosswalk_width, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 3 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + FreeSpace, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + 2 * FreeSpace + crosswalk_width, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);

            //left T - crosswalk - left
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 9 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 9 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 9 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 10 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 10 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 10 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);

            //left T - crosswalk - right
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 9 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 9 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 9 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 10 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 10 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 10 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);

            //right T
            //right T - crosswalk - top
            g.DrawRectangle(WhitePen, x + length * 9 + FreeSpace, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 9 + 2 * FreeSpace + crosswalk_width, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 9 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + FreeSpace, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + 2 * FreeSpace + crosswalk_width, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);

            //right T - crosswalk - left
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 9 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 9 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 9 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 10 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 10 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 10 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);

            //right T - crosswalk - right
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 9 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 9 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 9 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 10 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 10 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 10 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);

            #endregion

            //TrafficLights
            #region TrafficLights

            //crossorad1
            #region Crossroad1
            //crossroad1 - trafficlight - top
            g.DrawRectangle(WhitePen, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_height);
            g.FillEllipse(red, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            //crossroad1 - trafficlight - left
            g.DrawRectangle(WhitePen, x + length + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length + 3 * FreeSpace + TrafficLights_width * 2, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length + 3 * FreeSpace + TrafficLights_width, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);

            //crossroad1 - trafficlight - right
            g.DrawRectangle(WhitePen, x + length * 6 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length * 6 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 6 + FreeSpace + TrafficLights_width, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 6 + FreeSpace + TrafficLights_width * 2, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            //crossroad1 - trafficlight - bottom
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_height);
            g.FillEllipse(red, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
            
            #endregion

            //crossroad2
            #region Crossroad2
            //crossroad2 - trafficlight - top
            g.DrawRectangle(WhitePen, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_height);
            g.FillEllipse(red, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            //crossroad2 - trafficlight - left
            g.DrawRectangle(WhitePen, x + length * 7 + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length * 7 + 3 * FreeSpace + TrafficLights_width * 2, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 7 + 3 * FreeSpace + TrafficLights_width, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 7 + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);

            //crossroad2 - trafficlight - right
            g.DrawRectangle(WhitePen, x + length * 12 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length * 12 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 12 + FreeSpace + TrafficLights_width, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 12 + FreeSpace + TrafficLights_width * 2, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            //crossroad2 - trafficlight - bottom
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_height);
            g.FillEllipse(red, x + length * 11 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 11 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 11 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);

            #endregion

            //left T
            #region Left T
            //left T - trafficlight - top
            g.DrawRectangle(WhitePen, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace, TrafficLights_width, TrafficLights_height);
            g.FillEllipse(red, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            //left T - trafficlight - left
            g.DrawRectangle(WhitePen, x + length + 3 * FreeSpace, y + length * 11 + FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length + 3 * FreeSpace + TrafficLights_width * 2, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length + 3 * FreeSpace + TrafficLights_width, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length + 3 * FreeSpace, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);

            //left T - trafficlight - right
            g.DrawRectangle(WhitePen, x + length * 6 + FreeSpace, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length * 6 + FreeSpace, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 6 + FreeSpace + TrafficLights_width, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 6 + FreeSpace + TrafficLights_width * 2, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            #endregion

            //right T
            #region Right T
            //right T - trafficlight - top
            g.DrawRectangle(WhitePen, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace, TrafficLights_width, TrafficLights_height);
            g.FillEllipse(red, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            //right T - trafficlight - left
            g.DrawRectangle(WhitePen, x + length * 7 + 3 * FreeSpace, y + length * 11 + FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length * 7 + 3 * FreeSpace + TrafficLights_width * 2, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 7 + 3 * FreeSpace + TrafficLights_width, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 7 + 3 * FreeSpace, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);

            //right T - trafficlight - right
            g.DrawRectangle(WhitePen, x + length * 12 + FreeSpace, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length * 12 + FreeSpace, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 12 + FreeSpace + TrafficLights_width, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 12 + FreeSpace + TrafficLights_width * 2, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            #endregion

            #endregion

            //CrosswalkLights
            #region CrosswalkLights 

            //crossroad1
            #region Crossroad1
            //top
            //crossroad1 - CrosswalkLights - top - left
            g.DrawRectangle(WhitePen, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad1 - CrosswalkLights - top - right
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //bottom
            //crossroad1 - CrosswalkLights - bottom - left
            g.DrawRectangle(WhitePen, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad1 - CrosswalkLights - bottom - right
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //left
            //crossroad1 - CrosswalkLights - left - top
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad1 - CrosswalkLights - left - bottom
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //right
            //crossroad1 - CrosswalkLights - right - top
            g.DrawRectangle(WhitePen, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad1 - CrosswalkLights - right - bottom
            g.DrawRectangle(WhitePen, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            #endregion

            //crossroad2
            #region Crossroad2
            //top
            //crossroad2 - CrosswalkLights - top - left
            g.DrawRectangle(WhitePen, x + length * 8 + crosswalk_width + 3 * FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 8 + crosswalk_width + 3 * FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 8 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad2 - CrosswalkLights - top - right
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 11 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 11 + FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //bottom
            //crossroad2 - CrosswalkLights - bottom - left
            g.DrawRectangle(WhitePen, x + length * 8 + crosswalk_width + 3 * FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 8 + crosswalk_width + 3 * FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 8 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad2 - CrosswalkLights - bottom - right
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 11 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 11 + FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //left
            //crossroad2 - CrosswalkLights - left - top
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 8 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 8 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad2 - CrosswalkLights - left - bottom
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 8 + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 8 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //right
            //crossroad2 - CrosswalkLights - right - top
            g.DrawRectangle(WhitePen, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad2 - CrosswalkLights - right - bottom
            g.DrawRectangle(WhitePen, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            #endregion

            //left T
            #region Left T
            //left T - CrosswalkLights - top - left
            g.DrawRectangle(WhitePen, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //left T - CrosswalkLights - top - right
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //left T - CrosswalkLights - left - top
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 2 + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //left T - CrosswalkLights - left - bottom
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + length * 11 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 2 + FreeSpace, y + length * 11 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + FreeSpace, y + length * 11 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //left T - CrosswalkLights - right - top 
            g.DrawRectangle(WhitePen, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //left T - CrosswalkLights - right - bottom
            g.DrawRectangle(WhitePen, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 11 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 11 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 11 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            #endregion

            //right T
            #region Right T
            //right T - CrosswalkLights - top - left
            g.DrawRectangle(WhitePen, x + length * 8 + crosswalk_width + 3 * FreeSpace, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 8 + crosswalk_width + 3 * FreeSpace, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 8 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //right T - CrosswalkLights - top - right
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 11 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 11 + FreeSpace, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //right T - CrosswalkLights - left - top
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 8 + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 8 + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //right T - CrosswalkLights - left - botom
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + length * 11 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 8 + FreeSpace, y + length * 11 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 8 + FreeSpace, y + length * 11 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //right T - CrosswalkLights - right - top
            g.DrawRectangle(WhitePen, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //right T - CrosswalkLights - right - bottom
            g.DrawRectangle(WhitePen, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 11 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 11 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 11 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            #endregion


            #endregion

            */
            #endregion
        }

        //Methods for rendering crossroad
        #region Methods for rendering crossroad

        //Crossroad1
        public void BasicCrossroad()
        {
            var g = this.CreateGraphics();

            //background
            //g.Clear(Color.Black);

            //pen color
            Pen BlackPen = new Pen(Color.Black, 2);
            Pen WhitePen = new Pen(Color.White, 2);
            Pen RedPen = new Pen(Color.Red, 2);
            Pen GreenPen = new Pen(Color.Green, 2);
            Pen YellowPen = new Pen(Color.Yellow, 2);

            this.Refresh();

            //Traffic lines
            #region Traffic lines

            //crossroad1
            #region crossroad1

            #region Corners
            //left top corner 

            //right top corner

            //left bottom corner

            //right botoom corner 

            #endregion

            #region whitelines / traffic lines 

            //top 
                        
            //bottom

            //left 
            
            //right

            #endregion

            #endregion


            //crossroad - left up corner
            //vertical line
            g.DrawLine(WhitePen, x + length * 3, y, x + length * 3, y + length);
            g.DrawLine(WhitePen, x + length * 3, y + length, x + length * 3, y + length * 2);
            g.DrawLine(WhitePen, x + length * 3, y + length * 2, x + length * 3, y + length * 3);
            //horizontal line
            g.DrawLine(WhitePen, x, y + length * 3, x + length, y + length * 3);
            g.DrawLine(WhitePen, x + length, y + length * 3, x + length * 2, y + length * 3);
            g.DrawLine(WhitePen, x + length * 2, y + length * 3, x + length * 3, y + length * 3);

            //white line - crossorad1 - left - internal
            //g.DrawLine(WhitePen, x + length * 3, y + length * 3, x + length * 3, y + length * 4); //up
            //g.DrawLine(WhitePen, x + length * 3, y + length * 4, x + length * 3, y + length * 5); //down
            //white line - crossroad1 - left - external
            //g.DrawLine(WhitePen, x + length * 2, y + length * 3, x + length * 2, y + length * 4); //up
            g.DrawLine(WhitePen, x + length * 2, y + length * 4, x + length * 2, y + length * 5); //down

            //crossroad - left down corner
            //horizontal line
            g.DrawLine(WhitePen, x, y + length * 5, x + length, y + length * 5);
            g.DrawLine(WhitePen, x + length, y + length * 5, x + length * 2, y + length * 5);
            g.DrawLine(WhitePen, x + length * 2, y + length * 5, x + length * 3, y + length * 5);
            //vertical line
            g.DrawLine(WhitePen, x + length * 3, y + length * 5, x + length * 3, y + length * 6);
            g.DrawLine(WhitePen, x + length * 3, y + length * 6, x + length * 3, y + length * 7);
            g.DrawLine(WhitePen, x + length * 3, y + length * 7, x + length * 3, y + length * 8);
            g.DrawLine(WhitePen, x + length * 3, y + length * 8, x + length * 3, y + length * 9);

            //white line - crossroad1 - top
            //internal
            //g.DrawLine(WhitePen, x + length * 3, y + length * 3, x + length * 4, y + length * 3); //left
            //g.DrawLine(WhitePen, x + length * 4, y + length * 3, x + length * 5, y + length * 3); //right
            //exteral
            g.DrawLine(WhitePen, x + length * 3, y + length * 2, x + length * 4, y + length * 2); //left
            //g.DrawLine(WhitePen, x + length * 4, y + length * 2, x + length * 5, y + length * 2); //right

            //crossroad - mid up - left
            g.DrawLine(WhitePen, x + length * 5, y, x + length * 5, y + length);
            g.DrawLine(WhitePen, x + length * 5, y + length, x + length * 5, y + length * 2);
            g.DrawLine(WhitePen, x + length * 5, y + length * 2, x + length * 5, y + length * 3);

            //crossroad - mid up - mid 
            g.DrawLine(WhitePen, x + length * 5, y + length * 3, x + length * 6, y + length * 3);
            g.DrawLine(WhitePen, x + length * 6, y + length * 3, x + length * 7, y + length * 3);
            g.DrawLine(WhitePen, x + length * 7, y + length * 3, x + length * 8, y + length * 3);
            g.DrawLine(WhitePen, x + length * 8, y + length * 3, x + length * 9, y + length * 3);
            //jednoduše
            //g.DrawLine(WhitePen, x + length * 5, y + length * 3, x + length * 9, y + length * 3);
                        
            //white line - crossroad1 - right
            //internal line
            //g.DrawLine(WhitePen, x + length * 5, y + length * 3, x + length * 5, y + length * 4); //up
            //g.DrawLine(WhitePen, x + length * 5, y + length * 4, x + length * 5, y + length * 5); //down
            //external line
            g.DrawLine(WhitePen, x + length * 6, y + length * 3, x + length * 6, y + length * 4); //up
            //g.DrawLine(WhitePen, x + length * 6, y + length * 4, x + length * 6, y + length * 5); //down

            //crossroad - mid down - left
            g.DrawLine(WhitePen, x + length * 5, y + length * 5, x + length * 5, y + length * 6);
            g.DrawLine(WhitePen, x + length * 5, y + length * 6, x + length * 5, y + length * 7);
            g.DrawLine(WhitePen, x + length * 5, y + length * 7, x + length * 5, y + length * 8);
            g.DrawLine(WhitePen, x + length * 5, y + length * 8, x + length * 5, y + length * 9);

            //crossroad - mid down - mid 
            g.DrawLine(WhitePen, x + length * 5, y + length * 5, x + length * 6, y + length * 5);
            g.DrawLine(WhitePen, x + length * 6, y + length * 5, x + length * 7, y + length * 5);
            g.DrawLine(WhitePen, x + length * 7, y + length * 5, x + length * 8, y + length * 5);
            g.DrawLine(WhitePen, x + length * 8, y + length * 5, x + length * 9, y + length * 5);
                     
            //white line - crossroad1 - bottom
            //internal
            //g.DrawLine(WhitePen, x + length * 3, y + length * 5, x + length * 4, y + length * 5); //left
            //g.DrawLine(WhitePen, x + length * 4, y + length * 5, x + length * 5, y + length * 5); //right
            //external
            //g.DrawLine(WhitePen, x + length * 3, y + length * 6, x + length * 4, y + length * 6); //left
            g.DrawLine(WhitePen, x + length * 4, y + length * 6, x + length * 5, y + length * 6); //right      

            #endregion

            //Center lines
            #region Center lines

            //vertical center line - left
            //top
            g.DrawLine(WhitePen, x + length * 4, y, x + length * 4, y + length_interrupted);
            g.DrawLine(WhitePen, x + length * 4, y + length_interrupted * 2, x + length * 4, y + length_interrupted * 3);
            //g.DrawLine(WhitePen, x + length * 4, y + length_interrupted * 4, x + length * 4, y + length_interrupted * 5);
            //g.DrawLine(WhitePen, x + length * 4, y + length_interrupted * 6, x + length * 4, y + length_interrupted * 7);
            g.DrawLine(WhitePen, x + length * 4, y + length, x + length * 4, y + length * 2);
            //bottom
            g.DrawLine(WhitePen, x + length * 4, y + length * 6, x + length * 4, y + length * 8);
                        
            //horizontal center line - top 
            g.DrawLine(WhitePen, x, y + length * 4, x + length_interrupted, y + length * 4);
            g.DrawLine(WhitePen, x + length_interrupted * 2, y + length * 4, x + length_interrupted * 3, y + length * 4);
            g.DrawLine(WhitePen, x + length, y + length * 4, x + length * 2, y + length * 4);
            g.DrawLine(WhitePen, x + length * 6, y + length * 4, x + length * 8, y + length * 4);
            //g.DrawLine(WhitePen, x + length * 12, y + length * 4, x + length * 13, y + length * 4);
            //g.DrawLine(WhitePen, x + length * 13 + length_interrupted, y + length * 4, x + length * 13 + length_interrupted * 2, y + length * 4);
            //g.DrawLine(WhitePen, x + length * 13 + length_interrupted * 3, y + length * 4, x + length * 13 + length_interrupted * 4, y + length * 4);

            //g.DrawLine(WhitePen, x + length_interrupted * 4, y + length * 3, x + length_interrupted * 5, y + length * 3);
            //g.DrawLine(WhitePen, x + length_interrupted * 6, y + length * 3, x + length_interrupted * 7, y + length * 3);
                                  
            #endregion

            //Crosswalks
            #region Crosswalks

            //crossroad1 
            #region Crossroad 1
            //crossroad1 - crosswalk - top
            g.DrawRectangle(WhitePen, x + length * 3 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 3 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 3 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 3 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 3 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 3 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 4 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 4 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 4 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);

            //crossroad1 - crosswalk - left
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 3 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 3 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 3 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 4 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 4 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 4 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);

            //crossroad1 - crosswalk - right
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 3 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 3 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 3 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 4 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 4 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 4 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);

            //crossroad1 - crosswalk - bottom
            g.DrawRectangle(WhitePen, x + length * 3 + FreeSpace, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 3 + 2 * FreeSpace + crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 3 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + FreeSpace, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + 2 * FreeSpace + crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);

            #endregion
                        
            #endregion

            //TrafficLights
            #region TrafficLights

            //crossorad1
            #region Crossroad1
            //crossroad1 - trafficlight - top
            g.DrawRectangle(WhitePen, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_height);
            g.FillEllipse(red, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            //crossroad1 - trafficlight - left
            g.DrawRectangle(WhitePen, x + length + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length + 3 * FreeSpace + TrafficLights_width * 2, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length + 3 * FreeSpace + TrafficLights_width, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);

            //crossroad1 - trafficlight - right
            g.DrawRectangle(WhitePen, x + length * 6 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length * 6 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 6 + FreeSpace + TrafficLights_width, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 6 + FreeSpace + TrafficLights_width * 2, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            //crossroad1 - trafficlight - bottom
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_height);
            g.FillEllipse(red, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);

            #endregion
                       
            #endregion

            //CrosswalkLights
            #region CrosswalkLights 

            //crossroad1
            #region Crossroad1
            //top
            //crossroad1 - CrosswalkLights - top - left
            g.DrawRectangle(WhitePen, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad1 - CrosswalkLights - top - right
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //bottom
            //crossroad1 - CrosswalkLights - bottom - left
            g.DrawRectangle(WhitePen, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad1 - CrosswalkLights - bottom - right
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //left
            //crossroad1 - CrosswalkLights - left - top
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad1 - CrosswalkLights - left - bottom
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //right
            //crossroad1 - CrosswalkLights - right - top
            g.DrawRectangle(WhitePen, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad1 - CrosswalkLights - right - bottom
            g.DrawRectangle(WhitePen, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            #endregion
                       
            #endregion
                        
        }

        //Crossroad1 + Crossorad2
        public void CrossroadExtension1()
        {
            var g = this.CreateGraphics();
            
            //background
            //g.Clear(Color.Black);

            //pen color
            Pen BlackPen = new Pen(Color.Black, 2);
            Pen WhitePen = new Pen(Color.White, 2);
            Pen RedPen = new Pen(Color.Red, 2);
            Pen GreenPen = new Pen(Color.Green, 2);
            Pen YellowPen = new Pen(Color.Yellow, 2);

            this.Refresh();

            //Traffic lines 
            #region Traffic lines

            //crossroad1
            #region crossroad1

            #region Corners
            //left top corner 

            //right top corner

            //left bottom corner

            //right botoom corner 

            #endregion

            #region whitelines / traffic lines 

            //top 

            //bottom

            //left 

            //right

            #endregion

            #endregion

            //crossorad2
            #region crossroad2 

            #region Corners
            //left top corner 

            //right top corner

            //left bottom corner

            //right botoom corner 

            #endregion

            #region whitelines / traffic lines 

            //top 

            //bottom

            //left 

            //right

            #endregion

            #endregion



            //crossroad - left up corner
            //vertical line
            g.DrawLine(WhitePen, x + length * 3, y, x + length * 3, y + length);
            g.DrawLine(WhitePen, x + length * 3, y + length, x + length * 3, y + length * 2);
            g.DrawLine(WhitePen, x + length * 3, y + length * 2, x + length * 3, y + length * 3);
            //horizontal line
            g.DrawLine(WhitePen, x, y + length * 3, x + length, y + length * 3);
            g.DrawLine(WhitePen, x + length, y + length * 3, x + length * 2, y + length * 3);
            g.DrawLine(WhitePen, x + length * 2, y + length * 3, x + length * 3, y + length * 3);

            //white line - crossorad1 - left - internal
            //g.DrawLine(WhitePen, x + length * 3, y + length * 3, x + length * 3, y + length * 4); //up
            //g.DrawLine(WhitePen, x + length * 3, y + length * 4, x + length * 3, y + length * 5); //down
            //white line - crossroad1 - left - external
            //g.DrawLine(WhitePen, x + length * 2, y + length * 3, x + length * 2, y + length * 4); //up
            g.DrawLine(WhitePen, x + length * 2, y + length * 4, x + length * 2, y + length * 5); //down

            //crossroad - left down corner
            //horizontal line
            g.DrawLine(WhitePen, x, y + length * 5, x + length, y + length * 5);
            g.DrawLine(WhitePen, x + length, y + length * 5, x + length * 2, y + length * 5);
            g.DrawLine(WhitePen, x + length * 2, y + length * 5, x + length * 3, y + length * 5);
            //vertical line
            g.DrawLine(WhitePen, x + length * 3, y + length * 5, x + length * 3, y + length * 6);
            g.DrawLine(WhitePen, x + length * 3, y + length * 6, x + length * 3, y + length * 7);
            g.DrawLine(WhitePen, x + length * 3, y + length * 7, x + length * 3, y + length * 8);
            g.DrawLine(WhitePen, x + length * 3, y + length * 8, x + length * 3, y + length * 9);

            //white line - crossroad1 - top
            //internal
            //g.DrawLine(WhitePen, x + length * 3, y + length * 3, x + length * 4, y + length * 3); //left
            //g.DrawLine(WhitePen, x + length * 4, y + length * 3, x + length * 5, y + length * 3); //right
            //exteral
            g.DrawLine(WhitePen, x + length * 3, y + length * 2, x + length * 4, y + length * 2); //left
            //g.DrawLine(WhitePen, x + length * 4, y + length * 2, x + length * 5, y + length * 2); //right

            //crossroad - mid up - left
            g.DrawLine(WhitePen, x + length * 5, y, x + length * 5, y + length);
            g.DrawLine(WhitePen, x + length * 5, y + length, x + length * 5, y + length * 2);
            g.DrawLine(WhitePen, x + length * 5, y + length * 2, x + length * 5, y + length * 3);

            //crossroad - mid up - mid 
            g.DrawLine(WhitePen, x + length * 5, y + length * 3, x + length * 6, y + length * 3);
            g.DrawLine(WhitePen, x + length * 6, y + length * 3, x + length * 7, y + length * 3);
            g.DrawLine(WhitePen, x + length * 7, y + length * 3, x + length * 8, y + length * 3);
            g.DrawLine(WhitePen, x + length * 8, y + length * 3, x + length * 9, y + length * 3);
            //jednoduše
            //g.DrawLine(WhitePen, x + length * 5, y + length * 3, x + length * 9, y + length * 3);

            //crossroad - mid up - right 
            g.DrawLine(WhitePen, x + length * 9, y, x + length * 9, y + length);
            g.DrawLine(WhitePen, x + length * 9, y + length, x + length * 9, y + length * 2);
            g.DrawLine(WhitePen, x + length * 9, y + length * 2, x + length * 9, y + length * 3);

            //white line - crossroad1 - right
            //internal line
            //g.DrawLine(WhitePen, x + length * 5, y + length * 3, x + length * 5, y + length * 4); //up
            //g.DrawLine(WhitePen, x + length * 5, y + length * 4, x + length * 5, y + length * 5); //down
            //external line
            g.DrawLine(WhitePen, x + length * 6, y + length * 3, x + length * 6, y + length * 4); //up
            //g.DrawLine(WhitePen, x + length * 6, y + length * 4, x + length * 6, y + length * 5); //down

            //crossroad - mid down - left
            g.DrawLine(WhitePen, x + length * 5, y + length * 5, x + length * 5, y + length * 6);
            g.DrawLine(WhitePen, x + length * 5, y + length * 6, x + length * 5, y + length * 7);
            g.DrawLine(WhitePen, x + length * 5, y + length * 7, x + length * 5, y + length * 8);
            g.DrawLine(WhitePen, x + length * 5, y + length * 8, x + length * 5, y + length * 9);

            //crossroad - mid down - mid 
            g.DrawLine(WhitePen, x + length * 5, y + length * 5, x + length * 6, y + length * 5);
            g.DrawLine(WhitePen, x + length * 6, y + length * 5, x + length * 7, y + length * 5);
            g.DrawLine(WhitePen, x + length * 7, y + length * 5, x + length * 8, y + length * 5);
            g.DrawLine(WhitePen, x + length * 8, y + length * 5, x + length * 9, y + length * 5);

            //crossroad - mid down - right
            g.DrawLine(WhitePen, x + length * 9, y + length * 5, x + length * 9, y + length * 6);
            g.DrawLine(WhitePen, x + length * 9, y + length * 6, x + length * 9, y + length * 7);
            g.DrawLine(WhitePen, x + length * 9, y + length * 7, x + length * 9, y + length * 8);
            g.DrawLine(WhitePen, x + length * 9, y + length * 8, x + length * 9, y + length * 9);

            //white line - crossroad1 - bottom
            //internal
            //g.DrawLine(WhitePen, x + length * 3, y + length * 5, x + length * 4, y + length * 5); //left
            //g.DrawLine(WhitePen, x + length * 4, y + length * 5, x + length * 5, y + length * 5); //right
            //external
            //g.DrawLine(WhitePen, x + length * 3, y + length * 6, x + length * 4, y + length * 6); //left
            g.DrawLine(WhitePen, x + length * 4, y + length * 6, x + length * 5, y + length * 6); //right

            //white line - crossroad2 - left
            //exteranl
            //g.DrawLine(WhitePen, x + length * 8, y + length * 3, x + length * 8, y + length * 4); //up
            g.DrawLine(WhitePen, x + length * 8, y + length * 4, x + length * 8, y + length * 5); //down
            //internal
            //g.DrawLine(WhitePen, x + length * 9, y + length * 3, x + length * 9, y + length * 4); //up
            //g.DrawLine(WhitePen, x + length * 9, y + length * 4, x + length * 9, y + length * 5); //down 

            //crossroad - right up corner
            //vertical line
            g.DrawLine(WhitePen, x + length * 11, y, x + length * 11, y + length);
            g.DrawLine(WhitePen, x + length * 11, y + length, x + length * 11, y + length * 2);
            g.DrawLine(WhitePen, x + length * 11, y + length * 2, x + length * 11, y + length * 3);
            //horizontal line
            g.DrawLine(WhitePen, x + length * 11, y + length * 3, x + length * 12, y + length * 3);
            g.DrawLine(WhitePen, x + length * 12, y + length * 3, x + length * 13, y + length * 3);
            g.DrawLine(WhitePen, x + length * 13, y + length * 3, x + length * 14, y + length * 3);

            //white line - crossroad2 - top
            //external
            g.DrawLine(WhitePen, x + length * 9, y + length * 2, x + length * 10, y + length * 2); //left
            //g.DrawLine(WhitePen, x + length * 10, y + length * 2, x + length * 11, y + length * 2); //right
            //internal
            //g.DrawLine(WhitePen, x + length * 9, y + length * 3, x + length * 10, y + length * 3); //left
            //g.DrawLine(WhitePen, x + length * 10, y + length * 3, x + length * 11, y + length * 3); //right

            //crossroad - right down corner
            //vertical line
            g.DrawLine(WhitePen, x + length * 11, y + length * 5, x + length * 11, y + length * 6);
            g.DrawLine(WhitePen, x + length * 11, y + length * 6, x + length * 11, y + length * 7);
            g.DrawLine(WhitePen, x + length * 11, y + length * 7, x + length * 11, y + length * 8);
            g.DrawLine(WhitePen, x + length * 11, y + length * 8, x + length * 11, y + length * 9);
            //horizontal line
            g.DrawLine(WhitePen, x + length * 11, y + length * 5, x + length * 12, y + length * 5);
            g.DrawLine(WhitePen, x + length * 12, y + length * 5, x + length * 13, y + length * 5);
            g.DrawLine(WhitePen, x + length * 13, y + length * 5, x + length * 14, y + length * 5);

            //white line - crossroad2 - bottom
            //internal
            //g.DrawLine(WhitePen, x + length * 9, y + length * 5, x + length * 10, y + length * 5); //left
            //g.DrawLine(WhitePen, x + length * 10, y + length * 5, x + length * 11, y + length * 5); //right
            //external
            //g.DrawLine(WhitePen, x + length * 9, y + length * 6, x + length * 10, y + length * 6); //left
            g.DrawLine(WhitePen, x + length * 10, y + length * 6, x + length * 11, y + length * 6); //right

            //white line - crossroad2 - right
            //internal
            //g.DrawLine(WhitePen, x + length * 11, y + length * 3, x + length * 11, y + length * 4); //up
            //g.DrawLine(WhitePen, x + length * 11, y + length * 4, x + length * 11, y + length * 5); //down
            //external
            g.DrawLine(WhitePen, x + length * 12, y + length * 3, x + length * 12, y + length * 4); //up
            //g.DrawLine(WhitePen, x + length * 12, y + length * 4, x + length * 12, y + length * 5); //down
                                             
            #endregion

            //Center lines
            #region Center lines

            //vertical center line - left
            //top
            g.DrawLine(WhitePen, x + length * 4, y, x + length * 4, y + length_interrupted);
            g.DrawLine(WhitePen, x + length * 4, y + length_interrupted * 2, x + length * 4, y + length_interrupted * 3);
            //g.DrawLine(WhitePen, x + length * 4, y + length_interrupted * 4, x + length * 4, y + length_interrupted * 5);
            //g.DrawLine(WhitePen, x + length * 4, y + length_interrupted * 6, x + length * 4, y + length_interrupted * 7);
            g.DrawLine(WhitePen, x + length * 4, y + length, x + length * 4, y + length * 2);
            //bottom
            g.DrawLine(WhitePen, x + length * 4, y + length * 6, x + length * 4, y + length * 8);

            //vertical center line - right
            g.DrawLine(WhitePen, x + length * 10, y, x + length * 10, y + length_interrupted);
            g.DrawLine(WhitePen, x + length * 10, y + length_interrupted * 2, x + length * 10, y + length_interrupted * 3);
            //g.DrawLine(WhitePen, x + length * 4, y + length_interrupted * 4, x + length * 4, y + length_interrupted * 5);
            //g.DrawLine(WhitePen, x + length * 4, y + length_interrupted * 6, x + length * 4, y + length_interrupted * 7);
            g.DrawLine(WhitePen, x + length * 10, y + length, x + length * 10, y + length * 2);
            //bottom
            g.DrawLine(WhitePen, x + length * 10, y + length * 6, x + length * 10, y + length * 8);

            //horizontal center line - top 
            g.DrawLine(WhitePen, x, y + length * 4, x + length_interrupted, y + length * 4);
            g.DrawLine(WhitePen, x + length_interrupted * 2, y + length * 4, x + length_interrupted * 3, y + length * 4);
            g.DrawLine(WhitePen, x + length, y + length * 4, x + length * 2, y + length * 4);
            g.DrawLine(WhitePen, x + length * 6, y + length * 4, x + length * 8, y + length * 4);
            g.DrawLine(WhitePen, x + length * 12, y + length * 4, x + length * 13, y + length * 4);
            g.DrawLine(WhitePen, x + length * 13 + length_interrupted, y + length * 4, x + length * 13 + length_interrupted * 2, y + length * 4);
            g.DrawLine(WhitePen, x + length * 13 + length_interrupted * 3, y + length * 4, x + length * 13 + length_interrupted * 4, y + length * 4);

            //g.DrawLine(WhitePen, x + length_interrupted * 4, y + length * 3, x + length_interrupted * 5, y + length * 3);
            //g.DrawLine(WhitePen, x + length_interrupted * 6, y + length * 3, x + length_interrupted * 7, y + length * 3);
                      
            #endregion

            //Crosswalks
            #region Crosswalks

            //crossroad1 
            #region Crossorad1
            //crossroad1 - crosswalk - top
            g.DrawRectangle(WhitePen, x + length * 3 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 3 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 3 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 3 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 3 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 3 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 4 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 4 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 4 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);

            //crossroad1 - crosswalk - left
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 3 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 3 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 3 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 4 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 4 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 4 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);

            //crossroad1 - crosswalk - right
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 3 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 3 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 3 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 4 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 4 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 4 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);

            //crossroad1 - crosswalk - bottom
            g.DrawRectangle(WhitePen, x + length * 3 + FreeSpace, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 3 + 2 * FreeSpace + crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 3 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + FreeSpace, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + 2 * FreeSpace + crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);

            #endregion

            //crossroad2 
            #region Crossorad 2

            //crossroad2 - crosswalk - top
            g.DrawRectangle(WhitePen, x + length * 9 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 9 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 9 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);

            //crossroad2 - crosswalk - bottom
            g.DrawRectangle(WhitePen, x + length * 9 + FreeSpace, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 9 + 2 * FreeSpace + crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 9 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + FreeSpace, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + 2 * FreeSpace + crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);

            //crossroad2 - crosswalk - left
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 3 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 3 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 3 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 4 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 4 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 4 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);

            //crossroad2 - crosswalk - right
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 3 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 3 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 3 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 4 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 4 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 4 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);

            #endregion
                        
            #endregion

            //TrafficLights
            #region TrafficLights

            //crossorad1
            #region Crossroad1
            //crossroad1 - trafficlight - top
            g.DrawRectangle(WhitePen, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_height);
            g.FillEllipse(red, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            //crossroad1 - trafficlight - left
            g.DrawRectangle(WhitePen, x + length + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length + 3 * FreeSpace + TrafficLights_width * 2, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length + 3 * FreeSpace + TrafficLights_width, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);

            //crossroad1 - trafficlight - right
            g.DrawRectangle(WhitePen, x + length * 6 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length * 6 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 6 + FreeSpace + TrafficLights_width, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 6 + FreeSpace + TrafficLights_width * 2, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            //crossroad1 - trafficlight - bottom
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_height);
            g.FillEllipse(red, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);

            #endregion

            //crossroad2
            #region Crossroad2
            //crossroad2 - trafficlight - top
            g.DrawRectangle(WhitePen, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_height);
            g.FillEllipse(red, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            //crossroad2 - trafficlight - left
            g.DrawRectangle(WhitePen, x + length * 7 + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length * 7 + 3 * FreeSpace + TrafficLights_width * 2, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 7 + 3 * FreeSpace + TrafficLights_width, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 7 + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);

            //crossroad2 - trafficlight - right
            g.DrawRectangle(WhitePen, x + length * 12 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length * 12 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 12 + FreeSpace + TrafficLights_width, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 12 + FreeSpace + TrafficLights_width * 2, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            //crossroad2 - trafficlight - bottom
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_height);
            g.FillEllipse(red, x + length * 11 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 11 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 11 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);

            #endregion
                        
            #endregion

            //CrosswalkLights
            #region CrosswalkLights 

            //crossroad1
            #region Crossroad1
            //top
            //crossroad1 - CrosswalkLights - top - left
            g.DrawRectangle(WhitePen, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad1 - CrosswalkLights - top - right
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //bottom
            //crossroad1 - CrosswalkLights - bottom - left
            g.DrawRectangle(WhitePen, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad1 - CrosswalkLights - bottom - right
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //left
            //crossroad1 - CrosswalkLights - left - top
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad1 - CrosswalkLights - left - bottom
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //right
            //crossroad1 - CrosswalkLights - right - top
            g.DrawRectangle(WhitePen, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad1 - CrosswalkLights - right - bottom
            g.DrawRectangle(WhitePen, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            #endregion

            //crossroad2
            #region Crossroad2
            //top
            //crossroad2 - CrosswalkLights - top - left
            g.DrawRectangle(WhitePen, x + length * 8 + crosswalk_width + 3 * FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 8 + crosswalk_width + 3 * FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 8 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad2 - CrosswalkLights - top - right
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 11 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 11 + FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //bottom
            //crossroad2 - CrosswalkLights - bottom - left
            g.DrawRectangle(WhitePen, x + length * 8 + crosswalk_width + 3 * FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 8 + crosswalk_width + 3 * FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 8 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad2 - CrosswalkLights - bottom - right
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 11 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 11 + FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //left
            //crossroad2 - CrosswalkLights - left - top
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 8 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 8 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad2 - CrosswalkLights - left - bottom
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 8 + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 8 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //right
            //crossroad2 - CrosswalkLights - right - top
            g.DrawRectangle(WhitePen, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad2 - CrosswalkLights - right - bottom
            g.DrawRectangle(WhitePen, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            
            #endregion
                        
            #endregion

        }

        //Crossroad1 + Crossorad2 + Left T
        public void CrossroadExtension2()
        {
            var g = this.CreateGraphics();

            //background
            //g.Clear(Color.Black);

            //pen color
            Pen BlackPen = new Pen(Color.Black, 2);
            Pen WhitePen = new Pen(Color.White, 2);
            Pen RedPen = new Pen(Color.Red, 2);
            Pen GreenPen = new Pen(Color.Green, 2);
            Pen YellowPen = new Pen(Color.Yellow, 2);

            this.Refresh();

            //Traffic lines 
            #region Traffic lines

            //crossroad1
            #region crossroad1

            #region Corners
            //left top corner 

            //right top corner

            //left bottom corner

            //right botoom corner 

            #endregion

            #region whitelines / traffic lines 

            //top 

            //bottom

            //left 

            //right

            #endregion

            #endregion

            //crossorad2
            #region crossroad2 

            #region Corners
            //left top corner 

            //right top corner

            //left bottom corner

            //right botoom corner 

            #endregion

            #region whitelines / traffic lines 

            //top 

            //bottom

            //left 

            //right

            #endregion

            #endregion

            //Left T
            #region Left T

            #region Corners
            //left top corner 

            //right top corner

            //left bottom corner

            //right botoom corner 

            #endregion

            #region whitelines / traffic lines 

            //top 

            //bottom

            //left 

            //right

            #endregion

            #endregion


            //crossroad - left up corner
            //vertical line
            g.DrawLine(WhitePen, x + length * 3, y, x + length * 3, y + length);
            g.DrawLine(WhitePen, x + length * 3, y + length, x + length * 3, y + length * 2);
            g.DrawLine(WhitePen, x + length * 3, y + length * 2, x + length * 3, y + length * 3);
            //horizontal line
            g.DrawLine(WhitePen, x, y + length * 3, x + length, y + length * 3);
            g.DrawLine(WhitePen, x + length, y + length * 3, x + length * 2, y + length * 3);
            g.DrawLine(WhitePen, x + length * 2, y + length * 3, x + length * 3, y + length * 3);

            //white line - crossorad1 - left - internal
            //g.DrawLine(WhitePen, x + length * 3, y + length * 3, x + length * 3, y + length * 4); //up
            //g.DrawLine(WhitePen, x + length * 3, y + length * 4, x + length * 3, y + length * 5); //down
            //white line - crossroad1 - left - external
            //g.DrawLine(WhitePen, x + length * 2, y + length * 3, x + length * 2, y + length * 4); //up
            g.DrawLine(WhitePen, x + length * 2, y + length * 4, x + length * 2, y + length * 5); //down

            //crossroad - left down corner
            //horizontal line
            g.DrawLine(WhitePen, x, y + length * 5, x + length, y + length * 5);
            g.DrawLine(WhitePen, x + length, y + length * 5, x + length * 2, y + length * 5);
            g.DrawLine(WhitePen, x + length * 2, y + length * 5, x + length * 3, y + length * 5);
            //vertical line
            g.DrawLine(WhitePen, x + length * 3, y + length * 5, x + length * 3, y + length * 6);
            g.DrawLine(WhitePen, x + length * 3, y + length * 6, x + length * 3, y + length * 7);
            g.DrawLine(WhitePen, x + length * 3, y + length * 7, x + length * 3, y + length * 8);
            g.DrawLine(WhitePen, x + length * 3, y + length * 8, x + length * 3, y + length * 9);

            //white line - crossroad1 - top
            //internal
            //g.DrawLine(WhitePen, x + length * 3, y + length * 3, x + length * 4, y + length * 3); //left
            //g.DrawLine(WhitePen, x + length * 4, y + length * 3, x + length * 5, y + length * 3); //right
            //exteral
            g.DrawLine(WhitePen, x + length * 3, y + length * 2, x + length * 4, y + length * 2); //left
            //g.DrawLine(WhitePen, x + length * 4, y + length * 2, x + length * 5, y + length * 2); //right

            //crossroad - mid up - left
            g.DrawLine(WhitePen, x + length * 5, y, x + length * 5, y + length);
            g.DrawLine(WhitePen, x + length * 5, y + length, x + length * 5, y + length * 2);
            g.DrawLine(WhitePen, x + length * 5, y + length * 2, x + length * 5, y + length * 3);

            //crossroad - mid up - mid 
            g.DrawLine(WhitePen, x + length * 5, y + length * 3, x + length * 6, y + length * 3);
            g.DrawLine(WhitePen, x + length * 6, y + length * 3, x + length * 7, y + length * 3);
            g.DrawLine(WhitePen, x + length * 7, y + length * 3, x + length * 8, y + length * 3);
            g.DrawLine(WhitePen, x + length * 8, y + length * 3, x + length * 9, y + length * 3);
            //jednoduše
            //g.DrawLine(WhitePen, x + length * 5, y + length * 3, x + length * 9, y + length * 3);

            //crossroad - mid up - right 
            g.DrawLine(WhitePen, x + length * 9, y, x + length * 9, y + length);
            g.DrawLine(WhitePen, x + length * 9, y + length, x + length * 9, y + length * 2);
            g.DrawLine(WhitePen, x + length * 9, y + length * 2, x + length * 9, y + length * 3);

            //white line - crossroad1 - right
            //internal line
            //g.DrawLine(WhitePen, x + length * 5, y + length * 3, x + length * 5, y + length * 4); //up
            //g.DrawLine(WhitePen, x + length * 5, y + length * 4, x + length * 5, y + length * 5); //down
            //external line
            g.DrawLine(WhitePen, x + length * 6, y + length * 3, x + length * 6, y + length * 4); //up
            //g.DrawLine(WhitePen, x + length * 6, y + length * 4, x + length * 6, y + length * 5); //down

            //crossroad - mid down - left
            g.DrawLine(WhitePen, x + length * 5, y + length * 5, x + length * 5, y + length * 6);
            g.DrawLine(WhitePen, x + length * 5, y + length * 6, x + length * 5, y + length * 7);
            g.DrawLine(WhitePen, x + length * 5, y + length * 7, x + length * 5, y + length * 8);
            g.DrawLine(WhitePen, x + length * 5, y + length * 8, x + length * 5, y + length * 9);

            //crossroad - mid down - mid 
            g.DrawLine(WhitePen, x + length * 5, y + length * 5, x + length * 6, y + length * 5);
            g.DrawLine(WhitePen, x + length * 6, y + length * 5, x + length * 7, y + length * 5);
            g.DrawLine(WhitePen, x + length * 7, y + length * 5, x + length * 8, y + length * 5);
            g.DrawLine(WhitePen, x + length * 8, y + length * 5, x + length * 9, y + length * 5);

            //crossroad - mid down - right
            g.DrawLine(WhitePen, x + length * 9, y + length * 5, x + length * 9, y + length * 6);
            g.DrawLine(WhitePen, x + length * 9, y + length * 6, x + length * 9, y + length * 7);
            g.DrawLine(WhitePen, x + length * 9, y + length * 7, x + length * 9, y + length * 8);
            g.DrawLine(WhitePen, x + length * 9, y + length * 8, x + length * 9, y + length * 9);

            //white line - crossroad1 - bottom
            //internal
            //g.DrawLine(WhitePen, x + length * 3, y + length * 5, x + length * 4, y + length * 5); //left
            //g.DrawLine(WhitePen, x + length * 4, y + length * 5, x + length * 5, y + length * 5); //right
            //external
            //g.DrawLine(WhitePen, x + length * 3, y + length * 6, x + length * 4, y + length * 6); //left
            g.DrawLine(WhitePen, x + length * 4, y + length * 6, x + length * 5, y + length * 6); //right

            //white line - crossroad2 - left
            //exteranl
            //g.DrawLine(WhitePen, x + length * 8, y + length * 3, x + length * 8, y + length * 4); //up
            g.DrawLine(WhitePen, x + length * 8, y + length * 4, x + length * 8, y + length * 5); //down
            //internal
            //g.DrawLine(WhitePen, x + length * 9, y + length * 3, x + length * 9, y + length * 4); //up
            //g.DrawLine(WhitePen, x + length * 9, y + length * 4, x + length * 9, y + length * 5); //down 

            //crossroad - right up corner
            //vertical line
            g.DrawLine(WhitePen, x + length * 11, y, x + length * 11, y + length);
            g.DrawLine(WhitePen, x + length * 11, y + length, x + length * 11, y + length * 2);
            g.DrawLine(WhitePen, x + length * 11, y + length * 2, x + length * 11, y + length * 3);
            //horizontal line
            g.DrawLine(WhitePen, x + length * 11, y + length * 3, x + length * 12, y + length * 3);
            g.DrawLine(WhitePen, x + length * 12, y + length * 3, x + length * 13, y + length * 3);
            g.DrawLine(WhitePen, x + length * 13, y + length * 3, x + length * 14, y + length * 3);

            //white line - crossroad2 - top
            //external
            g.DrawLine(WhitePen, x + length * 9, y + length * 2, x + length * 10, y + length * 2); //left
            //g.DrawLine(WhitePen, x + length * 10, y + length * 2, x + length * 11, y + length * 2); //right
            //internal
            //g.DrawLine(WhitePen, x + length * 9, y + length * 3, x + length * 10, y + length * 3); //left
            //g.DrawLine(WhitePen, x + length * 10, y + length * 3, x + length * 11, y + length * 3); //right

            //crossroad - right down corner
            //vertical line
            g.DrawLine(WhitePen, x + length * 11, y + length * 5, x + length * 11, y + length * 6);
            g.DrawLine(WhitePen, x + length * 11, y + length * 6, x + length * 11, y + length * 7);
            g.DrawLine(WhitePen, x + length * 11, y + length * 7, x + length * 11, y + length * 8);
            g.DrawLine(WhitePen, x + length * 11, y + length * 8, x + length * 11, y + length * 9);
            //horizontal line
            g.DrawLine(WhitePen, x + length * 11, y + length * 5, x + length * 12, y + length * 5);
            g.DrawLine(WhitePen, x + length * 12, y + length * 5, x + length * 13, y + length * 5);
            g.DrawLine(WhitePen, x + length * 13, y + length * 5, x + length * 14, y + length * 5);

            //white line - crossroad2 - bottom
            //internal
            //g.DrawLine(WhitePen, x + length * 9, y + length * 5, x + length * 10, y + length * 5); //left
            //g.DrawLine(WhitePen, x + length * 10, y + length * 5, x + length * 11, y + length * 5); //right
            //external
            //g.DrawLine(WhitePen, x + length * 9, y + length * 6, x + length * 10, y + length * 6); //left
            g.DrawLine(WhitePen, x + length * 10, y + length * 6, x + length * 11, y + length * 6); //right

            //white line - crossroad2 - right
            //internal
            //g.DrawLine(WhitePen, x + length * 11, y + length * 3, x + length * 11, y + length * 4); //up
            //g.DrawLine(WhitePen, x + length * 11, y + length * 4, x + length * 11, y + length * 5); //down
            //external
            g.DrawLine(WhitePen, x + length * 12, y + length * 3, x + length * 12, y + length * 4); //up
            //g.DrawLine(WhitePen, x + length * 12, y + length * 4, x + length * 12, y + length * 5); //down

            //crossroad - T - left 
            g.DrawLine(WhitePen, x, y + length * 9, x + length, y + length * 9);
            g.DrawLine(WhitePen, x + length, y + length * 9, x + length * 2, y + length * 9);
            g.DrawLine(WhitePen, x + length * 2, y + length * 9, x + length * 3, y + length * 9);

            //crossroad - T - mid 
            g.DrawLine(WhitePen, x + length * 5, y + length * 9, x + length * 6, y + length * 9);
            g.DrawLine(WhitePen, x + length * 6, y + length * 9, x + length * 7, y + length * 9);
            g.DrawLine(WhitePen, x + length * 7, y + length * 9, x + length * 8, y + length * 9);
            g.DrawLine(WhitePen, x + length * 8, y + length * 9, x + length * 9, y + length * 9);

            //crossroad - T - bottom 
            g.DrawLine(WhitePen, x, y + length * 11, x + length * 9, y + length * 11);

            //whiteline - T
            //left
            g.DrawLine(WhitePen, x + length * 2, y + length * 10, x + length * 2, y + length * 11);
            //top
            g.DrawLine(WhitePen, x + length * 3, y + length * 8, x + length * 4, y + length * 8);
            //right
            g.DrawLine(WhitePen, x + length * 6, y + length * 9, x + length * 6, y + length * 10);
                        
            #endregion

            //Center lines
            #region Center lines

            //vertical center line - left
            //top
            g.DrawLine(WhitePen, x + length * 4, y, x + length * 4, y + length_interrupted);
            g.DrawLine(WhitePen, x + length * 4, y + length_interrupted * 2, x + length * 4, y + length_interrupted * 3);
            //g.DrawLine(WhitePen, x + length * 4, y + length_interrupted * 4, x + length * 4, y + length_interrupted * 5);
            //g.DrawLine(WhitePen, x + length * 4, y + length_interrupted * 6, x + length * 4, y + length_interrupted * 7);
            g.DrawLine(WhitePen, x + length * 4, y + length, x + length * 4, y + length * 2);
            //bottom
            g.DrawLine(WhitePen, x + length * 4, y + length * 6, x + length * 4, y + length * 8);

            //vertical center line - right
            g.DrawLine(WhitePen, x + length * 10, y, x + length * 10, y + length_interrupted);
            g.DrawLine(WhitePen, x + length * 10, y + length_interrupted * 2, x + length * 10, y + length_interrupted * 3);
            //g.DrawLine(WhitePen, x + length * 4, y + length_interrupted * 4, x + length * 4, y + length_interrupted * 5);
            //g.DrawLine(WhitePen, x + length * 4, y + length_interrupted * 6, x + length * 4, y + length_interrupted * 7);
            g.DrawLine(WhitePen, x + length * 10, y + length, x + length * 10, y + length * 2);
            //bottom
            g.DrawLine(WhitePen, x + length * 10, y + length * 6, x + length * 10, y + length * 8);

            //horizontal center line - top 
            g.DrawLine(WhitePen, x, y + length * 4, x + length_interrupted, y + length * 4);
            g.DrawLine(WhitePen, x + length_interrupted * 2, y + length * 4, x + length_interrupted * 3, y + length * 4);
            g.DrawLine(WhitePen, x + length, y + length * 4, x + length * 2, y + length * 4);
            g.DrawLine(WhitePen, x + length * 6, y + length * 4, x + length * 8, y + length * 4);
            g.DrawLine(WhitePen, x + length * 12, y + length * 4, x + length * 13, y + length * 4);
            g.DrawLine(WhitePen, x + length * 13 + length_interrupted, y + length * 4, x + length * 13 + length_interrupted * 2, y + length * 4);
            g.DrawLine(WhitePen, x + length * 13 + length_interrupted * 3, y + length * 4, x + length * 13 + length_interrupted * 4, y + length * 4);

            //g.DrawLine(WhitePen, x + length_interrupted * 4, y + length * 3, x + length_interrupted * 5, y + length * 3);
            //g.DrawLine(WhitePen, x + length_interrupted * 6, y + length * 3, x + length_interrupted * 7, y + length * 3);

            g.DrawLine(WhitePen, x + length, y + length * 10, x + length * 2, y + length * 10);
            g.DrawLine(WhitePen, x + length * 6, y + length * 10, x + length * 8, y + length * 10);

            
            //horizontal center line - bottom 
            //left T
            g.DrawLine(WhitePen, x, y + length * 10, x + length_interrupted, y + length * 10);
            g.DrawLine(WhitePen, x + length_interrupted * 2, y + length * 10, x + length_interrupted * 3, y + length * 10);
            g.DrawLine(WhitePen, x + length, y + length * 10, x + length * 2, y + length * 10);

            g.DrawLine(WhitePen, x + length * 6, y + length * 10, x + length * 8, y + length * 10);

            




            #endregion

            //Crosswalks
            #region Crosswalks

            //crossroad1 
            #region Crossroad1 

            //crossroad1 - crosswalk - top
            g.DrawRectangle(WhitePen, x + length * 3 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 3 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 3 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 3 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 3 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 3 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 4 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 4 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 4 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);

            //crossroad1 - crosswalk - left
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 3 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 3 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 3 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 4 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 4 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 4 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);

            //crossroad1 - crosswalk - right
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 3 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 3 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 3 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 4 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 4 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 4 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);

            //crossroad1 - crosswalk - bottom
            g.DrawRectangle(WhitePen, x + length * 3 + FreeSpace, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 3 + 2 * FreeSpace + crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 3 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + FreeSpace, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + 2 * FreeSpace + crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);

            #endregion

            //crossroad2
            #region Crossorad2

            //crossroad2 - crosswalk - top
            g.DrawRectangle(WhitePen, x + length * 9 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 9 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 9 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);

            //crossroad2 - crosswalk - bottom
            g.DrawRectangle(WhitePen, x + length * 9 + FreeSpace, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 9 + 2 * FreeSpace + crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 9 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + FreeSpace, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + 2 * FreeSpace + crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);

            //crossroad2 - crosswalk - left
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 3 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 3 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 3 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 4 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 4 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 4 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);

            //crossroad2 - crosswalk - right
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 3 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 3 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 3 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 4 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 4 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 4 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);

            #endregion

            //left T
            #region Left T

            //left T - crosswalk - top
            g.DrawRectangle(WhitePen, x + length * 3 + FreeSpace, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 3 + 2 * FreeSpace + crosswalk_width, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 3 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + FreeSpace, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + 2 * FreeSpace + crosswalk_width, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);

            //left T - crosswalk - left
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 9 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 9 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 9 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 10 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 10 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 10 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);

            //left T - crosswalk - right
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 9 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 9 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 9 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 10 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 10 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 10 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);

            #endregion
                        
            #endregion

            //TrafficLights
            #region TrafficLights

            //crossorad1
            #region Crossroad1
            //crossroad1 - trafficlight - top
            g.DrawRectangle(WhitePen, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_height);
            g.FillEllipse(red, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            //crossroad1 - trafficlight - left
            g.DrawRectangle(WhitePen, x + length + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length + 3 * FreeSpace + TrafficLights_width * 2, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length + 3 * FreeSpace + TrafficLights_width, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);

            //crossroad1 - trafficlight - right
            g.DrawRectangle(WhitePen, x + length * 6 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length * 6 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 6 + FreeSpace + TrafficLights_width, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 6 + FreeSpace + TrafficLights_width * 2, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            //crossroad1 - trafficlight - bottom
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_height);
            g.FillEllipse(red, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);

            #endregion

            //crossroad2
            #region Crossroad2
            //crossroad2 - trafficlight - top
            g.DrawRectangle(WhitePen, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_height);
            g.FillEllipse(red, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            //crossroad2 - trafficlight - left
            g.DrawRectangle(WhitePen, x + length * 7 + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length * 7 + 3 * FreeSpace + TrafficLights_width * 2, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 7 + 3 * FreeSpace + TrafficLights_width, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 7 + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);

            //crossroad2 - trafficlight - right
            g.DrawRectangle(WhitePen, x + length * 12 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length * 12 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 12 + FreeSpace + TrafficLights_width, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 12 + FreeSpace + TrafficLights_width * 2, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            //crossroad2 - trafficlight - bottom
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_height);
            g.FillEllipse(red, x + length * 11 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 11 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 11 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);

            #endregion

            //left T
            #region Left T
            //left T - trafficlight - top
            g.DrawRectangle(WhitePen, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace, TrafficLights_width, TrafficLights_height);
            g.FillEllipse(red, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            //left T - trafficlight - left
            g.DrawRectangle(WhitePen, x + length + 3 * FreeSpace, y + length * 11 + FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length + 3 * FreeSpace + TrafficLights_width * 2, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length + 3 * FreeSpace + TrafficLights_width, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length + 3 * FreeSpace, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);

            //left T - trafficlight - right
            g.DrawRectangle(WhitePen, x + length * 6 + FreeSpace, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length * 6 + FreeSpace, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 6 + FreeSpace + TrafficLights_width, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 6 + FreeSpace + TrafficLights_width * 2, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            #endregion

            #endregion

            //CrosswalkLights
            #region CrosswalkLights 

            //crossroad1
            #region Crossroad1
            //top
            //crossroad1 - CrosswalkLights - top - left
            g.DrawRectangle(WhitePen, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad1 - CrosswalkLights - top - right
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //bottom
            //crossroad1 - CrosswalkLights - bottom - left
            g.DrawRectangle(WhitePen, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad1 - CrosswalkLights - bottom - right
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //left
            //crossroad1 - CrosswalkLights - left - top
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad1 - CrosswalkLights - left - bottom
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //right
            //crossroad1 - CrosswalkLights - right - top
            g.DrawRectangle(WhitePen, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad1 - CrosswalkLights - right - bottom
            g.DrawRectangle(WhitePen, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            #endregion

            //crossroad2
            #region Crossroad2
            //top
            //crossroad2 - CrosswalkLights - top - left
            g.DrawRectangle(WhitePen, x + length * 8 + crosswalk_width + 3 * FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 8 + crosswalk_width + 3 * FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 8 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad2 - CrosswalkLights - top - right
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 11 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 11 + FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //bottom
            //crossroad2 - CrosswalkLights - bottom - left
            g.DrawRectangle(WhitePen, x + length * 8 + crosswalk_width + 3 * FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 8 + crosswalk_width + 3 * FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 8 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad2 - CrosswalkLights - bottom - right
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 11 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 11 + FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //left
            //crossroad2 - CrosswalkLights - left - top
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 8 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 8 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad2 - CrosswalkLights - left - bottom
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 8 + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 8 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //right
            //crossroad2 - CrosswalkLights - right - top
            g.DrawRectangle(WhitePen, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad2 - CrosswalkLights - right - bottom
            g.DrawRectangle(WhitePen, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            #endregion

            //left T
            #region Left T
            //left T - CrosswalkLights - top - left
            g.DrawRectangle(WhitePen, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //left T - CrosswalkLights - top - right
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //left T - CrosswalkLights - left - top
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 2 + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //left T - CrosswalkLights - left - bottom
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + length * 11 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 2 + FreeSpace, y + length * 11 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + FreeSpace, y + length * 11 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //left T - CrosswalkLights - right - top 
            g.DrawRectangle(WhitePen, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //left T - CrosswalkLights - right - bottom
            g.DrawRectangle(WhitePen, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 11 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 11 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 11 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            #endregion

            #endregion
                       
        }

        //Crossroad1 + Crossorad2 + Left T + Right T
        public void CrossroadExtension3()
        {
            var g = this.CreateGraphics();

            //background
            //g.Clear(Color.Black);

            //pen color
            Pen BlackPen = new Pen(Color.Black, 2);
            Pen WhitePen = new Pen(Color.White, 2);
            Pen RedPen = new Pen(Color.Red, 2);
            Pen GreenPen = new Pen(Color.Green, 2);
            Pen YellowPen = new Pen(Color.Yellow, 2);

            this.Refresh();

            //Traffic lines 
            #region Traffic lines 

            //crossroad1
            #region crossroad1

            #region Corners
            //left top corner 

            //right top corner

            //left bottom corner

            //right botoom corner 

            #endregion

            #region whitelines / traffic lines 

            //top 

            //bottom

            //left 

            //right

            #endregion

            #endregion

            //crossorad2
            #region crossroad2 

            #region Corners
            //left top corner 

            //right top corner

            //left bottom corner

            //right botoom corner 

            #endregion

            #region whitelines / traffic lines 

            //top 

            //bottom

            //left 

            //right

            #endregion

            #endregion

            //Left T
            #region Left T

            #region Corners
            //left top corner 

            //right top corner

            //left bottom corner

            //right botoom corner 

            #endregion

            #region whitelines / traffic lines 

            //top 

            //bottom

            //left 

            //right

            #endregion

            #endregion

            //Right T
            #region Right T

            #region Corners
            //left top corner 

            //right top corner

            //left bottom corner

            //right botoom corner 

            #endregion

            #region whitelines / traffic lines 

            //top 

            //bottom

            //left 

            //right

            #endregion

            #endregion

            //crossroad - left up corner
            //vertical line
            g.DrawLine(WhitePen, x + length * 3, y, x + length * 3, y + length);
            g.DrawLine(WhitePen, x + length * 3, y + length, x + length * 3, y + length * 2);
            g.DrawLine(WhitePen, x + length * 3, y + length * 2, x + length * 3, y + length * 3);
            //horizontal line
            g.DrawLine(WhitePen, x, y + length * 3, x + length, y + length * 3);
            g.DrawLine(WhitePen, x + length, y + length * 3, x + length * 2, y + length * 3);
            g.DrawLine(WhitePen, x + length * 2, y + length * 3, x + length * 3, y + length * 3);

            //white line - crossorad1 - left - internal
            //g.DrawLine(WhitePen, x + length * 3, y + length * 3, x + length * 3, y + length * 4); //up
            //g.DrawLine(WhitePen, x + length * 3, y + length * 4, x + length * 3, y + length * 5); //down
            //white line - crossroad1 - left - external
            //g.DrawLine(WhitePen, x + length * 2, y + length * 3, x + length * 2, y + length * 4); //up
            g.DrawLine(WhitePen, x + length * 2, y + length * 4, x + length * 2, y + length * 5); //down

            //crossroad - left down corner
            //horizontal line
            g.DrawLine(WhitePen, x, y + length * 5, x + length, y + length * 5);
            g.DrawLine(WhitePen, x + length, y + length * 5, x + length * 2, y + length * 5);
            g.DrawLine(WhitePen, x + length * 2, y + length * 5, x + length * 3, y + length * 5);
            //vertical line
            g.DrawLine(WhitePen, x + length * 3, y + length * 5, x + length * 3, y + length * 6);
            g.DrawLine(WhitePen, x + length * 3, y + length * 6, x + length * 3, y + length * 7);
            g.DrawLine(WhitePen, x + length * 3, y + length * 7, x + length * 3, y + length * 8);
            g.DrawLine(WhitePen, x + length * 3, y + length * 8, x + length * 3, y + length * 9);

            //white line - crossroad1 - top
            //internal
            //g.DrawLine(WhitePen, x + length * 3, y + length * 3, x + length * 4, y + length * 3); //left
            //g.DrawLine(WhitePen, x + length * 4, y + length * 3, x + length * 5, y + length * 3); //right
            //exteral
            g.DrawLine(WhitePen, x + length * 3, y + length * 2, x + length * 4, y + length * 2); //left
            //g.DrawLine(WhitePen, x + length * 4, y + length * 2, x + length * 5, y + length * 2); //right

            //crossroad - mid up - left
            g.DrawLine(WhitePen, x + length * 5, y, x + length * 5, y + length);
            g.DrawLine(WhitePen, x + length * 5, y + length, x + length * 5, y + length * 2);
            g.DrawLine(WhitePen, x + length * 5, y + length * 2, x + length * 5, y + length * 3);

            //crossroad - mid up - mid 
            g.DrawLine(WhitePen, x + length * 5, y + length * 3, x + length * 6, y + length * 3);
            g.DrawLine(WhitePen, x + length * 6, y + length * 3, x + length * 7, y + length * 3);
            g.DrawLine(WhitePen, x + length * 7, y + length * 3, x + length * 8, y + length * 3);
            g.DrawLine(WhitePen, x + length * 8, y + length * 3, x + length * 9, y + length * 3);
            //jednoduše
            //g.DrawLine(WhitePen, x + length * 5, y + length * 3, x + length * 9, y + length * 3);

            //crossroad - mid up - right 
            g.DrawLine(WhitePen, x + length * 9, y, x + length * 9, y + length);
            g.DrawLine(WhitePen, x + length * 9, y + length, x + length * 9, y + length * 2);
            g.DrawLine(WhitePen, x + length * 9, y + length * 2, x + length * 9, y + length * 3);

            //white line - crossroad1 - right
            //internal line
            //g.DrawLine(WhitePen, x + length * 5, y + length * 3, x + length * 5, y + length * 4); //up
            //g.DrawLine(WhitePen, x + length * 5, y + length * 4, x + length * 5, y + length * 5); //down
            //external line
            g.DrawLine(WhitePen, x + length * 6, y + length * 3, x + length * 6, y + length * 4); //up
            //g.DrawLine(WhitePen, x + length * 6, y + length * 4, x + length * 6, y + length * 5); //down

            //crossroad - mid down - left
            g.DrawLine(WhitePen, x + length * 5, y + length * 5, x + length * 5, y + length * 6);
            g.DrawLine(WhitePen, x + length * 5, y + length * 6, x + length * 5, y + length * 7);
            g.DrawLine(WhitePen, x + length * 5, y + length * 7, x + length * 5, y + length * 8);
            g.DrawLine(WhitePen, x + length * 5, y + length * 8, x + length * 5, y + length * 9);

            //crossroad - mid down - mid 
            g.DrawLine(WhitePen, x + length * 5, y + length * 5, x + length * 6, y + length * 5);
            g.DrawLine(WhitePen, x + length * 6, y + length * 5, x + length * 7, y + length * 5);
            g.DrawLine(WhitePen, x + length * 7, y + length * 5, x + length * 8, y + length * 5);
            g.DrawLine(WhitePen, x + length * 8, y + length * 5, x + length * 9, y + length * 5);

            //crossroad - mid down - right
            g.DrawLine(WhitePen, x + length * 9, y + length * 5, x + length * 9, y + length * 6);
            g.DrawLine(WhitePen, x + length * 9, y + length * 6, x + length * 9, y + length * 7);
            g.DrawLine(WhitePen, x + length * 9, y + length * 7, x + length * 9, y + length * 8);
            g.DrawLine(WhitePen, x + length * 9, y + length * 8, x + length * 9, y + length * 9);

            //white line - crossroad1 - bottom
            //internal
            //g.DrawLine(WhitePen, x + length * 3, y + length * 5, x + length * 4, y + length * 5); //left
            //g.DrawLine(WhitePen, x + length * 4, y + length * 5, x + length * 5, y + length * 5); //right
            //external
            //g.DrawLine(WhitePen, x + length * 3, y + length * 6, x + length * 4, y + length * 6); //left
            g.DrawLine(WhitePen, x + length * 4, y + length * 6, x + length * 5, y + length * 6); //right

            //white line - crossroad2 - left
            //exteranl
            //g.DrawLine(WhitePen, x + length * 8, y + length * 3, x + length * 8, y + length * 4); //up
            g.DrawLine(WhitePen, x + length * 8, y + length * 4, x + length * 8, y + length * 5); //down
            //internal
            //g.DrawLine(WhitePen, x + length * 9, y + length * 3, x + length * 9, y + length * 4); //up
            //g.DrawLine(WhitePen, x + length * 9, y + length * 4, x + length * 9, y + length * 5); //down 

            //crossroad - right up corner
            //vertical line
            g.DrawLine(WhitePen, x + length * 11, y, x + length * 11, y + length);
            g.DrawLine(WhitePen, x + length * 11, y + length, x + length * 11, y + length * 2);
            g.DrawLine(WhitePen, x + length * 11, y + length * 2, x + length * 11, y + length * 3);
            //horizontal line
            g.DrawLine(WhitePen, x + length * 11, y + length * 3, x + length * 12, y + length * 3);
            g.DrawLine(WhitePen, x + length * 12, y + length * 3, x + length * 13, y + length * 3);
            g.DrawLine(WhitePen, x + length * 13, y + length * 3, x + length * 14, y + length * 3);

            //white line - crossroad2 - top
            //external
            g.DrawLine(WhitePen, x + length * 9, y + length * 2, x + length * 10, y + length * 2); //left
            //g.DrawLine(WhitePen, x + length * 10, y + length * 2, x + length * 11, y + length * 2); //right
            //internal
            //g.DrawLine(WhitePen, x + length * 9, y + length * 3, x + length * 10, y + length * 3); //left
            //g.DrawLine(WhitePen, x + length * 10, y + length * 3, x + length * 11, y + length * 3); //right

            //crossroad - right down corner
            //vertical line
            g.DrawLine(WhitePen, x + length * 11, y + length * 5, x + length * 11, y + length * 6);
            g.DrawLine(WhitePen, x + length * 11, y + length * 6, x + length * 11, y + length * 7);
            g.DrawLine(WhitePen, x + length * 11, y + length * 7, x + length * 11, y + length * 8);
            g.DrawLine(WhitePen, x + length * 11, y + length * 8, x + length * 11, y + length * 9);
            //horizontal line
            g.DrawLine(WhitePen, x + length * 11, y + length * 5, x + length * 12, y + length * 5);
            g.DrawLine(WhitePen, x + length * 12, y + length * 5, x + length * 13, y + length * 5);
            g.DrawLine(WhitePen, x + length * 13, y + length * 5, x + length * 14, y + length * 5);

            //white line - crossroad2 - bottom
            //internal
            //g.DrawLine(WhitePen, x + length * 9, y + length * 5, x + length * 10, y + length * 5); //left
            //g.DrawLine(WhitePen, x + length * 10, y + length * 5, x + length * 11, y + length * 5); //right
            //external
            //g.DrawLine(WhitePen, x + length * 9, y + length * 6, x + length * 10, y + length * 6); //left
            g.DrawLine(WhitePen, x + length * 10, y + length * 6, x + length * 11, y + length * 6); //right

            //white line - crossroad2 - right
            //internal
            //g.DrawLine(WhitePen, x + length * 11, y + length * 3, x + length * 11, y + length * 4); //up
            //g.DrawLine(WhitePen, x + length * 11, y + length * 4, x + length * 11, y + length * 5); //down
            //external
            g.DrawLine(WhitePen, x + length * 12, y + length * 3, x + length * 12, y + length * 4); //up
            //g.DrawLine(WhitePen, x + length * 12, y + length * 4, x + length * 12, y + length * 5); //down

            //crossroad - T - left 
            g.DrawLine(WhitePen, x, y + length * 9, x + length, y + length * 9);
            g.DrawLine(WhitePen, x + length, y + length * 9, x + length * 2, y + length * 9);
            g.DrawLine(WhitePen, x + length * 2, y + length * 9, x + length * 3, y + length * 9);

            //crossroad - T - mid 
            g.DrawLine(WhitePen, x + length * 5, y + length * 9, x + length * 6, y + length * 9);
            g.DrawLine(WhitePen, x + length * 6, y + length * 9, x + length * 7, y + length * 9);
            g.DrawLine(WhitePen, x + length * 7, y + length * 9, x + length * 8, y + length * 9);
            g.DrawLine(WhitePen, x + length * 8, y + length * 9, x + length * 9, y + length * 9);

            //crossroad - T - bottom 
            g.DrawLine(WhitePen, x, y + length * 11, x + length * 14, y + length * 11);

            //crossroad - T - right
            g.DrawLine(WhitePen, x + length * 11, y + length * 9, x + length * 12, y + length * 9);
            g.DrawLine(WhitePen, x + length * 12, y + length * 9, x + length * 13, y + length * 9);
            g.DrawLine(WhitePen, x + length * 13, y + length * 9, x + length * 14, y + length * 9);

            //pravá spojka mezi crossroad2 a T2 - čára pro auto
            g.DrawLine(WhitePen, x + length * 9, y + length * 8, x + length * 10, y + length * 8);

            //whiteline - T
            //left
            g.DrawLine(WhitePen, x + length * 2, y + length * 10, x + length * 2, y + length * 11);
            //top
            g.DrawLine(WhitePen, x + length * 3, y + length * 8, x + length * 4, y + length * 8);
            //right
            g.DrawLine(WhitePen, x + length * 6, y + length * 9, x + length * 6, y + length * 10);

            g.DrawLine(WhitePen, x + length * 8, y + length * 10, x + length * 8, y + length * 11);

            g.DrawLine(WhitePen, x + length * 12, y + length * 9, x + length * 12, y + length * 10);

            #endregion

            //Center lines
            #region Center lines

            //vertical center line - left
            //top
            g.DrawLine(WhitePen, x + length * 4, y, x + length * 4, y + length_interrupted);
            g.DrawLine(WhitePen, x + length * 4, y + length_interrupted * 2, x + length * 4, y + length_interrupted * 3);
            //g.DrawLine(WhitePen, x + length * 4, y + length_interrupted * 4, x + length * 4, y + length_interrupted * 5);
            //g.DrawLine(WhitePen, x + length * 4, y + length_interrupted * 6, x + length * 4, y + length_interrupted * 7);
            g.DrawLine(WhitePen, x + length * 4, y + length, x + length * 4, y + length * 2);
            //bottom
            g.DrawLine(WhitePen, x + length * 4, y + length * 6, x + length * 4, y + length * 8);

            //vertical center line - right
            g.DrawLine(WhitePen, x + length * 10, y, x + length * 10, y + length_interrupted);
            g.DrawLine(WhitePen, x + length * 10, y + length_interrupted * 2, x + length * 10, y + length_interrupted * 3);
            //g.DrawLine(WhitePen, x + length * 4, y + length_interrupted * 4, x + length * 4, y + length_interrupted * 5);
            //g.DrawLine(WhitePen, x + length * 4, y + length_interrupted * 6, x + length * 4, y + length_interrupted * 7);
            g.DrawLine(WhitePen, x + length * 10, y + length, x + length * 10, y + length * 2);
            //bottom
            g.DrawLine(WhitePen, x + length * 10, y + length * 6, x + length * 10, y + length * 8);

            //horizontal center line - top 
            g.DrawLine(WhitePen, x, y + length * 4, x + length_interrupted, y + length * 4);
            g.DrawLine(WhitePen, x + length_interrupted * 2, y + length * 4, x + length_interrupted * 3, y + length * 4);
            g.DrawLine(WhitePen, x + length, y + length * 4, x + length * 2, y + length * 4);
            g.DrawLine(WhitePen, x + length * 6, y + length * 4, x + length * 8, y + length * 4);
            g.DrawLine(WhitePen, x + length * 12, y + length * 4, x + length * 13, y + length * 4);
            g.DrawLine(WhitePen, x + length * 13 + length_interrupted, y + length * 4, x + length * 13 + length_interrupted * 2, y + length * 4);
            g.DrawLine(WhitePen, x + length * 13 + length_interrupted * 3, y + length * 4, x + length * 13 + length_interrupted * 4, y + length * 4);

            //g.DrawLine(WhitePen, x + length_interrupted * 4, y + length * 3, x + length_interrupted * 5, y + length * 3);
            //g.DrawLine(WhitePen, x + length_interrupted * 6, y + length * 3, x + length_interrupted * 7, y + length * 3);

            g.DrawLine(WhitePen, x + length, y + length * 10, x + length * 2, y + length * 10);
            g.DrawLine(WhitePen, x + length * 6, y + length * 10, x + length * 8, y + length * 10);

            g.DrawLine(WhitePen, x + length * 12, y + length * 10, x + length * 13, y + length * 10);

            //horizontal center line - bottom 
            //left T
            g.DrawLine(WhitePen, x, y + length * 10, x + length_interrupted, y + length * 10);
            g.DrawLine(WhitePen, x + length_interrupted * 2, y + length * 10, x + length_interrupted * 3, y + length * 10);
            g.DrawLine(WhitePen, x + length, y + length * 10, x + length * 2, y + length * 10);

            g.DrawLine(WhitePen, x + length * 6, y + length * 10, x + length * 8, y + length * 10);

            //right T 
            g.DrawLine(WhitePen, x + length * 12, y + length * 10, x + length * 13, y + length * 10);
            g.DrawLine(WhitePen, x + length * 13 + length_interrupted, y + length * 10, x + length * 13 + length_interrupted * 2, y + length * 10);
            g.DrawLine(WhitePen, x + length * 13 + length_interrupted * 3, y + length * 10, x + length * 13 + length_interrupted * 4, y + length * 10);




            #endregion

            //Crosswalks
            #region Crosswalks

            //crossroad1 
            #region Crossroad1
            //crossroad1 - crosswalk - top
            g.DrawRectangle(WhitePen, x + length * 3 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 3 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 3 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 3 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 3 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 3 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 4 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 4 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 4 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);

            //crossroad1 - crosswalk - left
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 3 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 3 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 3 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 4 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 4 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 4 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);

            //crossroad1 - crosswalk - right
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 3 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 3 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 3 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 4 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 4 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 4 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);

            //crossroad1 - crosswalk - bottom
            g.DrawRectangle(WhitePen, x + length * 3 + FreeSpace, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 3 + 2 * FreeSpace + crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 3 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + FreeSpace, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + 2 * FreeSpace + crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);

            #endregion

            //crossroad2
            #region Crossorad2 
            //crossroad2 - crosswalk - top
            g.DrawRectangle(WhitePen, x + length * 9 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 9 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 9 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);

            //crossroad2 - crosswalk - bottom
            g.DrawRectangle(WhitePen, x + length * 9 + FreeSpace, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 9 + 2 * FreeSpace + crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 9 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + FreeSpace, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + 2 * FreeSpace + crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);

            //crossroad2 - crosswalk - left
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 3 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 3 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 3 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 4 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 4 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 4 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);

            //crossroad2 - crosswalk - right
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 3 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 3 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 3 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 4 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 4 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 4 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);

            #endregion

            //left T
            #region Left T
            //left T - crosswalk - top
            g.DrawRectangle(WhitePen, x + length * 3 + FreeSpace, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 3 + 2 * FreeSpace + crosswalk_width, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 3 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + FreeSpace, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + 2 * FreeSpace + crosswalk_width, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);

            //left T - crosswalk - left
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 9 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 9 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 9 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 10 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 10 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 10 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);

            //left T - crosswalk - right
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 9 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 9 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 9 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 10 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 10 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 10 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);

            #endregion

            //right T
            #region Right T
            //right T - crosswalk - top
            g.DrawRectangle(WhitePen, x + length * 9 + FreeSpace, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 9 + 2 * FreeSpace + crosswalk_width, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 9 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + FreeSpace, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + 2 * FreeSpace + crosswalk_width, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);

            //right T - crosswalk - left
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 9 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 9 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 9 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 10 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 10 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 10 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);

            //right T - crosswalk - right
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 9 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 9 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 9 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 10 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 10 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 10 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);

            #endregion

            #endregion

            //TrafficLights
            #region TrafficLights

            //crossorad1
            #region Crossroad1
            //crossroad1 - trafficlight - top
            g.DrawRectangle(WhitePen, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_height);
            g.FillEllipse(red, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            //crossroad1 - trafficlight - left
            g.DrawRectangle(WhitePen, x + length + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length + 3 * FreeSpace + TrafficLights_width * 2, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length + 3 * FreeSpace + TrafficLights_width, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);

            //crossroad1 - trafficlight - right
            g.DrawRectangle(WhitePen, x + length * 6 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length * 6 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 6 + FreeSpace + TrafficLights_width, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 6 + FreeSpace + TrafficLights_width * 2, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            //crossroad1 - trafficlight - bottom
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_height);
            g.FillEllipse(red, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);

            #endregion

            //crossroad2
            #region Crossroad2
            //crossroad2 - trafficlight - top
            g.DrawRectangle(WhitePen, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_height);
            g.FillEllipse(red, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            //crossroad2 - trafficlight - left
            g.DrawRectangle(WhitePen, x + length * 7 + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length * 7 + 3 * FreeSpace + TrafficLights_width * 2, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 7 + 3 * FreeSpace + TrafficLights_width, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 7 + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);

            //crossroad2 - trafficlight - right
            g.DrawRectangle(WhitePen, x + length * 12 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length * 12 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 12 + FreeSpace + TrafficLights_width, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 12 + FreeSpace + TrafficLights_width * 2, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            //crossroad2 - trafficlight - bottom
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_height);
            g.FillEllipse(red, x + length * 11 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 11 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 11 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);

            #endregion

            //left T
            #region Left T
            //left T - trafficlight - top
            g.DrawRectangle(WhitePen, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace, TrafficLights_width, TrafficLights_height);
            g.FillEllipse(red, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            //left T - trafficlight - left
            g.DrawRectangle(WhitePen, x + length + 3 * FreeSpace, y + length * 11 + FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length + 3 * FreeSpace + TrafficLights_width * 2, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length + 3 * FreeSpace + TrafficLights_width, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length + 3 * FreeSpace, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);

            //left T - trafficlight - right
            g.DrawRectangle(WhitePen, x + length * 6 + FreeSpace, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length * 6 + FreeSpace, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 6 + FreeSpace + TrafficLights_width, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 6 + FreeSpace + TrafficLights_width * 2, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            #endregion

            //right T
            #region Right T
            //right T - trafficlight - top
            g.DrawRectangle(WhitePen, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace, TrafficLights_width, TrafficLights_height);
            g.FillEllipse(red, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            //right T - trafficlight - left
            g.DrawRectangle(WhitePen, x + length * 7 + 3 * FreeSpace, y + length * 11 + FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length * 7 + 3 * FreeSpace + TrafficLights_width * 2, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 7 + 3 * FreeSpace + TrafficLights_width, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 7 + 3 * FreeSpace, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);

            //right T - trafficlight - right
            g.DrawRectangle(WhitePen, x + length * 12 + FreeSpace, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length * 12 + FreeSpace, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 12 + FreeSpace + TrafficLights_width, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 12 + FreeSpace + TrafficLights_width * 2, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            #endregion

            #endregion

            //CrosswalkLights
            #region CrosswalkLights 

            //crossroad1
            #region Crossroad1
            //top
            //crossroad1 - CrosswalkLights - top - left
            g.DrawRectangle(WhitePen, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad1 - CrosswalkLights - top - right
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //bottom
            //crossroad1 - CrosswalkLights - bottom - left
            g.DrawRectangle(WhitePen, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad1 - CrosswalkLights - bottom - right
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //left
            //crossroad1 - CrosswalkLights - left - top
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad1 - CrosswalkLights - left - bottom
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //right
            //crossroad1 - CrosswalkLights - right - top
            g.DrawRectangle(WhitePen, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad1 - CrosswalkLights - right - bottom
            g.DrawRectangle(WhitePen, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            #endregion

            //crossroad2
            #region Crossroad2
            //top
            //crossroad2 - CrosswalkLights - top - left
            g.DrawRectangle(WhitePen, x + length * 8 + crosswalk_width + 3 * FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 8 + crosswalk_width + 3 * FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 8 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad2 - CrosswalkLights - top - right
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 11 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 11 + FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //bottom
            //crossroad2 - CrosswalkLights - bottom - left
            g.DrawRectangle(WhitePen, x + length * 8 + crosswalk_width + 3 * FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 8 + crosswalk_width + 3 * FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 8 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad2 - CrosswalkLights - bottom - right
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 11 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 11 + FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //left
            //crossroad2 - CrosswalkLights - left - top
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 8 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 8 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad2 - CrosswalkLights - left - bottom
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 8 + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 8 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //right
            //crossroad2 - CrosswalkLights - right - top
            g.DrawRectangle(WhitePen, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad2 - CrosswalkLights - right - bottom
            g.DrawRectangle(WhitePen, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            #endregion

            //left T
            #region Left T
            //left T - CrosswalkLights - top - left
            g.DrawRectangle(WhitePen, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //left T - CrosswalkLights - top - right
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //left T - CrosswalkLights - left - top
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 2 + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //left T - CrosswalkLights - left - bottom
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + length * 11 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 2 + FreeSpace, y + length * 11 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + FreeSpace, y + length * 11 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //left T - CrosswalkLights - right - top 
            g.DrawRectangle(WhitePen, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //left T - CrosswalkLights - right - bottom
            g.DrawRectangle(WhitePen, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 11 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 11 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 11 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            #endregion

            //right T
            #region Right T
            //right T - CrosswalkLights - top - left
            g.DrawRectangle(WhitePen, x + length * 8 + crosswalk_width + 3 * FreeSpace, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 8 + crosswalk_width + 3 * FreeSpace, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 8 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //right T - CrosswalkLights - top - right
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 11 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 11 + FreeSpace, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //right T - CrosswalkLights - left - top
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 8 + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 8 + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //right T - CrosswalkLights - left - botom
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + length * 11 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 8 + FreeSpace, y + length * 11 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 8 + FreeSpace, y + length * 11 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //right T - CrosswalkLights - right - top
            g.DrawRectangle(WhitePen, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //right T - CrosswalkLights - right - bottom
            g.DrawRectangle(WhitePen, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 11 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 11 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 11 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            #endregion


            #endregion
                      
        }

        #endregion

        private void UserControlCrossroad_Load(object sender, EventArgs e)
        {

        }
    }



}
