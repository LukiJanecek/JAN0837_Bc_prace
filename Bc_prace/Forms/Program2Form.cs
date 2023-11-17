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
using Bc_prace.Settings;

namespace Bc_prace
{
    public partial class Program2Form : Form
    {


        public Program2Form()
        {
            InitializeComponent();
        }

        //Varialbes
        #region Variables

        //Count
        int countWax = 0;
        int countSoap = 0;
        int countActiveFoam = 0;

        #endregion

        //Start CarWash
        #region Start CarWash
        private void btnStartCarWash_Click(object sender, EventArgs e)
        {
            Program2SelectionForm Selection = new Program2SelectionForm();
            Selection.ShowDialog(this);
        }
        #endregion

        //Settings
        #region Settings 
        private void btnSettings_Click(object sender, EventArgs e)
        {
            statusStripCarWash.Items.Clear();
            ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Running Settings");
            statusStripCarWash.Items.Add(lblStatus);

            Program2SettingsForm Settings = new Program2SettingsForm();
            Settings.ShowDialog(this);
        }
        #endregion

        private void Program2_Load(object sender, EventArgs e)
        {



        }

        //Emergency + system error 
        #region Emergency + system error 
        private void btnEmergency_Click(object sender, EventArgs e)
        {
            statusStripCarWash.Items.Clear();
            ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Emergency mode activated");
            statusStripCarWash.Items.Add(lblStatus);
        }
        #endregion

        //btn End 
        #region Close window 
        private void btnEnd_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion


    }
}
