using System;
using System.Windows.Forms;

namespace xSimulate
{
    public partial class MainFrm : Form
    {
        private AutomationManagement manager;
        private WebBrowser webBrowser;
        private MyTraceListener trace;
        private Timer timer;

        public MainFrm()
        {
            InitializeComponent();

            Init();
            InitWebBrowser();
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            this.timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.timer.Stop();
            Run();
        }

        public void Init()
        {
            manager = new AutomationManagement();

            this.Load += MainFrm_Load;
            this.timer = new Timer();
            this.timer.Tick += timer_Tick;
            this.timer.Interval = 1000;
            this.timer.Enabled = false;

            trace = new MyTraceListener(this.TextBoxLog);

            //System.Diagnostics.Trace.Listeners.Add(trace);
            System.Diagnostics.Debug.Listeners.Add(trace);
        }

        private void InitWebBrowser()
        {
            this.webBrowser = manager.Browser;
            this.webBrowser.Dock = DockStyle.Fill;
            this.tabPageWebBrowser.Controls.Add(this.webBrowser);
        }

        private void Run()
        {
            manager.LoadConfig();
            manager.Run();
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            manager.Run();
        }
    }
}