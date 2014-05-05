using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using xSimulate.Action;

namespace xSimulate.Browser
{
    public class ScrollBrowser : BrowserBase
    {
        private Timer timer;

        public ScrollBrowser(WebBrowser webBrowser)
            : base(webBrowser)
        {
            timer = new Timer();
            timer.Interval = 500;
            timer.Tick += timer_Tick;
            timer.Enabled = false;
        }

        public override void Run(Action.IAction action)
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

            if (scrollAction.Position == Position.PageBottom)
            {
                int max = GetMaxPosition();
                ToY(max);
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.timer.Stop();
            if (Process())
            {
                return;
            }

            this.timer.Start();
        }

        private bool Process()
        {
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
