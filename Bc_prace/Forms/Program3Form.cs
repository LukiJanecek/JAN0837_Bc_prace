using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bc_prace.Controls;
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
            statusStripCrossroad.Items.Clear();
            ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Running Settings");
            statusStripCrossroad.Items.Add(lblStatus);

            Program3SettingsForm Settings = new Program3SettingsForm();
            Settings.ShowDialog(this);
        }
        #endregion


        private void Program3_Load(object sender, EventArgs e)
        {
            rBtnCrossroadBasic.Checked = true;
            rBtnCrossroadExtension1.Checked = false;
            rBtnCrossroadExtension2.Checked = false;
            rBtnCrossroadExtension3.Checked = false;

            Draw();

        }

        private void Draw()
        {
            if (rBtnCrossroadBasic.Checked)
            {
                statusStripCrossroad.Items.Clear();
                ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Basic crossroad");
                statusStripCrossroad.Items.Add(lblStatus);

                // Vykreslete čáry pro křižovatku
                userControlCrossroad1.BasicCrossroad();
            }
            else if (rBtnCrossroadExtension1.Checked)
            {
                statusStripCrossroad.Items.Clear();
                ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Crossroad extension 1");
                statusStripCrossroad.Items.Add(lblStatus);

                // Vykreslete čáry pro volbu 1
                userControlCrossroad1.CrossroadExtension1();
            }
            else if (rBtnCrossroadExtension2.Checked)
            {
                statusStripCrossroad.Items.Clear();
                ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Crossroad extension 1");
                statusStripCrossroad.Items.Add(lblStatus);

                // Vykreslete čáry pro volbu 2
                userControlCrossroad1.CrossroadExtension2();

            }
            else if (rBtnCrossroadExtension3.Checked)
            {
                statusStripCrossroad.Items.Clear();
                ToolStripStatusLabel lblStatus = new ToolStripStatusLabel("Crossroad extension 1");
                statusStripCrossroad.Items.Add(lblStatus);

                // Vykreslete čáry pro volbu 3
                userControlCrossroad1.CrossroadExtension3();

            }
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
