using System;

namespace CodeBase.Data
{
    [Serializable]
    public class PositionOnLevel
    {
        public string LevelName;
        public Vector3Data Position;

        public PositionOnLevel(string initLevel)
        {
            LevelName = initLevel;
        }

        public PositionOnLevel(Vector3Data position, string levelName)
        {
            Position = position;
            LevelName = levelName;
        }
    }
}