using System.Windows.Forms;

namespace xSimulate.Action
{
    public class FindElementAction : ActionBase
    {
        public string ID { get; set; }

        public string ClassName { get; set; }

        public string Url { get; set; }

        public string XPath { get; set; }

        public override ActionType ActionType
        {
            get { return ActionType.FindElementAction; }
        }
    }
}