using System.Collections.Generic;
using xSimulate.Configuration;

namespace xSimulate.Action
{
    public interface IAction
    {
        ActionType ActionType { get; }

        List<IAction> ChildAction { get; set; }

        List<IAction> ConditoinAction { get; set; }

        bool HasChild { get; }

        AutomationAction AutomationActionData { get; }
    }
}