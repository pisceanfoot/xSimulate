using System.Collections.Generic;

namespace xSimulate.Action
{
    public interface IAction
    {
        ActionType ActionType { get; }

        List<IAction> ChildAction { get; set; }

        List<IAction> ConditoinAction { get; set; }

        bool HasChild { get; }
    }
}