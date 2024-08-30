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
using System.Security.Cryptography;
using System.Web;
using Newtonsoft.Json;
using JAN0837_BP.Classes;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using Bc_prace.Forms;

namespace Bc_prace
{
    public partial class ChooseOptionForm : Form
    {
        public S7Client client = new S7Client();

        //MessageBox control
        private bool errorMessageBoxShown;

        //files
        public const string Test_JSONFilePath = "/Data/Test.json";
        public const string Backup_JSONFilePath = "/Data/backupFile.json";
        public const string MaintainDB_JSONFilePath = "/Data/MaintainDB.json";
        public const string ElevatorDB_JSONFilePath = "/Data/ElevatorDB.json";
        public const string CarWashDB_JSONFilePath = "/Data/CarWashDB.json";
        public const string CrossroadDB_JSONFilePath = "/Data/CrossroadDB.json";
        public const string Logger_JSONFilePath = "/Data/Logger_file.json";
        public const string PLC_Startup_Data_JSONFilePath = "/Data/PLC_Startup_data.json";

        //
        private bool BackupDataDone = false;

        ElevatorForm Program1 = null;
        CarWashForm Program2 = null;
        CrossroadForm Program3 = null;

        TestForm TestForm = null;

        private static bool program1Opened = false;
        private static bool program2Opened = false;
        private static bool program3Opened = false;

        private static bool TestFormOpened = false;

        //Buffers variables 
        #region Buffers variables

        //DB11 => Maintain_DB 
        private int DBNumber_DB11 = 11;
        public byte[] read_buffer_DB11 = new byte[1];
        public byte[] previous_buffer_DB11;
        public byte[] PreviousBufferHash_DB11;
        public byte[] send_buffer_DB11 = new byte[1];

        //DB4 => Elevator_DB 
        private int DBNumber_DB4 = 4;
        public byte[] read_buffer_DB4 = new byte[30];
        public byte[] previous_buffer_DB4;
        public byte[] PreviousBufferHash_DB4;
        public byte[] send_buffer_DB4 = new byte[30];

        //DB5 => CarWash_DB 
        private int DBNumber_DB5 = 5;
        public byte[] read_buffer_DB5 = new byte[11];
        public byte[] previous_buffer_DB5;
        public byte[] PreviousBufferHash_DB5;
        public byte[] send_buffer_DB5 = new byte[11];

        //DB14 => Crossroad_DB 
        private int DBNumber_DB14 = 14;
        public byte[] read_buffer_DB14 = new byte[4];
        public byte[] previous_buffer_DB14;
        public byte[] PreviousBufferHash_DB14;
        public byte[] send_buffer_DB14 = new byte[4];
        //+ other structs are Timers 

        //DB1 => Crossroad_1_DB 
        private int DBNumber_DB1 = 1;
        public byte[] read_buffer_DB1 = new byte[7];
        public byte[] previous_buffer_DB1;
        public byte[] PreviousBufferHash_DB1;
        public byte[] send_buffer_DB1 = new byte[7];

        //DB19 => Crossroad_2_DB 
        private int DBNumber_DB19 = 19;
        public byte[] read_buffer_DB19 = new byte[7];
        public byte[] previous_buffer_DB19;
        public byte[] PreviousBufferHash_DB19;
        public byte[] send_buffer_DB19 = new byte[7];

        //DB20 => Crossroad_LeftT_DB 
        private int DBNumber_DB20 = 20;
        public byte[] read_buffer_DB20 = new byte[6];
        public byte[] previous_buffer_DB20;
        public byte[] PreviousBufferHash_DB20;
        public byte[] send_buffer_DB20 = new byte[6];

        //DB21 => Crossroad_RightT_DB 
        private int DBNumber_DB21 = 21;
        public byte[] read_buffer_DB21 = new byte[6];
        public byte[] previous_buffer_DB21;
        public byte[] PreviousBufferHash_DB21;
        public byte[] send_buffer_DB21 = new byte[6];

        #endregion

        //MaintainDB variables
        #region MaintainDB variables

        public bool Option1 = false;
        public bool Option2 = false;
        public bool Option3 = false;

        #endregion

        //ElevatorDB variables 
        #region ElevatorDB variables 

        //Input variables 
        #region Input variables 

        public bool ElevatorBTNCabin1;
        public bool ElevatorBTNCabin2;
        public bool ElevatorBTNCabin3;
        public bool ElevatorBTNCabin4;
        public bool ElevatorBTNCabin5;
        public bool ElevatorBTNFloor1;
        public bool ElevatorBTNFloor2;
        public bool ElevatorBTNFloor3;
        public bool ElevatorBTNFloor4;
        public bool ElevatorBTNFloor5;
        public bool ElevatorDoorSEQ;
        public bool ElevatorBTNOPENCLOSE;
        public bool ElevatorEmergencySTOP;
        public bool ElevatorErrorSystem;
        public bool ElevatorActualFloorSENS1;
        public bool ElevatorActualFloorSENS2;
        public bool ElevatorActualFloorSENS3;
        public bool ElevatorActualFloorSENS4;
        public bool ElevatorActualFloorSENS5;
        public bool ElevatorDoorClOSE;
        public bool ElevatorDoorOPEN;
        public bool ElevatorInactivity;

        #endregion

        //Output variables
        #region Output variables

        public bool ElevatorMotorON;
        public bool ElevatorMotorDOWN;
        public bool ElevatorMotorUP;
        public bool ElevatroHoming;
        public bool ElevatorSystemReady;
        public int ElevatorActualFloor;
        public bool ElevatorMoving;
        public bool ElevatorSystemWorking;
        public int ElevatorGoToFloor;
        public bool ElevatorDirection;
        public bool ElevatorActualFloorLED1;
        public bool ElevatorActualFloorLED2;
        public bool ElevatorActualFloorLED3;
        public bool ElevatorActualFloorLED4;
        public bool ElevatorActualFloorLED5;
        public bool ElevatorActualFloorCabinLED1;
        public bool ElevatorActualFloorCabinLED2;
        public bool ElevatorActualFloorCabinLED3;
        public bool ElevatorActualFloorCabinLED4;
        public bool ElevatorActualFloorCabinLED5;
        public int ElevatorTimeDoorSQOPEN; //time
        public int ElevatroTimeDoorSQCLOSE; //time
        public int ElevatorCabinSpeed;
        public int ElevatorTimeToGetDown; //time

        #endregion

        //MEM variables 
        #region MEM variables

        public bool ElevatorMEMDoor;
        public bool ElevatorMEMDoorTrig;
        public bool ElevatorMEMDoorCloseTrig;
        public bool ElevatorMEMMovingtrig;
        public bool ElevatorMEMEndMovingTrig;
        public bool ElevatorMEMBTNFloor1;
        public bool ElevatorMEMBTNFloor2;
        public bool ElevatorMEMBTNFloor3;
        public bool ElevatorMEMBTNFloor4;
        public bool ElevatorMEMBTNFloor5;

        #endregion

        #endregion

        //CarWashDB variables 
        #region CarWashDB variables

        //Input variables
        #region Input variables

        public bool CarWashEmergencySTOP;
        public bool CarWashErrorSystem;
        public bool CarWashStartCarWash;
        public bool CarWashWaitingForIncomingCar;
        public bool CarWashWaitingForOutgoingCar;
        public bool CarWashPerfetWash;
        public bool CarWashPerfectPolish;
        public bool CarWashPositionShower;
        public bool CarWashPositionCar;

        #endregion

        //Output variables
        #region Output variables 

        public bool CarWashGreenLight;
        public bool CarWashRedLight;
        public bool CarWashYellowLight;
        public bool CarWashDoor1UP;
        public bool CarWashDoor1DOWN;
        public bool CarWashDoor2UP;
        public bool CarWashDoor2DOWN;
        public bool CarWashWater;
        public bool CarWashWashingChemicalsFRONT;
        public bool CarWashWashingChemicalsSIDES;
        public bool CarWashWashingChemicalsBACK;
        public bool CarWashWax;
        public bool CarWashVarnishProtection;
        public bool CarWashDry;
        public bool CarWashPreWash;
        public bool CarWashBrushes;
        public bool CarWashSoap;
        public bool CarWashActiveFoam;
        public int CarWashTimeDoorMovement; //time

        #endregion

        //MEM variables 
        #region MEM variables 

        public bool CarWashMEMDoor;
        public bool CarWashMEMDoorTrig;
        public bool CarWashMEMDoorCloseTrig;

        #endregion

        #endregion

        //CrossroadDB variables 
        #region CrossroadDB variables

        //Input variables 
        #region Input variables 

        //Crossroad_DB DB14
        #region Crossroad_DB DB14

        public bool CrossroadModeOFF;
        public bool CrossroadModeNIGHT;
        public bool CrossroadModeDAY;
        public bool CrossroadEmergencySTOP;
        public bool CrossroadErrorSystem;

        #endregion

        //Crossroad_1_DB DB1
        #region Crossroad_1_DB DB1

        public bool Crossroad1LeftCrosswalkBTN1;
        public bool Crossroad1LeftCrosswalkBTN2;
        public bool Crossroad1TopCrosswalkBTN1;
        public bool Crossroad1TopCrosswalkBTN2;

        #endregion

        //Crossroad_2_DB DB19
        #region Crossroad_2_DB DB19

        public bool Crossroad2LeftCrosswalkBTN1;
        public bool Crossroad2LeftCrosswalkBTN2;
        public bool Crossroad2RightCrosswalkBTN1;
        public bool Crossroad2RightCrosswalkBTN2;

        #endregion

        //Crossroad_LeftT_DB DB20
        #region Crossroad_LeftT_DB DB20

        public bool CrossroadLeftTLeftCrosswalkBTN1;
        public bool CrossroadLeftTLeftCrosswalkBTN2;

        #endregion

        //Crossroad_RightT_DB DB21
        #region Crossroad_RightT_DB DB21

        public bool CrossroadRightTTopCrosswalkBTN1;
        public bool CrossroadRightTTopCrosswalkBTN2;

        #endregion

        #endregion

        //Output variables 
        #region Output variables 

        //Crossroad_DB DB14
        #region Crossroad_DB DB14

        public int TrafficLightsSQ;

        #endregion

        //Crossroad_1_DB DB1
        #region Crossroad_1_DB DB1

        public int Crossroad1CrosswalkSQ;

        public bool Crossroad1TopRED;
        public bool Crossroad1TopGREEN;
        public bool Crossroad1TopYELLOW;
        public bool Crossroad1LeftRED;
        public bool Crossroad1LeftGREEN;
        public bool Crossroad1LeftYELLOW;
        public bool Crossroad1RightRED;
        public bool Crossroad1RightGREEN;
        public bool Crossroad1RightYELLOW;
        public bool Crossroad1BottomRED;
        public bool Crossroad1BottomGREEN;
        public bool Crossroad1BottomYELLOW;

        public bool Crossroad1TopCrosswalkRED1;
        public bool Crossroad1TopCrosswalkRED2;
        public bool Crossroad1TopCrosswalkGREEN1;
        public bool Crossroad1TopCrosswalkGREEN2;
        public bool Crossroad1LeftCrosswalkRED1;
        public bool Crossroad1LeftCrosswalkRED2;
        public bool Crossroad1LeftCrosswalkGREEN1;
        public bool Crossroad1LeftCrosswalkGREEN2;

        #endregion

        //Crossroad_2_DB DB19
        #region Crossroad_2_DB DB19

        public int Crossroad2CrosswalkSQ;

        public bool Crossroad2TopRED;
        public bool Crossroad2TopGREEN;
        public bool Crossroad2TopYELLOW;
        public bool Crossroad2LeftRED;
        public bool Crossroad2LeftGREEN;
        public bool Crossroad2LeftYELLOW;
        public bool Crossroad2RightRED;
        public bool Crossroad2RightGREEN;
        public bool Crossroad2RightYELLOW;
        public bool Crossroad2BottomRED;
        public bool Crossroad2BottomGREEN;
        public bool Crossroad2BottomYELLOW;

        public bool Crossroad2LeftCrosswalkRED1;
        public bool Crossroad2LeftCrosswalkRED2;
        public bool Crossroad2LeftCrosswalkGREEN1;
        public bool Crossroad2LeftCrosswalkGREEN2;
        public bool Crossroad2RightCrosswalkRED1;
        public bool Crossroad2RightCrosswalkRED2;
        public bool Crossroad2RightCrosswalkGREEN1;
        public bool Crossroad2RightCrosswalkGREEN2;

        #endregion

        //Crossroad_LeftT_DB DB20
        #region Crossroad_LeftT_DB DB20

        public int CrossroadLeftTCrosswalkSQ;

