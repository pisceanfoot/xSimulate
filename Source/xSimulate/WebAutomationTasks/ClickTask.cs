using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using mshtml;
using xSimulate.Action;
using xSimulate.Browse;
using xSimulate.Storage;
using xSimulate.Util;

namespace xSimulate.WebAutomationTasks
{
    public class ClickTask : FindElementTask
    {
        public ClickTask(WebBrowserEx webBrowser)
            : base(webBrowser)
        {
        }

        protected override void OnProcess(IAction action)
        {
            base.OnProcess(action);

            HtmlElement element = TaskStorage.Storage as HtmlElement;
            if (element == null)
            {
                LoggerManager.Error("Element Not Found");
                return;
            }

            ClickAction clickAction = action as ClickAction;
            if (clickAction == null)
            {
                return;
            }

            if (clickAction.Click)
            {
                LoggerManager.Debug("Trigger Click");
                Click(element);
            }
            if (clickAction.ClickNew)
            {
                LoggerManager.Debug("Trigger ClickNew");
                ClickNew(element);
            }
            if (clickAction.MouseClick)
            {
                LoggerManager.Debug("Trigger MouseClick");
                MouseClick(element);
            }
        }

        public void Click(HtmlElement h)
        {
            Over(h);
            //Focus(h);
            Down(h);
            h.InvokeMember("click");
        }

        public void ClickNew(HtmlElement h)
        {
            Over(h);
            IHTMLRect location = HtmlHelp.GetLocation(h);
            Random random = new Random();
            int num = random.Next(location.left, location.right);
            int num2 = random.Next(location.top, location.bottom);

            IntPtr handle = this.webBrowser.Handle;

            StringBuilder lpClassName = new StringBuilder(100);
            while (lpClassName.ToString() != "Internet Explorer_Server")
            {
                handle = Win32API.GetWindow(handle, 5);
                Win32API.GetClassName(handle, lpClassName, lpClassName.Capacity);
            }

            IntPtr lParam = (IntPtr)((num2 << 0x10) | num);
            IntPtr zero = IntPtr.Zero;
            Win32API.SendMessage(handle, 0x201, zero, lParam);
            Win32API.SendMessage(handle, 0x202, zero, lParam);
        }

        public void MouseClick(HtmlElement h)
        {
            h.InvokeMember("fireEvent", new object[] { "onmouseclick" });
        }

        public void Over(HtmlElement h)
        {
            h.InvokeMember("fireEvent", new object[] { "onmouseover" });
        }

        public void Down(HtmlElement h)
        {
            h.InvokeMember("fireEvent", new object[] { "onmousedown" });
        }
    }
}
