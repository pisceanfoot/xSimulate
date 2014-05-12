using System;
using System.Collections.Generic;
using System.Text;

namespace xSimulate
{
    public delegate void Callback();

    public delegate void Callback<T>(T obj);

    public delegate R Callback<T, R>(T obj);

    public delegate void MessageHandle<T>(T obj);

    public delegate void MessageHandle<T, T1>(T obj, T1 obje1);
}
