using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc.Html;

namespace ValidBox4Mvc.ValidRules
{
    /// <summary>
    /// 表示验证不要和正则表达式匹配
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class NotMatchAttribute : MatchAttribute
    {
        /// <summary>
        /// 验证不要和正则表达式匹配
        /// </summary>
        /// <param name="pattern">正则表达式</param>
        public NotMatchAttribute(string pattern)
            : base(pattern)
        {
        }

        /// <summary>
        /// 后台验证
        /// </summary>      
        /// <param name="value">属性值</param>
        /// <returns></returns>
        protected override bool IsValid(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return true;
            }
            return !base.IsValid(value);
        }
    }
}
