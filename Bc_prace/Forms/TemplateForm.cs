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
    public partial class TemplateForm : Form
    {
        //Paths
        public static string projectRootPath = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\..\"));
        public static string dataDirectoryPath = Path.Combine(projectRootPath, "Data");
        public static string resourcesDirectoryPath = Path.Combine(projectRootPath, "Resources");

        //files
        public const string Test_JSONFilePath = "Test.json";
        public const string Backup_JSONFilePath = "backupFile.json";
        public const string MaintainDB_JSONFilePath = "MaintainDB.json";
        public const string ElevatorDB_JSONFilePath = "ElevatorDB.json";
        public const string CarWashDB_JSONFilePath = "CarWashDB.json";
        public const string CrossroadDB_JSONFilePath = "CrossroadDB.json";
        public const string Logger_JSONFilePath = "Logger_file.json";
        public const string PLC_Startup_Data_JSONFilePath = "PLC_Startup_data.json";

        //pictures 
        public static string Car64Path = "car_64.png";
        public static string CarBrushes64Path = "car_brushes_64.png";
        public static string CarDone64Path = "car_done_64.png";
        public static string CarWashing64Path = "car_washing_64.png";

        private ChooseOptionForm chooseOptionFormInstance;

        public S7Client client;

        //MessageBox control
        private bool errorMessageBoxShown = false;

        //Variables 
        #region Variables 

        #endregion

        public TemplateForm(ChooseOptionForm chooseOptionFormInstance)
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

        private void Template_Load(object sender, EventArgs e)
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
    }
}
