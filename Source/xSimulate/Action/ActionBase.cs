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

            if (this.automationActionData.Context != null)
            {
                string strFrame = this.automationActionData.Context.Frame;

                if (!string.IsNullOrEmpty(strFrame))
                {
                    this.ActionContext = new ActionContext();
                    int frameIndex;
                    if (int.TryParse(strFrame, out frameIndex))
                    {
                        this.ActionContext.FrameIndex = frameIndex;
                    }
                    else
                    {
                        this.ActionContext.FrameIndex = -1;
                        this.ActionContext.FrameName = strFrame;
                    }
                }
            }

            this.SaveData = StringConvertTo.ConvertTo<bool>(automationActionData.SaveData, true);
            this.GetDatakey = GetAttributeValue<string>("getDatakey");
            this.SaveDatakey = GetAttributeValue<string>("saveDatakey");
            this.Wait = GetAttributeValue<int>("wait", 0);
        }

        public abstract ActionType ActionType { get; }

        #region Data
        public ActionContext ActionContext { get; set; }

        public bool SaveData { get; set; }

        public string GetDatakey { get; set; }

        public string SaveDatakey { get; set; }

        public int Wait { get; set; }
        #endregion

        #region Action
        public List<IAction> ChildAction { get; set; }

        public void AddNext(IAction action)
        {
            if (this.ChildAction == null)
            {
                this.ChildAction = new List<IAction>();
            }

            this.ChildAction.Add(action);
        }

        public bool HasChild
        {
            get { return this.ChildAction != null && this.ChildAction.Count > 0; }
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
            List<T> list = null;

            name = name.ToLower();
            if (this.automationActionData.AttributeList != null && this.automationActionData.AttributeList.Count > 0)
            {
                foreach (AutomationActionAttribute attr in this.automationActionData.AttributeList)
                {
                    if (attr.Name.ToLower().StartsWith(name))
                    {
                        if (list == null)
                        {
                            list = new List<T>();
                        }

                        list.Add(StringConvertTo.ConvertTo<T>(attr.Value, defaultValue));
                    }
                }
            }

            return list;
        }
        #endregion
    }
}