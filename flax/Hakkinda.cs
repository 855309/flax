using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace flax
{
    public partial class Hakkinda : Form
    {
        public Hakkinda()
        {
            InitializeComponent();
        }

        private void Hakkinda_Load(object sender, EventArgs e)
        {
            transpar(label1);
            transpar(label2);
            transpar(label3);
            transpar(label4);
            transpar(linkLabel1);
            transpar(linkLabel2);
            textBox1.SelectedText = "";
        }

        public void transpar(Label label1)
        {
            var pos = this.PointToScreen(label1.Location);
            pos = pictureBox2.PointToClient(pos);
            label1.Parent = pictureBox2;
            label1.Location = pos;
            label1.BackColor = Color.Transparent;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/wgex");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.turkhackteam.org/members/846375.html");
        }
    }
}
