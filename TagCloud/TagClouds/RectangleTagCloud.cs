using System;
using System.Drawing;

namespace TagCloud.TagClouds
{
    public class RectangleTagCloud : TagCloud<Rectangle>
    {
        public override void AddElement(Rectangle element)
        {
            LeftUp.X = Math.Min(LeftUp.X, element.X);
            LeftUp.Y = Math.Min(LeftUp.Y, element.Y);
            RightDown.X = Math.Max(RightDown.X, element.X + element.Width);
            RightDown.Y = Math.Max(RightDown.Y, element.Y + element.Height);
            base.AddElement(element);
        }
    }
}