        public bool CrossroadLeftTTopRED;
        public bool CrossroadLeftTTopGREEN;
        public bool CrossroadLeftTTopYELLOW;
        public bool CrossroadLeftTLeftRED;
        public bool CrossroadLeftTLeftGREEN;
        public bool CrossroadLeftTLeftYELLOW;
        public bool CrossroadLeftTRightRED;
        public bool CrossroadLeftTRightGREEN;
        public bool CrossroadLeftTRightYELLOW;

        public bool CrossroadLeftTLeftCrosswalkRED1;
        public bool CrossroadLeftTLeftCrosswalkRED2;
        public bool CrossroadLeftTLeftCrosswalkGREEN1;
        public bool CrossroadLeftTLeftCrosswalkGREEN2;

        #endregion

        //Crossroad_RightT_DB DB21
        #region Crossroad_RightT_DB DB21

        public int CrossroadRightTCrosswalkSQ;

        public bool CrossroadRightTTopRED;
        public bool CrossroadRightTTopGREEN;
        public bool CrossroadRightTTopYELLOW;
        public bool CrossroadRightTLeftRED;
        public bool CrossroadRightTLeftGREEN;
        public bool CrossroadRightTLeftYELLOW;
        public bool CrossroadRightTRightRED;
        public bool CrossroadRightTRightGREEN;
        public bool CrossroadRightTRightYELLOW;

        public bool CrossroadRightTTopCrosswalkRED1;
        public bool CrossroadRightTTopCrosswalkRED2;
        public bool CrossroadRightTTopCrosswalkGREEN1;
        public bool CrossroadRightTTopCrosswalkGREEN2;

        #endregion

        #endregion

        //MEMs

        #endregion

        public ChooseOptionForm()
        {
            InitializeComponent();
            this.MinimumSize = new Size(450, 300);
        }

        private void ChooseOption_Load(object sender, EventArgs e)
        {
            //visibility 
            lblChooseSIM.Visible = false;
            btnElevator.Visible = false;
            btnCarWash.Visible = false;
            btnCrossroad.Visible = false;

            //Files verification and header
            #region Files verification and header

            //string directoryPath = Path.Combine(Directory.GetParent(Application.StartupPath).Parent.Parent.FullName, "Data");


            //Test
            string TestfullPath = Path.Combine(Application.StartupPath, Test_JSONFilePath);
            EnsureFileExists(TestfullPath);
            Header_json_Class TestHeader = CreateHeader();
            TestHeader.title = "Test file";
            TestHeader.message = "Here is some test data: \n";
            WriteDataToFileJSON(TestfullPath, TestHeader);

            //Backup data
            string BackupfullPath = Path.Combine(Application.StartupPath, Backup_JSONFilePath);
            EnsureFileExists(BackupfullPath);
            Header_json_Class BackupHeader = CreateHeader();
            BackupHeader.title = "Backup data";
            BackupHeader.message = "Backup of all variables: \n";
            WriteDataToFileJSON(BackupfullPath, BackupHeader);

            //Maintain data
            string MaintainfullPath = Path.Combine(Application.StartupPath, MaintainDB_JSONFilePath);
            EnsureFileExists(MaintainfullPath);
            Header_json_Class MaintainHeader = CreateHeader();
            MaintainHeader.title = "Maintain variables";
            MaintainHeader.message = "MaintainDB: \n";
            WriteDataToFileJSON(MaintainfullPath, MaintainHeader);

            //Elevator data
            string ElevatorfullPath = Path.Combine(Application.StartupPath, ElevatorDB_JSONFilePath);
            EnsureFileExists(ElevatorfullPath);
            Header_json_Class ElevatorHeader = CreateHeader();
            ElevatorHeader.title = "Elevator variables";
            ElevatorHeader.message = "ElevatorDB: \n";
            WriteDataToFileJSON(ElevatorfullPath, ElevatorHeader);

            //CarWash data
            string CarWashfullPath = Path.Combine(Application.StartupPath, CarWashDB_JSONFilePath);
            EnsureFileExists(CarWashfullPath);
            Header_json_Class CarWashHeader = CreateHeader();
            CarWashHeader.title = "CarWash variables";
            CarWashHeader.message = "CarWashDB: \n";
            WriteDataToFileJSON(CarWashfullPath, CarWashHeader);

            //Crossroad data
            string CrossoradfullPath = Path.Combine(Application.StartupPath, CrossroadDB_JSONFilePath);
            EnsureFileExists(CrossoradfullPath);
            Header_json_Class CrossroadHeader = CreateHeader();
            CrossroadHeader.title = "CrossroadDB variables";
            CrossroadHeader.message = "CrossroadDB: \n";
            WriteDataToFileJSON(CrossoradfullPath, CrossroadHeader);

            //Logger data
            string LoggerfullPath = Path.Combine(Application.StartupPath, Logger_JSONFilePath);
            EnsureFileExists(LoggerfullPath);
            Header_json_Class LoggerHeader = CreateHeader();
            LoggerHeader.title = "Logger file";
            LoggerHeader.message = "Logger messages: \n";
            WriteDataToFileJSON(LoggerfullPath, LoggerHeader);

            //PLC Startup data
            string PLC_Startup_Data_fullPath = Path.Combine(Application.StartupPath, PLC_Startup_Data_JSONFilePath);
            EnsureFileExists(PLC_Startup_Data_fullPath);
            Header_json_Class PLC_Startup_Data_Header = CreateHeader();
            PLC_Startup_Data_Header.title = "PLC Startup Data";
            PLC_Startup_Data_Header.message = "StartupDB: \n";
            WriteDataToFileJSON(PLC_Startup_Data_fullPath, PLC_Startup_Data_Header);

            /*
            CreateFileIfNotExists(TestJSONFilePath);
            CreateFileIfNotExists(BackupJSONFilePath);
            CreateFileIfNotExists(ElevatroDBJSONFilePath);
            CreateFileIfNotExists(CarWashDBJSONFilePath);
            CreateFileIfNotExists(CrossroadDBJSONFilePath);
            CreateFileIfNotExists(Logger_file);
            */

            //test json 
            /*
            string fullPath = Path.Combine(Application.StartupPath, TestJSONFilePath);
            EnsureFileExists(fullPath);

            OperationResults results = CalculateResults();
            WriteDataToFileJSON(testJSONFilePath, results);

            string fileContent = File.ReadAllText(fullPath);
            //showing json file
            Process.Start(new ProcessStartInfo
            {
                FileName = "notepad.exe",
                Arguments = fullPath,
                UseShellExecute = true
            });
            */

            #endregion
        }

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
                    //MessageBox
                    /*
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

            var jsonData = JsonConvert.DeserializeObject<Dictionary<string, object>>(existingJson);

            JArray dataList;
            if (jsonData.ContainsKey(sectionName))
            {
                dataList = jsonData[sectionName] as JArray;
            }
            else
            {
                dataList = new JArray();
            }

            dataList.Add(JObject.FromObject(data));

            jsonData[sectionName] = dataList;

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

        //testing
        /*
        public static TestingResults CalculateResults()
        {
            TestingResults results = new TestingResults
            {
                Sum_3_3 = 3 + 3,
                Concatenate_3_3 = "3" + "3",
                Concatenate_3_String3 = 3 + "3",
                Concatenate_String3_3 = "3" + 3,
                Expression_3_3_3 = 3 + 3 - 3,
                Expression_String3_String3_String3 = int.Parse("3") + int.Parse("3") - int.Parse("3")
            };

            return results;
        }
        */
        #endregion

