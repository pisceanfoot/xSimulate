using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xSimulate.Action
{
    [Serializable]
    public abstract class ActionBase : IAction
    {
        public abstract ActionType ActionType
        {
            get;
        }

        public List<IAction> NextAction { get; set; }

        public void AddNext(IAction action)
        {
            if (this.NextAction == null)
            {
                this.NextAction = new List<IAction>();
            }

            this.NextAction.Add(action);
        }
    }
}
