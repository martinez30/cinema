using System;
using System.ComponentModel;

namespace Infra
{
    public static class Extensions
    {
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attributes = field?.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes?.Length > 0 ? ((DescriptionAttribute)attributes[0]).Description : value.ToString() ?? value.ToString();
        }
    }
}
