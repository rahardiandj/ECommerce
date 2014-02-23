using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace ecommerce.core
{
    public static class MergeExtension
    {
        public static void Merge(this object destination, object source)
        {
            Type sourceType = source.GetType();
            Type destType = destination.GetType();

            PropertyInfo[] sourceProperties = sourceType.GetProperties();

            foreach (var sourceProperty in sourceProperties)
            {
                var destProperty = destType.GetProperty(sourceProperty.Name);

                // Merge ICollection property
                if (destProperty != null &&
                    MergeExtension.IsTypeOrInterface(destProperty, "ICollection`1") &&
                    MergeExtension.IsTypeOrInterface(sourceProperty, "ICollection`1"))
                {
                    // Get extension method for merging ICollection
                    var method = typeof(MergeExtension).GetMethod("MergeList");

                    // Get source and destination value
                    var sourceValue = sourceProperty.GetValue(source, null);
                    var destValue = destProperty.GetValue(destination, null);

                    // Invoke the extension method to merge source and destination
                    method.MakeGenericMethod(new Type[] { destProperty.PropertyType.GetGenericArguments()[0], sourceProperty.PropertyType.GetGenericArguments()[0] }).Invoke(destProperty, new[] { destValue, sourceValue });
                }
                // Merge property other than List
                else if (destProperty != null && destProperty.PropertyType == sourceProperty.PropertyType && destProperty.CanWrite)
                {
                    var sourceValue = sourceProperty.GetValue(source, null);
                    destProperty.SetValue(destination, sourceValue, null);
                }
            }

        }

        private static bool IsTypeOrInterface(PropertyInfo property, string interfaceName)
        {
            var interfaceType = property.PropertyType.GetInterface(interfaceName);
            return (property.PropertyType.IsInterface && property.PropertyType.Name == interfaceName) || (interfaceType != null && interfaceType.Name == interfaceName) && (property.PropertyType.BaseType != typeof(Array));
        }

        public static void MergeList<T, U>(this ICollection<T> destination, ICollection<U> source) where T : new()
        {
            if (destination == null)
                throw new ArgumentException("destination cannot be null");

            if (source == null)
                return;

            destination.Clear();

            var method = destination.GetType().GetMethod("Add");
            foreach (var sourceItem in source)
            {
                // Check if its a string/value type/object   
                if (IsSimpleType(typeof(U)))
                {
                    if (typeof(U) == typeof(T))
                        method.Invoke(destination, new object[] { sourceItem });
                }
                else
                {
                    T item = new T();
                    item.Merge(sourceItem);
                    method.Invoke(destination, new object[] { item });
                }
            }
        }

        public static void MergeDictionary<T, U, V>(this IDictionary<T, U> destination, IDictionary<T, V> source)
            where U : new()
            where V : new()
        {
            if (destination == null)
                throw new ArgumentException("destination cannot be null");

            if (source == null)
                return;

            destination.Clear();

            var method = destination.GetType().GetMethod("Add");
            foreach (var sourceItem in source)
            {
                // Check if its a string/value type/object   
                if (IsSimpleType(typeof(U)))
                {
                    if (typeof(U) == typeof(V))
                        method.Invoke(destination, new object[] { sourceItem.Key, sourceItem.Value });
                }
                else
                {
                    U item = new U();
                    item.Merge(sourceItem.Value);
                    method.Invoke(destination, new object[] { sourceItem.Key, item });
                }
            }

        }

        public static void AddRange<T>(this IList<T> destination, IEnumerable<T> source)
        {
            if (destination == null)
                throw new ArgumentException("destination cannot be null");

            if (source == null)
                return;

            foreach (var sourceItem in source)
                destination.Add(sourceItem);
        }

        private static bool IsSimpleType(Type type)
        {
            return type == typeof(string) || type == typeof(object) || type.IsValueType || type.IsPrimitive;
        }

    }
}
