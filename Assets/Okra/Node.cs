﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Okra.Utilities
{
    class Node : Point
    {
        /**
     * current node 's parent node.
     */
        public Node Parent { get; set; }
        /**
         * the cost of move from start point to current point.
         */
        public int G { get; set; }
        /**
         * the cost of move from current point to end point. h(n) = |x - endX| + |y - endY|
         * 本算法中曼哈顿距离简化为  (忽略障碍物). 效率高于计算两点间距离{@link Point#distance(double, double, double, double)}
         */
        public int H { get; set; }
        /**
         * f(n) = g(n) + h(n)
         */
        public int F { get; set; }

        public Node(int x, int y, Node parent) : base(x, y)
        {
            this.Parent = parent;
        }


    }
}
