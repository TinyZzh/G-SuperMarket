using System;
using System.Collections.Generic;

namespace Okra.Tiled.AStar
{
    /// <summary>
    /// A*寻路算法. 由Okra框架中的A*寻路算法改成C#版本
    /// </summary>
    public class AStarAlgorithm
    {
        /// <summary>
        /// vertical and parallel path estimate cost
        /// </summary>
        private const int COST_STRAIGHT = 10;
        /// <summary>
        /// diagonal path estimate cost
        /// </summary>
        private const int COST_DIAGONAL = 14;

        /// <summary>
        /// A* search algorithm
        /// </summary>
        /// <param name="x1">start point x coordinate</param>
        /// <param name="y1">start point y coordinate</param>
        /// <param name="x2">end point x coordinate</param>
        /// <param name="y2">end point y coordinate</param>
        /// <param name="blocks">the tiled map blocks info.</param>
        /// <returns>Return the shortest path from start point to end point.otherwise return empty list.</returns>
        public static LinkedList<Point> Find(int x1, int y1, int x2, int y2, int[,] blocks)
        {
            return Find(new Node(x1, y1, null), new Node(x2, y2, null), blocks, true);
        }

        /// <summary>
        /// A* search algorithm
        /// </summary>
        /// <param name="x1">start point x coordinate</param>
        /// <param name="y1">start point y coordinate</param>
        /// <param name="x2">end point x coordinate</param>
        /// <param name="y2">end point y coordinate</param>
        /// <param name="blocks">the tiled map blocks info.</param>
        /// <param name="allowDiagonals">is allow diagonal.</param>
        /// <returns>Return the shortest path from start point to end point.otherwise return empty list.</returns>
        public static LinkedList<Point> Find(int x1, int y1, int x2, int y2, int[,] blocks, bool allowDiagonals)
        {
            if (blocks == null)
            {
                throw new NullReferenceException("blocks");
            }
            if (blocks[x1, y1] != 1 || blocks[x2, y2] != 1)
            {
                throw new Exception("Target point unreachable.");
            }
            if (IsOutOfBounds(x1, y1, blocks))
            {
                throw new IndexOutOfRangeException("sNode");
            }
            if (IsOutOfBounds(x2, y2, blocks))
            {
                throw new IndexOutOfRangeException("eNode");
            }
            return Find(new Node(x1, y1, null), new Node(x2, y2, null), blocks, allowDiagonals);
        }

        /// <summary>
        /// A* search algorithm
        /// </summary>
        /// <param name="sNode">start point x coordinate</param>
        /// <param name="eNode">start point y coordinate</param>
        /// <param name="blocks">the tiled map blocks info.</param>
        /// <param name="allowDiagonals">is allow diagonal.</param>
        /// <returns>Return the shortest path from start point to end point.otherwise return empty list.</returns>
        /// <example>
        ///     Dependency <see cref="Point"/> {@link Point#equals} method
        /// </example>
        private static LinkedList<Point> Find(Node sNode, Node eNode, int[,] blocks, bool allowDiagonals)
        {
            List<Node> opened = new List<Node>();
            HashSet<Node> cked = new HashSet<Node>();
            opened.Add(sNode);
            LinkedList<Point> paths = new LinkedList<Point>();
            while (opened.Count > 0)
            {
                Node minNode = opened[0];
                opened.RemoveAt(0);
                if (minNode.Equals(eNode) ||
                    cked.Contains(eNode) //  unreachable point
                    )
                {
                    while (minNode.Parent != null)
                    {
                        paths.AddFirst(new Point(minNode.X, minNode.Y));
                        minNode = minNode.Parent;
                    }
                    break;
                }
                List<Node> rounds = Rounds(minNode, blocks, allowDiagonals);
                foreach (var current in rounds)
                {
                    if (blocks[current.X, current.Y] != 1)
                    {
                        cked.Add(current);
                        continue;
                    }
                    else if (cked.Contains(current))
                    {
                        continue;
                    }
                    Node parent = current.Parent;
                    int cost = (parent.X == current.X || parent.Y == current.Y) ? COST_STRAIGHT : COST_DIAGONAL;
                    int index;
                    if ((index = opened.IndexOf(current)) != -1)
                    {
                        if ((parent.G + cost) < opened[index].G)
                        {
                            Gn(current, cost);
                            Fn(current);
                            opened.Insert(index, current);
                        }
                    }
                    else
                    {
                        Gn(current, cost);
                        Hn(current, eNode);
                        Fn(current);
                        opened.Add(current);
                    }
                }
                cked.Add(minNode);
                opened.Sort((o1, o2) => o1.F - o2.F);
            }
            return paths;
        }


        /**
         * @return Return all node which surround the center point.
         */
        private static List<Node> Rounds(Node center, int[,] blocks, bool allowDiagonals)
        {
            List<Node> result = new List<Node>();
            if (blocks == null || blocks.Length <= 0)
            {
                return result;
            }
            int centerX = center.X;
            int centerY = center.Y;
            if (allowDiagonals)
            { // 8 direction
                for (int i = centerX - 1; i <= centerX + 1; i++)
                {//    row
                    for (int j = centerY - 1; j <= centerY + 1; j++)
                    {//    column
                        if ((centerX == i && centerY == j)
                                || (i < 0 || i >= blocks.GetLength(0) || j < 0 || j >= blocks.GetLength(1)))
                        {
                            continue;
                        }
                        result.Add(new Node(i, j, center));
                    }
                }
            }
            else
            {
                int[,] points = new int[,]{
                    {centerX, centerY - 1},//  up
                    {centerX, centerY + 1},//  down
                    {centerX - 1, centerY},//  left
                    {centerX + 1, centerY},//  right
                };
                for (int i = 0; i < points.GetLength(0); i++)
                {
                    if (IsOutOfBounds(points[i, 0], points[i, 1], blocks))
                    {
                        continue;
                    }
                    result.Add(new Node(points[i, 0], points[i, 1], center));
                }
            }
            return result;
        }

        /**
         * Calculate G value.
         * g(n) is the cost of the path from the start node to n.
         *
         * @param node last node
         * @param cost the cost of one step.
         */
        private static void Gn(Node node, int cost)
        {
            node.G = (node.Parent == null ? 0 : node.Parent.G) + cost;
        }

        /**
         * Calculate manhattan distance(H).
         * <p>
         * h(n) is a heuristic that estimates the cost of the cheapest path from n to the goal.
         *
         * @param node  current node.
         * @param eNode the end node.
         */
        private static void Hn(Node node, Node eNode)
        {
            node.H = Math.Abs(node.X - eNode.X) + Math.Abs(node.Y - eNode.Y);
        }

        /**
         * Calculate F value.
         * f(n) = g(n) + h(n)
         */
        private static void Fn(Node node)
        {
            node.F = node.G + node.H;
        }

        /**
         * Is point's position (x,y) out of tiled map's bounds.
         *
         * @return Return true if position out of bounds. otherwise false.
         */
        private static bool IsOutOfBounds(int x, int y, int[,] blocks)
        {
            return (x < 0 || x >= blocks.GetLength(0) || y < 0 || y >= blocks.GetLength(1));
        }
    }
}

