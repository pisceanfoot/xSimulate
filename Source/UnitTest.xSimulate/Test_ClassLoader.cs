using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using xSimulate.Action;
using xSimulate.Configuration;
using xSimulate.Util;

namespace UnitTest.xSimulate
{
    [TestFixture, Category("ClassLoader")]
    public class Test_ClassLoader
    {
        [Test]
        public void Test_LoadAction()
        {
            AutomationAction actionConfig = new AutomationAction();
            actionConfig.Type = "Attribute";
            IAction action =  ClassLoader.LoadAction(actionConfig);

            Assert.IsNotNull(action);
            Assert.AreEqual(action.ActionType, ActionType.AttributeAction);
        }

        [Test]
        public void Test_LoadBrowserAction()
        {
            AutomationAction actionConfig = new AutomationAction();
            actionConfig.Type = "Mouse";
            IAction action = ClassLoader.LoadAction(actionConfig);

            Assert.IsNotNull(action);
            Assert.AreEqual(action.ActionType, ActionType.MouseAction);
        }
    }
}
