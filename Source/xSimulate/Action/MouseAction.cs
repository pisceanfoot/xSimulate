using System;
using System.Xml.Serialization;
namespace xSimulate.Action
{
    public class MouseAction : FindElementAction
    {
        public override ActionType ActionType
        {
            get { return Action.ActionType.MouseAction; }
        }

        public bool Click { get; set; }

        public bool MouseClick { get; set; }
    }
}