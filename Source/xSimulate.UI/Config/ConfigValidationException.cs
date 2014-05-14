using System;
using System.Collections.Generic;
using System.Text;

namespace xSimulate.UI.Config
{
    public class ConfigValidationException : Exception
    {
        public ConfigValidationException(string message)
            : base(message)
        {

        }
    }
}
