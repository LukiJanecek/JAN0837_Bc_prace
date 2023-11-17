using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bc_prace.Extensions;
using Bc_prace.Settings;

namespace Bc_prace
{
    public partial class Program2SettingsForm : Form
    {
        public Program2SettingsForm()
        {
            InitializeComponent();
            DialogResult = DialogResult.Cancel;
            propertyGridCarWash.SelectedObject = Program.AppSettings.Data.DeepClone();
        }

        private void Program2Settings_Load(object sender, EventArgs e)
        {

        }

        //btn End 
        #region Close window 
        private void btnEnd_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        //Emergency + system error 
        #region Emergency + system error 
        private void btnEmergencySim_Click(object sender, EventArgs e)
        {
            /*
            statusStripCarWash.Items.Clear();
            ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Emergency mode activated");
            statusStripCarWash.Items.Add(lblStatus);
            */
        }
        #endregion

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
