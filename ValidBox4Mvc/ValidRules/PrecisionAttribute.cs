using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidBox4Mvc.ValidRules
{
    /// <summary>
    /// 表示精度验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class PrecisionAttribute : ValidRuleBase
    {
        /// <summary>
        /// 获取或设置最小精度
        /// </summary>
        public int Min { get; set; }
        /// <summary>
        /// 获取或设置最大精度
        /// </summary>
        public int Max { get; set; }

        /// <summary>
        /// 表示精度验证
        /// </summary>
        public PrecisionAttribute(int min, int max)
        {
            this.Min = min;
            this.Max = max;
            this.ErrorMessage = "精度为{0}到{1}位小数";
        }
        /// <summary>
        /// 转换为对应的ValidBox类型
        /// </summary>
        /// <returns></returns>
        public override ValidBox ToValidBox()
        {
            return ValidBox.New(this.ValidType, this.ErrorMessage, this.Min, this.Max);
        }

        /// <summary>
        /// 后台验证
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            var values = value.ToString().Split('.');
            if (this.Max > 0 && values.Length > 0)
            {
                return values.Last().Length <= Max;
            }
            return true;
        }

        /// <summary>
        /// 获取错误提示信息
        /// </summary>     
        /// <returns></returns>
        public override string FormatErrorMessage(string name)
        {
            return string.Format(this.ErrorMessage, this.Min, this.Max);
        }
    }
}
