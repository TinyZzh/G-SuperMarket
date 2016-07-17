using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Okra.Unit;
using Okra.Utilities;
using UnityEngine.UI;

namespace Game.SuperMarket
{
    /// <summary>
    /// 超市
    /// </summary>
    public class Market : MonoBehaviour
    {
        /// <summary>
        /// 超市等级
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// 网格
        /// </summary>
        public Grid[,] Layout { get; set; }
        /// <summary>
        /// 摆设
        /// </summary>
        public List<Furnish> Furnishes { get; set; }
        /// <summary>
        /// 收银员 - 结算
        /// </summary>
        public object Cashier { get; set; }
        /// <summary>
        /// 售货员 - 负责补货
        /// </summary>
        public object Salesman { get; set; }


        //  参数
        public Material Red;
        public Material Green;
        public Material Blue;
        public Material Purple;


        // Use this for initialization
        void Start()
        {



            Layout = new Grid[20, 20];
            for (int i = 0; i < Layout.GetLength(0); i++)
            {
                for (int j = 0; j < Layout.GetLength(1); j++)
                {
                    if (i == 0 || j == 0 || i == Layout.GetLength(0) - 1 || j == Layout.GetLength(1) - 1)
                    {
                        Layout[i, j] = new Grid(new Point(i, j), GridType.Wall, 0);
                        Test.ShowNode(new Point(i, j), Blue);
                    }
                    else
                    {
                        Layout[i, j] = null; //  new Grid(new Point(i, j), GridType.Normal, 0);
                    }
                }
            }

            Layout[19, 15] = null;
            Layout[13, 19] = null;

            var path = PathUtil.Find(19, 15, 13, 19, Layout, false);
            //var path = AStarAlgorithm.Find(5, 3, 6, 3, blocks, false);
            Debug.Log("路径：" + path.Count);
            path.AddFirst(new Point(5, 19));
            path.AddLast(new Point(0, 15));
            Test.ShowPath(path, new[]
            {
                Red,Green,Blue, Purple
            });




        }

        public void Initialize()
        {
            // TODO: 1.获取地图配置
            var width = 20;
            var height = 20;

            var x1 = 1;
            var y1 = 1;
            var x2 = 1;
            var y2 = 1;
            Layout = new Grid[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (i == 0 || j == 0 || i == width - 1 || j == height - 1)
                    {
                        Layout[i, j] = new Grid(new Point(i, j), GridType.Wall, 0);
                        Test.ShowNode(new Point(i, j), Blue);
                    }
                    else if (i == x1 && j == y1)
                    {
                        Layout[i, j] = new Grid(new Point(i, j), GridType.Normal, 0);
                    }
                    else if (i == x2 && j == y2)
                    {
                        Layout[i, j] = new Grid(new Point(i, j), GridType.Normal, 0);
                    }
                    else
                    {
                        Layout[i, j] = null; //  new Grid(new Point(i, j), GridType.Normal, 0);
                    }
                }
            }
            // TODO: 2.入口 + 出口

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
