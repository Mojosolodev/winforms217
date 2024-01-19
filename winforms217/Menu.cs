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
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }
        public void loadform(object form)
        {
            if(this.mainPanel.Controls.Count>0)
                this.mainPanel.Controls.RemoveAt(0);
            Form f = form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.mainPanel.Controls.Add(f);
            this.mainPanel.Tag = f;
            f.Show();
        }

        private void Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            loadform(new ListeResidents());
            bunifuFlatButton1.Enabled = false;
            bunifuFlatButton2.Enabled = true;
            bunifuFlatButton3.Enabled = true;
            bunifuFlatButton4.Enabled = true;
            bunifuFlatButton6.Enabled = true;
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            loadform(new Enregistrement());
            bunifuFlatButton1.Enabled = true;
            bunifuFlatButton2.Enabled = true;
            bunifuFlatButton3.Enabled = false;
            bunifuFlatButton4.Enabled = true;
            bunifuFlatButton6.Enabled = true;
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            loadform(new Chambres());
            bunifuFlatButton1.Enabled = true;
            bunifuFlatButton2.Enabled = false;
            bunifuFlatButton3.Enabled = true;
            bunifuFlatButton4.Enabled = true;
            bunifuFlatButton6.Enabled = true;
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            loadform(new ChiffreDaffaire());
            bunifuFlatButton1.Enabled = true;
            bunifuFlatButton2.Enabled = true;
            bunifuFlatButton3.Enabled = true;
            bunifuFlatButton4.Enabled = false;
            bunifuFlatButton6.Enabled = true;
        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            //logout button
            Login loginpage = new Login();
            loginpage.Show();
            this.Hide();
        }

        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
            loadform(new Batiment());
            bunifuFlatButton1.Enabled = true;
            bunifuFlatButton2.Enabled = true;
            bunifuFlatButton3.Enabled = true;
            bunifuFlatButton4.Enabled = true;
            bunifuFlatButton6.Enabled = false;
        }
    }
}
