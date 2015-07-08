using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ValidBox4AspNet;
using ValidBox4AspNet.ValidRules;

namespace System.Web
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
        /// <returns></returns>
        public static ValidBox Required(this ValidBox box)
        {
            var newBox = new RequiredRule().ToValidBox();
            return ValidBox.Merge(box, newBox);
        }

        /// <summary>
        /// 验证必须输入
        /// </summary>
        /// <param name="box">验证框</param>
        /// <param name="errorMessage">提示信息</param>        
        /// <returns></returns>
        public static ValidBox Required(this ValidBox box, string errorMessage)
        {
            var newBox = new RequiredRule { ErrorMessage = errorMessage }.ToValidBox();
            return ValidBox.Merge(box, newBox);
        }

        /// <summary>      
        /// 验证输入是URL
        /// </summary>
        /// <param name="box">验证框</param>              
        /// <returns></returns>
        public static ValidBox Url(this ValidBox box)
        {
            var newBox = new UrlRule().ToValidBox();
            return ValidBox.Merge(box, newBox);
        }

        /// <summary>      
        /// 验证输入是URL
        /// </summary>
        /// <param name="box">验证框</param>        
        /// <param name="errorMessage">提示信息</param>
        /// <returns></returns>
        public static ValidBox Url(this ValidBox box, string errorMessage)
        {
            var newBox = new UrlRule { ErrorMessage = errorMessage }.ToValidBox();
            return ValidBox.Merge(box, newBox);
        }

        /// <summary>
        /// 验证输入是否是Email
        /// </summary>
        /// <param name="box">验证框</param>    
        /// <returns></returns>
        public static ValidBox Email(this ValidBox box)
        {
            var newBox = new EmailRule().ToValidBox();
            return ValidBox.Merge(box, newBox);
        }

        /// <summary>
        /// 验证输入是否是Email
        /// </summary>
        /// <param name="box">验证框</param>      
        /// <param name="errorMessage">提示信息</param>
        /// <returns></returns>
        public static ValidBox Email(this ValidBox box, string errorMessage)
        {
            var newBox = new EmailRule { ErrorMessage = errorMessage }.ToValidBox();
            return ValidBox.Merge(box, newBox);
        }

        /// <summary>
        /// 验证输入的长度
        /// </summary>
        /// <param name="box">验证框</param>       
        /// <param name="minLength">最小长度</param>
        /// <param name="maxLength">最大长度</param>       
        /// <returns></returns>
        public static ValidBox Length(this ValidBox box, int minLength, int maxLength)
        {
            var newBox = new LengthRule(minLength, maxLength).ToValidBox();
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
        public static ValidBox Length(this ValidBox box, int minLength, int maxLength, string errorMessage)
        {
            var newBox = new LengthRule(minLength, maxLength) { ErrorMessage = errorMessage }.ToValidBox();
            return ValidBox.Merge(box, newBox);
        }

        /// <summary>
        /// 验证输入的最小长度
        /// </summary>
        /// <param name="box">验证框</param>       
        /// <param name="length">最小长度</param>      
        /// <returns></returns>
        public static ValidBox MinLength(this ValidBox box, int length)
        {
            var newBox = new MinLengthRule(length).ToValidBox();
            return ValidBox.Merge(box, newBox);
        }

        /// <summary>
        /// 验证输入的最小长度
        /// </summary>
        /// <param name="box">验证框</param>       
        /// <param name="length">最小长度</param>
        /// <param name="errorMessage">提示信息</param>
        /// <returns></returns>
        public static ValidBox MinLength(this ValidBox box, int length, string errorMessage)
        {
            var newBox = new MinLengthRule(length) { ErrorMessage = errorMessage }.ToValidBox();
            return ValidBox.Merge(box, newBox);
        }

        /// <summary>
        /// 验证输入的最大长度
        /// </summary>
        /// <param name="box">验证框</param>       
        /// <param name="length">最大长度</param>     
        /// <returns></returns>
        public static ValidBox MaxLength(this ValidBox box, int length)
        {
            var newBox = new MaxLengthRule(length).ToValidBox();
            return ValidBox.Merge(box, newBox);
        }

        /// <summary>
        /// 验证输入的最大长度
        /// </summary>
        /// <param name="box">验证框</param>       
        /// <param name="length">最大长度</param>
        /// <param name="errorMessage">提示信息</param>
        /// <returns></returns>
        public static ValidBox MaxLength(this ValidBox box, int length, string errorMessage)
        {
            var newBox = new MaxLengthRule(length) { ErrorMessage = errorMessage }.ToValidBox();
            return ValidBox.Merge(box, newBox);
        }

        /// <summary>
        /// 验证输入的值的范围
        /// </summary>
        /// <param name="box">验证框</param>       
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>       
        /// <returns></returns>
        public static ValidBox Range(this ValidBox box, double minValue, double maxValue)
        {
            var newBox = new RangeRule(minValue, maxValue).ToValidBox();
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
        public static ValidBox Range(this ValidBox box, double minValue, double maxValue, string errorMessage)
        {
            var newBox = new RangeRule(minValue, maxValue) { ErrorMessage = errorMessage }.ToValidBox();
            return ValidBox.Merge(box, newBox);
        }

        /// <summary>
        /// 验证输入的值的范围
        /// </summary>
        /// <param name="box">验证框</param>       
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>       
        /// <returns></returns>
        public static ValidBox Range(this ValidBox box, int minValue, int maxValue)
        {
            var newBox = new RangeRule(minValue, maxValue).ToValidBox();
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
        public static ValidBox Range(this ValidBox box, int minValue, int maxValue, string errorMessage)
        {
            var newBox = new RangeRule(minValue, maxValue) { ErrorMessage = errorMessage }.ToValidBox();
            return ValidBox.Merge(box, newBox);
        }


        /// <summary>
        /// 验证精度（小数点数）
        /// </summary>
        /// <param name="box">验证框</param>
        /// <param name="min">最小精度</param>
        /// <param name="max">最大精度</param>     
        /// <returns></returns>
        public static ValidBox Precision(this ValidBox box, int min, int max)
        {
            var newBox = new PrecisionRule(min, max).ToValidBox();
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
        public static ValidBox Precision(this ValidBox box, int min, int max, string errorMessage)
        {
            var newBox = new PrecisionRule(min, max) { ErrorMessage = errorMessage }.ToValidBox();
            return ValidBox.Merge(box, newBox);
        }

        /// <summary>
        /// 验证和正则表达式是否匹配
        /// </summary>
        /// <param name="box">验证框</param>        
        /// <param name="regexPattern">表达式</param>    
        /// <returns></returns>
        public static ValidBox Match(this ValidBox box, string regexPattern)
        {
            var newBox = new MatchRule(regexPattern).ToValidBox();
            return ValidBox.Merge(box, newBox);
        }

        /// <summary>
        /// 验证和正则表达式是否匹配
        /// </summary>
        /// <param name="box">验证框</param>        
        /// <param name="regexPattern">表达式</param>
        /// <param name="errorMessage">提示信息</param>
        /// <returns></returns>
        public static ValidBox Match(this ValidBox box, string regexPattern, string errorMessage)
        {
            var newBox = new MatchRule(regexPattern) { ErrorMessage = errorMessage }.ToValidBox();
            return ValidBox.Merge(box, newBox);
        }


        /// <summary>
        /// 验证输入和正则表达式不匹配
        /// </summary>
        /// <param name="box">验证框</param>
        /// <param name="regexPattern">表达式</param>     
        /// <returns></returns>
        public static ValidBox NotMatch(this ValidBox box, string regexPattern)
        {
            var newBox = new NotMatchRule(regexPattern).ToValidBox();
            return ValidBox.Merge(box, newBox);
        }


        /// <summary>
        /// 验证输入和正则表达式不匹配
        /// </summary>
        /// <param name="box">验证框</param>
        /// <param name="regexPattern">表达式</param>
        /// <param name="errorMessage">提示信息</param>
        /// <returns></returns>
        public static ValidBox NotMatch(this ValidBox box, string regexPattern, string errorMessage)
        {
            var newBox = new NotMatchRule(regexPattern) { ErrorMessage = errorMessage }.ToValidBox();
            return ValidBox.Merge(box, newBox);
        }

        /// <summary>
        /// 验证输入是否和目标ID元素的值相等
        /// </summary>
        /// <param name="box">验证框</param>      
        /// <param name="targetID">目标元素ID</param>    
        /// <returns></returns>
        public static ValidBox EqualTo(this ValidBox box, string targetID)
        {
            var newBox = new EqualToRule(targetID).ToValidBox();
            return ValidBox.Merge(box, newBox);
        }

        /// <summary>
        /// 验证输入是否和目标ID元素的值相等
        /// </summary>
        /// <param name="box">验证框</param>      
        /// <param name="targetID">目标元素ID</param>
        /// <param name="errorMessage">提示信息</param>
        /// <returns></returns>
        public static ValidBox EqualTo(this ValidBox box, string targetID, string errorMessage)
        {
            var newBox = new EqualToRule(targetID) { ErrorMessage = errorMessage }.ToValidBox();
            return ValidBox.Merge(box, newBox);
        }

        /// <summary>
        /// 验证输入是否和目标ID元素的值不相等
        /// </summary>
        /// <param name="box">验证框</param>
        /// <param name="targetID">目标元素ID</param>    
        /// <returns></returns>
        public static ValidBox NotEqualTo(this ValidBox box, string targetID)
        {
            var newBox = new NotEqualToRule(targetID).ToValidBox();
            return ValidBox.Merge(box, newBox);
        }

        /// <summary>
        /// 验证输入是否和目标ID元素的值不相等
        /// </summary>
        /// <param name="box">验证框</param>
        /// <param name="targetID">目标元素ID</param>
        /// <param name="errorMessage">提示信息</param>
        /// <returns></returns>
        public static ValidBox NotEqualTo(this ValidBox box, string targetID, string errorMessage)
        {
            var newBox = new NotEqualToRule(targetID) { ErrorMessage = errorMessage }.ToValidBox();
            return ValidBox.Merge(box, newBox);
        }


        /// <summary>
        /// 远程验证输入值
        /// 不支持后台验证功能
        /// </summary>
        /// <param name="box">验证框</param>
        /// <param name="url">远程地址</param>
        /// <param name="targetID">提交的目标元素的ID</param>
        /// <returns></returns>
        public static ValidBox Remote(this ValidBox box, string url, params string[] targetID)
        {
            var newBox = new RemoteRule(url, targetID).ToValidBox();
            return ValidBox.Merge(box, newBox);
        }

        /// <summary>
        /// 验证输入是否为数字类型
        /// </summary>
        /// <param name="box">验证框</param>       
        /// <returns></returns>
        public static ValidBox Number(this ValidBox box)
        {
            var newBox = new NumberRule().ToValidBox();
            return ValidBox.Merge(box, newBox);
        }

        /// <summary>
        /// 验证输入是否为数字类型
        /// </summary>
        /// <param name="box">验证框</param>
        /// <param name="errorMessage">错误提示消息</param>
        /// <returns></returns>
        public static ValidBox Number(this ValidBox box, string errorMessage)
        {
            var newBox = new NumberRule() { ErrorMessage = errorMessage }.ToValidBox();
            return ValidBox.Merge(box, newBox);
        }
    }
}
