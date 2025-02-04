using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace JAN0837_BP.FileHelper.JSON
{
    public static class JsonFileHelper
    {
        public static bool exceptionMessageBoxShown = false;
        public static bool errorMessageBoxShown = false;

        public static string projectRootPath = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\..\"));
        public static string dataDirectoryPath = Path.Combine(projectRootPath, "Data");

        public static void CreateFileIfNotExists(string relativePath)
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
                if (!exceptionMessageBoxShown)
                {
                    exceptionMessageBoxShown = true;

                    var stackTrace = new StackTrace(ex, true);
                    var frame = stackTrace.GetFrame(0); 
                    var file = frame.GetFileName(); 
                    var line = frame.GetFileLineNumber();
                    string title = "Exception MessageBox";

                    //MessageBox
                    MessageBox.Show($"Error: {ex.Message}\nFile: {file}\nLine: {line}", title,
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
        }

        public static void EnsureFileExists(string filePath)
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
                if (!exceptionMessageBoxShown)
                {
                    exceptionMessageBoxShown = true;

                    var stackTrace = new StackTrace(ex, true);
                    var frame = stackTrace.GetFrame(0);
                    var file = frame.GetFileName();
                    var line = frame.GetFileLineNumber();
                    string title = "Exception MessageBox";

                    //MessageBox
                    MessageBox.Show($"Error: {ex.Message}\nFile: {file}\nLine: {line}", title,
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
        }

        public static void WriteDataToFileJSON<T>(string selectedFile, T data)
        {
            string fullFilePath = Path.Combine(dataDirectoryPath, selectedFile);

            try
            {
                string jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText(fullFilePath, jsonData);

                //Verify written data
                #region Verify written data

                bool isVerified = VerifyData(fullFilePath, data);

                if (isVerified)
                {
                    //all good
                }
                else
                {
                    if (!errorMessageBoxShown)
                    {
                        errorMessageBoxShown = true;

                        var stackTrace = new StackTrace(true);
                        var frame = stackTrace.GetFrame(1); 
                        var file = frame.GetFileName(); 
                        var line = frame.GetFileLineNumber();
                        string title = "Error MessageBox";

                        //MessageBox
                        MessageBox.Show($"Error: \nFile: {file}\nLine: {line}\n\n" + 
                            "Written data were not verified. Please check writting data and the data itself.\n", 
                            title, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                if (!errorMessageBoxShown)
                {
                    errorMessageBoxShown = true;

                    var stackTrace = new StackTrace(ex, true);
                    var frame = stackTrace.GetFrame(0);
                    var file = frame.GetFileName();
                    var line = frame.GetFileLineNumber();
                    string title = "Exception MessageBox";

                    //MessageBox
                    MessageBox.Show($"Error: {ex.Message}\nFile: {file}\nLine: {line}", title,
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
        }

        public static T ReadDataFromFile<T>(string selectedFile)
        {
            string fileFullPath = Path.Combine(dataDirectoryPath, selectedFile);

            try
            {
                EnsureFileExists(fileFullPath);

                string jsonData = File.ReadAllText(fileFullPath);
                return JsonConvert.DeserializeObject<T>(jsonData);
            }
            catch (Exception ex)
            {
                if (!errorMessageBoxShown)
                {
                    errorMessageBoxShown = true;

                    var stackTrace = new StackTrace(ex, true);
                    var frame = stackTrace.GetFrame(0);
                    var file = frame.GetFileName();
                    var line = frame.GetFileLineNumber();
                    string title = "Exception MessageBox";

                    //MessageBox
                    MessageBox.Show($"Error: {ex.Message}\nFile: {file}\nLine: {line}", title,
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
                        
            return default(T);
        }

        public static void AddDataToFile(object data, string selectedFile, string sectionName)
        {
            string fullFilePath = Path.Combine(dataDirectoryPath, selectedFile);

            string existingJson = File.ReadAllText(fullFilePath);

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
            /*
            dataObject.Remove("title");
            dataObject.Remove("data_time");
            dataObject.Remove("message");
            */
            jsonData[sectionName] = dataObject;

            string updatedJson = JsonConvert.SerializeObject(jsonData, Formatting.Indented);
            File.WriteAllText(fullFilePath, updatedJson);

            //Verify written data
            #region Verify written data

            bool isVerified = VerifyData(fullFilePath, data);

            if (isVerified)
            {
                //all good
            }
            else
            {
                if (!errorMessageBoxShown)
                {
                    errorMessageBoxShown = true;

                    //MessageBox
                    MessageBox.Show($"Error: \n" + "Written data were not verified. Please check writting data and the data itself.\n", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }

            #endregion

            try
            {

            }
            catch (Exception ex) 
            {
                if (!errorMessageBoxShown)
                {
                    errorMessageBoxShown = true;

                    var stackTrace = new StackTrace(ex, true);
                    var frame = stackTrace.GetFrame(0);
                    var file = frame.GetFileName();
                    var line = frame.GetFileLineNumber();
                    string title = "Exception MessageBox";

                    //MessageBox
                    MessageBox.Show($"Error: {ex.Message}\nFile: {file}\nLine: {line}", title,
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
        }

        public static bool VerifyData<T>(string filePath, T originalData)
        {
            try
            {
                EnsureFileExists(filePath);

                string jsonDataRead = File.ReadAllText(filePath);
                T readData = JsonConvert.DeserializeObject<T>(jsonDataRead);

                return JsonConvert.SerializeObject(originalData) == JsonConvert.SerializeObject(readData);
                
            }
            catch (Exception ex)
            { 
                if (!errorMessageBoxShown)
                {
                    errorMessageBoxShown = true;

                    var stackTrace = new StackTrace(ex, true);
                    var frame = stackTrace.GetFrame(0);
                    var file = frame.GetFileName();
                    var line = frame.GetFileLineNumber();
                    string title = "Exception MessageBox";

                    //MessageBox
                    MessageBox.Show($"Error: {ex.Message}\nFile: {file}\nLine: {line}", title,
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }

                return false;
            }
        }

    }
}
