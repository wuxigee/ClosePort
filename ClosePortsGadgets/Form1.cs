using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net;
// 下载于www.mycodes.net
namespace 关闭端口小工具
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Process p;
        private void button1_Click(object sender, EventArgs e)
        {
            SendMsgToCMD(string.Format("netstat -aon|findstr {0}", textBox1.Text));
        }

        private void SendMsgToCMD(string CMD)
        {
            p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            //要执行的程序名称
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            //可能接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardOutput = true;
            //由调用程序获取输出信息
            p.StartInfo.CreateNoWindow = true;
            p.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
            p.Start();//启动程序
            //向CMD窗口发送输入信息：
            p.StandardInput.WriteLine(CMD);
            p.BeginOutputReadLine();
        }

        void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!String.IsNullOrEmpty(e.Data))
            {
                StringBuilder sb = new StringBuilder(this.textBox1.Text);
                this.Invoke(new MethodInvoker(() =>
                {
                    this.listBox1.Items.Add(e.Data.ToString());

                    listBox1.SelectedIndex = listBox1.Items.Count - 1;
                }));
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (p != null)
                p.Close();
        }

        private void toolbtnClose_Click(object sender, EventArgs e)
        {
            string tmpmsg = listBox1.SelectedItem.ToString();
            string CMDMsg = tmpmsg.Substring(tmpmsg.LastIndexOf(' '));
            SendMsgToCMD(string.Format("tasklist|findstr {0}", CMDMsg));
        }

        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (listBox1.Items == null || listBox1.SelectedItem == null) return;

            if (e.Button == MouseButtons.Right
               && (listBox1.SelectedItem.ToString().Contains("TCP")
               || listBox1.SelectedItem.ToString().Contains("UDP")))
                listBox1.ContextMenuStrip = contextMenuStrip1;
            else
                listBox1.ContextMenuStrip = null;
        }

        /**0
         * 关闭端口
         * **/
        private void btnClosePoint_Click(object sender, EventArgs e)
        {
            string tmpmsg = listBox1.SelectedItem.ToString();
            string CMDMsg = tmpmsg.Substring(tmpmsg.LastIndexOf (' '));
            SendMsgToCMD(string.Format("taskkill /f /t /im {0}", CMDMsg));
        }

    }
}
