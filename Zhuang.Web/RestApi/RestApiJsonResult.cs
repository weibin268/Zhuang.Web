using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zhuang.Web.RestApi
{
    public class RestApiJsonResult
    {
        public bool Valid { get; set; }

        public bool Success { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }
    }
}
