using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAN0837_BP.Classes
{
    public class ElevatorDB_Class
    {
        //input
        public bool ElevatorBTNCabin1 { get; set; }
        public bool ElevatorBTNCabin2 { get; set; }
        public bool ElevatorBTNCabin3 { get; set; }
        public bool ElevatorBTNCabin4 { get; set; }
        public bool ElevatorBTNCabin5 { get; set; }
        public bool ElevatorBTNFloor1 { get; set; }
        public bool ElevatorBTNFloor2 { get; set; }
        public bool ElevatorBTNFloor3 { get; set; }
        public bool ElevatorBTNFloor4 { get; set; }
        public bool ElevatorBTNFloor5 { get; set; }
        public bool ElevatorDoorSEQ { get; set; }
        public bool ElevatorBTNOPENCLOSE { get; set; }
        public bool ElevatorEmergencySTOP { get; set; }
        public bool ElevatorErrorSystem { get; set; }
        public bool ElevatorActualFloorSENS1 { get; set; }
        public bool ElevatorActualFloorSENS2 { get; set; }
        public bool ElevatorActualFloorSENS3 { get; set; }
        public bool ElevatorActualFloorSENS4 { get; set; }
        public bool ElevatorActualFloorSENS5 { get; set; }
        public bool ElevatorDoorClOSE { get; set; }
        public bool ElevatorDoorOPEN { get; set; }
        public bool ElevatorInactivity { get; set; }

        //output
        public bool ElevatorMotorON { get; set; }
        public bool ElevatorMotorDOWN { get; set; }
        public bool ElevatorMotorUP { get; set; }
        public bool ElevatroHoming { get; set; }
        public bool ElevatorSystemReady { get; set; }
        public int ElevatorActualFloor { get; set; }
        public bool ElevatorMoving { get; set; }
        public bool ElevatorSystemWorking { get; set; }
        public int ElevatorGoToFloor { get; set; }
        public bool ElevatorDirection { get; set; }
        public bool ElevatorActualFloorLED1 { get; set; }
        public bool ElevatorActualFloorLED2 { get; set; }
        public bool ElevatorActualFloorLED3 { get; set; }
        public bool ElevatorActualFloorLED4 { get; set; }
        public bool ElevatorActualFloorLED5 { get; set; }
        public bool ElevatorActualFloorCabinLED1 { get; set; }
        public bool ElevatorActualFloorCabinLED2 { get; set; }
        public bool ElevatorActualFloorCabinLED3 { get; set; }
        public bool ElevatorActualFloorCabinLED4 { get; set; }
        public bool ElevatorActualFloorCabinLED5 { get; set; }
        public int ElevatorTimeDoorSQOPEN { get; set; } //time
        public int ElevatroTimeDoorSQCLOSE { get; set; } //time
        public int ElevatorCabinSpeed { get; set; }
        public int ElevatorTimeToGetDown { get; set; } //time

        //MEMs
        public bool ElevatorMEMDoor { get; set; }
        public bool ElevatorMEMDoorTrig { get; set; }
        public bool ElevatorMEMDoorCloseTrig { get; set; }
        public bool ElevatorMEMMovingtrig { get; set; }
        public bool ElevatorMEMEndMovingTrig { get; set; }
        public bool ElevatorMEMBTNFloor1 { get; set; }
        public bool ElevatorMEMBTNFloor2 { get; set; }
        public bool ElevatorMEMBTNFloor3 { get; set; }
        public bool ElevatorMEMBTNFloor4 { get; set; }
        public bool ElevatorMEMBTNFloor5 { get; set; }
    }
}
