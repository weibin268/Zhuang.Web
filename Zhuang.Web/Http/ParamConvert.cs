using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zhuang.Web.Http
{
    public class ParamConvert
    {
        public static string ToString(Dictionary<string, string> param)
        {
            string result = string.Empty;

            IList<string> lsParam = new List<string>();

            foreach (var entry in param)
            {
                lsParam.Add(entry.Key + "=" + entry.Value);
            }

            result = string.Join("&", lsParam);

            return result;

        }
        
        public static Dictionary<string, object> ToMap(string param)
        {
            var result = new Dictionary<string, object>();

            if (string.IsNullOrEmpty(param))
            {
                return result;
            }

            string[] arrParam = param.Split('&');

            foreach (var strParam in arrParam)
            {
                string[] arrP = strParam.Split('=');
                result.Add(arrP[0], arrP.Length > 1 ? arrP[1] : "");
            }

            return result;
        }


    }
}
