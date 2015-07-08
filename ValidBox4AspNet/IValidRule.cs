using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidBox4AspNet
{
    /// <summary>
    /// 验证规则接口
    /// </summary>
    public interface IValidRule
    {
        /// <summary>
        /// 转换为对应的ValidBox类型
        /// </summary>
        /// <returns></returns>
        ValidBox ToValidBox();

        /// <summary>
        /// 后台验证
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool IsValid(string value);

        /// <summary>
        /// 返回格式化的错误提示语
        /// </summary>
        /// <returns></returns>
        string FormatErrorMessage();
    }
}
