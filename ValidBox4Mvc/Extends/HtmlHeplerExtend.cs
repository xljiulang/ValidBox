using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using ValidBox4Mvc;
using ValidBox4Mvc.ValidRules;

namespace System.Web.Mvc.Html
{
    /// <summary>
    /// Html验证扩展类
    /// </summary>
    public static class HtmlHeplerExtend
    {
        /// <summary>
        /// 生成前端验证规则
        /// </summary>
        /// <param name="html">html助手</param>
        /// <returns></returns>
        public static ValidBox Valid(this HtmlHelper html)
        {
            return ValidBox.Empty();
        }

        /// <summary>
        /// 获取表达式对应属性的验证框描述
        /// </summary>
        /// <typeparam name="T">Model类型</typeparam>
        /// <typeparam name="TKey">键</typeparam>
        /// <param name="html">Html</param>
        /// <param name="keySelector">属性选择表达式</param>
        /// <returns></returns>
        private static ValidBox GetPropertyValidBox<T, TKey>(this HtmlHelper<T> html, Expression<Func<T, TKey>> keySelector)
        {
            // 解析表达式
            var body = keySelector.Body as MemberExpression;
            if (body == null || body.Member.DeclaringType.IsAssignableFrom(typeof(T)) == false || body.Expression.NodeType != ExpressionType.Parameter)
            {
                return ValidBox.Empty();
            }

            // 过滤获取验证属性
            var boxAttributeArray = typeof(T).GetProperty(body.Member.Name)
                .GetCustomAttributes(false)
                .Where(item => item is IValidRule)
                .Cast<IValidRule>()
                .OrderBy(item => item.OrderIndex)
                .ToArray();

            if (boxAttributeArray.Length == 0)
            {
                return ValidBox.Empty();
            }

            // 生成验证规则
            var validBox = boxAttributeArray[0].ToValidBox();
            if (boxAttributeArray.Length > 1)
            {
                for (var i = 1; i < boxAttributeArray.Length; i++)
                {
                    var box = boxAttributeArray[i].ToValidBox();
                    validBox = validBox & box;
                }
            }
            return validBox;
        }

        /// <summary>
        /// 依据实体属性的特性生成前端验证规则
        /// </summary>
        /// <typeparam name="T">Model类型</typeparam>
        /// <typeparam name="TKey">键</typeparam>
        /// <param name="html">Html</param>
        /// <param name="keySelector">属性选择表达式</param>      
        /// <returns></returns>
        public static object ValidFor<T, TKey>(this HtmlHelper<T> html, Expression<Func<T, TKey>> keySelector)
        {
            return html.GetPropertyValidBox(keySelector).AsHtmlAttribute();
        }

        /// <summary>
        /// 依据实体属性的特性生成前端验证规则
        /// </summary>
        /// <typeparam name="T">Model类型</typeparam>
        /// <typeparam name="TKey">键</typeparam>
        /// <param name="html">Html</param>
        /// <param name="keySelector">属性选择表达式</param>    
        /// <param name="attribute">附加的html属性</param>
        /// <returns></returns>
        public static IDictionary<string, object> ValidFor<T, TKey>(this HtmlHelper<T> html, Expression<Func<T, TKey>> keySelector, object attribute)
        {
            return html.GetPropertyValidBox(keySelector).AsHtmlAttribute(attribute);
        }
    }
}