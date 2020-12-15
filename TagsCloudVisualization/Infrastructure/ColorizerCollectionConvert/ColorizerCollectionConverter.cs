using System;
using System.ComponentModel;
using System.Globalization;

namespace TagsCloudVisualization.Infrastructure.ColorizerCollectionConvert
{
    public class ColorizerCollectionConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(
            ITypeDescriptorContext context, 
            CultureInfo culture,
            object value, 
            Type destType)
            {
                if (destType == typeof(string) && value is ColorizerCollection)
                    return "Colorize algorithms...";
                return base.ConvertTo(context, culture, value, destType);
            }
    }
}