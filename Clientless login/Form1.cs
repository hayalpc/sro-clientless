using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SilkroadSecurityApi;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;
namespace Clientless_login
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Globals.MainWindow = this;
            CheckForIllegalCrossThreadCalls = false;
        }
        public void Log(string msg,params object[] values)
        {
            msg = string.Format(msg, values);
            logs.AppendText(msg + "\n");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Gateway.StartThread();
        }
        
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Login.sendCredential();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Login.sendCaptcha();
        }

        private void button4_Click(object sender, EventArgs e)
        {
           // Login.sendCharName();
        }

        private void Ping_Tick(object sender, EventArgs e)
        {
            if (Globals.Server != Globals.ServerEnum.None)
            {
                Packet response = new Packet(0x2002);
                if (Globals.Server == Globals.ServerEnum.Gateway)
                {
                    //Gateway.SendToServer(response);
                }
                else if (Globals.Server == Globals.ServerEnum.Agent)
                {
                    //Agent.Send(response);
                }
            }
        }

    }
}
