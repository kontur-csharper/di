﻿using System.Drawing;

namespace TagCloud.ColorPicker
{
    public interface IColorPicker
    {
        Color AdjustColor(Color baseColor, int usingFrequency);
    }
}