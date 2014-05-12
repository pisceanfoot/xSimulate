using System;
using System.Text;
using System.Windows.Forms;
using mshtml;
using xSimulate.Action;
using xSimulate.Browse;
using xSimulate.Util;

namespace xSimulate.WebAutomationTasks
{
    public class MouseTask : FindTask
    {
        public MouseTask(AutomationManagement manager)
            : base(manager)
        {
        }

        protected override void OnProcess(IAction action)
        {
            base.OnProcess(action);

            HtmlElement element = this.GetData(action) as HtmlElement;
            if (element == null)
            {
                LoggerManager.Error("Element Not Found");
                throw new ElementNoFoundException("Element Not Found", action);
            }

            MouseAction mouseAction = action as MouseAction;
            if (mouseAction == null)
            {
                return;
            }

            if (mouseAction.Click)
            {
                LoggerManager.Debug("MouseTask Trigger Click");
                this.Call<HtmlElement>(Click, element);
            }
            if (mouseAction.ClickNew)
            {
                LoggerManager.Debug("MouseTask Trigger ClickNew");
                this.Call<HtmlElement>(ClickNew, element);
            }
            if (mouseAction.MouseClick)
            {
                LoggerManager.Debug("MouseTask Trigger MouseClick");
                this.Call<HtmlElement>(MouseClick, element);
            }
            if (mouseAction.Over)
            {
                LoggerManager.Debug("MouseTask Trigger Mouse Over");
                this.Call<HtmlElement>(Over, element);
            }
            if (mouseAction.OverNew)
            {
                LoggerManager.Debug("MouseTask Trigger Mouse OverNew");
                this.Call<HtmlElement>(OverNew, element);
            }
            if (mouseAction.Down)
            {
                LoggerManager.Debug("MouseTask Trigger Mouse Down");
                this.Call<HtmlElement>(Down, element);
            }
            if (mouseAction.Up)
            {
                LoggerManager.Debug("MouseTask Trigger Mouse Up");
                this.Call<HtmlElement>(Up, element);
            }
            if (mouseAction.Focus)
            {
                LoggerManager.Debug("MouseTask Trigger Mouse Focus");
                this.Call<HtmlElement>(Focus, element);
            }
            if (mouseAction.Move)
            {
                LoggerManager.Debug("MouseTask Trigger Mouse Move");
                this.Call<HtmlElement>(Move, element);
            }
            if (mouseAction.MoveNew)
            {
                LoggerManager.Debug("MouseTask Trigger Mouse MoveNew");
                this.Call<HtmlElement>(MoveNew, element);
            }
            if (mouseAction.MoveEnter)
            {
                LoggerManager.Debug("MouseTask Trigger Mouse MoveEnter");
                this.Call<HtmlElement>(MoveEnter, element);
            }
        }

        public void Click(HtmlElement h)
        {
            Focus(h);
            Over(h);
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

        public void OverNew(HtmlElement h)
        {
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
            Win32API.SendMessage(handle, 0x2a1, zero, lParam);
        }

        public void Down(HtmlElement h)
        {
            h.InvokeMember("fireEvent", new object[] { "onmousedown" });
        }

        public void Up(HtmlElement h)
        {
            h.InvokeMember("fireEvent", new object[] { "onmouseup" });
        }

        public void Focus(HtmlElement h)
        {
            h.Focus();
        }

        public void Move(HtmlElement h)
        {
            h.InvokeMember("fireEvent", new object[] { "onmousemove" });
        }

        public void MoveNew(HtmlElement h)
        {
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
            Over(h);
            Move(h);
            Win32API.SendMessage(handle, 0x200, zero, lParam);
        }

        public void MoveEnter(HtmlElement h)
        {
            h.InvokeMember("fireEvent", new object[] { "onmouseenter" });
        }
    }
}