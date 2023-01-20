using System;

namespace CodeBase.Data
{
    [Serializable]
    public class State
    {
        public float CurrentHealth;
        public float MaxHealth;

        public State(float maxHealth)
        {
            MaxHealth = maxHealth;
            ResetHealth();
        }

        public void ResetHealth()
        {
            CurrentHealth = MaxHealth;
        }
    }
}