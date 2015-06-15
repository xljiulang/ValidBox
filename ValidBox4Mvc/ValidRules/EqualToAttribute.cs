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
        /// 获取或设置目标ID值
        /// </summary>
        private string TargetId { get; set; }

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
        /// 转换为对应的ValidBox类型
        /// </summary>
        /// <returns></returns>
        public override ValidBox ToValidBox()
        {
            return ValidBox.New(this.ValidType, this.ErrorMessage, this.TargetId);
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
                var targetProperty = this.ValidationContext.ObjectType.GetProperty(this.TargetId, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
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
