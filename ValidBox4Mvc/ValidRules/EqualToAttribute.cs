using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc.Html;

namespace ValidBox4Mvc.ValidRules
{
    /// <summary>
    /// 表示验证是否和目标ID的值一致
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class EqualToAttribute : ValidRuleBase
    {
        /// <summary>
        /// 匹配的目标ID
        /// </summary>
        public string TargetId { get; set; }

        /// <summary>
        /// 验证是否和目标ID的值一致
        /// </summary>        
        /// <param name="targetId">目标id</param>
        public EqualToAttribute(string targetId)
        {
            this.TargetId = targetId;
            this.ErrorMessage = "两次输入的字符不一至";
        }

        /// <summary>
        /// 生成验证框对象
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
            string currentValue;
            if (base.HasStringValue(value, out currentValue))
            {
                var targetProperty = this.ValidationContext.ObjectType.GetProperty(TargetId, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
                if (targetProperty == null)
                {
                    return false;
                }
                var tagrgetValue = targetProperty.GetValue(this.ValidationContext.ObjectInstance, null);
                if (tagrgetValue == null)
                {
                    return false;
                }
                return currentValue == tagrgetValue.ToString();
            }
            return true;
        }
    }
}
