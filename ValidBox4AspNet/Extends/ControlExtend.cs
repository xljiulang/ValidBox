using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ValidBox4AspNet;

namespace System.Web
{
    /// <summary>
    /// Html控件扩展
    /// </summary>
    public static partial class ControlExtend
    {
        /// <summary>
        /// 生成控件的前面验证规则
        /// </summary>
        /// <param name="ctrl">控件</param>
        /// <returns></returns>
        public static ValidBox Valid(this HtmlInputControl ctrl)
        {
            return ValidBox.Empty(ctrl);
        }

        /// <summary>
        /// 生成控件的前面验证规则
        /// </summary>
        /// <param name="ctrl">控件</param>
        /// <returns></returns>
        public static ValidBox Valid(this HtmlSelect ctrl)
        {
            return ValidBox.Empty(ctrl);
        }

        /// <summary>
        /// 生成控件的前面验证规则
        /// </summary>
        /// <param name="ctrl">控件</param>
        /// <returns></returns>
        public static ValidBox Valid(this HtmlTextArea ctrl)
        {
            return ValidBox.Empty(ctrl);
        }

        /// <summary>
        /// 生成控件的前面验证规则
        /// </summary>
        /// <param name="ctrl">控件</param>
        /// <returns></returns>
        public static ValidBox Valid(this TextBox ctrl)
        {
            return ValidBox.Empty(ctrl);
        }

        /// <summary>
        /// 生成控件的前面验证规则
        /// </summary>
        /// <param name="ctrl">控件</param>
        /// <returns></returns>
        public static ValidBox Valid(this DropDownList ctrl)
        {
            return ValidBox.Empty(ctrl);
        }

        /// <summary>
        /// 设置前端验证错误显示消息
        /// </summary>
        /// <param name="ctrl">控件</param>
        /// <param name="errorMessage">错误显示消息</param>
        /// <param name="otherClass">其它的class属性</param>
        private static void SetErrorMessageInternal(Control ctrl, string errorMessage, string otherClass)
        {
            var ctrlAttr = ctrl.GetType().GetProperty("Attributes").GetValue(ctrl, null) as AttributeCollection;
            ctrlAttr.Add("class", ("validBox valid-error " + otherClass).Trim());
            ctrlAttr.Add("message", errorMessage);
        }

        /// <summary>
        /// 设置前端验证错误显示消息
        /// </summary>
        /// <param name="ctrl">控件</param>
        /// <param name="errorMessage">错误显示消息</param>      
        public static void SetErrorMessage(this HtmlInputControl ctrl, string errorMessage)
        {
            SetErrorMessageInternal(ctrl, errorMessage, null);
        }

        /// <summary>
        /// 设置前端验证错误显示消息
        /// </summary>
        /// <param name="ctrl">控件</param>
        /// <param name="errorMessage">错误显示消息</param>
        /// <param name="otherClass">其它的class属性</param>
        public static void SetErrorMessage(this HtmlInputControl ctrl, string errorMessage, string otherClass)
        {
            SetErrorMessageInternal(ctrl, errorMessage, otherClass);
        }

        /// <summary>
        /// 设置前端验证错误显示消息
        /// </summary>
        /// <param name="ctrl">控件</param>
        /// <param name="errorMessage">错误显示消息</param>       
        public static void SetErrorMessage(this HtmlSelect ctrl, string errorMessage)
        {
            SetErrorMessageInternal(ctrl, errorMessage, null);
        }

        /// <summary>
        /// 设置前端验证错误显示消息
        /// </summary>
        /// <param name="ctrl">控件</param>
        /// <param name="errorMessage">错误显示消息</param>
        /// <param name="otherClass">其它的class属性</param>
        public static void SetErrorMessage(this HtmlSelect ctrl, string errorMessage, string otherClass)
        {
            SetErrorMessageInternal(ctrl, errorMessage, otherClass);
        }

        /// <summary>
        /// 设置前端验证错误显示消息
        /// </summary>
        /// <param name="ctrl">控件</param>
        /// <param name="errorMessage">错误显示消息</param>       
        public static void SetErrorMessage(this HtmlTextArea ctrl, string errorMessage)
        {
            SetErrorMessageInternal(ctrl, errorMessage, null);
        }

