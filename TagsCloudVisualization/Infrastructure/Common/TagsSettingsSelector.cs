using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using TagsCloudCreating.Contracts;
using TagsCloudVisualization.Infrastructure.ColorizerCollectionConvert;

namespace TagsCloudVisualization.Infrastructure.Common
{
    public class TagsSettingsSelector
    {
        [DisplayName("Tag's font")]
        [Description("Choose tag's font. ATTENTION: size will be minimum size for tags.")]
        public Font SelectedFont { get; set; }

        [DisplayName("Color algorithm")]
        [Description("Choose tag's painter")]
        [TypeConverter(typeof(ColorizerCollectionTypeConverter))]
        public IColorizer SelectedColorizer { get; set; }

        [Browsable(false)]
        [Description("All colorizers: technical detail, not shown.")]
        [TypeConverter(typeof(ColorizerCollectionConverter))]
        public ColorizerCollection ColorizerCollection { get; set; }

        public TagsSettingsSelector(IEnumerable<IColorizer> colorizers)
        {
            SelectedFont = SystemFonts.DefaultFont;
            ColorizerCollection = new ColorizerCollection(colorizers);
            SelectedColorizer = ColorizerCollection[0];
        }
    }
}