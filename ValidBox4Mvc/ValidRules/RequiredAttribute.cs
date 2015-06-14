using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVC = System.ComponentModel.DataAnnotations;

namespace ValidBox4Mvc.ValidRules
{
    /// <summary>
    /// 表示要求必须输入
    /// 此特性影响EF-CodeFirst生成的数据库字段为非空约束
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class RequiredAttribute : MVC.RequiredAttribute, IValidRule
    {
        /// <summary>
        /// 排序索引
        /// </summary>
        public int OrderIndex { get; set; }

        /// <summary>
        /// 要求必须输入
        /// </summary>
        public RequiredAttribute()
        {
            this.OrderIndex = -1;
            this.ErrorMessage = "该项为必填项";
        }

        /// <summary>
        /// 生成验证框对象
        /// </summary>
        /// <returns></returns>
        public ValidBox ToValidBox()
        {
            return new ValidBox(this.ErrorMessage);
        }
    }
}
