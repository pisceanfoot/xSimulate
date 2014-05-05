using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xSimulate.Action
{
    [Serializable]
    public class PageAction : ActionBase
    {
        public string Name { get; set; }

        public string Uri { get; set; }

        public override ActionType ActionType
        {
            get { return ActionType.PageAction; }
        }
    }
}
