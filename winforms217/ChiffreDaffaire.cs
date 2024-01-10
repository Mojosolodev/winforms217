using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace winforms217
{
    public partial class ChiffreDaffaire : Form
    {
        public ChiffreDaffaire()
        {
            InitializeComponent();
        }

        private void ChiffreDaffaire_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
