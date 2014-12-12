using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Web.Mvc
{
    /// <summary>
    /// ModelState扩展
    /// </summary>
    public static class ModelStateExtend
    {
        /// <summary>
        /// 获取第一个验证错误信息
        /// </summary>
        /// <param name="modelState">modelState</param>
        /// <returns></returns>
        public static ModelError FirstModelError(this ModelStateDictionary modelState)
        {
            foreach (var key in modelState.Keys)
            {
                if (modelState[key].Errors.Count > 0)
                {
                    return modelState[key].Errors[0];
                }
            }
            return null;
        }
    }
}
