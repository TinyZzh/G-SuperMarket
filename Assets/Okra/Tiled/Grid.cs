

using Okra.Tiled.AStar;

namespace Okra.Tiled
{
    /// <summary>
    /// 地图格子
    /// </summary>
    public class Grid
    {
        /// <summary>
        /// 位置
        /// </summary>
        public Point Location { get; set; }
        /// <summary>
        /// 格子类型
        /// </summary>
        public GridType Type { get; set; }
        /// <summary>
        /// 格子开放等级
        /// </summary>
        public int Level { get; set; }

        public Grid(Point location, GridType type, int level)
        {
            Location = location;
            Type = type;
            Level = level;
        }

        /// <summary>
        /// 校验格子是否可以通过
        /// </summary>
        /// <returns>返回True表示可以通过，否则返回False</returns>
        public bool IsWalkable()
        {
            return Type == GridType.Normal;
        }
    }

    /// <summary>
    /// 格子类型
    /// </summary>
    public enum GridType
    {
        /// <summary>
        /// 墙 - 墙饰
        /// </summary>
        Wall = 0,
        /// <summary>
        /// 普通 - 行走, 货架
        /// </summary>
        Normal = 1,
        /// <summary>
        /// 地板 - 装饰
        /// </summary>
        Floor,
        /// <summary>
        /// 货架
        /// </summary>
        Shelf,
        /// <summary>
        /// 未解锁的
        /// </summary>
        Locked,
        /// <summary>
        /// 未知
        /// </summary>
        Unknown

    }
}
