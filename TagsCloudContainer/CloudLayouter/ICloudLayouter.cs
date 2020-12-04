using System;
using System.Drawing;

namespace TagsCloudContainer.CloudLayouter
{
    public interface ICloudLayouter : IDisposable
    {
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}