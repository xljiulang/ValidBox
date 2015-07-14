using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidBox4AspNet.ValidRules
{
    /// <summary>
    /// 表示要求必须输入
    /// 此特性影响EF-CodeFirst生成的数据库字段为非空约束
    /// </summary>   
    public class RequiredRule : ValidRuleBase
    {
        /// <summary>
        /// 要求必须输入
        /// </summary>
        public RequiredRule()
        {
            this.ErrorMessage = "该项为必填项";
        }

        /// <summary>
        /// 生成验证框对象
        /// </summary>
        /// <returns></returns>
        public override ValidBox ToValidBox()
        {
            return ValidBox.Request(this, this.ErrorMessage);
        }

        /// <summary>
        /// 后台验证
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(string value)
        {
            return value != null && value.Trim().Length > 0;
        }
    }
}
