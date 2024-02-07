using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.XPath;
using Bc_prace.Controls.MyGraphControl.Entities;
using Microsoft.VisualBasic;
using Bc_prace.Settings;
using Sharp7;

namespace Bc_prace
{
    public partial class ChooseOptionForm : Form
    {
        //Variables
        #region Variables 

        //C# variables
        #region C# variables

        private static bool program1Opened = false;
        private static bool program2Opened = false;
        private static bool program3Opened = false;

        private bool errorMessageBoxShown = false;

        Program1Form Program1 = null;
        Program2Form Program2 = null;
        Program3Form Program3 = null;

        #endregion

        //Tia variables
        #region Tia variables

        public S7Client client = new S7Client();

        //možná změnit velikost, Klus měl 6u
        //we need to read/write 3 bits (3 times bool) -> 1 byte
        private byte[] read_buffer = new byte[1];
        private byte[] send_buffer = new byte[1];

        bool Option1 = false;
        bool Option2 = false;
        bool Option3 = false;

        #endregion

        #endregion

        //Tia variables
        #region Tia connection

        private void Timer_read_from_PLC_Tick(object sender, EventArgs e)
        {
            try
            {
                //DB11 => Maintain_DB
                int readResult = client.DBRead(11, 0, read_buffer.Length, read_buffer);
                //pokud je readResult roven 0, tak čtení bylo úspěšné
                if (readResult != 0)
                {
                    statusStrip1.Items.Clear();
                    ToolStripStatusLabel lblStatus1 = new ToolStripStatusLabel("Tia didn't respond. BE doesn't work properly. Data from PLC weren't read!!!");
                    statusStrip1.Items.Add(lblStatus1);

                    if (!errorMessageBoxShown)
                    {
                        //MessageBox
                        MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data from PLC weren't read from DB11!!!", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                        errorMessageBoxShown = true;
                    }
                }
                else
                {
                    Option1 = S7.GetBitAt(read_buffer, 0, 0);
                    Option2 = S7.GetBitAt(read_buffer, 0, 1);
                    Option3 = S7.GetBitAt(read_buffer, 0, 2);

                    errorMessageBoxShown = false;
                }
            }
            catch (Exception ex)
            {
                if (!errorMessageBoxShown)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
            
        }

        #endregion

        public ChooseOptionForm()
        {
            InitializeComponent();
        }

        //Choices and messages 
        #region Choose your simulation
        private void btnProgram1_Click(object sender, EventArgs e)
        {
            if (!program1Opened)
            {
                statusStrip1.Items.Clear();
                ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Running Program 1");
                statusStrip1.Items.Add(lblStatus);

                Option1 = true;
                S7.SetBitAt(ref send_buffer, 0, 0, true);

                //write to PLC
                int writeResult = client.DBWrite(11, 0, send_buffer.Length, send_buffer);
                if (writeResult != 0)
                {
                    //write error
                    if (!errorMessageBoxShown)
                    {
                        //MessageBox
                        MessageBox.Show("BE doesn't work properly. Data couldt be written to PLC!!!", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                        errorMessageBoxShown = true;
                    }
                }
                else
                {
                    //write was successful
                }

                Program1 = new Program1Form();
                Program1.Show();
                program1Opened = true;
                                
                Program1.FormClosed += (sender, e) => { program1Opened = false; };
            }
            else
            {
                statusStrip1.Items.Clear();
                ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Program 1 is already running.");
                statusStrip1.Items.Add(lblStatus);

                if (Program1 != null && !Program1.IsDisposed)
                {
                    Program1.BringToFront();
                }


            }
        }

        private void btnProgram2_Click(object sender, EventArgs e)
        {
            if (!program2Opened)
            {
                statusStrip1.Items.Clear();
                ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Running Program 2");
                statusStrip1.Items.Add(lblStatus);

                Option2 = true;
                S7.SetBitAt(ref send_buffer, 0, 1, Option2);
                int writeResult = client.DBWrite(11, 0, send_buffer.Length, send_buffer);

                //write to PLC
                if (writeResult != 0)
                {
                    //write error
                    if (!errorMessageBoxShown)
                    {
                        //MessageBox
                        MessageBox.Show("BE doesn't work properly. Data couldt be written to PLC!!!", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                        errorMessageBoxShown = true;
                    }
                }
                else
                {
                    //write was successful
                }

                Program2 = new Program2Form();
                Program2.Show();
                program2Opened = true;

                Program2.FormClosed += (sender, e) => { program1Opened = false; };
            }
            else
            {
                statusStrip1.Items.Clear();
                ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Program 2 is already running");
                statusStrip1.Items.Add(lblStatus);

                if (Program2 != null && !Program2.IsDisposed)
                {
                    Program2.BringToFront();
                }
                
            }

        }

        private void btnProgram3_Click(object sender, EventArgs e)
        {
            if (!program3Opened)
            {
                statusStrip1.Items.Clear();
                ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Running Program 3");
                statusStrip1.Items.Add(lblStatus);

                Option3 = true;
                S7.SetBitAt(ref send_buffer, 0, 2, Option3);
                int writeResult = client.DBWrite(11, 0, send_buffer.Length, send_buffer);
                
                //write to PLC
                if (writeResult != 0)
                {
                    //write error
                    if (!errorMessageBoxShown)
                    {
                        //MessageBox
                        MessageBox.Show("BE doesn't work properly. Data couldt be written to PLC!!!", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                        errorMessageBoxShown = true;
                    }
                }
                else
                {
                    //write was successful
                }

                Program3 = new Program3Form();
                Program3.Show();
                program3Opened = true;

                Program3.FormClosed += (sender, e) => { program1Opened = false; };
            }
            else
            {
                statusStrip1.Items.Clear();
                ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Program 3 is already running");
                statusStrip1.Items.Add(lblStatus);


                if (Program3 != null && !Program3.IsDisposed)
                {
                    Program3.BringToFront();
                }

            }

        }
        #endregion

        //Connection + messages
        #region Connecting to PLC 
        private void btnConnect_Click(object sender, EventArgs e)
        {
            btnConnect.Text = "Connecting...";
            statusStrip1.Items.Clear();
            ToolStripStatusLabel lblStat = new ToolStripStatusLabel("Connecting to " + txtBoxPLCIP.Text);
            statusStrip1.Items.Add(lblStat);

            int plc = client.ConnectTo(txtBoxPLCIP.Text, 0, 1);

            if (plc == 0)
            {
                statusStrip1.Items.Clear();
                ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Connected to " + txtBoxPLCIP.Text);
                statusStrip1.Items.Add(lblStatus);
                btnConnect.Text = "Connected";

                //start timer
                Timer_read_from_PLC.Start();
                //set time interval (ms)
                Timer_read_from_PLC.Interval = 100;   
            }
            else
            {
                statusStrip1.Items.Clear();
                ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Connecting to " + txtBoxPLCIP.Text + "FAILED! Please, chech your IP address or PLC itself.");
                statusStrip1.Items.Add(lblStatus);
                btnConnect.Text = "Connect";
            }
        }
        #endregion


        private void txtBoxPLCIP_TextChanged(object sender, EventArgs e)
        {
               
        }


        private void ChooseOption_Load(object sender, EventArgs e)
        {

        }

        //btn End 
        #region Close window 
        private void btnEnd_Click(object sender, EventArgs e)
        {
            //Option1 = false
            Option1 = false;
            S7.SetBitAt(ref send_buffer, 0, 0, Option1);
            //Option2 = false
            Option2 = false;
            S7.SetBitAt(ref send_buffer, 0, 1, Option2);
            //Option3 = false
            Option3 = false;
            S7.SetBitAt(ref send_buffer, 0, 2, Option3);
            
            //write to PLC
            int writeResult = client.DBWrite(11, 0, send_buffer.Length, send_buffer);
            if (writeResult != 0)
            {
                //write error
                if (!errorMessageBoxShown)
                {
                    //MessageBox
                    MessageBox.Show("BE doesn't work properly. Data couldt be written to PLC!!!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                    errorMessageBoxShown = true;
                }
            }
            else
            {
                //write was successful
            }

            //Tia disconnect
            client.Disconnect();

            //close program
            this.Close();
        }
        #endregion
    }
}
