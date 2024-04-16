using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace VoenMedLibrary.Models
{
    public static class EnumHelper
    {
        public static string GetDescription(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                System.Reflection.FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    DescriptionAttribute attr =
                           Attribute.GetCustomAttribute(field,
                             typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }
            return null;
        }
        public static List<string> GetDescriptionsAsList(this Enum yourEnum)
        {
            List<string> descriptions = new List<string>();

            foreach (Enum enumValue in Enum.GetValues(yourEnum.GetType()))
            {
                if (yourEnum.HasFlag(enumValue))
                {
                    descriptions.Add(enumValue.GetDescription());
                }
            }

            return descriptions;
        }

        public static string GetDescriptionsAsText(this Enum value)
        {
            return string.Join(", ", value.GetDescriptionsAsList());
        }

    }
}

