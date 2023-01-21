using System;

namespace CodeBase.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData;
        public State HeroState;
        public Abilities Abilities;
        public KillData KillData;

        public PlayerProgress(string initialLevel, float maxHealth, float damage, float attackRadius)
        {
            WorldData = new WorldData(initialLevel);
            HeroState = new State(maxHealth);
            Abilities = new Abilities(damage, attackRadius);
            KillData = new KillData();
        }
    }

    [Serializable]
    public class Abilities
    {
        public float Damage;
        public float AttackRadius;

        public Abilities(float damage, float attackRadius)
        {
            Damage = damage;
            AttackRadius = attackRadius;
        }
    }
}