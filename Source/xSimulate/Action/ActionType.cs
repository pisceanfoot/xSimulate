using System;
namespace xSimulate.Action
{
    [Serializable]
    public enum ActionType
    {
        BrowserAction,
        ClickAction,
        FindElementAction,
        PageAction,
        ScrollAction,
        MouseAction,
        AttributeAction,
        ClearDataAction,
        WaitAction
    }
}