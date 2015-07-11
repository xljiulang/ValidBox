using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace ValidBox4AspNet.ValidRules
{
    /// <summary>
    /// 表示验证不要和正则表达式匹配
    /// </summary>    
    public class NotMatchRule : MatchRule
    {
        /// <summary>
        /// 验证不要和正则表达式匹配
        /// </summary>
        /// <param name="pattern">正则表达式</param>
        public NotMatchRule(string pattern)
            : base(pattern)
        {
        }

        /// <summary>
        /// 后台验证
        /// </summary>      
        /// <param name="value">属性值</param>
        /// <returns></returns>
        public override bool IsValid(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return true;
            }
            return !base.IsValid(value);
        }
    }
}
