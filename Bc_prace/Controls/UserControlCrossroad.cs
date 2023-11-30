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
        private float lenght = 80;
        //private float lenght_gap = 10; //nebudeme řešit :)
        private float lenght_interrupted = 20;
        private float TrafficLights_height = 60;
        private float TrafficLights_width = 20;
        private float crosswalk_width = 20;
        private float crosswalk_height = 60;
        private float TrafficLightsCrosswalk_width = 20;
        private float TrafficLightsCrosswalk_height = 40;
        private float FreeSpace = 5;

        /*
        DCircle dCircle = new DCircle()
        {
            Center = new DPoint(x + TrafficLights_width / 2, y + TrafficLights_width / 2),
            Diameter = TrafficLights_width,
            Color = Color.White,
            PenWidth = 1,
            Solid = true,
            Visible = true,
        };
        */

        #endregion


        public UserControlCrossroad()
        {
            InitializeComponent();
            DoubleBuffered = true;
            Paint += UserControl1_Paint;

            /*
            DCircle dCircle = new DCircle()
            {
                Center = new DPoint(x + TrafficLights_width / 2, y + TrafficLights_width / 2),
                Diameter = TrafficLights_width,
                Color = Color.Red,
                PenWidth = 1,
                Solid = true,
                SolidColor = Color.Red,
                Visible = true,
            };
            */

        }

        private void UserControl1_Paint(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            //background
            g.Clear(Color.Black);

            //pen color
            Pen BlackPen = new Pen(Color.Black, 2);
            Pen WhitePen = new Pen(Color.White, 2);

            BasicCrossroad();
            CrossroadExtension1();
            CrossroadExtension2();  
            CrossroadExtension3(); 

            //crossroad - left up corner
            //vertical line
            g.DrawLine(WhitePen, x + lenght * 3, y, x + lenght * 3, y + lenght);
            g.DrawLine(WhitePen, x + lenght * 3, y + lenght, x + lenght * 3, y + lenght * 2);
            g.DrawLine(WhitePen, x + lenght * 3, y + lenght * 2, x + lenght * 3, y + lenght * 3);
            //horizontal line
            g.DrawLine(WhitePen, x, y + lenght * 3, x + lenght, y + lenght * 3);
            g.DrawLine(WhitePen, x + lenght, y + lenght * 3, x + lenght * 2, y + lenght * 3);
            g.DrawLine(WhitePen, x + lenght * 2, y + lenght * 3, x + lenght * 3, y + lenght * 3);

            //white line - crossorad1 - left - internal
            g.DrawLine(WhitePen, x + lenght * 3, y + lenght * 3, x + lenght * 3, y + lenght * 4); //up
            g.DrawLine(WhitePen, x + lenght * 3, y + lenght * 4, x + lenght * 3, y + lenght * 5); //down
            //white line - crossroad1 - left - external
            g.DrawLine(WhitePen, x + lenght * 2, y + lenght * 3, x + lenght * 2, y + lenght * 4); //up
            g.DrawLine(WhitePen, x + lenght * 2, y + lenght * 4, x + lenght * 2, y + lenght * 5); //down

            /*
            //interrupted line - horizontal
            g.DrawLine(WhitePen, x, y + lenght * 3, x + lenght_interrupted, y + lenght * 3);
            g.DrawLine(WhitePen, x + lenght_interrupted * 2, y + lenght * 3, x + lenght_interrupted * 3, y + lenght * 3);
            g.DrawLine(WhitePen, x + lenght_interrupted * 4, y + lenght * 3, x + lenght_interrupted * 5, y + lenght * 3);
            g.DrawLine(WhitePen, x + lenght_interrupted * 6, y + lenght * 3, x + lenght_interrupted * 7, y + lenght * 3);
            g.DrawLine(WhitePen, x + lenght_interrupted * 8, y + lenght * 3, x + lenght_interrupted * 9, y + lenght * 3);
            g.DrawLine(WhitePen, x + lenght_interrupted * 10, y + lenght * 3, x + lenght_interrupted * 11, y + lenght * 3);
            g.DrawLine(WhitePen, x + lenght_interrupted * 12, y + lenght * 3, x + lenght_interrupted * 13, y + lenght * 3);
            g.DrawLine(WhitePen, x + lenght_interrupted * 14, y + lenght * 3, x + lenght_interrupted * 15, y + lenght * 3);
            g.DrawLine(WhitePen, x + lenght_interrupted * 16, y + lenght * 3, x + lenght_interrupted * 17, y + lenght * 3);
            g.DrawLine(WhitePen, x + lenght_interrupted * 18, y + lenght * 3, x + lenght_interrupted * 19, y + lenght * 3);
            g.DrawLine(WhitePen, x + lenght_interrupted * 20, y + lenght * 3, x + lenght_interrupted * 21, y + lenght * 3);
            g.DrawLine(WhitePen, x + lenght_interrupted * 22, y + lenght * 3, x + lenght_interrupted * 23, y + lenght * 3);
            g.DrawLine(WhitePen, x + lenght_interrupted * 24, y + lenght * 3, x + lenght_interrupted * 25, y + lenght * 3);
            g.DrawLine(WhitePen, x + lenght_interrupted * 26, y + lenght * 3, x + lenght_interrupted * 27, y + lenght * 3);
            g.DrawLine(WhitePen, x + lenght_interrupted * 28, y + lenght * 3, x + lenght_interrupted * 29, y + lenght * 3);
            g.DrawLine(WhitePen, x + lenght_interrupted * 30, y + lenght * 3, x + lenght_interrupted * 31, y + lenght * 3);
            g.DrawLine(WhitePen, x + lenght_interrupted * 32, y + lenght * 3, x + lenght_interrupted * 33, y + lenght * 3);
            g.DrawLine(WhitePen, x + lenght_interrupted * 34, y + lenght * 3, x + lenght_interrupted * 35, y + lenght * 3);
            g.DrawLine(WhitePen, x + lenght_interrupted * 36, y + lenght * 3, x + lenght_interrupted * 37, y + lenght * 3);
            g.DrawLine(WhitePen, x + lenght_interrupted * 38, y + lenght * 3, x + lenght_interrupted * 39, y + lenght * 3);
            g.DrawLine(WhitePen, x + lenght_interrupted * 40, y + lenght * 3, x + lenght_interrupted * 41, y + lenght * 3);
            g.DrawLine(WhitePen, x + lenght_interrupted * 42, y + lenght * 3, x + lenght_interrupted * 43, y + lenght * 3);
            //g.DrawLine(WhitePen, x + lenght_interrupted * 44, y + lenght * 3, x + lenght_interrupted * 45, y + lenght * 3);
            //g.DrawLine(WhitePen, x + lenght_interrupted * 46, y + lenght * 3, x + lenght_interrupted * 47, y + lenght * 3);
            //g.DrawLine(WhitePen, x + lenght_interrupted * 48, y + lenght * 3, x + lenght_interrupted * 49, y + lenght * 3);
            //g.DrawLine(WhitePen, x + lenght_interrupted * 50, y + lenght * 3, x + lenght_interrupted * 51, y + lenght * 3);
            */

            //crossroad - left down corner
            //horizontal line
            g.DrawLine(WhitePen, x, y + lenght * 5, x + lenght, y + lenght * 5);
            g.DrawLine(WhitePen, x + lenght, y + lenght * 5, x + lenght * 2, y + lenght * 5);
            g.DrawLine(WhitePen, x + lenght * 2, y + lenght * 5, x + lenght * 3, y + lenght * 5);
            //vertical line
            g.DrawLine(WhitePen, x + lenght * 3, y + lenght * 5, x + lenght * 3, y + lenght * 6);
            g.DrawLine(WhitePen, x + lenght * 3, y + lenght * 6, x + lenght * 3, y + lenght * 7);
            g.DrawLine(WhitePen, x + lenght * 3, y + lenght * 7, x + lenght * 3, y + lenght * 8);
            g.DrawLine(WhitePen, x + lenght * 3, y + lenght * 8, x + lenght * 3, y + lenght * 9);

            //white line - crossroad1 - top
            //internal
            g.DrawLine(WhitePen, x + lenght * 3, y + lenght * 3, x + lenght * 4, y + lenght * 3); //left
            g.DrawLine(WhitePen, x + lenght * 4, y + lenght * 3, x + lenght * 5, y + lenght * 3); //right
            //exteral
            g.DrawLine(WhitePen, x + lenght * 3, y + lenght * 2, x + lenght * 4, y + lenght * 2); //left
            g.DrawLine(WhitePen, x + lenght * 4, y + lenght * 2, x + lenght * 5, y + lenght * 2); //right
            
            //crossroad - mid up - left
            g.DrawLine(WhitePen, x + lenght * 5, y, x + lenght * 5, y + lenght);
            g.DrawLine(WhitePen, x + lenght * 5, y + lenght, x + lenght * 5, y + lenght * 2);
            g.DrawLine(WhitePen, x + lenght * 5, y + lenght * 2, x + lenght * 5, y + lenght * 3);
             
            //crossroad - mid up - mid 
            g.DrawLine(WhitePen, x + lenght * 5, y + lenght * 3, x + lenght * 6, y + lenght * 3);
            g.DrawLine(WhitePen, x + lenght * 6, y + lenght * 3, x + lenght * 7, y + lenght * 3);
            g.DrawLine(WhitePen, x + lenght * 7, y + lenght * 3, x + lenght * 8, y + lenght * 3);
            g.DrawLine(WhitePen, x + lenght * 8, y + lenght * 3, x + lenght * 9, y + lenght * 3);
            //jednoduše
            //g.DrawLine(WhitePen, x + lenght * 5, y + lenght * 3, x + lenght * 9, y + lenght * 3);

            //crossroad - mid up - right 
            g.DrawLine(WhitePen, x + lenght * 9, y, x + lenght * 9, y + lenght);
            g.DrawLine(WhitePen, x + lenght * 9, y + lenght, x + lenght * 9, y + lenght * 2);
            g.DrawLine(WhitePen, x + lenght * 9, y + lenght * 2, x + lenght * 9, y + lenght * 3);

            /*
            //interrupted line - vertical 1
            g.DrawLine(WhitePen, x + lenght * 3, y, x + lenght * 3, y + lenght_interrupted);
            g.DrawLine(WhitePen, x + lenght * 3, y + lenght_interrupted * 2, x + lenght * 3, y + lenght_interrupted * 3);
            g.DrawLine(WhitePen, x + lenght * 3, y + lenght_interrupted * 4, x + lenght * 3, y + lenght_interrupted * 5);
            g.DrawLine(WhitePen, x + lenght * 3, y + lenght_interrupted * 6, x + lenght * 3, y + lenght_interrupted * 7);
            g.DrawLine(WhitePen, x + lenght * 3, y + lenght_interrupted * 8, x + lenght * 3, y + lenght_interrupted * 9);
            g.DrawLine(WhitePen, x + lenght * 3, y + lenght_interrupted * 10, x + lenght * 3, y + lenght_interrupted * 11);
            g.DrawLine(WhitePen, x + lenght * 3, y + lenght_interrupted * 12, x + lenght * 3, y + lenght_interrupted * 13);
            g.DrawLine(WhitePen, x + lenght * 3, y + lenght_interrupted * 14, x + lenght * 3, y + lenght_interrupted * 15);
            g.DrawLine(WhitePen, x + lenght * 3, y + lenght_interrupted * 16, x + lenght * 3, y + lenght_interrupted * 17);
            g.DrawLine(WhitePen, x + lenght * 3, y + lenght_interrupted * 18, x + lenght * 3, y + lenght_interrupted * 19);
            g.DrawLine(WhitePen, x + lenght * 3, y + lenght_interrupted * 20, x + lenght * 3, y + lenght_interrupted * 21);
            g.DrawLine(WhitePen, x + lenght * 3, y + lenght_interrupted * 22, x + lenght * 3, y + lenght_interrupted * 23);
            g.DrawLine(WhitePen, x + lenght * 3, y + lenght_interrupted * 24, x + lenght * 3, y + lenght_interrupted * 25);
            g.DrawLine(WhitePen, x + lenght * 3, y + lenght_interrupted * 26, x + lenght * 3, y + lenght_interrupted * 27);
            //g.DrawLine(WhitePen, x + lenght * 3, y + lenght_interrupted * 28, x + lenght * 3, y + lenght_interrupted * 29);
            //g.DrawLine(WhitePen, x + lenght * 3, y + lenght_interrupted * 30, x + lenght * 3, y + lenght_interrupted * 31);
            //g.DrawLine(WhitePen, x + lenght * 3, y + lenght_interrupted * 32, x + lenght * 3, y + lenght_interrupted * 33);
            //g.DrawLine(WhitePen, x + lenght * 3, y + lenght_interrupted * 34, x + lenght * 3, y + lenght_interrupted * 35);
            */

            //white line - crossroad1 - right
            //internal line
            g.DrawLine(WhitePen, x + lenght * 5, y + lenght * 3, x + lenght * 5, y + lenght * 4); //up
            g.DrawLine(WhitePen, x + lenght * 5, y + lenght * 4, x + lenght * 5, y + lenght * 5); //down
            //external line
            g.DrawLine(WhitePen, x + lenght * 6, y + lenght * 3, x + lenght * 6, y + lenght * 4); //up
            g.DrawLine(WhitePen, x + lenght * 6, y + lenght * 4, x + lenght * 6, y + lenght * 5); //down

            //crossroad - mid down - left
            g.DrawLine(WhitePen, x + lenght * 5, y + lenght * 5, x + lenght * 5, y + lenght * 6);          
            g.DrawLine(WhitePen, x + lenght * 5, y + lenght * 6, x + lenght * 5, y + lenght * 7);
            g.DrawLine(WhitePen, x + lenght * 5, y + lenght * 7, x + lenght * 5, y + lenght * 8);
            g.DrawLine(WhitePen, x + lenght * 5, y + lenght * 8, x + lenght * 5, y + lenght * 9);

            //crossroad - mid down - mid 
            g.DrawLine(WhitePen, x + lenght * 5, y + lenght * 5, x + lenght * 6, y + lenght * 5);
            g.DrawLine(WhitePen, x + lenght * 6, y + lenght * 5, x + lenght * 7, y + lenght * 5);
            g.DrawLine(WhitePen, x + lenght * 7, y + lenght * 5, x + lenght * 8, y + lenght * 5);
            g.DrawLine(WhitePen, x + lenght * 8, y + lenght * 5, x + lenght * 9, y + lenght * 5);

            //crossroad - mid down - right
            g.DrawLine(WhitePen, x + lenght * 9, y + lenght * 5, x + lenght * 9, y + lenght * 6);
            g.DrawLine(WhitePen, x + lenght * 9, y + lenght * 6, x + lenght * 9, y + lenght * 7);
            g.DrawLine(WhitePen, x + lenght * 9, y + lenght * 7, x + lenght * 9, y + lenght * 8);
            g.DrawLine(WhitePen, x + lenght * 9, y + lenght * 8, x + lenght * 9, y + lenght * 9);

            //white line - crossroad1 - bottom
            //internal
            g.DrawLine(WhitePen, x + lenght * 3, y + lenght * 5, x + lenght * 4, y + lenght * 5); //left
            g.DrawLine(WhitePen, x + lenght * 4, y + lenght * 5, x + lenght * 5, y + lenght * 5); //right
            //external
            g.DrawLine(WhitePen, x + lenght * 3, y + lenght * 6, x + lenght * 4, y + lenght * 6); //left
            g.DrawLine(WhitePen, x + lenght * 4, y + lenght * 6, x + lenght * 5, y + lenght * 6); //right

            //white line - crossroad2 - left
            //exteranl
            g.DrawLine(WhitePen, x + lenght * 8, y + lenght * 3, x + lenght * 8, y + lenght * 4); //up
            g.DrawLine(WhitePen, x + lenght * 8, y + lenght * 4, x + lenght * 8, y + lenght * 5); //down
            //internal
            g.DrawLine(WhitePen, x + lenght * 9, y + lenght * 3, x + lenght * 9, y + lenght * 4); //up
            g.DrawLine(WhitePen, x + lenght * 9, y + lenght * 4, x + lenght * 9, y + lenght * 5); //down 

            //crossroad - right up corner
            //vertical line
            g.DrawLine(WhitePen, x + lenght * 11, y, x + lenght * 11, y + lenght);
            g.DrawLine(WhitePen, x + lenght * 11, y + lenght, x + lenght * 11, y + lenght * 2);
            g.DrawLine(WhitePen, x + lenght * 11, y + lenght * 2, x + lenght * 11, y + lenght * 3);
            //horizontal line
            g.DrawLine(WhitePen, x + lenght * 11, y + lenght * 3, x + lenght * 12, y + lenght * 3);
            g.DrawLine(WhitePen, x + lenght * 12, y + lenght * 3, x + lenght * 13, y + lenght * 3);
            g.DrawLine(WhitePen, x + lenght * 13, y + lenght * 3, x + lenght * 14, y + lenght * 3);

            //white line - crossroad2 - top
            //external
            g.DrawLine(WhitePen, x + lenght * 9, y + lenght * 2, x + lenght * 10, y + lenght * 2); //left
            g.DrawLine(WhitePen, x + lenght * 10, y + lenght * 2, x + lenght * 11, y + lenght * 2); //right
            //internal
            g.DrawLine(WhitePen, x + lenght * 9, y + lenght * 3, x + lenght * 10, y + lenght * 3); //left
            g.DrawLine(WhitePen, x + lenght * 10, y + lenght * 3, x + lenght * 11, y + lenght * 3); //right

            //crossroad - right down corner
            //vertical line
            g.DrawLine(WhitePen, x + lenght * 11, y + lenght * 5, x + lenght * 11, y + lenght * 6);
            g.DrawLine(WhitePen, x + lenght * 11, y + lenght * 6, x + lenght * 11, y + lenght * 7);
            g.DrawLine(WhitePen, x + lenght * 11, y + lenght * 7, x + lenght * 11, y + lenght * 8);
            g.DrawLine(WhitePen, x + lenght * 11, y + lenght * 8, x + lenght * 11, y + lenght * 9);
            //horizontal line
            g.DrawLine(WhitePen, x + lenght * 11, y + lenght * 5, x + lenght * 12, y + lenght * 5);
            g.DrawLine(WhitePen, x + lenght * 12, y + lenght * 5, x + lenght * 13, y + lenght * 5);
            g.DrawLine(WhitePen, x + lenght * 13, y + lenght * 5, x + lenght * 14, y + lenght * 5);

            //white line - crossroad2 - bottom
            //internal
            g.DrawLine(WhitePen, x + lenght * 9, y + lenght * 5, x + lenght * 10, y + lenght * 5); //left
            g.DrawLine(WhitePen, x + lenght * 10, y + lenght * 5, x + lenght * 11, y + lenght * 5); //right
            //external
            g.DrawLine(WhitePen, x + lenght * 9, y + lenght * 6, x + lenght * 10, y + lenght * 6); //left
            g.DrawLine(WhitePen, x + lenght * 10, y + lenght * 6, x + lenght * 11, y + lenght * 6); //right

            //white line - crossroad2 - right
            //internal
            g.DrawLine(WhitePen, x + lenght * 11, y + lenght * 3, x + lenght * 11, y + lenght * 4); //up
            g.DrawLine(WhitePen, x + lenght * 11, y + lenght * 4, x + lenght * 11, y + lenght * 5); //down
            //external
            g.DrawLine(WhitePen, x + lenght * 12, y + lenght * 3, x + lenght * 12, y + lenght * 4); //up
            g.DrawLine(WhitePen, x + lenght * 12, y + lenght * 4, x + lenght * 12, y + lenght * 5); //down

            /*
            //interrupted line - vertical 2
            g.DrawLine(WhitePen, x + lenght * 8, y, x + lenght * 8, y + lenght_interrupted);
            g.DrawLine(WhitePen, x + lenght * 8, y + lenght_interrupted * 2, x + lenght * 8, y + lenght_interrupted * 3);
            g.DrawLine(WhitePen, x + lenght * 8, y + lenght_interrupted * 4, x + lenght * 8, y + lenght_interrupted * 5);
            g.DrawLine(WhitePen, x + lenght * 8, y + lenght_interrupted * 6, x + lenght * 8, y + lenght_interrupted * 7);
            g.DrawLine(WhitePen, x + lenght * 8, y + lenght_interrupted * 8, x + lenght * 8, y + lenght_interrupted * 9);
            g.DrawLine(WhitePen, x + lenght * 8, y + lenght_interrupted * 10, x + lenght * 8, y + lenght_interrupted * 11);
            g.DrawLine(WhitePen, x + lenght * 8, y + lenght_interrupted * 12, x + lenght * 8, y + lenght_interrupted * 13);
            g.DrawLine(WhitePen, x + lenght * 8, y + lenght_interrupted * 14, x + lenght * 8, y + lenght_interrupted * 15);
            g.DrawLine(WhitePen, x + lenght * 8, y + lenght_interrupted * 16, x + lenght * 8, y + lenght_interrupted * 17);
            g.DrawLine(WhitePen, x + lenght * 8, y + lenght_interrupted * 18, x + lenght * 8, y + lenght_interrupted * 19);
            g.DrawLine(WhitePen, x + lenght * 8, y + lenght_interrupted * 20, x + lenght * 8, y + lenght_interrupted * 21);
            g.DrawLine(WhitePen, x + lenght * 8, y + lenght_interrupted * 22, x + lenght * 8, y + lenght_interrupted * 23);
            g.DrawLine(WhitePen, x + lenght * 8, y + lenght_interrupted * 24, x + lenght * 8, y + lenght_interrupted * 25);
            g.DrawLine(WhitePen, x + lenght * 8, y + lenght_interrupted * 26, x + lenght * 8, y + lenght_interrupted * 27);
            //g.DrawLine(WhitePen, x + lenght * 8, y + lenght_interrupted * 28, x + lenght * 8, y + lenght_interrupted * 29);
            //g.DrawLine(WhitePen, x + lenght * 8, y + lenght_interrupted * 30, x + lenght * 8, y + lenght_interrupted * 31);
            //g.DrawLine(WhitePen, x + lenght * 8, y + lenght_interrupted * 32, x + lenght * 8, y + lenght_interrupted * 33);
            //g.DrawLine(WhitePen, x + lenght * 8, y + lenght_interrupted * 34, x + lenght * 8, y + lenght_interrupted * 35);
            */

            //crossroad - T - left 
            g.DrawLine(WhitePen, x, y + lenght * 9, x + lenght, y + lenght * 9);
            g.DrawLine(WhitePen, x + lenght, y + lenght * 9, x + lenght * 2, y + lenght * 9);
            g.DrawLine(WhitePen, x + lenght * 2, y + lenght * 9, x + lenght * 3, y + lenght * 9);

            //crossroad - T - mid 
            g.DrawLine(WhitePen, x + lenght * 5, y + lenght * 9, x + lenght * 6, y + lenght * 9);
            g.DrawLine(WhitePen, x + lenght * 6, y + lenght * 9, x + lenght * 7, y + lenght * 9);
            g.DrawLine(WhitePen, x + lenght * 7, y + lenght * 9, x + lenght * 8, y + lenght * 9);
            g.DrawLine(WhitePen, x + lenght * 8, y + lenght * 9, x + lenght * 9, y + lenght * 9);

            //crossroad - T - bottom 
            g.DrawLine(WhitePen, x, y + lenght * 11, x + lenght * 14, y + lenght * 11);

            //crossroad - T - right
            g.DrawLine(WhitePen, x + lenght * 11, y + lenght * 9, x + lenght * 12, y + lenght * 9);
            g.DrawLine(WhitePen, x + lenght * 12, y + lenght * 9, x + lenght * 13, y + lenght * 9);
            g.DrawLine(WhitePen, x + lenght * 13, y + lenght * 9, x + lenght * 14, y + lenght * 9);

            //whiteline - T
            //left
            g.DrawLine(WhitePen, x + lenght * 2, y + lenght * 10, x + lenght * 2, y + lenght * 11);
            //top
            g.DrawLine(WhitePen, x + lenght * 3, y + lenght * 8, x + lenght * 4, y + lenght * 8);
            //right
            g.DrawLine(WhitePen, x + lenght * 6, y + lenght * 9, x + lenght * 6, y + lenght * 10);

            //Center lines
            #region Center lines

            //vertical center line - left
            //top
            g.DrawLine(WhitePen, x + lenght * 4, y, x + lenght * 4, y + lenght_interrupted);
            g.DrawLine(WhitePen, x + lenght * 4, y + lenght_interrupted * 2, x + lenght * 4, y + lenght_interrupted * 3);
            //g.DrawLine(WhitePen, x + lenght * 4, y + lenght_interrupted * 4, x + lenght * 4, y + lenght_interrupted * 5);
            //g.DrawLine(WhitePen, x + lenght * 4, y + lenght_interrupted * 6, x + lenght * 4, y + lenght_interrupted * 7);
            g.DrawLine(WhitePen, x + lenght * 4, y + lenght, x + lenght * 4, y + lenght * 2);
            //bottom
            g.DrawLine(WhitePen, x + lenght * 4, y + lenght * 6, x + lenght * 4, y + lenght * 8);

            //vertical center line - right
            g.DrawLine(WhitePen, x + lenght * 10, y, x + lenght * 10, y + lenght_interrupted);
            g.DrawLine(WhitePen, x + lenght * 10, y + lenght_interrupted * 2, x + lenght * 10, y + lenght_interrupted * 3);
            //g.DrawLine(WhitePen, x + lenght * 4, y + lenght_interrupted * 4, x + lenght * 4, y + lenght_interrupted * 5);
            //g.DrawLine(WhitePen, x + lenght * 4, y + lenght_interrupted * 6, x + lenght * 4, y + lenght_interrupted * 7);
            g.DrawLine(WhitePen, x + lenght * 10, y + lenght, x + lenght * 10, y + lenght * 2);
            //bottom
            g.DrawLine(WhitePen, x + lenght * 10, y + lenght * 6, x + lenght * 10, y + lenght * 8);

            //horizontal center line - top 
            g.DrawLine(WhitePen, x, y + lenght * 3, x + lenght_interrupted, y + lenght * 3);
            g.DrawLine(WhitePen, x + lenght_interrupted * 2, y + lenght * 3, x + lenght_interrupted * 3, y + lenght * 3);
            //g.DrawLine(WhitePen, x + lenght_interrupted * 4, y + lenght * 3, x + lenght_interrupted * 5, y + lenght * 3);
            //g.DrawLine(WhitePen, x + lenght_interrupted * 6, y + lenght * 3, x + lenght_interrupted * 7, y + lenght * 3);
            
            g.DrawLine(WhitePen, x + lenght, y + lenght * 10, x + lenght * 2, y + lenght * 10);
            g.DrawLine(WhitePen, x + lenght * 6, y + lenght * 10, x + lenght * 8, y + lenght * 10);

            g.DrawLine(WhitePen, x + lenght * 12, y + lenght * 10, x + lenght * 13, y + lenght * 10);

            g.DrawLine(WhitePen, x + lenght_interrupted * 36, y + lenght * 10, x + lenght_interrupted * 37, y + lenght * 10);
            g.DrawLine(WhitePen, x + lenght_interrupted * 38, y + lenght * 10, x + lenght_interrupted * 39, y + lenght * 10);
            g.DrawLine(WhitePen, x + lenght_interrupted * 40, y + lenght * 10, x + lenght_interrupted * 41, y + lenght * 10);
            g.DrawLine(WhitePen, x + lenght_interrupted * 42, y + lenght * 10, x + lenght_interrupted * 43, y + lenght * 10);
            //g.DrawLine(WhitePen, x + lenght_interrupted * 44, y + lenght * 3, x + lenght_interrupted * 45, y + lenght * 3);
            //g.DrawLine(WhitePen, x + lenght_interrupted * 46, y + lenght * 3, x + lenght_interrupted * 47, y + lenght * 3);
            //g.DrawLine(WhitePen, x + lenght_interrupted * 48, y + lenght * 3, x + lenght_interrupted * 49, y + lenght * 3);
            //g.DrawLine(WhitePen, x + lenght_interrupted * 50, y + lenght * 3, x + lenght_interrupted * 51, y + lenght * 3);

            //horizontal center line - bottom 
            g.DrawLine(WhitePen, x, y + lenght * 10, x + lenght_interrupted, y + lenght * 10);
            g.DrawLine(WhitePen, x + lenght_interrupted * 2, y + lenght * 10, x + lenght_interrupted * 3, y + lenght * 10);
            //g.DrawLine(WhitePen, x + lenght_interrupted * 4, y + lenght * 3, x + lenght_interrupted * 5, y + lenght * 3);
            //g.DrawLine(WhitePen, x + lenght_interrupted * 6, y + lenght * 3, x + lenght_interrupted * 7, y + lenght * 3);

            g.DrawLine(WhitePen, x + lenght, y + lenght * 10, x + lenght * 2, y + lenght * 10);
            g.DrawLine(WhitePen, x + lenght * 6, y + lenght * 10, x + lenght * 8, y + lenght * 10);

            g.DrawLine(WhitePen, x + lenght * 12, y + lenght * 10, x + lenght * 13, y + lenght * 10);

            g.DrawLine(WhitePen, x + lenght_interrupted * 36, y + lenght * 10, x + lenght_interrupted * 37, y + lenght * 10);
            g.DrawLine(WhitePen, x + lenght_interrupted * 38, y + lenght * 10, x + lenght_interrupted * 39, y + lenght * 10);
            g.DrawLine(WhitePen, x + lenght_interrupted * 40, y + lenght * 10, x + lenght_interrupted * 41, y + lenght * 10);
            g.DrawLine(WhitePen, x + lenght_interrupted * 42, y + lenght * 10, x + lenght_interrupted * 43, y + lenght * 10);
            //g.DrawLine(WhitePen, x + lenght_interrupted * 44, y + lenght * 3, x + lenght_interrupted * 45, y + lenght * 3);
            //g.DrawLine(WhitePen, x + lenght_interrupted * 46, y + lenght * 3, x + lenght_interrupted * 47, y + lenght * 3);
            //g.DrawLine(WhitePen, x + lenght_interrupted * 48, y + lenght * 3, x + lenght_interrupted * 49, y + lenght * 3);
            //g.DrawLine(WhitePen, x + lenght_interrupted * 50, y + lenght * 3, x + lenght_interrupted * 51, y + lenght * 3);


            #endregion

            //Crosswalks
            #region Crosswalks

            //crossroad1 - top
            g.DrawRectangle(WhitePen, x + lenght * 3 + FreeSpace, y + lenght * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + lenght * 3 + 2 * FreeSpace + crosswalk_width, y + lenght * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + lenght * 3 + 3 * FreeSpace + 2 * crosswalk_width, y + lenght * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + lenght * 4 + FreeSpace, y + lenght * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + lenght * 4 + 2 * FreeSpace + crosswalk_width, y + lenght * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + lenght * 4 + 3 * FreeSpace + 2 * crosswalk_width, y + lenght * 2 + FreeSpace, crosswalk_width, crosswalk_height);

            //crossroad1 - left
            g.DrawRectangle(WhitePen, x + lenght * 2 + FreeSpace, y + 3 * lenght + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + lenght * 2 + FreeSpace, y + 3 * lenght + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + lenght * 2 + FreeSpace, y + 3 * lenght + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + lenght * 2 + FreeSpace, y + 4 * lenght + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + lenght * 2 + FreeSpace, y + 4 * lenght + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + lenght * 2 + FreeSpace, y + 4 * lenght + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);

            //crossroad1 - right
            g.DrawRectangle(WhitePen, x + lenght * 5 + FreeSpace, y + 3 * lenght + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + lenght * 5 + FreeSpace, y + 3 * lenght + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + lenght * 5 + FreeSpace, y + 3 * lenght + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + lenght * 5 + FreeSpace, y + 4 * lenght + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + lenght * 5 + FreeSpace, y + 4 * lenght + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + lenght * 5 + FreeSpace, y + 4 * lenght + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);

            //crossroad1 - bottom
            g.DrawRectangle(WhitePen, x + lenght * 3 + FreeSpace, y + lenght * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + lenght * 3 + 2 * FreeSpace + crosswalk_width, y + lenght * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + lenght * 3 + 3 * FreeSpace + 2 * crosswalk_width, y + lenght * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + lenght * 4 + FreeSpace, y + lenght * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + lenght * 4 + 2 * FreeSpace + crosswalk_width, y + lenght * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + lenght * 4 + 3 * FreeSpace + 2 * crosswalk_width, y + lenght * 5 + FreeSpace, crosswalk_width, crosswalk_height);

            //crossroad2 - top
            g.DrawRectangle(WhitePen, x + lenght * 9 + FreeSpace, y + lenght * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + lenght * 9 + 2 * FreeSpace + crosswalk_width, y + lenght * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + lenght * 9 + 3 * FreeSpace + 2 * crosswalk_width, y + lenght * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + lenght * 10 + FreeSpace, y + lenght * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + lenght * 10 + 2 * FreeSpace + crosswalk_width, y + lenght * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + lenght * 10 + 3 * FreeSpace + 2 * crosswalk_width, y + lenght * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            
            //crossroad2 - bottom
            g.DrawRectangle(WhitePen, x + lenght * 9 + FreeSpace, y + lenght * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + lenght * 9 + 2 * FreeSpace + crosswalk_width, y + lenght * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + lenght * 9 + 3 * FreeSpace + 2 * crosswalk_width, y + lenght * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + lenght * 10 + FreeSpace, y + lenght * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + lenght * 10 + 2 * FreeSpace + crosswalk_width, y + lenght * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + lenght * 10 + 3 * FreeSpace + 2 * crosswalk_width, y + lenght * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            
            //crossroad2 - left
            g.DrawRectangle(WhitePen, x + lenght * 8 + FreeSpace, y + 3 * lenght + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + lenght * 8 + FreeSpace, y + 3 * lenght + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + lenght * 8 + FreeSpace, y + 3 * lenght + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + lenght * 8 + FreeSpace, y + 4 * lenght + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + lenght * 8 + FreeSpace, y + 4 * lenght + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + lenght * 8 + FreeSpace, y + 4 * lenght + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);

            //crossroad2 - right
            g.DrawRectangle(WhitePen, x + lenght * 11 + FreeSpace, y + 3 * lenght + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + lenght * 11 + FreeSpace, y + 3 * lenght + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + lenght * 11 + FreeSpace, y + 3 * lenght + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + lenght * 11 + FreeSpace, y + 4 * lenght + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + lenght * 11 + FreeSpace, y + 4 * lenght + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + lenght * 11 + FreeSpace, y + 4 * lenght + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            
            #endregion

            //TrafficLights
            #region trafficlights
            //crossorad1
            //crossroad1 - trafficlight - top
            g.DrawRectangle(WhitePen, x + lenght * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + lenght + 3 * FreeSpace, TrafficLights_width, TrafficLights_height);
            //crossroad1 - trafficlight - left
            g.DrawRectangle(WhitePen, x + lenght + 3 * FreeSpace, y + 5 * lenght + FreeSpace, TrafficLights_height, TrafficLights_width);
            //crossroad1 - trafficlight - right
            g.DrawRectangle(WhitePen, x + lenght * 6 + FreeSpace, y + lenght * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_height, TrafficLights_width);
            //crossroad1 - trafficlight - bottom
            g.DrawRectangle(WhitePen, x + lenght * 5 + FreeSpace, y + lenght * 6 + FreeSpace, TrafficLights_width, TrafficLights_height);



            //crossroad2
            //crossroad2 - trafficlight - top
            g.DrawRectangle(WhitePen, x + lenght * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + lenght + 3 * FreeSpace, TrafficLights_width, TrafficLights_height);
            //crossroad2 - trafficlight - left
            g.DrawRectangle(WhitePen, x + lenght * 7 + 3 * FreeSpace, y + 5 * lenght + FreeSpace, TrafficLights_height, TrafficLights_width);
            //crossroad2 - trafficlight - right
            g.DrawRectangle(WhitePen, x + lenght * 12 + FreeSpace, y + lenght * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_height, TrafficLights_width);
            //crossroad2 - trafficlight - bottom
            g.DrawRectangle(WhitePen, x + lenght * 11 + FreeSpace, y + lenght * 6 + FreeSpace, TrafficLights_width, TrafficLights_height);


            #endregion

            //CrosswalkLights
            #region CrosswalkLights 

            //crossroad1
            //top
            //crossroad1 - crosswalk - top - left
            g.DrawRectangle(WhitePen, x + lenght * 2+ crosswalk_width + 3 * FreeSpace, y + lenght * 2 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            //crossroad1 - crosswalk - top - right
            g.DrawRectangle(WhitePen, x + lenght * 5 + FreeSpace, y + lenght * 2 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);

            //bottom
            //crossroad1 - crosswalk - bottom - left
            g.DrawRectangle(WhitePen, x + lenght * 2 + crosswalk_width + 3 * FreeSpace, y + lenght * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            //crossroad1 - crosswalk - bottom - right
            g.DrawRectangle(WhitePen, x + lenght * 5 + FreeSpace, y + lenght * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);

            //left
            //crossroad1 - crosswalk - left - top
            g.DrawRectangle(WhitePen, x + lenght * 2 + FreeSpace, y + lenght * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            //crossroad1 - crosswalk - left - bottom
            g.DrawRectangle(WhitePen, x + lenght * 2 + FreeSpace, y + lenght * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            
            //right
            //crossroad1 - crosswalk - right - top
            g.DrawRectangle(WhitePen, x + lenght * 5 + 2 * crosswalk_width + FreeSpace, y + lenght * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            //crossroad1 - crosswalk - right - bottom
            g.DrawRectangle(WhitePen, x + lenght * 5 + 2 * crosswalk_width + FreeSpace, y + lenght * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            
            
            //crossroad2
            //top
            //crossroad2 - crosswalk - top - left
            g.DrawRectangle(WhitePen, x + lenght * 8 + crosswalk_width + 3 * FreeSpace, y + lenght * 2 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            //crossroad2 - crosswalk - top - right
            g.DrawRectangle(WhitePen, x + lenght * 11 + FreeSpace, y + lenght * 2 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);

            //bottom
            //crossroad2 - crosswalk - bottom - left
            g.DrawRectangle(WhitePen, x + lenght * 8 + crosswalk_width + 3 * FreeSpace, y + lenght * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            //crossroad2 - crosswalk - bottom - right
            g.DrawRectangle(WhitePen, x + lenght * 11 + FreeSpace, y + lenght * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);

            //left
            //crossroad2 - crosswalk - left - top
            g.DrawRectangle(WhitePen, x + lenght * 8 + FreeSpace, y + lenght * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            //crossroad2 - crosswalk - left - bottom
            g.DrawRectangle(WhitePen, x + lenght * 8 + FreeSpace, y + lenght * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            
            //right
            //crossroad2 - crosswalk - right - top
            g.DrawRectangle(WhitePen, x + lenght * 11 + 2 * crosswalk_width + FreeSpace, y + lenght * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            //crossroad2 - crosswalk - right - bottom
            g.DrawRectangle(WhitePen, x + lenght * 11 + 2 * crosswalk_width + FreeSpace, y + lenght * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);

            #endregion

        }

        //Methods for rendering crossroad
        #region Methods for rendering crossroad

        public void BasicCrossroad()
        {
            /*
             
            
            
            //crossroad - left up corner
            //vertical line
            g.DrawLine(WhitePen, x + lenght * 3, y, x + lenght * 3, y + lenght);
            g.DrawLine(WhitePen, x + lenght * 3, y + lenght, x + lenght * 3, y + lenght * 2);
            g.DrawLine(WhitePen, x + lenght * 3, y + lenght * 2, x + lenght * 3, y + lenght * 3);
            //horizontal line
            g.DrawLine(WhitePen, x, y + lenght * 3, x + lenght, y + lenght * 3);
            g.DrawLine(WhitePen, x + lenght, y + lenght * 3, x + lenght * 2, y + lenght * 3);
            g.DrawLine(WhitePen, x + lenght * 2, y + lenght * 3, x + lenght * 3, y + lenght * 3);
            
            //white line - crossroad1 - left - 
            g.DrawLine(WhitePen, x + lenght * 2, y + lenght * 4, x + lenght * 2, y + lenght * 5); //down

            //crossroad - left down corner
            //horizontal line
            g.DrawLine(WhitePen, x, y + lenght * 5, x + lenght, y + lenght * 5);
            g.DrawLine(WhitePen, x + lenght, y + lenght * 5, x + lenght * 2, y + lenght * 5);
            g.DrawLine(WhitePen, x + lenght * 2, y + lenght * 5, x + lenght * 3, y + lenght * 5);
            //vertical line
            g.DrawLine(WhitePen, x + lenght * 2, y + lenght * 4, x + lenght * 2, y + lenght * 5);
            g.DrawLine(WhitePen, x + lenght * 2, y + lenght * 5, x + lenght * 2, y + lenght * 6);
            g.DrawLine(WhitePen, x + lenght * 2, y + lenght * 6, x + lenght * 2, y + lenght * 7);

            //white line - crossroad1 - top
            g.DrawLine(WhitePen, x + lenght * 3, y + lenght, x + lenght * 4, y + lenght); //left
            // g.DrawLine(WhitePen, x + lenght * 4, y + lenght, x + lenght * 5, y + lenght); //right

            //crossroad - mid up - left
            g.DrawLine(WhitePen, x + lenght * 5, y, x + lenght * 5, y + lenght);
            g.DrawLine(WhitePen, x + lenght * 5, y + lenght, x + lenght * 5, y + lenght * 2);
            g.DrawLine(WhitePen, x + lenght * 5, y + lenght * 2, x + lenght * 5, y + lenght * 3);

            //crossroad - mid up - mid 
            g.DrawLine(WhitePen, x + lenght * 5, y + lenght * 3, x + lenght * 6, y + lenght * 3);
            g.DrawLine(WhitePen, x + lenght * 6, y + lenght * 3, x + lenght * 7, y + lenght * 3);
            g.DrawLine(WhitePen, x + lenght * 7, y + lenght * 3, x + lenght * 8, y + lenght * 3);
            g.DrawLine(WhitePen, x + lenght * 8, y + lenght * 3, x + lenght * 9, y + lenght * 3);
            //jednoduše
            //g.DrawLine(WhitePen, x + lenght * 5, y + lenght * 3, x + lenght * 9, y + lenght * 3);

            //crossroad - mid up - right 
            g.DrawLine(WhitePen, x + lenght * 9, y, x + lenght * 9, y + lenght);
            g.DrawLine(WhitePen, x + lenght * 9, y + lenght, x + lenght * 9, y + lenght * 2);
            g.DrawLine(WhitePen, x + lenght * 9, y + lenght * 2, x + lenght * 9, y + lenght * 3);

            //white line - crossroad1 - right
            g.DrawLine(WhitePen, x + lenght * 5, y + lenght * 3, x + lenght * 5, y + lenght * 4); //up

            //crossroad - mid down - left
            g.DrawLine(WhitePen, x + lenght * 5, y + lenght * 5, x + lenght * 5, y + lenght * 6);
            g.DrawLine(WhitePen, x + lenght * 5, y + lenght * 6, x + lenght * 5, y + lenght * 7);
            g.DrawLine(WhitePen, x + lenght * 5, y + lenght * 7, x + lenght * 5, y + lenght * 8);

            //crossroad - mid down - mid 
            g.DrawLine(WhitePen, x + lenght * 5, y + lenght * 5, x + lenght * 6, y + lenght * 5);
            g.DrawLine(WhitePen, x + lenght * 6, y + lenght * 5, x + lenght * 7, y + lenght * 5);
            g.DrawLine(WhitePen, x + lenght * 7, y + lenght * 5, x + lenght * 8, y + lenght * 5);
            g.DrawLine(WhitePen, x + lenght * 8, y + lenght * 5, x + lenght * 9, y + lenght * 5);
            //jednoduše
            //g.DrawLine(WhitePen, x + lenght * 5, y + lenght * 5, x + lenght * 9, y + lenght * 5);

            //crossroad - mid down - right
            g.DrawLine(WhitePen, x + lenght * 9, y + lenght * 5, x + lenght * 9, y + lenght * 6);
            g.DrawLine(WhitePen, x + lenght * 9, y + lenght * 6, x + lenght * 9, y + lenght * 7);
            g.DrawLine(WhitePen, x + lenght * 9, y + lenght * 7, x + lenght * 9, y + lenght * 8);

            //white line - crossroad1 - bottom
            g.DrawLine(WhitePen, x + lenght * 4, y + lenght * 6, x + lenght * 5, y + lenght * 6); //right

            */

        }

        public void CrossroadExtension1()
        {

        }

        public void CrossroadExtension2()
        {

        }

        public void CrossroadExtension3()
        {

        }


        #endregion

        private void UserControlCrossroad_Load(object sender, EventArgs e)
        {

        }
    }



}
