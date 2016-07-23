using Game.SuperMarket.Service;

namespace Game.SuperMarket.Impl
{
    public class World
    {
        private static World _world = new World();

        private GoManager _goManager = new GoManager();

        public static World Instance()
        {
            return _world;
        }






    }
}