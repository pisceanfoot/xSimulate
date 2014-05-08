using System;
using System.Xml.Serialization;

namespace xSimulate.Action
{
    public class PageAction : ActionBase
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public override ActionType ActionType
        {
            get { return ActionType.PageAction; }
        }
    }
}