using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace MvcPlayground.Helpers
{
    public class PascalCaseWordSplittingEnumConverter : EnumConverter
    {
        public PascalCaseWordSplittingEnumConverter(Type type)
            : base(type)
        {
        }
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                string stringValue = (string)base.ConvertTo(context, culture, value, destinationType);
                stringValue = SplitString(stringValue);
                return stringValue;
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public string SplitString(string stringValue)
        {
            return Regex.Replace(stringValue, @"((?<=[a-z])[A-Z]\w|(?<=\w)[A-Z][a-z])", @" $0");
        }

        
    }
}