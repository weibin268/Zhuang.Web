using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zhuang.Web.RestApi
{
    public class BaseController
    {
        public T GetArgs<T>(RestApiContext context) where T : BaseArgs
        {
            T args = JsonConvert.DeserializeObject<T>(context.Args);

            args.Init();

            return args;
        }

    }
}
