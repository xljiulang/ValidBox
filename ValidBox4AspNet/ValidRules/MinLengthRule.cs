using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ValidBox4AspNet.ValidRules
{
    /// <summary>
    /// 表示验证长度不能小于指定值
    /// </summary>   
    public class MinLengthRule : MaxLengthRule
    {
        /// <summary>
        /// 验证长度不能小于指定值
        /// </summary>
        /// <param name="length">最小长度</param>
        public MinLengthRule(int length)
            : base(length)
        {
            this.ErrorMessage = "长度不能小于{0}个字";
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
            return value.Length >= this.Length;
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
