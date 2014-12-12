using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc.Html;

namespace ValidBox4Mvc.ValidRules
{
    /// <summary>
    /// 表示验证是否和正则表达式匹配
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class MatchAttribute : ValidRuleBase
    {
        /// <summary>
        /// 正则表达式
        /// </summary>
        public string RegParam { get; set; }

        /// <summary>
        /// 验证是否和正则表达式匹配
        /// </summary>
        /// <param name="regParam">正则表达式</param>
        public MatchAttribute(string regParam)
        {
            this.RegParam = regParam;
            this.OrderIndex = 1;
            this.ErrorMessage = "请输入正确的值";
        }

        /// <summary>
        /// 生成验证框
        /// </summary>
        /// <returns></returns>
        public override ValidBox ToValidBox()
        {
            var validType = this.ValidTypeName + ValidBox.MakeJsArray(this.RegParam.Replace(@"\", @"\\"));
            return new ValidBox(validType, this.ErrorMessage);
        }

        /// <summary>
        /// 后台验证
        /// </summary>
        /// <param name="value">属性值</param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            string currentValue;
            if (base.HasStringValue(value, out currentValue))
            {
                return Regex.IsMatch(currentValue, this.RegParam);
            }
            return true;
        }
    }
}
