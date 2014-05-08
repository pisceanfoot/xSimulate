using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace xSimulate.Action
{
    public abstract class ActionBase : IAction
    {
        public ActionBase()
        {
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