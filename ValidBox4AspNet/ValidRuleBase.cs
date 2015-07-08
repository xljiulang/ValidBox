using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ValidBox4AspNet
{
    /// <summary>
    /// 验证规则特性基础类
    /// </summary>  
    public abstract class ValidRuleBase : IValidRule
    {
        /// <summary>
        /// 获取或设置错误提示浙江省上
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 转换为对应的ValidBox类型
        /// </summary>
        /// <returns></returns>
        public abstract ValidBox ToValidBox();

        /// <summary>
        /// 获取自身对应的验证类型
        /// </summary>
        protected virtual string ValidType
        {
            get
            {
                var validType = Regex.Replace(this.GetType().Name, "Rule$", string.Empty);
                return char.ToLower(validType.First()).ToString() + new string(validType.Skip(1).ToArray());
            }
        }

        /// <summary>
        /// 后台验证
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual bool IsValid(string value)
        {
            return true;
        }

        /// <summary>
        /// 返回格式化的错误提示语
        /// </summary>
        /// <returns></returns>
        public virtual string FormatErrorMessage()
        {
            return this.ErrorMessage;
        }
    }
}
