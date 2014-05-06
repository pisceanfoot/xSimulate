using System.Collections.Generic;

namespace xSimulate.Action
{
    public interface IAction
    {
        ActionType ActionType { get; }

        List<IAction> NextAction { get; set; }
    }
}