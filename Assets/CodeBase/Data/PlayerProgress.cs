using System;

namespace CodeBase.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData;
        public PlayerState PlayerState;
        private string _initialeLevel;

        public PlayerProgress(string initLevel)
        {
            PlayerState = new PlayerState();
            _initialeLevel = initLevel;
            WorldData = new WorldData(initLevel);
        }
    }
}
