using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ValidBox4Mvc;
using ValidBox4Mvc.ValidRules;

namespace System.Web.Mvc.Html
{
    /// <summary>
    /// 验证框扩展 
    /// </summary>
    public static partial class ValidBoxExtend
    {
        /// <summary>
        /// 验证必须输入
        /// </summary>
        /// <param name="box">验证框</param>
        /// <param name="errorMessage">提示信息</param>        
        /// <returns></returns>
        public static ValidBox Required(this ValidBox box, string errorMessage = null)
        {
            var newBox = new RequiredAttribute { ErrorMessage = errorMessage }.ToValidBox();
            return ValidBox.Merge(box, newBox);
        }

        /// <summary>      
        /// 验证输入是URL
        /// </summary>
        /// <param name="box">验证框</param>        
        /// <param name="errorMessage">提示信息</param>
        /// <returns></returns>
        public static ValidBox Url(this ValidBox box, string errorMessage = null)
        {
            var newBox = new UrlAttribute { ErrorMessage = errorMessage }.ToValidBox();
            return ValidBox.Merge(box, newBox);
        }

        /// <summary>
        /// 验证输入是否是Email
        /// </summary>
        /// <param name="box">验证框</param>      
        /// <param name="errorMessage">提示信息</param>
        /// <returns></returns>
        public static ValidBox Email(this ValidBox box, string errorMessage = null)
        {
            var newBox = new EmailAttribute { ErrorMessage = errorMessage }.ToValidBox();
            return ValidBox.Merge(box, newBox);
        }

        /// <summary>
        /// 验证输入的长度
        /// </summary>
        /// <param name="box">验证框</param>       
        /// <param name="minLength">最小长度</param>
        /// <param name="maxLength">最大长度</param>
        /// <param name="errorMessage">提示信息</param>
        /// <returns></returns>
        public static ValidBox Length(this ValidBox box, int minLength, int maxLength, string errorMessage = null)
        {
            var newBox = new LengthAttribute(minLength, maxLength) { ErrorMessage = errorMessage }.ToValidBox();
            return ValidBox.Merge(box, newBox);
        }

        /// <summary>
        /// 验证输入的最小长度
        /// </summary>
        /// <param name="box">验证框</param>       
        /// <param name="length">最小长度</param>
        /// <param name="errorMessage">提示信息</param>
        /// <returns></returns>
        public static ValidBox MinLength(this ValidBox box, int length, string errorMessage = null)
        {
            var newBox = new MinLengthAttribute(length) { ErrorMessage = errorMessage }.ToValidBox();
            return ValidBox.Merge(box, newBox);
        }

        /// <summary>
        /// 验证输入的最大长度
        /// </summary>
        /// <param name="box">验证框</param>       
        /// <param name="length">最大长度</param>
        /// <param name="errorMessage">提示信息</param>
        /// <returns></returns>
        public static ValidBox MaxLength(this ValidBox box, int length, string errorMessage = null)
        {
            var newBox = new MaxLengthAttribute(length) { ErrorMessage = errorMessage }.ToValidBox();
            return ValidBox.Merge(box, newBox);
        }

        /// <summary>
        /// 验证输入的值的范围
        /// </summary>
        /// <param name="box">验证框</param>       
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        /// <param name="errorMessage">提示信息</param>
        /// <returns></returns>
        public static ValidBox Range(this ValidBox box, double minValue, double maxValue, string errorMessage = null)
        {
            var newBox = new RangeAttribute(minValue, maxValue) { ErrorMessage = errorMessage }.ToValidBox();
            return ValidBox.Merge(box, newBox);
        }

        /// <summary>
        /// 验证输入的值的范围
        /// </summary>
        /// <param name="box">验证框</param>       
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        /// <param name="errorMessage">提示信息</param>
        /// <returns></returns>
        public static ValidBox Range(this ValidBox box, int minValue, int maxValue, string errorMessage = null)
        {
            var newBox = new RangeAttribute(minValue, maxValue) { ErrorMessage = errorMessage }.ToValidBox();
            return ValidBox.Merge(box, newBox);
        }

        /// <summary>
        /// 验证精度（小数点数）
        /// </summary>
        /// <param name="box">验证框</param>
        /// <param name="min">最小精度</param>
        /// <param name="max">最大精度</param>
        /// <param name="errorMessage">提示信息</param>
        /// <returns></returns>
        public static ValidBox Precision(this ValidBox box, int min, int max, string errorMessage = null)
        {
            var newBox = new PrecisionAttribute(min, max) { ErrorMessage = errorMessage }.ToValidBox();
            return ValidBox.Merge(box, newBox);
        }

        /// <summary>
        /// 验证和正则表达式是否匹配
        /// </summary>
        /// <param name="box">验证框</param>        
        /// <param name="regexPattern">表达式</param>
        /// <param name="errorMessage">提示信息</param>
        /// <returns></returns>
        public static ValidBox Match(this ValidBox box, string regexPattern, string errorMessage = null)
        {
            var newBox = new MatchAttribute(regexPattern) { ErrorMessage = errorMessage }.ToValidBox();
            return ValidBox.Merge(box, newBox);
        }

        /// <summary>
        /// 验证输入和正则表达式不匹配
        /// </summary>
        /// <param name="box">验证框</param>
        /// <param name="regexPattern">表达式</param>
        /// <param name="errorMessage">提示信息</param>
        /// <returns></returns>
        public static ValidBox NotMatch(this ValidBox box, string regexPattern, string errorMessage = null)
        {
            var newBox = new NotMatchAttribute(regexPattern) { ErrorMessage = errorMessage }.ToValidBox();
            return ValidBox.Merge(box, newBox);
        }

        /// <summary>
        /// 验证输入是否和目标ID元素的值相等
        /// </summary>
        /// <param name="box">验证框</param>      
        /// <param name="targetID">目标元素ID</param>
        /// <param name="errorMessage">提示信息</param>
        /// <returns></returns>
        public static ValidBox EqualTo(this ValidBox box, string targetID, string errorMessage = null)
        {
            var newBox = new EqualToAttribute(targetID) { ErrorMessage = errorMessage }.ToValidBox();
            return ValidBox.Merge(box, newBox);
        }


        /// <summary>
        /// 验证输入是否和目标ID元素的值不相等
        /// </summary>
        /// <param name="box">验证框</param>
        /// <param name="targetID">目标元素ID</param>
        /// <param name="errorMessage">提示信息</param>
        /// <returns></returns>
        public static ValidBox NotEqualTo(this ValidBox box, string targetID, string errorMessage = null)
        {
            var newBox = new NotEqualToAttribute(targetID) { ErrorMessage = errorMessage }.ToValidBox();
            return ValidBox.Merge(box, newBox);
        }


        /// <summary>
        /// 远程验证输入值
        /// </summary>
        /// <param name="box">提示信息</param>
        /// <param name="url">远程地址</param>
        /// <param name="targetID">提交的目标元素的ID</param>
        /// <returns></returns>
        public static ValidBox Remote(this ValidBox box, string url, params string[] targetID)
        {
            var newBox = new ValidBox4Mvc.ValidRules.RemoteAttribute(url, targetID).ToValidBox();
            return ValidBox.Merge(box, newBox);
        }
    }
}
