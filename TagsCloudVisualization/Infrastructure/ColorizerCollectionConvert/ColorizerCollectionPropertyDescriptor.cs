using System;
using System.ComponentModel;

namespace TagsCloudVisualization.Infrastructure.ColorizerCollectionConvert
{
    public class ColorizerCollectionPropertyDescriptor : PropertyDescriptor
    {
        private readonly ColorizerCollection collection;
        private readonly int index;

        public ColorizerCollectionPropertyDescriptor(ColorizerCollection collection, int id)
            : base("#" + id, null)
        {
            this.collection = collection;
            index = id;
        }

        public override AttributeCollection Attributes => new AttributeCollection(null);

        public override bool CanResetValue(object component) => true;

        public override Type ComponentType => collection.GetType();

        public override string DisplayName => collection[index].GetType().Name;

        public override string Description => $"{collection[index]}, {index + 1}";

        public override object GetValue(object component) => collection[index];

        public override bool IsReadOnly => false;

        public override string Name => "#" + index;

        public override Type PropertyType => collection[index].GetType();

        public override void ResetValue(object component) { }

        public override bool ShouldSerializeValue(object component) => true;

        public override void SetValue(object component, object value) { }
    }
}