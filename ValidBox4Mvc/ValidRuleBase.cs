using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ValidBox4Mvc
{
    /// <summary>
    /// 验证规则特性基础类
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class ValidRuleBase : ValidationAttribute, IValidRule
    {
        /// <summary>
        /// 获取验证上下文
        /// </summary>
        protected ValidationContext ValidationContext { get; private set; }

        /// <summary>
        /// 获取自身对应的验证类型
        /// </summary>
        protected virtual string ValidType
        {
            get
            {
                var validType = this.GetType().Name.Replace("Attribute", null);
                return char.ToLower(validType.First()).ToString() + new string(validType.Skip(1).ToArray());
            }
        }

        /// <summary>
        /// 获取或设置排序索引
        /// 越小越优先
        /// </summary>
        public int OrderIndex { get; set; }


        /// <summary>
        /// 转换为对应的ValidBox类型
        /// </summary>
        /// <returns></returns>
        public abstract ValidBox ToValidBox();

        /// <summary>
        /// 根据当前的验证特性来验证指定的值
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="validationContext">上下文</param>
        /// <returns></returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            this.ValidationContext = validationContext;
            return base.IsValid(value, validationContext);
        }

        /// <summary>
        /// 根据当前的验证特性来验证指定的值
        /// 默认返回true
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            return true;
        }

        /// <summary>
        /// 格式化错误提示信息
        /// </summary>
        /// <param name="name">字段名字</param>
        /// <returns></returns>
        public override string FormatErrorMessage(string name)
        {
            return this.ErrorMessage;
        }

        /// <summary>
        /// 判断字段是否有值
        /// </summary>
        /// <param name="relateValue">属性值</param>
        /// <param name="value">输出值</param>
        /// <returns></returns>
        protected bool HasStringValue(object relateValue, out string value)
        {
            value = relateValue == null ? null : relateValue.ToString();
            return string.IsNullOrEmpty(value) == false;
        }
    }
}
