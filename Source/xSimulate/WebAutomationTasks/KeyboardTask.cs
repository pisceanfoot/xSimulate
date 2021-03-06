﻿using System.Windows.Forms;
using xSimulate.Action;
using xSimulate.Browse;

namespace xSimulate.WebAutomationTasks
{
    public class KeyboardTask : CommonTask
    {
        public KeyboardTask(AutomationManagement manager)
            : base(manager)
        {
        }

        protected override void OnProcess(Action.IAction action)
        {
            KeyboardAction keyboardAction = action as KeyboardAction;
            if (keyboardAction == null)
            {
                return;
            }

            LoggerManager.Debug(action.AutomationActionData);

            HtmlElement element = this.GetData(action) as HtmlElement;
            if (element == null)
            {
                LoggerManager.Error("Element Not Found");
                throw new ElementNoFoundException("Element Not Found", action);
            }

            if (keyboardAction.KeyDown)
            {
                this.Call<HtmlElement>(Down, element);
            }

            if (keyboardAction.KeyUp)
            {
                this.Call<HtmlElement>(Up, element);
            }
        }

        public void Down(HtmlElement h)
        {
            h.InvokeMember("fireEvent", new object[] { "onkeydown" });
        }

        public void Up(HtmlElement h)
        {
            h.InvokeMember("fireEvent", new object[] { "onkeyup" });
        }
    }
}