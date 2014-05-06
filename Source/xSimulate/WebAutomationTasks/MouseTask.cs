using System;
using System.Text;
using System.Windows.Forms;
using mshtml;
using xSimulate.Action;
using xSimulate.Browse;
using xSimulate.Storage;
using xSimulate.Util;
using xSimulate.WebAutomationTasks;

namespace xSimulate.Browser
{
    public class MouseTask : CommonTask
    {
        public MouseTask(WebBrowserEx webBrowser)
            : base(webBrowser)
        {
        }

        protected override void OnProcess(IAction action)
        {
            HtmlElement element = TaskStorage.Storage as HtmlElement;
            if (element == null)
            {
                return;
            }

            MouseAction mouseAction = action as MouseAction;
            if (mouseAction == null)
            {
                return;
            }

            if (mouseAction.Click)
            {
                Click(element);
            }
            if (mouseAction.MouseClick)
            {
                ClickNew(element);
            }

            if (!mouseAction.SaveData)
            {
                TaskStorage.Storage = null;
            }
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

        public void Click(HtmlElement h)
        {
            Over(h);
            Down(h);
            h.InvokeMember("click");
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