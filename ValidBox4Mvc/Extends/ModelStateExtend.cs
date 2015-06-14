using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace System.Web.Mvc
{
    /// <summary>
    /// ModelState扩展
    /// </summary>
    public static partial class ModelStateExtend
    {
        /// <summary>
        /// 获取第一个验证错误信息
        /// </summary>
        /// <param name="modelState">modelState</param>
        /// <returns></returns>
        public static KeyValuePair<string, string> FirstModelError(this ModelStateDictionary modelState)
        {
            foreach (var key in modelState.Keys)
            {
                if (modelState[key].Errors.Count > 0)
                {
                    var error = modelState[key].Errors[0];
                    return new KeyValuePair<string, string>(key, error.ErrorMessage);
                }
            }
            return new KeyValuePair<string, string>();
        }

        /// <summary>
        /// 获取第一个验证错误信息内容
        /// </summary>
        /// <param name="modelState">modelState</param>
        /// <returns></returns>
        public static string FirstModelErrorMessage(this ModelStateDictionary modelState)
        {
            return modelState.FirstModelError().Value;
        }
    }
}
