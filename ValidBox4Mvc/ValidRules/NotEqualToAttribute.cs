using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc.Html;

namespace ValidBox4Mvc.ValidRules
{
    /// <summary>
    /// 表示验证不要和目标ID的值一致
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class NotEqualToAttribute : EqualToAttribute
    {
        /// <summary>
        /// 验证不要和目标ID的值一致
        /// </summary>       
        /// <param name="targetId">目标id</param>
        public NotEqualToAttribute(string targetId)
            : base(targetId)
        {
            this.ErrorMessage = "输入的内容不能重复";
        }

        /// <summary>
        /// 后台验证
        /// </summary>       
        /// <param name="value">属性的值</param>
        /// <returns></returns>
        protected override bool IsValid(string value)
        {
            return !base.IsValid(value);
        }
    }
}
