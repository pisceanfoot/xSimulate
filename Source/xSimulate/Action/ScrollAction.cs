namespace xSimulate.Action
{
    public enum Position
    {
        None,
        PageBottom
    }

    public class ScrollAction : ActionBase
    {
        public override ActionType ActionType
        {
            get { return ActionType.ScrollAction; }
        }

        public Position Position { get; set; }
    }
}