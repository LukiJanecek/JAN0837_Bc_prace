using Bc_prace.Controls.MyGraphControl.Entities;
using Sharp7;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Bc_prace.Controls
{
    public partial class UserControlCrossroad : UserControl
    {
        private Program3Form program3FormInstance;
        private ChooseOptionForm chooseOptionFormInstance;

        private S7Client client;

        //DB14 => Crossroad_DB -> 11 structs -> x variables -> size 110.0 
        private int DBNumber_DB14 = 14;
        //first struct -> Input -> 5 variables -> size 0.4
        private byte[] read_buffer_DB14_Input;
        public byte[] previous_buffer_DB14_Input;
        public byte[] PreviousBufferHash_DB14_Input;
        private byte[] send_buffer_DB14_Input;
        //second struct -> Output -> 1 variable -> size 2.0
        private byte[] read_buffer_DB14_Output;
        private byte[] send_buffer_DB14_Output;
        //other structs are Timers 

        //DB1 => Crossroad_1_DB -> Crossroad 1 -> 2 structs -> 25 variables -> size 6.3
        private int DBNumber_DB1 = 1;
        //first struct -> Input -> 4 variables -> size 0.3
        private byte[] read_buffer_DB1_Input;
        public byte[] previous_buffer_DB1_Input;
        public byte[] PreviousBufferHash_DB1_Input;
        private byte[] send_buffer_DB1_Input;
        //second struct -> Output -> 21 variables -> size 6.3 
        private byte[] read_buffer_DB1_Output;
        private byte[] send_buffer_DB1_Output;

        //DB19 => Crossroad_2_DB -> Crossroad 2 -> 2 structs -> 25 variables -> size 6.3  
        private int DBNumber_DB19 = 19;
        //first struct -> Input -> 4 variables -> size 0.3
        private byte[] read_buffer_DB19_Input;
        public byte[] previous_buffer_DB19_Input;
        public byte[] PreviousBufferHash_DB19_Input;
        private byte[] send_buffer_DB19_Input;
        //second struct -> Output -> 21 variables -> size 6.3  
        private byte[] read_buffer_DB19_Output;
        private byte[] send_buffer_DB19_Output;

        //DB20 => Crossroad_LeftT_DB - Left T -> 2 structs -> 16 variables -> size 5.4 
        private int DBNumber_DB20 = 20;
        //first struct -> Input -> 2 variables -> size 0.1
        private byte[] read_buffer_DB20_Input;
        public byte[] previous_buffer_DB20_Input;
        public byte[] PreviousBufferHash_DB20_Input;
        private byte[] send_buffer_DB20_Input;
        //second struct -> Output -> 14 variables -> size 5.4
        private byte[] read_buffer_DB20_Output;
        private byte[] send_buffer_DB20_Output;

        //DB21 => Crossroad_RightT_DB - Right T -> 2 structs -> 16 variables -> size 5.4 
        private int DBNumber_DB21 = 21;
        //first struct -> Input -> 2 variables -> size 0.1
        private byte[] read_buffer_DB21_Input;
        public byte[] previous_buffer_DB21_Input;
        public byte[] PreviousBufferHash_DB21_Input;
        private byte[] send_buffer_DB21_Input;
        //second struct -> Output -> 14 variables -> size 5.4
        private byte[] read_buffer_DB21_Output;
        private byte[] send_buffer_DB21_Output;

        //Input variables
        #region Input variables 

        //Crossroad_DB DB14
        #region Crossroad_DB DB14

        bool CrossroadModeOFF;
        bool CrossroadModeNIGHT;
        bool CrossroadModeDAY;
        bool CrossroadEmergencySTOP;
        bool CrossroadErrorSystem;

        #endregion

        //Crossroad_1_DB DB1
        #region Crossroad_1_DB DB1

        bool Crossroad1LeftCrosswalkBTN1;
        bool Crossroad1LeftCrosswalkBTN2;
        bool Crossroad1TopCrosswalkBTN1;
        bool Crossroad1TopCrosswalkBTN2;

        #endregion

        //Crossroad_2_DB DB19
        #region Crossroad_2_DB DB19

        bool Crossroad2LeftCrosswalkBTN1;
        bool Crossroad2LeftCrosswalkBTN2;
        bool Crossroad2RightCrosswalkBTN1;
        bool Crossroad2RightCrosswalkBTN2;

        #endregion

        //Crossroad_LeftT_DB DB20
        #region Crossroad_LeftT_DB DB20

        bool CrossroadLeftTLeftCrosswalkBTN1;
        bool CrossroadLeftTLeftCrosswalkBTN2;

        #endregion

        //Crossroad_RightT_DB DB21
        #region Crossroad_RightT_DB DB21

        bool CrossroadRightTTopCrosswalkBTN1;
        bool CrossroadRightTTopCrosswalkBTN2;

        #endregion

        #endregion

        //Output variables
        #region Output variables 

        //Crossroad_DB DB14
        #region Crossroad_DB DB14

        int TrafficLightsSQ;

        #endregion

        //Crossroad_1_DB DB1
        #region Crossroad_1_DB DB1

        int Crossroad1CrosswalkSQ;

        bool Crossroad1TopRED;
        bool Crossroad1TopGREEN;
        bool Crossroad1TopYELLOW;
        bool Crossroad1LeftRED;
        bool Crossroad1LeftGREEN;
        bool Crossroad1LeftYELLOW;
        bool Crossroad1RightRED;
        bool Crossroad1RightGREEN;
        bool Crossroad1RightYELLOW;
        bool Crossroad1BottomRED;
        bool Crossroad1BottomGREEN;
        bool Crossroad1BottomYELLOW;

        bool Crossroad1TopCrosswalkRED1;
        bool Crossroad1TopCrosswalkRED2;
        bool Crossroad1TopCrosswalkGREEN1;
        bool Crossroad1TopCrosswalkGREEN2;
        bool Crossroad1LeftCrosswalkRED1;
        bool Crossroad1LeftCrosswalkRED2;
        bool Crossroad1LeftCrosswalkGREEN1;
        bool Crossroad1LeftCrosswalkGREEN2;

        #endregion

        //Crossroad_2_DB DB19
        #region Crossroad_2_DB DB19

        int Crossroad2CrosswalkSQ;

        bool Crossroad2TopRED;
        bool Crossroad2TopGREEN;
        bool Crossroad2TopYellow;
        bool Crossroad2LeftRED;
        bool Crossroad2LeftGREEN;
        bool Crossroad2LeftYellow;
        bool Crossroad2RightRED;
        bool Crossroad2RightGREEN;
        bool Crossroad2RightYellow;
        bool Crossroad2BottomRED;
        bool Crossroad2BottomGREEN;
        bool Crossroad2BottomYellow;

        bool Crossroad2LeftCrosswalkRED1;
        bool Crossroad2LeftCrosswalkRED2;
        bool Crossroad2LeftCrosswalkGREEN1;
        bool Crossroad2LeftCrosswalkGREEN2;
        bool Crossroad2RightCrosswalkRED1;
        bool Crossroad2RightCrosswalkRED2;
        bool Crossroad2RightCrosswalkGREEN1;
        bool Crossroad2RightCrosswalkGREEN2;

        #endregion

        //Crossroad_LeftT_DB DB20
        #region Crossroad_LeftT_DB DB20

        int CrossroadLeftTCrosswalkSQ;

        bool CrossroadLeftTTopRED;
        bool CrossroadLeftTTopGREEN;
        bool CrossroadLeftTTopYellow;
        bool CrossroadLeftTLeftRED;
        bool CrossroadLeftTLeftGREEN;
        bool CrossroadLeftTLeftYellow;
        bool CrossroadLeftTRightRED;
        bool CrossroadLeftTRightGREEN;
        bool CrossroadLeftTRightYellow;

        bool CrossroadLeftTLeftCrosswalkRED1;
        bool CrossroadLeftTLeftCrosswalkRED2;
        bool CrossroadLeftTLeftCrosswalkGREEN1;
        bool CrossroadLeftTLeftCrosswalkGREEN2;

        #endregion

        //Crossroad_RightT_DB DB21
        #region Crossroad_RightT_DB DB21

        int CrossroadRightTCrosswalkSQ;

        bool CrossroadRightTTopRED;
        bool CrossroadRightTTopGREEN;
        bool CrossroadRightTTopYellow;
        bool CrossroadRightTLeftRED;
        bool CrossroadRightTLeftGREEN;
        bool CrossroadRightTLeftYellow;
        bool CrossroadRightTRightRED;
        bool CrossroadRightTRightGREEN;
        bool CrossroadRightTRightYellow;

        bool CrossroadRightTTopCrosswalkRED1;
        bool CrossroadRightTTopCrosswalkRED2;
        bool CrossroadRightTTopCrosswalkGREEN1;
        bool CrossroadRightTTopCrosswalkGREEN2;

        #endregion

        #endregion

        //Variables
        #region Variables
        //Crossroad1
        private Button btnCrossroad1TopCrosswalkLEFT;
        private Button btnCrossroad1TopCrosswalkRIGHT;
        private Button btnCrossroad1LeftCrosswalkTOP;
        private Button btnCrossroad1LeftCrosswalkBOTTOM;
        //Crossroad2
        private Button btnCrossroad2LeftCrosswalkTOP;
        private Button btnCrossroad2LeftCrosswalkBOTTOM;
        private Button btnCrossroad2RightCrosswalkTOP;
        private Button btnCrossroad2RightCrosswalkBOTTOM;
        //Left T
        private Button btnLeftTLeftCrosswalkTOP;
        private Button btnLeftTLeftCrosswalkBOTTOM;
        //Right T
        private Button btnRightTTopCrosswalkLEFT;
        private Button btnRightTTopCrosswalkRIGHT;

        public event EventHandler<string>? ButtonClicked;

        //beggining points of drawing
        private float x = 15;
        private float y = 15;

        //basic parametres
        private float length = 80;
        private float length_interrupted = 20;
        private float TrafficLights_height = 60;
        private float TrafficLights_width = 20;
        private float crosswalk_width = 20;
        private float crosswalk_height = 60;
        private float TrafficLightsCrosswalk_width = 20;
        private float TrafficLightsCrosswalk_height = 40;
        private float FreeSpace = 5;

        private float Button_height = 60;
        private float Button_width = 80;

        private SolidBrush green = new SolidBrush(Color.Green);
        private SolidBrush yellow = new SolidBrush(Color.Yellow);
        private SolidBrush red = new SolidBrush(Color.Red);
        private SolidBrush white = new SolidBrush(Color.White);

        #endregion

        public UserControlCrossroad(Program3Form program3FormInstance) //ChooseOptionForm chooseOptionFormInstance
        {
            InitializeComponent();
            InitializeButtons();

            DoubleBuffered = true;
            Paint += UserControl1_Paint;

            this.program3FormInstance = program3FormInstance;

            client = program3FormInstance.client;

            //buffers 
            //DB14 => Crossroad_DB
            read_buffer_DB14_Input = program3FormInstance.read_buffer_DB14_Input;
            send_buffer_DB14_Input = program3FormInstance.send_buffer_DB14_Input;
            read_buffer_DB14_Output = program3FormInstance.read_buffer_DB14_Output;
            send_buffer_DB14_Output = program3FormInstance.send_buffer_DB14_Output;
            //DB1 => Crossroad_1_DB
            read_buffer_DB1_Input = program3FormInstance.read_buffer_DB1_Input;
            send_buffer_DB1_Input = program3FormInstance.send_buffer_DB1_Input;
            read_buffer_DB1_Output = program3FormInstance.read_buffer_DB1_Output;
            send_buffer_DB1_Output = program3FormInstance.send_buffer_DB1_Output;
            //DB19 => Crossroad_2_DB
            read_buffer_DB19_Input = program3FormInstance.read_buffer_DB19_Input;
            send_buffer_DB19_Input = program3FormInstance.send_buffer_DB19_Input;
            read_buffer_DB19_Output = program3FormInstance.read_buffer_DB19_Output;
            send_buffer_DB19_Output = program3FormInstance.send_buffer_DB19_Output;
            //DB20 => Crossroad_LeftT_DB
            read_buffer_DB20_Input = program3FormInstance.read_buffer_DB20_Input;
            send_buffer_DB20_Input = program3FormInstance.send_buffer_DB20_Input;
            read_buffer_DB20_Output = program3FormInstance.read_buffer_DB20_Output;
            send_buffer_DB20_Output = program3FormInstance.send_buffer_DB20_Output;
            //DB21 => Crossroad_RightT_DB
            read_buffer_DB21_Input = program3FormInstance.read_buffer_DB21_Input;
            send_buffer_DB21_Input = program3FormInstance.send_buffer_DB21_Input;
            read_buffer_DB21_Output = program3FormInstance.read_buffer_DB21_Output;
            send_buffer_DB21_Output = program3FormInstance.send_buffer_DB21_Output;
                        
        }

        private void UserControl1_Paint(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            //background
            g.Clear(Color.Black);

            //pen color
            Pen BlackPen = new Pen(Color.Black, 2);
            Pen WhitePen = new Pen(Color.White, 2);
            Pen RedPen = new Pen(Color.Red, 2);
            Pen GreenPen = new Pen(Color.Green, 2);
            Pen YellowPen = new Pen(Color.Yellow, 2);

            #region Původní nástavba 
            /*
            //BasicCrossroad();

            //crossroad - left up corner
            //vertical line
            g.DrawLine(WhitePen, x + length * 3, y, x + length * 3, y + length);
            g.DrawLine(WhitePen, x + length * 3, y + length, x + length * 3, y + length * 2);
            g.DrawLine(WhitePen, x + length * 3, y + length * 2, x + length * 3, y + length * 3);
            //horizontal line
            g.DrawLine(WhitePen, x, y + length * 3, x + length, y + length * 3);
            g.DrawLine(WhitePen, x + length, y + length * 3, x + length * 2, y + length * 3);
            g.DrawLine(WhitePen, x + length * 2, y + length * 3, x + length * 3, y + length * 3);

            //white line - crossorad1 - left - internal
            //g.DrawLine(WhitePen, x + length * 3, y + length * 3, x + length * 3, y + length * 4); //up
            //g.DrawLine(WhitePen, x + length * 3, y + length * 4, x + length * 3, y + length * 5); //down
            //white line - crossroad1 - left - external
            //g.DrawLine(WhitePen, x + length * 2, y + length * 3, x + length * 2, y + length * 4); //up
            g.DrawLine(WhitePen, x + length * 2, y + length * 4, x + length * 2, y + length * 5); //down
                        
            //crossroad - left down corner
            //horizontal line
            g.DrawLine(WhitePen, x, y + length * 5, x + length, y + length * 5);
            g.DrawLine(WhitePen, x + length, y + length * 5, x + length * 2, y + length * 5);
            g.DrawLine(WhitePen, x + length * 2, y + length * 5, x + length * 3, y + length * 5);
            //vertical line
            g.DrawLine(WhitePen, x + length * 3, y + length * 5, x + length * 3, y + length * 6);
            g.DrawLine(WhitePen, x + length * 3, y + length * 6, x + length * 3, y + length * 7);
            g.DrawLine(WhitePen, x + length * 3, y + length * 7, x + length * 3, y + length * 8);
            g.DrawLine(WhitePen, x + length * 3, y + length * 8, x + length * 3, y + length * 9);

            //white line - crossroad1 - top
            //internal
            //g.DrawLine(WhitePen, x + length * 3, y + length * 3, x + length * 4, y + length * 3); //left
            //g.DrawLine(WhitePen, x + length * 4, y + length * 3, x + length * 5, y + length * 3); //right
            //exteral
            g.DrawLine(WhitePen, x + length * 3, y + length * 2, x + length * 4, y + length * 2); //left
            //g.DrawLine(WhitePen, x + length * 4, y + length * 2, x + length * 5, y + length * 2); //right
            
            //crossroad - mid up - left
            g.DrawLine(WhitePen, x + length * 5, y, x + length * 5, y + length);
            g.DrawLine(WhitePen, x + length * 5, y + length, x + length * 5, y + length * 2);
            g.DrawLine(WhitePen, x + length * 5, y + length * 2, x + length * 5, y + length * 3);
             
            //crossroad - mid up - mid 
            g.DrawLine(WhitePen, x + length * 5, y + length * 3, x + length * 6, y + length * 3);
            g.DrawLine(WhitePen, x + length * 6, y + length * 3, x + length * 7, y + length * 3);
            g.DrawLine(WhitePen, x + length * 7, y + length * 3, x + length * 8, y + length * 3);
            g.DrawLine(WhitePen, x + length * 8, y + length * 3, x + length * 9, y + length * 3);
            //jednoduše
            //g.DrawLine(WhitePen, x + length * 5, y + length * 3, x + length * 9, y + length * 3);

            //crossroad - mid up - right 
            g.DrawLine(WhitePen, x + length * 9, y, x + length * 9, y + length);
            g.DrawLine(WhitePen, x + length * 9, y + length, x + length * 9, y + length * 2);
            g.DrawLine(WhitePen, x + length * 9, y + length * 2, x + length * 9, y + length * 3);
                       
            //white line - crossroad1 - right
            //internal line
            //g.DrawLine(WhitePen, x + length * 5, y + length * 3, x + length * 5, y + length * 4); //up
            //g.DrawLine(WhitePen, x + length * 5, y + length * 4, x + length * 5, y + length * 5); //down
            //external line
            g.DrawLine(WhitePen, x + length * 6, y + length * 3, x + length * 6, y + length * 4); //up
            //g.DrawLine(WhitePen, x + length * 6, y + length * 4, x + length * 6, y + length * 5); //down

            //crossroad - mid down - left
            g.DrawLine(WhitePen, x + length * 5, y + length * 5, x + length * 5, y + length * 6);          
            g.DrawLine(WhitePen, x + length * 5, y + length * 6, x + length * 5, y + length * 7);
            g.DrawLine(WhitePen, x + length * 5, y + length * 7, x + length * 5, y + length * 8);
            g.DrawLine(WhitePen, x + length * 5, y + length * 8, x + length * 5, y + length * 9);

            //crossroad - mid down - mid 
            g.DrawLine(WhitePen, x + length * 5, y + length * 5, x + length * 6, y + length * 5);
            g.DrawLine(WhitePen, x + length * 6, y + length * 5, x + length * 7, y + length * 5);
            g.DrawLine(WhitePen, x + length * 7, y + length * 5, x + length * 8, y + length * 5);
            g.DrawLine(WhitePen, x + length * 8, y + length * 5, x + length * 9, y + length * 5);

            //crossroad - mid down - right
            g.DrawLine(WhitePen, x + length * 9, y + length * 5, x + length * 9, y + length * 6);
            g.DrawLine(WhitePen, x + length * 9, y + length * 6, x + length * 9, y + length * 7);
            g.DrawLine(WhitePen, x + length * 9, y + length * 7, x + length * 9, y + length * 8);
            g.DrawLine(WhitePen, x + length * 9, y + length * 8, x + length * 9, y + length * 9);

            //white line - crossroad1 - bottom
            //internal
            //g.DrawLine(WhitePen, x + length * 3, y + length * 5, x + length * 4, y + length * 5); //left
            //g.DrawLine(WhitePen, x + length * 4, y + length * 5, x + length * 5, y + length * 5); //right
            //external
            //g.DrawLine(WhitePen, x + length * 3, y + length * 6, x + length * 4, y + length * 6); //left
            g.DrawLine(WhitePen, x + length * 4, y + length * 6, x + length * 5, y + length * 6); //right

            //white line - crossroad2 - left
            //exteranl
            //g.DrawLine(WhitePen, x + length * 8, y + length * 3, x + length * 8, y + length * 4); //up
            g.DrawLine(WhitePen, x + length * 8, y + length * 4, x + length * 8, y + length * 5); //down
            //internal
            //g.DrawLine(WhitePen, x + length * 9, y + length * 3, x + length * 9, y + length * 4); //up
            //g.DrawLine(WhitePen, x + length * 9, y + length * 4, x + length * 9, y + length * 5); //down 

            //crossroad - right up corner
            //vertical line
            g.DrawLine(WhitePen, x + length * 11, y, x + length * 11, y + length);
            g.DrawLine(WhitePen, x + length * 11, y + length, x + length * 11, y + length * 2);
            g.DrawLine(WhitePen, x + length * 11, y + length * 2, x + length * 11, y + length * 3);
            //horizontal line
            g.DrawLine(WhitePen, x + length * 11, y + length * 3, x + length * 12, y + length * 3);
            g.DrawLine(WhitePen, x + length * 12, y + length * 3, x + length * 13, y + length * 3);
            g.DrawLine(WhitePen, x + length * 13, y + length * 3, x + length * 14, y + length * 3);

            //white line - crossroad2 - top
            //external
            g.DrawLine(WhitePen, x + length * 9, y + length * 2, x + length * 10, y + length * 2); //left
            //g.DrawLine(WhitePen, x + length * 10, y + length * 2, x + length * 11, y + length * 2); //right
            //internal
            //g.DrawLine(WhitePen, x + length * 9, y + length * 3, x + length * 10, y + length * 3); //left
            //g.DrawLine(WhitePen, x + length * 10, y + length * 3, x + length * 11, y + length * 3); //right

            //crossroad - right down corner
            //vertical line
            g.DrawLine(WhitePen, x + length * 11, y + length * 5, x + length * 11, y + length * 6);
            g.DrawLine(WhitePen, x + length * 11, y + length * 6, x + length * 11, y + length * 7);
            g.DrawLine(WhitePen, x + length * 11, y + length * 7, x + length * 11, y + length * 8);
            g.DrawLine(WhitePen, x + length * 11, y + length * 8, x + length * 11, y + length * 9);
            //horizontal line
            g.DrawLine(WhitePen, x + length * 11, y + length * 5, x + length * 12, y + length * 5);
            g.DrawLine(WhitePen, x + length * 12, y + length * 5, x + length * 13, y + length * 5);
            g.DrawLine(WhitePen, x + length * 13, y + length * 5, x + length * 14, y + length * 5);

            //white line - crossroad2 - bottom
            //internal
            //g.DrawLine(WhitePen, x + length * 9, y + length * 5, x + length * 10, y + length * 5); //left
            //g.DrawLine(WhitePen, x + length * 10, y + length * 5, x + length * 11, y + length * 5); //right
            //external
            //g.DrawLine(WhitePen, x + length * 9, y + length * 6, x + length * 10, y + length * 6); //left
            g.DrawLine(WhitePen, x + length * 10, y + length * 6, x + length * 11, y + length * 6); //right

            //white line - crossroad2 - right
            //internal
            //g.DrawLine(WhitePen, x + length * 11, y + length * 3, x + length * 11, y + length * 4); //up
            //g.DrawLine(WhitePen, x + length * 11, y + length * 4, x + length * 11, y + length * 5); //down
            //external
            g.DrawLine(WhitePen, x + length * 12, y + length * 3, x + length * 12, y + length * 4); //up
            //g.DrawLine(WhitePen, x + length * 12, y + length * 4, x + length * 12, y + length * 5); //down
                        
            //crossroad - T - left 
            g.DrawLine(WhitePen, x, y + length * 9, x + length, y + length * 9);
            g.DrawLine(WhitePen, x + length, y + length * 9, x + length * 2, y + length * 9);
            g.DrawLine(WhitePen, x + length * 2, y + length * 9, x + length * 3, y + length * 9);

            //crossroad - T - mid 
            g.DrawLine(WhitePen, x + length * 5, y + length * 9, x + length * 6, y + length * 9);
            g.DrawLine(WhitePen, x + length * 6, y + length * 9, x + length * 7, y + length * 9);
            g.DrawLine(WhitePen, x + length * 7, y + length * 9, x + length * 8, y + length * 9);
            g.DrawLine(WhitePen, x + length * 8, y + length * 9, x + length * 9, y + length * 9);

            //crossroad - T - bottom 
            g.DrawLine(WhitePen, x, y + length * 11, x + length * 14, y + length * 11);

            //crossroad - T - right
            g.DrawLine(WhitePen, x + length * 11, y + length * 9, x + length * 12, y + length * 9);
            g.DrawLine(WhitePen, x + length * 12, y + length * 9, x + length * 13, y + length * 9);
            g.DrawLine(WhitePen, x + length * 13, y + length * 9, x + length * 14, y + length * 9);

            //pravá spojka mezi crossroad2 a T2 - čára pro auto
            g.DrawLine(WhitePen, x + length * 9, y + length * 8, x + length * 10, y + length * 8);

            //whiteline - T
            //left
            g.DrawLine(WhitePen, x + length * 2, y + length * 10, x + length * 2, y + length * 11);
            //top
            g.DrawLine(WhitePen, x + length * 3, y + length * 8, x + length * 4, y + length * 8);
            //right
            g.DrawLine(WhitePen, x + length * 6, y + length * 9, x + length * 6, y + length * 10);

            g.DrawLine(WhitePen, x + length * 8, y + length * 10, x + length * 8, y + length * 11);

            g.DrawLine(WhitePen, x + length * 12, y + length * 9, x + length * 12, y + length * 10);

            //Center lines
            #region Center lines

            //vertical center line - left
            //top
            g.DrawLine(WhitePen, x + length * 4, y, x + length * 4, y + length_interrupted);
            g.DrawLine(WhitePen, x + length * 4, y + length_interrupted * 2, x + length * 4, y + length_interrupted * 3);
            //g.DrawLine(WhitePen, x + length * 4, y + length_interrupted * 4, x + length * 4, y + length_interrupted * 5);
            //g.DrawLine(WhitePen, x + length * 4, y + length_interrupted * 6, x + length * 4, y + length_interrupted * 7);
            g.DrawLine(WhitePen, x + length * 4, y + length, x + length * 4, y + length * 2);
            //bottom
            g.DrawLine(WhitePen, x + length * 4, y + length * 6, x + length * 4, y + length * 8);

            //vertical center line - right
            g.DrawLine(WhitePen, x + length * 10, y, x + length * 10, y + length_interrupted);
            g.DrawLine(WhitePen, x + length * 10, y + length_interrupted * 2, x + length * 10, y + length_interrupted * 3);
            //g.DrawLine(WhitePen, x + length * 4, y + length_interrupted * 4, x + length * 4, y + length_interrupted * 5);
            //g.DrawLine(WhitePen, x + length * 4, y + length_interrupted * 6, x + length * 4, y + length_interrupted * 7);
            g.DrawLine(WhitePen, x + length * 10, y + length, x + length * 10, y + length * 2);
            //bottom
            g.DrawLine(WhitePen, x + length * 10, y + length * 6, x + length * 10, y + length * 8);

            //horizontal center line - top 
            g.DrawLine(WhitePen, x, y + length * 4, x + length_interrupted, y + length * 4);
            g.DrawLine(WhitePen, x + length_interrupted * 2, y + length * 4, x + length_interrupted * 3, y + length * 4);
            g.DrawLine(WhitePen, x + length, y + length * 4, x + length * 2, y + length * 4);
            g.DrawLine(WhitePen, x + length * 6, y + length * 4, x + length * 8, y + length * 4);
            g.DrawLine(WhitePen, x + length * 12, y + length * 4, x + length * 13, y + length * 4);
            g.DrawLine(WhitePen, x + length * 13 + length_interrupted, y + length * 4, x + length * 13 + length_interrupted * 2, y + length * 4);
            g.DrawLine(WhitePen, x + length * 13 + length_interrupted * 3, y + length * 4, x + length * 13 + length_interrupted * 4, y + length * 4);

            //g.DrawLine(WhitePen, x + length_interrupted * 4, y + length * 3, x + length_interrupted * 5, y + length * 3);
            //g.DrawLine(WhitePen, x + length_interrupted * 6, y + length * 3, x + length_interrupted * 7, y + length * 3);

            g.DrawLine(WhitePen, x + length, y + length * 10, x + length * 2, y + length * 10);
            g.DrawLine(WhitePen, x + length * 6, y + length * 10, x + length * 8, y + length * 10);

            g.DrawLine(WhitePen, x + length * 12, y + length * 10, x + length * 13, y + length * 10);

            //horizontal center line - bottom 
            //left T
            g.DrawLine(WhitePen, x, y + length * 10, x + length_interrupted, y + length * 10);
            g.DrawLine(WhitePen, x + length_interrupted * 2, y + length * 10, x + length_interrupted * 3, y + length * 10);
            g.DrawLine(WhitePen, x + length, y + length * 10, x + length * 2, y + length * 10);
            
            g.DrawLine(WhitePen, x + length * 6, y + length * 10, x + length * 8, y + length * 10);

            //right T 
            g.DrawLine(WhitePen, x + length * 12, y + length * 10, x + length * 13, y + length * 10);
            g.DrawLine(WhitePen, x + length * 13 + length_interrupted, y + length * 10, x + length * 13 + length_interrupted * 2, y + length * 10);
            g.DrawLine(WhitePen, x + length * 13 + length_interrupted * 3, y + length * 10, x + length * 13 + length_interrupted * 4, y + length * 10);




            #endregion

            //Crosswalks
            #region Crosswalks

            //crossroad1 
            //crossroad1 - crosswalk - top
            g.DrawRectangle(WhitePen, x + length * 3 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 3 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 3 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 3 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 3 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 3 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 4 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 4 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 4 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);

            //crossroad1 - crosswalk - left
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 3 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 3 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 3 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 4 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 4 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 4 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);

            //crossroad1 - crosswalk - right
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 3 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 3 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 3 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 4 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 4 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 4 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);

            //crossroad1 - crosswalk - bottom
            g.DrawRectangle(WhitePen, x + length * 3 + FreeSpace, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 3 + 2 * FreeSpace + crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 3 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + FreeSpace, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + 2 * FreeSpace + crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);

            //crossroad2 
            //crossroad2 - crosswalk - top
            g.DrawRectangle(WhitePen, x + length * 9 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 9 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 9 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);

            //crossroad2 - crosswalk - bottom
            g.DrawRectangle(WhitePen, x + length * 9 + FreeSpace, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 9 + 2 * FreeSpace + crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 9 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + FreeSpace, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + 2 * FreeSpace + crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);

            //crossroad2 - crosswalk - left
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 3 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 3 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 3 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 4 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 4 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 4 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);

            //crossroad2 - crosswalk - right
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 3 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 3 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 3 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 4 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 4 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 4 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);

            //left T
            //left T - crosswalk - top
            g.DrawRectangle(WhitePen, x + length * 3 + FreeSpace, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 3 + 2 * FreeSpace + crosswalk_width, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 3 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + FreeSpace, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + 2 * FreeSpace + crosswalk_width, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);

            //left T - crosswalk - left
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 9 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 9 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 9 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 10 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 10 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 10 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);

            //left T - crosswalk - right
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 9 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 9 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 9 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 10 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 10 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 10 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);

            //right T
            //right T - crosswalk - top
            g.DrawRectangle(WhitePen, x + length * 9 + FreeSpace, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 9 + 2 * FreeSpace + crosswalk_width, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 9 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + FreeSpace, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + 2 * FreeSpace + crosswalk_width, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);

            //right T - crosswalk - left
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 9 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 9 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 9 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 10 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 10 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 10 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);

            //right T - crosswalk - right
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 9 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 9 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 9 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 10 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 10 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 10 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);

            #endregion

            //TrafficLights
            #region TrafficLights

            //crossorad1
            #region Crossroad1
            //crossroad1 - trafficlight - top
            g.DrawRectangle(WhitePen, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_height);
            g.FillEllipse(red, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            //crossroad1 - trafficlight - left
            g.DrawRectangle(WhitePen, x + length + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length + 3 * FreeSpace + TrafficLights_width * 2, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length + 3 * FreeSpace + TrafficLights_width, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);

            //crossroad1 - trafficlight - right
            g.DrawRectangle(WhitePen, x + length * 6 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length * 6 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 6 + FreeSpace + TrafficLights_width, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 6 + FreeSpace + TrafficLights_width * 2, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            //crossroad1 - trafficlight - bottom
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_height);
            g.FillEllipse(red, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
            
            #endregion

            //crossroad2
            #region Crossroad2
            //crossroad2 - trafficlight - top
            g.DrawRectangle(WhitePen, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_height);
            g.FillEllipse(red, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            //crossroad2 - trafficlight - left
            g.DrawRectangle(WhitePen, x + length * 7 + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length * 7 + 3 * FreeSpace + TrafficLights_width * 2, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 7 + 3 * FreeSpace + TrafficLights_width, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 7 + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);

            //crossroad2 - trafficlight - right
            g.DrawRectangle(WhitePen, x + length * 12 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length * 12 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 12 + FreeSpace + TrafficLights_width, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 12 + FreeSpace + TrafficLights_width * 2, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            //crossroad2 - trafficlight - bottom
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_height);
            g.FillEllipse(red, x + length * 11 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 11 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 11 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);

            #endregion

            //left T
            #region Left T
            //left T - trafficlight - top
            g.DrawRectangle(WhitePen, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace, TrafficLights_width, TrafficLights_height);
            g.FillEllipse(red, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            //left T - trafficlight - left
            g.DrawRectangle(WhitePen, x + length + 3 * FreeSpace, y + length * 11 + FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length + 3 * FreeSpace + TrafficLights_width * 2, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length + 3 * FreeSpace + TrafficLights_width, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length + 3 * FreeSpace, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);

            //left T - trafficlight - right
            g.DrawRectangle(WhitePen, x + length * 6 + FreeSpace, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length * 6 + FreeSpace, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 6 + FreeSpace + TrafficLights_width, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 6 + FreeSpace + TrafficLights_width * 2, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            #endregion

            //right T
            #region Right T
            //right T - trafficlight - top
            g.DrawRectangle(WhitePen, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace, TrafficLights_width, TrafficLights_height);
            g.FillEllipse(red, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            //right T - trafficlight - left
            g.DrawRectangle(WhitePen, x + length * 7 + 3 * FreeSpace, y + length * 11 + FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length * 7 + 3 * FreeSpace + TrafficLights_width * 2, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 7 + 3 * FreeSpace + TrafficLights_width, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 7 + 3 * FreeSpace, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);

            //right T - trafficlight - right
            g.DrawRectangle(WhitePen, x + length * 12 + FreeSpace, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length * 12 + FreeSpace, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 12 + FreeSpace + TrafficLights_width, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 12 + FreeSpace + TrafficLights_width * 2, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            #endregion

            #endregion

            //CrosswalkLights
            #region CrosswalkLights 

            //crossroad1
            #region Crossroad1
            //top
            //crossroad1 - CrosswalkLights - top - left
            g.DrawRectangle(WhitePen, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad1 - CrosswalkLights - top - right
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //bottom
            //crossroad1 - CrosswalkLights - bottom - left
            g.DrawRectangle(WhitePen, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad1 - CrosswalkLights - bottom - right
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //left
            //crossroad1 - CrosswalkLights - left - top
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad1 - CrosswalkLights - left - bottom
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //right
            //crossroad1 - CrosswalkLights - right - top
            g.DrawRectangle(WhitePen, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad1 - CrosswalkLights - right - bottom
            g.DrawRectangle(WhitePen, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            #endregion

            //crossroad2
            #region Crossroad2
            //top
            //crossroad2 - CrosswalkLights - top - left
            g.DrawRectangle(WhitePen, x + length * 8 + crosswalk_width + 3 * FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 8 + crosswalk_width + 3 * FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 8 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad2 - CrosswalkLights - top - right
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 11 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 11 + FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //bottom
            //crossroad2 - CrosswalkLights - bottom - left
            g.DrawRectangle(WhitePen, x + length * 8 + crosswalk_width + 3 * FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 8 + crosswalk_width + 3 * FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 8 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad2 - CrosswalkLights - bottom - right
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 11 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 11 + FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //left
            //crossroad2 - CrosswalkLights - left - top
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 8 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 8 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad2 - CrosswalkLights - left - bottom
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 8 + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 8 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //right
            //crossroad2 - CrosswalkLights - right - top
            g.DrawRectangle(WhitePen, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad2 - CrosswalkLights - right - bottom
            g.DrawRectangle(WhitePen, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            #endregion

            //left T
            #region Left T
            //left T - CrosswalkLights - top - left
            g.DrawRectangle(WhitePen, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //left T - CrosswalkLights - top - right
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //left T - CrosswalkLights - left - top
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 2 + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //left T - CrosswalkLights - left - bottom
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + length * 11 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 2 + FreeSpace, y + length * 11 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + FreeSpace, y + length * 11 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //left T - CrosswalkLights - right - top 
            g.DrawRectangle(WhitePen, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //left T - CrosswalkLights - right - bottom
            g.DrawRectangle(WhitePen, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 11 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 11 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 11 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            #endregion

            //right T
            #region Right T
            //right T - CrosswalkLights - top - left
            g.DrawRectangle(WhitePen, x + length * 8 + crosswalk_width + 3 * FreeSpace, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 8 + crosswalk_width + 3 * FreeSpace, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 8 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //right T - CrosswalkLights - top - right
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 11 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 11 + FreeSpace, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //right T - CrosswalkLights - left - top
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 8 + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 8 + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //right T - CrosswalkLights - left - botom
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + length * 11 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 8 + FreeSpace, y + length * 11 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 8 + FreeSpace, y + length * 11 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //right T - CrosswalkLights - right - top
            g.DrawRectangle(WhitePen, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //right T - CrosswalkLights - right - bottom
            g.DrawRectangle(WhitePen, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 11 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 11 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 11 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            #endregion


            #endregion

            */
            #endregion
        }

        //BTNs in UserControl
        #region BTNs in UserControl
        private void InitializeButtons()
        {
            //Crossroad1 
            #region Crossroad1 BTNs

            //Crossroad1 - top crosswalk
            #region Crossroad1 - top crosswalk
            //left  
            btnCrossroad1TopCrosswalkLEFT = new Button();
            btnCrossroad1TopCrosswalkLEFT.Text = "Crossroad1 Top crosswalk";
            btnCrossroad1TopCrosswalkLEFT.BackColor = Color.White;
            btnCrossroad1TopCrosswalkLEFT.Visible = false;
            btnCrossroad1TopCrosswalkLEFT.Enabled = false;
            btnCrossroad1TopCrosswalkLEFT.Location = new System.Drawing.Point(Convert.ToInt32(length), Convert.ToInt32(length)); //cannot invert float to int 
            btnCrossroad1TopCrosswalkLEFT.Size = new Size(Convert.ToInt32(Button_width), Convert.ToInt32(Button_height)); //cannot invert float to int 
            btnCrossroad1TopCrosswalkLEFT.Click += btnCrossroad1TopCrosswalkLEFT_Click;
            btnCrossroad1TopCrosswalkLEFT.Click += (sender, e) => OnButtonClicked("btnCrossroad1TopCrosswalkLEFT");
            
            //right
            btnCrossroad1TopCrosswalkRIGHT = new Button();
            btnCrossroad1TopCrosswalkRIGHT.Text = "Crossroad1 Top crosswalk";
            btnCrossroad1TopCrosswalkRIGHT.BackColor = Color.White;
            btnCrossroad1TopCrosswalkRIGHT.Visible = false;
            btnCrossroad1TopCrosswalkRIGHT.Enabled = false;
            btnCrossroad1TopCrosswalkRIGHT.Location = new System.Drawing.Point(Convert.ToInt32(length * 6), Convert.ToInt32(length)); //cannot invert float to int 
            btnCrossroad1TopCrosswalkRIGHT.Size = new Size(Convert.ToInt32(Button_width), Convert.ToInt32(Button_height)); //cannot invert float to int 
            btnCrossroad1TopCrosswalkRIGHT.Click += btnCrossroad1TopCrosswalkRIGHT_Click;
            btnCrossroad1TopCrosswalkRIGHT.Click += (sender, e) => OnButtonClicked("btnCrossroad1TopCrosswalkRIGHT");


            #endregion

            //Crossroad1 - left crosswalk
            #region Crossroad1 - left crosswalk
            //top 
            btnCrossroad1LeftCrosswalkTOP = new Button();
            btnCrossroad1LeftCrosswalkTOP.Text = "Crossroad1 Left crosswalk";
            btnCrossroad1LeftCrosswalkTOP.BackColor = Color.White;
            btnCrossroad1LeftCrosswalkTOP.Visible = false;
            btnCrossroad1LeftCrosswalkTOP.Enabled = false;
            btnCrossroad1LeftCrosswalkTOP.Location = new System.Drawing.Point(Convert.ToInt32(length), Convert.ToInt32(length * 2)); //cannot invert float to int 
            btnCrossroad1LeftCrosswalkTOP.Size = new Size(Convert.ToInt32(Button_width), Convert.ToInt32(Button_height)); //cannot invert float to int 
            btnCrossroad1LeftCrosswalkTOP.Click += btnCrossroad1LeftCrosswalkTOP_Click;
            btnCrossroad1LeftCrosswalkTOP.Click += (sender, e) => OnButtonClicked("btnCrossroad1LeftCrosswalkTOP");

            //bottom
            btnCrossroad1LeftCrosswalkBOTTOM = new Button();
            btnCrossroad1LeftCrosswalkBOTTOM.Text = "Crossroad1 Left crosswalk";
            btnCrossroad1LeftCrosswalkBOTTOM.BackColor = Color.White;
            btnCrossroad1LeftCrosswalkBOTTOM.Visible = false;
            btnCrossroad1LeftCrosswalkBOTTOM.Enabled = false;
            btnCrossroad1LeftCrosswalkBOTTOM.Location = new System.Drawing.Point(Convert.ToInt32(length), Convert.ToInt32(length * 6)); //cannot invert float to int 
            btnCrossroad1LeftCrosswalkBOTTOM.Size = new Size(Convert.ToInt32(Button_width), Convert.ToInt32(Button_height)); //cannot invert float to int 
            btnCrossroad1LeftCrosswalkBOTTOM.Click += btnCrossroad1LeftCrosswalkBOTTOM_Click;
            btnCrossroad1LeftCrosswalkBOTTOM.Click += (sender, e) => OnButtonClicked("btnCrossroad1LeftCrosswalkBOTTOM");

            #endregion

            #endregion

            //Crossroad2
            #region Crossroad2 BTNs

            //Crossroad2 - left crosswalk 
            #region Crossroad2 - left crosswalk 
            //top 
            btnCrossroad2LeftCrosswalkTOP = new Button();
            btnCrossroad2LeftCrosswalkTOP.Text = "Crossroad2 Left crosswalk";
            btnCrossroad2LeftCrosswalkTOP.BackColor = Color.White;
            btnCrossroad2LeftCrosswalkTOP.Visible = false;
            btnCrossroad2LeftCrosswalkTOP.Enabled = false;
            btnCrossroad2LeftCrosswalkTOP.Location = new System.Drawing.Point(Convert.ToInt32(length * 7), Convert.ToInt32(length * 2)); //cannot invert float to int 
            btnCrossroad2LeftCrosswalkTOP.Size = new Size(Convert.ToInt32(Button_width), Convert.ToInt32(Button_height)); //cannot invert float to int 
            btnCrossroad2LeftCrosswalkTOP.Click += btnCrossroad2LeftCrosswalkTOP_Click;
            btnCrossroad2LeftCrosswalkTOP.Click += (sender, e) => OnButtonClicked("btnCrossroad2LeftCrosswalkTOP");


            //bottom
            btnCrossroad2LeftCrosswalkBOTTOM = new Button();
            btnCrossroad2LeftCrosswalkBOTTOM.Text = "Crossroad2 Left crosswalk";
            btnCrossroad2LeftCrosswalkBOTTOM.BackColor = Color.White;
            btnCrossroad2LeftCrosswalkBOTTOM.Visible = false;
            btnCrossroad2LeftCrosswalkBOTTOM.Enabled = false;
            btnCrossroad2LeftCrosswalkBOTTOM.Location = new System.Drawing.Point(Convert.ToInt32(length * 7), Convert.ToInt32(length * 6)); //cannot invert float to int 
            btnCrossroad2LeftCrosswalkBOTTOM.Size = new Size(Convert.ToInt32(Button_width), Convert.ToInt32(Button_height)); //cannot invert float to int 
            btnCrossroad2LeftCrosswalkBOTTOM.Click += btnCrossroad2LeftCrosswalkBOTTOM_Click;
            btnCrossroad2LeftCrosswalkBOTTOM.Click += (sender, e) => OnButtonClicked("btnCrossroad2LeftCrosswalkBOTTOM");

            #endregion

            //Crossroad2 - right crosswalk
            #region Crossroad2 - right crosswalk
            //top 
            btnCrossroad2RightCrosswalkTOP = new Button();
            btnCrossroad2RightCrosswalkTOP.Text = "Crossroad2 Right crosswalk";
            btnCrossroad2RightCrosswalkTOP.BackColor = Color.White;
            btnCrossroad2RightCrosswalkTOP.Visible = false;
            btnCrossroad2RightCrosswalkTOP.Enabled = false;
            btnCrossroad2RightCrosswalkTOP.Location = new System.Drawing.Point(Convert.ToInt32(length * 13), Convert.ToInt32(length * 2)); //cannot invert float to int 
            btnCrossroad2RightCrosswalkTOP.Size = new Size(Convert.ToInt32(Button_width), Convert.ToInt32(Button_height)); //cannot invert float to int 
            btnCrossroad2RightCrosswalkTOP.Click += btnCrossroad2RightCrosswalkTOP_Click;
            btnCrossroad2RightCrosswalkTOP.Click += (sender, e) => OnButtonClicked("btnCrossroad2RightCrosswalkTOP");

            //bottom
            btnCrossroad2RightCrosswalkBOTTOM = new Button();
            btnCrossroad2RightCrosswalkBOTTOM.Text = "Crossroad2 Right crosswalk";
            btnCrossroad2RightCrosswalkBOTTOM.BackColor = Color.White;
            btnCrossroad2RightCrosswalkBOTTOM.Visible = false;
            btnCrossroad2RightCrosswalkBOTTOM.Enabled = false;
            btnCrossroad2RightCrosswalkBOTTOM.Location = new System.Drawing.Point(Convert.ToInt32(length * 13), Convert.ToInt32(length * 6)); //cannot invert float to int 
            btnCrossroad2RightCrosswalkBOTTOM.Size = new Size(Convert.ToInt32(Button_width), Convert.ToInt32(Button_height)); //cannot invert float to int 
            btnCrossroad2RightCrosswalkBOTTOM.Click += btnCrossroad2RightCrosswalkBOTTOM_Click;
            btnCrossroad2RightCrosswalkBOTTOM.Click += (sender, e) => OnButtonClicked("btnCrossroad2RightCrosswalkBOTTOM");

            #endregion

            #endregion

            //Left T
            #region LeftT BTNs

            #region LeftT - left crosswalk
            //top 
            btnLeftTLeftCrosswalkTOP = new Button();
            btnLeftTLeftCrosswalkTOP.Text = "LeftT\nLeft crosswalk";
            btnLeftTLeftCrosswalkTOP.BackColor = Color.White;
            btnLeftTLeftCrosswalkTOP.Visible = false;
            btnLeftTLeftCrosswalkTOP.Enabled = false;
            btnLeftTLeftCrosswalkTOP.Location = new System.Drawing.Point(Convert.ToInt32(length), Convert.ToInt32(length * 8)); //cannot invert float to int 
            btnLeftTLeftCrosswalkTOP.Size = new Size(Convert.ToInt32(Button_width), Convert.ToInt32(Button_height)); //cannot invert float to int 
            btnLeftTLeftCrosswalkTOP.Click += btnLeftTLeftCrosswalkTOP_CLick;
            btnLeftTLeftCrosswalkTOP.Click += (sender, e) => OnButtonClicked("btnLeftTLeftCrosswalkTOP");

            //bottom
            btnLeftTLeftCrosswalkBOTTOM = new Button();
            btnLeftTLeftCrosswalkBOTTOM.Text = "LeftT\nLeft crosswalk";
            btnLeftTLeftCrosswalkBOTTOM.BackColor = Color.White;
            btnLeftTLeftCrosswalkBOTTOM.Visible = false;
            btnLeftTLeftCrosswalkBOTTOM.Enabled = false;
            btnLeftTLeftCrosswalkBOTTOM.Location = new System.Drawing.Point(Convert.ToInt32(length), Convert.ToInt32(length * 12)); //cannot invert float to int 
            btnLeftTLeftCrosswalkBOTTOM.Size = new Size(Convert.ToInt32(Button_width), Convert.ToInt32(Button_height)); //cannot invert float to int 
            btnLeftTLeftCrosswalkBOTTOM.Click += btnLeftTLeftCrosswalkBOTTOM_CLick;
            btnLeftTLeftCrosswalkBOTTOM.Click += (sender, e) => OnButtonClicked("btnLeftTLeftCrosswalkBOTTOM");

            #endregion

            #endregion

            //Right T 
            #region RightT BTNs

            #region RightT - top crosswalk
            //left  
            btnRightTTopCrosswalkLEFT = new Button();
            btnRightTTopCrosswalkLEFT.Text = "RightT\nTop crosswalk";
            btnRightTTopCrosswalkLEFT.BackColor = Color.White;
            btnRightTTopCrosswalkLEFT.Visible = false;
            btnRightTTopCrosswalkLEFT.Enabled = false;
            btnRightTTopCrosswalkLEFT.Location = new System.Drawing.Point(Convert.ToInt32(length * 7), Convert.ToInt32(length * 8)); //cannot invert float to int 
            btnRightTTopCrosswalkLEFT.Size = new Size(Convert.ToInt32(Button_width), Convert.ToInt32(Button_height)); //cannot invert float to int 
            btnRightTTopCrosswalkLEFT.Click += btnCrossroad1TopCrosswalkLEFT_Click;
            btnRightTTopCrosswalkLEFT.Click += (sender, e) => OnButtonClicked("btnRightTTopCrosswalkLEFT");

            //right
            btnRightTTopCrosswalkRIGHT = new Button();
            btnRightTTopCrosswalkRIGHT.Text = "RightT\nTop crosswalk";
            btnRightTTopCrosswalkRIGHT.BackColor = Color.White;
            btnRightTTopCrosswalkRIGHT.Visible = false;
            btnRightTTopCrosswalkRIGHT.Enabled = false;
            btnRightTTopCrosswalkRIGHT.Location = new System.Drawing.Point(Convert.ToInt32(length * 12), Convert.ToInt32(length * 8)); //cannot invert float to int 
            btnRightTTopCrosswalkRIGHT.Size = new Size(Convert.ToInt32(Button_width), Convert.ToInt32(Button_height)); //cannot invert float to int 
            btnRightTTopCrosswalkRIGHT.Click += btnCrossroad1TopCrosswalkRIGHT_Click;
            btnRightTTopCrosswalkRIGHT.Click += (sender, e) => OnButtonClicked("btnRightTTopCrosswalkRIGHT");

            #endregion

            #endregion


            //final add to UserControl
            //Crossroad1
            this.Controls.Add(btnCrossroad1TopCrosswalkLEFT);
            this.Controls.Add(btnCrossroad1TopCrosswalkRIGHT);
            this.Controls.Add(btnCrossroad1LeftCrosswalkTOP);
            this.Controls.Add(btnCrossroad1LeftCrosswalkBOTTOM);
            //Crossroad2
            this.Controls.Add(btnCrossroad2LeftCrosswalkTOP);
            this.Controls.Add(btnCrossroad2LeftCrosswalkBOTTOM);
            this.Controls.Add(btnCrossroad2RightCrosswalkTOP);
            this.Controls.Add(btnCrossroad2RightCrosswalkBOTTOM);
            //LeftT
            this.Controls.Add(btnLeftTLeftCrosswalkTOP);
            this.Controls.Add(btnLeftTLeftCrosswalkBOTTOM);
            //Right T
            this.Controls.Add(btnRightTTopCrosswalkLEFT);
            this.Controls.Add(btnRightTTopCrosswalkRIGHT);
        
        }
        #endregion

        //Methods for BTN_CLick action
        #region Methods for BTN_CLick action

        private void OnButtonClicked(string buttonIdentifier)
        {
            ButtonClicked?.Invoke(this, buttonIdentifier);
        }

        private void btnCrossroad1TopCrosswalkLEFT_Click(object? sender, EventArgs e)
        {
            ButtonClicked?.Invoke(this, "btnCrossroad1TopCrosswalkLEFT");
        }

        private void btnCrossroad1TopCrosswalkRIGHT_Click(object? sender, EventArgs e)
        {
            ButtonClicked?.Invoke(this, "btnCrossroad1TopCrosswalkRIGHT");
        }

        private void btnCrossroad1LeftCrosswalkTOP_Click(object? sender, EventArgs e)
        {
            ButtonClicked?.Invoke(this, "btnCrossroad1LeftCrosswalkTOP");
        }

        private void btnCrossroad1LeftCrosswalkBOTTOM_Click(object? sender, EventArgs e)
        {
            ButtonClicked?.Invoke(this, "btnCrossroad1LeftCrosswalkBOTTOM");
        }

        private void btnCrossroad2LeftCrosswalkTOP_Click(object? sender, EventArgs e)
        {
            ButtonClicked?.Invoke(this, "btnCrossroad2LeftCrosswalkTOP");
        }

        private void btnCrossroad2LeftCrosswalkBOTTOM_Click(object? sender, EventArgs e)
        {
            ButtonClicked?.Invoke(this, "btnCrossroad2LeftCrosswalkBOTTOM");
        }

        private void btnCrossroad2RightCrosswalkTOP_Click(object? sender, EventArgs e)
        {
            ButtonClicked?.Invoke(this, "btnCrossroad2RightCrosswalkTOP");
        }

        private void btnCrossroad2RightCrosswalkBOTTOM_Click(object? sender, EventArgs e)
        {
            ButtonClicked?.Invoke(this, "btnCrossroad2RightCrosswalkBOTTOM");
        }

        private void btnLeftTLeftCrosswalkTOP_CLick(object? sender, EventArgs e)
        {
            ButtonClicked?.Invoke(this, "btnLeftTLeftCrosswalkTOP");
        }

        private void btnLeftTLeftCrosswalkBOTTOM_CLick(object? sender, EventArgs e)
        {
            ButtonClicked?.Invoke(this, "btnLeftTLeftCrosswalkBOTTOM");
        }

        private void btnRightTTopCrosswalkLEFT_Click(object? sender, EventArgs e)
        {
            ButtonClicked?.Invoke(this, "btnRightTTopCrosswalkLEFT");
        }
        private void btnRightTTopCrosswalkRIGHT_Click(object? sender, EventArgs e)
        {
            ButtonClicked?.Invoke(this, "btnRightTTopCrosswalkRIGHT");
        }

        #endregion

        //Methods for rendering crossroad
        #region Methods for rendering crossroad

        //Crossroad1
        public void BasicCrossroad()
        {
            var g = this.CreateGraphics();

            //background
            //g.Clear(Color.Black);

            //pen color
            Pen BlackPen = new Pen(Color.Black, 2);
            Pen WhitePen = new Pen(Color.White, 2);
            Pen RedPen = new Pen(Color.Red, 2);
            Pen GreenPen = new Pen(Color.Green, 2);
            Pen YellowPen = new Pen(Color.Yellow, 2);

            this.Refresh();

            //BTN allow
            #region BTN alow 

            //crossroad 1
            btnCrossroad1TopCrosswalkLEFT.Visible = true;
            btnCrossroad1TopCrosswalkLEFT.Enabled = true;
            btnCrossroad1TopCrosswalkRIGHT.Visible = true;
            btnCrossroad1TopCrosswalkRIGHT.Enabled = true;
            btnCrossroad1LeftCrosswalkTOP.Visible = true;
            btnCrossroad1LeftCrosswalkTOP.Enabled = true;
            btnCrossroad1LeftCrosswalkBOTTOM.Visible = true;
            btnCrossroad1LeftCrosswalkBOTTOM.Enabled = true;

            //crossorad2 
            btnCrossroad2LeftCrosswalkBOTTOM.Visible = false;
            btnCrossroad2LeftCrosswalkBOTTOM.Enabled = false;
            btnCrossroad2LeftCrosswalkTOP.Visible = false;
            btnCrossroad2LeftCrosswalkTOP.Enabled = false;
            btnCrossroad2RightCrosswalkBOTTOM.Visible = false;
            btnCrossroad2RightCrosswalkBOTTOM.Enabled = false;
            btnCrossroad2RightCrosswalkTOP.Visible = false;
            btnCrossroad2RightCrosswalkTOP.Enabled = false;

            //left T
            btnLeftTLeftCrosswalkTOP.Visible = false;
            btnLeftTLeftCrosswalkTOP.Enabled = false;
            btnLeftTLeftCrosswalkBOTTOM.Visible = false;
            btnLeftTLeftCrosswalkBOTTOM.Enabled = false;

            //right T
            btnRightTTopCrosswalkLEFT.Visible = false;
            btnRightTTopCrosswalkLEFT.Enabled = false;
            btnRightTTopCrosswalkRIGHT.Visible = false;
            btnRightTTopCrosswalkRIGHT.Enabled = false;
            
            #endregion

            //Traffic lines
            #region Traffic lines

            //crossroad1
            #region crossroad1

            #region Corners

            //left top corner 
            //vertical line
            g.DrawLine(WhitePen, x + length * 3, y, x + length * 3, y + length * 3);
            /*
            g.DrawLine(WhitePen, x + length * 3, y, x + length * 3, y + length);
            g.DrawLine(WhitePen, x + length * 3, y + length, x + length * 3, y + length * 2);
            g.DrawLine(WhitePen, x + length * 3, y + length * 2, x + length * 3, y + length * 3);
            */
            //horizontal line
            g.DrawLine(WhitePen, x, y + length * 3, x + length * 3, y + length * 3);
            /*
            g.DrawLine(WhitePen, x, y + length * 3, x + length, y + length * 3);
            g.DrawLine(WhitePen, x + length, y + length * 3, x + length * 2, y + length * 3);
            g.DrawLine(WhitePen, x + length * 2, y + length * 3, x + length * 3, y + length * 3);
            */

            //left bottom corner
            //vertical line 
            g.DrawLine(WhitePen, x, y + length * 5, x + length * 3, y + length * 5);
            /*
            g.DrawLine(WhitePen, x, y + length * 5, x + length, y + length * 5);
            g.DrawLine(WhitePen, x + length, y + length * 5, x + length * 2, y + length * 5);
            g.DrawLine(WhitePen, x + length * 2, y + length * 5, x + length * 3, y + length * 5);
            */
            //horizontal line 
            g.DrawLine(WhitePen, x + length * 3, y + length * 5, x + length * 3, y + length * 9);
            /*
            g.DrawLine(WhitePen, x + length * 3, y + length * 5, x + length * 3, y + length * 6);
            g.DrawLine(WhitePen, x + length * 3, y + length * 6, x + length * 3, y + length * 7);
            g.DrawLine(WhitePen, x + length * 3, y + length * 7, x + length * 3, y + length * 8);
            g.DrawLine(WhitePen, x + length * 3, y + length * 8, x + length * 3, y + length * 9);
            */

            //right top corner
            //vertical line
            g.DrawLine(WhitePen, x + length * 5, y, x + length * 5, y + length * 3);
            /*
            g.DrawLine(WhitePen, x + length * 5, y, x + length * 5, y + length);
            g.DrawLine(WhitePen, x + length * 5, y + length, x + length * 5, y + length * 2);
            g.DrawLine(WhitePen, x + length * 5, y + length * 2, x + length * 5, y + length * 3);
            */
            //horizontal line 
            //crossroad - mid up - mid 
            g.DrawLine(WhitePen, x + length * 5, y + length * 3, x + length * 9, y + length * 3);
            /*
            g.DrawLine(WhitePen, x + length * 5, y + length * 3, x + length * 6, y + length * 3);
            g.DrawLine(WhitePen, x + length * 6, y + length * 3, x + length * 7, y + length * 3);
            g.DrawLine(WhitePen, x + length * 7, y + length * 3, x + length * 8, y + length * 3);
            g.DrawLine(WhitePen, x + length * 8, y + length * 3, x + length * 9, y + length * 3);
            */

            //right bottom corner 
            //vertical line
            g.DrawLine(WhitePen, x + length * 5, y + length * 5, x + length * 5, y + length * 9);
            /*
            g.DrawLine(WhitePen, x + length * 5, y + length * 5, x + length * 5, y + length * 6);
            g.DrawLine(WhitePen, x + length * 5, y + length * 6, x + length * 5, y + length * 7);
            g.DrawLine(WhitePen, x + length * 5, y + length * 7, x + length * 5, y + length * 8);
            g.DrawLine(WhitePen, x + length * 5, y + length * 8, x + length * 5, y + length * 9);
            */
            //horizontal line 
            g.DrawLine(WhitePen, x + length * 5, y + length * 5, x + length * 9, y + length * 5);
            /*
            g.DrawLine(WhitePen, x + length * 5, y + length * 5, x + length * 6, y + length * 5);
            g.DrawLine(WhitePen, x + length * 6, y + length * 5, x + length * 7, y + length * 5);
            g.DrawLine(WhitePen, x + length * 7, y + length * 5, x + length * 8, y + length * 5);
            g.DrawLine(WhitePen, x + length * 8, y + length * 5, x + length * 9, y + length * 5);
            */

            #endregion

            #region whitelines / traffic lines 

            //top 
            //white line - crossroad1 - top - internal
            //g.DrawLine(WhitePen, x + length * 3, y + length * 3, x + length * 4, y + length * 3); //left
            //g.DrawLine(WhitePen, x + length * 4, y + length * 3, x + length * 5, y + length * 3); //right
            //white line - crossroad1 - top - exteral
            g.DrawLine(WhitePen, x + length * 3, y + length * 2, x + length * 4, y + length * 2); //left
            //g.DrawLine(WhitePen, x + length * 4, y + length * 2, x + length * 5, y + length * 2); //right

            //bottom
            //white line - crossroad1 - bottom - internal
            //g.DrawLine(WhitePen, x + length * 3, y + length * 5, x + length * 4, y + length * 5); //left
            //g.DrawLine(WhitePen, x + length * 4, y + length * 5, x + length * 5, y + length * 5); //right
            //white line - crossroad1 - bottom - external
            //g.DrawLine(WhitePen, x + length * 3, y + length * 6, x + length * 4, y + length * 6); //left
            g.DrawLine(WhitePen, x + length * 4, y + length * 6, x + length * 5, y + length * 6); //right   

            //left 
            //white line - crossorad1 - left - internal
            //g.DrawLine(WhitePen, x + length * 3, y + length * 3, x + length * 3, y + length * 4); //up
            //g.DrawLine(WhitePen, x + length * 3, y + length * 4, x + length * 3, y + length * 5); //down
            //white line - crossroad1 - left - external
            //g.DrawLine(WhitePen, x + length * 2, y + length * 3, x + length * 2, y + length * 4); //up
            g.DrawLine(WhitePen, x + length * 2, y + length * 4, x + length * 2, y + length * 5); //down

            //right
            //white line - crossroad1 - right - internal
            //g.DrawLine(WhitePen, x + length * 5, y + length * 3, x + length * 5, y + length * 4); //up
            //g.DrawLine(WhitePen, x + length * 5, y + length * 4, x + length * 5, y + length * 5); //down
            //white line - crossroad1 - right - external
            g.DrawLine(WhitePen, x + length * 6, y + length * 3, x + length * 6, y + length * 4); //up
                                                                                                  //g.DrawLine(WhitePen, x + length * 6, y + length * 4, x + length * 6, y + length * 5); //down

            #endregion

            #endregion

            #endregion

            //Center lines
            #region Center lines

            //vertical center line - left
            //top
            g.DrawLine(WhitePen, x + length * 4, y, x + length * 4, y + length_interrupted);
            g.DrawLine(WhitePen, x + length * 4, y + length_interrupted * 2, x + length * 4, y + length_interrupted * 3);
            //g.DrawLine(WhitePen, x + length * 4, y + length_interrupted * 4, x + length * 4, y + length_interrupted * 5);
            //g.DrawLine(WhitePen, x + length * 4, y + length_interrupted * 6, x + length * 4, y + length_interrupted * 7);
            g.DrawLine(WhitePen, x + length * 4, y + length, x + length * 4, y + length * 2);
            //bottom
            g.DrawLine(WhitePen, x + length * 4, y + length * 6, x + length * 4, y + length * 8);
                        
            //horizontal center line - top 
            g.DrawLine(WhitePen, x, y + length * 4, x + length_interrupted, y + length * 4);
            g.DrawLine(WhitePen, x + length_interrupted * 2, y + length * 4, x + length_interrupted * 3, y + length * 4);
            g.DrawLine(WhitePen, x + length, y + length * 4, x + length * 2, y + length * 4);
            g.DrawLine(WhitePen, x + length * 6, y + length * 4, x + length * 8, y + length * 4);
            //g.DrawLine(WhitePen, x + length * 12, y + length * 4, x + length * 13, y + length * 4);
            //g.DrawLine(WhitePen, x + length * 13 + length_interrupted, y + length * 4, x + length * 13 + length_interrupted * 2, y + length * 4);
            //g.DrawLine(WhitePen, x + length * 13 + length_interrupted * 3, y + length * 4, x + length * 13 + length_interrupted * 4, y + length * 4);
            //g.DrawLine(WhitePen, x + length_interrupted * 4, y + length * 3, x + length_interrupted * 5, y + length * 3);
            //g.DrawLine(WhitePen, x + length_interrupted * 6, y + length * 3, x + length_interrupted * 7, y + length * 3);
                                  
            #endregion
                        
            //TrafficLights
            #region TrafficLights

            //crossorad1
            #region Crossroad1
            //crossroad1 - trafficlight - top
            g.DrawRectangle(WhitePen, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_height);
            g.FillEllipse(red, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            //crossroad1 - trafficlight - left
            g.DrawRectangle(WhitePen, x + length + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length + 3 * FreeSpace + TrafficLights_width * 2, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length + 3 * FreeSpace + TrafficLights_width, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);

            //crossroad1 - trafficlight - right
            g.DrawRectangle(WhitePen, x + length * 6 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length * 6 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 6 + FreeSpace + TrafficLights_width, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 6 + FreeSpace + TrafficLights_width * 2, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            //crossroad1 - trafficlight - bottom
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_height);
            g.FillEllipse(red, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);

            #endregion
                       
            #endregion
            
            //Crosswalks
            #region Crosswalks

            //crossroad1 
            #region Crossroad 1
            //crossroad1 - crosswalk - top
            g.DrawRectangle(WhitePen, x + length * 3 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 3 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 3 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 3 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 3 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 3 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 4 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 4 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 4 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);

            //crossroad1 - crosswalk - left
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 3 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 3 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 3 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 4 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 4 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 4 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);

            //crossroad1 - crosswalk - right
            /*
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 3 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 3 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 3 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 4 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 4 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 4 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            */

            //crossroad1 - crosswalk - bottom
            /*
            g.DrawRectangle(WhitePen, x + length * 3 + FreeSpace, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 3 + 2 * FreeSpace + crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 3 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + FreeSpace, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + 2 * FreeSpace + crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            */

            #endregion
                        
            #endregion
            
            //CrosswalkLights
            #region CrosswalkLights 

            //crossroad1
            #region Crossroad1
            //top
            //crossroad1 - CrosswalkLights - top - left
            g.DrawRectangle(WhitePen, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad1 - CrosswalkLights - top - right
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //bottom
            //crossroad1 - CrosswalkLights - bottom - left
            /*
            g.DrawRectangle(WhitePen, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            */
            //crossroad1 - CrosswalkLights - bottom - right
            /*
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            */

            //left
            //crossroad1 - CrosswalkLights - left - top
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad1 - CrosswalkLights - left - bottom
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //right
            //crossroad1 - CrosswalkLights - right - top
            /*
            g.DrawRectangle(WhitePen, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            */
            //crossroad1 - CrosswalkLights - right - bottom
            /*
            g.DrawRectangle(WhitePen, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            */

            #endregion
                       
            #endregion
                        
        }

        //Crossroad1 + Crossorad2
        public void CrossroadExtension1()
        {
            var g = this.CreateGraphics();
            
            //background
            //g.Clear(Color.Black);

            //pen color
            Pen BlackPen = new Pen(Color.Black, 2);
            Pen WhitePen = new Pen(Color.White, 2);
            Pen RedPen = new Pen(Color.Red, 2);
            Pen GreenPen = new Pen(Color.Green, 2);
            Pen YellowPen = new Pen(Color.Yellow, 2);

            this.Refresh();

            //BTN allow
            #region BTN alow 
            //Crossroad1
            //top crosswalk
            btnCrossroad1TopCrosswalkLEFT.Visible = true;
            btnCrossroad1TopCrosswalkLEFT.Enabled = true;
            btnCrossroad1TopCrosswalkRIGHT.Visible = true;
            btnCrossroad1TopCrosswalkRIGHT.Enabled = true;
            //left crosswalk
            btnCrossroad1LeftCrosswalkTOP.Visible = true;
            btnCrossroad1LeftCrosswalkTOP.Enabled = true;
            btnCrossroad1LeftCrosswalkBOTTOM.Visible = true;
            btnCrossroad1LeftCrosswalkBOTTOM.Enabled = true;

            //Crossroad2
            //left crosswalk
            btnCrossroad2LeftCrosswalkTOP.Visible = true;
            btnCrossroad2LeftCrosswalkTOP.Enabled = true;
            btnCrossroad2LeftCrosswalkBOTTOM.Visible = true;
            btnCrossroad2LeftCrosswalkBOTTOM.Enabled = true;
            //right crosswalk
            btnCrossroad2RightCrosswalkTOP.Visible = true;
            btnCrossroad2RightCrosswalkTOP.Enabled = true;
            btnCrossroad2RightCrosswalkBOTTOM.Visible = true;
            btnCrossroad2RightCrosswalkBOTTOM.Enabled = true;

            //left T
            btnLeftTLeftCrosswalkTOP.Visible = false;
            btnLeftTLeftCrosswalkTOP.Enabled = false;
            btnLeftTLeftCrosswalkBOTTOM.Visible = false;
            btnLeftTLeftCrosswalkBOTTOM.Enabled = false;

            //right T
            btnRightTTopCrosswalkLEFT.Visible = false;
            btnRightTTopCrosswalkLEFT.Enabled = false;
            btnRightTTopCrosswalkRIGHT.Visible = false;
            btnRightTTopCrosswalkRIGHT.Enabled = false;

            #endregion

            //Traffic lines 
            #region Traffic lines

            //crossroad1
            #region crossroad1

            #region Corners

            //left top corner 
            //vertical line
            g.DrawLine(WhitePen, x + length * 3, y, x + length * 3, y + length * 3);
            /*
            g.DrawLine(WhitePen, x + length * 3, y, x + length * 3, y + length);
            g.DrawLine(WhitePen, x + length * 3, y + length, x + length * 3, y + length * 2);
            g.DrawLine(WhitePen, x + length * 3, y + length * 2, x + length * 3, y + length * 3);
            */

            //horizontal line
            g.DrawLine(WhitePen, x, y + length * 3, x + length * 3, y + length * 3);
            /*
            g.DrawLine(WhitePen, x, y + length * 3, x + length, y + length * 3);
            g.DrawLine(WhitePen, x + length, y + length * 3, x + length * 2, y + length * 3);
            g.DrawLine(WhitePen, x + length * 2, y + length * 3, x + length * 3, y + length * 3);
            */

            //left bottom corner
            //vertical line 
            g.DrawLine(WhitePen, x, y + length * 5, x + length * 3, y + length * 5);
            /*
            g.DrawLine(WhitePen, x, y + length * 5, x + length, y + length * 5);
            g.DrawLine(WhitePen, x + length, y + length * 5, x + length * 2, y + length * 5);
            g.DrawLine(WhitePen, x + length * 2, y + length * 5, x + length * 3, y + length * 5);
            */

            //horizontal line 
            g.DrawLine(WhitePen, x + length * 3, y + length * 5, x + length * 3, y + length * 9);
            /*
            g.DrawLine(WhitePen, x + length * 3, y + length * 5, x + length * 3, y + length * 6);
            g.DrawLine(WhitePen, x + length * 3, y + length * 6, x + length * 3, y + length * 7);
            g.DrawLine(WhitePen, x + length * 3, y + length * 7, x + length * 3, y + length * 8);
            g.DrawLine(WhitePen, x + length * 3, y + length * 8, x + length * 3, y + length * 9);
            */

            //right top corner
            //vertical line
            g.DrawLine(WhitePen, x + length * 5, y, x + length * 5, y + length * 3);
            /*
            g.DrawLine(WhitePen, x + length * 5, y, x + length * 5, y + length);
            g.DrawLine(WhitePen, x + length * 5, y + length, x + length * 5, y + length * 2);
            g.DrawLine(WhitePen, x + length * 5, y + length * 2, x + length * 5, y + length * 3);
            */

            //horizontal line 
            //crossroad - mid up - mid 
            g.DrawLine(WhitePen, x + length * 5, y + length * 3, x + length * 9, y + length * 3);
            /*
            g.DrawLine(WhitePen, x + length * 5, y + length * 3, x + length * 6, y + length * 3);
            g.DrawLine(WhitePen, x + length * 6, y + length * 3, x + length * 7, y + length * 3);
            g.DrawLine(WhitePen, x + length * 7, y + length * 3, x + length * 8, y + length * 3);
            g.DrawLine(WhitePen, x + length * 8, y + length * 3, x + length * 9, y + length * 3);
            */

            //right bottom corner 
            //vertical line
            g.DrawLine(WhitePen, x + length * 5, y + length * 5, x + length * 5, y + length * 9);
            /*
            g.DrawLine(WhitePen, x + length * 5, y + length * 5, x + length * 5, y + length * 6);
            g.DrawLine(WhitePen, x + length * 5, y + length * 6, x + length * 5, y + length * 7);
            g.DrawLine(WhitePen, x + length * 5, y + length * 7, x + length * 5, y + length * 8);
            g.DrawLine(WhitePen, x + length * 5, y + length * 8, x + length * 5, y + length * 9);
            */

            //horizontal line 
            g.DrawLine(WhitePen, x + length * 5, y + length * 5, x + length * 9, y + length * 5);
            /*
            g.DrawLine(WhitePen, x + length * 5, y + length * 5, x + length * 6, y + length * 5);
            g.DrawLine(WhitePen, x + length * 6, y + length * 5, x + length * 7, y + length * 5);
            g.DrawLine(WhitePen, x + length * 7, y + length * 5, x + length * 8, y + length * 5);
            g.DrawLine(WhitePen, x + length * 8, y + length * 5, x + length * 9, y + length * 5);
            */

            #endregion

            #region whitelines / traffic lines 

            //top 
            //white line - crossroad1 - top - internal
            //g.DrawLine(WhitePen, x + length * 3, y + length * 3, x + length * 4, y + length * 3); //left
            //g.DrawLine(WhitePen, x + length * 4, y + length * 3, x + length * 5, y + length * 3); //right
            //white line - crossroad1 - top - exteral
            g.DrawLine(WhitePen, x + length * 3, y + length * 2, x + length * 4, y + length * 2); //left
            //g.DrawLine(WhitePen, x + length * 4, y + length * 2, x + length * 5, y + length * 2); //right

            //bottom
            //white line - crossroad1 - bottom - internal
            //g.DrawLine(WhitePen, x + length * 3, y + length * 5, x + length * 4, y + length * 5); //left
            //g.DrawLine(WhitePen, x + length * 4, y + length * 5, x + length * 5, y + length * 5); //right
            //white line - crossroad1 - bottom - external
            //g.DrawLine(WhitePen, x + length * 3, y + length * 6, x + length * 4, y + length * 6); //left
            g.DrawLine(WhitePen, x + length * 4, y + length * 6, x + length * 5, y + length * 6); //right   

            //left 
            //white line - crossorad1 - left - internal
            //g.DrawLine(WhitePen, x + length * 3, y + length * 3, x + length * 3, y + length * 4); //up
            //g.DrawLine(WhitePen, x + length * 3, y + length * 4, x + length * 3, y + length * 5); //down
            //white line - crossroad1 - left - external
            //g.DrawLine(WhitePen, x + length * 2, y + length * 3, x + length * 2, y + length * 4); //up
            g.DrawLine(WhitePen, x + length * 2, y + length * 4, x + length * 2, y + length * 5); //down

            //right
            //white line - crossroad1 - right - internal
            //g.DrawLine(WhitePen, x + length * 5, y + length * 3, x + length * 5, y + length * 4); //up
            //g.DrawLine(WhitePen, x + length * 5, y + length * 4, x + length * 5, y + length * 5); //down
            //white line - crossroad1 - right - external
            g.DrawLine(WhitePen, x + length * 6, y + length * 3, x + length * 6, y + length * 4); //up
            //g.DrawLine(WhitePen, x + length * 6, y + length * 4, x + length * 6, y + length * 5); //down

            #endregion

            #endregion

            //crossorad2
            #region crossroad2 

            #region Corners
            //left top corner 
            //vertical line
            g.DrawLine(WhitePen, x + length * 9, y, x + length * 9, y + length * 3);
            /*
            g.DrawLine(WhitePen, x + length * 9, y, x + length * 9, y + length);
            g.DrawLine(WhitePen, x + length * 9, y + length, x + length * 9, y + length * 2);
            g.DrawLine(WhitePen, x + length * 9, y + length * 2, x + length * 9, y + length * 3);
            */

            //right top corner
            //vertical line
            g.DrawLine(WhitePen, x + length * 11, y, x + length * 11, y + length * 3);
            /*
            g.DrawLine(WhitePen, x + length * 11, y, x + length * 11, y + length);
            g.DrawLine(WhitePen, x + length * 11, y + length, x + length * 11, y + length * 2);
            g.DrawLine(WhitePen, x + length * 11, y + length * 2, x + length * 11, y + length * 3);
            */
            //horizontal line
            g.DrawLine(WhitePen, x + length * 11, y + length * 3, x + length * 14, y + length * 3);
            /*
            g.DrawLine(WhitePen, x + length * 11, y + length * 3, x + length * 12, y + length * 3);
            g.DrawLine(WhitePen, x + length * 12, y + length * 3, x + length * 13, y + length * 3);
            g.DrawLine(WhitePen, x + length * 13, y + length * 3, x + length * 14, y + length * 3);
            */

            //left bottom corner
            //vertical line 
            g.DrawLine(WhitePen, x + length * 9, y + length * 5, x + length * 9, y + length * 9);
            /*
            g.DrawLine(WhitePen, x + length * 9, y + length * 5, x + length * 9, y + length * 6);
            g.DrawLine(WhitePen, x + length * 9, y + length * 6, x + length * 9, y + length * 7);
            g.DrawLine(WhitePen, x + length * 9, y + length * 7, x + length * 9, y + length * 8);
            g.DrawLine(WhitePen, x + length * 9, y + length * 8, x + length * 9, y + length * 9);
            */

            //right bottom corner 
            //vertical line
            g.DrawLine(WhitePen, x + length * 11, y + length * 5, x + length * 11, y + length * 9);
            /*
            g.DrawLine(WhitePen, x + length * 11, y + length * 5, x + length * 11, y + length * 6);
            g.DrawLine(WhitePen, x + length * 11, y + length * 6, x + length * 11, y + length * 7);
            g.DrawLine(WhitePen, x + length * 11, y + length * 7, x + length * 11, y + length * 8);
            g.DrawLine(WhitePen, x + length * 11, y + length * 8, x + length * 11, y + length * 9);
            */
            //horizontal line
            g.DrawLine(WhitePen, x + length * 11, y + length * 5, x + length * 14, y + length * 5);
            /*
            g.DrawLine(WhitePen, x + length * 11, y + length * 5, x + length * 12, y + length * 5);
            g.DrawLine(WhitePen, x + length * 12, y + length * 5, x + length * 13, y + length * 5);
            g.DrawLine(WhitePen, x + length * 13, y + length * 5, x + length * 14, y + length * 5);
            */

            #endregion

            #region whitelines / traffic lines 

            //top
            //white line - crossorad 2 - top - external
            g.DrawLine(WhitePen, x + length * 9, y + length * 2, x + length * 10, y + length * 2); //left
            //g.DrawLine(WhitePen, x + length * 10, y + length * 2, x + length * 11, y + length * 2); //right
            //white line - crossorad 2 - top - internal
            //g.DrawLine(WhitePen, x + length * 9, y + length * 3, x + length * 10, y + length * 3); //left
            //g.DrawLine(WhitePen, x + length * 10, y + length * 3, x + length * 11, y + length * 3); //right

            //bottom
            //white line - crossorad 2 - bottom - internal
            //g.DrawLine(WhitePen, x + length * 9, y + length * 5, x + length * 10, y + length * 5); //left
            //g.DrawLine(WhitePen, x + length * 10, y + length * 5, x + length * 11, y + length * 5); //right
            //white line - crossorad 2 - bottom - external
            //g.DrawLine(WhitePen, x + length * 9, y + length * 6, x + length * 10, y + length * 6); //left
            g.DrawLine(WhitePen, x + length * 10, y + length * 6, x + length * 11, y + length * 6); //right

            //left 
            //white line - crossroad2 - left - exteranl
            //g.DrawLine(WhitePen, x + length * 8, y + length * 3, x + length * 8, y + length * 4); //up
            g.DrawLine(WhitePen, x + length * 8, y + length * 4, x + length * 8, y + length * 5); //down
            //white line - crossroad2 - left - internal
            //g.DrawLine(WhitePen, x + length * 9, y + length * 3, x + length * 9, y + length * 4); //up
            //g.DrawLine(WhitePen, x + length * 9, y + length * 4, x + length * 9, y + length * 5); //down 

            //right
            //white line - crossorad 2 - right - internal
            //g.DrawLine(WhitePen, x + length * 11, y + length * 3, x + length * 11, y + length * 4); //up
            //g.DrawLine(WhitePen, x + length * 11, y + length * 4, x + length * 11, y + length * 5); //down
            //white line - crossorad 2 - right - external
            g.DrawLine(WhitePen, x + length * 12, y + length * 3, x + length * 12, y + length * 4); //up
            //g.DrawLine(WhitePen, x + length * 12, y + length * 4, x + length * 12, y + length * 5); //down

            #endregion

            #endregion

            #endregion

            //Center lines
            #region Center lines

            //vertical center line - left
            //top
            g.DrawLine(WhitePen, x + length * 4, y, x + length * 4, y + length_interrupted);
            g.DrawLine(WhitePen, x + length * 4, y + length_interrupted * 2, x + length * 4, y + length_interrupted * 3);
            //g.DrawLine(WhitePen, x + length * 4, y + length_interrupted * 4, x + length * 4, y + length_interrupted * 5);
            //g.DrawLine(WhitePen, x + length * 4, y + length_interrupted * 6, x + length * 4, y + length_interrupted * 7);
            g.DrawLine(WhitePen, x + length * 4, y + length, x + length * 4, y + length * 2);
            //bottom
            g.DrawLine(WhitePen, x + length * 4, y + length * 6, x + length * 4, y + length * 8);

            //vertical center line - right
            //top
            g.DrawLine(WhitePen, x + length * 10, y, x + length * 10, y + length_interrupted);
            g.DrawLine(WhitePen, x + length * 10, y + length_interrupted * 2, x + length * 10, y + length_interrupted * 3);
            //g.DrawLine(WhitePen, x + length * 4, y + length_interrupted * 4, x + length * 4, y + length_interrupted * 5);
            //g.DrawLine(WhitePen, x + length * 4, y + length_interrupted * 6, x + length * 4, y + length_interrupted * 7);
            g.DrawLine(WhitePen, x + length * 10, y + length, x + length * 10, y + length * 2);
            //bottom
            g.DrawLine(WhitePen, x + length * 10, y + length * 6, x + length * 10, y + length * 8);

            //horizontal center line - top 
            g.DrawLine(WhitePen, x, y + length * 4, x + length_interrupted, y + length * 4);
            g.DrawLine(WhitePen, x + length_interrupted * 2, y + length * 4, x + length_interrupted * 3, y + length * 4);
            g.DrawLine(WhitePen, x + length, y + length * 4, x + length * 2, y + length * 4);
            g.DrawLine(WhitePen, x + length * 6, y + length * 4, x + length * 8, y + length * 4);
            g.DrawLine(WhitePen, x + length * 12, y + length * 4, x + length * 13, y + length * 4);
            g.DrawLine(WhitePen, x + length * 13 + length_interrupted, y + length * 4, x + length * 13 + length_interrupted * 2, y + length * 4);
            g.DrawLine(WhitePen, x + length * 13 + length_interrupted * 3, y + length * 4, x + length * 13 + length_interrupted * 4, y + length * 4);
            //g.DrawLine(WhitePen, x + length_interrupted * 4, y + length * 3, x + length_interrupted * 5, y + length * 3);
            //g.DrawLine(WhitePen, x + length_interrupted * 6, y + length * 3, x + length_interrupted * 7, y + length * 3);
                      
            #endregion
                        
            //TrafficLights
            #region TrafficLights

            //crossorad1
            #region Crossroad1
            //crossroad1 - trafficlight - top
            g.DrawRectangle(WhitePen, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_height);
            g.FillEllipse(red, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            //crossroad1 - trafficlight - left
            g.DrawRectangle(WhitePen, x + length + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length + 3 * FreeSpace + TrafficLights_width * 2, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length + 3 * FreeSpace + TrafficLights_width, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);

            //crossroad1 - trafficlight - right
            g.DrawRectangle(WhitePen, x + length * 6 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length * 6 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 6 + FreeSpace + TrafficLights_width, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 6 + FreeSpace + TrafficLights_width * 2, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            //crossroad1 - trafficlight - bottom
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_height);
            g.FillEllipse(red, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);

            #endregion

            //crossroad2
            #region Crossroad2
            //crossroad2 - trafficlight - top
            g.DrawRectangle(WhitePen, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_height);
            g.FillEllipse(red, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            //crossroad2 - trafficlight - left
            g.DrawRectangle(WhitePen, x + length * 7 + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length * 7 + 3 * FreeSpace + TrafficLights_width * 2, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 7 + 3 * FreeSpace + TrafficLights_width, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 7 + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);

            //crossroad2 - trafficlight - right
            g.DrawRectangle(WhitePen, x + length * 12 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length * 12 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 12 + FreeSpace + TrafficLights_width, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 12 + FreeSpace + TrafficLights_width * 2, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            //crossroad2 - trafficlight - bottom
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_height);
            g.FillEllipse(red, x + length * 11 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 11 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 11 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);

            #endregion
                        
            #endregion
            
            //Crosswalks
            #region Crosswalks

            //crossroad1 
            #region Crossorad1
            //crossroad1 - crosswalk - top
            g.DrawRectangle(WhitePen, x + length * 3 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 3 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 3 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 3 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 3 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 3 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 4 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 4 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 4 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            
            //crossroad1 - crosswalk - left
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 3 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 3 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 3 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 4 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 4 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 4 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);

            //crossroad1 - crosswalk - right
            /*
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 3 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 3 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 3 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 4 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 4 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 4 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            */

            //crossroad1 - crosswalk - bottom
            /*
            g.DrawRectangle(WhitePen, x + length * 3 + FreeSpace, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 3 + 2 * FreeSpace + crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 3 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + FreeSpace, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + 2 * FreeSpace + crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            */

            #endregion

            //crossroad2 
            #region Crossorad 2

            //crossroad2 - crosswalk - top
            /*
            g.DrawRectangle(WhitePen, x + length * 9 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 9 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 9 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            */

            //crossroad2 - crosswalk - bottom
            /*
            g.DrawRectangle(WhitePen, x + length * 9 + FreeSpace, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 9 + 2 * FreeSpace + crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 9 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + FreeSpace, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + 2 * FreeSpace + crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            */

            //crossroad2 - crosswalk - left
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 3 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 3 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 3 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 4 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 4 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 4 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);

            //crossroad2 - crosswalk - right
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 3 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 3 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 3 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 4 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 4 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 4 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);

            #endregion
                        
            #endregion

            //CrosswalkLights
            #region CrosswalkLights 

            //crossroad1
            #region Crossroad1
            //top
            //crossroad1 - CrosswalkLights - top - left
            g.DrawRectangle(WhitePen, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad1 - CrosswalkLights - top - right
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //bottom
            //crossroad1 - CrosswalkLights - bottom - left
            /*
            g.DrawRectangle(WhitePen, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            */
            //crossroad1 - CrosswalkLights - bottom - right
            /*
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            */

            //left
            //crossroad1 - CrosswalkLights - left - top
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad1 - CrosswalkLights - left - bottom
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //right
            //crossroad1 - CrosswalkLights - right - top
            /*
            g.DrawRectangle(WhitePen, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            */
            //crossroad1 - CrosswalkLights - right - bottom
            /*
            g.DrawRectangle(WhitePen, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            */

            #endregion

            //crossroad2
            #region Crossroad2
            //top
            //crossroad2 - CrosswalkLights - top - left
            /*
            g.DrawRectangle(WhitePen, x + length * 8 + crosswalk_width + 3 * FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 8 + crosswalk_width + 3 * FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 8 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            */
            //crossroad2 - CrosswalkLights - top - right
            /*
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 11 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 11 + FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            */

            //bottom
            //crossroad2 - CrosswalkLights - bottom - left
            /*
            g.DrawRectangle(WhitePen, x + length * 8 + crosswalk_width + 3 * FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 8 + crosswalk_width + 3 * FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 8 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            */
            //crossroad2 - CrosswalkLights - bottom - right
            /*
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 11 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 11 + FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            */

            //left
            //crossroad2 - CrosswalkLights - left - top
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 8 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 8 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad2 - CrosswalkLights - left - bottom
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 8 + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 8 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //right
            //crossroad2 - CrosswalkLights - right - top
            g.DrawRectangle(WhitePen, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad2 - CrosswalkLights - right - bottom
            g.DrawRectangle(WhitePen, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            
            #endregion
                        
            #endregion

        }

        //Crossroad1 + Crossorad2 + Left T
        public void CrossroadExtension2()
        {
            var g = this.CreateGraphics();

            //background
            //g.Clear(Color.Black);

            //pen color
            Pen BlackPen = new Pen(Color.Black, 2);
            Pen WhitePen = new Pen(Color.White, 2);
            Pen RedPen = new Pen(Color.Red, 2);
            Pen GreenPen = new Pen(Color.Green, 2);
            Pen YellowPen = new Pen(Color.Yellow, 2);

            this.Refresh();

            //BTN allow
            #region BTN alow 
            //Crossroad1
            //top crosswalk
            btnCrossroad1TopCrosswalkLEFT.Visible = true;
            btnCrossroad1TopCrosswalkLEFT.Enabled = true;
            btnCrossroad1TopCrosswalkRIGHT.Visible = true;
            btnCrossroad1TopCrosswalkRIGHT.Enabled = true;
            //left crosswalk
            btnCrossroad1LeftCrosswalkTOP.Visible = true;
            btnCrossroad1LeftCrosswalkTOP.Enabled = true;
            btnCrossroad1LeftCrosswalkBOTTOM.Visible = true;
            btnCrossroad1LeftCrosswalkBOTTOM.Enabled = true;

            //Crossroad2
            //left crosswalk
            btnCrossroad2LeftCrosswalkTOP.Visible = true;
            btnCrossroad2LeftCrosswalkTOP.Enabled = true;
            btnCrossroad2LeftCrosswalkBOTTOM.Visible = true;
            btnCrossroad2LeftCrosswalkBOTTOM.Enabled = true;
            //right crosswalk
            btnCrossroad2RightCrosswalkTOP.Visible = true;
            btnCrossroad2RightCrosswalkTOP.Enabled = true;
            btnCrossroad2RightCrosswalkBOTTOM.Visible = true;
            btnCrossroad2RightCrosswalkBOTTOM.Enabled = true;

            //Left T
            //left crosswalk
            btnLeftTLeftCrosswalkTOP.Visible = true;
            btnLeftTLeftCrosswalkTOP.Enabled = true;
            btnLeftTLeftCrosswalkBOTTOM.Visible = true;
            btnLeftTLeftCrosswalkBOTTOM.Enabled = true;
                        
            //right T
            btnRightTTopCrosswalkLEFT.Visible = false;
            btnRightTTopCrosswalkLEFT.Enabled = false;
            btnRightTTopCrosswalkRIGHT.Visible = false;
            btnRightTTopCrosswalkRIGHT.Enabled = false;

            #endregion

            //Traffic lines 
            #region Traffic lines

            //crossroad1
            #region crossroad1

            #region Corners

            //left top corner 
            //vertical line
            g.DrawLine(WhitePen, x + length * 3, y, x + length * 3, y + length * 3);
            /*
            g.DrawLine(WhitePen, x + length * 3, y, x + length * 3, y + length);
            g.DrawLine(WhitePen, x + length * 3, y + length, x + length * 3, y + length * 2);
            g.DrawLine(WhitePen, x + length * 3, y + length * 2, x + length * 3, y + length * 3);
            */

            //horizontal line
            g.DrawLine(WhitePen, x, y + length * 3, x + length * 3, y + length * 3);
            /*
            g.DrawLine(WhitePen, x, y + length * 3, x + length, y + length * 3);
            g.DrawLine(WhitePen, x + length, y + length * 3, x + length * 2, y + length * 3);
            g.DrawLine(WhitePen, x + length * 2, y + length * 3, x + length * 3, y + length * 3);
            */

            //left bottom corner
            //vertical line 
            g.DrawLine(WhitePen, x, y + length * 5, x + length * 3, y + length * 5);
            /*
            g.DrawLine(WhitePen, x, y + length * 5, x + length, y + length * 5);
            g.DrawLine(WhitePen, x + length, y + length * 5, x + length * 2, y + length * 5);
            g.DrawLine(WhitePen, x + length * 2, y + length * 5, x + length * 3, y + length * 5);
            */

            //horizontal line 
            g.DrawLine(WhitePen, x + length * 3, y + length * 5, x + length * 3, y + length * 9);
            /*
            g.DrawLine(WhitePen, x + length * 3, y + length * 5, x + length * 3, y + length * 6);
            g.DrawLine(WhitePen, x + length * 3, y + length * 6, x + length * 3, y + length * 7);
            g.DrawLine(WhitePen, x + length * 3, y + length * 7, x + length * 3, y + length * 8);
            g.DrawLine(WhitePen, x + length * 3, y + length * 8, x + length * 3, y + length * 9);
            */

            //right top corner
            //vertical line
            g.DrawLine(WhitePen, x + length * 5, y, x + length * 5, y + length * 3);
            /*
            g.DrawLine(WhitePen, x + length * 5, y, x + length * 5, y + length);
            g.DrawLine(WhitePen, x + length * 5, y + length, x + length * 5, y + length * 2);
            g.DrawLine(WhitePen, x + length * 5, y + length * 2, x + length * 5, y + length * 3);
            */

            //horizontal line 
            //crossroad - mid up - mid 
            g.DrawLine(WhitePen, x + length * 5, y + length * 3, x + length * 9, y + length * 3);
            /*
            g.DrawLine(WhitePen, x + length * 5, y + length * 3, x + length * 6, y + length * 3);
            g.DrawLine(WhitePen, x + length * 6, y + length * 3, x + length * 7, y + length * 3);
            g.DrawLine(WhitePen, x + length * 7, y + length * 3, x + length * 8, y + length * 3);
            g.DrawLine(WhitePen, x + length * 8, y + length * 3, x + length * 9, y + length * 3);
            */

            //right bottom corner 
            //vertical line
            g.DrawLine(WhitePen, x + length * 5, y + length * 5, x + length * 5, y + length * 9);
            /*
            g.DrawLine(WhitePen, x + length * 5, y + length * 5, x + length * 5, y + length * 6);
            g.DrawLine(WhitePen, x + length * 5, y + length * 6, x + length * 5, y + length * 7);
            g.DrawLine(WhitePen, x + length * 5, y + length * 7, x + length * 5, y + length * 8);
            g.DrawLine(WhitePen, x + length * 5, y + length * 8, x + length * 5, y + length * 9);
            */

            //horizontal line 
            g.DrawLine(WhitePen, x + length * 5, y + length * 5, x + length * 9, y + length * 5);
            /*
            g.DrawLine(WhitePen, x + length * 5, y + length * 5, x + length * 6, y + length * 5);
            g.DrawLine(WhitePen, x + length * 6, y + length * 5, x + length * 7, y + length * 5);
            g.DrawLine(WhitePen, x + length * 7, y + length * 5, x + length * 8, y + length * 5);
            g.DrawLine(WhitePen, x + length * 8, y + length * 5, x + length * 9, y + length * 5);
            */

            #endregion

            #region whitelines / traffic lines 

            //top 
            //white line - crossroad1 - top - internal
            //g.DrawLine(WhitePen, x + length * 3, y + length * 3, x + length * 4, y + length * 3); //left
            //g.DrawLine(WhitePen, x + length * 4, y + length * 3, x + length * 5, y + length * 3); //right
            //white line - crossroad1 - top - exteral
            g.DrawLine(WhitePen, x + length * 3, y + length * 2, x + length * 4, y + length * 2); //left
            //g.DrawLine(WhitePen, x + length * 4, y + length * 2, x + length * 5, y + length * 2); //right

            //bottom
            //white line - crossroad1 - bottom - internal
            //g.DrawLine(WhitePen, x + length * 3, y + length * 5, x + length * 4, y + length * 5); //left
            //g.DrawLine(WhitePen, x + length * 4, y + length * 5, x + length * 5, y + length * 5); //right
            //white line - crossroad1 - bottom - external
            //g.DrawLine(WhitePen, x + length * 3, y + length * 6, x + length * 4, y + length * 6); //left
            g.DrawLine(WhitePen, x + length * 4, y + length * 6, x + length * 5, y + length * 6); //right   

            //left 
            //white line - crossorad1 - left - internal
            //g.DrawLine(WhitePen, x + length * 3, y + length * 3, x + length * 3, y + length * 4); //up
            //g.DrawLine(WhitePen, x + length * 3, y + length * 4, x + length * 3, y + length * 5); //down
            //white line - crossroad1 - left - external
            //g.DrawLine(WhitePen, x + length * 2, y + length * 3, x + length * 2, y + length * 4); //up
            g.DrawLine(WhitePen, x + length * 2, y + length * 4, x + length * 2, y + length * 5); //down

            //right
            //white line - crossroad1 - right - internal
            //g.DrawLine(WhitePen, x + length * 5, y + length * 3, x + length * 5, y + length * 4); //up
            //g.DrawLine(WhitePen, x + length * 5, y + length * 4, x + length * 5, y + length * 5); //down
            //white line - crossroad1 - right - external
            g.DrawLine(WhitePen, x + length * 6, y + length * 3, x + length * 6, y + length * 4); //up
            //g.DrawLine(WhitePen, x + length * 6, y + length * 4, x + length * 6, y + length * 5); //down

            #endregion

            #endregion

            //crossorad2
            #region crossroad2 

            #region Corners
            //left top corner 
            //vertical line
            g.DrawLine(WhitePen, x + length * 9, y, x + length * 9, y + length * 3);
            /*
            g.DrawLine(WhitePen, x + length * 9, y, x + length * 9, y + length);
            g.DrawLine(WhitePen, x + length * 9, y + length, x + length * 9, y + length * 2);
            g.DrawLine(WhitePen, x + length * 9, y + length * 2, x + length * 9, y + length * 3);
            */

            //right top corner
            //vertical line
            g.DrawLine(WhitePen, x + length * 11, y, x + length * 11, y + length * 3);
            /*
            g.DrawLine(WhitePen, x + length * 11, y, x + length * 11, y + length);
            g.DrawLine(WhitePen, x + length * 11, y + length, x + length * 11, y + length * 2);
            g.DrawLine(WhitePen, x + length * 11, y + length * 2, x + length * 11, y + length * 3);
            */
            //horizontal line
            g.DrawLine(WhitePen, x + length * 11, y + length * 3, x + length * 14, y + length * 3);
            /*
            g.DrawLine(WhitePen, x + length * 11, y + length * 3, x + length * 12, y + length * 3);
            g.DrawLine(WhitePen, x + length * 12, y + length * 3, x + length * 13, y + length * 3);
            g.DrawLine(WhitePen, x + length * 13, y + length * 3, x + length * 14, y + length * 3);
            */

            //left bottom corner
            //vertical line 
            g.DrawLine(WhitePen, x + length * 9, y + length * 5, x + length * 9, y + length * 9);
            /*
            g.DrawLine(WhitePen, x + length * 9, y + length * 5, x + length * 9, y + length * 6);
            g.DrawLine(WhitePen, x + length * 9, y + length * 6, x + length * 9, y + length * 7);
            g.DrawLine(WhitePen, x + length * 9, y + length * 7, x + length * 9, y + length * 8);
            g.DrawLine(WhitePen, x + length * 9, y + length * 8, x + length * 9, y + length * 9);
            */

            //right bottom corner 
            //vertical line
            g.DrawLine(WhitePen, x + length * 11, y + length * 5, x + length * 11, y + length * 9);
            /*
            g.DrawLine(WhitePen, x + length * 11, y + length * 5, x + length * 11, y + length * 6);
            g.DrawLine(WhitePen, x + length * 11, y + length * 6, x + length * 11, y + length * 7);
            g.DrawLine(WhitePen, x + length * 11, y + length * 7, x + length * 11, y + length * 8);
            g.DrawLine(WhitePen, x + length * 11, y + length * 8, x + length * 11, y + length * 9);
            */
            //horizontal line
            g.DrawLine(WhitePen, x + length * 11, y + length * 5, x + length * 14, y + length * 5);
            /*
            g.DrawLine(WhitePen, x + length * 11, y + length * 5, x + length * 12, y + length * 5);
            g.DrawLine(WhitePen, x + length * 12, y + length * 5, x + length * 13, y + length * 5);
            g.DrawLine(WhitePen, x + length * 13, y + length * 5, x + length * 14, y + length * 5);
            */

            #endregion

            #region whitelines / traffic lines 

            //top
            //white line - crossorad 2 - top - external
            g.DrawLine(WhitePen, x + length * 9, y + length * 2, x + length * 10, y + length * 2); //left
            //g.DrawLine(WhitePen, x + length * 10, y + length * 2, x + length * 11, y + length * 2); //right
            //white line - crossorad 2 - top - internal
            //g.DrawLine(WhitePen, x + length * 9, y + length * 3, x + length * 10, y + length * 3); //left
            //g.DrawLine(WhitePen, x + length * 10, y + length * 3, x + length * 11, y + length * 3); //right

            //bottom
            //white line - crossorad 2 - bottom - internal
            //g.DrawLine(WhitePen, x + length * 9, y + length * 5, x + length * 10, y + length * 5); //left
            //g.DrawLine(WhitePen, x + length * 10, y + length * 5, x + length * 11, y + length * 5); //right
            //white line - crossorad 2 - bottom - external
            //g.DrawLine(WhitePen, x + length * 9, y + length * 6, x + length * 10, y + length * 6); //left
            g.DrawLine(WhitePen, x + length * 10, y + length * 6, x + length * 11, y + length * 6); //right

            //left 
            //white line - crossroad2 - left - exteranl
            //g.DrawLine(WhitePen, x + length * 8, y + length * 3, x + length * 8, y + length * 4); //up
            g.DrawLine(WhitePen, x + length * 8, y + length * 4, x + length * 8, y + length * 5); //down
            //white line - crossroad2 - left - internal
            //g.DrawLine(WhitePen, x + length * 9, y + length * 3, x + length * 9, y + length * 4); //up
            //g.DrawLine(WhitePen, x + length * 9, y + length * 4, x + length * 9, y + length * 5); //down 

            //right
            //white line - crossorad 2 - right - internal
            //g.DrawLine(WhitePen, x + length * 11, y + length * 3, x + length * 11, y + length * 4); //up
            //g.DrawLine(WhitePen, x + length * 11, y + length * 4, x + length * 11, y + length * 5); //down
            //white line - crossorad 2 - right - external
            g.DrawLine(WhitePen, x + length * 12, y + length * 3, x + length * 12, y + length * 4); //up
            //g.DrawLine(WhitePen, x + length * 12, y + length * 4, x + length * 12, y + length * 5); //down

            #endregion

            #endregion

            //Left T
            #region Left T

            #region Corners
            //left top corner 
            //horizontal line 
            g.DrawLine(WhitePen, x, y + length * 9, x + length * 3, y + length * 9);
            /*
            g.DrawLine(WhitePen, x, y + length * 9, x + length, y + length * 9);
            g.DrawLine(WhitePen, x + length, y + length * 9, x + length * 2, y + length * 9);
            g.DrawLine(WhitePen, x + length * 2, y + length * 9, x + length * 3, y + length * 9);
            */

            //right top corner
            //horizontal line 
            g.DrawLine(WhitePen, x + length * 5, y + length * 9, x + length * 9, y + length * 9);
            /*
            g.DrawLine(WhitePen, x + length * 5, y + length * 9, x + length * 6, y + length * 9);
            g.DrawLine(WhitePen, x + length * 6, y + length * 9, x + length * 7, y + length * 9);
            g.DrawLine(WhitePen, x + length * 7, y + length * 9, x + length * 8, y + length * 9);
            g.DrawLine(WhitePen, x + length * 8, y + length * 9, x + length * 9, y + length * 9);
            */

            //bottom line
            g.DrawLine(WhitePen, x, y + length * 11, x + length * 9, y + length * 11);

            #endregion

            #region whitelines / traffic lines 

            //top
            g.DrawLine(WhitePen, x + length * 3, y + length * 8, x + length * 4, y + length * 8);
            
            //left
            g.DrawLine(WhitePen, x + length * 2, y + length * 10, x + length * 2, y + length * 11);

            //right
            g.DrawLine(WhitePen, x + length * 6, y + length * 9, x + length * 6, y + length * 10);

            #endregion

            #endregion
                                    
            #endregion

            //Center lines
            #region Center lines

            //vertical center line - left
            //top
            g.DrawLine(WhitePen, x + length * 4, y, x + length * 4, y + length_interrupted);
            g.DrawLine(WhitePen, x + length * 4, y + length_interrupted * 2, x + length * 4, y + length_interrupted * 3);
            //g.DrawLine(WhitePen, x + length * 4, y + length_interrupted * 4, x + length * 4, y + length_interrupted * 5);
            //g.DrawLine(WhitePen, x + length * 4, y + length_interrupted * 6, x + length * 4, y + length_interrupted * 7);
            g.DrawLine(WhitePen, x + length * 4, y + length, x + length * 4, y + length * 2);
            //bottom
            g.DrawLine(WhitePen, x + length * 4, y + length * 6, x + length * 4, y + length * 8);

            //vertical center line - right
            //top
            g.DrawLine(WhitePen, x + length * 10, y, x + length * 10, y + length_interrupted);
            g.DrawLine(WhitePen, x + length * 10, y + length_interrupted * 2, x + length * 10, y + length_interrupted * 3);
            //g.DrawLine(WhitePen, x + length * 4, y + length_interrupted * 4, x + length * 4, y + length_interrupted * 5);
            //g.DrawLine(WhitePen, x + length * 4, y + length_interrupted * 6, x + length * 4, y + length_interrupted * 7);
            g.DrawLine(WhitePen, x + length * 10, y + length, x + length * 10, y + length * 2);
            //bottom
            g.DrawLine(WhitePen, x + length * 10, y + length * 6, x + length * 10, y + length * 8);

            //horizontal center line - top 
            g.DrawLine(WhitePen, x, y + length * 4, x + length_interrupted, y + length * 4);
            g.DrawLine(WhitePen, x + length_interrupted * 2, y + length * 4, x + length_interrupted * 3, y + length * 4);
            g.DrawLine(WhitePen, x + length, y + length * 4, x + length * 2, y + length * 4);
            g.DrawLine(WhitePen, x + length * 6, y + length * 4, x + length * 8, y + length * 4);
            g.DrawLine(WhitePen, x + length * 12, y + length * 4, x + length * 13, y + length * 4);
            g.DrawLine(WhitePen, x + length * 13 + length_interrupted, y + length * 4, x + length * 13 + length_interrupted * 2, y + length * 4);
            g.DrawLine(WhitePen, x + length * 13 + length_interrupted * 3, y + length * 4, x + length * 13 + length_interrupted * 4, y + length * 4);
            //g.DrawLine(WhitePen, x + length_interrupted * 4, y + length * 3, x + length_interrupted * 5, y + length * 3);
            //g.DrawLine(WhitePen, x + length_interrupted * 6, y + length * 3, x + length_interrupted * 7, y + length * 3);
            g.DrawLine(WhitePen, x + length, y + length * 10, x + length * 2, y + length * 10);
            g.DrawLine(WhitePen, x + length * 6, y + length * 10, x + length * 8, y + length * 10);
            
            //horizontal center line - bottom 
            //left T
            g.DrawLine(WhitePen, x, y + length * 10, x + length_interrupted, y + length * 10);
            g.DrawLine(WhitePen, x + length_interrupted * 2, y + length * 10, x + length_interrupted * 3, y + length * 10);
            g.DrawLine(WhitePen, x + length, y + length * 10, x + length * 2, y + length * 10);
            g.DrawLine(WhitePen, x + length * 6, y + length * 10, x + length * 8, y + length * 10);

            #endregion
                        
            //TrafficLights
            #region TrafficLights

            //crossorad1
            #region Crossroad1
            //crossroad1 - trafficlight - top
            g.DrawRectangle(WhitePen, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_height);
            g.FillEllipse(red, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            //crossroad1 - trafficlight - left
            g.DrawRectangle(WhitePen, x + length + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length + 3 * FreeSpace + TrafficLights_width * 2, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length + 3 * FreeSpace + TrafficLights_width, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);

            //crossroad1 - trafficlight - right
            g.DrawRectangle(WhitePen, x + length * 6 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length * 6 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 6 + FreeSpace + TrafficLights_width, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 6 + FreeSpace + TrafficLights_width * 2, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            //crossroad1 - trafficlight - bottom
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_height);
            g.FillEllipse(red, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);

            #endregion

            //crossroad2
            #region Crossroad2
            //crossroad2 - trafficlight - top
            g.DrawRectangle(WhitePen, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_height);
            g.FillEllipse(red, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            //crossroad2 - trafficlight - left
            g.DrawRectangle(WhitePen, x + length * 7 + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length * 7 + 3 * FreeSpace + TrafficLights_width * 2, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 7 + 3 * FreeSpace + TrafficLights_width, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 7 + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);

            //crossroad2 - trafficlight - right
            g.DrawRectangle(WhitePen, x + length * 12 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length * 12 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 12 + FreeSpace + TrafficLights_width, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 12 + FreeSpace + TrafficLights_width * 2, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            //crossroad2 - trafficlight - bottom
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_height);
            g.FillEllipse(red, x + length * 11 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 11 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 11 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);

            #endregion

            //left T
            #region Left T
            //left T - trafficlight - top
            g.DrawRectangle(WhitePen, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace, TrafficLights_width, TrafficLights_height);
            g.FillEllipse(red, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            //left T - trafficlight - left
            g.DrawRectangle(WhitePen, x + length + 3 * FreeSpace, y + length * 11 + FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length + 3 * FreeSpace + TrafficLights_width * 2, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length + 3 * FreeSpace + TrafficLights_width, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length + 3 * FreeSpace, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);

            //left T - trafficlight - right
            g.DrawRectangle(WhitePen, x + length * 6 + FreeSpace, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length * 6 + FreeSpace, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 6 + FreeSpace + TrafficLights_width, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 6 + FreeSpace + TrafficLights_width * 2, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            #endregion

            #endregion

            //Crosswalks
            #region Crosswalks

            //crossroad1 
            #region Crossroad1 

            //crossroad1 - crosswalk - top
            g.DrawRectangle(WhitePen, x + length * 3 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 3 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 3 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 3 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 3 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 3 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 4 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 4 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 4 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);

            //crossroad1 - crosswalk - left
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 3 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 3 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 3 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 4 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 4 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 4 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);

            //crossroad1 - crosswalk - right
            /*
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 3 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 3 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 3 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 4 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 4 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 4 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            */

            //crossroad1 - crosswalk - bottom
            /*
            g.DrawRectangle(WhitePen, x + length * 3 + FreeSpace, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 3 + 2 * FreeSpace + crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 3 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + FreeSpace, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + 2 * FreeSpace + crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            */

            #endregion

            //crossroad2
            #region Crossorad2

            //crossroad2 - crosswalk - top
            /*
            g.DrawRectangle(WhitePen, x + length * 9 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 9 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 9 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            */

            //crossroad2 - crosswalk - bottom
            /*
            g.DrawRectangle(WhitePen, x + length * 9 + FreeSpace, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 9 + 2 * FreeSpace + crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 9 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + FreeSpace, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + 2 * FreeSpace + crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            */

            //crossroad2 - crosswalk - left
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 3 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 3 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 3 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 4 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 4 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 4 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);

            //crossroad2 - crosswalk - right
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 3 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 3 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 3 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 4 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 4 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 4 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);

            #endregion

            //left T
            #region Left T

            //left T - crosswalk - top
            /*
            g.DrawRectangle(WhitePen, x + length * 3 + FreeSpace, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 3 + 2 * FreeSpace + crosswalk_width, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 3 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + FreeSpace, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + 2 * FreeSpace + crosswalk_width, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);
            */
            
            //left T - crosswalk - left
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 9 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 9 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 9 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 10 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 10 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 10 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);

            //left T - crosswalk - right
            /*
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 9 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 9 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 9 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 10 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 10 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 10 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            */

            #endregion

            #endregion

            //CrosswalkLights
            #region CrosswalkLights 

            //crossroad1
            #region Crossroad1
            //top
            //crossroad1 - CrosswalkLights - top - left
            g.DrawRectangle(WhitePen, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad1 - CrosswalkLights - top - right
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //bottom
            //crossroad1 - CrosswalkLights - bottom - left
            /*
            g.DrawRectangle(WhitePen, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            */
            //crossroad1 - CrosswalkLights - bottom - right
            /*
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            */

            //left
            //crossroad1 - CrosswalkLights - left - top
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad1 - CrosswalkLights - left - bottom
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //right
            //crossroad1 - CrosswalkLights - right - top
            /*
            g.DrawRectangle(WhitePen, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            */
            //crossroad1 - CrosswalkLights - right - bottom
            /*
            g.DrawRectangle(WhitePen, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            */

            #endregion

            //crossroad2
            #region Crossroad2
            //top
            //crossroad2 - CrosswalkLights - top - left
            /*
            g.DrawRectangle(WhitePen, x + length * 8 + crosswalk_width + 3 * FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 8 + crosswalk_width + 3 * FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 8 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            */
            //crossroad2 - CrosswalkLights - top - right
            /*
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 11 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 11 + FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            */

            //bottom
            //crossroad2 - CrosswalkLights - bottom - left
            /*
            g.DrawRectangle(WhitePen, x + length * 8 + crosswalk_width + 3 * FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 8 + crosswalk_width + 3 * FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 8 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            */
            //crossroad2 - CrosswalkLights - bottom - right
            /*
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 11 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 11 + FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            */

            //left
            //crossroad2 - CrosswalkLights - left - top
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 8 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 8 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad2 - CrosswalkLights - left - bottom
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 8 + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 8 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //right
            //crossroad2 - CrosswalkLights - right - top
            g.DrawRectangle(WhitePen, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad2 - CrosswalkLights - right - bottom
            g.DrawRectangle(WhitePen, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            #endregion

            //left T
            #region Left T
            //left T - CrosswalkLights - top - left
            /*
            g.DrawRectangle(WhitePen, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            */
            //left T - CrosswalkLights - top - right
            /*
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            */

            //left T - CrosswalkLights - left - top
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 2 + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //left T - CrosswalkLights - left - bottom
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + length * 11 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 2 + FreeSpace, y + length * 11 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + FreeSpace, y + length * 11 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //left T - CrosswalkLights - right - top 
            /*
            g.DrawRectangle(WhitePen, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            */
            //left T - CrosswalkLights - right - bottom
            /*
            g.DrawRectangle(WhitePen, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 11 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 11 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 11 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            */

            #endregion

            #endregion
                       
        }

        //Crossroad1 + Crossorad2 + Left T + Right T
        public void CrossroadExtension3()
        {
            var g = this.CreateGraphics();

            //background
            //g.Clear(Color.Black);

            //pen color
            Pen BlackPen = new Pen(Color.Black, 2);
            Pen WhitePen = new Pen(Color.White, 2);
            Pen RedPen = new Pen(Color.Red, 2);
            Pen GreenPen = new Pen(Color.Green, 2);
            Pen YellowPen = new Pen(Color.Yellow, 2);

            this.Refresh();

            //BTN allow
            #region BTN alow 

            //Crossroad1
            //top
            btnCrossroad1TopCrosswalkLEFT.Visible = true;
            btnCrossroad1TopCrosswalkLEFT.Enabled = true;
            btnCrossroad1TopCrosswalkRIGHT.Visible = true;
            btnCrossroad1TopCrosswalkRIGHT.Enabled = true;
            //left
            btnCrossroad1LeftCrosswalkTOP.Visible = true;
            btnCrossroad1LeftCrosswalkTOP.Enabled = true;
            btnCrossroad1LeftCrosswalkBOTTOM.Visible = true;
            btnCrossroad1LeftCrosswalkBOTTOM.Enabled = true;

            //Crossroad2
            //left crosswalk
            btnCrossroad2LeftCrosswalkTOP.Visible = true;
            btnCrossroad2LeftCrosswalkTOP.Enabled = true;
            btnCrossroad2LeftCrosswalkBOTTOM.Visible = true;
            btnCrossroad2LeftCrosswalkBOTTOM.Enabled = true;
            //right crosswalk
            btnCrossroad2RightCrosswalkTOP.Visible = true;
            btnCrossroad2RightCrosswalkTOP.Enabled = true;
            btnCrossroad2RightCrosswalkBOTTOM.Visible = true;
            btnCrossroad2RightCrosswalkBOTTOM.Enabled = true;

            //Left T
            //left crosswalk
            btnLeftTLeftCrosswalkTOP.Visible = true;
            btnLeftTLeftCrosswalkTOP.Enabled = true;
            btnLeftTLeftCrosswalkBOTTOM.Visible = true;
            btnLeftTLeftCrosswalkBOTTOM.Enabled = true;

            //Right T
            btnRightTTopCrosswalkLEFT.Visible = true;
            btnRightTTopCrosswalkLEFT.Enabled = true;
            btnRightTTopCrosswalkRIGHT.Visible = true;
            btnRightTTopCrosswalkRIGHT.Enabled = true;

            #endregion

            //Traffic lines 
            #region Traffic lines 

            //crossroad1
            #region crossroad1

            #region Corners

            //left top corner 
            //vertical line
            g.DrawLine(WhitePen, x + length * 3, y, x + length * 3, y + length * 3);
            /*
            g.DrawLine(WhitePen, x + length * 3, y, x + length * 3, y + length);
            g.DrawLine(WhitePen, x + length * 3, y + length, x + length * 3, y + length * 2);
            g.DrawLine(WhitePen, x + length * 3, y + length * 2, x + length * 3, y + length * 3);
            */

            //horizontal line
            g.DrawLine(WhitePen, x, y + length * 3, x + length * 3, y + length * 3);
            /*
            g.DrawLine(WhitePen, x, y + length * 3, x + length, y + length * 3);
            g.DrawLine(WhitePen, x + length, y + length * 3, x + length * 2, y + length * 3);
            g.DrawLine(WhitePen, x + length * 2, y + length * 3, x + length * 3, y + length * 3);
            */

            //left bottom corner
            //vertical line 
            g.DrawLine(WhitePen, x, y + length * 5, x + length * 3, y + length * 5);
            /*
            g.DrawLine(WhitePen, x, y + length * 5, x + length, y + length * 5);
            g.DrawLine(WhitePen, x + length, y + length * 5, x + length * 2, y + length * 5);
            g.DrawLine(WhitePen, x + length * 2, y + length * 5, x + length * 3, y + length * 5);
            */

            //horizontal line 
            g.DrawLine(WhitePen, x + length * 3, y + length * 5, x + length * 3, y + length * 9);
            /*
            g.DrawLine(WhitePen, x + length * 3, y + length * 5, x + length * 3, y + length * 6);
            g.DrawLine(WhitePen, x + length * 3, y + length * 6, x + length * 3, y + length * 7);
            g.DrawLine(WhitePen, x + length * 3, y + length * 7, x + length * 3, y + length * 8);
            g.DrawLine(WhitePen, x + length * 3, y + length * 8, x + length * 3, y + length * 9);
            */

            //right top corner
            //vertical line
            g.DrawLine(WhitePen, x + length * 5, y, x + length * 5, y + length * 3);
            /*
            g.DrawLine(WhitePen, x + length * 5, y, x + length * 5, y + length);
            g.DrawLine(WhitePen, x + length * 5, y + length, x + length * 5, y + length * 2);
            g.DrawLine(WhitePen, x + length * 5, y + length * 2, x + length * 5, y + length * 3);
            */

            //horizontal line 
            //crossroad - mid up - mid 
            g.DrawLine(WhitePen, x + length * 5, y + length * 3, x + length * 9, y + length * 3);
            /*
            g.DrawLine(WhitePen, x + length * 5, y + length * 3, x + length * 6, y + length * 3);
            g.DrawLine(WhitePen, x + length * 6, y + length * 3, x + length * 7, y + length * 3);
            g.DrawLine(WhitePen, x + length * 7, y + length * 3, x + length * 8, y + length * 3);
            g.DrawLine(WhitePen, x + length * 8, y + length * 3, x + length * 9, y + length * 3);
            */

            //right bottom corner 
            //vertical line
            g.DrawLine(WhitePen, x + length * 5, y + length * 5, x + length * 5, y + length * 9);
            /*
            g.DrawLine(WhitePen, x + length * 5, y + length * 5, x + length * 5, y + length * 6);
            g.DrawLine(WhitePen, x + length * 5, y + length * 6, x + length * 5, y + length * 7);
            g.DrawLine(WhitePen, x + length * 5, y + length * 7, x + length * 5, y + length * 8);
            g.DrawLine(WhitePen, x + length * 5, y + length * 8, x + length * 5, y + length * 9);
            */

            //horizontal line 
            g.DrawLine(WhitePen, x + length * 5, y + length * 5, x + length * 9, y + length * 5);
            /*
            g.DrawLine(WhitePen, x + length * 5, y + length * 5, x + length * 6, y + length * 5);
            g.DrawLine(WhitePen, x + length * 6, y + length * 5, x + length * 7, y + length * 5);
            g.DrawLine(WhitePen, x + length * 7, y + length * 5, x + length * 8, y + length * 5);
            g.DrawLine(WhitePen, x + length * 8, y + length * 5, x + length * 9, y + length * 5);
            */

            #endregion

            #region whitelines / traffic lines 

            //top 
            //white line - crossroad1 - top - internal
            //g.DrawLine(WhitePen, x + length * 3, y + length * 3, x + length * 4, y + length * 3); //left
            //g.DrawLine(WhitePen, x + length * 4, y + length * 3, x + length * 5, y + length * 3); //right
            //white line - crossroad1 - top - exteral
            g.DrawLine(WhitePen, x + length * 3, y + length * 2, x + length * 4, y + length * 2); //left
            //g.DrawLine(WhitePen, x + length * 4, y + length * 2, x + length * 5, y + length * 2); //right

            //bottom
            //white line - crossroad1 - bottom - internal
            //g.DrawLine(WhitePen, x + length * 3, y + length * 5, x + length * 4, y + length * 5); //left
            //g.DrawLine(WhitePen, x + length * 4, y + length * 5, x + length * 5, y + length * 5); //right
            //white line - crossroad1 - bottom - external
            //g.DrawLine(WhitePen, x + length * 3, y + length * 6, x + length * 4, y + length * 6); //left
            g.DrawLine(WhitePen, x + length * 4, y + length * 6, x + length * 5, y + length * 6); //right   

            //left 
            //white line - crossorad1 - left - internal
            //g.DrawLine(WhitePen, x + length * 3, y + length * 3, x + length * 3, y + length * 4); //up
            //g.DrawLine(WhitePen, x + length * 3, y + length * 4, x + length * 3, y + length * 5); //down
            //white line - crossroad1 - left - external
            //g.DrawLine(WhitePen, x + length * 2, y + length * 3, x + length * 2, y + length * 4); //up
            g.DrawLine(WhitePen, x + length * 2, y + length * 4, x + length * 2, y + length * 5); //down

            //right
            //white line - crossroad1 - right - internal
            //g.DrawLine(WhitePen, x + length * 5, y + length * 3, x + length * 5, y + length * 4); //up
            //g.DrawLine(WhitePen, x + length * 5, y + length * 4, x + length * 5, y + length * 5); //down
            //white line - crossroad1 - right - external
            g.DrawLine(WhitePen, x + length * 6, y + length * 3, x + length * 6, y + length * 4); //up
            //g.DrawLine(WhitePen, x + length * 6, y + length * 4, x + length * 6, y + length * 5); //down

            #endregion

            #endregion

            //crossorad2
            #region crossroad2 

            #region Corners
            //left top corner 
            //vertical line
            g.DrawLine(WhitePen, x + length * 9, y, x + length * 9, y + length * 3);
            /*
            g.DrawLine(WhitePen, x + length * 9, y, x + length * 9, y + length);
            g.DrawLine(WhitePen, x + length * 9, y + length, x + length * 9, y + length * 2);
            g.DrawLine(WhitePen, x + length * 9, y + length * 2, x + length * 9, y + length * 3);
            */

            //right top corner
            //vertical line
            g.DrawLine(WhitePen, x + length * 11, y, x + length * 11, y + length * 3);
            /*
            g.DrawLine(WhitePen, x + length * 11, y, x + length * 11, y + length);
            g.DrawLine(WhitePen, x + length * 11, y + length, x + length * 11, y + length * 2);
            g.DrawLine(WhitePen, x + length * 11, y + length * 2, x + length * 11, y + length * 3);
            */
            //horizontal line
            g.DrawLine(WhitePen, x + length * 11, y + length * 3, x + length * 14, y + length * 3);
            /*
            g.DrawLine(WhitePen, x + length * 11, y + length * 3, x + length * 12, y + length * 3);
            g.DrawLine(WhitePen, x + length * 12, y + length * 3, x + length * 13, y + length * 3);
            g.DrawLine(WhitePen, x + length * 13, y + length * 3, x + length * 14, y + length * 3);
            */

            //left bottom corner
            //vertical line 
            g.DrawLine(WhitePen, x + length * 9, y + length * 5, x + length * 9, y + length * 9);
            /*
            g.DrawLine(WhitePen, x + length * 9, y + length * 5, x + length * 9, y + length * 6);
            g.DrawLine(WhitePen, x + length * 9, y + length * 6, x + length * 9, y + length * 7);
            g.DrawLine(WhitePen, x + length * 9, y + length * 7, x + length * 9, y + length * 8);
            g.DrawLine(WhitePen, x + length * 9, y + length * 8, x + length * 9, y + length * 9);
            */

            //right bottom corner 
            //vertical line
            g.DrawLine(WhitePen, x + length * 11, y + length * 5, x + length * 11, y + length * 9);
            /*
            g.DrawLine(WhitePen, x + length * 11, y + length * 5, x + length * 11, y + length * 6);
            g.DrawLine(WhitePen, x + length * 11, y + length * 6, x + length * 11, y + length * 7);
            g.DrawLine(WhitePen, x + length * 11, y + length * 7, x + length * 11, y + length * 8);
            g.DrawLine(WhitePen, x + length * 11, y + length * 8, x + length * 11, y + length * 9);
            */
            //horizontal line
            g.DrawLine(WhitePen, x + length * 11, y + length * 5, x + length * 14, y + length * 5);
            /*
            g.DrawLine(WhitePen, x + length * 11, y + length * 5, x + length * 12, y + length * 5);
            g.DrawLine(WhitePen, x + length * 12, y + length * 5, x + length * 13, y + length * 5);
            g.DrawLine(WhitePen, x + length * 13, y + length * 5, x + length * 14, y + length * 5);
            */

            #endregion

            #region whitelines / traffic lines 

            //top
            //white line - crossorad 2 - top - external
            g.DrawLine(WhitePen, x + length * 9, y + length * 2, x + length * 10, y + length * 2); //left
            //g.DrawLine(WhitePen, x + length * 10, y + length * 2, x + length * 11, y + length * 2); //right
            //white line - crossorad 2 - top - internal
            //g.DrawLine(WhitePen, x + length * 9, y + length * 3, x + length * 10, y + length * 3); //left
            //g.DrawLine(WhitePen, x + length * 10, y + length * 3, x + length * 11, y + length * 3); //right

            //bottom
            //white line - crossorad 2 - bottom - internal
            //g.DrawLine(WhitePen, x + length * 9, y + length * 5, x + length * 10, y + length * 5); //left
            //g.DrawLine(WhitePen, x + length * 10, y + length * 5, x + length * 11, y + length * 5); //right
            //white line - crossorad 2 - bottom - external
            //g.DrawLine(WhitePen, x + length * 9, y + length * 6, x + length * 10, y + length * 6); //left
            g.DrawLine(WhitePen, x + length * 10, y + length * 6, x + length * 11, y + length * 6); //right

            //left 
            //white line - crossroad2 - left - exteranl
            //g.DrawLine(WhitePen, x + length * 8, y + length * 3, x + length * 8, y + length * 4); //up
            g.DrawLine(WhitePen, x + length * 8, y + length * 4, x + length * 8, y + length * 5); //down
            //white line - crossroad2 - left - internal
            //g.DrawLine(WhitePen, x + length * 9, y + length * 3, x + length * 9, y + length * 4); //up
            //g.DrawLine(WhitePen, x + length * 9, y + length * 4, x + length * 9, y + length * 5); //down 

            //right
            //white line - crossorad 2 - right - internal
            //g.DrawLine(WhitePen, x + length * 11, y + length * 3, x + length * 11, y + length * 4); //up
            //g.DrawLine(WhitePen, x + length * 11, y + length * 4, x + length * 11, y + length * 5); //down
            //white line - crossorad 2 - right - external
            g.DrawLine(WhitePen, x + length * 12, y + length * 3, x + length * 12, y + length * 4); //up
            //g.DrawLine(WhitePen, x + length * 12, y + length * 4, x + length * 12, y + length * 5); //down

            #endregion

            #endregion

            //Left T
            #region Left T

            #region Corners
            //left top corner 
            //horizontal line 
            g.DrawLine(WhitePen, x, y + length * 9, x + length * 3, y + length * 9);
            /*
            g.DrawLine(WhitePen, x, y + length * 9, x + length, y + length * 9);
            g.DrawLine(WhitePen, x + length, y + length * 9, x + length * 2, y + length * 9);
            g.DrawLine(WhitePen, x + length * 2, y + length * 9, x + length * 3, y + length * 9);
            */

            //right top corner
            //horizontal line 
            g.DrawLine(WhitePen, x + length * 5, y + length * 9, x + length * 9, y + length * 9);
            /*
            g.DrawLine(WhitePen, x + length * 5, y + length * 9, x + length * 6, y + length * 9);
            g.DrawLine(WhitePen, x + length * 6, y + length * 9, x + length * 7, y + length * 9);
            g.DrawLine(WhitePen, x + length * 7, y + length * 9, x + length * 8, y + length * 9);
            g.DrawLine(WhitePen, x + length * 8, y + length * 9, x + length * 9, y + length * 9);
            */

            //bottom line
            g.DrawLine(WhitePen, x, y + length * 11, x + length * 9, y + length * 11);

            #endregion

            #region whitelines / traffic lines 

            //top
            g.DrawLine(WhitePen, x + length * 3, y + length * 8, x + length * 4, y + length * 8);

            //left
            g.DrawLine(WhitePen, x + length * 2, y + length * 10, x + length * 2, y + length * 11);

            //right
            g.DrawLine(WhitePen, x + length * 6, y + length * 9, x + length * 6, y + length * 10);

            #endregion

            #endregion

            //Right T
            #region Right T

            #region Corners
            
            //right top corner
            g.DrawLine(WhitePen, x + length * 11, y + length * 9, x + length * 14, y + length * 9);
            /*
            g.DrawLine(WhitePen, x + length * 11, y + length * 9, x + length * 12, y + length * 9);
            g.DrawLine(WhitePen, x + length * 12, y + length * 9, x + length * 13, y + length * 9);
            g.DrawLine(WhitePen, x + length * 13, y + length * 9, x + length * 14, y + length * 9);
            */

            //bottom line 
            g.DrawLine(WhitePen, x + length * 9, y + length * 11, x + length * 14, y + length * 11);

            #endregion

            #region whitelines / traffic lines 

            //top 
            g.DrawLine(WhitePen, x + length * 9, y + length * 8, x + length * 10, y + length * 8);

            //left 
            g.DrawLine(WhitePen, x + length * 8, y + length * 10, x + length * 8, y + length * 11);

            //right
            g.DrawLine(WhitePen, x + length * 12, y + length * 9, x + length * 12, y + length * 10);

            #endregion

            #endregion

            #endregion

            //Center lines
            #region Center lines

            //vertical center line - left
            //top
            g.DrawLine(WhitePen, x + length * 4, y, x + length * 4, y + length_interrupted);
            g.DrawLine(WhitePen, x + length * 4, y + length_interrupted * 2, x + length * 4, y + length_interrupted * 3);
            //g.DrawLine(WhitePen, x + length * 4, y + length_interrupted * 4, x + length * 4, y + length_interrupted * 5);
            //g.DrawLine(WhitePen, x + length * 4, y + length_interrupted * 6, x + length * 4, y + length_interrupted * 7);
            g.DrawLine(WhitePen, x + length * 4, y + length, x + length * 4, y + length * 2);
            //bottom
            g.DrawLine(WhitePen, x + length * 4, y + length * 6, x + length * 4, y + length * 8);

            //vertical center line - right
            //top
            g.DrawLine(WhitePen, x + length * 10, y, x + length * 10, y + length_interrupted);
            g.DrawLine(WhitePen, x + length * 10, y + length_interrupted * 2, x + length * 10, y + length_interrupted * 3);
            //g.DrawLine(WhitePen, x + length * 4, y + length_interrupted * 4, x + length * 4, y + length_interrupted * 5);
            //g.DrawLine(WhitePen, x + length * 4, y + length_interrupted * 6, x + length * 4, y + length_interrupted * 7);
            g.DrawLine(WhitePen, x + length * 10, y + length, x + length * 10, y + length * 2);
            //bottom
            g.DrawLine(WhitePen, x + length * 10, y + length * 6, x + length * 10, y + length * 8);

            //horizontal center line - top 
            g.DrawLine(WhitePen, x, y + length * 4, x + length_interrupted, y + length * 4);
            g.DrawLine(WhitePen, x + length_interrupted * 2, y + length * 4, x + length_interrupted * 3, y + length * 4);
            g.DrawLine(WhitePen, x + length, y + length * 4, x + length * 2, y + length * 4);
            g.DrawLine(WhitePen, x + length * 6, y + length * 4, x + length * 8, y + length * 4);
            g.DrawLine(WhitePen, x + length * 12, y + length * 4, x + length * 13, y + length * 4);
            g.DrawLine(WhitePen, x + length * 13 + length_interrupted, y + length * 4, x + length * 13 + length_interrupted * 2, y + length * 4);
            g.DrawLine(WhitePen, x + length * 13 + length_interrupted * 3, y + length * 4, x + length * 13 + length_interrupted * 4, y + length * 4);
            //g.DrawLine(WhitePen, x + length_interrupted * 4, y + length * 3, x + length_interrupted * 5, y + length * 3);
            //g.DrawLine(WhitePen, x + length_interrupted * 6, y + length * 3, x + length_interrupted * 7, y + length * 3);
            g.DrawLine(WhitePen, x + length, y + length * 10, x + length * 2, y + length * 10);
            g.DrawLine(WhitePen, x + length * 6, y + length * 10, x + length * 8, y + length * 10);
            g.DrawLine(WhitePen, x + length * 12, y + length * 10, x + length * 13, y + length * 10);

            //horizontal center line - bottom 
            //left T
            g.DrawLine(WhitePen, x, y + length * 10, x + length_interrupted, y + length * 10);
            g.DrawLine(WhitePen, x + length_interrupted * 2, y + length * 10, x + length_interrupted * 3, y + length * 10);
            g.DrawLine(WhitePen, x + length, y + length * 10, x + length * 2, y + length * 10);
            g.DrawLine(WhitePen, x + length * 6, y + length * 10, x + length * 8, y + length * 10);
            //right T 
            g.DrawLine(WhitePen, x + length * 12, y + length * 10, x + length * 13, y + length * 10);
            g.DrawLine(WhitePen, x + length * 13 + length_interrupted, y + length * 10, x + length * 13 + length_interrupted * 2, y + length * 10);
            g.DrawLine(WhitePen, x + length * 13 + length_interrupted * 3, y + length * 10, x + length * 13 + length_interrupted * 4, y + length * 10);

            #endregion
                        
            //TrafficLights
            #region TrafficLights

            //crossorad1
            #region Crossroad1
            //crossroad1 - trafficlight - top
            g.DrawRectangle(WhitePen, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_height);
            g.FillEllipse(red, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            //crossroad1 - trafficlight - left
            g.DrawRectangle(WhitePen, x + length + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length + 3 * FreeSpace + TrafficLights_width * 2, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length + 3 * FreeSpace + TrafficLights_width, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);

            //crossroad1 - trafficlight - right
            g.DrawRectangle(WhitePen, x + length * 6 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length * 6 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 6 + FreeSpace + TrafficLights_width, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 6 + FreeSpace + TrafficLights_width * 2, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            //crossroad1 - trafficlight - bottom
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_height);
            g.FillEllipse(red, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);

            #endregion

            //crossroad2
            #region Crossroad2
            //crossroad2 - trafficlight - top
            g.DrawRectangle(WhitePen, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_height);
            g.FillEllipse(red, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            //crossroad2 - trafficlight - left
            g.DrawRectangle(WhitePen, x + length * 7 + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length * 7 + 3 * FreeSpace + TrafficLights_width * 2, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 7 + 3 * FreeSpace + TrafficLights_width, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 7 + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);

            //crossroad2 - trafficlight - right
            g.DrawRectangle(WhitePen, x + length * 12 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length * 12 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 12 + FreeSpace + TrafficLights_width, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 12 + FreeSpace + TrafficLights_width * 2, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            //crossroad2 - trafficlight - bottom
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_height);
            g.FillEllipse(red, x + length * 11 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 11 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 11 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);

            #endregion

            //left T
            #region Left T
            //left T - trafficlight - top
            g.DrawRectangle(WhitePen, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace, TrafficLights_width, TrafficLights_height);
            g.FillEllipse(red, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            //left T - trafficlight - left
            g.DrawRectangle(WhitePen, x + length + 3 * FreeSpace, y + length * 11 + FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length + 3 * FreeSpace + TrafficLights_width * 2, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length + 3 * FreeSpace + TrafficLights_width, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length + 3 * FreeSpace, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);

            //left T - trafficlight - right
            g.DrawRectangle(WhitePen, x + length * 6 + FreeSpace, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length * 6 + FreeSpace, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 6 + FreeSpace + TrafficLights_width, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 6 + FreeSpace + TrafficLights_width * 2, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            #endregion

            //right T
            #region Right T
            //right T - trafficlight - top
            g.DrawRectangle(WhitePen, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace, TrafficLights_width, TrafficLights_height);
            g.FillEllipse(red, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            //right T - trafficlight - left
            g.DrawRectangle(WhitePen, x + length * 7 + 3 * FreeSpace, y + length * 11 + FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length * 7 + 3 * FreeSpace + TrafficLights_width * 2, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 7 + 3 * FreeSpace + TrafficLights_width, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 7 + 3 * FreeSpace, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);

            //right T - trafficlight - right
            g.DrawRectangle(WhitePen, x + length * 12 + FreeSpace, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_height, TrafficLights_width);
            g.FillEllipse(red, x + length * 12 + FreeSpace, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(yellow, x + length * 12 + FreeSpace + TrafficLights_width, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            g.FillEllipse(green, x + length * 12 + FreeSpace + TrafficLights_width * 2, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

            #endregion

            #endregion

            //Crosswalks
            #region Crosswalks

            //crossroad1 
            #region Crossroad1
            //crossroad1 - crosswalk - top
            g.DrawRectangle(WhitePen, x + length * 3 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 3 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 3 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 3 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 3 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 3 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 4 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 4 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.FillRectangle(white, x + length * 4 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);

            //crossroad1 - crosswalk - left
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 3 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 3 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 3 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 4 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 4 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 4 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);

            //crossroad1 - crosswalk - right
            /*
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 3 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 3 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 3 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 4 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 4 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 4 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            */

            //crossroad1 - crosswalk - bottom
            /*
            g.DrawRectangle(WhitePen, x + length * 3 + FreeSpace, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 3 + 2 * FreeSpace + crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 3 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + FreeSpace, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + 2 * FreeSpace + crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            */

            #endregion

            //crossroad2
            #region Crossorad2 
            //crossroad2 - crosswalk - top
            /*
            g.DrawRectangle(WhitePen, x + length * 9 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 9 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 9 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + FreeSpace, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + 2 * FreeSpace + crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 2 + FreeSpace, crosswalk_width, crosswalk_height);
            */

            //crossroad2 - crosswalk - bottom
            /*
            g.DrawRectangle(WhitePen, x + length * 9 + FreeSpace, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 9 + 2 * FreeSpace + crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 9 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + FreeSpace, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + 2 * FreeSpace + crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 5 + FreeSpace, crosswalk_width, crosswalk_height);
            */

            //crossroad2 - crosswalk - left
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 3 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 3 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 3 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 4 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 4 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 4 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);

            //crossroad2 - crosswalk - right
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 3 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 3 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 3 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 4 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 4 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 4 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);

            #endregion

            //left T
            #region Left T
            //left T - crosswalk - top
            /*
            g.DrawRectangle(WhitePen, x + length * 3 + FreeSpace, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 3 + 2 * FreeSpace + crosswalk_width, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 3 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + FreeSpace, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + 2 * FreeSpace + crosswalk_width, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 4 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);
            */

            //left T - crosswalk - left
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 9 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 9 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 9 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 10 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 10 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + 10 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);

            //left T - crosswalk - right
            /*
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 9 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 9 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 9 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 10 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 10 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + 10 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            */

            #endregion

            //right T
            #region Right T
            //right T - crosswalk - top
            g.DrawRectangle(WhitePen, x + length * 9 + FreeSpace, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 9 + 2 * FreeSpace + crosswalk_width, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 9 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + FreeSpace, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + 2 * FreeSpace + crosswalk_width, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);
            g.DrawRectangle(WhitePen, x + length * 10 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 8 + FreeSpace, crosswalk_width, crosswalk_height);

            //right T - crosswalk - left
            /*
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 9 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 9 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 9 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 10 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 10 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + 10 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            */

            //right T - crosswalk - right
            /*
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 9 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 9 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 9 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 10 * length + FreeSpace, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 10 * length + 2 * FreeSpace + crosswalk_width, crosswalk_height, crosswalk_width);
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + 10 * length + 3 * FreeSpace + 2 * crosswalk_width, crosswalk_height, crosswalk_width);
            */

            #endregion

            #endregion

            //CrosswalkLights
            #region CrosswalkLights 

            //crossroad1
            #region Crossroad1
            //top
            //crossroad1 - CrosswalkLights - top - left
            g.DrawRectangle(WhitePen, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad1 - CrosswalkLights - top - right
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //bottom
            //crossroad1 - CrosswalkLights - bottom - left
            /*
            g.DrawRectangle(WhitePen, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            */
            //crossroad1 - CrosswalkLights - bottom - right
            /*
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            */

            //left
            //crossroad1 - CrosswalkLights - left - top
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad1 - CrosswalkLights - left - bottom
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //right
            //crossroad1 - CrosswalkLights - right - top
            /*
            g.DrawRectangle(WhitePen, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            */
            //crossroad1 - CrosswalkLights - right - bottom
            /*
            g.DrawRectangle(WhitePen, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            */

            #endregion

            //crossroad2
            #region Crossroad2
            //top
            //crossroad2 - CrosswalkLights - top - left
            /*
            g.DrawRectangle(WhitePen, x + length * 8 + crosswalk_width + 3 * FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 8 + crosswalk_width + 3 * FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 8 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            */
            //crossroad2 - CrosswalkLights - top - right
            /*
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 11 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 11 + FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            */

            //bottom
            //crossroad2 - CrosswalkLights - bottom - left
            /*
            g.DrawRectangle(WhitePen, x + length * 8 + crosswalk_width + 3 * FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 8 + crosswalk_width + 3 * FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 8 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            */
            //crossroad2 - CrosswalkLights - bottom - right
            /*
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 11 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 11 + FreeSpace, y + length * 5 + 2 * crosswalk_width + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            */

            //left
            //crossroad2 - CrosswalkLights - left - top
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 8 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 8 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad2 - CrosswalkLights - left - bottom
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 8 + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 8 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //right
            //crossroad2 - CrosswalkLights - right - top
            g.DrawRectangle(WhitePen, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //crossroad2 - CrosswalkLights - right - bottom
            g.DrawRectangle(WhitePen, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            #endregion

            //left T
            #region Left T
            //left T - CrosswalkLights - top - left
            /*
            g.DrawRectangle(WhitePen, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            */
            //left T - CrosswalkLights - top - right
            /*
            g.DrawRectangle(WhitePen, x + length * 5 + FreeSpace, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            */

            //left T - CrosswalkLights - left - top
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 2 + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //left T - CrosswalkLights - left - bottom
            g.DrawRectangle(WhitePen, x + length * 2 + FreeSpace, y + length * 11 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 2 + FreeSpace, y + length * 11 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 2 + FreeSpace, y + length * 11 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //left T - CrosswalkLights - right - top 
            /*
            g.DrawRectangle(WhitePen, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            */
            //left T - CrosswalkLights - right - bottom
            /*
            g.DrawRectangle(WhitePen, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 11 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 11 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 5 + 2 * crosswalk_width + FreeSpace, y + length * 11 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            */

            #endregion

            //right T
            #region Right T
            //right T - CrosswalkLights - top - left
            g.DrawRectangle(WhitePen, x + length * 8 + crosswalk_width + 3 * FreeSpace, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 8 + crosswalk_width + 3 * FreeSpace, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 8 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            //right T - CrosswalkLights - top - right
            g.DrawRectangle(WhitePen, x + length * 11 + FreeSpace, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_height, TrafficLightsCrosswalk_width);
            g.FillEllipse(red, x + length * 11 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 11 + FreeSpace, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);

            //right T - CrosswalkLights - left - top
            /*
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 8 + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 8 + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            */
            //right T - CrosswalkLights - left - botom
            /*
            g.DrawRectangle(WhitePen, x + length * 8 + FreeSpace, y + length * 11 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 8 + FreeSpace, y + length * 11 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 8 + FreeSpace, y + length * 11 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            */

            //right T - CrosswalkLights - right - top
            /*
            g.DrawRectangle(WhitePen, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            */
            //right T - CrosswalkLights - right - bottom
            /*
            g.DrawRectangle(WhitePen, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 11 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_height);
            g.FillEllipse(red, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 11 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            g.FillEllipse(green, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 11 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            */

            #endregion

            #endregion
                      
        }

        #endregion

        //Methods for UpdateTrafficLights
        #region Methods for UpdateTrafficLights
        public void UpdateTrafficLightsCrossroad1TOP(bool red, bool yellow, bool green)
        {
            if (red)
            {

            }
            else if (yellow)
            {

            }
            else if (green)
            {

            }

            Invalidate();
        }

        public void UpdateTrafficLightsCrossroad1BOTTOM(bool red, bool yellow, bool green)
        {
            if (red)
            {

            }
            else if (yellow)
            {

            }
            else if (green)
            {

            }

            Invalidate();
        }

        public void UpdateTrafficLightsCrossroad1LEFT(bool red, bool yellow, bool green)
        {
            if (red)
            {

            }
            else if (yellow)
            {

            }
            else if (green)
            {

            }

            Invalidate();
        }

        public void UpdateTrafficLightsCrossroad1RIGHT(bool red, bool yellow, bool green)
        {
            if (red)
            {

            }
            else if (yellow)
            {

            }
            else if (green)
            {

            }

            Invalidate();
        }

        public void UpdateTrafficLightsCrossroad1CrosswalkTOP(bool red, bool yellow, bool green)
        {
            if (red)
            {

            }
            else if (yellow)
            {

            }
            else if (green)
            {

            }

            Invalidate();
        }

        public void UpdateTrafficLightsCrossroad1CrosswalkLEFT(bool red, bool yellow, bool green)
        {
            if (red)
            {

            }
            else if (yellow)
            {

            }
            else if (green)
            {

            }

            Invalidate();
        }

        #endregion

        private void UserControlCrossroad_Load(object sender, EventArgs e)
        {

        }
    }



}
