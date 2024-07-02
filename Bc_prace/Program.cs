using Bc_prace;
using Bc_prace.Logger;
using Bc_prace.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JAN0837_BP
{
    internal static class Program
    {
        public const string SETTINGS_FILE_JSON = "settings.json";

        public static AppSettingsJson<ElevatorSettingsData> AppSettings { get; private set; }

        public static string GlobalPath { get; private set; }

        public static bool DEBUG { get; set; }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
          
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            //Application.Run(new ChooseOption());

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                               | SecurityProtocolType.Tls11
                               | SecurityProtocolType.Tls12
                               /*| SecurityProtocolType.Ssl3*/; //nepodporovany Security Protokol

                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);


                DEBUG = false;
                ArgsInitialize(args);

                LogsInitialize();

                LoadSettings();
                UpdateSettings();

                Application.Run(new ChooseOptionForm());
            }
            #if !DEBUG
            catch (Exception ex)
            {
                ResolveException(ex);
            }
            #endif
            finally
            {

            }
            Environment.Exit(Environment.ExitCode);
        }

        private static void ArgsInitialize(string[] args)
        {
            int length = args.Length;
            for (int i = 0; i < length; i++)
            {
                string item = args[i];
                switch (item.ToLower())
                {
                    case "-debug":
                        DEBUG = true;
                        break;
                }
            }
        }

        public static void UpdateSettings()
        {
            // If application settings was updated do something
        }

        private static void LoadSettings()
        {
            string appDataPath = Application.LocalUserAppDataPath;
            string settingsFile = Path.Combine(appDataPath, SETTINGS_FILE_JSON);
            try
            {
                AppSettings = new AppSettingsJson<ElevatorSettingsData>(settingsFile);
                AppSettings.LoadSettings();
            }
            catch
            {
                try
                {
                    // Pokusim se importovat predchozi nastaveni
                    string parentFolder = Path.GetDirectoryName(appDataPath);
                    var dirs = Directory.GetDirectories(parentFolder);
                    Dictionary<Version, string> versions = new Dictionary<Version, string>();
                    foreach (var dir in dirs)
                    {
                        if (Version.TryParse(Path.GetFileName(dir), out Version version))
                        {
                            versions.Add(version, dir);
                        }
                    }
                    var keys = versions.Keys.ToList();
                    keys.Sort();

                    int one = 0;
                    if (Directory.Exists(appDataPath))
                        one = 1;

                    if (keys.Count > one)
                    {
                        string previousVersion = versions[keys[keys.Count - 1 - one]];
                        settingsFile = Path.Combine(previousVersion, SETTINGS_FILE_JSON);
                        AppSettings = new AppSettingsJson<ElevatorSettingsData>(settingsFile);
                        AppSettings.LoadSettings();

                        settingsFile = Path.Combine(appDataPath, SETTINGS_FILE_JSON);
                        AppSettings.SaveSettings(settingsFile, "Copy previous to local");

                        AppSettings = new AppSettingsJson<ElevatorSettingsData>(settingsFile);
                        AppSettings.LoadSettings();
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch
                {
                    try
                    {
                        settingsFile = Path.Combine(GlobalPath, SETTINGS_FILE_JSON); // Load from global path (factory default)
                        AppSettings = new AppSettingsJson<ElevatorSettingsData>(settingsFile);
                        AppSettings.LoadSettings();
                        settingsFile = Path.Combine(appDataPath, SETTINGS_FILE_JSON);
                        AppSettings.SaveSettings(settingsFile, "Copy factory to local");

                        AppSettings = new AppSettingsJson<ElevatorSettingsData>(settingsFile);
                        AppSettings.LoadSettings();
                    }
                    catch 
                    { 
                    
                    }
                }
            }
        }

        private static void LogsInitialize()
        {
            ErrLog.GlobalAppPath = Application.UserAppDataPath;
            ErrLog.Extension = "err";
            ErrLog.ErrPath = "err";

            AppLog.GlobalAppPath = Application.UserAppDataPath;
            AppLog.Extension = "log";
            AppLog.ErrPath = "log";
        }

        /// <summary>
        /// Zobrazi chybovou hlasku a ulozi log
        /// </summary>
        /// <param name="e"></param>
        public static void ResolveException(Exception e)
        {
            string errFile = ErrLog.Log(e);
            MessageBox.Show(string.Format(Language.CRITICAL_APPLICATION_ERROR, errFile), Language.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Neodchycene vyjimky
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ResolveException((Exception)e.ExceptionObject);
        }
    }
}