using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidBox4AspNet.ValidRules
{
    /// <summary>
    /// 表示验值要在一定的区间中
    /// 支持整型和双精度型验证
    /// </summary>    
    public class RangeRule : ValidRuleBase
    {
        /// <summary>
        /// 是否为整数
        /// </summary>
        private bool isInteger;

        /// <summary>
        /// 获取或设置最小值
        /// </summary>
        protected double MinValue { get; set; }

        /// <summary>
        /// 获取或设置最大值
        /// </summary>
        protected double MaxValue { get; set; }

        /// <summary>
        /// 获取验证类型
        /// </summary>
        protected override string ValidType
        {
            get
            {
                return this.isInteger ? "rangeInt" : "range";
            }
        }

        /// <summary>
        /// 验值要在一定的区间中
        /// </summary>
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        public RangeRule(int minValue, int maxValue)
        {
            this.isInteger = true;
            this.MinValue = minValue;
            this.MaxValue = maxValue;
            this.ErrorMessage = "值要在区间[{0},{1}]内的整数";
        }

        /// <summary>
        /// 验值要在一定的区间中
        /// </summary>
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        public RangeRule(double minValue, double maxValue)
        {
            this.MinValue = minValue;
            this.MaxValue = maxValue;
            this.ErrorMessage = "值要在区间[{0},{1}]内的数";
        }

        /// <summary>
        /// 生成验证框对象
        /// </summary>
        /// <returns></returns>
        public override ValidBox ToValidBox()
        {
            return ValidBox.New(this, this.ValidType, this.ErrorMessage, this.MinValue, this.MaxValue);
        }

        /// <summary>
        /// 后台验证
        /// </summary>       
        /// <param name="value">属性值</param>
        /// <returns></returns>
        public override bool IsValid(string value)
        {
            var number = 0d;
            if (double.TryParse(value, out number))
            {
                return number >= this.MinValue && number <= this.MaxValue;
            }
            return false;
        }

        /// <summary>
        /// 返回格式化错误提示语
        /// </summary>
        /// <returns></returns>
        public override string FormatErrorMessage()
        {
            return string.Format(this.ErrorMessage, this.MinValue, this.MaxValue);
        }
    }
}
