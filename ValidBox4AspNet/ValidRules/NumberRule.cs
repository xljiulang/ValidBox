using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidBox4AspNet.ValidRules
{
    /// <summary>
    /// 表示数字
    /// </summary>
    public class NumberRule : ValidRuleBase
    {
        /// <summary>
        /// 转换为ValidBox
        /// </summary>
        /// <returns></returns>
        public override ValidBox ToValidBox()
        {
            this.ErrorMessage = "请输入正确的数值";
            return ValidBox.New(this, this.ValidType, this.ErrorMessage);
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
            var number = 0d;
            return double.TryParse(value, out number);
        }
    }
}
