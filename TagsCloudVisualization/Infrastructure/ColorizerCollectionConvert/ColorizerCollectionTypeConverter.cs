using System;
using System.ComponentModel;
using System.Globalization;
using TagsCloudCreating.Configuration;
using TagsCloudCreating.Contracts;
using TagsCloudCreating.Core.ColorizeAlgorithms;
using TagsCloudVisualization.Infrastructure.Common;

namespace TagsCloudVisualization.Infrastructure.ColorizerCollectionConvert
{
    public class ColorizerCollectionTypeConverter : ExpandableObjectConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => true;

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context) => true;

        private static ColorizerCollection GetCollection(ITypeDescriptorContext context)
        {
            var collection = context.Instance.GetType().Name switch
            {
                nameof(TagsSettings) => ((TagsSettingsSelector) context.Instance).ColorizerCollection,
                _ => ((TagsSettingsSelector) context.Instance).ColorizerCollection
            };

            return collection;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            var standardValuesCollection = new StandardValuesCollection(GetCollection(context));
            return standardValuesCollection;
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) =>
            destinationType == typeof(string);

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) =>
            sourceType == typeof(string);

        public override object ConvertTo(
            ITypeDescriptorContext context,
            CultureInfo culture,
            object value,
            Type destinationType)
        {
            if (destinationType == typeof(string) && value is IColorizer item)
                return item.Name;
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override object ConvertFrom(
            ITypeDescriptorContext context,
            CultureInfo culture,
            object value)
        {
            if (!(value is string valueAsString))
                return base.ConvertFrom(context, culture, value);

            var colorizerCollection = GetCollection(context);
            var itemSelected = colorizerCollection.Count == 0
                ? new RandomColorizer()
                : GetCollection(context)[0];

            foreach (IColorizer item in colorizerCollection)
            {
                var itemName = item.Name;
                if (itemName == valueAsString)
                    itemSelected = item;
            }

            return itemSelected;
        }
    }
}