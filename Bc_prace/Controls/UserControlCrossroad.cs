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
        private float x = 0;
        private float y = 0;

        //basic parametres
        private float lenght = 20;
        private float lenght_gap = 10;
        private float lenght_interrupted = 5;

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
            g.Clear(Color.White);

            //pen color
            Pen BlackPen = new Pen(Color.Black, 2);
            Pen WhitePen = new Pen(Color.White, 2);

            //crossroad - left up corner
            g.DrawLine(BlackPen, x + lenght, y + lenght, x + lenght * 2, y + lenght);
            g.DrawLine(BlackPen, x + lenght * 2, y + lenght, x + lenght * 2, y + lenght * 2);
            g.DrawLine(WhitePen, x + lenght * 2, y + lenght * 2, x + lenght, y + lenght * 2);

            //interrupted line 
            g.DrawLine(BlackPen, x + lenght, y + lenght * 2 + lenght_interrupted, x + lenght + 10, y + lenght * 2 + lenght_interrupted);
            g.DrawLine(BlackPen, x + lenght + 20, y + lenght * 2 + lenght_interrupted, x + lenght + 30, y + lenght * 2 + lenght_interrupted);
            g.DrawLine(BlackPen, x + lenght + 40, y + lenght * 2 + lenght_interrupted, x + lenght + 50, y + lenght * 2 + lenght_interrupted);
            g.DrawLine(BlackPen, x + lenght + 60, y + lenght * 2 + lenght_interrupted, x + lenght + 70, y + lenght * 2 + lenght_interrupted);
            g.DrawLine(BlackPen, x + lenght + 80, y + lenght * 2 + lenght_interrupted, x + lenght + 90, y + lenght * 2 + lenght_interrupted);

            //crossroad - left down corner
            g.DrawLine(BlackPen, x + lenght, y + lenght * 3, x + lenght * 2, y + lenght * 3);
            g.DrawLine(BlackPen, x + lenght * 2, y + lenght * 3, x + lenght * 2, y + lenght * 4);

            //crossroad - mid up 
            g.DrawLine(BlackPen, x + lenght * 2 + lenght_gap * 2, y + lenght, x + lenght * 2 + lenght_gap * 2, y + lenght * 3);
            g.DrawLine(BlackPen, x + lenght * 2 + lenght_gap * 2, y + lenght * 3, x + lenght * 3 + lenght_gap * 2, y + lenght * 3);
            g.DrawLine(BlackPen, x + lenght * 3 + lenght_gap * 2, y + lenght * 3, x + lenght * 3 + lenght_gap * 2, y + lenght * 2);

            //crossroad - mid down 
            g.DrawLine(BlackPen, x + lenght * 2 + lenght_gap * 2, y + lenght * 2 + lenght_gap * 2, x + lenght * 2 + lenght_gap * 2, y + lenght * 3 + lenght_gap * 2);
            g.DrawLine(BlackPen, x + lenght * 2 + lenght_gap * 2, y + lenght * 3 + lenght_gap * 2, x + lenght * 3 + lenght_gap * 2, y + lenght * 3 + lenght_gap * 2);
            g.DrawLine(BlackPen, x + lenght * 3 + lenght_gap * 2, y + lenght * 3 + lenght_gap * 2, x + lenght * 3 + lenght_gap * 2, y + lenght * 4 + lenght_gap * 2);

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
