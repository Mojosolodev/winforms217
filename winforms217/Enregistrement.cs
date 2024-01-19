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
    public partial class Enregistrement : Form
    {
        public Enregistrement()
        {
            InitializeComponent();
        }
        private void Enregistrement_Load(object sender, EventArgs e)
        {
            //pas handicape au debut
            radioButton2.Checked = true;
            //male au debut
            radioButton3.Checked = true;
        }
        private void Enregistrement_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            bool handicap=false;
            if(radioButton1.Checked)
            {
                handicap= true;
            }
            else if(radioButton2.Checked)
            {
                handicap= false;
            }
            char sexe='m';
            if(radioButton3.Checked)
            {
                sexe = 'm';
            }
            else if(radioButton4.Checked)
            {
                sexe = 'f';
            }
            
            Etudiant etudiant = new Etudiant(textBox4.Text,textBox1.Text,sexe,textBox2.Text,handicap,dateTimePicker1.Value.ToString(),"NULL");
            //ajouter etudiant dans bd
            etudiant.ajouter_etudiant();
            //vider les textboxs
            textBox1.Text = "";
            textBox2.Text = "";
            textBox4.Text = "";
            textBox6.Text = "";
            textBox8.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked=true; 
            radioButton3.Checked=true;
            radioButton4.Checked=false;
        }

        
    }
}
