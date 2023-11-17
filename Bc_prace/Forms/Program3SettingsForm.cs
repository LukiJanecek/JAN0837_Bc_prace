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
    public partial class Program3SettingsForm : Form
    {
        public Program3SettingsForm()
        {
            InitializeComponent();
        }

        private void Program3Settings_Load(object sender, EventArgs e)
        {

        }

        //btn End
        #region Close window 
        private void btnEnd_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