        //Choices and messages 
        #region Choose your simulation
        private void btnProgram1_Click(object sender, EventArgs e)
        {
            S7MultiVar writer = new S7MultiVar(client);

            if (!program1Opened)
            {
                statusStripChooseOption.Items.Clear();
                ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Running Program 1");
                statusStripChooseOption.Items.Add(lblStatus);

                Option1 = true;
                S7.SetBitAt(send_buffer_DB11, 0, 0, Option1);

                //write to PLC
                int writeResultDB11 = client.DBWrite(DBNumber_DB11, 0, send_buffer_DB11.Length, send_buffer_DB11);
                if (writeResultDB11 == 0)
                {
                    //write was successful
                }
                else
                {
                    //write error
                    if (!errorMessageBoxShown)
                    {
                        errorMessageBoxShown = true;

                        //MessageBox
                        MessageBox.Show("BE doesn't work properly. Data could´t be written to DB11!!! \n\n" +
                            $"Error message: {writeResultDB11} \n", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }

                Program1 = new ElevatorForm(this);
                Program1.Show();
                program1Opened = true;

                Program1.FormClosed += (sender, e) => { program1Opened = false; };
            }
            else
            {
                statusStripChooseOption.Items.Clear();
                ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Program 1 is already running.");
                statusStripChooseOption.Items.Add(lblStatus);

                if (Program1 != null && !Program1.IsDisposed)
                {
                    Program1.BringToFront();
                }
            }
        }

        private void btnProgram2_Click(object sender, EventArgs e)
        {
            S7MultiVar writer = new S7MultiVar(client);

            if (!program2Opened)
            {
                statusStripChooseOption.Items.Clear();
                ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Running Program 2");
                statusStripChooseOption.Items.Add(lblStatus);

                Option2 = true;
                S7.SetBitAt(send_buffer_DB11, 0, 1, Option2);

                //write to PLC
                int writeResultDB11 = client.DBWrite(DBNumber_DB11, 0, send_buffer_DB11.Length, send_buffer_DB11);
                if (writeResultDB11 == 0)
                {
                    //write was successful
                }
                else
                {
                    //write error
                    if (!errorMessageBoxShown)
                    {
                        errorMessageBoxShown = true;

                        //MessageBox
                        MessageBox.Show("BE doesn't work properly. Data could´t be written to DB11!!! \n\n" +
                            $"Error message: {writeResultDB11} \n", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }

                Program2 = new CarWashForm(this);
                Program2.Show();
                program2Opened = true;

                Program2.FormClosed += (sender, e) => { program2Opened = false; };
            }
            else
            {
                statusStripChooseOption.Items.Clear();
                ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Program 2 is already running");
                statusStripChooseOption.Items.Add(lblStatus);

                if (Program2 != null && !Program2.IsDisposed)
                {
                    Program2.BringToFront();
                }
            }
        }

        private void btnProgram3_Click(object sender, EventArgs e)
        {
            S7MultiVar writer = new S7MultiVar(client);

            if (!program3Opened)
            {
                statusStripChooseOption.Items.Clear();
                ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Running Program 3");
                statusStripChooseOption.Items.Add(lblStatus);

                Option3 = true;
                S7.SetBitAt(send_buffer_DB11, 0, 2, Option3);

                //write to PLC
                int writeResultDB11 = client.DBWrite(DBNumber_DB11, 0, send_buffer_DB11.Length, send_buffer_DB11);
                if (writeResultDB11 == 0)
                {
                    //write was successful
                }
                else
                {
                    //write error
                    if (!errorMessageBoxShown)
                    {
                        errorMessageBoxShown = true;

                        //MessageBox
                        MessageBox.Show("BE doesn't work properly. Data could´t be written to DB11!!! \n\n" +
                            $"Error message: {writeResultDB11} \n", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }

                Program3 = new CrossroadForm(this);
                Program3.Show();
                program3Opened = true;

                Program3.FormClosed += (sender, e) => { program3Opened = false; };
            }
            else
            {
                statusStripChooseOption.Items.Clear();
                ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Program 3 is already running");
                statusStripChooseOption.Items.Add(lblStatus);


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
            statusStripChooseOption.Items.Clear();
            ToolStripStatusLabel lblStat = new ToolStripStatusLabel("Connecting to " + txtBoxPLCIP.Text);
            statusStripChooseOption.Items.Add(lblStat);

            //0 -> MPI -> Multi Point Interface -> zde připojení nefunguje 
            //1 -> PPI -> Point to Point interface
            //2 -> OP -> Engineering point
            //3 -> S7 Basic -> S7 communication using Ethernet or Profibus
            //10 -> ISOTCP -> TCP/IP protocol -> Ethernet -> zde připojení nefunguje
            client.SetConnectionType(1);

            int plcConnect = client.ConnectTo(txtBoxPLCIP.Text, 0, 1);

            if (plcConnect == 0)
            {
                statusStripChooseOption.Items.Clear();
                ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Connected to " + txtBoxPLCIP.Text);
                statusStripChooseOption.Items.Add(lblStatus);
                btnConnect.Text = "Connected";

                //start Timer_read_from_PLC
                Timer_read_from_PLC.Start();
                //set time interval (ms)
                Timer_read_from_PLC.Interval = 100;

                //btns visibility
                lblChooseSIM.Visible = true;
                btnElevator.Visible = true;
                btnCarWash.Visible = true;
                btnCrossroad.Visible = true;
                btnConnect.Visible = false;
                btnDisconnect.Visible = true;

                //work with .json file


            }
            else
            {
                statusStripChooseOption.Items.Clear();
                ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Connection to " + txtBoxPLCIP.Text + " FAILED! Please, chech your IP address or PLC itself.");
                statusStripChooseOption.Items.Add(lblStatus);
                btnConnect.Text = "Connect";

                //MessageBox
                MessageBox.Show("PLC didn´t connected. Please, chech your IP address or PLC itself.\n\n" +
                    $"Error message: {plcConnect} \n", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        #endregion

        //Disconnect + messages
        #region Disconnect from PLC
        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            S7MultiVar writer = new S7MultiVar(client);

            ToolStripStatusLabel lblStat;

            btnDisconnect.Text = "Disconnecting...";
            statusStripChooseOption.Items.Clear();
            lblStat = new ToolStripStatusLabel("Disconnecting from " + txtBoxPLCIP.Text);
            statusStripChooseOption.Items.Add(lblStat);

            //stop Timer_read_from_PLC
            Timer_read_from_PLC.Stop();

            //
            BackupDataDone = false;

            //btns visibility
            lblChooseSIM.Visible = false;
            btnElevator.Visible = false;
            btnCarWash.Visible = false;
            btnCrossroad.Visible = false;
            btnConnect.Visible = false;
            btnDisconnect.Visible = true;

            //Reading variables form backup and writting them to DBs
            #region Reading variables form backup and writting them to DBs

            //MaintainDB DB11
            MaintainDB_Class MaintainDB_Data_Backup = ReadDataFromFile<MaintainDB_Class>(Backup_JSONFilePath);
            //ElevatorDB DB4
            ElevatorDB_Class ElevatorDB_Data_Backup = ReadDataFromFile<ElevatorDB_Class>(Backup_JSONFilePath);
            //CarWashDB DB5
            CarWashDB_Class CarWashDB_Data_Backup = ReadDataFromFile<CarWashDB_Class>(Backup_JSONFilePath);
            //CrossoradDB DB14
            CrossroadDB_Class CrossroadDB_Data_Backup = ReadDataFromFile<CrossroadDB_Class>(Backup_JSONFilePath);

            //MaintainDB DB11
            #region MaintainDB DB11 

            //MAintain variables
            Option1 = MaintainDB_Data_Backup.Option1;
            Option2 = MaintainDB_Data_Backup.Option2;
            Option3 = MaintainDB_Data_Backup.Option3;

            //Set bits
            Option1 = false;
            S7.SetBitAt(send_buffer_DB11, 0, 0, Option1);
            Option2 = false;
            S7.SetBitAt(send_buffer_DB11, 0, 1, Option2);
            Option3 = false;
            S7.SetBitAt(send_buffer_DB11, 0, 2, Option3);

            int writeResultDB11 = writer.Write();

            if (writeResultDB11 == 0)
            {
                statusStripChooseOption.Items.Clear();
                lblStat = new ToolStripStatusLabel("Variables were writtne to MaintainDB DB11.");
                statusStripChooseOption.Items.Add(lblStat);
            }
            else
            {
                //error
                statusStripChooseOption.Items.Clear();
                lblStat = new ToolStripStatusLabel("Variables were not writtne to MaintainDB DB11.");
                statusStripChooseOption.Items.Add(lblStat);

                if (!errorMessageBoxShown)
                {
                    errorMessageBoxShown = true;

                    //MessageBox
                    MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data weren't written to MaintainDB DB11! \n\n" +
                        $"Error message: {writeResultDB11} \n", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }

            #endregion

            //ElevatorDB DB4
            #region ElevatorDB DB4 

            //Elevator variables
            #region Elevator variables

            //Input
            ElevatorBTNCabin1 = ElevatorDB_Data_Backup.ElevatorBTNCabin1;
            ElevatorBTNCabin2 = ElevatorDB_Data_Backup.ElevatorBTNCabin2;
            ElevatorBTNCabin3 = ElevatorDB_Data_Backup.ElevatorBTNCabin3;
            ElevatorBTNCabin4 = ElevatorDB_Data_Backup.ElevatorBTNCabin4;
            ElevatorBTNCabin5 = ElevatorDB_Data_Backup.ElevatorBTNCabin5;
            ElevatorBTNFloor1 = ElevatorDB_Data_Backup.ElevatorBTNFloor1;
            ElevatorBTNFloor2 = ElevatorDB_Data_Backup.ElevatorBTNFloor2;
            ElevatorBTNFloor3 = ElevatorDB_Data_Backup.ElevatorBTNFloor3;
            ElevatorBTNFloor4 = ElevatorDB_Data_Backup.ElevatorBTNFloor4;
            ElevatorBTNFloor5 = ElevatorDB_Data_Backup.ElevatorBTNFloor5;
            ElevatorDoorSEQ = ElevatorDB_Data_Backup.ElevatorDoorSEQ;
            ElevatorBTNOPENCLOSE = ElevatorDB_Data_Backup.ElevatorBTNOPENCLOSE;
            ElevatorEmergencySTOP = ElevatorDB_Data_Backup.ElevatorEmergencySTOP;
            ElevatorErrorSystem = ElevatorDB_Data_Backup.ElevatorErrorSystem;
            ElevatorActualFloorSENS1 = ElevatorDB_Data_Backup.ElevatorActualFloorSENS1;
            ElevatorActualFloorSENS2 = ElevatorDB_Data_Backup.ElevatorActualFloorSENS2;
            ElevatorActualFloorSENS3 = ElevatorDB_Data_Backup.ElevatorActualFloorSENS3;
            ElevatorActualFloorSENS4 = ElevatorDB_Data_Backup.ElevatorActualFloorSENS4;
            ElevatorActualFloorSENS5 = ElevatorDB_Data_Backup.ElevatorActualFloorSENS5;
            ElevatorDoorClOSE = ElevatorDB_Data_Backup.ElevatorDoorClOSE;
            ElevatorDoorOPEN = ElevatorDB_Data_Backup.ElevatorDoorOPEN;
            ElevatorInactivity = ElevatorDB_Data_Backup.ElevatorInactivity;

            //Output
            ElevatorMotorON = ElevatorDB_Data_Backup.ElevatorMotorON;
            ElevatorMotorDOWN = ElevatorDB_Data_Backup.ElevatorMotorDOWN;
            ElevatorMotorUP = ElevatorDB_Data_Backup.ElevatorMotorUP;
            ElevatroHoming = ElevatorDB_Data_Backup.ElevatroHoming;
            ElevatorSystemReady = ElevatorDB_Data_Backup.ElevatorSystemReady;
            ElevatorActualFloor = ElevatorDB_Data_Backup.ElevatorActualFloor;
            ElevatorMoving = ElevatorDB_Data_Backup.ElevatorMoving;
            ElevatorSystemWorking = ElevatorDB_Data_Backup.ElevatorSystemWorking;
            ElevatorGoToFloor = ElevatorDB_Data_Backup.ElevatorGoToFloor;
            ElevatorDirection = ElevatorDB_Data_Backup.ElevatorDirection;
            ElevatorActualFloorLED1 = ElevatorDB_Data_Backup.ElevatorActualFloorLED1;
            ElevatorActualFloorLED2 = ElevatorDB_Data_Backup.ElevatorActualFloorLED2;
            ElevatorActualFloorLED3 = ElevatorDB_Data_Backup.ElevatorActualFloorLED3;
            ElevatorActualFloorLED4 = ElevatorDB_Data_Backup.ElevatorActualFloorLED4;
            ElevatorActualFloorLED5 = ElevatorDB_Data_Backup.ElevatorActualFloorLED5;
            ElevatorActualFloorCabinLED1 = ElevatorDB_Data_Backup.ElevatorActualFloorCabinLED1;
            ElevatorActualFloorCabinLED2 = ElevatorDB_Data_Backup.ElevatorActualFloorCabinLED2;
            ElevatorActualFloorCabinLED3 = ElevatorDB_Data_Backup.ElevatorActualFloorCabinLED3;
            ElevatorActualFloorCabinLED4 = ElevatorDB_Data_Backup.ElevatorActualFloorCabinLED4;
            ElevatorActualFloorCabinLED5 = ElevatorDB_Data_Backup.ElevatorActualFloorCabinLED5;
            ElevatorTimeDoorSQOPEN = ElevatorDB_Data_Backup.ElevatorTimeDoorSQOPEN;
            ElevatroTimeDoorSQCLOSE = ElevatorDB_Data_Backup.ElevatroTimeDoorSQCLOSE;
            ElevatorCabinSpeed = ElevatorDB_Data_Backup.ElevatorCabinSpeed;
            ElevatorTimeToGetDown = ElevatorDB_Data_Backup.ElevatorTimeToGetDown;

            //MEMs
            ElevatorMEMDoor = ElevatorDB_Data_Backup.ElevatorMEMDoor;
            ElevatorMEMDoorTrig = ElevatorDB_Data_Backup.ElevatorMEMDoorTrig;
            ElevatorMEMDoorCloseTrig = ElevatorDB_Data_Backup.ElevatorMEMDoorCloseTrig;
            ElevatorMEMEndMovingTrig = ElevatorDB_Data_Backup.ElevatorMEMEndMovingTrig;
            ElevatorMEMBTNFloor1 = ElevatorDB_Data_Backup.ElevatorMEMBTNFloor1;
            ElevatorMEMBTNFloor2 = ElevatorDB_Data_Backup.ElevatorMEMBTNFloor2;
            ElevatorMEMBTNFloor3 = ElevatorDB_Data_Backup.ElevatorMEMBTNFloor3;
            ElevatorMEMBTNFloor4 = ElevatorDB_Data_Backup.ElevatorMEMBTNFloor4;
            ElevatorMEMBTNFloor5 = ElevatorDB_Data_Backup.ElevatorMEMBTNFloor5;

            #endregion

            //Setting bits 
            #region Setting bits

            //Input variables
            #region Input variables

            S7.SetBitAt(read_buffer_DB4, 0, 0, ElevatorBTNCabin1);
            S7.SetBitAt(read_buffer_DB4, 0, 1, ElevatorBTNCabin2);
            S7.SetBitAt(read_buffer_DB4, 0, 2, ElevatorBTNCabin3);
            S7.SetBitAt(read_buffer_DB4, 0, 3, ElevatorBTNCabin4);
            S7.SetBitAt(read_buffer_DB4, 0, 4, ElevatorBTNCabin5);
            S7.SetBitAt(read_buffer_DB4, 0, 5, ElevatorBTNFloor1);
            S7.SetBitAt(read_buffer_DB4, 0, 6, ElevatorBTNFloor2);
            S7.SetBitAt(read_buffer_DB4, 0, 7, ElevatorBTNFloor3);
            S7.SetBitAt(read_buffer_DB4, 1, 0, ElevatorBTNFloor4);
            S7.SetBitAt(read_buffer_DB4, 1, 1, ElevatorBTNFloor5);
            S7.SetBitAt(read_buffer_DB4, 1, 2, ElevatorDoorSEQ);
            S7.SetBitAt(read_buffer_DB4, 1, 3, ElevatorBTNOPENCLOSE);
            S7.SetBitAt(read_buffer_DB4, 1, 4, ElevatorEmergencySTOP);
            S7.SetBitAt(read_buffer_DB4, 1, 5, ElevatorErrorSystem);
            S7.SetBitAt(read_buffer_DB4, 1, 6, ElevatorActualFloorSENS1);
            S7.SetBitAt(read_buffer_DB4, 1, 7, ElevatorActualFloorSENS2);
            S7.SetBitAt(read_buffer_DB4, 2, 0, ElevatorActualFloorSENS3);
            S7.SetBitAt(read_buffer_DB4, 2, 1, ElevatorActualFloorSENS4);
            S7.SetBitAt(read_buffer_DB4, 2, 2, ElevatorActualFloorSENS5);
            S7.SetBitAt(read_buffer_DB4, 2, 3, ElevatorDoorClOSE);
            S7.SetBitAt(read_buffer_DB4, 2, 4, ElevatorDoorOPEN);
            S7.SetBitAt(read_buffer_DB4, 2, 5, ElevatorInactivity);

            #endregion

            //Output variables
            #region Output variables

            S7.SetBitAt(read_buffer_DB4, 4, 0, ElevatorMotorON);
            S7.SetBitAt(read_buffer_DB4, 4, 1, ElevatorMotorDOWN);
            S7.SetBitAt(read_buffer_DB4, 4, 2, ElevatorMotorUP);
            S7.SetBitAt(read_buffer_DB4, 4, 3, ElevatroHoming);
            S7.SetBitAt(read_buffer_DB4, 4, 4, ElevatorSystemReady);
            S7.SetIntAt(read_buffer_DB4, 6, (short)ElevatorActualFloor);
            S7.SetBitAt(read_buffer_DB4, 8, 0, ElevatorMoving);
            S7.SetBitAt(read_buffer_DB4, 8, 1, ElevatorSystemWorking);
            S7.SetIntAt(read_buffer_DB4, 10, (short)ElevatorGoToFloor);
            S7.SetBitAt(read_buffer_DB4, 12, 0, ElevatorDirection);
            S7.SetBitAt(read_buffer_DB4, 12, 1, ElevatorActualFloorLED1);
            S7.SetBitAt(read_buffer_DB4, 12, 2, ElevatorActualFloorLED2);
            S7.SetBitAt(read_buffer_DB4, 12, 3, ElevatorActualFloorLED3);
            S7.SetBitAt(read_buffer_DB4, 12, 4, ElevatorActualFloorLED4);
            S7.SetBitAt(read_buffer_DB4, 12, 5, ElevatorActualFloorLED5);
            S7.SetBitAt(read_buffer_DB4, 12, 6, ElevatorActualFloorCabinLED1);
            S7.SetBitAt(read_buffer_DB4, 12, 7, ElevatorActualFloorCabinLED2);
            S7.SetBitAt(read_buffer_DB4, 13, 0, ElevatorActualFloorCabinLED3);
            S7.SetBitAt(read_buffer_DB4, 13, 1, ElevatorActualFloorCabinLED4);
            S7.SetBitAt(read_buffer_DB4, 13, 2, ElevatorActualFloorCabinLED5);
            S7.SetDIntAt(read_buffer_DB4, 14, ElevatorTimeDoorSQOPEN); //Time
            S7.SetDIntAt(read_buffer_DB4, 18, ElevatroTimeDoorSQCLOSE); //Time
            S7.SetIntAt(read_buffer_DB4, 22, (short)ElevatorCabinSpeed);
            S7.SetDIntAt(read_buffer_DB4, 24, ElevatorTimeToGetDown); //Time

            #endregion

            //MEM variables 
            #region MEM varialbes 

            S7.SetBitAt(read_buffer_DB4, 28, 0, ElevatorMEMDoor);
            S7.SetBitAt(read_buffer_DB4, 28, 1, ElevatorMEMDoorTrig);
            S7.SetBitAt(read_buffer_DB4, 28, 2, ElevatorMEMDoorCloseTrig);
            S7.SetBitAt(read_buffer_DB4, 28, 3, ElevatorMEMMovingtrig);
            S7.SetBitAt(read_buffer_DB4, 28, 4, ElevatorMEMEndMovingTrig);
            S7.SetBitAt(read_buffer_DB4, 28, 5, ElevatorMEMBTNFloor1);
            S7.SetBitAt(read_buffer_DB4, 28, 6, ElevatorMEMBTNFloor2);
            S7.SetBitAt(read_buffer_DB4, 28, 7, ElevatorMEMBTNFloor3);
            S7.SetBitAt(read_buffer_DB4, 29, 0, ElevatorMEMBTNFloor4);
            S7.SetBitAt(read_buffer_DB4, 29, 1, ElevatorMEMBTNFloor5);

            #endregion

            #endregion

            int writeResultDB4 = writer.Write();

            if (writeResultDB4 == 0)
            {
                statusStripChooseOption.Items.Clear();
                lblStat = new ToolStripStatusLabel("Variables were written to ElevatorDB DB4.");
                statusStripChooseOption.Items.Add(lblStat);
            }
            else
            {
                //error
                statusStripChooseOption.Items.Clear();
                lblStat = new ToolStripStatusLabel("Variables were not written to ElevatorDB DB4.");
                statusStripChooseOption.Items.Add(lblStat);

                if (!errorMessageBoxShown)
                {
                    errorMessageBoxShown = true;

                    //MessageBox
                    MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data weren't written to ElevatorDB DB4! \n\n" +
                        $"Error message: {writeResultDB4} \n", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }

            #endregion

            //CarWashDB DB5
            #region CarWashDB DB5 

            //CarWashDB variables
            #region CarWashDB variables

            //Input variables
            #region Input variables

            CarWashEmergencySTOP = CarWashDB_Data_Backup.CarWashEmergencySTOP;
            CarWashErrorSystem = CarWashDB_Data_Backup.CarWashErrorSystem;
            CarWashStartCarWash = CarWashDB_Data_Backup.CarWashStartCarWash;
            CarWashWaitingForIncomingCar = CarWashDB_Data_Backup.CarWashWaitingForIncomingCar;
            CarWashWaitingForOutgoingCar = CarWashDB_Data_Backup.CarWashWaitingForOutgoingCar;
            CarWashPerfetWash = CarWashDB_Data_Backup.CarWashPerfetWash;
            CarWashPerfectPolish = CarWashDB_Data_Backup.CarWashPerfectPolish;
            CarWashPositionShower = CarWashDB_Data_Backup.CarWashPositionShower;
            CarWashPositionCar = CarWashDB_Data_Backup.CarWashPositionCar;

            #endregion

            //Output variables
            #region Output variables 

            CarWashGreenLight = CarWashDB_Data_Backup.CarWashGreenLight;
            CarWashRedLight = CarWashDB_Data_Backup.CarWashRedLight;
            CarWashYellowLight = CarWashDB_Data_Backup.CarWashYellowLight;
            CarWashDoor1UP = CarWashDB_Data_Backup.CarWashDoor1UP;
            CarWashDoor1DOWN = CarWashDB_Data_Backup.CarWashDoor1DOWN;
            CarWashDoor2UP = CarWashDB_Data_Backup.CarWashDoor2UP;
            CarWashDoor2DOWN = CarWashDB_Data_Backup.CarWashDoor2DOWN;
            CarWashWater = CarWashDB_Data_Backup.CarWashWater;
            CarWashWashingChemicalsFRONT = CarWashDB_Data_Backup.CarWashWashingChemicalsFRONT;
            CarWashWashingChemicalsSIDES = CarWashDB_Data_Backup.CarWashWashingChemicalsSIDES;
            CarWashWashingChemicalsBACK = CarWashDB_Data_Backup.CarWashWashingChemicalsBACK;
            CarWashWax = CarWashDB_Data_Backup.CarWashWax;
            CarWashVarnishProtection = CarWashDB_Data_Backup.CarWashVarnishProtection;
            CarWashDry = CarWashDB_Data_Backup.CarWashDry;
            CarWashPreWash = CarWashDB_Data_Backup.CarWashPreWash;
            CarWashBrushes = CarWashDB_Data_Backup.CarWashBrushes;
            CarWashSoap = CarWashDB_Data_Backup.CarWashSoap; ;
            CarWashActiveFoam = CarWashDB_Data_Backup.CarWashActiveFoam;
            CarWashTimeDoorMovement = CarWashDB_Data_Backup.CarWashTimeDoorMovement;
            CarWashMEMDoor = CarWashDB_Data_Backup.CarWashMEMDoor;
            CarWashMEMDoorTrig = CarWashDB_Data_Backup.CarWashMEMDoorTrig;
            CarWashMEMDoorCloseTrig = CarWashDB_Data_Backup.CarWashMEMDoorCloseTrig;

            #endregion

            #endregion

            //Setting bits 
            #region Setting bits

            //Input variables
            #region Input variables

            S7.SetBitAt(read_buffer_DB5, 0, 0, CarWashEmergencySTOP);
            S7.SetBitAt(read_buffer_DB5, 0, 1, CarWashErrorSystem);
            S7.SetBitAt(read_buffer_DB5, 0, 2, CarWashStartCarWash);
            S7.SetBitAt(read_buffer_DB5, 0, 3, CarWashWaitingForIncomingCar);
            S7.SetBitAt(read_buffer_DB5, 0, 4, CarWashWaitingForOutgoingCar);
            S7.SetBitAt(read_buffer_DB5, 0, 5, CarWashPerfetWash);
            S7.SetBitAt(read_buffer_DB5, 0, 6, CarWashPerfectPolish);
            S7.SetBitAt(read_buffer_DB5, 0, 7, CarWashPositionShower);
            S7.SetBitAt(read_buffer_DB5, 1, 0, CarWashPositionCar);

            #endregion

            //Output variables
            #region Output variables 

            S7.SetBitAt(read_buffer_DB5, 2, 0, CarWashGreenLight);
            S7.SetBitAt(read_buffer_DB5, 2, 1, CarWashRedLight);
            S7.SetBitAt(read_buffer_DB5, 2, 2, CarWashYellowLight);
            S7.SetBitAt(read_buffer_DB5, 2, 3, CarWashDoor1UP);
            S7.SetBitAt(read_buffer_DB5, 2, 4, CarWashDoor1DOWN);
            S7.SetBitAt(read_buffer_DB5, 2, 5, CarWashDoor2UP);
            S7.SetBitAt(read_buffer_DB5, 2, 6, CarWashDoor2DOWN);
            S7.SetBitAt(read_buffer_DB5, 2, 7, CarWashWater);
            S7.SetBitAt(read_buffer_DB5, 3, 0, CarWashWashingChemicalsFRONT);
            S7.SetBitAt(read_buffer_DB5, 3, 1, CarWashWashingChemicalsSIDES);
            S7.SetBitAt(read_buffer_DB5, 3, 2, CarWashWashingChemicalsBACK);
            S7.SetBitAt(read_buffer_DB5, 3, 3, CarWashWax);
            S7.SetBitAt(read_buffer_DB5, 3, 4, CarWashVarnishProtection);
            S7.SetBitAt(read_buffer_DB5, 3, 5, CarWashDry);
            S7.SetBitAt(read_buffer_DB5, 3, 6, CarWashPreWash);
            S7.SetBitAt(read_buffer_DB5, 3, 7, CarWashBrushes);
            S7.SetBitAt(read_buffer_DB5, 4, 0, CarWashSoap);
            S7.SetBitAt(read_buffer_DB5, 4, 1, CarWashActiveFoam);
            S7.SetDIntAt(read_buffer_DB5, 6, CarWashTimeDoorMovement); //Time
            S7.SetBitAt(read_buffer_DB5, 10, 0, CarWashMEMDoor);
            S7.SetBitAt(read_buffer_DB5, 10, 1, CarWashMEMDoorTrig);
            S7.SetBitAt(read_buffer_DB5, 10, 2, CarWashMEMDoorCloseTrig);

            #endregion

            #endregion

            int writeResultDB5 = writer.Write();

            if (writeResultDB5 == 0)
            {
                statusStripChooseOption.Items.Clear();
                lblStat = new ToolStripStatusLabel("Variables were written to CarWashDB DB5.");
                statusStripChooseOption.Items.Add(lblStat);
            }
            else
            {
                //error
                statusStripChooseOption.Items.Clear();
                lblStat = new ToolStripStatusLabel("Variables were not written to CarWashDB DB5.");
                statusStripChooseOption.Items.Add(lblStat);

                if (!errorMessageBoxShown)
                {
                    errorMessageBoxShown = true;

                    //MessageBox
                    MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data weren't written to CarWashDB DB5! \n\n" +
                        $"Error message: {writeResultDB5} \n", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }

            #endregion

            //CrossoradDB DB14
            #region CrossoradDB DB14 

            //CrossoradDB variables
            #region CrossoradDB variable

            //Crossroad_DB DB14
            //Input
            CrossroadModeOFF = CrossroadDB_Data_Backup.CrossroadModeOFF;
            CrossroadModeNIGHT = CrossroadDB_Data_Backup.CrossroadModeNIGHT;
            CrossroadModeDAY = CrossroadDB_Data_Backup.CrossroadModeDAY;
            CrossroadEmergencySTOP = CrossroadDB_Data_Backup.CrossroadEmergencySTOP;
            CrossroadErrorSystem = CrossroadDB_Data_Backup.CrossroadErrorSystem;
            //Output
            TrafficLightsSQ = CrossroadDB_Data_Backup.TrafficLightsSQ;

            //Crossroad_1_DB DB1
            //Input
            //Output
            //Crossroad_2_DB DB19
            //Input
            //Output
            //Crossroad_LeftT_DB DB20
            //Input
            //Output
            //Crossroad_RightT_DB DB21
            //Input
            //Output

            #endregion

            //Setting bits 
            #region Setting bits

            //Crossroad_DB DB14
            //Input
            S7.SetBitAt(read_buffer_DB14, 0, 0, CrossroadModeOFF);
            S7.SetBitAt(read_buffer_DB14, 0, 1, CrossroadModeNIGHT);
            S7.SetBitAt(read_buffer_DB14, 0, 2, CrossroadModeDAY);
            S7.SetBitAt(read_buffer_DB14, 0, 3, CrossroadEmergencySTOP);
            S7.SetBitAt(read_buffer_DB14, 0, 4, CrossroadErrorSystem);
            //Output
            S7.SetIntAt(read_buffer_DB14, 2, (short)TrafficLightsSQ);

            //Crossroad_1_DB DB1
            //Input
            //Output
            //Crossroad_2_DB DB19
            //Input
            //Output
            //Crossroad_LeftT_DB DB20
            //Input
            //Output
            //Crossroad_RightT_DB DB21
            //Input
            //Output

            #endregion

            int writeResultDB14 = writer.Write();

            if (writeResultDB14 == 0)
            {
                statusStripChooseOption.Items.Clear();
                lblStat = new ToolStripStatusLabel("Variables were written to CrossoradDB DB14.");
                statusStripChooseOption.Items.Add(lblStat);
            }
            else
            {
                //error
                statusStripChooseOption.Items.Clear();
                lblStat = new ToolStripStatusLabel("Variables were not written to CrossoradDB DB14.");
                statusStripChooseOption.Items.Add(lblStat);

                if (!errorMessageBoxShown)
                {
                    errorMessageBoxShown = true;

                    //MessageBox
                    MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data weren't written to CrossoradDB DB14! \n\n" +
                        $"Error message: {writeResultDB14} \n", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }

            #endregion

            #endregion

            //PLC Disconnect 
            #region PLC Disconnect 

            int plcDisconnect = client.Disconnect();

            if (plcDisconnect == 0)
            {
                btnDisconnect.Text = "Disconnected";
                statusStripChooseOption.Items.Clear();
                lblStat = new ToolStripStatusLabel("Disconnected");
                statusStripChooseOption.Items.Add(lblStat);

                btnConnect.Visible = true;
                btnDisconnect.Visible = false;
            }
            else
            {
                btnDisconnect.Text = "Disconnect";
                statusStripChooseOption.Items.Clear();
                lblStat = new ToolStripStatusLabel("Disconnection from PLC failed");
                statusStripChooseOption.Items.Add(lblStat);

                btnConnect.Visible = false;
                btnDisconnect.Visible = true;

                //write error
                if (!errorMessageBoxShown)
                {
                    errorMessageBoxShown = true;

                    //MessageBox
                    MessageBox.Show("BE doesn't work properly.Disconnection from PLC failed. \n\n", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }

            #endregion
        }

        #endregion

        //PLC Control Functions 
        #region PLC Control Functions 

        //PLC Stop
        private void StopPLC()
        {
            client.PlcStop();
        }

        //PLC Cold Start
        private void ColdStartPLC()
        {
            client.PlcColdStart();
        }

        //PLC Hot Start 
        private void HotStartPLC()
        {
            client.PlcHotStart();
        }

        //PLC Get Status 
        private void GetPLCStatus()
        {
            int status = 0;

            int PLCStatus = client.PlcGetStatus(ref status);

            if (PLCStatus == 0)
            {
                string statusMessage = status switch
                {
                    S7Consts.S7CpuStatusRun => "PLC is in RUN mode.",
                    S7Consts.S7CpuStatusStop => "PLC is in STOP mode.",
                    S7Consts.S7CpuStatusUnknown => "PLC status is unknown.",
                    _ => "Unknown status code."
                };

                //
            }
            else
            {
                //

            }
        }

        #endregion

        //Periodical reading from DBs
        #region Periodical reading from DBs

        private void Timer_read_from_PLC_Tick(object sender, EventArgs e)
        {
            try
            {
                //Reading variables with MultiVar method
                #region Multi read -> MultiVar

                S7MultiVar reader = new S7MultiVar(client);

                //DB11 => Maintain_DB
                #region Reading from DB11 Maintain_DB
                //DB11 => Maintain_DB 
                if (previous_buffer_DB11 == null)
                {
                    previous_buffer_DB11 = new byte[read_buffer_DB11.Length];
                    Array.Copy(read_buffer_DB11, previous_buffer_DB11, read_buffer_DB11.Length);

                    PreviousBufferHash_DB11 = ComputeHash(read_buffer_DB11);
                }

                reader.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, DBNumber_DB11, 0, read_buffer_DB11.Length, ref read_buffer_DB11);

                int readResultDB11 = reader.Read();

                if (readResultDB11 == 0)
                {
                    byte[] currentHashDB11 = ComputeHash(read_buffer_DB11);

                    if (!ArraysAreEqual(currentHashDB11, PreviousBufferHash_DB11))
                    {
                        Array.Copy(read_buffer_DB11, previous_buffer_DB11, read_buffer_DB11.Length);
                        PreviousBufferHash_DB11 = currentHashDB11;

                        Option1 = S7.GetBitAt(read_buffer_DB11, 0, 0);
                        Option2 = S7.GetBitAt(read_buffer_DB11, 0, 1);
                        Option3 = S7.GetBitAt(read_buffer_DB11, 0, 2);

                        errorMessageBoxShown = false;
                    }

                    errorMessageBoxShown = false;
                }
                else
                {
                    //error
                    statusStripChooseOption.Items.Clear();
                    ToolStripStatusLabel lblStatus1 = new ToolStripStatusLabel("Variables were not read.");
                    statusStripChooseOption.Items.Add(lblStatus1);

                    if (!errorMessageBoxShown)
                    {
                        errorMessageBoxShown = true;

                        //MessageBox
                        MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data from PLC weren't read from DB11! \n\n" +
                            $"Error message: {readResultDB11} \n", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }

                #endregion

                //DB4 => Elevator_DB
                #region Reading from DB4 Elevator_DB
                //DB4 => Elevator_DB 
                if (previous_buffer_DB4 == null)
                {
                    previous_buffer_DB4 = new byte[read_buffer_DB4.Length];
                    Array.Copy(read_buffer_DB4, previous_buffer_DB4, read_buffer_DB4.Length);

                    PreviousBufferHash_DB4 = ComputeHash(read_buffer_DB4);
                }

                reader.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, DBNumber_DB4, 0, read_buffer_DB4.Length, ref read_buffer_DB4);

                int readResultDB4 = reader.Read();

                if (readResultDB4 == 0)
                {
                    byte[] currentHashDB4_Input = ComputeHash(read_buffer_DB4);

                    if (!ArraysAreEqual(currentHashDB4_Input, PreviousBufferHash_DB4))
                    {
                        Array.Copy(read_buffer_DB4, previous_buffer_DB4, read_buffer_DB4.Length);
                        PreviousBufferHash_DB4 = currentHashDB4_Input;

                        //Input variables
                        #region Input variables

                        ElevatorBTNCabin1 = S7.GetBitAt(read_buffer_DB4, 0, 0);
                        ElevatorBTNCabin2 = S7.GetBitAt(read_buffer_DB4, 0, 1);
                        ElevatorBTNCabin3 = S7.GetBitAt(read_buffer_DB4, 0, 2);
                        ElevatorBTNCabin4 = S7.GetBitAt(read_buffer_DB4, 0, 3);
                        ElevatorBTNCabin5 = S7.GetBitAt(read_buffer_DB4, 0, 4);
                        ElevatorBTNFloor1 = S7.GetBitAt(read_buffer_DB4, 0, 5);
                        ElevatorBTNFloor2 = S7.GetBitAt(read_buffer_DB4, 0, 6);
                        ElevatorBTNFloor3 = S7.GetBitAt(read_buffer_DB4, 0, 7);
                        ElevatorBTNFloor4 = S7.GetBitAt(read_buffer_DB4, 1, 0);
                        ElevatorBTNFloor5 = S7.GetBitAt(read_buffer_DB4, 1, 1);
                        ElevatorDoorSEQ = S7.GetBitAt(read_buffer_DB4, 1, 2);
                        ElevatorBTNOPENCLOSE = S7.GetBitAt(read_buffer_DB4, 1, 3);
                        ElevatorEmergencySTOP = S7.GetBitAt(read_buffer_DB4, 1, 4);
                        ElevatorErrorSystem = S7.GetBitAt(read_buffer_DB4, 1, 5);
                        ElevatorActualFloorSENS1 = S7.GetBitAt(read_buffer_DB4, 1, 6);
                        ElevatorActualFloorSENS2 = S7.GetBitAt(read_buffer_DB4, 1, 7);
                        ElevatorActualFloorSENS3 = S7.GetBitAt(read_buffer_DB4, 2, 0);
                        ElevatorActualFloorSENS4 = S7.GetBitAt(read_buffer_DB4, 2, 1);
                        ElevatorActualFloorSENS5 = S7.GetBitAt(read_buffer_DB4, 2, 2);
                        ElevatorDoorClOSE = S7.GetBitAt(read_buffer_DB4, 2, 3);
                        ElevatorDoorOPEN = S7.GetBitAt(read_buffer_DB4, 2, 4);
                        ElevatorInactivity = S7.GetBitAt(read_buffer_DB4, 2, 5);

                        #endregion

                        //Output variables
                        #region Output variables

                        ElevatorMotorON = S7.GetBitAt(read_buffer_DB4, 4, 0);
                        ElevatorMotorDOWN = S7.GetBitAt(read_buffer_DB4, 4, 1);
                        ElevatorMotorUP = S7.GetBitAt(read_buffer_DB4, 4, 2);
                        ElevatroHoming = S7.GetBitAt(read_buffer_DB4, 4, 3);
                        ElevatorSystemReady = S7.GetBitAt(read_buffer_DB4, 4, 4);
                        ElevatorActualFloor = S7.GetIntAt(read_buffer_DB4, 6);
                        ElevatorMoving = S7.GetBitAt(read_buffer_DB4, 8, 0);
                        ElevatorSystemWorking = S7.GetBitAt(read_buffer_DB4, 8, 1);
                        ElevatorGoToFloor = S7.GetIntAt(read_buffer_DB4, 10);
                        ElevatorDirection = S7.GetBitAt(read_buffer_DB4, 12, 0);
                        ElevatorActualFloorLED1 = S7.GetBitAt(read_buffer_DB4, 12, 1);
                        ElevatorActualFloorLED2 = S7.GetBitAt(read_buffer_DB4, 12, 2);
                        ElevatorActualFloorLED3 = S7.GetBitAt(read_buffer_DB4, 12, 3);
                        ElevatorActualFloorLED4 = S7.GetBitAt(read_buffer_DB4, 12, 4);
                        ElevatorActualFloorLED5 = S7.GetBitAt(read_buffer_DB4, 12, 5);
                        ElevatorActualFloorCabinLED1 = S7.GetBitAt(read_buffer_DB4, 12, 6);
                        ElevatorActualFloorCabinLED2 = S7.GetBitAt(read_buffer_DB4, 12, 7);
                        ElevatorActualFloorCabinLED3 = S7.GetBitAt(read_buffer_DB4, 13, 0);
                        ElevatorActualFloorCabinLED4 = S7.GetBitAt(read_buffer_DB4, 13, 1);
                        ElevatorActualFloorCabinLED5 = S7.GetBitAt(read_buffer_DB4, 13, 2);
                        ElevatorTimeDoorSQOPEN = S7.GetDIntAt(read_buffer_DB4, 14); //Time
                        ElevatroTimeDoorSQCLOSE = S7.GetDIntAt(read_buffer_DB4, 18); //Time
                        ElevatorCabinSpeed = S7.GetIntAt(read_buffer_DB4, 22);
                        ElevatorTimeToGetDown = S7.GetDIntAt(read_buffer_DB4, 24); //Time

                        #endregion

                        //MEM variables 
                        #region MEM varialbes 

                        ElevatorMEMDoor = S7.GetBitAt(read_buffer_DB4, 28, 0);
                        ElevatorMEMDoorTrig = S7.GetBitAt(read_buffer_DB4, 28, 1);
                        ElevatorMEMDoorCloseTrig = S7.GetBitAt(read_buffer_DB4, 28, 2);
                        ElevatorMEMMovingtrig = S7.GetBitAt(read_buffer_DB4, 28, 3);
                        ElevatorMEMEndMovingTrig = S7.GetBitAt(read_buffer_DB4, 28, 4);
                        ElevatorMEMBTNFloor1 = S7.GetBitAt(read_buffer_DB4, 28, 5);
                        ElevatorMEMBTNFloor2 = S7.GetBitAt(read_buffer_DB4, 28, 6);
                        ElevatorMEMBTNFloor3 = S7.GetBitAt(read_buffer_DB4, 28, 7);
                        ElevatorMEMBTNFloor4 = S7.GetBitAt(read_buffer_DB4, 29, 0);
                        ElevatorMEMBTNFloor5 = S7.GetBitAt(read_buffer_DB4, 29, 1);

                        #endregion

                        errorMessageBoxShown = false;
                    }

                    errorMessageBoxShown = false;
                }
                else
                {
                    //error
                    statusStripChooseOption.Items.Clear();
                    ToolStripStatusLabel lblStatus1 = new ToolStripStatusLabel("Variables were not read.");
                    statusStripChooseOption.Items.Add(lblStatus1);

                    if (!errorMessageBoxShown)
                    {
                        errorMessageBoxShown = true;

                        //MessageBox
                        MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data weren't read from DB4!!! \n\n" +
                            $"Error message {readResultDB4} \n", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }

                #endregion

                //DB5 => CarWash_DB
                #region Reading from DB5 CarWash_DB
                //DB5 => CarWash_DB 
                if (previous_buffer_DB5 == null)
                {
                    previous_buffer_DB5 = new byte[read_buffer_DB5.Length];
                    Array.Copy(read_buffer_DB5, previous_buffer_DB5, read_buffer_DB5.Length);

                    PreviousBufferHash_DB5 = ComputeHash(read_buffer_DB5);
                }

                reader.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, DBNumber_DB5, 0, read_buffer_DB5.Length, ref read_buffer_DB5);

                int readResultDB5 = reader.Read();

                if (readResultDB5 == 0)
                {
                    byte[] currentHashDB5_Input = ComputeHash(read_buffer_DB5);

                    if (!ArraysAreEqual(currentHashDB5_Input, PreviousBufferHash_DB5))
                    {
                        Array.Copy(read_buffer_DB5, previous_buffer_DB5, read_buffer_DB5.Length);
                        PreviousBufferHash_DB5 = currentHashDB5_Input;

                        //Input variables
                        #region Input variables

                        CarWashEmergencySTOP = S7.GetBitAt(read_buffer_DB5, 0, 0);
                        CarWashErrorSystem = S7.GetBitAt(read_buffer_DB5, 0, 1);
                        CarWashStartCarWash = S7.GetBitAt(read_buffer_DB5, 0, 2);
                        CarWashWaitingForIncomingCar = S7.GetBitAt(read_buffer_DB5, 0, 3);
                        CarWashWaitingForOutgoingCar = S7.GetBitAt(read_buffer_DB5, 0, 4);
                        CarWashPerfetWash = S7.GetBitAt(read_buffer_DB5, 0, 5);
                        CarWashPerfectPolish = S7.GetBitAt(read_buffer_DB5, 0, 6);
                        CarWashPositionShower = S7.GetBitAt(read_buffer_DB5, 0, 7);
                        CarWashPositionCar = S7.GetBitAt(read_buffer_DB5, 1, 0);

                        #endregion

                        //Output variables
                        #region Output variables 

                        CarWashGreenLight = S7.GetBitAt(read_buffer_DB5, 2, 0);
                        CarWashRedLight = S7.GetBitAt(read_buffer_DB5, 2, 1);
                        CarWashYellowLight = S7.GetBitAt(read_buffer_DB5, 2, 2);
                        CarWashDoor1UP = S7.GetBitAt(read_buffer_DB5, 2, 3);
                        CarWashDoor1DOWN = S7.GetBitAt(read_buffer_DB5, 2, 4);
                        CarWashDoor2UP = S7.GetBitAt(read_buffer_DB5, 2, 5);
                        CarWashDoor2DOWN = S7.GetBitAt(read_buffer_DB5, 2, 6);
                        CarWashWater = S7.GetBitAt(read_buffer_DB5, 2, 7);
                        CarWashWashingChemicalsFRONT = S7.GetBitAt(read_buffer_DB5, 3, 0);
                        CarWashWashingChemicalsSIDES = S7.GetBitAt(read_buffer_DB5, 3, 1);
                        CarWashWashingChemicalsBACK = S7.GetBitAt(read_buffer_DB5, 3, 2);
                        CarWashWax = S7.GetBitAt(read_buffer_DB5, 3, 3);
                        CarWashVarnishProtection = S7.GetBitAt(read_buffer_DB5, 3, 4);
                        CarWashDry = S7.GetBitAt(read_buffer_DB5, 3, 5);
                        CarWashPreWash = S7.GetBitAt(read_buffer_DB5, 3, 6);
                        CarWashBrushes = S7.GetBitAt(read_buffer_DB5, 3, 7);
                        CarWashSoap = S7.GetBitAt(read_buffer_DB5, 4, 0); ;
                        CarWashActiveFoam = S7.GetBitAt(read_buffer_DB5, 4, 1);
                        CarWashTimeDoorMovement = S7.GetDIntAt(read_buffer_DB5, 6); //Time
                        CarWashMEMDoor = S7.GetBitAt(read_buffer_DB5, 10, 0);
                        CarWashMEMDoorTrig = S7.GetBitAt(read_buffer_DB5, 10, 1);
                        CarWashMEMDoorCloseTrig = S7.GetBitAt(read_buffer_DB5, 10, 2);

                        #endregion

                        errorMessageBoxShown = false;
                    }

                    errorMessageBoxShown = false;
                }
                else
                {
                    //error
                    statusStripChooseOption.Items.Clear();
                    ToolStripStatusLabel lblStatus1 = new ToolStripStatusLabel("Variables were not read.");
                    statusStripChooseOption.Items.Add(lblStatus1);

                    if (!errorMessageBoxShown)
                    {
                        errorMessageBoxShown = true;

                        //MessageBox
                        MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data weren't read from DB5!!! \n\n" +
                            $"Error message {readResultDB5} \n", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }

                #endregion

                //DB14 => Crossroad_DB 
                #region Reading from DB14 Crossroad_DB
                //DB14 => Crossroad_DB 
                if (previous_buffer_DB14 == null)
                {
                    previous_buffer_DB14 = new byte[read_buffer_DB14.Length];
                    Array.Copy(read_buffer_DB14, previous_buffer_DB14, read_buffer_DB14.Length);

                    PreviousBufferHash_DB14 = ComputeHash(read_buffer_DB14);
                }

                reader.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, DBNumber_DB14, 0, read_buffer_DB14.Length, ref read_buffer_DB14);

                int readResultDB14 = reader.Read();

                if (readResultDB14 == 0)
                {
                    byte[] currentHashDB14_Input = ComputeHash(read_buffer_DB14);

                    if (!ArraysAreEqual(currentHashDB14_Input, PreviousBufferHash_DB14))
                    {
                        Array.Copy(read_buffer_DB14, previous_buffer_DB14, read_buffer_DB14.Length);
                        PreviousBufferHash_DB14 = currentHashDB14_Input;

                        //Input variables
                        #region Input variables

                        CrossroadModeOFF = S7.GetBitAt(read_buffer_DB14, 0, 0);
                        CrossroadModeNIGHT = S7.GetBitAt(read_buffer_DB14, 0, 1);
                        CrossroadModeDAY = S7.GetBitAt(read_buffer_DB14, 0, 2);
                        CrossroadEmergencySTOP = S7.GetBitAt(read_buffer_DB14, 0, 3);
                        CrossroadErrorSystem = S7.GetBitAt(read_buffer_DB14, 0, 4);

                        #endregion

                        //Output variables
                        #region Output variables 

                        TrafficLightsSQ = S7.GetIntAt(read_buffer_DB14, 2);

                        #endregion

                        errorMessageBoxShown = false;
                    }
                }
                else
                {
                    //error
                    statusStripChooseOption.Items.Clear();
                    ToolStripStatusLabel lblStatus1 = new ToolStripStatusLabel("Variables were not read.");
                    statusStripChooseOption.Items.Add(lblStatus1);

                    if (!errorMessageBoxShown)
                    {
                        errorMessageBoxShown = true;

                        //MessageBox
                        MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data weren't read from DB14!!! \n\n" +
                            $"Error message {readResultDB14} \n", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }

                #endregion

                //DB1 => Crossroad_1_DB - Crossroad 1
                #region Reading from DB1 Crossroad_1_DB
                //DB1 => Crossroad_1_DB -> Crossroad 1 
                if (previous_buffer_DB1 == null)
                {
                    previous_buffer_DB1 = new byte[read_buffer_DB1.Length];
                    Array.Copy(read_buffer_DB1, previous_buffer_DB1, read_buffer_DB1.Length);

                    PreviousBufferHash_DB1 = ComputeHash(read_buffer_DB1);
                }

                reader.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, DBNumber_DB1, 0, read_buffer_DB1.Length, ref read_buffer_DB1);

                int readResultDB1 = reader.Read();

                if (readResultDB1 == 0)
                {
                    byte[] currentHashDB1_Input = ComputeHash(read_buffer_DB1);

                    if (!ArraysAreEqual(currentHashDB1_Input, PreviousBufferHash_DB1))
                    {
                        Array.Copy(read_buffer_DB1, previous_buffer_DB1, read_buffer_DB1.Length);
                        PreviousBufferHash_DB1 = currentHashDB1_Input;

                        //Input variables
                        #region Input variables

                        Crossroad1LeftCrosswalkBTN1 = S7.GetBitAt(read_buffer_DB1, 0, 0);
                        Crossroad1LeftCrosswalkBTN2 = S7.GetBitAt(read_buffer_DB1, 0, 1);
                        Crossroad1TopCrosswalkBTN1 = S7.GetBitAt(read_buffer_DB1, 0, 2);
                        Crossroad1TopCrosswalkBTN2 = S7.GetBitAt(read_buffer_DB1, 0, 3);

                        #endregion

                        //Output variables
                        #region Output variables

                        Crossroad1CrosswalkSQ = S7.GetIntAt(read_buffer_DB1, 2);

                        Crossroad1TopGREEN = S7.GetBitAt(read_buffer_DB1, 4, 0);
                        Crossroad1TopYELLOW = S7.GetBitAt(read_buffer_DB1, 4, 1);
                        Crossroad1TopRED = S7.GetBitAt(read_buffer_DB1, 4, 2);

                        Crossroad1LeftGREEN = S7.GetBitAt(read_buffer_DB1, 4, 3);
                        Crossroad1LeftYELLOW = S7.GetBitAt(read_buffer_DB1, 4, 4);
                        Crossroad1LeftRED = S7.GetBitAt(read_buffer_DB1, 4, 5);

                        Crossroad1BottomGREEN = S7.GetBitAt(read_buffer_DB1, 4, 6);
                        Crossroad1BottomYELLOW = S7.GetBitAt(read_buffer_DB1, 4, 7);
                        Crossroad1BottomRED = S7.GetBitAt(read_buffer_DB1, 5, 0);

                        Crossroad1RightGREEN = S7.GetBitAt(read_buffer_DB1, 5, 1);
                        Crossroad1RightYELLOW = S7.GetBitAt(read_buffer_DB1, 5, 2);
                        Crossroad1RightRED = S7.GetBitAt(read_buffer_DB1, 5, 3);

                        Crossroad1TopCrosswalkRED1 = S7.GetBitAt(read_buffer_DB1, 5, 4);
                        Crossroad1TopCrosswalkRED2 = S7.GetBitAt(read_buffer_DB1, 5, 5);
                        Crossroad1TopCrosswalkGREEN1 = S7.GetBitAt(read_buffer_DB1, 5, 6);
                        Crossroad1TopCrosswalkGREEN2 = S7.GetBitAt(read_buffer_DB1, 5, 7);

                        Crossroad1LeftCrosswalkRED1 = S7.GetBitAt(read_buffer_DB1, 6, 0);
                        Crossroad1LeftCrosswalkRED2 = S7.GetBitAt(read_buffer_DB1, 6, 1);
                        Crossroad1LeftCrosswalkGREEN1 = S7.GetBitAt(read_buffer_DB1, 6, 2);
                        Crossroad1LeftCrosswalkGREEN2 = S7.GetBitAt(read_buffer_DB1, 6, 3);

                        #endregion

                        errorMessageBoxShown = false;
                    }
                }
                else
                {
                    //error
                    statusStripChooseOption.Items.Clear();
                    ToolStripStatusLabel lblStatus1 = new ToolStripStatusLabel("Variables were not read.");
                    statusStripChooseOption.Items.Add(lblStatus1);

                    if (!errorMessageBoxShown)
                    {
                        errorMessageBoxShown = true;

                        //MessageBox
                        MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data weren't read from DB1!!! \n\n" +
                            $"Error message {readResultDB1} \n", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }

                #endregion

                //DB19 => Crossroad_2_DB - Crossroad 2 
                #region Reading from DB19 Crossroad_2_DB
                //DB19 => Crossroad_2_DB -> Crossroad 2
                if (previous_buffer_DB19 == null)
                {
                    previous_buffer_DB19 = new byte[read_buffer_DB19.Length];
                    Array.Copy(read_buffer_DB19, previous_buffer_DB19, read_buffer_DB19.Length);

                    PreviousBufferHash_DB19 = ComputeHash(read_buffer_DB19);
                }

                reader.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, DBNumber_DB19, 0, read_buffer_DB19.Length, ref read_buffer_DB19);

                int readResultDB19 = reader.Read();

                if (readResultDB19 == 0)
                {
                    byte[] currentHashDB19_Input = ComputeHash(read_buffer_DB19);

                    if (!ArraysAreEqual(currentHashDB19_Input, PreviousBufferHash_DB19))
                    {
                        Array.Copy(read_buffer_DB19, previous_buffer_DB19, read_buffer_DB19.Length);
                        PreviousBufferHash_DB19 = currentHashDB19_Input;

                        //Input variable
                        #region Input variables

                        Crossroad2LeftCrosswalkBTN1 = S7.GetBitAt(read_buffer_DB19, 0, 0);
                        Crossroad2LeftCrosswalkBTN2 = S7.GetBitAt(read_buffer_DB19, 0, 1);
                        Crossroad2RightCrosswalkBTN1 = S7.GetBitAt(read_buffer_DB19, 0, 2);
                        Crossroad2RightCrosswalkBTN2 = S7.GetBitAt(read_buffer_DB19, 0, 3);

                        #endregion

                        //Output variables
                        #region Output variables

                        Crossroad2CrosswalkSQ = S7.GetIntAt(read_buffer_DB19, 2);

                        Crossroad2TopGREEN = S7.GetBitAt(read_buffer_DB19, 4, 0);
                        Crossroad2TopYELLOW = S7.GetBitAt(read_buffer_DB19, 4, 1);
                        Crossroad2TopRED = S7.GetBitAt(read_buffer_DB19, 4, 2);

                        Crossroad2LeftGREEN = S7.GetBitAt(read_buffer_DB19, 4, 3);
                        Crossroad2LeftYELLOW = S7.GetBitAt(read_buffer_DB19, 4, 4);
                        Crossroad2LeftRED = S7.GetBitAt(read_buffer_DB19, 4, 5);

                        Crossroad2BottomGREEN = S7.GetBitAt(read_buffer_DB19, 4, 6);
                        Crossroad2BottomYELLOW = S7.GetBitAt(read_buffer_DB19, 4, 7);
                        Crossroad2BottomRED = S7.GetBitAt(read_buffer_DB19, 5, 0);

                        Crossroad2RightGREEN = S7.GetBitAt(read_buffer_DB19, 5, 1);
                        Crossroad2RightYELLOW = S7.GetBitAt(read_buffer_DB19, 5, 2);
                        Crossroad2RightRED = S7.GetBitAt(read_buffer_DB19, 5, 3);

                        Crossroad2RightCrosswalkRED1 = S7.GetBitAt(read_buffer_DB19, 5, 4);
                        Crossroad2RightCrosswalkRED2 = S7.GetBitAt(read_buffer_DB19, 5, 5);
                        Crossroad2RightCrosswalkGREEN1 = S7.GetBitAt(read_buffer_DB19, 5, 6);
                        Crossroad2RightCrosswalkGREEN2 = S7.GetBitAt(read_buffer_DB19, 5, 7);

                        Crossroad2LeftCrosswalkRED1 = S7.GetBitAt(read_buffer_DB19, 6, 0);
                        Crossroad2LeftCrosswalkRED2 = S7.GetBitAt(read_buffer_DB19, 6, 1);
                        Crossroad2LeftCrosswalkGREEN1 = S7.GetBitAt(read_buffer_DB19, 6, 2);
                        Crossroad2LeftCrosswalkGREEN2 = S7.GetBitAt(read_buffer_DB19, 6, 3);

                        #endregion

                        errorMessageBoxShown = false;
                    }
                }
                else
                {
                    //error
                    statusStripChooseOption.Items.Clear();
                    ToolStripStatusLabel lblStatus1 = new ToolStripStatusLabel("Variables were not read.");
                    statusStripChooseOption.Items.Add(lblStatus1);

                    if (!errorMessageBoxShown)
                    {
                        errorMessageBoxShown = true;

                        //MessageBox
                        MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data weren't read from DB19!!! \n\n" +
                            $"Error message {readResultDB19} \n", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }

                #endregion

                //DB20 => Crossroad_LeftT_DB - Left T 
                #region Reading from DB20 Crossroad_LeftT_DB
                //DB20 => Crossroad_LeftT_DB - Left T 
                if (previous_buffer_DB20 == null)
                {
                    previous_buffer_DB20 = new byte[read_buffer_DB20.Length];
                    Array.Copy(read_buffer_DB20, previous_buffer_DB20, read_buffer_DB20.Length);

                    PreviousBufferHash_DB20 = ComputeHash(read_buffer_DB20);
                }

                reader.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, DBNumber_DB20, 0, read_buffer_DB20.Length, ref read_buffer_DB20);

                int readResultDB20 = reader.Read();

                if (readResultDB20 == 0)
                {
                    byte[] currentHashDB20_Input = ComputeHash(read_buffer_DB20);

                    if (!ArraysAreEqual(currentHashDB20_Input, PreviousBufferHash_DB20))
                    {
                        Array.Copy(read_buffer_DB20, previous_buffer_DB20, read_buffer_DB20.Length);
                        PreviousBufferHash_DB20 = currentHashDB20_Input;

                        //Input variable
                        #region Input variables

                        CrossroadLeftTLeftCrosswalkBTN1 = S7.GetBitAt(read_buffer_DB20, 0, 0);
                        CrossroadLeftTLeftCrosswalkBTN2 = S7.GetBitAt(read_buffer_DB20, 0, 1);

                        #endregion

                        //Output variables
                        #region Output variables

                        CrossroadLeftTCrosswalkSQ = S7.GetIntAt(read_buffer_DB20, 2);

                        CrossroadLeftTTopGREEN = S7.GetBitAt(read_buffer_DB20, 4, 0);
                        CrossroadLeftTTopYELLOW = S7.GetBitAt(read_buffer_DB20, 4, 1);
                        CrossroadLeftTTopRED = S7.GetBitAt(read_buffer_DB20, 4, 2);

                        CrossroadLeftTLeftGREEN = S7.GetBitAt(read_buffer_DB20, 4, 3);
                        CrossroadLeftTLeftYELLOW = S7.GetBitAt(read_buffer_DB20, 4, 4);
                        CrossroadLeftTLeftRED = S7.GetBitAt(read_buffer_DB20, 4, 5);

                        CrossroadLeftTRightGREEN = S7.GetBitAt(read_buffer_DB20, 4, 6);
                        CrossroadLeftTRightYELLOW = S7.GetBitAt(read_buffer_DB20, 4, 7);
                        CrossroadLeftTRightRED = S7.GetBitAt(read_buffer_DB20, 5, 0);

                        CrossroadLeftTLeftCrosswalkRED1 = S7.GetBitAt(read_buffer_DB20, 5, 1);
                        CrossroadLeftTLeftCrosswalkRED2 = S7.GetBitAt(read_buffer_DB20, 5, 2);

                        CrossroadLeftTLeftCrosswalkGREEN1 = S7.GetBitAt(read_buffer_DB20, 5, 3);
                        CrossroadLeftTLeftCrosswalkGREEN2 = S7.GetBitAt(read_buffer_DB20, 5, 4);

                        #endregion

                        errorMessageBoxShown = false;
                    }
                }
                else
                {
                    //error
                    statusStripChooseOption.Items.Clear();
                    ToolStripStatusLabel lblStatus1 = new ToolStripStatusLabel("Variables were not read.");
                    statusStripChooseOption.Items.Add(lblStatus1);

                    if (!errorMessageBoxShown)
                    {
                        errorMessageBoxShown = true;

                        //MessageBox
                        MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data weren't read from DB20!!! \n\n" +
                            $"Error message {readResultDB20} \n", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }

                #endregion

                //DB21 => Crossroad_RightT_DB - Right T
                #region Reading from DB21 Crossroad_RightT_DB
                //DB21 => Crossroad_RightT_DB - Right T
                if (previous_buffer_DB21 == null)
                {
                    previous_buffer_DB21 = new byte[read_buffer_DB21.Length];
                    Array.Copy(read_buffer_DB21, previous_buffer_DB21, read_buffer_DB21.Length);

                    PreviousBufferHash_DB21 = ComputeHash(read_buffer_DB21);
                }

                reader.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, DBNumber_DB21, 0, read_buffer_DB21.Length, ref read_buffer_DB21);

                int readResultDB21 = reader.Read();

                if (readResultDB21 == 0)
                {
                    byte[] currentHashDB21_Input = ComputeHash(read_buffer_DB21);

                    if (!ArraysAreEqual(currentHashDB21_Input, PreviousBufferHash_DB21))
                    {
                        Array.Copy(read_buffer_DB21, previous_buffer_DB21, read_buffer_DB21.Length);
                        PreviousBufferHash_DB21 = currentHashDB21_Input;

                        //Input variable
                        #region Input variables

                        CrossroadRightTTopCrosswalkBTN1 = S7.GetBitAt(read_buffer_DB21, 0, 0);
                        CrossroadRightTTopCrosswalkBTN2 = S7.GetBitAt(read_buffer_DB21, 0, 1);

                        #endregion

                        //Output variables
                        #region Output variables

                        CrossroadRightTCrosswalkSQ = S7.GetIntAt(read_buffer_DB21, 2);

                        CrossroadRightTTopGREEN = S7.GetBitAt(read_buffer_DB21, 4, 0);
                        CrossroadRightTTopYELLOW = S7.GetBitAt(read_buffer_DB21, 4, 1);
                        CrossroadRightTTopRED = S7.GetBitAt(read_buffer_DB21, 4, 2);

                        CrossroadRightTLeftGREEN = S7.GetBitAt(read_buffer_DB21, 4, 3);
                        CrossroadRightTLeftYELLOW = S7.GetBitAt(read_buffer_DB21, 4, 4);
                        CrossroadRightTLeftRED = S7.GetBitAt(read_buffer_DB21, 4, 5);

                        CrossroadRightTRightGREEN = S7.GetBitAt(read_buffer_DB21, 4, 6);
                        CrossroadRightTRightYELLOW = S7.GetBitAt(read_buffer_DB21, 4, 7);
                        CrossroadRightTRightRED = S7.GetBitAt(read_buffer_DB21, 5, 0);

                        CrossroadRightTTopCrosswalkRED1 = S7.GetBitAt(read_buffer_DB21, 5, 1);
                        CrossroadRightTTopCrosswalkRED2 = S7.GetBitAt(read_buffer_DB21, 5, 2);

                        CrossroadRightTTopCrosswalkGREEN1 = S7.GetBitAt(read_buffer_DB21, 5, 3);
                        CrossroadRightTTopCrosswalkGREEN2 = S7.GetBitAt(read_buffer_DB21, 5, 4);

                        #endregion

                        errorMessageBoxShown = false;
                    }
                }
                else
                {
                    //error
                    statusStripChooseOption.Items.Clear();
                    ToolStripStatusLabel lblStatus1 = new ToolStripStatusLabel("Variables were not read.");
                    statusStripChooseOption.Items.Add(lblStatus1);

                    if (!errorMessageBoxShown)
                    {
                        errorMessageBoxShown = true;

                        //MessageBox
                        MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data weren't read from DB21!!! \n\n" +
                            $"Error message {readResultDB21} \n", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }

                #endregion

                #endregion

                //Reading variables with DBRead method
                /*
                #region DBRead

                //DB11 => Maintain_DB
                int readResultDB11 = client.DBRead(DBNumber_DB11, 0, read_buffer_DB11.Length, read_buffer_DB11);
                //pokud je readResult roven 0, tak čtení bylo úspěšné
                if (readResultDB11 != 0)
                {
                    //error
                    statusStripChooseOption.Items.Clear();
                    ToolStripStatusLabel lblStatus1 = new ToolStripStatusLabel("Variables were not read.");
                    statusStripChooseOption.Items.Add(lblStatus1);

                    if (!errorMessageBoxShown)
                    {
                        errorMessageBoxShown = true;

                        //MessageBox
                        MessageBox.Show("Tia didn't respond. BE doesn't work properly. Data weren't read from DB4!!! \n\n" +
                            $"Error message {readResultDB11} \n", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }
                else
                {
                    Option1 = S7.GetBitAt(read_buffer_DB11, 0, 0);
                    Option2 = S7.GetBitAt(read_buffer_DB11, 0, 1);
                    Option3 = S7.GetBitAt(read_buffer_DB11, 0, 2);

                    errorMessageBoxShown = false;
                }

                #endregion

                */

                //Writting variables to files 
                #region Writting variables to files 

                //New Class
                MaintainDB_Class MaintainDB_Data = MaintainDB();
                ElevatorDB_Class ElevatorDB_Data = ElevatorDB();
                CarWashDB_Class CarWashDB_Data = CarWashDB();
                CrossroadDB_Class CrossroadDB_Data = CrossroadDB();

                //Writing all values of variables to class variables
                #region Writing all values of variables to class variables

                //MaintainDB
                #region MaintainDB
                MaintainDB_Data.Option1 = Option1;
                MaintainDB_Data.Option2 = Option2;
                MaintainDB_Data.Option3 = Option3;
                #endregion

                //ElevatorDB
                #region ElevatorDB
                //Input
                ElevatorDB_Data.ElevatorBTNCabin1 = ElevatorBTNCabin1;
                ElevatorDB_Data.ElevatorBTNCabin2 = ElevatorBTNCabin2;
                ElevatorDB_Data.ElevatorBTNCabin3 = ElevatorBTNCabin3;
                ElevatorDB_Data.ElevatorBTNCabin4 = ElevatorBTNCabin4;
                ElevatorDB_Data.ElevatorBTNCabin5 = ElevatorBTNCabin5;
                ElevatorDB_Data.ElevatorBTNFloor1 = ElevatorBTNFloor1;
                ElevatorDB_Data.ElevatorBTNFloor2 = ElevatorBTNFloor2;
                ElevatorDB_Data.ElevatorBTNFloor3 = ElevatorBTNFloor3;
                ElevatorDB_Data.ElevatorBTNFloor4 = ElevatorBTNFloor4;
                ElevatorDB_Data.ElevatorBTNFloor5 = ElevatorBTNFloor5;
                ElevatorDB_Data.ElevatorDoorSEQ = ElevatorDoorSEQ;
                ElevatorDB_Data.ElevatorBTNOPENCLOSE = ElevatorBTNOPENCLOSE;
                ElevatorDB_Data.ElevatorEmergencySTOP = ElevatorEmergencySTOP;
                ElevatorDB_Data.ElevatorErrorSystem = ElevatorErrorSystem;
                ElevatorDB_Data.ElevatorActualFloorSENS1 = ElevatorActualFloorSENS1;
                ElevatorDB_Data.ElevatorActualFloorSENS2 = ElevatorActualFloorSENS2;
                ElevatorDB_Data.ElevatorActualFloorSENS3 = ElevatorActualFloorSENS3;
                ElevatorDB_Data.ElevatorActualFloorSENS4 = ElevatorActualFloorSENS4;
                ElevatorDB_Data.ElevatorActualFloorSENS5 = ElevatorActualFloorSENS5;
                ElevatorDB_Data.ElevatorDoorClOSE = ElevatorDoorClOSE;
                ElevatorDB_Data.ElevatorDoorOPEN = ElevatorDoorOPEN;
                ElevatorDB_Data.ElevatorInactivity = ElevatorInactivity;
                //Output
                ElevatorDB_Data.ElevatorMotorON = ElevatorMotorON;
                ElevatorDB_Data.ElevatorMotorDOWN = ElevatorMotorDOWN;
                ElevatorDB_Data.ElevatorMotorUP = ElevatorMotorUP;
                ElevatorDB_Data.ElevatroHoming = ElevatroHoming;
                ElevatorDB_Data.ElevatorSystemReady = ElevatorSystemReady;
                ElevatorDB_Data.ElevatorActualFloor = ElevatorActualFloor;
                ElevatorDB_Data.ElevatorMoving = ElevatorMoving;
                ElevatorDB_Data.ElevatorSystemWorking = ElevatorSystemWorking;
                ElevatorDB_Data.ElevatorGoToFloor = ElevatorGoToFloor;
                ElevatorDB_Data.ElevatorDirection = ElevatorDirection;
                ElevatorDB_Data.ElevatorActualFloorLED1 = ElevatorActualFloorLED1;
                ElevatorDB_Data.ElevatorActualFloorLED2 = ElevatorActualFloorLED2;
                ElevatorDB_Data.ElevatorActualFloorLED3 = ElevatorActualFloorLED3;
                ElevatorDB_Data.ElevatorActualFloorLED4 = ElevatorActualFloorLED4;
                ElevatorDB_Data.ElevatorActualFloorLED5 = ElevatorActualFloorLED5;
                ElevatorDB_Data.ElevatorActualFloorCabinLED1 = ElevatorActualFloorCabinLED1;
                ElevatorDB_Data.ElevatorActualFloorCabinLED2 = ElevatorActualFloorCabinLED2;
                ElevatorDB_Data.ElevatorActualFloorCabinLED3 = ElevatorActualFloorCabinLED3;
                ElevatorDB_Data.ElevatorActualFloorCabinLED4 = ElevatorActualFloorCabinLED4;
                ElevatorDB_Data.ElevatorActualFloorCabinLED5 = ElevatorActualFloorCabinLED5;
                ElevatorDB_Data.ElevatorTimeDoorSQOPEN = ElevatorTimeDoorSQOPEN;
                ElevatorDB_Data.ElevatroTimeDoorSQCLOSE = ElevatroTimeDoorSQCLOSE;
                ElevatorDB_Data.ElevatorCabinSpeed = ElevatorCabinSpeed;
                ElevatorDB_Data.ElevatorTimeToGetDown = ElevatorTimeToGetDown;
                //MEMs
                ElevatorDB_Data.ElevatorMEMDoor = ElevatorMEMDoor;
                ElevatorDB_Data.ElevatorMEMDoorTrig = ElevatorMEMDoorTrig;
                ElevatorDB_Data.ElevatorMEMDoorCloseTrig = ElevatorMEMDoorCloseTrig;
                ElevatorDB_Data.ElevatorMEMEndMovingTrig = ElevatorMEMEndMovingTrig;
                ElevatorDB_Data.ElevatorMEMBTNFloor1 = ElevatorMEMBTNFloor1;
                ElevatorDB_Data.ElevatorMEMBTNFloor2 = ElevatorMEMBTNFloor2;
                ElevatorDB_Data.ElevatorMEMBTNFloor3 = ElevatorMEMBTNFloor3;
                ElevatorDB_Data.ElevatorMEMBTNFloor4 = ElevatorMEMBTNFloor4;
                ElevatorDB_Data.ElevatorMEMBTNFloor5 = ElevatorMEMBTNFloor5;
                #endregion

                //CarWashDB
                #region CarWashDB
                //Input
                CarWashDB_Data.CarWashEmergencySTOP = CarWashEmergencySTOP;
                CarWashDB_Data.CarWashErrorSystem = CarWashErrorSystem;
                CarWashDB_Data.CarWashStartCarWash = CarWashStartCarWash;
                CarWashDB_Data.CarWashWaitingForIncomingCar = CarWashWaitingForIncomingCar;
                CarWashDB_Data.CarWashActiveFoam = CarWashWaitingForOutgoingCar;
                CarWashDB_Data.CarWashPerfetWash = CarWashPerfetWash;
                CarWashDB_Data.CarWashPerfectPolish = CarWashPerfectPolish;
                CarWashDB_Data.CarWashPositionShower = CarWashPositionShower;
                CarWashDB_Data.CarWashPositionCar = CarWashPositionCar;
                //Output
                CarWashDB_Data.CarWashGreenLight = CarWashGreenLight;
                CarWashDB_Data.CarWashRedLight = CarWashRedLight;
                CarWashDB_Data.CarWashYellowLight = CarWashYellowLight;
                CarWashDB_Data.CarWashDoor1UP = CarWashDoor1UP;
                CarWashDB_Data.CarWashDoor1DOWN = CarWashDoor1DOWN;
                CarWashDB_Data.CarWashDoor2UP = CarWashDoor2UP;
                CarWashDB_Data.CarWashDoor2DOWN = CarWashDoor2DOWN;
                CarWashDB_Data.CarWashWater = CarWashWater;
                CarWashDB_Data.CarWashWashingChemicalsFRONT = CarWashWashingChemicalsFRONT;
                CarWashDB_Data.CarWashWashingChemicalsSIDES = CarWashWashingChemicalsSIDES;
                CarWashDB_Data.CarWashWashingChemicalsBACK = CarWashWashingChemicalsBACK;
                CarWashDB_Data.CarWashWax = CarWashWax;
                CarWashDB_Data.CarWashVarnishProtection = CarWashVarnishProtection;
                CarWashDB_Data.CarWashDry = CarWashDry;
                CarWashDB_Data.CarWashPreWash = CarWashPreWash;
                CarWashDB_Data.CarWashBrushes = CarWashBrushes;
                CarWashDB_Data.CarWashSoap = CarWashSoap;
                CarWashDB_Data.CarWashActiveFoam = CarWashActiveFoam;
                CarWashDB_Data.CarWashTimeDoorMovement = CarWashTimeDoorMovement;
                //MEMs
                CarWashDB_Data.CarWashMEMDoor = CarWashMEMDoor;
                CarWashDB_Data.CarWashMEMDoorTrig = CarWashMEMDoorTrig;
                CarWashDB_Data.CarWashMEMDoorCloseTrig = CarWashMEMDoorCloseTrig;
                #endregion

                //CrossroadDB
                #region CrossroadDB
                CrossroadDB_Data.CrossroadModeOFF = CrossroadModeOFF;
                CrossroadDB_Data.CrossroadModeNIGHT = CrossroadModeNIGHT;
                CrossroadDB_Data.CrossroadModeDAY = CrossroadModeDAY;
                CrossroadDB_Data.CrossroadEmergencySTOP = CrossroadEmergencySTOP;
                CrossroadDB_Data.CrossroadErrorSystem = CrossroadErrorSystem;

                //Crossroad_DB DB14
                //Input
                //Output
                //Crossroad_1_DB DB1
                //Input
                //Output
                //Crossroad_2_DB DB19
                //Input
                //Output
                //Crossroad_LeftT_DB DB20
                //Input
                //Output
                //Crossroad_RightT_DB DB21
                //Input
                //Output

                #endregion

                #endregion

                //Backup data
                if (!BackupDataDone)
                {
                    AddDataToFile(MaintainDB_Data, Backup_JSONFilePath, "MaintainDB");
                    AddDataToFile(ElevatorDB_Data, Backup_JSONFilePath, "ElevatorDB");
                    AddDataToFile(CarWashDB_Data, Backup_JSONFilePath, "CarWashDB");
                    AddDataToFile(CrossroadDB_Data, Backup_JSONFilePath, "CrossroadDB");

                    BackupDataDone = true;
                }

                //MaintainDB
                AddDataToFile(MaintainDB_Data, MaintainDB_JSONFilePath, "MaintainDB");
                //ElevatorDB
                AddDataToFile(ElevatorDB_Data, ElevatorDB_JSONFilePath, "ElevatorDB");
                //CarWashDB
                AddDataToFile(CarWashDB_Data, CarWashDB_JSONFilePath, "CarWashDB");
                //CrossoradDB
                AddDataToFile(CrossroadDB_Data, CrossroadDB_JSONFilePath, "CrossroadDB");

                #endregion
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

        private bool ArraysAreEqual(byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length)
            {
                return false;
            }

            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                {
                    return false;
                }
            }

            return true;
        }

        private byte[] ComputeHash(byte[] data)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(data);
            }
        }

        #endregion

        //btn End 
        #region Close window 
        private void btnEnd_Click(object sender, EventArgs e)
        {
            if (client.Connected)
            {
                btnDisconnect_Click(sender, e);

                //MaintainDB 
                Option1 = false;
                S7.SetBitAt(send_buffer_DB11, 0, 0, Option1);
                Option2 = false;
                S7.SetBitAt(send_buffer_DB11, 0, 1, Option2);
                Option3 = false;
                S7.SetBitAt(send_buffer_DB11, 0, 2, Option3);

                //MaintainDB
                int writeResultDB11 = client.DBWrite(DBNumber_DB11, 0, send_buffer_DB11.Length, send_buffer_DB11);
                if (writeResultDB11 == 0)
                {
                    //write was successful
                }
                else
                {
                    //write error
                    if (!errorMessageBoxShown)
                    {
                        errorMessageBoxShown = true;

                        //MessageBox
                        MessageBox.Show("BE doesn't work properly. Data could´t be written to DB11!!! \n\n" +
                            $"Error message: {writeResultDB11} \n", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }
            }

            //close program
            this.Close();
        }

        #endregion

        private void btnTest_Click(object sender, EventArgs e)
        {
            TestForm = new TestForm(this);
            TestForm.Show();
            TestFormOpened = true;

            TestForm.FormClosed += (sender, e) => { TestFormOpened = false; };
        }
    }
}
