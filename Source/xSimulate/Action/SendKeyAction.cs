using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xSimulate.Configuration;

namespace xSimulate.Action
{
    public class SendKeyAction : FindAction
    {
        public SendKeyAction(AutomationAction automationActionData)
            : base(automationActionData)
        {
            this.Keys = GetAttributeValueStartWith<string>("sendkey_");
        }

        public List<string> Keys { get; set; }

        public override ActionType ActionType
        {
            get { return ActionType.SendKeyAction; }
        }
    }
}
