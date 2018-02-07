using System;
using System.Collections.Generic;
using System.Drawing;
using NUnit.Framework;
using TagsCloudContainer.Architecture.Themes;

namespace TagsCloudContainer.Architecture
{
    public interface ICloudLayouter
    {
        Result<Rectangle> PutNextRectangle(Size rectangleSize);
    }
}