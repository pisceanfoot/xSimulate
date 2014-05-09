using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xSimulate.Configuration;

namespace xSimulate.Action
{
    public class ClickAction : FindAction
    {
        public ClickAction(AutomationAction automationActionData)
            : base(automationActionData)
        {
            this.Click = GetAttributeValue<bool>("click");
            this.ClickNew = GetAttributeValue<bool>("clicknew");
            this.MouseClick = GetAttributeValue<bool>("mouseclick");
        }

        public override ActionType ActionType
        {
            get { return ActionType.ClickAction; }
        }

        public bool Click { get; set; }

        public bool ClickNew { get; set; }

        public bool MouseClick { get; set; }
    }
}
