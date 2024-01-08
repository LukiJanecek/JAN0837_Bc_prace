using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bc_prace.Controls;
using Bc_prace.Controls.MyGraphControl.Entities;
using Bc_prace.Settings;
using Sharp7;

namespace Bc_prace
{
    public partial class Program3Form : Form
    {
        public Program3Form()
        {
            InitializeComponent();
        }

        //Tia connection
        #region Tia connection
        /*
        //zde vypisu vsechny promenne
        public S7Client client = new S7Client();
        public byte[] send_buffer = new byte[5u];
        public byte[] read_buffer = new byte[6u];

        //input 
        #region Input variables 
        bool CrossroadPedestrianBTN;
        bool CrossroadModeOFF;
        bool CrossroadModeNIGHT;
        bool CrossroadModeDAY;
        bool CrossroadEmergencySTOP;
        bool CrossroadErrorSystem;
        #endregion

        //output
        #region Output varialbes 

        //crossorad1
        #region Crossroad1
        bool Crossroad1TopRED;
        bool Crossroad1TopGREEN;
        bool Crossroad1TopYellow;
        bool Crossroad1LeftRED;
        bool Crossroad1LeftGREEN;
        bool Crossroad1LeftYellow;
        bool Crossroad1RightRED;
        bool Crossroad1RightGREEN;
        bool Crossroad1RightYellow;
        bool Crossroad1BottomRED;
        bool Crossroad1BottomGREEN;
        bool Crossroad1BottomYellow;

        bool Crossroad1TopCrosswalkRED1;
        bool Crossroad1TopCrosswalkRED2;
        bool Crossroad1TopCrosswalkGREEN1;
        bool Crossroad1TopCrosswalkGREEN2;
        bool Crossroad1LeftCrosswalkRED1;
        bool Crossroad1LeftCrosswalkRED2;
        bool Crossroad1LeftCrosswalkGREEN1;
        bool Crossroad1LeftCrosswalkGREEN2;
        bool Crossroad1RightCrosswalkRED1;
        bool Crossroad1RightCrosswalkRED2;
        bool Crossroad1RightCrosswalkGREEN1;
        bool Crossroad1RightCrosswalkGREEN2;
        bool Crossroad1BottomCrosswalkRED1;
        bool Crossroad1BottomCrosswalkRED2;
        bool Crossroad1BottomCrosswalkGREEN1;
        bool Crossroad1BottomCrosswalkGREEN2;
        #endregion

        //crossroad2
        #region Crossroad2
        bool Crossroad2TopRED;
        bool Crossroad2TopGREEN;
        bool Crossroad2TopYellow;
        bool Crossroad2LeftRED;
        bool Crossroad2LeftGREEN;
        bool Crossroad2LeftYellow;
        bool Crossroad2RightRED;
        bool Crossroad2RightGREEN;
        bool Crossroad2RightYellow;
        bool Crossroad2BottomRED;
        bool Crossroad2BottomGREEN;
        bool Crossroad2BottomYellow;

        bool Crossroad2TopCrosswalkRED1;
        bool Crossroad2TopCrosswalkRED2;
        bool Crossroad2TopCrosswalkGREEN1;
        bool Crossroad2TopCrosswalkGREEN2;
        bool Crossroad2LeftCrosswalkRED1;
        bool Crossroad2LeftCrosswalkRED2;
        bool Crossroad2LeftCrosswalkGREEN1;
        bool Crossroad2LeftCrosswalkGREEN2;
        bool Crossroad2RightCrosswalkRED1;
        bool Crossroad2RightCrosswalkRED2;
        bool Crossroad2RightCrosswalkGREEN1;
        bool Crossroad2RightCrosswalkGREEN2;
        bool Crossroad2BottomCrosswalkRED1;
        bool Crossroad2BottomCrosswalkRED2;
        bool Crossroad2BottomCrosswalkGREEN1;
        bool Crossroad2BottomCrosswalkGREEN2;
        #endregion

        //LeftT
        #region LeftT
        bool CrossroadLeftTTopRED;
        bool CrossroadLeftTTopGREEN;
        bool CrossroadLeftTTopYellow;
        bool CrossroadLeftTLeftRED;
        bool CrossroadLeftTLeftGREEN;
        bool CrossroadLeftTLeftYellow;
        bool CrossroadLeftTRightRED;
        bool CrossroadLeftTRightGREEN;
        bool CrossroadLeftTRightYellow;

        bool CrossroadLeftTTopCrosswalkRED1;
        bool CrossroadLeftTTopCrosswalkRED2;
        bool CrossroadLeftTTopCrosswalkGREEN1;
        bool CrossroadLeftTTopCrosswalkGREEN2;
        bool CrossroadLeftTLeftCrosswalkRED1;
        bool CrossroadLeftTLeftCrosswalkRED2;
        bool CrossroadLeftTLeftCrosswalkGREEN1;
        bool CrossroadLeftTLeftCrosswalkGREEN2;
        bool CrossroadLeftTRightCrosswalkRED1;
        bool CrossroadLeftTRightCrosswalkRED2;
        bool CrossroadLeftTRightCrosswalkGREEN1;
        bool CrossroadLeftTRightCrosswalkGREEN2;
        
        #endregion

        //RightT
        #region RightT
        bool CrossroadRightTTopRED;
        bool CrossroadRightTTopGREEN;
        bool CrossroadRightTTopYellow;
        bool CrossroadRightTLeftRED;
        bool CrossroadRightTLeftGREEN;
        bool CrossroadRightTLeftYellow;
        bool CrossroadRightTRightRED;
        bool CrossroadRightTRightGREEN;
        bool CrossroadRightTRightYellow;

        bool CrossroadRightTTopCrosswalkRED1;
        bool CrossroadRightTTopCrosswalkRED2;
        bool CrossroadRightTTopCrosswalkGREEN1;
        bool CrossroadRightTTopCrosswalkGREEN2;
        bool CrossroadRightTLeftCrosswalkRED1;
        bool CrossroadRightTLeftCrosswalkRED2;
        bool CrossroadRightTLeftCrosswalkGREEN1;
        bool CrossroadRightTLeftCrosswalkGREEN2;
        bool CrossroadRightTRightCrosswalkRED1;
        bool CrossroadRightTRightCrosswalkRED2;
        bool CrossroadRightTRightCrosswalkGREEN1;
        bool CrossroadRightTRightCrosswalkGREEN2;
        
        #endregion

        #endregion

        /*
        private void Timer_read_from_PLC_Tick(object sender, EventArgs e)
        {
            int readResult = client.DBRead(11, 0, read_buffer.Length, read_buffer);
            if (readResult != 0)
            {
                //možná raději přidat label 
                ToolStripStatusLabel lblStatus1 = new ToolStripStatusLabel("Variables were not read.");
                statusStripCrossroad.Items.Add(lblStatus1);

                Console.WriteLine("Tia didn't respond. BE doesn't work properly. Data from PLC weren't read!!!");
            }
            else
            {
                //data přečtena 
                //všechny moje proměnné:
                #region Input variables
                /*
                CrossroadPedestrianBTN = S7.GetBitAt(read_buffer, ,);
                CrossroadModeOFF = S7.GetBitAt(read_buffer, ,);
                CrossroadModeNIGHT = S7.GetBitAt(read_buffer, ,);
                CrossroadModeDAY = S7.GetBitAt(read_buffer, ,);
                CrossroadEmergencySTOP = S7.GetBitAt(read_buffer, ,);
                CrossroadErrorSystem = S7.GetBitAt(read_buffer, ,);
                */
                #endregion

