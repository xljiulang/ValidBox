using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc.Html;

namespace ValidBox4Mvc.ValidRules
{
    /// <summary>
    /// 表示远程验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class RemoteAttribute : ValidRuleBase
    {
        /// <summary>
        /// 获取或设置远程地址
        /// </summary>
        protected string Url { get; set; }

        /// <summary>
        /// 获取或设置要传递的目标标签的Id
        /// </summary>
        protected string[] TargetId { get; set; }

        /// <summary>
        /// 远程验证
        /// </summary>
        /// <param name="url">远程地址</param>
        /// <param name="targetId">要传递的目标标签的Id</param>
        public RemoteAttribute(string url, params string[] targetId)
        {
            if (targetId == null)
            {
                throw new ArgumentNullException("targetId");
            }
            if (targetId.Length == 0)
            {
                throw new ArgumentOutOfRangeException("targetId", "目标元素ID至少为一个");
            }
            this.Url = url;
            this.TargetId = targetId;
            this.OrderIndex = 2;
        }

        /// <summary>
        /// 生成验证框
        /// </summary>
        /// <returns></returns>
        public override ValidBox ToValidBox()
        {
            var parameters = new object[] { this.Url }.Concat(this.TargetId).ToArray();
            return new ValidBox(this.ValidType, this.ErrorMessage, parameters);
        }
    }
}
