using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using xSimulate.Action;
using xSimulate.Browser;

namespace xSimulate
{
    public partial class MainFrm : Form
    {
        private WebBrowser webBrowser;
        private BrowserFactory factory;
        private Timer timer;

        public MainFrm()
        {
            InitializeComponent();

            InitWebBrowser();
            Init();

            this.Load += MainFrm_Load;
            this.timer = new Timer();
            this.timer.Tick += timer_Tick;
            this.timer.Interval = 1000;
            this.timer.Enabled = false;
        }

        void MainFrm_Load(object sender, EventArgs e)
        {
            this.timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            this.timer.Stop();
            Run();
        }

        public void Init()
        {
            factory = new BrowserFactory(this.webBrowser);
        }

        private void InitWebBrowser()
        {
            this.webBrowser = new WebBrowser();
            this.webBrowser.Dock = DockStyle.Fill;
            this.Controls.Add(this.webBrowser);
        }

        private void Run()
        {
            PageAction action = new PageAction();
            action.Uri = "http://www.baidu.com";

            FindElementAction findElementAction = new FindElementAction();
            findElementAction.ID = "kw1";

            IBrowser browser = factory.Create(action);
            browser.Run();

            while (!browser.IsComplete())
            {
                System.Threading.Thread.Sleep(1);
                Application.DoEvents();
            }

            browser = factory.Create(findElementAction);
            browser.Run();
        }
    }
}
