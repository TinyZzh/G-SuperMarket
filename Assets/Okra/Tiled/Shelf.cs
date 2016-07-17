namespace Okra.Unit
{
    /// <summary>
    /// 货架
    /// </summary>
    public class Shelf : Furnish
    {

        public int CfgFurnishId;

        public int Id
        {
            get
            {
                return CfgFurnishId;
            }
            set { this.CfgFurnishId = value; }
        }

        public int Sell;

        public Grid[,] Occupy { get; set; }


    }
}