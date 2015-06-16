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
    public static partial class HtmlHeplerExtend
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
        /// 生成前端验证规则
        /// </summary>
        /// <param name="html">html助手</param>   
        /// <param name="field">绑定的ModelState字段名</param>
        /// <returns></returns>
        public static ValidBox Valid(this HtmlHelper html, string field)
        {
            var message = html.ViewData.ModelState.GetErrorMessage(field);
            return ValidBox.Empty(message);
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
            if (keySelector == null)
            {
                throw new ArgumentNullException("keySelector");
            }

            var body = keySelector.Body as MemberExpression;
            if (body == null)
            {
                throw new ArgumentException("表达式必须为MemberExpression", "keySelector");
            }

            if (body.Member.DeclaringType.IsAssignableFrom(typeof(T)) == false || body.Expression.NodeType != ExpressionType.Parameter)
            {
                throw new ArgumentException("无法解析的表达式", "keySelector");
            }

            var boxs = Attribute.GetCustomAttributes(typeof(T).GetProperty(body.Member.Name), typeof(IValidRule), false)
                .Cast<IValidRule>()
                .OrderBy(item => item.OrderIndex)
                .Select(item => item.ToValidBox())
                .ToArray();

            var message = html.ViewData.ModelState.GetErrorMessage(body.Member.Name);
            var validBox = ValidBox.Empty(message);

            foreach (var box in boxs)
            {
                validBox = ValidBox.Merge(validBox, box);
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
        public static IDictionary<string, object> ValidFor<T, TKey>(this HtmlHelper<T> html, Expression<Func<T, TKey>> keySelector)
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

        /// <summary>
        /// 依据实体属性的特性生成前端验证规则
        /// </summary>
        /// <typeparam name="T">Model类型</typeparam>
        /// <typeparam name="TKey">键</typeparam>
        /// <param name="html">Html</param>
        /// <param name="keySelector">属性选择表达式</param>    
        /// <param name="attribute">附加的html属性</param>
        /// <returns></returns>
        public static IDictionary<string, object> ValidFor<T, TKey>(this HtmlHelper<T> html, Expression<Func<T, TKey>> keySelector, IDictionary<string, object> attribute)
        {
            return html.GetPropertyValidBox(keySelector).AsHtmlAttribute(attribute);
        }
    }
}