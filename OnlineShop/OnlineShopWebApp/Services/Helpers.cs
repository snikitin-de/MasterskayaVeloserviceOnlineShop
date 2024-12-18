using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace OnlineShopWebApp.Services
{
    public static class Helpers
    {
        public static string GetFieldDisplayName(object value)
        {
            var field = value
                .GetType()
                .GetField(value.ToString());

            if (field != null)
            {
                var attribute = field.GetCustomAttribute<DisplayAttribute>();

                return attribute == null ? value.ToString() : attribute.Name;
            }

            return value.ToString();
        }
    }
}
