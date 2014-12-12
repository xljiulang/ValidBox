using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidBox4Mvc
{
    /// <summary>
    /// 验证规则接口
    /// </summary>
    public interface IValidRule
    {
        /// <summary>
        /// 排序索引
        /// 越小越优先
        /// </summary>
        int OrderIndex { get; set; }

        /// <summary>
        /// 转换为对应的ValidBox类型
        /// </summary>
        /// <returns></returns>
        ValidBox ToValidBox();
    }
}
