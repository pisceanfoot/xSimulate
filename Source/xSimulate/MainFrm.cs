using System;
using System.Windows.Forms;
using xSimulate.Action;
using xSimulate.Browse;
using xSimulate.Factory;

namespace xSimulate
{
    public partial class MainFrm : Form
    {
        private WebBrowserEx webBrowser;
        private BrowserFactory factory;
        private MyTraceListener trace;
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

            trace = new MyTraceListener(this.TextBoxLog);
            System.Diagnostics.Trace.Listeners.Add(trace);
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
            factory = new BrowserFactory(this.webBrowser);
        }

        private void InitWebBrowser()
        {
            this.webBrowser = new WebBrowserEx();
            this.webBrowser.Dock = DockStyle.Fill;
            this.tabPageWebBrowser.Controls.Add(this.webBrowser);
        }

        private void Run()
        {
            PageAction action = new PageAction();
            action.Uri = "http://www.newegg.cn";

            FindElementAction findElementAction = new FindElementAction();
            findElementAction.ID = "topSearch";
            action.AddNext(findElementAction);

            MouseAction mouseAction = new MouseAction();
            mouseAction.Click = true;
            action.AddNext(mouseAction);

            AttributeAction attributeAction = new AttributeAction();
            attributeAction.SetValue = "手机";
            action.AddNext(attributeAction);

            //mouseAction = new MouseAction();
            //mouseAction.Click = true;
            //mouseAction.SaveData = false;
            //action.AddNext(mouseAction);

            //
            //findElementAction = new FindElementAction();
            //findElementAction.ClassName = "btn_search";
            //action.AddNext(findElementAction);

            //findElementAction = new FindElementAction();
            //findElementAction.Url = "http://www.newegg.cn/Product/A36-296-C0S-02.htm?&neg_sp=Home-_-A36-296-C0S-02-_-HotSaleArea";
            //action.AddNext(findElementAction);

            //mouseAction = new MouseAction();
            //mouseAction.Click = true;
            //action.AddNext(mouseAction);

            ScrollAction scrollAction = new ScrollAction();
            scrollAction.Position = Position.PageBottom;
            action.AddNext(scrollAction);

            factory.Run(action);
        }
    }
}