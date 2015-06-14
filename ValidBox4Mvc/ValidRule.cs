using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidBox4Mvc
{
    /// <summary>
    /// 表示验证规则
    /// </summary>
    internal class ValidRule
    { 
        /// <summary>
        /// 规则名
        /// </summary>
        public string r { get; set; }
        /// <summary>
        /// 参数
        /// </summary>
        public object[] p { get; set; }
        /// <summary>
        /// 提示消息
        /// </summary>
        public string m { get; set; }
    }
}
