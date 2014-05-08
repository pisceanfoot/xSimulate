using System;
using System.Xml.Serialization;

namespace xSimulate.Action
{
    public class ClickAction : FindElementAction
    {
        public override ActionType ActionType
        {
            get { return ActionType.ClickAction; }
        }
    }
}