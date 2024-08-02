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

namespace Bc_prace.Forms
{
    public partial class TestForm : Form
    {
        //files
        public const string backupJSONFilePath = "~/Data/backupFile.json";
        public const string ElevatroDBJSONFilePath = "~/Data/ElevatorDB.json";
        public const string CarWashDBJSONFilePath = "~/Data/CarWashDB.json";
        public const string CrossroadDBJSONFilePath = "~/Data/CrossroadDB.json";
        public const string logger_file = "~/Data/Logger_file.json";

        private ChooseOptionForm chooseOptionFormInstance;

        public S7Client client;

        //MessageBox control
        private bool errorMessageBoxShown = false;

        //Variables 
        #region Variables 

        #endregion

        public TestForm(ChooseOptionForm chooseOptionFormInstance)
        {
            InitializeComponent();
            //this.Minimumsize = new Size(x, y);

            this.chooseOptionFormInstance = chooseOptionFormInstance;

            client = chooseOptionFormInstance.client;

            //Buffers initialize
            #region 

            //here will be your buffers 

            #endregion

            if (client.Connected)
            {
                //start timer
                Timer_read_actual.Start();
                //set time interval (ms)
                Timer_read_actual.Interval = 100;
            }
        }

        private void Test_Load(object sender, EventArgs e)
        {

        }

        private async void Timer_read_actual_Tick(object sender, EventArgs e)
        {
            try
            {
                //Here is your variables for reading 

                //Action on variable value
                #region Action on variable value

                #endregion

                errorMessageBoxShown = false;
            }
            catch (Exception ex)
            {
                if (!errorMessageBoxShown)
                {
                    errorMessageBoxShown = true;

                    //MessageBox
                    MessageBox.Show($"Error: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
        }

        private void btnEmergency_Click(object sender, EventArgs e)
        {

        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRead_Click(object sender, EventArgs e)
        {

        }

        private void btnSend_Click(object sender, EventArgs e)
        {

        }

        private void btnReadJSON_Click(object sender, EventArgs e)
        {

        }
    }
}