                #region Output varialbes 
                /*
                //crossorad1
                #region Crossroad1
                Crossroad1TopRED = S7.GetBitAt(read_buffer, ,);
                Crossroad1TopGREEN = S7.GetBitAt(read_buffer, ,);
                Crossroad1TopYellow = S7.GetBitAt(read_buffer, ,);
                Crossroad1LeftRED = S7.GetBitAt(read_buffer, ,);
                Crossroad1LeftGREEN = S7.GetBitAt(read_buffer, ,);
                Crossroad1LeftYellow = S7.GetBitAt(read_buffer, ,);
                Crossroad1RightRED = S7.GetBitAt(read_buffer, ,);
                Crossroad1RightGREEN = S7.GetBitAt(read_buffer, ,);
                Crossroad1RightYellow = S7.GetBitAt(read_buffer, ,);
                Crossroad1BottomRED = S7.GetBitAt(read_buffer, ,);
                Crossroad1BottomGREEN = S7.GetBitAt(read_buffer, ,);
                Crossroad1BottomYellow = S7.GetBitAt(read_buffer, ,);

                Crossroad1TopCrosswalkRED1 = S7.GetBitAt(read_buffer, ,);
                Crossroad1TopCrosswalkRED2 = S7.GetBitAt(read_buffer, ,);
                Crossroad1TopCrosswalkGREEN1 = S7.GetBitAt(read_buffer, ,);
                Crossroad1TopCrosswalkGREEN2 = S7.GetBitAt(read_buffer, ,);
                Crossroad1LeftCrosswalkRED1 = S7.GetBitAt(read_buffer, ,);
                Crossroad1LeftCrosswalkRED2 = S7.GetBitAt(read_buffer, ,);
                Crossroad1LeftCrosswalkGREEN1 = S7.GetBitAt(read_buffer, ,);
                Crossroad1LeftCrosswalkGREEN2 = S7.GetBitAt(read_buffer, ,);
                Crossroad1RightCrosswalkRED1 = S7.GetBitAt(read_buffer, ,);
                Crossroad1RightCrosswalkRED2 = S7.GetBitAt(read_buffer, ,);
                Crossroad1RightCrosswalkGREEN1 = S7.GetBitAt(read_buffer, ,);
                Crossroad1RightCrosswalkGREEN2 = S7.GetBitAt(read_buffer, ,);
                Crossroad1BottomCrosswalkRED1 = S7.GetBitAt(read_buffer, ,);
                Crossroad1BottomCrosswalkRED2 = S7.GetBitAt(read_buffer, ,);
                Crossroad1BottomCrosswalkGREEN1 = S7.GetBitAt(read_buffer, ,);
                Crossroad1BottomCrosswalkGREEN2 = S7.GetBitAt(read_buffer, ,);
                #endregion

                //crossroad2
                #region Crossroad2
                Crossroad2TopRED = S7.GetBitAt(read_buffer, ,);
                Crossroad2TopGREEN = S7.GetBitAt(read_buffer, ,);
                Crossroad2TopYellow = S7.GetBitAt(read_buffer, ,);
                Crossroad2LeftRED = S7.GetBitAt(read_buffer, ,);
                Crossroad2LeftGREEN = S7.GetBitAt(read_buffer, ,);
                Crossroad2LeftYellow = S7.GetBitAt(read_buffer, ,);
                Crossroad2RightRED = S7.GetBitAt(read_buffer, ,);
                Crossroad2RightGREEN = S7.GetBitAt(read_buffer, ,);
                Crossroad2RightYellow = S7.GetBitAt(read_buffer, ,);
                Crossroad2BottomRED = S7.GetBitAt(read_buffer, ,);
                Crossroad2BottomGREEN = S7.GetBitAt(read_buffer, ,);
                Crossroad2BottomYellow = S7.GetBitAt(read_buffer, ,);

                Crossroad2TopCrosswalkRED1 = S7.GetBitAt(read_buffer, ,);
                Crossroad2TopCrosswalkRED2 = S7.GetBitAt(read_buffer, ,);
                Crossroad2TopCrosswalkGREEN1 = S7.GetBitAt(read_buffer, ,);
                Crossroad2TopCrosswalkGREEN2 = S7.GetBitAt(read_buffer, ,);
                Crossroad2LeftCrosswalkRED1 = S7.GetBitAt(read_buffer, ,);
                Crossroad2LeftCrosswalkRED2 = S7.GetBitAt(read_buffer, ,);
                Crossroad2LeftCrosswalkGREEN1 = S7.GetBitAt(read_buffer, ,);
                Crossroad2LeftCrosswalkGREEN2 = S7.GetBitAt(read_buffer, ,);
                Crossroad2RightCrosswalkRED1 = S7.GetBitAt(read_buffer, ,);
                Crossroad2RightCrosswalkRED2 = S7.GetBitAt(read_buffer, ,);
                Crossroad2RightCrosswalkGREEN1 = S7.GetBitAt(read_buffer, ,);
                Crossroad2RightCrosswalkGREEN2 = S7.GetBitAt(read_buffer, ,);
                Crossroad2BottomCrosswalkRED1 = S7.GetBitAt(read_buffer, ,);
                Crossroad2BottomCrosswalkRED2 = S7.GetBitAt(read_buffer, ,);
                Crossroad2BottomCrosswalkGREEN1 = S7.GetBitAt(read_buffer, ,);
                Crossroad2BottomCrosswalkGREEN2 = S7.GetBitAt(read_buffer, ,);
                #endregion

                //LeftT
                #region LeftT
                CrossroadLeftTTopRED = S7.GetBitAt(read_buffer, ,);
                CrossroadLeftTTopGREEN = S7.GetBitAt(read_buffer, ,);
                CrossroadLeftTTopYellow = S7.GetBitAt(read_buffer, ,);
                CrossroadLeftTLeftRED = S7.GetBitAt(read_buffer, ,);
                CrossroadLeftTLeftGREEN = S7.GetBitAt(read_buffer, ,);
                CrossroadLeftTLeftYellow = S7.GetBitAt(read_buffer, ,);
                CrossroadLeftTRightRED = S7.GetBitAt(read_buffer, ,);
                CrossroadLeftTRightGREEN = S7.GetBitAt(read_buffer, ,);
                CrossroadLeftTRightYellow = S7.GetBitAt(read_buffer, ,);

                CrossroadLeftTTopCrosswalkRED1 = S7.GetBitAt(read_buffer, ,);
                CrossroadLeftTTopCrosswalkRED2 = S7.GetBitAt(read_buffer, ,);
                CrossroadLeftTTopCrosswalkGREEN1 = S7.GetBitAt(read_buffer, ,);
                CrossroadLeftTTopCrosswalkGREEN2 = S7.GetBitAt(read_buffer, ,);
                CrossroadLeftTLeftCrosswalkRED1 = S7.GetBitAt(read_buffer, ,);
                CrossroadLeftTLeftCrosswalkRED2 = S7.GetBitAt(read_buffer, ,);
                CrossroadLeftTLeftCrosswalkGREEN1 = S7.GetBitAt(read_buffer, ,);
                CrossroadLeftTLeftCrosswalkGREEN2 = S7.GetBitAt(read_buffer, ,);
                CrossroadLeftTRightCrosswalkRED1 = S7.GetBitAt(read_buffer, ,);
                CrossroadLeftTRightCrosswalkRED2 = S7.GetBitAt(read_buffer, ,);
                CrossroadLeftTRightCrosswalkGREEN1 = S7.GetBitAt(read_buffer, ,);
                CrossroadLeftTRightCrosswalkGREEN2 = S7.GetBitAt(read_buffer, ,);

                #endregion

                //RightT
                #region RightT
                CrossroadRightTTopRED = S7.GetBitAt(read_buffer, ,);
                CrossroadRightTTopGREEN = S7.GetBitAt(read_buffer, ,);
                CrossroadRightTTopYellow = S7.GetBitAt(read_buffer, ,);
                CrossroadRightTLeftRED = S7.GetBitAt(read_buffer, ,);
                CrossroadRightTLeftGREEN = S7.GetBitAt(read_buffer, ,);
                CrossroadRightTLeftYellow = S7.GetBitAt(read_buffer, ,);
                CrossroadRightTRightRED = S7.GetBitAt(read_buffer, ,);
                CrossroadRightTRightGREEN = S7.GetBitAt(read_buffer, ,);
                CrossroadRightTRightYellow = S7.GetBitAt(read_buffer, ,);

                CrossroadRightTTopCrosswalkRED1 = S7.GetBitAt(read_buffer, ,);
                CrossroadRightTTopCrosswalkRED2 = S7.GetBitAt(read_buffer, ,);
                CrossroadRightTTopCrosswalkGREEN1 = S7.GetBitAt(read_buffer, ,);
                CrossroadRightTTopCrosswalkGREEN2 = S7.GetBitAt(read_buffer, ,);
                CrossroadRightTLeftCrosswalkRED1 = S7.GetBitAt(read_buffer, ,);
                CrossroadRightTLeftCrosswalkRED2 = S7.GetBitAt(read_buffer, ,);
                CrossroadRightTLeftCrosswalkGREEN1 = S7.GetBitAt(read_buffer, ,);
                CrossroadRightTLeftCrosswalkGREEN2 = S7.GetBitAt(read_buffer, ,);
                CrossroadRightTRightCrosswalkRED1 = S7.GetBitAt(read_buffer, ,);
                CrossroadRightTRightCrosswalkRED2 = S7.GetBitAt(read_buffer, ,);
                CrossroadRightTRightCrosswalkGREEN1 = S7.GetBitAt(read_buffer, ,);
                CrossroadRightTRightCrosswalkGREEN2 = S7.GetBitAt(read_buffer, ,);

                #endregion
                */
                #endregion
        /*
            }
        }
        
        

        #endregion
        */

