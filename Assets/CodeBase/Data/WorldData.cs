using System;

namespace CodeBase.Data
{
    [Serializable]
    public class WorldData
    {
        public PositionOnLevel PositionOnLevel;
        private string _initLevel;

        public WorldData(string initLevel)
        {
            _initLevel = initLevel;
            PositionOnLevel = new PositionOnLevel(initLevel);
        }
    }
}