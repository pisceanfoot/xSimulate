using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using xSimulate.Configuration;
using xSimulate.Util;

namespace xSimulate.Action
{
    public abstract class ActionBase : IAction
    {
        protected AutomationAction automationActionData;

        public ActionBase(AutomationAction automationActionData)
        {
            this.automationActionData = automationActionData;

            this.SaveData = GetAttributeValue<bool>("savedata");
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

        public bool HasChild
        {
            get { return this.NextAction != null && this.NextAction.Count > 0; }
        }

        protected T GetAttributeValue<T>(string name)
        {
            name = name.ToLower();
            if (this.automationActionData.AttributeList != null && this.automationActionData.AttributeList.Count >0)
            {
                foreach (AutomationActionAttribute attr in this.automationActionData.AttributeList)
                {
                    if (attr.Name.ToLower() == name)
                    {
                        return StringConvertTo.ConvertTo<T>(attr.Value);
                    }
                }
            }

            return default(T);
        }
    }
}