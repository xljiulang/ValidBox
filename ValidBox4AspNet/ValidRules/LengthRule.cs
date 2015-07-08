using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ValidBox4AspNet.ValidRules
{
    /// <summary>
    /// 表示验证输入的长度范围
    /// maxLength参数会影响EF-CodeFirst生成的数据库字段最大长度
    /// </summary>   
    public class LengthRule : ValidRuleBase, IValidRule
    {
        /// <summary>
        /// 获取或设置最大小长度
        /// </summary>
        public int MinimumLength { get; set; }

        /// <summary>
        /// 获取或设置最大长度
        /// </summary>
        public int MaximumLength { get; set; }

        /// <summary>
        /// 验证输入的长度范围
        /// </summary>
        /// <param name="maxLength">最大长度</param>
        public LengthRule(int maxLength)
        {
            this.MaximumLength = maxLength;
            this.ErrorMessage = "长度必须介于1到{1}个字";
        }

        /// <summary>
        /// 验证输入的长度范围
        /// </summary>       
        /// <param name="minLength">最小长度</param>
        /// <param name="maxLength">最大长度</param>
        public LengthRule(int minLength, int maxLength)
            : this(maxLength)
        {
            this.MinimumLength = minLength;
            this.ErrorMessage = "长度必须介于{0}到{1}个字";
        }

        /// <summary>
        /// 生成验证框对象
        /// </summary>
        /// <returns></returns>
        public override ValidBox ToValidBox()
        {
            return ValidBox.New(this, "length", this.ErrorMessage, this.MinimumLength, this.MaximumLength);
        }

        /// <summary>
        /// 后台验证
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return true;
            }
            return value.Length >= this.MinimumLength && value.Length <= this.MaximumLength;
        }

        /// <summary>
        /// 返回格式化错误提示语
        /// </summary>
        /// <returns></returns>
        public override string FormatErrorMessage()
        {
            return string.Format(this.ErrorMessage, this.MinimumLength, this.MaximumLength);
        }
    }
}
