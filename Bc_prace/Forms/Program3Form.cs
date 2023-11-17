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
    public partial class Program3Form : Form
    {
        public Program3Form()
        {
            InitializeComponent();
        }

        //Settings
        #region Settings 
        private void btnSettings_Click(object sender, EventArgs e)
        {
            statusStripGarage.Items.Clear();
            ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Running Settings");
            statusStripGarage.Items.Add(lblStatus);

            Program3SettingsForm Settings = new Program3SettingsForm();
            Settings.ShowDialog(this);
        }
        #endregion


        private void Program3_Load(object sender, EventArgs e)
        {

        }

        //Emergency + system error 
        #region Emergency + system error 
        private void btnEmergency_Click(object sender, EventArgs e)
        {

        }
        #endregion

        //btn End 
        #region Close window 
        private void btnEnd_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
