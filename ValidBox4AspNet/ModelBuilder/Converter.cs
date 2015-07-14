using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ValidBox4AspNet.ModelBuilder
{
    /// <summary>
    /// 提供类型和模型转换
    /// </summary>
    public static class Converter
    {
        /// <summary>
        /// 将字典值更新到模型实例
        /// </summary>
        /// <typeparam name="T">模型</typeparam>
        /// <param name="model">模型实例</param>
        /// <param name="keyValues">字典</param>
        public static void UpdateModel<T>(T model, IDictionary<string, string> keyValues)
        {
            if (model == null || keyValues == null)
            {
                return;
            }

            var properties = Property.GetProperties(typeof(T));
            foreach (var property in properties)
            {
                string value;
                if (keyValues.TryGetValue(property.Name, out value) == true)
                {
                    var targetValue = CastToType(property.PropertyType, value);
                    var currentValue = property.Get(model);

                    if (currentValue == null || targetValue.Equals(currentValue) == false)
                    {
                        property.Set(model, targetValue);
                    }
                }
            }
        }

        /// <summary>
        /// 将值转换为目标类型
        /// </summary>
        /// <param name="targetType">目标类型</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static object CastToType(Type targetType, string value)
        {
            if (targetType == typeof(string) || targetType == typeof(object))
            {
                return value;
            }

            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException();
            }

            if (targetType == typeof(Guid))
            {
                return new Guid(value);
            }

            if (targetType.IsEnum == true)
            {
                return Enum.Parse(targetType, value, true);
            }

            if (typeof(IConvertible).IsAssignableFrom(targetType) == true)
            {
                return ((IConvertible)value).ToType(targetType, null);
            }

            throw new NotSupportedException();
        }
    }
}
