using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JAN0837_BP.Classes;
using Newtonsoft.Json;
using System.Security.Cryptography;
using Sharp7;

namespace Bc_prace.Forms
{
    public partial class TestForm : Form
    {
        //files
        public const string Test_JSONFilePath = "/Data/Test.json";
        public const string Backup_JSONFilePath = "/Data/backupFile.json";
        public const string ElevatorDB_JSONFilePath = "/Data/ElevatorDB.json";
        public const string CarWashDB_JSONFilePath = "/Data/CarWashDB.json";
        public const string CrossroadDB_JSONFilePath = "/Data/CrossroadDB.json";
        public const string Logger_JSONFilePath = "/Data/Logger_file.json";
        public const string PLC_Startup_Data_JSONFilePath = "/Data/PLC_Startup_data.json";

        private ChooseOptionForm chooseOptionFormInstance;

        public S7Client client;

        //MessageBox control
        private bool errorMessageBoxShown = false;

        //Variables 
        #region Variables 

        //Buffers variables => Testing_DB
        private int DBNumber_DB28 = 28;
        public byte[] read_buffer_DB28 = new byte[10];
        public byte[] previous_buffer_DB28;
        public byte[] PreviousBufferHash_DB28;
        public byte[] write_buffer_DB28 = new byte[10];

        //Top
        private int Int1;
        private bool Bool1;
        private int Time1;
        //Bottom
        private int Int2;
        private bool Bool2;
        private int Time2;

        #endregion

