using System;
using System.Windows.Forms;

namespace xSimulate.Browse
{
    public class WebBrowserTimer : Timer
    {
        private WebBrowser webBrowser;

        public WebBrowserTimer(WebBrowser browser)
        {
            this.webBrowser = browser;
        }

        protected override void OnTick(EventArgs e)
        {
            if (this.webBrowser.IsDisposed)
            {
                this.Enabled = false;
                return;
            }

            base.OnTick(e);
        }
    }
}