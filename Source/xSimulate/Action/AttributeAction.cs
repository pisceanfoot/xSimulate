using System;
using System.Xml.Serialization;

namespace xSimulate.Action
{
    public class AttributeAction : FindElementAction
    {
        public override ActionType ActionType
        {
            get { return ActionType.AttributeAction; }
        }

        public string SetValue { get; set; }
    }
}