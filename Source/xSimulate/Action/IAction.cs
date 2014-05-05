using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xSimulate.Action
{
    public interface IAction
    {
        ActionType ActionType { get; }

        List<IAction> NextAction { get; set; }
    }
}
