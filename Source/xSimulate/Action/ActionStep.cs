using System.Collections.Generic;

namespace xSimulate.Action
{
    public class ActionStep
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public List<IAction> ActionList { get; set; }
    }
}