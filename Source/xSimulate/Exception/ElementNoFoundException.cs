using System;
using System.Collections.Generic;
using System.Text;
using xSimulate.Action;

namespace xSimulate
{
    public class ElementNoFoundException : Exception
    {
        public ElementNoFoundException(string message, IAction action)
            : base(message)
        {
            this.Action = action;
        }

        public IAction Action { get; set; }
    }
}
