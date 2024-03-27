using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bc_prace.Controls.MyGraphControl.Entities;
using Sharp7;

namespace Bc_prace.Controls
{
    public delegate void CrossroadBTNClick(object sender, string id);

    public partial class UserControlCrossroad : UserControl
    {
        private Program3Form program3FormInstance = null;
        //private ChooseOptionForm chooseOptionFormInstance;

        private S7Client client;

        //MessageBox control
        private bool errorMessageBoxShown;

        public event CrossroadBTNClick OnCrossroadBTNClick;

        //BTNS define
        #region BTNS define
        //Crossroad1
        private Button btnCrossroad1TopCrosswalkLEFT = new Button();
        private Button btnCrossroad1TopCrosswalkRIGHT = new Button();
        private Button btnCrossroad1LeftCrosswalkTOP = new Button();
        private Button btnCrossroad1LeftCrosswalkBOTTOM = new Button();
        //Crossroad2
        private Button btnCrossroad2LeftCrosswalkTOP = new Button();
        private Button btnCrossroad2LeftCrosswalkBOTTOM = new Button();
        private Button btnCrossroad2RightCrosswalkTOP = new Button();
        private Button btnCrossroad2RightCrosswalkBOTTOM = new Button();
        //Left T
        private Button btnLeftTLeftCrosswalkTOP = new Button();
        private Button btnLeftTLeftCrosswalkBOTTOM = new Button();
        //Right T
        private Button btnRightTTopCrosswalkLEFT = new Button();
        private Button btnRightTTopCrosswalkRIGHT = new Button();

        #endregion

        //Buffers variables => probably useless
        #region Buffers variables

        //DB14 => Crossroad_DB -> 11 structs -> x variables -> size 110.0 
        private int DBNumber_DB14 = 14;
        private byte[] read_buffer_DB14;
        public byte[] previous_buffer_DB14;
        public byte[] PreviousBufferHash_DB14;
        private byte[] send_buffer_DB14;
        //+ other structs are Timers 

        //DB1 => Crossroad_1_DB -> Crossroad 1 -> 2 structs -> 25 variables -> size 6.3
        private int DBNumber_DB1 = 1;
        private byte[] read_buffer_DB1;
        public byte[] previous_buffer_DB1;
        public byte[] PreviousBufferHash_DB1;
        private byte[] send_buffer_DB1;

        //DB19 => Crossroad_2_DB -> Crossroad 2 -> 2 structs -> 25 variables -> size 6.3  
        private int DBNumber_DB19 = 19;
        private byte[] read_buffer_DB19;
        public byte[] previous_buffer_DB19;
        public byte[] PreviousBufferHash_DB19;
        private byte[] send_buffer_DB19;

        //DB20 => Crossroad_LeftT_DB - Left T -> 2 structs -> 16 variables -> size 5.4 
        private int DBNumber_DB20 = 20;
        private byte[] read_buffer_DB20;
        public byte[] previous_buffer_DB20;
        public byte[] PreviousBufferHash_DB20;
        private byte[] send_buffer_DB20;

        //DB21 => Crossroad_RightT_DB - Right T -> 2 structs -> 16 variables -> size 5.4 
        private int DBNumber_DB21 = 21;
        private byte[] read_buffer_DB21;
        public byte[] previous_buffer_DB21;
        public byte[] PreviousBufferHash_DB21;
        private byte[] send_buffer_DB21;

        #endregion

        //Input variables => probably useless
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

        //Output variables => probably useless
        #region Output variables 

        //Crossroad_DB DB14
        #region Crossroad_DB DB14

        int TrafficLightsSQ;

        #endregion

        //Crossroad_1_DB => DB1
        #region Crossroad_1_DB DB1

        int Crossroad1CrosswalkSQ;

        bool crossroad1TopRED;
        bool crossroad1TopGREEN;
        bool crossroad1TopYELLOW;
        bool crossroad1LeftRED;
        bool crossroad1LeftGREEN;
        bool crossroad1LeftYELLOW;
        bool crossroad1RightRED;
        bool crossroad1RightGREEN;
        bool crossroad1RightYELLOW;
        bool crossroad1BottomRED;
        bool crossroad1BottomGREEN;
        bool crossroad1BottomYELLOW;

        bool crossroad1TopCrosswalkRED1;
        bool crossroad1TopCrosswalkRED2;
        bool crossroad1TopCrosswalkGREEN1;
        bool crossroad1TopCrosswalkGREEN2;
        bool crossroad1LeftCrosswalkRED1;
        bool crossroad1LeftCrosswalkRED2;
        bool crossroad1LeftCrosswalkGREEN1;
        bool crossroad1LeftCrosswalkGREEN2;

        #endregion

        //Crossroad_2_DB => DB19
        #region Crossroad_2_DB DB19

        int Crossroad2CrosswalkSQ;

        bool crossroad2TopRED;
        bool crossroad2TopGREEN;
        bool crossroad2TopYELLOW;
        bool crossroad2LeftRED;
        bool crossroad2LeftGREEN;
        bool crossroad2LeftYELLOW;
        bool crossroad2RightRED;
        bool crossroad2RightGREEN;
        bool crossroad2RightYELLOW;
        bool crossroad2BottomRED;
        bool crossroad2BottomGREEN;
        bool crossroad2BottomYELLOW;

        bool crossroad2LeftCrosswalkRED1;
        bool crossroad2LeftCrosswalkRED2;
        bool crossroad2LeftCrosswalkGREEN1;
        bool crossroad2LeftCrosswalkGREEN2;
        bool crossroad2RightCrosswalkRED1;
        bool crossroad2RightCrosswalkRED2;
        bool crossroad2RightCrosswalkGREEN1;
        bool crossroad2RightCrosswalkGREEN2;

        #endregion

        //Crossroad_LeftT_DB => DB20
        #region Crossroad_LeftT_DB DB20

        int CrossroadLeftTCrosswalkSQ;

        bool crossroadLeftTTopRED;
        bool crossroadLeftTTopGREEN;
        bool crossroadLeftTTopYELLOW;
        bool crossroadLeftTLeftRED;
        bool crossroadLeftTLeftGREEN;
        bool crossroadLeftTLeftYELLOW;
        bool crossroadLeftTRightRED;
        bool crossroadLeftTRightGREEN;
        bool crossroadLeftTRightYELLOW;

        bool crossroadLeftTLeftCrosswalkRED1;
        bool crossroadLeftTLeftCrosswalkRED2;
        bool crossroadLeftTLeftCrosswalkGREEN1;
        bool crossroadLeftTLeftCrosswalkGREEN2;

        #endregion

        //Crossroad_RightT_DB => DB21
        #region Crossroad_RightT_DB DB21

        int CrossroadRightTCrosswalkSQ;

        bool crossroadRightTTopRED;
        bool crossroadRightTTopGREEN;
        bool crossroadRightTTopYELLOW;
        bool crossroadRightTLeftRED;
        bool crossroadRightTLeftGREEN;
        bool crossroadRightTLeftYELLOW;
        bool crossroadRightTRightRED;
        bool crossroadRightTRightGREEN;
        bool crossroadRightTRightYELLOW;

        bool crossroadRightTTopCrosswalkRED1;
        bool crossroadRightTTopCrosswalkRED2;
        bool crossroadRightTTopCrosswalkGREEN1;
        bool crossroadRightTTopCrosswalkGREEN2;

        #endregion

        #endregion

        //Selection posibilities of crossroad variant
        private bool drawBasicCrossroad;
        private bool drawCrossroadExtension1;
        private bool drawCrossroadExtension2;
        private bool drawCrossroadExtension3;

        //Drawing variables 
        #region Drawing variables 

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
        private SolidBrush black = new SolidBrush(Color.Black);

        #endregion

        //EventHandler -> doesnt work properly
        #region EventHandler -> doesnt work properly 
        /*
        //public event EventHandler<string> ButtonClicked;

        //Crossroad1 => DB1
        public event EventHandler<bool> Crossroad1TopREDChanged;
        public event EventHandler<bool> Crossroad1TopGREENChanged;
        public event EventHandler<bool> Crossroad1TopYELLOWChanged;
        public event EventHandler<bool> Crossroad1LeftREDChanged;
        public event EventHandler<bool> Crossroad1LeftGREENChanged;
        public event EventHandler<bool> Crossroad1LeftYELLOWChanged;
        public event EventHandler<bool> Crossroad1RightREDChanged;
        public event EventHandler<bool> Crossroad1RightGREENChanged;
        public event EventHandler<bool> Crossroad1RightYELLOWChanged;
        public event EventHandler<bool> Crossroad1BottomREDChanged;
        public event EventHandler<bool> Crossroad1BottomGREENChanged;
        public event EventHandler<bool> Crossroad1BottomYELLOWChanged;

        public event EventHandler<bool> Crossroad1TopCrosswalkRED1Changed;
        public event EventHandler<bool> Crossroad1TopCrosswalkRED2Changed;
        public event EventHandler<bool> Crossroad1TopCrosswalkGREEN1Changed;
        public event EventHandler<bool> Crossroad1TopCrosswalkGREEN2Changed;
        public event EventHandler<bool> Crossroad1LeftCrosswalkRED1Changed;
        public event EventHandler<bool> Crossroad1LeftCrosswalkRED2Changed;
        public event EventHandler<bool> Crossroad1LeftCrosswalkGREEN1Changed;
        public event EventHandler<bool> Crossroad1LeftCrosswalkGREEN2Changed;

        //Crossroad2 => DB19
        public event EventHandler<bool> Crossroad2TopREDChanged;
        public event EventHandler<bool> Crossroad2TopGREENChanged;
        public event EventHandler<bool> Crossroad2TopYELLOWChanged;
        public event EventHandler<bool> Crossroad2LeftREDChanged;
        public event EventHandler<bool> Crossroad2LeftGREENChanged;
        public event EventHandler<bool> Crossroad2LeftYELLOWChanged;
        public event EventHandler<bool> Crossroad2RightREDChanged;
        public event EventHandler<bool> Crossroad2RightGREENChanged;
        public event EventHandler<bool> Crossroad2RightYELLOWChanged;
        public event EventHandler<bool> Crossroad2BottomREDChanged;
        public event EventHandler<bool> Crossroad2BottomGREENChanged;
        public event EventHandler<bool> Crossroad2BottomYELLOWChanged;

        public event EventHandler<bool> Crossroad2LeftCrosswalkRED1Changed;
        public event EventHandler<bool> Crossroad2LeftCrosswalkRED2Changed;
        public event EventHandler<bool> Crossroad2LeftCrosswalkGREEN1Changed;
        public event EventHandler<bool> Crossroad2LeftCrosswalkGREEN2Changed;
        public event EventHandler<bool> Crossroad2RightCrosswalkRED1Changed;
        public event EventHandler<bool> Crossroad2RightCrosswalkRED2Changed;
        public event EventHandler<bool> Crossroad2RightCrosswalkGREEN1Changed;
        public event EventHandler<bool> Crossroad2RightCrosswalkGREEN2Changed;

        //Crossroad LeftT => DB20
        public event EventHandler<bool> CrossroadLeftTTopREDChanged;
        public event EventHandler<bool> CrossroadLeftTTopGREENChanged;
        public event EventHandler<bool> CrossroadLeftTTopYELLOWChanged;
        public event EventHandler<bool> CrossroadLeftTLeftREDChanged;
        public event EventHandler<bool> CrossroadLeftTLeftGREENChanged;
        public event EventHandler<bool> CrossroadLeftTLeftYELLOWChanged;
        public event EventHandler<bool> CrossroadLeftTRightREDChanged;
        public event EventHandler<bool> CrossroadLeftTRightGREENChanged;
        public event EventHandler<bool> CrossroadLeftTRightYELLOWChanged;

        public event EventHandler<bool> CrossroadLeftTLeftCrosswalkRED1Changed;
        public event EventHandler<bool> CrossroadLeftTLeftCrosswalkRED2Changed;
        public event EventHandler<bool> CrossroadLeftTLeftCrosswalkGREEN1Changed;
        public event EventHandler<bool> CrossroadLeftTLeftCrosswalkGREEN2Changed;

        //Crossroad RightT => DB21
        public event EventHandler<bool> CrossroadRightTTopREDChanged;
        public event EventHandler<bool> CrossroadRightTTopGREENChanged;
        public event EventHandler<bool> CrossroadRightTTopYELLOWChanged;
        public event EventHandler<bool> CrossroadRightTLeftREDChanged;
        public event EventHandler<bool> CrossroadRightTLeftGREENChanged;
        public event EventHandler<bool> CrossroadRightTLeftYELLOWChanged;
        public event EventHandler<bool> CrossroadRightTRightREDChanged;
        public event EventHandler<bool> CrossroadRightTRightGREENChanged;
        public event EventHandler<bool> CrossroadRightTRightYELLOWChanged;

        public event EventHandler<bool> CrossroadRightTTopCrosswalkRED1Changed;
        public event EventHandler<bool> CrossroadRightTTopCrosswalkRED2Changed;
        public event EventHandler<bool> CrossroadRightTTopCrosswalkGREEN1Changed;
        public event EventHandler<bool> CrossroadRightTTopCrosswalkGREEN2Changed;

        */

        #endregion

        //Variables for crossroad versions  
        #region Variables for crossroad versions   
        public bool DrawBasicCrossroad
        {
            get { return drawBasicCrossroad; }
            set
            {
                if (value != drawBasicCrossroad)
                {
                    drawBasicCrossroad = value;
                    Invalidate();
                }
            }
        }

        public bool DrawCrossroadExtension1
        {
            get { return drawCrossroadExtension1; }
            set
            {
                if (value != drawCrossroadExtension1)
                {
                    drawCrossroadExtension1 = value;
                    Invalidate();
                }
            }
        }

        public bool DrawCrossroadExtension2
        {
            get { return drawCrossroadExtension2; }
            set
            {
                if (value != drawCrossroadExtension2)
                {
                    drawCrossroadExtension2 = value;
                    Invalidate();
                }
            }
        }

        public bool DrawCrossroadExtension3
        {
            get { return drawCrossroadExtension3; }
            set
            {
                if (value != drawCrossroadExtension3)
                {
                    drawCrossroadExtension3 = value;
                    Invalidate();
                }
            }
        }

        #endregion

        //Variables for crossroad lights
        #region Variables for crossroad lights

