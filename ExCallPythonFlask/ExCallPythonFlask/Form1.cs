using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ExCallPythonFlask
{
    public partial class Form1 : Form
    {
        private System.Threading.Timer t;
        private string command2Python;
        private DateTime timeStart;
        public Form1()
        {
            InitializeComponent();
            command2Python = "";
            timer1.Start();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            command2Python = "send";
            timeStart = DateTime.Now;
            t = new System.Threading.Timer(new TimerCallback(TickTimer),
                null,
                1000,
                1000);
        }

        void TickTimer(object state)
        {
            if (command2Python == "send")
            {
                setRespText("");
                setlbTimeText("0.0");
                command2Python = "waitSend";
                var client = new RestClient(tbURL.Text);
                // client.Authenticator = new HttpBasicAuthenticator(username, password);
                var request = new RestRequest("");
                request.AddParameter("video", tbVName.Text);
                //request.AddHeader("header", "value");
                //request.AddFile("file", path);
                var response = client.Post(request);
                var content = response.Content;
                setRespText(content);
                command2Python = "";
            }
        }

        private void setlbTimeText(string txt)
        {
            if (lbTime.InvokeRequired)
            {
                lbTime.Invoke((MethodInvoker)delegate
                {
                    lbTime.Text = txt;
                });
            }
        }

        private void setRespText(string txt)
        {
            if (tbResponse.InvokeRequired)
            {
                tbResponse.Invoke((MethodInvoker)delegate
                {
                    tbResponse.Text = txt;
                });
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (command2Python == "waitSend")
            {
                var diff = DateTime.Now - timeStart;
                lbTime.Text = Math.Round(diff.TotalSeconds,2).ToString();
                //setTimeResText(diff.TotalSeconds.ToString());
            }else if(command2Python == "")
            {
                //lbTime.Text = command2Python;
            }
            //lbTime.Text = command2Python;
        }
    }
}
