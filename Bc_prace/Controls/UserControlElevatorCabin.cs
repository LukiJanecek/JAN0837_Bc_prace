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
    public partial class UserControlElevatorCabin : UserControl
    {
        //Drawing variables
        #region Drawing variables

        private float x; //x coordinate
        private float y; //width
        private float a; //y coordinate
        private float b; //length
        public float Step = 10;

        #endregion

        public UserControlElevatorCabin()
        {
            InitializeComponent();
            DoubleBuffered = true;
            Paint += UserControl1_Paint;
        }

        private void UserControl1_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            
            //backgroud
            g.Clear(Color.White);

            //ElevatorCabin
            g.DrawRectangle(new Pen(Color.Black), x + 20, a + 30, y + 50, b + 100);
        }

        //Cabin movement + parameters
        #region Cabin movement + parameters
        public void MoveRight()
        {
            x += Step;
            this.Refresh();
        }

        public void MoveLeft()
        {
            x -= Step;
            this.Refresh();
        }

        public void MoveUp()
        {
            a -= Step;
            this.Refresh();
        }

        public void MoveDown()
        {
            a += Step;
            this.Refresh();
        }

        public void WidthBigger()
        {
            y += Step;
            this.Refresh();
        }

        public void WidthSmaller()
        {
            y -= Step;
            this.Refresh();
        }

        public void LengthBigger()
        {
            b += Step;
            this.Refresh();
        }

        public void LengthSmaller()
        {
            b -= Step;
            this.Refresh();
        }
        #endregion

    }
}
