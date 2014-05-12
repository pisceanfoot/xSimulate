using System.Text.RegularExpressions;
using System.Windows.Forms;
using xSimulate.Action;
using xSimulate.Browse;
using xSimulate.Util;
using xSimulate.WebAutomationTasks;

namespace xSimulate.Browser
{
    public class PageTask : ClickTask
    {
        public PageTask(AutomationManagement manager)
            : base(manager)
        {
        }

        protected override void OnProcess(IAction action)
        {
        }

        public override bool ChildComplete(IAction action)
        {
            PageAction pageAction = action as PageAction;

            HtmlElement element = this.GetData(action) as HtmlElement;
            if (element == null)
            {
                RunConditionAction(pageAction);

                base.OnProcess(action);
                return false;
            }

            return true;
        }

        private void RunConditionAction(PageAction pageAction)
        {
            if (pageAction.ConditoinAction == null || pageAction.ConditoinAction.Count == 0)
            {
                return;
            }

            foreach (IAction action in pageAction.ConditoinAction)
            {
                this.RunAction(action);
            }


            int pageIndex = StringConvertTo.ConvertTo<int>(this.GetData<string>("conditionActions_page_Index"));
            int pageCount = StringConvertTo.ConvertTo<int>(this.GetData<string>("conditionActions_page_Count"));

            if (pageIndex < 1)
            {
                throw new ElementNoFoundException("PageTask No Result", pageAction);
            }
            if (pageCount < 1)
            {
                throw new ElementNoFoundException("PageTask No Result", pageAction);
            }

            int max = pageAction.MaxPageIndex > pageCount ? pageCount : pageAction.MaxPageIndex;
            if (pageIndex < max)
            {
                return;
            }

            throw new ElementNoFoundException("PageTask Element Not Found", pageAction);
        }
    }
}