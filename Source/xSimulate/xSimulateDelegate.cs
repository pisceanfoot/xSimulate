using System;
using System.Collections.Generic;
using System.Text;

namespace xSimulate
{
    public delegate void Callback();

    public delegate void Callback<T>(T obj);

    public delegate R Callback<T, R>(T obj);
}
