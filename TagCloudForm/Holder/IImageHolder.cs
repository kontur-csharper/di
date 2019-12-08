﻿using System.Drawing;
using TagCloud.Visualization;

namespace TagCloudForm.Holder
{
    public interface IImageHolder
    {
        Graphics StartDrawing();
        void UpdateUi();
        void RecreateImage(ImageSettings settings);
        void SaveImage(string fileName);
    }
}