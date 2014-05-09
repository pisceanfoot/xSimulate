using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace xSimulate
{
    /// <summary>
    /// 
    /// </summary>
    /// <example>
    /// TraceListener debugListener = new MyTraceListener (theTextBox);
    /// Debug.Listeners.Add(debugListener);
    /// Trace.Listeners.Add(debugListener);
    /// </example>
    public class MyTraceListener : TraceListener
    {
        private TextBoxBase output;

        public MyTraceListener(TextBoxBase output)
        {
            this.Name = "Trace";
            this.output = output;
        }


        public override void Write(string message)
        {
            if (this.output.IsDisposed)
            {
                return;
            }

            System.Action append = delegate()
            {
                output.AppendText(string.Format("[{0}] ", DateTime.Now.ToString()));
                output.AppendText(message);
            };
            if (output.InvokeRequired)
            {
                output.BeginInvoke(append);
            }
            else
            {
                append();
            }

        }

        public override void WriteLine(string message)
        {
            Write(message + Environment.NewLine);
        }
    }
}
