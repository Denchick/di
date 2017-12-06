using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Utils;

namespace TagsCloudContainer.Tests
{
    [TestFixture]
    public class TestVector
    {
        [TestCase(10, 4)]
        [TestCase(-10, -4)]
        [TestCase(0, 0)]
        public void CorrectConstructor_WhenTwoIntegersAsArguments(int x, int y)
        {
            var vector = new Vector(x, y);

            vector.X.Should().Be(x);
            vector.Y.Should().Be(y);
        }

        [TestCase(10, 4)]
        [TestCase(-10, -4)]
        [TestCase(0, 0)]
        public void CorrectConstructor_WhenPointAsArgument(int x, int y)
        {
            var p = new Point(x, y);
            var vector = new Vector(p);

            vector.X.Should().Be(p.X);
            vector.Y.Should().Be(p.Y);
        }

        [TestCase(1, 0, 0, 0, 1, 0, "+", TestName = "X-axis takes into account the coordinates of the 1st vector")]
        [TestCase(0, 1, 0, 0, 0, 1, "+", TestName = "Y-axis takes into account the coordinates of the 1st vector")]
        [TestCase(0, 0, 1, 0, 1, 0, "+", TestName = "X-axis takes into account the coordinates of the 2nd vector")]
        [TestCase(0, 0, 0, 1, 0, 1, "+", TestName = "Y-axis takes into account the coordinates of the 2nd vector")]
        [TestCase(1, 2, -1, -2, 0, 0, "+", TestName = "sum of opposite vectors is the zero vector")]
        [TestCase(1, 0, 0, 0, 1, 0, "-", TestName = "X-axis takes into account the coordinates of the 1st vector")]
        [TestCase(0, 1, 0, 0, 0, 1, "-", TestName = "Y-axis takes into account the coordinates of the 1st vector")]
        [TestCase(0, 0, 1, 0, -1, 0, "-", TestName = "X-axis takes into account the coordinates of the 2nd vector")]
        [TestCase(0, 0, 0, 1, 0, -1, "-", TestName = "Y-axis takes into account the coordinates of the 2nd vector")]
        [TestCase(1, 2, 1, 2, 0, 0, "-", TestName = "difference two equals vectors in zero vector")]
        public void CorrectOperation_BetweenTwoVectors(int x1, int y1, int x2, int y2,
            int expectedX, int expectedY, string operation)
        {
            var vector1 = new Vector(x1, y1);
            var vector2 = new Vector(x2, y2);

            var result = GetOperationResult(vector1, vector2, operation);

            result.X.Should().Be(expectedX);
            result.Y.Should().Be(expectedY);
        }

        [TestCase(2, 0, 2, 1, 0, "/", TestName = "the 1st coordinate is taken into account")]
        [TestCase(0, 2, 2, 0, 1, "/", TestName = "the 2nd coordinate is taken into account")]
        [TestCase(2, 2, 2, 1, 1, "/", TestName = "both coordinates is taken into account")]
        [TestCase(2, 2, 3, 0, 0, "/", TestName = "result is a non-integer division is rounded down")]
        public void CorrectOperation_BetweenVectorAndNumber(int x, int y, int number,
            int expectedX, int expectedY, string operation)
        {
            var vector = new Vector(x, y);

            var result = GetOperationResult(vector, number, operation);

            result.X.Should().Be(expectedX);
            result.Y.Should().Be(expectedY);
        }

        [TestCase(1, 0, 1, 0, TestName = "X-axis takes into account the vector's coordinates")]
        [TestCase(0, 1, 0, 1, TestName = "Y-axis takes into account the vector's coordinates")]
        [TestCase(1, 1, 1, 1, TestName = "both axis takes into account the vector's coordinates")]
        public void СorrectСonversionVectorToPoint(int x, int y, int expecredX, int expectedY)
        {
            var point = new Vector(x, y).ToPoint();

            point.X.Should().Be(expecredX);
            point.Y.Should().Be(expectedY);
        }

        private Vector GetOperationResult(object object1, object object2, string operation)
        {
            if (operation == "+") return (Vector)object1 + (Vector)object2;
            else if (operation == "-") return (Vector)object1 - (Vector)object2;
            else if (operation == "/") return (Vector)object1 / (int)object2;
            else throw new ArgumentException($"Unknown operation: {operation}");
        }
    }
}
