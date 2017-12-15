using System.Collections.Generic;
using System.Drawing;
using NUnit.Framework;
using TagsCloudContainer.Architecture.Themes;

namespace TagsCloudContainer.Architecture
{
    public interface ICloudLayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}