        //Crossroad1TopGREEN 
        //private bool crossroad1TopGREEN;
        public bool Crossroad1TopGREEN
        {
            get { return crossroad1TopGREEN; }

            set
            {
                crossroad1TopGREEN = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //Crossroad1TopYELLOW 
        //private bool crossroad1TopYELLOW;
        public bool Crossroad1TopYELLOW
        {
            get { return crossroad1TopYELLOW; }

            set
            {
                crossroad1TopYELLOW = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //Crossroad1TopRED 
        //private bool crossroad1TopRED;
        public bool Crossroad1TopRED
        {
            get { return crossroad1TopRED; }

            set
            {
                crossroad1TopRED = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //Crossroad1BottomGREEN 
        //private bool crossroad1BottomGREEN;
        public bool Crossroad1BottomGREEN
        {
            get { return crossroad1BottomGREEN; }

            set
            {
                crossroad1BottomGREEN = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //Crossroad1BottomYELLOW 
        //private bool crossroad1BottomYELLOW;
        public bool Crossroad1BottomYELLOW
        {
            get { return crossroad1BottomYELLOW; }

            set
            {
                crossroad1BottomYELLOW = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //Crossroad1BottomRED 
        //private bool crossroad1BottomRED;
        public bool Crossroad1BottomRED
        {
            get { return crossroad1BottomRED; }

            set
            {
                crossroad1BottomRED = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //Crossroad1LeftGREEN 
        //private bool crossroad1LeftGREEN;
        public bool Crossroad1LeftGREEN
        {
            get { return crossroad1LeftGREEN; }

            set
            {
                crossroad1LeftGREEN = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //Crossroad1LeftYELLOW 
        //private bool crossroad1LeftYELLOW;
        public bool Crossroad1LeftYELLOW
        {
            get { return crossroad1LeftYELLOW; }

            set
            {
                crossroad1LeftYELLOW = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //Crossroad1LeftRED
        //private bool crossroad1LeftRED;
        public bool Crossroad1LeftRED
        {
            get { return crossroad1LeftRED; }

            set
            {
                crossroad1LeftRED = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //Crossroad1RightGREEN 
        //private bool crossroad1RightGREEN;
        public bool Crossroad1RightGREEN
        {
            get { return crossroad1RightGREEN; }

            set
            {
                crossroad1RightGREEN = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //Crossroad1RightYELLOW 
        //private bool crossroad1RightYELLOW;
        public bool Crossroad1RightYELLOW
        {
            get { return crossroad1RightYELLOW; }

            set
            {
                crossroad1RightYELLOW = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //Crossroad1RightRED
        //private bool crossroad1RightRED;
        public bool Crossroad1RightRED
        {
            get { return crossroad1RightRED; }

            set
            {
                crossroad1RightRED = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //Crossroad1TopCrosswalkRED1
        //private bool crossroad1TopCrosswalkRED1;
        public bool Crossroad1TopCrosswalkRED1
        {
            get { return crossroad1TopCrosswalkRED1; }

            set
            {
                crossroad1TopCrosswalkRED1 = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //Crossroad1TopCrosswalkGREEN1
        //private bool crossroad1TopCrosswalkGREEN1;
        public bool Crossroad1TopCrosswalkGREEN1
        {
            get { return crossroad1TopCrosswalkGREEN1; }

            set
            {
                crossroad1TopCrosswalkGREEN1 = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //Crossroad1TopCrosswalkRED2
        //private bool crossroad1TopCrosswalkRED2;
        public bool Crossroad1TopCrosswalkRED2
        {
            get { return crossroad1TopCrosswalkRED2; }

            set
            {
                crossroad1TopCrosswalkRED2 = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //Crossroad1TopCrosswalkGREEN2
        //private bool crossroad1TopCrosswalkGREEN2;
        public bool Crossroad1TopCrosswalkGREEN2
        {
            get { return crossroad1TopCrosswalkGREEN2; }

            set
            {
                crossroad1TopCrosswalkGREEN2 = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //Crossroad1LeftCrosswalkRED1
        //private bool crossroad1LeftCrosswalkRED1;
        public bool Crossroad1LeftCrosswalkRED1
        {
            get { return crossroad1LeftCrosswalkRED1; }

            set
            {
                crossroad1LeftCrosswalkRED1 = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //Crossroad1LeftCrosswalkGREEN1
        //private bool crossroad1LeftCrosswalkGREEN1;
        public bool Crossroad1LeftCrosswalkGREEN1
        {
            get { return crossroad1LeftCrosswalkGREEN1; }

            set
            {
                crossroad1LeftCrosswalkGREEN1 = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //Crossroad1LeftCrosswalkRED2
        //private bool crossroad1LeftCrosswalkRED2;
        public bool Crossroad1LeftCrosswalkRED2
        {
            get { return crossroad1LeftCrosswalkRED2; }

            set
            {
                crossroad1LeftCrosswalkRED2 = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //Crossroad1LeftCrosswalkGREEN2
        //private bool crossroad1LeftCrosswalkGREEN2;
        public bool Crossroad1LeftCrosswalkGREEN2
        {
            get { return crossroad1LeftCrosswalkGREEN2; }

            set
            {
                crossroad1LeftCrosswalkGREEN2 = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }


        //Crossroad2TopGREEN 
        //private bool crossroad2TopGREEN;
        public bool Crossroad2TopGREEN
        {
            get { return crossroad2TopGREEN; }

            set
            {
                crossroad2TopGREEN = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //Crossroad2TopYELLOW 
        //private bool crossroad2TopYELLOW;
        public bool Crossroad2TopYELLOW
        {
            get { return crossroad2TopYELLOW; }

            set
            {
                crossroad2TopYELLOW = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //Crossroad2TopRED
        //private bool crossroad2TopRED;
        public bool Crossroad2TopRED
        {
            get { return crossroad2TopRED; }

            set
            {
                crossroad2TopRED = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //Crossroad2BottomGREEN 
        //private bool crossroad2BottomGREEN;
        public bool Crossroad2BottomGREEN
        {
            get { return crossroad2BottomGREEN; }

            set
            {
                crossroad2BottomGREEN = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //Crossroad2BottomYELLOW 
        //private bool crossroad2BottomYELLOW;
        public bool Crossroad2BottomYELLOW
        {
            get { return crossroad2BottomYELLOW; }

            set
            {
                crossroad2BottomYELLOW = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //Crossroad2BottomRED 
        //private bool crossroad2BottomRED;
        public bool Crossroad2BottomRED
        {
            get { return crossroad2BottomRED; }

            set
            {
                crossroad2BottomRED = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //Crossroad2LeftGREEN 
        //private bool crossroad2LeftGREEN;
        public bool Crossroad2LeftGREEN
        {
            get { return crossroad2LeftGREEN; }

            set
            {
                crossroad2LeftGREEN = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //Crossroad2LeftYELLOW
        //private bool crossroad2LeftYELLOW;
        public bool Crossroad2LeftYELLOW
        {
            get { return crossroad2LeftYELLOW; }

            set
            {
                crossroad2LeftYELLOW = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //Crossroad2LeftRED
        //private bool crossroad2LeftRED;
        public bool Crossroad2LeftRED
        {
            get { return crossroad2LeftRED; }

            set
            {
                crossroad2LeftRED = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //Crossroad2RightGREEN 
        //private bool crossroad1BottomGREEN;
        public bool Crossroad2RightGREEN
        {
            get { return crossroad2RightGREEN; }

            set
            {
                crossroad2RightGREEN = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //Crossroad2RightYELLOW 
        //private bool crossroad2RightYELLOW;
        public bool Crossroad2RightYELLOW
        {
            get { return crossroad2RightYELLOW; }

            set
            {
                crossroad2RightYELLOW = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //Crossroad2RightRED 
        //private bool crossroad2RightRED;
        public bool Crossroad2RightRED
        {
            get { return crossroad2RightRED; }

            set
            {
                crossroad2RightRED = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //Crossroad2LeftCrosswalkRED1
        //private bool crossroad2LeftCrosswalkRED1;
        public bool Crossroad2LeftCrosswalkRED1
        {
            get { return crossroad2LeftCrosswalkRED1; }

            set
            {
                crossroad2LeftCrosswalkRED1 = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //Crossroad2LeftCrosswalkGREEN1
        //private bool crossroad2LeftCrosswalkGREEN1;
        public bool Crossroad2LeftCrosswalkGREEN1
        {
            get { return crossroad2LeftCrosswalkGREEN1; }

            set
            {
                crossroad2LeftCrosswalkGREEN1 = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //Crossroad2LeftCrosswalkRED2
        //private bool crossroad2LeftCrosswalkRED2;
        public bool Crossroad2LeftCrosswalkRED2
        {
            get { return crossroad2LeftCrosswalkRED2; }

            set
            {
                crossroad2LeftCrosswalkRED2 = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //Crossroad2LeftCrosswalkGREEN2
        //private bool crossroad2LeftCrosswalkGREEN2;
        public bool Crossroad2LeftCrosswalkGREEN2
        {
            get { return crossroad2LeftCrosswalkGREEN2; }

            set
            {
                crossroad2LeftCrosswalkGREEN2 = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //Crossroad2RightCrosswalkRED1
        //private bool crossroad2RightCrosswalkRED1;
        public bool Crossroad2RightCrosswalkRED1
        {
            get { return crossroad2RightCrosswalkRED1; }

            set
            {
                crossroad2RightCrosswalkRED1 = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //Crossroad2RightCrosswalkGREEN1
        //private bool crossroad2RightCrosswalkGREEN1;
        public bool Crossroad2RightCrosswalkGREEN1
        {
            get { return crossroad2RightCrosswalkGREEN1; }

            set
            {
                crossroad2RightCrosswalkGREEN1 = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //Crossroad2RightCrosswalkRED2
        //private bool crossroad2RightCrosswalkRED2;
        public bool Crossroad2RightCrosswalkRED2
        {
            get { return crossroad2RightCrosswalkRED2; }

            set
            {
                crossroad2RightCrosswalkRED2 = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //Crossroad2RightCrosswalkGREEN2
        //private bool crossroad2RightCrosswalkGREEN2;
        public bool Crossroad2RightCrosswalkGREEN2
        {
            get { return crossroad2RightCrosswalkGREEN2; }

            set
            {
                crossroad2RightCrosswalkGREEN2 = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }


        //CrossroadLeftTTopGREEN 
        //private bool crossroadLeftTTopGREEN;
        public bool CrossroadLeftTTopGREEN
        {
            get { return crossroadLeftTTopGREEN; }

            set
            {
                crossroadLeftTTopGREEN = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //CrossroadLeftTTopYELLOW 
        //private bool crossroadLeftTTopYELLOW;
        public bool CrossroadLeftTTopYELLOW
        {
            get { return crossroadLeftTTopYELLOW; }

            set
            {
                crossroadLeftTTopYELLOW = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //CrossroadLeftTTopRED 
        //private bool crossroadLeftTTopRED;
        public bool CrossroadLeftTTopRED
        {
            get { return crossroadLeftTTopRED; }

            set
            {
                crossroadLeftTTopRED = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //CrossroadLeftTLeftGREEN 
        //private bool crossroadLeftTLeftGREEN;
        public bool CrossroadLeftTLeftGREEN
        {
            get { return crossroadLeftTLeftGREEN; }

            set
            {
                crossroadLeftTLeftGREEN = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //CrossroadLeftTLeftYELLOW 
        //private bool crossroadLeftTLeftYELLOW;
        public bool CrossroadLeftTLeftYELLOW
        {
            get { return crossroadLeftTLeftYELLOW; }

            set
            {
                crossroadLeftTLeftYELLOW = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //CrossroadLeftTLeftRED 
        //private bool crossroadLeftTLeftRED;
        public bool CrossroadLeftTLeftRED
        {
            get { return crossroadLeftTLeftRED; }

            set
            {
                crossroadLeftTLeftRED = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //CrossroadLeftTRightGREEN 
        //private bool crossroadLeftTRightGREEN;
        public bool CrossroadLeftTRightGREEN
        {
            get { return crossroadLeftTRightGREEN; }

            set
            {
                crossroadLeftTRightGREEN = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //CrossroadLeftTRightYELLOW 
        //private bool crossroadLeftTRightYELLOW;
        public bool CrossroadLeftTRightYELLOW
        {
            get { return crossroadLeftTRightYELLOW; }

            set
            {
                crossroadLeftTRightYELLOW = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //CrossroadLeftTRightRED 
        //private bool crossroadLeftTRightRED;
        public bool CrossroadLeftTRightRED
        {
            get { return crossroadLeftTRightRED; }

            set
            {
                crossroadLeftTRightRED = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //CrossroadLeftTLeftCrosswalkRED1
        //private bool crossroadLeftTLeftCrosswalkRED1;
        public bool CrossroadLeftTLeftCrosswalkRED1
        {
            get { return crossroadLeftTLeftCrosswalkRED1; }

            set
            {
                crossroadLeftTLeftCrosswalkRED1 = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //CrossroadLeftTLeftCrosswalkGREEN1
        //private bool crossroadLeftTLeftCrosswalkGREEN1;
        public bool CrossroadLeftTLeftCrosswalkGREEN1
        {
            get { return crossroadLeftTLeftCrosswalkGREEN1; }

            set
            {
                crossroadLeftTLeftCrosswalkGREEN1 = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //CrossroadLeftTLeftCrosswalkRED2
        //private bool crossroadLeftTLeftCrosswalkRED2;
        public bool CrossroadLeftTLeftCrosswalkRED2
        {
            get { return crossroadLeftTLeftCrosswalkRED2; }

            set
            {
                crossroadLeftTLeftCrosswalkRED2 = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //CrossroadLeftTLeftCrosswalkGREEN2
        //private bool crossroadLeftTLeftCrosswalkGREEN2;
        public bool CrossroadLeftTLeftCrosswalkGREEN2
        {
            get { return crossroadLeftTLeftCrosswalkGREEN2; }

            set
            {
                crossroadLeftTLeftCrosswalkGREEN2 = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }


        //CrossroadRightTTopGREEN 
        //private bool crossroadRightTTopGREEN;
        public bool CrossroadRightTTopGREEN
        {
            get { return crossroadRightTTopGREEN; }

            set
            {
                crossroadRightTTopGREEN = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //CrossroadRightTTopYELLOW
        //private bool crossroadRightTTopYELLOW;
        public bool CrossroadRightTTopYELLOW
        {
            get { return crossroadRightTTopYELLOW; }

            set
            {
                crossroadRightTTopYELLOW = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //CrossroadRightTTopRED
        //private bool crossroadRightTTopRED;
        public bool CrossroadRightTTopRED
        {
            get { return crossroadRightTTopRED; }

            set
            {
                crossroadRightTTopRED = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //CrossroadRightTLeftGREEN 
        //private bool crossroadRightTLeftGREEN;
        public bool CrossroadRightTLeftGREEN
        {
            get { return crossroadRightTLeftGREEN; }

            set
            {
                crossroadRightTLeftGREEN = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //CrossroadRightTLeftYELLOW 
        //private bool crossroadRightTLeftYELLOW;
        public bool CrossroadRightTLeftYELLOW
        {
            get { return crossroadRightTLeftYELLOW; }

            set
            {
                crossroadRightTLeftYELLOW = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //CrossroadRightTLeftRED 
        //private bool crossroadRightTLeftRED;
        public bool CrossroadRightTLeftRED
        {
            get { return crossroadRightTLeftRED; }

            set
            {
                crossroadRightTLeftRED = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //CrossroadRightTRightGREEN 
        //private bool crossroadRightTRightGREEN;
        public bool CrossroadRightTRightGREEN
        {
            get { return crossroadRightTRightGREEN; }

            set
            {
                crossroadRightTRightGREEN = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //CrossroadRightTRightYELLOW 
        //private bool crossroadRightTRightYELLOW;
        public bool CrossroadRightTRightYELLOW
        {
            get { return crossroadRightTRightYELLOW; }

            set
            {
                crossroadRightTRightYELLOW = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //CrossroadRightTRightRED 
        //private bool crossroadRightTRightRED;
        public bool CrossroadRightTRightRED
        {
            get { return crossroadRightTRightRED; }

            set
            {
                crossroadRightTRightRED = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //CrossroadRightTTopCrosswalkRED1
        //private bool crossroadRightTTopCrosswalkRED1;
        public bool CrossroadRightTTopCrosswalkRED1
        {
            get { return crossroadRightTTopCrosswalkRED1; }

            set
            {
                crossroadRightTTopCrosswalkRED1 = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //CrossroadRightTTopCrosswalkGREEN1
        //private bool crossroadRightTTopCrosswalkGREEN1;
        public bool CrossroadRightTTopCrosswalkGREEN1
        {
            get { return crossroadRightTTopCrosswalkGREEN1; }

            set
            {
                crossroadRightTTopCrosswalkGREEN1 = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //CrossroadRightTTopCrosswalkRED2
        //private bool crossroadRightTTopCrosswalkRED2;
        public bool CrossroadRightTTopCrosswalkRED2
        {
            get { return crossroadRightTTopCrosswalkRED2; }

            set
            {
                crossroadRightTTopCrosswalkRED2 = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }

        //CrossroadRightTTopCrosswalkGREEN2
        //private bool crossroadRightTTopCrosswalkGREEN2;
        public bool CrossroadRightTTopCrosswalkGREEN2
        {
            get { return crossroadRightTTopCrosswalkGREEN2; }

            set
            {
                crossroadRightTTopCrosswalkGREEN2 = value;

                Invalidate(); //toto tu asi být nemusí 
            }
        }


        #endregion

        ////Edit variables => didn't work properly
        /*
        //Lights
        #region Crossroad Lights 

        //Crossroad1
        #region Crossroad1

        //Top
        #region Top

        public bool Crossroad1TopRED
        {
            get { return crossroad1TopRED; }
            set
            {
                if (value != crossroad1TopRED)
                {
                    crossroad1TopRED = value;
                    Crossroad1TopREDChanged?.Invoke(this, crossroad1TopRED);
                    Invalidate();
                }
            }
        }

        public bool Crossroad1TopGREEN
        {
            get { return crossroad1TopGREEN; }
            set
            {
                if (value != crossroad1TopGREEN)
                {
                    crossroad1TopGREEN = value;
                    Crossroad1TopGREENChanged?.Invoke(this, crossroad1TopGREEN);
                    Invalidate();
                }
            }
        }

        public bool Crossroad1TopYELLOW
        {
            get { return crossroad1TopYELLOW; }
            set
            {
                if (value != crossroad1TopYELLOW)
                {
                    crossroad1TopYELLOW = value;
                    Crossroad1TopYELLOWChanged?.Invoke(this, crossroad1TopYELLOW);
                    Invalidate();
                }
            }
        }

        #endregion

        //Left 
        #region Left
        public bool Crossroad1LeftRED
        {
            get { return crossroad1LeftRED; }
            set
            {
                if (value != crossroad1LeftRED)
                {
                    crossroad1LeftRED = value;
                    Crossroad1LeftREDChanged?.Invoke(this, crossroad1LeftRED);
                    Invalidate();
                }
            }
        }

        public bool Crossroad1LeftGREEN
        {
            get { return crossroad1LeftGREEN; }
            set
            {
                if (value != crossroad1LeftGREEN)
                {
                    crossroad1LeftGREEN = value;
                    Crossroad1LeftGREENChanged?.Invoke(this, crossroad1LeftGREEN);
                    Invalidate();
                }
            }
        }

        public bool Crossroad1LeftYELLOW
        {
            get { return crossroad1LeftYELLOW; }
            set
            {
                if (value != crossroad1LeftYELLOW)
                {
                    crossroad1LeftYELLOW = value;
                    Crossroad1LeftYELLOWChanged?.Invoke(this, crossroad1LeftYELLOW);
                    Invalidate();
                }
            }
        }

        #endregion

        //Right 
        #region Right 
        public bool Crossroad1RightRED
        {
            get { return crossroad1RightRED; }
            set
            {
                if (value != crossroad1RightRED)
                {
                    crossroad1RightRED = value;
                    Crossroad1RightREDChanged?.Invoke(this, crossroad1RightRED);
                    Invalidate();
                }
            }
        }

        public bool Crossroad1RightGREEN
        {
            get { return crossroad1RightGREEN; }
            set
            {
                if (value != crossroad1RightGREEN)
                {
                    crossroad1RightGREEN = value;
                    Crossroad1RightGREENChanged?.Invoke(this, crossroad1RightGREEN);
                    Invalidate();
                }
            }
        }

        public bool Crossroad1RightYELLOW
        {
            get { return crossroad1RightYELLOW; }
            set
            {
                if (value != crossroad1RightYELLOW)
                {
                    crossroad1RightYELLOW = value;
                    Crossroad1RightYELLOWChanged?.Invoke(this, crossroad1RightYELLOW);
                    Invalidate();
                }
            }
        }

        #endregion

        //Bottom 
        #region Bottom 
        public bool Crossroad1BottomRED
        {
            get { return crossroad1BottomRED; }
            set
            {
                if (value != crossroad1BottomRED)
                {
                    crossroad1BottomRED = value;
                    Crossroad1BottomREDChanged?.Invoke(this, crossroad1BottomRED);
                    Invalidate();
                }
            }
        }

        public bool Crossroad1BottomGREEN
        {
            get { return crossroad1BottomGREEN; }
            set
            {
                if (value != crossroad1BottomGREEN)
                {
                    crossroad1BottomGREEN = value;
                    Crossroad1BottomGREENChanged?.Invoke(this, crossroad1BottomGREEN);
                    Invalidate();
                }
            }
        }

        public bool Crossroad1BottomYELLOW
        {
            get { return crossroad1BottomYELLOW; }
            set
            {
                if (value != crossroad1BottomYELLOW)
                {
                    crossroad1BottomYELLOW = value;
                    Crossroad1BottomYELLOWChanged?.Invoke(this, crossroad1BottomYELLOW);
                    Invalidate();
                }
            }
        }

        #endregion

        //Crosswalk Top
        #region Crosswalk Top
        public bool Crossroad1TopCrosswalkRED1
        {
            get { return crossroad1TopCrosswalkRED1; }
            set
            {
                if (value != crossroad1TopCrosswalkRED1)
                {
                    crossroad1TopCrosswalkRED1 = value;
                    Crossroad1TopCrosswalkRED1Changed?.Invoke(this, crossroad1TopCrosswalkRED1);
                    Invalidate();
                }
            }
        }

        public bool Crossroad1TopCrosswalkRED2
        {
            get { return crossroad1TopCrosswalkRED2; }
            set
            {
                if (value != crossroad1TopCrosswalkRED2)
                {
                    crossroad1TopCrosswalkRED2 = value;
                    Crossroad1TopCrosswalkRED2Changed?.Invoke(this, crossroad1TopCrosswalkRED2);
                    Invalidate();
                }
            }
        }

        public bool Crossroad1TopCrosswalkGREEN1
        {
            get { return crossroad1TopCrosswalkGREEN1; }
            set
            {
                if (value != crossroad1TopCrosswalkGREEN1)
                {
                    crossroad1TopCrosswalkGREEN1 = value;
                    Crossroad1TopCrosswalkGREEN1Changed?.Invoke(this, crossroad1TopCrosswalkGREEN1);
                    Invalidate();
                }
            }
        }

        public bool Crossroad1TopCrosswalkGREEN2
        {
            get { return crossroad1TopCrosswalkGREEN2; }
            set
            {
                if (value != crossroad1TopCrosswalkGREEN2)
                {
                    crossroad1TopCrosswalkGREEN2 = value;
                    Crossroad1TopCrosswalkGREEN2Changed?.Invoke(this, crossroad1TopCrosswalkGREEN2);
                    Invalidate();
                }
            }
        }

        #endregion

        //Crosswalk Left
        #region Crosswalk Left 
        public bool Crossroad1LeftCrosswalkRED1
        {
            get { return crossroad1LeftCrosswalkRED1; }
            set
            {
                if (value != crossroad1LeftCrosswalkRED1)
                {
                    crossroad1LeftCrosswalkRED1 = value;
                    Crossroad1LeftCrosswalkRED1Changed?.Invoke(this, crossroad1LeftCrosswalkRED1);
                    Invalidate();
                }
            }
        }

        public bool Crossroad1LeftCrosswalkRED2
        {
            get { return crossroad1LeftCrosswalkRED2; }
            set
            {
                if (value != crossroad1LeftCrosswalkRED2)
                {
                    crossroad1LeftCrosswalkRED2 = value;
                    Crossroad1LeftCrosswalkRED2Changed?.Invoke(this, crossroad1LeftCrosswalkRED2);
                    Invalidate();
                }
            }
        }

        public bool Crossroad1LeftCrosswalkGREEN1
        {
            get { return crossroad1LeftCrosswalkGREEN1; }
            set
            {
                if (value != crossroad1LeftCrosswalkGREEN1)
                {
                    crossroad1LeftCrosswalkGREEN1 = value;
                    Crossroad1LeftCrosswalkGREEN1Changed?.Invoke(this, crossroad1LeftCrosswalkGREEN1);
                    Invalidate();
                }
            }
        }

        public bool Crossroad1LeftCrosswalkGREEN2
        {
            get { return crossroad1LeftCrosswalkGREEN2; }
            set
            {
                if (value != crossroad1LeftCrosswalkGREEN2)
                {
                    crossroad1LeftCrosswalkGREEN2 = value;
                    Crossroad1LeftCrosswalkGREEN2Changed?.Invoke(this, crossroad1LeftCrosswalkGREEN2);
                    Invalidate();
                }
            }
        }

        #endregion


        #endregion

        //Crossroad2
        #region Crossroad2

        //Top
        #region Top

        public bool Crossroad2TopRED
        {
            get { return crossroad2TopRED; }
            set
            {
                if (value != crossroad2TopRED)
                {
                    crossroad2TopRED = value;
                    Crossroad2TopREDChanged?.Invoke(this, crossroad2TopRED);
                    Invalidate();
                }
            }
        }

        public bool Crossroad2TopGREEN
        {
            get { return crossroad2TopGREEN; }
            set
            {
                if (value != crossroad2TopGREEN)
                {
                    crossroad2TopGREEN = value;
                    Crossroad2TopGREENChanged?.Invoke(this, crossroad2TopGREEN);
                    Invalidate();
                }
            }
        }

        public bool Crossroad2TopYELLOW
        {
            get { return crossroad2TopYELLOW; }
            set
            {
                if (value != crossroad2TopYELLOW)
                {
                    crossroad2TopYELLOW = value;
                    Crossroad2TopYELLOWChanged?.Invoke(this, crossroad2TopYELLOW);
                    Invalidate();
                }
            }
        }

        #endregion

        //Left 
        #region Left
        public bool Crossroad2LeftRED
        {
            get { return crossroad2LeftRED; }
            set
            {
                if (value != crossroad2LeftRED)
                {
                    crossroad2LeftRED = value;
                    Crossroad2LeftREDChanged?.Invoke(this, crossroad2LeftRED);
                    Invalidate();
                }
            }
        }

        public bool Crossroad2LeftGREEN
        {
            get { return crossroad2LeftGREEN; }
            set
            {
                if (value != crossroad2LeftGREEN)
                {
                    crossroad2LeftGREEN = value;
                    Crossroad2LeftGREENChanged?.Invoke(this, crossroad2LeftGREEN);
                    Invalidate();
                }
            }
        }

        public bool Crossroad2LeftYELLOW
        {
            get { return crossroad2LeftYELLOW; }
            set
            {
                if (value != crossroad2LeftYELLOW)
                {
                    crossroad2LeftYELLOW = value;
                    Crossroad2LeftYELLOWChanged?.Invoke(this, crossroad2LeftYELLOW);
                    Invalidate();
                }
            }
        }

        #endregion

        //Right 
        #region Right 
        public bool Crossroad2RightRED
        {
            get { return crossroad2RightRED; }
            set
            {
                if (value != crossroad2RightRED)
                {
                    crossroad2RightRED = value;
                    Crossroad2RightREDChanged?.Invoke(this, crossroad2RightRED);
                    Invalidate();
                }
            }
        }

        public bool Crossroad2RightGREEN
        {
            get { return crossroad2RightGREEN; }
            set
            {
                if (value != crossroad2RightGREEN)
                {
                    crossroad2RightGREEN = value;
                    Crossroad2RightGREENChanged?.Invoke(this, crossroad2RightGREEN);
                    Invalidate();
                }
            }
        }

        public bool Crossroad2RightYELLOW
        {
            get { return crossroad2RightYELLOW; }
            set
            {
                if (value != crossroad2RightYELLOW)
                {
                    crossroad2RightYELLOW = value;
                    Crossroad2RightYELLOWChanged?.Invoke(this, crossroad2RightYELLOW);
                    Invalidate();
                }
            }
        }

        #endregion

        //Bottom 
        #region Bottom 
        public bool Crossroad2BottomRED
        {
            get { return crossroad2BottomRED; }
            set
            {
                if (value != crossroad2BottomRED)
                {
                    crossroad2BottomRED = value;
                    Crossroad2BottomREDChanged?.Invoke(this, crossroad2BottomRED);
                    Invalidate();
                }
            }
        }

        public bool Crossroad2BottomGREEN
        {
            get { return crossroad2BottomGREEN; }
            set
            {
                if (value != crossroad2BottomGREEN)
                {
                    crossroad2BottomGREEN = value;
                    Crossroad2BottomGREENChanged?.Invoke(this, crossroad2BottomGREEN);
                    Invalidate();
                }
            }
        }

        public bool Crossroad2BottomYELLOW
        {
            get { return crossroad2BottomYELLOW; }
            set
            {
                if (value != crossroad2BottomYELLOW)
                {
                    crossroad2BottomYELLOW = value;
                    Crossroad2BottomYELLOWChanged?.Invoke(this, crossroad2BottomYELLOW);
                    Invalidate();
                }
            }
        }

        #endregion

        //Crosswalk Left
        #region Crosswalk Left
        public bool Crossroad2LeftCrosswalkRED1
        {
            get { return crossroad2LeftCrosswalkRED1; }
            set
            {
                if (value != crossroad2LeftCrosswalkRED1)
                {
                    crossroad2LeftCrosswalkRED1 = value;
                    Crossroad2LeftCrosswalkRED1Changed?.Invoke(this, crossroad2LeftCrosswalkRED1);
                    Invalidate();
                }
            }
        }

        public bool Crossroad2LeftCrosswalkRED2
        {
            get { return crossroad2LeftCrosswalkRED2; }
            set
            {
                if (value != crossroad2LeftCrosswalkRED2)
                {
                    crossroad2LeftCrosswalkRED2 = value;
                    Crossroad2LeftCrosswalkRED2Changed?.Invoke(this, crossroad2LeftCrosswalkRED2);
                    Invalidate();
                }
            }
        }

        public bool Crossroad2LeftCrosswalkGREEN1
        {
            get { return crossroad2LeftCrosswalkGREEN1; }
            set
            {
                if (value != crossroad2LeftCrosswalkGREEN1)
                {
                    crossroad2LeftCrosswalkGREEN1 = value;
                    Crossroad2LeftCrosswalkGREEN1Changed?.Invoke(this, crossroad2LeftCrosswalkGREEN1);
                    Invalidate();
                }
            }
        }

        public bool Crossroad2LeftCrosswalkGREEN2
        {
            get { return crossroad2LeftCrosswalkGREEN2; }
            set
            {
                if (value != crossroad2LeftCrosswalkGREEN2)
                {
                    crossroad2LeftCrosswalkGREEN2 = value;
                    Crossroad2LeftCrosswalkGREEN2Changed?.Invoke(this, crossroad2LeftCrosswalkGREEN2);
                    Invalidate();
                }
            }
        }

        #endregion

        //Crosswalk Right
        #region Crosswalk Right 
        public bool Crossroad2RightCrosswalkRED1
        {
            get { return crossroad2RightCrosswalkRED1; }
            set
            {
                if (value != crossroad2RightCrosswalkRED1)
                {
                    crossroad2RightCrosswalkRED1 = value;
                    Crossroad2RightCrosswalkRED1Changed?.Invoke(this, crossroad2RightCrosswalkRED1);
                    Invalidate();
                }
            }
        }

        public bool Crossroad2RightCrosswalkRED2
        {
            get { return crossroad2RightCrosswalkRED2; }
            set
            {
                if (value != crossroad2RightCrosswalkRED2)
                {
                    crossroad2RightCrosswalkRED2 = value;
                    Crossroad2RightCrosswalkRED2Changed?.Invoke(this, crossroad2RightCrosswalkRED2);
                    Invalidate();
                }
            }
        }

        public bool Crossroad2RightCrosswalkGREEN1
        {
            get { return crossroad2RightCrosswalkGREEN1; }
            set
            {
                if (value != crossroad2RightCrosswalkGREEN1)
                {
                    crossroad2RightCrosswalkGREEN1 = value;
                    Crossroad2RightCrosswalkGREEN1Changed?.Invoke(this, crossroad2RightCrosswalkGREEN1);
                    Invalidate();
                }
            }
        }

        public bool Crossroad2RightCrosswalkGREEN2
        {
            get { return crossroad2RightCrosswalkGREEN2; }
            set
            {
                if (value != crossroad2RightCrosswalkGREEN2)
                {
                    crossroad2RightCrosswalkGREEN2 = value;
                    Crossroad2RightCrosswalkGREEN2Changed?.Invoke(this, crossroad2RightCrosswalkGREEN2);
                    Invalidate();
                }
            }
        }

        #endregion

        #endregion

        //Crossroad LeftT
        #region Crossroad LeftT

        //Top
        #region Top

        public bool CrossroadLeftTTopRED
        {
            get { return crossroadLeftTTopRED; }
            set
            {
                if (value != crossroadLeftTTopRED)
                {
                    crossroadLeftTTopRED = value;
                    CrossroadLeftTTopREDChanged?.Invoke(this, crossroadLeftTTopRED);
                    Invalidate();
                }
            }
        }

        public bool CrossroadLeftTTopGREEN
        {
            get { return crossroadLeftTTopGREEN; }
            set
            {
                if (value != crossroadLeftTTopGREEN)
                {
                    crossroadLeftTTopGREEN = value;
                    CrossroadLeftTTopGREENChanged?.Invoke(this, crossroadLeftTTopGREEN);
                    Invalidate();
                }
            }
        }

        public bool CrossroadLeftTTopYELLOW
        {
            get { return crossroadLeftTTopYELLOW; }
            set
            {
                if (value != crossroadLeftTTopYELLOW)
                {
                    crossroadLeftTTopYELLOW = value;
                    CrossroadLeftTTopYELLOWChanged?.Invoke(this, crossroadLeftTTopYELLOW);
                    Invalidate();
                }
            }
        }

        #endregion

        //Left 
        #region Left
        public bool CrossroadLeftTLeftRED
        {
            get { return crossroadLeftTLeftRED; }
            set
            {
                if (value != crossroadLeftTLeftRED)
                {
                    crossroadLeftTLeftRED = value;
                    CrossroadLeftTLeftREDChanged?.Invoke(this, crossroadLeftTLeftRED);
                    Invalidate();
                }
            }
        }

        public bool CrossroadLeftTLeftGREEN
        {
            get { return crossroadLeftTLeftGREEN; }
            set
            {
                if (value != crossroadLeftTLeftGREEN)
                {
                    crossroadLeftTLeftGREEN = value;
                    CrossroadLeftTLeftGREENChanged?.Invoke(this, crossroadLeftTLeftGREEN);
                    Invalidate();
                }
            }
        }

        public bool CrossroadLeftTLeftYELLOW
        {
            get { return crossroadLeftTLeftYELLOW; }
            set
            {
                if (value != crossroadLeftTLeftYELLOW)
                {
                    crossroadLeftTLeftYELLOW = value;
                    CrossroadLeftTLeftYELLOWChanged?.Invoke(this, crossroadLeftTLeftYELLOW);
                    Invalidate();
                }
            }
        }

        #endregion

        //Right 
        #region Right 
        public bool CrossroadLeftTRightRED
        {
            get { return crossroadLeftTRightRED; }
            set
            {
                if (value != crossroadLeftTRightRED)
                {
                    crossroadLeftTRightRED = value;
                    CrossroadLeftTRightREDChanged?.Invoke(this, crossroadLeftTRightRED);
                    Invalidate();
                }
            }
        }

        public bool CrossroadLeftTRightGREEN
        {
            get { return crossroadLeftTRightGREEN; }
            set
            {
                if (value != crossroadLeftTRightGREEN)
                {
                    crossroadLeftTRightGREEN = value;
                    CrossroadLeftTRightGREENChanged?.Invoke(this, crossroadLeftTRightGREEN);
                    Invalidate();
                }
            }
        }

        public bool CrossroadLeftTRightYELLOW
        {
            get { return crossroadLeftTRightYELLOW; }
            set
            {
                if (value != crossroadLeftTRightYELLOW)
                {
                    crossroadLeftTRightYELLOW = value;
                    CrossroadLeftTRightYELLOWChanged?.Invoke(this, crossroadLeftTRightYELLOW);
                    Invalidate();
                }
            }
        }

        #endregion

        //Crosswalk Left
        #region Crosswalk Left 
        public bool CrossroadLeftTLeftCrosswalkRED1
        {
            get { return crossroadLeftTLeftCrosswalkRED1; }
            set
            {
                if (value != crossroadLeftTLeftCrosswalkRED1)
                {
                    crossroadLeftTLeftCrosswalkRED1 = value;
                    CrossroadLeftTLeftCrosswalkRED1Changed?.Invoke(this, crossroadLeftTLeftCrosswalkRED1);
                    Invalidate();
                }
            }
        }

        public bool CrossroadLeftTLeftCrosswalkRED2
        {
            get { return crossroadLeftTLeftCrosswalkRED2; }
            set
            {
                if (value != crossroadLeftTLeftCrosswalkRED2)
                {
                    crossroadLeftTLeftCrosswalkRED2 = value;
                    CrossroadLeftTLeftCrosswalkRED2Changed?.Invoke(this, crossroadLeftTLeftCrosswalkRED2);
                    Invalidate();
                }
            }
        }

        public bool CrossroadLeftTLeftCrosswalkGREEN1
        {
            get { return crossroadLeftTLeftCrosswalkGREEN1; }
            set
            {
                if (value != crossroadLeftTLeftCrosswalkGREEN1)
                {
                    crossroadLeftTLeftCrosswalkGREEN1 = value;
                    CrossroadLeftTLeftCrosswalkGREEN1Changed?.Invoke(this, crossroadLeftTLeftCrosswalkGREEN1);
                    Invalidate();
                }
            }
        }

        public bool CrossroadLeftTLeftCrosswalkGREEN2
        {
            get { return crossroadLeftTLeftCrosswalkGREEN2; }
            set
            {
                if (value != crossroadLeftTLeftCrosswalkGREEN2)
                {
                    crossroadLeftTLeftCrosswalkGREEN2 = value;
                    CrossroadLeftTLeftCrosswalkGREEN2Changed?.Invoke(this, crossroadLeftTLeftCrosswalkGREEN2);
                    Invalidate();
                }
            }
        }

        #endregion

        #endregion

        //Crossroad RightT 
        #region Crossroad RightT

        //Top
        #region Top

        public bool CrossroadRightTTopRED
        {
            get { return crossroadRightTTopRED; }
            set
            {
                if (value != crossroadRightTTopRED)
                {
                    crossroadRightTTopRED = value;
                    CrossroadRightTTopREDChanged?.Invoke(this, crossroadRightTTopRED);
                    Invalidate();
                }
            }
        }

        public bool CrossroadRightTTopGREEN
        {
            get { return crossroadRightTTopGREEN; }
            set
            {
                if (value != crossroadRightTTopGREEN)
                {
                    crossroadRightTTopGREEN = value;
                    CrossroadRightTTopGREENChanged?.Invoke(this, crossroadRightTTopGREEN);
                    Invalidate();
                }
            }
        }

        public bool CrossroadRightTTopYELLOW
        {
            get { return crossroadRightTTopYELLOW; }
            set
            {
                if (value != crossroadRightTTopYELLOW)
                {
                    crossroadRightTTopYELLOW = value;
                    CrossroadRightTTopYELLOWChanged?.Invoke(this, crossroadRightTTopYELLOW);
                    Invalidate();
                }
            }
        }

        #endregion

        //Left 
        #region Left
        public bool CrossroadRightTLeftRED
        {
            get { return crossroadRightTLeftRED; }
            set
            {
                if (value != crossroadRightTLeftRED)
                {
                    crossroadRightTLeftRED = value;
                    CrossroadRightTLeftREDChanged?.Invoke(this, crossroadRightTLeftRED);
                    Invalidate();
                }
            }
        }

        public bool CrossroadRightTLeftGREEN
        {
            get { return crossroadRightTLeftGREEN; }
            set
            {
                if (value != crossroadRightTLeftGREEN)
                {
                    crossroadRightTLeftGREEN = value;
                    CrossroadRightTLeftGREENChanged?.Invoke(this, crossroadRightTLeftGREEN);
                    Invalidate();
                }
            }
        }

        public bool CrossroadRightTLeftYELLOW
        {
            get { return crossroadRightTLeftYELLOW; }
            set
            {
                if (value != crossroadRightTLeftYELLOW)
                {
                    crossroadRightTLeftYELLOW = value;
                    CrossroadRightTLeftYELLOWChanged?.Invoke(this, crossroadRightTLeftYELLOW);
                    Invalidate();
                }
            }
        }

        #endregion

        //Right 
        #region Right 
        public bool CrossroadRightTRightRED
        {
            get { return crossroadRightTRightRED; }
            set
            {
                if (value != crossroadRightTRightRED)
                {
                    crossroadRightTRightRED = value;
                    CrossroadRightTRightREDChanged?.Invoke(this, crossroadRightTRightRED);
                    Invalidate();
                }
            }
        }

        public bool CrossroadRightTRightGREEN
        {
            get { return crossroadRightTRightGREEN; }
            set
            {
                if (value != crossroadRightTRightGREEN)
                {
                    crossroadRightTRightGREEN = value;
                    CrossroadRightTRightGREENChanged?.Invoke(this, crossroadRightTRightGREEN);
                    Invalidate();
                }
            }
        }

        public bool CrossroadRightTRightYELLOW
        {
            get { return crossroadRightTRightYELLOW; }
            set
            {
                if (value != crossroadRightTRightYELLOW)
                {
                    crossroadRightTRightYELLOW = value;
                    CrossroadRightTRightYELLOWChanged?.Invoke(this, crossroadRightTRightYELLOW);
                    Invalidate();
                }
            }
        }

        #endregion

        //Crosswalk Top
        #region Crosswalk Top 
        public bool CrossroadRightTTopCrosswalkRED1
        {
            get { return crossroadRightTTopCrosswalkRED1; }
            set
            {
                if (value != crossroadRightTTopCrosswalkRED1)
                {
                    crossroadRightTTopCrosswalkRED1 = value;
                    CrossroadRightTTopCrosswalkRED1Changed?.Invoke(this, crossroadRightTTopCrosswalkRED1);
                    Invalidate();
                }
            }
        }

        public bool CrossroadRightTTopCrosswalkRED2
        {
            get { return crossroadRightTTopCrosswalkRED2; }
            set
            {
                if (value != crossroadRightTTopCrosswalkRED2)
                {
                    crossroadRightTTopCrosswalkRED2 = value;
                    CrossroadRightTTopCrosswalkRED2Changed?.Invoke(this, crossroadRightTTopCrosswalkRED2);
                    Invalidate();
                }
            }
        }

        public bool CrossroadRightTTopCrosswalkGREEN1
        {
            get { return crossroadRightTTopCrosswalkGREEN1; }
            set
            {
                if (value != crossroadRightTTopCrosswalkGREEN1)
                {
                    crossroadRightTTopCrosswalkGREEN1 = value;
                    CrossroadRightTTopCrosswalkGREEN1Changed?.Invoke(this, crossroadRightTTopCrosswalkGREEN1);
                    Invalidate();
                }
            }
        }

        public bool CrossroadRightTTopCrosswalkGREEN2
        {
            get { return crossroadRightTTopCrosswalkGREEN2; }
            set
            {
                if (value != crossroadRightTTopCrosswalkGREEN2)
                {
                    crossroadRightTTopCrosswalkGREEN2 = value;
                    CrossroadRightTTopCrosswalkGREEN2Changed?.Invoke(this, crossroadRightTTopCrosswalkGREEN2);
                    Invalidate();
                }
            }
        }

        #endregion

        #endregion

        #endregion

        */

        public UserControlCrossroad() //ChooseOptionForm chooseOptionFormInstance x Program3Form program3FormInstance
        {
            InitializeComponent();
            
            //ButtonClicked += (sender, identifier) => {};

            DoubleBuffered = true;
            Paint += UserControlCrossroad_Paint;
        }

        public void SetControl(Program3Form program3FormInstance)
        {
            InitializeButtons();

            this.program3FormInstance = program3FormInstance;

            client = program3FormInstance.client;

            //Buffers initialize => totally useless
            #region Buffers initialize

            //DB14 => Crossroad_DB
            read_buffer_DB14 = program3FormInstance.read_buffer_DB14; //read_buffer is not needed
            send_buffer_DB14 = program3FormInstance.send_buffer_DB14;
            //DB1 => Crossroad_1_DB
            read_buffer_DB1 = program3FormInstance.read_buffer_DB1; //read_buffer is not needed
            send_buffer_DB1 = program3FormInstance.send_buffer_DB1;
            //DB19 => Crossroad_2_DB
            read_buffer_DB19 = program3FormInstance.read_buffer_DB19; //read_buffer is not needed
            send_buffer_DB19 = program3FormInstance.send_buffer_DB19;
            //DB20 => Crossroad_LeftT_DB
            read_buffer_DB20 = program3FormInstance.read_buffer_DB20; //read_buffer is not needed
            send_buffer_DB20 = program3FormInstance.send_buffer_DB20;
            //DB21 => Crossroad_RightT_DB
            read_buffer_DB21 = program3FormInstance.read_buffer_DB21; //read_buffer is not needed
            send_buffer_DB21 = program3FormInstance.send_buffer_DB21;

            #endregion
        }

        private void UserControlCrossroad_Paint(object? sender, PaintEventArgs e)
        {
            if (program3FormInstance == null)
                return;

            var g = e.Graphics;

            //background
            g.Clear(Color.Black);

            Draw(g);

            //pen color
            Pen BlackPen = new Pen(Color.Black, 2);
            Pen WhitePen = new Pen(Color.White, 2);
            Pen RedPen = new Pen(Color.Red, 2);
            Pen GreenPen = new Pen(Color.Green, 2);
            Pen YellowPen = new Pen(Color.Yellow, 2);

            //Drawing the selected variant of crossroad
            #region Drawing the selected variant of crossroad

            if (drawBasicCrossroad) //Crossroad1
            {
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

                //Conditions for lights based on value
                #region Conditions for lights based on value 

                //Crossroad1
                #region Crossroad1
                /*
                UpdateTrafficLightsCrossroad1TOP(program3FormInstance.Crossroad1TopRED, program3FormInstance.Crossroad1TopYELLOW, program3FormInstance.Crossroad1TopGREEN);
                UpdateTrafficLightsCrossroad1LEFT(program3FormInstance.Crossroad1LeftRED, program3FormInstance.Crossroad1LeftYELLOW, program3FormInstance.Crossroad1LeftGREEN);
                UpdateTrafficLightsCrossroad1RIGHT(program3FormInstance.Crossroad1RightRED, program3FormInstance.Crossroad1RightYELLOW, program3FormInstance.Crossroad1RightGREEN);
                UpdateTrafficLightsCrossroad1BOTTOM(program3FormInstance.Crossroad1BottomRED, program3FormInstance.Crossroad1BottomYELLOW, program3FormInstance.Crossroad1BottomGREEN);
                UpdateCrosswalkLightsCrossroad1TOP(program3FormInstance.Crossroad1TopCrosswalkRED1, program3FormInstance.Crossroad1TopCrosswalkGREEN1, program3FormInstance.Crossroad1TopCrosswalkRED2, program3FormInstance.Crossroad1TopCrosswalkGREEN2);
                UpdateCrosswalkLightsCrossroad1LEFT(program3FormInstance.Crossroad1LeftCrosswalkRED1, program3FormInstance.Crossroad1LeftCrosswalkGREEN1, program3FormInstance.Crossroad1LeftCrosswalkRED2, program3FormInstance.Crossroad1LeftCrosswalkGREEN2);
                */

                //Top
                #region Top
                if (Crossroad1TopRED)
                {
                    g.FillEllipse(red, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
                }

                if (Crossroad1TopYELLOW)
                {
                    g.FillEllipse(yellow, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                }

                if (Crossroad1TopGREEN)
                {
                    g.FillEllipse(green, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                }

                #endregion

                //Left
                #region Left
                if (Crossroad1LeftRED)
                {
                    g.FillEllipse(red, x + length + 3 * FreeSpace + TrafficLights_width * 2, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(white, x + length + 3 * FreeSpace + TrafficLights_width * 2, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                }

                if (Crossroad1LeftYELLOW)
                {
                    g.FillEllipse(yellow, x + length + 3 * FreeSpace + TrafficLights_width, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(white, x + length + 3 * FreeSpace + TrafficLights_width, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                }

                if (Crossroad1LeftGREEN)
                {
                    g.FillEllipse(green, x + length + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(white, x + length + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                }

                #endregion

                //Right
                #region Right
                if (Crossroad1RightRED)
                {
                    g.FillEllipse(red, x + length * 6 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 6 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                }

                if (Crossroad1RightYELLOW)
                {
                    g.FillEllipse(yellow, x + length * 6 + FreeSpace + TrafficLights_width, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 6 + FreeSpace + TrafficLights_width, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                }

                if (Crossroad1RightGREEN)
                {
                    g.FillEllipse(green, x + length * 6 + FreeSpace + TrafficLights_width * 2, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(green, x + length * 6 + FreeSpace + TrafficLights_width * 2, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                }
                #endregion

                //Bottom
                #region Bottom
                if (Crossroad1BottomRED)
                {
                    g.FillEllipse(red, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_width);
                }

                if (Crossroad1BottomYELLOW)
                {
                    g.FillEllipse(yellow, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                }

                if (Crossroad1BottomGREEN)
                {
                    g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
                }

                #endregion

                //Crossroad1 Top Crosswalk
                #region Crossroad1 Top Crosswalk

                if (Crossroad1TopCrosswalkRED1)
                {
                    g.FillEllipse(red, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }

                if (Crossroad1TopCrosswalkRED2)
                {
                    g.FillEllipse(red, x + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }

                if (Crossroad1TopCrosswalkGREEN1)
                {
                    g.FillEllipse(green, x + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }

                if (Crossroad1TopCrosswalkGREEN2)
                {
                    g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 5 + FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }

                #endregion

                //Crossroad1 Left Crosswalk
                #region Crossroad1 Left Crosswalk

                if (Crossroad1LeftCrosswalkRED1)
                {
                    g.FillEllipse(red, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }

                if (Crossroad1LeftCrosswalkRED2)
                {
                    g.FillEllipse(red, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }

                if (Crossroad1LeftCrosswalkGREEN1)
                {
                    g.FillEllipse(green, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }

                if (Crossroad1LeftCrosswalkGREEN2)
                {
                    g.FillEllipse(green, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }

                #endregion

                #endregion

                #endregion

            }
            else if (drawCrossroadExtension1) //Crossroad1 + Crossorad2
            {
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

                //Conditions for lights based on value
                #region Conditions for lights based on value

                //Crossroad1
                #region Crossroad1
                /*
                UpdateTrafficLightsCrossroad1TOP(program3FormInstance.Crossroad1TopRED, program3FormInstance.Crossroad1TopYELLOW, program3FormInstance.Crossroad1TopGREEN);
                UpdateTrafficLightsCrossroad1LEFT(program3FormInstance.Crossroad1LeftRED, program3FormInstance.Crossroad1LeftYELLOW, program3FormInstance.Crossroad1LeftGREEN);
                UpdateTrafficLightsCrossroad1RIGHT(program3FormInstance.Crossroad1RightRED, program3FormInstance.Crossroad1RightYELLOW, program3FormInstance.Crossroad1RightGREEN);
                UpdateTrafficLightsCrossroad1BOTTOM(program3FormInstance.Crossroad1BottomRED, program3FormInstance.Crossroad1BottomYELLOW, program3FormInstance.Crossroad1BottomGREEN);
                UpdateCrosswalkLightsCrossroad1TOP(program3FormInstance.Crossroad1TopCrosswalkRED1, program3FormInstance.Crossroad1TopCrosswalkGREEN1, program3FormInstance.Crossroad1TopCrosswalkRED2, program3FormInstance.Crossroad1TopCrosswalkGREEN2);
                UpdateCrosswalkLightsCrossroad1LEFT(program3FormInstance.Crossroad1LeftCrosswalkRED1, program3FormInstance.Crossroad1LeftCrosswalkGREEN1, program3FormInstance.Crossroad1LeftCrosswalkRED2, program3FormInstance.Crossroad1LeftCrosswalkGREEN2);
                */

                //Top
                #region Top
                if (Crossroad1TopRED)
                {
                    g.FillEllipse(red, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
                }

                if (Crossroad1TopYELLOW)
                {
                    g.FillEllipse(yellow, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                }

                if (Crossroad1TopGREEN)
                {
                    g.FillEllipse(green, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                }

                #endregion

                //Left
                #region Left
                if (Crossroad1LeftRED)
                {
                    g.FillEllipse(red, x + length + 3 * FreeSpace + TrafficLights_width * 2, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(white, x + length + 3 * FreeSpace + TrafficLights_width * 2, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                }

                if (Crossroad1LeftYELLOW)
                {
                    g.FillEllipse(yellow, x + length + 3 * FreeSpace + TrafficLights_width, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(white, x + length + 3 * FreeSpace + TrafficLights_width, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                }

                if (Crossroad1LeftGREEN)
                {
                    g.FillEllipse(green, x + length + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(white, x + length + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                }

                #endregion

                //Right
                #region Right
                if (Crossroad1RightRED)
                {
                    g.FillEllipse(red, x + length * 6 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 6 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                }

                if (Crossroad1RightYELLOW)
                {
                    g.FillEllipse(yellow, x + length * 6 + FreeSpace + TrafficLights_width, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 6 + FreeSpace + TrafficLights_width, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                }

                if (Crossroad1RightGREEN)
                {
                    g.FillEllipse(green, x + length * 6 + FreeSpace + TrafficLights_width * 2, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(green, x + length * 6 + FreeSpace + TrafficLights_width * 2, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                }
                #endregion

                //Bottom
                #region Bottom
                if (Crossroad1BottomRED)
                {
                    g.FillEllipse(red, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_width);
                }

                if (Crossroad1BottomYELLOW)
                {
                    g.FillEllipse(yellow, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                }

                if (Crossroad1BottomGREEN)
                {
                    g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
                }

                #endregion

                //Crossroad1 Top Crosswalk
                #region Crossroad1 Top Crosswalk

                if (Crossroad1TopCrosswalkRED1)
                {
                    g.FillEllipse(red, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }

                if (Crossroad1TopCrosswalkRED2)
                {
                    g.FillEllipse(red, x + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }

                if (Crossroad1TopCrosswalkGREEN1)
                {
                    g.FillEllipse(green, x + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }

                if (Crossroad1TopCrosswalkGREEN2)
                {
                    g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 5 + FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }

                #endregion

                //Crossroad1 Left Crosswalk
                #region Crossroad1 Left Crosswalk

                if (Crossroad1LeftCrosswalkRED1)
                {
                    g.FillEllipse(red, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }

                if (Crossroad1LeftCrosswalkRED2)
                {
                    g.FillEllipse(red, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }

                if (Crossroad1LeftCrosswalkGREEN1)
                {
                    g.FillEllipse(green, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }

                if (Crossroad1LeftCrosswalkGREEN2)
                {
                    g.FillEllipse(green, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }

                #endregion

                #endregion

                //Crossroad2
                #region Crossroad2
                /*
                UpdateTrafficLightsCrossroad2TOP(program3FormInstance.Crossroad2TopRED, program3FormInstance.Crossroad2TopYELLOW, program3FormInstance.Crossroad2TopGREEN);
                UpdateTrafficLightsCrossroad2LEFT(program3FormInstance.Crossroad2LeftRED, program3FormInstance.Crossroad2LeftYELLOW, program3FormInstance.Crossroad2LeftGREEN);
                UpdateTrafficLightsCrossroad2RIGHT(program3FormInstance.Crossroad2RightRED, program3FormInstance.Crossroad2RightYELLOW, program3FormInstance.Crossroad2RightGREEN);
                UpdateTrafficLightsCrossroad2BOTTOM(program3FormInstance.Crossroad2BottomRED, program3FormInstance.Crossroad2BottomYELLOW, program3FormInstance.Crossroad2BottomGREEN);
                UpdateCrosswalkLightsCrossroad2LEFT(program3FormInstance.Crossroad2LeftCrosswalkRED1, program3FormInstance.Crossroad2LeftCrosswalkGREEN1, program3FormInstance.Crossroad2LeftCrosswalkRED2, program3FormInstance.Crossroad2LeftCrosswalkGREEN2);
                UpdateCrosswalkLightsCrossroad2RIGHT(program3FormInstance.Crossroad2RightCrosswalkRED1, program3FormInstance.Crossroad2RightCrosswalkGREEN1, program3FormInstance.Crossroad2RightCrosswalkRED2, program3FormInstance.Crossroad2RightCrosswalkGREEN2);
                */

                #endregion

                #endregion
            }
            else if (drawCrossroadExtension2) //Crossroad1 + Crossorad2 + Left T
            {
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

                //Conditions for lights based on value
                #region Conditions for lights based on value 

                //Crossroad1
                #region Crossroad1
                /*
                UpdateTrafficLightsCrossroad1TOP(program3FormInstance.Crossroad1TopRED, program3FormInstance.Crossroad1TopYELLOW, program3FormInstance.Crossroad1TopGREEN);
                UpdateTrafficLightsCrossroad1LEFT(program3FormInstance.Crossroad1LeftRED, program3FormInstance.Crossroad1LeftYELLOW, program3FormInstance.Crossroad1LeftGREEN);
                UpdateTrafficLightsCrossroad1RIGHT(program3FormInstance.Crossroad1RightRED, program3FormInstance.Crossroad1RightYELLOW, program3FormInstance.Crossroad1RightGREEN);
                UpdateTrafficLightsCrossroad1BOTTOM(program3FormInstance.Crossroad1BottomRED, program3FormInstance.Crossroad1BottomYELLOW, program3FormInstance.Crossroad1BottomGREEN);
                UpdateCrosswalkLightsCrossroad1TOP(program3FormInstance.Crossroad1TopCrosswalkRED1, program3FormInstance.Crossroad1TopCrosswalkGREEN1, program3FormInstance.Crossroad1TopCrosswalkRED2, program3FormInstance.Crossroad1TopCrosswalkGREEN2);
                UpdateCrosswalkLightsCrossroad1LEFT(program3FormInstance.Crossroad1LeftCrosswalkRED1, program3FormInstance.Crossroad1LeftCrosswalkGREEN1, program3FormInstance.Crossroad1LeftCrosswalkRED2, program3FormInstance.Crossroad1LeftCrosswalkGREEN2);
                */

                //Top
                #region Top
                if (Crossroad1TopRED)
                {
                    g.FillEllipse(red, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
                }

                if (Crossroad1TopYELLOW)
                {
                    g.FillEllipse(yellow, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                }

                if (Crossroad1TopGREEN)
                {
                    g.FillEllipse(green, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                }

                #endregion

                //Left
                #region Left
                if (Crossroad1LeftRED)
                {
                    g.FillEllipse(red, x + length + 3 * FreeSpace + TrafficLights_width * 2, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(white, x + length + 3 * FreeSpace + TrafficLights_width * 2, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                }

                if (Crossroad1LeftYELLOW)
                {
                    g.FillEllipse(yellow, x + length + 3 * FreeSpace + TrafficLights_width, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(white, x + length + 3 * FreeSpace + TrafficLights_width, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                }

                if (Crossroad1LeftGREEN)
                {
                    g.FillEllipse(green, x + length + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(white, x + length + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                }

                #endregion

                //Right
                #region Right
                if (Crossroad1RightRED)
                {
                    g.FillEllipse(red, x + length * 6 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 6 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                }

                if (Crossroad1RightYELLOW)
                {
                    g.FillEllipse(yellow, x + length * 6 + FreeSpace + TrafficLights_width, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 6 + FreeSpace + TrafficLights_width, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                }

                if (Crossroad1RightGREEN)
                {
                    g.FillEllipse(green, x + length * 6 + FreeSpace + TrafficLights_width * 2, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(green, x + length * 6 + FreeSpace + TrafficLights_width * 2, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                }
                #endregion

                //Bottom
                #region Bottom
                if (Crossroad1BottomRED)
                {
                    g.FillEllipse(red, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_width);
                }

                if (Crossroad1BottomYELLOW)
                {
                    g.FillEllipse(yellow, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                }

                if (Crossroad1BottomGREEN)
                {
                    g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
                }

                #endregion

                //Crossroad1 Top Crosswalk
                #region Crossroad1 Top Crosswalk

                if (Crossroad1TopCrosswalkRED1)
                {
                    g.FillEllipse(red, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }

                if (Crossroad1TopCrosswalkRED2)
                {
                    g.FillEllipse(red, x + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }

                if (Crossroad1TopCrosswalkGREEN1)
                {
                    g.FillEllipse(green, x + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }

                if (Crossroad1TopCrosswalkGREEN2)
                {
                    g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 5 + FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }

                #endregion

                //Crossroad1 Left Crosswalk
                #region Crossroad1 Left Crosswalk

                if (Crossroad1LeftCrosswalkRED1)
                {
                    g.FillEllipse(red, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }

                if (Crossroad1LeftCrosswalkRED2)
                {
                    g.FillEllipse(red, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }

                if (Crossroad1LeftCrosswalkGREEN1)
                {
                    g.FillEllipse(green, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }

                if (Crossroad1LeftCrosswalkGREEN2)
                {
                    g.FillEllipse(green, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }

                #endregion

                #endregion

                //Crossroad2
                #region Crossroad2
                /*
                UpdateTrafficLightsCrossroad2TOP(program3FormInstance.Crossroad2TopRED, program3FormInstance.Crossroad2TopYELLOW, program3FormInstance.Crossroad2TopGREEN);
                UpdateTrafficLightsCrossroad2LEFT(program3FormInstance.Crossroad2LeftRED, program3FormInstance.Crossroad2LeftYELLOW, program3FormInstance.Crossroad2LeftGREEN);
                UpdateTrafficLightsCrossroad2RIGHT(program3FormInstance.Crossroad2RightRED, program3FormInstance.Crossroad2RightYELLOW, program3FormInstance.Crossroad2RightGREEN);
                UpdateTrafficLightsCrossroad2BOTTOM(program3FormInstance.Crossroad2BottomRED, program3FormInstance.Crossroad2BottomYELLOW, program3FormInstance.Crossroad2BottomGREEN);
                UpdateCrosswalkLightsCrossroad2LEFT(program3FormInstance.Crossroad2LeftCrosswalkRED1, program3FormInstance.Crossroad2LeftCrosswalkGREEN1, program3FormInstance.Crossroad2LeftCrosswalkRED2, program3FormInstance.Crossroad2LeftCrosswalkGREEN2);
                UpdateCrosswalkLightsCrossroad2RIGHT(program3FormInstance.Crossroad2RightCrosswalkRED1, program3FormInstance.Crossroad2RightCrosswalkGREEN1, program3FormInstance.Crossroad2RightCrosswalkRED2, program3FormInstance.Crossroad2RightCrosswalkGREEN2);
                */

                //Top
                #region Top
                if (Crossroad2TopRED)
                {
                    g.FillEllipse(red, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
                }

                if (Crossroad2TopYELLOW)
                {
                    g.FillEllipse(yellow, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                }

                if (Crossroad2TopGREEN)
                {
                    g.FillEllipse(green, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                }

                #endregion

                //Left
                #region Left
                if (Crossroad2LeftRED)
                {
                    g.FillEllipse(red, x + length + 3 * FreeSpace + TrafficLights_width * 2, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(white, x + length + 3 * FreeSpace + TrafficLights_width * 2, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                }

                if (Crossroad2LeftYELLOW)
                {
                    g.FillEllipse(yellow, x + length + 3 * FreeSpace + TrafficLights_width, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(white, x + length + 3 * FreeSpace + TrafficLights_width, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                }

                if (Crossroad2LeftGREEN)
                {
                    g.FillEllipse(green, x + length + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(white, x + length + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                }

                #endregion

                //Right
                #region Right
                if (Crossroad2RightRED)
                {
                    g.FillEllipse(red, x + length * 6 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 6 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                }

                if (Crossroad2RightYELLOW)
                {
                    g.FillEllipse(yellow, x + length * 6 + FreeSpace + TrafficLights_width, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 6 + FreeSpace + TrafficLights_width, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                }

                if (Crossroad2RightGREEN)
                {
                    g.FillEllipse(green, x + length * 6 + FreeSpace + TrafficLights_width * 2, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(green, x + length * 6 + FreeSpace + TrafficLights_width * 2, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                }
                #endregion

                //Bottom
                #region Bottom
                if (Crossroad2BottomRED)
                {
                    g.FillEllipse(red, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_width);
                }

                if (Crossroad2BottomYELLOW)
                {
                    g.FillEllipse(yellow, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                }

                if (Crossroad2BottomGREEN)
                {
                    g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
                }

                #endregion

                //Crossroad1 Left Crosswalk
                #region Crossroad1 Left Crosswalk

                if (Crossroad2LeftCrosswalkRED1)
                {
                    g.FillEllipse(red, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }

                if (Crossroad2LeftCrosswalkRED2)
                {
                    g.FillEllipse(red, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }

                if (Crossroad2LeftCrosswalkGREEN1)
                {
                    g.FillEllipse(green, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }

                if (Crossroad2LeftCrosswalkGREEN2)
                {
                    g.FillEllipse(green, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }

                #endregion

                //Crossroad1 Right Crosswalk
                #region Crossroad1 Right Crosswalk

                if (Crossroad2RightCrosswalkRED1)
                {
                    g.FillEllipse(red, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }

                if (Crossroad2RightCrosswalkRED2)
                {
                    g.FillEllipse(red, x + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }

                if (Crossroad2RightCrosswalkGREEN1)
                {
                    g.FillEllipse(green, x + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }

                if (Crossroad2RightCrosswalkGREEN2)
                {
                    g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 5 + FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }

                #endregion

                #endregion

                //LeftT
                #region 
                /*
                UpdateTrafficLightsCrossroadLeftTTOP(program3FormInstance.CrossroadLeftTTopRED, program3FormInstance.CrossroadLeftTTopYELLOW, program3FormInstance.CrossroadLeftTTopGREEN);
                UpdateTrafficLightsCrossroadLeftTLEFT(program3FormInstance.CrossroadLeftTLeftRED, program3FormInstance.CrossroadLeftTLeftYELLOW, program3FormInstance.CrossroadLeftTLeftGREEN);
                UpdateTrafficLightsCrossroadLeftTRIGHT(program3FormInstance.CrossroadLeftTRightRED, program3FormInstance.CrossroadLeftTRightYELLOW, program3FormInstance.CrossroadLeftTRightGREEN);
                UpdateCrosswalkLightsCrossroadLeftTLEFT(program3FormInstance.CrossroadLeftTLeftCrosswalkRED1, program3FormInstance.CrossroadLeftTLeftCrosswalkGREEN1, program3FormInstance.CrossroadLeftTLeftCrosswalkRED2, program3FormInstance.CrossroadLeftTLeftCrosswalkGREEN2);
                */



                #endregion

                #endregion
            }
            else if (drawCrossroadExtension3) //Crossroad1 + Crossorad2 + Left T + Right T
            {
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

                //Conditions for lights based on value
                #region Conditions for lights based on value 

                //Crossroad1
                #region Crossroad1
                /*
                UpdateTrafficLightsCrossroad1TOP(program3FormInstance.Crossroad1TopRED, program3FormInstance.Crossroad1TopYELLOW, program3FormInstance.Crossroad1TopGREEN);
                UpdateTrafficLightsCrossroad1LEFT(program3FormInstance.Crossroad1LeftRED, program3FormInstance.Crossroad1LeftYELLOW, program3FormInstance.Crossroad1LeftGREEN);
                UpdateTrafficLightsCrossroad1RIGHT(program3FormInstance.Crossroad1RightRED, program3FormInstance.Crossroad1RightYELLOW, program3FormInstance.Crossroad1RightGREEN);
                UpdateTrafficLightsCrossroad1BOTTOM(program3FormInstance.Crossroad1BottomRED, program3FormInstance.Crossroad1BottomYELLOW, program3FormInstance.Crossroad1BottomGREEN);
                UpdateCrosswalkLightsCrossroad1TOP(program3FormInstance.Crossroad1TopCrosswalkRED1, program3FormInstance.Crossroad1TopCrosswalkGREEN1, program3FormInstance.Crossroad1TopCrosswalkRED2, program3FormInstance.Crossroad1TopCrosswalkGREEN2);
                UpdateCrosswalkLightsCrossroad1LEFT(program3FormInstance.Crossroad1LeftCrosswalkRED1, program3FormInstance.Crossroad1LeftCrosswalkGREEN1, program3FormInstance.Crossroad1LeftCrosswalkRED2, program3FormInstance.Crossroad1LeftCrosswalkGREEN2);
                */

                //Top
                #region Top
                if (Crossroad1TopRED)
                {
                    g.FillEllipse(red, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
                }

                if (Crossroad1TopYELLOW)
                {
                    g.FillEllipse(yellow, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                }

                if (Crossroad1TopGREEN)
                {
                    g.FillEllipse(green, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                }

                #endregion

                //Left
                #region Left
                if (Crossroad1LeftRED)
                {
                    g.FillEllipse(red, x + length + 3 * FreeSpace + TrafficLights_width * 2, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(white, x + length + 3 * FreeSpace + TrafficLights_width * 2, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                }

                if (Crossroad1LeftYELLOW)
                {
                    g.FillEllipse(yellow, x + length + 3 * FreeSpace + TrafficLights_width, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(white, x + length + 3 * FreeSpace + TrafficLights_width, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                }

                if (Crossroad1LeftGREEN)
                {
                    g.FillEllipse(green, x + length + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(white, x + length + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                }

                #endregion

                //Right
                #region Right
                if (Crossroad1RightRED)
                {
                    g.FillEllipse(red, x + length * 6 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 6 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                }

                if (Crossroad1RightYELLOW)
                {
                    g.FillEllipse(yellow, x + length * 6 + FreeSpace + TrafficLights_width, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 6 + FreeSpace + TrafficLights_width, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                }

                if (Crossroad1RightGREEN)
                {
                    g.FillEllipse(green, x + length * 6 + FreeSpace + TrafficLights_width * 2, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(green, x + length * 6 + FreeSpace + TrafficLights_width * 2, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                }
                #endregion

                //Bottom
                #region Bottom
                if (Crossroad1BottomRED)
                {
                    g.FillEllipse(red, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_width);
                }

                if (Crossroad1BottomYELLOW)
                {
                    g.FillEllipse(yellow, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                }

                if (Crossroad1BottomGREEN)
                {
                    g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
                }
                else
                {
                    g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
                }

                #endregion

                //Crossroad1 Top Crosswalk
                #region Crossroad1 Top Crosswalk

                if (Crossroad1TopCrosswalkRED1)
                {
                    g.FillEllipse(red, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }

                if (Crossroad1TopCrosswalkRED2)
                {
                    g.FillEllipse(red, x + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }

                if (Crossroad1TopCrosswalkGREEN1)
                {
                    g.FillEllipse(green, x + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }

                if (Crossroad1TopCrosswalkGREEN2)
                {
                    g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 5 + FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }

                #endregion

                //Crossroad1 Left Crosswalk
                #region Crossroad1 Left Crosswalk

                if (Crossroad1LeftCrosswalkRED1)
                {
                    g.FillEllipse(red, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }

                if (Crossroad1LeftCrosswalkRED2)
                {
                    g.FillEllipse(red, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }

                if (Crossroad1LeftCrosswalkGREEN1)
                {
                    g.FillEllipse(green, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }

                if (Crossroad1LeftCrosswalkGREEN2)
                {
                    g.FillEllipse(green, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }
                else
                {
                    g.FillEllipse(white, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                }

                #endregion

                #endregion

                //Crossroad2
                #region Crossroad2
                /*
                UpdateTrafficLightsCrossroad2TOP(program3FormInstance.Crossroad2TopRED, program3FormInstance.Crossroad2TopYELLOW, program3FormInstance.Crossroad2TopGREEN);
                UpdateTrafficLightsCrossroad2LEFT(program3FormInstance.Crossroad2LeftRED, program3FormInstance.Crossroad2LeftYELLOW, program3FormInstance.Crossroad2LeftGREEN);
                UpdateTrafficLightsCrossroad2RIGHT(program3FormInstance.Crossroad2RightRED, program3FormInstance.Crossroad2RightYELLOW, program3FormInstance.Crossroad2RightGREEN);
                UpdateTrafficLightsCrossroad2BOTTOM(program3FormInstance.Crossroad2BottomRED, program3FormInstance.Crossroad2BottomYELLOW, program3FormInstance.Crossroad2BottomGREEN);
                UpdateCrosswalkLightsCrossroad2LEFT(program3FormInstance.Crossroad2LeftCrosswalkRED1, program3FormInstance.Crossroad2LeftCrosswalkGREEN1, program3FormInstance.Crossroad2LeftCrosswalkRED2, program3FormInstance.Crossroad2LeftCrosswalkGREEN2);
                UpdateCrosswalkLightsCrossroad2RIGHT(program3FormInstance.Crossroad2RightCrosswalkRED1, program3FormInstance.Crossroad2RightCrosswalkGREEN1, program3FormInstance.Crossroad2RightCrosswalkRED2, program3FormInstance.Crossroad2RightCrosswalkGREEN2);
                */



                #endregion

                //LeftT
                #region LeftT
                /*
                UpdateTrafficLightsCrossroadLeftTTOP(program3FormInstance.CrossroadLeftTTopRED, program3FormInstance.CrossroadLeftTTopYELLOW, program3FormInstance.CrossroadLeftTTopGREEN);
                UpdateTrafficLightsCrossroadLeftTLEFT(program3FormInstance.CrossroadLeftTLeftRED, program3FormInstance.CrossroadLeftTLeftYELLOW, program3FormInstance.CrossroadLeftTLeftGREEN);
                UpdateTrafficLightsCrossroadLeftTRIGHT(program3FormInstance.CrossroadLeftTRightRED, program3FormInstance.CrossroadLeftTRightYELLOW, program3FormInstance.CrossroadLeftTRightGREEN);
                UpdateCrosswalkLightsCrossroadLeftTLEFT(program3FormInstance.CrossroadLeftTLeftCrosswalkRED1, program3FormInstance.CrossroadLeftTLeftCrosswalkGREEN1, program3FormInstance.CrossroadLeftTLeftCrosswalkRED2, program3FormInstance.CrossroadLeftTLeftCrosswalkGREEN2);
                */



                #endregion

                //RightT
                #region RightT
                /*
                UpdateTrafficLightsCrossroadRightTTOP(program3FormInstance.CrossroadRightTTopRED, program3FormInstance.CrossroadRightTTopYELLOW, program3FormInstance.CrossroadRightTTopGREEN);
                UpdateTrafficLightsCrossroadRightTLEFT(program3FormInstance.CrossroadRightTLeftRED, program3FormInstance.CrossroadRightTLeftYELLOW, program3FormInstance.CrossroadRightTLeftGREEN);
                UpdateTrafficLightsCrossroadRightTRIGHT(program3FormInstance.CrossroadRightTRightRED, program3FormInstance.CrossroadRightTRightYELLOW, program3FormInstance.CrossroadRightTRightGREEN);
                UpdateCrosswalkLightsCrossroadRightTTOP(program3FormInstance.CrossroadRightTTopCrosswalkRED1, program3FormInstance.CrossroadRightTTopCrosswalkGREEN1, program3FormInstance.CrossroadRightTTopCrosswalkRED2, program3FormInstance.CrossroadRightTTopCrosswalkGREEN2);
                */



                #endregion

                #endregion
            }
            else
            {
                //???
            }

            #endregion

            

            //Lights change on value change -> don't work
            /*
            #region Lights change 

            //Crossroad1
            #region Crossroad1

            //Top
            #region Top

            if (crossroad1TopRED)
            {
                //RedLight => ON
                g.FillEllipse(red, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else if (crossroad1TopYELLOW)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
                //YellowLight => ON
                g.FillEllipse(yellow, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else if (crossroad1TopGREEN)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                //GreenLight => ON
                g.FillEllipse(green, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else
            {
                //???
            }

            #endregion

            //Left 
            #region Left 

            if (crossroad1LeftRED)
            {
                //RedLight => ON
                g.FillEllipse(red, x + length + 3 * FreeSpace + TrafficLights_width * 2, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length + 3 * FreeSpace + TrafficLights_width, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else if (crossroad1LeftYELLOW)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length + 3 * FreeSpace + TrafficLights_width * 2, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => ON
                g.FillEllipse(yellow, x + length + 3 * FreeSpace + TrafficLights_width, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else if (crossroad1LeftGREEN)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length + 3 * FreeSpace + TrafficLights_width * 2, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length + 3 * FreeSpace + TrafficLights_width, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => ON
                g.FillEllipse(green, x + length + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else
            {
                //???
            }

            #endregion

            //Right
            #region Right

            if (crossroad1RightRED)
            {
                //RedLight => ON
                g.FillEllipse(red, x + length * 6 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 6 + FreeSpace + TrafficLights_width, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 6 + FreeSpace + TrafficLights_width * 2, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else if (crossroad1RightYELLOW)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 6 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => ON
                g.FillEllipse(yellow, x + length * 6 + FreeSpace + TrafficLights_width, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 6 + FreeSpace + TrafficLights_width * 2, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else if (crossroad1RightGREEN)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 6 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 6 + FreeSpace + TrafficLights_width, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => ON
                g.FillEllipse(green, x + length * 6 + FreeSpace + TrafficLights_width * 2, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else
            {
                //???
            }

            #endregion

            //Bottom 
            #region Bottom 

            if (crossroad1BottomRED)
            {
                //RedLight => ON
                g.FillEllipse(red, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
            }
            else if (crossroad1BottomYELLOW)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => ON
                g.FillEllipse(yellow, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
            }
            else if (crossroad1BottomGREEN)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                //GreenLight => ON
                g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
            }
            else
            {
                //???
            }

            #endregion

            //Crosswalk
            #region Crosswalk 

            //Crosswalk Left 
            #region Crosswalk Left

            if (crossroad1LeftCrosswalkRED1)
            {
                //RedLight => ON
                g.FillEllipse(red, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }
            else if (crossroad1LeftCrosswalkGREEN1)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => ON
                g.FillEllipse(green, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }
            else
            {
                //???
            }

            if (crossroad1LeftCrosswalkRED2)
            {
                //RedLight => ON
                g.FillEllipse(red, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }
            else if (crossroad1LeftCrosswalkGREEN2)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => ON
                g.FillEllipse(green, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }
            else
            {
                //???
            }

            #endregion

            //Crosswalk Top
            #region Crosswalk Top

            if (crossroad1TopCrosswalkRED1)
            {
                //RedLight => ON
                g.FillEllipse(red, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }
            else if (crossroad1TopCrosswalkGREEN1)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => ON
                g.FillEllipse(green, x + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }
            else
            {
                //???
            }

            if (crossroad1TopCrosswalkRED2)
            {
                //RedLight => ON
                g.FillEllipse(red, x + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 5 + FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }
            else if (crossroad1TopCrosswalkGREEN2)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => ON
                g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }
            else
            {
                //???
            }

            #endregion

            #endregion

            #endregion

            //Crossroad2
            #region Crossroad2

            //Top
            #region Top

            if (crossroad2TopRED)
            {
                //RedLight => ON
                g.FillEllipse(red, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else if (crossroad2TopYELLOW)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
                //YellowLight => ON
                g.FillEllipse(yellow, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else if (crossroad2TopGREEN)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                //GreenLight => ON
                g.FillEllipse(green, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else
            {
                //???
            }

            #endregion

            //Left 
            #region Left 

            if (crossroad2LeftRED)
            {
                //RedLight => ON
                g.FillEllipse(red, x + length * 7 + 3 * FreeSpace + TrafficLights_width * 2, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 7 + 3 * FreeSpace + TrafficLights_width, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 7 + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else if (crossroad2LeftYELLOW)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 7 + 3 * FreeSpace + TrafficLights_width * 2, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => ON
                g.FillEllipse(yellow, x + length * 7 + 3 * FreeSpace + TrafficLights_width, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 7 + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else if (crossroad2LeftGREEN)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 7 + 3 * FreeSpace + TrafficLights_width * 2, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 7 + 3 * FreeSpace + TrafficLights_width, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => ON
                g.FillEllipse(green, x + length * 7 + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else
            {
                //???
            }

            #endregion

            //Right
            #region Right

            if (crossroad2RightRED)
            {
                //RedLight => ON
                g.FillEllipse(red, x + length * 12 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 12 + FreeSpace + TrafficLights_width, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 12 + FreeSpace + TrafficLights_width * 2, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else if (crossroad2RightYELLOW)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 12 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => ON
                g.FillEllipse(yellow, x + length * 12 + FreeSpace + TrafficLights_width, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 12 + FreeSpace + TrafficLights_width * 2, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else if (crossroad2RightGREEN)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 12 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 12 + FreeSpace + TrafficLights_width, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => ON
                g.FillEllipse(green, x + length * 12 + FreeSpace + TrafficLights_width * 2, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else
            {
                //???
            }

            #endregion

            //Bottom 
            #region Bottom 

            if (crossroad2BottomRED)
            {
                //RedLight => ON
                g.FillEllipse(red, x + length * 11 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 11 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 11 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
            }
            else if (crossroad2BottomYELLOW)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 11 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => ON
                g.FillEllipse(yellow, x + length * 11 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 11 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
            }
            else if (crossroad2BottomGREEN)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 11 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 11 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                //GreenLight => ON
                g.FillEllipse(green, x + length * 11 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
            }
            else
            {
                //???
            }

            #endregion

            //Crosswalk
            #region Crosswalk 

            //Crosswalk Left 
            #region Crosswalk Left

            if (crossroad2LeftCrosswalkRED1)
            {
                //RedLight => ON
                g.FillEllipse(red, x + length * 8 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 8 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }
            else if (crossroad2LeftCrosswalkGREEN1)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 8 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => ON
                g.FillEllipse(green, x + length * 8 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }
            else
            {
                //???
            }

            if (crossroad2LeftCrosswalkRED2)
            {
                //RedLight => ON
                g.FillEllipse(red, x + length * 8 + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 8 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }
            else if (crossroad2LeftCrosswalkGREEN2)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 8 + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => ON
                g.FillEllipse(green, x + length * 8 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }
            else
            {
                //???
            }

            #endregion

            //Crosswalk Top
            #region Crosswalk Right

            if (crossroad2RightCrosswalkRED1)
            {
                //RedLight => ON
                g.FillEllipse(red, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }
            else if (crossroad2RightCrosswalkGREEN1)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => ON
                g.FillEllipse(green, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }
            else
            {
                //???
            }

            if (crossroad2RightCrosswalkRED2)
            {
                //RedLight => ON
                g.FillEllipse(red, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }
            else if (crossroad2RightCrosswalkGREEN2)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => ON
                g.FillEllipse(green, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }
            else
            {
                //???
            }

            #endregion

            #endregion

            #endregion

            //LeftT
            #region LeftT

            //Top
            #region Top

            if (crossroadLeftTTopRED)
            {
                //RedLight => ON
                g.FillEllipse(red, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else if (crossroadLeftTTopYELLOW)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
                //YellowLight => ON
                g.FillEllipse(yellow, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else if (crossroadLeftTTopGREEN)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                //GreenLight => ON
                g.FillEllipse(green, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else
            {
                //???
            }

            #endregion

            //Left 
            #region Left 

            if (crossroadLeftTLeftRED)
            {
                //RedLight => ON
                g.FillEllipse(red, x + length + 3 * FreeSpace + TrafficLights_width * 2, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length + 3 * FreeSpace + TrafficLights_width, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length + 3 * FreeSpace, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else if (crossroadLeftTLeftYELLOW)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length + 3 * FreeSpace + TrafficLights_width * 2, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => ON
                g.FillEllipse(yellow, x + length + 3 * FreeSpace + TrafficLights_width, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length + 3 * FreeSpace, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else if (crossroadLeftTLeftGREEN)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length + 3 * FreeSpace + TrafficLights_width * 2, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length + 3 * FreeSpace + TrafficLights_width, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => ON
                g.FillEllipse(green, x + length + 3 * FreeSpace, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else
            {
                //???
            }

            #endregion

            //Right
            #region Right

            if (crossroadLeftTRightRED)
            {
                //RedLight => ON
                g.FillEllipse(red, x + length * 6 + FreeSpace, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 6 + FreeSpace + TrafficLights_width, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 6 + FreeSpace + TrafficLights_width * 2, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else if (crossroadLeftTRightYELLOW)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 6 + FreeSpace, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => ON
                g.FillEllipse(yellow, x + length * 6 + FreeSpace + TrafficLights_width, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 6 + FreeSpace + TrafficLights_width * 2, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else if (crossroadLeftTRightGREEN)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 6 + FreeSpace, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 6 + FreeSpace + TrafficLights_width, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => ON
                g.FillEllipse(green, x + length * 6 + FreeSpace + TrafficLights_width * 2, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else
            {
                //???
            }

            #endregion

            //Crosswalk
            #region Crosswalk 

            //Crosswalk Left 
            #region Crosswalk Left

            if (crossroadLeftTLeftCrosswalkRED1)
            {
                //RedLight => ON
                g.FillEllipse(red, x + length * 2 + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 2 + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }
            else if (crossroadLeftTLeftCrosswalkGREEN1)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 2 + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => ON
                g.FillEllipse(green, x + length * 2 + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }
            else
            {
                //???
            }

            if (crossroadLeftTLeftCrosswalkRED2)
            {
                //RedLight => ON
                g.FillEllipse(red, x + length * 2 + FreeSpace, y + length * 11 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 2 + FreeSpace, y + length * 11 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }
            else if (crossroadLeftTLeftCrosswalkGREEN2)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 2 + FreeSpace, y + length * 11 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => ON
                g.FillEllipse(green, x + length * 2 + FreeSpace, y + length * 11 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }
            else
            {
                //???
            }

            #endregion

            #endregion

            #endregion

            //RightT
            #region RightT

            //Top
            #region Top

            if (crossroadRightTTopRED)
            {
                //RedLight => ON
                g.FillEllipse(red, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else if (crossroadRightTTopYELLOW)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
                //YellowLight => ON
                g.FillEllipse(yellow, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else if (crossroadRightTTopGREEN)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                //GreenLight => ON
                g.FillEllipse(green, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else
            {
                //???
            }

            #endregion

            //Left 
            #region Left 

            if (crossroadRightTLeftRED)
            {
                //RedLight => ON
                g.FillEllipse(red, x + length * 7 + 3 * FreeSpace + TrafficLights_width * 2, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 7 + 3 * FreeSpace + TrafficLights_width, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 7 + 3 * FreeSpace, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else if (crossroadRightTLeftYELLOW)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 7 + 3 * FreeSpace + TrafficLights_width * 2, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => ON
                g.FillEllipse(yellow, x + length * 7 + 3 * FreeSpace + TrafficLights_width, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 7 + 3 * FreeSpace, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else if (crossroadRightTLeftGREEN)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 7 + 3 * FreeSpace + TrafficLights_width * 2, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 7 + 3 * FreeSpace + TrafficLights_width, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => ON
                g.FillEllipse(green, x + length * 7 + 3 * FreeSpace, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else
            {
                //???
            }

            #endregion

            //Right
            #region Right

            if (crossroadRightTRightRED)
            {
                //RedLight => ON
                g.FillEllipse(red, x + length * 12 + FreeSpace, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 12 + FreeSpace + TrafficLights_width, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 12 + FreeSpace + TrafficLights_width * 2, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else if (crossroadRightTRightYELLOW)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 12 + FreeSpace, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => ON
                g.FillEllipse(yellow, x + length * 12 + FreeSpace + TrafficLights_width, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 12 + FreeSpace + TrafficLights_width * 2, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else if (crossroadRightTRightGREEN)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 12 + FreeSpace, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 12 + FreeSpace + TrafficLights_width, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => ON
                g.FillEllipse(green, x + length * 12 + FreeSpace + TrafficLights_width * 2, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else
            {
                //???
            }

            #endregion

            //Crosswalk
            #region Crosswalk 

            //Crosswalk Left 
            #region Crosswalk Top

            if (crossroadRightTTopCrosswalkRED1)
            {
                //RedLight => ON
                g.FillEllipse(red, x + length * 8 + crosswalk_width + 3 * FreeSpace, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 8 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }
            else if (crossroadRightTTopCrosswalkGREEN1)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 8 + crosswalk_width + 3 * FreeSpace, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => ON
                g.FillEllipse(green, x + length * 8 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }
            else
            {
                //???
            }

            if (crossroadRightTTopCrosswalkRED2)
            {
                //RedLight => ON
                g.FillEllipse(red, x + length * 11 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 11 + FreeSpace, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }
            else if (crossroadRightTTopCrosswalkGREEN2)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 11 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => ON
                g.FillEllipse(green, x + length * 11 + FreeSpace, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }
            else
            {
                //???
            }

            #endregion

            #endregion

            #endregion

            #endregion

            */

            //Reaction on variable change in Program3Form
            /*

            //Crossroad1
            #region Crossroad1 

            #region Lights 

            //TOP
            #region TOP
            if (program3FormInstance.Crossroad1TopRED)
            {
                //RedLight => ON
                g.FillEllipse(red, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

                Invalidate();
            }
            else if (program3FormInstance.Crossroad1TopYELLOW)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
                //YellowLight => ON
                g.FillEllipse(yellow, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

                Invalidate();
            }
            else if (program3FormInstance.Crossroad1TopGREEN)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                //GreenLight => ON
                g.FillEllipse(green, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

                Invalidate();
            }
            else
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);

                Invalidate();
            }
            #endregion

            //LEFT
            #region LEFT

            #endregion

            //RIGHT
            #region RIGHT 

            #endregion

            //BOTTOM
            #region BOTTOM 

            #endregion

            #endregion

            #region Crosswalk lights

            //TOP
            #region TOP

            #endregion

            //LEFT
            #region LEFT 

            #endregion

            #endregion

            #endregion

            //Crossroad2
            #region Crossroad2 

            #region Lights 

            //TOP
            #region TOP

            #endregion

            //LEFT
            #region LEFT

            #endregion

            //RIGHT
            #region RIGHT 

            #endregion

            //BOTTOM
            #region BOTTOM 

            #endregion

            #endregion

            #region Crosswalk lights

            //LEFT
            #region LEFT

            #endregion

            //RIGHT
            #region RIGHT 

            #endregion

            #endregion

            #endregion

            //LeftT
            #region LeftT 

            #region Lights 

            //TOP
            #region TOP

            #endregion

            //LEFT
            #region LEFT

            #endregion

            //RIGHT
            #region RIGHT 

            #endregion

            #endregion

            #region Crosswalk lights

            //TOP
            #region TOP

            #endregion

            #endregion

            #endregion

            //RightT
            #region RightT 

            #region Lights 

            //TOP
            #region TOP

            #endregion

            //LEFT
            #region LEFT

            #endregion

            //RIGHT
            #region RIGHT 

            #endregion

            #endregion

            #region Crosswalk lights

            //LEFT
            #region LEFT

            #endregion

            #endregion

            #endregion

            */

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

            Invalidate();
        }

        private void Draw(Graphics g)
        {
            try
            {

            }
            catch
            {

            }
        }

        //InitializeButtons
        #region InitializeButtons
        private void InitializeButtons()
        {
            //Crossroad1 
            #region Crossroad1 BTNs

            //Crossroad1 - top crosswalk
            #region Crossroad1 - top crosswalk
            //left  
            //Button btnCrossroad1TopCrosswalkLEFT = new Button();
            btnCrossroad1TopCrosswalkLEFT.Text = "Crossroad1 Top crosswalk BTN1";
            btnCrossroad1TopCrosswalkLEFT.BackColor = Color.White;
            btnCrossroad1TopCrosswalkLEFT.Visible = false;
            btnCrossroad1TopCrosswalkLEFT.Enabled = false;
            btnCrossroad1TopCrosswalkLEFT.Location = new System.Drawing.Point(Convert.ToInt32(length), Convert.ToInt32(length)); //cannot invert float to int 
            btnCrossroad1TopCrosswalkLEFT.Size = new Size(Convert.ToInt32(Button_width), Convert.ToInt32(Button_height)); //cannot invert float to int 
            btnCrossroad1TopCrosswalkLEFT.Click += btnCrossroad1TopCrosswalkLEFT_Click;
            //btnCrossroad1TopCrosswalkLEFT.Click += (sender, e) => OnButtonClicked("btnCrossroad1TopCrosswalkLEFT");
            Controls.Add(btnCrossroad1TopCrosswalkLEFT);

            //right
            //Button btnCrossroad1TopCrosswalkRIGHT = new Button();
            btnCrossroad1TopCrosswalkRIGHT.Text = "Crossroad1 Top crosswalk BTN2";
            btnCrossroad1TopCrosswalkRIGHT.BackColor = Color.White;
            btnCrossroad1TopCrosswalkRIGHT.Visible = false;
            btnCrossroad1TopCrosswalkRIGHT.Enabled = false;
            btnCrossroad1TopCrosswalkRIGHT.Location = new System.Drawing.Point(Convert.ToInt32(length * 6), Convert.ToInt32(length)); //cannot invert float to int 
            btnCrossroad1TopCrosswalkRIGHT.Size = new Size(Convert.ToInt32(Button_width), Convert.ToInt32(Button_height)); //cannot invert float to int 
            btnCrossroad1TopCrosswalkRIGHT.Click += btnCrossroad1TopCrosswalkRIGHT_Click;
            //btnCrossroad1TopCrosswalkRIGHT.Click += (sender, e) => OnButtonClicked("btnCrossroad1TopCrosswalkRIGHT");
            Controls.Add(btnCrossroad1TopCrosswalkRIGHT);

            #endregion

            //Crossroad1 - left crosswalk
            #region Crossroad1 - left crosswalk
            //top 
            //Button btnCrossroad1LeftCrosswalkTOP = new Button();
            btnCrossroad1LeftCrosswalkTOP.Text = "Crossroad1 Left crosswalk BTN1";
            btnCrossroad1LeftCrosswalkTOP.BackColor = Color.White;
            btnCrossroad1LeftCrosswalkTOP.Visible = false;
            btnCrossroad1LeftCrosswalkTOP.Enabled = false;
            btnCrossroad1LeftCrosswalkTOP.Location = new System.Drawing.Point(Convert.ToInt32(length), Convert.ToInt32(length * 2)); //cannot invert float to int 
            btnCrossroad1LeftCrosswalkTOP.Size = new Size(Convert.ToInt32(Button_width), Convert.ToInt32(Button_height)); //cannot invert float to int 
            btnCrossroad1LeftCrosswalkTOP.Click += btnCrossroad1LeftCrosswalkTOP_Click;
            //btnCrossroad1LeftCrosswalkTOP.Click += (sender, e) => OnButtonClicked("btnCrossroad1LeftCrosswalkTOP");
            Controls.Add(btnCrossroad1LeftCrosswalkTOP);


            //bottom
            //Button btnCrossroad1LeftCrosswalkBOTTOM = new Button();
            btnCrossroad1LeftCrosswalkBOTTOM.Text = "Crossroad1 Left crosswalk BTN2";
            btnCrossroad1LeftCrosswalkBOTTOM.BackColor = Color.White;
            btnCrossroad1LeftCrosswalkBOTTOM.Visible = false;
            btnCrossroad1LeftCrosswalkBOTTOM.Enabled = false;
            btnCrossroad1LeftCrosswalkBOTTOM.Location = new System.Drawing.Point(Convert.ToInt32(length), Convert.ToInt32(length * 6)); //cannot invert float to int 
            btnCrossroad1LeftCrosswalkBOTTOM.Size = new Size(Convert.ToInt32(Button_width), Convert.ToInt32(Button_height)); //cannot invert float to int 
            btnCrossroad1LeftCrosswalkBOTTOM.Click += btnCrossroad1LeftCrosswalkBOTTOM_Click;
            //btnCrossroad1LeftCrosswalkBOTTOM.Click += (sender, e) => OnButtonClicked("btnCrossroad1LeftCrosswalkBOTTOM");
            Controls.Add(btnCrossroad1LeftCrosswalkBOTTOM);

            #endregion

            #endregion

            //Crossroad2
            #region Crossroad2 BTNs

            //Crossroad2 - left crosswalk 
            #region Crossroad2 - left crosswalk 
            //top 
            //Button btnCrossroad2LeftCrosswalkTOP = new Button();
            btnCrossroad2LeftCrosswalkTOP.Text = "Crossroad2 Left crosswalk BTN1";
            btnCrossroad2LeftCrosswalkTOP.BackColor = Color.White;
            btnCrossroad2LeftCrosswalkTOP.Visible = false;
            btnCrossroad2LeftCrosswalkTOP.Enabled = false;
            btnCrossroad2LeftCrosswalkTOP.Location = new System.Drawing.Point(Convert.ToInt32(length * 7), Convert.ToInt32(length * 2)); //cannot invert float to int 
            btnCrossroad2LeftCrosswalkTOP.Size = new Size(Convert.ToInt32(Button_width), Convert.ToInt32(Button_height)); //cannot invert float to int 
            btnCrossroad2LeftCrosswalkTOP.Click += btnCrossroad2LeftCrosswalkTOP_Click;
            //btnCrossroad2LeftCrosswalkTOP.Click += (sender, e) => OnButtonClicked("btnCrossroad2LeftCrosswalkTOP");
            Controls.Add(btnCrossroad2LeftCrosswalkTOP);

            //bottom
            //Button btnCrossroad2LeftCrosswalkBOTTOM = new Button();
            btnCrossroad2LeftCrosswalkBOTTOM.Text = "Crossroad2 Left crosswalk BTN2";
            btnCrossroad2LeftCrosswalkBOTTOM.BackColor = Color.White;
            btnCrossroad2LeftCrosswalkBOTTOM.Visible = false;
            btnCrossroad2LeftCrosswalkBOTTOM.Enabled = false;
            btnCrossroad2LeftCrosswalkBOTTOM.Location = new System.Drawing.Point(Convert.ToInt32(length * 7), Convert.ToInt32(length * 6)); //cannot invert float to int 
            btnCrossroad2LeftCrosswalkBOTTOM.Size = new Size(Convert.ToInt32(Button_width), Convert.ToInt32(Button_height)); //cannot invert float to int 
            btnCrossroad2LeftCrosswalkBOTTOM.Click += btnCrossroad2LeftCrosswalkBOTTOM_Click;
            //btnCrossroad2LeftCrosswalkBOTTOM.Click += (sender, e) => OnButtonClicked("btnCrossroad2LeftCrosswalkBOTTOM");
            Controls.Add(btnCrossroad2LeftCrosswalkBOTTOM);

            #endregion

            //Crossroad2 - right crosswalk
            #region Crossroad2 - right crosswalk
            //top 
            //Button btnCrossroad2RightCrosswalkTOP = new Button();
            btnCrossroad2RightCrosswalkTOP.Text = "Crossroad2 Right crosswalk BTN1";
            btnCrossroad2RightCrosswalkTOP.BackColor = Color.White;
            btnCrossroad2RightCrosswalkTOP.Visible = false;
            btnCrossroad2RightCrosswalkTOP.Enabled = false;
            btnCrossroad2RightCrosswalkTOP.Location = new System.Drawing.Point(Convert.ToInt32(length * 13), Convert.ToInt32(length * 2)); //cannot invert float to int 
            btnCrossroad2RightCrosswalkTOP.Size = new Size(Convert.ToInt32(Button_width), Convert.ToInt32(Button_height)); //cannot invert float to int 
            btnCrossroad2RightCrosswalkTOP.Click += btnCrossroad2RightCrosswalkTOP_Click;
            //btnCrossroad2RightCrosswalkTOP.Click += (sender, e) => OnButtonClicked("btnCrossroad2RightCrosswalkTOP");
            Controls.Add(btnCrossroad2RightCrosswalkTOP);

            //bottom
            //Button btnCrossroad2RightCrosswalkBOTTOM = new Button();
            btnCrossroad2RightCrosswalkBOTTOM.Text = "Crossroad2 Right crosswalk BTN2";
            btnCrossroad2RightCrosswalkBOTTOM.BackColor = Color.White;
            btnCrossroad2RightCrosswalkBOTTOM.Visible = false;
            btnCrossroad2RightCrosswalkBOTTOM.Enabled = false;
            btnCrossroad2RightCrosswalkBOTTOM.Location = new System.Drawing.Point(Convert.ToInt32(length * 13), Convert.ToInt32(length * 6)); //cannot invert float to int 
            btnCrossroad2RightCrosswalkBOTTOM.Size = new Size(Convert.ToInt32(Button_width), Convert.ToInt32(Button_height)); //cannot invert float to int 
            btnCrossroad2RightCrosswalkBOTTOM.Click += btnCrossroad2RightCrosswalkBOTTOM_Click;
            //btnCrossroad2RightCrosswalkBOTTOM.Click += (sender, e) => OnButtonClicked("btnCrossroad2RightCrosswalkBOTTOM");
            Controls.Add(btnCrossroad2RightCrosswalkBOTTOM);

            #endregion

            #endregion

            //Left T
            #region LeftT BTNs

            #region LeftT - left crosswalk
            //top 
            //Button btnLeftTLeftCrosswalkTOP = new Button();
            btnLeftTLeftCrosswalkTOP.Text = "LeftT\nLeft crosswalk BTN1";
            btnLeftTLeftCrosswalkTOP.BackColor = Color.White;
            btnLeftTLeftCrosswalkTOP.Visible = false;
            btnLeftTLeftCrosswalkTOP.Enabled = false;
            btnLeftTLeftCrosswalkTOP.Location = new System.Drawing.Point(Convert.ToInt32(length), Convert.ToInt32(length * 8)); //cannot invert float to int 
            btnLeftTLeftCrosswalkTOP.Size = new Size(Convert.ToInt32(Button_width), Convert.ToInt32(Button_height)); //cannot invert float to int 
            btnLeftTLeftCrosswalkTOP.Click += btnLeftTLeftCrosswalkTOP_CLick;
            //btnLeftTLeftCrosswalkTOP.Click += (sender, e) => OnButtonClicked("btnLeftTLeftCrosswalkTOP");
            Controls.Add(btnLeftTLeftCrosswalkTOP);

            //bottom
            //Button btnLeftTLeftCrosswalkBOTTOM = new Button();
            btnLeftTLeftCrosswalkBOTTOM.Text = "LeftT\nLeft crosswalk BTN2";
            btnLeftTLeftCrosswalkBOTTOM.BackColor = Color.White;
            btnLeftTLeftCrosswalkBOTTOM.Visible = false;
            btnLeftTLeftCrosswalkBOTTOM.Enabled = false;
            btnLeftTLeftCrosswalkBOTTOM.Location = new System.Drawing.Point(Convert.ToInt32(length), Convert.ToInt32(length * 12)); //cannot invert float to int 
            btnLeftTLeftCrosswalkBOTTOM.Size = new Size(Convert.ToInt32(Button_width), Convert.ToInt32(Button_height)); //cannot invert float to int 
            btnLeftTLeftCrosswalkBOTTOM.Click += btnLeftTLeftCrosswalkBOTTOM_CLick;
            //btnLeftTLeftCrosswalkBOTTOM.Click += (sender, e) => OnButtonClicked("btnLeftTLeftCrosswalkBOTTOM");
            Controls.Add(btnLeftTLeftCrosswalkBOTTOM);

            #endregion

            #endregion

            //Right T 
            #region RightT BTNs

            #region RightT - top crosswalk
            //left  
            //Button btnRightTTopCrosswalkLEFT = new Button();
            btnRightTTopCrosswalkLEFT.Text = "RightT\nTop crosswalk BTN1";
            btnRightTTopCrosswalkLEFT.BackColor = Color.White;
            btnRightTTopCrosswalkLEFT.Visible = false;
            btnRightTTopCrosswalkLEFT.Enabled = false;
            btnRightTTopCrosswalkLEFT.Location = new System.Drawing.Point(Convert.ToInt32(length * 7), Convert.ToInt32(length * 8)); //cannot invert float to int 
            btnRightTTopCrosswalkLEFT.Size = new Size(Convert.ToInt32(Button_width), Convert.ToInt32(Button_height)); //cannot invert float to int 
            btnRightTTopCrosswalkLEFT.Click += btnRightTTopCrosswalkLEFT_Click;
            //btnRightTTopCrosswalkLEFT.Click += (sender, e) => OnButtonClicked("btnRightTTopCrosswalkLEFT");
            Controls.Add(btnRightTTopCrosswalkLEFT);

            //right
            //Button btnRightTTopCrosswalkRIGHT = new Button();
            btnRightTTopCrosswalkRIGHT.Text = "RightT\nTop crosswalk BTN2";
            btnRightTTopCrosswalkRIGHT.BackColor = Color.White;
            btnRightTTopCrosswalkRIGHT.Visible = false;
            btnRightTTopCrosswalkRIGHT.Enabled = false;
            btnRightTTopCrosswalkRIGHT.Location = new System.Drawing.Point(Convert.ToInt32(length * 12), Convert.ToInt32(length * 8)); //cannot invert float to int 
            btnRightTTopCrosswalkRIGHT.Size = new Size(Convert.ToInt32(Button_width), Convert.ToInt32(Button_height)); //cannot invert float to int 
            btnRightTTopCrosswalkRIGHT.Click += btnRightTTopCrosswalkRIGHT_Click;
            //btnRightTTopCrosswalkRIGHT.Click += (sender, e) => OnButtonClicked("btnRightTTopCrosswalkRIGHT");
            Controls.Add(btnRightTTopCrosswalkRIGHT);

            #endregion

            #endregion

        }
        #endregion

        //Methods for BTN_CLick action
        #region Methods for BTN_CLick action

        private void btnCrossroad1TopCrosswalkLEFT_Click(object sender, EventArgs e)
        {
            //ButtonClicked.Invoke(this, "btnCrossroad1TopCrosswalkLEFT");

            if (OnCrossroadBTNClick != null)
                OnCrossroadBTNClick(sender, ((Button)sender).Text);

            //toto se bude dít v Program3Form
            /*
            Crossroad1TopCrosswalkBTN1 = true;
            S7.SetBitAt(program3FormInstance.send_buffer_DB1, 0, 2, Crossroad1TopCrosswalkBTN1);

            //write to PLC
            int writeResultDB1_Crossroad1TopCrosswalkBTN1 = program3FormInstance.client.DBWrite(DBNumber_DB1, 0, program3FormInstance.send_buffer_DB1.Length, program3FormInstance.send_buffer_DB1);
            if (writeResultDB1_Crossroad1TopCrosswalkBTN1 != 0)
            {
                //write error
                if (!errorMessageBoxShown)
                {
                    //MessageBox
                    MessageBox.Show("BE doesn't work properly. Data could´t be written to DB1!!! \n\n" +
                        $"Error message: writeResultDB1_Crossroad1TopCrosswalkBTN1 = {writeResultDB1_Crossroad1TopCrosswalkBTN1} \n", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                    errorMessageBoxShown = true;
                }
            }
            else
            {
                //write was successful
            }
            */
        }

        private void btnCrossroad1TopCrosswalkRIGHT_Click(object sender, EventArgs e)
        {
            //ButtonClicked.Invoke(this, "btnCrossroad1TopCrosswalkRIGHT");

            if (OnCrossroadBTNClick != null)
                OnCrossroadBTNClick(sender, ((Button)sender).Text);

            //toto se bude dít v Program3Form
            /*
            Crossroad1TopCrosswalkBTN2 = true;
            S7.SetBitAt(program3FormInstance.send_buffer_DB1, 0, 3, Crossroad1TopCrosswalkBTN2);

            //write to PLC
            int writeResultDB1_Crossroad1TopCrosswalkBTN2 = program3FormInstance.client.DBWrite(DBNumber_DB1, 0, program3FormInstance.send_buffer_DB1.Length, program3FormInstance.send_buffer_DB1);
            if (writeResultDB1_Crossroad1TopCrosswalkBTN2 != 0)
            {
                //write error
                if (!errorMessageBoxShown)
                {
                    //MessageBox
                    MessageBox.Show("BE doesn't work properly. Data could´t be written to DB1!!! \n\n" +
                        $"Error message: writeResultDB1_Crossroad1TopCrosswalkBTN2 = {writeResultDB1_Crossroad1TopCrosswalkBTN2} \n", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                    errorMessageBoxShown = true;
                }
            }
            else
            {
                //write was successful
            }
            */
        }

        private void btnCrossroad1LeftCrosswalkTOP_Click(object sender, EventArgs e)
        {
            //ButtonClicked.Invoke(this, "btnCrossroad1LeftCrosswalkTOP");

            if (OnCrossroadBTNClick != null)
                OnCrossroadBTNClick(sender, ((Button)sender).Text);

            //toto se bude dít v Program3Form
            /*
            Crossroad1LeftCrosswalkBTN1 = true;
            S7.SetBitAt(program3FormInstance.send_buffer_DB1, 0, 0, Crossroad1LeftCrosswalkBTN1);

            //write to PLC
            int writeResultDB1_Crossroad1LeftCrosswalkBTN1 = program3FormInstance.client.DBWrite(DBNumber_DB1, 0, program3FormInstance.send_buffer_DB1.Length, program3FormInstance.send_buffer_DB1);
            if (writeResultDB1_Crossroad1LeftCrosswalkBTN1 != 0)
            {
                //write error
                if (!errorMessageBoxShown)
                {
                    //MessageBox
                    MessageBox.Show("BE doesn't work properly. Data could´t be written to DB1!!! \n\n" +
                        $"Error message: writeResultDB1_Crossroad1LeftCrosswalkBTN1 = {writeResultDB1_Crossroad1LeftCrosswalkBTN1} \n", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                    errorMessageBoxShown = true;
                }
            }
            else
            {
                //write was successful
            }
            */
        }

        private void btnCrossroad1LeftCrosswalkBOTTOM_Click(object sender, EventArgs e)
        {
            //ButtonClicked.Invoke(this, "btnCrossroad1LeftCrosswalkBOTTOM");

            if (OnCrossroadBTNClick != null)
                OnCrossroadBTNClick(sender, ((Button)sender).Text);

            //toto se bude dít v Program3Form
            /*
            Crossroad1LeftCrosswalkBTN2 = true;
            S7.SetBitAt(program3FormInstance.send_buffer_DB1, 0, 1, Crossroad1LeftCrosswalkBTN2);

            //write to PLC
            int writeResultDB1_Crossroad1LeftCrosswalkBTN2 = program3FormInstance.client.DBWrite(DBNumber_DB1, 0, program3FormInstance.send_buffer_DB1.Length, program3FormInstance.send_buffer_DB1);
            if (writeResultDB1_Crossroad1LeftCrosswalkBTN2 != 0)
            {
                //write error
                if (!errorMessageBoxShown)
                {
                    //MessageBox
                    MessageBox.Show("BE doesn't work properly. Data could´t be written to DB1!!! \n\n" +
                        $"Error message: writeResultDB1_Crossroad1LeftCrosswalkBTN2 = {writeResultDB1_Crossroad1LeftCrosswalkBTN2} \n", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                    errorMessageBoxShown = true;
                }
            }
            else
            {
                //write was successful
            }
            */
        }

        private void btnCrossroad2LeftCrosswalkTOP_Click(object sender, EventArgs e)
        {
            //ButtonClicked.Invoke(this, "btnCrossroad2LeftCrosswalkTOP");

            if (OnCrossroadBTNClick != null)
                OnCrossroadBTNClick(sender, ((Button)sender).Text);

            //toto se bude dít v Program3Form
            /*
            Crossroad2LeftCrosswalkBTN1 = true;
            S7.SetBitAt(program3FormInstance.send_buffer_DB19, 0, 0, Crossroad2LeftCrosswalkBTN1);

            //write to PLC
            int writeResultDB1_Crossroad2LeftCrosswalkBTN1 = program3FormInstance.client.DBWrite(DBNumber_DB19, 0, program3FormInstance.send_buffer_DB19.Length, program3FormInstance.send_buffer_DB19);
            if (writeResultDB1_Crossroad2LeftCrosswalkBTN1 != 0)
            {
                //write error
                if (!errorMessageBoxShown)
                {
                    //MessageBox
                    MessageBox.Show("BE doesn't work properly. Data could´t be written to DB19!!! \n\n" +
                        $"Error message: writeResultDB1_Crossroad2LeftCrosswalkBTN1 = {writeResultDB1_Crossroad2LeftCrosswalkBTN1} \n", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                    errorMessageBoxShown = true;
                }
            }
            else
            {
                //write was successful
            }
            */
        }

        private void btnCrossroad2LeftCrosswalkBOTTOM_Click(object sender, EventArgs e)
        {
            //ButtonClicked.Invoke(this, "btnCrossroad2LeftCrosswalkBOTTOM");

            if (OnCrossroadBTNClick != null)
                OnCrossroadBTNClick(sender, ((Button)sender).Text);

            //toto se bude dít v Program3Form
            /*
            Crossroad2LeftCrosswalkBTN2 = true;
            S7.SetBitAt(program3FormInstance.send_buffer_DB19, 0, 1, Crossroad2LeftCrosswalkBTN2);

            //write to PLC
            int writeResultDB1_Crossroad2LeftCrosswalkBTN2 = program3FormInstance.client.DBWrite(DBNumber_DB19, 0, program3FormInstance.send_buffer_DB19.Length, program3FormInstance.send_buffer_DB19);
            if (writeResultDB1_Crossroad2LeftCrosswalkBTN2 != 0)
            {
                //write error
                if (!errorMessageBoxShown)
                {
                    //MessageBox
                    MessageBox.Show("BE doesn't work properly. Data could´t be written to DB19!!! \n\n" +
                        $"Error message: writeResultDB1_Crossroad2LeftCrosswalkBTN2 = {writeResultDB1_Crossroad2LeftCrosswalkBTN2} \n", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                    errorMessageBoxShown = true;
                }
            }
            else
            {
                //write was successful
            }
            */
        }

        private void btnCrossroad2RightCrosswalkTOP_Click(object sender, EventArgs e)
        {
            //ButtonClicked.Invoke(this, "btnCrossroad2RightCrosswalkTOP");

            if (OnCrossroadBTNClick != null)
                OnCrossroadBTNClick(sender, ((Button)sender).Text);

            //toto se bude dít v Program3Form
            /*
            Crossroad2RightCrosswalkBTN1 = true;
            S7.SetBitAt(program3FormInstance.send_buffer_DB19, 0, 2, Crossroad2RightCrosswalkBTN1);

            //write to PLC
            int writeResultDB1_Crossroad2RightCrosswalkBTN1 = program3FormInstance.client.DBWrite(DBNumber_DB19, 0, program3FormInstance.send_buffer_DB19.Length, program3FormInstance.send_buffer_DB19);
            if (writeResultDB1_Crossroad2RightCrosswalkBTN1 != 0)
            {
                //write error
                if (!errorMessageBoxShown)
                {
                    //MessageBox
                    MessageBox.Show("BE doesn't work properly. Data could´t be written to DB19!!! \n\n" +
                        $"Error message: writeResultDB1_Crossroad2RightCrosswalkBTN1 = {writeResultDB1_Crossroad2RightCrosswalkBTN1} \n", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                    errorMessageBoxShown = true;
                }
            }
            else
            {
                //write was successful
            }
            */
        }

        private void btnCrossroad2RightCrosswalkBOTTOM_Click(object sender, EventArgs e)
        {
            //ButtonClicked.Invoke(this, "btnCrossroad2RightCrosswalkBOTTOM");

            if (OnCrossroadBTNClick != null)
                OnCrossroadBTNClick(sender, ((Button)sender).Text);

            //toto se bude dít v Program3Form
            /*
            Crossroad2RightCrosswalkBTN2 = true;
            S7.SetBitAt(program3FormInstance.send_buffer_DB19, 0, 3, Crossroad2RightCrosswalkBTN2);

            //write to PLC
            int writeResultDB1_Crossroad2RightCrosswalkBTN2 = program3FormInstance.client.DBWrite(DBNumber_DB19, 0, program3FormInstance.send_buffer_DB19.Length, program3FormInstance.send_buffer_DB19);
            if (writeResultDB1_Crossroad2RightCrosswalkBTN2 != 0)
            {
                //write error
                if (!errorMessageBoxShown)
                {
                    //MessageBox
                    MessageBox.Show("BE doesn't work properly. Data could´t be written to DB19!!! \n\n" +
                        $"Error message: writeResultDB1_Crossroad2RightCrosswalkBTN2 = {writeResultDB1_Crossroad2RightCrosswalkBTN2} \n", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                    errorMessageBoxShown = true;
                }
            }
            else
            {
                //write was successful
            }
            */
        }

        private void btnLeftTLeftCrosswalkTOP_CLick(object sender, EventArgs e)
        {
            //ButtonClicked.Invoke(this, "btnLeftTLeftCrosswalkTOP");

            if (OnCrossroadBTNClick != null)
                OnCrossroadBTNClick(sender, ((Button)sender).Text);

            //toto se bude dít v Program3Form
            /*
            CrossroadLeftTLeftCrosswalkBTN1 = true;
            S7.SetBitAt(program3FormInstance.send_buffer_DB20, 0, 0, CrossroadLeftTLeftCrosswalkBTN1);

            //write to PLC
            int writeResultDB1_CrossroadLeftTLeftCrosswalkBTN1 = program3FormInstance.client.DBWrite(DBNumber_DB20, 0, program3FormInstance.send_buffer_DB20.Length, program3FormInstance.send_buffer_DB20);
            if (writeResultDB1_CrossroadLeftTLeftCrosswalkBTN1 != 0)
            {
                //write error
                if (!errorMessageBoxShown)
                {
                    //MessageBox
                    MessageBox.Show("BE doesn't work properly. Data could´t be written to DB20!!! \n\n" +
                        $"Error message: writeResultDB1_CrossroadLeftTLeftCrosswalkBTN1 = {writeResultDB1_CrossroadLeftTLeftCrosswalkBTN1} \n", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                    errorMessageBoxShown = true;
                }
            }
            else
            {
                //write was successful
            }
            */
        }

        private void btnLeftTLeftCrosswalkBOTTOM_CLick(object sender, EventArgs e)
        {
            //ButtonClicked.Invoke(this, "btnLeftTLeftCrosswalkBOTTOM");

            if (OnCrossroadBTNClick != null)
                OnCrossroadBTNClick(sender, ((Button)sender).Text);

            //toto se bude dít v Program3Form
            /*
            CrossroadLeftTLeftCrosswalkBTN2 = true;
            S7.SetBitAt(program3FormInstance.send_buffer_DB20, 0, 1, CrossroadLeftTLeftCrosswalkBTN2);

            //write to PLC
            int writeResultDB1_CrossroadLeftTLeftCrosswalkBTN2 = program3FormInstance.client.DBWrite(DBNumber_DB20, 0, program3FormInstance.send_buffer_DB20.Length, program3FormInstance.send_buffer_DB20);
            if (writeResultDB1_CrossroadLeftTLeftCrosswalkBTN2 != 0)
            {
                //write error
                if (!errorMessageBoxShown)
                {
                    //MessageBox
                    MessageBox.Show("BE doesn't work properly. Data could´t be written to DB20!!! \n\n" +
                        $"Error message: writeResultDB1_CrossroadLeftTLeftCrosswalkBTN2 = {writeResultDB1_CrossroadLeftTLeftCrosswalkBTN2} \n", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                    errorMessageBoxShown = true;
                }
            }
            else
            {
                //write was successful
            }
            */
        }

        private void btnRightTTopCrosswalkLEFT_Click(object sender, EventArgs e)
        {
            //ButtonClicked.Invoke(this, "btnRightTTopCrosswalkLEFT");

            if (OnCrossroadBTNClick != null)
                OnCrossroadBTNClick(sender, ((Button)sender).Text);

            //toto se bude dít v Program3Form
            /*
            CrossroadRightTTopCrosswalkBTN1 = true;
            S7.SetBitAt(program3FormInstance.send_buffer_DB21, 0, 0, CrossroadRightTTopCrosswalkBTN1);

            //write to PLC
            int writeResultDB1_CrossroadRightTTopCrosswalkBTN1 = program3FormInstance.client.DBWrite(DBNumber_DB21, 0, program3FormInstance.send_buffer_DB21.Length, program3FormInstance.send_buffer_DB21);
            if (writeResultDB1_CrossroadRightTTopCrosswalkBTN1 != 0)
            {
                //write error
                if (!errorMessageBoxShown)
                {
                    //MessageBox
                    MessageBox.Show("BE doesn't work properly. Data could´t be written to DB21!!! \n\n" +
                        $"Error message: writeResultDB1_CrossroadRightTTopCrosswalkBTN1 = {writeResultDB1_CrossroadRightTTopCrosswalkBTN1} \n", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                    errorMessageBoxShown = true;
                }
            }
            else
            {
                //write was successful
            }
            */
        }
        private void btnRightTTopCrosswalkRIGHT_Click(object sender, EventArgs e)
        {
            //ButtonClicked.Invoke(this, "btnRightTTopCrosswalkRIGHT");

            if (OnCrossroadBTNClick != null)
                OnCrossroadBTNClick(sender, ((Button)sender).Text);

            //toto se bude dít v Program3Form
            /*
            CrossroadRightTTopCrosswalkBTN2 = true;
            S7.SetBitAt(program3FormInstance.send_buffer_DB21, 0, 1, CrossroadRightTTopCrosswalkBTN2);

            //write to PLC
            int writeResultDB1_CrossroadRightTTopCrosswalkBTN2 = program3FormInstance.client.DBWrite(DBNumber_DB21, 0, program3FormInstance.send_buffer_DB21.Length, program3FormInstance.send_buffer_DB21);
            if (writeResultDB1_CrossroadRightTTopCrosswalkBTN2 != 0)
            {
                //write error
                if (!errorMessageBoxShown)
                {
                    //MessageBox
                    MessageBox.Show("BE doesn't work properly. Data could´t be written to DB21!!! \n\n" +
                        $"Error message: writeResultDB1_CrossroadRightTTopCrosswalkBTN2 = {writeResultDB1_CrossroadRightTTopCrosswalkBTN2} \n", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                    errorMessageBoxShown = true;
                }
            }
            else
            {
                //write was successful
            }
            */
        }

        #endregion

        //Methods for rendering crossroad -> old
        #region Methods for rendering crossroad -> old

        //Crossroad1
        public void BasicCrossroad()
        {
            var g = this.CreateGraphics();

            //background
            g.Clear(Color.Black);

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
            g.Clear(Color.Black);

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
            g.Clear(Color.Black);

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
            g.Clear(Color.Black);

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

        //Methods for UpdateLights
        #region Methods for UpdateLights

        //Crossroad1
        #region Crosroad1
        public void UpdateTrafficLightsCrossroad1TOP(bool RedLight, bool YellowLight, bool GreenLight)
        {
            var g = this.CreateGraphics();

            if (RedLight)
            {
                //RedLight => ON
                g.FillEllipse(red, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else if (YellowLight)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
                //YellowLight => ON
                g.FillEllipse(yellow, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else if (GreenLight)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                //GreenLight => ON
                g.FillEllipse(green, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }

            Invalidate();
        }

        public void UpdateTrafficLightsCrossroad1LEFT(bool RedLight, bool YellowLight, bool GreenLight)
        {
            var g = this.CreateGraphics();

            if (RedLight)
            {
                //RedLight => ON
                g.FillEllipse(red, x + length + 3 * FreeSpace + TrafficLights_width * 2, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length + 3 * FreeSpace + TrafficLights_width, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else if (YellowLight)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length + 3 * FreeSpace + TrafficLights_width * 2, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => ON
                g.FillEllipse(yellow, x + length + 3 * FreeSpace + TrafficLights_width, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else if (GreenLight)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length + 3 * FreeSpace + TrafficLights_width * 2, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length + 3 * FreeSpace + TrafficLights_width, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => ON
                g.FillEllipse(green, x + length + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length + 3 * FreeSpace + TrafficLights_width * 2, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length + 3 * FreeSpace + TrafficLights_width, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
            }

            Invalidate();
        }

        public void UpdateTrafficLightsCrossroad1RIGHT(bool RedLight, bool YellowLight, bool GreenLight)
        {
            var g = this.CreateGraphics();

            if (RedLight)
            {
                //RedLight => ON
                g.FillEllipse(red, x + length * 6 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 6 + FreeSpace + TrafficLights_width, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 6 + FreeSpace + TrafficLights_width * 2, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else if (YellowLight)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 6 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => ON
                g.FillEllipse(yellow, x + length * 6 + FreeSpace + TrafficLights_width, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 6 + FreeSpace + TrafficLights_width * 2, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else if (GreenLight)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 6 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 6 + FreeSpace + TrafficLights_width, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => ON
                g.FillEllipse(green, x + length * 6 + FreeSpace + TrafficLights_width * 2, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 6 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 6 + FreeSpace + TrafficLights_width, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 6 + FreeSpace + TrafficLights_width * 2, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }

            Invalidate();
        }

        public void UpdateTrafficLightsCrossroad1BOTTOM(bool RedLight, bool YellowLight, bool GreenLight)
        {
            var g = this.CreateGraphics();

            if (RedLight)
            {
                //RedLight => ON
                g.FillEllipse(red, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
            }
            else if (YellowLight)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => ON
                g.FillEllipse(yellow, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
            }
            else if (GreenLight)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                //GreenLight => ON
                g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
            }
            else
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 5 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
            }

            Invalidate();
        }

        public void UpdateCrosswalkLightsCrossroad1TOP(bool RedLight1, bool GreenLight1, bool RedLight2, bool GreenLight2)
        {
            var g = this.CreateGraphics();

            if (RedLight1)
            {
                //RedLight => ON
                g.FillEllipse(red, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }
            else if (GreenLight1)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => ON
                g.FillEllipse(green, x + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }
            else
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 2 + crosswalk_width + 3 * FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }

            if (RedLight2)
            {
                //RedLight => ON
                g.FillEllipse(red, x + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 5 + FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }
            else if (GreenLight2)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => ON
                g.FillEllipse(green, x + length * 5 + FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }
            else
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 5 + FreeSpace, y + length * 2 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }

            Invalidate();
        }

        public void UpdateCrosswalkLightsCrossroad1LEFT(bool RedLight1, bool GreenLight1, bool RedLight2, bool GreenLight2)
        {
            var g = this.CreateGraphics();

            if (RedLight1)
            {
                //RedLight => ON
                g.FillEllipse(red, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }
            else if (GreenLight1)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => ON
                g.FillEllipse(green, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }
            else
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 2 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }

            if (RedLight2)
            {
                //RedLight => ON
                g.FillEllipse(red, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }
            else if (GreenLight2)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => ON
                g.FillEllipse(green, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }
            else
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 2 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }

            Invalidate();
        }

        #endregion

        //Crossroad2
        #region Crossroad2
        public void UpdateTrafficLightsCrossroad2TOP(bool RedLight, bool YellowLight, bool GreenLight)
        {
            var g = this.CreateGraphics();

            if (RedLight)
            {
                //RedLight => ON
                g.FillEllipse(red, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else if (YellowLight)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
                //YellowLight => ON
                g.FillEllipse(yellow, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else if (GreenLight)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                //GreenLight => ON
                g.FillEllipse(green, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }

            Invalidate();
        }

        public void UpdateTrafficLightsCrossroad2LEFT(bool RedLight, bool YellowLight, bool GreenLight)
        {
            var g = this.CreateGraphics();

            if (RedLight)
            {
                //RedLight => ON
                g.FillEllipse(red, x + length * 7 + 3 * FreeSpace + TrafficLights_width * 2, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 7 + 3 * FreeSpace + TrafficLights_width, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 7 + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else if (YellowLight)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 7 + 3 * FreeSpace + TrafficLights_width * 2, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => ON
                g.FillEllipse(yellow, x + length * 7 + 3 * FreeSpace + TrafficLights_width, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 7 + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else if (GreenLight)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 7 + 3 * FreeSpace + TrafficLights_width * 2, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 7 + 3 * FreeSpace + TrafficLights_width, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => ON
                g.FillEllipse(green, x + length * 7 + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 7 + 3 * FreeSpace + TrafficLights_width * 2, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 7 + 3 * FreeSpace + TrafficLights_width, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 7 + 3 * FreeSpace, y + length * 5 + FreeSpace, TrafficLights_width, TrafficLights_width);
            }

            Invalidate();
        }

        public void UpdateTrafficLightsCrossroad2RIGHT(bool RedLight, bool YellowLight, bool GreenLight)
        {
            var g = this.CreateGraphics();

            if (RedLight)
            {
                //RedLight => ON
                g.FillEllipse(red, x + length * 12 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 12 + FreeSpace + TrafficLights_width, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 12 + FreeSpace + TrafficLights_width * 2, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else if (YellowLight)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 12 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => ON
                g.FillEllipse(yellow, x + length * 12 + FreeSpace + TrafficLights_width, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 12 + FreeSpace + TrafficLights_width * 2, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else if (GreenLight)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 12 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 12 + FreeSpace + TrafficLights_width, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => ON
                g.FillEllipse(green, x + length * 12 + FreeSpace + TrafficLights_width * 2, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 12 + FreeSpace, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 12 + FreeSpace + TrafficLights_width, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 12 + FreeSpace + TrafficLights_width * 2, y + length * 2 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }

            Invalidate();
        }

        public void UpdateTrafficLightsCrossroad2BOTTOM(bool RedLight, bool YellowLight, bool GreenLight)
        {
            var g = this.CreateGraphics();

            if (RedLight)
            {
                //RedLight => ON
                g.FillEllipse(red, x + length * 11 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 11 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 11 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
            }
            else if (YellowLight)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 11 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => ON
                g.FillEllipse(yellow, x + length * 11 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 11 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
            }
            else if (GreenLight)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 11 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 11 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                //GreenLight => ON
                g.FillEllipse(green, x + length * 11 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
            }
            else
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 11 + FreeSpace, y + length * 6 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 11 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 11 + FreeSpace, y + length * 6 + FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
            }

            Invalidate();
        }

        public void UpdateCrosswalkLightsCrossroad2LEFT(bool RedLight1, bool GreenLight1, bool RedLight2, bool GreenLight2)
        {
            var g = this.CreateGraphics();

            if (RedLight1)
            {
                //RedLight => ON
                g.FillEllipse(red, x + length * 8 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 8 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }
            else if (GreenLight1)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 8 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => ON
                g.FillEllipse(green, x + length * 8 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }
            else
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 8 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 8 + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }

            if (RedLight2)
            {
                //RedLight => ON
                g.FillEllipse(red, x + length * 8 + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 8 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }
            else if (GreenLight2)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 8 + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => ON
                g.FillEllipse(green, x + length * 8 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }
            else
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 8 + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => ON
                g.FillEllipse(green, x + length * 8 + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }

            Invalidate();
        }

        public void UpdateCrosswalkLightsCrossroad2RIGHT(bool RedLight1, bool GreenLight1, bool RedLight2, bool GreenLight2)
        {
            var g = this.CreateGraphics();

            if (RedLight1)
            {
                //RedLight => ON
                g.FillEllipse(red, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }
            else if (GreenLight1)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => ON
                g.FillEllipse(green, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }
            else
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 2 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }

            if (RedLight2)
            {
                //RedLight => ON
                g.FillEllipse(red, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }
            else if (GreenLight2)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => ON
                g.FillEllipse(green, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }
            else
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 11 + 2 * crosswalk_width + FreeSpace, y + length * 5 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }

            Invalidate();
        }

        #endregion

        //LeftT
        #region LeftT
        public void UpdateTrafficLightsCrossroadLeftTTOP(bool RedLight, bool YellowLight, bool GreenLight)
        {
            var g = this.CreateGraphics();

            if (RedLight)
            {
                //RedLight => ON
                g.FillEllipse(red, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else if (YellowLight)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
                //YellowLight => ON
                g.FillEllipse(yellow, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else if (GreenLight)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                //GreenLight => ON
                g.FillEllipse(green, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 2 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }

            Invalidate();
        }

        public void UpdateTrafficLightsCrossroadLeftTLEFT(bool RedLight, bool YellowLight, bool GreenLight)
        {
            var g = this.CreateGraphics();

            if (RedLight)
            {
                //RedLight => ON
                g.FillEllipse(red, x + length + 3 * FreeSpace + TrafficLights_width * 2, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length + 3 * FreeSpace + TrafficLights_width, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length + 3 * FreeSpace, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else if (YellowLight)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length + 3 * FreeSpace + TrafficLights_width * 2, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => ON
                g.FillEllipse(yellow, x + length + 3 * FreeSpace + TrafficLights_width, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length + 3 * FreeSpace, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else if (GreenLight)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length + 3 * FreeSpace + TrafficLights_width * 2, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length + 3 * FreeSpace + TrafficLights_width, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => ON
                g.FillEllipse(green, x + length + 3 * FreeSpace, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length + 3 * FreeSpace + TrafficLights_width * 2, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length + 3 * FreeSpace + TrafficLights_width, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length + 3 * FreeSpace, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
            }

            Invalidate();
        }

        public void UpdateTrafficLightsCrossroadLeftTRIGHT(bool RedLight, bool YellowLight, bool GreenLight)
        {
            var g = this.CreateGraphics();

            if (RedLight)
            {
                //RedLight => ON
                g.FillEllipse(red, x + length * 6 + FreeSpace, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 6 + FreeSpace + TrafficLights_width, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 6 + FreeSpace + TrafficLights_width * 2, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else if (YellowLight)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 6 + FreeSpace, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => ON
                g.FillEllipse(yellow, x + length * 6 + FreeSpace + TrafficLights_width, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 6 + FreeSpace + TrafficLights_width * 2, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else if (GreenLight)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 6 + FreeSpace, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 6 + FreeSpace + TrafficLights_width, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => ON
                g.FillEllipse(green, x + length * 6 + FreeSpace + TrafficLights_width * 2, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 6 + FreeSpace, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 6 + FreeSpace + TrafficLights_width, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 6 + FreeSpace + TrafficLights_width * 2, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }

            Invalidate();
        }

        public void UpdateCrosswalkLightsCrossroadLeftTLEFT(bool RedLight1, bool GreenLight1, bool RedLight2, bool GreenLight2)
        {
            var g = this.CreateGraphics();

            if (RedLight1)
            {
                //RedLight => ON
                g.FillEllipse(red, x + length * 2 + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 2 + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }
            else if (GreenLight1)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 2 + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => ON
                g.FillEllipse(green, x + length * 2 + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }
            else
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 2 + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 2 + FreeSpace, y + length * 8 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }

            if (RedLight2)
            {
                //RedLight => ON
                g.FillEllipse(red, x + length * 2 + FreeSpace, y + length * 11 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 2 + FreeSpace, y + length * 11 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }
            else if (GreenLight2)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 2 + FreeSpace, y + length * 11 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => ON
                g.FillEllipse(green, x + length * 2 + FreeSpace, y + length * 11 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }
            else
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 2 + FreeSpace, y + length * 11 + FreeSpace + TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 2 + FreeSpace, y + length * 11 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }

            Invalidate();
        }

        #endregion

        //RightT
        #region RightT
        public void UpdateTrafficLightsCrossroadRightTTOP(bool RedLight, bool YellowLight, bool GreenLight)
        {
            var g = this.CreateGraphics();

            if (RedLight)
            {
                //RedLight => ON
                g.FillEllipse(red, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else if (YellowLight)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
                //YellowLight => ON
                g.FillEllipse(yellow, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else if (GreenLight)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                //GreenLight => ON
                g.FillEllipse(green, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace + TrafficLights_width * 2, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace + TrafficLights_width, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 8 + 3 * FreeSpace + 2 * crosswalk_width, y + length * 7 + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }

            Invalidate();
        }

        public void UpdateTrafficLightsCrossroadRightTLEFT(bool RedLight, bool YellowLight, bool GreenLight)
        {
            var g = this.CreateGraphics();

            if (RedLight)
            {
                //RedLight => ON
                g.FillEllipse(red, x + length * 7 + 3 * FreeSpace + TrafficLights_width * 2, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 7 + 3 * FreeSpace + TrafficLights_width, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 7 + 3 * FreeSpace, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else if (YellowLight)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 7 + 3 * FreeSpace + TrafficLights_width * 2, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => ON
                g.FillEllipse(yellow, x + length * 7 + 3 * FreeSpace + TrafficLights_width, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 7 + 3 * FreeSpace, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else if (GreenLight)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 7 + 3 * FreeSpace + TrafficLights_width * 2, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 7 + 3 * FreeSpace + TrafficLights_width, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => ON
                g.FillEllipse(green, x + length * 7 + 3 * FreeSpace, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 7 + 3 * FreeSpace + TrafficLights_width * 2, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 7 + 3 * FreeSpace + TrafficLights_width, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 7 + 3 * FreeSpace, y + length * 11 + FreeSpace, TrafficLights_width, TrafficLights_width);
            }

            Invalidate();
        }

        public void UpdateTrafficLightsCrossroadRightTRIGHT(bool RedLight, bool YellowLight, bool GreenLight)
        {
            var g = this.CreateGraphics();

            if (RedLight)
            {
                //RedLight => ON
                g.FillEllipse(red, x + length * 12 + FreeSpace, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 12 + FreeSpace + TrafficLights_width, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 12 + FreeSpace + TrafficLights_width * 2, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else if (YellowLight)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 12 + FreeSpace, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => ON
                g.FillEllipse(yellow, x + length * 12 + FreeSpace + TrafficLights_width, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 12 + FreeSpace + TrafficLights_width * 2, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else if (GreenLight)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 12 + FreeSpace, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 12 + FreeSpace + TrafficLights_width, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => ON
                g.FillEllipse(green, x + length * 12 + FreeSpace + TrafficLights_width * 2, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }
            else
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 12 + FreeSpace, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //YellowLight => OFF
                g.FillEllipse(black, x + length * 12 + FreeSpace + TrafficLights_width, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 12 + FreeSpace + TrafficLights_width * 2, y + length * 8 + 2 * crosswalk_width + 3 * FreeSpace, TrafficLights_width, TrafficLights_width);
            }

            Invalidate();
        }

        public void UpdateCrosswalkLightsCrossroadRightTTOP(bool RedLight1, bool GreenLight1, bool RedLight2, bool GreenLight2)
        {
            var g = this.CreateGraphics();

            if (RedLight1)
            {
                //RedLight => ON
                g.FillEllipse(red, x + length * 8 + crosswalk_width + 3 * FreeSpace, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 8 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }
            else if (GreenLight1)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 8 + crosswalk_width + 3 * FreeSpace, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => ON
                g.FillEllipse(green, x + length * 8 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }
            else
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 8 + crosswalk_width + 3 * FreeSpace, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 8 + crosswalk_width + 3 * FreeSpace + TrafficLightsCrosswalk_width, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }

            if (RedLight2)
            {
                //RedLight => ON
                g.FillEllipse(red, x + length * 11 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 11 + FreeSpace, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }
            else if (GreenLight2)
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 11 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => ON
                g.FillEllipse(green, x + length * 11 + FreeSpace, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }
            else
            {
                //RedLight => OFF
                g.FillEllipse(black, x + length * 11 + FreeSpace + TrafficLightsCrosswalk_width, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
                //GreenLight => OFF
                g.FillEllipse(black, x + length * 11 + FreeSpace, y + length * 8 + FreeSpace, TrafficLightsCrosswalk_width, TrafficLightsCrosswalk_width);
            }

            Invalidate();
        }

        #endregion

        #endregion

    }
}

