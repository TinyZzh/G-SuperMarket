using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Game.SuperMarket
{
    /// <summary>
    /// 单位组件
    /// </summary>
    public class CoUnit : MonoBehaviour
    {

        public object player;

        public Dictionary<Int32, Int32> map = new Dictionary<Int32, Int32>();

        private Dictionary<Int32, Int32> bag = new Dictionary<Int32, Int32>();


        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Buy(int type, int cfgItemId, int count)
        {
            int need = map[type];


        }

    }
}

