using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Okra.Tiled;
using Okra.Tiled.AStar;

namespace Game.SuperMarket.Component
{
    /// <summary>
    /// 超市组件
    /// </summary>
    public class CoMarket : MonoBehaviour
    {
        /// <summary>
        /// 超市等级
        /// </summary>
        public int Level;
        /// <summary>
        /// 网格
        /// </summary>
        public Grid[,] Layout;
        /// <summary>
        /// 摆设
        /// </summary>
        public List<Furnish> Furnishes;
        /// <summary>
        /// 收银员 - 结算
        /// </summary>
        public object Cashier;
        /// <summary>
        /// 售货员 - 负责补货
        /// </summary>
        public object Salesman;
        /// <summary>
        /// 初始点 - 入口
        /// </summary>
        public Point GatePoint = new Point(19, 15);
        /// <summary>
        /// 结束点 - 出口
        /// </summary>
        public Point ExitPoint = new Point(13, 19);


        //  参数
        public Material Red;
        public Material Green;
        public Material Blue;
        public Material Purple;


        // Use this for initialization
        void Start()
        {
            Layout = new Grid[20, 20];      //  地图规格
            Furnishes = new List<Furnish>(); // 用户的摆设列表

            Initialize(20, 20, GatePoint, ExitPoint);

            Layout[GatePoint.X, ExitPoint.Y] = null;
            Layout[ExitPoint.X, ExitPoint.Y] = null;
            
            var path = LookupPath();

            
            var mUnitGo = Test.ShowNode(GatePoint, "coUnit", Red);
            var coUnit = mUnitGo.AddComponent<CoUnit>();
            coUnit.Path = path.ToArray();
            coUnit.Index = 0;
            coUnit.Unit = mUnitGo.transform;
            
           

            

            
            //var path = AStarAlgorithm.Find(5, 3, 6, 3, blocks, false);
            Debug.Log("路径：" + path.Count);

            //path.AddFirst(new Point(start.X, start.Y));
            //path.AddLast(new Point(end.X, end.Y));

            //Test.ShowPath(path, new[]
            //{
            //    Red, Green, Blue, Purple
            //});

        }

        public LinkedList<Point> LookupPath()
        {
            return PathUtil.Find(GatePoint.X, GatePoint.Y, ExitPoint.X, ExitPoint.Y, Layout, false);
        } 

        public void Initialize(int width, int height, Point start, Point end)
        {
            Initialize(width, height, start.X, start.Y, end.X, end.Y);
        }

        public void Initialize(int width, int height, int x1, int y1, int x2, int y2)
        {
            // TODO: 1.获取地图配置
            Layout = new Grid[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (i == x1 && j == y1)  // TODO: 2.入口
                    {
                        Layout[i, j] = new Grid(new Point(i, j), GridType.Normal, 0);
                        Test.ShowNode(new Point(x1, y1), "Start", Green);
                    }
                    else if (i == x2 && j == y2)  // TODO: 2.出口
                    {
                        Layout[i, j] = new Grid(new Point(i, j), GridType.Normal, 0);
                        Test.ShowNode(new Point(x2, y2), "End", Red);
                        Debug.Log("Red");
                    }
                    else if (i == 0 || j == 0 || i == width - 1 || j == height - 1)
                    {
                        Layout[i, j] = new Grid(new Point(i, j), GridType.Wall, 0);
                        Test.ShowNode(new Point(i, j), "Cube" + (i * width + j), Blue);
                    }
                    else
                    {
                        Layout[i, j] = null; //  new Grid(new Point(i, j), GridType.Normal, 0);
                    }
                }
            }

            // TODO: 3.摆设
            foreach (var furnish in this.Furnishes)
            {
                if (furnish is Shelf)
                {
                    var shelf = (Shelf)furnish;
                    foreach (var grid in shelf.Occupy)
                    {

                    }
                }
            }


        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
