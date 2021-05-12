﻿using System.ComponentModel;
using System.Text.RegularExpressions;


namespace Frotcom.Challenge.SendTrackingDataWorker.Helpers
{

    public static class EnumHelper
    {
        public static string GetDescription(System.Enum enumValue)
        {
            if (enumValue != null)
            {
                //Look for DescriptionAttributes on the enum field
                object[] attr = enumValue.GetType().GetField(enumValue.ToString())
                    .GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attr.Length > 0) // a DescriptionAttribute exists; use it
                    return ((DescriptionAttribute)attr[0]).Description;

                //The above code is all you need if you always use DescriptionAttributes;
                //If you don't, the below code will semi-intelligently
                //"humanize" an UpperCamelCased Enum identifier
                string result = enumValue.ToString();

                //"FooBar" -> "Foo Bar"
                result = Regex.Replace(result, @"([a-z])([A-Z])", "$1 $2");

                //"Foo123" -> "Foo 123"
                result = Regex.Replace(result, @"([A-Za-z])([0-9])", "$1 $2");

                //"123Foo" -> "123 Foo"
                result = Regex.Replace(result, @"([0-9])([A-Za-z])", "$1 $2");

                //"FOOBar" -> "FOO Bar"
                result = Regex.Replace(result, @"(?<!^)(?<! )([A-Z][a-z])", " $1");

                return result;
            }

            return string.Empty;

        }
    }
}
