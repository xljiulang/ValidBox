using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc.Html;

namespace ValidBox4Mvc.ValidRules
{
    /// <summary>
    /// 表示验证不要和目标ID的值一致
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class NotEqualToAttribute : EqualToAttribute
    {
        /// <summary>
        /// 验证不要和目标ID的值一致
        /// </summary>       
        /// <param name="targetId">目标id</param>
        public NotEqualToAttribute(string targetId)
            : base(targetId)
        {
            this.ErrorMessage = "输入的内容不能重复";
        }

        /// <summary>
        /// 生成验证框
        /// </summary>
        /// <returns></returns>
        public override ValidBox ToValidBox()
        {
            var validType = this.ValidTypeName + ValidBox.MakeJsArray(this.TargetId);
            return new ValidBox(validType, this.ErrorMessage);
        }

        /// <summary>
        /// 后台验证
        /// </summary>       
        /// <param name="value">属性的值</param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return true;
            }
            var targetProperty = this.ValidationContext.ObjectType.GetProperty(TargetId, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
            if (targetProperty == null)
            {
                return false;
            }
            var tagrgetValue = targetProperty.GetValue(this.ValidationContext.ObjectInstance, null);
            return !value.Equals(tagrgetValue);
        }
    }
}
