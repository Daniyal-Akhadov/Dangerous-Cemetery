using System;

namespace CodeBase.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData;

        public State HeroState;

        public PlayerProgress(string initialLevel, float maxHealth)
        {
            WorldData = new WorldData(initialLevel);
            HeroState = new State(maxHealth);
        }
    }
}