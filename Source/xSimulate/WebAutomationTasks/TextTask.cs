using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using xSimulate.Action;

namespace xSimulate.WebAutomationTasks
{
    public class TextTask : FindTask
    {
        public TextTask(AutomationManagement manager)
            : base(manager)
        {
        }

        protected override void OnProcess(IAction action)
        {
            base.OnProcess(action);

            LoggerManager.Debug(action.AutomationActionData);

            TextAction textAction = action as TextAction;

            HtmlElement element = GetData(action) as HtmlElement;
            if (element == null)
            {
                LoggerManager.Error("Element Not Found");
                throw new ElementNoFoundException("Element Not Found", action);
            }

            string value = null;
            if (!string.IsNullOrEmpty(textAction.Attrbute))
            {
                value = element.GetAttribute(textAction.Attrbute);
            }
            if (!string.IsNullOrEmpty(textAction.AttrbuteRegex))
            {
                Match match = Regex.Match(value, textAction.AttrbuteRegex, RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    value = match.Groups[1].Value;
                }
            }

            this.SaveData<string>(textAction.TextSaveKey, value);
        }
    }
}
