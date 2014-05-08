using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using xSimulate.Configuration;

namespace UnitTest.xSimulate
{
    [TestFixture, Category("Configuration")]
    public class Test_Configuration
    {
        [Test]
        public void Test_ConfigurationSerialize()
        {
            WebAutomationConfig config = new WebAutomationConfig();
            config.Name = "Test1";
            config.Version = "0.1";

            AutomationStep step = new AutomationStep();
            step.Name = "aaa";
            step.ActionList = new System.Collections.Generic.List<AutomationAction>();

            config.Add(step);

            AutomationAction action = new AutomationAction();
            action.Type = "Wait";

            AutomationActionAttribute attr = new AutomationActionAttribute();
            attr.Name = "name";
            attr.Value = "value";
            action.Add(attr);

            AutomationAction childAction = new AutomationAction();
            childAction.Type = "Click";
            childAction.Add(attr);
            
            action.AddChild(childAction);
            step.Add(action);

            WebAutomationConfig.Save(config);

            WebAutomationConfig loadConfig = WebAutomationConfig.Load();

            Assert.IsNotNull(loadConfig);
            Assert.AreEqual(loadConfig.Name, config.Name);
            Assert.AreEqual(loadConfig.StepList.Count, config.StepList.Count);
            Assert.AreEqual(loadConfig.StepList[0].Name, config.StepList[0].Name);
        }
    }
}
