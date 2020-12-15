using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TagsCloudCreating.Contracts;

namespace TagsCloudVisualization.Infrastructure.ColorizerCollectionConvert
{
    public class ColorizerCollection : CollectionBase, ICustomTypeDescriptor
    {
        public ColorizerCollection(IEnumerable<IColorizer> colorizers)
        {
            foreach (var colorizer in colorizers)
                Add(colorizer);
        }

        private void Add(IColorizer item) => List.Add(item);

        public IColorizer this[int index] => (IColorizer) List[index];


        public AttributeCollection GetAttributes() => TypeDescriptor.GetAttributes(this, true);

        public string GetClassName() => TypeDescriptor.GetClassName(this, true);

        public string GetComponentName() => TypeDescriptor.GetComponentName(this, true);

        public TypeConverter GetConverter() => TypeDescriptor.GetConverter(this, true);

        public EventDescriptor GetDefaultEvent() => TypeDescriptor.GetDefaultEvent(this, true);

        public PropertyDescriptor GetDefaultProperty() => TypeDescriptor.GetDefaultProperty(this, true);

        public object GetEditor(Type editorBaseType) => TypeDescriptor.GetEditor(this, editorBaseType, true);

        public EventDescriptorCollection GetEvents() => TypeDescriptor.GetEvents(this, true);

        public EventDescriptorCollection GetEvents(Attribute[] attributes) =>
            TypeDescriptor.GetEvents(this, attributes, true);

        public PropertyDescriptorCollection GetProperties()
        {
            var pds = new PropertyDescriptorCollection(null);

            for (var i = 0; i < List.Count; i++) 
                pds.Add(new ColorizerCollectionPropertyDescriptor(this, i));

            return pds;
        }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes) => GetProperties();

        public object GetPropertyOwner(PropertyDescriptor pd) => this;
    }
}