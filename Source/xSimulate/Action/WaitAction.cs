using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xSimulate.Action
{
    public class WaitAction : ActionBase
    {
        public override ActionType ActionType
        {
            get { return ActionType.WaitAction; }
        }

        public int Seconds { get; set; }

        public int Milliseconds { get; set; }
    }
}
