using System;
using System.Reflection;

namespace Acme.BookStore.Web.Extensions
{
    public static class ModelReflectionExtensions
    {
        public static T GetValue<T>(this object model, string propertyName)
        {
            if (model == null)
                return default;

            var prop = model.GetType().GetProperty(propertyName,
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

            if (prop == null)
                return default;

            var value = prop.GetValue(model);

            if (value == null)
                return default;

            return (T)value;
        }
    }
}
