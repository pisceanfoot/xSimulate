using xSimulate.Action;

namespace xSimulate.WebAutomationTasks
{
    public interface ITask
    {
        void Run(IAction action);

        bool IsComplete();
    }
}