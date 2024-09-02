using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAN0837_BP.FileHelper.JSON
{
    public static class JsonFileHelper
    {
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
                if (!errorMessageBoxShown)
                {
                    errorMessageBoxShown = true;

                    //MessageBox
                    MessageBox.Show($"Error: {ex.Message}", "Error",
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
                errorMessageBoxShown = true;

                //MessageBox
                MessageBox.Show($"Error: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        public static void WriteDataToFileJSON<T>(string selectedFile, T data)
        {
            string filefullPath = Path.Combine(dataDirectoryPath, selectedFile);

            try
            {
                string jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText(filefullPath, jsonData);
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

        public static T ReadDataFromFile<T>(string selectedFile)
        {
            string filefullPath = Path.Combine(dataDirectoryPath, selectedFile);

            try
            {

            }
            catch (Exception ex)
            {

            }

            if (File.Exists(filefullPath))
            {
                string jsonData = File.ReadAllText(filefullPath);
                return JsonConvert.DeserializeObject<T>(jsonData);
            }

            return default(T);
        }

        public static void AddDataToFile(object data, string selectedFile, string sectionName)
        {
            string fullfilePath = Path.Combine(dataDirectoryPath, selectedFile);

            string existingJson = File.ReadAllText(fullfilePath);

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
            File.WriteAllText(fullfilePath, updatedJson);
        }


    }
}
