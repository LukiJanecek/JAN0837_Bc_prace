using Bc_prace;
using Bc_prace.Logger;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bc_prace.Settings
{
    public class AppSettingsJson<T> where T : new()
    {
        private object locker = new object();

        public AppSettingsJson(string settingsFile)
        {
            this.settingsFile = settingsFile;
            this.Data = new T();
        }

        public T Data { get; set; }

        private string settingsFile;

        public void LoadSettings()
        {
            LoadSettings(this.settingsFile);
        }

        public void LoadSettings(string fileName)
        {
            string json;
            lock (locker)
            {
                json = File.ReadAllText(fileName);
            }
            Data = JsonConvert.DeserializeObject<T>(json);
        }


        public void SaveSettings(string message = "Unknown call")
        {
            SaveSettings(this.settingsFile, message);
        }

        public void SaveSettings(string fileName, string message = "Unknown call")
        {
            tries = 0;
            SaveSettingsSafe(fileName, message);
        }
        int tries = 0;
        private void SaveSettingsSafe(string fileName, string message)
        {

            string json = JsonConvert.SerializeObject(Data);
            lock (locker)
            {
                File.WriteAllText(fileName, json);
            }
            try
            {
                AppSettingsJson<T> tmp = new AppSettingsJson<T>(this.settingsFile);
                tmp.LoadSettings();

                if (IsDataEqual(this.Data, tmp.Data) == false)
                {
                    throw new Exception("Load settings check fails.");
                }
                AppLog.Log("SaveSettingsSafe: " + message, AppLog.MessageType.Info);

            }
            catch (Exception ex)
            {
                AppLog.Log("SaveSettingsSafe: " + ex.Message, AppLog.MessageType.Error);
                tries++;
                if (tries < 3)
                {
                    Task.Run(() =>
                    {
                        Thread.Sleep(3000); // Další pokus o uložení po 3 s
                        SaveSettingsSafe(fileName, message + " " + tries.ToString());
                    });
                }
                else
                {
                    AppLog.Log("SaveSettingsSafe: Settings probably corrupted.", AppLog.MessageType.Exclamation);
                }
            }

        }

        private bool IsDataEqual(T data1, T data2)
        {
            string json1 = JsonConvert.SerializeObject(data1);
            string json2 = JsonConvert.SerializeObject(data2);
            return json1 == json2;
        }
    }
}
