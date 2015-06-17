using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ValidBox4Mvc
{
    /// <summary>
    /// 输入框验证实体
    /// 承担C#模型转换为ValidBox.js验证参数功能
    /// 此类不可继承
    /// </summary>
    public sealed class ValidBox
    {
        /// <summary>
        /// 初始时消息
        /// </summary>
        private string message;

        /// <summary>
        /// 是否为必须输入
        /// </summary>
        private bool required;

        /// <summary>
        /// 必须输入且无输入时提示语
        /// </summary>
        private string requiredMessage;

        /// <summary>
        /// Html属性缓存
        /// </summary>
        private IDictionary<string, object> attributeCached;

        /// <summary>
        /// 验证规则
        /// </summary>
        private List<object> validRuleList = new List<object>();

        /// <summary>
        /// 验证框        
        /// </summary>       
        private ValidBox()
        {
        }

        /// <summary>
        /// 转换为Html属性对象
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, object> AsHtmlAttribute()
        {
            if (this.attributeCached != null)
            {
                return this.attributeCached;
            }

            var attributes = new Dictionary<string, object>();
            attributes.Add("class", "validBox");

            if (this.required == true)
            {
                attributes.Add("required", "required");
                if (string.IsNullOrEmpty(this.requiredMessage) == false)
                {
                    attributes.Add("required-message", this.requiredMessage);
                }
            }

            if (this.validRuleList.Count > 0)
            {
                var rules = new JavaScriptSerializer().Serialize(this.validRuleList);
                attributes.Add("valid-rule", rules);
            }

            if (string.IsNullOrEmpty(this.message) == false)
            {
                attributes["class"] = "validBox valid-error";
                attributes.Add("message", this.message);
            }

            this.attributeCached = attributes;
            return attributes;
        }

        /// <summary>
        /// 转换为Html属性对象
        /// <param name="attribute">附加的html属性</param>
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, object> AsHtmlAttribute(IDictionary<string, object> attribute)
        {
            if (attribute == null)
            {
                return this.AsHtmlAttribute();
            }

            var attributes = this.AsHtmlAttribute().ToDictionary((kv) => kv.Key, kv => kv.Value, StringComparer.OrdinalIgnoreCase);
            foreach (var item in attribute)
            {
                if (attributes.ContainsKey(item.Key) == false)
                {
                    attributes.Add(item.Key, item.Value);
                }
                else if (string.Equals(item.Key, "class", StringComparison.OrdinalIgnoreCase))
                {
                    attributes[item.Key] = string.Format("{0} {1}", attributes[item.Key], item.Value).Trim();
                }
            }
            return attributes;
        }

        /// <summary>
        /// 转换为Html属性对象
        /// <param name="attribute">附加的html属性</param>
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, object> AsHtmlAttribute(object attribute)
        {
            if (attribute == null)
            {
                return this.AsHtmlAttribute();
            }

            var kvs = attribute as IDictionary<string, object>;
            if (kvs == null)
            {
                kvs = HtmlHelper.AnonymousObjectToHtmlAttributes(attribute);
            }
            return this.AsHtmlAttribute(kvs);
        }

        /// <summary>
        /// 表示生成无规则的空验证框
        /// </summary>       
        /// <returns></returns>
        public static ValidBox Empty()
        {
            return new ValidBox();
        }

        /// <summary>
        /// 表示生成无规则的空验证框
        /// </summary>
        /// <param name="message">初始化提示消息</param>
        /// <returns></returns>
        public static ValidBox Empty(string message)
        {
            return new ValidBox() { message = message };
        }

        /// <summary>
        /// 表示必须输入的验证框对象
        /// </summary>
        /// <param name="requiredMessage">未输入时的提示信息</param>
        public static ValidBox Request(string requiredMessage)
        {
            return new ValidBox { required = true, requiredMessage = requiredMessage };
        }

        /// <summary>
        /// 生成验证规则实体
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="message">消息</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        private static object GenerateRule(string name, string message, object[] parameters)
        {
            var nonMessage = string.IsNullOrEmpty(message);
            var nonParameter = parameters == null || parameters.Length == 0;

            if (nonMessage && nonParameter)
            {
                return new { n = name };
            }
            if (nonMessage == true)
            {
                return new { n = name, p = parameters };
            }
            if (nonParameter == true)
            {
                return new { n = name, m = message };
            }
            return new { n = name, p = parameters, m = message };
        }

        /// <summary>
        /// 表示一般验证框对象
        /// <param name="validType">验证类型</param>
        /// <param name="validMessage">不通过时提示信息</param>
        /// <param name="parameters">验证的参数</param>
        /// </summary>
        public static ValidBox New(string validType, string validMessage, params object[] parameters)
        {
            var box = new ValidBox();
            var rule = ValidBox.GenerateRule(validType, validMessage, parameters);
            box.validRuleList.Add(rule);
            return box;
        }


        /// <summary>
        /// 验证框合并操作
        /// 返回合并后的验证框
        /// </summary>       
        /// <param name="box1">要合并的对象1</param>
        /// /// <param name="box2">要合并的对象2</param>
        /// <returns></returns>
        public static ValidBox Merge(ValidBox box1, ValidBox box2)
        {
            var box = new ValidBox();
            box.validRuleList.AddRange(box1.validRuleList);
            box.validRuleList.AddRange(box2.validRuleList);

            box.required = box1.required ? box1.required : box2.required;
            box.requiredMessage = box1.required ? box1.requiredMessage : box2.requiredMessage;
            box.message = string.IsNullOrEmpty(box1.message) ? box2.message : box1.message;
            return box;
        }
    }
}