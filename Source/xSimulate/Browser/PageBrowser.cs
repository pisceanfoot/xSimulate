using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using xSimulate.Action;

namespace xSimulate.Browser
{
    public class PageBrowser : BrowserBase
    {
        private PageAction action;
        private bool runComplete;

        public PageBrowser(WebBrowser webBrowser, PageAction action)
            : base(webBrowser)
        {
            this.action = action;
        }

        public override void Run()
        {
            this.runComplete = false;
            webBrowser.Navigate(this.action.Uri);
        }

        protected override void OnDocumentCompleted()
        {
            this.runComplete = true;
        }

        public override bool IsComplete()
        {
            return this.runComplete;
        }
    }
}
