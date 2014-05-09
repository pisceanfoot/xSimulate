using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace xSimulate.WebAutomationTasks
{
    class KeyboardTask
    {
        public void Down(HtmlElement h)
        {
            h.InvokeMember("fireEvent", new object[] { "onkeydown" });
        }

        public void Up(HtmlElement h)
        {
            h.InvokeMember("fireEvent", new object[] { "onkeyup" });
        }
    }
}
