﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TagsCloudContainer.Visualizer
{
    public interface IVisualizerSettings
    {
        Size ImageSize { get; }
        Brush BackgroundBrush { get; }

        Font GetFont(WordRectangle wordRectangle);
        Brush GetBrush(WordRectangle wordRectangle);
    }
}
