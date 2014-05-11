using System;
using System.Collections.Generic;
using System.Text;
using xSimulate.Configuration;

namespace xSimulate.Action
{
    public class TimerAction : ActionBase
    {
        public TimerAction(AutomationAction automationActionData)
            : base(automationActionData)
        {
            this.Seconds = GetAttributeValue<int>("seconds");
            this.Milliseconds = GetAttributeValue<int>("milliseconds");
        }

        public override ActionType ActionType
        {
            get { return ActionType.TimerAction; }
        }

        public int Seconds { get; set; }

        public int Milliseconds { get; set; }
    }
}
