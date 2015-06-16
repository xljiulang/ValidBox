using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using ValidBox4Mvc;
using ValidBox4Mvc.ValidRules;
using System.Collections.Concurrent;

namespace System.Web.Mvc.Html
{
    /// <summary>
    /// Html验证扩展类
    /// </summary>
    public static partial class HtmlHeplerExtend
    {
        /// <summary>
        /// 缓存
        /// </summary>
        private readonly static ConcurrentDictionary<PropertyInfo, ValidBox> validBoxCached = new ConcurrentDictionary<PropertyInfo, ValidBox>();

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

            var property = body.Member as PropertyInfo;
            if (property == null)
            {
                throw new ArgumentException("表达式选择的字段不是属性", "keySelector");
            }

            var message = html.ViewData.ModelState.GetErrorMessage(property.Name);
            return validBoxCached.GetOrAdd(property, (p) => GetPropertyValidBox(p, message));
        }

        /// <summary>
        /// 获取属性的ValidBox
        /// </summary>
        /// <param name="property">属性</param>
        /// <param name="message">初始化提示消息</param>
        /// <returns></returns>
        private static ValidBox GetPropertyValidBox(PropertyInfo property, string message)
        {
            var validBox = ValidBox.Empty(message);
            // 值类型非空检测
            if (property.PropertyType.IsValueType && Attribute.IsDefined(property, typeof(RequiredAttribute)) == false)
            {
                validBox = ValidBox.Request(null);
            }

            var boxs = Attribute.GetCustomAttributes(property, false)
                .Where(item => item is IValidRule)
                .Cast<IValidRule>()
                .OrderBy(item => item.OrderIndex)
                .Select(item => item.ToValidBox());

            foreach (var box in boxs)
            {
                validBox = ValidBox.Merge(validBox, box);
            }

            // 数字类型输入检测
            if (property.PropertyType.IsNumberType() == true)
            {
                validBox = ValidBox.Merge(validBox, ValidBox.New("number", null));
            }
            return validBox;
        }

        /// <summary>
        /// 计算类型是否数值类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        private static bool IsNumberType(this Type type)
        {
            return
                type == typeof(byte) ||
                type == typeof(sbyte) ||
                type == typeof(short) ||
                type == typeof(ushort) ||
                type == typeof(int) ||
                type == typeof(uint) ||
                type == typeof(long) ||
                type == typeof(ulong) ||
                type == typeof(float) ||
                type == typeof(double) ||
                type == typeof(decimal);
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