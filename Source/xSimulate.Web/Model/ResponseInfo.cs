using System;
using System.Collections.Generic;
using System.Web;

namespace xSimulate.Web.Model
{
    public class ResponseInfo<T>
        where T : class
    {
        public int Code { get; set; }

        public string Message { get; set; }

        public T Value { get; set; }
    }
}