        /// <summary>
        /// 设置前端验证错误显示消息
        /// </summary>
        /// <param name="ctrl">控件</param>
        /// <param name="errorMessage">错误显示消息</param>
        /// <param name="otherClass">其它的class属性</param>
        public static void SetErrorMessage(this HtmlTextArea ctrl, string errorMessage, string otherClass)
        {
            SetErrorMessageInternal(ctrl, errorMessage, otherClass);
        }

        /// <summary>
        /// 设置前端验证错误显示消息
        /// </summary>
        /// <param name="ctrl">控件</param>
        /// <param name="errorMessage">错误显示消息</param>       
        public static void SetErrorMessage(this TextBox ctrl, string errorMessage)
        {
            SetErrorMessageInternal(ctrl, errorMessage, null);
        }

        /// <summary>
        /// 设置前端验证错误显示消息
        /// </summary>
        /// <param name="ctrl">控件</param>
        /// <param name="errorMessage">错误显示消息</param>
        /// <param name="otherClass">其它的class属性</param>
        public static void SetErrorMessage(this TextBox ctrl, string errorMessage, string otherClass)
        {
            SetErrorMessageInternal(ctrl, errorMessage, otherClass);
        }

        /// <summary>
        /// 设置前端验证错误显示消息
        /// </summary>
        /// <param name="ctrl">控件</param>
        /// <param name="errorMessage">错误显示消息</param>       
        public static void SetErrorMessage(this DropDownList ctrl, string errorMessage)
        {
            SetErrorMessageInternal(ctrl, errorMessage, null);
        }

        /// <summary>
        /// 设置前端验证错误显示消息
        /// </summary>
        /// <param name="ctrl">控件</param>
        /// <param name="errorMessage">错误显示消息</param>
        /// <param name="otherClass">其它的class属性</param>
        public static void SetErrorMessage(this DropDownList ctrl, string errorMessage, string otherClass)
        {
            SetErrorMessageInternal(ctrl, errorMessage, otherClass);
        }

        /// <summary>
        /// 验证控件是否输入合法
        /// 如果失败则将提示信息输出到UI
        /// </summary>
        /// <param name="ctrl">控件</param>
        /// <param name="value">控件的值</param>
        /// <returns></returns>
        private static bool IsValidInternal(Control ctrl, string value)
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
            SetErrorMessageInternal(ctrl, firstError.FormatErrorMessage(), null);
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
            return IsValidInternal(ctrl, ctrl.Value);
        }

        /// <summary>
        /// 验证TextArea控件是否输入合法
        /// 如果失败则将提示信息输出到UI
        /// </summary>
        /// <param name="ctrl">控件</param>     
        /// <returns></returns>
        public static bool IsValid(this HtmlTextArea ctrl)
        {
            return IsValidInternal(ctrl, ctrl.Value);
        }

        /// <summary>
        /// 验证Select控件是否输入合法
        /// 如果失败则将提示信息输出到UI
        /// </summary>
        /// <param name="ctrl">控件</param>     
        /// <returns></returns>
        public static bool IsValid(this HtmlSelect ctrl)
        {
            return IsValidInternal(ctrl, ctrl.Value);
        }

        /// <summary>
        /// 验证Select控件是否输入合法
        /// 如果失败则将提示信息输出到UI
        /// </summary>
        /// <param name="ctrl">控件</param>     
        /// <returns></returns>
        public static bool IsValid(this TextBox ctrl)
        {
            return IsValidInternal(ctrl, ctrl.Text);
        }

        /// <summary>
        /// 验证Select控件是否输入合法
        /// 如果失败则将提示信息输出到UI
        /// </summary>
        /// <param name="ctrl">控件</param>     
        /// <returns></returns>
        public static bool IsValid(this DropDownList ctrl)
        {
            return IsValidInternal(ctrl, ctrl.SelectedValue);
        }

        /// <summary>
        /// 后台验证表单下控件输入是否通过
        /// 如果失败则将提示信息输出到UI
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns></returns>
        public static bool IsValid(this HtmlForm form)
        {
            var ctrls = form.Controls.Cast<Control>();
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

                var textBox = ctrl as TextBox;
                if (textBox != null && textBox.IsValid() == false)
                {
                    return false;
                }

                var dropdownList = ctrl as DropDownList;
                if (dropdownList != null && dropdownList.IsValid() == false)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
