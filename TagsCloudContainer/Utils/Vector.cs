using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.Utils
{
    public class Vector
    {
        public int X;
        public int Y;

        public static Vector operator +(Vector element1, Vector element2)
        {
            return new Vector(element1.X + element2.X, element1.Y + element2.Y);
        }

        public static Vector operator -(Vector element1, Vector element2)
        {
            return new Vector(element1.X - element2.X, element1.Y - element2.Y);
        }

        public static Vector operator /(Vector vector, int number)
        {
            return new Vector(vector.X / number, vector.Y / number);
        }

        public Point ToPoint()
        {
            return new Point(X, Y);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Vector)) return false;
            var vector = obj as Vector;
            return X == vector.X && Y == vector.Y;
        }

        public Vector(Point point)
        {
            X = point.X;
            Y = point.Y;
        }

        public Vector(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
