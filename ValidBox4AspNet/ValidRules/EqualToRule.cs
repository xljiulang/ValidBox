using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace ValidBox4AspNet.ValidRules
{
    /// <summary>
    /// 表示验证是否和目标ID的值一致
    /// </summary>    
    public class EqualToRule : ValidRuleBase
    {
        /// <summary>
        /// 获取或设置目标ID值
        /// </summary>
        private string TargetId { get; set; }

        /// <summary>
        /// 验证是否和目标ID的值一致
        /// </summary>        
        /// <param name="targetId">目标id</param>
        public EqualToRule(string targetId)
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
            return ValidBox.New(this, this.ValidType, this.ErrorMessage, this.TargetId);
        }

        /// <summary>
        /// 后台验证
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(string value)
        {
            var target = HttpContext.Current.Request[this.TargetId];
            if (target == null)
            {
                target = string.Empty;
            }
            if (value == null)
            {
                value = string.Empty;
            }
            return target == value;
        }
    }
}
