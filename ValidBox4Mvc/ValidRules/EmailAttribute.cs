using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc.Html;

namespace ValidBox4Mvc.ValidRules
{
    /// <summary>
    /// 表示验证是邮箱格式
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class EmailAttribute : ValidRuleBase
    {
        /// <summary>
        /// 验证是邮箱格式
        /// </summary>
        public EmailAttribute()
        {
            this.ErrorMessage = "请输入正确的邮箱";
        }

        /// <summary>
        /// 生成验证框对象
        /// </summary>
        /// <returns></returns>
        public override ValidBox ToValidBox()
        {
            return new ValidBox(this.ValidTypeName, this.ErrorMessage);
        }

        /// <summary>
        /// 后台验证
        /// </summary>
        /// <param name="value">实体属性的值</param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            string currentValue;
            if (base.HasStringValue(value, out currentValue))
            {
                return Regex.IsMatch(currentValue, @"^\w+(\.\w*)*@\w+\.\w+$");
            }
            return true;
        }
    }
}
