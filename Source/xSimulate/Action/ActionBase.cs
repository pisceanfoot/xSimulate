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

            this.SaveData = GetAttributeValue<bool>("savedata", true);
            this.GetDatakey = GetAttributeValue<string>("getDatakey");
            this.SaveDatakey = GetAttributeValue<string>("saveDatakey");
        }

        public abstract ActionType ActionType { get; }

        public bool SaveData { get; set; }

        public string GetDatakey { get; set; }

        public string SaveDatakey { get; set; }

        #region Action
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
        #endregion

        #region GetAttributeValue
        protected T GetAttributeValue<T>(string name)
        {
            return GetAttributeValue<T>(name, default(T));
        }

        protected T GetAttributeValue<T>(string name, T defaultValue)
        {
            name = name.ToLower();
            if (this.automationActionData.AttributeList != null && this.automationActionData.AttributeList.Count >0)
            {
                foreach (AutomationActionAttribute attr in this.automationActionData.AttributeList)
                {
                    if (attr.Name.ToLower() == name)
                    {
                        return StringConvertTo.ConvertTo<T>(attr.Value, defaultValue);
                    }
                }
            }

            return defaultValue;
        }

        protected List<T> GetAttributeValueStartWith<T>(string name)
        {
            return GetAttributeValueStartWith<T>(name, default(T));
        }

        protected List<T> GetAttributeValueStartWith<T>(string name, T defaultValue)
        {
            List<T> list = new List<T>();

            name = name.ToLower();
            if (this.automationActionData.AttributeList != null && this.automationActionData.AttributeList.Count > 0)
            {
                foreach (AutomationActionAttribute attr in this.automationActionData.AttributeList)
                {
                    if (attr.Name.ToLower().StartsWith(name))
                    {
                        list.Add(StringConvertTo.ConvertTo<T>(attr.Value, defaultValue));
                    }
                }
            }

            return list;
        }
        #endregion
    }
}