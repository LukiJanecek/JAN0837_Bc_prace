using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAN0837_BP.Classes
{
    public class CrossroadDB_Class
    {
        //input
        //Crossroad_DB DB14
        public bool CrossroadModeOFF { get; set; }
        public bool CrossroadModeNIGHT { get; set; }
        public bool CrossroadModeDAY { get; set; }
        public bool CrossroadEmergencySTOP { get; set; }
        public bool CrossroadErrorSystem { get; set; }
        //Crossroad_1_DB DB1
        public bool Crossroad1LeftCrosswalkBTN1 { get; set; }
        public bool Crossroad1LeftCrosswalkBTN2 { get; set; }
        public bool Crossroad1TopCrosswalkBTN1 { get; set; }
        public bool Crossroad1TopCrosswalkBTN2 { get; set; }
        //Crossroad_2_DB DB19
        public bool Crossroad2LeftCrosswalkBTN1 { get; set; }
        public bool Crossroad2LeftCrosswalkBTN2 { get; set; }
        public bool Crossroad2RightCrosswalkBTN1 { get; set; }
        public bool Crossroad2RightCrosswalkBTN2 { get; set; }
        //Crossroad_LeftT_DB DB20
        public bool CrossroadLeftTLeftCrosswalkBTN1 { get; set; }
        public bool CrossroadLeftTLeftCrosswalkBTN2 { get; set; }
        //Crossroad_RightT_DB DB21
        public bool CrossroadRightTTopCrosswalkBTN1 { get; set; }
        public bool CrossroadRightTTopCrosswalkBTN2 { get; set; }

        //ouput
        //Crossroad_DB DB14
        public int TrafficLightsSQ;
        //Crossroad_1_DB DB1
        public int Crossroad1CrosswalkSQ { get; set; }
        public bool Crossroad1TopRED { get; set; }
        public bool Crossroad1TopGREEN { get; set; }
        public bool Crossroad1TopYELLOW { get; set; }
        public bool Crossroad1LeftRED { get; set; }
        public bool Crossroad1LeftGREEN { get; set; }
        public bool Crossroad1LeftYELLOW { get; set; }
        public bool Crossroad1RightRED { get; set; }
        public bool Crossroad1RightGREEN { get; set; }
        public bool Crossroad1RightYELLOW { get; set; }
        public bool Crossroad1BottomRED { get; set; }
        public bool Crossroad1BottomGREEN { get; set; }
        public bool Crossroad1BottomYELLOW { get; set; }
        public bool Crossroad1TopCrosswalkRED1 { get; set; }
        public bool Crossroad1TopCrosswalkRED2 { get; set; }
        public bool Crossroad1TopCrosswalkGREEN1 { get; set; }
        public bool Crossroad1TopCrosswalkGREEN2 { get; set; }
        public bool Crossroad1LeftCrosswalkRED1 { get; set; }
        public bool Crossroad1LeftCrosswalkRED2 { get; set; }
        public bool Crossroad1LeftCrosswalkGREEN1 { get; set; }
        public bool Crossroad1LeftCrosswalkGREEN2 { get; set; }
        //Crossroad_2_DB DB19
        public int Crossroad2CrosswalkSQ { get; set; }
        public bool Crossroad2TopRED { get; set; }
        public bool Crossroad2TopGREEN { get; set; }
        public bool Crossroad2TopYELLOW { get; set; }
        public bool Crossroad2LeftRED { get; set; }
        public bool Crossroad2LeftGREEN { get; set; }
        public bool Crossroad2LeftYELLOW { get; set; }
        public bool Crossroad2RightRED { get; set; }
        public bool Crossroad2RightGREEN { get; set; }
        public bool Crossroad2RightYELLOW { get; set; }
        public bool Crossroad2BottomRED { get; set; }
        public bool Crossroad2BottomGREEN { get; set; }
        public bool Crossroad2BottomYELLOW { get; set; }
        public bool Crossroad2LeftCrosswalkRED1 { get; set; }
        public bool Crossroad2LeftCrosswalkRED2 { get; set; }
        public bool Crossroad2LeftCrosswalkGREEN1 { get; set; }
        public bool Crossroad2LeftCrosswalkGREEN2 { get; set; }
        public bool Crossroad2RightCrosswalkRED1 { get; set; }
        public bool Crossroad2RightCrosswalkRED2 { get; set; }
        public bool Crossroad2RightCrosswalkGREEN1 { get; set; }
        public bool Crossroad2RightCrosswalkGREEN2 { get; set; }
        //Crossroad_LeftT_DB DB20
        public int CrossroadLeftTCrosswalkSQ { get; set; }
        public bool CrossroadLeftTTopRED { get; set; }
        public bool CrossroadLeftTTopGREEN { get; set; }
        public bool CrossroadLeftTTopYELLOW { get; set; }
        public bool CrossroadLeftTLeftRED { get; set; }
        public bool CrossroadLeftTLeftGREEN { get; set; }
        public bool CrossroadLeftTLeftYELLOW { get; set; }
        public bool CrossroadLeftTRightRED { get; set; }
        public bool CrossroadLeftTRightGREEN { get; set; }
        public bool CrossroadLeftTRightYELLOW { get; set; }
        public bool CrossroadLeftTLeftCrosswalkRED1 { get; set; }
        public bool CrossroadLeftTLeftCrosswalkRED2 { get; set; }
        public bool CrossroadLeftTLeftCrosswalkGREEN1 { get; set; }
        public bool CrossroadLeftTLeftCrosswalkGREEN2 { get; set; }
        //Crossroad_RightT_DB DB21
        public int CrossroadRightTCrosswalkSQ { get; set; }
        public bool CrossroadRightTTopRED { get; set; }
        public bool CrossroadRightTTopGREEN { get; set; }
        public bool CrossroadRightTTopYELLOW { get; set; }
        public bool CrossroadRightTLeftRED { get; set; }
        public bool CrossroadRightTLeftGREEN { get; set; }
        public bool CrossroadRightTLeftYELLOW { get; set; }
        public bool CrossroadRightTRightRED { get; set; }
        public bool CrossroadRightTRightGREEN { get; set; }
        public bool CrossroadRightTRightYELLOW { get; set; }
        public bool CrossroadRightTTopCrosswalkRED1 { get; set; }
        public bool CrossroadRightTTopCrosswalkRED2 { get; set; }
        public bool CrossroadRightTTopCrosswalkGREEN1 { get; set; }
        public bool CrossroadRightTTopCrosswalkGREEN2 { get; set; }

        //MEMs


    }
}
