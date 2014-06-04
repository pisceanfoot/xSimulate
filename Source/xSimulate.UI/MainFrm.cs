using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace xSimulate.UI
{
    public partial class MainFrm : Form
    {
        public MainFrm()
        {
            InitializeComponent();
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            LoginFrm loginFrm = new LoginFrm();
            if (loginFrm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                LblUserName.Text = SessionContext.CustomerInfo.CustomerID;
                LblAmount.Text = SessionContext.CustomerInfo.Account.Amount.ToString("f");
            }
            else
            {
                this.Close();
            }
        }

        private void BtnRun_Click(object sender, EventArgs e)
        {
            if (BtnRun.Tag.ToString() == "run")
            {
                this.BtnRun.Text = "停止";
                this.BtnRun.Tag = "stop";
                StopAgent();
            }
            else
            {
                this.BtnRun.Text = "运行";
                this.BtnRun.Tag = "run";
                RunAgent();
            }
        }

        private Process agentProcess = null;
        private void RunAgent()
        {
            if (agentProcess == null)
            {
                agentProcess = new Process();
                agentProcess.StartInfo = new ProcessStartInfo();
                agentProcess.StartInfo.Arguments = string.Format("{0} {1}", "{721F6B5C-125C-41A9-9EB2-C1D3B94C302B}", SessionContext.CustomerInfo.SysNo);
                agentProcess.StartInfo.FileName = "agent.exe";
                agentProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                agentProcess.Start();
            }
        }

        private void StopAgent()
        {
            if (agentProcess != null)
            {
                try
                {
                    agentProcess.Kill();
                    agentProcess = null;
                }
                catch
                {
                }
            }
        }
    }
}
