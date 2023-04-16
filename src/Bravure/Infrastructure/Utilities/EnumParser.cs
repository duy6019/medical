using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Bravure.Infrastructure.Utilities
{
    public static class EnumParser
    {
        public static bool TryParseEnumValueFromDescription<T>(string description, out T convertedEnum, bool ignoreCase = true)
        {
            var stringComparison = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;

            foreach (var field in typeof(T).GetFields())
            {
                DescriptionAttribute[] attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes != null && attributes.Length > 0 && string.Equals(attributes[0].Description, description, stringComparison))
                {
                    convertedEnum = (T)Enum.Parse(typeof(T), field.Name, ignoreCase);
                    return true;
                }
            }

            var result = Enum.TryParse(typeof(T), description, ignoreCase, out var enumOutput);
            convertedEnum = result ? (T)enumOutput : default(T);
            return result;
        }
    }
}
