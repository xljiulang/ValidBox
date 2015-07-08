using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc.Html;

namespace ValidBox4Mvc.ValidRules
{
    /// <summary>
    /// 表示验证长度不能小于指定值
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class MinLengthAttribute : MaxLengthAttribute
    {
        /// <summary>
        /// 验证长度不能小于指定值
        /// </summary>
        /// <param name="length">最小长度</param>
        public MinLengthAttribute(int length)
            : base(length)
        {
            this.ErrorMessage = "长度不能小于{0}个字";
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
            return value.Length >= this.Length;
        }
    }
}
