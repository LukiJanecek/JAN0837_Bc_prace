using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAN0837_BP.Classes
{
    public class CarWash_DB_Class
    {
        //input
        public bool CarWashEmergencySTOP { get; set; }
        public bool CarWashErrorSystem { get; set; }
        public bool CarWashStartCarWash { get; set; }
        public bool CarWashWaitingForIncomingCar { get; set; }
        public bool CarWashWaitingForOutgoingCar { get; set; }
        public bool CarWashPerfetWash { get; set; }
        public bool CarWashPerfectPolish { get; set; }
        public bool CarWashPositionShower { get; set; }
        public bool CarWashPositionCar { get; set; }

        //output
        public bool CarWashGreenLight { get; set; }
        public bool CarWashRedLight { get; set; }
        public bool CarWashYellowLight { get; set; }
        public bool CarWashDoor1UP { get; set; }
        public bool CarWashDoor1DOWN { get; set; }
        public bool CarWashDoor2UP { get; set; }
        public bool CarWashDoor2DOWN { get; set; }
        public bool CarWashWater { get; set; }
        public bool CarWashWashingChemicalsFRONT { get; set; }
        public bool CarWashWashingChemicalsSIDES { get; set; }
        public bool CarWashWashingChemicalsBACK { get; set; }
        public bool CarWashWax { get; set; }
        public bool CarWashVarnishProtection { get; set; }
        public bool CarWashDry { get; set; }
        public bool CarWashPreWash { get; set; }
        public bool CarWashBrushes { get; set; }
        public bool CarWashSoap { get; set; }
        public bool CarWashActiveFoam { get; set; }
        public int CarWashTimeDoorMovement { get; set; } //time

        //MEMs
        public bool CarWashMEMDoor { get; set; }
        public bool CarWashMEMDoorTrig { get; set; }
        public bool CarWashMEMDoorCloseTrig { get; set; }

    }
}
