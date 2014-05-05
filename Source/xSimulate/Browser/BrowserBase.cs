using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace xSimulate.Browser
{
    public abstract class BrowserBase : IBrowser
    {
        protected WebBrowser webBrowser;

        public BrowserBase(WebBrowser webBrowser)
        {
            this.webBrowser = webBrowser;

            this.webBrowser.DocumentCompleted += WebBrowser_DocumentCompleted;
        }

        private void WebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            OnDocumentCompleted();
        }

        public abstract void Run();

        public virtual bool IsComplete()
        {
            return true;
        }

        protected virtual void OnDocumentCompleted()
        {

        }
    }
}
