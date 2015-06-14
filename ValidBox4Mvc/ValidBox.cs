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
        private string _message;

        /// <summary>
        /// 获取字段是否是必须输入的
        /// </summary>
        private KeyValuePair<bool, string> _required;

        /// <summary>
        /// 验证规则
        /// </summary>
        private List<ValidRule> _validRuleList;

        /// <summary>
        /// 验证框        
        /// </summary>       
        private ValidBox()
        {
            this._validRuleList = new List<ValidRule>();
        }

        /// <summary>
        /// 表示必须输入的验证框对象
        /// </summary>
        /// <param name="requiredMessage">未输入时的提示信息</param>
        internal ValidBox(string requiredMessage)
            : this()
        {
            this._required = new KeyValuePair<bool, string>(true, requiredMessage);
        }

        /// <summary>
        /// 表示一般验证框对象
        /// <param name="validType">验证类型</param>
        /// <param name="validMessage">不通过时提示信息</param>
        /// <param name="parameters">验证的参数</param>
        /// </summary>
        public ValidBox(string validType, string validMessage, params object[] parameters)
            : this()
        {
            var rule = new ValidRule
            {
                r = validType,
                p = parameters,
                m = validMessage
            };
            this._validRuleList.Add(rule);
        }

        /// <summary>
        /// 转换为Html属性对象
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, object> AsHtmlAttribute()
        {
            var attributes = new Dictionary<string, object>();
            attributes.Add("class", "validBox");

            if (this._required.Key)
            {
                attributes.Add("required", "required");
                if (string.IsNullOrEmpty(this._required.Value) == false)
                {
                    attributes.Add("required-message", this._required.Value);
                }
            }

            if (this._validRuleList.Count > 0)
            {
                var rules = new JavaScriptSerializer().Serialize(this._validRuleList);
                attributes.Add("valid-rule", rules);
            }

            if (string.IsNullOrEmpty(this._message) == false)
            {
                attributes["class"] = "validBox valid-error";
                attributes.Add("message", this._message);
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
            var attributes = this.AsHtmlAttribute();
            if (attribute == null)
            {
                return attributes;
            }

            var kvs = attribute as IDictionary<string, object>;
            if (kvs == null)
            {
                kvs = HtmlHelper.AnonymousObjectToHtmlAttributes(attribute);
            }

            foreach (var item in kvs)
            {
                if (attributes.ContainsKey(item.Key))
                {
                    if (item.Key == "class")
                    {
                        attributes[item.Key] = string.Format("{0} {1}", attributes[item.Key], item.Value).Trim();
                    }
                }
                else
                {
                    attributes.Add(item.Key, item.Value);
                }
            }
            return attributes;
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
            return new ValidBox() { _message = message };
        }

        /// <summary>
        /// 验证框合并操作
        /// 返回合并后的验证框
        /// </summary>       
        /// <param name="box1">要合并的对象1</param>
        /// /// <param name="box2">要合并的对象2</param>
        /// <returns></returns>
        private static ValidBox Merge(ValidBox box1, ValidBox box2)
        {
            var box = new ValidBox();
            box._validRuleList.AddRange(box1._validRuleList);
            box._validRuleList.AddRange(box2._validRuleList);

            box._required = box1._required.Key ? box1._required : box2._required;
            box._message = string.IsNullOrEmpty(box1._message) ? box2._message : box1._message;
            return box;
        }

        /// <summary>
        /// 合并操作符号 
        /// </summary>
        /// <param name="box1">要合并的对象1</param>
        /// <param name="box2">要合并的对象2</param>
        /// <returns></returns>
        public static ValidBox operator &(ValidBox box1, ValidBox box2)
        {
            return ValidBox.Merge(box1, box2);
        }
    }
}