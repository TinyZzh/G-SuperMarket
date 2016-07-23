using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Okra.Tiled.AStar;

namespace Game.SuperMarket.Component
{
    /// <summary>
    /// 单位组件
    /// </summary>
    public class CoUnit : MonoBehaviour
    {

        public Transform Unit;

        /// <summary>
        /// 上一次移动时间
        /// </summary>
        public long TimeLastMove;

        // pri

        /// <summary>
        ///  行走路径
        /// </summary>
        public Point[] Path { get; set; }
        /// <summary>
        /// 单位私有行进索引
        /// </summary>
        public int Index { get; set; }


        public object player;

        public Dictionary<Int32, Int32> map = new Dictionary<Int32, Int32>();

        private Dictionary<Int32, Int32> bag = new Dictionary<Int32, Int32>();


        // Use this for initialization
        void Start()
        {

        }

        void FixedUpdate()
        {
            var now = DateTime.Now.Ticks / 1000;
            if (now - TimeLastMove < 5000)
                return;
            TimeLastMove = now;
            if (Index >= Path.Length)
                return;
            var target = Path[Index];

            Unit.position = new Vector3(target.X, 0, target.Y);
            Index++;

        }

        // Update is called once per frame
        void Update()
        {

        }

        public static void Buy(int type, int cfgItemId, int count)
        {
            //int need = map[type];


        }

    }
}

