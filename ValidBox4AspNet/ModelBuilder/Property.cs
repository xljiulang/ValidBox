using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ValidBox4AspNet.ModelBuilder
{
    /// <summary>
    /// 表示属性调用优化    
    /// </summary>   
    public class Property
    {
        /// <summary>
        /// 同步锁
        /// </summary>
        private static readonly object syncRoot = new object();

        /// <summary>
        /// 缓存
        /// </summary>
        private static Dictionary<Type, Property[]> cached = new Dictionary<Type, Property[]>();

        /// <summary>
        /// 获取类型的属性
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="propeties">属性获取委托</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public static Property[] GetOrAddProperties(Type type, Func<Type, IEnumerable<PropertyInfo>> propeties)
        {
            if (type == null)
            {
                throw new ArgumentNullException();
            }

            lock (syncRoot)
            {
                if (cached.ContainsKey(type) == false)
                {
                    return cached[type] = propeties(type).Select(item => new Property(item)).ToArray();
                }
                return cached[type];
            }
        }

        /// <summary>
        /// 属性的Get方法
        /// </summary>
        private Func<object, object[], object> getter;

        /// <summary>
        /// 属性的Set方法
        /// </summary>
        private Func<object, object[], object> setter;

        /// <summary>
        /// 获取属性名
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 获取属性类型
        /// </summary>
        public Type PropertyType { get; private set; }

        /// <summary>
        /// 属性
        /// </summary>
        /// <param name="property">属性</param>
        /// <exception cref="ArgumentException"></exception>
        public Property(PropertyInfo property)
        {
            if (property == null)
            {
                throw new ArgumentNullException();
            }

            this.getter = CreateMethodInvoker(property.GetGetMethod());
            this.setter = CreateMethodInvoker(property.GetSetMethod());
            this.Name = property.Name;
            this.PropertyType = property.PropertyType;
        }

        /// <summary>
        /// 获取属性的值
        /// </summary>
        /// <param name="instance">服务实例</param>        
        /// <returns></returns>
        public object Get(object instance)
        {
            return this.getter.Invoke(instance, null);
        }

        /// <summary>
        /// 设置属性的值
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="value"></param>
        public void Set(object instance, object value)
        {
            this.setter.Invoke(instance, new[] { value });
        }

        /// <summary>
        /// 生成方法的委托
        /// </summary>
        /// <param name="method">方法成员信息</param>
        /// <exception cref="ArgumentException"></exception>
        /// <returns></returns>
        private static Func<object, object[], object> CreateMethodInvoker(MethodInfo method)
        {
            var instance = Expression.Parameter(typeof(object), "instance");
            var parameters = Expression.Parameter(typeof(object[]), "parameters");

            var instanceCast = method.IsStatic ? null : Expression.Convert(instance, method.ReflectedType);
            var parametersCast = method.GetParameters().Select((p, i) =>
            {
                var parameter = Expression.ArrayIndex(parameters, Expression.Constant(i));
                return Expression.Convert(parameter, p.ParameterType);
            }).ToArray();

            var body = Expression.Call(instanceCast, method, parametersCast);

            if (method.ReturnType == typeof(void))
            {
                var action = Expression.Lambda<Action<object, object[]>>(body, instance, parameters).Compile();
                return (_instance, _parameters) =>
                {
                    action.Invoke(_instance, _parameters);
                    return null;
                };
            }
            else
            {
                var bodyCast = Expression.Convert(body, typeof(object));
                return Expression.Lambda<Func<object, object[], object>>(bodyCast, instance, parameters).Compile();
            }
        }

        /// <summary>
        /// 字符串显示
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Name;
        }
    }
}
