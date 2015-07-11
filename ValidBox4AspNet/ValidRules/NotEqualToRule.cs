using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;


namespace ValidBox4AspNet.ValidRules
{
    /// <summary>
    /// 表示验证不要和目标ID的值一致
    /// </summary>   
    public class NotEqualToRule : EqualToRule
    {
        /// <summary>
        /// 验证不要和目标ID的值一致
        /// </summary>       
        /// <param name="targetId">目标id</param>
        public NotEqualToRule(string targetId)
            : base(targetId)
        {
            this.ErrorMessage = "输入的内容不能重复";
        }

        /// <summary>
        /// 后台验证
        /// </summary>       
        /// <param name="value">属性的值</param>
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
