using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Zhuang.Web.RestApi
{
    public class RestApiContext
    {
        public HttpContext Context { get; set; }

        public string Action { get; set; }

        public string Args { get; set; }

        public RestApiJsonResult Result { get; set; }
    }
}
