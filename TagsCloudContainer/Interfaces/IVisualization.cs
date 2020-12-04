﻿using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.TextProcessing;

namespace TagsCloudContainer.Interfaces
{
    public interface IVisualization
    {
        Image GetImageCloud(int cloudRadius, List<WordTag> tags);
    }
}