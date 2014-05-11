using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace xSimulate
{
    public class LoggerManager
    {
        /// <summary>
        /// Logs at information level
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void Info(string message, params object[] args)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }
            Trace.WriteLine(string.Format(message, args));
        }

        /// <summary>
        /// Logs at information level
        /// </summary>
        /// <param name="message"></param>
        public static void Info(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }
            Trace.WriteLine(message);
        }

        /// <summary>
        /// Logs at debug level
        /// </summary>
        /// <param name="message"></param>
        public static void Debug(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }
            System.Diagnostics.Debug.WriteLine(message);
        }

        /// <summary>
        /// Logs at debug level
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void Debug(string message, params object[] args)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }
            System.Diagnostics.Debug.WriteLine(string.Format(message, args));
        }

        /// <summary>
        /// Logs at warning level
        /// </summary>
        /// <param name="message"></param>
        public static void Warning(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }
            Trace.TraceWarning(message);
        }

        /// <summary>
        /// Logs at warning level
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void Warning(string message, params object[] args)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }
            Trace.TraceWarning(string.Format(message, args));
        }

        /// <summary>
        /// Logs at warning level
        /// </summary>
        /// <param name="ex"></param>
        public static void Warning(Exception ex)
        {
            string message = CreateExceptionMessage(ex);
            Trace.TraceWarning(message);
        }

        /// <summary>
        /// Logs at Error level
        /// </summary>
        /// <param name="message"></param>
        public static void Error(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }
            Trace.TraceError(message);
        }

        /// <summary>
        /// Logs at error level
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void Error(string message, params object[] args)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }
            Trace.TraceError(string.Format(message, args));
        }

        /// <summary>
        /// Logs at errorlevel
        /// </summary>
        /// <param name="ex"></param>
        public static void Error(Exception ex)
        {
            string message = CreateExceptionMessage(ex);
            Trace.TraceError(message);
        }

        /// <summary>
        /// Logs at fatal level
        /// </summary>
        /// <param name="message"></param>
        public static void Fatal(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }
            Trace.Fail(message);
        }

        /// <summary>
        /// Logs at fatal level
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void Fatal(string message, params object[] args)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }
            Trace.Fail(string.Format(message, args));
        }

        /// <summary>
        /// Logs at fatal level
        /// </summary>
        /// <param name="ex"></param>
        public static void Fatal(Exception ex)
        {
            string message = CreateExceptionMessage(ex);
            Trace.Fail(message);
        }

        private static string GetCallingClassName()
        {
            StackTrace stack = default(StackTrace);
            StackFrame currentFrame = default(StackFrame);
            string myAssemblyName = null;
            string myClassName = null;
            string myMethodName = null;

            try
            {
                stack = new StackTrace();
                currentFrame = stack.GetFrame(2);
                myAssemblyName = currentFrame.GetMethod().ReflectedType.Assembly.FullName.Split(',')[0];
                myClassName = currentFrame.GetMethod().ReflectedType.ToString();
                myMethodName = currentFrame.GetMethod().Name;
            }
            catch
            {
                myClassName = "";
                myMethodName = "";
            }

            return string.Format(System.Globalization.CultureInfo.CurrentCulture,
                "{0} - {1}.{2} : ",
                myAssemblyName, myClassName, myMethodName);
        }

        private static string CreateExceptionMessage(Exception ex)
        {
            if (ex is ThreadAbortException)
            {
                return "Thread aborted for Project: " + Thread.CurrentThread.Name;
            }

            StringBuilder buffer = new StringBuilder();
            buffer.Append(ex.Message).Append(Environment.NewLine);
            buffer.Append("----------").Append(Environment.NewLine);
            buffer.Append(ex.ToString()).Append(Environment.NewLine);
            buffer.Append("----------").Append(Environment.NewLine);
            return buffer.ToString();
        }

        public static string FormatElement(HtmlElement element)
        {
            string message = null;
            if (element != null)
            {
                message = string.Format("Tag:{0},ID:{1},Html:{2}", element.TagName, element.Id, element.OuterHtml);
            }

            return message;
        }
    }
}