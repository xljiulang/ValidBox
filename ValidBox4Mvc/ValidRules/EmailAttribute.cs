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
        /// 转换为对应的ValidBox类型
        /// </summary>
        /// <returns></returns>
        public override ValidBox ToValidBox()
        {
            return ValidBox.New(this.ValidType, this.ErrorMessage);
        }

        /// <summary>
        /// 后台验证
        /// </summary>
        /// <param name="value">实体属性的值</param>
        /// <returns></returns>
        protected override bool IsValid(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return true;
            }
            return Regex.IsMatch(value, @"^\w+(\.\w*)*@\w+\.\w+$");
        }
    }
}
