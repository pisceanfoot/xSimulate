namespace xSimulate.Action
{
    public class AttributeAction : ActionBase
    {
        public override ActionType ActionType
        {
            get { return ActionType.AttributeAction; }
        }

        public string SetValue { get; set; }
    }
}