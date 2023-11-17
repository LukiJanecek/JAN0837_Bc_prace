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
using System.Xml.XPath;
using Bc_prace.Controls.MyGraphControl.Entities;
using Microsoft.VisualBasic;
using Bc_prace.Settings;

namespace Bc_prace
{
    public partial class ChooseOptionForm : Form
    {
        //Tia variablesx
        #region Tia connection
        public S7Client client = new S7Client();
        private byte[] send_buffer = new byte[5u];
        private byte[] read_buffer = new byte[6u];
        bool Option1 = false;
        bool Option2 = false;
        bool Option3 = false;
        public int ActualFloor;

        private void Timer_read_from_PLC_Tick(object sender, EventArgs e)
        {
            int readResult = client.DBRead(1, 0, read_buffer.Length, read_buffer);
                if (readResult != 0)
                {
                    //možná raději přidat label 
                    ToolStripStatusLabel lblStatus1 = new ToolStripStatusLabel("Variables were not read.");
                    statusStrip1.Items.Add(lblStatus1);

                    Console.WriteLine("Tia didn't respond. BE doesn't work properly. Data from PLC weren't read!!!");
                }
                else
                {
                //data přečtena 
                //všechny moje proměnné:
                Option1 = S7.GetBitAt(read_buffer, 0, 1);
                Option2 = S7.GetBitAt(read_buffer, 0, 2);
                Option3 = S7.GetBitAt(read_buffer, 0, 3);
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
            /*potrebuju to tady? 
            //muze byt lepe vyreseno primo v Program1
            int writeResult = client.DBWrite(11, 0, send_buffer.Length, send_buffer);
            Option1 = true;
            S7.SetBitAt(ref send_buffer, 0, 0, true);
            // ale cool podmínka X error hláška
            */

            Option1 = true;
            S7.SetBitAt(ref send_buffer, 0, 0, true);

            statusStrip1.Items.Clear();
            ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Running Program 1");
            statusStrip1.Items.Add(lblStatus);

            Program1Form Program1 = new Program1Form();
            Program1.Show();
        }

        private void btnProgram2_Click(object sender, EventArgs e)
        {
            /*potrebuju to tady? 
            //muze byt lepe vyreseno primo v Program1
            int writeResult = client.DBWrite(11, 0, send_buffer.Length, send_buffer);
            if (writeResult == 0)
            {
                Option2 = true;
                S7.SetBitAt(ref send_buffer, 0, 1, true);
            }
            else
            {
                Console.WriteLine("Tia didn't respond. BE doesn't work properly.");
            }
            // ale cool podmínka X error hláška
            */

            Option2 = true;
            S7.SetBitAt(ref send_buffer, 0, 1, true);

            statusStrip1.Items.Clear();
            ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Running Program 2");
            statusStrip1.Items.Add(lblStatus);

            Program2Form Program2 = new Program2Form();
            Program2.Show();
        }

        private void btnProgram3_Click(object sender, EventArgs e)
        {
            /*
            int writeResult = client.DBWrite(11, 0, send_buffer.Length, send_buffer);
            if (writeResult == 0)
            {
                Option3 = true;
                S7.SetBitAt(ref send_buffer, 0, 2, true);
            }
            else
            {
                Console.WriteLine("Tia didn't respond. BE doesn't work properly.");
            }
            // ale cool podmínka X error hláška */


            Option3 = true;
            S7.SetBitAt(ref send_buffer, 0, 2, true);
            
            statusStrip1.Items.Clear();
            ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Running Program 3");
            statusStrip1.Items.Add(lblStatus);

            Program3Form Program3 = new Program3Form();
            Program3.Show();
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

                /*int writeResult = client.DBWrite(11, 0, send_buffer.Length, send_buffer);
                if (writeResult == 0)
                {
                    Option1 = false;
                    S7.SetBitAt(ref send_buffer, 0, 0, false);
                    Option3 = false;
                    S7.SetBitAt(ref send_buffer, 0, 1, false);
                    Option3 = false;
                    S7.SetBitAt(ref send_buffer, 0, 2, false);
                }
                else
                {
                    Console.WriteLine("Tia didn't respond. BE doesn't work properly.");
                }*/

                /*int readResult = client.DBRead(1, 0, read_buffer.Length, read_buffer);
                if (readResult != 0)
                {
                    //možná raději přidat label 
                    ToolStripStatusLabel lblStatus1 = new ToolStripStatusLabel("Variables were not read.");
                    statusStrip1.Items.Add(lblStatus1);
                }
                else
                {
                    //data přečtena 
                    //všechny moje proměnné:

                }*/

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
            S7.SetBitAt(ref send_buffer, 0, 0, false);
            //Option2 = false
            Option2 = false;
            S7.SetBitAt(ref send_buffer, 0, 1, false);
            //Option3 = false
            Option3 = false;
            S7.SetBitAt(ref send_buffer, 0, 2, false);

            //Tia disconnect
            client.Disconnect();

            //close program
            this.Close();
        }
        #endregion
    }
}
