using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xSimulate.Browser
{
    public interface IBrowser
    {
        void Run();

        bool IsComplete();
    }
}
