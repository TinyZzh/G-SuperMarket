using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Okra.Tiled;
using Okra.Tiled.AStar;

namespace Game.SuperMarket.Component
{
    /// <summary>
    /// 角色组件
    /// </summary>
    public class CoRole : MonoBehaviour
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public long Uid;
        /// <summary>
        /// 昵称
        /// </summary>
        public string Name;
        /// <summary>
        /// 形象
        /// </summary>
        public string Figure;
        /// <summary>
        /// 声望
        /// </summary>
        public int Prestige;


        // Use this for initialization
        void Start()
        {
           
        }


        // Update is called once per frame
        void Update()
        {

        }
    }
}