        //Functions for work with JSON files
        #region Functions for work with JSON files
        public void CreateFileIfNotExists(string relativePath)
        {
            try
            {
                string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
                string directory = Path.GetDirectoryName(fullPath);

                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                if (!File.Exists(fullPath))
                {
                    File.Create(fullPath).Dispose();
                }
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

        public void EnsureFileExists(string filePath)
        {
            try
            {
                string directoryPath = Path.GetDirectoryName(filePath);

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);

                    //MessageBox
                    MessageBox.Show($"Info: \n" + "Directory created: " + directoryPath, "Info",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }

                if (!File.Exists(filePath))
                {
                    File.Create(filePath).Close();

                    //MessageBox
                    MessageBox.Show($"Info: \n" + "File created: " + filePath, "Info",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
                else
                {
                    //MessageBox
                    MessageBox.Show($"Info: \n" + "File already exists: " + filePath, "Info",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception ex)
            {
                errorMessageBoxShown = true;

                //MessageBox
                MessageBox.Show($"Error: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        public void WriteDataToFileJSON<T>(string filePath, T data)
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText(filePath, jsonData);
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

        public static T ReadDataFromFile<T>(string filePath)
        {
            try
            {

            }
            catch (Exception ex)
            {

            }

            if (File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<T>(jsonData);
            }

            return default(T);
        }

        public void AddDataToFile(object data, string filePath)
        {
            string existingJson = File.ReadAllText(filePath);
            List<object> existingData = JsonConvert.DeserializeObject<List<object>>(existingJson);

            existingData.Add(data);

            string updatedJson = JsonConvert.SerializeObject(existingData);
            File.WriteAllText(filePath, updatedJson);
        }

        public static Header_json_Class CreateHeader()
        {
            Header_json_Class result = new Header_json_Class
            {
                title = "Title",
                data_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                message = "Default Message"
            };

            return result;
        }

        public static Logger_Class Log()
        {
            Logger_Class result = new Logger_Class
            {
                title = "Title",
                data_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                author = "Author",
                message = "Message"
            };

            return result;
        }

        public static MaintainDB_Class MaintainDB()
        {
            MaintainDB_Class result = new MaintainDB_Class
            {

            };

            return result;
        }

        public static ElevatorDB_Class ElevatorDB()
        {
            ElevatorDB_Class result = new ElevatorDB_Class
            {

            };

            return result;
        }

        public static CarWashDB_Class CarWashDB()
        {
            CarWashDB_Class result = new CarWashDB_Class
            {

            };

            return result;
        }

        public static CrossroadDB_Class CrossroadDB()
        {
            CrossroadDB_Class result = new CrossroadDB_Class
            {

            };

            return result;
        }
                
        public static Test_Class TestVariables()
        {
            Test_Class result = new Test_Class
            {
                /*
                PLC_Int1 =
                PLC_Bool1 = 
                PLC_Time1 = 

                PLC_Int2 = 
                PLC_Bool2 = 
                PLC_Time2 = 
                */
            };

            return result;
        }


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
                Periodic_Function.Start();
                //set time interval (ms)
                Periodic_Function.Interval = 100;
            }
        }

        private void Test_Load(object sender, EventArgs e)
        {

        }

        private async void Periodic_Function_Tick(object sender, EventArgs e)
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
        
        private void btnReadFromPLC_Click(object sender, EventArgs e)
        {
            S7MultiVar reader = new S7MultiVar(client);

            reader.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, DBNumber_DB28, 0, read_buffer_DB28.Length, ref read_buffer_DB28);

            int readResultDB28 = reader.Read();

            if (readResultDB28 == 0)
            {
                Int1 = S7.GetIntAt(read_buffer_DB28, 0);
                Bool1 = S7.GetBitAt(read_buffer_DB28, 2, 0);
                Time1 = S7.GetIntAt(read_buffer_DB28, 3);

                Int2 = S7.GetIntAt(read_buffer_DB28, 5);
                Bool2 = S7.GetBitAt(read_buffer_DB28, 7, 0);
                Time2 = S7.GetIntAt(read_buffer_DB28, 9);
            }
            else
            {
                //error
                statusStripTestForm.Items.Clear();
                ToolStripStatusLabel lblStatus1 = new ToolStripStatusLabel("Variables were not read.");
                statusStripTestForm.Items.Add(lblStatus1);

                if (!errorMessageBoxShown)
                {
                    errorMessageBoxShown = true;

                    //MessageBox
                    MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data weren't read from DB28!!! \n\n" +
                        $"Error message {readResultDB28} \n", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
        }

        private void btnSendToPLC_Click(object sender, EventArgs e)
        {
            S7MultiVar writer = new S7MultiVar(client);



            S7.SetIntAt(write_buffer_DB28, 0, (short)Int1);
            S7.SetBitAt(write_buffer_DB28, 2, 0, Bool1);
            S7.SetIntAt(write_buffer_DB28, 3, (short)Time1);

            S7.SetIntAt(write_buffer_DB28, 5, (short)Int2);
            S7.SetBitAt(write_buffer_DB28, 7, 0, Bool2);
            S7.SetIntAt(write_buffer_DB28, 9, (short)Time2);

            writer.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, DBNumber_DB28, 0, read_buffer_DB28.Length, ref read_buffer_DB28);

            int writeResultDB28 = writer.Write();

            if (writeResultDB28 == 0)
            {
                //
            }
            else
            {
                //error
                statusStripTestForm.Items.Clear();
                ToolStripStatusLabel lblStatus1 = new ToolStripStatusLabel("Variables were not read.");
                statusStripTestForm.Items.Add(lblStatus1);

                if (!errorMessageBoxShown)
                {
                    errorMessageBoxShown = true;

                    //MessageBox
                    MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data weren't written to DB28!!! \n\n" +
                        $"Error message {writeResultDB28} \n", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
        }
        private void btnReadJSON_Click(object sender, EventArgs e)
        {
            string fullPath = Path.Combine(Application.StartupPath, Test_JSONFilePath);
            string fileContent = File.ReadAllText(fullPath);

            listBoxJSON.Items.Clear();
            listBoxJSONVariables.Items.Clear();

            var items = JsonConvert.DeserializeObject<List<string>>(fileContent);
            foreach (var item in items)
            {
                listBoxJSON.Items.Add(item);
            }

            List<string> variables = ReadDataFromFile<List<string>>(fullPath);
            foreach(var variable in variables)
            {
                listBoxJSONVariables.Items.Add(variable);
            }
        }

        private void btnSendToJSON_Click(object sender, EventArgs e)
        {
            string fullPath = Path.Combine(Application.StartupPath, Test_JSONFilePath);
            EnsureFileExists(fullPath);

            Test_Class results = TestVariables();
            WriteDataToFileJSON(Test_JSONFilePath, results);
        }

        //btn End
        #region Close window
        private void btnEnd_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
