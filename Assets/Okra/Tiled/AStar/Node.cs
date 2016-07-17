namespace Okra.Tiled.AStar
{
    /// <summary>
    /// A* algorithm Node.
    /// </summary>
    class Node : Point
    {
        /// <summary>
        /// Current node 's parent node.
        /// </summary>
        public Node Parent { get; set; }
        /// <summary>
        /// The cost of move from start point to current point.
        /// </summary>
        public int G { get; set; }
        /// <summary>
        /// The cost of move from current point to end point. h(n) = |x - endX| + |y - endY|
        /// 本算法中曼哈顿距离简化为(忽略障碍物). 效率高于计算两点间距离{@link Point#distance(double, double, double, double)}
        /// </summary>
        public int H { get; set; }
        /// <summary>
        /// f(n) = g(n) + h(n)
        /// </summary>
        public int F { get; set; }

        public Node(int x, int y, Node parent) : base(x, y)
        {
            this.Parent = parent;
        }
    }
}
