using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bc_prace.Settings;

namespace Bc_prace
{
    public partial class Program2SelectionForm : Form
    {
        public Program2SelectionForm()
        {
            InitializeComponent();
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

