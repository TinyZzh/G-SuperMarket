using System;

namespace Okra.Tiled.AStar
{
    public class Point
    {

        public static Point Zero = new Point(0, 0);

        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public double Distance(Point that)
        {
            return Distance(that.X, that.Y);
        }

        public double Distance(int x2, int y2)
        {
            return Distance(X, Y, x2, y2);
        }

        /**
         * This equals method is important for A* Algorithm. {@link AStarAlgorithm#find(Node, Node, int[][], boolean)}
         * <p>
         * {@link Point} are equal if the values of their x and y.
         *
         * @param obj object
         * @return Return true if the obj is equal this. otherwise false.
         */
        public override bool Equals(Object obj)
        {
            var point = obj as Point;
            if (point != null)
            {
                return point.X == this.X && point.Y == this.Y;
            }
            else
                return false;
        }

        public override int GetHashCode()
        {
            //        return ((x & 0xFFFF ) << 16) | (y & 0xFFFF); // mine implement
            long bits = BitConverter.DoubleToInt64Bits(X);
            bits ^= BitConverter.DoubleToInt64Bits(Y) * 31;
            return (((int)bits) ^ ((int)(bits >> 32)));
        }
        
        public override string ToString()
        {
            return GetType().Name + " - [" + X + ", " + Y + "]";
        }

        // Static Method
        
        public static double Distance(double x1, double y1, double x2, double y2)
        {
            x2 -= x1;
            y2 -= y1;
            return Math.Sqrt(x2 * x2 + y2 * y2);
        }
    }
}
