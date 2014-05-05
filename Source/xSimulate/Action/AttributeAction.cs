using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xSimulate.Action
{
    public class AttributeAction : ActionBase
    {
        public override ActionType ActionType
        {
            get { return ActionType.AttributeAction; }
        }

        public string SetValue { get; set; }
    }
}
