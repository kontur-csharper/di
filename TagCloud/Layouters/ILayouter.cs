using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TagCloud.Layouters
{
    public interface ILayouter
    {
        Rectangle PutNextRectangle(Size size);
    }
}
