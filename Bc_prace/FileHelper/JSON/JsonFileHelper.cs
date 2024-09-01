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

        public static void WriteDataToFileJSON<T>(string filePath, T data)
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

        public static void AddDataToFile(object data, string filePath, string sectionName)
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


    }
}
