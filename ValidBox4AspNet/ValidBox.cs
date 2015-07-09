using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ValidBox4AspNet
{
    /// <summary>
    /// 输入框验证实体
    /// 承担C#模型转换为ValidBox.js验证参数功能
    /// 此类不可继承
    /// </summary>
    public sealed class ValidBox
    {
        /// <summary>
        /// 关联的控件
        /// </summary>
        private Control ctrl;

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
        /// 前台验证规则
        /// </summary>
        private List<object> ruleList = new List<object>();

        /// <summary>
        /// 后台验证规则
        /// </summary>
        private List<IValidRule> validRuleList = new List<IValidRule>();

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
        private IDictionary<string, object> AsHtmlAttribute()
        {
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

            if (this.ruleList.Count > 0)
            {
                var rules = new JavaScriptSerializer().Serialize(this.ruleList);
                attributes.Add("valid-rule", rules);
            }

            if (string.IsNullOrEmpty(this.message) == false)
            {
                attributes["class"] = "validBox valid-error";
                attributes.Add("message", this.message);
            }
            return attributes;
        }

        /// <summary>
        /// 应用验证规则
        /// </summary>
        public void Apply()
        {
            var ctrlAttr = this.ctrl.GetType().GetProperty("Attributes").GetValue(this.ctrl, null) as AttributeCollection;
            if (ctrlAttr == null)
            {
                return;
            }

            var attributes = this.AsHtmlAttribute();
            foreach (var kv in attributes)
            {
                ctrlAttr.Add(kv.Key, kv.Value.ToString());
            }
            this.ctrl.Page.Items[this.ctrl] = this.validRuleList;
        }

        /// <summary>
        /// 表示生成无规则的空验证框
        /// </summary>       
        /// <param name="ctrl">要绑定的控件</param>
        /// <returns></returns>
        public static ValidBox Empty(Control ctrl)
        {
            return new ValidBox { ctrl = ctrl };
        }

        /// <summary>
        /// 表示生成无规则的空验证框
        /// </summary>
        /// <param name="ctrl">要绑定的控件</param>
        /// <param name="message">初始化提示消息</param>
        /// <returns></returns>
        public static ValidBox Empty(Control ctrl, string message)
        {
            return new ValidBox() { ctrl = ctrl, message = message };
        }

        /// <summary>
        /// 表示必须输入的验证框对象
        /// </summary>
        /// <param name="validRule">后台验证规则</param>
        /// <param name="requiredMessage">未输入时的提示信息</param>
        public static ValidBox Request(IValidRule validRule, string requiredMessage)
        {
            var box = new ValidBox { required = true, requiredMessage = requiredMessage };
            box.validRuleList.Add(validRule);
            return box;
        }

        /// <summary>
        /// 表示一般验证框对象
        /// <param name="validRule">后台验证规则</param>
        /// <param name="validType">验证类型</param>
        /// <param name="validMessage">不通过时提示信息</param>
        /// <param name="parameters">验证的参数</param>
        /// </summary>
        public static ValidBox New(IValidRule validRule, string validType, string validMessage, params object[] parameters)
        {
            var box = new ValidBox();
            var rule = ValidBox.GenerateRule(validType, validMessage, parameters);
            box.validRuleList.Add(validRule);
            box.ruleList.Add(rule);
            return box;
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
        /// 验证框合并操作
        /// 返回合并后的验证框
        /// </summary>       
        /// <param name="box1">要合并的对象1</param>
        /// /// <param name="box2">要合并的对象2</param>
        /// <returns></returns>
        public static ValidBox Merge(ValidBox box1, ValidBox box2)
        {
            var box = new ValidBox();

            box.ruleList.AddRange(box1.ruleList);
            box.ruleList.AddRange(box2.ruleList);

            box.validRuleList.AddRange(box1.validRuleList);
            box.validRuleList.AddRange(box2.validRuleList);
            box.ctrl = box1.ctrl == null ? box2.ctrl : box1.ctrl;

            box.required = box1.required ? box1.required : box2.required;
            box.requiredMessage = box1.required ? box1.requiredMessage : box2.requiredMessage;
            box.message = string.IsNullOrEmpty(box1.message) ? box2.message : box1.message;
            return box;
        }
    }
}