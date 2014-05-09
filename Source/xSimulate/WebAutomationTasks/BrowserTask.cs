﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using xSimulate.Action;
using xSimulate.Browse;

namespace xSimulate.WebAutomationTasks
{
    public class BrowserTask : CommonTask
    {
        public BrowserTask(WebBrowserEx webBrowser)
            : base(webBrowser)
        {
        }

        public override void Run(IAction action)
        {
            OnProcess(action);
        }

        protected override void OnProcess(IAction action)
        {
            BrowserAction pageAction = action as BrowserAction;
            webBrowser.Navigate(pageAction.Url);

            LoggerManager.Debug("Browser: {0}", pageAction.Url);
        }

        public override bool IsComplete()
        {
            if (this.webBrowser.IsDisposed)
            {
                return true;
            }

            return this.webBrowser.ReadyState == WebBrowserReadyState.Complete;
        }
    }
}