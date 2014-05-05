using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xSimulate.Action
{
    public class ClickAction : ActionBase
    {
        public override ActionType ActionType
        {
            get { return ActionType.ClickAction; }
        }
    }
}
