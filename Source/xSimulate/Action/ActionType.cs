using System;
namespace xSimulate.Action
{
    [Serializable]
    public enum ActionType
    {
        BrowserAction,
        FindElementAction,
        PageAction,
        ScrollAction,
        MouseAction,
        AttributeAction,
        ClearDataAction,
        WaitAction,
        ClickAction
    }
}