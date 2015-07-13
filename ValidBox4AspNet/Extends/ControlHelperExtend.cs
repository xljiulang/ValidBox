using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ValidBox4AspNet.ModelBuilder;

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
        /// <param name="valueField">键字段名</param>
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
        /// 获取表达式对应的属性
        /// </summary>
        /// <typeparam name="T">Model类型</typeparam>
        /// <typeparam name="TKey">键</typeparam>      
        /// <param name="keySelector">属性选择表达式</param>
        /// <returns></returns>
        private static PropertyInfo GetExpressionProperty<T, TKey>(Expression<Func<T, TKey>> keySelector)
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
            return property;
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TText"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="ctrl">控件</param>
        /// <param name="dataSource">数据源</param>
        /// <param name="textField">文本字段</param>
        /// <param name="valueField">键字段</param>
        /// <returns></returns>
        public static DropDownList BindData<T, TText, TValue>(this DropDownList ctrl, IEnumerable<T> dataSource, Expression<Func<T, TText>> textField, Expression<Func<T, TValue>> valueField)
        {
            var textFieldName = GetExpressionProperty(textField).Name;
            var valueFieldName = GetExpressionProperty(valueField).Name;
            return ctrl.BindData(dataSource.Cast<object>(), textFieldName, valueFieldName);
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

            var hidden = ctrl as HiddenField;
            if (hidden != null)
            {
                return new KeyValuePair<bool, string>(true, hidden.Value);
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
        /// 将表单上的控件值更新到模型
        /// </summary>
        /// <typeparam name="T">模型类型</typeparam>
        /// <param name="form">表单</param>
        /// <param name="model">模型实例</param>          
        /// <returns></returns>
        public static bool TryUpdateModel<T>(this HtmlForm form, T model)
        {
            string error;
            return form.TryUpdateModel<T>(model, out error);
        }

        /// <summary>
        /// 将表单上的控件值更新到模型
        /// </summary>
        /// <typeparam name="T">模型类型</typeparam>
        /// <param name="form">表单</param>
        /// <param name="model">模型实例</param>
        /// <param name="error">错误消息</param>         
        /// <returns></returns>
        public static bool TryUpdateModel<T>(this HtmlForm form, T model, out string error)
        {
            try
            {
                form.UpdateModel<T>(model);
                error = null;
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 将表单上的控件值更新到模型     
        /// </summary>
        /// <typeparam name="T">模型类型</typeparam>
        /// <param name="form">表单</param>
        /// <param name="model">模型实例</param>       
        /// <exception cref="ArgumentNullException"></exception>            
        public static void UpdateModel<T>(this HtmlForm form, T model)
        {
            if (model == null)
            {
                throw new ArgumentNullException();
            }

            var keyValues = form.Controls
                .Cast<Control>()
                .Select(item => new { item.ID, Value = GetControlValueInternal(item) })
                .Where(item => item.Value.Key)
                .ToDictionary(k => k.ID, v => v.Value.Value, StringComparer.OrdinalIgnoreCase);

            Converter.UpdateModel<T>(model, keyValues);
        }
    }
}
