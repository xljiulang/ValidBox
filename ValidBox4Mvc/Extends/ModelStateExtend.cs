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
        /// 获取ModelState的第一个错误信息
        /// </summary>
        /// <param name="modelState">modelState</param>
        /// <returns></returns>
        private static string FirstErrorMessage(this ModelState modelState)
        {
            if (modelState == null || modelState.Errors.Count == 0)
            {
                return null;
            }
            return modelState.Errors[0].ErrorMessage;
        }

        /// <summary>
        /// 获取错误提示信息
        /// </summary>
        /// <param name="modelStates">ModelStateDictionary</param>
        /// <param name="field">字段名</param>
        /// <returns></returns>
        public static string GetErrorMessage(this ModelStateDictionary modelStates, string field)
        {
            ModelState state;
            modelStates.TryGetValue(field, out state);
            return state.FirstErrorMessage();
        }

        /// <summary>
        /// 获取第一个验证错误信息
        /// </summary>
        /// <param name="modelStates">ModelStateDictionary</param>
        /// <returns></returns>
        public static KeyValuePair<string, string> FirstModelError(this ModelStateDictionary modelStates)
        {
            foreach (var key in modelStates.Keys)
            {
                if (modelStates[key].Errors.Count > 0)
                {
                    var error = modelStates[key].Errors[0];
                    return new KeyValuePair<string, string>(key, error.ErrorMessage);
                }
            }
            return new KeyValuePair<string, string>();
        }

        /// <summary>
        /// 获取第一个验证错误信息内容
        /// </summary>
        /// <param name="modelStates">ModelStateDictionary</param>
        /// <returns></returns>
        public static string FirstModelErrorMessage(this ModelStateDictionary modelStates)
        {
            return modelStates.FirstModelError().Value;
        }
    }
}
