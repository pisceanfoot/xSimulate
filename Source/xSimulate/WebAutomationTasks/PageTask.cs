using System.Windows.Forms;
using xSimulate.Action;
using xSimulate.Browse;
using xSimulate.Storage;
using xSimulate.WebAutomationTasks;

namespace xSimulate.Browser
{
    public class PageTask : CommonTask
    {
        public PageTask(WebBrowserEx webBrowser)
            : base(webBrowser)
        {
        }

        public override void Run(IAction action)
        {
            PageAction pageAction = action as PageAction;
            webBrowser.Navigate(pageAction.Uri);
        }

        //bool hasSet = false;
        //protected override void OnDocumentCompleted()
        //{
        //    if (!hasSet)
        //    {
        //        //TaskStorage.Storage = this.webBrowser.Document.Body;
        //        hasSet = true;
        //    }
        //}

        public override bool IsComplete()
        {
            return this.webBrowser.ReadyState == WebBrowserReadyState.Complete;
        }
    }
}