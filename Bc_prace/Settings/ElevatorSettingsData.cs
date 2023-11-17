using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bc_prace.Settings
{
    public class ElevatorSettingsData
    {
        //private const string GENERAL_CAT = "0: Obecné";
        //private const string SERVER_CAT = "1: Server";

        private const string TIAVARIABLES_CAT = "1: Tia Portal Variables";
        private const string TIATIMEVARIABLES_CAT = "2: Tia Protal Time Variables";

        /*
        #region GENERAL
        [Browsable(true)]
        [DisplayName("Adresa")]
        [Category(GENERAL_CAT)]
        public string Address { get; set; }
        #endregion
        */

        /*
        #region SERVER
        [Browsable(true)]
        [DisplayName("Port")]
        [Category(SERVER_CAT)]
        public int Port { get; set; }
        #endregion
        */

        #region TIAVARIABLES
        [Browsable(true)]
        [DisplayName("Speed")]
        [Category(TIAVARIABLES_CAT)]
        public string ElevatorSpeed { get; set; }
        
        [Browsable(true)]
        [DisplayName("Inactivity_Time")]
        [Category(TIATIMEVARIABLES_CAT)]
        public string InactivityTime { get; set;}

        [Browsable(true)]
        [DisplayName("Time_Door_OPEN")]
        [Category(TIATIMEVARIABLES_CAT)]
        public string TimeDoorOPEN { get; set;}

        [Browsable(true)]
        [DisplayName("Time_Door_CLOSE")]
        [Category(TIATIMEVARIABLES_CAT)]
        public string TimeDoorCLOSE { get; set;}    


        #endregion

    }
}
