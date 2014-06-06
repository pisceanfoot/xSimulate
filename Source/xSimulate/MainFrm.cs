using System;
using System.IO;
using System.Windows.Forms;
using xSimulate.Services;
using xSimulate.Web.Model;

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

        public void Init()
        {
            manager = new AutomationManagement();
            manager.ErrorMessage += manager_ErrorMessage;
            manager.Complete += manager_Complete;

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

        private void MainFrm_Load(object sender, EventArgs e)
        {
            this.timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.timer.Stop();
            if (RecieveTask())
            {
                this.timer.Start();
            }
        }

        private bool RecieveTask()
        {
            TaskService service = Services.ServiceManager.CreateTaskService();
            RetrieveTask task = service.GetTask(SessionCustomer.CustomerSysNo);
            if (task != null)
            {
                SessionCustomer.CurrentTask = task;
                return !Run(task.Task);
            }
            else
            {
                return true;
            }
        }

        private bool Run(Task task)
        {
            try
            {
                manager.LoadConfig(task.Setting.Setting);
                manager.Run();
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            manager.Run();
        }

        private void manager_ErrorMessage(string obj, string obje1)
        {
            // ERROR LOG
            RetrieveTask retrieveTask = new RetrieveTask();
            retrieveTask.Description = "执行失败";
            retrieveTask.RunTaskSysNo = SessionCustomer.CurrentTask.RunTaskSysNo;
            retrieveTask.SysNo = SessionCustomer.CurrentTask.SysNo;
            retrieveTask.Status = "E";

            TaskService  task = ServiceManager.CreateTaskService();
            task.UpdateRunTaskStatus(retrieveTask);
        }

        private void manager_Complete()
        {

            try
            {
                RetrieveTask retrieveTask = new RetrieveTask();
                retrieveTask.Description = "执行完成";
                retrieveTask.RunTaskSysNo = SessionCustomer.CurrentTask.RunTaskSysNo;
                retrieveTask.SysNo = SessionCustomer.CurrentTask.SysNo;
                retrieveTask.Status = "S";

                TaskService task = ServiceManager.CreateTaskService();
                task.UpdateRunTaskStatus(retrieveTask);
            }
            catch
            {
                
            }

            // next
            this.timer.Start();
        }
    }
}