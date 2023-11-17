using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bc_prace.Logger
{
    public static class ErrLog
    {
        private static string _extension = "err";
        /// <summary>
        /// Přípona souborů ErrorLogu
        /// </summary>
        public static string Extension
        {
            get { return ErrLog._extension; }
            set { ErrLog._extension = value; }
        }

        private static string _errPath = "err\\";
        /// <summary>
        /// Nastaví relativní cestu začíná bez lomítka končí lomítkem
        /// </summary>
        public static string ErrPath
        {
            get { return ErrLog._errPath; }
            set { ErrLog._errPath = value; }
        }

        private static string _appName = "";
        /// <summary>
        /// Jménmo aplikace pro rozlišení názvu Logu
        /// </summary>
        public static string AppName
        {
            get { return ErrLog._appName; }
            set { ErrLog._appName = value; }
        }

        private static string _globalAppPath = "";
        /// <summary>
        /// Nastaví globání cestu k aplikaci Updater
        /// </summary>
        public static string GlobalAppPath
        {
            get { return ErrLog._globalAppPath; }
            set { ErrLog._globalAppPath = value; }
        }


        /// <summary>
        /// Zaloguje chybovou hlášku e do souboru.
        /// </summary>
        /// <param name="e">Chybová proměná Exception</param>
        public static string Log(Exception e)
        {
            try
            {
                string path = Path.Combine(_globalAppPath, _errPath);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string errString = e.ToString();
                String fileName;
                if (_appName == "")
                {
                    fileName = Path.Combine(path, DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString("00") + "_" +
                      DateTime.Now.Day.ToString("00") + "." + _extension);
                }
                else
                {
                    fileName = Path.Combine(path, _appName + "_" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString("00") + "_" +
                   DateTime.Now.Day.ToString("00") + "." + _extension);
                }

                bool append = File.Exists(fileName);

                using (StreamWriter sw = new StreamWriter(fileName,
                           append, new UnicodeEncoding(true, true)))
                {
                    sw.Write("ERROR AT: " + DateTime.Now.ToString() + " \r\n");
                    sw.Write("*********************************************************************************\r\n");
                    sw.Write("Platform: " + (IntPtr.Size == 8 ? "x64" : "x86") + "\r\n");
                    sw.Write("AppVersion: " + Application.ProductVersion.ToString() + "\n\n");
                    sw.Write("Environment:\r\n");
                    sw.Write("------------\r\n");
                    //sw.Write(string.Format("Is64BitOperatingSystem: {0}\r\n", System.Environment.Is64BitOperatingSystem));
                    //sw.Write(string.Format("Is64BitProcess: {0}\r\n", System.Environment.Is64BitProcess));
                    sw.Write(string.Format("MachineName: {0}\r\n", System.Environment.MachineName));
                    sw.Write(string.Format("OSVersion: {0}\r\n", System.Environment.OSVersion));
                    sw.Write(string.Format("ProcessorCount: {0}\r\n", System.Environment.ProcessorCount));
                    sw.Write(string.Format("UserName: {0}\r\n", System.Environment.UserName));
                    sw.Write(string.Format("Version: {0}\r\n", System.Environment.Version));
                    sw.Write("\r\nError string:\r\n");
                    sw.Write("-------------\r\n");
                    sw.Write(errString);
                    sw.Write("\r\n\r\n\r\n");
                }
                return fileName;
            }
            catch
            { }
            return null;
        }

    }
}
