using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JAN0837_BP.Controls
{
    public partial class UserControlSablona : UserControl
    {
        public UserControlSablona()
        {
            InitializeComponent();
            DoubleBuffered = true;
            Paint += UserControlSablona_Paint;
        }

        private void UserControlSablona_Load(object sender, EventArgs e)
        {

        }

        private void UserControlSablona_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            //background color
            g.Clear(Color.White);

            Draw(g);
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
        }

    }
}
