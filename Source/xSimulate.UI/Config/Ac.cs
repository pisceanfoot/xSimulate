using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace xSimulate.UI.Config
{
    public class Ac
    {
        public string Read(string file, params string[] arg)
        {
            string content = File.ReadAllText(Path.Combine("Setting", file));
            return string.Format(content, arg);
        }
    }
}
 