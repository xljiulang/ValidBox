using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Web;
using System.Web.Mvc;

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
        /// 获取字段是否是必须输入的
        /// </summary>
        private KeyValuePair<bool, string> _required;

        /// <summary>
        /// 获取验证失败时的提示语       
        /// </summary>
        private List<string> _messageList;

        /// <summary>
        /// 获取验证类型
        /// </summary>
        private List<string> _validtypeList;

        /// <summary>
        /// 验证框
        /// </summary>
        private ValidBox()
        {
            this._messageList = new List<string>();
            this._validtypeList = new List<string>();
        }

        /// <summary>
        /// 表示必须输入的验证框对象
        /// </summary>
        /// <param name="message">未输入时的提示信息</param>
        public ValidBox(string message)
            : this()
        {
            this._required = new KeyValuePair<bool, string>(true, message);
        }

        /// <summary>
        /// 表示一般验证框对象
        /// <param name="validType">验证方法和参数</param>
        /// <param name="mesage">不通过时提示信息</param>
        /// </summary>
        public ValidBox(string validType, string mesage)
            : this()
        {
            this._messageList.Add(mesage);
            this._validtypeList.Add(validType);
        }

        /// <summary>
        /// 转换为Html属性对象
        /// </summary>
        /// <returns></returns>
        public object AsHtmlAttribute()
        {
            if (this._validtypeList.Count > 0)
            {
                if (this._required.Key)
                {
                    return new
                    {
                        @class = "validBox",
                        required = "required",
                        required_message = this._required.Value,
                        message = ValidBox.MakeJsArray(this._messageList.ToArray()),
                        validtype = string.Join(";", this._validtypeList)
                    };
                }
                else
                {
                    return new
                    {
                        @class = "validBox",
                        message = ValidBox.MakeJsArray(this._messageList.ToArray()),
                        validtype = string.Join(";", this._validtypeList)
                    };
                }
            }

            if (this._required.Key)
            {
                return new
                {
                    @class = "validBox",
                    required = "required",
                    required_message = this._required.Value
                };
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 将html属性合并到字典中
        /// </summary>
        /// <param name="dic">字典</param>
        /// <param name="attribute">属性</param>
        private void MergenAttribute(ref Dictionary<string, object> dic, object attribute)
        {
            if (attribute == null)
            {
                return;
            }

            var properties = attribute.GetType().GetProperties();
            foreach (var p in properties)
            {
                var key = p.Name.Replace("_", "-").ToLower();
                var value = p.GetValue(attribute, null);

                if (dic.ContainsKey(key))
                {
                    if (key == "class")
                    {
                        dic[key] = string.Format("{0} {1}", dic[key], value).Trim();
                    }
                }
                else
                {
                    dic.Add(key, value);
                }
            }
        }


        /// <summary>
        /// 转换为Html属性对象
        /// <param name="attribute">附加的html属性</param>
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, object> AsHtmlAttribute(object attribute)
        {
            var dic = new Dictionary<string, object>();
            this.MergenAttribute(ref dic, this.AsHtmlAttribute());
            this.MergenAttribute(ref dic, attribute);
            return dic;
        }

        /// <summary>
        /// 生成JavaScript数组的字符串表达方式
        /// </summary>       
        /// <param name="param">参数</param>
        /// <returns></returns>
        public static string MakeJsArray(params object[] param)
        {
            if (param == null) param = new object[0];

            Func<object, string> selector = (item) =>
            {
                if (item == null || item.GetType() == typeof(string))
                {
                    return string.Format("'{0}'", item);
                }
                return item.ToString();
            };

            return "[" + string.Join(",", param.Select(selector)) + "]";
        }

        /// <summary>
        /// 验证框与运算
        /// </summary>
        /// <param name="left">左验证框</param>
        /// <param name="right">右验证框</param>
        /// <returns></returns>
        public static ValidBox operator &(ValidBox left, ValidBox right)
        {
            var newBox = new ValidBox();
            newBox._messageList.AddRange(left._messageList);
            newBox._messageList.AddRange(right._messageList);

            newBox._validtypeList.AddRange(left._validtypeList);
            newBox._validtypeList.AddRange(right._validtypeList);

            newBox._required = right._required.Key ? right._required : left._required;
            return newBox;
        }

        /// <summary>
        /// 表示生成无任何意义的空验证框
        /// </summary>
        /// <returns></returns>
        public static ValidBox Empty()
        {
            return new ValidBox();
        }
    }
}