using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using ValidBox4AspNet;

namespace System.Web
{
    /// <summary>
    /// Html控件扩展
    /// </summary>
    public static partial class HtmlControlExtend
    {
        /// <summary>
        /// 生成控件的前面验证规则
        /// </summary>
        /// <param name="ctrl">控件</param>
        /// <returns></returns>
        public static ValidBox Valid(this HtmlControl ctrl)
        {
            return ValidBox.Empty(ctrl);
        }

        /// <summary>
        /// 设置前端验证错误显示消息
        /// </summary>
        /// <param name="ctrl">控件</param>
        /// <param name="errorMessage">错误显示消息</param>
        public static void SetErrorMessage(this HtmlControl ctrl, string errorMessage)
        {
            ctrl.Attributes.Add("class", "validBox valid-error");
            ctrl.Attributes.Add("message", errorMessage);
        }

        /// <summary>
        /// 设置前端验证错误显示消息
        /// </summary>
        /// <param name="ctrl">控件</param>
        /// <param name="errorMessage">错误显示消息</param>
        /// <param name="otherClass">其它的class属性</param>
        public static void SetErrorMessage(this HtmlControl ctrl, string errorMessage, string otherClass)
        {
            ctrl.Attributes.Add("class", ("validBox valid-error " + otherClass).Trim());
            ctrl.Attributes.Add("message", errorMessage);
        }

        /// <summary>
        /// 验证控件是否输入合法
        /// 如果失败则将提示信息输出到UI
        /// </summary>
        /// <param name="ctrl">控件</param>
        /// <param name="value">控件的值</param>
        /// <returns></returns>
        private static bool IsValid(this HtmlControl ctrl, string value)
        {
            var validRules = ctrl.Page.Items[ctrl] as List<IValidRule>;
            if (validRules == null || validRules.Count == 0)
            {
                return true;
            }

            var firstError = validRules.FirstOrDefault(item => item.IsValid(value) == false);
            if (firstError == null)
            {
                return true;
            }
            ctrl.SetErrorMessage(firstError.FormatErrorMessage());
            return false;
        }

        /// <summary>
        /// 验证Input控件是否输入合法
        /// 如果失败则将提示信息输出到UI
        /// </summary>
        /// <param name="ctrl">控件</param>     
        /// <returns></returns>
        public static bool IsValid(this HtmlInputControl ctrl)
        {
            return ctrl.IsValid(ctrl.Value);
        }

        /// <summary>
        /// 验证TextArea控件是否输入合法
        /// 如果失败则将提示信息输出到UI
        /// </summary>
        /// <param name="ctrl">控件</param>     
        /// <returns></returns>
        public static bool IsValid(this HtmlTextArea ctrl)
        {
            return ctrl.IsValid(ctrl.Value);
        }

        /// <summary>
        /// 验证Select控件是否输入合法
        /// 如果失败则将提示信息输出到UI
        /// </summary>
        /// <param name="ctrl">控件</param>     
        /// <returns></returns>
        public static bool IsValid(this HtmlSelect ctrl)
        {
            return ctrl.IsValid(ctrl.Value);
        }

        /// <summary>
        /// 后台验证表单输入是否通过
        /// 如果失败则将提示信息输出到UI
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns></returns>
        public static bool IsValid(this HtmlForm form)
        {
            var ctrls = form.Controls.Cast<Control>().ToArray();
            foreach (var ctrl in ctrls)
            {
                var input = ctrl as HtmlInputControl;
                if (input != null && input.IsValid() == false)
                {
                    return false;
                }

                var textArea = ctrl as HtmlTextArea;
                if (textArea != null && textArea.IsValid() == false)
                {
                    return false;
                }

                var select = ctrl as HtmlSelect;
                if (select != null && select.IsValid() == false)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
