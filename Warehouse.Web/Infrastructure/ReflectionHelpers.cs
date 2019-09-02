using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Castle.Core.Internal;

namespace Warehouse.Web.Infrastructure
{
    public static class ReflectionHelpers
    {
        public static PropertyInfo GetComplexProperty(this Type type, string propName)
        {
            PropertyInfo res = null;
            var propPaths = propName.Split(".");

            foreach (var propPath in propPaths)
            {
                if (res is null)
                {
                    // first property path
                    if (type.Name == propPath)
                    {
                        //check if base type is the same as the passed in
                        continue;
                    }
                    else
                    {
                        res = type.GetProperty(propPath);
                    }
                }
                else
                {
                    // children property paths
                    res = res.PropertyType.GetProperty(propPath);
                }
            }

            return res;
        }

        /// <summary>
        /// Gets nested property value of passed in object.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="propName"></param>
        /// <returns></returns>
        public static object GetPropertyValue(object src, string propName)
        {
            if (src == null) throw new ArgumentException("Value cannot be null.", "src");
            if (propName == null) throw new ArgumentException("Value cannot be null.", "propName");

            if (propName.Contains("."))//complex type nested
            {
                var temp         = propName.Split(new char[] { '.' }, 2);

                //remove proxy identifier if proxy
                var baseTypeName = src.GetType().Name.Replace("Proxy", string.Empty);

                // skip same base object type
                if (temp[0] == baseTypeName)
                {
                    // end property
                    if (!temp[1].Contains("."))
                    {
                        var prop = src.GetType().GetProperty(temp[1]);
                        return prop.GetValue(src, null);
                    }
                    else
                    {
                        temp = temp[1].Split(new char[] {'.'}, 2, StringSplitOptions.None);
                    }
                }

                return GetPropertyValue(GetPropertyValue(src, temp[0]), temp[1]);
            }
            else
            {
                var prop = src.GetType().GetProperty(propName);
                return prop != null ? prop.GetValue(src, null) : null;
            }
        }

        public static string GetDisplayNameValue(this PropertyInfo prop)
        {
            string result = string.Empty;

            // try get display name from display name attribute
            var displayNameAttributes = prop.GetCustomAttributes(typeof(DisplayNameAttribute), true)
                .Cast<DisplayNameAttribute>();

            if (displayNameAttributes.Any())
            {
                result = displayNameAttributes.Single().DisplayName;

                if (!string.IsNullOrEmpty(result))
                {
                    return displayNameAttributes.Single().DisplayName;
                }
            }

            // try get display name from display attribute
            var displayAttributes = prop.GetCustomAttributes(typeof(DisplayAttribute), true)
                .Cast<DisplayAttribute>();

            if (displayAttributes.Any())
            {
                result = displayAttributes.Single().Name;

                if (!string.IsNullOrEmpty(result))
                {
                    return result;
                }
            }

            // return the property name if no display name was found
            return prop.Name;
        }

        public static string GetComplexPropName<TSource>(Expression<Func<TSource, object>> prop)
        {
            if (object.Equals(prop, null))
            {
                throw new NullReferenceException("prop is required");
            }

            MemberExpression expr = null;

            if (prop.Body is MemberExpression)
            {
                expr = (MemberExpression)prop.Body;
            }
            else if (prop.Body is UnaryExpression)
            {
                expr = (MemberExpression)((UnaryExpression)prop.Body).Operand;
            }
            else
            {
                const string Format = "Expression '{0}' not supported.";
                string message = string.Format(Format, prop);

                throw new ArgumentException(message, "prop");
            }

            var baseType = prop.Type.GenericTypeArguments[0].Name;

            var firstDot = prop.ToString().IndexOf('.');
            var result = baseType + prop.ToString().Substring(firstDot);

            if (result.Contains(','))
            {
                result = result.Substring(0, result.IndexOf(','));
            }

            return result;
        }
    }
}
