using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace System.Web
{
    /// <summary>
    /// 提供对控件使用扩展
    /// </summary>
    public static class ControlHelperExtend
    {
        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="ctrl">控件</param>
        /// <param name="dataSource">数据源</param>
        /// <param name="textField">文本字段名</param>
        /// <param name="valueField">键值字段名</param>
        /// <returns>返回控件自身</returns>
        public static DropDownList BindData(this DropDownList ctrl, IEnumerable<object> dataSource, string textField, string valueField)
        {
            ctrl.DataSource = dataSource;
            ctrl.DataTextField = textField;
            ctrl.DataValueField = valueField;
            ctrl.DataBind();
            return ctrl;
        }


        /// <summary>
        /// 获取控件的值
        /// </summary>
        /// <param name="ctrl">控件</param>
        /// <returns></returns>
        private static KeyValuePair<bool, string> GetControlValueInternal(Control ctrl)
        {
            var input = ctrl as HtmlInputControl;
            if (input != null)
            {
                return new KeyValuePair<bool, string>(true, input.Value);
            }

            var textArea = ctrl as HtmlTextArea;
            if (textArea != null)
            {
                return new KeyValuePair<bool, string>(true, textArea.Value);
            }

            var select = ctrl as HtmlSelect;
            if (select != null)
            {
                return new KeyValuePair<bool, string>(true, select.Value);
            }

            var textBox = ctrl as TextBox;
            if (textBox != null)
            {
                return new KeyValuePair<bool, string>(true, textBox.Text);
            }

            var dropdownList = ctrl as DropDownList;
            if (dropdownList != null)
            {
                return new KeyValuePair<bool, string>(true, dropdownList.SelectedValue);
            }
            return new KeyValuePair<bool, string>(false, null);
        }

        /// <summary>
        /// 尝试将表单上的控件值更新到模型
        /// 要求表单上控件ID和模型的属性名相同
        /// </summary>
        /// <typeparam name="T">模型类型</typeparam>
        /// <param name="form">表单</param>
        /// <param name="model">模型实例</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        [Obsolete("此方法还没有开发完成", false)]
        public static bool TryUpdateModel<T>(this HtmlForm form, T model)
        {
            if (model == null)
            {
                throw new ArgumentNullException();
            }

            var ctrls = form.Controls.Cast<Control>();
            foreach (var ctrl in ctrls)
            {
                var kv = GetControlValueInternal(ctrl);
                if (kv.Key == false)
                {
                    continue;
                }

                var fieldName = ctrl.ID;
                var property = typeof(T).GetProperty(fieldName);

                if (property != null)
                {
                    property.SetValue(model, kv.Value, null);
                }
            }

            return true;
        }
    }
}
