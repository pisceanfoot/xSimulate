using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using xSimulate;
using xSimulate.Configuration;

namespace UnitTest.xSimulate
{
    [TestFixture, Category("AutomationManagement")]
    public class Test_AutomationManagement
    {
        [Test]
        [STAThread]
        public void Test_LoadConfig()
        {
            AutomationAction action = new AutomationAction();
            action.Type = "Mouse";

            AutomationAction childAction = new AutomationAction();
            childAction.Type = "Mouse";
            action.AddChild(childAction);

            AutomationStep step = new AutomationStep();
            step.Name = "First";
            step.Description = "test";
            step.Add(action);

            WebAutomationConfig config = new WebAutomationConfig();
            config.Add(step);

            AutomationManagement manager = new AutomationManagement();
            manager.LoadConfig(config);
        }
    }
}
