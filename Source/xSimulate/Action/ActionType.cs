using System;

namespace xSimulate.Action
{
    [Serializable]
    public enum ActionType
    {
        PackageAction,
        BrowserAction,
        FindAction,
        PageAction,
        ScrollAction,
        MouseAction,
        AttributeAction,
        ClearDataAction,
        WaitAction,
        ClickAction,
        ScriptAction,
        KeyboardAction,
        SendKeyAction,
        ConditionAction,
        TimerAction,
        ClearHistoryAction,
        TextAction
    }
}