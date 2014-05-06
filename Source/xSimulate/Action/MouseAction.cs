namespace xSimulate.Action
{
    public class MouseAction : ActionBase
    {
        public override ActionType ActionType
        {
            get { return Action.ActionType.MouseAction; }
        }

        public bool Click { get; set; }

        public bool MouseClick { get; set; }
    }
}