using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Zhuang.Web.Http
{
    public static class WebClientExtensions
    {
        public static string Post(this WebClient wc, string addr, string strData)
        {
            return Post(wc, addr, strData, Encoding.UTF8);
        }

        public static string Post(this WebClient wc, string addr, string strData, Encoding encoding)
        {
            string result = string.Empty;
            wc.Encoding = encoding;
            wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            byte[] postData = encoding.GetBytes(strData);
            var resData = wc.UploadData(addr, "POST", postData);

            result = encoding.GetString(resData);
            return result;
        }
    }
}
