using System;
using System.Collections.Generic;

namespace xSimulate.Action
{
    [Serializable]
    public abstract class ActionBase : IAction
    {
        public ActionBase()
        {
            this.SaveData = true;
        }

        public abstract ActionType ActionType { get; }

        public bool SaveData { get; set; }

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