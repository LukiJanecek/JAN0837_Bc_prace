using Sharp7;
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
        private Program1Form program1FormInstance;

        private S7Client client;

        //MessageBox control
        private bool errorMessageBoxShown;

        //Drawing variables
        #region Drawing variables

        private float x = 0; //x coordinate
        private float y = 0; //y coordinate
        private float xCabin = 0; //x cabin coordinate
        private float yCabin = 0; //y cabin coordinate
        private float widthCabin; //cabin width 
        private float lengthCabin; //cabin length 
        private float widthButton = 50; //button width
        private float heightButton = 28; //button height
        public float Step = 10;
        private float length = 100;

        #endregion

        //BTNs define 
        #region BTNs define 

        private Button btnElevatorFloor1;
        private Button btnElevatorFloor2;
        private Button btnElevatorFloor3;
        private Button btnElevatorFloor4;
        private Button btnElevatorFloor5;

        #endregion

        public UserControlElevatorCabin(Program1Form program1FormInstance)
        {
            InitializeComponent();
            InitializeButtons();

            DoubleBuffered = true;
            Paint += UserControl1_Paint;

            this.program1FormInstance = program1FormInstance;

            client = program1FormInstance.client;
        }

        private void UserControl1_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            //backgroud
            g.Clear(Color.White);

            //pen color
            Pen BlackPen = new Pen(Color.Black);

            //text parameters
            Font labelFont = new Font("Arial", 9);
            SolidBrush labelBrush = new SolidBrush(Color.Black);

            //ElevatorCabin
            g.DrawRectangle(BlackPen, xCabin + 50, yCabin + 20, widthCabin + 50, lengthCabin + 80);

            //Floors and labels
            //1st floor
            g.DrawLine(BlackPen, x, y + length, x + length * 3, y + length);
            string lblFirstFloor = "1st floor";
            g.DrawString(lblFirstFloor, labelFont, labelBrush, x, y + length - length / 2);

            //2nd floor
            g.DrawLine(BlackPen, x, y + length * 2, x + length * 3, y + length * 2);
            string lblSecondFloor = "2nd floor";
            g.DrawString(lblSecondFloor, labelFont, labelBrush, x, y + length * 2 - length / 2);

            //3rd floor
            g.DrawLine(BlackPen, x, y + length * 3, x + length * 3, y + length * 3);
            string lblThirdFloor = "3rd floor";
            g.DrawString(lblThirdFloor, labelFont, labelBrush, x, y + length * 3 - length / 2);

            //4th floor
            g.DrawLine(BlackPen, x, y + length * 4, x + length * 3, y + length * 4);
            string lblFourthFloor = "4th floor";
            g.DrawString(lblFourthFloor, labelFont, labelBrush, x, y + length * 4 - length / 2);

            //5th floor
            g.DrawLine(BlackPen, x, y + length * 5, x + length * 3, y + length * 5);
            string lblFifthFloor = "5th floor";
            g.DrawString(lblFifthFloor, labelFont, labelBrush, x, y + length * 5 - length / 2);

        }

        //InitializeButtons
        #region InitializeButtons

        private void InitializeButtons()
        {
            //btnElevatorFloor1
            btnElevatorFloor1.Text = "1";
            btnElevatorFloor1.BackColor = Color.White;
            btnElevatorFloor1.Visible = false;
            btnElevatorFloor1.Enabled = false;
            btnElevatorFloor1.Location = new System.Drawing.Point(Convert.ToInt32(length), Convert.ToInt32(length)); //cannot invert float to int 
            btnElevatorFloor1.Size = new Size(Convert.ToInt32(widthButton), Convert.ToInt32(heightButton)); //cannot invert float to int 
            btnElevatorFloor1.Click += btnElevatorFloor1_Click;
            Controls.Add(btnElevatorFloor1);
            //btnElevatorFloor2
            btnElevatorFloor2.Text = "2";
            btnElevatorFloor2.BackColor = Color.White;
            btnElevatorFloor2.Visible = false;
            btnElevatorFloor2.Enabled = false;
            btnElevatorFloor2.Location = new System.Drawing.Point(Convert.ToInt32(length), Convert.ToInt32(length)); //cannot invert float to int 
            btnElevatorFloor2.Size = new Size(Convert.ToInt32(widthButton), Convert.ToInt32(heightButton)); //cannot invert float to int 
            btnElevatorFloor2.Click += btnElevatorFloor2_Click;
            Controls.Add(btnElevatorFloor2);
            //btnElevatorFloor3
            btnElevatorFloor3.Text = "3";
            btnElevatorFloor3.BackColor = Color.White;
            btnElevatorFloor3.Visible = false;
            btnElevatorFloor3.Enabled = false;
            btnElevatorFloor3.Location = new System.Drawing.Point(Convert.ToInt32(length), Convert.ToInt32(length)); //cannot invert float to int 
            btnElevatorFloor3.Size = new Size(Convert.ToInt32(widthButton), Convert.ToInt32(heightButton)); //cannot invert float to int 
            btnElevatorFloor3.Click += btnElevatorFloor3_Click;
            Controls.Add(btnElevatorFloor3);
            //btnElevatorFloor4
            btnElevatorFloor4.Text = "4";
            btnElevatorFloor4.BackColor = Color.White;
            btnElevatorFloor4.Visible = false;
            btnElevatorFloor4.Enabled = false;
            btnElevatorFloor4.Location = new System.Drawing.Point(Convert.ToInt32(length), Convert.ToInt32(length)); //cannot invert float to int 
            btnElevatorFloor4.Size = new Size(Convert.ToInt32(widthButton), Convert.ToInt32(heightButton)); //cannot invert float to int 
            btnElevatorFloor4.Click += btnElevatorFloor4_Click;
            Controls.Add(btnElevatorFloor4);
            //btnElevatorFloor¨5
            btnElevatorFloor5.Text = "5";
            btnElevatorFloor5.BackColor = Color.White;
            btnElevatorFloor5.Visible = false;
            btnElevatorFloor5.Enabled = false;
            btnElevatorFloor5.Location = new System.Drawing.Point(Convert.ToInt32(length), Convert.ToInt32(length)); //cannot invert float to int 
            btnElevatorFloor5.Size = new Size(Convert.ToInt32(widthButton), Convert.ToInt32(heightButton)); //cannot invert float to int 
            btnElevatorFloor5.Click += btnElevatorFloor5_Click;
            Controls.Add(btnElevatorFloor5);
        }

        #endregion

        //Methods for Cabin and Floor BTN action
        #region Methods for Cabin and Floor BTN action

        private void btnElevatorFloor1_Click(object sender, EventArgs e)
        {
            program1FormInstance.ElevatorBTNFloor1 = true;
            S7.SetBitAt(program1FormInstance.send_buffer_DB4, 0, 5, program1FormInstance.ElevatorBTNFloor1);

            //write to PLC
            int writeResultDB4_btnElevatorFloor1 = program1FormInstance.client.DBWrite(program1FormInstance.DBNumber_DB4, 0, program1FormInstance.send_buffer_DB4.Length, program1FormInstance.send_buffer_DB4);
            if (writeResultDB4_btnElevatorFloor1 != 0)
            {
                //write error
                if (!errorMessageBoxShown)
                {
                    //MessageBox
                    MessageBox.Show("BE doesn't work properly. Data could´t be written to DB4!!! \n\n" +
                        $"Error message: writeResultDB4_btnElevatorFloor1 = {writeResultDB4_btnElevatorFloor1} \n", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                    errorMessageBoxShown = true;
                }
            }
            else
            {
                //write was successful
            }
        }

        private void btnElevatorFloor2_Click(object sender, EventArgs e)
        {
            program1FormInstance.ElevatorBTNFloor2 = true;
            S7.SetBitAt(program1FormInstance.send_buffer_DB4, 0, 6, program1FormInstance.ElevatorBTNFloor2);

            //write to PLC
            int writeResultDB4_btnElevatorFloor2 = program1FormInstance.client.DBWrite(program1FormInstance.DBNumber_DB4, 0, program1FormInstance.send_buffer_DB4.Length, program1FormInstance.send_buffer_DB4);
            if (writeResultDB4_btnElevatorFloor2 != 0)
            {
                //write error
                if (!errorMessageBoxShown)
                {
                    //MessageBox
                    MessageBox.Show("BE doesn't work properly. Data could´t be written to DB4!!! \n\n" +
                        $"Error message: writeResultDB4_btnElevatorFloor2 = {writeResultDB4_btnElevatorFloor2} \n", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                    errorMessageBoxShown = true;
                }
            }
            else
            {
                //write was successful
            }
        }

        private void btnElevatorFloor3_Click(object sender, EventArgs e)
        {
            program1FormInstance.ElevatorBTNFloor3 = true;
            S7.SetBitAt(program1FormInstance.send_buffer_DB4, 0, 7, program1FormInstance.ElevatorBTNFloor3);

            //write to PLC
            int writeResultDB4_btnElevatorFloor3 = program1FormInstance.client.DBWrite(program1FormInstance.DBNumber_DB4, 0, program1FormInstance.send_buffer_DB4.Length, program1FormInstance.send_buffer_DB4);
            if (writeResultDB4_btnElevatorFloor3 != 0)
            {
                //write error
                if (!errorMessageBoxShown)
                {
                    //MessageBox
                    MessageBox.Show("BE doesn't work properly. Data could´t be written to DB4!!! \n\n" +
                        $"Error message: writeResultDB4_btnElevatorFloor3 = {writeResultDB4_btnElevatorFloor3} \n", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                    errorMessageBoxShown = true;
                }
            }
            else
            {
                //write was successful
            }
        }

        private void btnElevatorFloor4_Click(object sender, EventArgs e)
        {
            program1FormInstance.ElevatorBTNFloor4 = true;
            S7.SetBitAt(program1FormInstance.send_buffer_DB4, 1, 0, program1FormInstance.ElevatorBTNFloor4);

            //write to PLC
            int writeResultDB4_btnElevatorFloor4 = program1FormInstance.client.DBWrite(program1FormInstance.DBNumber_DB4, 0, program1FormInstance.send_buffer_DB4.Length, program1FormInstance.send_buffer_DB4);
            if (writeResultDB4_btnElevatorFloor4 != 0)
            {
                //write error
                if (!errorMessageBoxShown)
                {
                    //MessageBox
                    MessageBox.Show("BE doesn't work properly. Data could´t be written to DB4!!! \n\n" +
                        $"Error message: writeResultDB4_btnElevatorFloor4 = {writeResultDB4_btnElevatorFloor4} \n", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                    errorMessageBoxShown = true;
                }
            }
            else
            {
                //write was successful
            }
        }

        private void btnElevatorFloor5_Click(object sender, EventArgs e)
        {
            program1FormInstance.ElevatorBTNFloor5 = true;
            S7.SetBitAt(program1FormInstance.send_buffer_DB4, 1, 1, program1FormInstance.ElevatorBTNFloor5);

            //write to PLC
            int writeResultDB4_btnElevatorFloor5 = program1FormInstance.client.DBWrite(program1FormInstance.DBNumber_DB4, 0, program1FormInstance.send_buffer_DB4.Length, program1FormInstance.send_buffer_DB4);
            if (writeResultDB4_btnElevatorFloor5 != 0)
            {
                //write error
                if (!errorMessageBoxShown)
                {
                    //MessageBox
                    MessageBox.Show("BE doesn't work properly. Data could´t be written to DB5!!! \n\n" +
                        $"Error message: writeResultDB4_btnElevatorFloor5 = {writeResultDB4_btnElevatorFloor5} \n", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                    errorMessageBoxShown = true;
                }
            }
            else
            {
                //write was successful
            }
        }

        #endregion

        //Cabin movement + parameters
        #region Cabin movement + parameters
        public void MoveRight()
        {
            xCabin += Step;
            this.Refresh();
        }

        public void MoveLeft()
        {
            xCabin -= Step;
            this.Refresh();
        }

        public void MoveUp()
        {
            yCabin -= Step;
            this.Refresh();
        }

        public void MoveDown()
        {
            yCabin += Step;
            this.Refresh();
        }

        public void WidthBigger()
        {
            widthCabin += Step;
            this.Refresh();
        }

        public void WidthSmaller()
        {
            widthCabin -= Step;
            this.Refresh();
        }

        public void LengthBigger()
        {
            lengthCabin += Step;
            this.Refresh();
        }

        public void LengthSmaller()
        {
            lengthCabin -= Step;
            this.Refresh();
        }
        #endregion

    }
}
