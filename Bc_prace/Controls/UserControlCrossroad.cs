using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bc_prace.Controls
{
    public partial class UserControlCrossroad : UserControl
    {
        //beggining points of drawing
        private float x = 15;
        private float y = 15;

        //basic parametres
        private float lenght = 120;
        //private float lenght_gap = 10;
        private float lenght_interrupted = 30;

        

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
            g.Clear(Color.LightGray);

            //pen color
            Pen BlackPen = new Pen(Color.Black, 2);
            Pen WhitePen = new Pen(Color.White, 2);

            //crossroad - left up corner
            g.DrawLine(BlackPen, x, y, x + lenght, y);
            g.DrawLine(BlackPen, x, y, x, y + lenght);
            g.DrawLine(BlackPen, x + lenght, y, x + lenght, y + lenght);
            g.DrawLine(BlackPen, x + lenght, y + lenght, x, y + lenght);

            //white line 
            g.DrawLine(WhitePen, x + lenght, y + lenght, x + lenght, y + lenght * 2);

            //interrupted line 
            g.DrawLine(BlackPen, x, y + lenght * 2, x + lenght_interrupted, y + lenght * 2);
            g.DrawLine(BlackPen, x + lenght_interrupted * 2, y + lenght * 2, x + lenght_interrupted * 3, y + lenght * 2);
            g.DrawLine(BlackPen, x + lenght_interrupted * 4, y + lenght * 2, x + lenght_interrupted * 5, y + lenght * 2);
            g.DrawLine(BlackPen, x + lenght_interrupted * 6, y + lenght * 2, x + lenght_interrupted * 7, y + lenght * 2);
            g.DrawLine(BlackPen, x + lenght_interrupted * 8, y + lenght * 2, x + lenght_interrupted * 9, y + lenght * 2);
            g.DrawLine(BlackPen, x + lenght_interrupted * 10, y + lenght * 2, x + lenght_interrupted * 11, y + lenght * 2);
            g.DrawLine(BlackPen, x + lenght_interrupted * 12, y + lenght * 2, x + lenght_interrupted * 13, y + lenght * 2);
            g.DrawLine(BlackPen, x + lenght_interrupted * 14, y + lenght * 2, x + lenght_interrupted * 15, y + lenght * 2);
            g.DrawLine(BlackPen, x + lenght_interrupted * 16, y + lenght * 2, x + lenght_interrupted * 17, y + lenght * 2);

            //white line 
            g.DrawLine(WhitePen, x + lenght, y + lenght * 2, x + lenght, y + lenght * 3);

            //crossroad - left down corner
            g.DrawLine(BlackPen, x, y + lenght * 3, x + lenght, y + lenght * 3);
            g.DrawLine(BlackPen, x, y + lenght * 3, x, y + lenght * 4);
            g.DrawLine(BlackPen, x + lenght, y + lenght * 3, x + lenght, y + lenght * 4);
            g.DrawLine(BlackPen, x + lenght, y + lenght * 4, x, y + lenght * 4);

            //white line 
            g.DrawLine(WhitePen, x + lenght, y + lenght, x + lenght * 2, y + lenght);

            //crossroad - mid up
            g.DrawLine(BlackPen, x + lenght * 3, y, x + lenght * 4, y);
            g.DrawLine(BlackPen, x + lenght * 3, y + lenght, x + lenght * 4, y + lenght);
            g.DrawLine(BlackPen, x + lenght * 3, y, x + lenght * 3, y + lenght);
            g.DrawLine(BlackPen, x + lenght * 4, y, x + lenght * 4, y + lenght);

            //white line 
            g.DrawLine(WhitePen, x + lenght * 2, y + lenght, x + lenght * 3, y + lenght);

            //interrupted line 
            g.DrawLine(BlackPen, x + lenght * 2, y, x + lenght * 2, y + lenght_interrupted);
            g.DrawLine(BlackPen, x + lenght * 2, y + lenght_interrupted * 2, x + lenght * 2, y + lenght_interrupted * 3);
            g.DrawLine(BlackPen, x + lenght * 2, y + lenght_interrupted * 4, x + lenght * 2, y + lenght_interrupted * 5);
            g.DrawLine(BlackPen, x + lenght * 2, y + lenght_interrupted * 6, x + lenght * 2, y + lenght_interrupted * 7);
            g.DrawLine(BlackPen, x + lenght * 2, y + lenght_interrupted * 8, x + lenght * 2, y + lenght_interrupted * 9);
            g.DrawLine(BlackPen, x + lenght * 2, y + lenght_interrupted * 10, x + lenght * 2, y + lenght_interrupted * 11);
            g.DrawLine(BlackPen, x + lenght * 2, y + lenght_interrupted * 12, x + lenght * 2, y + lenght_interrupted * 13);
            g.DrawLine(BlackPen, x + lenght * 2, y + lenght_interrupted * 14, x + lenght * 2, y + lenght_interrupted * 15);
            g.DrawLine(BlackPen, x + lenght * 2, y + lenght_interrupted * 16, x + lenght * 2, y + lenght_interrupted * 17);

            //white line 
            g.DrawLine(WhitePen, x + lenght, y + lenght * 3, x + lenght * 2, y + lenght * 3);

            //white line 
            g.DrawLine(WhitePen, x + lenght * 2, y + lenght * 3, x + lenght * 3, y + lenght * 3);

            //crossroad - mid down 
            g.DrawLine(BlackPen, x + lenght * 3, y + lenght * 3, x + lenght * 4, y + lenght * 3);
            g.DrawLine(BlackPen, x + lenght * 3, y + lenght * 4, x + lenght * 4, y + lenght * 4);
            g.DrawLine(BlackPen, x + lenght * 3, y + lenght * 3, x + lenght * 3, y + lenght * 4);
            g.DrawLine(BlackPen, x + lenght * 4, y + lenght * 3, x + lenght * 4, y + lenght * 4);

            //white line 
            g.DrawLine(WhitePen, x + lenght, y + lenght * 4, x + lenght * 2, y + lenght * 4);

            //white line 
            g.DrawLine(WhitePen, x + lenght * 2, y + lenght * 4, x + lenght * 3, y + lenght * 4);

            //white line 
            g.DrawLine(WhitePen, x + lenght * 3, y + lenght, x + lenght * 3, y + lenght * 2);

            //white line 
            g.DrawLine(WhitePen, x + lenght * 3, y + lenght * 2, x + lenght * 3, y + lenght * 3);

            //white line 
            g.DrawLine(WhitePen, x + lenght * 4, y + lenght, x + lenght * 4, y + lenght * 2);

            //white line 
            g.DrawLine(WhitePen, x + lenght * 4, y + lenght * 2, x + lenght * 4, y + lenght * 3);

            //crossroad - right up corner


            //crossroad - right down corner


            //Crosswalk
            //g.DrawRectangle(new Pen(Color.Black), x - , , y + 50, 100);


        }

        private void UserControlCrossroad_Load(object sender, EventArgs e)
        {

        }
    }



}
