using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ValidBox4AspNet.ValidRules
{
    /// <summary>
    /// 表示验证长度不能大于指定值
    /// </summary>   
    public class MaxLengthRule : ValidRuleBase
    {
        /// <summary>
        /// 获取或设置长度
        /// </summary>
        protected int Length { get; set; }

        /// <summary>
        /// 验证长度长度不能大于指定值
        /// </summary>
        /// <param name="length">长度</param>
        public MaxLengthRule(int length)
        {
            this.Length = length;
            this.ErrorMessage = "长度不能超过{0}个字";
        }

        /// <summary>
        /// 生成验证框对象
        /// </summary>
        /// <returns></returns>
        public override ValidBox ToValidBox()
        {
            return ValidBox.New(this, this.ValidType, this.ErrorMessage, this.Length);
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
            return value.Length <= this.Length;
        }

        /// <summary>
        /// 返回格式化错误提示语
        /// </summary>
        /// <returns></returns>
        public override string FormatErrorMessage()
        {
            return string.Format(this.ErrorMessage, this.Length);
        }
    }
}
