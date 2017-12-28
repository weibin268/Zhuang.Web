using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Zhuang.Web.RestApi
{
    public class RestApiHandler
    {
        public const string CONTROLER_SUFFIX = "Controller";

        public const string ACTION_NAME = "action";

        public const string ARGS_NAME = "args";

        public const char CONTROLLER_ACTION_SEPARATOR = '-';

        public static void ProcessRequest(HttpContext context)
        {
            ProcessRequest(context, typeof(BaseController).Namespace);
        }

        public static void ProcessRequest(HttpContext context, string controlerNamespace)
        {
            string action = context.Request.QueryString[ACTION_NAME];
            string args = context.Request.Form[ARGS_NAME] ?? "{}";
            RestApiJsonResult result = new RestApiJsonResult();
            result.Success = true;
            result.Valid = true;

            try
            {
                string[] arrAction = action.Split(CONTROLLER_ACTION_SEPARATOR);
                var controlerName = arrAction[0];
                var actionName = arrAction.Length > 1 ? arrAction[1] : "";

                controlerName = controlerName.EndsWith(CONTROLER_SUFFIX) ? controlerName : controlerName + CONTROLER_SUFFIX;
                var controlerFullName = controlerNamespace + "." + controlerName;

                var controler = Activator.CreateInstance(typeof(RestApiContext).Assembly.FullName, controlerFullName).Unwrap();

                var controlerType = controler.GetType();

                Type[] paramsType = new Type[] { typeof(RestApiContext) };

                var actionMethod = controlerType.GetMethod(actionName, paramsType);

                if (actionMethod != null)
                {
                    var RestApiContext = new RestApiContext() { Context = context, Action = action, Args = args, Result = result };

                    object[] objPrams = new object[] { RestApiContext };

                    var isContinue = actionMethod.Invoke(controler, objPrams);

                    if (isContinue != null && (bool)isContinue == false)
                        return;

                }
                else
                {
                    throw new RestApiException(string.Format("接口“{0}”找不到对应的处理方法！", action));
                }
            }
            catch (Exception outerEx)
            {
                Exception ex = outerEx.InnerException == null ? outerEx : outerEx.InnerException;

                if (ex.GetType() == typeof(RestApiException))
                {
                    result.Message = string.Format("{0}", ex.Message);
                    result.Valid = false;
                }
                else
                {
                    result.Message = string.Format("{0}|StackTrace{1}", ex.Message, ex.StackTrace);

                    try
                    {
                        //QASupervision.BLL.Common.LogHelper.ErrorLog(ex, new { action = action, args = args });
                    }
                    catch
                    {
                    }

                    result.Success = false;
                }

            }

            IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
            timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

            string strResult = Newtonsoft.Json.JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented, timeFormat);
            
            //context.Response.ContentType = "text/plain";
            context.Response.ContentType = "application/json;charset=UTF-8";

            context.Response.Write(strResult);
            context.Response.End();

        }

    }
}
