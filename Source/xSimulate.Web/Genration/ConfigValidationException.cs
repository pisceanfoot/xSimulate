using System;
using System.Collections.Generic;
using System.Text;

namespace xSimulate.Web.Genration
{
    public class ConfigValidationException : Exception
    {
        public ConfigValidationException(string message)
            : base(message)
        {

        }
    }
}
