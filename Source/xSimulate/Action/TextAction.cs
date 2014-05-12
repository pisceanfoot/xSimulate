using System;
using System.Collections.Generic;
using System.Text;
using xSimulate.Configuration;

namespace xSimulate.Action
{
    public class TextAction : FindAction
    {
        public TextAction(AutomationAction automationActionData)
            : base(automationActionData)
        {
            this.Attrbute = this.GetAttributeValue<string>("text_attr");
            this.AttrbuteRegex = this.GetAttributeValue<string>("text_attr_regex");

            this.TextSaveKey = this.GetAttributeValue<string>("textSaveDatakey");
            this.TextGetKey = this.GetAttributeValue<string>("textGetDatakey");
        }

        public string Attrbute { get; set; }

        public string AttrbuteRegex { get; set; }

        public string TextSaveKey { get; set; }

        public string TextGetKey { get; set; }

        public override ActionType ActionType
        {
            get { return ActionType.TextAction; }
        }
    }
}
