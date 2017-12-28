using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zhuang.Web.RestApi
{
    public class RestApiException : Exception
    {
        public RestApiException() : base()//调用基类的构造器
        {

        }
        public RestApiException(string message) : base(message)//调用基类的构造器
        {

        }
        public RestApiException(string message, Exception innerException) : base(message, innerException)//调用基类的构造器
        {

        }
    }
}