        //Settings
        #region Settings 
        private void btnSettings_Click(object sender, EventArgs e)
        {
            statusStripCrossroad.Items.Clear();
            ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Running Settings");
            statusStripCrossroad.Items.Add(lblStatus);

            Program3SettingsForm Settings = new Program3SettingsForm();
            Settings.ShowDialog(this);
        }
        #endregion


        private void Program3_Load(object sender, EventArgs e)
        {
            rBtnCrossroadBasic.Checked = true;
            rBtnCrossroadExtension1.Checked = false;
            rBtnCrossroadExtension2.Checked = false;
            rBtnCrossroadExtension3.Checked = false;

            Draw();

        }

        private void Draw()
        {
            if (rBtnCrossroadBasic.Checked)
            {
                // Vykreslete čáry pro křižovatku
                userControlCrossroad1.BasicCrossroad();
            }
            else if (rBtnCrossroadExtension1.Checked)
            {
                // Vykreslete čáry pro volbu 1
                userControlCrossroad1.CrossroadExtension1();
            }
            else if (rBtnCrossroadExtension2.Checked)
            {
                // Vykreslete čáry pro volbu 2
                userControlCrossroad1.CrossroadExtension2();

            }
            else if (rBtnCrossroadExtension3.Checked)
            {
                // Vykreslete čáry pro volbu 3
                userControlCrossroad1.CrossroadExtension3();

            }
        }

        #region Radiobutton clicked
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            statusStripCrossroad.Items.Clear();
            ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Basic crossroad");
            statusStripCrossroad.Items.Add(lblStatus);
        }

        private void rBtnCrossroadExtension1_CheckedChanged(object sender, EventArgs e)
        {
            statusStripCrossroad.Items.Clear();
            ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Crossroad extension 1");
            statusStripCrossroad.Items.Add(lblStatus);
        }

        private void rBtnCrossroadExtension2_CheckedChanged(object sender, EventArgs e)
        {
            statusStripCrossroad.Items.Clear();
            ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Crossroad extension 2");
            statusStripCrossroad.Items.Add(lblStatus);
        }

        private void rBtnCrossroadExtension3_CheckedChanged(object sender, EventArgs e)
        {
            statusStripCrossroad.Items.Clear();
            ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Crossroad extension 3");
            statusStripCrossroad.Items.Add(lblStatus);
        }
        #endregion

        //Emergency + system error 
        #region Emergency + system error 
        private void btnEmergency_Click(object sender, EventArgs e)
        {

        }
        #endregion

        //btn End 
        #region Close window 
        private void btnEnd_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        
    }
}
