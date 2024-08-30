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
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

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
        public byte[] read_buffer_DB28 = new byte[16];
        public byte[] previous_buffer_DB28;
        public byte[] PreviousBufferHash_DB28;
        public byte[] write_buffer_DB28 = new byte[16];

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
                            MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }

                if (!File.Exists(filePath))
                {
                    File.Create(filePath).Close();

                    //MessageBox
                    MessageBox.Show($"Info: \n" + "File created: " + filePath, "Info",
                            MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
                else
                {
                    /*
                    //MessageBox
                    MessageBox.Show($"Info: \n" + "File already exists: " + filePath, "Info",
                            MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    */
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

        public void AddDataToFile(object data, string filePath, string sectionName)
        {
            string existingJson = File.ReadAllText(filePath);
            /*
            var jsonData = JsonConvert.DeserializeObject<JObject>(existingJson);

            JArray dataList;
            if (jsonData.ContainsKey(sectionName))
            {
                dataList = jsonData[sectionName] as JArray;
            }
            else
            {
                dataList = new JArray();
                jsonData[sectionName] = dataList;
            }

            var dataObject = JObject.FromObject(data);

            dataObject.Remove("title");
            dataObject.Remove("data_time");
            dataObject.Remove("message");

            dataList.Add(dataObject);

            //dataList.Add(JObject.FromObject(data));
            */

            var jsonData = JsonConvert.DeserializeObject<JObject>(existingJson) ?? new JObject();

            var dataObject = JObject.FromObject(data);

            dataObject.Remove("title");
            dataObject.Remove("data_time");
            dataObject.Remove("message");

            jsonData[sectionName] = dataObject;

            string updatedJson = JsonConvert.SerializeObject(jsonData, Formatting.Indented);
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
            PopulateComboBoxWithJsonFiles();
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
                Time1 = S7.GetIntAt(read_buffer_DB28, 4);

                Int2 = S7.GetIntAt(read_buffer_DB28, 8);
                Bool2 = S7.GetBitAt(read_buffer_DB28, 10, 0);
                Time2 = S7.GetIntAt(read_buffer_DB28, 12);

                //writting to JSON
                Test_Class Test_DB = TestVariables();

                Test_DB.PLC_Int1 = Int1;
                Test_DB.PLC_Bool1 = Bool1;
                Test_DB.PLC_Time1 = Time1;
                Test_DB.PLC_Int2 = Int2;
                Test_DB.PLC_Bool2 = Bool2;
                Test_DB.PLC_Time2 = Time2;

                AddDataToFile(Test_DB, Test_JSONFilePath, "TestDB");
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

            //TextBox iput 
            #region TextBox input 

            bool Int1_verification;
            bool Bool1_verification;
            bool Time1_verification;
            bool Int2_verification;
            bool Bool2_verification;
            bool Time2_verification;

            if (!string.IsNullOrEmpty(textBoxInt1.Text))
            {
                if (int.TryParse(textBoxInt1.Text, out int value))
                {
                    Int1 = value;
                    Int1_verification = true;
                }
                else
                {
                    //error -> bad input
                    Int1_verification = false;
                }
            }
            else
            {
                //textbox is empty or null
                Int1_verification = false;
            }

            if (!string.IsNullOrEmpty(textBoxBool1.Text))
            {
                if (TryParseBoolean(textBoxBool1.Text, out bool value))
                {
                    Bool1 = value;
                    Bool1_verification = true;
                }
                else
                {
                    //error -> bad input
                    Bool1_verification = false;
                }
            }
            else
            {
                //textbox is empty or null
                Bool1_verification = false;
            }

            if (!string.IsNullOrEmpty(textBoxTime1.Text))
            {
                if (int.TryParse(textBoxTime1.Text, out int value))
                {
                    Time1 = value;
                    Time1_verification = true;
                }
                else
                {
                    //error -> bad input
                    Time1_verification = false;
                }
            }
            else
            {
                //textbox is empty or null
                Time1_verification = false;
            }

            if (!string.IsNullOrEmpty(textBoxInt2.Text))
            {
                if (int.TryParse(textBoxInt2.Text, out int value))
                {
                    Int2 = value;
                    Int2_verification = true;
                }
                else
                {
                    //error -> bad input
                    Int2_verification = false;
                }
            }
            else
            {
                //textbox is empty or null
                Int2_verification = false;
            }

            if (!string.IsNullOrEmpty(textBoxBool2.Text))
            {
                if (TryParseBoolean(textBoxBool2.Text, out bool value))
                {
                    Bool2 = value;
                    Bool2_verification = true;
                }
                else
                {
                    //error -> bad input
                    Bool2_verification = false;
                }
            }
            else
            {
                //textbox is empty or null
                Bool2_verification = false;
            }

            if (!string.IsNullOrEmpty(textBoxTime2.Text))
            {
                if (int.TryParse(textBoxTime2.Text, out int value))
                {
                    Time2 = value;
                    Time2_verification = true;
                }
                else
                {
                    //error -> bad input
                    Time2_verification = false;
                }
            }
            else
            {
                //textbox is empty or null
                Time2_verification = false;
            }

            #endregion

            if (Int1_verification == true)
                S7.SetIntAt(write_buffer_DB28, 0, (short)Int1);

            if (Bool1_verification == true)
                S7.SetBitAt(write_buffer_DB28, 2, 0, Bool1);

            if (Time1_verification == true)
                S7.SetDIntAt(write_buffer_DB28, 4, (short)Time1); //Time

            if (Int2_verification == true)
                S7.SetIntAt(write_buffer_DB28, 8, (short)Int2);

            if (Bool2_verification == true)
                S7.SetBitAt(write_buffer_DB28, 10, 0, Bool2);

            if (Time2_verification == true)
                S7.SetDIntAt(write_buffer_DB28, 12, (short)Time2); //Time

            writer.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, DBNumber_DB28, 0, write_buffer_DB28.Length, ref write_buffer_DB28);

            int writeResultDB28 = writer.Write();

            if (writeResultDB28 == 0)
            {
                //write successfull

                textBoxInt1.Clear();
                textBoxBool1.Clear();
                textBoxTime1.Clear();
                textBoxInt2.Clear();
                textBoxBool2.Clear();
                textBoxTime2.Clear();

                statusStripTestForm.Items.Clear();
                ToolStripStatusLabel lblStatus1 = new ToolStripStatusLabel("Variables were written to PLC.");
                statusStripTestForm.Items.Add(lblStatus1);
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
            statusStripTestForm.Items.Clear();
            ToolStripStatusLabel lblStatus1 = new ToolStripStatusLabel("Reading JSON...");
            statusStripTestForm.Items.Add(lblStatus1);

            string fullPath = Path.Combine(Application.StartupPath, Test_JSONFilePath);
            string fileContent = File.ReadAllText(fullPath);

            listBoxJSON.Items.Clear();
            listBoxJSONVariables.Items.Clear();

            /*
            var jsonData = JsonConvert.DeserializeObject<Test_Class>(fileContent);

            foreach (var property in jsonData.GetType().GetProperties())
            {
                string propertyName = property.Name;
                object propertyValue = property.GetValue(jsonData, null);
                listBoxJSONVariables.Items.Add($"{propertyName}: {propertyValue} \n");
            }
            */

            /*
            var jsonData = JsonConvert.DeserializeObject<Dictionary<string, JArray>>(fileContent);

            if (jsonData.ContainsKey("TestDB"))
            {
                foreach (JObject dataItem in jsonData["TestDB"])
                {
                    foreach (var property in dataItem.Properties())
                    {
                        listBoxJSONVariables.Items.Add($"{property.Name}: {property.Value}");
                    }
                }
            }
            */

            var jsonData = JsonConvert.DeserializeObject<JObject>(fileContent);
            /*
            foreach (var property in jsonData.Properties())
            {
                if (property.Value is JArray array)
                {
                    listBoxJSON.Items.Add($"{property.Name}:");
                    foreach (JObject dataItem in array)
                    {
                        foreach (var itemProperty in dataItem.Properties())
                        {
                            listBoxJSONVariables.Items.Add($"{itemProperty.Name}: {itemProperty.Value}");
                        }
                        listBoxJSONVariables.Items.Add(""); 
                    }
                }
                else
                {
                    listBoxJSON.Items.Add($"{property.Name}: {property.Value}");
                }
            }
            */

            listBoxJSON.Items.Add($"title: {jsonData["title"]}");
            listBoxJSON.Items.Add($"data_time: {jsonData["data_time"]}");
            listBoxJSON.Items.Add($"message: {jsonData["message"]}");

            if (jsonData.ContainsKey("TestDB"))
            {
                var testDbData = jsonData["TestDB"];

                // Zkontrolujte, zda je TestDB pole (JArray) nebo objekt (JObject)
                if (testDbData is JArray testDbArray)
                {
                    // Pokud je to pole, iterujeme přes každý objekt v poli
                    listBoxJSON.Items.Add("TestDB:");
                    foreach (JObject dataItem in testDbArray)
                    {
                        foreach (var itemProperty in dataItem.Properties())
                        {
                            listBoxJSONVariables.Items.Add($"{itemProperty.Name}: {itemProperty.Value}");
                        }
                        listBoxJSONVariables.Items.Add("");
                    }
                }
                else if (testDbData is JObject testDbObject)
                {
                    // Pokud je to objekt, přímo iterujeme přes jeho vlastnosti
                    listBoxJSON.Items.Add("TestDB:");
                    foreach (var itemProperty in testDbObject.Properties())
                    {
                        listBoxJSONVariables.Items.Add($"{itemProperty.Name}: {itemProperty.Value}");
                    }
                }
            }

            listBoxJSON.Items.Add(fileContent);

            statusStripTestForm.Items.Clear();
        }

        private void btnSendToJSON_Click(object sender, EventArgs e)
        {
            //TextBox iput 
            #region TextBox input 

            bool Int1_verification;
            bool Bool1_verification;
            bool Time1_verification;
            bool Int2_verification;
            bool Bool2_verification;
            bool Time2_verification;

            if (!string.IsNullOrEmpty(textBoxInt1.Text))
            {
                if (int.TryParse(textBoxInt1.Text, out int value))
                {
                    Int1 = value;
                    Int1_verification = true;
                }
                else
                {
                    //error -> bad input
                    Int1_verification = false;
                }
            }
            else
            {
                //textbox is empty or null
                Int1_verification = false;
            }

            if (!string.IsNullOrEmpty(textBoxBool1.Text))
            {
                if (TryParseBoolean(textBoxBool1.Text, out bool value))
                {
                    Bool1 = value;
                    Bool1_verification = true;
                }
                else
                {
                    //error -> bad input
                    Bool1_verification = false;
                }
            }
            else
            {
                //textbox is empty or null
                Bool1_verification = false;
            }

            if (!string.IsNullOrEmpty(textBoxTime1.Text))
            {
                if (int.TryParse(textBoxTime1.Text, out int value))
                {
                    Time1 = value;
                    Time1_verification = true;
                }
                else
                {
                    //error -> bad input
                    Time1_verification = false;
                }
            }
            else
            {
                //textbox is empty or null
                Time1_verification = false;
            }

            if (!string.IsNullOrEmpty(textBoxInt2.Text))
            {
                if (int.TryParse(textBoxInt2.Text, out int value))
                {
                    Int2 = value;
                    Int2_verification = true;
                }
                else
                {
                    //error -> bad input
                    Int2_verification = false;
                }
            }
            else
            {
                //textbox is empty or null
                Int2_verification = false;
            }

            if (!string.IsNullOrEmpty(textBoxBool2.Text))
            {
                if (TryParseBoolean(textBoxBool2.Text, out bool value))
                {
                    Bool2 = value;
                    Bool2_verification = true;
                }
                else
                {
                    //error -> bad input
                    Bool2_verification = false;
                }
            }
            else
            {
                //textbox is empty or null
                Bool2_verification = false;
            }

            if (!string.IsNullOrEmpty(textBoxTime2.Text))
            {
                if (int.TryParse(textBoxTime2.Text, out int value))
                {
                    Time2 = value;
                    Time2_verification = true;
                }
                else
                {
                    //error -> bad input
                    Time2_verification = false;
                }
            }
            else
            {
                //textbox is empty or null
                Time2_verification = false;
            }

            #endregion

            string fullPath = Path.Combine(Application.StartupPath, Test_JSONFilePath);
            EnsureFileExists(fullPath);

            Test_Class Test_DB = TestVariables();

            if (Int1_verification == true)
                Test_DB.PLC_Int1 = Int1;

            if (Bool1_verification == true)
                Test_DB.PLC_Bool1 = Bool1;

            if (Time1_verification == true)
                Test_DB.PLC_Time1 = Time1; //Time

            if (Int2_verification == true)
                Test_DB.PLC_Int2 = Int2;

            if (Bool2_verification == true)
                Test_DB.PLC_Bool2 = Bool2;

            if (Time2_verification == true)
                Test_DB.PLC_Time2 = Time2; //Time          

            //WriteDataToFileJSON(Test_JSONFilePath, Test_DB);
            AddDataToFile(Test_DB, Test_JSONFilePath, "TestDB");

            //write successfull

            textBoxInt1.Clear();
            textBoxBool1.Clear();
            textBoxTime1.Clear();
            textBoxInt2.Clear();
            textBoxBool2.Clear();
            textBoxTime2.Clear();


            statusStripTestForm.Items.Clear();
            ToolStripStatusLabel lblStatus1 = new ToolStripStatusLabel("Variables were written to JSON.");
            statusStripTestForm.Items.Add(lblStatus1);
        }

        //btn End
        #region Close window
        private void btnEnd_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        private bool TryParseBoolean(string input, out bool result)
        {
            input = input.ToLower().Trim();

            if (input == "true" || input == "1")
            {
                result = true;
                return true;
            }
            else if (input == "false" || input == "0")
            {
                result = false;
                return true;
            }
            else
            {
                result = false;
                return false;
            }
        }

        private void btnShowJson_Click(object sender, EventArgs e)
        {
            if (comboBoxFileChoice.SelectedItem != null)
            {
                string selectedFile = comboBoxFileChoice.SelectedItem.ToString();

                //string directoryPath = Path.Combine(Directory.GetParent(Application.StartupPath).Parent.Parent.FullName, "Data");

                string fullPath = Path.Combine(Application.StartupPath, "Data", selectedFile);

                // Zobrazení JSON souboru v Notepadu
                Process.Start(new ProcessStartInfo
                {
                    FileName = "notepad.exe",
                    Arguments = fullPath,
                    UseShellExecute = true
                });
            }
            else
            {
                MessageBox.Show("Please select a JSON file from the list.", "No File Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void PopulateComboBoxWithJsonFiles()
        {
            //string directoryPath = Path.Combine(Directory.GetParent(Application.StartupPath).Parent.Parent.FullName, "Data");


            string directoryPath = Path.Combine(Application.StartupPath, "Data");
            string[] jsonFiles = Directory.GetFiles(directoryPath, "*.json");

            comboBoxFileChoice.Items.Clear();
            foreach (string file in jsonFiles)
            {
                comboBoxFileChoice.Items.Add(Path.GetFileName(file));
            }

            if (comboBoxFileChoice.Items.Count > 0)
            {
                comboBoxFileChoice.SelectedIndex = 0; // Nastaví výchozí výběr na první soubor
            }
        }

        
    }
}
