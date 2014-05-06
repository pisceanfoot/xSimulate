using System;
using System.Drawing;
using System.Windows.Forms;
using xSimulate.Action;
using xSimulate.Browse;
using xSimulate.WebAutomationTasks;

namespace xSimulate.Browser
{
    public class ScrollTask : CommonTask
    {
        private Timer timer;
        private int step = 100;
        private int bottom = 0;
        private bool running = true;
        private int lastY = -1;

        public ScrollTask(WebBrowserEx webBrowser)
            : base(webBrowser)
        {
            timer = new Timer();
            timer.Interval = 500;
            timer.Tick += timer_Tick;
            timer.Enabled = false;
        }

        protected override void OnProcess(Action.IAction action)
        {
            ScrollAction scrollAction = action as ScrollAction;
            if (scrollAction == null)
            {
                return;
            }

            // page
            if (scrollAction.Position == Position.None)
            {
                return;
            }

            lastY = -1;
            running = true;
            if (scrollAction.Position == Position.PageBottom)
            {
                bottom = GetMaxPosition();
                step = 100;
            }

            this.timer.Start();
        }

        public override bool IsComplete()
        {
            return !running;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.timer.Stop();
            if (Process())
            {
                running = false;
                return;
            }

            this.timer.Start();
        }

        
        private bool Process()
        {
            int y = GetY();
            if (lastY == y)
            {
                return true;
            }
            lastY = y;
            if (bottom >= y)
            {
                ToY(y + step);

                return false;
            }

            return true;
        }

        private int GetMaxPosition()
        {
            return this.webBrowser.Document.Window.Size.Height;
        }

        private int GetY()
        {
            return int.Parse(this.webBrowser.Document.GetElementsByTagName("HTML")[0].ScrollTop.ToString());
        }

        private void ToY(int y)
        {
            this.webBrowser.Document.Window.ScrollTo(new Point(0, y));
        }
    }
}