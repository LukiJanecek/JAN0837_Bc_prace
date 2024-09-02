using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JAN0837_BP.Controls
{
    public partial class UserControlSablona : UserControl
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

        public UserControlSablona()
        {
            InitializeComponent();
            DoubleBuffered = true;
            Paint += UserControlSablona_Paint;
        }

        private void UserControlSablona_Load(object sender, EventArgs e)
        {

        }

        private void UserControlSablona_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            //background color
            g.Clear(Color.White);

            Draw(g);
        }

        private void Draw(Graphics g)
        {
            //pen color
            Pen BlackPen = new Pen(Color.Black, 2);
            Pen WhitePen = new Pen(Color.White, 2);
            Pen RedPen = new Pen(Color.Red, 2);
            Pen GreenPen = new Pen(Color.Green, 2);
            Pen YellowPen = new Pen(Color.Yellow, 2);

            //label in UserControl
            Font labelFont = new Font("Arial", 9);
            SolidBrush labelBrush = new SolidBrush(Color.Black);
        }

    }
}
