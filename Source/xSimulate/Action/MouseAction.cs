using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xSimulate.Action
{
    public class MouseAction : ActionBase
    {
        public override ActionType ActionType
        {
            get { return Action.ActionType.MouseAction; }
        }

        public bool Click { get; set; }
    }
}
